<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DashBoard.aspx.cs" Inherits="ABAINFRA.Web.DashBoard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row principal">
        <div class="col-lg-12 col-md-12 col-sm-12">
            <br />
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">Módulo Gerencial
                    </h3>
                </div>
                <div class="panel-body">
                    <ul class="nav nav-tabs" role="tablist">
                        <li class="active" id="tabdashboard">
                            <a href="#dashboard" id="linkdashboard" role="tab" data-toggle="tab">
                                <i class="fa fa-edit" style="padding-right: 8px;"></i>DashBoard
                            </a>
                        </li>
                    </ul>
                    <div class="tab-content">
                        <div class="tab-pane fade active in" id="dashboard">
                            <div class="row topMarg flexdiv">
                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <label class="control-label">Consultar por:<span class="required">*</span></label>
                                        <select id="ddlVendedor" class="form-control">
                                            <option value="0">Selecione</option>
                                        </select>
                                    </div>
                                </div>
                                <div class="col-sm-1">
                                    <div class="form-group">
                                        <label class="control-label">Mês - Inicial<span class="required">&nbsp</span></label>
                                        <asp:DropDownList ID="ddlMesInicial" runat="server" CssClass="form-control"></asp:DropDownList>      
                                    </div>
                                </div>
                                <div class="col-md-1">
                                    <div class="form-group">
                                        <label class="control-label">Ano - Inicial<span class="required">&nbsp</span></label>
                                        <asp:DropDownList ID="ddlAnoInicial" runat="server" CssClass="form-control"></asp:DropDownList>                                    </div>
                                </div>
                                <div class="col-md-1">
                                    <div class="form-group">
                                        <label class="control-label">Mês - Final<span class="required">&nbsp</span></label>
                                        <asp:DropDownList ID="ddlMesFinal" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-1">
                                    <div class="form-group">
                                        <label class="control-label">Ano - Final<span class="required">&nbsp</span></label>
                                        <asp:DropDownList ID="ddlAnoFinal" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-1">
                                    <div class="form-group">
                                        <label class="control-label">Tipo Operação<span class="required">&nbsp</span></label>
                                        <asp:DropDownList ID="ddlTipoOperacao" runat="server" CssClass="form-control"></asp:DropDownList>                                    </div>
                                </div>
                                <div class="col-md-1">
                                    <div class="form-group">
                                        <label class="control-label">Cores<span class="required">&nbsp</span></label>
                                        <asp:DropDownList ID="ddlCores" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-1">
                                    <div class="form-group">
                                        <label class="control-label">Estilo<span class="required">&nbsp</span></label>
                                        <select id="ddlEstilo" class="form-control">
                                            <option value="bar">Barra</option>
                                            <option value="line">Linha</option>
                                            <option value="pie">Pizza</option>
                                            <option value="doughnut">Donuts</option>
                                        </select>
                                    </div>
                                </div>
                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <button type="button" id="btnConsulta" onclick="gerarGrafico()" class="btn btn-primary">Carregar Gráfico</button>
                                    </div>
                                </div>
                            </div>
                            <div class="row topMarg">
                                <div class="form-group" style="display:flex;align-items:center; margin-bottom: 0px; margin-left: 10px;">
                                    <div>
                                        <asp:CheckBox ID="chk3D" runat="server" CssClass="form-control noborder" Text="&nbsp;Visualizar em 3D"></asp:CheckBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-10 col-sm-offset-1">
                                    <div class="alert alert-success text-center" id="msgSuccessUpdate">
                                        Courrier atualizado com sucesso.
                                    </div>
                                    <div class="alert alert-danger text-center" id="msgErrUpdate">
                                        Erro ao atualizar courrier.
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="panel-body">
                            <ul class="nav nav-tabs" role="tablist">
                                <li class="active" id="tabprocessoExpectGrid">
                                    <a href="#processoExpectGrid" id="linkprocessoExcepctGrid" role="tab" data-toggle="tab">
                                        <i class="fa fa-edit" style="padding-right: 8px;"></i>Gráficos
                                    </a>
                                </li>
                                <li id="tabprocessoRealGrid">
                                    <a href="#processoRealGrid" id="linkprocessoRealGrid" role="tab" data-toggle="tab">
                                        <i class="fa fa-edit" style="padding-right: 8px;"></i>Indicadores
                                    </a>
                                </li>
                            </ul>
                            <div class="tab-content">
                                <div class="tab-pane fade active in" id="processoExpectGrid">
                                    <div id="graph">
                                        
                                    </div>
                                    <div style="display:flex; justify-content:space-between">
                                        <div id="processGraph"></div>
                                        <div id="cntrGraph"></div>
                                        <div id="teusGraph"></div>
                                    </div>
                                </div>
                                <div class="tab-pane fade" id="processoRealGrid">
                            
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">
    <script src="Content/chart.js-3.2.0/package/dist/chart.js"></script>
    <script>
        var myChart;
        var myChart2;
        var myChart3;
        var myChart4;
        const graph = document.getElementById("graph");
        $(document).ready(function () {
            $.ajax({
                type: "POST",
                url: "Gerencial.asmx/CarregarVendedores",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    var data = data.d;
                    data = $.parseJSON(data);
                    for (let i = 0; i < data.length; i++) {
                        $("#ddlVendedor").append("<option value='" + data[i]["ID_PARCEIRO"] + "'>" + data[i]["NM_RAZAO"] + "</option>");
                    }

                },
                error: function (data) {

                },
            });

        });
        function gerarGrafico() {
            var anoInicial = document.getElementById("MainContent_ddlAnoInicial");
            var anoFinal = document.getElementById("MainContent_ddlAnoFinal");
            var mesInicial = document.getElementById("MainContent_ddlMesInicial");
            var mesFinal = document.getElementById("MainContent_ddlMesFinal");
            var vendedor = document.getElementById("ddlVendedor");
            var tipoEstufagem = document.getElementById("MainContent_ddlTipoOperacao");
            var type = document.getElementById("ddlEstilo").value;
            $.ajax({
                type: "POST",
                url: "Gerencial.asmx/CarregarEstatistica",
                data: '{anoI:"' + anoInicial.value + '", anoF:"' + anoFinal.value + '", mesI: "' + mesInicial.value + '",mesF: "' + mesFinal.value + '",vendedor: "' + vendedor.value + '",tipo: "' + tipoEstufagem.value+'" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    console.log(data);
                    var data = data.d;
                    data = $.parseJSON(data);
                    lineChartProcesso = [];
                    lineChartCntr = [];
                    lineChartTeus= [];
                    label = [];
                    if (data != null) {
                        for (var i = 0; i < data.length; i++) {
                            lineChartProcesso[i] = data[i]["PROCESSO"];
                            lineChartCntr[i] = data[i]["CONTAINER"];
                            lineChartTeus[i] = data[i]["TEUS"];
                            label[i] = data[i]["PERIODO"];
                        }
                    }
                    else {
                        $("#graph").append("<canvas id='mainGraph' style='width:'100%;'><div class='row'><p class='text-center'>Resultados não encontrados.</p></div></canvas>");
                    }
                    $("#graph").empty();
                    $("#graph").append("<canvas id='mainGraph' style='width:100%; height: 300px;'></canvas>");
                    var ctx = document.getElementById('mainGraph').getContext('2d');
                    myChart = new Chart(ctx, {
                        type: type,
                        data: {
                            labels: label,
                            datasets: [
                                {
                                    barThickness: 30,
                                    label: 'TEUS',
                                    data: [lineChartTeus[0]],
                                    backgroundColor: ['rgba(255, 206, 86, 1)'],
                                    borderWidth: 1
                                },
                                {
                                    barThickness: 30,
                                    label: 'Processos',
                                    data: [lineChartProcesso[0]],
                                    backgroundColor: ['rgba(255, 99, 132, 1)'],
                                    borderWidth: 1
                                },
                                {
                                    barThickness: 30,
                                    label: 'Containers',
                                    data: [lineChartCntr[0]],
                                    backgroundColor: ['rgba(54, 162, 235,1)'],
                                    borderWidth: 1
                                }
                            ]
                        },
                        options: {
                            layout: {
                                padding: 10
                            },
                            plugins: {
                                title: {
                                    display: true,
                                    text: 'Teus, Processos e Qtde. de CNTR por Vendedor',
                                    font: { size: 20 }
                                }
                            }
                        }
                    });
                },
                error: function (data) {

                },
            });

            var ctx2 = document.getElementById('processGraph').getContext('2d');
            $("#graph").append("<canvas id='pgraph' style='height: 300px;'></canvas>");
            var myChart2 = new Chart(ctx2, {
                type: 'bar',
                data: {
                    labels: ['JAN/2021', 'FEV/2021', 'MAR/2021', 'ABR/2021', 'MAI/2021', 'JUN/2021', 'JUL/2021', 'AGO/2021', 'SET/2021', 'OUT/2021', 'NOV/2021', 'DEZ/2021'],
                    datasets: [
                        {
                            label: 'TEUS',
                            data: [9, 8, 3, 15, 20, 13, 4, 26, 30, 11, 14, 24],
                            backgroundColor: ['rgba(255, 206, 86, 1)'],
                            borderWidth: 1
                        }
                    ]
                },
                options: {
                    plugins: {
                        title: {
                            display: true,
                            text: 'Teus por Modal',
                            font: { size: 20 }
                        }
                    }
                }
            });

            var ctx3 = document.getElementById('cntrGraph').getContext('2d');
            $("#graph").append("<canvas id='cgraph' style='height: 300px;'></canvas>");
            var myChart3 = new Chart(ctx3, {
                type: 'bar',
                data: {
                    labels: ['JAN/2021', 'FEV/2021', 'MAR/2021', 'ABR/2021', 'MAI/2021', 'JUN/2021', 'JUL/2021', 'AGO/2021', 'SET/2021', 'OUT/2021', 'NOV/2021', 'DEZ/2021'],
                    datasets: [
                        {
                            label: 'Processos',
                            data: [12, 10, 14, 23, 12, 4, 16, 20, 19, 7, 26, 13],
                            backgroundColor: ['rgba(255, 99, 132, 1)'],
                            borderWidth: 1
                        }
                    ]
                },
                options: {
                    plugins: {
                        title: {
                            display: true,
                            text: 'Qtde de Processos por Modal',
                            font: { size: 20 }
                        }
                    }
                }
            });

            var ctx4 = document.getElementById('teusGraph').getContext('2d');
            $("#graph").append("<canvas id='tgraph' style='height: 300px;'></canvas>");
            var myChart4 = new Chart(ctx4, {
                type: 'bar',
                data: {
                    labels: ['JAN/2021', 'FEV/2021', 'MAR/2021', 'ABR/2021', 'MAI/2021', 'JUN/2021', 'JUL/2021', 'AGO/2021', 'SET/2021', 'OUT/2021', 'NOV/2021', 'DEZ/2021'],
                    datasets: [
                        {
                            label: 'Containers',
                            data: [9, 8, 3, 15, 20, 13, 4, 26, 30, 11, 14, 24],
                            backgroundColor: ['rgba(54, 162, 235,1)'],
                            borderWidth: 1
                        }
                    ]
                },
                options: {
                    plugins: {
                        title: {
                            display: true,
                            text: 'Qtde de Containers por Modal',
                            font: { size: 20 }
                        }
                    }
                }
            });
        }
    </script>
</asp:Content>
