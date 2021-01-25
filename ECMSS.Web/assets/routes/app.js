const ROOT_DT_URL = "api/FileInfo/GetFileInfosByUserId?empId=" + EMPLOYEE_ID;

window.addEventListener("load", function () {
    initDataTable(ROOT_DT_URL);

    router.add("/", function () {
        initTreeFolder();
        $("#tbMainDefault").DataTable().ajax.url(ROOT_DT_URL).load();
    });

    router.add("/get-by-dir-{dirId}", function (dirId) {
        var url = "api/FileInfo/GetFileInfosByDirId?dirId=" + dirId;
        $("#tbMainDefault").DataTable().ajax.url(url).load();
    });

    router.add("/favorites-file", function () {
        $("#tbMainDefault").DataTable().ajax.url("api/FileInfo/GetFavoriteFiles?empId=" + EMPLOYEE_ID).load();
    });

    router.add("/important-file", function () {
        $("#tbMainDefault").DataTable().ajax.url("api/FileInfo/GetImportantFiles?empId=" + EMPLOYEE_ID).load();
    });

    router.add("/department-file", function () {
        $("#tbMainDefault").DataTable().ajax.url("api/FileInfo/GetDepartmentFiles?empId=" + EMPLOYEE_ID).load();
    });

    router.add("/trash-content", function () {
        configDT.trashRoute = true;
        $("#tbMainDefault").DataTable().ajax.url("api/FileInfo/GetTrashContents?empId=" + EMPLOYEE_ID).load();
    });

    router.add("/shared-file", function () {
        $("#tbMainDefault").DataTable().ajax.url("api/FileInfo/GetSharedFiles?empId=" + EMPLOYEE_ID).load();
    });

    router.add("/open-content-{id}", function (id) {
        api.get("FileInfo/GetFileUrl?id=" + id).then(function (response) {
            var fileUrl = response.data;
            var item = {
                view: "ECMProtocol: " + fileUrl[0],
                edit: "ECMProtocol: " + fileUrl[1],
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
        }).catch(function (error) {
            console.log(error);
        });
    });

    router.add("/filter-{fileType}-file", function (fileType) {
        filterDTByFileType(fileType);
    });

    router.navigateTo("/");

    $(document).delegate("a", "click", function (event) {
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