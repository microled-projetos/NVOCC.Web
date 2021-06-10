﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="CadastroCotacao.aspx.vb" Inherits="NVOCC.Web.CadastroCotacao" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
       <div class="row principal">
        <div class="col-lg-12 col-md-12 col-sm-12">

            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">COTAÇÃO

                    </h3>
                </div>
                <div class="panel-body">
                    
                    <ul class="nav nav-tabs" role="tablist">
                        <li class="active">
                            <a href="#basico" role="tab" data-toggle="tab">
                                <i class="fa fa-edit" style="padding-right:8px;"></i>Inf. Básicas
                            </a>
                        </li>
                        <li>
                            <a href="#frete" role="tab" data-toggle="tab">
                                <i class="fa fa-edit" style="padding-right:8px;"></i>Frete
                            </a>
                        </li>
                         <li>
                            <a href="#mercadorias" role="tab" data-toggle="tab">
                                <i class="fa fa-edit" style="padding-right:8px;"></i>Embalagem
                            </a>
                        </li>
                              <li>
                            <a href="#taxas" role="tab" data-toggle="tab">
                                <i class="fa fa-edit" style="padding-right:8px;"></i>Taxas
                            </a>
                        </li>
                        <li>
                            <a href="#historico" role="tab" data-toggle="tab">
                                <i class="fa fa-edit" style="padding-right:8px;"></i>Histórico
                            </a>
                        </li>
                    </ul>
                       <asp:button runat="server" style="display:none" id="Button5" CssClass="btn btn-primary" />
                    <div class="tab-content">
                        <div class="tab-pane fade active in" id="basico" >
                            <asp:UpdatePanel ID="updPainel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
    <ContentTemplate>
                                                        <br />      <asp:TextBox ID="txtcnpj" runat="server" CssClass="form-control" style="display:none"></asp:TextBox>

                            <div class="alert alert-success" ID="divsuccess" runat="server" visible="false">
                                        <asp:label ID="lblmsgSuccess" runat="server" Text="Registro cadastrado/atualizado com sucesso!"></asp:label>
                                    </div>
                                    <div class="alert alert-danger" ID="diverro" runat="server" visible="false">
                                        <asp:label ID="lblmsgErro" runat="server"></asp:label>
                                    </div>
                            <br />
                            <div class="row" STYLE="DISPLAY:NONE">
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <label class="control-label">Código:</label>
                                        <asp:TextBox ID="txtID" runat="server" CssClass="form-control"  Enabled="false"></asp:TextBox>
                                    </div>
                                    </div>
                            </div>
                            <div class="row">
                                    
                                 <div class="col-sm-4">
                                     <div class="form-group">
                                        <label class="control-label">Número de Cotação:</label></label><label runat="server" style="color:red" >*</label>
                                        <asp:TextBox ID="txtNumeroCotacao" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                     </div>
                                 <div class="col-sm-4">
                                     <div class="form-group">
                                        <label class="control-label">Data de Abertura:</label></label><label runat="server" style="color:red" >*</label>
                                        <asp:TextBox ID="txtAbertura" runat="server" CssClass="form-control data"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Estufagem:</label></label><label runat="server" style="color:red" >*</label>
                                         <asp:DropDownList ID="ddlEstufagem" runat="server" AutoPostBack="true" CssClass="form-control" Font-Size="11px" DataValueField="ID_TIPO_ESTUFAGEM" DataTextField="NM_TIPO_ESTUFAGEM" DataSourceID="dsEstufagem"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>  
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Usuario Status:</label></label><label runat="server" style="color:red" >*</label>
                                         <asp:DropDownList ID="ddlUsuarioStatus" runat="server" CssClass="form-control" Enabled="False" Font-Size="11px" DataValueField="ID_USUARIO" DataTextField="NOME" DataSourceID="dsUsuario">
                                        </asp:DropDownList>
                                    </div>

                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Status:</label></label><label runat="server" style="color:red" >*</label>
                                         <asp:DropDownList ID="ddlStatusCotacao" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_STATUS_COTACAO" DataSourceID="dsStatusCotacao" DataValueField="ID_STATUS_COTACAO">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                 <div class="col-sm-4">
                                <div class="form-group">
                                        <label class="control-label">Data de Status:</label></label><label runat="server" style="color:red" >*</label>
                                        <asp:TextBox ID="txtDataStatus" runat="server" Enabled="false" CssClass="form-control data"></asp:TextBox>
                                    </div>
                                    </div>
                                    </div>
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Data de Validade:</label></label><label runat="server" style="color:red" >*</label>
                                        <asp:TextBox ID="txtValidade"  runat="server" CssClass="form-control data" MaxLength="100"></asp:TextBox>
                                    </div>
                                </div>
                                    <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Data de Envio:</label></label><label runat="server" style="color:red" >*</label>
                                        <asp:TextBox ID="txtEnvio" enabled="false"  runat="server" CssClass="form-control data" MaxLength="100"></asp:TextBox>
                                    </div>
                                </div>                               
                              
                                    <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Destinatario Comercial:</label></label><label runat="server" style="color:red" >*</label>
                                         <asp:DropDownList ID="ddlDestinatarioComercial" runat="server" CssClass="form-control" Font-Size="11px"   DataTextField="NM_DESTINATARIO_COMERCIAL" DataSourceID="dsDestinatarioComercial" DataValueField="ID_DESTINATARIO_COMERCIAL">
                                           
                                        </asp:DropDownList>
                                    </div>

                                </div>
                           </div>
                            <div class="row">
                                   <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Agente Internacional:</label></label><label runat="server" style="color:red" >*</label>
                                      <asp:DropDownList ID="ddlAgente" runat="server" CssClass="form-control" Font-Size="11px"  DataTextField="NM_RAZAO" DataSourceID="dsAgente" DataValueField="ID_PARCEIRO"></asp:DropDownList>              </div>
                                    </div>
                              
                                   <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Incoterm:</label></label><label runat="server" style="color:red" >*</label>
                                       <asp:DropDownList ID="ddlIncoterm" runat="server" CssClass="form-control" Font-Size="11px"  DataTextField="NM_INCOTERM" DataSourceID="dsIncoterm" DataValueField="ID_INCOTERM"></asp:DropDownList>              </div>
                                    </div>
                       
                                  
                               <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Analista:</label></label><label runat="server" style="color:red" >*</label>
                                         <asp:DropDownList ID="ddlAnalista" runat="server" CssClass="form-control" Enabled="false" Font-Size="11px" DataValueField="ID_USUARIO" DataTextField="NOME" DataSourceID="dsUsuario">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                
                                </div>                     
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Cliente:</label></label><label runat="server" style="color:red" >*</label>
                                       <asp:DropDownList ID="ddlCliente" runat="server" CssClass="form-control" autopostback="true" Font-Size="11px"  DataValueField="ID_PARCEIRO" DataTextField="NM_RAZAO" DataSourceID="dsCliente" >
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Contato:</label></label><label runat="server" style="color:red" >*</label>
                                        <asp:DropDownList ID="ddlContato" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_CONTATO" DataSourceID="dsContato" DataValueField="ID_CONTATO">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                  <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Serviço:</label></label><label runat="server" style="color:red" >*</label>
                                       <asp:DropDownList ID="ddlServico" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_SERVICO" DataSourceID="dsServico" DataValueField="ID_SERVICO">
                                                </asp:DropDownList>           
                                    </div>
                                </div>
                                </div>  
                            <div class="row">
                            <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Tipo BL:</label></label><label runat="server" style="color:red" >*</label>
                                         <asp:DropDownList ID="ddlTipoBL" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_TIPO_BL" DataSourceID="dsBL" DataValueField="ID_TIPO_BL">
                                        </asp:DropDownList>
                                    </div>

                                </div>
                                <div class="col-sm-4" >
                                    <div class="form-group">
                                        <label class="control-label">Número Processo:</label>
                                        <asp:TextBox ID="txtProcessoCotacao" runat="server" Enabled="false" CssClass="form-control" MaxLength="18"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-4" id="divClienteFinal" runat="server">
                                    <div class="form-group">
                                        <label class="control-label">Cliente Final:</label></label>
                                         <asp:DropDownList ID="ddlClienteFinal" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_CLIENTE_FINAL" DataSourceID="dsClienteFinal" DataValueField="ID_CLIENTE_FINAL">
                                        </asp:DropDownList>
                                    </div>
                            </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4"  >
                                    <div class="form-group">
                                        <label class="control-label">Vendedor:</label></label><label runat="server" style="color:red" >*</label>
                                       <asp:DropDownList ID="ddlVendedor" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_RAZAO" DataSourceID="dsVendedor"  DataValueField="ID_PARCEIRO">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                             
                                 <div class="col-sm-4" >
                                    <div class="form-group">
                                        <label class="control-label">Data de Calculo:</label>
                                        <asp:TextBox ID="txtDataCalculo" runat="server" Enabled="false" CssClass="form-control data" MaxLength="18"></asp:TextBox>
                                    </div>
                                </div>
                                 <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Motivo Cancelamento:</label>
                                         <asp:DropDownList ID="ddlMotivoCancelamento" runat="server" CssClass="form-control" Font-Size="11px"  DataTextField="NM_MOTIVO_CANCELAMENTO" DataSourceID="dsMotivoCancelamento" DataValueField="ID_MOTIVO_CANCELAMENTO">
                                        </asp:DropDownList>
                                    </div>  

                                </div>
                            </div>
                            <div class="row">
                                 <div class="col-sm-4" >
                                    <div class="form-group">
                                        <label class="control-label">Obs Cliente:</label>
                                        <asp:TextBox ID="txtObsCliente" runat="server" TextMode="MultiLine" Rows="2" CssClass="form-control" MaxLength="1000"></asp:TextBox>
                                    </div>
                                </div>
                                 <div class="col-sm-4" >
                                    <div class="form-group">
                                        <label class="control-label">Obs Operacional:</label>
                                        <asp:TextBox ID="txtObsOperacional" runat="server" TextMode="MultiLine" Rows="2" CssClass="form-control" MaxLength="1000"></asp:TextBox>
                                    </div>
                                </div>
                                
                                <div class="col-sm-4" >
                                    <div class="form-group">
                                        <label class="control-label">Obs Motivo Cancelamento:</label>
                                        <asp:TextBox ID="txtObsCancelamento" runat="server" TextMode="MultiLine" Rows="2" CssClass="form-control" MaxLength="1000"></asp:TextBox>
                                    </div>
                                </div>
                                </div>                                          
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
                                        <asp:Button ID="btnGravar" runat="server" CssClass="btn btn-primary btn-block" Text="Gravar"  />
                                    </div>
                                </div>
                            </div>


                             </ContentTemplate>

    <Triggers>
        <ASP:PostBackTrigger ControlID="btnGravar" />
                <asp:AsyncPostBackTrigger  ControlID="ddlCliente" />
    </Triggers>
