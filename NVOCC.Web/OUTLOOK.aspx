<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="OUTLOOK.aspx.vb" Inherits="NVOCC.Web.OUTLOOK" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <div class="col-lg-6 col-lg-offset-3 col-md-12 col-sm-12">
     <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">EMAIL COTAÇÃO
                    </h3>
                </div>
                <div class="panel-body">

                    
                     <div class="tab-content">
                      
                    
                  <div class="alert alert-success" id="divSuccess" runat="server" visible="false">                                       
                      <asp:label ID="lblSuccess" runat="server"  /> 
                  </div>
                    <div class="alert alert-danger" id="divErro" runat="server" visible="false">                                       
                      <asp:label ID="lblerro" runat="server"  /> 
                  </div>
              
                 <div class="row" style="display:none">
                    <div class="col-md-12">
                        <div class="form-group">
                            <label class="control-label">De:</label><label runat="server" style="color:red" >*</label> 
                            <asp:TextBox ID="txtRemetente" runat="server" CssClass="form-control"  TextMode="MultiLine" Rows="1" onkeyUp="return CheckMaxCount(this,event,250);"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div class="form-group">
                            <label class="control-label">Para:</label><label runat="server" style="color:red" >*</label> 
                            <asp:TextBox ID="txtDestinatario" runat="server" CssClass="form-control"  TextMode="MultiLine" Rows="1" onkeyUp="return CheckMaxCount(this,event,250);"></asp:TextBox><small style="color:gray">(Informe 1 ou mais endereços de eMail's separados por ponto e vírgula)</small>                       
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div class="form-group">
                            <label class="control-label">CC:</label>
                            <asp:TextBox ID="txtCC" runat="server" CssClass="form-control"  TextMode="MultiLine" Rows="1" onkeyUp="return CheckMaxCount(this,event,250);"></asp:TextBox><small style="color:gray">(Informe 1 ou mais endereços de eMail's separados por ponto e vírgula)</small>                       
                        </div>
                    </div>
                </div>
              <div class="row" style="display:none">
                    <div class="col-md-12">
                        <div class="form-group">
                            <label class="control-label">CCO:</label>
                            <asp:TextBox ID="txtCCO" runat="server" CssClass="form-control"  TextMode="MultiLine" Rows="1" onkeyUp="return CheckMaxCount(this,event,250);"></asp:TextBox><small style="color:gray">(Informe 1 ou mais endereços de eMail's separados por ponto e vírgula)</small>   
                        </div>
                    </div>
                </div>
                          <div class="row"> 
                        <div class="col-sm-12">
                                    <div class="form-group">
                                        <label class="control-label">Assunto:</label>
                                        <asp:TextBox ID="txtAssunto" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                              </div>  
                    </div>
                          <div class="row">
                    <div class="col-md-12">
                        <div class="form-group">
                            <label class="control-label">Mensagem:</label>
                            <asp:TextBox ID="txtMsg" runat="server" CssClass="form-control"  TextMode="MultiLine" Rows="5" onkeyUp="return CheckMaxCount(this,event,250);"></asp:TextBox>                     
                        </div>
                    </div>
                </div>        
                <div class="row">

                                <div class="col-sm-3 col-sm-offset-6">
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <asp:Button ID="btnSair" runat="server" CssClass="btn btn-warning btn-block" Text="Sair"  />
                                    </div>
                                </div>

                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <asp:Button ID="btnEnviar" runat="server" CssClass="btn btn-primary btn-block" Text="Enviar"  />
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
