function TextBox_Buscar_Altas(e) {
    e = e || window.event;

    if (document.getElementById("tb_fecha_inicio_altas").value != "")
    {
        document.getElementById("tb_fecha_inicio_altas").value = "";
    }

    if (document.getElementById("tb_fecha_fin_altas").value != "")
    {
        document.getElementById("tb_fecha_fin_altas").value = "";
    }

    if (e.keyCode == 13)
    {
        document.getElementById('btn_buscar').click();
        return false;
    }
    return true;
}

function TextBox_Buscar_Bajas(e) {
    e = e || window.event;



    if (document.getElementById("tb_fecha_inicio_bajas").value != "")
    {
        document.getElementById("tb_fecha_inicio_bajas").value = "";
    }

    if (document.getElementById("tb_fecha_fin_bajas").value != "")
    {
        document.getElementById("tb_fecha_fin_bajas").value = "";
    }


    if (e.keyCode == 13) {
        document.getElementById('btn_buscar').click();
        return false;
    }
    return true;
}

function TextBox_Buscar_Activos(e) {
    e = e || window.event;

    if (e.keyCode == 13) {
        document.getElementById('btn_buscar').click();
        return false;
    }
    return true;
}

function TextBox_Buscar_Vencimientos(e) {
    e = e || window.event;

    if (document.getElementById("drop_documento").SelectedValue != "") {
        document.getElementById("drop_documento").SelectedValue = "";
    }

    if (e.keyCode == 13) {
        document.getElementById('btn_buscar').click();
        return false;
    }
    return true;
}

function TextBox_Buscar_Incapacidades(e) {
    e = e || window.event;

    if (document.getElementById("txtInicioIncapacidades").value != "") {
        document.getElementById("txtInicioIncapacidades").value = "";
    }

    if (document.getElementById("txtFinIncapacidades").value != "") {
        document.getElementById("txtFinIncapacidades").value = "";
    }

    if (e.keyCode == 13) {
        document.getElementById('btn_buscar').click();
        return false;
    }
    return true;
}

function TextBox_Buscar_Suspensiones(e) {
    e = e || window.event;

    if (document.getElementById("txtInicioSuspensiones").value != "") {
        document.getElementById("txtInicioSuspensiones").value = "";
    }

    if (document.getElementById("txtFinSuspensiones").value != "") {
        document.getElementById("txtFinSuspensiones").value = "";
    }

    if (e.keyCode == 13) {
        document.getElementById('btn_buscar').click();
        return false;
    }
    return true;
}

function TextBox_Buscar_Vacaciones(e) {
    e = e || window.event;

    if (document.getElementById("txtInicioVacaciones").value != "") {
        document.getElementById("txtInicioVacaciones").value = "";
    }

    if (document.getElementById("txtFinVacaciones").value != "") {
        document.getElementById("txtFinVacaciones").value = "";
    }

    if (e.keyCode == 13) {
        document.getElementById('btn_buscar').click();
        return false;
    }
    return true;
}

function TextBox_Buscar_Permisos(e) {
    e = e || window.event;

    if (document.getElementById("txtInicioPermisos").value != "") {
        document.getElementById("txtInicioPermisos").value = "";
    }

    if (document.getElementById("txtFinPermisos").value != "") {
        document.getElementById("txtFinPermisos").value = "";
    }

    if (e.keyCode == 13) {
        document.getElementById('btn_buscar').click();
        return false;
    }
    return true;
}

function TextBox_Buscar_HrsExtra(e) {
    e = e || window.event;

    if (document.getElementById("txtInicioHrsExtra").value != "") {
        document.getElementById("txtInicioHrsExtra").value = "";
    }

    if (document.getElementById("txtFinHrsExtra").value != "") {
        document.getElementById("txtFinHrsExtra").value = "";
    }

    if (e.keyCode == 13) {
        document.getElementById('btn_buscar').click();
        return false;
    }
    return true;
}

function TextBox_Buscar_Extras(e) {
    e = e || window.event;

    if (document.getElementById("ddlExtras").value != "") {
        document.getElementById("ddlExtras").value = "";
    }

    if (e.keyCode == 13) {
        document.getElementById('btn_buscar').click();
        return false;
    }
    return true;
}

function TextBox_Buscar_Avanzado(e) {
    e = e || window.event;

    //if (document.getElementById("ddlExtras").value != "") {
    //    document.getElementById("ddlExtras").value = "";
    //}

    if (e.keyCode == 13) {
        document.getElementById('btn_buscar').click();
        return false;
    }
    return true;
}