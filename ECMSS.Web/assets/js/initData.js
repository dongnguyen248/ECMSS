"use strict";

function initTreeFolder() {
    api.get("/Directory/GetTreeDirectory").then(function (response) {
        $(".mycontList .treefdBox").html(
            renderTreeFolder(listToTree(response.data), "sidebar-menu1", true)
        );
        $(".addFolderTreeBox").html(
            renderTreeFolder(listToTree(response.data), "sidebar-menu", false)
        );
        $(".addFolderTreeBox2").html(
            renderTreeFolder(listToTree(response.data), "sidebar-menu2", false)
        );

        $('#tbMainDefault').on('xhr.dt', function () { $.fn.dataTable.ext.search.pop(); });
    }).catch(function (error) {
        console.log(error)
    });
};

function initDataTable(url) {
    $("#tbMainDefault").DataTable({
        columnDefs: [{ orderable: false, targets: 0 }],
        order: [[1, "asc"]],
        bFilter: true,
        searching: true,
        info: false,
        ajax: {
            url: url,
            dataSrc: "fileInfos",
            beforeSend: function (xhr) {
                xhr.setRequestHeader("Authorization", "Bearer " + localStorage.token);
            }
        },
        columns: [
            {
                data: "Name",
                render: function (data, type, row) {
                    var favoriteImgSrc = row["IsFavorite"] ? "/assets/imgs/ico_fav_blue_on.png" : "/assets/imgs/ico_fav.png";
                    var importantClass = row["IsImportant"] ? "backgroundImp" : "";
                    if (configDT.trashRoute) {
                        configDT.trashRoute = !configDT.trashRoute;

                        return String.format("<div class='contentTitle'>" +
                            "<div class='checkbox'>" +
                            "<label>" +
                            "<input type='checkbox' value='{1}'>" +
                            "<span class='cr'><i class='cr-icon glyphicon glyphicon-ok'></i></span>" +
                            "</label>" +
                            "</div>" +
                            "<a class='important {0}'><i class='fas fa-info'></i></a>" +
                            "<a class='addfavorite'>" +
                            "<img src='{2}' alt='icon_start' />" +
                            "</a>" +
                            "<a class='contentname'>{3}</a>" +
                            "</div>", importantClass, row["Id"], favoriteImgSrc, data);
                    }
                    return String.format("<div class='contentTitle'>" +
                        "<div class='checkbox'>" +
                        "<label>" +
                        "<input type='checkbox' value='{1}'>" +
                        "<span class='cr'><i class='cr-icon glyphicon glyphicon-ok'></i></span>" +
                        "</label>" +
                        "</div>" +
                        "<a class='important {0}' onclick='addImportant(this, {1})'><i class='fas fa-info'></i></a>" +
                        "<a class='addfavorite' onclick='addFavorite(this, {1})'>" +
                        "<img src='{2}' alt='icon_start' />" +
                        "</a>" +
                        "<a onclick='openContent({1})' class='contentname'>{3}</a>" +
                        "</div>", importantClass, row["Id"], favoriteImgSrc, data);
                }
            },
            { data: "Owner" },
            { data: "Modifier" },
            { data: "Size" },
            { data: "SecurityLevel" },
            { data: "Version" },
            {
                data: "ModifiedDate",
                type: "date",
                render: function (data) {
                    return data ? moment(data).format("DD/MM/yyyy") : "";
                },
            },
        ],
    });
}

function listToTree(list) {
    var map = {},
        node,
        roots = [],
        i;
    for (i = 0; i < list.length; i += 1) {
        map[list[i].Id] = i;
        list[i].Childrens = [];
    }
    for (i = 0; i < list.length; i += 1) {
        node = list[i];
        if (node.ParentId) {
            list[map[node.ParentId]].Childrens.push(node);
        } else {
            roots.push(node);
        }
    }
    return roots;
}

function renderTreeFolder(children, rootClass, renderLink) {
    var childrenName;
    if (children[0]) {
        if (children[0].Name === "Root") {
            childrenName = rootClass;
        } else {
            childrenName = "treeview-menu";
        }
    }
    return (
        String.format("<ul {0}>", childrenName ? "class='" + childrenName + "'" : "") +
        children
            .map(
                function (node) {
                    return "<li class='treeview'>" +
                        String.format("<a {0}>", renderLink ? "href='/get-by-dir-" + node.Id + "'" : "") +
                        "<img src='/assets/imgs/ico_folder_off.png' alt='Folder Image' /> " +
                        node.Name +
                        "</a>" +
                        renderTreeFolder(node.Childrens, rootClass, renderLink) +
                        "</li>"
                }
            )
            .join("\n") +
        "</ul>"
    );
}

