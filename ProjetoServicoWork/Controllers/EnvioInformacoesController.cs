using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace ProjetoServicoWork.Controllers
{
    public class EnvioInformacoesController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly I

    public EnvioInformacoesController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

        public string SessionKeyIdEquipe { get; private set; }
        public string SessionKeyId { get; private set; }

        public async Task<IActionResult> IndexAsync(int id, int? pagina, string filter)
    {
            const int itensPorPagina = 10;
            int numeroPagina = pagina ?? 1;

            HttpContext.Session.SetInt32(SessionKeyIdEquipe, id);
            // Obtenha o token JWT
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var idEvento = HttpContext.Session.GetInt32(SessionKeyId).ToString();

            var atletas = await _servicoListDados.ListaAtletasListAsync(id.ToString(), accessToken);
            atletas = atletas.Where(c => c.eqpCodigo == id);

            // Filter list if filter is provided.
            var model = !string.IsNullOrWhiteSpace(filter)
                        ? atletas.Where(p => p.Nome.Contains(filter)).ToList()
                        : atletas;
            return View(model.ToPagedList(numeroPagina, ItensPorPagina));
        }

}
}
