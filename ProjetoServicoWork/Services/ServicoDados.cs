using ProjetoServicoWork.Lib;
using ProjetoServicoWork.Models;
using ProjetoServicoWork.Services.Contracts;
using System.Text.Json;

namespace ProjetoServicoWork.Services
{
    public class ServicoDados : IServicoDados
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly JsonSerializerOptions _options;
        private readonly IConfiguration _configuration;

        public ServicoDados(IHttpClientFactory httpClientFactory, JsonSerializerOptions options, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _options = options;
            _configuration = configuration;
        }

        public Task Delete(decimal id, string token)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<RegistroServico>?> GetAll(string token)
        {
            try
            {

                return await HttpService.SendHttpRequestAsyncList<RegistroServico>(_httpClientFactory, _configuration, "/api/Servico", token);
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                Console.WriteLine($"Error in UpdateEquipe: {ex.Message}");
                return null;
            }
        }

        public async Task<RegistroServico> GetAllId(string id, string token)
        {
            try
            {
                return await HttpService.SendHttpRequestAsync<RegistroServico>(_httpClientFactory, _configuration, "/api/Servico/" + id, token, HttpMethod.Get);
            }catch (Exception ex)
            {
                Console.WriteLine($"Error in GetId:  {ex.Message}");
                return null;
            }
        }
        public async Task<RegistroServico> CreateEqpAtleta(RegistroServico registroServico, string token)
        {
            try
            {
                return await HttpService.SendHttpRequestAsync<RegistroServico>(_httpClientFactory,
                    _configuration,
                    "api/Servico",
                    token,
                    HttpMethod.Post,
                    registroServico);
            }catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
        public async Task<RegistroServico> UpdateEqpAtleta(RegistroServico registroServico, string token)
        {
            try
            {
                return await HttpService.SendHttpRequestAsync<RegistroServico>(_httpClientFactory,
                    _configuration,
                    "api/Servico",
                    token,
                    HttpMethod.Put,
                    registroServico);
            }catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
    }
}
