"use strict";

(function () {
    $.ajax({
        type: "GET",
        url: "/api/Token/GetTokenV2",
        data: {
            epLiteId: getEpLiteUserFromInp()
        }, success: function (data) {
            localStorage.setItem("token", data.token);
            localStorage.setItem("curEmp", JSON.stringify(data.curEmp));
            $(".top-right .username").text(data.curEmp.LastName + " " + data.curEmp.FirstName);
        }, error: function () {
            swal("Failed!", "Validation error, you need access via EpLite", "error");
            window.stop();
        }
    });
})();

function getEpLiteUserFromInp() {
    var token = $("#txtToken").val();
    $("#txtToken").remove();
    return token;
}

if (!String.format) {
    String.format = function (format) {
        var args = Array.prototype.slice.call(arguments, 1);
        return format.replace(/{(\d+)}/g, function (match, number) {
            return typeof args[number] != "undefined" ? args[number] : match;
        });
    };
}

const router = new Router({
    mode: "history"
});

const api = axios.create({
    baseURL: "https://localhost:44372/api/",
    timeout: 5000,
    headers: { "Authorization": "Bearer " + localStorage.getItem("token") }
});

var configDT = {
    trashRoute: false
};