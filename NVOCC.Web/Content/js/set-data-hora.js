function setarDataHora(campo) {

    event.preventDefault();

    var agora = new Date();

    var dia = agora.getDate().toString().padStart(2, "0");
    var mes = (agora.getMonth() + 1).toString().padStart(2, "0");
    var ano = agora.getFullYear();

    var hora = agora.getHours().toString().padStart(2, "0");
    var minuto = agora.getMinutes().toString().padStart(2, "0");

    agora = dia + '/' + mes + '/' + ano + ' ' + hora + ':' + minuto;

    if (!$('#MainContent_' + campo).is(':disabled')) {
        $('#MainContent_' + campo).val(agora);
    }
}

function setarDataHoraA(campo) {

    event.preventDefault();

    var agora = new Date();

    var dia = agora.getDate().toString().padStart(2, "0");
    var mes = (agora.getMonth() + 1).toString().padStart(2, "0");
    var ano = agora.getFullYear();

    var hora = agora.getHours().toString().padStart(2, "0");
    var minuto = agora.getMinutes().toString().padStart(2, "0");

    agora = dia + '/' + mes + '/' + ano + ' ' + hora + ':' + minuto;

    if (!$('#' + campo).is(':disabled')) {
        $('#' + campo).val(agora);
    }
}