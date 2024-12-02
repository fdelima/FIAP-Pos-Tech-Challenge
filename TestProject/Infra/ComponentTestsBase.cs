namespace TestProject.Infra
{
    public class ComponentTestsBase : IDisposable
    {
        private readonly SqlServerTestFixture _sqlserverTest;
        internal readonly ApiTestFixture _apiTest;
        private static int _tests = 0;

        public ComponentTestsBase()
        {
            _tests += 1;
            _sqlserverTest = new SqlServerTestFixture(
                imageNameMssqlTools: "fdelima/fiap-pos-techchallenge-micro-servico-pedido-gurpo-71-scripts-database:fase4-component-test",
                containerNameMssqlTools: "mssql-tools-pedido-component-test",
                databaseContainerName: "sqlserver-db-pedido-component-test", port: "1428");
            _apiTest = new ApiTestFixture();
        }

        public void Dispose()
        {
            _tests -= 1;
            if (_tests == 0)
            {
                _sqlserverTest.Dispose();
                _apiTest.Dispose();
            }
        }
    }
}
