using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoServicoWork.Services.Contracts;
using X.PagedList;

namespace ProjetoServicoWork.Controllers
{
    public class EnvioInformacoesController : Controller
    {
        const int ItensPorPagina = 10;
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IServicoDados _servicoDados;

        public EnvioInformacoesController(ILogger<HomeController> logger,
            IConfiguration configuration,
            IWebHostEnvironment webHostEnvironment,
            IServicoDados servicoDados)
        {   

            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
            _servicoDados = servicoDados ?? throw new ArgumentNullException(nameof(servicoDados));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public string SessionKeyIdEquipe { get; private set; }
        public string SessionKeyId { get; private set; }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> IndexAsync(int? id, int? pagina, string? filter)
        {

            const int itensPorPagina = 10;
            int numeroPagina = pagina ?? 1;
            //HttpContext.Session.SetInt32(SessionKeyIdEquipe, id);
            // Obtenha o token JWT
           // var accessToken = await HttpContext.GetTokenAsync("access_token");
            //var idEvento = HttpContext.Session.GetInt32(SessionKeyId).ToString();

            var ListServico = await _servicoDados.GetAll("accessToken");

            // Filter list if filter is provided.
            var model = !string.IsNullOrWhiteSpace(filter)
                        ? ListServico.Where(p => p.Titulo.Contains(filter)).ToList()
                        : ListServico;
            return View(model.ToPagedList(numeroPagina, ItensPorPagina));
        }

    }
}
