<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="CadastrarFreteTransportador.aspx.vb" Inherits="NVOCC.Web.CadastrarFreteTransportador" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server"> 
    <%--<asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
    <ContentTemplate>--%>
    <div class="row principal">
        <div class="col-lg-12 col-md-12 col-sm-12">

            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">FRETE TRANSPORTADOR                                         
                    </h3>
                </div>
                <div class="panel-body">
                     <ul class="nav nav-tabs" role="tablist">
                        <li class="active">
                            <a href="#basico" role="tab" data-toggle="tab">
                                <i class="fa fa-edit" style="padding-right:8px;"></i>Informações Basicas
                            </a>
                        </li>
                        <li>
                            <a href="#tarifario" role="tab" data-toggle="tab">
                                <i class="fa fa-edit" style="padding-right:8px;"></i>Frete Tarifário
                            </a>
                        </li>
                         <li>
                            <a href="#taxas" role="tab" data-toggle="tab">
                                <i class="fa fa-edit" style="padding-right:8px;"></i>Taxas
                            </a>
                        </li> 
                    </ul>
                   
                    <div class="tab-content">
                        <div class="tab-pane fade active in" id="basico">
                            <asp:UpdatePanel ID="updPainel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
    <ContentTemplate>
         <br/>
                                    <div class="alert alert-success" ID="divsuccess" runat="server" visible="false">
                                        <asp:label ID="lblmsgSuccess" runat="server" Text="Registro cadastrado/atualizado com sucesso!"></asp:label>
                                    </div>
                                    <div class="alert alert-danger" ID="diverro" runat="server" visible="false">
                                        <asp:label ID="lblmsgErro" runat="server"></asp:label>
                                    </div>
        <br/>
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <label class="control-label">Código:</label>
                                        <asp:TextBox ID="txtID_FreteTransportador" runat="server" Enabled="false" CssClass="form-control" ></asp:TextBox>
                                    </div>     
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <asp:checkbox ID="ckbAtivo" Style="margin-top:20px" CssClass="form-control" text="&nbsp;&nbsp;Ativo" runat="server"  />
                                    </div>
                                </div>
                                 <div class="col-sm-6">
                                                                            <div class="form-group">
                                                                                <label class="control-label">Origem Serviço:</label><label runat="server" style="color:red" >*</label>
                                                                                <asp:DropDownList ID="ddlOrigem_Pagamento" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_ORIGEM_PAGAMENTO" DataSourceID="dsOrigemPagamento" DataValueField="ID_ORIGEM_PAGAMENTO">
                                                                                </asp:DropDownList>


                                                                            </div>
                                                                        </div>
                            </div>
                            

                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="control-label">Porto de Origem:</label><label runat="server" style="color:red" >*</label>
                                        <asp:DropDownList ID="ddlOrigem" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_PORTO" DataSourceID="dsPorto" DataValueField="ID_PORTO">
                                        </asp:DropDownList>                                    </div>
                                </div>
                                 <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="control-label">Porto de Destino:</label><label runat="server" style="color:red" >*</label>
                                        <asp:DropDownList ID="ddlDestino" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_PORTO" DataSourceID="dsPorto" DataValueField="ID_PORTO">
                                        </asp:DropDownList>                                    </div>
                                </div>
                                
                            </div>
                               
                            <div class="row">

                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="control-label">Tipo moeda:</label><label runat="server" style="color:red" >*</label>
                                        <asp:DropDownList ID="ddlMoeda" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_MOEDA" DataSourceID="dsMoeda" DataValueField="ID_MOEDA" >
                                        </asp:DropDownList>                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="control-label">Carga:</label><label runat="server" style="color:red" >*</label>
                                        <asp:DropDownList ID="ddlTipoCarga" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_TIPO_CARGA" DataSourceID="dsCarga" DataValueField="ID_TIPO_CARGA" >
                                        </asp:DropDownList>                                    </div>
                                </div>
                                </div>
                             
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="control-label">Rota:</label><label runat="server" style="color:red" >*</label>
                                        <asp:DropDownList ID="ddlRota" runat="server" AutoPostBack="true" CssClass="form-control" Font-Size="11px" DataTextField="NM_VIA_ROTA" DataSourceID="dsRota" DataValueField="ID_VIA_ROTA" >
                                        </asp:DropDownList>                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="control-label">Tipo Comex:</label><label runat="server" style="color:red" >*</label>
                                        <asp:DropDownList ID="ddlComex" runat="server" AutoPostBack="true" CssClass="form-control" Font-Size="11px"  DataTextField="NM_TIPO_COMEX" DataSourceID="dsComex" DataValueField="ID_TIPO_COMEX">
                                        </asp:DropDownList>                                    </div>
                                </div>
                            </div>
                                <div class="row" id="divEscala" runat="server" style="display:none">
                                  <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Porto de Escala I:</label><label runat="server" style="color:red" >*</label>
                                        <asp:DropDownList ID="ddlEscala1" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_PORTO" DataSourceID="dsPorto" DataValueField="ID_PORTO">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                    <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Porto de Escala II:</label><label runat="server" style="color:red" >*</label>
                                        <asp:DropDownList ID="ddlEscala2" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_PORTO" DataSourceID="dsPorto" DataValueField="ID_PORTO">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                    <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Porto de Escala III:</label><label runat="server" style="color:red" >*</label>
                                        <asp:DropDownList ID="ddlEscala3" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_PORTO" DataSourceID="dsPorto" DataValueField="ID_PORTO">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                              </div>
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="control-label">Transportador:</label><label runat="server" style="color:red" >*</label>
                                        <asp:DropDownList ID="ddlTransportador" runat="server" CssClass="form-control" Font-Size="11px"  DataTextField="NM_RAZAO" DataSourceID="dsTransportador" DataValueField="ID_PARCEIRO"></asp:DropDownList>            </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group"><label runat="server" style="color:red" >*</label>
                                        <label class="control-label">Agente Internacional:</label>
                                        <asp:DropDownList ID="ddlAgente" runat="server" CssClass="form-control" Font-Size="11px"  DataTextField="NM_RAZAO" DataSourceID="dsAgente" DataValueField="ID_PARCEIRO"></asp:DropDownList>              </div>
                                </div>
                            </div>

                            <div class="row">

                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="control-label">Dias Transittime(Inicial):</label><label runat="server" style="color:red" >*</label>
                                        <asp:TextBox ID="txtTransittimeInicial" runat="server" CssClass="form-control ApenasNumeros" ></asp:TextBox>
                                    </div>
                                </div>
                                 <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="control-label">Dias Transittime(Final):</label><label runat="server" style="color:red" >*</label>
                                        <asp:TextBox ID="txtTransittimeFinal" runat="server" CssClass="form-control ApenasNumeros" ></asp:TextBox>
                                    </div>
                                </div>

                            </div>

                            
                             <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="control-label">Frequência:</label><label runat="server" style="color:red" >*</label>
                                        <asp:DropDownList ID="ddlFrequencia" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_TIPO_FREQUENCIA" DataSourceID="dsFrequencia" DataValueField="ID_TIPO_FREQUENCIA">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="control-label">Taxas Included:</label>
                                        <asp:TextBox ID="txtTaxas" runat="server" CssClass="form-control" ></asp:TextBox>
                                    </div>
                                </div>         </div><div class="row">                      
                                 <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="control-label">Validade Final:</label>
                                        <asp:TextBox ID="txtValidadeFinal" runat="server" Enabled="false" CssClass="form-control data" ></asp:TextBox>
                                    </div>
                                </div>    <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="control-label">Via Transporte:</label><label runat="server" style="color:red" >*</label>
                                        <asp:DropDownList ID="ddlViaTransporte" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_VIATRANSPORTE" DataSourceID="dsViaTransporte" DataValueField="ID_VIATRANSPORTE" >
                                        </asp:DropDownList>                                    </div>
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
       <asp:AsyncPostBackTrigger ControlID="ddlRota" />
        <ASP:PostBackTrigger ControlID="btnGravar" />
               <asp:AsyncPostBackTrigger ControlID="btnSalvarTarifario" />
    </Triggers>
