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