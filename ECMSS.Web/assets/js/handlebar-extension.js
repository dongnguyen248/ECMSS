Handlebars.registerHelper('formatDate', function (odate) {
    var date = new Date(odate);
    var day = date.getDate() + "";
    var month = date.getMonth() + 1 + "";
    var year = date.getFullYear() + "";
    var hour = date.getHours() + "";
    var minutes = date.getMinutes() + "";
    var seconds = date.getSeconds() + "";

    day = checkZero(day);
    month = checkZero(month);
    year = checkZero(year);
    hour = checkZero(hour);
    minutes = checkZero(minutes);
    seconds = checkZero(seconds);

    return (
        day + "/" + month + "/" + year + " " + hour + ":" + minutes
    );
});

function checkZero(data) {
    if (data.length == 1) {
        data = "0" + data;
    }
    return data;
}