</asp:UpdatePanel>

                        </div>

                        <div class="tab-pane fade" id="frete" >
                             <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
    <ContentTemplate>  
        <ajaxToolkit:ModalPopupExtender id="mpeNovoFrete" runat="server" PopupControlID="Panel2" TargetControlID="btnNovoFrete"></ajaxToolkit:ModalPopupExtender>   
            <asp:Panel ID="Panel2" runat="server" CssClass="modalPopup" style="display:none" >     
                 <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
    <ContentTemplate> 
                                                <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title" id="modalFrete"> FRETE </h5>
                                                        </div>
                                                        <div class="modal-body">                                                           
                                    <div class="alert alert-success" ID="divSuccessFrete" runat="server" visible="false">
                                        <asp:label ID="lblmsgSuccessNovo" runat="server" Text="Registro cadastrado/atualizado com sucesso!"></asp:label>
                                    </div>
                                    <div class="alert alert-danger" ID="divErroFrete" runat="server" visible="false">
                                        <asp:label ID="lblErroFrete" runat="server"></asp:label>
                                    </div>

                                    <div id="detalhes" class="tab-pane fade active in">
                                        <div class="row">
                                   <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Transportador:</label><label runat="server" style="color:red" >*</label>
                                        <asp:DropDownList ID="ddlTransportadorFrete" runat="server"  CssClass="form-control" Font-Size="11px"  DataTextField="NM_RAZAO" DataSourceID="dsTransportador" DataValueField="ID_PARCEIRO"></asp:DropDownList>
                                    </div>
                                </div>
                               <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Porto de Origem:</label><label runat="server" style="color:red" >*</label>
                                         <asp:DropDownList ID="ddlOrigemFrete" runat="server"  CssClass="form-control" Font-Size="11px" DataTextField="NM_PORTO" DataSourceID="dsPorto" DataValueField="ID_PORTO">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Porto de Destino:</label><label runat="server" style="color:red" >*</label>
                                         <asp:DropDownList ID="ddlDestinoFrete" runat="server" AutoPostBack="true" CssClass="form-control" Font-Size="11px" DataTextField="NM_PORTO" DataSourceID="dsPorto" DataValueField="ID_PORTO">
                                        </asp:DropDownList>
                                    </div>
                                </div></div>                                         
                                          <div class="row">
                               <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Tabela de Frete:</label>
                                         <asp:DropDownList ID="ddlFreteTransportador_Frete" AutoPostBack="true" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="Descricao" DataSourceID="dsFreteTransportador" DataValueField="ID_FRETE_TRANSPORTADOR" >
                                        </asp:DropDownList>
                                    </div> 
                               </div>    
                                 <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Tipo de Carga:</label>
                                       <asp:DropDownList ID="ddlTipoCargaFrete" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_TIPO_CARGA" DataSourceID="dsCarga" DataValueField="ID_TIPO_CARGA" >
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Via Rota:</label>
                                         <asp:DropDownList ID="ddlRotaFrete" runat="server" AutoPostBack="true" CssClass="form-control" Font-Size="11px" DataTextField="NM_VIA_ROTA" DataSourceID="dsRota" DataValueField="ID_VIA_ROTA" >
                                        </asp:DropDownList> 
                                    </div>

                                </div>

                                         </div>  
                                        <div class="row" id="divEscala" runat="server" >
                                  <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Porto de Escala I:</label>
                                        <asp:DropDownList ID="ddlEscala1Frete" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_PORTO" DataSourceID="dsPorto" DataValueField="ID_PORTO">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                    <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Porto de Escala II:</label>
                                        <asp:DropDownList ID="ddlEscala2Frete" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_PORTO" DataSourceID="dsPorto" DataValueField="ID_PORTO">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                    <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Porto de Escala III:</label>
                                        <asp:DropDownList ID="ddlEscala3Frete" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_PORTO" DataSourceID="dsPorto" DataValueField="ID_PORTO">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                              </div>
                                        <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Transttime Inicial:</label>
                                        <asp:TextBox ID="txtTTimeFreteInicial"   runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Transttime Final:</label>
                                        <asp:TextBox ID="txtTTimeFreteFinal" autopostback="true"  runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                    </div>
                                </div>
                                            <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Transttime Média:</label>
                                        <asp:TextBox ID="txtTTimeFreteMedia" enabled="false"  runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                    </div>
                                </div>
                                            </div>
                                         
                                        <div class="row">
                                  <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Frequencia:</label>
                                          <asp:DropDownList ID="ddlFrequenciaFrete" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_TIPO_FREQUENCIA" DataSourceID="dsFrequencia" DataValueField="ID_TIPO_FREQUENCIA">
                                        </asp:DropDownList>
                                    </div>

                                </div>
                                  <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Valor Frequencia:</label>
                                        <asp:TextBox ID="txtValorFrequenciaFrete" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                    </div>
                                </div>
                                            
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Valor Peso Taxado:</label>
                                        <asp:TextBox ID="txtPesoTaxadoFrete" Enabled="false" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                    </div>
                                </div>
                                  
                              </div>  
                                        
                                
                                        <div class="row">
                                            <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Estufagem:</label>
                                         <asp:DropDownList ID="ddlEstufagemFrete" runat="server" CssClass="form-control" Font-Size="11px" DataValueField="ID_TIPO_ESTUFAGEM" DataTextField="NM_TIPO_ESTUFAGEM" DataSourceID="dsEstufagem">
                                        </asp:DropDownList>
                                    </div>

                                </div> 
                                        <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Tipo Divisão Profit:</label>
                                         <asp:DropDownList ID="ddlDivisaoProfit" runat="server" CssClass="form-control" Font-Size="11px" DataValueField="ID_TIPO_DIVISAO_PROFIT" DataTextField="NM_TIPO_DIVISAO_PROFIT" DataSourceID="dsDivisaoProfit">
                                        </asp:DropDownList>
                                    </div>

                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Valor Divisão Profit:</label>
                                        <asp:TextBox ID="txtValorDivisaoProfit"   runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                    </div>
                                </div>
                                </div>
                                 <div class="row">
                                            <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Moeda Frete:</label>
                                         <asp:DropDownList ID="ddlMoedaFrete" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_MOEDA" DataSourceID="dsMoeda" DataValueField="ID_MOEDA" >
                                        </asp:DropDownList> 
                                    </div>

                                </div>
                                  <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Valor Frete (Compra):</label>
                                        <asp:TextBox ID="txtFreteCompra" runat="server" enabled="false" CssClass="form-control moeda" MaxLength="100"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Valor Frete (Venda):</label>
                                        <asp:TextBox ID="txtFreteVenda" runat="server" enabled="false" CssClass="form-control moeda" MaxLength="100"></asp:TextBox>
                                    </div>
                                </div>
                                      
                                            </div>
                                        <div class="row" id="divMinimosFCL" runat="server">                                            
                                  <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Valor Frete (Compra Mínina):</label>
                                        <asp:TextBox ID="txtCompraMinimaFCL" runat="server" CssClass="form-control moeda" MaxLength="100"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Valor Frete (Venda Mínina):</label>
                                        <asp:TextBox ID="txtVendaMinimaFCL" runat="server" CssClass="form-control moeda" MaxLength="100"></asp:TextBox>
                                    </div>
                                </div>                                     
                           </div>
                   <div class="row">
                                <div class="col-sm-12">
                                    <div class="form-group">
                                        <label class="control-label">Taxas Included:</label>
                                        <asp:TextBox ID="txtIncludedFrete" runat="server" TextMode="MultiLine" Rows="2" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div></div>
                                    </div>
                               </div>
                               <div class="modal-footer">
                                                            <asp:Button runat="server" Text="Salvar" id="btnSalvarFrete" CssClass="btn btn-success" />
                                                            <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFecharFrete" text="Close" />
                                                        </div>
                                                    
                                                </div>
      
                                       </div>            
   </ContentTemplate>
