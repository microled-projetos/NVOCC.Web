<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="CadastrarParceiro.aspx.vb" Inherits="NVOCC.Web.CadastrarParceiro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row principal">
        <div class="col-lg-12 col-md-12 col-sm-12">

            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">PARCEIROS
                    </h3>
                </div>
                <div class="panel-body">

                    <ul class="nav nav-tabs" role="tablist">
                        <li class="active">
                            <a href="#cadastro" role="tab" data-toggle="tab">
                                <i class="fa fa-edit" style="padding-right:8px;"></i>Dados Cadastrais
                            </a>
                        </li>
                        <li>
                            <a href="#adicionais" role="tab" data-toggle="tab">
                                <i class="fas fa-id-card" style="padding-right:8px;"></i>Inf. Adicionais
                            </a>
                        </li>
                         <li>
                            <a href="#contatos" role="tab" data-toggle="tab">
                                <i class="fas fa-phone-square" style="padding-right:8px;"></i>Contatos
                            </a>
                        </li> <li>
                            <a href="#email" role="tab" data-toggle="tab">
                                <i class="far fa-calendar-alt" style="padding-right:8px;"></i>Email x Eventos
                            </a>
                        </li>
                    </ul>

                    <div class="tab-content">
                        <div class="tab-pane fade active in" id="cadastro" >
                             <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="always" ChildrenAsTriggers="True">
    <ContentTemplate>

                            <div class="row">

                                <div class="col-sm-12">
                                    <br />
                                    <asp:ValidationSummary ID="Validacoes" runat="server" ShowModelStateErrors="true" CssClass="alert alert-danger" />

                                    <div class="alert alert-success" id="divmsg" runat="server" visible="false">
                                        Registro cadastrado/atualizado com sucesso!
                                    </div>
                                    <div class="alert alert-danger" id="divmsg1" runat="server" visible="false">
                                    <asp:Label runat="server"  ID="msgErro" />
                                    </div>
                                    <div class="alert alert-warning" id="divInformativa"  visible="false" runat="server">
                                    <asp:label ID="lblInformacao" runat="server"></asp:label>
                                    </div>

                                </div>
                            </div>

                            
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <label class="control-label">Código:</label>
                                        <asp:TextBox ID="txtID" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <label class="control-label">Tipo Pessoa:</label><label runat="server" style="color:red" >*</label>
                                         <asp:DropDownList ID="ddlTipoPessoa" runat="server" CssClass="form-control" Font-Size="11px" AutoPostBack="True">
                                            <asp:ListItem Value="0" Selected="True">Selecione</asp:ListItem>
                                            <asp:ListItem Value="1">Juridico</asp:ListItem>
                                            <asp:ListItem Value="2">Fisica</asp:ListItem>
                                            <asp:ListItem Value="3">Estrangeira</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                 <div class="col-sm-3">
                               
                                </div>
                                <div class="col-sm-3" visible="false" runat="server">
                                    <div class="form-group"> 
                                        <asp:linkButton ID="btnClienteFinal" href="ClienteFinal.aspx" targat="_blank" runat="server" Style="margin-top: 20px;" CssClass="btn btn-success" >
                                            <span class="glyphicon glyphicon-plus"  style="font-size:medium"></span> &nbsp;Cliente Final
                                        </asp:linkButton>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="control-label">Razão Social:</label><label runat="server" style="color:red" >*</label>
                                        <asp:TextBox ID="txtRazaoSocial" runat="server" CssClass="form-control" MaxLength="80"></asp:TextBox>
                                    </div>
                                </div>                          
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="control-label">Nome Fantasia:</label>
                                        <asp:TextBox ID="txtNomeFantasia" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                    </div>
                                </div>                                 
                                </div>
                             <div class="row" id="divCNPJ" runat="server" visible="false">
                                <div class="col-sm-6"  >
                                    <div class="form-group">
                                        <label class="control-label">CNPJ:</label><label runat="server" style="color:red" >*</label>
                                        <asp:TextBox ID="txtCNPJ" runat="server"  CssClass="form-control cnpj" MaxLength="18"></asp:TextBox>
                                    </div>
                                </div>
                                 <div class="col-sm-6">
                                    <div class="form-group"> 
                                        <label class="control-label" style="color:white">x</label>
                                        <asp:Button ID="btnConsultaCNPJ" runat="server" Style="margin-top: 20px;" CssClass="btn btn-success" Text="Consultar CNPJ"  />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                 <div class="col-sm-6" id="divCPF" runat="server"  visible="false">
                                    <div class="form-group">
                                        <label class="control-label">CPF:</label><label runat="server" style="color:red" >*</label>
                                        <asp:TextBox ID="txtCPF" runat="server" CssClass="form-control cpf" MaxLength="18"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Inscrição Estadual:</label>
                                        <asp:TextBox ID="txtInscEstadual" runat="server" CssClass="form-control inteiro" MaxLength="18"></asp:TextBox>
                                    </div>
                                </div>
                                 <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Inscrição Municipal:</label>
                                        <asp:TextBox ID="txtInscMunicipal" runat="server" CssClass="form-control inteiro" MaxLength="18"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Email:</label>
                                        <asp:TextBox ID="txtEmailParceiro" runat="server" CssClass="form-control" MaxLength="200"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <label class="control-label">Telefone:</label>
                                        <asp:TextBox ID="txtTelefone" runat="server" CssClass="form-control telefone"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <label class="control-label">CEP:</label><asp:label ID="RedCEP"  runat="server" style="color:red" >*</asp:label>
                                        <asp:TextBox ID="txtCEP" runat="server" CssClass="form-control cep" MaxLength="10"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="control-label">Endereço:</label><asp:label ID="RedEnd" runat="server" style="color:red" >*</asp:label>
                                        <asp:TextBox ID="txtEndereco" runat="server" CssClass="form-control" MaxLength="150"></asp:TextBox>
                                    </div>
                                </div>
                                
                            </div>

                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <label class="control-label">Número:</label><asp:label ID="RedNum"  runat="server" style="color:red" >*</asp:label>
                                        <asp:TextBox ID="txtNumero" runat="server" CssClass="form-control inteiro" MaxLength="10"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="control-label">Bairro:</label><asp:label ID="RedBairro" runat="server" style="color:red" >*</asp:label>
                                        <asp:TextBox ID="txtBairro" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <label class="control-label">Cidade:</label><asp:label ID="RedCidade" runat="server" style="color:red" >*</asp:label>
                                           <asp:DropDownList ID="ddlCidade" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_CIDADE" DataSourceID="dsCidades" DataValueField="ID_CIDADE">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="form-group">
                                        <label class="control-label">Complemento:</label>
                                        <asp:TextBox ID="txtComplemento" runat="server" CssClass="form-control"  Rows="8" TextMode="Multiline" MaxLength="1000"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                           <ajaxToolkit:ModalPopupExtender id="mpeTaxas" runat="server" PopupControlID="Panel1" TargetControlID="txtID"  CancelControlID="btnNao"></ajaxToolkit:ModalPopupExtender>
   <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" style="display:none;" >            
                                           <center>     <div class=" modal-dialog modal-dialog-centered modal-sm" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title" id="modalMercaoriaNova">Taxas</h5>
                                                        </div>
                                                        <div class="modal-body">    
                                                             <br/>
                                   
                                  
                            <div class="row">
                               <h5>DESEJA INSERIR TAXAS PARA ESSE PARCEIRO?</h5>
                             </div>
                           
                      
                                                       
                                                        </div>                     
                               <div class="modal-footer">
                                                            <asp:Button runat="server" CssClass="btn btn-danger" ID="btnNao" text="Não" />
                                                            <asp:Button runat="server" CssClass="btn btn-success" ID="btnSim" text="Sim" />
                                                        </div>
                                                    
                                                </div>
      
                                       </div>     </center>       
     </asp:Panel>
        </ContentTemplate> 
                                  <Triggers>
       <asp:PostBackTrigger ControlID="ddlTipoPessoa"  />
