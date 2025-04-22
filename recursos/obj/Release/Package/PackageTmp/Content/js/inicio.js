$(document).ready(function funcion() {
    var string = $("#a").val();
    var array = string.split(",");
    //var array = [0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1];

    for (var i = 1; i < array.length; i++) {

        if (array[i] == 2) {
            ////document.getElementById("b" + i).disabled = true;
            document.getElementById("b" + i).style.display = "none";
        }

        else if (array[i] == 0) {

        }

        else if (array[i] == 1) {
            document.getElementById("b" + i).disabled = false;
            document.getElementById("b" + i).className = "menuBtn";
            document.getElementById("b" + i).setAttribute('src', "/images/menuPrincipal/menuBtn_recursos_" + i + ".png");
        }
    }
    //alert(array[0])
});