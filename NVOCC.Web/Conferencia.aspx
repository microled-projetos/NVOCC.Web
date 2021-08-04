<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Conferencia.aspx.vb" Inherits="NVOCC.Web.Conferencia" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
         #imgFundo {
            display: none;
        }
        </style>
    <div style="display:none">
    <asp:Label ID="lblIDINVOICE"  runat="server"/>
        <asp:Label ID="lblID_BL"  runat="server"/>
        <asp:Label ID="lblGrau"  runat="server"/>
        </div>
            <div class="row principal">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">CONFERÊNCIA
                    </h3>
                </div>
                <div class="panel-body">
                    <ul class="nav nav-tabs" role="tablist">
                        <li class="active">
                            <a href="#Master" role="tab" data-toggle="tab">
                                <i class="fa fa-edit" style="padding-right: 8px;"></i>Master
                            </a>
                        </li>
                        <li>
                            <a href="#House" role="tab" data-toggle="tab">
                                <i class="fa fa-edit" style="padding-right: 8px;"></i>House
                            </a>
                        </li>
                    </ul>

                    <div class="tab-content">
                        <div class="tab-pane fade active in" id="Master">
                            <br />
                            <div runat="server" id="divMaster">

                            <div class="row">
                                <div class="linha-colorida">MASTER</div>
                                       <div class="col-sm-2">
                                    <div class="form-group">
                                        <strong>MBL:</strong>&nbsp;<asp:Label ID="lblMBL"  runat="server"/>
                                        </div>
                                           </div>
                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <strong>TIPO FRETE:</strong>&nbsp;<asp:Label ID="lblTipoFrete"  runat="server"/>
                                        </div>
                                           </div>
                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <strong>EMBARQUE:</strong>&nbsp;<asp:Label ID="lblEmbarque"  runat="server"/>
                                        </div>
                                           </div>

                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <strong>SEM DEVOLUÇÃO DO FRETE:</strong>&nbsp;
                                        </div>
                                           </div>
                                </div>
                             <div class="row">
                                       <div class="col-sm-2">
                                    <div class="form-group">
                                        <strong>MOEDA:</strong>&nbsp;<asp:Label ID="lblMoeda"  runat="server"/>
                                        </div>
                                           </div>
                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <strong>TIPO ESTUFAGEM:</strong>&nbsp;<asp:Label ID="lblEstufagem"  runat="server"/>
                                        </div>
                                           </div>
                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <strong>CHEGADA:</strong>&nbsp;<asp:Label ID="lblChegada"  runat="server"/>
                                        </div>
                                           </div>

                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <strong>FREE HAND:</strong>&nbsp;<asp:Label ID="lblFreeHand"  runat="server"/>
                                        </div>
                                           </div>
                                </div>
                            <br />                            <br />
                            <br />

                             <div class="row">
                                       <div class="col-sm-8">
                                    <div class="form-group">                                        <h5>DEVOLUÇÃO FRETE</h5>

                                                                                  <asp:GridView ID="dgvDevolucaoMBL" DataKeyNames="ID_BL_TAXA" DataSourceID="dsDevolucao" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" EmptyDataText="Nenhum registro encontrado." AllowPaging="true" PageSize="10">
                                            <Columns>
                                                <asp:TemplateField HeaderText="ID" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID_BL_TAXA") %>'  />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                               <asp:BoundField DataField="NR_PROCESSO" HeaderText="PROCESSO" SortExpression="NR_PROCESSO" />                                                     <asp:BoundField DataField="SIGLA_MOEDA" HeaderText="MOEDA" SortExpression="SIGLA_MOEDA" />                        
                                                <asp:TemplateField HeaderText="VALOR COMPRA" SortExpression="VL_COMPRA">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblValorCompra" runat="server" Text='<%# Eval("VL_COMPRA") %>'  />
                                                    </ItemTemplate>
                                                </asp:TemplateField>    
                                                 <asp:TemplateField HeaderText="VALOR VENDA" SortExpression="VL_VENDA">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblValorVenda" runat="server" Text='<%# Eval("VL_VENDA") %>'  />
                                                    </ItemTemplate>
                                                </asp:TemplateField>    
                                                                                               <asp:BoundField DataField="DT_RECEBIMENTO" HeaderText="DATA RECEBIMENTO" SortExpression="DT_RECEBIMENTO" />
                                       
                                            </Columns>
                                            <HeaderStyle CssClass="headerStyle" />
                                        </asp:GridView>
                                        </div>
                                           </div>
                                <div class="col-sm-4">
                                    <div class="form-group">
                                                                                <h5>DEVOLUÇÃO AGENTE</h5>