<%--               <asp:AsyncPostBackTrigger ControlID="txtCEP" />--%>
                                              <ASP:AsyncPostBackTrigger ControlID="txtCEP" />


    </Triggers>

      </asp:UpdatePanel>
                            </div>                       
                        <div class="tab-pane fade" id="adicionais" >
   <br />
                             <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True">
    <ContentTemplate>
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Valor Alíquota ISS:</label>
                                        <asp:TextBox ID="txtAliquotaISS" runat="server" CssClass="form-control aliquotas" MaxLength="50"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Valor Alíquota PIS:</label>
                                        <asp:TextBox ID="txtAliquotaPIS" runat="server" CssClass="form-control aliquotas" MaxLength="50"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Valor Alíquota COFINS:</label>
                                        <asp:TextBox ID="txtAliquotaCOFINS" runat="server" CssClass="form-control aliquotas" MaxLength="50"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <asp:Checkbox ID="ckbISS" runat="server" CssClass="form-control" text="&nbsp;&nbsp;Isenção ISS" ></asp:Checkbox>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <asp:Checkbox ID="ckbPIS" runat="server" CssClass="form-control" text="&nbsp;&nbsp;Isenção PIS" ></asp:Checkbox>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <asp:Checkbox ID="ckbCOFINS" runat="server" CssClass="form-control" text="&nbsp;&nbsp;Isenção COFINS" ></asp:Checkbox>
                                    </div>
                                </div>
                             </div >
                            <div class="row"> <div class="linha-colorida">SPREAD MARITIMO IMPORTAÇÃO</div>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <label class="control-label">FCL:</label>
                                        <asp:TextBox ID="txtMaritimoImpoFCL" runat="server"  CssClass="form-control " MaxLength="6" onkeypress="return nomeFuncao( this , event ) ;"></asp:TextBox>
                                    </div>
                                </div>
                                  <div class="col-sm-3">
                                    <div class="form-group">
                                        <label class="control-label">Acordo Câmbio</label>
                                        <asp:DropDownList ID="ddlAcordoCambioMaritimoImpoFCL" runat="server"  CssClass="form-control" Font-Size="11px" DataTextField="NM_ACORDO_CAMBIO" DataSourceID="dsAcordoCambio" DataValueField="ID_ACORDO_CAMBIO">
                                            </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <label class="control-label">LCL:</label>
                                        <asp:TextBox ID="txtMaritimoImpoLCL" runat="server" CssClass="form-control " MaxLength="6" onkeypress="return nomeFuncao( this , event ) ;"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <label class="control-label">Acordo Câmbio</label>
                                        <asp:DropDownList ID="ddlAcordoCambioMaritimoImpoLCL" runat="server"  CssClass="form-control" Font-Size="11px" DataTextField="NM_ACORDO_CAMBIO" DataSourceID="dsAcordoCambio" DataValueField="ID_ACORDO_CAMBIO">
                                            </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
        <div class="row">                                                     <div class="linha-colorida">SPREAD MARITIMO EXPORTAÇÃO</div>
            
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <label class="control-label">FCL:</label>
                                        <asp:TextBox ID="txtMaritimoExpoFCL" runat="server" CssClass="form-control" MaxLength="6" onkeypress="return nomeFuncao( this , event ) ;"></asp:TextBox>
                                    </div>
                                </div>
            <div class="col-sm-3">
                                    <div class="form-group">
                                        <label class="control-label">Acordo Câmbio</label>
                                        <asp:DropDownList ID="ddlAcordoCambioMaritimoExpoFCL" runat="server"  CssClass="form-control" Font-Size="11px" DataTextField="NM_ACORDO_CAMBIO" DataSourceID="dsAcordoCambio" DataValueField="ID_ACORDO_CAMBIO">
                                            </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <label class="control-label">LCL:</label>
                                        <asp:TextBox ID="txtMaritimoExpoLCL" runat="server" CssClass="form-control" onkeypress="return nomeFuncao( this , event ) ;"  MaxLength="6"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <label class="control-label">Acordo Câmbio</label>
                                        <asp:DropDownList ID="ddlAcordoCambioMaritimoExpoLCL" runat="server"  CssClass="form-control" Font-Size="11px" DataTextField="NM_ACORDO_CAMBIO" DataSourceID="dsAcordoCambio" DataValueField="ID_ACORDO_CAMBIO">
                                            </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                    <div class="row">
                                                     <div class="linha-colorida">SPREAD AEREO</div>

                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Importação:</label>
                                        <asp:TextBox ID="txtAereoImpo" runat="server" CssClass="form-control " MaxLength="6" onkeypress="return nomeFuncao( this , event ) ;"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Exportação:</label>
                                        <asp:TextBox ID="txtAereoExpo" runat="server" CssClass="form-control " MaxLength="6" onkeypress="return nomeFuncao( this , event ) ;"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Acordo Câmbio</label>
                                        <asp:DropDownList ID="ddlAcordoCambioAereo" runat="server"  CssClass="form-control" Font-Size="11px"  DataTextField="NM_ACORDO_CAMBIO" DataSourceID="dsAcordoCambio" DataValueField="ID_ACORDO_CAMBIO">
                                            </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">eMail NF Eletrônica:</label><label ID="lblRed" runat="server" visible="false" style="color:red" >*</label>
                                        <asp:TextBox ID="txtEmailNF" runat="server" CssClass="form-control" MaxLength="200"></asp:TextBox>
                                    </div>
                                </div>
             
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Código IATA:</label>
                                        <asp:TextBox ID="txtCDIATA" runat="server" CssClass="form-control inteiro" MaxLength="50" ></asp:TextBox>
                                    </div>
                                </div>
                                 <div class="col-sm-2">
                                    <div class="form-group">
                                        <label class="control-label">Qtd. Dias de Faturamento:</label>
                                        <asp:TextBox ID="txtQtdFaturamento" runat="server" CssClass="form-control inteiro" MaxLength="50" ></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <label class="control-label">Tipo de Faturamento:</label>
                                       <asp:DropDownList ID="ddlTipoFaturamento" runat="server"  CssClass="form-control" Font-Size="11px"  DataTextField="NM_TIPO_FATURAMENTO" DataSourceID="dsTipoFaturamento" DataValueField="ID_TIPO_FATURAMENTO">
                                            </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <asp:Checkbox ID="ckbAtivo" runat="server" CssClass="form-control" text="&nbsp;&nbsp;Ativo" ></asp:Checkbox>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <asp:Checkbox ID="ckbSimplesNacional" runat="server" CssClass="form-control" text="&nbsp;Simples Nacional" ></asp:Checkbox>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <asp:Checkbox ID="ckbRFB" runat="server" CssClass="form-control" text="&nbsp;&nbsp;Ativo RFB" ></asp:Checkbox>
                                    </div>
                                </div>
                              </div><br />
                                 <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <asp:Checkbox ID="ckbImportador" runat="server" autopostback="true" CssClass="form-control" text="&nbsp;&nbsp;Importador" ></asp:Checkbox>
                                    </div>
                                </div>
                                 <div class="col-sm-4">
                                    <div class="form-group">
                                        <asp:Checkbox ID="ckbExportador" runat="server"  autopostback="true" CssClass="form-control" text="&nbsp;&nbsp;Exportador" ></asp:Checkbox>

                                    </div>
                                </div>
                                     <div class="col-sm-4">
                                    <div class="form-group">
                                        <asp:Checkbox ID="ckbComissaria" runat="server" autopostback="true" CssClass="form-control" text="&nbsp;&nbsp;Comissária" ></asp:Checkbox>
                       

                                    </div>
                                </div>
                                 
                                
                                </div>   
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <asp:Checkbox ID="ckbAgente" runat="server" autopostback="true"  CssClass="form-control" text="&nbsp;&nbsp;NVOCC" ></asp:Checkbox>

                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <asp:Checkbox ID="ckbAgenteInternacional" runat="server" CssClass="form-control" text="&nbsp;Agente Internacional" ></asp:Checkbox>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <asp:Checkbox ID="ckbIndicador" runat="server" CssClass="form-control" text="&nbsp;&nbsp;Indicador" ></asp:Checkbox>
                                    </div>
                                </div>
                                </div>
         <div class="row">
                                        <div class="col-sm-4">
                                    <div class="form-group">
                                        <asp:Checkbox ID="ckbArmazemAtracacao" runat="server" CssClass="form-control" text="&nbsp;&nbsp;Armazém de Atracação" ></asp:Checkbox>
                                    </div>
                                </div>                               
                               <div class="col-sm-4">
                                    <div class="form-group">
                                        <asp:Checkbox ID="ckbArmazemDesembaraco" runat="server" CssClass="form-control" text="&nbsp;&nbsp;Armazém de Desembaraço" ></asp:Checkbox>
                                    </div>
                                </div>      
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <asp:Checkbox ID="ckbArmazemDescarga" runat="server" CssClass="form-control" text="&nbsp;&nbsp;Armazém de Descarga" ></asp:Checkbox>
                                    </div>
                                </div>      
                            </div>
                            <div class="row">
                                 <div class="col-sm-4">
                                    <div class="form-group">
                                        <asp:Checkbox ID="ckbTransportador" runat="server" CssClass="form-control" text="&nbsp;Transportador" ></asp:Checkbox>
                                    </div>
                                </div>
                                         <div class="col-sm-4">
                                    <div class="form-group">
                                        <asp:Checkbox ID="ckbPrestador" runat="server" CssClass="form-control" text="&nbsp;&nbsp;Prestador" ></asp:Checkbox>
                                    </div>
                                </div>
                               
                                     <div class="col-sm-4">
                                    <div class="form-group">
                                        <asp:Checkbox ID="ckbEquipeInsideSales" runat="server" CssClass="form-control" text="&nbsp;&nbsp;Equipe Inside Sales" ></asp:Checkbox>
                                    </div>
                                </div>
                                 
                                
                        </div>        
        <div class="row">
            <div class="col-sm-4">
                                    <div class="form-group">
                                        <asp:Checkbox ID="ckbVendedor" runat="server" CssClass="form-control" text="&nbsp;&nbsp;Vendedor" ></asp:Checkbox>
                                    </div>
                                </div>
                                 <div class="col-sm-4">
                                    <div class="form-group">
                                        <asp:Checkbox ID="ckbVendedorDireto" runat="server" CssClass="form-control" text="&nbsp;Vendedor Direto" ></asp:Checkbox>
                                    </div>
                                </div>
                                 <div class="col-sm-4">
                                    <div class="form-group">
                                        <asp:Checkbox ID="ckbShipper" runat="server" CssClass="form-control" text="&nbsp;&nbsp;Shipper" ></asp:Checkbox>
                                    </div>
                                </div>     
                                    
                               
                        </div>        
                            <div id="divVendedor" class="divVendedor" style="display:none" runat="server">
                                 <div class="row" >
                                <div class="col-sm-9">
                                    <div class="form-group">
                                        <label class="control-label">Vendedor:</label><label ID="lblRed2" runat="server" visible="false" style="color:red" >*</label>
                                        <asp:DropDownList ID="ddlVendedor" runat="server"  CssClass="form-control" Font-Size="11px"  DataTextField="NM_RAZAO" DataSourceID="dsVendedor"  DataValueField="ID_PARCEIRO">
                                            </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            </div>

                       </ContentTemplate>
