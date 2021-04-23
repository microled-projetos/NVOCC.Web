<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ItemDespesa.aspx.vb" Inherits="NVOCC.Web.ItemDespesa" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server"> 
    <div class="row principal">
        <div class="col-lg-6 col-lg-offset-3 col-md-12 col-sm-12">

            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">ITEM DESPESA
                    </h3>
                </div>
                <div class="panel-body">

                    <ul class="nav nav-tabs" role="tablist">
                        <li class="active">
                            <a href="#cadastro" role="tab" data-toggle="tab">
                                <i class="fa fa-edit" style="padding-right: 8px;"></i>Cadastro
                            </a>
                        </li>
                        <li>
                            <a href="#consulta" role="tab" data-toggle="tab">
                                <i class="fa fa-search" style="padding-right: 8px;"></i>Consulta
                            </a>
                        </li>
                    </ul>

                    <div class="tab-content">
                        <div class="tab-pane fade active in" id="cadastro">

                            <div class="row">

                                <div class="col-sm-12">
                                    <br />
                                    <asp:ValidationSummary ID="Validacoes" runat="server" ShowModelStateErrors="true" CssClass="alert alert-danger" />


                                    <div id="divmsg" runat="server" visible="false" class="alert alert-success">
                                        Registro cadastrado/atualizado com sucesso!
                                    </div>

                                     <div id="divErro" runat="server" visible="false" class="alert alert-danger">
                                        <asp:label ID="lblErro" runat="server" />
                                     </div>
                                </div>
                            </div>

                            <div class="row">

                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Código:</label>
                                        <asp:TextBox ID="txtIDItemDespesa" runat="server"  CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>
                                 <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label"></label>
                                        <asp:Checkbox ID="ckbAtivo" runat="server" CssClass="form-control" text="&nbsp;&nbsp;Ativo" ></asp:Checkbox>
                                    </div>
                                </div>

                               <div class="col-sm-4">
                                    <div class="form-group">
                                       <label class="control-label"></label>
                                        <asp:Checkbox ID="ckbIntegraPA" runat="server" CssClass="form-control" text="&nbsp;&nbsp;Integra PA" ></asp:Checkbox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Nome:</label>
                                        <asp:TextBox ID="txtNome" runat="server"  CssClass="form-control" MaxLength="100"></asp:TextBox>
                                    </div>
                                </div>
                                  <div class="col-sm-4">
                                    <div class="form-group">
                                       <label class="control-label">Tipo Item Despesa:</label>
                                       <asp:DropDownList ID="ddlTipoItemDespesa" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_TIPO_ITEM_DESPESA" DataSourceID="dsTipoItemDespesa" DataValueField="ID_TIPO_ITEM_DESPESA">
                                       </asp:DropDownList>
                                    </div>
                                 </div>                                
                                    <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Código Natureza:</label>
                                        <asp:TextBox ID="txtNatureza" runat="server"  CssClass="form-control" MaxLength="100"></asp:TextBox>
                                    </div>
                                </div>
                          
                            </div>
                            <div class="row">

                                <div class="col-sm-3 col-sm-offset-6">
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <asp:Button ID="btnLimpar" runat="server" CssClass="btn btn-warning btn-block" Text="Limpar Campos" />
                                    </div>
                                </div>

                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <asp:Button ID="btnGravar"  runat="server" CssClass="btn btn-primary btn-block" Text="Gravar" />
                                    </div>
                                </div>
                            </div>

                        </div>  

                        <div class="tab-pane fade" id="consulta">
                            <br />
                           
                                <asp:UpdatePanel ID="updPainel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True">
    <ContentTemplate>
                  <div class="row" style="padding:10px">                        
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <label class="control-label">Consultar por:</label>
                                         <asp:DropDownList ID="ddlConsulta" runat="server" CssClass="form-control" Font-Size="11px" AutoPostBack="True">
                                            <asp:ListItem Value="0" Selected="True">Selecione</asp:ListItem>
                                            <asp:ListItem Value="1">ID</asp:ListItem>
                                            <asp:ListItem Value="2">NOME</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-3" id="divPesquisa" runat="server" Visible="false">
                                    <div class="form-group">   
                                        <label class="control-label">Pesquisar</label>
                                        <asp:TextBox ID="txtConsulta" runat="server" AutoPostBack="true" CssClass="form-control"  MaxLength="50"></asp:TextBox>
                                    </div>                                   
                                </div>
                       </div>
        <div id="divInfo" runat="server" visible="false" class="alert alert-success">
                              <asp:label ID="lblInfo" Text="" runat="server" /></div>
         <div class="table-responsive tableFixHead">
                                <asp:GridView ID="dgvItemDespesa" style="max-height:600px; overflow:auto;"  DataKeyNames="Id" DataSourceID="dsItemDespesa"  CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server"  AllowSorting="true" OnSorting="dgvItemDespesa_Sorting" AutoGenerateColumns="false" EmptyDataText="Nenhum registro encontrado.">
                                    <Columns>
                                        <asp:BoundField DataField="Id" HeaderText="#" SortExpression="Id" />
                                        <asp:BoundField DataField="NM_ITEM_DESPESA" HeaderText="Descrição" SortExpression="NM_ITEM_DESPESA" />
                                        <asp:BoundField DataField="NM_TIPO_ITEM_DESPESA" HeaderText="Tipo" SortExpression="NM_TIPO_ITEM_DESPESA"/>
                                        <asp:BoundField DataField="ATIVO" HeaderText="Ativo" SortExpression="ATIVO" />
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <a href="ItemDespesa.aspx?id=<%# Eval("Id") %>" class="btn btn-info btn-sm" data-toggle="tooltip" data-placement="top" title="Editar"><span class="glyphicon glyphicon-edit"  style="font-size:medium"></span></a>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>                    
                                   <ItemTemplate>                          
                            <asp:LinkButton ID="btnDelete" runat="server" title="Ativar/Desativar" CausesValidation="False" CommandName="desativar" CommandArgument='<%# Eval("Id") %>'
                                  CssClass="btn btn-sm btn-warning"><i class="fa fa-toggle-on" style="font-size:medium"></i></asp:LinkButton>
                                   </ItemTemplate>            
                                              <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                    </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle CssClass="headerStyle" />
                                </asp:GridView>
             </div>
                                 </ContentTemplate>
                                    
    <Triggers>
       <asp:AsyncPostBackTrigger EventName="Sorting" ControlID="dgvItemDespesa" />
               <asp:AsyncPostBackTrigger EventName="TextChanged" ControlID="txtConsulta" />
    </Triggers>
