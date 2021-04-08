(function () {
    initDepartments();

    $("#tbMainDefault th").resizable({
        handles: "e",
        minWidth: 15,
        stop: function (e, ui) {
            $(this).width(ui.size.width);
        }
    });
})();

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
                //+ "<div class='dropdown'><a class=' dropdown-toggle' type='button' data-toggle='dropdown'><i class='fas fa-caret-down'></i></a><ul class='dropdown-menu'><li><a href='#'>Coppy Link to Eplite</a></li><li><a onclick='editFileContent({1})'>Edit Content</a></li></ul></div>" +
                data: "Name",
                title: "FileName",
                width: "50%",
                render: function (data, type, row) {
                    var favoriteImgSrc = row["IsFavorite"] ? "/assets/imgs/ico_fav_blue_on.png" : "/assets/imgs/ico_fav.png";
                    var importantClass = row["IsImportant"] ? "backgroundImp" : "";
                    if (isTrashUrl()) {
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
                        "<div class='dropdown'><a class='dropdown-toggle' type='button' data-toggle='dropdown'><i class='fas fa-caret-down'></i></a><ul class='dropdown-menu'><li><a onclick='getFileShareUrl({1})'>Copy Link</a></li><li><a onclick='editFileContent({1})'>Edit</a></li></ul></div>" +
                        "</div>", importantClass, row["Id"], favoriteImgSrc, data);
                }
            },
            { data: "Owner", title: "Owner" },
            { data: "Modifier", title: "Modifier" },
            { data: "Size", title: "Size(kb)" },
            { data: "SecurityLevel", title: "SecurityLevel" },
            { data: "Version", title: "Version" },
            {
                data: "ModifiedDate",
                title: "ModifiedDate",
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
        if (children[0].ParentId === null) {
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
            $("#changefile .modal-footer center a:nth-child(2)").css("display", "");
            $("#changefile .modal-footer center a:nth-child(2)").attr("href", "ECMProtocol: " + fileUrl[1]);
        } else {
            $("#changefile .modal-footer center a:nth-child(2)").css("display", "none");
            $("#changefile .modal-footer center a:nth-child(2)").removeAttr("href");
        }

        $("#nameContent").val(fileUrl[2]);
        $("#changefile").modal("show");
    } catch (error) {
        console.log(error);
    }
}

async function getFileShareUrl(fileId) {
    var response = await api.get(String.format("FileInfo/GetFileShareUrl?id={0}", fileId));
    var fileUrl = "ECMProtocol: " + response.data;
    copyToClipboard(fileUrl);
}

function deleteFile() {
    var selectedFile = $(".checkbox input:checked").toArray();
    if (selectedFile.length > 0) {
        swal({
            title: "Are you sure?",
            text: "Once deleted, you will not be able to recover this imaginary file!",
            icon: "warning",
            buttons: true,
            dangerMode: true,
        })
            .then(async function (willDelete) {
                if (willDelete) {
                    fileIds = [];
                    for (var i = 0; i < selectedFile.length; i++) {
                        fileIds.push(parseInt($(selectedFile[i]).val()));
                    }
                    try {
                        if (isTrashUrl()) {
                            await api.post("Trash/CleanTrash", fileIds);
                        } else {
                            await api.post("Trash/AddFilesToTrash", fileIds);
                        }
                        router.navigateTo(window.location.pathname);
                        swal("Poof! Your imaginary file has been deleted!", { icon: "success" });
                    } catch (error) {
                        swal("Failed!", "Delete content failed, try again later!", "error");
                    }
                } else {
                    swal("You file is safe!");
                }
            });
    } else {
        swal("Please select a file want to delete!");
    }
}

async function recoverFile() {
    try {
        var selectedFile = $(".checkbox input:checked").toArray();
        var fileIds = [];
        for (var i = 0; i < selectedFile.length; i++) {
            fileIds.push(parseInt($(selectedFile[i]).val()));
        }
        await api.post("Trash/RecoverFile", fileIds);
        router.navigateTo(window.location.pathname);
        swal("Your file has been recover!", { icon: "success" });
    } catch (error) {
        swal("Failed!", "Recover content failed, try again later!", "error");
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
        router.navigateTo(window.location.pathname);
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
    var isSuccess = false;
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
            DirectoryId: dir.Id,
            SecurityLevel: $("#security-option").find(".active").text().trim()
        }
        try {
            var response = await api.post("FileInfo/AddNewFile", fileInfo);
            result.push(response.data);
            isSuccess = true
        } catch (error) {
            var isSuccess = false;
        }
    }
    if (isSuccess) {
        swal("Success!", "Create content successfully!", "success");
        $("#addNew").modal("hide");
        return result;
    }
    swal("Failed!", "add file failed, check your input and try again!", "error");
    return null;
}

