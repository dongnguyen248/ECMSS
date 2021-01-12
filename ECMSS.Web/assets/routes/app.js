window.addEventListener('load', () => {
    const api = axios.create({
        baseURL: 'https://localhost:44372/api',
        timeout: 5000,
    });

    const router = new Router({
        mode: 'history'
    });

    const mainTable = $('#tbMainDefault tbody');
    const mainTableTemplate = Handlebars.compile($('#main-table-template').html());

    router.add('/', async () => {
        let html = mainTableTemplate();
        mainTable.html(html);
        try {
            const response = await api.get('/fileinfo');
            const fileInfo = response.data.map(f => ({
                Id: f.Id, Name: f.Name, Employee: f.Employee, FileHistories: f.FileHistories.reduce((prev, current) =>
                    +prev.id > +current.id ? prev : current
                )
            }));

            html = mainTableTemplate({ fileInfo });
            mainTable.html(html);
            this.table.reload();
        } catch (error) {
            console.log(error);
        }
    });

    router.add('/open-content/{id}', async (id) => {
        try {
            const response = await api.get(`/fileinfo/getfileurl/${id}`);
            var fileUrl = response.data;
            var item = {
                view: `${fileUrl[0]}[true]`,
                edit: `${fileUrl[0]}[false]`,
            };
            $("#changefile .modal-footer center a:nth-child(1)").attr(
                "href",
                item.view
            );
            $("#changefile .modal-footer center a:nth-child(2)").attr(
                "href",
                item.edit
            );
            $("#nameContent").val(fileUrl[1]);
            $("#changefile").modal("show");
        } catch (error) {
            console.log(error);
        }
    });

    router.navigateTo("/");

    $(document).delegate("a", "click", (event) => {
        event.preventDefault();
        var target = $(event.currentTarget);
        var href = target.attr("href");
        router.navigateTo(href);
    });
});