</asp:UpdatePanel>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>



    
    <asp:SqlDataSource ID="dsItemDespesa" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT A.ID_ITEM_DESPESA as Id,A.NM_ITEM_DESPESA,A.ID_TIPO_ITEM_DESPESA,B.NM_TIPO_ITEM_DESPESA,A.CD_NATUREZA,A.FL_INTEGRA_PA,A.FL_ATIVO, 
CASE WHEN A.FL_ATIVO = 1 THEN 'Sim' ELSE 'Não' end ATIVO,
CASE WHEN A.FL_INTEGRA_PA = 1 THEN 'Sim' ELSE 'Não' end INTEGRA_PA
FROM [dbo].[TB_ITEM_DESPESA] A
        LEFT JOIN [dbo].[TB_TIPO_ITEM_DESPESA] B ON B.ID_TIPO_ITEM_DESPESA =  A.ID_TIPO_ITEM_DESPESA /*FILTRO*/  " >  
        </asp:SqlDataSource>

     <asp:SqlDataSource ID="dsTipoItemDespesa" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_TIPO_ITEM_DESPESA, NM_TIPO_ITEM_DESPESA FROM TB_TIPO_ITEM_DESPESA union SELECT  0, 'Selecione' FROM TB_TIPO_ITEM_DESPESA ORDER BY ID_TIPO_ITEM_DESPESA" >
        <DeleteParameters>
                    <asp:Parameter Name="ID" Type="Int32" />
                </DeleteParameters>
        </asp:SqlDataSource>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">
        <script src="Content/js/viacep.js"></script>
    <script src="Content/js/jquery.dataTables.min.js"></script>
       <link href="Content/css/select2.css" rel="stylesheet" />
        <script src="Content/ScrollableGridPlugin.js"></script>
</asp:Content>
