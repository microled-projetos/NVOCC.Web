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
                            <asp:UpdatePanel ID="UpdatePanel15" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
                                        <ContentTemplate>
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
                                        <label class="control-label">Tax ID:</label>
                                        <asp:TextBox ID="txtTaxID" runat="server" MaxLength="100" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                     <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Agreement Number:</label>
                                        <asp:TextBox ID="txtAgreement" runat="server" MaxLength="100" CssClass="form-control" ></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Payment To:</label>
                                        <asp:TextBox ID="txtPayment" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                    </div>
                                </div>

                                </div>
                                 <div class="row">
                                                                     <div class="col-sm-3">
                                    <div class="form-group">
                                        <label class="control-label">Bank Name:</label>
                                        <asp:TextBox ID="txtBank" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                    </div>
                                </div>                                
                                    <div class="col-sm-3">
                                    <div class="form-group">
                                        <label class="control-label">Bank Address:</label>
                                        <asp:TextBox ID="txtBankAddress" runat="server" MaxLength="100" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <label class="control-label">Account Number:</label>
                                        <asp:TextBox ID="txtAccount" runat="server" MaxLength="100" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <label class="control-label">Agency Number:</label>
                                        <asp:TextBox ID="txtAgency" runat="server" MaxLength="100" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            
                            <div class="row"> 
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Swift Code:</label>
                                        <asp:TextBox ID="txtSwift" runat="server" MaxLength="100" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                 <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Refund:</label>
                                        <asp:TextBox ID="txtRefund" runat="server" MaxLength="100" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                  <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Iban Br:</label>
                                        <asp:TextBox ID="txtIban" runat="server" MaxLength="100" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                
                                </div>
         <div class="row">
                                 <div class="col-sm-10">
                                    <div class="form-group">
                                        <label class="control-label">Observações:</label>
                                        <asp:TextBox ID="txtObs" runat="server" CssClass="form-control" MaxLength="1000" ></asp:TextBox>
                                    </div>
                                </div>
                               <div class="col-sm-2">
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <asp:button  ID="btnGravar" OnClientClick="javascript:return confirm('Deseja realmente gravar essas informações?');"  runat="server" CssClass="btn btn-primary btn-block" Text="Gravar"  />
                                    </div>
                                </div>
                            </div>  
                                            
                                            </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnGravar" />
                                            <asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="gvArquivos" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                            </div><br />
                                                        <div class="linha-colorida">Upload</div>
            
        <br />
                           
                               <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <label>&nbsp;</label> 
                                        <asp:FileUpload ID="FileUpload1"  CssClass="form-control" runat="server" Visible="true" style="display:block"  onchange="Javascript: VerificaTamanhoArquivo();" ></asp:FileUpload>                                                                                         
                                    </div>
                                </div>
                                   <div class="col-sm-1">
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <asp:button  ID="btnUpload" OnClientClick="javascript:return confirm('Deseja realmente realizar o upload?');"  runat="server" CssClass="btn btn-success btn-block" Text="Upload"  />
                                    </div>
                                </div>
                            </div>
                                 <br />
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
                                        <ContentTemplate>
                            <div class="row">
                                 <div class="col-sm-12">
                                      <asp:TextBox ID="txtArquivoSelecionado" runat="server" style="display:none"></asp:TextBox>
                                <asp:GridView ID="gvArquivos" runat="server" AutoGenerateColumns="false" EmptyDataText="Nenhum arquivo enviado"  CssClass="table table-hover table-condensed table-bordered">
            <Columns>
                <asp:BoundField DataField="Text" HeaderText="Nome do Arquivo"/>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkVisualizar" Text="Visualizar"  CommandName="Visualizar" CommandArgument='<%# Eval("Value") %>' runat="server"></asp:LinkButton>
                    </ItemTemplate>
                     <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                </asp:TemplateField>
                 <asp:TemplateField Visible="false">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkDownload" Text="Download"  CommandName="Download" CommandArgument='<%# Eval("Value") %>' runat="server"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkDeleta" Text="Deletar"  OnClientClick="javascript:return confirm('Deseja realmente excluir este arquivo?');"  CommandName="Excluir" CommandArgument='<%# Eval("Value") %>' runat="server"  />
                    </ItemTemplate>
                     <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView></div>
                            </div>
  </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="gvArquivos" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                            </div>
                        
                        </div>

            </div>
       </div>
    </div>
    

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">
    <script>
     function AbrirArquivo() {


         var Arquivo = document.getElementById('<%= txtArquivoSelecionado.ClientID %>').value;
         console.log(Arquivo);

         window.open(Arquivo, '_blank');
     }

        function VerificaTamanhoArquivo() {

            var btn = document.getElementById('<%= btnUpload.ClientID %>');
            var fi = document.getElementById('<%= FileUpload1.ClientID %>');
            var maxFileSize = 4194304; // 4MB -> 4 * 1024 * 1024

            if (fi.files.length > 0) {

                for (var i = 0; i <= fi.files.length - 1; i++) {

                    var fsize = fi.files.item(i).size;

                    if (fsize < maxFileSize) {
                        btn.style.display = 'block';
                    }
                    else {
                        alert("Arquivo excede tamanho permitido!");
                        fi.value = null;
                        btn.style.display = 'none';
                    }

                }
            }
        }
    </script>
</asp:Content>