</asp:UpdatePanel>   

                    </div>    
                        <div class="tab-pane fade" id="contatos" >
   <br />
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="control-label">Nome:</label>
                                        <asp:TextBox ID="txtNomeContato" runat="server"  CssClass="form-control" MaxLength="50"></asp:TextBox>
                                    </div>
                                </div>
                                </div>
                                <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="control-label">Telefone:</label>
                                        <asp:TextBox ID="txtTelContato" runat="server"  CssClass="form-control telefone" ></asp:TextBox>
                                    </div>
                                </div> 
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="control-label">Celular:</label>
                                        <asp:TextBox ID="txtCelularContato" runat="server"  CssClass="form-control celular" ></asp:TextBox>
                                    </div>
                                </div> 
                                </div>
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="control-label">eMail:</label>
                                        <asp:TextBox ID="txtEmailContato" runat="server"  CssClass="form-control" MaxLength="50"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="control-label">Departamento:</label>
                                        <asp:TextBox ID="txtDepartamento" runat="server"  CssClass="form-control" MaxLength="30"></asp:TextBox>
                                    </div>
                                </div>
                                 </div> 

                        </div>
                        <div class="tab-pane fade" id="email" >
                             <ul class="nav nav-tabs" role="tablist">
                        <li class="active">
                            <a href="#detalhes" role="tab" data-toggle="tab">
                                <i class="fa fa-edit" style="padding-right: 8px;"></i>Detalhes
                            </a>
                        </li>
                        <li>
                            <a href="#lista" role="tab" data-toggle="tab">
                                <i class="fa fa-search" style="padding-right: 8px;"></i>Lista
                            </a>
                        </li>
                    </ul>
   <br />
                 <div class="tab-content">
                        <div class="tab-pane fade active in" id="detalhes">
                            <asp:UpdatePanel ID="updPainel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="TRUE">
    <ContentTemplate>
                <div class="row">
                   <div class="col-sm-6">
                    <div class="form-group">
                         <label class="control-label">Evento:</label>
                                <asp:DropDownList ID="ddlEvento" runat="server" CssClass="form-control" Font-Size="11px" AutoPostBack="True" DataTextField="NMTIPOAVISO" DataSourceID="dsEventos" DataValueField="IDTIPOAVISO">
                                </asp:DropDownList>
                   </div>
                 </div>
                    <div class="col-sm-6">
                    <div class="form-group">
                         <label class="control-label">Porto:</label>
                                <asp:DropDownList ID="ddlPorto" runat="server" CssClass="form-control" Font-Size="11px" AutoPostBack="True" DataTextField="NM_PORTO" DataSourceID="dsPorto" DataValueField="ID_PORTO">
                                </asp:DropDownList>
                   </div>
                 </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div class="form-group">
                            <label class="control-label">Endereços de eMail:</label>
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2" onkeyUp="return CheckMaxCount(this,event,250);"></asp:TextBox>                           
                        </div>
                    </div>
                </div>
        <div class="row">
            <div class="col-sm-12">
                <div class="form-group">
                    <asp:Checkbox ID="ckbReplica" runat="server" CssClass="form-control" text="&nbsp;&nbsp;Desejo replicar estes emails para todos os eventos" ></asp:Checkbox>
                 </div>
              </div>
        </div>

        </ContentTemplate>
    <Triggers>
       <asp:AsyncPostBackTrigger ControlID="ddlEvento"  />
    </Triggers>
