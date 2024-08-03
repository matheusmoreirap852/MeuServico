using AutoMapper;
using BackEndApi.Dtos;
using BackEndApi.Models;
using BackEndApi.Repositories.IRepositories;
using BackEndApi.Service.IService;
using System.Data;

namespace BackEndApi.Service
{
    public class MinhaInformacoesService : IMinhaInformacoes
    {

        private readonly IMinhaInformacoesRepositorie _minhaInformacoes;
        private readonly IMapper _mapper;
        private readonly IDbConnection _dbConnection;

        public MinhaInformacoesService(IMinhaInformacoesRepositorie minhaInformacoes, IMapper mapper, IDbConnection dbConnection)
        {
            _minhaInformacoes = minhaInformacoes;
            _mapper = mapper;
            _dbConnection = dbConnection;
        }


        public async Task<bool> DeleteById(decimal id)
        {
            try
            {
                var ValorEntity = _minhaInformacoes.GetAllId(id).Result;
                await _minhaInformacoes.DeleteById(ValorEntity.Id);
                return true;
            }catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public async Task<IEnumerable<RegistroServiceDto>> GetAll()
        {
            var ValorEntity = await _minhaInformacoes.GetAll();
            return _mapper.Map<IEnumerable<RegistroServiceDto>>(ValorEntity);
        } 

        public async Task<RegistroServiceDto> GetAllId(decimal id)
        {
            try
            {
                var ValorEntity = await _minhaInformacoes.GetAllId(id);
                return _mapper.Map<RegistroServiceDto>(ValorEntity);
            }
            catch  (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        public async Task<RegistroServiceDto> Set(RegistroServiceDto dados)
        {
            try
            {
                var ValorEntity = _mapper.Map<RegistroServico>(dados);
                await _minhaInformacoes.Set(ValorEntity);
                dados.Id = ValorEntity.Id;
                return dados;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        public async Task<RegistroServiceDto> Update(RegistroServiceDto dados)
        {
            try
            {
                var ValoresEntity = _mapper.Map<RegistroServico>(dados);
                await _minhaInformacoes.Update(ValoresEntity);
                dados.Id  = ValoresEntity.Id;
                return dados;   
            }catch  (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
    }
}
