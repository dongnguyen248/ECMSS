$(document).ready(function () {
    //hide side bar
    $("#btn_area").click(function () {
        var padding = $("#ecmcontent").css("padding-left");

        if (padding === "200px") {
            $("#ecmcontent").css({ "padding-left": "0px" });
        } else {
            $("#ecmcontent").css({ "padding-left": "200px" });
        }
    });
    //show all folder for select location
    $("#showAllFolder").click(function () {
        $(".listFolder").slideToggle("fast");
    });
    $("#showFolder").click(function () {
        $(".listFolder1").slideToggle("fast");
    });
    //config tabs on homepage
    $("#tabs li a:not(:first)").addClass("inactive");
    $(".tab_on").hide();
    $(".tab_on:first").show();

    $("#tabs li a").click(function () {
        var t = $(this).attr("id");
        if ($(this).hasClass("inactive")) {
            $("#tabs li a").addClass("inactive");
            $(this).removeClass("inactive");

            $(".tab_on").hide();
            $("#" + t + "C").fadeIn("slow");
        }
    });

    //change Tab on  add new file modal
    $("#tabs2 li a:not(:first)").addClass("inactive");
    $(".tabcontent").hide();
    $(".tabcontent:first").show();

    $("#tabs2 li a").click(function () {
        var t = $(this).attr("id");
        if ($(this).hasClass("inactive")) {
            $("#tabs2 li a").addClass("inactive");
            $(this).removeClass("inactive");

            $(".tabcontent").hide();
            $("#" + t + "C").fadeIn("slow");
        }
    });
});

$(document).ajaxStop(function () {
    //check extension when show file
    $(".contentname").each(function () {
        var text = $(this).text();
        var fileExtension = text.split(".").pop().trim();
        if (fileExtension === "doc" || fileExtension === "docx") {
            backgroundIcon = "/assets/imgs/ico_doc_on.png";
        } else if (fileExtension === "xls" || fileExtension === "xlsx") {
            backgroundIcon = "/assets/imgs/ico_xlsx_on.png";
        } else if (fileExtension === "ppt" || fileExtension === "pptx") {
            backgroundIcon = "/assets/imgs/ico_ppt_on.png";
        } else if (
            fileExtension === "jpg" ||
            fileExtension === "gif" ||
            fileExtension === "jpg" ||
            fileExtension === "jpeg"
        ) {
            backgroundIcon = "/assets/imgs/ico_img_on.png";
        } else {
            backgroundIcon = "/assets/imgs/ico_pdf_on.png";
        }
        $(this).css("background-image", "url(" + backgroundIcon + ")");
    });
});

function addnewclass(id) {
    var checkbox = $("#" + id);
    var checked = checkbox.prop("checked");
    if (checked) {
        checkbox.parent().parent().addClass("checked");
    } else {
        checkbox.parent().parent().removeClass("checked");
    }
}
function addnew() {
    $("#addnew").modal("show");
}

function createnew() {
    $("#createnew").modal("show");
}

// btn move left move right
var $bottom_each = $(".safe_btn_box a");
var $bottom_select = $(".subsecondL");
$bottom_each.click(function () {
    $bottom_each.removeClass("active");
    $(this).addClass("active");
});
$bottom_select.click(function () {
    $bottom_select.removeClass("active");
    $(this).addClass("active");
});

function moveToRight(id) {
    $("#userList .checked").appendTo("#" + id + " table tbody");
}
function moveToLeft(id) {
    $("#" + id + " .checked").appendTo("#userList");
}
$("#listAllcheck").click(function () {
    var checkbox = $("#userList input:checkbox")
        .not(this)
        .prop("checked", this.checked);
    var checked = checkbox.prop("checked");
    if (checked) {
        $("#userList tr").addClass("checked");
    } else {
        $("#userList tr").removeClass("checked");
    }
});

function deleteAllSelect(id) {
    $("#" + id + " tr").remove();
}

