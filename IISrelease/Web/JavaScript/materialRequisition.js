$(document).ready(function () {
    $.ajax({
        timeout: 3000,
        async: false,
        type: "POST",
        url: "WareHouse.ashx",
        dataType: "json",
        data: {
            warehouse: $("#issued_sub_key_c").val(),
        },
        success: function (data) {
            for (var i = 0; i < data.length; i++) {
                $("#issued_sub_key_c").append("<option>" + data[i].Name + "</option>");
            }
        }
    });
});



$(document).ready(function () {
    $.ajax({
        timeout: 3000,
        async:false,
        type: "POST",
        url: "operation_seq.ashx",
        dataType: "json",
        data: {
            warehouse: $("#operation_seq_num_c").val(),
        },
        success: function (data) {
            for (var i = 0; i < data.length; i++) {
                $("#operation_seq_num_c").append("<option>" + data[i].Name + "</option>");
            }
        }
    });
});



$(document).ready(function () {
    $.ajax({
        timeout: 3000,
        async: false,
        type: "POST",
        url: "WO_NO.ashx",
        dataType: "json",
        data: {
            warehouse: $("#wo_no_c").val(),
        },
        success: function (data) {
            for (var i = 0; i < data.length; i++) {
                $("#wo_no_c").append("<option>" + data[i].Name + "</option>");
            }
        }
    });
});



$(document).ready(function () {
    $.ajax({
        timeout:3000,
        async: false,
        type: "POST",
        url: "frame.ashx",
        dataType: "json",
        data: {
            warehouse: $("#frame_key_c").val(),
        },
        success: function (data) {
            for (var i = 0; i < data.length; i++) {
                $("#frame_key_c").append("<option>" + data[i].Name + "</option>");
            }
        }
    });
});