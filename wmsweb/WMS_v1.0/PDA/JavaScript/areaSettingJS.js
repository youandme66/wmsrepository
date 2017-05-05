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


var btn_delete = document.getElementsByClassName("btn_delete");
var div_togger3 = document.getElementById("div_togger3");
for (var i = 0; i < btn_delete.length; i++) {
    btn_delete[i].index = i;
    btn_delete[i].onclick = function () {
        zindex.style.display = "block";
        div_togger3.style.display = "block";

        var table = document.getElementById("Line_Table");
        var tr = table.getElementsByTagName("tr")[this.index + 1];
        var td = tr.getElementsByTagName("td")[0];
        document.getElementById("lab").value = td.innerHTML;
        document.getElementById("Lable1").innerHTML = td.innerHTML;

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


        document.getElementById("region_key2").value = td[0].innerHTML;
        document.getElementById("region_name2").value = td[1].innerHTML;

        var option = document.getElementById("subinventory_name2").getElementsByTagName("option");
        for (var i = 0; i < option.length; i++) {
            if (option[i].innerHTML == td[2].innerHTML) {
                option[i].setAttribute("selected", "selected");
            }
        }

        document.getElementById("create_by2").value = td[3].innerHTML;
        document.getElementById("create_time2").value = td[4].innerHTML;
        document.getElementById("enabled2").value = td[7].innerHTML;
        document.getElementById("description2").value = td[8].innerHTML;

    }
}

        $(document).ready(function () {
            $.ajax({
                async: false,
                type: "POST",
                url: "WareHouse.ashx",
                dataType: "json",
                data: {
                    warehouse: $("#subinventory_name").val(),
                },
                success: function (data) {
                    for (var i = 0; i < data.length; i++) {
                        $("#subinventory_name").append("<option>" + data[i].Name + "</option>");
                    }
                }
            });
        });


        $(document).ready(function () {
            $.ajax({
                async: false,
                type: "POST",
                url: "WareHouse.ashx",
                dataType: "json",
                data: {
                    warehouse: $("#subinventory_name1").val(),
                },
                success: function (data) {
                    for (var i = 0; i < data.length; i++) {
                        $("#subinventory_name1").append("<option>" + data[i].Name + "</option>");
                    }
                }
            });
        });

        $(document).ready(function () {
            $.ajax({
                async: false,
                type: "POST",
                url: "WareHouse.ashx",
                dataType: "json",
                data: {
                    warehouse: $("#subinventory_name2").val(),
                },
                success: function (data) {
                    for (var i = 0; i < data.length; i++) {
                        $("#subinventory_name2").append("<option>" + data[i].Name + "</option>");
                    }
                }
            });
        });