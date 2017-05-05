var zindex = document.getElementById("zindex");
var div_togger1 = document.getElementById("div_togger1");
var div_togger2 = document.getElementById("div_togger2");
var div_togger3 = document.getElementById("div_togger3");
var div_togger4 = document.getElementById("div_togger4");
var div_togger5 = document.getElementById("div_togger5");
var div_togger6 = document.getElementById("div_togger6");


/*添加单头信息*/
var btn_insert = document.getElementById("btn_insert");
btn_insert.onclick = function () {
    zindex.style.display = "block";
    div_togger1.style.display = "block";
    getitem_name_list();

}


/*编辑单头信息*/
var Top_Edit = document.getElementsByClassName("Top_Edit");

for (var i = 0; i < Top_Edit.length; i++) {

    Top_Edit[i].index = i;

    Top_Edit[i].onclick = function () {

        zindex.style.display = "block";

        div_togger2.style.display = "block";

        var table = document.getElementById("Top_GridView");

        /*获取行的第一个单元格 */
        var child = table.getElementsByTagName("tr")[this.index + 1];

        /*获取选择行的所有列*/
        var SZ_col = child.getElementsByTagName("td");

        document.getElementById("edit_ship_key").value = SZ_col[0].innerHTML;
        document.getElementById("edit_customer").value = SZ_col[1].innerHTML;
        document.getElementById("edit_part_no").value = SZ_col[2].innerHTML;
        document.getElementById("edit_ship_no").value = SZ_col[3].innerHTML;
        document.getElementById("Top_request_qty").value = SZ_col[4].innerHTML;
        document.getElementById("Top_picked_qty").value = SZ_col[5].innerHTML;

    }
}


/*删除单头信息*/
var Top_Delete = document.getElementsByClassName("Top_Delete");

for (var i = 0; i < Top_Delete.length; i++) {
    Top_Delete[i].index = i;
    Top_Delete[i].onclick = function () {

        zindex.style.display = "block";

        div_togger3.style.display = "block";

        var table = document.getElementById("Top_GridView");

        /*获取行的第一个单元格 */
        var child = table.getElementsByTagName("tr")[this.index + 1];

        /*获取选择行的所有列*/
        var SZ_col = child.getElementsByTagName("td");

        document.getElementById("delete_id").value = SZ_col[0].innerHTML;
        document.getElementById("delete_customer").value = SZ_col[1].innerHTML;
        document.getElementById("delete_item_name1").value = SZ_col[2].innerHTML;
        document.getElementById("delete_request_qty").value = SZ_col[4].innerHTML;
        document.getElementById("delete_picked_qty1").value = SZ_col[5].innerHTML;
    }
}

///*插入单身信息*/
//var Insert_Line = document.getElementsByClassName("Insert_Line");

//for (var i = 0; i < Insert_Line.length; i++) {

//    Insert_Line[i].index = i;

//    Insert_Line[i].onclick = function () {

//        zindex.style.display = "block";

//        div_togger4.style.display = "block";

//        var table = document.getElementById("Top_GridView");

//        /*获取行的第一个单元格 */
//        var child = table.getElementsByTagName("tr")[this.index + 1];

//        /*获取选择行的所有列*/
//        var SZ_col = child.getElementsByTagName("td");

//        document.getElementById("insert_ship_key").value = SZ_col[0].innerHTML;

//        getwo_no_list(SZ_col[2].innerHTML);

//        $('#wo_no_list').change(function () {
//            //var wo_no = $(this).children('option:selected').val();//这就是selected的值 
//            getPart_no_By_wono();
//        });

//    }
//}

///*删除身表信息*/
//var Line_Delete = document.getElementsByClassName("Line_Delete");

//for (var i = 0; i < Line_Delete.length; i++) {
//    Line_Delete[i].index = i;
//    Line_Delete[i].onclick = function () {

//        zindex.style.display = "block";

//        div_togger5.style.display = "block";

//        var table = document.getElementById("Line_GridView");

//        /*获取行的第一个单元格 */
//        var child = table.getElementsByTagName("tr")[this.index + 1];

//        /*获取选择行的所有列*/
//        var SZ_col = child.getElementsByTagName("td");

