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
    public class ProductController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public ProductController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            IEnumerable<Product> products = await _unitOfWork.Product.GetAllAsync(includeProperties: "CleaningType,WindowType");
            var productsResult = _mapper.Map<IEnumerable<ProductDto>>(products);
            return Ok(productsResult);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _unitOfWork.Product.GetFirstOrDefaultAsync(x => x.Id == id, includeProperties: "CleaningType,WindowType");
            if (product == null)
            {
                return NotFound();
            }
            else
            {
                var productResult = _mapper.Map<ProductDto>(product);
                return Ok(productResult);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductCreateDto productCreateDto)
        {
            if (productCreateDto == null)
            {
                return BadRequest("Product object is null");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model object");
            }

            var productEntity = _mapper.Map<Product>(productCreateDto);
            await _unitOfWork.Product.AddAsync(productEntity);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductUpdateDto productUpdateDto)
        {
            if (productUpdateDto == null)
            {
                return BadRequest("Product object is null");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model object");
            }
            var productEntity = await _unitOfWork.Product.GetFirstOrDefaultAsync(i => i.Id == id);
            if (productEntity == null)
            {
                return NotFound($"Product with id: {id} has not been found in db.");
            }

            _mapper.Map(productUpdateDto, productEntity);
            await _unitOfWork.Product.UpdateAsync(productEntity);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _unitOfWork.Product.GetFirstOrDefaultAsync(x => x.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            _unitOfWork.Product.Remove(product);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }
    }
}
