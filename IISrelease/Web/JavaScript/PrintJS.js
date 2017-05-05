var zindex = document.getElementById("zindex");

//关闭按钮
var btn_close = document.getElementsByClassName("btn_close");
for (var i = 0; i < btn_close.length; i++) {
    btn_close[i].onclick = function () {
        zindex.style.display = "none";
        div_togger1.style.display = "none";
    }
}


//点击打印按钮
var Print = document.getElementById("Print");
var div_togger1 = document.getElementById("div_togger1");
Print.onclick = function () {
    zindex.style.display = "block";
    div_togger1.style.display = "block";
}

