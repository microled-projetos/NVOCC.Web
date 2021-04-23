$(document).ready(function () {

    function limpa_formulário_cep() {

        $("#MainContent_txtEndereco").val("");
        $("#MainContent_txtBairro").val("");
        $("#MainContent_txtCidade").val("");
        $("#MainContent_cbEstado").val("");
    }

    $("#MainContent_txtCEP").blur(function () {

        var cep = $(this).val().replace(/\D/g, '');

        if (cep !== "") {

            var validacep = /^[0-9]{8}$/;

            if (validacep.test(cep)) {

                limpa_formulário_cep();

                $.getJSON("https://viacep.com.br/ws/" + cep + "/json/?callback=?", function (dados) {

                    if (!("erro" in dados)) {

                        $("#MainContent_txtEndereco").val(dados.logradouro);
                        $("#MainContent_txtBairro").val(dados.bairro);
                        $("#MainContent_txtCidade").val(dados.localidade);
                        $("#MainContent_cbEstado").val(dados.uf);

                        $('#MainContent_txtNumero').focus();
                    }
                    else {

                        limpa_formulário_cep();
                        alert("CEP não encontrado.");
                    }
                });
            }
            else {

                limpa_formulário_cep();
                alert("Formato de CEP inválido.");
            }
        }
        else {
            limpa_formulário_cep();
        }
    });
});