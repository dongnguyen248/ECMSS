window.addEventListener('load', () => {
    var table = initDataTable("api/fileinfo");

    router.add('/', async () => {
        table.ajax.url("api/fileinfo").load();
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
        var target = $(event.currentTarget);
        var href = target.attr("href");
        if (href) {
            if (!href.includes("ECMProtocol")) {
                event.preventDefault();
                router.navigateTo(href);
            }
        }
    });
});