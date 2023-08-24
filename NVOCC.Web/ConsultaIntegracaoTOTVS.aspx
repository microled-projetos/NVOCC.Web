<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ConsultaIntegracaoTOTVS.aspx.vb" Inherits="NVOCC.Web.ConsultaIntegracaoTOTVS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row principal">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <h3 class="panel-title">Consulta Integração TOTVS
                </h3>
            </div>
            <div class="panel-body">
                <div class="tab-pane fade active in" id="consulta">
                    <div class="row">
                        <div class="col-sm-2">
                            <div class="form-group">
                                Tipo:
                               <asp:DropDownList ID="ddlFiltroTipo" runat="server" CssClass="form-control" Font-Size="15px" DataTextField="DESCR" DataSourceID="dsFiltroTipo" DataValueField="COD"/>                              
                            </div>
                        </div>
                        <div class="col-sm-1">
                            <div class="form-group">
                                Emissão De:
                               <asp:TextBox ID="txtDataInicioBusca" runat="server" CssClass="form-control data"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-sm-1">
                            <div class="form-group">
                                Até:
                               <asp:TextBox ID="txtDataFimBusca" runat="server" CssClass="form-control data"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-sm-1">
                            <div class="form-group">
                                <br />
                                <asp:Button runat="server" Text="Pesquisar" ID="btnPesquisar" CssClass="btn btn-block btn-success" />
                            </div>
                        </div>
                        <div class="col-sm-1">
                            <div class="form-group">
                                <br />
                                <asp:Button runat="server" Text="Limpar" ID="btnLimpar" CssClass="btn btn-block btn-info" />
                            </div>
                        </div>
                        <br />
                        <br />
                    </div>
                    <div class="row" id="divDados" runat="server" visible="false">
                        <asp:GridView ID="dgvConsulta" DataKeyNames="NUMERO_DOC" DataSourceID="dsConsulta" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" EmptyDataText="Nenhum registro encontrado." PageSize="100">
                            <Columns>
                                <asp:BoundField DataField="NUMERO_DOC" HeaderText="Número" SortExpression="NUMERO_DOC" />
                                <asp:BoundField DataField="SERIE" HeaderText="SERIE" SortExpression="SERIE" />
                                <asp:BoundField DataField="DATA_EMISSAO" HeaderText="Emissão" SortExpression="DATA_EMISSAO" />
                                <asp:BoundField DataField="VALOR" HeaderText="VALOR" SortExpression="VALOR" />
                                <asp:BoundField DataField="CLIENTE" HeaderText="CLIENTE" SortExpression="CLIENTE" />
                                <asp:BoundField DataField="CGC" HeaderText="CGC" SortExpression="CGC" />
                                <asp:BoundField DataField="TIPO" HeaderText="TIPO" SortExpression="TIPO" />

                                <%-- <asp:BoundField DataField="CANCELADA" HeaderText="CANCELADA" SortExpression="CANCELADA" />
                                         <asp:BoundField DataField="FLAG_API_TOTVS" HeaderText="FLAG_API_TOTVS" SortExpression="FLAG_API_TOTVS" />
                                            <asp:BoundField DataField="DATA_INTEG_NOTA" HeaderText="DATA_INTEG_NOTA" SortExpression="DATA_INTEG_NOTA" />
                                            <asp:BoundField DataField="FLAG_API_TOTVS_TITULO" HeaderText="FLAG_API_TOTVS_TITULO" SortExpression="FLAG_API_TOTVS_TITULO" />
                                            <asp:BoundField DataField="DATA_INTEG_REC" HeaderText="DATA_INTEG_REC" SortExpression="DATA_INTEG_REC" />
                                            <asp:BoundField DataField="FLAG_API_TOTVS_CANCEL" HeaderText="FLAG_API_TOTVS_CANCEL" SortExpression="FLAG_API_TOTVS_CANCEL" />
                                            <asp:BoundField DataField="FLAG_API_TOTVS_CANCEL_REC" HeaderText="FLAG_API_TOTVS_CANCEL_REC" SortExpression="FLAG_API_TOTVS_CANCEL_REC" />
                                            <asp:BoundField DataField="MSG_RETORNO_NFE" HeaderText="MSG_RETORNO_NFE" SortExpression="MSG_RETORNO_NFE" />
                                            <asp:BoundField DataField="MSG_RETORNO_REC" HeaderText="MSG_RETORNO_REC" SortExpression="MSG_RETORNO_REC" />
                                            <asp:BoundField DataField="MSG_RETORNO_NFE_C" HeaderText="MSG_RETORNO_NFE_C" SortExpression="MSG_RETORNO_NFE_C" />
                                            <asp:BoundField DataField="MSG_RETORNO_REC_C" HeaderText="MSG_RETORNO_REC_C" SortExpression="MSG_RETORNO_REC_C" />
                                            <asp:BoundField DataField="MSG_RETORNO_CLI" HeaderText="MSG_RETORNO_CLI" SortExpression="MSG_RETORNO_CLI" />--%>
                            </Columns>
                            <HeaderStyle CssClass="headerStyle" />
                        </asp:GridView>
                        <br />&nbsp;&nbsp;Notas Fiscais:<asp:Label ID="lblTotalNF" runat="server"></asp:Label>
                        <br />&nbsp;&nbsp;Notas Fiscais Canceladas:<asp:Label ID="lblNFCanceladas" runat="server"></asp:Label>
                        <br />&nbsp;&nbsp;Notas Fiscais Integradas:<asp:Label ID="lblNFIntegradas" runat="server"></asp:Label>
                        <br />&nbsp;&nbsp;Notas Fiscais Não Integradas:<asp:Label ID="lblNFNaoIntegradas" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
        </div>

    </div> 

    <asp:SqlDataSource ID="dsConsulta" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT NUMERO_DOC, SERIE, TIPO, DATA_EMISSAO, VALOR, CLIENTE, CGC, CANCELADA, FLAG_API_TOTVS, DATA_INTEG_NOTA, FLAG_API_TOTVS_TITULO, DATA_INTEG_REC, FLAG_API_TOTVS_CANCEL, FLAG_API_TOTVS_CANCEL_REC, MSG_RETORNO_NFE, MSG_RETORNO_REC, MSG_RETORNO_NFE_C, MSG_RETORNO_REC_C, MSG_RETORNO_CLI FROM VW_INTEGRACAO_TOTVS WHERE CONVERT(DATE,DATA_EMISSAO,103) = CONVERT(DATE,GETDATE(),103) ORDER BY DATA_EMISSAO DESC "></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsFiltroTipo" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT DISTINCT TIPO AS 'COD', TIPO as 'DESCR'  FROM VW_INTEGRACAO_TOTVS UNION SELECT  '0', '     Selecione' ORDER BY TIPO"></asp:SqlDataSource>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
