window.addEventListener("load", () => {
    initDataTable(`api/FileInfo/GetFileInfosByUserId?empId=${EMPLOYEE_ID}`);

    router.add("/", () => {
        $("#tbMainDefault").DataTable().ajax.url(`api/FileInfo/GetFileInfosByUserId?empId=${EMPLOYEE_ID}`).load();
    });

    router.add("/get-by-dir-{dirId}", (dirId) => {
        var url = `api/FileInfo/GetFileInfosByDirId?dirId=${dirId}`;
        $("#tbMainDefault").DataTable().ajax.url(url).load();
    });

    router.add("/favorites-file", () => {
        $("#tbMainDefault").DataTable().ajax.url(`api/FileInfo/GetFavoriteFiles?empId=${EMPLOYEE_ID}`).load();
    });

    router.add("/important-file", () => {
        $("#tbMainDefault").DataTable().ajax.url(`api/FileInfo/GetImportantFiles?empId=${EMPLOYEE_ID}`).load();
    });

    router.add("/open-content-{id}", async (id) => {
        try {
            const response = await api.get(`FileInfo/GetFileUrl?id=${id}`);
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

    router.add("/filter-{fileType}-file", (fileType) => {
        filterDTByFileType(fileType);
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