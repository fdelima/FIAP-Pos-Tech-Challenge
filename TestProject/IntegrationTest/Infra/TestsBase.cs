using Microsoft.EntityFrameworkCore;

namespace TestProject.IntegrationTest.Infra
{
    public class TestsBase : IDisposable
    {
        protected readonly DbContextOptions<FIAP.Pos.Tech.Challenge.Micro.Servico.Pedido.Infra.Context> _options;
        internal readonly SqlServerTestFixture _sqlserverTest;
        internal readonly ApiTestFixture _apiTest;

        public TestsBase()
        {
            // Do "global" initialization here; Called before every test method.
            _sqlserverTest = new SqlServerTestFixture();
            _apiTest = new ApiTestFixture();
        }

        public void Dispose()
        {
            // Do "global" teardown here; Called after every test method.
            _sqlserverTest.Dispose();
            _apiTest.Dispose();
        }
    }
}
