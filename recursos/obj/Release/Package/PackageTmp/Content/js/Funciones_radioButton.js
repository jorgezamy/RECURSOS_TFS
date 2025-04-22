//$(function () {
    //Seleccionar por default que no tiene ninos, el numero por default es 0 y el campo esta deshabilitado
//    $('input[type=radio][value=no]').attr('checked', true);
//    $('#Drop_NoNinos option:eq(0)').attr('selected', true);
//    $('#Drop_NoNinos').attr("disabled", 'disabled');

//    $('#<%= NinosList.ClientID %>').click(function () {
//    $('#NinosList').click(function () {   //La forma de llamar un ID sin <% %> NO funciona con JavaScript asi que hay que poner el campo como ClientIDMode=Static
//        var NinosListValue = $(this).find('input[type=radio]:checked').val();

//        if (NinosListValue == 'no') {
//            $('#Drop_NoNinos option:eq(0)').attr('selected', true); //Option con index 0 seleccionada
//            $('#Drop_NoNinos').attr("disabled", 'disabled'); //Campo deshabilitado
//        }
//        else {
//            $('#Drop_NoNinos option:eq(1)').attr('selected', true);
//            $('#Drop_NoNinos').removeAttr("disabled"); //Remover deshabilitado

            //Habilitar un campo ASP
            //$('#Drop_NoNinos').removeAttr("disabled"); //Remover deshabilitado
            //$('#Drop_NoNinos').attr("disabled", false);

            //Selecccionar una opcion de un dropdownlist
            //$('#Drop_NoNinos option:eq(0)')attr('selected', true);
            //$('#Drop_NoNinos option:eq(0)').prop('selected', true);

            //Desabilitar un campo ASP
            //$('input:text').attr("disabled", 'disabled');

            //Cambiar el texto de un campo ASP
            //$('#txb_nombre').val("texto");

            //Agregar un item a un dropdownlist
            //$('#Drop_NoNinos').append(
            //$('<option>0</option>').val(2).html(2));

            //Eliminar el numero de un dropdownlist que se ubique en la posicion #1
            //var index = $('#Drop_NoNinos').get(0).selectedIndex;
            //$('#Drop_NoNinos option:eq('+ index +')').remove();
//        }
//    })
//});