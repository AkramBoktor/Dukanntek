using System.Net.Http;
using Dukkantek.Service.Helpers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Dukkantek.Domain.Interfaces.Services;
using Dukkantek.Domain.ViewModels.Products;

namespace Dukkantek.Api.Controllers.Patient
{
    [ApiController]
    public class PatientProfileController : ControllerBase
    {
        private readonly IProductService _productService;

        public PatientProfileController(IProductService productService)
        {
            _productService = productService;
        }

   
        [HttpGet("api/products")]
        public async Task<IActionResult> GetAllPatientProfiles()
        {
            var generalResponse = await _productService.GetAllProducts();
            HttpResponseMessage response = new HttpResponseMessage();
            if (generalResponse.IsSucceeded)
            {
                response.StatusCode = System.Net.HttpStatusCode.OK;
            }
            else
            {
                response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                generalResponse.Data = null;
            }
            string serializeData = Serialization.SerializeToJson(generalResponse);

            response.Content = new StringContent(
                 serializeData,
                 Encoding.UTF8,
                 "application/json"
                 );
            return Ok(response);
        }

    
      
        [HttpGet("api/product/{id}")]
        public async Task<IActionResult> GetProductProfileById(int id)
        {
            var generalResponse = await _productService.GetProductById(id);
            HttpResponseMessage response = new HttpResponseMessage();
            if (generalResponse.IsSucceeded)
            {
                response.StatusCode = System.Net.HttpStatusCode.OK;
            }
            else
            {
                response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                generalResponse.Data = null;
            }
            string serializeData = Serialization.SerializeToJson(generalResponse);

            response.Content = new StringContent(
                 serializeData,
                 Encoding.UTF8,
                 "application/json"
                 );
            return  Ok(response);
        }

  
        [HttpPost("api/product/add")]
        public async Task<IActionResult> AddProduct([FromBody] AddProductsRequestVm addProductViewModel)
        {
            var generalResponse = await _productService.AddProduct(addProductViewModel);
            HttpResponseMessage response = new HttpResponseMessage();
            if (generalResponse.IsSucceeded)
            {
                response.StatusCode = System.Net.HttpStatusCode.OK;
            }
            else
            {
                response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                generalResponse.Data = null;
            }
            string serializeData = Serialization.SerializeToJson(generalResponse);

            response.Content = new StringContent(
                serializeData,
                Encoding.UTF8,
                "application/json"
            );
            return Ok(response);
        }

      
        [HttpDelete("api/product/delete/{id}")]
        public async Task<IActionResult> DeletePatientProfileById(int id)
        {
            var generalResponse = await _productService.DeleteProductById(id);
            HttpResponseMessage response = new HttpResponseMessage();
            if (generalResponse.IsSucceeded)
            {
                response.StatusCode = System.Net.HttpStatusCode.OK;
            }
            else
            {
                response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                generalResponse.Data = null;
            }
            string serializeData = Serialization.SerializeToJson(generalResponse);

            response.Content = new StringContent(
                 serializeData,
                 Encoding.UTF8,
                 "application/json"
                 );
            return Ok(response);
        }


       


    }
}
