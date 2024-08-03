using BackEndApi.Dtos;
using BackEndApi.Models;

namespace BackEndApi.Service.IService
{
    public interface IMinhaInformacoes
    {
        Task<IEnumerable<RegistroServiceDto>> GetAll();
        Task<RegistroServiceDto> GetAllId(decimal id);
        Task<RegistroServiceDto> Update(RegistroServiceDto dados);
        Task<RegistroServiceDto> Set(RegistroServiceDto dados);
        Task<bool> DeleteById(decimal id);
    }
}
