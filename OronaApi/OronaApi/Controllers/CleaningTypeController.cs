using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OronaApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CleaningTypeController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public CleaningTypeController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCleaningTypes()
        {
            IEnumerable<CleaningType> cleaningTypes = await _unitOfWork.CleaningType.GetAllAsync();
            var cleaningTypesResult = _mapper.Map<IEnumerable<CleaningTypeDto>>(cleaningTypes);

            return Ok(cleaningTypesResult);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "CUSTOM")]
        public async Task<IActionResult> GetCleaningTypeById(int id)
        {
            var cleaningType = await _unitOfWork.CleaningType.GetFirstOrDefaultAsync(x => x.Id == id);
            if (cleaningType == null)
            {
                return NotFound();
            }
            else
            {
                var cleaningTypeResult = _mapper.Map<CleaningTypeDto>(cleaningType);
                return Ok(cleaningTypeResult);
            }
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateCleaningType([FromBody] CleaningTypeCreateDto cleaningTypeCreateDto)
        {
            if (cleaningTypeCreateDto == null)
            {
                return BadRequest("CleaningType object is null");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model object");
            }

            var cleaningTypeEntity = _mapper.Map<CleaningType>(cleaningTypeCreateDto);

            var checkIfCleaningExists = await _unitOfWork.CleaningType.CleaningTypeExistAsync(cleaningTypeEntity);

            if (checkIfCleaningExists == null)
            {
                await _unitOfWork.CleaningType.AddAsync(cleaningTypeEntity);
                await _unitOfWork.SaveAsync();
                return NoContent();
            }

            return BadRequest("This cleaning type already exists.");
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateCleaningType(int id, [FromBody] CleaningTypeUpdateDto cleaningTypeUpdateDto)
        {
            if (cleaningTypeUpdateDto == null)
            {
                return BadRequest("CleanignType object is null");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model object");
            }
            var cleaningTypeEntity = await _unitOfWork.CleaningType.GetFirstOrDefaultAsync(i => i.Id == id);
            if (cleaningTypeEntity == null)
            {
                return NotFound($"Cleaning Type with id: {id} has not been found in db.");
            }

            _mapper.Map(cleaningTypeUpdateDto, cleaningTypeEntity);

            var checkIfCleaningExists = await _unitOfWork.CleaningType.CleaningTypeExistAsync(cleaningTypeEntity);

            if (checkIfCleaningExists == null)
            {
                await _unitOfWork.CleaningType.UpdateAsync(cleaningTypeEntity);
                await _unitOfWork.SaveAsync();
                return NoContent();
            }

            return BadRequest("This cleaning type already exists.");
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteCleaningType(int id)
        {
            var cleaningType = await _unitOfWork.CleaningType.GetFirstOrDefaultAsync(x => x.Id == id);
            if (cleaningType == null)
            {
                return NotFound();
            }

            _unitOfWork.CleaningType.Remove(cleaningType);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }
    }
}
