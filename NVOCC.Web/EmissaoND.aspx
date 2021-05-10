<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="EmissaoND.aspx.vb" Inherits="NVOCC.Web.EmissaoND" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        #DivImpressao, #imgFundo {
            display: none;
        }

        @media print {

            @page {
            }

            #divGrid, #imgFundo {
                display: none;
            }
        }
    </style>
    <div id="divGrid" class="divGrid" runat="server">
        <asp:Label runat="server" ID="lblMBL" CssClass="control-label" />

        <asp:TextBox ID="txtID_BL" Style="display: none" runat="server" CssClass="form-control"></asp:TextBox>
        <asp:TextBox ID="txtLinhaBL" Style="display: none" runat="server" CssClass="form-control"></asp:TextBox>
        <div class="row principal">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">CALCULAR RECEBIMENTO
                        <asp:Label runat="server" ID="Label20" CssClass="control-label" />

                    </h3>
                </div>
                <div class="panel-body">

                    <div class="tab-content">
                        <div class="table-responsive tableFixHead">
                            <asp:GridView ID="dgvNotas" DataKeyNames="ID_CONTA_PAGAR_RECEBER" DataSourceID="dsNotas" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" EmptyDataText="Nenhum registro encontrado.">
                                <Columns>
                                    <asp:TemplateField HeaderText="ID" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblID_BL" runat="server" Text='<%# Eval("ID_CONTA_PAGAR_RECEBER") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="Acessar" runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="VL_LANCAMENTO" HeaderText="Valor de Lançamento" SortExpression="VL_LANCAMENTO" />
                                    <asp:BoundField DataField="VL_DESCONTO" HeaderText="Valor de Desconto" SortExpression="VL_DESCONTO" />
                                    <asp:BoundField DataField="VL_ACRESCIMO" HeaderText="Valor de Acréscimo" SortExpression="VL_ACRESCIMO" />
                                    <asp:BoundField DataField="VL_LIQUIDO" HeaderText="Valor Liquido" SortExpression="VL_LIQUIDO" />
                                    <asp:BoundField DataField="NOME_USUARIO_LANCAMENTO" HeaderText="Usuario de Lançamento" SortExpression="NOME_USUARIO_LANCAMENTO" />
                                    <asp:BoundField DataField="DT_VENCIMENTO" HeaderText="Vencimento" SortExpression="DT_VENCIMENTO" />

                                </Columns>
                                <HeaderStyle CssClass="headerStyle" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div id="DivImpressao" class="DivImpressao table-content" style="font-size: 15px; margin-bottom: 20px">
        <table border="1">
            <tr>
                <td>
                    <center>
    <strong>FCA COMERCIO EXTERIOR E LOGISTICA LTDA.</strong><br /></center>
                    <div style="text-align: center;">
                        R QUINZE DE NOVEMBRO, 46/48 - CENTRO<br />
                        SANTOS - SÃO PAULO - BRASIL - CEP:11010150<br />
                        FONE:+55 13 3797-7850 - FAX:<br />
                        CNPJ:00.693.367/0003-11  &nbsp;&nbsp;&nbsp;I.E: 633.672.235.110
                    </div>
                </td>
                <td>
                    <center>
    <img src="Content/imagens/FCA-Log - Copia.png" /><br />
    <strong>FCA COMERCIO EXTERIOR E LOGISTICA LTDA.</strong>&nbsp;<br /></center>

                </td>
            </tr>
        </table>
        <table border="1">

            <tr>
                <td>
                    <div style="text-align: center;">NOTA DE DÉBITO: XXXXXXXXXXXX</div>
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td>
                    <strong>CLIENTE</strong>&nbsp;<asp:Label ID="lblNavioViagem_LCL" runat="server" />
                    <br />
                </td>
                <td>
                    <strong>CNPJ:</strong>&nbsp;<asp:Label ID="lblDataEmbarque_LCL" runat="server" />
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                    <strong>ENDEREÇO:</strong>&nbsp;<asp:Label ID="lblEstufagem_LCL" runat="server" /><asp:Label ID="lblServico_LCL" runat="server" />
                    <br />
                </td>
                <td>
                    <strong>CEP:</strong>&nbsp;<asp:Label ID="lblAgente_LCL" runat="server" />
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                    <strong>BAIRRO:</strong>&nbsp;<asp:Label ID="lblOrigem_LCL" runat="server" />
                    <br />
                </td>
                <td>
                    <strong>CIDADE:</strong>&nbsp;<asp:Label ID="lblDestino_LCL" runat="server" />
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                    <strong>TELEFONE:</strong>&nbsp;<asp:Label ID="lblMaster_LCL" runat="server" />
                    <br />
                </td>
                <td>
                    <strong>FAX:</strong>&nbsp;<asp:Label ID="lblArmador_LCL" runat="server" />
                    <br />
                </td>
            </tr>
        </table>
        <table border="1">

            <tr>
                <td>
                    <strong>DATA DE EMISSAO:</strong>&nbsp;<asp:Label ID="Label1" runat="server" />

                </td>
                <td><strong>DATA DE VENCIMENTO:</strong>&nbsp;<asp:Label ID="Label2" runat="server" />
                </td>
                <td><strong>Nº FATURA:</strong>&nbsp;<asp:Label ID="Label3" runat="server" />
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td>
                    <strong>EXPORTADOR</strong>&nbsp;<asp:Label ID="Label4" runat="server" />
                    <br />
                </td>
                <td>
                    <strong>REF. CLIENTE:</strong>&nbsp;<asp:Label ID="Label5" runat="server" />
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                    <strong>FRETE:</strong>&nbsp;<asp:Label ID="Label6" runat="server" /><asp:Label ID="Label7" runat="server" />
                    <br />
                </td>
                <td>
                    <strong>NAVIO:</strong>&nbsp;<asp:Label ID="Label8" runat="server" />
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                    <strong>CONHECIMENTO:</strong>&nbsp;<asp:Label ID="Label9" runat="server" />
                    <br />
                </td>
                <td>
                    <strong>HOUSE:</strong>&nbsp;<asp:Label ID="Label10" runat="server" />
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                    <strong>ORIGEM:</strong>&nbsp;<asp:Label ID="Label11" runat="server" />
                    <br />
                </td>
                <td>
                    <strong>DESTINO:</strong>&nbsp;<asp:Label ID="Label12" runat="server" />
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                    <strong>EMBARQUE:</strong>&nbsp;<asp:Label ID="Label13" runat="server" />
                    <br />
                </td>
                <td>
                    <strong>CHEGADA:</strong>&nbsp;<asp:Label ID="Label14" runat="server" />
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                    <strong>QTD. VOLUMES:</strong>&nbsp;<asp:Label ID="Label15" runat="server" />
                    <br />
                </td>
                <td>
                    <strong>PESO BRUTO:</strong>&nbsp;<asp:Label ID="Label16" runat="server" />
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                    <strong>CONTAINER</strong>&nbsp;<asp:Label ID="Label17" runat="server" />
                    <br />
                </td>
                <td>
                    <br />
                </td>
            </tr>
        </table>

        <div id="divConteudoDinamico" runat="server">
        </div>
        <div>
            Horário de Pagamento: Envio de comprovante até as 13h. Pagamentos efetuados após o horário podem incidir em difereça cambial
            <br />
            <br />
            Prazo para Desbloqueio:<br />
            FCL: Em até 24 horas<br />
            LCL: Até o final do dia<br />
            (Após o pagamento e apresentação dos documentos)<br />
            <br />
            <br />

            O comprovante de pagamento deverá ser enviado para o e-mail financeiro@fcalog.com com a identificação do processo ou número do BL, para que possamos confirmá-lo e liberar a carga.
            <br />
            <br />
            <br />
            <br />
            <br />
            <strong>No aguardo do seu contato,&nbsp;<asp:Label ID="Label18" runat="server" /></strong>
            <div style="float: right;">Impresso &nbsp;<asp:Label ID="Label19" runat="server" /></div>
        </div>
    </div>

    <asp:SqlDataSource ID="dsNotas" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT * FROM [dbo].[View_Emissao_ND]"></asp:SqlDataSource>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">
    <%-- <script>
        $(window).load(function () {
            window.print();
        });
    </script>--%>
</asp:Content>