async function getDirFromPath(path) {
    try {
        var response = await api.get(String.format("Directory/GetDirectoryFromPath?path={0}", path));
        return response.data;
    } catch (error) {
        console.log(error);
    }
}

$("#tab3C > a > img").click(async function () {
    var empName = $("#tab3C > input[name='empName']").val();
    try {
        var response = await api.get(String.format("Employee/GetEmployeesByName?empName={0}", empName));
        var emps = response.data;
        $("#userList").html("");
        $(emps).each(function (index, value) {
            var checkElem = $(".positionSearch").find("table input[data-emp-id='" + value.Id + "']").length > 0;
            if (!checkElem) {
                $("#userList").append(
                    String.format("<tr>" +
                        "<td>" +
                        "<input type='checkbox' name='name' value='' data-emp-id='{0}' onchange=\"addNewClass({0})\" />" +
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

async function initDepartments() {
    try {
        var response = await api.get("Department/GetDepartments");
        var depts = response.data;
        $(depts).each(function (index, value) {
            var nextElemId = index + 1 <= depts.length - 1 ? depts[index + 1].Id : null;
            $("#organList").append(String.format(
                "<tr class='dept-list' data-dept-id='{0}'>" +
                "<td class= 'bdr0'>" +
                "<div>" +
                "<label>" +
                "<input type='checkbox' onclick='selectEmpsInDept({0}, {2})'>" +
                "</label>" +
                "</div>" +
                "</td>" +
                "<td class='name-dept'>" +
                "<strong>{1}</strong>" +
                "<a href='#' onclick='showUser({0})'>" +
                "<img src='/assets/imgs/btn_show_peo.gif' />" +
                "</a>" +
                "</td>" +
                "</tr>", value.Id, value.Name, nextElemId
            ));
        });
    } catch (error) {
        console.log(error);
    }
}

async function showUser(deptId) {
    try {
        var response = await api.get("Employee/GetEmployeesByDeptId?deptId=" + deptId);
        var emps = response.data;
        $(emps).each(function (index, value) {
            var elem = $("#organList").find("tr[data-emp-id='" + value.Id + "']");
            if (elem.length === 0) {
                $("#organList tr[data-dept-id='" + deptId + "']").after(String.format("<tr class='groupUser activeUser' data-emp-id='{0}'>" +
                    "<td class='bdr1'>" +
                    "<div>" +
                    "<label>" +
                    "<input type='checkbox' onchange='changeActiveStatus(this)'>" +
                    "</label>" +
                    "</div>" +
                    "</td>" +
                    "<td>" +
                    "<div class='d_tooltip'>{1}</div>" +
                    "</td>" +
                    "</tr>", value.Id, String.format("{0} {1}", value.LastName, value.FirstName)));
            } else {
                elem.toggleClass("activeUser");
            }
        });
    } catch (error) {
        console.log(error);
    }
}

async function selectEmpsInDept(fromId, toId) {
    var fromElem = $("tr[data-dept-id='" + fromId + "']");
    var toElem = $("tr[data-dept-id='" + toId + "']");
    var empElems = fromElem.nextUntil(toElem);
    var isRootChecked = fromElem.find("input:checkbox").prop("checked");

    if (empElems.length === 0) {
        var response = await api.get("Employee/GetEmployeesByDeptId?deptId=" + fromId);
        var emps = response.data;
        $(emps).each(function (index, value) {
            var elem = $("#organList").find("tr[data-emp-id='" + value.Id + "']");
            if (elem.length === 0) {
                $("#organList tr[data-dept-id='" + fromId + "']").after(String.format("<tr class='groupUser' data-emp-id='{0}'>" +
                    "<td class='bdr1'>" +
                    "<div>" +
                    "<label>" +
                    "<input type='checkbox' onchange='changeActiveStatus(this)'>" +
                    "</label>" +
                    "</div>" +
                    "</td>" +
                    "<td>" +
                    "<div class='d_tooltip'>{1}</div>" +
                    "</td>" +
                    "</tr>", value.Id, String.format("{0} {1}", value.LastName, value.FirstName)));
            }
        });
        empElems = fromElem.nextUntil(toElem);
    }

    if (empElems.length > 0) {
        $(empElems).each(function (index, value) {
            var elem = $(empElems[index]).find("input:checkbox");
            elem.prop("checked", isRootChecked);
            elem.change();
        });
    }
}