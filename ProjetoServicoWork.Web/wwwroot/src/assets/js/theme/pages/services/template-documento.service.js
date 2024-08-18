import Environmnet from "../base/environment.js";

export default class TemplateDocumentoService {    
    static download(data, controller = "habilitacaoUrl") {
        return new Promise(function (resolve, reject) {
            let env = new Environmnet();
            $.ajax({
                url: `${env[controller]}template-documento/download`,
                type: "GET",
                method: "GET",
                crossDomain: true,
                data: data,
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
}
