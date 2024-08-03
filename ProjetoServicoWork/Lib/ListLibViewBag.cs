using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProjetoServicoWork.Lib
{
    public class ListLibViewBag
    {
        //private static IServicoListDados _servicoListDados;
       /* public static void Initialize(IServicoListDados servicoListDados)
        {
            _servicoListDados = servicoListDados;
        }*/
        public async static Task GetMunicipiosEvento(dynamic viewBag, string idEvento, string token)
        {
            try
            {
               // IEnumerable<TbMunicipio> municipios = await _servicoListDados.MunicipiosListEventoAsync(idEvento, token);
               // var teste = new SelectList(municipios, "munCodigo", "munDescricao", "munDescricao");
               // viewBag.carregadadosMunicipio = teste;
            }
            catch (Exception ex)
            {
                // Tratar a exceção apropriada ou logar
                throw;
            }
        }
    }
}
