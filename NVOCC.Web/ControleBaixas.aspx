<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ControleBaixas.aspx.cs" Inherits="ABAINFRA.Web.ControleBaixas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row principal">
        <div class="col-lg-12 col-md-12 col-sm-12">
            <br />
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">Controle de Baixas - Personal Freight
                    </h3>
                </div>
                <div class="panel-body">
                    <ul class="nav nav-tabs" role="tablist">
                        <li class="active" id="tabprocessoExpectGrid">
                            <a href="#baixaPersonal" id="linkBaixaPersonalTab" role="tab" data-toggle="tab">
                                <i class="fa fa-edit" style="padding-right: 8px;"></i>Baixas Personal Freight
                            </a>
                        </li>
                        <li id="tabprocessoEstimativaGrid">
                            <a href="#compararRelatorio" id="linkpCompararRelatorioTab" role="tab" data-toggle="tab">
                                <i class="fa fa-edit" style="padding-right: 8px;"></i>Comparar Relatórios
                            </a>
                        </li>
                    </ul>
                    <div class="tab-content">
                        <div class="tab-pane fade active in" id="baixaPersonal">
                            <div class="row topMarg">
                                <div class="row" style="display: flex; margin:auto; margin-top:10px;">
                                    <div style="margin: auto">
                                        <button type="button" id="btnExportSaldo" class="btn btn-primary" onclick="exportCSV('Saldo.csv')">Exportar Grid - CSV</button>
                                    </div>
                                </div>
                                
                                <div class="row flexdiv topMarg" style="padding: 0 15px">
                                    <div class="col-sm-2">
                                        <div class="form-group">
                                            <label class="control-label">Filtro</label>
                                            <select id="ddlFilterSaldo" class="form-control">
                                                <option value="">Selecione</option>
                                                <option value="1" selected>NR DOCUMENTO FISCAL</option>
                                                <option value="2">PEDIDO DE COMPRA</option>
                                            </select>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <div class="form-group">
                                            <label class="control-label">*</label>
                                            <input id="txtFiltro" class="form-control" type="text" />
                                        </div>
                                    </div>
                                    <div class="col-sm-1">
                                        <div class="form-group">
                                            <label class="control-label">Competencia Inicial</label>
                                            <input id="dtCompetenciaInicial" class="form-control competencia" type="text" />
                                        </div>
                                    </div>
                                    <div class="col-sm-1">
                                        <div class="form-group">
                                            <label class="control-label">Competencia Final</label>
                                            <input id="dtCompetenciaFinal" class="form-control competencia" type="text" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <button type="button" id="btnConsultarTaxas" class="btn btn-primary">Consultar</button>
                                    </div>
                                    <div class="form-group" style="margin-left: 20px">
                                        <button type="button" id="btnLimparFiltros" class="btn btn-primary">Limpar Filtro</button>
                                        <button type="button" id="btnFiltroAvancado" class="btn btn-primary">Filtro Avançado</button>
                                        <button type="button" id="btnCriarPedido" class="btn btn-warning">Criar Pedido Compra (TOTVS)</button>
                                        <button type="button" id="btnEstorno" class="btn btn-danger">Estornar NF/ND (TOTVS)</button>
                                    </div>
                                </div> 
                                <div class="table-responsive fixedDoubleHead topMarg">
                                    <table id="grdSaldo" class="table tablecont">
                                        <thead>
                                            <tr>
                                                <th class="text-center" scope="col">MARCAR / DESMARCAR TODOS</th>
                                                <th class="text-center" scope="col">EDITAR</th>
                                                <th class="text-center" scope="col">Nº DOCUMENTO FISCAL</th>
                                                <th class="text-center" scope="col">TIPO DOCUMENTO FISCAL</th>
                                                <th class="text-center" scope="col">PEDIDO DE COMRPA</th>
                                                <th class="text-center" scope="col">BAIXA TOTVS</th>
                                                <th class="text-center" scope="col">DATA DE CHEGADA</th>
                                                <th class="text-center" scope="col">Nº PROCESSO</th>
                                                <th class="text-center" scope="col">MBL</th>
                                                <th class="text-center" scope="col">HBL</th>
                                                <th class="text-center" scope="col">DATA PAGAMENTO</th>
                                                <th class="text-center" scope="col">COMPETÊNCIA (EMISSÃO DOC FISCAL)</th>
                                                <th class="text-center" scope="col">ITEM DESPESA</th>
                                                <th class="text-center" scope="col">VALOR</th>
                                                <th class="text-center" scope="col">STATUS</th>
                                            </tr>
                                        </thead>
                                        <tbody id="grdSaldoBody">

                                        </tbody>
                                        <tfoot id="grdSaldoFooter" style="position: sticky !important;bottom: 0;background-color: #e6eefa;">
                                             
                                        </tfoot>
                                    </table>
                                </div>
                            </div>
                        </div>

                        <div class="tab-pane fade" id="compararRelatorio">
                            <div class="row topMarg">                                
                                <div class="row flexdiv" style="padding: 0 15px;justify-content: center;">
                                    <div class="col-sm-2">
                                        <div class="form-group">
                                            <input id="excelInput" class="form-control"  type="file" required="required"/>
                                        </div>
                                    </div>
                                </div> 
                                <div class="row flexdiv" style="justify-content:center">
                                    <div class="col-sm-3 flexdiv" style="justify-content: center; align-items: center; gap: 5px;">
                                        <button type="button" id="btnLiberarBaixa" disabled class="btn btn-success">Liberar para Baixa</button>
                                        <button type="button" id="btnExportarCSV" class="btn btn-primary">Exportar CSV</button>
                                        <button type="button" id="btnValidarProcessos" class="btn btn-primary">Validar Processos</button>
                                    </div>
                                </div>
                                <div class="table-responsive fixedDoubleHead topMarg" id="tableContainer">
                                    <table id="grdTaxas" class="table tablecont">
                                        <thead>
                                            <tr>
                                                <th class="text-center" scope="col">REFERÊNCIA CLIENTE</th>
                                                <th class="text-center" scope="col">ITEM DE DESPESA</th>
                                                <th class="text-center" scope="col">NÚMERO MASTER</th>
                                                <th class="text-center" scope="col">NÚMERO HOUSE</th>
                                                <th class="text-center" scope="col">VALOR</th>
                                                <th class="text-center" scope="col">DATA DE CHEGADA</th>
                                            </tr>
                                        </thead>
                                        <tbody id="grdTaxasBody">

                                        </tbody>
                                    </table>
                                </div>

                                <div class="modal fade bd-example-modal-md" id="modalInformacaoAdicional" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                                    <div class="modal-dialog modal-dialog-centered modal-md" role="document">
                                        <div class="modal-content">
                                            <div class="modal-header">
                                                <h5 class="modal-title modalTitle" id="modalFCLexpoTitle">Informações Adicionais</h5>
                                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                    <span aria-hidden="true">&times;</span>
                                                </button>
                                            </div>
                                            <div class="modal-body" style="padding:20px">
                                                <div class="form-group row">
                                                <label for="dtCompetencia" class="col-sm-2 col-form-label">Competência</label>
                                                <div class="col-sm-10">
                                                  <input type="text" class="form-control" id="dtCompetencia" placeholder="MM/AAAA">
                                                </div>
                                              </div>
                                              <div class="form-group row">
                                                <label for="nrDocumento" class="col-sm-2 col-form-label">Nº Documento Fiscal</label>
                                                <div class="col-sm-10">
                                                  <input type="text" class="form-control" id="nrDocumento" placeholder="Nº Documento Fiscal">
                                                </div>
                                              </div>
                                              <fieldset class="form-group">
                                                <div class="row">
                                                  <label class="col-form-label col-sm-2 pt-0">Tipo Documento</label>
                                                  <div class="col-sm-10">
                                                    <div class="form-check">
                                                      <input class="form-check-input" type="radio" name="tipoDoc" id="tpDocNf" value="NF" checked>
                                                      <label class="form-check-label" for="tpDocNf">
                                                        NF
                                                      </label>
                                                    </div>
                                                    <div class="form-check">
                                                      <input class="form-check-input" type="radio" name="tipoDoc" id="tpDocNd" value="ND">
                                                      <label class="form-check-label" for="tpDocNd">
                                                        ND
                                                      </label>
                                                    </div>
                                                  </div>
                                                </div>
                                              </fieldset>
                                            </div>
                                            <div class="modal-footer">
                                                <button type="button" id="btnEnviarBaixa" class="btn btn-success">Salvar</button>
                                                <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                                            </div>
                                        </div>
                                    </div>
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
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/xlsx/0.13.5/xlsx.full.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/xlsx/0.13.5/jszip.js"></script>
    <script src="Content/js/papaparse.min.js"></script>    
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jspdf/1.5.3/jspdf.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/html2pdf.js/0.9.2/html2pdf.bundle.js"></script>
    <script src="//cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>
    <script>

       
        document.addEventListener('DOMContentLoaded', function () {

            //Criar pedido de Compra TOTVS
            const btnCriarPedido = document.getElementById('btnCriarPedido');
            btnCriarPedido.addEventListener('click', criarPedido);

            function criarPedido() {
                const contas = [];
                const processosCheck = document.querySelectorAll('input[name="enviarTaxa"]:checked');
                processosCheck.forEach(e => {
                    contas.push(e.value)
                })

                swal({
                    text: 'Deseja criar pedido de compra para as taxas selecionadas?',
                    button: {
                        text: "Sim",
                        closeModal: false,
                    },
                }).then((result) => {
                    console.log(result);
                    const fetchPromise = fetchPedido(contas);

                    fetchPromise.then((data) => {
                        var mensagem = data.resultado[0].mensagem;
                        var match = mensagem.match(/\[\d+\]/);
                        if (data.resultado[0].code == "200") {
                            swal({
                                title: "Pedido Criado com Sucesso",
                                text: "Numero do Pedido: " + match[0].replace(/\D/g, '') + ""
                            });
                        } else {
                            swal({
                                title: "Erro ao criar Pedido",
                                text: "Erro: " + data.resultado[0].mensagem + ""
                            });
                        }
                        listarTaxas();
                    }).catch((data) => {
                        swal({
                            title: "Erro na API"
                        });
                        listarTaxas();
                    })
                })
            }

            function fetchPedido(dados) {
                return new Promise((resolve, reject) => {
                    $.ajax({
                        type: "POST",
                        url: "Services/Taxas.asmx/criarPedido",
                        data: JSON.stringify({dados: (dados)}),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        beforeSend: function () {
                            $("#grdSaldoBody").empty();
                            $("#grdSaldoBody").append("<tr><td class='text-center' colspan='14'><div class='loader text-center'></div></td></tr>");
                        },
                        success: function (response) {
                            $("#grdSaldoBody").empty();
                            var result = response.d;
                            result = $.parseJSON(result);
                            resolve(result);
                        },
                        error: function (error) {
                            $("#grdSaldoBody").empty();
                            reject(error);
                        }
                    });
                });
            }

            const btnEstornarPedido = document.getElementById('btnEstorno');
            btnEstornarPedido.addEventListener('click', estornarPedido);

            function estornarPedido() {
                const contas = [];
                const processosCheck = document.querySelectorAll('input[name="enviarTaxa"]:checked');
                processosCheck.forEach(e => {
                    contas.push(e.value)
                })

                swal({
                    text: 'Deseja Estonar pedido de compra para as taxas selecionadas?',
                    button: {
                        text: "Sim",
                        closeModal: false,
                    },
                }).then((result) => {
                    console.log(result);
                    const fetchPromise = fetchEstornoPedido(contas);

                    fetchPromise.then((data) => {
                        var mensagem = data.resultado[0].mensagem;
                        var match = mensagem.match(/\[\d+\]/);
                        if (data.resultado[0].code == "200") {
                            swal({
                                title: "Pedido Estornado com Sucesso",
                                text: "Numero do Pedido: " + match[0].replace(/\D/g, '') + ""
                            });
                        } else {
                            swal({
                                title: "Erro ao Estornar Pedido",
                                text: "Erro: " + data.resultado[0].mensagem + ""
                            });
                        }
                        listarTaxas();
                    }).catch((data) => {
                        swal({
                            title: "Erro na API"
                        });
                        listarTaxas();
                    })
                })
            }

            function fetchEstornoPedido(dados) {
                return new Promise((resolve, reject) => {
                    $.ajax({
                        type: "POST",
                        url: "Services/Taxas.asmx/EstornarPedido",
                        data: JSON.stringify({ dados: (dados) }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        beforeSend: function () {
                            $("#grdSaldoBody").empty();
                            $("#grdSaldoBody").append("<tr><td class='text-center' colspan='14'><div class='loader text-center'></div></td></tr>");
                        },
                        success: function (response) {
                            $("#grdSaldoBody").empty();
                            var result = response.d;
                            result = $.parseJSON(result);
                            resolve(result);
                        },
                        error: function (error) {
                            $("#grdSaldoBody").empty();
                            reject(error);
                        }
                    });
                });
            }

             //Importando Excel para Table HTML
            const excelInput = document.getElementById('excelInput');
            excelInput.addEventListener('change', handleFile);

            function handleFile(event) {
                const file = event.target.files[0];
                if (file) {
                    const reader = new FileReader();
                    reader.onload = function (e) {
                        const data = new Uint8Array(e.target.result);
                        const workbook = XLSX.read(data, { type: 'array' });
                        const sheetName = workbook.SheetNames[0];
                        const sheetData = XLSX.utils.sheet_to_json(workbook.Sheets[sheetName], { header: 1 });

                        createTable(sheetData);
                    };
                    reader.readAsArrayBuffer(file);
                }
            }
            function createTable(data) {
                const tableBody = document.getElementById('grdTaxasBody');
                for (let i = 1; i < data.length; i++) {
                    const row = document.createElement('tr');
                    for (let j = 0; j < data[i].length; j++) {
                        const cell = document.createElement('td');
                        cell.textContent = data[i][j];
                        row.appendChild(cell);
                    }
                    tableBody.appendChild(row);
                }
            }


            //Validando Taxas
            const btnValidarProcessos = document.getElementById('btnValidarProcessos');
            btnValidarProcessos.addEventListener('click', validarProcesso);

            function validarProcesso() {
                swal({
                    text: 'Deseja validar as taxas?',
                    button: {
                        text: "Sim",
                        closeModal: false,
                    },
                }).then(() => {
                    const dataTable = document.getElementById('grdTaxasBody');
                    const rows = dataTable.getElementsByTagName('tr');
                    const btnLiberarBaixa = document.getElementById('btnLiberarBaixa');
                    const data = [];
                    var dataSuccess = 0;
                    var dataFailed = 0;

                    const fetchPromises = [];

                    for (let i = 0; i < rows.length; i++) {
                        const cells = rows[i].getElementsByTagName('td');
                        const rowData = {
                            "PROCESSO": cells[0].textContent,
                            "ITEM": cells[1].textContent,
                            "HOUSE": cells[2].textContent,
                            "MASTER": cells[3].textContent,
                            "VALOR": cells[4].textContent,
                            "CHEGADA": cells[5].textContent
                        };
                        data.push(rowData);

                        // Push a fetch promise for each row
                        const fetchPromise = fetchConferirTaxas(rowData); // Assume fetchConferirTaxas is a function that returns a Promise for AJAX request
                        fetchPromises.push(fetchPromise);
                    }

                    Promise.all(fetchPromises)
                        .then(responses => {
                            responses.forEach((response, i) => {
                                const row = rows[i]; // Get the corresponding row element

                                // Process the response and modify styles
                                if (response == "1") {
                                    console.log(1);
                                    row.style.color = '#10a81c';
                                    row.style.fontWeight = 'bold';
                                    dataSuccess++;
                                } else {
                                    console.log(0);
                                    row.style.color = '#d40202';
                                    row.style.fontWeight = 'bold';
                                    dataFailed++;
                                }
                            });

                            let allRowsFound = true;
                            for (let i = 0; i < rows.length; i++) {
                                if (rows[i].style.color === "rgb(212, 2, 2)") { // Compare to RGB color value
                                    allRowsFound = false;
                                    break;
                                }
                            }

                            if (allRowsFound) {
                                btnLiberarBaixa.disabled = false;
                            }
                            return { sucesso: dataSuccess, falha: dataFailed }


                        }).then(data => {
                            swal({
                                title: "Verificação Finalizada",
                                text: "Taxas validadas: " + data.sucesso + ", Taxas não econtradas: " + data.falha + ""
                            });
                        }).catch(error => {
                            console.error(error);
                        });
                })
            }
            function fetchConferirTaxas(dados) {
                return new Promise((resolve, reject) => {
                    $.ajax({
                        type: "POST",
                        url: "Services/Taxas.asmx/conferirTaxas",
                        data: JSON.stringify({ dados: (dados) }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            var result = response.d;
                            result = $.parseJSON(result);
                            resolve(result);
                        },
                        error: function (error) {
                            reject(error);
                        }
                    });
                });
            }


            //Liberar Baixa
            const btnLiberarBaixa = document.getElementById('btnLiberarBaixa');
            btnLiberarBaixa.addEventListener('click', function () {
                $("#modalInformacaoAdicional").modal('show');
            });

            const btnEnviarBaixa = document.getElementById('btnEnviarBaixa');
            btnEnviarBaixa.addEventListener('click', enviarBaixa);

            function enviarBaixa() {
                const dataTable = document.getElementById('grdTaxasBody');
                const competencia = document.getElementById('dtCompetencia').value;
                const documento = document.getElementById('nrDocumento').value;
                const tipo = document.querySelector('input[name="tipoDoc"]:checked').value;
                const rows = dataTable.getElementsByTagName('tr');
                const data = [];


                for (let i = 0; i < rows.length; i++) {
                    const cells = rows[i].getElementsByTagName('td');
                    const rowData = {
                        "PROCESSO": cells[0].textContent,
                        "ITEM": cells[1].textContent,
                        "HOUSE": cells[2].textContent,
                        "MASTER": cells[3].textContent,
                        "VALOR": cells[4].textContent,
                        "CHEGADA": cells[5].textContent,
                        "COMPETENCIA": competencia,
                        "DOCUMENTO": documento,
                        "TIPO": tipo
                    };
                    data.push(rowData);
                }
                const fetchPromise = liberarBaixa(data);

                fetchPromise.then((response) => {
                    if (response) {
                        $("#modalInformacaoAdicional").modal('hide');
                        Swal.fire('Saved!', '', 'success')
                    } else {
                        $("#modalInformacaoAdicional").modal('hide');
                        Swal.fire('Changes are not saved', '', 'info')
                    }
                })
            }
            function liberarBaixa(dados) {
                return new Promise((resolve, reject) => {
                    $.ajax({
                        type: "POST",
                        url: "Services/Taxas.asmx/LiberarTaxas",
                        data: JSON.stringify({ dados: (dados) }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            var result = response.d;
                            result = $.parseJSON(result);
                            resolve(result);
                        },
                        error: function (error) {
                            reject(error);
                        }
                    });
                });
            }

            //Listar Taxas Liberadas
            const btnConsultarTaxas = document.getElementById('btnConsultarTaxas');
            btnConsultarTaxas.addEventListener('click', listarTaxas);
           
            function listarTaxas() {
                var result = "";
                const filtro = {
                }
                $("#grdSaldoBody").empty();
                const fetchPromise = fetchListarTaxas(filtro);

               fetchPromise.then(response => {
                   $("#grdSaldoBody").empty();
                    response.forEach((response) => {
                        result += "<tr>";
                        result += "<td><input type='checkbox' value='" + response["ID_CONTA_PAGAR_RECEBER_ITENS"] +"' name='enviarTaxa'/></td>";
                        result += "<td>" + response["ID_CONTA_PAGAR_RECEBER_ITENS"] + "</td>";
                        result += "<td>" + response["NR_DOCUMENTO"] + "</td>";
                        result += "<td>" + response["TP_DOCUMENTO"] + "</td>";
                        result += "<td>" + response["NR_PEDIDO_COMPRA_TOTVS"] + "</td>";
                        result += "<td>" + response["BAIXA_TOTVS"] + "</td>";
                        result += "<td>" + response["DT_CHEGADA"] + "</td>";
                        result += "<td>" + response["NR_PROCESSO"] + "</td>";
                        result += "<td>" + response["MASTER"] + "</td>";
                        result += "<td>" + response["HOUSE"] + "</td>";
                        result += "<td>" + response["DT_LIQUIDACAO"] + "</td>";
                        result += "<td>" + response["DT_COMPETENCIA"] + "</td>";
                        result += "<td>" + response["NM_ITEM_DESPESA"] + "</td>";
                        result += "<td>" + response["VL_TAXA_CALCULADO"] + "</td>";
                        result += "<td>" + response["DS_STATUS_TOTVS"] + "</td>";
                        result += "</tr>";
                    })
                   $("#grdSaldoBody").append(result);
                })
                
            }

            function fetchListarTaxas() {
                return new Promise((resolve, reject) => {
                    $.ajax({
                        type: "POST",
                        url: "Services/Taxas.asmx/listarTaxas",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        beforeSend: function () {
                            $("#grdSaldoBody").empty();
                            $("#grdSaldoBody").append("<tr><td class='text-center' colspan='14'><div class='loader text-center'></div></td></tr>");
                        },
                        success: function (response) {
                            $("#grdSaldoBody").empty();
                            var result = response.d;
                            result = $.parseJSON(result);
                            resolve(result);
                        },
                        error: function (error) {
                            $("#grdSaldoBody").empty();
                            reject(error);
                        }
                    });
                });
            }

            //Exportar CSV
            const btnExportCSV = document.getElementById("btnExportarCSV");
            btnExportCSV.addEventListener('click', function () {
                exportEstimativaCSV('TaxasPersonal');
            })

            function exportEstimativaCSV(filename) {
                var csv = [];
                var rows = document.querySelectorAll("#grdTaxas tr");

                for (var i = 0; i < rows.length; i++) {
                    var row = [], cols = rows[i].querySelectorAll("#grdTaxas td, #grdTaxas th");

                    for (var j = 0; j < cols.length; j++)
                        row.push(cols[j].innerText);

                    csv.push(row.join(";"));
                }

                // Download CSV file
                exportTableToCSVEstimativaPagamentosRecebimentos(csv.join("\n"), filename);
            }
            function exportTableToCSVEstimativaPagamentosRecebimentos(csv, filename) {
                var csvFile;

                var downloadLink;

                // CSV file
                csvFile = new Blob(["\uFEFF" + csv], { type: "text/csv;charset=utf-8;" });
                // Download link
                downloadLink = document.createElement("a");
                // File name
                downloadLink.download = filename;
                // Create a link to the file
                downloadLink.href = window.URL.createObjectURL(csvFile);
                // Hide download link
                downloadLink.style.display = "none";
                // Add the link to DOM
                document.body.appendChild(downloadLink);
                // Click download link
                downloadLink.click();
            }
        });
    </script>
</asp:Content>