</asp:UpdatePanel>
                        </div>
 
                        <div class="tab-pane fade" id="tarifario"> 

                        
<asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
    <ContentTemplate>  <ajaxToolkit:ModalPopupExtender id="mpeNovoTarifario" runat="server" PopupControlID="Panel2" TargetControlID="btnNovoTarifario"  CancelControlID="Button1"></ajaxToolkit:ModalPopupExtender>   
                        <asp:Panel ID="Panel2" runat="server" CssClass="modalPopup" style="display:none" >     
         <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
    <ContentTemplate> 
                                                <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title" id="modalTarifarioNovo">TARIFARIO FRETE TRANSPORTADOR</h5>
                                                        </div>
                                                        <div class="modal-body">                                                           
                                    <div class="alert alert-success" ID="divSuccessTarifario" runat="server" visible="false">
                                        <asp:label ID="lblmsgSuccessNovo" runat="server" Text="Registro cadastrado/atualizado com sucesso!"></asp:label>
                                    </div>
                                    <div class="alert alert-danger" ID="divErroTarifario" runat="server" visible="false">
                                        <asp:label ID="lblmsgErroTarifario" runat="server"></asp:label>
                                    </div>
                                   <div class="alert alert-info" ID="divInfoTarifario" runat="server" visible="false">
                                        <asp:label ID="lblInfoTarifario" runat="server"></asp:label>
                                    </div>
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <label class="control-label">Código:</label>
                                        <asp:TextBox ID="txtIDTarifario" runat="server" Enabled="false" CssClass="form-control" ></asp:TextBox>
                                    </div>     
                                </div> 
                                 <div class="col-sm-6" style="display:none">
                                    <div class="form-group">
                                        <label class="control-label">ID Estufagem:</label><label runat="server" style="color:red" >*</label>
                                     <asp:TextBox ID="txtEstufagem" CssClass="txtEstufagem form-control"   runat="server" Width="50px" TabIndex="1" MaxLength="40"></asp:TextBox>                                                                          </div>
                                </div>
                              
                                </div>
                             <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <label class="control-label">Frete Transportador:</label>
                                        <asp:TextBox ID="txtFreteTransportadorTarifario" runat="server" Enabled="false" CssClass="form-control" ></asp:TextBox>        </div>
                            </div>        
                                 <div class="col-sm-3">
                                    <div class="form-group">
                                        <label class="control-label">Valor da Compra:</label><label runat="server" style="color:red" >*</label>
                                        <asp:TextBox ID="txtValorCompra" runat="server" CssClass="form-control moeda" maxlength="15" ></asp:TextBox>
                                                                          </div>
                                </div>

                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <label class="control-label">Data de Validade (Inicial):</label><label runat="server" style="color:red" >*</label>
                                        <asp:TextBox ID="txtValidadeInicial" runat="server" CssClass="form-control data" ></asp:TextBox>
                                                                            </div>
                                </div>
                               
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <label class="control-label">Data de Validade (Final):</label><label runat="server" style="color:red" >*</label>
                                        <asp:TextBox ID="txtValidadeFinal_Tarifario" runat="server" CssClass="form-control data" ></asp:TextBox>
                                    </div>
                                </div>

                            </div>   
                              <div  id="divMercadoriaServico" runat="server" style="display:none">
                                      <div class="row">
                                          <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Tipo de Mercadoria:</label><label runat="server" style="color:red" >*</label>
                                        <asp:DropDownList ID="ddlMercadoria" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_MERCADORIA" DataSourceID="dsMercadoria" DataValueField="ID_MERCADORIA" ></asp:DropDownList>   </div>           </div>
                                          </div>
                                        <div class="row">
                                            <div class="col-md-12">
                        <div class="form-group">
                            <label class="control-label">Serviço:</label><label runat="server" style="color:red" >*</label> 
                            <asp:TextBox ID="txtservico" runat="server" CssClass="form-control" Style="max-width: 800px;" TextMode="MultiLine" Rows="2" onkeyUp="return CheckMaxCount(this,event,250);"></asp:TextBox>
                        </div>
                                          </div>
                                  </div>
                                 </div>

                              <br/>                  
                            <ul class="nav nav-tabs" role="tablist">
               
                                 <li class="active">
                            <a href="#FCL" role="tab" data-toggle="tab" id="fcl" ><i class="fa fa-angle-double-right"></i> FCL
                            </a>
                        </li>
                        <li>
                            <a href="#LCL" role="tab" data-toggle="tab"  id="lcl"><i class="fa fa-angle-double-right"></i> LCL
                            </a>
                        </li>
                       
                        
                    </ul>  <div class="tab-content">

                             <div  class="tab-pane fade active in" id="FCL">
                             <div class="linha-colorida">FCL</div>
                                 <asp:Checkbox ID="ckfcl" runat="server" CssClass="form-control" visible="false" ></asp:Checkbox>
                             <br />
                              <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group">
                                          <label class="control-label"></label>
                                        <asp:Checkbox ID="ckbIMO" runat="server" CssClass="form-control" text="&nbsp;&nbsp;IMO" ></asp:Checkbox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                          <label class="control-label"></label>
                                        <asp:Checkbox ID="ckbCargaEspecial" runat="server" CssClass="form-control" text="&nbsp;&nbsp;Carga Especial" ></asp:Checkbox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <label class="control-label">Dias Freetime:</label><label runat="server" style="color:red" >*</label>
                                        <asp:TextBox ID="txtFreetime" runat="server" CssClass="form-control inteiro" ></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <label class="control-label">Tipo de Container:</label><label runat="server" style="color:red" >*</label>
                                        <asp:DropDownList ID="ddlContainer" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_TIPO_CONTAINER" DataSourceID="dsContainer" DataValueField="ID_TIPO_CONTAINER" ></asp:DropDownList>   </div>           </div>

                                
                                
                            
                                       </div>
                           
                             </div>
                         <div class="tab-pane fade" id="LCL">
                             <div class="linha-colorida">LCL</div>
                             
                                          <asp:Checkbox ID="ckblcl" runat="server" CssClass="form-control" visible="false" ></asp:Checkbox>

                             <br />
                             <div class="row">
                                 <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Valor M3 (Inicial):</label><label runat="server" style="color:red" >*</label>
                                        <asp:TextBox ID="txtM3Inicial" runat="server" CssClass="form-control moeda" maxlength="13"></asp:TextBox>
                                      </div>
                                 </div>
                                     <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Valor M3 (Final):</label><label runat="server" style="color:red" >*</label>
                                        <asp:TextBox ID="txtM3Final" runat="server" CssClass="form-control moeda" maxlength="13"></asp:TextBox>
                                  </div>
                              
                             </div>                               
                         
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Valor Mínimo:</label><label runat="server" style="color:red" >*</label>
                                        <asp:TextBox ID="txtValorMinimo" runat="server" CssClass="form-control moeda" maxlength="15"></asp:TextBox>
                                  </div>
                                </div>
                               
                            </div>
                                                         
