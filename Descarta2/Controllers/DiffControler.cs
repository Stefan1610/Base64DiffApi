using Descarta2.Models;
using Descarta2.Models.DiffResult;
using Descarta2.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Descarta2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiffControler : ControllerBase
    {
        private readonly DiffService _service;

        public DiffControler(DiffService JsonDiffService)
        {
            _service = JsonDiffService;
        }

        /*      
         Inputing into the left "base", if id or input are null or empty
       returns bad request, else if everything is okay returns 201, Created
       Triggers service method Save, to store the item.
         */
        [HttpPut]
        [Route("{id}/left")]
        public async Task<IActionResult> Left(int id, [FromBody] DiffDataRequest jsonData)
        {
            if (string.IsNullOrEmpty(jsonData.data))
                return StatusCode(400);

            DiffItemDTO item = new DiffItemDTO() { Id = id, Position = diffPosition.Left, Data = jsonData.data };

            try
            {
                bool success = await _service.Save(item);

                if (success)
                    return StatusCode(201);
                else
                    return StatusCode(400);
            }
            catch (Exception)
            {
                
                return StatusCode(400);
            }

        }

       /*
        Inputing into the right "base", if id or input are null or empty
       returns bad request, else if everything is okay returns 201, Created
       Triggers service method Save, to store the item.

        */
		[HttpPut]
        [Route("{id}/right")]
        public async Task<IActionResult> Right(int id, [FromBody] DiffDataRequest jsonData)
        {
            if (string.IsNullOrEmpty(jsonData.data))
                return StatusCode(400);

            DiffItemDTO item = new DiffItemDTO() { Id = id, Position = diffPosition.Right, Data = jsonData.data };

            try
            {
                bool success = await _service.Save(item);

                if (success)
                    return StatusCode(201);
                else
                    return StatusCode(400);
            }
            catch (Exception)
            {
                
                return StatusCode(400);
            }

        }

        /* 
         If id == null returns 404 not found
        Recieves id, and triggers the service method to compare with recieved id
        if id exist return both status 200 and result
        if something is not working returns code 404 and message
         */
		[HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DiffCheck(int id)
        {
            if (id == null)
            {
                return StatusCode(404);
            }

            
                JsonDiffDTO diffResult = await _service.Compare(id);
            if(diffResult.Diffs == null)
            {
                return StatusCode(200, new WithoutDiff {DiffResultType = diffResult.DiffResultType});
            }
                return StatusCode(200,diffResult);
            
            
          
        }
    }
}
