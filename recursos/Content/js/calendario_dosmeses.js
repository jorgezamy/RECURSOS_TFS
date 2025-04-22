/*********************** Calendario JavaScript dentro de un UpdatePanel y un MultiView ****************************/

//Esta funcion carga siempre que hace un POSTBACK o PAGELOAD - PARTE independiente
$(document).ready(function Load() {
    window.onload = load;
});

//Esta funcion sirve para EndRequestHandler y forzar a que se inicie el JavaScript - PARTE 1 de 2
function load() {
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(calendario);
}

//Esta funcion sirve para EndRequestHandler y forzar a que se inicie el JavaScript - PARTE 2 de 2
function calendario(sender, args) {
    $('#date_ServicioVig').datepicker({
        //showOn: "both", //Le asigna un boton al calendario para poder seleccionarlo
        dayNamesMin: ['Dom', 'Lun', 'Mar', 'Mie', 'Jue', 'Vie', 'Sab'],
        changeMonth: true,
        changeYear: true,
        dateFormat: "yy/mm/dd", //asi se maneja el calendario por default "02/20/2017" dateformat sirve para decirle como es la fecha
        minDate: "-2m" //minDate: "02/20/2017"
        //onSelect: function () {
        // $('#ToDate').datepicker("option", "minDate", $('#FromDate').datepicker("getDate"));
        // $("#mostrar").html(dateText);
        //}
    });
}
/*********************** End ****************************/