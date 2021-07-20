<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="FollowUp.aspx.vb" Inherits="NVOCC.Web.FollowUp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .none {
            display: none;
        }
       
    </style>

    <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
        <ContentTemplate>
            <ajaxToolkit:ModalPopupExtender ID="mpeDetalhes" runat="server" PopupControlID="Panel1" CancelControlID="btnFechar" TargetControlID="txtID_BL"></ajaxToolkit:ModalPopupExtender>

            <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" Style="display: none">
                <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="modalMercaoriaNova">
                                <asp:Label ID="lblTituloDetalhe" runat="server" /></h5>
                        </div>
                        <div class="modal-body">
                            <div runat="server" id="DetalhesBL">
                            </div>

                        </div>
                        <div class="modal-footer">
                            <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFechar" Text="Close" />
                        </div>

                    </div>

                </div>

            </asp:Panel>
            <div class="row">
                <div class="col-lg-12 col-md-12 col-sm-12">

                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            <h3 class="panel-title">FOLLOW UP
                                <asp:Label ID="NumeroBL" runat="server" />
                            </h3>
                        </div>
                        <div class="panel-body">
                            <asp:TextBox ID="txtID_BL" runat="server" Style="display: none" CssClass="form-control"></asp:TextBox>
                            <div class="table-responsive tableFixHead">

                                <asp:GridView ID="gdvFollowUp" DataKeyNames="ID_FOLLOWUP" DataSourceID="dsFollowUp" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" Style="max-height: 400px; overflow: auto;"
                                    AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:BoundField DataField="ID_FOLLOWUP" Visible="false" HeaderText="ID_FOLLOWUP" SortExpression="ID_FOLLOWUP" />
                                        <asp:BoundField DataField="NM_EVENTO" HeaderText="ETAPA" SortExpression="NM_EVENTO" />
                                        <asp:TemplateField HeaderText="STATUS" SortExpression="STATUS_ETAPA">
                                            <ItemTemplate>

                                                <asp:Image ID="Image1" runat="server" />
                                                -
                                                <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("STATUS_ETAPA") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="USUARIO" Visible="False" HeaderText="USUARIO" SortExpression="USUARIO" />
                                        <asp:BoundField DataField="DATA" HeaderText="DATA" SortExpression="DATA" />
                                        <asp:BoundField DataField="STATUS_ETAPA" ItemStyle-CssClass="none" />
                                        <%--<asp:TemplateField HeaderText="Cummulative Amount">
      <ItemTemplate>
        <asp:Label runat="server" ID="lbldata" Text='<%# Eval("DATA") %>' />
      </ItemTemplate>
    </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="teste">
      <ItemTemplate>
        <asp:Label runat="server" ID="lblteste" />
      </ItemTemplate>
    </asp:TemplateField>--%>
                                        <%--<asp:TemplateField HeaderText="">
                                              <ItemTemplate>
                                                 <asp:linkButton ID="btnSelecionar" runat="server"  
                                CommandArgument='<%# Eval("ID_FOLLOWUP") %>'  Visible='<%# (Eval("ID_FOLLOWUP") = 21) %>'
  CommandName="Detalhes" Text=" + Detalhes"></asp:linkButton>                     
                                              </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                        </asp:TemplateField> --%>
                                        <asp:TemplateField ItemStyle-Width="20px">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnSelecionar2" runat="server"
                                                    CommandArgument='<%# Eval("ID_FOLLOWUP") %>' Visible='<%# (Eval("ID_FOLLOWUP") = 21) %>'
                                                    CommandName="Detalhes">
                                                            <img border="0" src="Content/imagens/plus.png" alt="" /></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Width="20px" VerticalAlign="Middle"></ItemStyle>
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle CssClass="headerStyle" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="gdvFollowUp" />
            <asp:AsyncPostBackTrigger ControlID="btnFechar" />
        </Triggers>
    </asp:UpdatePanel>

    
    <asp:SqlDataSource ID="dsFollowUp" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>" SelectCommand="EXEC [dbo].[PROC_FOLLOWUP] @ID_BL">
        <SelectParameters>
            <asp:ControlParameter Name="ID_BL" Type="Int32" ControlID="txtID_BL" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">
    <script src="Content/js/jquery.smartWizard.js"></script>
    <script src="Content/js/select2.min.js"></script>
</asp:Content>