<Triggers>
<%--            <asp:AsyncPostBackTrigger  ControlID="btnFecharFrete" />--%>
            <asp:AsyncPostBackTrigger  ControlID="btnSalvarFrete" />
            <asp:AsyncPostBackTrigger  ControlID="ddlDestinoFrete" />
            <asp:AsyncPostBackTrigger  ControlID="ddlFreteTransportador_Frete" />
            <asp:AsyncPostBackTrigger  ControlID="ddlRotaFrete" />
       <asp:AsyncPostBackTrigger  ControlID="txtTTimeFreteFinal" />
     </Triggers>  
     </asp:UpdatePanel>
             </asp:Panel>
          <br/>
                           <div class="row">
                                     
                                  <div class="col-sm-4"">
                                                    <div class="form-group">
                                                        <asp:button runat="server" Text="Novo Frete" id="btnNovoFrete" CssClass="btn btn-primary" />
                                                    </div>
                                                </div>
                            </div>
        <br />



                                     <div class="tab-content">

                                <br />
                              <div class="alert alert-success" id="divCadErroFrete" runat="server" visible="false">                                       
                      <asp:label ID="lblErrocadFrete" runat="server" /> 
                  </div>
         <div class="alert alert-danger" id="divDeleteErroFrete" runat="server" visible="false">                                       
                      <asp:label ID="lblDeleteErroFrete" runat="server"  /> 
                  </div>
                               <br />
                            <div class="table-responsive">
                                <asp:GridView ID="dgvFrete" DataKeyNames="ID_COTACAO" DataSourceID="dsCotacao" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server"  AutoGenerateColumns="false"   style="max-height:400px; overflow:auto;" AllowSorting="true" OnSorting="dgvFrete_Sorting" EmptyDataText="Nenhum registro encontrado.">
                                    <Columns>
                                        <asp:BoundField DataField="ID_COTACAO" HeaderText="#"  VISIBLE="false" SortExpression="ID_TABELA_FRETE_TAXA"/>
                                        <asp:BoundField DataField="NR_COTACAO" HeaderText="Nº Cotação" SortExpression="NR_COTACAO"/>
                                        <asp:BoundField DataField="Origem" HeaderText="Origem"  SortExpression="Origem"/>
                                        <asp:BoundField DataField="Destino" HeaderText="Destino"  SortExpression="Destino" />
                                        <asp:BoundField DataField="CLIENTE_FINAL" HeaderText="Cliente Final" SortExpression="CLIENTE_FINAL" />
                                        
                                           <asp:TemplateField HeaderText="" >
                                         <ItemTemplate>                          
                            <asp:LinkButton ID="btnVisualizar" runat="server" CausesValidation="False" CommandName="visualizar" CommandArgument='<%# Eval("ID_COTACAO") %>'  
                                Text="Visualizar" title="Editar"  CssClass="btn btn-info btn-sm" ><span class="glyphicon glyphicon-edit"  style="font-size:medium"></span></asp:LinkButton>
                                   </ItemTemplate>   
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle CssClass="headerStyle" />
                                </asp:GridView>
                            </div>
                            
                                                    </div>

                           
 </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="dgvFrete" />
<%--        <asp:PostBackTrigger ControlID="btnGravar" />--%>
        <asp:AsyncPostBackTrigger ControlID="btnFecharFrete" />
              <%--  <asp:PostBackTrigger ControlID="btnFecharFrete" />--%>

        <asp:AsyncPostBackTrigger ControlID="btnSalvarFrete" />

        
    </Triggers>
</asp:UpdatePanel>
                  </div>

                        <div class="tab-pane fade" id="mercadorias" >
        
                                        <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
    <ContentTemplate>              
         <ajaxToolkit:ModalPopupExtender id="mpeNovoMercadoria" runat="server" PopupControlID="Panel1" TargetControlID="btnNovaMercadoria" ></ajaxToolkit:ModalPopupExtender>
     
   <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" style="display:none" >     
         <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
    <ContentTemplate> 
                                                <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title" id="modalMercaoriaNova">Mercadoria</h5>
                                                        </div>
                                                        <div class="modal-body">    
                                    <div class="alert alert-success" ID="divSuccessMercadoria" runat="server" visible="false">
                                        <asp:label ID="lblSuccessMercadoria" runat="server" Text="Registro cadastrado/atualizado com sucesso!"></asp:label>
                                    </div>
                                    <div class="alert alert-danger" ID="divErroMercadoria" runat="server" visible="false">
                                        <asp:label ID="lblErroMercadoria" runat="server"></asp:label>
                                    </div>

                                   <div class="row">
                                <div class="col-sm-3"  style="display:none">
                                    <div class="form-group">
                                        <label class="control-label">Código:</label>
                                        <asp:TextBox ID="txtIDMercadoria" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                    </div>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <label class="control-label">Cotação:</label>
                                        <asp:TextBox ID="txtCotacaoMercadoria" runat="server" enabled="false" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    </div>
                                      </div> 
                                    <div class="row">
                                     <div class="col-sm-3">
                                    <div class="form-group">
                                        <label class="control-label">Mercadoria:</label><label runat="server" style="color:red" >*</label>
                                         <asp:DropDownList ID="ddlMercadoria" runat="server" CssClass="form-control" Font-Size="11px"  DataTextField="NM_MERCADORIA" DataSourceID="dsMercadoria" DataValueField="ID_MERCADORIA">
                                        </asp:DropDownList>
                                    </div>
                                </div> 
                                     <div class="col-sm-3">
                                    <div class="form-group">
                                        <label class="control-label">Tipo de Container:</label><asp:label Visible="false" runat="server" ID="RedContainer" style="color:red" >*</asp:label>
                                         <asp:DropDownList ID="ddlTipoContainerMercadoria" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_TIPO_CONTAINER" DataSourceID="dsContainer" DataValueField="ID_TIPO_CONTAINER">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                      
                                        <div class="col-sm-3">
                                    <div class="form-group">
                                        <label class="control-label">Quantidade Container:</label><asp:label Visible="false" runat="server" ID="RedQTDContainer" style="color:red" >*</asp:label>
                                         <asp:TextBox ID="txtQtdContainerMercadoria" AutoPostBack="true" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                    </div>
                                </div>
                                        <div class="col-sm-3" id="divQtdMercadoria" runat="server">
                                    <div class="form-group">
                                        <label class="control-label">Qtd. Mercadoria:</label><asp:label Visible="false" runat="server" ID="RedQTDMercadoria" style="color:red" >*</asp:label>
                                           <asp:TextBox ID="txtQtdMercadoria" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                         <div class="col-sm-3" id="DivFreetime" runat="server" >
                                    <div class="form-group">
                                        <label class="control-label">Freetime:</label><asp:label Visible="false" runat="server" ID="RedFree" style="color:red" >*</asp:label>
                                        <asp:TextBox ID="txtFreeTimeMercadoria"   runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                             <div class="row">
                                  <div class="col-sm-3">
                                    <div class="form-group">
                                        <label class="control-label">Valor Frete(Compra):</label>
                                        <asp:TextBox ID="txtFreteCompraMercadoria"   runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                    </div>
                                </div>
                                 <div class="col-sm-3" runat="server" id="divCompraMinimaLCL">
                                    <div class="form-group">
                                        <label class="control-label">Valor Frete (Compra Mínina):</label>
                                        <asp:TextBox ID="txtFreteCompraMinima" runat="server" CssClass="form-control moeda" MaxLength="100"></asp:TextBox>
                                    </div>
                                </div>     
                                 <div class="col-sm-3">
                                    <div class="form-group">
                                        <label class="control-label">Valor Frete(Venda):</label>
                                        <asp:TextBox ID="txtFreteVendaMercadoria"   runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                    </div>
                                </div>
                                  <div class="col-sm-3" runat="server" id="divVendaMinimaLCL">
                                    <div class="form-group">
                                        <label class="control-label">Valor Frete (Venda Mínina):</label>
                                        <asp:TextBox ID="txtFreteVendaMinima" runat="server" CssClass="form-control moeda" MaxLength="100"></asp:TextBox>
                                    </div>
                                </div>
                          </div> 
                                                            <div class="row">
                                 <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Valor Peso(Bruto):</label><asp:label Visible="false" runat="server" ID="RedPesoBruto" style="color:red" >*</asp:label>
                                        <asp:TextBox ID="txtPesoBrutoMercadoria"   runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                    </div>
                                </div>
                                  
                                  <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Valor M3:</label><asp:label Visible="false" runat="server" ID="RedM3" style="color:red" >*</asp:label>
                                        <asp:TextBox ID="txtM3Mercadoria"   runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                    </div>
                                </div>
                                                                   <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Valor da Carga:</label>
                                        <asp:TextBox ID="txtValorCargaMercadoria"   runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                    </div>
                                </div>
                                </div>
                           
                           
                                 <div class="row">                              
                                  <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Comprimento:</label>
                                        <asp:TextBox ID="txtComprimentoMercadoria"   runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                    </div>
                                </div>                                
                                     <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Largura:</label>
                                        <asp:TextBox ID="txtLarguraMercadoria"   runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                    </div>
                                </div>
                                     <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Altura:</label>
                                        <asp:TextBox ID="txtAlturaMercadoria"   runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                    </div>
                                </div>

                                </div>
                       
                                  
                           
                            <div class="row">
                                 <div class="col-sm-12" >
                                    <div class="form-group">
                                        <label class="control-label">DS Mercadoria:</label>
                                        <asp:TextBox ID="txtDsMercadoria" runat="server" TextMode="MultiLine" Rows="2" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                 
                                </div>
                           
                      
                                                       
                                                        </div>                     
                               <div class="modal-footer">
                                                            <asp:Button runat="server" Text="Salvar" id="btnSalvarMercadoria" CssClass="btn btn-success" />
                                                            <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFecharMercadoria" text="Close" />
                                                        </div>
                                                    
                                                </div>
      
                                       </div>            
   </ContentTemplate>
