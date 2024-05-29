using ProjetoServicoWork.Models;

namespace ProjetoServicoWork.Services.Contracts;

public interface IServicoDados
{
    Task<IEnumerable<RegistroServico>> GetAll(string token);
    Task<RegistroServico> GetAllId(string id, string token);
    Task<RegistroServico> CreateServico(RegistroServico registroServico, string token);
    Task<RegistroServico> UpdateServico(RegistroServico registroServico, string token);
    Task Delete(decimal id, string token);
}
