var zindex3 = document.getElementById("zindex3");

//关闭按钮
var btn_close = document.getElementsByClassName("btn_close");
for (var i = 0; i < btn_close.length; i++) {
    btn_close[i].onclick = function () {
        zindex3.style.display = "none";
        div_togger3.style.display = "none";
    }
}


//点击打印按钮
var Print = document.getElementById("Print");
var div_togger3 = document.getElementById("div_togger3");
Print.onclick = function () {
    zindex3.style.display = "block";
    div_togger3.style.display = "block";
}