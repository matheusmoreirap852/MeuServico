using System.Net.Http.Headers;

namespace ProjetoServicoWork.Lib;

public static class HttpClientExtensions
{
    public static void PutTokenInHeaderAuthorization(string token, HttpClient client)
    {
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }
}
