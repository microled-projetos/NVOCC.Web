<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Financeiro.aspx.vb" Inherits="NVOCC.Web.Financeiro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .btnn {
            background-color: #d5d8db;
            margin: 3px;
            font-size: 13px
        }

        .selected1 {
            color: black;
            font-family: verdana;
            font-size: 8pt;
            background-color: #e6c3a5;
        }

        .none {
            display: none
        }

        th {
            position: sticky !important;
            top: 0;
            background-color: #e6eefa;
            text-align: center;
        }

        td, th {
            padding: 0;
            padding-top: 5px;
            margin: 0;
        }
    </style>
    <div class="row principal">
        <div class="col-lg-12 col-md-12 col-sm-12">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">FINANCEIRO
                    </h3>
                </div>

                <div class="panel-body">
                    <div class="tab-pane fade active in" id="consulta">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="always" ChildrenAsTriggers="True">
                            <ContentTemplate>

                                <div class="alert alert-success" id="divSuccess" runat="server" visible="false">
                                    <asp:Label ID="lblmsgSuccess" runat="server"></asp:Label>
                                </div>
                                <div class="alert alert-danger" id="divErro" runat="server" visible="false">
                                    <asp:Label ID="lblmsgErro" runat="server"></asp:Label>
                                </div>
<div class="row linhabotao" >           
                       <div class="col-sm-4">
                           <asp:Label ID="Label6" runat="server">CONTAS A PAGAR:</asp:Label><br />
                           <div style="border: ridge 1px;">
                          <asp:LinkButton ID="lkSolicitacaoPagamento" runat="server" CssClass="btn btnn btn-default btn-sm">Solicitar Pagamento</asp:LinkButton>
                            <asp:LinkButton ID="lkMontagemPagamento" runat="server" CssClass="btn btnn btn-default btn-sm">Montar Pagamento</asp:LinkButton>
                           <asp:LinkButton ID="lkBaixaCancel_Pagar" runat="server" CssClass="btn btnn btn-default btn-sm">Baixar/Cancelar</asp:LinkButton></div>

                       </div> 
                                
                      
                       <div class="col-sm-4">
                                      <asp:Label ID="Label1" runat="server">CONTAS A RECEBER:</asp:Label><br />
                                        <div style="border: ridge 1px;">

                          <asp:LinkButton ID="lkCalcularRecebimento" runat="server" CssClass="btn btnn btn-default btn-sm">Calcular Recebimento</asp:LinkButton>
                           <asp:LinkButton ID="lkEmissaoND" runat="server" CssClass="btn btnn btn-default btn-sm">Emitir ND</asp:LinkButton>
                            <asp:LinkButton ID="lkBaixaCancel_Receber" runat="server" CssClass="btn btnn btn-default btn-sm">Baixar/Cancelar</asp:LinkButton>
                           <asp:LinkButton ID="lkFaturar" runat="server" CssClass="btn btnn btn-default btn-sm">Faturar Recebimento</asp:LinkButton></div>
                                        </div>
                       
                 
                       <div class="col-sm-4">
                                      <asp:Label ID="Label5" runat="server">DEMONSTRATIVOS:</asp:Label><br />
                                        <div style="border: ridge 1px;">

                      <button type="button" id="btnPagamentosRecebimentos" class="btn btnn"  data-toggle="modal" onclick="PagamentosRecebimentos()">Pagamentos e Recebimentos</button> 
                            <button type="button" id="btnEstimativaPagamentosRecebimentos" class="btn btnn" data-toggle="modal" onclick="EstimativaPagamentosRecebimentos()">Estimativa Pagamentos e Recebimentos</button>             </div>
                                
