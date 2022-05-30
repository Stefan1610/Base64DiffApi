using Microsoft.AspNetCore.TestHost;
using NUnit.Framework.Internal;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Xunit.Sdk;

namespace UnitTests
{
    public class UnitTest1
    {

        [Fact]
        public async Task PassingTest()
        {
            var webHostBuilder = CreateWebHostBuilder();
            var server = new TestServer(webHostBuilder);

            using (var client = server.CreateClient())
            {
                var requestMessage = new HttpRequestMessage(new HttpMethod("GET"), "/api/values/");
                var responseMessage = await client.SendAsync(requestMessage);

                var content = await responseMessage.Content.ReadAsStringAsync();

                Assert.Equal(content, "Hello World!");
            }
        }
    }
}