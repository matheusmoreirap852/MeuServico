using BackEndApi.Models;

namespace BackEndApi.Service.IService
{
    public interface IMinhaInformacoes
    {
        Task<IEnumerable<RegistroServico>> GetAll();
        Task<RegistroServico> GetAllId(decimal id);
        Task<RegistroServico> Update(RegistroServico dados);
        Task<RegistroServico> Set(RegistroServico dados);
        Task<bool> DeleteById(decimal id);
    }
}
