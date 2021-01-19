const ROOT_DT_URL = `api/FileInfo/GetFileInfosByUserId?empId=${EMPLOYEE_ID}`;

window.addEventListener("load", () => {
    initDataTable(ROOT_DT_URL);

    router.add("/", () => {
        $("#tbMainDefault").DataTable().ajax.url(ROOT_DT_URL).load();
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

    router.add("/department-file", () => {
        $("#tbMainDefault").DataTable().ajax.url(`api/FileInfo/GetDepartmentFiles?empId=${EMPLOYEE_ID}`).load();
    });

    router.add("/trash-content", () => {
        configDT.trashRoute = true;
        $("#tbMainDefault").DataTable().ajax.url(`api/FileInfo/GetTrashContents?empId=${EMPLOYEE_ID}`).load();
    });

    router.add("/shared-file", () => {
        $("#tbMainDefault").DataTable().ajax.url(`api/FileInfo/GetSharedFiles?empId=${EMPLOYEE_ID}`).load();
    });

    router.add("/open-content-{id}", async (id) => {
        try {
            const response = await api.get(`FileInfo/GetFileUrl?id=${id}`);
            var fileUrl = response.data;
            var item = {
                view: `ECMProtocol: ${fileUrl[0]}`,
                edit: `ECMProtocol: ${fileUrl[1]}`,
            };
            $("#changefile .modal-footer center a:nth-child(1)").attr(
                "href",
                item.view
            );
            $("#changefile .modal-footer center a:nth-child(2)").attr(
                "href",
                item.edit
            );
            $("#nameContent").val(fileUrl[2]);
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