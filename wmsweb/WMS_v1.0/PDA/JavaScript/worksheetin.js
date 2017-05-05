function change() {
    $.ajax({
        timeout: 3000,
        async: false,
        type: "POST",
        url: "aboutWorkSheetIn.ashx",
        dataType: "json",
        data: {
            wo_no: $("#wo_no").val(),
        },
        success: function (data) {
            for (var i = 0; i < data.length; i++) {
                if (i == 0) {
                    $("#onhand").val(data[i].Name);
                }
                if (i == 1) {
                    $("#item_name").val(data[i].Name);
                }
            }
        }
    });
}