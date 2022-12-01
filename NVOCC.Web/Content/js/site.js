
$(document).ready(function () {

    $(".BL").on("keypress keyup blur", function (event) {
        console.log("entrou")
        return String.fromCharCode(event.charCode).match(/[^a-zA-Z0-9]/g) === null

    }); 

    $(".inteiro").on("keypress keyup blur", function (event) {
        $(this).val($(this).val().replace(/[^\d].+/, ""));
        if ((event.which < 48 || event.which > 57)) {
            event.preventDefault();
        }
    });  

    $(".peso").mask('#0.000', { reverse: true });

    $(".moeda").mask('#.##0,00', { reverse: true });

    $(".ApenasNumeros").mask('#', { reverse: true });

    $(".numero").mask('##0,00', { reverse: true });

    $(".aliquotas").mask('##0,000', { reverse: true });

    $(".txs").mask('##0,00000', { reverse: true });

    $('.cpf').mask('000.000.000-00');

    $('.cnpj').mask('00.000.000/0000-00');

    $('.cep').mask('00000-000');

    $('.telefone').mask('(00) 0000-0000');

    $('.celular').mask('(00) 00000-0000');

    $('.data').mask('00/00/0000');

    $('.data-hora').mask('00/00/0000 00:00');

    $('.placa').mask('SSS-9A99');

    $('.competencia').mask('00/0000');

});


function CheckMaxCount(txtBox, e, maxLength) {
    if (txtBox) {
        if (txtBox.value.length > maxLength) {
            txtBox.value = txtBox.value.substring(0, maxLength);
        }
        if (!checkSpecialKeys(e)) {
            return (txtBox.value.length <= maxLength)
        }
    }
}

function checkSpecialKeys(e) {
    if (e.keyCode != 8 && e.keyCode != 46 && e.keyCode != 37 && e.keyCode != 38 && e.keyCode != 39 && e.keyCode != 40)
        return false;
    else
        return true;
}


$(".senha").keyup(function () {

    var start = $(this)[0].selectionStart;
    var end = $(this)[0].selectionEnd;

    $(this).val($(this).val().toCapitalize())
    $(this).selectRange(start, end);
});


$("input[type=text]").keyup(function () {

    var start = $(this)[0].selectionStart;
    var end = $(this)[0].selectionEnd;

    $(this).val($(this).val().toUpperCase());
    $(this).selectRange(start, end);
});

$.fn.selectRange = function (start, end) {
    $(this).each(function () {
        var el = $(this)[0];

        if (el) {
            el.focus();

            if (el.setSelectionRange) {
                el.setSelectionRange(start, end);

            } else if (el.createTextRange) {
                var range = el.createTextRange();
                range.collapse(true);
                range.moveEnd('character', end);
                range.moveStart('character', start);
                range.select();

            } else if (el.selectionStart) {
                el.selectionStart = start;
                el.selectionEnd = end;
            }
        }
    });
};

$(document).on('keyup keypress', 'form input', function (e) {
    if (e.keyCode === 13) {
        e.preventDefault();
        return false;
    }
});