</asp:UpdatePanel>
                                 <div class="row">

                                <div class="col-sm-3 col-sm-offset-6">
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <asp:Button ID="btnLimpar" runat="server" CssClass="btn btn-warning btn-block" Text="Limpar Campos"  />
                                    </div>
                                </div>

                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <asp:button  ID="btnGravar" OnClientClick="javascript:return confirm('Deseja realmente gravar essas informações?');"  runat="server" CssClass="btn btn-primary btn-block" Text="Gravar"  />
                                    </div>
                                </div>
                            </div>

       
                            </div>
                       <div class="tab-pane fade" id="lista">
                              <br />
                           <div class="table-responsive tableFixHead">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
    <ContentTemplate>
        <asp:GridView ID="dgvEmailvento" DataKeyNames="Id" DataSourceID="dsEmailvento" EmptyDataText="Esse parceiro não possui email cadastrado."  CssClass="table table-hover table-sm grdViewTable" GridLines="None"  style="max-height:600px; overflow:auto;" AllowSorting="true" OnSorting="dgvEmailvento_Sorting" CellSpacing="-1" runat="server"  AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:BoundField DataField="Id" HeaderText="#" SortExpression="Id" />                                    
                                        <asp:BoundField DataField="NMTIPOAVISO" HeaderText="Tipo de Aviso" SortExpression="NMTIPOAVISO"  />
                                        <asp:BoundField DataField="NM_PORTO" HeaderText="Terminal" SortExpression="NM_PORTO" />
                                        <asp:BoundField DataField="NM_RAZAO" HeaderText="Parceiro" SortExpression="NM_RAZAO"  />
                                        <asp:BoundField DataField="ENDERECOS" HeaderText="Emails" SortExpression="ENDERECOS" />                                        

                                    </Columns>
                                    <HeaderStyle CssClass="headerStyle" />
                                </asp:GridView>
           </ContentTemplate>
    <Triggers>
       <asp:AsyncPostBackTrigger EventName="Sorting" ControlID="dgvEmailvento" />
    </Triggers>
