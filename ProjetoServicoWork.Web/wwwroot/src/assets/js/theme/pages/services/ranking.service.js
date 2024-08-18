import Environmnet from "../base/environment.js";

const _env = new Environmnet(),
  _baseUrl = _env["baseUrl"],
  _controller = "ranking",
  _url = _baseUrl + _controller;

export default class RankingService {
  static gerarRanking(data, controller = "baseUrl") {
    return new Promise(function (resolve, reject) {
      $.ajax({
        url: `${_url}/gerar-ranking`,
        type: "POST",
        method: "POST",
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
          if (e && e.responseJSON && e.responseJSON.Message) {
            let content = e.responseJSON.Message;
            reject(JSON.parse(content));
          } else {
            reject(e);
          }
        },
        complete: function () {
          loading(false);
        },
      });
    });
  }
}
