using Microsoft.EntityFrameworkCore;

namespace TestProject.Infra
{
    public class IntegrationTestsBase : IDisposable
    {
        internal readonly SqlServerTestFixture _sqlserverTest;
        private static int _tests = 0;

        public IntegrationTestsBase()
        {
            _tests += 1;
            _sqlserverTest = new SqlServerTestFixture(
                imageNameMssqlTools: "fdelima/fiap-pos-techchallenge-micro-servico-pedido-gurpo-71-scripts-database:fase4-test",
                containerNameMssqlTools: "mssql-tools-pedido-test",
                databaseContainerName: "sqlserver-db-pedido-test", port: "1430");
        }

        public void Dispose()
        {
            _tests -= 1;
            if (_tests == 0)
            {
                _sqlserverTest.Dispose();
            }
        }
    }
}