<asp:GridView ID="dgvComissoesMBL" DataKeyNames="ID_BL_TAXA" DataSourceID="dsComissoes" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" EmptyDataText="Nenhum registro encontrado." AllowPaging="true" PageSize="10">
                                            <Columns>
                                               <asp:TemplateField HeaderText="ID" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID_BL_TAXA") %>'  />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="NR_PROCESSO" HeaderText="PROCESSO" SortExpression="NR_PROCESSO" />
             
                                                <asp:BoundField DataField="SIGLA_MOEDA" HeaderText="MOEDA" SortExpression="SIGLA_MOEDA" />                          <asp:TemplateField HeaderText="VALOR" SortExpression="VL_TAXA">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblValor" runat="server" Text='<%# Eval("VL_TAXA") %>'  />
                                                    </ItemTemplate>
                                                </asp:TemplateField>         
                                                                                           
                                            </Columns>
                                            <HeaderStyle CssClass="headerStyle" />
                                        </asp:GridView>
                                        </div>
                                           </div>
                                
                                </div>
                                                        <br />

                            <div class="row">
                                       <div class="col-sm-12">
                                    <div class="form-group">
                                                                                <h5>TAXAS</h5>

                                          <asp:GridView ID="dgvOutrasTaxasMBL" DataKeyNames="ID_BL_TAXA" DataSourceID="dsOutrasTaxas" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" EmptyDataText="Nenhum registro encontrado." AllowPaging="true" PageSize="10">
                                            <Columns>
                                                 <asp:TemplateField HeaderText="ID" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID_BL_TAXA") %>'  />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="NR_PROCESSO" HeaderText="PROCESSO" SortExpression="NR_PROCESSO" />
                                                <asp:BoundField DataField="NM_ITEM_DESPESA" HeaderText="DESPESA" SortExpression="NM_ITEM_DESPESA" />
                                                <asp:BoundField DataField="CD_DECLARADO" HeaderText="DECLARADO" SortExpression="CD_DECLARADO" />
                                                <asp:BoundField DataField="SIGLA_MOEDA" HeaderText="MOEDA" SortExpression="SIGLA_MOEDA" />       
                                                               <asp:TemplateField HeaderText="VALOR" SortExpression="VL_TAXA" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblValor" runat="server" Text='<%# Eval("VL_TAXA") %>'  />
                                                    </ItemTemplate>
                                                </asp:TemplateField>                                <asp:BoundField DataField="DT_RECEBIMENTO" HeaderText="DATA RECEBIMENTO" SortExpression="DT_RECEBIMENTO" />

                                            </Columns>
                                            <HeaderStyle CssClass="headerStyle" />
                                        </asp:GridView>
                                        </div>
                                           </div>
                                
                                </div>
                                                        <br />

                            <div class="row">
                                       <div class="col-sm-12">
                                    <div class="form-group">
                                        <h5>INVOICES</h5>
                                            <asp:GridView ID="dgvInvoiceMBL" DataKeyNames="ID_ACCOUNT_INVOICE" DataSourceID="dsInvoice" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" EmptyDataText="Nenhum registro encontrado." AllowPaging="true" PageSize="10">
                                                <Columns>
                                                    <asp:BoundField DataField="NR_INVOICE" HeaderText="Nº INVOICE" SortExpression="NR_INVOICE" />
                                                    <asp:BoundField DataField="NM_ACCOUNT_TIPO_INVOICE" HeaderText="TIPO" SortExpression="NM_ACCOUNT_TIPO_INVOICE" />
                                                    <asp:BoundField DataField="NM_ACCOUNT_TIPO_EMISSOR" HeaderText="EMISSOR" SortExpression="NM_ACCOUNT_TIPO_EMISSOR" />
                                                    <asp:BoundField DataField="NM_ACCOUNT_TIPO_FATURA" HeaderText="TIPO FATURA" SortExpression="NM_ACCOUNT_TIPO_FATURA" />
                                                    <asp:BoundField DataField="SIGLA_MOEDA" HeaderText="MOEDA" SortExpression="SIGLA_MOEDA" />
                                                       <asp:BoundField DataField="NM_ACCOUNT_TIPO_FATURA" HeaderText="TIPO FATURA" SortExpression="NM_ACCOUNT_TIPO_FATURA" />
                                                    <asp:BoundField DataField="VALOR_FRETE" HeaderText="FRETE" SortExpression="VALOR_FRETE" />
                                                       <asp:BoundField DataField="VALOR_TAXA_EXTERIOR" HeaderText="TAXA EXTERIOR" SortExpression="VALOR_TAXA_EXTERIOR" />
                                                    <asp:BoundField DataField="VALOR_TAXA_DECLADADA" HeaderText="TAXA DECLADADA" SortExpression="VALOR_TAXA_DECLADADA" />
                                                       <asp:BoundField DataField="VALOR_OUTRAS_TAXAS" HeaderText="OUTRAS TAXAS" SortExpression="VALOR_OUTRAS_TAXAS" />
                                                    <asp:BoundField DataField="VALOR_COMISSAO" HeaderText="COMISSAO" SortExpression="VALOR_COMISSAO" />

                                                </Columns>
                                                <HeaderStyle CssClass="headerStyle" />
                                            </asp:GridView>
                                        </div>
                                           </div>
                               
                                
                                </div>
                                                          
                            </div>
                        </div>




                        <div class="tab-pane fade" id="House">
                            <br />

