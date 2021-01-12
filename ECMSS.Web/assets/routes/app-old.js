const BASE_URL = "https://localhost:44372/api";

var app = $.sammy(function () {
    this.use("Template");

    this.get("#/", async function (context) {
        var elem = $("#tbMainDefault tbody");
        await axios
            .get(`${BASE_URL}/fileinfo`)
            .then(function (response) {
                $(elem).html("");
                response.data.forEach(function (value) {
                    console.log(value);
                    var fileHistories = value.FileHistories.reduce((prev, current) =>
                        +prev.id > +current.id ? prev : current
                    );
                    var fileInfo = {
                        id: value.Id,
                        name: value.Name,
                        owner: value.Employee.EpLiteId,
                        size: fileHistories.Size,
                        version: fileHistories.Version,
                        modifiedDate: formatDate(fileHistories.ModifiedDate),
                    };
                    context.render(
                        "static/templates/main.template",
                        { item: fileInfo },
                        function (output) {
                            $(elem).append(output);
                        }
                    );
                });
            })
            .catch(function (error) {
                console.log(error);
            });
    });

    this.get("#/open-content/:id", async function () {
        await axios
            .get(`${BASE_URL}/fileinfo/getfileurl/${this.params["id"]}`)
            .then(function (response) {
                var fileUrl = response.data;
                var item = {
                    view: `${fileUrl}[true]`,
                    edit: `${fileUrl}[false]`,
                };
                $("#changefile .modal-footer center a:nth-child(1)").attr(
                    "href",
                    item.view
                );
                $("#changefile .modal-footer center a:nth-child(2)").attr(
                    "href",
                    item.edit
                );
            })
            .catch(function (error) {
                console.log(error);
            });
    });
}).run("#/");