</div>
    </div>
    
    <br /><br /><br />
                   <div class="row linhabotao" >
   
                       <div class="col-sm-2">
                            <asp:Label ID="Label2" runat="server">FILTRO:</asp:Label>
                           <div class="form-group">
                               <asp:DropDownList ID="ddlFiltro" runat="server" CssClass="form-control" Font-Size="15px">
                                   <asp:ListItem Value="1">Número do Master</asp:ListItem>
                                   <asp:ListItem Value="4">Número do House</asp:ListItem>
                                   <asp:ListItem Value="2">Número do processo</asp:ListItem>
                                   <asp:ListItem Value="3" Selected="True">Todos em aberto</asp:ListItem>

                               </asp:DropDownList>
                           </div>

                       </div>
                       <div class="col-sm-2" >
                           <div class="form-group">
                                                           <asp:Label ID="Label3" Style="color:white" runat="server">x</asp:Label>

                               <asp:TextBox ID="txtPesquisa" runat="server" CssClass="form-control"></asp:TextBox>
                           </div>
                       </div>  
                       <div class="col-sm-1" >
                           <div class="form-group">
                                                           <asp:Label ID="Label4" Style="color:white" runat="server">X</asp:Label><br />

                               <asp:Button runat="server" Text="Pesquisar" ID="btnPesquisa" CssClass="btn btn-success" />

                           </div>
                       </div>
                       <div class="col-sm-2" style="border: ridge 1px;">
                                                <div class="form-group">
                                                    <label class="control-label">Via Transporte:</label>
                                                    <asp:RadioButtonList ID="rdTransporte" runat="server" RepeatDirection="Horizontal" AutoPostBack="true">
                                                        <asp:ListItem Value="1" Selected="True">&nbsp;Marítimo</asp:ListItem>
                                                        <asp:ListItem Value="2">&nbsp;Aéreo</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                       <div class="col-sm-2" style="border: ridge 1px; margin-left: 10px">
                                                <div class="form-group">
                                                    <label class="control-label">Serviço:</label>
                                                    <asp:RadioButtonList ID="rdServico" runat="server" RepeatDirection="Horizontal" AutoPostBack="true">
                                                        <asp:ListItem Value="1" Selected="True">&nbsp;Importação</asp:ListItem>
                                                        <asp:ListItem Value="2">&nbsp;Exportação</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                          

                        
                   </div> 
                    
          

                               

                               

                                
                            </ContentTemplate>
                            <Triggers>
                                <%--<asp:AsyncPostBackTrigger ControlID="lkFiltrar" />
             <asp:PostBackTrigger ControlID="lkImprimir" />
                   <asp:PostBackTrigger ControlID="btnImprimir" />--%>
                            </Triggers>
                        </asp:UpdatePanel>
                        <br />
                        <asp:UpdatePanel ID="updPainel1" runat="server" UpdateMode="always" ChildrenAsTriggers="True">
                            <ContentTemplate>
                                <div runat="server" id="divAuxiliar" visible="false">
                                    <asp:TextBox ID="txtID" runat="server" CssClass="form-control" Width="50PX"></asp:TextBox>
                                    <asp:TextBox ID="txtlinha" runat="server" CssClass="form-control" Width="50PX"></asp:TextBox>
                                </div>
                                <div class="table-responsive tableFixHead DivGrid" id="DivGrid" style="text-align: center">
                                    <asp:GridView ID="dgvFinanceiro" DataKeyNames="ID_BL" CssClass="table table-hover table-sm grdViewTable" dgAlwayShowSelection="True" dgRowSelect="True" GridLines="None" CellSpacing="-1" runat="server" DataSourceID="dsFinanceiro" AutoGenerateColumns="False" Style="max-height: 600px; overflow: auto;" AllowSorting="True" EmptyDataText="Nenhum registro encontrado." HeaderStyle-HorizontalAlign="Center" Visible="false">
                                        <Columns>
                                            <asp:BoundField DataField="ID_BL" HeaderText="#" Visible="false" />
                                                                                        <asp:BoundField DataField="NR_PROCESSO" HeaderText="Processo" SortExpression="NR_PROCESSO" />

                                            <asp:BoundField DataField="NR_BL_MASTER" HeaderText="MBL" SortExpression="NR_BL_MASTER" />
                                            <asp:BoundField DataField="DT_CHEGADA_MASTER" HeaderText="Chegada" SortExpression="DT_CHEGADA_MASTER" />
                                            <asp:BoundField DataField="NM_PARCEIRO_CLIENTE" HeaderText="Cliente" SortExpression="NM_PARCEIRO_CLIENTE" />
                                            <asp:BoundField DataField="NM_PARCEIRO_TRANSPORTADOR" HeaderText="Transportador" SortExpression="NM_PARCEIRO_TRANSPORTADOR" />
                                            <asp:BoundField DataField="QT_TAXAS_PAGAR" HeaderText="Qtd. taxas a Pagar" SortExpression="QT_TAXAS_PAGAR" />
                                            <asp:BoundField DataField="QT_TAXAS_PAGAS" HeaderText="Qtd. taxas Pagas" SortExpression="QT_TAXAS_PAGAS" />
                                            <asp:BoundField DataField="QT_TAXAS_PAGAR_ABERTA" HeaderText="Qtd. taxas (a Pagar) em aberto" SortExpression="QT_TAXAS_PAGAR_ABERTA" />
                                            <asp:BoundField DataField="QT_TAXAS_RECEBER" HeaderText="Qtd. taxas a Receber" SortExpression="QT_TAXAS_RECEBER" />
                                            <asp:BoundField DataField="QT_TAXAS_RECEBIDAS" HeaderText="Qtd. Quantidade taxas Recebidas" SortExpression="QT_TAXAS_RECEBIDAS" />
                                            <asp:BoundField DataField="QT_TAXAS_RECEBER_ABERTA" HeaderText="Quantidade taxas (a Receber) em aberto" SortExpression="QT_TAXAS_RECEBER_ABERTA" />
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnSelecionar" runat="server" CssClass="btn btn-primary btn-sm"
                                                        CommandArgument='<%# Eval("ID_BL") & "|" & Container.DataItemIndex %>' CommandName="Selecionar" Text="Selecionar"  OnClientClick="SalvaPosicao()"></asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle CssClass="headerStyle" />
                                    </asp:GridView>
                                </div>
                            
                            </ContentTemplate>
                            <Triggers>
                                <%--       <asp:AsyncPostBackTrigger ControlID="bntPesquisar" />
       <asp:AsyncPostBackTrigger EventName="Sorting" ControlID="dgvCotacao" />
       <asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="dgvCotacao" />
       <asp:AsyncPostBackTrigger ControlID="lkCalcular" />--%>
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>

                </div>


            </div>
        </div>

    </div>