</div>
                       </div></div>
                               <div class="modal-footer">
                                                            <asp:Button runat="server" Text="Salvar Taxa" id="btnSalvarTarifario" CssClass="btn btn-success" />
                                                            <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFecharTarifario" text="Close" />
                                                        </div>
                                                    
                                                </div>
      
                                       </div>            
   </ContentTemplate>
<Triggers>
           <asp:AsyncPostBackTrigger ControlID="ddlcomex" />

            <asp:AsyncPostBackTrigger  ControlID="btnSalvarTarifario" />
                 <asp:AsyncPostBackTrigger  ControlID="btnFecharTarifario" />
     </Triggers>  
     </asp:UpdatePanel>
     </asp:Panel>


                       

                                            <br/>
                           <div class="row">
                                     
                                  <div class="col-sm-4"">
                                                    <div class="form-group">
                                                        <asp:button runat="server" Text="Novo Tarifario" id="btnNovoTarifario" CssClass="btn btn-primary" />
                                                    </div>
                                                </div>
                            </div>
        <br />
                             <div class="tab-content">
                      
                             <br />
                              <div class="alert alert-success" id="divMsgExcluir" runat="server" visible="false">                                       
                      <asp:label ID="lblMsgExcluir" runat="server"  /> 
                  </div>
                                  

         <div class="alert alert-danger" id="divMsgErro" runat="server" visible="false">                                       
                      <asp:label ID="Label1" runat="server"  /> 
                  </div>
                               <br />
                            <div class="table-responsive ">
                                <asp:GridView ID="dgvFreteTarifario" DataKeyNames="ID_TARIFARIO_FRETE_TRANSPORTADOR" DataSourceID="dsFreteTarifario" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server"  AutoGenerateColumns="false"  style="max-height:400px; overflow:auto;" AllowSorting="true" OnSorting="dgvFreteTarifario_Sorting" EmptyDataText="Nenhum registro encontrado.">
                                    <Columns>

                                        <asp:BoundField DataField="ID_TARIFARIO_FRETE_TRANSPORTADOR" HeaderText="#" SortExpression="ID_TARIFARIO_FRETE_TRANSPORTADOR" />
                                        <asp:BoundField DataField="ID_FRETE_TRANSPORTADOR" HeaderText="Frete Transportador" SortExpression="ID_FRETE_TRANSPORTADOR"/>
                                        <asp:BoundField DataField="NM_TIPO_CONTAINER" HeaderText="Tipo de Container" SortExpression="NM_TIPO_CONTAINER"/>
                                        <asp:BoundField DataField="NM_TIPO_ESTUFAGEM" HeaderText="Tipo de Estufagem" SortExpression="NM_TIPO_ESTUFAGEM" />
                                        <asp:BoundField DataField="DT_VALIDADE_INICIAL" HeaderText="Validade (Inicial)" SortExpression="DT_VALIDADE_INICIAL" DataFormatString="{0:dd/MM/yyyy}"/>
                                        <asp:BoundField DataField="VL_COMPRA" HeaderText="Valor de Compra" SortExpression="VL_COMPRA"  />
                                        <asp:BoundField DataField="VL_MINIMO" HeaderText="Valor Mínimo" SortExpression="VL_MINIMO" />
                                        <asp:BoundField DataField="QT_DIAS_FREETIME" HeaderText="Qtd. Dias Freetime" SortExpression="QT_DIAS_FREETIME" />
                                        <asp:BoundField DataField="VL_M3_INICIAL" HeaderText="Valor M3 (Inicial)" SortExpression="VL_M3_INICIAL"  />
                                        <asp:BoundField DataField="VL_M3_FINAL" HeaderText="Valor M3 (Final)" SortExpression="VL_M3_FINAL" />
                                        <asp:BoundField DataField="IMO" HeaderText="IMO" SortExpression="IMO"/>
                                        <asp:BoundField DataField="CARGA_ESPECIAL" HeaderText="Carga Especial" SortExpression="CARGA_ESPECIAL"/>
                                              <asp:TemplateField HeaderText="" >
                                         <ItemTemplate>                          
                            <asp:LinkButton ID="btnVisualizar" runat="server" CausesValidation="False" CommandName="visualizar" CommandArgument='<%# Eval("ID_TARIFARIO_FRETE_TRANSPORTADOR") %>'  
                                Text="Visualizar" title="Editar"  CssClass="btn btn-info btn-sm" ><span class="glyphicon glyphicon-edit"  style="font-size:medium"></span></asp:LinkButton>
                                   </ItemTemplate>   
                                                   <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>                    
                                   <ItemTemplate>                          
                            <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="False" CommandName="Excluir"  CommandArgument='<%# Eval("ID_TARIFARIO_FRETE_TRANSPORTADOR") %>' 
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
            <asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="dgvFreteTarifario" />
                 <asp:AsyncPostBackTrigger ControlID="btnFecharTarifario" />
     

     </Triggers>   
     </asp:UpdatePanel>
