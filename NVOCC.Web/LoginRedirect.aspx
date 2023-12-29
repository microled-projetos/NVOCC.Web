<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginRedirect.aspx.cs" Inherits="ABAINFRA.Web.LoginRedirect" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet">
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.14.0/css/all.min.css" rel="stylesheet">
    <title></title>
</head>
    <style>
        .image-container {
            text-align: center;
            margin-bottom: 20px;
        }

        .login-image {
            display: inline-block;
            width: 150px;
            height: auto;
        }
    </style>
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.9.2/dist/umd/popper.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
<body>
     <div class="container d-flex justify-content-center align-items-center" style="height: 100vh;">
        <div class="card w-60">
            <div class="card-body">
                <div class="image-container">
                <img src="Content\imagens\ABA_Infra.png" alt="Logo" class="login-image"/>
            </div>
<!--
                <div class="input-group mb-3">
                    <div class="input-group-prepend">
                        <span class="input-group-text" id="basic-addon1"><i class="fas fa-user"></i></span>
                    </div>
                    <input type="text" class="form-control" placeholder="Usuário" aria-label="Usuário" aria-describedby="basic-addon1">
                </div>

                <div class="input-group mb-3">
                    <div class="input-group-prepend">
                        <span class="input-group-text" id="basic-addon2"><i class="fas fa-key"></i></span>
                    </div>
                    <input type="password" class="form-control" placeholder="Senha" aria-label="Senha" aria-describedby="basic-addon2">
                </div>
-->
                <div class="input-group mb-3">
                    <div class="input-group-prepend">
                        <span class="input-group-text" id="basic-addon3"><i class="fas fa-building"></i></span>
                    </div>
                    <select class="custom-select min-width-select"  id="empresa" >
                        <option selected>Selecione uma empresa...</option>
                    </select>
                </div>

                <div class="input-group mb-3">
                    <div class="input-group-prepend">
                        <span class="input-group-text" id="basic-addon4"><i class="fas fa-link"></i></span>
                    </div>
                    <select class="custom-select min-width-select"  id="url">
                         <option selected>Selecione uma empresa antes...</option>
                    </select>
                </div>

                <!--<button class="btn btn-primary btn-block" type="submit">Direcionar</button>-->
                <button class="btn btn-primary btn-block" type="submit" id="btnDirecionar" disabled>Direcionar</button>

            </div>
        </div>
    </div>
    </body>
    <script src="Content/js/jquery.min.js"></script>
    <script>
        $(document).ready(function () {
            var empresas = "";
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/listarEmpresas",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    if (dado.length > 0) {
                        for (let i = 0; i < dado.length; i++) {
                            empresas += "<option value='" + dado[i]["ID_EMPRESA"] + "'>" + dado[i]["NM_EMPRESA"] + "</option>";
                        }
                        $("#empresa").append(empresas);
                    }
                }
            })

            $("#empresa").change(function () {
                var aplicacao = "";
                let empresa = $(this).val();

                $("#url").empty();

                $('#btnDirecionar').prop('disabled', true);

                $("#url").append('<option selected>Selecione uma empresa antes...</option>');
                $.ajax({
                    type: "POST",
                    url: "DemurrageService.asmx/listarAplicacoes",
                    data: '{idEmpresa: "' + empresa + '" }',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (dado) {
                        var dado = dado.d;
                        dado = $.parseJSON(dado);
                        if (dado.length > 0) {
                            for (let i = 0; i < dado.length; i++) {
                                aplicacao += "<option value='" + dado[i]["DS_URL"] + "'>"+dado[i]["NM_EMPRESA"]+" - "+dado[i]["DS_URL"]+"</option>";
                            }
                            $("#url").append(aplicacao);
                        }
                    }
                })

                checkSelections();
            });

            $("#url").change(function () {
                checkSelections();
            });

        });

        $("button[type='submit']").click(function (e) {

            e.preventDefault();

            const siteURL = $("#url").val();

            if (siteURL) {
                /*window.location.href = siteURL;*/
                window.open(siteURL, '_blank');

                /*
                $("#empresa").val('Selecione uma empresa...'); 
                $("#url").empty();
                $("#url").append('<option selected>Selecione uma empresa antes...</option>');
                */

            } else {
                alert('Por favor, selecione uma empresa e um site.');
            }
        });

        function checkSelections() {
            let selectedEmpresa = $("#empresa").val();
            let selectedURL = $("#url").val();

            if (selectedEmpresa && selectedEmpresa !== 'Selecione uma empresa...' &&
                selectedURL && selectedURL !== 'Selecione uma empresa antes...') {
                $('#btnDirecionar').prop('disabled', false);
            } else {
                $('#btnDirecionar').prop('disabled', true);
            }
        }

    </script>
</html>
