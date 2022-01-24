<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ConsultaParceiro.aspx.vb" Inherits="NVOCC.Web.ConsultaParceiro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        th{
            padding:0px !important;
            margin:0px !important;
        }
        .table tbody tr td{
            padding:0px !important;
            margin:0px !important;
            font-size:12px !important;
            padding-right:2px !important;
        }
        /*.table tbody tr td{
            padding-top:0px !important;
            margin-top:0px !important;
                        padding-bottom:0px !important;
            margin-bottom:0px !important;
           
        }*/
    </style>
     <div class="row principal">
        <div class="col-lg-12  col-md-12 col-sm-12">
            <br />
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">PARCEIROS
                    </h3>
                </div>
                <asp:UpdatePanel ID="updPainel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True">
    <ContentTemplate>
              <asp:TextBox ID="txtID" runat="server" CssClass="form-control" style="display:none"></asp:TextBox>
                       <div class="row" style="padding:10px">                        
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <label class="control-label">Consultar por:</label>
                                         <asp:DropDownList ID="ddlConsulta" runat="server" CssClass="form-control" Font-Size="11px" AutoPostBack="True">
                                            <asp:ListItem Value="0" Selected="True">Selecione</asp:ListItem>
                                            <asp:ListItem Value="1">CNPJ</asp:ListItem>
                                            <asp:ListItem Value="3">CPF</asp:ListItem>
                                            <asp:ListItem Value="2">Razão Social</asp:ListItem>
                                            <asp:ListItem Value="4">Apelido</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-3" id="divPesquisa" runat="server" Visible="false">
                                    <div class="form-group">   
                                        <label class="control-label">Pesquisar</label>
                                        <asp:TextBox ID="txtConsulta" runat="server" autopostback="true" CssClass="form-control"></asp:TextBox>
                                        <asp:label ID="msgerro" runat="server" style ="color:red" />
                                    </div>                                   
                                </div>
                       </div>
                <div class="panel-body">
                        <div class="tab-pane fade active in" id="consulta">
                            <br />
                            <div class="table-responsive tableFixHead">
                                   <div id="diverro" runat="server" visible="false" class="alert alert-danger">
                                        <asp:label ID="lblErroExcluir" Text="" runat="server" />
                                   </div>
                                   <div id="divInfo" runat="server" visible="false" class="alert alert-success">
                                        <asp:label ID="lblInfo" Text="" runat="server" />
                                   </div>   
                                
                                <asp:GridView ID="dgvParceiros" DataKeyNames="Id" OnSorting="dgvParceiros_Sorting"  style="max-height:600px; overflow:auto;" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" DataSourceID="dsParceiros"  AutoGenerateColumns="false" AllowSorting="True" EmptyDataText="Nenhum registro encontrado." allowpaging="true" PageSize="100">
                                    <Columns>
                                        <asp:BoundField DataField="Id" HeaderText="#" SortExpression="Id" />
                                        <asp:BoundField DataField="RazaoSocial" HeaderText="Razão Social" SortExpression="RazaoSocial"/>
                                        <asp:BoundField DataField="CNPJ" HeaderText="CNPJ" SortExpression="CNPJ"/>
                                        <asp:BoundField DataField="CPF" HeaderText="CPF" SortExpression="CPF" />
                                        <asp:TemplateField HeaderText="Ativo" SortExpression="Ativo" >
                    <ItemTemplate>                     
                         <asp:Label ID="lblAtivo"  runat="server" Text='<%# Eval("Ativo") %>'/>
                    </ItemTemplate>
                </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                 <asp:linkButton ID="btnTaxas" title="Taxa Parceiro" runat="server" style="background-color:#e3d810;border-color:#e3d810" CssClass="btn btn-info btn-sm" CommandName="Taxas"
                                  CommandArgument='<%# Eval("Id") %>'><i class="fas fa-plus" style="font-size:medium"></i></asp:linkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <a href="CadastrarParceiro.aspx?id=<%# Eval("Id") %>" class="btn btn-info btn-sm" data-toggle="tooltip" data-placement="top" title="Editar"><span class="glyphicon glyphicon-edit" style="font-size:medium"></span></span></a>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="" >
                                            <ItemTemplate>
                                                <a href="EmailParceiro.aspx?p=<%# Eval("Id") %>" class="btn btn-success btn-sm" data-toggle="tooltip" data-placement="top" title="Adicionar Email"><span class="glyphicon glyphicon-envelope"  style="font-size:medium"></span></a>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" >
                                            <ItemTemplate>
                                                <a href="ClienteFinal.aspx?id=<%# Eval("Id") %>" class="btn btn-warning btn-sm" data-toggle="tooltip" data-placement="top" title="Adicionar cliente"><i class="fa fa-user" ></i></a>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                 <asp:linkButton ID="btnDuplicar" title="Duplicar Parceiro" style="display:none"  runat="server" CssClass="btn btn-primary btn-sm" CommandName="Duplicar" OnClientClick="javascript:return confirm('Deseja realmente duplicar este parceiro?');" 
                                  CommandArgument='<%# Eval("Id") %>'><i class="glyphicon glyphicon-duplicate" style="font-size:medium"></i></asp:linkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText=""  >
                                            <ItemTemplate>
                                                <asp:linkButton ID="btnExcluir" title="Excluir" runat="server"  CssClass="btn btn-danger btn-sm" CommandName="Excluir"
                                OnClientClick="javascript:return confirm('Deseja realmente excluir este parceiro?');"  CommandArgument='<%# Eval("Id") %>' Autopostback="true" ><span class="glyphicon glyphicon-trash"  style="font-size:medium"></span></asp:linkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle CssClass="headerStyle" />
                                </asp:GridView>
         
                            </div>
                        </div>
            
                     </div>
            </div>
                              </ContentTemplate>
    <Triggers>
       <asp:AsyncPostBackTrigger EventName="Sorting" ControlID="dgvParceiros" />
    </Triggers>
</asp:UpdatePanel>
        </div>
</div>
         </div>
        <asp:SqlDataSource ID="dsCidades" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT ID_CIDADE, NM_CIDADE FROM [dbo].[TB_CIDADE] Order by ID_CIDADE">
</asp:SqlDataSource>

     <asp:SqlDataSource ID="dsParceiros" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT ID_PARCEIRO as Id, CNPJ , UPPER(NM_RAZAO) RazaoSocial, CPF, CASE WHEN ISNULL(FL_ATIVO,0) = 0 THEN 'Não' WHEN ISNULL(FL_ATIVO,0) = 1 THEN 'Sim' end Ativo  FROM TB_PARCEIRO #FILTRO  ">
</asp:SqlDataSource>



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">
     <script src="Content/js/viacep.js"></script>
    <script src="Content/js/jquery.dataTables.min.js"></script>
       <link href="Content/css/select2.css" rel="stylesheet" />
        <script src="Content/ScrollableGridPlugin.js"></script>
    <script>
       function TaxaParceiro() {


            var ID = document.getElementById('<%= txtID.ClientID %>').value;
             console.log(ID);

            window.open('TaxaParceiro.aspx?id=' + ID, '_blank');
        }
        function TaxaTransportador() {


            var ID = document.getElementById('<%= txtID.ClientID %>').value;
            console.log(ID);

            window.open('TaxasLocaisArmador.aspx?id=' + ID, '_blank');
        }
    </script>
   
</asp:Content>