</asp:UpdatePanel>
                            </div>
                        </div>
                       </div>

                     </div>
                            </div>
                
                                                    </div>
                </div>
            </div>
        </div>

<asp:SqlDataSource ID="dsCidades" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand=" SELECT ID_CIDADE,upper( NM_CIDADE)NM_CIDADE FROM [dbo].[TB_CIDADE] #FILTRO union SELECT  0 as Id, 'Selecione' as Descricao FROM [dbo].[TB_CIDADE] Order by ID_CIDADE">
</asp:SqlDataSource>

     <asp:SqlDataSource ID="dsParceiros" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT ID_PARCEIRO as Id, CNPJ , NM_RAZAO RazaoSocial FROM TB_PARCEIRO ORDER BY ID_PARCEIRO">
</asp:SqlDataSource>

 <asp:SqlDataSource ID="dsEmpresas" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT ID_PARCEIRO, NM_FANTASIA FROM [dbo].[TB_PARCEIRO] #FILTRO Order by NM_FANTASIA">
</asp:SqlDataSource>
 <asp:SqlDataSource ID="dsEventos" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT [IDTIPOAVISO],[NMTIPOAVISO] FROM [dbo].[TB_TIPOAVISO] union SELECT 0 , 'Selecione' Order by [IDTIPOAVISO]">
