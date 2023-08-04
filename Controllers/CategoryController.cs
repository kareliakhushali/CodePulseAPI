using CodePulseAPI.Data;
using CodePulseAPI.Models.Domain;
using CodePulseAPI.Models.DTO;
using CodePulseAPI.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodePulseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository categoryRepository;
       /* private readonly ApplicationDbContext dbContext;
        public CategoryController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }*/
       public CategoryController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;

        }
        [HttpPost]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> CreateCategory( [FromBody] CreateCategoryRequestDto request)
        {
            // map DTO to Domain Model
            var category = new Category
            {
                Name = request.Name,
                UrlHandle = request.UrlHandle
            };

            /* await dbContext.Categories.AddAsync(category);
             await dbContext.SaveChangesAsync();*/
            await categoryRepository.CreateAsync(category); 

            //Domain Model  to DTO

            var response = new CategoryDto()
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle

            };
            return Ok(response);
        }
        // GET : https://localhost:44369/api/Category
        [HttpGet]
       
        public async Task<IActionResult> GetAllCategories()
        {
           var categories = await categoryRepository.GetAllAsync();
            //Map domain model to DTO
            var response = new List<CategoryDto>(); 
            foreach(var category in categories)
            {
                response.Add(new CategoryDto
                {
                    Id =  category.Id,  
                    Name = category.Name,
                    UrlHandle = category.UrlHandle

                });
            }
            return Ok(response);

        }
        // GET : https://localhost:44369/api/Category/id
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task <IActionResult> GetCategoryById([FromRoute] Guid id)
        {
            var existingCategory = await categoryRepository.GetById(id);
            if(existingCategory is null)
            {
                return NotFound();
            }
            var response = new CategoryDto
            {
                Id = existingCategory.Id,
                Name = existingCategory.Name,
                UrlHandle = existingCategory.UrlHandle
            };
            return Ok(response);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> EditCategory([FromRoute] Guid id,UpdateCategoryRequestDto request)
        {
            //convert dto to domain model
            var category = new Category
            {
                Id = id,
                Name = request.Name,
                UrlHandle = request.UrlHandle
            };
           category =  await categoryRepository.UpdateAsync(category);  
            if(category == null)
            {
                return NotFound();
            }
            //convert Domain Model to DTO
            var response = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle
            };
            return Ok(response);

        }
        //DELETE :https://localhost:44369/api/Category/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> DeleteCategory([FromRoute] Guid id)
        {
            var category = await categoryRepository.DeleteAsync(id);
            if(category is null)
            {
                return NotFound();
            }
            //convert Domain Model to DTO
            var response = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle
            };
            return Ok(response);

        }




    }

}
