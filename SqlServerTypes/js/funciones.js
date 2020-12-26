function HeaderCheckBox(checkbox) {
    var grvAutoDetalle = document.getElementById("grvAutoDetalle");

    for (var i = 1; i < grvAutoDetalle.rows.lenght; i++) {
        grvAutoDetalle.rows[i].cells[0].getElementByName("INPUT")[0].checked = checkbox.checked;
    }
}

function mostrarPestana(n) {
    var pestanas = document.getElementsByClassName("pestana");
    var cabPests = document.getElementsByClassName("cabPest");
    var contHojas = document.getElementsByClassName("contHoja");
    for (i = 0; i < pestanas.length; i++) {
        if (pestanas[i].className.includes("p-activa")) {
            pestanas[i].className = pestanas[i].className.replace("p-activa", "");
            cabPests[i].className = cabPests[i].className.replace("c-activa", "");
            contHojas[i].className = cabPests[i].className.replace("h-activa", "");
            break;
        }
    }

    pestanas[n].className += " p-activa";
    cabPests[n].className += " c-activa";
}