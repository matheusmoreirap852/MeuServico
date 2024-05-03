using AutoMapper;
using BackEndApi.Models;
using BackEndApi.Service.IService;
using System.Data;

namespace BackEndApi.Service
{
    public class MinhaInformacoes : IMinhaInformacoes
    {

        private readonly IMinhaInformacoes _minhaInformacoes;
        private readonly IMapper _mapper;
        private readonly IDbConnection _dbConnection;

        public MinhaInformacoes(IMinhaInformacoes minhaInformacoes, IMapper mapper, IDbConnection dbConnection)
        {
            _minhaInformacoes = minhaInformacoes;
            _mapper = mapper;
            _dbConnection = dbConnection;
        }


        public async Task<bool> DeleteById(decimal id)
        {
            try
            {
                return true;
            }catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public Task<IEnumerable<RegistroServico>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<RegistroServico> GetAllId(decimal id)
        {
            throw new NotImplementedException();
        }

        public Task<RegistroServico> Set(RegistroServico dados)
        {
            throw new NotImplementedException();
        }

        public Task<RegistroServico> Update(RegistroServico dados)
        {
            throw new NotImplementedException();
        }
    }
}
