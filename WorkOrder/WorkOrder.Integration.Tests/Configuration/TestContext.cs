using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using WorkOrder.Api;

namespace WorkOrder.Integration.Tests.Configuration
{
    public class TestContext
    {
        public HttpClient HttpClient { get; set; }

        private TestServer _testServer;

        public TestContext()
        {
            SetupCliente();
        }

        private void SetupCliente()
        {
            _testServer = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            HttpClient = _testServer.CreateClient();
        }
    }
}
