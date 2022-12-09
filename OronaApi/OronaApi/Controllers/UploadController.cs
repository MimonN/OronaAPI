using Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace OronaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;

        public UploadController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost("{id?}"), DisableRequestSizeLimit]
        public async Task<IActionResult> Upload(int? id)
        {
            if(id != null)
            {
                var windowTypeFromDb = await _unitOfWork.WindowType.GetFirstOrDefaultAsync(x => x.Id == id); 
                if(windowTypeFromDb == null) 
                {
                    return NotFound();
                }
                var oldImage = windowTypeFromDb.ImageUrl;
                if (System.IO.File.Exists(oldImage))
                {
                    System.IO.File.Delete(oldImage);
                }
            }

            var formCollection = await Request.ReadFormAsync();
            var file = formCollection.Files.First();
            var folderName = Path.Combine("Resourses", "Images");
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            if (file.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString();
                var extension = Path.GetExtension(formCollection.Files.First().FileName);
                var fullPath = Path.Combine(pathToSave, fileName + extension);
                var dbPath = Path.Combine(folderName, fileName + extension);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                return Ok(new { dbPath });
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
