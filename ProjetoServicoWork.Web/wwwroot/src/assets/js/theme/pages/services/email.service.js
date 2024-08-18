import Environmnet from "../base/environment.js";

export default class AtendimentoService {
    static getPessoaSCAByCPF(cpf) {
        return new Promise(function (resolve, reject) {
            let env = new Environmnet();
            $.ajax({
                url: `${env.baseUrl}email/getByCpf/${cpf}`,
                type: "GET",
                method: "GET",
                crossDomain: true,
                dataType: "json",
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
    static getPessoaAuthByEmail(email) {
        return new Promise(function (resolve, reject) {
            let env = new Environmnet();
            $.ajax({
                url: `${env.baseUrl}email/getPessoaAuthByEmail/${email}`,
                type: "GET",
                method: "GET",
                crossDomain: true,
                dataType: "json",
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

    static getByCPF(cpf) {
        return new Promise(function (resolve, reject) {
            let env = new Environmnet();
            $.ajax({
                url: `${env.baseUrl}email/getUserAuthByCpf/${cpf}`,
                type: "GET",
                method: "GET",
                crossDomain: true,
                dataType: "json",
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

    static changeEmail(form) {
        return new Promise(function (resolve, reject) {
            let env = new Environmnet();
            $.ajax({
                url: `${env.baseUrl}email/changeEmail`,
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

}
