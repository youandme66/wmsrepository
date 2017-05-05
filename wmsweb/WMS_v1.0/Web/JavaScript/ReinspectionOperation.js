//$(document).ready(function () {
//    $.ajax({
//        async: false,
//        type: "POST",
//        url: "getPnHeader.ashx",
//        dataType: "json",
//        data: {
//            warehouse: $("#select_name_reinspect").val(),
//        },
//        success: function (data) {
//            for (var i = 0; i < data.length; i++) {
//                $("#select_name_reinspect").append("<option>" + data[i].Name + "</option>");
//            }
//        }
//    });
//});


$(document).ready(function () {
    $.ajax({
        async: false,
        type: "POST",
        url: "frame.ashx",
        dataType: "json",
        data: {
            warehouse: $("#frame_name_reinspect").val(),
        },
        success: function (data) {
            for (var i = 0; i < data.length; i++) {
                $("#frame_name_reinspect").append("<option>" + data[i].Name + "</option>");
            }
        }
    });
});