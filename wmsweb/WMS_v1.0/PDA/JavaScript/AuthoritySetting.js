var zindex = document.getElementById("zindex");

var btn_close = document.getElementsByClassName("btn_close");
for (var i = 0; i < btn_close.length; i++) {
    btn_close[i].onclick = function () {
        zindex.style.display = "none";
        div_togger1.style.display = "none";
        div_togger2.style.display = "none";
        div_togger3.style.display = "none";
    }
}

var btn_insert = document.getElementById("btn_insert");
var div_togger1 = document.getElementById("div_togger1");
btn_insert.onclick = function () {
    zindex.style.display = "block";
    div_togger1.style.display = "block";
}

var btn_update = document.getElementsByClassName("btn_update");
var div_togger2 = document.getElementById("div_togger2");
for (var i = 0; i < btn_update.length; i++) {
    btn_update[i].index = i;
    btn_update[i].onclick = function () {
        zindex.style.display = "block";
        div_togger2.style.display = "block";

        var table = document.getElementById("Line_Table");
        var tr = table.getElementsByTagName("tr")[this.index + 1];
        var td = tr.getElementsByTagName("td");
        document.getElementById("update_program_id").value = td[0].innerHTML;
        document.getElementById("update_user_id_authority").value = td[1].innerHTML;
        document.getElementById("update_program_id_Authority").value = td[2].innerHTML;
        document.getElementById("update_select_id_Authority").value = td[3].innerHTML;


    }
}

var btn_delete = document.getElementsByClassName("btn_delete");
var div_togger3 = document.getElementById("div_togger3");
for (var i = 0; i < btn_delete.length; i++) {
    btn_delete[i].index = i;
    btn_delete[i].onclick = function () {
        zindex.style.display = "block";
        div_togger3.style.display = "block";

        var table = document.getElementById("Line_Table");
        var tr = table.getElementsByTagName("tr")[this.index + 1];
        var td = tr.getElementsByTagName("td");
        document.getElementById("delete_user_id_authority").value = td[0].innerHTML;
        document.getElementById("delete_user_name_authority").value = td[1].innerHTML;
        document.getElementById("delete_program_name_authority").value = td[2].innerHTML;
    }
}





$(document).ready(function () {
    $.ajax({
        async: false,
        type: "POST",
        url: "getUserName.ashx",
        dataType: "json",
        data: {
            warehouse: $("#user_id_Authority").val(),
        },
        success: function (data) {
            for (var i = 0; i < data.length; i++) {
                $("#user_id_Authority").append("<option>" + data[i].Name + "</option>");
            }
        }
    });
});



$(document).ready(function () {
    $.ajax({
        async: false,
        type: "POST",
        url: "getProgramName.ashx",
        dataType: "json",
        data: {
            warehouse: $("#program_id_Authority").val(),
        },
        success: function (data) {
            for (var i = 0; i < data.length; i++) {
                $("#program_id_Authority").append("<option>" + data[i].Name + "</option>");
            }
        }
    });
});

$(document).ready(function () {
    $.ajax({
        async: false,
        timeout: 3000,
        type: "POST",
        url: "getUserName.ashx",
        dataType: "json",
        data: {
            warehouse: $("#insert_user_id_Authority").val(),
        },
        success: function (data) {
            for (var i = 0; i < data.length; i++) {
                $("#insert_user_id_Authority").append("<option>" + data[i].Name + "</option>");
            }
        }
    });
});

$(document).ready(function () {
    $.ajax({
        async: false,
        type: "POST",
        url: "getProgramName.ashx",
        dataType: "json",
        data: {
            warehouse: $("#insert_program_id_Authority").val(),
        },
        success: function (data) {
            for (var i = 0; i < data.length; i++) {
                $("#insert_program_id_Authority").append("<option>" + data[i].Name + "</option>");
            }
        }
    });
});


$(document).ready(function () {
    $.ajax({
        async: false,
        type: "POST",
        url: "getUserName.ashx",
        dataType: "json",
        data: {
            warehouse: $("#update_user_id_authority").val(),
        },
        success: function (data) {
            for (var i = 0; i < data.length; i++) {
                $("#update_user_id_authority").append("<option>" + data[i].Name + "</option>");
            }
        }
    });
});

$(document).ready(function () {
    $.ajax({
        async: false,
        type: "POST",
        url: "getProgramName.ashx",
        dataType: "json",
        data: {
            warehouse: $("#update_program_id_Authority").val(),
        },
        success: function (data) {
            for (var i = 0; i < data.length; i++) {
                $("#update_program_id_Authority").append("<option>" + data[i].Name + "</option>");
            }
        }
    });
});