</asp:SqlDataSource>
    <asp:SqlDataSource ID="dsPorto" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="select ID_PORTO, NM_PORTO FROM [dbo].[TB_PORTO] union SELECT  0, 'Selecione' ORDER BY ID_PORTO ">
</asp:SqlDataSource>
          <asp:SqlDataSource ID="dsVendedor" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT ID_PARCEIRO, NM_RAZAO  FROM TB_PARCEIRO WHERE FL_VENDEDOR = 1
union SELECT  0, 'Selecione' ORDER BY ID_PARCEIRO">
</asp:SqlDataSource>  
      <asp:SqlDataSource ID="dsEmailvento" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT A.ID as Id, A.ENDERECOS,A.ID_EVENTO,C.NMTIPOAVISO, A.ID_TERMINAL,B.NM_PORTO, A.ID_PESSOA, D.NM_RAZAO FROM 
[TB_AMR_PESSOA_EVENTO] A
LEFT JOIN TB_PORTO B ON A.ID_TERMINAL = B.ID_PORTO
LEFT  JOIN TB_TIPOAVISO C ON C.IDTIPOAVISO = ID_EVENTO
LEFT JOIN TB_PARCEIRO D ON D.ID_PARCEIRO = ID_PESSOA WHERE ID_PESSOA = @ID">
          <SelectParameters>
          <asp:QueryStringParameter Name="ID" QueryStringField="id" />
        </SelectParameters>