</div>  

                        <div class="tab-pane fade" id="taxas">
            <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
    <ContentTemplate>              
         <ajaxToolkit:ModalPopupExtender id="mpeNovoTaxa" runat="server" PopupControlID="Panel1" TargetControlID="btnNovaTaxa"  CancelControlID="Button1"></ajaxToolkit:ModalPopupExtender>
     
   <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" style="display:none" >     
         <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
    <ContentTemplate> 
                                                <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title" id="modalTaxaNova">FRETE TAXA</h5>
                                                        </div>
                                                        <div class="modal-body">                                                           
                                    <div class="alert alert-success" ID="divSuccessTaxa" runat="server" visible="false">
                                        <asp:label ID="Label2" runat="server" Text="Registro cadastrado/atualizado com sucesso!"></asp:label>
                                    </div>
                                    <div class="alert alert-danger" ID="divErroTaxa" runat="server" visible="false">
                                        <asp:label ID="lblErroTaxa" runat="server"></asp:label>
                                    </div>
                              <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Código:</label>
                                        <asp:TextBox ID="txtIDTaxa" runat="server" Enabled="false" CssClass="form-control" ></asp:TextBox>
                                    </div>     
                                </div><div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Frete Transportador:</label>
                                        <asp:TextBox ID="txtFreteTransportadorTaxa" runat="server" Enabled="false" CssClass="form-control" ></asp:TextBox>        </div>
                                    </div>
                               
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Estufagem:</label><label runat="server" style="color:red" >*</label>
                                        <asp:DropDownList ID="ddlEstufagemTaxa" runat="server" CssClass="form-control" Font-Size="11px" DataValueField="ID_TIPO_ESTUFAGEM" DataTextField="NM_TIPO_ESTUFAGEM" DataSourceID="dsEstufagem">
                                        </asp:DropDownList>
                                    </div>     
                                </div>
                            </div>
                            <div class="row">
                                

                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Item  Despesa:</label><label runat="server" style="color:red" >*</label>
                                        <asp:DropDownList ID="ddlItemDespesa" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_ITEM_DESPESA" DataSourceID="dsItemDespesa" DataValueField="ID_ITEM_DESPESA"  ></asp:DropDownList>              </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Origem Serviço:</label><label runat="server" style="color:red" >*</label>
                                        <asp:DropDownList ID="ddlOrigemPagamento" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_ORIGEM_PAGAMENTO" DataSourceID="dsOrigemPagamento" DataValueField="ID_ORIGEM_PAGAMENTO" >
                                        </asp:DropDownList>                                    </div>
                                </div><div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Base Calculo Taxa:</label><label runat="server" style="color:red" >*</label>
                                        <asp:DropDownList ID="ddlBaseCalculoTaxa" runat="server" CssClass="form-control" Font-Size="11px"  DataTextField="NM_BASE_CALCULO_TAXA" DataSourceID="dsBaseCalculo" DataValueField="ID_BASE_CALCULO_TAXA">
                                        </asp:DropDownList>                                    </div>
                                </div>
                            </div>
                            <div class="row">                                
                                
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Moeda Compra:</label><label runat="server" style="color:red" >*</label>
                                        <asp:DropDownList ID="ddlMoedaCompra" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_MOEDA" DataSourceID="dsMoeda" DataValueField="ID_MOEDA">
                                        </asp:DropDownList>                                    </div>
                                </div>
                                       <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Valor Taxa Compra:</label><label runat="server" style="color:red" >*</label>
                                        <asp:TextBox ID="txtValorTaxaCompra" runat="server" CssClass="form-control moeda" ></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Valor Taxa Venda(Min):</label>
                                        <asp:TextBox ID="txtValorTaxaCompraMin" runat="server" CssClass="form-control moeda" ></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                        
                             <div class="row">

                           
                                 <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Moeda Venda:</label>
                                        <asp:DropDownList ID="ddlMoedaVenda" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_MOEDA" DataSourceID="dsMoeda" DataValueField="ID_MOEDA">
                                        </asp:DropDownList>                                    </div>
                                </div>
                                 <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Valor Taxa Venda:</label>
                                        <asp:TextBox ID="txtValorTaxaVenda" runat="server" CssClass="form-control moeda" ></asp:TextBox>
                                    </div>
                                </div>
                                 <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Valor Taxa Venda(Min):</label>
                                        <asp:TextBox ID="txtValorTaxaVendaMin" runat="server" CssClass="form-control moeda" ></asp:TextBox>
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
                                                        <asp:button runat="server" Text="Importar Taxas" id="btnImportar" CssClass="btn btn-success" />

                                                    </div>
                                                </div>
                            </div>
                                                <div class="tab-content">

                                <br />
                              <div class="alert alert-success" id="divDeleteTaxas" runat="server" visible="false">                                       
                      <asp:label ID="lblDeleteTaxas" runat="server"  /> 
                  </div>
         <div class="alert alert-danger" id="divDeleteErro" runat="server" visible="false">                                       
                      <asp:label ID="lblDeleteErro" runat="server"  /> 
                  </div>
                               <br />
                            <div class="table-responsive tableFixHead">
                                <asp:GridView ID="dgvTaxas" DataKeyNames="ID_TABELA_FRETE_TAXA" DataSourceID="dstaxas" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server"  AutoGenerateColumns="false"   style="max-height:400px; overflow:auto;" AllowSorting="true" OnSorting="dgvTaxas_Sorting" EmptyDataText="Nenhum registro encontrado.">
                                    <Columns>
                                        <asp:BoundField DataField="ID_TABELA_FRETE_TAXA" HeaderText="#"  SortExpression="ID_TABELA_FRETE_TAXA"/>
                                        <asp:BoundField DataField="ID_FRETE_TRANSPORTADOR" HeaderText="FRETE TRANSPORTADOR"  SortExpression="ID_FRETE_TRANSPORTADOR"/>
                                        <asp:BoundField DataField="NM_ITEM_DESPESA" HeaderText="ITEM DESPESA"  SortExpression="NM_ITEM_DESPESA"/>
                                        <asp:BoundField DataField="NM_ORIGEM_PAGAMENTO" HeaderText="ORIGEM DE PAGAMENTO"  SortExpression="NM_ORIGEM_PAGAMENTO" />
                                        <asp:BoundField DataField="NM_BASE_CALCULO_TAXA" HeaderText="BASE DE CALCULO"   SortExpression="NM_BASE_CALCULO_TAXA"/>
                                        <asp:BoundField DataField="MOEDACOMPRA" HeaderText="MOEDA COMPRA" SortExpression="MOEDACOMPRA" />
                                        <asp:BoundField DataField="VL_TAXA_COMPRA" HeaderText="VALOR TAXA DE COMPRA"  SortExpression="VL_TAXA_COMPRA"  />
                                        <asp:BoundField DataField="MOEDAVENDA" HeaderText="MOEDA VENDA"  SortExpression="MOEDAVENDA" />
                                        <asp:BoundField DataField="VL_TAXA_VENDA" HeaderText="VALOR TAXA DE VENDA" SortExpression="VL_TAXA_VENDA"  />      
                                        <asp:BoundField DataField="VL_TAXA_VENDA_MIN" HeaderText="VALOR TAXA DE VENDA(MIN)"  SortExpression="VL_TAXA_VENDA_MIN"  />
                                           <asp:TemplateField HeaderText="" >
                                         <ItemTemplate>                          
                            <asp:LinkButton ID="btnVisualizar" runat="server" CausesValidation="False" CommandName="visualizar" CommandArgument='<%# Eval("ID_TABELA_FRETE_TAXA") %>'  
                                Text="Visualizar" title="Editar"  CssClass="btn btn-info btn-sm" ><span class="glyphicon glyphicon-edit"  style="font-size:medium"></span></asp:LinkButton>
                                   </ItemTemplate>   
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>                    
                                   <ItemTemplate>                          
                            <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="False" CommandName="Excluir"  CommandArgument='<%# Eval("ID_TABELA_FRETE_TAXA") %>' 
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
                 <asp:AsyncPostBackTrigger ControlID="btnImportar" />

     

     </Triggers>   
     </asp:UpdatePanel>
                        </div>


                        </div>
                    </div>
                </div></div>



         <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary btn-block" Text="Gravar" style="display:none" />
    




    </div>
  <%--/ContentTemplate>
    <Triggers>
            <asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="dgvFreteTarifario" />
            <asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="dgvTaxas" />

    </Triggers>