function searchContent() {
    var inpTxt = $("#txtSearch").val().trim();
    if (inpTxt.length > 0) {
        $("#tbMainDefault").DataTable().ajax.url("api/FileInfo/Search?searchContent=" + inpTxt).load();
    } else {
        $("#tbMainDefault").DataTable().ajax.url(ROOT_DT_URL).load();
    }
}

function addImportant(obj, fileId) {
    api.post("FileImportant/AddOrRemoveImportantFile?fileId=" + fileId).then(function () {
        if (!$(obj).hasClass('backgroundImp')) {
            $(obj).addClass('backgroundImp');
        } else {
            $(obj).removeClass('backgroundImp');
        }
    }).catch(function (error) {
        console.log(error);
    });
}

function addFavorite(obj, fileId) {
    api.post("FileFavorite/AddOrRemoveFavoriteFile?fileId=" + fileId).then(function () {
        var img = obj.children;
        var src =
            $(img).attr("src") === "/assets/imgs/ico_fav.png"
                ? "/assets/imgs/ico_fav_blue_on.png"
                : "/assets/imgs/ico_fav.png";
        $(img).attr("src", src);
    }).catch(function (error) {
        console.log(error);
    });
}

$("#frm-create-directory").submit(function (event) {
    event.preventDefault();
    var dirName = $(this).find("input[name='dirName']").val();
    var path = $(this).find("input[name='parentName']").val();

    path = path.substring(path.indexOf(">") + 1).trim();
    path = path.replaceAll(">", "/");

    api.post("Directory/CreateDirectory?dirName=" + dirName + "&path=" + path).then(function () {
        initTreeFolder();
        swal("Success!", "Create content successfully!", "success");
        $("#createnew").modal("hide");
    }).catch(function () {
        swal("Failed!", "Create content failed, check your input and try again!", "error");
    });
});

function openContent(fileId) {
    api.get(String.format("FileInfo/GetFileUrl?id={0}", fileId)).then(function (response) {
        var fileUrl = response.data;

        $("#changefile .modal-footer center a:nth-child(1)").attr(
            "href",
            "ECMProtocol: " + fileUrl[0]
        );

        if (fileUrl[1]) {
            $("#changefile .modal-footer center a:nth-child(2)").removeAttr("disabled");
            $("#changefile .modal-footer center a:nth-child(2)").attr("href", "ECMProtocol: " + fileUrl[1]);
        } else {
            $("#changefile .modal-footer center a:nth-child(2)").attr("disabled", "disabled");
            $("#changefile .modal-footer center a:nth-child(2)").removeAttr("href");
        }

        $("#nameContent").val(fileUrl[2]);
        $("#changefile").modal("show");
    }).catch(function (error) {
        console.log(error);
    });
}

function deleteFile() {
    var selectedFile = $(".checkbox input:checked");
    if (selectedFile.length > 0) {
        swal({
            title: "Are you sure?",
            text: "Once deleted, you will not be able to recover this imaginary file!",
            icon: "warning",
            buttons: true,
            dangerMode: true,
        })
            .then(function (willDelete) {
                if (willDelete) {
                    $(selectedFile).each(function (index, value) {
                        //Code
                        swal("Poof! Your imaginary file has been deleted!", { icon: "success" });
                    });
                } else {
                    swal("You file is safe!");
                }
            });
    } else {
        swal("Please select a file want to delete!");
    }
}

function deleteFolder() {
    var selectedElem = $('.sidebar-menu1').find('.selectbackground');
    var root = $(".sidebar-menu1");
    var path = wrapDfs(root, selectedElem);
    tempPath = "";
    path = path.replaceAll(">", "/");
    if (selectedElem.length > 0) {
        swal({
            title: "Are you sure?",
            text: "Once deleted, You will not be able to recover all file in this Folder!",
            icon: "error",
            buttons: true,
            dangerMode: true,
        })
            .then(function (willDelete) {
                if (willDelete) {
                    api.post("Directory/DeleteDirectory?path=" + path).then(function () {
                        initTreeFolder();
                        swal("Success!", "Delete content successfully!", "success");
                        $("#createnew").modal("hide");
                        router.navigateTo("/");
                    }).catch(function () {
                        swal("Failed!", "Delete content failed, try again later!", "error");
                    });
                } else {
                    swal("Your content file is safe!");
                }
            });
    } else {
        swal("Please select a folder you want delete!");
    }
}