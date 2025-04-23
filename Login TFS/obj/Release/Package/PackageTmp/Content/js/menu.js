
function funcion() {
    var string = $("#a").val();
    var array = string.split(",");
    //var array = [0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1];

    for (var i = 0; i < array.length; i++) {

        //if (array[i] == 0) {
        //    ////document.getElementById("b" + i).disabled = true;
        //}

        if (array[i] == 1) {
            document.getElementById("b" + i).disabled = false;
            document.getElementById("b" + i).className = "menuBtn";
            document.getElementById("b" + i).setAttribute('src', "/images/menu/menuBtn_login_" + i + ".png");
        }
    }
}

window.onload = funcion;