<Triggers>
                <asp:AsyncPostBackTrigger  ControlID="ddlEstufagem" />
    
                    <asp:AsyncPostBackTrigger  ControlID="txtQtdContainerMercadoria" />

            <asp:AsyncPostBackTrigger  ControlID="btnSalvarMercadoria" />
                 <asp:AsyncPostBackTrigger  ControlID="btnFecharMercadoria" />
     </Triggers>  
     </asp:UpdatePanel>
     </asp:Panel>



      <br/>
                          <div class="row">
                                     
                                  <div class="col-sm-4"">
                                                    <div class="form-group">
                                                        <asp:button runat="server" Text="Nova Mercadoria" id="btnNovaMercadoria" CssClass="btn btn-primary" />
                                                    </div>
                                                </div>
                            </div>
                                                <div class="tab-content">

                                <br />
                              <div class="alert alert-success" id="divDeleteMercadoria" runat="server" visible="false">                                       
                      <asp:label ID="lblDeleteMercadoria" runat="server"  /> 
                  </div>
         <div class="alert alert-danger" id="divDeleteErroMercadoria" runat="server" visible="false">                                       
                      <asp:label ID="lblDeleteErroMercadoria" runat="server"  /> 
                  </div>
                               <br />
                            <div class="table-responsive">
                                <asp:GridView ID="dgvMercadoria" DataKeyNames="ID_COTACAO_MERCADORIA" DataSourceID="dsMercadorias" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server"  AutoGenerateColumns="false"   style="max-height:400px; overflow:auto;" AllowSorting="true" OnSorting="dgvMercadoria_Sorting" EmptyDataText="Nenhum registro encontrado.">
                                    <Columns>
                                        <asp:BoundField DataField="ID_COTACAO_MERCADORIA" HeaderText="#"  SortExpression="ID_COTACAO_MERCADORIA" Visible="false" />
                                        <asp:BoundField DataField="DS_MERCADORIA" HeaderText="Descrição"  SortExpression="DS_MERCADORIA"/>
                                        <asp:BoundField DataField="NM_TIPO_CONTAINER" HeaderText="Container"  SortExpression="NM_TIPO_CONTAINER" />
                                        <asp:BoundField DataField="QT_CONTAINER" HeaderText="Qtd. Container"   SortExpression="QT_CONTAINER"/>   
                                        <asp:BoundField DataField="VL_PESO_BRUTO" HeaderText="Peso Bruto"  SortExpression="VL_PESO_BRUTO"  />
                                        <asp:BoundField DataField="QT_DIAS_FREETIME" HeaderText="FreeTime" SortExpression="QT_DIAS_FREETIME" />      
                                           <asp:TemplateField HeaderText="" >
                                         <ItemTemplate>                          
                            <asp:LinkButton ID="btnVisualizar" runat="server" CausesValidation="False" CommandName="visualizar" CommandArgument='<%# Eval("ID_COTACAO_MERCADORIA") %>'  
                                Text="Visualizar" title="Editar"  CssClass="btn btn-info btn-sm" ><span class="glyphicon glyphicon-edit"  style="font-size:medium"></span></asp:LinkButton>
                                   </ItemTemplate>   
                                                  <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>                    
                                   <ItemTemplate>                          
                            <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="False" CommandName="Excluir"  CommandArgument='<%# Eval("ID_COTACAO_MERCADORIA") %>' 
                                Text="Excluir" OnClientClick="return confirm('Tem certeza que deseja excluir esse registro?')"  CssClass="btn btn-danger btn-sm" ><span class="glyphicon glyphicon-trash"  style="font-size:medium"></span></asp:LinkButton>
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
<asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="dgvMercadoria" />
    <asp:AsyncPostBackTrigger  ControlID="btnFecharMercadoria" />
                            <asp:AsyncPostBackTrigger ControlID="btnSalvarMercadoria" />

     </Triggers>  
     </asp:UpdatePanel>


                        </div>

                        <div class="tab-pane fade" id="taxas" >

                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
    <ContentTemplate>              
         <ajaxToolkit:ModalPopupExtender id="mpeNovoTaxa" runat="server" PopupControlID="Panel3" TargetControlID="btnNovaTaxa"  ></ajaxToolkit:ModalPopupExtender>
                           
             <asp:Panel ID="Panel3" runat="server" CssClass="modalPopup" style="display:none" >     
         <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
    <ContentTemplate> 
                                                <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title" id="modalTaxaNova">TAXA</h5>
                                                        </div>
                                                        <div class="modal-body">       
                                                             <div class="alert alert-success" ID="divSuccessTaxa" runat="server" visible="false">
                                        <asp:label ID="lblsuccessTaxa" runat="server" Text="Registro cadastrado/atualizado com sucesso!"></asp:label>
                                    </div>
                                    <div class="alert alert-danger" ID="divErroTaxa" runat="server" visible="false">
                                        <asp:label ID="lblErroTaxa" runat="server"></asp:label>
                                    </div>
                                      <div class="row">
                                <div class="col-sm-3" style="display:none">
                                    <div class="form-group">
                                        <label class="control-label">Código:</label>
                                        <asp:TextBox ID="txtIDTaxa" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                    </div>
                               <div class="col-sm-3">
                                    <div class="form-group">
                                        <label class="control-label">Cotação:</label><label runat="server" style="color:red" >*</label>
                                        <asp:TextBox ID="txtCotacaoTaxa" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>
                                          
                              <div class="col-sm-3">
                                    <div class="form-group">
                                          <label class="control-label"></label>
                                        <asp:Checkbox ID="ckbDeclaradoTaxa" runat="server" CssClass="form-control" text="&nbsp;&nbsp;Declarado" ></asp:Checkbox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                          <label class="control-label"></label>
                                        <asp:Checkbox ID="ckbProfitTaxa" runat="server" CssClass="form-control" text="&nbsp;&nbsp;Divisão PROFIT" ></asp:Checkbox>
                                    </div>
                                </div>          </div><div class="row">           
                                  <div class="col-sm-3">
                                    <div class="form-group">
                                        <label class="control-label">Destinatário Cobrança:</label><label runat="server" style="color:red" >*</label>
                                         <asp:DropDownList ID="ddlDestinatarioCobrancaTaxa" runat="server" CssClass="form-control" Font-Size="11px"  DataTextField="NM_DESTINATARIO_COBRANCA" DataSourceID="dsDestinatarioCobranca" DataValueField="ID_DESTINATARIO_COBRANCA" >
                                        </asp:DropDownList>
                                    </div>

                           </div>
                           
                                  <div class="col-sm-3">
                                    <div class="form-group">
                                        <label class="control-label">Base de Cálculo:</label><label runat="server" style="color:red" >*</label>
                                         <asp:DropDownList ID="ddlBaseCalculoTaxa" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_BASE_CALCULO_TAXA" DataSourceID="dsBaseCalculo" DataValueField="ID_BASE_CALCULO_TAXA" >
                                        </asp:DropDownList>
                                    </div>
                                </div></div>
                                 <div class="row">                             
                                     <div class="col-sm-3">
                                    <div class="form-group">
                                        <label class="control-label">Tipo de Item(Despesa):</label><label runat="server" style="color:red" >*</label>
                                         <asp:DropDownList ID="ddlItemDespesaTaxa" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_ITEM_DESPESA" DataSourceID="dsItemDespesa" DataValueField="ID_ITEM_DESPESA"  >
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                     <div class="col-sm-3">
                                    <div class="form-group">
                                        <label class="control-label">Tipo de pagamento:</label><label runat="server" style="color:red" >*</label>
                                         <asp:DropDownList ID="ddlTipoPagamentoTaxa" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_TIPO_PAGAMENTO" DataSourceID="dsTipoPagamento" DataValueField="ID_TIPO_PAGAMENTO" >
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                      <div class="col-sm-3">
                                    <div class="form-group">
                                        <label class="control-label">Origem pagamento:</label><label runat="server" style="color:red" >*</label>
                                         <asp:DropDownList ID="ddlOrigemPagamentoTaxa" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_ORIGEM_PAGAMENTO" DataSourceID="dsOrigemPagamento" DataValueField="ID_ORIGEM_PAGAMENTO">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                               
                               <div class="row">                      

                                  <div class="col-sm-3">
                                    <div class="form-group">
                                        <label class="control-label">Moeda Compra:</label><asp:label id="RedMoedaCompra" runat="server" style="color:red" >*</asp:label>
                                         <asp:DropDownList ID="ddlMoedaCompraTaxa" runat="server" CssClass="form-control" Font-Size="11px"  DataTextField="NM_MOEDA" DataSourceID="dsMoeda" DataValueField="ID_MOEDA" >
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                  
                                  <div class="col-sm-3">
                                    <div class="form-group">
                                        <label class="control-label">Valor Taxa(Compra):</label><asp:label id="RedValorTaxaCompra" runat="server" style="color:red" >*</asp:label>
                                        <asp:TextBox ID="txtValorTaxaCompra" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                    </div>
                                </div>
                                   <div class="col-sm-3">
                                    <div class="form-group">
                                        <label class="control-label">Valor Taxa Compra Minima:</label>
                                        <asp:TextBox ID="txtValorTaxaCompraMin" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                    </div>
                                </div>
                                   <div class="col-sm-3">
                                    <div class="form-group">
                                        <label class="control-label">Valor Taxa de Compra Calculado:</label>
                                        <asp:TextBox ID="txtValorTaxaCompraCalc" Enabled="false" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                    </div>
                                </div>
                                   </div>
                              <div class="row">
                                 <div class="col-sm-3">
                                    <div class="form-group">
                                        <label class="control-label">Moeda Venda:</label><label runat="server" style="color:red" >*</label>
                                         <asp:DropDownList ID="ddlMoedaVendaTaxa" runat="server" CssClass="form-control" Font-Size="11px"  DataTextField="NM_MOEDA" DataSourceID="dsMoeda" DataValueField="ID_MOEDA" >
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                  
                                  <div class="col-sm-3">
                                    <div class="form-group">
                                        <label class="control-label">Valor Taxa(Venda):</label><label runat="server" style="color:red" >*</label>
                                        <asp:TextBox ID="txtValorTaxaVenda"   runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                    </div>
                                </div>
                            
                                  <div class="col-sm-3">
                                    <div class="form-group">
                                        <label class="control-label">Valor Taxa Venda Minima:</label>
                                        <asp:TextBox ID="txtValorTaxaVendaMin"   runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                    </div>
                                </div>
                                
                                     <div class="col-sm-3">
                                    <div class="form-group">
                                        <label class="control-label">Valor Taxa de Venda Calculado:</label>
                                        <asp:TextBox ID="txtValorTaxaVendaCalc" Enabled="false"  runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                    </div>
                                </div>
                                </div>

                               
                           
                            <div class="row">
                                 <div class="col-sm-12" >
                                    <div class="form-group">
                                        <label class="control-label">Obs Taxas:</label>
                                        <asp:TextBox ID="txtObsTaxa" runat="server" TextMode="MultiLine" Rows="2" CssClass="form-control cpf" MaxLength="18"></asp:TextBox>
                                    </div>
                                </div>
                                 
                                </div>
                                                       
                                                        </div>                     
                               <div class="modal-footer">
                                                            <asp:Button runat="server" Text="Salvar Taxa" id="btnSalvarTaxa" CssClass="btn btn-success" />
                                                            <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFecharTaxa" text="Close" />
                                                        </div>
                                                    
                                                </div>
      
                                       </div>            
   </ContentTemplate>