function moveToUp(idFrom, idTo) {
    $("#" + idFrom + " .checked").appendTo("#" + idTo);
}
function moveToDown(idFrom, idTo) {
    $("#" + idFrom + " .checked").appendTo("#" + idTo);
}
//open folder and Change image folder
$(document).on("click", ".sidebar-menu li", function (e) {
    e.stopPropagation();
    $(".sidebar-menu").find("span").remove();
    $(this).toggleClass("active");
    if ($(this).hasClass("active")) {
        $(this).children().children().attr("src", "/assets/imgs/ico_folder_on.png");
        $(
            '<span onclick=selectFolder("#folderPath",".sidebar-menu") class="btnMove" id="btnGetPath" >Select <i class="fas fa-angle-right"></i></span>'
        ).insertAfter($(this).children("a"));
    } else {
        $(this)
            .children()
            .children()
            .attr("src", "/assets/imgs/ico_folder_off.png");
        $(this).children("span").remove();
    }
});

$(document).on("click", ".sidebar-menu2 li", function (e) {
    e.stopPropagation();
    $(".sidebar-menu2").find("span").remove();
    $(this).toggleClass("active");
    if ($(this).hasClass("active")) {
        $(this).children().children().attr("src", "/assets/imgs/ico_folder_on.png");
        $(
            '<span onclick=selectFolder("#folderPath2",".sidebar-menu2") class="btnMove" id="btnGetPath" >Select <i class="fas fa-angle-right"></i></span>'
        ).insertAfter($(this).children("a"));
    } else {
        $(this)
            .children()
            .children()
            .attr("src", "/assets/imgs/ico_folder_off.png");
        $(this).children("span").remove();
    }
});

$(document).on("click", ".sidebar-menu1 li", function (e) {
    e.stopPropagation();
    $(".sidebar-menu").find("span").remove();
    $(this).toggleClass("active");
    if ($(this).hasClass("active")) {
        $(this).children().children().attr("src", "/assets/imgs/ico_folder_on.png");
    } else {
        $(this)
            .children()
            .children()
            .attr("src", "/assets/imgs/ico_folder_off.png");
        $(this).children("span").remove();
    }
});

var tempPath = "";
function dfs(srcElem, desElem) {
    $(srcElem)
        .children()
        .each(function () {
            if ($(this).find(desElem).length > 0) {
                tempPath +=
                    $(this)
                        .clone()
                        .children(":not(a)")
                        .remove()
                        .end()
                        .children("a")
                        .text()
                        .trim() + ">";
            }
            dfs($(this), desElem);
        });
    tempPath = tempPath.replace(">>", ">");
    return tempPath;
}

function selectFolder(id, cls) {
    var desElem = $("#btnGetPath");
    var path = "PoscoVST>" + dfs($(cls), desElem);
    $(id).val(path.slice(0, -1));
    tempPath = "";
}

//upload file
var seq = 1;
$("#fileupload").click(function () {
    var inpID = "inputfile" + seq;
    var inpFullID = "#" + inpID;
    var optId = "file" + seq;
    //Append 1 new input element
    $("#inputhidden").append(
        "<input type='file' name='' class='inpImport' id='" + inpID + "' />"
    );

    //call event click of button
    $(inpFullID).click();

    //call event Change(affter select file) of input
    $(inpFullID).change(function () {
        let image = event.target.files[0];
        let filename = event.target.files[0].name;
        let checkExists = 0;
        var elements = document.getElementsByClassName("inpImport");

        if (elements.length > 1) {
            for (var i = 0; i < elements.length; i++) {
                if (filename === getFilename(elements[i].value)) {
                    checkExists++;
                    if (checkExists > 1) {
                        alert("File already upload!");
                        $("#" + inpID).remove();
                        break;
                    }
                }
            }
        }
        if (checkExists <= 1) {
            changebackgroundFilextension(filename, optId, inpID);
        }
    });

    seq++;
});

