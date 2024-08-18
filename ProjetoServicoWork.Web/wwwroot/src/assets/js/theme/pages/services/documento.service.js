import Environmnet from "../base/environment.js";

export default class DocumentoService {
    static salvar(form, controller = "habilitacaoUrl") {
        return new Promise(function (resolve, reject) {
            let env = new Environmnet();
            $.ajax({
                url: `${env[controller]}Documento/Salvar`,
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


    static download(documentoId, controller = "habilitacaoUrl") {
        return new Promise(function (resolve, reject) {
            let env = new Environmnet();
            $.ajax({
                url: `${env[controller]}Documento/Download`,
                type: "GET",
                method: "GET",
                crossDomain: true,
                data: {
                    Id: documentoId
                },
                dataType: "json",
                beforeSend: function () {
                    loading(true);
                },
                success: function (data) {
                    var sampleArr = DocumentoService.base64ToArrayBuffer(data.arrayBytes);
                    var fileUrl = DocumentoService.saveByteArray(data.nomeArquivo, sampleArr, data.mimeType);
                    var obj = {
                        fileUrl,
                        title: data.nomeArquivo
                    };
                    resolve(obj);
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

    static downloadEditalProjeto(projetoId, url) {
        return new Promise(function (resolve, reject) {
            let env = new Environmnet();
            $.ajax({
                url: url,
                type: "GET",
                method: "GET",
                crossDomain: true,
                data: {
                    Id: projetoId
                },
                dataType: "json",
                beforeSend: function () {
                    loading(true);
                },
                success: function (data) {
                    var sampleArr = DocumentoService.base64ToArrayBuffer(data.arrayBytes);
                    var fileUrl = DocumentoService.saveByteArray(data.nomeArquivo, sampleArr, data.mimeType);
                    var obj = {
                        fileUrl,
                        title: data.nomeArquivo
                    };
                    resolve(obj);
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

    static base64ToArrayBuffer(base64) {
        var binaryString = window.atob(base64);
        var binaryLen = binaryString.length;
        var bytes = new Uint8Array(binaryLen);
        for (var i = 0; i < binaryLen; i++) {
            var ascii = binaryString.charCodeAt(i);
            bytes[i] = ascii;
        }
        return bytes;
    }

    static saveByteArray(reportName, byte, mimeType) {
        var blob = new Blob([byte], { type: mimeType });
        return URL.createObjectURL(blob);
    }
}