<Triggers>
            <asp:AsyncPostBackTrigger  ControlID="btnSalvarTaxa" />
                 <asp:AsyncPostBackTrigger  ControlID="btnFecharTaxa" />

     </Triggers>  
     </asp:UpdatePanel>
     </asp:Panel>
                                 
        
        
        <br/>
                          <div class="row">
                                     
                                  <div class="col-sm-4"">
                                                    <div class="form-group">
                                                        <asp:button runat="server" Text="Nova Taxa" id="btnNovaTaxa" CssClass="btn btn-primary" />
                                                    </div>
                                                </div>
                            </div>
                                                <div class="tab-content">

                                <br />
                              <div class="alert alert-success" id="divDeleteTaxas" runat="server" visible="false">                                       
                      <asp:label ID="lblDeleteTaxas" runat="server"  /> 
                  </div>
         <div class="alert alert-danger" id="divDeleteErroTaxas" runat="server" visible="false">                                       
                      <asp:label ID="lblDeleteErroTaxas" runat="server"  /> 
                  </div>
                               <br />
                            <div class="table-responsive">
                                <asp:GridView ID="dgvTaxas" DataKeyNames="ID_COTACAO_TAXA" DataSourceID="dsTaxas" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server"  AutoGenerateColumns="false"   style="max-height:400px; overflow:auto;" AllowSorting="true" OnSorting="dgvTaxas_Sorting" EmptyDataText="Nenhum registro encontrado.">
                                    <Columns>
                                        <asp:BoundField DataField="ID_COTACAO_TAXA" HeaderText="#"  SortExpression="ID_COTACAO_TAXA"  Visible="false" />
<%--                                        <asp:BoundField DataField="ID_COTACAO" HeaderText="ID_COTACAO"  SortExpression="ID_COTACAO"/>--%>
                                        <asp:BoundField DataField="NM_TIPO_ITEM_DESPESA" HeaderText="Item Despesa"  SortExpression="NM_TIPO_ITEM_DESPESA"/>
                                        <asp:BoundField DataField="NM_ORIGEM_PAGAMENTO" HeaderText="Origem de Pagamento"  SortExpression="NM_ORIGEM_PAGAMENTO" />
                                        <asp:BoundField DataField="DECLARADO" HeaderText="Declarado"   SortExpression="DECLARADO"/>
                                        <asp:BoundField DataField="NM_BASE_CALCULO_TAXA" HeaderText="Base de Cálc."  SortExpression="NM_BASE_CALCULO_TAXA" />
                                        <asp:BoundField DataField="NM_MOEDA_COMPRA" HeaderText="Moeda de Compra"  SortExpression="NM_MOEDA_COMPRA" />
                                        <asp:BoundField DataField="VL_TAXA_COMPRA" HeaderText="Valor de Compra"  SortExpression="VL_TAXA_COMPRA"  />
                                        <asp:BoundField DataField="NM_MOEDA_VENDA" HeaderText="Moeda de Venda"  SortExpression="NM_MOEDA_VENDA" />
                                        <asp:BoundField DataField="VL_TAXA_VENDA" HeaderText="Valor de Venda" SortExpression="VL_TAXA_VENDA"  />      
                                           <asp:TemplateField HeaderText="" >
                                         <ItemTemplate>                          
                            <asp:LinkButton ID="btnVisualizar" runat="server" CausesValidation="False" CommandName="visualizar" CommandArgument='<%# Eval("ID_COTACAO_TAXA") %>'  
                                Text="Visualizar" title="Editar"  CssClass="btn btn-info btn-sm" ><span class="glyphicon glyphicon-edit"  style="font-size:medium"></span></asp:LinkButton>
                                   </ItemTemplate>   
                                                  <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>                    
                                   <ItemTemplate>                          
                            <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="False" CommandName="Excluir"  CommandArgument='<%# Eval("ID_COTACAO_TAXA") %>' 
                                Text="Excluir" OnClientClick="return confirm('Tem certeza que deseja excluir esse registro?')"  CssClass="btn btn-danger btn-sm" ><span class="glyphicon glyphicon-trash"  style="font-size:medium"></span></asp:LinkButton>
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
            <asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="dgvTaxas" />
            <asp:AsyncPostBackTrigger ControlID="btnFecharTaxa" />
                                 <asp:AsyncPostBackTrigger ControlID="btnSalvarTaxa" />
                        <asp:AsyncPostBackTrigger  ControlID="btnFecharFrete" />



     </Triggers>   
     </asp:UpdatePanel>

                  
         
               
         
                                                           <asp:button runat="server" style="display:none" id="Button1" CssClass="btn btn-primary" />
