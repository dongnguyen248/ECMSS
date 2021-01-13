$(document).ready(async function () {
    try {
        const response = await api.get(`/directory`);
        console.log(listToTree(response.data))
    } catch (error) {
        console.log(error);
    }
});

function initDataTable(url) {
    var table = $("#tbMainDefault").DataTable({
        columnDefs: [{ orderable: false, targets: 0 }],
        order: [[1, "asc"]],
        bFilter: false,
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
                                <a href="/open-content/${row['Id']}"
                                   class="contentname">
                                    ${data}
                                </a>
                            </div>`
                }
            },
            { data: "Employee.EpLiteId" },
            { data: "FileHistory.Employee.EpLiteId" },
            { data: "FileHistory.Size" },
            { data: "SecurityLevel" },
            { data: "FileHistory.Version" },
            { data: "FileHistory.ModifiedDate", type: "date", render: function (data) { return data ? moment(data).format("DD/MM/yyyy") : ""; } }
        ],
    });
    return table;
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