</asp:UpdatePanel>--%>
       <asp:SqlDataSource ID="dsFreteTarifario" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT ID_TARIFARIO_FRETE_TRANSPORTADOR,
           A.ID_FRETE_TRANSPORTADOR,
           
		   A.ID_TIPO_CONTAINER,
		  (SELECT NM_TIPO_CONTAINER FROM TB_TIPO_CONTAINER WHERE ID_TIPO_CONTAINER = A.ID_TIPO_CONTAINER)NM_TIPO_CONTAINER,
          
		  
		  A.ID_TIPO_ESTUFAGEM,
          (SELECT NM_TIPO_ESTUFAGEM FROM TB_TIPO_ESTUFAGEM WHERE ID_TIPO_ESTUFAGEM = A.ID_TIPO_ESTUFAGEM)NM_TIPO_ESTUFAGEM,

 
		   A.DT_VALIDADE_INICIAL,
           A.VL_COMPRA,
           A.VL_MINIMO,
           A.QT_DIAS_FREETIME,
           A.FL_IMO,
           A.FL_CARGA_ESPECIAL,
           case when FL_CARGA_ESPECIAL = 1 then 'Sim' else 'Não' end as CARGA_ESPECIAL,
case when FL_IMO = 1 then 'Sim' else 'Não' end as IMO,

           A.VL_M3_INICIAL,
           A.VL_M3_FINAL 
