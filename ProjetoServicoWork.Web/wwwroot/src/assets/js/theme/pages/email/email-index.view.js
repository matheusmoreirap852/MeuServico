import EmailService from "./../services/email.service.js";

export default class EmailIndexViewComponent {
    constructor() {
        this.callbackFunctions();
    }

    callbackFunctions() {
        $(".formGetPessoaByCpf").submit((e) => {
            this.getPessoaByCPF($("#cpf").val());
        });

        $(".formGetPessoaByEmail").submit((e) => {
            this.getPessoaByEmail($("#email-cliente").val());
        });


        $(".alterarEmail").click((e) => {
            e.preventDefault();

            if ($("#novoEmail").val())
                this.handleAlterarEmail($("#novoEmail").val(), $("#confirmaNovoEmail").val(), $("[name=cpfUsuario]").val());
            else
                toastr.error('Informe o novo e-mail! ', 'Erro');
        });

        $(".clearForm").click(() => {
            this.handleSetarInformacoesTela();
        });
    }

    getPessoaByCPF = async (cpf) => {
        this.handleSetarInformacoesTela();

        if (!cpf)
            return false;

        let resolveCpf = cpf.replace(".", "").replace(".", "").replace("-", "");

        let pessoa = await EmailService.getPessoaSCAByCPF(resolveCpf);
        let usuario = await EmailService.getByCPF(resolveCpf);

        if (!pessoa || !usuario.email) {
            toastr.error('Não identificamos nenhum usuário com este CPF.', 'Erro');
        }

        this.handleSetarInformacoesTela(pessoa, usuario);
    }

    getPessoaByEmail = async (email) => {
        this.handleSetarInformacoesTela();

        if (!email)
            return false;

        let usuario = await EmailService.getPessoaAuthByEmail(email);
        let pessoa = await EmailService.getPessoaSCAByCPF(usuario.cpf);

        if (!pessoa || !usuario.email) {
            toastr.error('Não identificamos nenhum usuário com este e-mail.', 'Erro');
        }

        this.handleSetarInformacoesTela(pessoa, usuario);
    }

    getByCpf = async (cpf) => {
        if (!cpf)
            return false;

        let resolveCpf = cpf.replace(".", "").replace(".", "").replace("-", "");
        let result = await EmailService.getByCPF(resolveCpf);
        return result;
    }

    handleSetarInformacoesTela(pessoa, usuario) {
        if (!pessoa || !usuario.email) {
            $("#lblnome").text("");
            $("#lblnmmae").text("");
            $("#lbldtnascimen").text("");
            $("#lblemail").text("");
            $("#lblemailconf").text("");

            $("#novoEmail").val("");
            $("#confirmaNovoEmail").val("");

            $("[name=cpfUsuario]").val("");

            $(".dados-cliente").addClass("invisible");
        } else {
            let resolveDtNascimen = pessoa.dtnascimen.replace(/(\d*)-(\d*)-(\d*).*/, '$3-$2-$1');
            $("#lblnome").text(pessoa.nmcliente);
            $("#lblnmmae").text(pessoa.nmmae);
            $("#lbldtnascimen").text(resolveDtNascimen);
            $("#lblemail").text(usuario.email);

            $("[name=cpfUsuario]").val(usuario.cpf);

            if (usuario.emailConfirmed) {
                $("#lblemailconf").html("Sim").css("color", "#0F0");
            } else {
                $("#lblemailconf").html("Não <small>(Ao alterar os dados o novo e-mail será confirmado automaticamente)</small>").css("color", "#f00");
            }

            $(".dados-cliente").removeClass("invisible");
        }
    }

    handleAlterarEmail(novoEmail, confirmaNovoEmail, cpf) {
        try {
            event.preventDefault();

            if (!ValidateEmail(novoEmail) || !ValidateEmail(confirmaNovoEmail)) {
                throw new Error('Os e-emails informados são inválidos!');
            }

            if (novoEmail !== confirmaNovoEmail) {
                throw new Error('Os e-emails informados não coincidem!');
            }

            let formData = new FormData();
            formData.append("cpf", cpf);
            formData.append("Email", novoEmail);

            EmailService.changeEmail(formData).then((r) => {
                if (!r) {
                    toastr.error('O usuário não foi cadastrado ou o novo email já está vinculado à outro usuário.', 'Erro');
                } else {
                    this.handleSetarInformacoesTela();
                    toastr.success('E-mail alterado com sucesso!', 'Success');
                }

            });
        } catch (err) {
            toastr.error(err.message, 'Erro');
        }

    }
}