</div>

                        <div class="tab-pane fade" id="historico" >
                                                                 <br />
                            <div class="row">
                                   <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="control-label">Quantidade:</label>
                                          <asp:TextBox ID="txtQtd" runat="server" AutoPostBack="True" MaxLength="50" Text="10" required="True" type="number" Width="70px" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <ul class="nav nav-tabs" role="tablist">
                        <li class="active">
                            <a href="#HistoricoCotacao" role="tab" data-toggle="tab">
                                <i class="fa fa-edit" style="padding-right:8px;"></i>Histórico Cotações FCA
                            </a>
                        </li>
                        <li>
                            <a href="#HistoricoFrete" role="tab" data-toggle="tab">
                                <i class="fa fa-edit" style="padding-right:8px;"></i>Histórico Valores de Frete
                            </a>
                        </li>
                        
                    </ul>
                             <div class="tab-content">
                                 <div class="tab-pane fade active in" id="HistoricoCotacao" >
                                     <br />
                            <div class="table-responsive">

                                 <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
    <ContentTemplate> 
                                <asp:GridView ID="dgvHistoricoCotacao" DataKeyNames="ID_COTACAO" DataSourceID="dsHistoricoCotacao" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server"  AutoGenerateColumns="false"   style="max-height:400px; overflow:auto;" AllowSorting="true" EmptyDataText="Nenhum registro encontrado.">
                                    <Columns>
                                        <asp:BoundField DataField="ID_COTACAO" HeaderText="#"  SortExpression="ID_COTACAO" Visible="false"/>
                                        <asp:BoundField DataField="NR_COTACAO" HeaderText="Número"  SortExpression="NR_COTACAO"/>
                                        <asp:BoundField DataField="DT_ABERTURA" HeaderText="Data de Abertura"  SortExpression="DT_ABERTURA"  DataFormatString="{0:dd/MM/yyyy}"/>
                                        <asp:BoundField DataField="NM_STATUS_COTACAO" HeaderText="Status"  SortExpression="NM_STATUS_COTACAO"/>
                                        <asp:BoundField DataField="CLIENTE" HeaderText="Cliente"  SortExpression="CLIENTE" />
                                        <asp:BoundField DataField="Origem" HeaderText="Origem"   SortExpression="Origem"/>
                                        <asp:BoundField DataField="Destino" HeaderText="Destino" SortExpression="Destino" />
                                         <asp:TemplateField HeaderText="">
                                              <ItemTemplate>
                                                 <asp:linkButton ID="btnSelecionar" runat="server"  CssClass="btn btn-primary btn-sm" 
                                CommandArgument='<%# Eval("ID_COTACAO") %>' Visible="false" CommandName="Selecionar" Text="Visualizar Cotação"></asp:linkButton> 
                                                  <a href="GeraPDF.aspx?l=p&c=<%# Eval("ID_COTACAO") %>" class="btn btn-primary btn-sm" data-toggle="tooltip" target="_blank" data-placement="top" title="Editar">Visualizar Cotação</a>
                                              </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                        </asp:TemplateField>   
                                    </Columns>
                                    <HeaderStyle CssClass="headerStyle" />
                                </asp:GridView>
        </ContentTemplate>
<Triggers>
<asp:AsyncPostBackTrigger ControlID="txtQtd" />
    <asp:AsyncPostBackTrigger ControlID="ddlCliente" />

        <ASP:PostBackTrigger ControlID="dgvHistoricoCotacao" />     
<%--            <ASP:AsyncPostBackTrigger ControlID="dgvHistoricoCotacao" EventName="Sorting"  />     --%>

     </Triggers>  
     </asp:UpdatePanel>
                            </div>
                                </div>
                                <div class="tab-pane fade" id="HistoricoFrete" >
                                  <br />
                            <div class="table-responsive">
                                <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
    <ContentTemplate> 
                                <asp:GridView ID="dgvHistoricoFrete" DataKeyNames="LOTE" DataSourceID="dsHistoricoFrete" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server"  AutoGenerateColumns="false"   style="max-height:400px; overflow:auto;" AllowSorting="true" OnSorting="dgvHistoricoFrete_Sorting" EmptyDataText="Nenhum registro encontrado.">
                                    <Columns>
                                        <asp:BoundField DataField="LOTE" HeaderText="Lote"  SortExpression="LOTE"/>
                                        <asp:BoundField DataField="MASTER" HeaderText="Master"  SortExpression="MASTER"/>
                                        <asp:BoundField DataField="VALOR_FRETE_MBL" HeaderText="Frete MBL"  SortExpression="VALOR_FRETE_MBL" />
                                        <asp:BoundField DataField="VALOR_FRETE_HBL" HeaderText="Frete HBL"   SortExpression="VALOR_FRETE_HBL"/>
                                        <asp:BoundField DataField="TIPO_FRETE" HeaderText="Tipo de Frete" SortExpression="TIPO_FRETE" />
                                        <asp:BoundField DataField="MOEDA" HeaderText="Moeda" SortExpression="MOEDA" />
                                        <asp:BoundField DataField="GRAU" HeaderText="Grau"  SortExpression="GRAU" />
                                    </Columns>
                                    <HeaderStyle CssClass="headerStyle" />
                                </asp:GridView>
                                </ContentTemplate>
<Triggers>
<asp:AsyncPostBackTrigger ControlID="txtQtd" />
    <asp:AsyncPostBackTrigger ControlID="dgvHistoricoFrete" />
        <asp:AsyncPostBackTrigger ControlID="ddlCliente" />

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




            <asp:SqlDataSource ID="dsCotacao" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT ID_COTACAO,NR_COTACAO,
A.ID_PORTO_ORIGEM,
(SELECT NM_PORTO FROM TB_PORTO WHERE ID_PORTO = A.ID_PORTO_ORIGEM) Origem,

A.ID_PORTO_DESTINO, 
(SELECT NM_PORTO FROM TB_PORTO WHERE ID_PORTO = A.ID_PORTO_DESTINO) Destino,

A.ID_PORTO_ESCALA1,
(SELECT NM_PORTO FROM TB_PORTO WHERE ID_PORTO = A.ID_PORTO_ESCALA1) Escala,

A.ID_CLIENTE_FINAL,
(SELECT NM_CLIENTE_FINAL FROM TB_CLIENTE_FINAL WHERE ID_CLIENTE_FINAL = A.ID_CLIENTE_FINAL) CLIENTE_FINAL,

A.QT_TRANSITTIME_INICIAL, A.QT_TRANSITTIME_FINAL, 

A.ID_TIPO_FREQUENCIA,
(SELECT NM_TIPO_FREQUENCIA FROM TB_TIPO_FREQUENCIA WHERE ID_TIPO_FREQUENCIA = A.ID_TIPO_FREQUENCIA) TIPO_FREQUENCIA,

A.VL_FREQUENCIA, 

A.NM_TAXAS_INCLUDED, 

A.ID_FRETE_TRANSPORTADOR,

A.VL_PESO_TAXADO, 

A.ID_TIPO_BL,
(SELECT NM_TIPO_BL FROM TB_TIPO_BL WHERE ID_TIPO_BL = A.ID_TIPO_BL) TIPO_BL,

A.ID_MOEDA_FRETE,
(SELECT NM_MOEDA FROM TB_MOEDA WHERE ID_MOEDA = A.ID_MOEDA_FRETE) MOEDA_FRETE,

A.VL_TOTAL_FRETE_COMPRA, A.VL_TOTAL_FRETE_VENDA, A.VL_TOTAL_FRETE_VENDA_MIN, 

A.ID_TIPO_DIVISAO_FRETE,

A.VL_TIPO_DIVISAO_FRETE, A.VL_DIVISAO_FRETE, 

A.ID_TRANSPORTADOR,
(SELECT NM_RAZAO FROM TB_PARCEIRO WHERE ID_PARCEIRO = A.ID_TRANSPORTADOR and FL_TRANSPORTADOR = 1) TRANSPORTADOR,

A.ID_TIPO_CARGA,
(SELECT NM_TIPO_CARGA FROM TB_TIPO_CARGA WHERE ID_TIPO_CARGA = A.ID_TIPO_CARGA) TIPO_CARGA,

A.ID_VIA_ROTA, 
(SELECT NM_VIA_ROTA FROM TB_VIA_ROTA WHERE ID_VIA_ROTA = A.ID_VIA_ROTA) VIA_ROTA,

