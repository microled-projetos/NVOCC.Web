<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="CalcularRecebimento.aspx.vb" Inherits="NVOCC.Web.CalcularRecebimento" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <div class="row principal">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <h3 class="panel-title">CALCULAR RECEBIMENTO
                </h3>
            </div>
            <div class="panel-body">

                <div class="tab-content">
                    <div class="tab-pane fade active in" id="Embarque">
                        <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
                            <ContentTemplate>
                                <asp:TextBox ID="txtID_BL" Style="display: none" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:TextBox ID="txtLinhaBL" Style="display: none" runat="server" CssClass="form-control"></asp:TextBox>
                                <div class="row linhabotao text-center" style="margin-left: 20px;border: ridge 1px;">
                                
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label class="control-label">FORNECEDOR:</label>
                                                <asp:DropDownList ID="ddlFornecedor" runat="server" CssClass="form-control" Font-Size="11px" AutoPostBack="true" DataTextField="NM_RAZAO" DataSourceID="dsFornecedor" DataValueField="ID_PARCEIRO"></asp:DropDownList>
                                            </div>
                                        </div>
                                         <div class="col-sm-6">
                                            <div class="form-group">
                                                <label class="control-label">CIDADE DO PARCEIRO:</label>
                                                <asp:label runat="server" CssClass="control-label"/>
                                            </div>
                                        </div> 
                                    </div>
                                <div class="row linhabotao text-center" style="margin-left: 20px;border: ridge 1px;">
                                    <div class="col-sm-6">
                                            <div class="form-group">
                                                <label class="control-label">ACORDO:</label>
                                                                                                <asp:label runat="server" CssClass="control-label"/>

                                            </div>
                                        </div> <div class="col-sm-3">
                                            <div class="form-group">
                                                <label class="control-label">SPREAD:</label>
                                                                                                <asp:label runat="server" CssClass="control-label"/>

                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label class="control-label">DATA CAMBIO:</label>
                                                  <asp:TextBox ID="TextBox1" runat="server" placeholder="__/__/____" CssClass="form-control data" Width="210px"></asp:TextBox>

                                            </div>
                                        </div>
                                </div>
                                <div class="row linhabotao text-center" style="margin-left: 20px;border: ridge 1px;">
                                    <div class="col-sm-6">
                                            <div class="form-group">
                                                <label class="control-label">TIPO FATURAMENTO:</label>
                                                                                                <asp:label runat="server" CssClass="control-label"/>

                                            </div>
                                        </div> <div class="col-sm-3">
                                            <div class="form-group">
                                                <label class="control-label">DIAS FATURAMENTO:</label>
                                                                                                <asp:label runat="server" CssClass="control-label"/>

                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label class="control-label">DATA DE VENCIMENTO:</label>
                                                <asp:TextBox ID="txtVencimento" runat="server" placeholder="__/__/____" CssClass="form-control data" Width="210px"></asp:TextBox>
                                            </div>
                                        </div>
                                </div>


                                <br />
                                <br />
                                    <div class="table-responsive tableFixHead">
                                        <asp:GridView ID="dgvTaxas" DataKeyNames="ID_BL" DataSourceID="dsTaxas" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" EmptyDataText="Nenhum registro encontrado.">
                                            <Columns>
                                                <asp:TemplateField HeaderText="ID" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblID_BL" runat="server" Text='<%# Eval("ID_BL") %>'  />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="Acessar" runat="server" Enabled='<%# (Eval("FL_CALCULADO") = 1) %>' ToolTip="Só é possivel selecionar taxas calculadas"/>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="NR_PROCESSO" HeaderText="Nº Processo" SortExpression="NR_PROCESSO" />
                                                <asp:BoundField DataField="NM_PARCEIRO_EMPRESA" HeaderText="Fornecedor" SortExpression="NM_PARCEIRO_EMPRESA" />
                                                <asp:BoundField DataField="NM_ITEM_DESPESA" HeaderText="Despesa" SortExpression="NM_ITEM_DESPESA" />
                                                <asp:BoundField DataField="NM_MOEDA" HeaderText="Moeda" SortExpression="NM_MOEDA" />
                                                <asp:BoundField DataField="VL_TAXA_CALCULADO" HeaderText="Valor da compra" SortExpression="VL_TAXA_CALCULADO" />
                                                <asp:BoundField DataField="VL_TAXA_BR" HeaderText="Valor da compra(R$)" SortExpression="VL_TAXA_BR" />

                                            </Columns>
                                            <HeaderStyle CssClass="headerStyle" />
                                        </asp:GridView>
                                    </div>
                                    <div class="table-responsive tableFixHead">
                                        <asp:GridView ID="dgvMoedas" DataKeyNames="ID_MOEDA_FRETE_ARMADOR" DataSourceID="dsMoeda" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" EmptyDataText="Nenhum registro encontrado.">
                                            <Columns>
                                                <asp:BoundField DataField="NM_MOEDA" HeaderText="Moeda" SortExpression="NM_MOEDA" ReadOnly="true" />
                                                <asp:BoundField DataField="VL_TXOFICIAL" HeaderText="Valor" SortExpression="VL_TXOFICIAL" />
                                                <asp:BoundField DataField="DT_CAMBIO" HeaderText="Data Câmbio" SortExpression="DT_CAMBIO" DataFormatString="{0:dd/MM/yyyy}" />
                                            </Columns>
                                            <HeaderStyle CssClass="headerStyle" />
                                        </asp:GridView>
                                </div>
                                <br />
                                <br />
                                <div class="row" style="border: ridge 1px;">
                                    <div class="col-sm-offset-5 col-sm-2 col-sm-offset-5">
                                        <div class="form-group">
                                            <br />
                                            <asp:Button runat="server" Text="Ok" ID="btnSolicitar" CssClass="btn btn-success btn-block" />
                                            <asp:Button runat="server" Text="Cancelar" ID="btnCancelar" CssClass="btn btn-danger btn-block" />
                                        </div>
                                    </div>
                                </div>


                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="dgvTaxas" />
                                <asp:AsyncPostBackTrigger ControlID="ddlFornecedor" />
                            </Triggers>
                        </asp:UpdatePanel>

                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:SqlDataSource ID="dsTaxas" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT * FROM [dbo].[View_BL_TAXAS]
WHERE ID_BL = @ID_BL ">
        <SelectParameters>
            <asp:ControlParameter Name="ID_BL" Type="Int32" ControlID="txtID_BL" />
        </SelectParameters>
    </asp:SqlDataSource>
   
    <asp:SqlDataSource ID="dsMoeda" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_MOEDA_FRETE_ARMADOR,VL_TXOFICIAL ,DT_CAMBIO,ID_MOEDA,(SELECT NM_MOEDA FROM TB_MOEDA WHERE ID_MOEDA = A.ID_MOEDA) NM_MOEDA FROM TB_MOEDA_FRETE_ARMADOR A"></asp:SqlDataSource>

     <asp:SqlDataSource ID="dsFornecedor" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_PARCEIRO, NM_RAZAO FROM [dbo].[TB_PARCEIRO] WHERE ID_PARCEIRO IN (SELECT ID_PARCEIRO_EMPRESA FROM dbo.TB_BL_TAXA WHERE ID_BL = @ID_BL)
union SELECT 0, 'Selecione' FROM [dbo].[TB_PARCEIRO] ORDER BY ID_PARCEIRO">
         <SelectParameters>
            <asp:ControlParameter Name="ID_BL" Type="Int32" ControlID="txtID_BL" />
        </SelectParameters>
     </asp:SqlDataSource>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
