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
            _configuration = configuration;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<bool> Delete(decimal id, string token)
        {
            try
            {
                // Enviando uma requisição DELETE
                var delete = await HttpService.SendHttpRequestAsync<RegistroServico>(_httpClientFactory, _configuration, "/api/Servico/" + id, token, HttpMethod.Delete);
                
                // Retorne true se a requisição for bem-sucedida (dependendo da resposta da API)
                return delete != null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in DeleteAsync:  {ex.Message}");
                return false;
            }
        }

        public async Task<IEnumerable<RegistroServico>?> GetAll(string token)
        {
            try
            {
                var registros = await HttpService.SendHttpRequestAsyncList<RegistroServico>(
                                                                        _httpClientFactory,
                                                                        _configuration,
                                                                        "/api/Servico/",
                                                                        null
                                                                    );
                return registros;
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
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetId:  {ex.Message}");
                return null;
            }
        }

        public async Task<RegistroServico> CreateServico(RegistroServico registroServico, string token)
        {
            try
            {
                return await HttpService.SendHttpRequestAsync<RegistroServico>(_httpClientFactory,
                    _configuration,
                    "api/Servico",
                    token,
                    HttpMethod.Post,
                    registroServico);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public async Task<RegistroServico> UpdateServico(RegistroServico registroServico, string token)
        {
            try
            {
                return await HttpService.SendHttpRequestAsync<RegistroServico>(_httpClientFactory,
                    _configuration,
                    "api/Servico",
                    token,
                    HttpMethod.Put,
                    registroServico);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        Task IServicoDados.Delete(decimal id, string token)
        {
            return Delete(id, token);
        }
    }
}
