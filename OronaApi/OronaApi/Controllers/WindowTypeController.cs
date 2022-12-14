using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OronaApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WindowTypeController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public WindowTypeController(IUnitOfWork unitOfWork, IMapper mapper) 
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWindowTypes()
        {
            var windowTypes = await _unitOfWork.WindowType.GetAllAsync();
            var windowTypesResult = _mapper.Map<IEnumerable<WindowTypeDto>>(windowTypes);
            return Ok(windowTypesResult);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWindowTypesWithProducts()
        {
            var windowTypes = await _unitOfWork.WindowType.GetAllAsync(includeProperties: "Products");
            var windowTypesResult = _mapper.Map<IEnumerable<WindowTypeWithProductsDto>>(windowTypes);
            return Ok(windowTypesResult);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWindowTypesWithProductsAndCleaningTypes()
        {
            var windowTypes = await _unitOfWork.WindowType.GetAllWindowTypesWithProductsWithCleaningTypes();
            var windowTypesResult = _mapper.Map<IEnumerable<WindowTypeWithProductsAndCleaningTypesDto>>(windowTypes);
            return Ok(windowTypesResult);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetWindowTypeById(int id)
        {
            var windowType = await _unitOfWork.WindowType.GetFirstOrDefaultAsync(x => x.Id == id);
            if (windowType == null)
            {
                return NotFound();
            }
            else
            {
                var windowTypeResult = _mapper.Map<WindowTypeDto>(windowType);
                return Ok(windowTypeResult);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateWindowType([FromBody] WindowTypeCreateDto windowTypeCreateDto)
        {
            if(windowTypeCreateDto == null)
            {
                return BadRequest("Window object is null");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model object");
            }

            var windowTypeEntity = _mapper.Map<WindowType>(windowTypeCreateDto);
            var checkIfWindowTypeExists = await _unitOfWork.WindowType.WindowTypeExistAsync(windowTypeEntity);

            if (checkIfWindowTypeExists == null)
            {
                await _unitOfWork.WindowType.AddAsync(windowTypeEntity);
                await _unitOfWork.SaveAsync();
                return NoContent();
            }

            return BadRequest("This window type already exists.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWindowType(int id, [FromBody]WindowTypeUpdateDto windowTypeUpdateDto)
        {
            if(windowTypeUpdateDto == null)
            {
                return BadRequest("Window object is null");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model object");
            }
            var windowTypeEntity = await _unitOfWork.WindowType.GetFirstOrDefaultAsync(i => i.Id == id);
            if(windowTypeEntity == null)
            {
                return NotFound($"Window Type with id: {id} has not been found in db.");
            }

            _mapper.Map(windowTypeUpdateDto, windowTypeEntity);

            var checkIfWindowTypeExists = await _unitOfWork.WindowType.WindowTypeExistAsync(windowTypeEntity);

            if (checkIfWindowTypeExists == null)
            {
                await _unitOfWork.WindowType.UpdateAsync(windowTypeEntity);
                await _unitOfWork.SaveAsync();
                return NoContent();
            }

            return BadRequest("This window type already exists.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWindowType(int id)
        {
            var windowType = await _unitOfWork.WindowType.GetFirstOrDefaultAsync(x => x.Id == id);
            if(windowType == null)
            {
                return NotFound();
            }

            var oldImage = windowType.ImageUrl;
            if (System.IO.File.Exists(oldImage))
            {
                System.IO.File.Delete(oldImage);
            }

            _unitOfWork.WindowType.Remove(windowType);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }
    }
}
