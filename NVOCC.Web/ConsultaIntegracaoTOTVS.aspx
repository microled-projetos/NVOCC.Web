<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ConsultaIntegracaoTOTVS.aspx.vb" Inherits="NVOCC.Web.ConsultaIntegracaoTOTVS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .valores{
            text-align: right !important;
        }
    </style>
    <div class="row principal">

        <div class="col-lg-12 col-md-12 col-sm-12">
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
                                <asp:BoundField DataField="NUMERO_DOC" HeaderText="Número" SortExpression="NUMERO_DOC" ItemStyle-HorizontalAlign="Right" HeaderStyle-CssClass="valores"/>
                                <asp:BoundField DataField="SERIE" HeaderText="SERIE" SortExpression="SERIE" ItemStyle-HorizontalAlign="Right" HeaderStyle-CssClass="valores"/>
                                <asp:BoundField DataField="DATA_EMISSAO" HeaderText="Emissão" SortExpression="DATA_EMISSAO" DataFormatString="{0:dd/MM/yyyy hh:mm}" ItemStyle-HorizontalAlign="Right" HeaderStyle-CssClass="valores"/>
                                <asp:BoundField DataField="VALOR" HeaderText="VALOR" SortExpression="VALOR" ItemStyle-HorizontalAlign="Right" HeaderStyle-CssClass="valores"/>
                                <asp:BoundField DataField="DADOS_CLIENTE" HeaderText="CLIENTE" SortExpression="DADOS_CLIENTE" ItemStyle-HorizontalAlign="Right" HeaderStyle-CssClass="valores"/>
                                <asp:BoundField DataField="TIPO" HeaderText="TIPO" SortExpression="TIPO" ItemStyle-HorizontalAlign="Right" HeaderStyle-CssClass="valores"/>
                               <asp:BoundField DataField="SERVICO" HeaderText="Serviço" SortExpression="SERVICO" ItemStyle-HorizontalAlign="Right" HeaderStyle-CssClass="valores" visible="false"/>
                                <asp:BoundField DataField="DATA_INTEG_REC" HeaderText="Integração" SortExpression="DATA_INTEG_REC" DataFormatString="{0:dd/MM/yyyy hh:mm}" ItemStyle-HorizontalAlign="Right" HeaderStyle-CssClass="valores"/>
                                <asp:BoundField DataField="RETORNO" HeaderText="Retorno" SortExpression="RETORNO" ItemStyle-HorizontalAlign="Right" HeaderStyle-CssClass="valores"/>
                                <asp:BoundField DataField="INTEGRACAO_CANCELADA" HeaderText="Integração Cancelada" SortExpression="INTEGRACAO_CANCELADA" ItemStyle-HorizontalAlign="Right" HeaderStyle-CssClass="valores"/>
                                <asp:BoundField DataField="CRITICA" HeaderText="Crítica" SortExpression="CRITICA" ItemStyle-HorizontalAlign="Right" HeaderStyle-CssClass="valores"/>
                            </Columns>
                            <HeaderStyle CssClass="headerStyle" />
                        </asp:GridView>
                        <br />&nbsp;&nbsp;<strong>Total de Documentos:</strong><asp:Label ID="lblTotalNF" runat="server"></asp:Label>
                        <br />&nbsp;&nbsp;<strong>Documentos Cancelados:</strong><asp:Label ID="lblNFCanceladas" runat="server"></asp:Label>
                        <br />&nbsp;&nbsp;<strong>Documentos Integrados:</strong><asp:Label ID="lblNFIntegradas" runat="server"></asp:Label>
                        <br />&nbsp;&nbsp;<strong>Documentos Não Integrados:</strong><asp:Label ID="lblNFNaoIntegradas" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
        </div>

    </div> 
 </div> 
    <asp:SqlDataSource ID="dsConsulta" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT * FROM FN_INTEGRACAO_TOTVS('', GETDATE(),GETDATE()) ORDER BY NUMERO_DOC DESC "></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsFiltroTipo" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT DISTINCT TIPO AS 'COD', TIPO as 'DESCR'  FROM VW_INTEGRACAO_TOTVS UNION SELECT  '0', '     Selecione' ORDER BY TIPO"></asp:SqlDataSource>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
