$(document).ready(async function () {
    try {
        const response = await api.get(`/directory`);
        $(".mycontList .treefdBox").html(renderTreeFolder(listToTree(response.data), "sidebar-menu1", true));
        $(".addFolderTreeBox").html(renderTreeFolder(listToTree(response.data), "sidebar-menu", false));
        $(".addFolderTreeBox2").html(renderTreeFolder(listToTree(response.data), "sidebar-menu2", false));
    } catch (error) {
        console.log(error);
    }
});

function initDataTable(url) {
    $("#tbMainDefault").DataTable({
        columnDefs: [{ orderable: false, targets: 0 }],
        order: [[1, "asc"]],
        processing: true,
        bFilter: true,
        searching: true,
        info: false,
        ajax: {
            url: url,
            dataSrc: "fileInfos"
        },
        columns: [
            {
                data: "Name", render: function (data, type, row) {
                    return `<div class="contentTitle">
                                <input type="checkbox" name="name" value="" />
                                <a class="addfavorite" onclick="AddFavorite(this)">
                                    <img src="/assets/imgs/ico_fav.png" alt="icon_start" />
                                </a>
                                <a href="/open-content-${row['Id']}"
                                   class="contentname">
                                    ${data}
                                </a>
                            </div>`
                }, searchable: true,
            },
            { data: "Owner", searchable: true, },
            { data: "Modifier" },
            { data: "Size" },
            { data: "SecurityLevel" },
            { data: "Version" },
            { data: "ModifiedDate", type: "date", render: function (data) { return data ? moment(data).format("DD/MM/yyyy") : ""; } }
        ]
    });
}

function listToTree(list) {
    var map = {}, node, roots = [], i;
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
    return `<ul ${childrenName ? `class='${childrenName}'` : ""}>` + children.map(node =>
        `<li class='treeview'>` +
        `<a ${renderLink ? `href='/get-by-dir-${node.Id}'` : ""}>` +
        "<img src='/assets/imgs/ico_folder_off.png' alt='Folder Image' /> " + node.Name +
        "</a>" + renderTreeFolder(node.Childrens, rootClass, renderLink) +
        "</li>").join('\n') +
        "</ul>";
}

function filterDTByFileType(type) {
    $.fn.dataTable.ext.search.pop()

    var extensions = []
    switch (type) {
        case "all":
            break;
        case "powerpoint": extensions = ["ppt", "pptx"];
            break;
        case "excel": extensions = ["xls", "xlsx"];
            break;
        case "word": extensions = ["doc", "docx"];
            break;
        case "pdf": extensions = ["pdf"];
            break;
        case "image": extensions = ["jpg", "gif", "jpg", "jpeg"];
            break;
        default: break;
    }

    $.fn.dataTable.ext.search.push(
        function (settings, data, dataIndex) {
            if (extensions.length === 0) {
                return true;
            }
            return extensions.includes(data[0].split('.').pop().trim());
        }
    );

    $("#tbMainDefault").DataTable().draw();
}