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
            await _unitOfWork.WindowType.AddAsync(windowTypeEntity);
            await _unitOfWork.SaveAsync();

            return NoContent();
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
            await _unitOfWork.WindowType.UpdateAsync(windowTypeEntity);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWindowType(int id)
        {
            var windowType = await _unitOfWork.WindowType.GetFirstOrDefaultAsync(x => x.Id == id);
            if(windowType == null)
            {
                return NotFound();
            }
            _unitOfWork.WindowType.Remove(windowType);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }
    }
}
