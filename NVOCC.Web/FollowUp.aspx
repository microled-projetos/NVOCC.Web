<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="FollowUp.aspx.vb" Inherits="NVOCC.Web.FollowUp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .none{
            display:none;
        }    
         .teste{
           text-align:left
       }
        </style>
    <div class="row">
                <div class="col-lg-12 col-md-12 col-sm-12">

    <div class="panel panel-primary"  >
                <div class="panel-heading">
                    <h3 class="panel-title">FOLLOW UP <asp:Label ID="NumeroBL" runat="server" />
                    </h3>
                </div>
                <div class="panel-body">
                                                                    <asp:TextBox ID="txtID_BL" runat="server" Style="display:none" CssClass="form-control"></asp:TextBox>
                                                <div class="table-responsive tableFixHead">

         <asp:GridView ID="gdvFollowUp" DataKeyNames="ID_FOLLOWUP" DataSourceID="dsFollowUp" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" Style="max-height: 400px; overflow: auto;"
                                            AutoGenerateColumns="false">
                                            <Columns>             
                                                <asp:BoundField DataField="ID_FOLLOWUP" Visible="False" HeaderText="ID_FOLLOWUP" SortExpression="ID_FOLLOWUP" />
                                                <asp:BoundField DataField="NM_EVENTO" HeaderText="ETAPA" SortExpression="NM_EVENTO" />
                                                 <asp:TemplateField HeaderText="STATUS" SortExpression="STATUS_ETAPA" ItemStyle-CssClass="teste">
                    <ItemTemplate>
                                            
                        <asp:Image ID="Image1" runat="server" /> - <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("STATUS_ETAPA") %>'/>
                    </ItemTemplate>
                </asp:TemplateField>
                                                <asp:BoundField DataField="USUARIO" HeaderText="USUARIO" SortExpression="USUARIO" />
                                                <asp:BoundField DataField="DATA" HeaderText="DATA" SortExpression="DATA"  />
                                                <asp:BoundField DataField="STATUS_ETAPA"  ItemStyle-CssClass="none" />

                                            </Columns>
                                            <HeaderStyle CssClass="headerStyle" />
                                        </asp:GridView> 
                                                    </div>
                    </div>
        </div>
        </div>
        </div>
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
