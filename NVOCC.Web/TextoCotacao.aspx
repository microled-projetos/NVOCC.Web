<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="TextoCotacao.aspx.vb" Inherits="NVOCC.Web.TextoCotacao" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <div class="row principal">
        <div class="col-lg-6 col-lg-offset-3 col-md-12 col-sm-12">

            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">TAXA CÂMBIO FCA
                    </h3>
                </div>
                <div class="panel-body">
                    <div class="tab-content">
                    <div class="rows">
                     <div class="col-sm-12">

                           <div class="form-group">
                                   <asp:RadioButtonList ID="rdTipo" runat="server" AutoPostBack="true">
                                                        <asp:ListItem Value="1" Selected="True">&nbsp;PORTUGUES</asp:ListItem>
                                                        <asp:ListItem Value="2">&nbsp;INGLÊS</asp:ListItem>
                                                    </asp:RadioButtonList>      
                           </div>
                       </div>
                        </div>
                    <div class="rows">
                     <div class="col-sm-12">
                                 <label class="control-label" style="text-align: left">Texto:</label>
                                 <asp:TextBox ID="txtTexto" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="30"></asp:TextBox>
                          </div>
                           </div>
                    <div class="rows">
                    <div class="col-sm-3">
                           <asp:Button ID="btnGravar" runat="server" CssClass="btn btn-primary btn-block" Text="Gravar" />  
                           </div>
    </div>
                 </div>

                </div>
                </div>
            </div>
         </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
