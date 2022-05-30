using Descarta2.Service;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NPOI.SS.Formula.Functions;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
        }
        [Fact]
        public async Task PassingTest()
        {
            var application = new WebApplicationFactory<Program>()
       .WithWebHostBuilder(builder =>
       {
           builder.ConfigureServices(services =>
           {
              services.AddScoped<DiffService>();
           });
       });
            var client = application.CreateClient();

            var response = await client.GetStringAsync("/");

            Assert.Equals("Test Hello", response);
        }
        
    }
}