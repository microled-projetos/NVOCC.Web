<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="CadastrarUsuarioGrupo.aspx.vb" Inherits="NVOCC.Web.CadastrarUsuarioGrupo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <br />
    <div class="row principal">
        <div class="col-lg-6 col-lg-offset-3 col-md-12 col-sm-12">

            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">GRUPOS DE USUÁRIOS
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

                                     <div id="msgErro" runat="server" visible="false" class="alert alert-danger">
                                        <asp:label ID="lblErro" Text="" runat="server" /></div>
                                </div>
                            </div>

                            <div class="row">

                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="control-label">Código:</label>
                                        <asp:TextBox ID="txtID" runat="server" required="true" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <asp:CheckBox ID="ckbAdmin" runat="server"  CssClass="form-control" Text="&nbsp;&nbsp;Administrador" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="form-group">
                                        <label class="control-label">Descrição:</label>
                                        <asp:TextBox ID="txtDescricao" runat="server" required="true" CssClass="form-control" MaxLength="100"></asp:TextBox>
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
                                        <asp:Button ID="btnGravar" runat="server" CssClass="btn btn-primary btn-block" Text="Gravar" />
                                    </div>
                                </div>
                            </div>

                        </div>

                        <div class="tab-pane fade" id="consulta">
                                   
                            <br />
                            <div class="table-responsive">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True">
    <ContentTemplate>
        <div id="diverro" runat="server" visible="false" class="alert alert-danger">
                                        <asp:label ID="lblErroExcluir" Text="" runat="server" /></div>
        <div id="divInfo" runat="server" visible="false" class="alert alert-success">
                              <asp:label ID="lblInfo" Text="" runat="server" /></div>
                                <asp:GridView ID="dgvUsuariosGrupos" DataKeyNames="Id" DataSourceID="dsTipoUsuario"  CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" AutoGenerateColumns="false" style="max-height:400px; overflow:auto;" AllowSorting="true" OnSorting="dgvUsuariosGrupos_Sorting" 
 >
                                    <Columns>
                                        <asp:BoundField DataField="Id" HeaderText="#" SortExpression="Id" />
                                        <asp:BoundField DataField="Descricao" HeaderText="Descrição" SortExpression="Descricao" />
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <a href="CadastrarUsuarioGrupo.aspx?id=<%# Eval("Id") %>" class="btn btn-info btn-sm" data-toggle="tooltip" data-placement="top" title="Editar"><span class="glyphicon glyphicon-edit"  style="font-size:medium"></span></a>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>                    
                                   <ItemTemplate>                          
                            <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="False" CommandName="excluir" CommandArgument='<%# Eval("Id") %>'  
                                Text="Excluir" OnClientClick="return confirm('Tem certeza que deseja excluir esse registro?')"  CssClass="btn btn-danger btn-sm" ><span class="glyphicon glyphicon-trash"  style="font-size:medium"></span></asp:LinkButton>
                                   </ItemTemplate>    
                                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />

                    </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle CssClass="headerStyle" />
                                </asp:GridView>
        
                                     </ContentTemplate>
    <Triggers>
       <asp:AsyncPostBackTrigger EventName="Sorting" ControlID="dgvUsuariosGrupos" />
    </Triggers>
</asp:UpdatePanel>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>

    </div>


     <asp:GridView ID="dgvMenus" Visible="false"
                                    style="max-height:600px; overflow:auto;"
                                    CssClass="table table-hover table-condensed table-bordered"
                                    runat="server"
                                    DataKeyNames="Id"
                                    AutoGenerateColumns="false"
                                    BorderStyle="None"
                                    BorderWidth="0px" 
                                    GridLines="None"
                                        DataSourceID="dsMenus"
                                    
                                    >
                                    <Columns>
                                        <asp:BoundField DataField="Id" HeaderText="#" Visible="False" />
                                        <asp:BoundField DataField="Descricao" HeaderText="Menu / Página" />
                                               <asp:TemplateField HeaderText="ID" Visible="False">
                                                <ItemTemplate>
                                             <asp:Label ID="lblID" runat="server" Text='<%# Eval("Id") %>'/>
                                                    </ItemTemplate>
                                            </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Acessar">
                                            <ItemTemplate>
                                            <asp:CheckBox ID="Acessar" runat="server" Checked='  <%# If(Eval("Acessar") Is DBNull.Value, "0", Eval("Acessar")) %>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Cadastrar">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="Cadastrar" runat="server" Checked='  <%# If(Eval("Cadastrar") Is DBNull.Value, "0", Eval("Cadastrar")) %>' />

                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Atualizar">
                                            <ItemTemplate>                        
                                                <asp:CheckBox ID="Atualizar" runat="server" Checked='  <%# If(Eval("Atualizar") Is DBNull.Value, "0", Eval("Atualizar")) %>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Excluir">
                                            <ItemTemplate>
                                                  <asp:CheckBox ID="Excluir" runat="server" Checked='  <%# If(Eval("Excluir") Is DBNull.Value, "0", Eval("Excluir")) %>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>

    <asp:SqlDataSource ID="dsTipoUsuario" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_TIPO_USUARIO as Id, NM_TIPO_USUARIO as Descricao FROM [dbo].[TB_TIPO_USUARIO]" >  
        </asp:SqlDataSource>

         <asp:SqlDataSource ID="dsMenus" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT 
	                    M.ID_MENUS as Id, 
	                    M.NM_MENUS as Descricao, 
                        M.NM_OBJETO as Objeto, 
	                    (SELECT FL_Acessar FROM [dbo].[TB_GRUPO_PERMISSAO] WHERE ID_MENU = M.ID_MENUS AND ID_TIPO_USUARIO = 0 ) As Acessar, 
	                    (SELECT FL_Cadastrar FROM [dbo].[TB_GRUPO_PERMISSAO] WHERE ID_MENU = M.ID_MENUS AND ID_TIPO_USUARIO = 0  ) As Cadastrar, 
	                    (SELECT FL_Atualizar FROM [dbo].[TB_GRUPO_PERMISSAO] WHERE ID_MENU = M.ID_MENUS AND ID_TIPO_USUARIO = 0 ) As Atualizar, 
	                    (SELECT FL_Excluir FROM [dbo].[TB_GRUPO_PERMISSAO] WHERE ID_MENU = M.ID_MENUS AND ID_TIPO_USUARIO = 0) As Excluir 
                    FROM 
	                    [dbo].[TB_MENUS] M
                    ORDER BY 
	                    M.NM_MENUS">
</asp:SqlDataSource>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">

    <script src="Content/js/jquery.dataTables.min.js"></script>

    <%--<script>
        $(document).ready(function () {

            $('#<%= dgvUsuariosGrupos.ClientID %>').DataTable({
                "language": {
                    "url": "Content/js/pt-br.json"
                }
            });
        });

        function excluir(id) {

            $('#modal-exclusao').data('id', id);
        }

        function confirmarExclusao() {

            var _id = $('#modal-exclusao').data('id');

            $.ajax({
                type: "POST",
                url: "CadastrarUsuarioGrupo.aspx/Excluir",
                data: '{ id: "' + _id + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function () {

                    toastr.success('Grupo de Usuários excluído com sucesso!', 'Se7');

                    $('#modal-exclusao')
                        .data('id', '0')
                        .modal('hide');

                    $('#linha-' + _id).remove();
                },
                error: function (response) {

                    var json = JSON.parse(response.responseText);

                    if (json != null) {
                        $('#modal-exclusao').modal('hide');
                        toastr.error(json.Message, 'Se7');
                    }
                }
            });
        }
    </script>--%>
</asp:Content>