<%--
     <div class="modal fade bd-example-modal-xl" id="modalPagamentoRecebimento" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                        <div class="modal-dialog modal-dialog-centered modal-xl" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title" id="modalPagamentoRecebimentoTitle"></h5>
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">&times;</span>
                                    </button>
                                </div>
                                <div class="modal-body">
                                    <div class="row" style="display: flex; margin:auto; margin-top:10px;">
                                        <div style="margin: auto">
                                            <button type="button" id="btnExportPagamentoRecebimento" class="btn btn-primary" onclick="exportCSV('Pagamento_Recebimento.csv')">Exportar Grid - CSV</button>
                                            <button type="button" id="btnPrintPagamentoRecebimento" class="btn btn-primary" onclick="printPagamentosRecebimentos()">Imprimir</button>
                                        </div>
                                    </div>
                                    <div class="row flexdiv topMarg">
                                        <div class="col-sm-2">
                                            <div class="form-group">
                                                <label class="control-label">Data Inicial:</label>
                                                <input id="txtDtInicialPagamentoRecebimento" class="form-control" type="date" required="required"/>
                                            </div>
                                        </div>
                                        <div class="col-sm-2">
                                            <div class="form-group">
                                                <label class="control-label">Data Final:</label>
                                                <input id="txtDtFinalPagamentoRecebimento" class="form-control" type="date" required="required"/>
                                            </div>
                                        </div>
                                        <div class="col-sm-2">
                                            <div class="form-group">
                                                <label class="control-label">Filtro</label>
                                                <select id="ddlFilterPagamentoRecebimento" class="form-control">
                                                    <option value="">Selecione</option>
                                                    <option value="1">Nr Processo</option>
                                                    <option value="2">Cliente</option>
                                                    <option value="3">Fornecedor</option>
                                                </select>
                                            </div>
                                        </div>
                                        <div class="col-sm-2">
                                            <div class="form-group">
                                                <label class="control-label">*</label>
                                                <input id="txtPagamentoRecebimento" class="form-control" type="text" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <button type="button" id="btnConsultarPagamentoRecebimento" onclick="PagamentosRecebimentos()" class="btn btn-primary">Consultar</button>
                                        </div>
                                    </div>
                                </div>
                                <div class="table-responsive fixedDoubleHead topMarg">
                                    <table id="grdPagamentoRecebimento" class="table tablecont">
                                        <thead>
                                            <tr>
                                                <th class="text-center" scope="col">NR PROCESSO</th>
                                                <th class="text-center" scope="col">ITEM DESPESA</th>
                                                <th class="text-center" scope="col">DATA (REC)</th>
                                                <th class="text-center" scope="col">CLIENTE (REC)</th>
                                                <th class="text-center" scope="col">DEVIDO (REC)</th>
                                                <th class="text-center" scope="col">MOEDA (REC)</th>
                                                <th class="text-center" scope="col">CAMBIO (REC)</th>
                                                <th class="text-center" scope="col">LIQUIDADO (REC)</th>
                                                <th class="text-center" scope="col">DATA (PAG)</th>
                                                <th class="text-center" scope="col">FORNECEDOR (PAG)</th>
                                                <th class="text-center" scope="col">DEVIDO (PAG)</th>
                                                <th class="text-center" scope="col">MOEDA (PAG)</th>
                                                <th class="text-center" scope="col">CAMBIO (PAG)</th>
                                                <th class="text-center" scope="col">LIQUIDADO (PAG)</th>
                                            </tr>
                                        </thead>
                                        <tbody id="grdPagamentoRecebimentoBody">

                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="modal fade bd-example-modal-xl" id="modalEstimativaPagamentoRecebimento" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                        <div class="modal-dialog modal-dialog-centered modal-xl" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title" id="modalEstimativaPagamentoRecebimentoTitle"></h5>
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">&times;</span>
                                    </button>
                                </div>
                                <div class="modal-body">
                                    <div class="row" style="display: flex; margin:auto; margin-top:10px;">
                                        <div style="margin: auto">
                                            <button type="button" id="btnExportEstimativaPagamentoRecebimento" class="btn btn-primary" onclick="exportCSV('Pagamento_Recebimento.csv')">Exportar Grid - CSV</button>
                                            <button type="button" id="btnPrintEstimativaPagamentoRecebimento" class="btn btn-primary" onclick="PrintEstimativaPagamentosRecebimentos()">Imprimir</button>
                                        </div>
                                    </div>
                                    <div class="row flexdiv topMarg">
                                        <div class="col-sm-2">
                                            <div class="form-group">
                                                <label class="control-label">Data Inicial:</label>
                                                <input id="txtDtInicialEstimativaPagamentoRecebimento" class="form-control" type="date" required="required"/>
                                            </div>
                                        </div>
                                        <div class="col-sm-2">
                                            <div class="form-group">
                                                <label class="control-label">Data Final:</label>
                                                <input id="txtDtFinalEstimativaPagamentoRecebimento" class="form-control" type="date" required="required"/>
                                            </div>
                                        </div>
                                        <div class="col-sm-2">
                                            <div class="form-group">
                                                <label class="control-label">Filtro</label>
                                                <select id="ddlFilterEstimativaPagamentoRecebimento" class="form-control">
                                                    <option value="">Selecione</option>
                                                    <option value="1">Nr Processo</option>
                                                    <option value="2">Cliente</option>
                                                    <option value="3">Fornecedor</option>
                                                </select>
                                            </div>
                                        </div>
                                        <div class="col-sm-2">
                                            <div class="form-group">
                                                <label class="control-label">*</label>
                                                <input id="txtEstimativaPagamentoRecebimento" class="form-control" type="text" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <button type="button" id="btnConsultarEstimativaPagamentoRecebimento" onclick="EstimativaPagamentosRecebimentos()" class="btn btn-primary">Consultar</button>
                                        </div>
                                    </div>
                                </div>
                                <div class="table-responsive fixedDoubleHead topMarg">
                                    <table id="grdEstimativaPagamentoRecebimento" class="table tablecont">
                                        <thead>
                                            <tr>
                                                <th class="text-center" scope="col">CHEGADA OU EMBARQUE</th>
                                                <th class="text-center" scope="col">NR PROCESSO</th>
                                                <th class="text-center" scope="col">IMP / EXP</th>
                                                <th class="text-center" scope="col">ITEM DESPESA</th>
                                                <th class="text-center" scope="col">CLIENTE (REC)</th>
                                                <th class="text-center" scope="col">DEVIDO (REC)</th>
                                                <th class="text-center" scope="col">MOEDA (REC)</th>
                                                <th class="text-center" scope="col">CAMBIO (REC)</th>
                                                <th class="text-center" scope="col">DATA CAMBIO (REC)</th>
                                                <th class="text-center" scope="col">RECEBER (R$)</th>
                                                <th class="text-center" scope="col">FORNECEDOR (PAG)</th>
                                                <th class="text-center" scope="col">DEVIDO (PAG)</th>
                                                <th class="text-center" scope="col">MOEDA (PAG)</th>
                                                <th class="text-center" scope="col">CAMBIO (PAG)</th>
                                                <th class="text-center" scope="col">DATA CAMBIO (PAG)</th>
                                                <th class="text-center" scope="col">PAGAR (R$)</th>
                                            </tr>
                                        </thead>
                                        <tbody id="grdEstimativaPagamentoRecebimentoBody">

                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>--%>


    <asp:SqlDataSource ID="dsFinanceiro" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT * FROM [View_Financeiro] WHERE QT_TAXAS_PAGAR_ABERTA > 0 OR QT_TAXAS_RECEBER_ABERTA > 0 ORDER BY NR_PROCESSO"></asp:SqlDataSource>
                          <asp:TextBox ID="TextBox1" Style="display:none" runat="server"></asp:TextBox>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">

    <script type="text/javascript">

        function SalvaPosicao() {
          var posicao = document.getElementById('DivGrid').scrollTop;
            if (posicao) {
                document.getElementById('<%= TextBox1.ClientID %>').value = posicao;
                console.log('if:' + posicao);

            }
            else {
                document.getElementById('<%= TextBox1.ClientID %>').value = posicao;
                console.log('else:' + posicao);

            }
      };
     
    
  Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);

        function EndRequestHandler(sender, args) {
            var valor = document.getElementById('<%= TextBox1.ClientID %>').value;
            document.getElementById('DivGrid').scrollTop = valor;
        };


        </script>






       

      <%--  <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/xlsx/0.13.5/xlsx.full.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/xlsx/0.13.5/jszip.js"></script>
    <script src="Content/js/papaparse.min.js"></script>    
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jspdf/1.5.3/jspdf.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/html2pdf.js/0.9.2/html2pdf.bundle.js"></script>
       <script> //scrips thiago
        var data = new Date();
        var dia = String(data.getDate()).padStart(2, '0');
        var mes = String(data.getMonth() + 1).padStart(2, '0');
        var ano = data.getFullYear();

        function consistir() {
            var linha = document.querySelectorAll("#grdEstimativaBody tr");
            var coluna = document.querySelectorAll("#grdEstimativaBody tr td");
            for (let i = 0; i < linha.length; i++) {
                for (let j = 0; j < coluna.length; j++) {
                    console.log(coluna[j].textContent);
                    if (validaDat(coluna[2].textContent) == true && validaDat(coluna[5].textContent) == true) {
                        if (compareDates(coluna[2].textContent, coluna[5].textContent) == true) {
                            console.log("ok");
                        } else {
                            console.log("erro");
                        }
                    } else {
                        console.log("erro v");
                    }
                }
            }
        }

        function CSV() {
            const head = [
                ["A1_COD;", "A1_LOJA;", "A1_NOME;", "A1_NREDUZ;", "A1_PESSOA;", "A1_TIPO;", "A1_END;", "A1_EST;", "A1_COD_MUN;", "A1_MUN;", "A1_NATUREZ;", "A1_BAIRRO;", "A1_CEP;", "A1_ATIVIDA;", "A1_TEL;", "A1_TELEX;", "A1_FAX;", "A1_CONTATO;", "A1_CGC;", "A1_INSCR;", "A1_INSCRM;", "A1_CONTA;", "A1_RECISS;", "A1_CONT"],
            ]

            let csvContent = new Blob(["\uFEFF" + head], { type: "text/csv;charset=utf-8;" });

            head.forEach(function (rowArray) {
                let row = rowArray.join(",");
                csvContent += row + "\r\n";
            });

            var encodedUri = encodeURI(csvContent);
            window.open(encodedUri);
        }

        function validaDat(data) {
            var date = data;
            var ardt = new Array;
            var ExpReg = new RegExp("(0[1-9]|[12][0-9]|3[01])/(0[1-9]|1[012])/[12][0-9]{3}");
            ardt = date.split("/");
            erro = false;
            if (date.search(ExpReg) == -1) {
                erro = true;
            }
            else if (((ardt[1] == 4) || (ardt[1] == 6) || (ardt[1] == 9) || (ardt[1] == 11)) && (ardt[0] > 30))
                erro = true;
            else if (ardt[1] == 2) {
                if ((ardt[0] > 28) && ((ardt[2] % 4) != 0))
                    erro = true;
                if ((ardt[0] > 29) && ((ardt[2] % 4) == 0))
                    erro = true;
            }
            if (erro) {
                return false;
            }
            return true;
        }

        function compareDates(dateE, dateV) {
            let emissao = dateE.split('/') // separa a data pelo caracter '/'
            let vencimento = dateV.split('/')      // pega a data atual

            dateE = new Date(emissao[2], emissao[1] - 1, emissao[0]) // formata 'date'
            dateV = new Date(vencimento[2], vencimento[1] - 1, vencimento[0])

            // compara se a data informada é maior que a data atual
            // e retorna true ou false
            return dateE > dateV ? false : true
        }


        //PagamentosRecebimentos

        function PagamentosRecebimentos() {
            $("#modalPagamentoRecebimento").modal('show');
            var dtInicial = document.getElementById("txtDtInicialPagamentoRecebimento").value;
            var dtFinal = document.getElementById("txtDtFinalPagamentoRecebimento").value;
            var nota = document.getElementById("txtPagamentoRecebimento").value;
            var filter = document.getElementById("ddlFilterPagamentoRecebimento").value;
            if (dtInicial != "" && dtFinal != "") {
                $.ajax({
                    type: "POST",
                    url: "DemurrageService.asmx/listarContasRecebidasPagas",
                    data: '{dataI:"' + dtInicial + '",dataF:"' + dtFinal + '", nota: "' + nota + '", filter: "' + filter + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    beforeSend: function () {
                        $("#grdPagamentoRecebimentoBody").empty();
                        $("#grdPagamentoRecebimentoBody").append("<tr><td colspan='14'><div class='loader'></div></td></tr>");
                    },
                    success: function (dado) {
                        var dado = dado.d;
                        dado = $.parseJSON(dado);
                        $("#grdPagamentoRecebimentoBody").empty();
                        if (dado != null) {
                            for (let i = 0; i < dado.length; i++) {
                                $("#grdPagamentoRecebimentoBody").append("<tr><td class='text-center'> " + dado[i]["NR_PROCESSO"] + "</td><td class='text-center'>" + dado[i]["NM_ITEM_DESPESA"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["DT_LIQUIDACAO_REC"] + "</td><td class='text-center'>" + dado[i]["NM_CLIENTE_REC"] + "</td><td class='text-center'>" + dado[i]["VL_DEVIDO_REC"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["MOEDA_REC"] + "</td><td class='text-center'>" + dado[i]["VL_CAMBIO_REC"] + "</td><td class='text-center'>" + dado[i]["VL_LIQUIDO_REC"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["DT_LIQUIDACAO_PAG"] + "</td><td class='text-center'>" + dado[i]["NM_FORNECEDOR_PAG"] + "</td><td class='text-center'>" + dado[i]["VL_DEVIDO_PAG"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["MOEDA_PAG"] + "</td><td class='text-center'>" + dado[i]["VL_CAMBIO_PAG"] + "</td><td class='text-center'>" + dado[i]["VL_LIQUIDO_PAG"] + "</td></tr>");
                            }
                        }
                        else {
                            $("#grdPagamentoRecebimentoBody").append("<tr id='msgEmptyDemurrageContainer'><td colspan='14' class='alert alert-light text-center'>Não há nenhum registro</td></tr>");
                        }
                    }
                })
            } else {

            }
        }

        function exportCSV(filename) {
            var csv = [];
            var rows = document.querySelectorAll("#grdPagamentoRecebimento tr");

            for (var i = 0; i < rows.length; i++) {
                var row = [], cols = rows[i].querySelectorAll("#grdPagamentoRecebimento td, #grdPagamentoRecebimento th");

                for (var j = 0; j < cols.length; j++)
                    row.push(cols[j].innerText);

                csv.push(row.join(";"));
            }

            // Download CSV file
            exportTableToCSVPagamentosRecebimentos(csv.join("\n"), filename);
        }

        function exportTableToCSVPagamentosRecebimentos(csv, filename) {
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

        function printPagamentosRecebimentos() {
            var dtInicial = document.getElementById("txtDtInicialPagamentoRecebimento").value;
            var dtFinal = document.getElementById("txtDtFinalPagamentoRecebimento").value;
            var nota = document.getElementById("txtPagamentoRecebimento").value;
            var filter = document.getElementById("ddlFilterPagamentoRecebimento").value;
            var position = 45;
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/listarContasRecebidasPagas",
                data: '{dataI:"' + dtInicial + '",dataF:"' + dtFinal + '", nota: "' + nota + '", filter: "' + filter + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    if (dado != null) {
                        var doc = new jsPDF('l');
                        doc.setFontSize(7);
                        doc.setFontStyle("bold");
                        doc.text("NR PROCESSO", 4, 45);
                        doc.setLineWidth(0.2);
                        doc.line(3.7, 38, 295, 38);
                        doc.line(3.7, 38, 3.7, 46);
                        doc.line(295, 46, 295, 46);
                        doc.line(295, 38, 295, 46);
                        doc.line(3.7, 46, 295, 46);
                        doc.line(3.7, 42, 295, 42);
                        doc.line(69, 38, 69, 46);
                        doc.line(89, 42, 89, 46);
                        doc.line(119, 42, 119, 46);
                        doc.line(134, 42, 134, 46);
                        doc.line(146, 42, 146, 46);
                        doc.line(161, 42, 161, 46);
                        doc.line(184, 38, 184, 46);
                        doc.line(199, 42, 199, 46);
                        doc.line(229, 42, 229, 46);
                        doc.line(244, 42, 244, 46);
                        doc.line(259, 42, 259, 46);
                        doc.line(274, 42, 274, 46);
                        doc.text("ITEM DESPESA", 27, 45);
                        doc.text("RECEBIMENTO", 110, 41);
                        doc.text("DATA", 70, 45);
                        doc.text("CLIENTE", 90, 45);
                        doc.text("DEVIDO", 120, 45);
                        doc.text("MOEDA", 135, 45);
                        doc.text("CAMBIO", 147, 45);
                        doc.text("LIQUIDADO", 162, 45);
                        doc.text("PAGAMENTO", 230, 41);
                        doc.text("DATA", 185, 45);
                        doc.text("FORNECEDOR", 200, 45);
                        doc.text("DEVIDO", 230, 45);
                        doc.text("MOEDA", 245, 45);
                        doc.text("CAMBIO", 260, 45);
                        doc.text("LIQUIDADO", 275, 45);
                        for (let i = 0; i < dado.length; i++) {
                            position = position + 5;
                            doc.setFontStyle("normal");
                            doc.text(dado[i]["NR_PROCESSO"], 4, position);
                            doc.text(dado[i]["NM_ITEM_DESPESA"], 27, position);
                            doc.text(dado[i]["DT_LIQUIDACAO_REC"], 70, position);
                            doc.text(dado[i]["NM_CLIENTE_REC"].substring(0, 15), 90, position);
                            doc.text(dado[i]["VL_DEVIDO_REC"], 120, position);
                            doc.text(dado[i]["MOEDA_REC"], 135, position);
                            doc.text(dado[i]["VL_CAMBIO_REC"], 147, position);
                            doc.text(dado[i]["VL_LIQUIDO_REC"], 162, position);
                            doc.text(dado[i]["DT_LIQUIDACAO_PAG"], 185, position);
                            doc.text(dado[i]["NM_FORNECEDOR_PAG"].substring(0, 15), 200, position);
                            doc.text(dado[i]["VL_DEVIDO_PAG"], 230, position);
                            doc.text(dado[i]["MOEDA_PAG"], 245, position);
                            doc.text(dado[i]["VL_CAMBIO_PAG"], 260, position);
                            doc.text(dado[i]["VL_LIQUIDO_PAG"], 275, position);
                        }
                        doc.output("dataurlnewwindow");
                    }
                    else {
                    }
                }
            })

        }

        function EstimativaPagamentosRecebimentos() {
            $("#modalEstimativaPagamentoRecebimento").modal('show');
            var dtInicial = document.getElementById("txtDtInicialEstimativaPagamentoRecebimento").value;
            var dtFinal = document.getElementById("txtDtFinalEstimativaPagamentoRecebimento").value;
            var nota = document.getElementById("txtEstimativaPagamentoRecebimento").value;
            var filter = document.getElementById("ddlFilterEstimativaPagamentoRecebimento").value;
            if (dtInicial != "" && dtFinal != "") {
                $.ajax({
                    type: "POST",
                    url: "DemurrageService.asmx/listarEstimativaContasRecebidasPagas",
                    data: '{dataI:"' + dtInicial + '",dataF:"' + dtFinal + '", nota: "' + nota + '", filter: "' + filter + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    beforeSend: function () {
                        $("#grdEstimativaPagamentoRecebimentoBody").empty();
                        $("#grdEstimativaPagamentoRecebimentoBody").append("<tr><td colspan='16'><div class='loader'></div></td></tr>");
                    },
                    success: function (dado) {
                        var dado = dado.d;
                        dado = $.parseJSON(dado);
                        $("#grdEstimativaPagamentoRecebimentoBody").empty();
                        if (dado != null) {
                            for (let i = 0; i < dado.length; i++) {
                                $("#grdEstimativaPagamentoRecebimentoBody").append("<tr><td class='text-center'> " + dado[i]["DT_CHEGADA"] + "</td><td class='text-center'>" + dado[i]["NR_PROCESSO"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["TP_SERVICO"] + "</td><td class='text-center'>" + dado[i]["NM_ITEM_DESPESA"] + "</td><td class='text-center'>" + dado[i]["NM_CLIENTE_REC"] + "</td><td class='text-center'>" + dado[i]["VL_DEVIDO_REC"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["MOEDA_REC"] + "</td><td class='text-center'>" + dado[i]["VL_CAMBIO_REC"] + "</td><td class='text-center'>" + dado[i]["DT_CAMBIO_REC"] + "</td><td class='text-center'>" + dado[i]["VL_LIQUIDO_REC"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["NM_FORNECEDOR_PAG"] + "</td><td class='text-center'>" + dado[i]["VL_DEVIDO_PAG"] + "</td>" +
                                    "<td class='text-center'>" + dado[i]["MOEDA_PAG"] + "</td><td class='text-center'>" + dado[i]["VL_CAMBIO_PAG"] + "</td><td class='text-center'>" + dado[i]["DT_CAMBIO_PAG"] + "</td><td class='text-center'>" + dado[i]["VL_LIQUIDO_PAG"] + "</td></tr>");
                            }
                        }
                        else {
                            $("#grdEstimativaPagamentoRecebimentoBody").append("<tr id='msgEmptyDemurrageContainer'><td colspan='16' class='alert alert-light text-center'>Não há nenhum registro</td></tr>");
                        }
                    }
                })
            } else {

            }
        }

        function exportEstimativaCSV(filename) {
            var csv = [];
            var rows = document.querySelectorAll("#grdEstimativaPagamentoRecebimento tr");

            for (var i = 0; i < rows.length; i++) {
                var row = [], cols = rows[i].querySelectorAll("#grdEstimativaPagamentoRecebimento td, #grdEstimativaPagamentoRecebimento th");

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

        function PrintEstimativaPagamentosRecebimentos() {
            $("#modalEstimativaPagamentoRecebimento").modal('show');
            var dtInicial = document.getElementById("txtDtInicialEstimativaPagamentoRecebimento").value;
            var dtFinal = document.getElementById("txtDtFinalEstimativaPagamentoRecebimento").value;
            var nota = document.getElementById("txtEstimativaPagamentoRecebimento").value;
            var filter = document.getElementById("ddlFilterEstimativaPagamentoRecebimento").value;
            var position = 45;
            if (dtInicial != "" && dtFinal != "") {
                $.ajax({
                    type: "POST",
                    url: "DemurrageService.asmx/listarEstimativaContasRecebidasPagas",
                    data: '{dataI:"' + dtInicial + '",dataF:"' + dtFinal + '", nota: "' + nota + '", filter: "' + filter + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (dado) {
                        var dado = dado.d;
                        dado = $.parseJSON(dado);
                        if (dado != null) {
                            var doc = new jsPDF('l');
                            doc.setFontSize(7);
                            doc.setFontStyle("bold");
                            doc.text("DATA CHEGADA", 4, 41);
                            doc.text("EMBARQUE", 4, 45);
                            doc.setLineWidth(0.2);
                            doc.line(3.7, 38, 295, 38);
                            doc.line(3.7, 38, 3.7, 46);
                            doc.line(295, 46, 295, 46);
                            doc.line(295, 38, 295, 46);
                            doc.line(3.7, 46, 295, 46);
                            doc.line(3.7, 42, 295, 42);
                            doc.line(94, 38, 94, 46);
                            doc.line(119, 42, 119, 46);
                            doc.line(134, 42, 134, 46);
                            doc.line(146, 42, 146, 46);
                            doc.line(159, 42, 159, 46);
                            doc.line(174, 42, 174, 46);
                            doc.line(195, 38, 195, 46);
                            doc.line(225, 42, 225, 46);
                            doc.line(238, 42, 238, 46);
                            doc.line(250, 42, 250, 46);
                            doc.line(262, 42, 262, 46);
                            doc.line(277, 42, 277, 46);
                            doc.text("NR PROCESSO", 27, 45);
                            doc.text("RECEBIMENTO", 110, 41);
                            doc.text("IMP / EXP", 50, 45);
                            doc.text("ITEM DESPESA", 65, 45);
                            doc.text("CLIENTE", 95, 45);
                            doc.text("DEVIDO", 120, 45);
                            doc.text("MOEDA", 135, 45);
                            doc.text("CAMBIO", 147, 45);
                            doc.text("DATA", 160, 41);
                            doc.text("CAMBIO", 160, 45);
                            doc.text("PAGAMENTO", 215, 41);
                            doc.text("RECEBER", 175, 45);
                            doc.text("FORNECEDOR", 196, 45);
                            doc.text("DEVIDO", 226, 45);
                            doc.text("MOEDA", 239, 45);
                            doc.text("CAMBIO", 251, 45);
                            doc.text("DATA", 263, 41);
                            doc.text("CAMBIO", 263, 45);
                            doc.text("PAGAR", 278, 45);
                            for (let i = 0; i < dado.length; i++) {
                                position = position + 5;
                                doc.setFontStyle("normal");
                                doc.text(dado[i]["DT_CHEGADA"], 4, position);
                                doc.text(dado[i]["NR_PROCESSO"], 27, position);
                                doc.text(dado[i]["TP_SERVICO"], 50, position);
                                doc.text(dado[i]["NM_ITEM_DESPESA"].substring(0, 15), 65, position);
                                doc.text(dado[i]["NM_CLIENTE_REC"].substring(0, 15), 95, position);
                                doc.text(dado[i]["VL_DEVIDO_REC"], 120, position);
                                doc.text(dado[i]["MOEDA_REC"], 135, position);
                                doc.text(dado[i]["VL_CAMBIO_REC"], 147, position);
                                doc.text(dado[i]["DT_CAMBIO_REC"], 160, position);
                                doc.text(dado[i]["VL_LIQUIDO_REC"].substring(0, 15), 175, position);
                                doc.text(dado[i]["NM_FORNECEDOR_PAG"].substring(0, 15), 196, position);
                                doc.text(dado[i]["VL_DEVIDO_PAG"], 226, position);
                                doc.text(dado[i]["MOEDA_PAG"], 239, position);
                                doc.text(dado[i]["VL_CAMBIO_PAG"], 251, position);
                                doc.text(dado[i]["DT_CAMBIO_PAG"], 263, position);
                                doc.text(dado[i]["VL_LIQUIDO_PAG"], 278, position);
                            }
                            doc.output("dataurlnewwindow");
                        }
                        else {

                        }
                    }
                })
            } else {

            }
        }
    </script>--%>
</asp:Content>
