import DocumentoService from "./../services/documento.service.js";
import AtendimentoService from "./../services/atendimento.service.js";
import TemplateDocumentoService from './../services/template-documento.service.js';

export default class PcgIndexViewComponent {
    constructor() {
        this.callbackFunctions();

        let checked = false;
        if ($(".add-for-me:checked").length > 0)
            checked = true;
        if ($(".add-for-me:checked:disabled").length > 0)
            checked = false;

        this.configAtendimento(checked);      
    }

    callbackFunctions() {
        $(".add-for-me").change(async (event) => {
            let formData = new FormData();
            formData.append("status", event.target.checked);
            formData.append("id", $(".add-for-me").attr("inscricao-id"));

            try {
                let response = await AtendimentoService.addForMe(formData, "pcgUrl");

                if (response.status) {
                    this.configAtendimento(event.target.checked);
                    toastr.success(response.mensagem, 'Atendimento');
                    return;
                }
                // Erro
                this.configAtendimento(!event.target.checked);
                toastr.error(response.mensagem, 'Erro');

            } catch (e) {
                toastr.error('Tente novamente mais tarde, ou contate o administrador!', 'Erro');
            }
        });

        $(".documento-status").change(async (e) => {
            let documentoId = $(e.target).attr("documento-id");
            let situacao = $(e.target).val();
            let formData = new FormData();

            formData.append("id", documentoId);
            formData.append("situacao", situacao);

            try {
                let response = await DocumentoService.salvar(formData, "pcgUrl");
                if (!response.status) {
                    toastr.error(response.mensagem, 'Erro');
                    return;
                }
                toastr.success('Documento atualizado com sucesso!', 'Atualizado');
            } catch (e) {
                toastr.error('Tente novamente mais tarde, ou contate o administrador!', 'Erro');
            }
        });

        $(".download-documento").click(async (elem) => {
            try {
                var result = await DocumentoService.download($(elem.target).attr("documento-id"), "pcgUrl");
                try {
                    var win = window.open(result.fileUrl);

                    setTimeout(() => {
                        win.document.title = result.title;
                    }, 100);
                } catch (e) {
                    toastr.error('O navegador bloqueou a abertura da p�gina. Verifique o bloqueio de pop-up e tente novamente.', 'Erro');
                }
            } catch (e) {
                toastr.error('Tente novamente mais tarde, ou contate o administrador!', 'Erro');
            }
        });

        $(".download-template-documento").click(async (elem) => {
            let data = {
                id: $(elem.target).attr("documento-id"),
                tipo: $(elem.target).attr("documento-tipo")
            };

            try {
                let documento = await TemplateDocumentoService.download(data, "pcgUrl");

                try {
                    let IEwindow = window.open();
                    setTimeout(() => {
                        IEwindow.document.write(documento.html);
                        IEwindow.print(documento.html);
                        IEwindow.close();
                    }, 100);
                } catch (e) {
                    toastr.error('O navegador bloqueou a abertura da p�gina. Verifique o bloqueio de pop-up e tente novamente.', 'Erro');
                }

            } catch (e) {
                toastr.error('Tente novamente mais tarde, ou contate o administrador!', 'Erro');
            }
        });

        $(".finalizar-atendimento").click(async (event) => {
            event.preventDefault();

            let situacao = $("[name=situacao]").val();

            if (!situacao) {
                toastr.error("Situação é obrigatório");
                return;
            }

            let formData = new FormData();
            formData.append("observacao", $("[name=observacao]").val());
            formData.append("situacao", situacao);
            formData.append("inscricaoId", $("[name=id]").val());

            try {
                let response = await AtendimentoService.finalizarAtendimento(formData, "pcgUrl");
                this.configAtendimento(!response.status);

                if (response.status) {
                    toastr.success(response.mensagem, 'Atendimento');
                    return;
                }

                // Erro
                toastr.error(response.mensagem, 'Erro');
            } catch (e) {
                toastr.error('Tente novamente mais tarde, ou contate o administrador!', 'Erro');
            }            
        });

        $(".update-inscricao").click(async (event) => {
            event.preventDefault();

            let formData = new FormData();
            formData.append("superAdmin", true);
            formData.append("id", $("[name=candidatoId]").val());
            formData.append("contatoId", $("[name=contatoId]").val());
            formData.append("sexo", $("[name=sexo]").val());
            formData.append("tipoDocumento", $("[name=tipoDocumento]").val());
            formData.append("tipoDeficiencia", $("[name=tipoDeficiencia]").val());
            formData.append("estadoCivil", $("[name=estadoCivil]").val());
            formData.append("outroEstadoCivil", $("[name=outroEstadoCivil]").val());
            formData.append("cpf", $("[name=cpf]").val().replace(/\D/gim, ''));
            formData.append("nome", $("[name=nome]").val());
            formData.append("dataNascimento", $("[name=dataNascimento]").val());
            formData.append("numeroDocumento", $("[name=numeroDocumento]").val());
            formData.append("nacionalidade", $("[name=nacionalidade]").val());
            formData.append("naturalidade", $("[name=naturalidade]").val());
            formData.append("profissao", $("[name=profissao]").val());
            formData.append("participouPCG", $("[name=participouPCG]").val());
            formData.append("outraAtividadePCG", $("[name=outraAtividadePCG]").val());

            formData.append("contato[id]", $("[name=contatoId]").val());
            formData.append("contato[email]", $("[name=email]").val());
            formData.append("contato[telefonePrincipal]", $("[name=telefonePrincipal]").val());
            formData.append("contato[telefoneSecundario]", $("[name=telefoneSecundario]").val());
            formData.append("contato[logradouro]", $("[name=logradouro]").val());
            formData.append("contato[complemento]", $("[name=complemento]").val());
            formData.append("contato[numero]", $("[name=numero]").val());
            formData.append("contato[cep]", $("[name=cep]").val());
            formData.append("contato[bairro]", $("[name=bairro]").val());

            if ($("[name=responsavelId]").val() != null) {
                formData.append("responsavelId", $("[name=responsavelId]").val());
                formData.append("responsavel[id]", $("[name=responsavelId]").val());
                formData.append("responsavel[contatoId]", $("[name=contatoIdResponsavel]").val());
                formData.append("responsavel[sexo]", $("[name=sexoResponsavel]").val());
                formData.append("responsavel[tipoDocumento]", $("[name=tipoDocumentoResponsavel]").val());
                formData.append("responsavel[tipoDeficiencia]", $("[name=tipoDeficienciaResponsavel]").val());
                formData.append("responsavel[grauParentescoTitular]", $("[name=parentescoResponsavel]").val());
                formData.append("responsavel[estadoCivil]", $("[name=estadoCivilResponsavel]").val());
                formData.append("responsavel[outroEstadoCivil]", $("[name=outroEstadoCivilResponsavel]").val());
                formData.append("responsavel[cpf]", $("[name=cpfResponsavel]").val().replace(/\D/gim, ''));
                formData.append("responsavel[nome]", $("[name=nomeResponsavel]").val());
                formData.append("responsavel[dataNascimento]", $("[name=dataNascimentoResponsavel]").val());
                formData.append("responsavel[numeroDocumento]", $("[name=numeroDocumentoResponsavel]").val());
                formData.append("responsavel[nacionalidade]", $("[name=nacionalidadeResponsavel]").val());
                formData.append("responsavel[naturalidade]", $("[name=naturalidadeResponsavel]").val());
                formData.append("responsavel[profissao]", $("[name=profissaoResponsavel]").val());
                formData.append("responsavel[participouPCG]", $("[name=participouPCGResponsavel]").val());
                formData.append("responsavel[outraAtividadePCG]", $("[name=outraAtividadePCGResponsavel]").val());
            }

            try {
                let response = await AtendimentoService.updateCandidato(formData, "pcgUrl");

                if (response.isSuccessStatusCode) {
                    toastr.success('Inscrição atualizada com sucesso!', 'Inscrição');
                    return;
                }

                // Erro
                toastr.error(response.content[0].message[0], 'Erro');
            } catch (e) {
                toastr.error('Tente novamente mais tarde, ou contate o administrador!', 'Erro');
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