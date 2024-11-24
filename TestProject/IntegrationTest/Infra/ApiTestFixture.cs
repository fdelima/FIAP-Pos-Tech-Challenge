namespace TestProject.IntegrationTest.Infra
{
    public class ApiTestFixture : IDisposable
    {
        const string port = "5000";
        const string network = "network-pedido-test";

        //api
        private const string ImageName = "fiap-pos-tech-challenge-micro-servico-pedido-gurpo-71-api:fase4";
        private const string DatabaseContainerName = "api-pedido-test";
        private const string DataBaseName = "tech-challenge-micro-servico-pedido-grupo-71";

        public ApiTestFixture()
        {
            if (DockerManager.UseDocker())
            {
                if (!DockerManager.ContainerIsRunning(DatabaseContainerName))
                {
                    DockerManager.PullImageIfDoesNotExists(ImageName);
                    DockerManager.KillContainer(DatabaseContainerName);
                    DockerManager.KillVolume(DatabaseContainerName);

                    DockerManager.CreateNetWork(network);

                    DockerManager.RunContainerIfIsNotRunning(DatabaseContainerName,
                        $"run --name {DatabaseContainerName} " +
                        $"-e ASPNETCORE_ENVIRONMENT=Test " +
                        $"-p {port}:8080 " +
                        $"--network {network} " +
                        $"-d {ImageName}");

                    Thread.Sleep(3000);
                }
            }
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
