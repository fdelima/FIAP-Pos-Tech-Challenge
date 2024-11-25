using Microsoft.EntityFrameworkCore;

namespace TestProject.IntegrationTest.Infra
{
    public class SqlServerTestFixture : IDisposable
    {
        string Port = "1430";
        const string pwd = "SqlServer2019!";
        const string network = "network-pedido-test";

        //sqlserver
        private const string ImageName = "mcr.microsoft.com/mssql/server:2019-latest";
        private string DatabaseContainerName = "sqlserver-db-pedido-test";
        private const string DataBaseName = "tech-challenge-micro-servico-pedido-grupo-71";

        //mssql-tools
        private const string ImageNameMssqlTools = "fdelima/fiap-pos-techchallenge-micro-servico-pedido-gurpo-71-scripts-database:fase4-test";
        private const string DatabaseContainerNameMssqlTools = "mssql-tools-pedido-test";

        public SqlServerTestFixture(string databaseContainerName = "sqlserver-db-pedido-test", string port = "1430")
        {
            if (DockerManager.UseDocker())
            {
                if (!DockerManager.ContainerIsRunning(DatabaseContainerName))
                {
                    DatabaseContainerName = databaseContainerName;
                    Port = port;

                    DockerManager.PullImageIfDoesNotExists(ImageName);
                    DockerManager.KillContainer(DatabaseContainerName);
                    DockerManager.KillVolume(DatabaseContainerName);

                    DockerManager.CreateNetWork(network);

                    DockerManager.RunContainerIfIsNotRunning(DatabaseContainerName,
                        $"run --name {DatabaseContainerName} " +
                        $"-e ACCEPT_EULA=Y " +
                        $"-e MSSQL_SA_PASSWORD={pwd} " +
                        $"-e MSSQL_PID=Developer " +
                        $"-p {Port}:1433 " +
                        $"--network {network} " +
                        $"-d {ImageName}");

                    Thread.Sleep(1000);

                    DockerManager.PullImageIfDoesNotExists(ImageNameMssqlTools);
                    DockerManager.KillContainer(DatabaseContainerNameMssqlTools);
                    DockerManager.KillVolume(DatabaseContainerNameMssqlTools);
                    DockerManager.RunContainerIfIsNotRunning(DatabaseContainerNameMssqlTools,
                        $"run --name {DatabaseContainerNameMssqlTools} " +
                        $"--network {network} " +
                        $"-d {ImageNameMssqlTools}");

                    while (DockerManager.ContainerIsRunning(DatabaseContainerNameMssqlTools))
                    {
                        Thread.Sleep(1000);
                    }
                }
            }
        }

        public FIAP.Pos.Tech.Challenge.Micro.Servico.Pedido.Infra.Context GetDbContext()
        {
            string ConnectionString = $"Server=localhost,{Port}; Database={DataBaseName}; User ID=sa; Password={pwd}; MultipleActiveResultSets=true; TrustServerCertificate=True";

            var options = new DbContextOptionsBuilder<FIAP.Pos.Tech.Challenge.Micro.Servico.Pedido.Infra.Context>()
                                .UseSqlServer(ConnectionString).Options;

            return new FIAP.Pos.Tech.Challenge.Micro.Servico.Pedido.Infra.Context(options);
        }

        public void Dispose()
        {
            if (DockerManager.UseDocker())
            {
                DockerManager.KillContainer(DatabaseContainerName);
                DockerManager.KillVolume(DatabaseContainerName);
            }
            GC.SuppressFinalize(this);
        }
    }
}
