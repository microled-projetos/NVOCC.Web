<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GerencialMaster.aspx.cs" Inherits="ABAINFRA.Web.GerencialMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row principal">
        <div class="col-lg-12 col-md-12 col-sm-12">
            <br />
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">Gerencial Master
                    </h3>
                </div>
                <div class="panel-body">
                    <ul class="nav nav-tabs" role="tablist">
                        <li class="active" id="tabprocessoExpectGrid">
                            <a href="#processoExpectGrid" id="linkprocessoExcepctGrid" role="tab" data-toggle="tab">
                                <i class="fa fa-edit" style="padding-right: 8px;"></i>Gerencial Master
                            </a>
                        </li>
                    </ul>
                    <div class="tab-content">
                        <div class="tab-pane fade active in" id="processoExpectGrid">
                            <div class="row topMarg flexdiv">
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <button type="button" id="btnConsulta" class="btn btn-primary">Gerar CSV</button>
                                    </div>
                                </div>
                            </div>
                            <div class="table-responsive tableFixHead topMarg">
                                <table id="grdMasterProcesso" class="table tablecont">
                                    <thead>
                                        <tr>
                                            <th class="text-center" scope="col">MBL</th>
                                            <th class="text-center" scope="col">CARRIER</th>
                                            <th class="text-center" scope="col">TIPO ESTUFAGEM - M - H</th>
                                            <th class="text-center" scope="col">CNTR 20</th>
                                            <th class="text-center" scope="col">CNTR 40</th>
                                            <th class="text-center" scope="col">ORIGEM</th>
                                            <th class="text-center" scope="col">DESTINO</th>
                                            <th class="text-center" scope="col">ETD</th>
                                            <th class="text-center" scope="col">ETA</th>
                                            <th class="text-center" scope="col">DATA CHEGADA</th>
                                            <th class="text-center" scope="col">DATA PAGAMENTO</th>
                                            <th class="text-center" scope="col">VALOR PAGAMENTO</th>
                                            <th class="text-center" scope="col">TX CAMBIO PAGTO</th>
                                            <th class="text-center" scope="col">DATA RECEBIMENTO</th>
                                            <th class="text-center" scope="col">VALOR RECEBIMENTO</th>
                                            <th class="text-center" scope="col">TX CAMBIO RECEBTO</th>
                                        </tr>
                                    </thead>
                                    <tbody id="grdMasterProcessoBody">
                                       
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/xlsx/0.13.5/xlsx.full.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/xlsx/0.13.5/jszip.js"></script>
    <script src="Content/js/papaparse.min.js"></script>    
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jspdf/1.5.3/jspdf.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/html2pdf.js/0.9.2/html2pdf.bundle.js"></script>
    <script>
        $(document).ready(function () {
            $.ajax({
                type: "POST",
                url: "Gerencial.asmx/listarProcessosMaster",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                beforeSend: function () {
                    $("#grdMasterProcessoBody").empty();
                    $("#grdMasterProcessoBody").append("<tr><td colspan='19'><div class='loader'></div></td></tr>");
                },
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    $("#grdMasterProcessoBody").empty();
                    if (dado != null) {
                        for (let i = 0; i < dado.length; i++) {
                            $("#grdMasterProcessoBody").append("<tr><td class='text-center'>" + dado[i]["NR_BL"] + "</td><td class='text-center'>" + dado[i]["TRANSPORTADOR"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["NMTPESTUFAGEM"] + "</td><td class='text-center'>" + dado[i]["QTDE20"] + "</td><td class='text-center'>" + dado[i]["QTDE40"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["ORIGEM"] + "</td><td class='text-center'>" + dado[i]["DESTINO"] + "</td><td class='text-center'>" + dado[i]["DTEMBARQUE"] + "</td>" +
                                "<td class='text-center'>" + dado[i]["DTPREVISAOCHEGADA"] + "</td><td class='text-center'>" + dado[i]["DTCHEGADA"] + "</td><td class='text-center'></td>" +
                                "<td class='text-center'></td><td class='text-center'></td><td class='text-center'></td>" +
                                "<td class='text-center'></td><td class='text-center'></td></tr>");
                        }
                    } else {
                        $("#grdMasterProcessoBody").empty();
                        $("#grdMasterProcessoBody").append("<tr id='msgEmptyWeek'><td colspan='19' class='alert alert-light text-center'>Tabela vazia.</td></tr>");
                    }
                }
            })
        })
    </script>
</asp:Content>
