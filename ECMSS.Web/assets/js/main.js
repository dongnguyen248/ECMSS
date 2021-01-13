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

    //check extension when show file
    $(".contentname").each(function () {
        var text = $(this).text();
        var lastFour = text.substr(text.length - 4);
        var lastThree = text.substr(text.length - 3);
        if (lastThree === "doc" || lastFour === "docx") {
            backgroundIcon = "/assets/imgs/ico_doc_on.png";
        } else if (lastThree === "xls" || lastFour === "xlsx") {
            backgroundIcon = "/assets/imgs/ico_xlsx_on.png";
        } else if (lastThree === "ppt" || lastFour === "pptx") {
            backgroundIcon = "/assets/imgs/ico_ppt_on.png";
        } else if (
            lastThree === "jpg" ||
            lastThree === "gif" ||
            lastThree === "jpg" ||
            lastFour === "jpeg"
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
$(".sidebar-menu li").on("click", function (e) {
    e.stopPropagation();
    $(".sidebar-menu").find("span").remove();
    $(this).toggleClass("active");
    if ($(this).hasClass("active")) {
        $(this).children().children().attr("src", "/assets/imgs/ico_folder_on.png");
        $(
            '<span onclick=selectFolder(this) class="btnMove" id="btnGetPath" >Select <i class="fas fa-angle-right"></i></span>'
        ).insertAfter($(this).children("a"));
    } else {
        $(this)
            .children()
            .children()
            .attr("src", "/assets/imgs/ico_folder_off.png");
        $(this).children("span").remove();
    }
});

$(".sidebar-menu1 li").on("click", function (e) {
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

var text = "PoscoVST>";
function dfs(elem, destinationClass) {
    $(elem)
        .children()
        .each(function () {
            if ($(this).find("#btnGetPath").length > 0) {
                text +=
                    $(this)
                        .clone()
                        .children(":not(a)")
                        .remove()
                        .end()
                        .children("a")
                        .text()
                        .trim() + ">";
            }
            dfs($(this), destinationClass);
        });
    text = text.replace(">>", ">");
    return text;
}

function selectFolder(obj) {
    var temp = dfs($(".sidebar-menu"), $(obj).attr("id"));
    $(".folderPath").val(temp.slice(0, -1));
    text = "PoscoVST>";
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
        `<li id=${optId}>${filename} <a onclick='removefile("${optId}","${inpID}")' class='btnfloatR'><img src='/assets/Content/img/ico_go_rcb.png'/></a></li>`
    );
    let extension = getFileExtension(filename);

    if (extension === "doc" || extension === "docx") {
        backgroundIcon = "/assets/Content/img/ico_doc_on.png";
    } else if (extension === "xls" || extension === "xlsx") {
        backgroundIcon = "/assets/Content/img/ico_xlsx_on.png";
    } else if (extension === "ppt" || extension === "pptx") {
        backgroundIcon = "/assets/Content/img/ico_ppt_on.png";
    } else if (
        extension === "jpg" ||
        extension === "gif" ||
        extension === "jpg" ||
        extension === "jpeg"
    ) {
        backgroundIcon = "/assets/Content/img/ico_img_on.png";
    } else {
        backgroundIcon = "/assets/Content/img/ico_pdf_on.png";
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

function AddFavorite(id) {
    var img = id.children;
    var src =
        $(img).attr("src") === "/assets/imgs/ico_fav.png"
            ? "/assets/imgs/ico_fav_blue_on.png"
            : "/assets/imgs/ico_fav.png";
    $(img).attr("src", src);
}