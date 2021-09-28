<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="TextoCotacao.aspx.vb" Inherits="NVOCC.Web.TextoCotacao" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <div class="row principal">
        <div class="col-lg-6 col-lg-offset-3 col-md-12 col-sm-12">

            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">TEXTO ADICIONAL - COTAÇÃO
                    </h3>
                </div>
                <div class="panel-body">
                
                    <div class="row">
                     <div class="col-sm-12">

                           <div class="form-group">
                               <label class="control-label" style="text-align: left">Linguagem da cotação:</label>
                                   <asp:RadioButtonList ID="rdTipo" runat="server" AutoPostBack="true">
                                                        <asp:ListItem Value="1" Selected="True">&nbsp;PORTUGUES</asp:ListItem>
                                                        <asp:ListItem Value="2">&nbsp;INGLÊS</asp:ListItem>
                                                    </asp:RadioButtonList>      
                           </div>
                       </div>
                        </div>
                    <div class="row">
                     <div class="col-sm-12">
                         <div class="form-group">
                                 <label class="control-label" style="text-align: left">Texto:</label>
                                 <asp:TextBox ID="txtTexto" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="30"></asp:TextBox>
                             </div>
                          </div>
                           </div>
                    <div class="row">
                    <div class="col-sm-offset-6 col-sm-3">
                        <div class="form-group">
                           <asp:Button ID="btnGravar" runat="server" CssClass="btn btn-primary btn-block" Text="Gravar" />  
                           </div>
                        </div>
                        <div class="col-sm-3">
                        <div class="form-group">
                           <asp:Button ID="btnLimpar" runat="server" CssClass="btn btn-warning btn-block" Text="Limpar" />  
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
