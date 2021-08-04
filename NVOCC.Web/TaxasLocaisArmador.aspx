<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="TaxasLocaisArmador.aspx.vb" Inherits="NVOCC.Web.TaxasLocaisArmador" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
      <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
    <ContentTemplate> 
 <div class="row principal">
     <ajaxToolkit:ModalPopupExtender id="mpe" runat="server" PopupControlID="Panel1" TargetControlID="Button1"  CancelControlID="Button2"></ajaxToolkit:ModalPopupExtender>
     
  
    <asp:Button runat="server" Text="teste" id="Button1" style="display:none" CssClass="btn btn-success" />
    <asp:Button runat="server" Text="teste" id="Button2" style="display:none" CssClass="btn btn-success" />


     <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" style="display:none" >     
     <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
    <ContentTemplate> 
                                                <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title" id="modalFCLimpoTitle">TAXAS LOCAIS ARMADOR</h5><asp:Linkbutton ID="lkAnterior" runat="server" BackColor="White" ForeColor="Black" style="float: right;" CssClass="btn btn-default"  ><i class="glyphicon glyphicon-step-backward"></i></asp:Linkbutton><asp:Linkbutton ID="lkProximo" runat="server" BackColor="White" ForeColor="Black" style="float: right;" CssClass="btn btn-default"  ><i class="glyphicon glyphicon-step-forward"></i></asp:Linkbutton>
                                                        </div>
                                                        <div class="modal-body">                                                           
                                    <div class="alert alert-success" ID="divSuccess" runat="server" visible="false">
                                        <asp:label ID="lblmsgSuccess" runat="server"></asp:label>
                                    </div>
                                                            
                                    <div class="alert alert-danger" ID="divErro" runat="server" visible="false">
                                        <asp:label ID="lblmsgErro" runat="server"></asp:label>
                                    </div>
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <label class="control-label">Código:</label>
                                        <asp:TextBox ID="txtIDTaxa" runat="server" Enabled="false" CssClass="form-control" ></asp:TextBox>
                                    </div>     
                                </div>
                                 <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Transportador:</label><label runat="server" style="color:red" >*</label>
                                        <asp:DropDownList ID="ddlTransportadorTaxa" runat="server" Enabled="false" CssClass="form-control" Font-Size="11px" DataTextField="NM_RAZAO" DataSourceID="dsTransportador" DataValueField="ID_PARCEIRO"></asp:DropDownList>            </div>
                                    </div>
                            </div>
                            <div class="row">
                               

                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Porto de Destino:</label><label runat="server" style="color:red" >*</label>
                                        <asp:DropDownList ID="ddlPortoTaxa" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_PORTO" DataSourceID="dsPorto" DataValueField="ID_PORTO"></asp:DropDownList>              </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Origem Serviço:</label><label runat="server" style="color:red" >*</label>
                                        <asp:DropDownList ID="ddlOrigemPagamento" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_ORIGEM_PAGAMENTO" DataSourceID="dsOrigemPagamento" DataValueField="ID_ORIGEM_PAGAMENTO" >
                                        </asp:DropDownList>             </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Via Transporte:</label><label runat="server" style="color:red" >*</label>
                                        <asp:DropDownList ID="ddlViaTransporte" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_VIATRANSPORTE" DataSourceID="dsViaTransporte" DataValueField="ID_VIATRANSPORTE" >
                                        </asp:DropDownList>                                    </div>
                                </div>
                            </div>
                            <div class="row">                                
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Tipo Comex:</label><label runat="server" style="color:red" >*</label>
                                        <asp:DropDownList ID="ddlComexTaxa" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_TIPO_COMEX" DataSourceID="dsComex" DataValueField="ID_TIPO_COMEX">
                                        </asp:DropDownList>                                    </div>
                                </div>
                               <%-- <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Continente:</label><label runat="server" style="color:red" >*</label>
                                        <asp:DropDownList ID="ddlContinenteTaxa" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_CONTINENTE" DataSourceID="dsContinente" DataValueField="ID_CONTINENTE">
                                        </asp:DropDownList>                                    </div>
                                </div>--%>
                                  <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Item de Despesa:</label><label runat="server" style="color:red" >*</label>
                                        <asp:DropDownList ID="ddlDespesaTaxa" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_ITEM_DESPESA" DataSourceID="dsItemDespesa" DataValueField="ID_ITEM_DESPESA" >
                                        </asp:DropDownList>                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Data de Validade (Inicial):</label><label runat="server" style="color:red" >*</label>
                                        <asp:TextBox ID="txtValidadeInicialTaxa" runat="server" CssClass="form-control data" ></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">

                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Valor Taxa Local:</label><label runat="server" style="color:red" >*</label>
                                        <asp:TextBox ID="txtValorTaxaLocal" runat="server" CssClass="form-control moeda" ></asp:TextBox>
                                    </div>
                                </div>
                                    <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Moeda:</label><label runat="server" style="color:red" >*</label>
                                        <asp:DropDownList ID="ddlMoeda" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_MOEDA" DataSourceID="dsMoeda" DataValueField="ID_MOEDA"  >
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                    <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Base de calculo:</label><label runat="server" style="color:red" >*</label>
                                        <asp:DropDownList ID="ddlBaseCalculo" runat="server" CssClass="form-control" Font-Size="11px"  DataTextField="NM_BASE_CALCULO_TAXA" DataSourceID="dsBaseCalculo" DataValueField="ID_BASE_CALCULO_TAXA" >
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                 
                                
                            </div></div>
                               <div class="modal-footer">
                                                            <asp:Button runat="server" Text="Salvar Taxa" id="btnSalvar" CssClass="btn btn-success" />
                                                            <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFechar" text="Close" />
                                                        </div>
                                                    
                                                </div>
      
                                       </div>            
      </ContentTemplate>
 <Triggers>
     <asp:AsyncPostBackTrigger  ControlID="lkProximo" />
          <asp:AsyncPostBackTrigger  ControlID="lkAnterior" />

            <asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="dgvTaxas" />
            <asp:AsyncPostBackTrigger  ControlID="btnSalvar" />
     </Triggers>   
     </asp:UpdatePanel>
     </asp:Panel>

 





      <ajaxToolkit:ModalPopupExtender id="mpeNovo" runat="server" PopupControlID="Panel2" TargetControlID="btnNovo"  CancelControlID="Button1"></ajaxToolkit:ModalPopupExtender>
     
   <asp:Panel ID="Panel2" runat="server" CssClass="modalPopup" style="display:none" >     
         <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
    <ContentTemplate> 
                                                <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title">TAXAS LOCAIS ARMADOR</h5>
                                                        </div>
                                                        <div class="modal-body">                                                           
                                    <div class="alert alert-success" ID="divSuccessNovo" runat="server" visible="false">
                                        <asp:label ID="lblmsgSuccessNovo" runat="server" Text="Registro cadastrado/atualizado com sucesso!"></asp:label>
                                    </div>
                                    <div class="alert alert-danger" ID="divErroNovo" runat="server" visible="false">
                                        <asp:label ID="lblmsgErroNovo" runat="server"></asp:label>
                                    </div>
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <label class="control-label">Código:</label>
                                        <asp:TextBox ID="txtIDTaxaNovo" runat="server" Enabled="false" CssClass="form-control" ></asp:TextBox>
                                    </div>     
                                </div> 
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Transportador:</label><label runat="server" style="color:red" >*</label>
                                        <asp:DropDownList ID="ddlTransportadorTaxaNovo" Enabled="false" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_RAZAO" DataSourceID="dsTransportador" DataValueField="ID_PARCEIRO"></asp:DropDownList>            </div>
                                    </div>
                            </div>
                            <div class="row">
                               

                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Porto Destino:</label><label runat="server" style="color:red" >*</label>
                                        <asp:DropDownList ID="ddlPortoTaxaNovo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_PORTO" DataSourceID="dsPorto" DataValueField="ID_PORTO"></asp:DropDownList>              </div>
                                </div>
                                 <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Origem Serviço:</label><label runat="server" style="color:red" >*</label>
                                        <asp:DropDownList ID="ddlOrigemPagamentoNovo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_ORIGEM_PAGAMENTO" DataSourceID="dsOrigemPagamento" DataValueField="ID_ORIGEM_PAGAMENTO" >
                                        </asp:DropDownList>             </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Via Transporte:</label><label runat="server" style="color:red" >*</label>
                                        <asp:DropDownList ID="ddlViaTransporteNovo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_VIATRANSPORTE" DataSourceID="dsViaTransporte" DataValueField="ID_VIATRANSPORTE" >
                                        </asp:DropDownList>                                    </div>
                                </div>
                            </div>
                            <div class="row">                                
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Tipo Comex:</label><label runat="server" style="color:red" >*</label>
                                        <asp:DropDownList ID="ddlComexTaxaNovo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_TIPO_COMEX" DataSourceID="dsComex" DataValueField="ID_TIPO_COMEX">
                                        </asp:DropDownList>                                    </div>
                                </div>
                               <%-- <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Continente:</label><label runat="server" style="color:red" >*</label>
                                        <asp:DropDownList ID="ddlContinenteTaxaNovo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_CONTINENTE" DataSourceID="dsContinente" DataValueField="ID_CONTINENTE">
                                        </asp:DropDownList>                                    </div>
                                </div>--%>
                                  <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Item de Despesa:</label><label runat="server" style="color:red" >*</label>
                                        <asp:DropDownList ID="ddlDespesaTaxaNovo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_ITEM_DESPESA" DataSourceID="dsItemDespesa" DataValueField="ID_ITEM_DESPESA" >
                                        </asp:DropDownList>                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Data de Validade (Inicial):</label><label runat="server" style="color:red" >*</label>
                                        <asp:TextBox ID="txtValidadeInicialTaxaNovo" runat="server" CssClass="form-control data" ></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">

                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Valor Taxa Local:</label><label runat="server" style="color:red" >*</label>
                                        <asp:TextBox ID="txtValorTaxaLocalNovo" runat="server" CssClass="form-control moeda" ></asp:TextBox>
                                    </div>
                                </div>
                                 <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Moeda:</label><label runat="server" style="color:red" >*</label>
                                        <asp:DropDownList ID="ddlMoedaNovo" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_MOEDA" DataSourceID="dsMoeda" DataValueField="ID_MOEDA"  >
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Base de calculo:</label><label runat="server" style="color:red" >*</label>
                                        <asp:DropDownList ID="ddlBaseCalculoNovo" runat="server" CssClass="form-control" Font-Size="11px"  DataTextField="NM_BASE_CALCULO_TAXA" DataSourceID="dsBaseCalculo" DataValueField="ID_BASE_CALCULO_TAXA" >
                                        </asp:DropDownList>
                                    </div>
                            </div>
                            </div></div>
                               <div class="modal-footer">
                                                            <asp:Button runat="server" Text="Salvar Taxa" id="btnSalvarNovo" CssClass="btn btn-success" />
                                                            <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFecharNovo" text="Close" />
                                                        </div>
                                                    
                                                </div>
      
                                       </div>            
      </ContentTemplate>
 <Triggers>
            <asp:AsyncPostBackTrigger  ControlID="btnSalvarNovo" />
                 <asp:AsyncPostBackTrigger  ControlID="btnFecharNovo" />
      
     </Triggers>   
     </asp:UpdatePanel>
     </asp:Panel>
                                                               


   








      
                     
        <div class="col-lg-12 ">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">TAXAS LOCAIS ARMADOR
                    </h3>                  
                </div>

                                                                        <div class="row" style="padding:10px">                        
                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <label class="control-label">Consultar por:</label>
                                         <asp:DropDownList ID="ddlConsulta" runat="server" CssClass="form-control" Font-Size="11px" AutoPostBack="True">
                                            <asp:ListItem Value="0" Selected="True">Selecione</asp:ListItem>
                                            <asp:ListItem Value="1">Porto</asp:ListItem>
                                            <asp:ListItem Value="3">Comex</asp:ListItem>
                                            <asp:ListItem Value="2">Via Transporte</asp:ListItem>

                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-2" id="divPesquisa" runat="server">
                                    <div class="form-group">   
                                        <label class="control-label">Pesquisar</label>
                                        <asp:TextBox ID="txtConsulta" runat="server" autopostback="true" CssClass="form-control"></asp:TextBox>
                                        <asp:label ID="msgerro" runat="server" style ="color:red" />
                                    </div>                                   
                                </div>
                       </div>

                <div class="panel-body">                                   
                            <br />
                             <div class="row">
                                     
                                  <div class="col-sm-4"">
                                                    <div class="form-group">
                                                        <asp:button runat="server" Text="Nova Taxa" id="btnNovo" CssClass="btn btn-primary" />
                                                    </div>
                                                </div>
                            </div>
                                
                                    <br /> 
        <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
    <ContentTemplate> 
        <div class="alert alert-success" ID="divExcluir_Success" runat="server" visible="false">
                                        <asp:label ID="lblExcluir_Success" runat="server" Text="Registro deletado com sucesso!"></asp:label>
                                    </div>

        <div class="alert alert-danger" ID="divExcluir_Erro" runat="server" visible="false">
                                        <asp:label ID="lblExcluir_Erro" runat="server" ></asp:label>
                                    </div>
                            <div class="table-responsive tableFixHead" id="divGrid" runat="server">
                                
                                <asp:GridView ID="dgvTaxas" DataKeyNames="ID_TAXA_LOCAL_TRANSPORTADOR" DataSourceID="dstaxas" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server"  AutoGenerateColumns="false"  style="max-height:400px; overflow:auto;" AllowSorting="true" OnSorting="dgvTaxas_Sorting" >
                                    <Columns> 
                                                                               
                                        <asp:TemplateField HeaderText="" >
                                         <ItemTemplate>                          
                            <asp:LinkButton ID="btnExcluir" title="Excluir" runat="server" CssClass="btn btn-danger btn-sm" CommandName="Excluir" OnClientClick="javascript:return confirm('Deseja realmente excluir este registro?');" CommandArgument='<%# Eval("ID_TAXA_LOCAL_TRANSPORTADOR") %>' Autopostback="true"><span class="glyphicon glyphicon-trash"></span></asp:LinkButton>
                                   </ItemTemplate>
                                             <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="" >
                                         <ItemTemplate>                          
                            <asp:LinkButton ID="btnVisualizar" runat="server" CausesValidation="False" CommandName="visualizar" CommandArgument='<%# Eval("ID_TAXA_LOCAL_TRANSPORTADOR") %>' Text="Visualizar"  CssClass="btn btn-primary btn-sm" ><i class="fas fa-eye"></i></div></asp:LinkButton>
                                   </ItemTemplate>  
                                             <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="" >
                                        <ItemTemplate>
                                                                        <asp:LinkButton ID="btnDuplicar" runat="server" CausesValidation="False" CommandName="Duplicar" CommandArgument='<%# Eval("ID_TAXA_LOCAL_TRANSPORTADOR") %>'
                                                                            Text="Duplicar" CssClass="btn btn-warning btn-sm"><i class="glyphicon glyphicon-duplicate"></i></div></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                                          <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />

                                                                             </asp:TemplateField>
                                        
                                        <asp:BoundField DataField="ID_TAXA_LOCAL_TRANSPORTADOR" HeaderText="#"  SortExpression="ID_TAXA_LOCAL_TRANSPORTADOR"/>
                                        <asp:BoundField DataField="NM_PORTO" HeaderText="Porto"  SortExpression="NM_PORTO"/>
                                        <asp:BoundField DataField="NM_TIPO_COMEX" HeaderText="Tipo Comex" SortExpression="NM_TIPO_COMEX" />
                                        <asp:BoundField DataField="NM_VIATRANSPORTE" HeaderText="Transporte" SortExpression="NM_VIATRANSPORTE" />
                                        <asp:BoundField DataField="NM_ITEM_DESPESA" HeaderText="Item Despesa" SortExpression="NM_ITEM_DESPESA" />
                                        <asp:BoundField DataField="VL_TAXA_LOCAL_COMPRA" HeaderText="Valor Taxa Local(Compra)" SortExpression="VL_TAXA_LOCAL_COMPRA" />
                                        <asp:BoundField DataField="DT_VALIDADE_INICIAL" HeaderText="Data de Validade (Inicial)" SortExpression="DT_VALIDADE_INICIAL" DataFormatString="{0:dd/MM/yyyy}" />                                
                                    </Columns>
                                    <HeaderStyle CssClass="headerStyle" />
                                </asp:GridView>
                            </div>        
         </ContentTemplate>
 <Triggers>
       <asp:AsyncPostBackTrigger EventName="Sorting" ControlID="dgvTaxas" />
            <asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="dgvTaxas" />
                    <asp:AsyncPostBackTrigger  ControlID="btnSalvar" />
                               <asp:AsyncPostBackTrigger  ControlID="btnSalvarNovo" />
                                    <asp:AsyncPostBackTrigger  ControlID="txtConsulta" />

