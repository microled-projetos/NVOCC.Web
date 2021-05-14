﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="BaixasCancelamentos.aspx.vb" Inherits="NVOCC.Web.BaixasCancelamentos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row principal">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <h3 class="panel-title">BAIXAS E CANCELAMENTOS -
                    <asp:Label runat="server" ID="lblTipo" /></h3>
            </div>
            <div class="panel-body">

                <div class="tab-content">
                    <div class="tab-pane fade active in">
                        <br />
                        <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
                            <ContentTemplate>

                                <asp:TextBox ID="txtID_BL" Style="display: none" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:TextBox ID="txtLinhaBL" Style="display: none" runat="server" CssClass="form-control"></asp:TextBox>

                                <div class="alert alert-success" id="divSuccess" runat="server" visible="false">
                                    <asp:Label ID="lblSuccess" runat="server"></asp:Label>
                                </div>
                                <div class="alert alert-danger" id="divErro" runat="server" visible="false">
                                    <asp:Label ID="lblErro" runat="server"></asp:Label>
                                </div>
                                <br />


                                <div class="row linhabotao text-center" style="margin-left: 20px; border: ridge 1px;">
                                    <div class="col-sm-1">
                                        <div class="form-group">
                                            <label class="control-label" style="text-align: left"></label>
                                            <asp:TextBox ID="txtData" runat="server" placeholder="__/__/__" CssClass="form-control data"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-2" runat="server">
                                        <div class="form-group">
                                            <br />
                                            <asp:Button runat="server" Text="Baixar Fatura" ID="btnBaixar" CssClass="btn btn-success" />
                                        </div>
                                    </div>
                                    <div class="col-sm-offset-7 col-sm-2" runat="server">
                                        <div class="form-group">
                                            <br />
                                            <asp:Button runat="server" Text="Cancelar Fatura" ID="btnCancelar" CssClass="btn btn-danger" />

                                        </div>
                                    </div>

                                </div>


                                <br />
                                <br />
                                <div class="row">
                                    <div class="table-responsive tableFixHead" runat="server" id="gridPagar">
                                        <asp:GridView ID="dgvTaxasPagar" DataKeyNames="ID_CONTA_PAGAR_RECEBER" DataSourceID="dsPagar" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" EmptyDataText="Nenhum registro encontrado.">
                                            <Columns>
                                                <asp:TemplateField HeaderText="ID" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID_CONTA_PAGAR_RECEBER") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="ckbSelecionar" runat="server" AutoPostBack="true"/>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Nº Fatura" SortExpression="NR_FATURA_FORNECEDOR">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFatura" runat="server" Text='<%# Eval("NR_FATURA_FORNECEDOR") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="DT_VENCIMENTO" HeaderText="Vencimento" SortExpression="DT_VENCIMENTO" />
                                                <asp:TemplateField HeaderText="Empresa" SortExpression="NM_PARCEIRO_EMPRESA">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFornecedor" runat="server" Text='<%# Eval("NM_PARCEIRO_EMPRESA") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="VL_TAXA_BR" HeaderText="Valor da compra(R$)" SortExpression="VL_TAXA_BR" />
                                                <asp:BoundField DataField="VL_DESCONTO" HeaderText="Desconto" SortExpression="VL_DESCONTO" />
                                                <asp:BoundField DataField="VL_ACRESCIMO" HeaderText="Acréscimo" SortExpression="VL_ACRESCIMO" />
                                                <asp:BoundField DataField="VL_LIQUIDO" HeaderText="Liquido" SortExpression="VL_LIQUIDO" />
                                                <asp:BoundField DataField="NOME_USUARIO_LANCAMENTO" HeaderText="Usuário laçamento" SortExpression="NOME_USUARIO_LANCAMENTO" />
                                            </Columns>
                                            <HeaderStyle CssClass="headerStyle" />
                                        </asp:GridView>
                                    </div>
                                    <div class="table-responsive tableFixHead" runat="server" id="gridReceber">
                                        <asp:GridView ID="dgvTaxasReceber" DataKeyNames="ID_CONTA_PAGAR_RECEBER" DataSourceID="dsReceber" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" EmptyDataText="Nenhum registro encontrado.">
                                            <Columns>
                                                <asp:TemplateField HeaderText="ID" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID_CONTA_PAGAR_RECEBER") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="ckbSelecionar" runat="server"  AutoPostBack="true"/>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Nº Processo" SortExpression="NM_PARCEIRO_EMPRESA">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblProcesso" runat="server" Text='<%# Eval("NR_PROCESSO") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="DT_VENCIMENTO" HeaderText="Vencimento" SortExpression="DT_VENCIMENTO" />
                                                <asp:TemplateField HeaderText="Empresa" SortExpression="NM_PARCEIRO_EMPRESA">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFornecedor" runat="server" Text='<%# Eval("NM_PARCEIRO_EMPRESA") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="VL_TAXA_BR" HeaderText="Valor da compra(R$)" SortExpression="VL_TAXA_BR" />
                                                <asp:BoundField DataField="VL_DESCONTO" HeaderText="Desconto" SortExpression="VL_DESCONTO" />
                                                <asp:BoundField DataField="VL_ACRESCIMO" HeaderText="Acréscimo" SortExpression="VL_ACRESCIMO" />
                                                <asp:BoundField DataField="VL_LIQUIDO" HeaderText="Liquido" SortExpression="VL_LIQUIDO" />
                                                <asp:BoundField DataField="NOME_USUARIO_LANCAMENTO" HeaderText="Usuário laçamento" SortExpression="NOME_USUARIO_LANCAMENTO" />
                                            </Columns>
                                            <HeaderStyle CssClass="headerStyle" />
                                        </asp:GridView>
                                    </div>

                                </div>

                                <ajaxToolkit:ModalPopupExtender ID="mpeObs" runat="server" PopupControlID="Panel1" TargetControlID="btnCancelar" CancelControlID="btnFechar"></ajaxToolkit:ModalPopupExtender>
                                <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" Style="display: none;">
                                    <center>     <div class=" modal-dialog modal-dialog-centered modal-lg" role="document">
                                                    <div class="modal-content" >
                                                        <div class="modal-header">
                                                            <h5 class="modal-title">OBSERVAÇÃO DE CANCELAMENTO</h5>
                                                        </div>
                                                        <div class="modal-body">    
                                   
                                            <h5><asp:label runat="server" ID="lblFaturaCancelamento" />                       
                                            <asp:label runat="server" ID="lblProcessoCancelamento" />
                                            <asp:label runat="server" ID="lblClienteCancelamento" /></h5>
                        
                                            <asp:TextBox ID="txtObs" runat="server" TextMode="MultiLine" Rows="2" CssClass="form-control" MaxLength="1000"></asp:TextBox>
                                        
                                         </div>
                                  
                           
                      
                                                       
                                                                        
                               <div class="modal-footer">
                                                            <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFechar" text="Fechar" />
                                                            <asp:Button runat="server" CssClass="btn btn-success" ID="btnSalvarCancelamento" text="Salvar" />
                                                        </div>
                                                    
                                                </div>
      
                                       </div>     </center>
                                </asp:Panel>





                                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="Panel2" TargetControlID="btnBaixar" CancelControlID="btnFecharBaixa"></ajaxToolkit:ModalPopupExtender>
                                <asp:Panel ID="Panel2" runat="server" CssClass="modalPopup" Style="display: none;">
                                    <center>     <div class=" modal-dialog modal-dialog-centered modal-lg" role="document">
                                                    <div class="modal-content" >
                                                        <div class="modal-header">
                                                            <h5 class="modal-title">BAIXA</h5>
                                                        </div>
                                                        <div class="modal-body">                                       
                                            <h5>
                                                <asp:label runat="server" ID="lblFaturaBaixa"  />
                                                <asp:label runat="server" ID="lblProcessoBaixa"  />                                          
                                            <asp:label runat="server" ID="lblClienteBaixa" /></h5>
                        
                                           <h3>CONFIRMAÇÃO DA LIQUIDAÇÃO</h3>
                                        
                                         </div>
                                                                                       
                                                                        
                                                        <div class="modal-footer">
                                                            <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFecharBaixa" text="Fechar" />
                                                            <asp:Button runat="server" CssClass="btn btn-success" ID="btnSalvarBaixa" text="Baixar Fatura" />
                                                        </div>
                                                    
                                                </div>
      
                                       </div>     </center>
                                </asp:Panel>


                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="dgvTaxasPagar" />
                                <asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="dgvTaxasReceber" />
                                <asp:AsyncPostBackTrigger EventName="Load" ControlID="dgvTaxasPagar" />
                                <asp:AsyncPostBackTrigger EventName="Load" ControlID="dgvTaxasReceber" />
                                <asp:AsyncPostBackTrigger ControlID="btnCancelar" />
                            </Triggers>
                        </asp:UpdatePanel>

                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:SqlDataSource ID="dsPagar" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT * FROM [dbo].[View_Baixas_Cancelamentos] WHERE CD_PR =  'P' ORDER BY DT_VENCIMENTO DESC, NR_FATURA_FORNECEDOR"></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsReceber" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT * FROM [dbo].[View_Baixas_Cancelamentos] WHERE CD_PR =  'R' ORDER BY DT_VENCIMENTO DESC"></asp:SqlDataSource>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