function changebackgroundFilextension(filename, optId, inpID) {
    let backgroundIcon = "";
    $(".listFileImport").css("display", "block");
    $(".listFileImport .list").append(
        String.format("<li id={0}>{1} <a onclick=\"removefile('{0}','{2}')\" class='btnfloatR'><img src='/assets/imgs/ico_go_rcb.png'/></a></li>", optId, filename, inpID));
    let extension = getFileExtension(filename);

    if (extension === "doc" || extension === "docx") {
        backgroundIcon = "/assets/imgs/ico_doc_on.png";
    } else if (extension === "xls" || extension === "xlsx") {
        backgroundIcon = "/assets/imgs/ico_xlsx_on.png";
    } else if (extension === "ppt" || extension === "pptx") {
        backgroundIcon = "/assets/imgs/ico_ppt_on.png";
    } else if (
        extension === "jpg" ||
        extension === "gif" ||
        extension === "jpg" ||
        extension === "jpeg"
    ) {
        backgroundIcon = "/assets/imgs/ico_img_on.png";
    } else {
        backgroundIcon = "/assets/imgs/ico_pdf_on.png";
    }
    $("#" + optId).css("background-image", "url(" + backgroundIcon + ")");
}

function removefile(optionID, inpID) {
    $("#" + optionID).remove();
    $("#" + inpID).remove();
    checkElementInUl();
}

function checkElementInUl() {
    if ($(".list").children().length === 0) {
        $(".listFileImport").css("display", "none");
    }
}

function getFileExtension(filename) {
    return filename.split(".").pop();
}

function getFilename(fullPath) {
    if (fullPath) {
        var startIndex =
            fullPath.indexOf("\\") >= 0
                ? fullPath.lastIndexOf("\\")
                : fullPath.lastIndexOf("/");
        var filename = fullPath.substring(startIndex);
        if (filename.indexOf("\\") === 0 || filename.indexOf("/") === 0) {
            filename = filename.substring(1);
        }
        return filename;
    }
}

function addFavorite(obj, fileId) {
    api.post("FileFavorite/AddOrRemoveFavoriteFile?fileId=" + fileId + "&employeeId=" + EMPLOYEE_ID).then(function () {
        var img = obj.children;
        var src =
            $(img).attr("src") === "/assets/imgs/ico_fav.png"
                ? "/assets/imgs/ico_fav_blue_on.png"
                : "/assets/imgs/ico_fav.png";
        $(img).attr("src", src);
    }).catch(function (error) {
        console.log(error)
    });
}

function addImportant(obj, fileId) {
    api.post("FileImportant/AddOrRemoveImportantFile?fileId=" + fileId + "&employeeId=" + EMPLOYEE_ID).then(function () {
        if (!$(obj).hasClass('backgroundImp')) {
            $(obj).addClass('backgroundImp');
        } else {
            $(obj).removeClass('backgroundImp');
        }
    }).catch(function (error) {
        console.log(error);
    });
}

function searchContent() {
    var inpTxt = $("#txtSearch").val().trim();
    if (inpTxt.length > 0) {
        $("#tbMainDefault").DataTable().ajax.url("api/FileInfo/Search?searchContent=" + inpTxt).load();
    } else {
        $("#tbMainDefault").DataTable().ajax.url(ROOT_DT_URL).load();
    }
}

$("#tab1C a").click(function () {
    var txt = $(this).text();
    $(".areacBox .txt p").text("Shortcut Box>" + txt);
});

$(document).on("click", "#tab2C .sidebar-menu1 a", function () {
    var root = $(".sidebar-menu1");
    var desElem = $(this);
    var path = dfs(root, desElem);
    $(".areacBox .txt p").text(path);
    tempPath = "";
});

$("#frm-create-directory").submit(function (event) {
    event.preventDefault();
    var dirName = $(this).find("input[name='dirName']").val();
    var parentName = $(this).find("input[name='parentName']").val();
    parentName = parentName.substring(parentName.lastIndexOf(">") + 1).trim();
    api.post("Directory/CreateDirectory?dirName=" + dirName + "&parentName=" + parentName).then(function () {
        initTreeFolder();
        $.notify("Create content successfully", "success");
    }).catch(function () {
        $.notify("Create content failed, check your input and try again", "error");
    });
});