<%--            <asp:AsyncPostBackTrigger  ControlID="btnFechar" />
            <asp:AsyncPostBackTrigger  ControlID="btnFecharNovo" />--%>


</Triggers>
   </asp:UpdatePanel>
  
                                
 
</div>

            </div></div>
    
    

         
   
     <asp:SqlDataSource ID="dsParceiros" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT ID_PARCEIRO as Id, CNPJ , NM_RAZAO RazaoSocial FROM TB_PARCEIRO #FILTRO ORDER BY ID_PARCEIRO">
</asp:SqlDataSource>
    <asp:SqlDataSource ID="dsTaxas" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT A.ID_TAXA_LOCAL_TRANSPORTADOR,
A.ID_TRANSPORTADOR,
A.ID_PORTO,B.NM_PORTO,
A.ID_TIPO_COMEX,D.NM_TIPO_COMEX,
A.ID_VIATRANSPORTE,C.NM_VIATRANSPORTE,
A.ID_ITEM_DESPESA,F.NM_ITEM_DESPESA,
A.VL_TAXA_LOCAL_COMPRA,
A.DT_VALIDADE_INICIAL 
FROM 
TB_TAXA_LOCAL_TRANSPORTADOR A 
LEFT JOIN TB_PORTO B ON B.ID_PORTO = A.ID_PORTO
LEFT JOIN TB_VIATRANSPORTE C ON C.ID_VIATRANSPORTE = A.ID_VIATRANSPORTE
LEFT JOIN TB_TIPO_COMEX D ON D.ID_TIPO_COMEX = A.ID_TIPO_COMEX
LEFT JOIN TB_ITEM_DESPESA F ON F.ID_ITEM_DESPESA = A.ID_ITEM_DESPESA
        WHERE ID_TRANSPORTADOR = @ID">
           <SelectParameters>
                <asp:Parameter Name="ID" Type="Int32"  />
            </SelectParameters>

