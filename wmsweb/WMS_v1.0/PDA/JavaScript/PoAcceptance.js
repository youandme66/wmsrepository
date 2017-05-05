$(document).ready(function () {
    $.ajax({
        async: false,
        type: "POST",
        url: "POSusp.ashx",
        dataType: "json",
        data: {
            POSusp: $("#receipt_no").val(),
        },
        success: function (data) {
            for (var i = 0; i < data.length; i++) {
                $("#receipt_no").append("<option>" + data[i].Name + "</option>");
            }
        }
    });
});
