import DocumentoService from "./../../services/documento.service.js";

export default class ProjetoIndexViewComponent {
  constructor() {
    this.callbackFunctions();
  }

  callbackFunctions() {
    let self = this;

    //display show in input file
    $(".alterar-documento").on("click", function (event) {
      $("#documentoEditalAlerar").hide();
      $("#downloadDocumentoEdital").hide();
      $("#uploadEdital").show();
    });

    //Download do Edital no cadastro do Projeto
    $(".download-documento").click(async (elem) => {
      try {
        var result = await DocumentoService.downloadEditalProjeto(
          $(elem.target).attr("projeto-id"),
          $(elem.target).attr("data-url")
        );
        try {
          var win = window.open(result.fileUrl);

          setTimeout(() => {
            win.document.title = result.title;
          }, 100);
        } catch (e) {
          toastr.error(
            "O navegador bloqueou a abertura da página. Verifique o bloqueio de pop-up e tente novamente.",
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

    //Abrir Modal de Atividade
    var placeholderElement = $("#modal-placeholder");
    $('button[data-toggle="ajax-modal"]').click(function (event) {
      var url = $(this).data("url");
      $.get(url).done(function (data) {
        placeholderElement.html(data);
        placeholderElement.find(".modal").modal("show");
      });
    });

    //Salvar Modal Atividade
    placeholderElement.on(
      "click",
      '[data-save="salvar-atividade"]',
      function (event) {
        event.preventDefault();

        var form = $(this).parents(".modal").find("form");
        var actionUrl = form.attr("action");
        var dataToSend = form.serialize();

        loading(true);

        $.post(actionUrl, dataToSend).done(function(data) {
          loading(false);
          var newBody = $(".modal-body", data);
          placeholderElement.find(".modal-body").replaceWith(newBody);

          self.setupDatepicker();
        });
      }
    );

    //reload na pagina ao fechar o modal
    placeholderElement.on("hidden.bs.modal", function () {
      window.location.reload();
    });

    placeholderElement.on("shown.bs.modal", function() {
      self.setupDatepicker();
    });

    //Btn nova Atividade
    placeholderElement.on(
      "click",
      '[data-open="nova-atividade"]',
      function (event) {
        var url = $(this).data("url");
        $.get(url).done(function (data) {
          placeholderElement.html(data);
          placeholderElement.find(".modal").modal("show");
        });
      }
    );
  }

  setupDatepicker() {
    $(".maskDate").each(function () {
      $(this)
        .inputmask("99/99/9999", { reverse: true })
        .datepicker({
          rtl: KTUtil.isRTL(),
          format: "dd/mm/yyyy",
          todayHighlight: true,
          autoclose: true,
          language: "pt-BR",
          orientation: "bottom left",
          templates: {
            leftArrow: '<i class="la la-angle-left"></i>',
            rightArrow: '<i class="la la-angle-right"></i>',
          },
        });
    });
  }
}
