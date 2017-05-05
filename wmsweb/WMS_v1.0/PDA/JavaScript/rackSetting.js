var zindex = document.getElementById("zindex");

var btn_close = document.getElementsByClassName("btn_close");
for (var i = 0; i < btn_close.length; i++) {
    btn_close[i].onclick = function () {
        zindex.style.display = "none";
        div_togger1.style.display = "none";
        div_togger2.style.display = "none";
        div_togger3.style.display = "none";

        Subinventory.selectedIndex = "0";
        $("#Region_key").empty();
        $("#Region_key").append("<option>--------select--------</option>");
    }
}

window.onload = function () {

    var btn_insert = document.getElementById("btn_insert");
    var div_togger1 = document.getElementById("div_togger1");
    btn_insert.onclick = function () {
        zindex.style.display = "block";
        div_togger1.style.display = "block";

        //var Create_by = document.getElementById("Create_by");
        var userName = document.getElementById("LabelUser");
        //Create_by.value = userName.innerHTML;
    }
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

        document.getElementById("Frame_key2").value = td[0].innerHTML;
        document.getElementById("Label2").innerHTML = td[0].innerHTML;

        document.getElementById("Frame_name2").value = td[1].innerHTML;
        document.getElementById("Enabled2").value = td[2].innerHTML;

        var option = document.getElementById("Subinventory2").getElementsByTagName("option");

        $("#Subinventory2").attr("name", "Subinventory2");
        for (var i = 0; i < option.length; i++) {
            if (option[i].innerHTML == td[4].innerHTML) {
                Subinventory2.selectedIndex = i;
            }
        }
        $("#Region_key2").empty();
        $("#Region_key2").append("<option>" + td[6].innerHTML + "</option>")

        document.getElementById("Create_by2").value = td[8].innerHTML;
        document.getElementById("Label3").innerHTML = td[8].innerHTML;

        document.getElementById("Description2").value = td[13].innerHTML;
        
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
        document.getElementById("Label1").innerHTML = td[0].innerHTML;
        document.getElementById("lab").value = td[0].innerHTML;

        document.getElementById("enable").value = td[2].innerHTML;
    }
}


$(document).ready(function () {
    $.ajax({
        async: false,
        type: "POST",
        url: "WareHouse.ashx",
        dataType: "json",
        data: {
            warehouse: $("#Subinventory").val(),
        },
        success: function (data) {
            for (var i = 0; i < data.length; i++) {
                $("#Subinventory").append("<option>" + data[i].Name + "</option>");
            }
        }
    });
});
$("#Region_key").attr("style","height:21px");
//二级联动，选择库别，改变区域选项
$("#Subinventory").change(function () {
    
    if ($("#Subinventory").find("option:selected").text() != "--------select--------") {
        
        $.ajax({
            async: false,
            type: "POST",
            url: "LD_warehouse.ashx",
            dataType: "json",
            data: {
                warehouse_name: $("#Subinventory option:selected").text(),
            },
            success: function (data) {
                $("#Region_key").empty();
                for (var i = 0; i < data.length; i++) {
                    $("#Region_key").append("<option>" + data[i].Name + "</option>");
                }
            }
        });
        
    }
});


//$(document).ready(function () {
//    $.ajax({
//        async: false,
//        type: "POST",
//        url: "WareHouse.ashx",
//        dataType: "json",
//        data: {
//            warehouse: $("#Subinventory").val(),
//        },
//        success: function (data) {
//            for (var i = 0; i < data.length; i++) {
//                $("#Subinventory").append("<option>" + data[i].Name + "</option>");
//            }
//        }
//    });
//});

//$(document).ready(function () {
//    $.ajax({
//        async: false,
//        type: "POST",
//        url: "getArea.ashx",
//        dataType: "json",
//        data: {
//            warehouse: $("#Region_key").val(),
//        },
//        success: function (data) {
//            for (var i = 0; i < data.length; i++) {
//                $("#Region_key").append("<option>" + data[i].Name + "</option>");
//            }
//        }
//    });
//});


$(document).ready(function () {
    $.ajax({
        async: false,
        type: "POST",
        url: "WareHouse.ashx",
        dataType: "json",
        data: {
            warehouse: $("#Subinventory2").val(),
        },
        success: function (data) {
            for (var i = 0; i < data.length; i++) {
                $("#Subinventory2").append("<option>" + data[i].Name + "</option>");
            }
        }
    });
});
$("#Region_key2").attr("style","height:21px");
$("#Subinventory2").click(function () {
    $.ajax({
        async: false,
        type: "POST",
        url: "LD_warehouse.ashx",
        dataType: "json",
        data: {
            warehouse_name: $("#Subinventory2 option:selected").text(),
        },
        success: function (data) {
            $("#Region_key2").empty();
            for (var i = 0; i < data.length; i++) {
                $("#Region_key2").append("<option>" + data[i].Name + "</option>");
            }
        }
    });
});




