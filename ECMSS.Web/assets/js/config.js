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
});

const router = new Router({
    mode: 'history'
});

const EMPLOYEE_ID = 1;

var configDT = {
    trashRoute: false
}