</asp:SqlDataSource>
      <asp:SqlDataSource ID="dsPorto" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
         selectcommand="SELECT ID_PORTO, NM_PORTO + ' - ' + CONVERT(VARCHAR,ID_PORTO) AS NM_PORTO FROM [dbo].[TB_PORTO]  WHERE NM_PORTO IS NOT NULL 
          union SELECT  0, ' Selecione' ORDER BY NM_PORTO ">
</asp:SqlDataSource>
    <asp:SqlDataSource ID="dsComex" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT ID_TIPO_COMEX,NM_TIPO_COMEX FROM [dbo].[TB_TIPO_COMEX]
union SELECT  0, 'Selecione' ORDER BY ID_TIPO_COMEX">
</asp:SqlDataSource>
        <asp:SqlDataSource ID="dsItemDespesa" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT ID_ITEM_DESPESA,NM_ITEM_DESPESA FROM  [dbo].[TB_ITEM_DESPESA]
union SELECT  0, ' Selecione' FROM [dbo].[TB_ITEM_DESPESA] ORDER BY NM_ITEM_DESPESA">
</asp:SqlDataSource>
    <asp:SqlDataSource ID="dsContinente" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT ID_CONTINENTE,NM_CONTINENTE FROM [dbo].[TB_CONTINENTE]
