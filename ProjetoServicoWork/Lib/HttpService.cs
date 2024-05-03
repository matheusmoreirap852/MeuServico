using System.Text.Json;
using System.Text;

namespace ProjetoServicoWork.Lib
{
    public class HttpService
    {
        public static async Task<IEnumerable<T>> SendHttpRequestAsyncList<T>(IHttpClientFactory httpClientFactory, IConfiguration configuration, string apiPath, string token)
        {
            var apiBase = configuration.GetValue<string>("ServiceUri:ProductApi");

            if (!Uri.TryCreate(apiBase, UriKind.Absolute, out var baseUri))
            {
                Console.WriteLine("A base da API não é uma URI válida.");
                return Enumerable.Empty<T>();
            }

            var requestUri = new Uri(baseUri, apiPath);
            var client = httpClientFactory.CreateClient("ProductApi");
            HttpClientExtensions.PutTokenInHeaderAuthorization(token, client);

            var response = await client.GetAsync(requestUri);

            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<IEnumerable<T>>(apiResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }

            // Handle error or return an empty collection, if necessary.
            return Enumerable.Empty<T>();
        }

        public static async Task<T> SendHttpRequestAsync<T>(IHttpClientFactory httpClientFactory, IConfiguration configuration, string apiPath, string token, HttpMethod method = null, object content = null)
        {
            var apiBase = configuration.GetValue<string>("ServiceUri:ProductApi");

            if (!Uri.TryCreate(apiBase, UriKind.Absolute, out var baseUri))
            {
                Console.WriteLine("A base da API não é uma URI válida.");
                return default;
            }

            var requestUri = new Uri(baseUri, apiPath);
            var client = httpClientFactory.CreateClient("ProductApi");
            HttpClientExtensions.PutTokenInHeaderAuthorization(token, client);

            HttpResponseMessage response;

            switch (method?.Method)
            {
                case "POST":
                    response = await client.PostAsync(requestUri, CreateStringContent(content));
                    break;

                case "PUT":
                    response = await client.PutAsync(requestUri, CreateStringContent(content));
                    break;

                case "DELETE":
                    response = await client.DeleteAsync(requestUri);
                    break;

                default:
                    response = await client.GetAsync(requestUri);
                    break;
            }

            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<T>(apiResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }

            // Handle error or return a default value, if necessary.
            return default;
        }

        private static StringContent CreateStringContent(object content)
        {
            if (content == null) return null;

            var json = JsonSerializer.Serialize(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return new StringContent(json, Encoding.UTF8, "application/json");
        }

    }
}