A.ID_TIPO_ESTUFAGEM,
(SELECT NM_TIPO_ESTUFAGEM FROM TB_TIPO_ESTUFAGEM WHERE ID_TIPO_ESTUFAGEM = A.ID_TIPO_ESTUFAGEM) TIPO_ESTUFAGEM,

A.ID_PROCESSO
FROM TB_COTACAO A WHERE ID_COTACAO = @ID_COTACAO

">
                 <SelectParameters>
                <asp:ControlParameter Name="ID_COTACAO" Type="Int32" ControlID="txtID"  />
            </SelectParameters>
</asp:SqlDataSource>        
        <asp:SqlDataSource ID="dsMoeda" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT ID_MOEDA, NM_MOEDA FROM [dbo].[TB_MOEDA] union SELECT  0, 'Selecione' FROM [dbo].[TB_MOEDA] ORDER BY ID_MOEDA">
</asp:SqlDataSource>

        <asp:SqlDataSource ID="dsPorto" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT ID_PORTO, NM_PORTO FROM [dbo].[TB_PORTO] union SELECT  0, 'Selecione' FROM [dbo].[TB_PORTO] ORDER BY ID_PORTO ">
</asp:SqlDataSource>
    <asp:SqlDataSource ID="dsComex" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT ID_TIPO_COMEX,NM_TIPO_COMEX FROM [dbo].[TB_TIPO_COMEX]
union SELECT  0, 'Selecione' FROM [dbo].[TB_BASE_CALCULO_TAXA] ORDER BY ID_TIPO_COMEX">
</asp:SqlDataSource>
    <asp:SqlDataSource ID="dsRota" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT ID_VIA_ROTA,NM_VIA_ROTA FROM [dbo].[TB_VIA_ROTA]
union SELECT  0, 'Selecione' FROM [dbo].[TB_VIA_ROTA] ORDER BY ID_VIA_ROTA">
</asp:SqlDataSource>
    <asp:SqlDataSource ID="dsTransportador" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT ID_PARCEIRO, NM_RAZAO FROM [dbo].[TB_PARCEIRO] WHERE FL_TRANSPORTADOR  = 1
union SELECT  0, 'Selecione' FROM [dbo].[TB_PARCEIRO] ORDER BY ID_PARCEIRO">
</asp:SqlDataSource>
    <asp:SqlDataSource ID="dsAgente" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT ID_PARCEIRO, NM_RAZAO FROM [dbo].[TB_PARCEIRO] WHERE FL_AGENTE_INTERNACIONAL = 1
union SELECT  0, 'Selecione' FROM [dbo].[TB_PARCEIRO] ORDER BY ID_PARCEIRO">
</asp:SqlDataSource>
     <asp:SqlDataSource ID="dsFrequencia" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT ID_TIPO_FREQUENCIA, NM_TIPO_FREQUENCIA FROM [dbo].[TB_TIPO_FREQUENCIA] 
union SELECT  0, 'Selecione' FROM [dbo].[TB_TIPO_FREQUENCIA] ORDER BY ID_TIPO_FREQUENCIA">
</asp:SqlDataSource>
    <asp:SqlDataSource ID="dsCarga" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT ID_TIPO_CARGA, NM_TIPO_CARGA FROM [dbo].[TB_TIPO_CARGA] WHERE FL_ATIVO = 1
union SELECT  0, 'Selecione' FROM [dbo].[TB_TIPO_CARGA] ORDER BY ID_TIPO_CARGA">
</asp:SqlDataSource>
    <asp:SqlDataSource ID="dsContainer" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT ID_TIPO_CONTAINER, NM_TIPO_CONTAINER FROM TB_TIPO_CONTAINER WHERE FL_ATIVO = 1
union SELECT  0, 'Selecione' FROM [dbo].[TB_TIPO_CONTAINER] ORDER BY ID_TIPO_CONTAINER">
</asp:SqlDataSource>
    <asp:SqlDataSource ID="dsMercadoria" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT ID_MERCADORIA, NM_MERCADORIA FROM [dbo].[TB_MERCADORIA] 
union SELECT  0, 'Selecione' FROM [dbo].[TB_MERCADORIA] ORDER BY ID_MERCADORIA">
</asp:SqlDataSource>
    <asp:SqlDataSource ID="dsEstufagem" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT ID_TIPO_ESTUFAGEM, NM_TIPO_ESTUFAGEM FROM [dbo].[TB_TIPO_ESTUFAGEM] 
union SELECT  0, 'Selecione' FROM [dbo].[TB_MERCADORIA] ORDER BY ID_TIPO_ESTUFAGEM">
</asp:SqlDataSource>

<asp:SqlDataSource ID="dsStatusCotacao" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT ID_STATUS_COTACAO, NM_STATUS_COTACAO FROM TB_STATUS_COTACAO 
union SELECT  0, 'Selecione' FROM TB_STATUS_COTACAO ORDER BY ID_STATUS_COTACAO">
</asp:SqlDataSource>
      <asp:SqlDataSource ID="dsDestinatarioComercial" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT ID_DESTINATARIO_COMERCIAL,NM_DESTINATARIO_COMERCIAL FROM TB_DESTINATARIO_COMERCIAL
union SELECT  0, 'Selecione' FROM TB_DESTINATARIO_COMERCIAL ORDER BY ID_DESTINATARIO_COMERCIAL">
</asp:SqlDataSource>
      <asp:SqlDataSource ID="dsIncoterm" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT ID_INCOTERM, cast((CD_INCOTERM)as varchar)+ ' - '+ NM_INCOTERM as NM_INCOTERM FROM TB_INCOTERM 
union SELECT  0, 'Selecione' FROM TB_INCOTERM ORDER BY ID_INCOTERM">
</asp:SqlDataSource>
      <asp:SqlDataSource ID="dsUsuario" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT ID_USUARIO, NOME FROM TB_USUARIO union SELECT  0, 'Selecione' FROM TB_USUARIO ORDER BY ID_USUARIO">
</asp:SqlDataSource>
      <asp:SqlDataSource ID="dsClienteFinal" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT ID_CLIENTE_FINAL,NM_CLIENTE_FINAL FROM TB_CLIENTE_FINAL 
union SELECT  0, 'Selecione' FROM TB_CLIENTE_FINAL ORDER BY ID_CLIENTE_FINAL">
</asp:SqlDataSource>
        <asp:SqlDataSource ID="dsCliente" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT ID_PARCEIRO, NM_RAZAO  FROM TB_PARCEIRO WHERE FL_EXPORTADOR= 1 OR FL_IMPORTADOR =1 OR FL_AGENTE = 1 OR FL_AGENTE_INTERNACIONAL =1 OR FL_COMISSARIA = 1 OR FL_INDICADOR = 1
union SELECT  0, 'Selecione' FROM TB_PARCEIRO ORDER BY ID_PARCEIRO">
</asp:SqlDataSource>
      <asp:SqlDataSource ID="dsContato" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT ID_CONTATO, NM_CONTATO FROM TB_CONTATO WHERE ID_PARCEIRO = @CLIENTE
union SELECT  0, 'Selecione' FROM TB_CONTATO ORDER BY ID_CONTATO">
           <SelectParameters>
                <asp:ControlParameter Name="CLIENTE" Type="Int32" ControlID="ddlCliente"  />
            </SelectParameters>
</asp:SqlDataSource>
      <asp:SqlDataSource ID="dsVendedor" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT ID_PARCEIRO, NM_RAZAO  FROM TB_PARCEIRO WHERE FL_VENDEDOR = 1
union SELECT  0, 'Selecione' FROM TB_PARCEIRO ORDER BY ID_PARCEIRO">
</asp:SqlDataSource>  
    <asp:SqlDataSource ID="dsMotivoCancelamento" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT ID_MOTIVO_CANCELAMENTO,NM_MOTIVO_CANCELAMENTO FROM TB_MOTIVO_CANCELAMENTO
union SELECT  0, 'Selecione' FROM TB_MOTIVO_CANCELAMENTO ORDER BY ID_MOTIVO_CANCELAMENTO">
</asp:SqlDataSource>
     <asp:SqlDataSource ID="dsServico" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT ID_SERVICO, NM_SERVICO FROM TB_SERVICO
union SELECT  0, 'Selecione' FROM TB_SERVICO ORDER BY ID_SERVICO">
</asp:SqlDataSource>
     <asp:SqlDataSource ID="dsBL" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT ID_TIPO_BL, NM_TIPO_BL FROM TB_TIPO_BL 
union SELECT  0, 'Selecione' FROM TB_TIPO_BL ORDER BY ID_TIPO_BL">
</asp:SqlDataSource>

         <asp:SqlDataSource ID="dsMercadorias" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT 