union SELECT  0, 'Selecione' ORDER BY ID_CONTINENTE">
</asp:SqlDataSource>
        <asp:SqlDataSource ID="dsViaTransporte" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT ID_VIATRANSPORTE,NM_VIATRANSPORTE FROM [dbo].[TB_VIATRANSPORTE]
union SELECT  0, 'Selecione' ORDER BY ID_VIATRANSPORTE">
</asp:SqlDataSource>
    <asp:SqlDataSource ID="dsTransportador" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT ID_PARCEIRO, NM_RAZAO FROM [dbo].[TB_PARCEIRO] WHERE FL_TRANSPORTADOR  = 1 
union SELECT  0, 'Selecione'  ORDER BY ID_PARCEIRO">
</asp:SqlDataSource>
     <asp:SqlDataSource ID="dsContainer" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT ID_TIPO_CONTAINER, NM_TIPO_CONTAINER FROM TB_TIPO_CONTAINER WHERE FL_ATIVO = 1
union SELECT  0, 'Selecione'  ORDER BY ID_TIPO_CONTAINER">
</asp:SqlDataSource>
  
        <asp:SqlDataSource ID="dsOrigemPagamento" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT ID_ORIGEM_PAGAMENTO,NM_ORIGEM_PAGAMENTO FROM  [dbo].[TB_ORIGEM_PAGAMENTO]
union SELECT  0, 'Selecione' ORDER BY ID_ORIGEM_PAGAMENTO">
</asp:SqlDataSource> 

 </div>
        </ContentTemplate>


          <Triggers>     
       <asp:AsyncPostBackTrigger  ControlID="btnNovo" />
            <asp:AsyncPostBackTrigger  ControlID="btnFechar" />
            <asp:AsyncPostBackTrigger  ControlID="btnFecharNovo" />

       <asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="dgvTaxas" />

</Triggers>
   </asp:UpdatePanel>


    <asp:SqlDataSource ID="dsMoeda" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT ID_MOEDA, NM_MOEDA FROM [dbo].[TB_MOEDA] union SELECT  0, 'Selecione'  ORDER BY ID_MOEDA">
</asp:SqlDataSource>

        <asp:SqlDataSource ID="dsBaseCalculo" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT ID_BASE_CALCULO_TAXA,NM_BASE_CALCULO_TAXA FROM [dbo].[TB_BASE_CALCULO_TAXA]
union SELECT  0, 'Selecione' ORDER BY ID_BASE_CALCULO_TAXA">
</asp:SqlDataSource>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