<asp:UpdatePanel ID="updPainel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True">
    <ContentTemplate>

                            <div runat="server" id="divHouse">
                             <div class="row">
                                                                         <div class="linha-colorida">MASTER</div>

                                       <div class="col-sm-2">
                                    <div class="form-group">
                                        <strong>MBL:</strong>&nbsp;<asp:Label ID="lblMBL_Master"  runat="server"/>
                                        </div>
                                           </div>
                                 <div class="col-sm-2">
                                    <div class="form-group">
                                        <strong>MOEDA:</strong>&nbsp;<asp:Label ID="lblMoeda_Master"  runat="server"/>
                                        </div>
                                           </div>
                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <strong>TIPO FRETE:</strong>&nbsp;<asp:Label ID="lblTipoFrete_Master"  runat="server"/>
                                        </div>
                                           </div>
                                 <div class="col-sm-2">
                                    <div class="form-group">
                                        <strong>ESTUFAGEM:</strong>&nbsp;<asp:Label ID="lblEstufagem_Master"  runat="server"/>
                                        </div>
                                           </div>
                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <strong>EMBARQUE:</strong>&nbsp;<asp:Label ID="lblEmbarque_Master"  runat="server"/>
                                        </div>
                                           </div>

                                <div class="col-sm-2">
                                    <div class="form-group">
                                          <strong>CHEGADA:</strong>&nbsp;<asp:Label ID="lblChegada_Master"  runat="server"/>
                                        </div>
                                           </div>
                                </div>
                             <div class="row">
                             <div class="linha-colorida">HOUSE</div>
                                 <div class="col-sm-2">
                                    <div class="form-group">
                                        <strong>HBL:</strong>&nbsp;<asp:Label ID="lblHBL_House"  runat="server"/>
                                        </div>
                                           </div>
                                 <div class="col-sm-2">
                                    <div class="form-group">
                                        <strong>PROCESSO:</strong>&nbsp;<asp:Label ID="lblProcesso_House"  runat="server"/>
                                        </div>
                                           </div>
                                      
                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <strong>TIPO FRETE:</strong>&nbsp;<asp:Label ID="lblTipoFrete_House"  runat="server"/>
                                        </div>
                                           </div>
                                 <div class="col-sm-2">
                                    <div class="form-group">
                                        <strong>ESTUFAGEM:</strong>&nbsp;<asp:Label ID="lblEstufagem_House"  runat="server"/>
                                        </div>
                                           </div>
                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <strong>PESO BRUTO:</strong>&nbsp;<asp:Label ID="lblPesoBruto_House"  runat="server"/>
                                        </div>
                                           </div>

                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <strong>PESO CUBICO:</strong>&nbsp;<asp:Label ID="lblM3_House"  runat="server"/>
                                        </div>
                                           </div>
                                </div>
