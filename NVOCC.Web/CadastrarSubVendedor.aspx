<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="CadastrarSubVendedor.aspx.vb" Inherits="NVOCC.Web.CadastrarSubVendedor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <div class="row principal">
        <div class="col-lg-12 col-md-12 col-sm-12">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">SUB VENDEDOR
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
                                                                


                                     <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender3" runat="server" PopupControlID="pnlCadastrarSub" TargetControlID="btnNovo" CancelControlID="btnFechar"></ajaxToolkit:ModalPopupExtender>
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                    <ContentTemplate>
                                         <asp:Panel ID="pnlCadastrarSub" runat="server" CssClass="modalPopup" Style="display: none;">
                                            <center>     <div class=" modal-dialog modal-dialog-centered modal-lg" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title">NOVO SUB VENDEDOR</h5>
                                                        </div>
                                                        <div class="modal-body">     
                                                            <div class="alert alert-danger" id="divErroNovo" runat="server" visible="false">
                                    <asp:Label ID="lblmsgErroNovo" runat="server"></asp:Label>
                                </div>
 <div class="row">
                                   <div class="col-sm-2">
                                    <div class="form-group">                                          

                                               <asp:Label ID="Label27" runat="server">ID</asp:Label><br />

                               <asp:TextBox ID="txtIDTabelaTaxa" enabled="false" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                         </div>
                                                                       <div class="col-sm-5">
                                    <div class="form-group">                                          
 <asp:Label ID="Label1" runat="server">Vendedor</asp:Label><label runat="server" style="color:red" >*</label><br />

                              <asp:DropDownList ID="ddlVendedor" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="RazaoSocial" DataSourceID="dsParceiros" DataValueField="Id" >
                                        </asp:DropDownList>
                                    
                                        </div>
                                         </div>
                                                                       <div class="col-sm-5">
                                    <div class="form-group">                                          
 <asp:Label ID="Label2" runat="server">Sub Vendedor</asp:Label><label runat="server" style="color:red" >*</label><br />

                              <asp:DropDownList ID="ddlSubVendedor" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="RazaoSocial" DataSourceID="dsParceiros" DataValueField="Id" >
                                        </asp:DropDownList>
                                    
                                        </div>
                                         </div>


     </div>
                                                            <div class="row">
                                     <div class="col-sm-2">
                                    <div class="form-group">                                          

                                               <asp:Label ID="Label5" runat="server">Validade Inicial</asp:Label><label runat="server" style="color:red" >*</label><br />

                               <asp:TextBox ID="txtValidade" placeholder="___/___/____" runat="server" CssClass="form-control data"></asp:TextBox>
                                        </div>
                                         </div>
                                     <div class="col-sm-3">
                                    <div class="form-group">                                          
                               
                                               <asp:Label ID="Label7" runat="server">Taxa Fixa</asp:Label><br />

                               <asp:TextBox ID="txtTaxaFixa"  runat="server" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                                        </div>
                                         </div>

                                     <div class="col-sm-4">
                                    <div class="form-group">                                          
 <asp:Label ID="Label8" runat="server">Base de cálculo</asp:Label><br />

                              <asp:DropDownList ID="ddlBaseCalculoTaxa" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_BASE_CALCULO_TAXA" DataSourceID="dsBaseCalculo" DataValueField="ID_BASE_CALCULO_TAXA" >
                                        </asp:DropDownList>
                                    
                                        </div>
                                         </div>
                                     <div class="col-sm-3">
                                    <div class="form-group">                                             
                                <asp:Label ID="Label9" runat="server">Percentual</asp:Label><br />

                               <asp:TextBox ID="txtPercentual"  runat="server" CssClass="form-control"  AutoPostBack="true"></asp:TextBox>
                                         
                                   </div>          
                                </div>  
                                                                            </div>
                                                        
                                                            </div>
                               <div class="modal-footer"> 
                                   <asp:Button runat="server" Text="Gravar" ID="btnGravar" CssClass="btn btn-success" /> 
                                         <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFechar" text="Close" />
                                                                 

                                                        </div>                                                    
                                            
                                       </div>

                                                         </div>     </center>
                                        </asp:Panel>
                                         </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnFechar" />
                                        <asp:AsyncPostBackTrigger ControlID="btnGravar" />
                                        <asp:AsyncPostBackTrigger ControlID="txtTaxaFixa"/>
                                        <asp:AsyncPostBackTrigger ControlID="txtPercentual" />
                                    </Triggers>
                                </asp:UpdatePanel>


                                    <div class="row">
                                     
                                  <div class="col-sm-4"">
                                                    <div class="form-group">
                                                        <asp:button runat="server" Text="Cadastrar Novo" id="btnNovo" CssClass="btn btn-primary" />
                                                    </div>
                                                </div>
                            </div>
                                
                                    <br /> 
                                <div class="rows">
                                    <div class="table-responsive tableFixHead DivGrid" id="DivGrid">
                                        <asp:GridView ID="dgvSubVendedor" DataKeyNames="ID_SUB_VENDEDOR" DataSourceID="dsSubVendedor" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" EmptyDataText="Nenhum registro encontrado.">
                                            <Columns>
                                                <asp:TemplateField HeaderText="ID" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID_SUB_VENDEDOR") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="VENDEDOR" HeaderText="VENDEDOR" SortExpression="VENDEDOR" />
                                                <asp:BoundField DataField="SUB_VENDEDOR" HeaderText="SUB VENDEDOR" SortExpression="SUB_VENDEDOR" />
                                                <asp:BoundField DataField="VL_PERCENTUAL" HeaderText="PERCENTUAL" SortExpression="VL_PERCENTUAL" />
                                                <asp:BoundField DataField="VL_TAXA_FIXA" HeaderText="TAXA FIXA" SortExpression="VL_TAXA_FIXA" />
                                                <asp:BoundField DataField="BASE_CALCULO" HeaderText="BASE DE CALCULO" SortExpression="BASE_CALCULO" />
                                                <asp:BoundField DataField="DT_VALIDADE_INICIAL" HeaderText="VALIDADE INICIAL" SortExpression="DT_VALIDADE_INICIAL" DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:TemplateField HeaderText=""  >
                                            <ItemTemplate>
                                                <asp:linkButton ID="btnExcluir" title="Excluir" runat="server"  CssClass="btn btn-danger btn-sm" CommandName="Excluir"
                                OnClientClick="javascript:return confirm('Deseja realmente excluir esta taxa?');"  CommandArgument='<%# Eval("ID_SUB_VENDEDOR") %>' Autopostback="true" ><span class="glyphicon glyphicon-trash"  style="font-size:medium"></span></asp:linkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                        </asp:TemplateField>
                                               
                                            </Columns>
                                            <HeaderStyle CssClass="headerStyle" />
                                        </asp:GridView>
                                </div>

                             </div>

                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="dgvSubVendedor" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>

                </div>


            </div>
        </div>

    </div>
     <asp:SqlDataSource ID="dsSubVendedor" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_SUB_VENDEDOR, (SELECT NM_RAZAO FROM TB_PARCEIRO A WHERE A.ID_PARCEIRO = B.ID_PARCEIRO_VENDEDOR)VENDEDOR,
