function TextBox_PresionarBtnEnter(e) {
    e = e || window.event;

    if (e.keyCode == 13) {
        document.getElementById('bt_btnbuscar').click();
        return false;
    }
    return true;
}


function PresionarBtnEnter(e) {
    e = e || window.event;


    if (e.keyCode == 13) {
        document.getElementById('btn_calcular').click();
        return false;
    }
    return true;
}

