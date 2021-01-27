"use strict";

(function () {
    $.ajax({
        type: "GET",
        url: "/api/Token/GetToken",
        data: {
            epLiteId: "anhhuy.le"
        }, success: function (data) {
            localStorage.token = data;
        }, error: function () {
            alert("Failed");
        }
    });
})();

if (!String.format) {
    String.format = function (format) {
        var args = Array.prototype.slice.call(arguments, 1);
        return format.replace(/{(\d+)}/g, function (match, number) {
            return typeof args[number] != 'undefined' ? args[number] : match;
        });
    };
}

const api = axios.create({
    baseURL: "https://localhost:44372/api/",
    timeout: 5000,
    headers: { "Authorization": "Bearer " + localStorage.token }
});

const router = new Router({
    mode: 'history'
});

var configDT = {
    trashRoute: false
};