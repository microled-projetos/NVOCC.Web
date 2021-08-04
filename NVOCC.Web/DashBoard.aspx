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
                                        <select id="ddlAnoInicial" class="form-control">
                                            <option value="">Selecione</option>
                                        </select>
                                    </div>
                                </div>
                                <div class="col-md-1" id="ddlMesFinalBox">
                                    <div class="form-group">
                                        <label class="control-label">Mês - Final<span class="required">&nbsp</span></label>
                                        <asp:DropDownList ID="ddlMesFinal" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-1" id="ddlAnoFinalBox">
                                    <div class="form-group">
                                        <label class="control-label">Ano - Final<span class="required">&nbsp</span></label>
                                        <select id="ddlAnoFinal" class="form-control">
                                            <option value="">Selecione</option>
                                        </select>
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
                                        <select id="ddlEstilo" class="form-control" onchange="dataChange()">
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
                                        <div id="processGraph" style="width:100%"></div>
                                        <div id="cntrGraph" style="width:100%"></div>
                                        <div id="teusGraph" style="width:100%"></div>
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

        var ano = new Date().getFullYear();
        for (var i = ano; i >= 2018; i--) {
            console.log(ano);
            var value = i.toString().substr(2, 2);
            $("#ddlAnoInicial").append("<option value='" + value + "'>" + i + "</option>")
            $("#ddlAnoFinal").append("<option value='" + value + "'>" + i + "</option>")
        }

        function dataChange() {
            var estilo = document.getElementById("ddlEstilo").value;
            if (estilo == "pie") {
                $("#ddlMesFinalBox").hide();
                $("#ddlAnoFinalBox").hide();
            } else if (estilo == "bar") {
                $("#ddlMesFinalBox").show();
                $("#ddlAnoFinalBox").show();
            }
        }

        function graficoGeral(processo,cntr,teus,data) {
            var ctx = document.getElementById('mainGraph').getContext('2d');
            myChart = new Chart(ctx, {
                type: 'bar',
                data: {
                    labels: data,
                    datasets: [
                        {
                            barThickness: 30,
                            label: 'TEUS',
                            data: teus,
                            backgroundColor: ['rgba(255, 206, 86, 1)'],
                            borderWidth: 1
                        },
                        {
                            barThickness: 30,
                            label: 'Processos',
                            data: processo,
                            backgroundColor: ['rgba(255, 99, 132, 1)'],
                            borderWidth: 1
                        },
                        {
                            barThickness: 30,
                            label: 'Containers',
                            data: cntr,
                            backgroundColor: ['rgba(54, 162, 235,1)'],
                            borderWidth: 1
                        }
                    ]
                },
                options: {
                    scales: {
                        yAxes: [{
                            display: true,
                            ticks: {
                                beginAtZero: true
                            }
                        }]
                    },
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
        }

        function graficoProcessos(processoIMP, processoEXP, processoAR, data) {
            var ctx2 = document.getElementById('pgraph').getContext('2d');
            var myChart2 = new Chart(ctx2, {
                type: 'bar',
                data: {
                    labels: data,
                    datasets: [
                        {
                            barThickness: 30,
                            label: 'IMP',
                            data: processoIMP,
                            backgroundColor: ['rgba(255, 206, 86, 1)'],
                            borderWidth: 1
                        },
                        {
                            barThickness: 30,
                            label: 'EXP',
                            data: processoEXP,
                            backgroundColor: ['rgba(255, 99, 132, 1)'],
                            borderWidth: 1
                        },
                        {
                            barThickness: 30,
                            label: 'AEREO',
                            data: processoAR,
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
                            text: 'Qtde. de Processos',
                            font: { size: 20 }
                        }
                    }
                }
            });
        }

        function graficoCntr(cntrIMP, cntrEXP, data) {
                var ctx2 = document.getElementById('cgraph').getContext('2d');
                var myChart2 = new Chart(ctx2, {
                    type: 'bar',
                    data: {
                        labels: data,
                        datasets: [
                            {
                                barThickness: 30,
                                label: 'IMP',
                                data: cntrIMP,
                                backgroundColor: ['rgba(255, 206, 86, 1)'],
                                borderWidth: 1
                            },
                            {
                                barThickness: 30,
                                label: 'EXP',
                                data: cntrEXP,
                                backgroundColor: ['rgba(255, 99, 132, 1)'],
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
                                text: 'Qtde. de CNTR',
                                font: { size: 20 }
                            }
                        }
                    }
                });            
        }

        function graficoTeus(processo,cntr,data) {
                var ctx2 = document.getElementById('tGraph').getContext('2d');
                var myChart2 = new Chart(ctx2, {
                    type: 'bar',
                    data: {
                        labels: data,
                        datasets: [
                            {
                                barThickness: 30,
                                label: 'IMP',
                                data: processo,
                                backgroundColor: ['rgba(255, 206, 86, 1)'],
                                borderWidth: 1
                            },
                            {
                                barThickness: 30,
                                label: 'EXP',
                                data: cntr,
                                backgroundColor: ['rgba(255, 99, 132, 1)'],
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
                                text: 'Teus',
                                font: { size: 20 }
                            }
                        }
                    }
                });
        }

        function graficoGeralPizza(processo, cntr, teus, data) {
            var ctx = document.getElementById('mainGraph').getContext('2d');
            myChart = new Chart(ctx, {
                type: 'pie',
                data: {
                    labels: ['PROCESSO', 'CNTR', 'TEUS'],
                    datasets: [
                        {
                            label: 'IMP',
                            data: [processo[0], cntr[0], teus[0]],
                            backgroundColor: ['rgba(255, 206, 86, 1)', 'rgba(255, 99, 132, 1)', 'rgba(54, 162, 235,1)'],
                        }
                    ]
                },
                options: {
                    legend: {
                        position: 'top',
                    },
                    plugins: {
                        title: {
                            display: true,
                            text: 'Teus, Processos e Qtde. de CNTR por Vendedor em ' + data ,
                            font: { size: 20 }
                        }
                    }
                }
            });
        }

        function graficoProcessosPizza(processoIMP, processoEXP, processoAR, data) {              
            var ctx2 = document.getElementById('pgraph').getContext('2d');
            var myChart2 = new Chart(ctx2, {
                type: 'pie',
                data: {
                    labels: ['IMP', 'EXP', 'AEREO'],
                    datasets: [
                        {
                            label: 'IMP',
                            data: [processoIMP[0], processoAR[0], processoEXP[0]],
                            backgroundColor: ['rgba(255, 206, 86, 1)', 'rgba(255, 99, 132, 1)', 'rgba(54, 162, 235,1)'],
                        }
                    ]
                },
                options: {
                    legend: {
                        position: 'top',
                    },
                    plugins: {
                        title: {
                            display: true,
                            text: 'Qtde. de Processos em '+data,
                            font: { size: 20 }
                        }
                    }
                }
            });
        }

        function graficoCntrPizza(cntrIMP, cntrEXP, data) {
            var ctx2 = document.getElementById('cgraph').getContext('2d');
            var myChart2 = new Chart(ctx2, {
                type: 'pie',
                data: {
                    labels: ['IMP', 'EXP'],
                    datasets: [
                        {
                            label: 'IMP',
                            data: [cntrIMP[0], cntrEXP[0]],
                            backgroundColor: ['rgba(255, 206, 86, 1)', 'rgba(255, 99, 132, 1)'],
                        }
                    ]
                },
                options: {
                    legend: {
                        position: 'top',
                    },
                    plugins: {
                        title: {
                            display: true,
                            text: 'Qtde. de CNTR em ' +data,
                            font: { size: 20 }
                        }
                    }
                }
            });
        }

        function graficoTeusPizza(processo, cntr, data) {
            var ctx2 = document.getElementById('tGraph').getContext('2d');
            var myChart2 = new Chart(ctx2, {
                type: 'pie',
                data: {
                    labels: ['IMP', 'EXP'],
                    datasets: [
                        {
                            label: 'IMP',
                            data: [processo[0], cntr[0]],
                            backgroundColor: ['rgba(255, 206, 86, 1)', 'rgba(255, 99, 132, 1)'],
                        }
                    ]
                },
                options: {
                    legend: {
                        position: 'top',
                    },
                    plugins: {
                        title: {
                            display: true,
                            text: 'Teus em '+data,
                            font: { size: 20 }
                        }
                    }
                }
            });
        }

        function gerarGrafico() {
            var anoInicial = document.getElementById("ddlAnoInicial");
            var anoFinal = document.getElementById("ddlAnoFinal");
            var mesInicial = document.getElementById("MainContent_ddlMesInicial");
            var mesFinal = document.getElementById("MainContent_ddlMesFinal");
            var vendedor = document.getElementById("ddlVendedor");
            var tipoEstufagem = document.getElementById("MainContent_ddlTipoOperacao");
            var type = document.getElementById("ddlEstilo").value;
            if (type == "bar") {
                $.ajax({
                    type: "POST",
                    url: "Gerencial.asmx/CarregarEstatistica",
                    data: '{anoI:"' + anoInicial.value + '", anoF:"' + anoFinal.value + '", mesI: "' + mesInicial.value + '",mesF: "' + mesFinal.value + '",vendedor: "' + vendedor.value + '",tipo: "' + tipoEstufagem.value + '" }',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        var data = data.d;
                        data = $.parseJSON(data);
                        var lineChartProcesso = [];
                        var lineChartCntr = [];
                        var lineChartTeus = [];
                        var label = [];
                        $("#graph").empty();
                        if (data != null) {
                            for (var i = 0; i < data.length; i++) {
                                lineChartProcesso.push(data[i]["PROCESSO"]);
                                lineChartCntr.push(data[i]["CONTAINER"]);
                                lineChartTeus.push(data[i]["TEUS"]);
                                label.push(data[i]["PERIODO"]);
                            }
                            $("#graph").append("<canvas id='mainGraph' style='width:100%; height: 300px; max-height: 300px'></canvas>");
                            graficoGeral(lineChartProcesso, lineChartCntr, lineChartTeus, label);
                        }
                        else {
                            lineChartProcesso.push(0);
                            lineChartCntr.push(0);
                            lineChartTeus.push(0);
                            label.push(0);
                            
                            $("#graph").append("<canvas id='mainGraph' style='width:100%; height: 300px; max-height: 300px'></canvas>");
                            graficoGeral(lineChartProcesso, lineChartCntr, lineChartTeus, label);
                        }
                    }
                });

                $.ajax({
                    type: "POST",
                    url: "Gerencial.asmx/CarregarProcessos",
                    data: '{anoI:"' + anoInicial.value + '", anoF:"' + anoFinal.value + '", mesI: "' + mesInicial.value + '",mesF: "' + mesFinal.value + '",vendedor: "' + vendedor.value + '",tipo: "' + tipoEstufagem.value + '" }',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        var data = data.d;
                        data = $.parseJSON(data);
                        var lineChartIMP = [];
                        var lineChartAR = [];
                        var lineChartEXP = [];
                        var label = [];
                        $("#processGraph").empty();
                        if (data != null) {
                            for (var i = 0; i < data.length; i++) {
                                lineChartIMP.push(data[i]["IMP"]);
                                lineChartAR.push(data[i]["AR"]);
                                lineChartEXP.push(data[i]["EXP"]);
                                label.push(data[i]["PERIODO"]);
                            }
                            $("#processGraph").append("<canvas id='pgraph' style='width:100%;height:300px;max-height: 300px''></canvas>");
                            graficoProcessos(lineChartIMP, lineChartEXP, lineChartAR, label);
                        }
                        else {
                            lineChartIMP.push(0);
                            lineChartAR.push(0);
                            lineChartEXP.push(0);
                            label.push(0);
                            $("#processGraph").append("<canvas id='pgraph' style='width:100%;height:300px;max-height: 300px''></canvas>");
                            graficoProcessos(lineChartIMP, lineChartEXP, lineChartAR, label);
                        }
                    }
                });

                $.ajax({
                    type: "POST",
                    url: "Gerencial.asmx/CarregarQtdCntr",
                    data: '{anoI:"' + anoInicial.value + '", anoF:"' + anoFinal.value + '", mesI: "' + mesInicial.value + '",mesF: "' + mesFinal.value + '",vendedor: "' + vendedor.value + '",tipo: "' + tipoEstufagem.value + '" }',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        var data = data.d;
                        data = $.parseJSON(data);
                        var lineChartIMP = [];
                        var lineChartEXP = [];
                        var label = [];
                        $("#cntrGraph").empty();
                        if (data != null) {
                            for (var i = 0; i < data.length; i++) {
                                lineChartIMP.push(data[i]["IMP"]);
                                lineChartEXP.push(data[i]["EXP"]);
                                label.push(data[i]["PERIODO"]);
                            }
                            $("#cntrGraph").append("<canvas id='cgraph' style='width:100%;height: 300px;max-height: 300px''></canvas>");
                            graficoCntr(lineChartIMP, lineChartEXP, label);
                        }
                        else {
                            lineChartIMP.push(0);
                            lineChartEXP.push(0);
                            label.push(0);
                            $("#cntrGraph").append("<canvas id='cgraph' style='width:100%;height: 300px;max-height: 300px''></canvas>");
                            graficoCntr(lineChartIMP, lineChartEXP, label);
                        }
                    }
                });

                $.ajax({
                    type: "POST",
                    url: "Gerencial.asmx/CarregarTeus",
                    data: '{anoI:"' + anoInicial.value + '", anoF:"' + anoFinal.value + '", mesI: "' + mesInicial.value + '",mesF: "' + mesFinal.value + '",vendedor: "' + vendedor.value + '",tipo: "' + tipoEstufagem.value + '" }',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        var data = data.d;
                        data = $.parseJSON(data);
                        var lineChartProcesso = [];
                        var lineChartCntr = [];
                        var label = [];
                        $("#teusGraph").empty();
                        if (data != null) {
                            for (var i = 0; i < data.length; i++) {
                                lineChartProcesso.push(data[i]["IMP"]);
                                lineChartCntr.push(data[i]["EXP"]);
                                label.push(data[i]["PERIODO"]);
                            }
                            $("#teusGraph").append("<canvas id='tGraph' style='width:100%;height: 300px;max-height: 300px'></canvas>");
                            graficoTeus(lineChartProcesso, lineChartCntr, label)
                        }
                        else {
                            lineChartProcesso.push(0);
                            lineChartCntr.push(0);
                            label.push(0);
                        
                            $("#teusGraph").append("<canvas id='tGraph' style='width:100%;height: 300px;max-height: 300px'></canvas>");
                            graficoTeus(lineChartProcesso, lineChartCntr, label)
                        }
                    }
                });
            } else if (type == "pie") {
                $.ajax({
                    type: "POST",
                    url: "Gerencial.asmx/CarregarEstatisticaPizza",
                    data: '{anoI:"' + anoInicial.value + '", mesI: "' + mesInicial.value + '",vendedor: "' + vendedor.value + '",tipo: "' + tipoEstufagem.value + '" }',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        var data = data.d;
                        data = $.parseJSON(data);
                        var lineChartProcesso = [];
                        var lineChartCntr = [];
                        var lineChartTeus = [];
                        var label = [];
                        $("#graph").empty();
                        if (data != null) {
                            for (var i = 0; i < data.length; i++) {
                                lineChartProcesso.push(data[i]["PROCESSO"]);
                                lineChartCntr.push(data[i]["CONTAINER"]);
                                lineChartTeus.push(data[i]["TEUS"]);
                                label.push(data[i]["PERIODO"]);
                            }
                            $("#graph").append("<canvas id='mainGraph' style='width:100%; height: 300px; max-height: 300px'></canvas>");
                            graficoGeralPizza(lineChartProcesso, lineChartCntr, lineChartTeus, label);
                        }
                        else {
                            lineChartProcesso.push(0);
                            lineChartCntr.push(0);
                            lineChartTeus.push(0);
                            label.push(mesInicial.value + "/" + anoInicial.value);
                            $("#graph").append("<canvas id='mainGraph' style='width:100%; height: 300px; max-height: 300px'></canvas>");
                            graficoGeralPizza(lineChartProcesso, lineChartCntr, lineChartTeus, label);
                        }
                    }
                });

                $.ajax({
                    type: "POST",
                    url: "Gerencial.asmx/CarregarProcessosPizza",
                    data: '{anoI:"' + anoInicial.value + '", mesI: "' + mesInicial.value + '",vendedor: "' + vendedor.value + '",tipo: "' + tipoEstufagem.value + '" }',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        var data = data.d;
                        data = $.parseJSON(data);
                        var lineChartIMP = [];
                        var lineChartAR = [];
                        var lineChartEXP = [];
                        var label = [];
                        $("#processGraph").empty();
                        if (data != null) {
                            for (var i = 0; i < data.length; i++) {
                                lineChartIMP.push(data[i]["IMP"]);
                                lineChartAR.push(data[i]["AR"]);
                                lineChartEXP.push(data[i]["EXP"]);
                                label.push(data[i]["PERIODO"]);
                            }
                            $("#processGraph").append("<canvas id='pgraph' style='width:100%;height:300px;max-height: 300px''></canvas>");
                            graficoProcessosPizza(lineChartIMP, lineChartEXP, lineChartAR, label);
                        }
                        else {
                            lineChartIMP.push(0);
                            lineChartAR.push(0);
                            lineChartEXP.push(0);
                            label.push(mesInicial.value + "/" + anoInicial.value);
                            $("#processGraph").append("<canvas id='pgraph' style='width:100%; height: 300px; max-height: 300px'></canvas>");
                            graficoProcessosPizza(lineChartIMP, lineChartEXP, lineChartAR, label);
                        }
                    }
                });

                $.ajax({
                    type: "POST",
                    url: "Gerencial.asmx/CarregarQtdCntrPizza",
                    data: '{anoI:"' + anoInicial.value + '", mesI: "' + mesInicial.value + '",vendedor: "' + vendedor.value + '",tipo: "' + tipoEstufagem.value + '" }',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        var data = data.d;
                        data = $.parseJSON(data);
                        var lineChartIMP = [];
                        var lineChartEXP = [];
                        var label = [];
                        $("#cntrGraph").empty();
                        if (data != null) {
                            for (var i = 0; i < data.length; i++) {
                                lineChartIMP.push(data[i]["IMP"]);
                                lineChartEXP.push(data[i]["EXP"]);
                                label.push(data[i]["PERIODO"]);
                            }
                            $("#cntrGraph").append("<canvas id='cgraph' style='width:100%;height: 300px;max-height: 300px''></canvas>");
                            graficoCntrPizza(lineChartIMP, lineChartEXP, label);
                        }
                        else {
                            lineChartIMP.push(0);
                            lineChartEXP.push(0);
                            label.push(mesInicial.value + "/" + anoInicial.value);
                            $("#cntrGraph").append("<canvas id='cgraph' style='width:100%; height: 300px; max-height: 300px'></canvas>");
                            graficoCntrPizza(lineChartIMP, lineChartEXP, label);
                        }
                    }
                });

                $.ajax({
                    type: "POST",
                    url: "Gerencial.asmx/CarregarTeusPizza",
                    data: '{anoI:"' + anoInicial.value + '", mesI: "' + mesInicial.value + '",vendedor: "' + vendedor.value + '",tipo: "' + tipoEstufagem.value + '" }',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        var data = data.d;
                        data = $.parseJSON(data);
                        var lineChartProcesso = [];
                        var lineChartCntr = [];
                        var label = [];
                        $("#teusGraph").empty();
                        if (data != null) {
                            for (var i = 0; i < data.length; i++) {
                                lineChartProcesso.push(data[i]["IMP"]);
                                lineChartCntr.push(data[i]["EXP"]);
                                label.push(data[i]["PERIODO"]);
                            }
                            $("#teusGraph").append("<canvas id='tGraph' style='width:100%;height: 300px;max-height: 300px'></canvas>");
                            graficoTeusPizza(lineChartProcesso, lineChartCntr, label)
                        }
                        else {
                            lineChartProcesso.push(0);
                            lineChartCntr.push(0);
                            label.push(mesInicial.value + "/" + anoInicial.value);
                            $("#teusGraph").append("<canvas id='tGraph' style='width:100%; height: 300px; max-height: 300px'></canvas>");
                            graficoTeusPizza(lineChartProcesso, lineChartCntr, label)
                        }
                    }
                });
            }
        }
    </script>
</asp:Content>
