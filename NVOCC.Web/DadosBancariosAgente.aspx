<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="DadosBancariosAgente.aspx.vb" Inherits="NVOCC.Web.DadosBancariosAgente" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
      <br />
<div class="row principal">
    <style>
        .larguraMinima{
     min-width: 140px;
} 
    </style>
       <div class="col-lg-12 col-md-12 col-sm-12">

            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">DADOS BANCARIOS DO AGENTE - <asp:label ID="lblRazaoSocial" runat="server"></asp:label> 
                    </h3>
                </div>
                <div class="panel-body">

                    <ul class="nav nav-tabs" role="tablist">
                        <li class="active">
                            <a href="#cadastro" role="tab" data-toggle="tab">
                                <i class="fa fa-edit" style="padding-right: 8px;"></i>Cadastro
                            </a>
                        </li>
                    </ul>
                     <div class="tab-content">
                        <div class="tab-pane fade active in" id="cadastro">

        <asp:TextBox ID="txtID" runat="server" CssClass="form-control" Style="display:none"></asp:TextBox>

                                    <div class="alert alert-success" id="divSuccess" runat="server" visible="false">
                                        Ação realizada com sucesso!
                                    </div>
                                    <div class="alert alert-danger" id="divErro" runat="server" visible="false">
                                    <asp:Label runat="server"  ID="msgErro" />
                                    </div>
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Payment To:</label>
                                        <asp:TextBox ID="txtPayment" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                    </div>
                                </div>
                                 <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Name Bank:</label>
                                        <asp:TextBox ID="txtBank" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <label class="control-label">Account:</label>
                                        <asp:TextBox ID="txtAccount" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <label class="control-label">Agency:</label>
                                        <asp:TextBox ID="txtAgency" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            
                            <div class="row"> 
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <label class="control-label">Swift:</label>
                                        <asp:TextBox ID="txtSwift" runat="server"  CssClass="form-control" MaxLength="6" onkeypress="return nomeFuncao( this , event ) ;"></asp:TextBox>
                                    </div>
                                </div>
                                 <div class="col-sm-3">
                                    <div class="form-group">
                                        <label class="control-label">Refund:</label>
                                        <asp:TextBox ID="txtRefund" runat="server"  CssClass="form-control" MaxLength="6" onkeypress="return nomeFuncao( this , event ) ;"></asp:TextBox>
                                    </div>
                                </div>
                                  <div class="col-sm-3">
                                    <div class="form-group">
                                        <label class="control-label">Iban Br:</label>
                                        <asp:TextBox ID="txtIban" runat="server"  CssClass="form-control" MaxLength="6" onkeypress="return nomeFuncao( this , event ) ;"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <label class="control-label">Agreement:</label>
                                        <asp:TextBox ID="txtAgreement" runat="server" CssClass="form-control" MaxLength="6" onkeypress="return nomeFuncao( this , event ) ;"></asp:TextBox>
                                    </div>
                                </div>
                                </div>
         <div class="row">
                                 <div class="col-sm-10">
                                    <div class="form-group">
                                        <label class="control-label">Obs:</label>
                                        <asp:TextBox ID="txtObs" runat="server" CssClass="form-control" MaxLength="100" onkeypress="return nomeFuncao( this , event ) ;"></asp:TextBox>
                                    </div>
                                </div>
                               <div class="col-sm-2">
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <asp:button  ID="btnGravar" OnClientClick="javascript:return confirm('Deseja realmente gravar essas informações?');"  runat="server" CssClass="btn btn-primary btn-block" Text="Gravar"  />
                                    </div>
                                </div>
                            </div>    <br />
                                                        <div class="linha-colorida">Upload</div>
            
        <br />
                           
                               <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <asp:FileUpload ID="txtUpload"  CssClass="form-control" runat="server" Visible="true" style="display:block"></asp:FileUpload>                                                                                         
                                    </div>
                                </div>
                                   <div class="col-sm-1">
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <asp:button  ID="Button1" OnClientClick="javascript:return confirm('Deseja realmente realizar o upload?');"  runat="server" CssClass="btn btn-success btn-block" Text="Upload"  />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                
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
