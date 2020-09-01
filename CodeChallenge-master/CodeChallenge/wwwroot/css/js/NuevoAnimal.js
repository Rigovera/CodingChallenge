function HabilitarInputs(tipo) {
    var elements = document.getElementsByClassName(tipo);
    for (var i = 0; i < elements.length; i++) {
        elements[i].disabled = false;
    } 
}

function DeshabilitarInputs() {
    var elements = document.getElementsByTagName('input');
    for (var i = 0; i < elements.length; i++) {
        elements[i].disabled = true;
    }
}