<br /><br /><br /><div class="row">
                                       <div class="col-sm-8">
                                    <div class="form-group">                                        <h5>DEVOLUÇÃO FRETE</h5>

                                                                                  <asp:GridView ID="dgvDevolucaoHBL" DataKeyNames="ID_BL_TAXA" DataSourceID="dsDevolucao" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" EmptyDataText="Nenhum registro encontrado." AllowPaging="true" PageSize="10">
                                            <Columns>
                                                <asp:TemplateField HeaderText="ID" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID_BL_TAXA") %>'  />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="NR_PROCESSO" HeaderText="PROCESSO" SortExpression="NR_PROCESSO" />
                                                <asp:BoundField DataField="SIGLA_MOEDA" HeaderText="MOEDA" SortExpression="SIGLA_MOEDA" />                        
                                                <asp:TemplateField HeaderText="VALOR COMPRA" SortExpression="VL_COMPRA">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblValorCompra" runat="server" Text='<%# Eval("VL_COMPRA") %>'  />
                                                    </ItemTemplate>
                                                </asp:TemplateField>    
                                                 <asp:TemplateField HeaderText="VALOR VENDA" SortExpression="VL_VENDA">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblValorVenda" runat="server" Text='<%# Eval("VL_VENDA") %>'  />
                                                    </ItemTemplate>
                                                </asp:TemplateField>    
                                                <asp:BoundField DataField="DT_RECEBIMENTO" HeaderText="DATA RECEBIMENTO" SortExpression="DT_RECEBIMENTO" />
                                       
                                            </Columns>
                                            <HeaderStyle CssClass="headerStyle" />
                                        </asp:GridView>
                                        </div>
                                           </div>
                                <div class="col-sm-4">
                                    <div class="form-group">
                                                                                <h5>DEVOLUÇÃO AGENTE</h5>

