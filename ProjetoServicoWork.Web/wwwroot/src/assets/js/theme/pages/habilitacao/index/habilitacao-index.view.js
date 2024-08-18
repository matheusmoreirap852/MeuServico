export default class HabilitacaoIndexViewComponent {
  constructor() {
    this.situacao = $("#situacoes");
    this.btnReset = $("#reset-form");

    this.callbackFunctions();
  }

  callbackFunctions() {
    this.situacao.select2({ placeholder: "Selecione a Situação" });

    this.btnReset.on("click", () => {
      $("input[name!=page][name!=Size][name!=status]").attr("value", "");
      this.situacao.val(null).trigger("change");
    });
  }
}
