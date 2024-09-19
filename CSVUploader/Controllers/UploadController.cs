using CSVUploader.Services;
using Microsoft.AspNetCore.Mvc;

namespace CSVUploader.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UploadController : ControllerBase
    {

        private readonly CsvService _csvService;

        public UploadController(CsvService csvService)
        {
            _csvService = csvService;
        }


        [HttpPost]
        public IActionResult UploadFile(IFormFile file)
        {
            return Ok(_csvService.Convert(file));
        }
    }
}