<asp:GridView ID="dgvComissoesHBL" DataKeyNames="ID_BL_TAXA" DataSourceID="dsComissoes" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" EmptyDataText="Nenhum registro encontrado." AllowPaging="true" PageSize="10">
                                            <Columns>
                                               <asp:TemplateField HeaderText="ID" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID_BL_TAXA") %>'  />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="NR_PROCESSO" HeaderText="PROCESSO" SortExpression="NR_PROCESSO" />
             
                                                <asp:BoundField DataField="SIGLA_MOEDA" HeaderText="MOEDA" SortExpression="SIGLA_MOEDA" />                          <asp:TemplateField HeaderText="VALOR" SortExpression="VL_TAXA">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblValor" runat="server" Text='<%# Eval("VL_TAXA") %>'  />
                                                    </ItemTemplate>
                                                </asp:TemplateField>         
                                                                                           
                                            </Columns>
                                            <HeaderStyle CssClass="headerStyle" />
                                        </asp:GridView>
                                        </div>
                                           </div>
                                
                                </div>
                                                        <br />

                            <div class="row">
                                       <div class="col-sm-12">
                                    <div class="form-group">
                                                                                <h5>TAXAS</h5>

                                          <asp:GridView ID="dgvOutrasTaxasHBL" DataKeyNames="ID_BL_TAXA" DataSourceID="dsOutrasTaxas" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" EmptyDataText="Nenhum registro encontrado." AllowPaging="true" PageSize="10">
                                            <Columns>
                                                               <asp:TemplateField HeaderText="ID" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID_BL_TAXA") %>'  />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="NR_PROCESSO" HeaderText="PROCESSO" SortExpression="NR_PROCESSO" />
                                                <asp:BoundField DataField="NM_ITEM_DESPESA" HeaderText="DESPESA" SortExpression="NM_ITEM_DESPESA" />
                                                <asp:BoundField DataField="CD_DECLARADO" HeaderText="DECLARADO" SortExpression="CD_DECLARADO" />
                                                <asp:BoundField DataField="SIGLA_MOEDA" HeaderText="MOEDA" SortExpression="SIGLA_MOEDA" />       
                                                               <asp:TemplateField HeaderText="VALOR" SortExpression="VL_TAXA" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblValor" runat="server" Text='<%# Eval("VL_TAXA") %>'  />
                                                    </ItemTemplate>
                                                </asp:TemplateField>                                <asp:BoundField DataField="DT_RECEBIMENTO" HeaderText="DATA RECEBIMENTO" SortExpression="DT_RECEBIMENTO" />

                                            </Columns>
                                            <HeaderStyle CssClass="headerStyle" />
                                        </asp:GridView>
                                        </div>
                                           </div>
                                
                                </div>
                            <br />
                            <div class="row">
                                       <div class="col-sm-12">
                                    <div class="form-group">                                        <h5>INVOICES</h5>


                                            <asp:GridView ID="dgvInvoiceHBL" DataKeyNames="ID_ACCOUNT_INVOICE" DataSourceID="dsInvoice" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" EmptyDataText="Nenhum registro encontrado." AllowPaging="true" PageSize="10" >
                                                <Columns>
                                                    <asp:BoundField DataField="NR_INVOICE" HeaderText="Nº INVOICE" SortExpression="NR_INVOICE" />
                                                    <asp:BoundField DataField="NM_ACCOUNT_TIPO_INVOICE" HeaderText="TIPO" SortExpression="NM_ACCOUNT_TIPO_INVOICE" />
                                                    <asp:BoundField DataField="NM_ACCOUNT_TIPO_EMISSOR" HeaderText="EMISSOR" SortExpression="NM_ACCOUNT_TIPO_EMISSOR" />
                                                    <asp:BoundField DataField="NM_ACCOUNT_TIPO_FATURA" HeaderText="TIPO FATURA" SortExpression="NM_ACCOUNT_TIPO_FATURA" />
                                                    <asp:BoundField DataField="SIGLA_MOEDA" HeaderText="MOEDA" SortExpression="SIGLA_MOEDA" />
                                                       <asp:BoundField DataField="NM_ACCOUNT_TIPO_FATURA" HeaderText="TIPO FATURA" SortExpression="NM_ACCOUNT_TIPO_FATURA" />
                                                    <asp:BoundField DataField="VALOR_FRETE" HeaderText="FRETE" SortExpression="VALOR_FRETE" />
                                                       <asp:BoundField DataField="VALOR_TAXA_EXTERIOR" HeaderText="TAXA EXTERIOR" SortExpression="VALOR_TAXA_EXTERIOR" />
                                                    <asp:BoundField DataField="VALOR_TAXA_DECLADADA" HeaderText="TAXA DECLADADA" SortExpression="VALOR_TAXA_DECLADADA" />
                                                       <asp:BoundField DataField="VALOR_OUTRAS_TAXAS" HeaderText="OUTRAS TAXAS" SortExpression="VALOR_OUTRAS_TAXAS" />
                                                    <asp:BoundField DataField="VALOR_COMISSAO" HeaderText="COMISSAO" SortExpression="VALOR_COMISSAO" />

                                                </Columns>
                                                <HeaderStyle CssClass="headerStyle" />
                                            </asp:GridView>
                                        </div>
                                           </div>
                               
                                
                                </div>

                            </div>
 </ContentTemplate>
    <Triggers>
       <asp:AsyncPostBackTrigger ControlID="dgvComissoesHBL" />
        <asp:AsyncPostBackTrigger ControlID="dgvInvoiceHBL" />
        <asp:AsyncPostBackTrigger ControlID="dgvOutrasTaxasHBL" />
        <asp:AsyncPostBackTrigger ControlID="dgvDevolucaoHBL" />
    </Triggers>
