"use strict";

const ROOT_DT_URL = "api/FileInfo/GetFileInfos";

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
        $("#tbMainDefault").DataTable().ajax.url("api/FileInfo/GetFavoriteFiles").load();
    });

    router.add("/important-file", function () {
        $("#tbMainDefault").DataTable().ajax.url("api/FileInfo/GetImportantFiles").load();
    });

    router.add("/department-file", function () {
        $("#tbMainDefault").DataTable().ajax.url("api/FileInfo/GetDepartmentFiles").load();
    });

    router.add("/trash-content", function () {
        configDT.trashRoute = true;
        $("#tbMainDefault").DataTable().ajax.url("api/FileInfo/GetTrashContents").load();
    });

    router.add("/shared-file", function () {
        $("#tbMainDefault").DataTable().ajax.url("api/FileInfo/GetSharedFiles").load();
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