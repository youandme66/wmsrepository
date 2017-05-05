function a() {
    document.getElementById('status').innerHTML = '更新中';
}
function Cmd(v) {
    for (var i = 1; i <= 3; i++) {
        if (i == v) {
            document.getElementById("panl" + i).style.display = "";
        }
        else
            document.getElementById("panl" + i).style.display = "none";
    }

}