A.ID_COTACAO_MERCADORIA,A.ID_COTACAO,A.ID_MERCADORIA,C.NM_MERCADORIA,A.ID_TIPO_CONTAINER,B.NM_TIPO_CONTAINER,A.QT_CONTAINER,A.VL_FRETE_COMPRA,A.VL_FRETE_VENDA,A.VL_PESO_BRUTO,A.VL_M3,A.DS_MERCADORIA,A.VL_COMPRIMENTO,A.VL_LARGURA,A.VL_ALTURA,A.VL_CARGA,A.QT_DIAS_FREETIME
FROM TB_COTACAO_MERCADORIA A
LEFT JOIN TB_TIPO_CONTAINER B ON B.ID_TIPO_CONTAINER = A.ID_TIPO_CONTAINER
LEFT JOIN TB_MERCADORIA C ON C.ID_MERCADORIA = A.ID_MERCADORIA 
WHERE ID_COTACAO = @ID_COTACAO
"> <SelectParameters>
                <asp:ControlParameter Name="ID_COTACAO" Type="Int32" ControlID="txtID"  />
            </SelectParameters>
 </asp:SqlDataSource>                 
  <asp:SqlDataSource ID="dsIDMercadoria" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT ID_MERCADORIA, NM_MERCADORIA FROM TB_MERCADORIA
union SELECT  0, 'Selecione' FROM TB_MERCADORIA ORDER BY ID_MERCADORIA">
</asp:SqlDataSource>
      <asp:SqlDataSource ID="dsFreteTransportador" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT ID_FRETE_TRANSPORTADOR, cast(ID_FRETE_TRANSPORTADOR as varchar) +' - ' + (SELECT NM_PORTO FROM TB_PORTO WHERE ID_PORTO = A.ID_PORTO_ORIGEM)+' - ' + (SELECT NM_PORTO FROM TB_PORTO WHERE ID_PORTO = A.ID_PORTO_DESTINO) as Descricao FROM TB_FRETE_TRANSPORTADOR A WHERE DT_VALIDADE_FINAL > getdate() and ID_PORTO_ORIGEM = 0 AND ID_PORTO_DESTINO = 0 AND ID_TRANSPORTADOR =0 union SELECT  0, 'Selecione' FROM TB_FRETE_TRANSPORTADOR ORDER BY ID_FRETE_TRANSPORTADOR">
</asp:SqlDataSource>
<asp:SqlDataSource ID="dsTaxas" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT 
ID_COTACAO_TAXA,
ID_COTACAO,

ID_ITEM_DESPESA,
(SELECT NM_ITEM_DESPESA FROM TB_ITEM_DESPESA WHERE ID_ITEM_DESPESA = A.ID_ITEM_DESPESA)NM_TIPO_ITEM_DESPESA,

ID_ORIGEM_PAGAMENTO,
(SELECT NM_ORIGEM_PAGAMENTO FROM TB_ORIGEM_PAGAMENTO WHERE ID_ORIGEM_PAGAMENTO = A.ID_ORIGEM_PAGAMENTO)NM_ORIGEM_PAGAMENTO,
(SELECT NM_BASE_CALCULO_TAXA FROM TB_BASE_CALCULO_TAXA WHERE ID_BASE_CALCULO_TAXA = A.ID_BASE_CALCULO_TAXA)NM_BASE_CALCULO_TAXA,
FL_DECLARADO,
case when FL_DECLARADO = 1 then 'Sim' else 'Não' end as DECLARADO,

FL_DIVISAO_PROFIT,
case when FL_DIVISAO_PROFIT = 1 then 'Sim' else 'Não' end as PROFIT,

ID_DESTINATARIO_COBRANCA,
(SELECT NM_DESTINATARIO_COBRANCA FROM TB_DESTINATARIO_COBRANCA WHERE ID_DESTINATARIO_COBRANCA =A.ID_DESTINATARIO_COBRANCA)NM_DESTINATARIO_COBRANCA,

ID_MOEDA_COMPRA,
(SELECT NM_MOEDA FROM TB_MOEDA WHERE ID_MOEDA = A.ID_MOEDA_COMPRA)NM_MOEDA_COMPRA,

VL_TAXA_COMPRA,

ID_MOEDA_VENDA,
(SELECT NM_MOEDA FROM TB_MOEDA WHERE ID_MOEDA = A.ID_MOEDA_VENDA)NM_MOEDA_VENDA,

VL_TAXA_VENDA,
    VL_TAXA_COMPRA_MIN,
    VL_TAXA_VENDA_MIN

FROM TB_COTACAO_TAXA A
    WHERE ID_COTACAO = @ID_COTACAO

"> <SelectParameters>
                <asp:ControlParameter Name="ID_COTACAO" Type="Int32" ControlID="txtID"  />
            </SelectParameters>
</asp:SqlDataSource>
     <asp:SqlDataSource ID="dsOrigemPagamento" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT ID_ORIGEM_PAGAMENTO,NM_ORIGEM_PAGAMENTO FROM  [dbo].[TB_ORIGEM_PAGAMENTO]
union SELECT  0, 'Selecione' FROM [dbo].[TB_ORIGEM_PAGAMENTO] ORDER BY ID_ORIGEM_PAGAMENTO">
</asp:SqlDataSource>
     <asp:SqlDataSource ID="dsDestinatarioCobranca" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="select ID_DESTINATARIO_COBRANCA,NM_DESTINATARIO_COBRANCA from TB_DESTINATARIO_COBRANCA
union SELECT  0, 'Selecione' FROM TB_DESTINATARIO_COBRANCA ORDER BY ID_DESTINATARIO_COBRANCA
">
</asp:SqlDataSource>
    <asp:SqlDataSource ID="dsHistoricoCotacao" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT top 10
ID_COTACAO, 
NR_COTACAO, 
DT_ABERTURA,
ID_STATUS_COTACAO,
(SELECT NM_STATUS_COTACAO FROM TB_STATUS_COTACAO WHERE ID_STATUS_COTACAO = A.ID_STATUS_COTACAO) NM_STATUS_COTACAO,
ID_CLIENTE,
                    (SELECT NM_RAZAO FROM TB_PARCEIRO WHERE ID_PARCEIRO = A.ID_CLIENTE )AS CLIENTE,
ID_PORTO_ORIGEM,
(SELECT NM_PORTO FROM TB_PORTO WHERE ID_PORTO = A.ID_PORTO_ORIGEM) Origem,
ID_PORTO_DESTINO, 
(SELECT NM_PORTO FROM TB_PORTO WHERE ID_PORTO = A.ID_PORTO_DESTINO) Destino
FROM TB_COTACAO A where ID_CLIENTE = @ID_CLIENTE AND ID_TIPO_ESTUFAGEM = @ID_TIPO_ESTUFAGEM ">
        <SelectParameters>
                <asp:Parameter Name="ID_CLIENTE" Type="Int32"  />
                <asp:Parameter Name="ID_TIPO_ESTUFAGEM" Type="Int32"  />
            </SelectParameters>
</asp:SqlDataSource>


        <asp:SqlDataSource ID="dsBaseCalculo" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT ID_BASE_CALCULO_TAXA,NM_BASE_CALCULO_TAXA FROM [dbo].[TB_BASE_CALCULO_TAXA]
union SELECT  0, 'Selecione' FROM [dbo].[TB_BASE_CALCULO_TAXA] ORDER BY ID_BASE_CALCULO_TAXA">
</asp:SqlDataSource>
    <asp:SqlDataSource ID="dsDivisaoProfit" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT ID_TIPO_DIVISAO_PROFIT,NM_TIPO_DIVISAO_PROFIT FROM [dbo].TB_TIPO_DIVISAO_PROFIT
union SELECT  0, 'Selecione' FROM [dbo].TB_TIPO_DIVISAO_PROFIT ORDER BY ID_TIPO_DIVISAO_PROFIT">
</asp:SqlDataSource>
    <asp:SqlDataSource ID="dsItemDespesa" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT ID_ITEM_DESPESA,NM_ITEM_DESPESA FROM  [dbo].[TB_ITEM_DESPESA]
union SELECT  0, ' Selecione' FROM [dbo].[TB_TIPO_ITEM_DESPESA] ORDER BY NM_ITEM_DESPESA">
</asp:SqlDataSource>
     <asp:SqlDataSource ID="dsTipoPagamento" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT ID_TIPO_PAGAMENTO, NM_TIPO_PAGAMENTO FROM TB_TIPO_PAGAMENTO
union SELECT  0, 'Selecione' FROM TB_TIPO_PAGAMENTO ORDER BY ID_TIPO_PAGAMENTO">
</asp:SqlDataSource>
 
   <asp:SqlDataSource ID="dsHistoricoFrete" runat="server" ConnectionString="<%$ ConnectionStrings:StringConexaoOracle %>" ProviderName="<%$ConnectionStrings:StringConexaoOracle.ProviderName %> "
        selectcommand="SELECT * FROM VW_VALOR_FRETE_LOTE where rownum <= 10 and cnpj = '@cnpj'">
       <SelectParameters>
                <asp:Parameter Name="cnpj" Type="string"  />
            </SelectParameters>
</asp:SqlDataSource>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
