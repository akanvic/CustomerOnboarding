using Microsoft.AspNetCore.Mvc;
using OnBoarding.Core.Responses;
using OnBoarding.Service.Interface;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace CustomerOnboarding.Controllers
{
    [Route("api/Shared/GetAllBanks")]

    public class BankController
    {
        private readonly IBankService _bankService;

        public BankController(IBankService bankService)
        {
            _bankService = bankService;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get All Banks with Codes", Description = "Endpoint to get all Banks from external API")]
        [SwaggerResponse(200, "Banks Successfully Retrieved", typeof(BankResponse))]
        public async Task<BankResponse> GetAllBanks()
        {
            try
            {
                var response = await _bankService.GetAsync();
                return (response is null) ? null : response;
            }
            catch (System.Net.Http.HttpRequestException ex)
            {
                throw ex;
            }
        }
    }
}
