//$(function () {

//    var fullDate = new Date()
//    //console.log(fullDate);
//    //Thu May 19 2011 17:25:38 GMT+1000 {}

//    //convert month to 2 digits
//    var twoDigitMonth = ((fullDate.getMonth().length + 1) === 1) ? (fullDate.getMonth() + 1) : '0' + (fullDate.getMonth() + 1);

//    var currentDate = fullDate.getFullYear() + "/" + n + "/" + fullDate.getDate();
//    //console.log(currentDate);

//    $('#datepicker').datepicker({
//        inline: true,
//        showOtherMonths: true,
//        dayNamesMin: ['Dom', 'Lun', 'Mar', 'Mie', 'Jue', 'Vie', 'Sab'],
//        minDate: new Date(currentDate)
//        //dayNamesMin: ['Sun', 'Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat'],
//    });
//});

/**********************************************************************************************************************/
//$(function () {
//    // Getter
//    var minDate = $('#datepicker').datepicker("option", "minDate");

//    // Setter
//    $('#datepicker').datepicker("option", "minDate", new Date(2017, 1 - 1, 1));
//});

/**********************************************************************************************************************/
//$(function () {
//    var d2 = new Date();
//    d2.setMonth(d2.getMonth() - 2);

//    $('#datepicker').datepicker({
//        inline: true,
//        showOtherMonths: true,
//        dayNamesMin: ['Dom', 'Lun', 'Mar', 'Mie', 'Jue', 'Vie', 'Sab'],
//        minDate: new Date(d2)
//    });
//});

/**********************************************************************************************************************/
//$(function calendarioo() {

//$('#FromDate').datepicker({
//    dateFormat: "dd/mm/yy", //asi se maneja el calendario por default "02/20/2017" dateformat sirve para decirle como es la fecha
//    //showOn: "both", //Le asigna un boton al calendario para poder seleccionarlo
//    minDate: "-2m", //minDate: "02/20/2017"
//    onSelect: function () {
//        $('#ToDate').datepicker("option", "minDate", $('#FromDate').datepicker("getDate"));
//        //$("#mostrar").html(dateText);
//    }
//});
//});




/*********************** Calendario JavaScript dentro de un UpdatePanel y un MultiView ****************************/

//Esta funcion carga siempre que hace un POSTBACK o PAGELOAD - PARTE independiente
//$(document).ready(function () {
//    window.onload = load;
//});

//Esta funcion sirve para EndRequestHandler y forzar a que se inicie el JavaScript - PARTE 1 de 2
//function calendariooooo(sender, args) {
//    $('#datepicker').datepicker({
        //showOn: "both", //Le asigna un boton al calendario para poder seleccionarlo
        //dayNamesMin: ['Dom', 'Lun', 'Mar', 'Mie', 'Jue', 'Vie', 'Sab'],
        //dateFormat: "dd/mm/yy", //asi se maneja el calendario por default "02/20/2017" dateformat sirve para decirle como es la fecha
        //minDate: "-2m", //minDate: "02/20/2017"
        //onSelect: function () {
        // $('#ToDate').datepicker("option", "minDate", $('#FromDate').datepicker("getDate"));
        // $("#mostrar").html(dateText);
        //}
//    });
//}

//Esta funcion sirve para EndRequestHandler y forzar a que se inicie el JavaScript - PARTE 2 de 2
//function load() {
//    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(calendariooooo);
//}
/*********************** End ****************************/