﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="FaturarRecebimento.aspx.vb" Inherits="NVOCC.Web.FaturarRecebimento" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <div class="row principal">
        <div class="col-lg-12 col-md-12 col-sm-12">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">FATURAR RECEBIMENTO
                    </h3>
                </div>

                                <div class="panel-body">
                    <div class="tab-pane fade active in" id="consulta">
                          <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="always" ChildrenAsTriggers="True">
                            <ContentTemplate>

                                <div class="alert alert-success" id="divSuccess" runat="server" visible="false">
                                    <asp:Label ID="lblmsgSuccess" runat="server"></asp:Label>
                                </div>
                                <div class="alert alert-danger" id="divErro" runat="server" visible="false">
                                    <asp:Label ID="lblmsgErro" runat="server"></asp:Label>
                                </div>

                                Filtro:
                   <div class="row linhabotao text-center" style="margin-left: 0px; border: ridge 1px; padding-top: 20px; padding-bottom: 20px; margin-right: 5px;">

                       <div class="col-sm-2" style="padding-top: 20px;">
                           <div class="form-group">
                               <asp:DropDownList ID="ddlFiltro" AutoPostBack="true" runat="server" CssClass="form-control" Font-Size="15px">
                                   <asp:ListItem Value="0" Text="Selecione"></asp:ListItem>
                                   <asp:ListItem Value="1">Número do processo</asp:ListItem>
                                   <asp:ListItem Value="2">Número do Master</asp:ListItem>
                                   <asp:ListItem Value="3">Nome do Cliente</asp:ListItem>
                                   <asp:ListItem Value="4">Referência  do Cliente</asp:ListItem>
                               </asp:DropDownList>
                           </div>

                       </div>
                       <div class="col-sm-2" style="padding-top: 20px;">
                           <div class="form-group">
                               <asp:TextBox ID="txtPesquisa" runat="server" CssClass="form-control"></asp:TextBox>
                           </div>
                       </div>  
                       <div class="col-sm-1">

                                        <div class="form-group">
                                            <asp:RadioButtonList ID="rdStatus" runat="server" Style="padding: 0px; font-size: 12px; text-align: justify">
                                                <asp:ListItem Value="0"  Selected="True">&nbsp;Abertos</asp:ListItem>
                                                <asp:ListItem Value="1">&nbsp;Cancelados</asp:ListItem>
                                                <asp:ListItem Value="2">&nbsp;Enviados</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                       <div class="col-sm-1" style="padding-top: 20px;">
                           <div class="form-group">
                               <asp:Button runat="server" Text="Pesquisar" ID="btnPesquisa" CssClass="btn btn-success" />

                           </div>
                       </div>
                       
                   </div>
                                <br />

                                <asp:Button runat="server" Text="Pesquisar" Style="display: none" ID="btnPesquisar" CssClass="btn btn-success" />
                                


                        <br />
                  
                                <div runat="server" id="divAuxiliar"  Style="display: none">
                                    <asp:TextBox ID="txtID" runat="server" CssClass="form-control" Width="50PX"></asp:TextBox>
                                    <asp:TextBox ID="txtlinha" runat="server" CssClass="form-control" Width="50PX"></asp:TextBox>
                                </div>
                                <div class="table-responsive tableFixHead" >
                                    <asp:GridView ID="dgvContasReceber" DataKeyNames="ID_CONTA_PAGAR_RECEBER" DataSourceID="dsContasReceber" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" EmptyDataText="Nenhum registro encontrado.">
                                         <Columns>
                                    <asp:TemplateField HeaderText="ID" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID_CONTA_PAGAR_RECEBER") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="NR_PROCESSO" HeaderText="Nº Processo" SortExpression="NR_PROCESSO" />
                                     <asp:BoundField DataField="NM_TIPO_ESTUFAGEM" HeaderText="Estufagem" SortExpression="NM_TIPO_ESTUFAGEM" />       
                                    <asp:BoundField DataField="PARCEIRO_EMPRESA" HeaderText="Cliente" SortExpression="PARCEIRO_EMPRESA" />
                                    <asp:BoundField DataField="QT_DIAS_FATURAMENTO" HeaderText="Qtd. Dias Faturamento" SortExpression="QT_DIAS_FATURAMENTO" />
                                    <asp:BoundField DataField="REFERENCIA_CLIENTE" HeaderText="Ref. Cliente" SortExpression="REFERENCIA_CLIENTE" />
                                    <asp:BoundField DataField="DT_VENCIMENTO" HeaderText="Data de Vencimento" SortExpression="DT_VENCIMENTO" />
                                    <asp:BoundField DataField="VL_LIQUIDO" HeaderText="Valor" SortExpression="VL_LIQUIDO" />
                                    <asp:BoundField DataField="DT_LIQUIDACAO" HeaderText="Data de Liquidação" SortExpression="DT_LIQUIDACAO" />
                                    <asp:BoundField DataField="NOME_USUARIO_LIQUIDACAO" HeaderText="Usuario de Liquidação" SortExpression="NOME_USUARIO_LIQUIDACAO" />
                                    <asp:BoundField DataField="DT_CANCELAMENTO" HeaderText="Data de Cancelamento" SortExpression="DT_CANCELAMENTO" />
                                    <asp:BoundField DataField="DT_ENVIO_FATURAMENTO" HeaderText="Data de Envio" SortExpression="DT_ENVIO_FATURAMENTO" DataFormatString="{0:dd/MM/yyyy}" />
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnSelecionar" runat="server" CssClass="btn btn-primary btn-sm"
                                                CommandArgument='<%# Eval("ID_CONTA_PAGAR_RECEBER") %>' CommandName="Selecionar" Text="Enviar"></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle CssClass="headerStyle" />
                            </asp:GridView>
                                </div>

                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="dgvContasReceber" />
                                <asp:PostBackTrigger ControlID="btnPesquisar" />
                            </Triggers>
                        </asp:UpdatePanel>




                                                         <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control" Style="display: none;"></asp:TextBox>
         <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="pnlEmail" TargetControlID="TextBox2" CancelControlID="TextBox2"></ajaxToolkit:ModalPopupExtender>
                         <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
                                        <ContentTemplate>
                                            <asp:Panel ID="pnlEmail" runat="server" CssClass="modalPopup" Style="display: none;">
                                    <center>     <div class=" modal-dialog modal-dialog-centered modal-lg" role="document">
                                                    <div class="modal-content" >
                                                        <div class="modal-header">
                                                            <h5 class="modal-title">EMAIL FATURAMENTO</h5>
                                                        </div>
                                                        <div class="modal-body">    
                    
                                                          <div class="row">
                                <div class="col-sm-12">
                                    <div class="form-group">
                                        <label>&nbsp;</label> 
                                        <asp:FileUpload ID="FileUpload1"  CssClass="form-control" runat="server" Visible="true" style="display:block"  onchange="Javascript: VerificaTamanhoArquivo();" ></asp:FileUpload>                                                                                         
                                    </div>
                                </div>
                            </div>
                      
                                     </div>                  
                                                                        
                               <div class="modal-footer">
                                                            <asp:Button runat="server" CssClass="btn btn-success" ID="btnProsseguir" text="Prosseguir"  />
                                                            <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFechar" text="Close" OnClientClick="FecharUpload();" />                                                       
                               </div>
                                                    
                                                
      
                                       </div>     </center>
                                </asp:Panel>

</ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="btnProsseguir" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                    </div>

                </div>




            </div>
        </div>

         
</div>

   <asp:SqlDataSource ID="dsContasReceber" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT * FROM [dbo].[View_Contas_Receber] WHERE (CD_PR = 'R') AND ISNULL(TP_EXPORTACAO,'') = '' AND DT_CANCELAMENTO IS NULL and DT_ENVIO_FATURAMENTO IS NULL"></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsParceiros" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_PARCEIRO as Id, CNPJ , NM_RAZAO RazaoSocial FROM TB_PARCEIRO #FILTRO ORDER BY ID_PARCEIRO"></asp:SqlDataSource>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">
    <script>
        function VerificaTamanhoArquivo() {

            var btn = document.getElementById('<%= btnProsseguir.ClientID %>');
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

        function FecharUpload() {
            var fi = document.getElementById('<%= FileUpload1.ClientID %>');
            var btn = document.getElementById('<%= btnProsseguir.ClientID %>');
            btn.style.display = 'block';
            document.getElementById('<%= txtID.ClientID %>').value = "0";
            fi.value = null;
        }
    </script>
</asp:Content>
