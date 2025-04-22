$(function () {
    //Seleccionar por default que no tiene ninos, el numero por default es 0 y el campo esta deshabilitado
    $('input[type=radio][value=no]').attr('checked', true);
    $('#Drop_NoNinos option:eq(0)').attr('selected', true);
    $('#Drop_NoNinos').attr("disabled", 'disabled');

    $('#NinosList').click(function () {
        var NinosListValue = $(this).find('input[type=radio]:checked').val();

        if (NinosListValue == 'no') {
            $('#Drop_NoNinos option:eq(0)').attr('selected', true); //Option con index 0 seleccionada
            $('#Drop_NoNinos').attr("disabled", 'disabled'); //Campo deshabilitado
        }
        else {
            $('#Drop_NoNinos option:eq(1)').attr('selected', true);
            $('#Drop_NoNinos').removeAttr("disabled"); //Remover deshabilitado
        }
    })
});