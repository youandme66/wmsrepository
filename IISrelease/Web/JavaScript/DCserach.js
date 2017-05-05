
//var zindex = document.getElementById("zindex");

//var btn_close = document.getElementsByClassName("btn_close");
//for (var i = 0; i < btn_close.length; i++) {
//    btn_close[i].onclick = function () {
//        zindex.style.display = "none";
//        div_togger1.style.display = "none";
        
//    }
//}

//var btn_insert = document.getElementById("btn_insert");
//var div_togger1 = document.getElementById("div_togger1");
//btn_insert.onclick = function () {
//    zindex.style.display = "block";
//    div_togger1.style.display = "block";
//}
$(document).ready(function () {
    $.ajax({
        timeout: 3000,
        async: false,
        type: "POST",
        url: "workorder.ashx",
        dataType: "json",
        data: {
            warehouse: $("#item_name").val(),
        },
        success: function (data) {
            for (var i = 0; i < data.length; i++) {
                $("#item_name").append("<option>" + data[i].Name + "</option>");
            }
        }
    });
});


function showDetial(obj) {



    var Top_Table = document.getElementById("Top_Table");

    var Top_Tr = Top_Table.getElementsByTagName("tr");

    console.log(obj);

    var FinalTr = obj.parentNode;

    var Top_Td = FinalTr.getElementsByTagName("td");

    for (var i = 0; i < Top_Tr.length; i++) {

        Top_Tr[i].setAttribute("class", "");
    }

    var Line_Table = document.getElementById("Line_Table");

    var Line_tr = Line_Table.getElementsByTagName("tr");

    for (var i = 1; i < Line_tr.length; i++) {

        Line_tr[i].setAttribute("class", "tr_hide");
    }

    /*重点：GYM
            当需要带出单身时，通过判断来显示其对应的单身
            Top_Td[0]是库存总表中的第一个显示值
            Line_Td[1]是库存总表中的第二个显示值

    */
    for (var i = 1; i < Line_tr.length; i++) {

        var Line_Td = Line_tr[i].getElementsByTagName("td");

        if ((Line_Td[0].innerHTML == Top_Td[1].innerHTML) && (Line_Td[2].innerHTML == Top_Td[0].innerHTML)) {

            Line_tr[i].setAttribute("class", "tr_show");

        }
    }

}