FROM TB_TARIFARIO_FRETE_TRANSPORTADOR A
WHERE A.ID_FRETE_TRANSPORTADOR = @ID_FRETE_TRANSPORTADOR "         
           >
           <SelectParameters>
                <asp:ControlParameter Name="ID_FRETE_TRANSPORTADOR" Type="Int32" ControlID="txtID_FreteTransportador"  />
            </SelectParameters>
           
</asp:SqlDataSource>
       <asp:SqlDataSource ID="dsTaxas" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT ID_TABELA_FRETE_TAXA,A.ID_FRETE_TRANSPORTADOR,A.ID_TIPO_ESTUFAGEM,B.NM_TIPO_ESTUFAGEM,
A.ID_ITEM_DESPESA,C.NM_ITEM_DESPESA,A.ID_ORIGEM_PAGAMENTO,D.NM_ORIGEM_PAGAMENTO,A.ID_BASE_CALCULO_TAXA,E.NM_BASE_CALCULO_TAXA,A.ID_MOEDA_COMPRA,A.VL_TAXA_COMPRA,A.ID_MOEDA_VENDA,A.VL_TAXA_VENDA,A.VL_TAXA_VENDA_MIN, F.NM_MOEDA MOEDACOMPRA,G.NM_MOEDA MOEDAVENDA
FROM TB_TABELA_FRETE_TAXA A
LEFT JOIN TB_TIPO_ESTUFAGEM B ON B.ID_TIPO_ESTUFAGEM = A.ID_TIPO_ESTUFAGEM
LEFT JOIN TB_ITEM_DESPESA C ON C.ID_ITEM_DESPESA = A.ID_ITEM_DESPESA
LEFT JOIN TB_ORIGEM_PAGAMENTO D ON D.ID_ORIGEM_PAGAMENTO =A.ID_ORIGEM_PAGAMENTO
LEFT JOIN TB_BASE_CALCULO_TAXA E ON E.ID_BASE_CALCULO_TAXA = A.ID_BASE_CALCULO_TAXA
LEFT JOIN TB_MOEDA F ON F.ID_MOEDA = A.ID_MOEDA_COMPRA
LEFT JOIN TB_MOEDA G ON G.ID_MOEDA = A.ID_MOEDA_VENDA 
           WHERE ID_FRETE_TRANSPORTADOR = @ID_FRETE_TRANSPORTADOR">
           <SelectParameters>
                <asp:ControlParameter Name="ID_FRETE_TRANSPORTADOR" Type="Int32" ControlID="txtID_FreteTransportador"  />
            </SelectParameters>
