import DocumentoService from "./../../services/documento.service.js";
import AtendimentoService from "./../../services/atendimento.service.js";

export default class HabilitacaoEditViewComponent {
  constructor() {
    this.callbackFunctions();
    this.loadFotoDocumento();

    let checked = false;
    if ($(".add-for-me:checked").length > 0) checked = true;
    if ($(".add-for-me:checked:disabled").length > 0) checked = false;

    this.configAtendimento(checked);
    this.DisableDocumentoReprovado();
  }

  callbackFunctions() {
    $(".reenviar-email").click(async (e) => {
      try {
        $("#cofirmSendEmail").modal("toggle");

        let formData = new FormData();
        formData.append("solicitacaoId", $(e.target).attr("solicitacaoId"));

        let response = await AtendimentoService.reenviarEmail(formData);

        if (response) {
          toastr.success("Email reenviado com sucesso!", "Atendimento");
          return;
        }

        throw new Error("Falha ao enviar!");
      } catch (e) {
        toastr.error(
          "Tente novamente mais tarde, ou contate o administrador!",
          "Erro"
        );
      }
    });

    $(".add-for-me").change(async (event) => {
      let formData = new FormData();
      formData.append("Status", event.target.checked);
      formData.append("Id", $(".add-for-me").attr("solicitacao-id"));

      try {
        let response = await AtendimentoService.addForMe(formData);

        if (response.status) {
          this.configAtendimento(event.target.checked);
          toastr.success(response.mensagem, "Atendimento");
          return;
        }
        // Erro          
          this.configAtendimento(!event.target.checked);
          if (response.mensagem == undefined) {
              toastr.error("Operação não permitada.", "Erro");
          } else {
              toastr.error(response.mensagem, "Erro");
          }
          
      } catch (e) {
        toastr.error(
          "Tente novamente mais tarde, ou contate o administrador!",
          "Erro"
        );
      }
    });

    $(".download-documento").click(async (e) => {
      try {
        var result = await DocumentoService.download(
          $(e.target).attr("documento-id")
        );
        try {
          var win = window.open(result.fileUrl);

          setTimeout(() => {
            win.document.title = result.title;
          }, 100);
        } catch (e) {
          toastr.error(
            "O navegador bloqueou a abertura da p�gina. Verifique o bloqueio de pop-up e tente novamente.",
            "Erro"
          );
        }
      } catch (e) {
        toastr.error(
          "Tente novamente mais tarde, ou contate o administrador!",
          "Erro"
        );
      }
    });

    $(".documento-status").change(async (e) => {
      let documentoId = $(e.target).attr("documento-id");
      let situacao = $(e.target).val();
      let formData = new FormData();

      formData.append("Id", documentoId);
      formData.append("Situacao", situacao);

      try {
        await DocumentoService.salvar(formData);
        toastr.success("Documento atualizado com sucesso!", "Atualizado");
      } catch (e) {
        toastr.error(
          "Tente novamente mais tarde, ou contate o administrador!",
          "Erro"
        );
      }
    });

    $(".finalizar-atendimento").click(async (event) => {
      event.preventDefault();

      let formData = new FormData();
      formData.append("observacao", $("[name=observacao]").val());
      formData.append("situacao", $("[name=situacao]").val());
      formData.append("SolicitacaoId", $("[name=id]").val());
      try {
        let response = await AtendimentoService.finalizarAtendimento(formData);
        this.configAtendimento(!response.status);

        if (response.status) {
          toastr.success(response.mensagem, "Atendimento");
          return;
        }

        // Erro
        toastr.error(response.mensagem, "Erro");
      } catch (e) {
        toastr.error(
          "Tente novamente mais tarde, ou contate o administrador!",
          "Erro"
        );
      }
    });
  }

  loadFotoDocumento() {
    try {
      if ($(".loadFotoDocumento")[0]) {
        $(".loadFotoDocumento").each(async (index, element) => {
          var result = await DocumentoService.download(
            $(element).attr("documento-id")
          );

          if (result && result.fileUrl) {
            $(element).attr("src", result.fileUrl);
            $(element).attr("title", result.title);
          }
        });
      }
    } catch (e) {
      console.log(e);
    }
  }

  DisableDocumentoReprovado() {
    $(".documento-status option:selected").each(function () {
      if ($(this).val() === 2) {
        $(this).parent("select").attr("disabled", "disabled");
        $(this).parent().parent().find("button").addClass("disabled");
      }
    });
  }

  configAtendimento(enabled = true) {
    if (enabled) {
      $(".add-for-me").prop("checked", true);
      $(".is-assigned-attendant").attr("disabled", false);
      $(".documento-status").attr("disabled", false);
      $(".documento-status").parent().find("button").removeClass("disabled");
    } else {
      $(".add-for-me").prop("checked", false);
      $(".is-assigned-attendant").attr("disabled", true);
      $(".documento-status").attr("disabled", true);
      $(".documento-status").parent().find("button").addClass("disabled");
    }
  }
}
