using OnBoarding.Core.Responses;
using System.Threading.Tasks;

namespace OnBoarding.Service.Interface
{
    public interface IBankService
    {
        Task<BankResponse> GetAsync();
    }
}