(SELECT NM_RAZAO FROM TB_PARCEIRO A WHERE A.ID_PARCEIRO = B.ID_PARCEIRO_SUB_VENDEDOR)SUB_VENDEDOR,
VL_PERCENTUAL,
VL_TAXA_FIXA,
(SELECT NM_BASE_CALCULO_TAXA FROM TB_BASE_CALCULO_TAXA A WHERE A.ID_BASE_CALCULO_TAXA = B.ID_BASE_CALCULO)BASE_CALCULO,
DT_VALIDADE_INICIAL
FROM TB_SUB_VENDEDOR B "></asp:SqlDataSource>
     <asp:SqlDataSource ID="dsBaseCalculo" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT ID_BASE_CALCULO_TAXA,NM_BASE_CALCULO_TAXA FROM [dbo].[TB_BASE_CALCULO_TAXA] WHERE ID_BASE_CALCULO_TAXA IN (34,32)
union SELECT  0, 'Selecione' ORDER BY ID_BASE_CALCULO_TAXA">
</asp:SqlDataSource>
      <asp:SqlDataSource ID="dsParceiros" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_PARCEIRO as Id , NM_RAZAO RazaoSocial FROM TB_PARCEIRO WHERE (FL_VENDEDOR = 1 OR FL_VENDEDOR_DIRETO = 1)
          union SELECT  0, ' Selecione' ORDER BY NM_RAZAO"></asp:SqlDataSource>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