</asp:SqlDataSource>
    <asp:SqlDataSource ID="dsOrigemPagamento" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT ID_ORIGEM_PAGAMENTO,NM_ORIGEM_PAGAMENTO FROM  [dbo].[TB_ORIGEM_PAGAMENTO]
union SELECT  0, 'Selecione' ORDER BY ID_ORIGEM_PAGAMENTO">
</asp:SqlDataSource>
    <asp:SqlDataSource ID="dsMoeda" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT ID_MOEDA, NM_MOEDA FROM [dbo].[TB_MOEDA] union SELECT  0, 'Selecione' ORDER BY ID_MOEDA">
</asp:SqlDataSource>
    <asp:SqlDataSource ID="dsItemDespesa" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT ID_ITEM_DESPESA,NM_ITEM_DESPESA FROM  [dbo].[TB_ITEM_DESPESA]
union SELECT  0, ' Selecione' FROM [dbo].[TB_ITEM_DESPESA] ORDER BY NM_ITEM_DESPESA">
</asp:SqlDataSource>
    <asp:SqlDataSource ID="dsBaseCalculo" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT ID_BASE_CALCULO_TAXA,NM_BASE_CALCULO_TAXA FROM [dbo].[TB_BASE_CALCULO_TAXA]
union SELECT  0, 'Selecione' ORDER BY ID_BASE_CALCULO_TAXA">
</asp:SqlDataSource>
        <asp:SqlDataSource ID="dsPorto" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT ID_PORTO, NM_PORTO + ' - ' + CONVERT(VARCHAR,ID_PORTO) AS NM_PORTO FROM [dbo].[TB_PORTO] union SELECT  0, 'Selecione' ORDER BY NM_PORTO ">