</asp:SqlDataSource>
    
        <asp:SqlDataSource ID="dsAcordoCambio" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="select ID_ACORDO_CAMBIO, NM_ACORDO_CAMBIO FROM [dbo].[TB_ACORDO_CAMBIO] union SELECT  0, 'Selecione' ORDER BY ID_ACORDO_CAMBIO">
</asp:SqlDataSource>

     <asp:SqlDataSource ID="dsTipoFaturamento" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="select ID_TIPO_FATURAMENTO,NM_TIPO_FATURAMENTO FROM [dbo].[TB_TIPO_FATURAMENTO] union SELECT  0, 'Selecione' ORDER BY ID_TIPO_FATURAMENTO">
</asp:SqlDataSource>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">
    <script src="Content/js/viacep.js"></script>
    <script src="Content/js/jquery.dataTables.min.js"></script>


     <script type="text/javascript">
         $(document).ready(function () {
             $(".SPREAD").keyup(function () {
                 var valor = $(".SPREAD").val().replace(/[^0-9-,]+/g, '');
             });
         });

         function nomeFuncao(obj, e) {
             var tecla = (window.event) ? e.keyCode : e.which;
             if (tecla == 8 || tecla == 0)
                 return true;
             if (tecla != 44 && tecla < 48 || tecla > 57)
                 return false;
         }
  
     </script>
</asp:Content>