</asp:UpdatePanel>



                        </div>
                                     
                    </div>



                </div>
            </div>
        </div>

    
    <asp:SqlDataSource ID="dsDevolucao" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT A.ID_BL_TAXA,A.ID_BL,A.NR_PROCESSO,A.SIGLA_MOEDA,A.VL_COMPRA,A.VL_VENDA,A.DT_RECEBIMENTO FROM FN_ACCOUNT_DEVOLUCAO_FRETE (@ID_BL , '@GRAU') A">
        <SelectParameters>
            <asp:ControlParameter Name="ID_BL" Type="string" ControlID="lblID_BL" />
            <asp:ControlParameter Name="GRAU" Type="string" ControlID="lblGrau" />

        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="dsComissoes" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT A.ID_BL_TAXA,A.ID_MOEDA,A.ID_BL,A.NR_PROCESSO,A.SIGLA_MOEDA,A.VL_TAXA FROM  FN_ACCOUNT_DEVOLUCAO_COMISSAO (@ID_BL , '@GRAU') A">
        <SelectParameters>
            <asp:ControlParameter Name="ID_BL" Type="string" ControlID="lblID_BL" />
            <asp:ControlParameter Name="GRAU" Type="string" ControlID="lblGrau" />

        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="dsOutrasTaxas" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT A.ID_BL_TAXA,A.ID_MOEDA,A.ID_BL,A.NR_PROCESSO,A.NM_ITEM_DESPESA,A.SIGLA_MOEDA,A.VL_TAXA,A.CD_DECLARADO,A.DT_RECEBIMENTO  FROM FN_ACCOUNT_OUTRAS_TAXAS (@ID_BL , '@GRAU') A">
        <SelectParameters>
            <asp:ControlParameter Name="ID_BL" Type="string" ControlID="lblID_BL" />
            <asp:ControlParameter Name="GRAU" Type="string" ControlID="lblGrau" />
        </SelectParameters>
    </asp:SqlDataSource>
    
     <asp:SqlDataSource ID="dsInvoice" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_ACCOUNT_INVOICE,
NR_INVOICE,
(SELECT NM_ACCOUNT_TIPO_INVOICE FROM TB_ACCOUNT_TIPO_INVOICE WHERE ID_ACCOUNT_TIPO_INVOICE = A.ID_ACCOUNT_TIPO_INVOICE )NM_ACCOUNT_TIPO_INVOICE,
(SELECT NM_ACCOUNT_TIPO_EMISSOR FROM TB_ACCOUNT_TIPO_EMISSOR WHERE ID_ACCOUNT_TIPO_EMISSOR = A.ID_ACCOUNT_TIPO_EMISSOR)NM_ACCOUNT_TIPO_EMISSOR,
(SELECT NM_ACCOUNT_TIPO_FATURA FROM TB_ACCOUNT_TIPO_FATURA WHERE ID_ACCOUNT_TIPO_FATURA = A.ID_ACCOUNT_TIPO_FATURA)NM_ACCOUNT_TIPO_FATURA,
(SELECT SIGLA_MOEDA FROM TB_MOEDA WHERE ID_MOEDA = A.ID_MOEDA)SIGLA_MOEDA,
(SELECT SUM(ISNULL(B.VL_TAXA,0)) FROM TB_ACCOUNT_INVOICE_ITENS B WHERE A.ID_ACCOUNT_INVOICE = B.ID_ACCOUNT_INVOICE AND B.CD_TIPO_DEVOLUCAO = 'DF')VALOR_FRETE,
(SELECT SUM(ISNULL(B.VL_TAXA,0)) FROM TB_ACCOUNT_INVOICE_ITENS B WHERE A.ID_ACCOUNT_INVOICE = B.ID_ACCOUNT_INVOICE AND  B.CD_TIPO_DEVOLUCAO = 'TE')VALOR_TAXA_EXTERIOR,
(SELECT SUM(ISNULL(B.VL_TAXA,0)) FROM TB_ACCOUNT_INVOICE_ITENS B WHERE A.ID_ACCOUNT_INVOICE = B.ID_ACCOUNT_INVOICE AND  B.CD_TIPO_DEVOLUCAO = 'TD')VALOR_TAXA_DECLADADA,
(SELECT SUM(ISNULL(B.VL_TAXA,0)) FROM TB_ACCOUNT_INVOICE_ITENS B WHERE A.ID_ACCOUNT_INVOICE = B.ID_ACCOUNT_INVOICE AND  B.CD_TIPO_DEVOLUCAO = 'OT')VALOR_OUTRAS_TAXAS,
(SELECT SUM(ISNULL(B.VL_TAXA,0)) FROM TB_ACCOUNT_INVOICE_ITENS B WHERE A.ID_ACCOUNT_INVOICE = B.ID_ACCOUNT_INVOICE AND B.CD_TIPO_DEVOLUCAO = 'CO')VALOR_COMISSAO FROM TB_ACCOUNT_INVOICE A WHERE  ID_BL = @ID_BL">
        <SelectParameters>
            <asp:ControlParameter Name="ID_BL" Type="string" ControlID="lblID_BL" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>