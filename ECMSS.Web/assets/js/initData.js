"use strict";

async function initTreeFolder() {
    try {
        var response = await api.get("/Directory/GetTreeDirectory");
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
    } catch (error) {
        console.log(error);
    }
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
            async: true,
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
                        "<a class='addfavorite' onclick='addOrRemoveFavorite(this, {1})'>" +
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

async function addImportant(obj, fileId) {
    try {
        await api.post("FileImportant/AddOrRemoveImportantFile?fileId=" + fileId);
        if (!$(obj).hasClass('backgroundImp')) {
            $(obj).addClass('backgroundImp');
        } else {
            $(obj).removeClass('backgroundImp');
        }
    } catch (error) {
        console.log(error);
    }
}

async function addOrRemoveFavorite(obj, fileId) {
    try {
        var curEmp = JSON.parse(localStorage.getItem("curEmp"));

        var fileFavorite = {
            FileId: fileId,
            EmployeeId: curEmp.Id
        };

        await api.post("FileFavorite/AddOrRemoveFavoriteFile", fileFavorite);
        var img = obj.children;
        var src =
            $(img).attr("src") === "/assets/imgs/ico_fav.png"
                ? "/assets/imgs/ico_fav_blue_on.png"
                : "/assets/imgs/ico_fav.png";
        $(img).attr("src", src);
    } catch (error) {
        console.log(error);
    }
}

$("#frm-create-directory").submit(async function (event) {
    event.preventDefault();
    var dirName = $(this).find("input[name='dirName']").val();
    var path = $(this).find("input[name='parentName']").val();

    path = path.substring(path.indexOf(">") + 1).trim();
    path = path.replaceAll(">", "/");

    try {
        await api.post("Directory/CreateDirectory?dirName=" + dirName + "&path=" + path);
        initTreeFolder();
        swal("Success!", "Create content successfully!", "success");
        $("#createnew").modal("hide");
    } catch (error) {
        swal("Failed!", "Create content failed, check your input and try again!", "error");
    }
});

async function openContent(fileId) {
    try {
        var response = await api.get(String.format("FileInfo/GetFileUrl?id={0}", fileId));
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
    } catch (error) {
        console.log(error);
    }
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
            .then(async function (willDelete) {
                if (willDelete) {
                    try {
                        await api.post("Directory/DeleteDirectory?path=" + path);
                        initTreeFolder();
                        swal("Success!", "Delete content successfully!", "success");
                        $("#createnew").modal("hide");
                        router.navigateTo("/");
                    } catch (error) {
                        swal("Failed!", "Delete content failed, try again later!", "error");
                    }
                } else {
                    swal("Your content file is safe!");
                }
            });
    } else {
        swal("Please select a folder you want delete!");
    }
}

$("#tab3C > a > img").click(async function () {
    var empName = $("#tab3C > input[name='empName']").val();
    try {
        var response = await api.get(String.format("Employee/GetEmployeesByName?empName={0}", empName));
        var emps = response.data;
        $(emps).each(function (index, value) {
            var checkElem = $(".positionSearch").find("table input[data-emp-id='" + value.Id + "']").length > 0;
            if (!checkElem) {
                $("#userList").append(
                    String.format("<tr>" +
                        "<td>" +
                        "<input type='checkbox' name='name' value='' data-emp-id='{0}' onchange=\"addnewclass({0})\" />" +
                        "<span> {1} {2} [{3}]</span>" +
                        "</td>" +
                        "</tr>", value.Id, value.LastName, value.FirstName, value.Department.Name)
                );
            }
        });
    } catch (error) {
        console.log(error);
    }
});