//        document.getElementById("delete_ship_lines_key").value = SZ_col[0].innerHTML;
//        document.getElementById("delete_wono").value = SZ_col[2].innerHTML;
//        document.getElementById("delete_item_name2").value = SZ_col[3].innerHTML;
//        document.getElementById("delete_picked_qty").value = SZ_col[4].innerHTML;
//    }
//}

///*编辑身表信息*/
//var Line_Edit = document.getElementsByClassName("Line_Edit");

//for (var i = 0; i < Line_Edit.length; i++) {

//    Line_Edit[i].index = i;

//    Line_Edit[i].onclick = function () {

//        zindex.style.display = "block";

//        div_togger6.style.display = "block";

//        var table = document.getElementById("Line_GridView");

//        /*获取行的第一个单元格 */
//        var child = table.getElementsByTagName("tr")[this.index + 1];

//        /*获取选择行的所有列*/
//        var SZ_col = child.getElementsByTagName("td");

//        document.getElementById("ship_lines_key").value = SZ_col[0].innerHTML;
//        document.getElementById("last_picked_qty").value = SZ_col[4].innerHTML;
//        document.getElementById("edit_wono").value = SZ_col[2].innerHTML;
//        document.getElementById("edit_item_name").value = SZ_col[3].innerHTML;
//        document.getElementById("edit_picked_qty").value = SZ_col[4].innerHTML;

//    }
//}

/*关闭弹出框*/
var btn_close = document.getElementsByClassName("btn_close");
for (var i = 0; i < btn_close.length; i++) {
    btn_close[i].onclick = function () {
        zindex.style.display = "none";
        div_togger1.style.display = "none";
        div_togger2.style.display = "none";
        div_togger3.style.display = "none";
        div_togger4.style.display = "none";
        div_togger5.style.display = "none";
        div_togger6.style.display = "none";
    }
}

$(document).ready(function () {
    $.ajax({
        timeout: 3000,
        async: false,
        type: "POST",
        url: "getCustomerNames.ashx",
        dataType: "json",
        data: {
            warehouse: $("#customer_name_c").val(),
        },
        success: function (data) {
            for (var i = 0; i < data.length; i++) {
                $("#customer_name_c").append("<option>" + data[i].Name + "</option>");
                $("#insert_customer_name_c").append("<option>" + data[i].Name + "</option>");
            }
        }
    });
});


function getwo_no_list(item_name) {
    $.ajax({
        timeout: 3000,
        async: false,
        type: "POST",
        url: "getWono_by_itemname.ashx",
        dataType: "json",
        data: {
            "item_name": item_name,
        },
        success: function (data) {
            document.getElementById("wo_no_list").options.length = 0;
            if (data == null || data.length == 0) {
                $("#wo_no_list").append("<option>" + "无可用工单编号!" + "</option>");
            }
            else {
                $("#wo_no_list").append("<option>" + "请选择工单编号" + "</option>");
                for (var i = 0; i < data.length; i++) {
                    $("#wo_no_list").append("<option>" + data[i].Name + "</option>");
                }
            }
        }
    });
}

function getitem_name_list() {
    $.ajax({
        timeout: 3000,
        async: false,
        type: "POST",
        url: "workorder.ashx",
        dataType: "json",
        //data: {
        //    "item_name": item_name,
        //},
        success: function (data) {
            document.getElementById("insert_item_name_c").options.length = 0;
            if (data == null || data.length == 0) {
                $("#insert_item_name_c").append("<option>" + "无可用料号!" + "</option>");
            }
            else {

                for (var i = 0; i < data.length; i++) {
                    $("#insert_item_name_c").append("<option>" + data[i].Name + "</option>");
                }
            }
        }
    });
}

function getNew_ship_no() {
    $.ajax({
        timeout: 3000,
        async: false,
        type: "GET",
        url: "get_new_ship_no.ashx",
        //dataType: "json",
        success: function (data) {
            $("#top_insert_ship_no").val(data);
        }
    });
}

function getPart_no_By_wono() {
    wo_no = $('#wo_no_list option:selected').val();
    $.ajax({
        timeout: 3000,
        async: false,
        type: "POST",
        url: "get_prat_no_by_wono.ashx",
        dataType: "json",
        data: {
            "wo_no": wo_no,
        },
        success: function (data) {
            if (data.length>0)
                $("#insert_lines_part_no").val(data[0].Name);
            else $("#insert_lines_part_no").val("");
        }
    });
}