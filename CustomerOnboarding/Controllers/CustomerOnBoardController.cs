using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnBoarding.Core.DTO;
using OnBoarding.Core.Responses;
using OnBoarding.Service.Interface;
using Swashbuckle.AspNetCore.Annotations;

namespace CustomerOnboarding.Controllers
{
    [Route("api/[controller]/OnBoard")]
    [ApiController]
    public class CustomerOnBoardController : ControllerBase
    {
        private ICustomerOnBoardService _onBoardService;
        public CustomerOnBoardController(ICustomerOnBoardService onBoardService)
        {
            _onBoardService = onBoardService;
        }
        [HttpPost, Route("OnboardCustomer")]
        [SwaggerOperation(Summary = "Onboards a New Customer", Description = "Endpoint to Create and Onboard New Customers")]
        [SwaggerResponse(200, "Customer Successfully Onboarded", typeof(ResponseModel))]
        public async Task<IActionResult> OnboardCustomer(CustomerDTO customerDto)
        {
            try
            {
                var onBoardCustomer = await _onBoardService.OnboardCustomer(customerDto);
                if (onBoardCustomer.State == 0)
                {
                    return NotFound(onBoardCustomer);
                }
                return Ok(onBoardCustomer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex?.InnerException?.InnerException?.Message ?? ex?.InnerException?.Message ?? ex?.Message);


            }
        }

        [HttpPost, Route("ValidateCustomer")]
        [SwaggerOperation(Summary = "Verify Onboarded Customer", Description = "Endpoint to Verify OnBoarded Customer")]
        [SwaggerResponse(200, "Customer Successfully Verified", typeof(ResponseModel))]
        public async Task<IActionResult> ValidateCustomer(string phoneNumber, string otp)
        {
            try
            {
                var onBoardCustomer = await _onBoardService.ValidateCustomer(phoneNumber,otp);
                if (onBoardCustomer.State == 0)
                {
                    return NotFound(onBoardCustomer);
                }
                return Ok(onBoardCustomer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex?.InnerException?.InnerException?.Message ?? ex?.InnerException?.Message ?? ex?.Message);


            }
        }
        [HttpGet, Route("GetExistingCustomers")]
        [SwaggerOperation(Summary = "Get Existing Customers", Description = "Endpoint to get all Onboarded Customers")]
        [SwaggerResponse(200, "Onboarded Customers Successfully Retrieved", typeof(OnboardedCustomersDto))]
        public async Task<IActionResult> GetExistingCustomers()
        {
            try
            {
                return Ok(await _onBoardService.GetExistingCustomers());
            }
            catch (Exception ex)
            {
                return BadRequest(ex?.InnerException?.InnerException?.Message ?? ex?.InnerException?.Message ?? ex?.Message);
            }
        }

    }
}