$(document).on("click", "#btnAddNewFile", async function () {
    var listFileSelected = $("#inputhidden").children().toArray();
    var isFavorite = $("#chktype:checked").length > 0
    var curEmp = JSON.parse(localStorage.getItem("curEmp"));
    var shareReads = $("#bodysub2 table tr input").toArray();
    var shareEdits = $("#bodysub1 table tr input").toArray();

    if ($(listFileSelected).length == 0) {
        swal("Failed!", "No file has been selected yet!", "error");
    } else {
        var fileInfos = await addFileInfo(listFileSelected, curEmp);
        if (isFavorite) {
            await addFavorites(fileInfos, curEmp.Id);
        }
        if (shareReads.length > 0 || shareEdits.length > 0) {
            await shareFile(fileInfos, shareReads, shareEdits);
        }
        resetFileInfoModal();
    }
});

async function shareFile(fileInfos, shareReads, shareEdits) {
    try {
        var fileShares = [];
        for (var i = 0; i < fileInfos.length; i++) {
            if (shareReads.length > 0) {
                for (var r = 0; r < shareReads.length; r++) {
                    var fileShare = {
                        FileId: fileInfos[i].Id,
                        EmployeeId: $(shareReads[r]).attr("data-emp-id"),
                        Permission: 1
                    }
                    fileShares.push(fileShare);
                }
            }
            if (shareEdits.length > 0) {
                for (var r = 0; r < shareEdits.length; r++) {
                    var fileShare = {
                        FileId: fileInfos[i].Id,
                        EmployeeId: $(shareEdits[r]).attr("data-emp-id"),
                        Permission: 2
                    }
                    fileShares.push(fileShare);
                }
            }
        }
        await api.post("FileShare/AddFileShares", fileShares);
        return true;
    } catch (error) {
        return false;
    }
}

async function addFavorites(fileInfos, empId) {
    try {
        var fileFavorites = [];
        for (var i = 0; i < fileInfos.length; i++) {
            var fileFavorite = {
                FileId: fileInfos[i].Id,
                EmployeeId: empId
            };
            fileFavorites.push(fileFavorite);
        }
        await api.post("FileFavorite/AddFavoriteFiles", fileFavorites);
        return true;
    } catch (error) {
        return false;
    }
}

async function addFileInfo(listFileSelected, curEmp) {
    var path = $("#folderPath").val();
    var elems = listFileSelected;
    path = path.substring(path.indexOf(">") + 1).trim();
    path = path.replaceAll(">", "/");
    var dir = await getDirFromPath(path);
    var result = [];
    for (var i = 0; i < elems.length; i++) {
        var value = elems[i];
        var fileInfo = {
            FileData: await fileToByteArray($(value).prop("files")[0]),
            Name: $(value).val().split(/(\\|\/)/g).pop(),
            Owner: curEmp.Id,
            Tag: $(".input_hashtag input[name='tag']").val(),
            DirectoryId: dir.Id
        }
        try {
            var response = await api.post("FileInfo/AddNewFile", fileInfo);
            swal("Success!", "Create content successfully!", "success");
            $("#addNew").modal("hide");
            result.push(response.data);
        } catch (error) {
            swal("Failed!", "add file failed, check your input and try again!", "error");
        }
    }
    return result;
}

function resetFileInfoModal() {
    $("#inputhidden").children().remove();
    $(".listFileImport .list").children().remove();
    $(".listFileImport").css("display", "none");
    $("#folderPath").text("PoscoVST");
}

async function getDirFromPath(path) {
    try {
        var response = await api.get(String.format("Directory/GetDirectoryFromPath?path={0}", path));
        return response.data;
    } catch (error) {
        console.log(error);
    }
}

function fileToByteArray(file) {
    return new Promise((resolve, reject) => {
        try {
            var reader = new FileReader();
            var fileByteArray = [];
            reader.readAsArrayBuffer(file);
            reader.onloadend = (evt) => {
                if (evt.target.readyState == FileReader.DONE) {
                    var arrayBuffer = evt.target.result;
                    var array = new Uint8Array(arrayBuffer);
                    for (byte of array) {
                        fileByteArray.push(byte);
                    }
                }
                resolve(fileByteArray);
            }
        }
        catch (error) {
            reject(error);
        }
    })
}