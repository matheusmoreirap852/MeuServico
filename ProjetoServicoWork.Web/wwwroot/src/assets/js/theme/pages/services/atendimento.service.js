import Environmnet from "../base/environment.js";

export default class AtendimentoService {
    static addForMe(form, controller = "habilitacaoUrl") {
        return new Promise(function (resolve, reject) {
            let env = new Environmnet();
            $.ajax({
                url: `${env[controller]}alterar-atendente`,
                type: "PUT",
                method: "PUT",
                data: form,
                processData: false,
                contentType: false,
                crossDomain: true,
                beforeSend: function () {
                    loading(true);
                },
                success: function (data) {
                    resolve(data);
                },
                error: function (e) {
                    reject(e);
                },
                complete: function () {
                    loading(false);
                }
            });
        });
    }

    static finalizarAtendimento(form, controller = "habilitacaoUrl") {
        return new Promise(function (resolve, reject) {
            let env = new Environmnet();
            $.ajax({
                url: `${env[controller]}finalizar-atendimento`,
                type: "PUT",
                method: "PUT",
                data: form,
                processData: false,
                contentType: false,
                crossDomain: true,
                beforeSend: function () {
                    loading(true);
                },
                success: function (data) {
                    resolve(data);
                },
                error: function (e) {
                    reject(e);
                },
                complete: function () {
                    loading(false);
                }
            });
        });
    }

    static updateCandidato(form, controller) {
        return new Promise(function (resolve, reject) {
            let env = new Environmnet();
            $.ajax({
                url: `${env[controller]}update-candidato`,
                type: "PUT",
                method: "PUT",
                data: form,
                processData: false,
                contentType: false,
                crossDomain: true,
                beforeSend: function () {
                    loading(true);
                },
                success: function (data) {
                    resolve(data);
                },
                error: function (e) {
                    reject(e);
                },
                complete: function () {
                    loading(false);
                }
            });
        });
    }

    static reenviarEmail(form, controller = "habilitacaoUrl") {
        return new Promise(function (resolve, reject) {
            let env = new Environmnet();
            $.ajax({
                url: `${env[controller]}reenviar-email`,
                type: "POST",
                method: "POST",
                data: form,
                processData: false,
                contentType: false,
                crossDomain: true,
                beforeSend: function () {
                    loading(true);
                },
                success: function (data) {
                    resolve(data);
                },
                error: function (e) {
                    reject(e);
                },
                complete: function () {
                    loading(false);
                }
            });
        });
    }
}
