using ProjetoServicoWork.Models;

namespace ProjetoServicoWork.Services.Contracts;

public interface IServicoDados
{
    Task<IEnumerable<RegistroServico>> GetAll(string token);
    Task<RegistroServico> GetAllId(string id, string token);
    Task<RegistroServico> CreateEqpAtleta(RegistroServico registroServico, string token);
    Task<RegistroServico> UpdateEqpAtleta(RegistroServico registroServico, string token);
    Task Delete(decimal id, string token);
}
