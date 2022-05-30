using Descarta2.Context;
using Descarta2.Models;
using Descarta2.Repository;
using Descarta2.Service;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Descarta2.IntegrationTests
{
    [TestClass]
    public class UnitTest1
    {
        public UnitTest1()
        {

        }

        [Fact(DisplayName = "Check if return the same as expected")]
        public void ShouldBeTheSame()
        {
            DiffContext context = GetContextWithData();
            DiffRepository _repository = new DiffRepository(context);
            DiffService _service = new DiffService(_repository);

            Task<JsonDiffDTO> returnDTO = _service.Compare(1);

            Assert.IsTrue(returnDTO.Result.DiffResultType == "Equal");
        }

        private DiffContext GetContextWithData()
        {
            var options = new DbContextOptionsBuilder<DiffContext>()
                              .UseInMemoryDatabase(Guid.NewGuid().ToString())
                              .Options;

            var context = new DiffContext(options);

            var jsonBase64ItemLEFT = new DiffItem { Id = 1, Position = "L", Data = "YXNkZmFzZGZhc2RmYXNkZmFzZGY=" };
            var jsonBase64ItemRIGHT = new DiffItem { Id = 1, Position = "R", Data = "YXNkZmFzZGZhc2RmYXNkZmFzZGY=" };

            context.DiffItems.Add(jsonBase64ItemLEFT);
            context.DiffItems.Add(jsonBase64ItemRIGHT);

            context.SaveChanges();

            return context;
        }
    }
}