</asp:SqlDataSource>
    <asp:SqlDataSource ID="dsComex" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT ID_TIPO_COMEX,NM_TIPO_COMEX FROM [dbo].[TB_TIPO_COMEX]
union SELECT  0, 'Selecione' ORDER BY ID_TIPO_COMEX">
</asp:SqlDataSource>
    <asp:SqlDataSource ID="dsRota" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT ID_VIA_ROTA,NM_VIA_ROTA FROM [dbo].[TB_VIA_ROTA]
union SELECT  0, 'Selecione' ORDER BY ID_VIA_ROTA">
</asp:SqlDataSource>
    <asp:SqlDataSource ID="dsTransportador" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT ID_PARCEIRO, NM_RAZAO FROM [dbo].[TB_PARCEIRO] WHERE FL_TRANSPORTADOR  = 1
union SELECT  0, 'Selecione' ORDER BY ID_PARCEIRO">
</asp:SqlDataSource>
    <asp:SqlDataSource ID="dsAgente" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT ID_PARCEIRO, NM_RAZAO FROM [dbo].[TB_PARCEIRO] WHERE FL_AGENTE_INTERNACIONAL = 1
union SELECT  0, 'Selecione' ORDER BY ID_PARCEIRO">
</asp:SqlDataSource>
     <asp:SqlDataSource ID="dsFrequencia" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT ID_TIPO_FREQUENCIA, NM_TIPO_FREQUENCIA FROM [dbo].[TB_TIPO_FREQUENCIA] 
union SELECT  0, 'Selecione' ORDER BY ID_TIPO_FREQUENCIA">
</asp:SqlDataSource>
    <asp:SqlDataSource ID="dsCarga" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT ID_TIPO_CARGA, NM_TIPO_CARGA FROM [dbo].[TB_TIPO_CARGA] WHERE FL_ATIVO = 1
union SELECT  0, 'Selecione' ORDER BY ID_TIPO_CARGA">
</asp:SqlDataSource>
    <asp:SqlDataSource ID="dsContainer" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT ID_TIPO_CONTAINER, NM_TIPO_CONTAINER FROM TB_TIPO_CONTAINER WHERE FL_ATIVO = 1
union SELECT  0, 'Selecione' ORDER BY ID_TIPO_CONTAINER">
</asp:SqlDataSource>
    <asp:SqlDataSource ID="dsMercadoria" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT ID_MERCADORIA, NM_MERCADORIA FROM [dbo].[TB_MERCADORIA] 
union SELECT  0, 'Selecione' ORDER BY ID_MERCADORIA">
</asp:SqlDataSource>
    <asp:SqlDataSource ID="dsEstufagem" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT ID_TIPO_ESTUFAGEM, NM_TIPO_ESTUFAGEM FROM [dbo].[TB_TIPO_ESTUFAGEM] 
union SELECT  0, 'Selecione' ORDER BY ID_TIPO_ESTUFAGEM">
</asp:SqlDataSource>
    <asp:SqlDataSource ID="dsViaTransporte" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT ID_VIATRANSPORTE,NM_VIATRANSPORTE FROM [dbo].[TB_VIATRANSPORTE]
union SELECT  0, 'Selecione' ORDER BY ID_VIATRANSPORTE">
</asp:SqlDataSource>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">
    <script>
        $(document).ready(function () {
            $("#fcl").click(function (event) {
                event.preventDefault();
                $('.txtEstufagem').val("1"); // Para definir

            });
        });


        $(document).ready(function () {
            $("#lcl").click(function (event) {
                event.preventDefault();
                $('.txtEstufagem').val("2"); // Para definir

            });
        });
    </script>
</asp:Content>
