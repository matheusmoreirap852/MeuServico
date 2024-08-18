import RankingService from "./../../pages/services/ranking.service.js";

export default class InscricoesListViewComponent {
  constructor() {
    this.atividadeId = $("#AtividadeId").val();
    this.callbackFunctions();
  }

  async callbackFunctions() {
    $("#gerar-ranking").click(async () => {
      Swal.fire({
        title: "Tem certeza que deseja gerar o ranking de inscrições?",
        text:"Esta ação não pode ser desfeita, e os candidatos vão receber notificações por email.",
        showCancelButton: true,
        confirmButtonText: `Gerar Ranking`,
        cancelButtonText: `Cancelar`,
      }).then((result) => {
        if (result.value === true) {
          this.gerarRanking();
        }
      });
    });
  }

  gerarRanking() {
    let filtro = {
      atividadeId: this.atividadeId,
    };

    RankingService.gerarRanking(filtro)
      .then((response) => {
        Swal.fire("Ranking gerado com sucesso", "", "success").then(() => {
          document.location.reload();
        });
      })
      .catch((e) => {
        let mensagem = e.content[0].message[0];
        Swal.fire(mensagem, "", "error");
      });
  }
}
