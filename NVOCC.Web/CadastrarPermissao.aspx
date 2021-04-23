<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="CadastrarPermissao.aspx.vb" Inherits="NVOCC.Web.CadastrarPermissao" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <br />
    <div class="row principal">
        <div class="col-lg-6 col-lg-offset-3 col-md-12 col-sm-12">

            <div class="panel panel-primary"  >
                <div class="panel-heading">
                    <h3 class="panel-title">PERMISSÕES DE USUÁRIOS
                    </h3>
                </div>
                <div class="panel-body">
                    <asp:UpdatePanel ID="updPainel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True">
    <ContentTemplate>
                    <div class="row">

                        <div class="col-sm-12">
                            <br />
                            <asp:ValidationSummary ID="Validacoes" runat="server" ShowModelStateErrors="true" CssClass="alert alert-danger" />

                           
                            <div class="alert alert-success" id="msgSucesso" runat="server" visible="false">
                               Permissões de acesso aplicadas com sucesso!
                            </div>
                            <div class="alert alert-danger" id="msgerro" runat="server" visible="false">
                               Usuário não tem permissão para realizar alterações!
                            </div>

                        </div>
                    </div>

                    <asp:TextBox ID="txtId" runat="server" CssClass="form-control" Enabled="false" Visible="false"></asp:TextBox>

                    <div class="row">
                        <div class="col-sm-12">
                            <div class="form-group">
                                <label class="control-label">Grupo de Usuário:</label>
                                <asp:DropDownList ID="cbGrupo" runat="server" CssClass="form-control" DataValueField="Id" DataTextField="Descricao" DataSourceID="dsGruposUsuario" Font-Size="11px" AutoPostBack="true" >
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row" id="divGrid" runat="server">

                        <div class="col-sm-12">
                            <div class="tableFixHead form-group">
                                <label class="control-label">Menus do Sistema:</label>

                                <asp:GridView ID="dgvMenus"
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


                            </div>
                        </div>

                    </div>

                    <div class="row">
                         <div class="col-sm-3">
                            <div class="form-group">
                                <label>&nbsp;</label>
                                <asp:Button ID="btnSelecionarTodos" runat="server" CssClass="btn btn-warning btn-block" Text="Selecionar Todos"  />
                            </div>
                        </div>

                        <div class="col-sm-3 pull-right">
                            <div class="form-group">
                                <label>&nbsp;</label>
                                <asp:Button ID="btnGravar" runat="server" CssClass="btn btn-primary btn-block" Text="Gravar"  />
                            </div>
                        </div>
                    </div>
         </ContentTemplate>
    <Triggers>
    </Triggers>
</asp:UpdatePanel>
                </div>
            </div>
        </div>

    </div>
      <asp:SqlDataSource ID="dsGruposUsuario" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT ID_TIPO_USUARIO as Id, NM_TIPO_USUARIO as Descricao FROM [dbo].[TB_TIPO_USUARIO]"
        >
</asp:SqlDataSource>
     <asp:SqlDataSource ID="dsMenus" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT 
	                    M.ID_MENUS as Id, 
	                    M.NM_MENUS as Descricao, 
                        M.NM_OBJETO as Objeto, 
	                    (SELECT FL_Acessar FROM [dbo].[TB_GRUPO_PERMISSAO] WHERE ID_MENU = M.ID_MENUS AND ID_TIPO_USUARIO = @Grupo) As Acessar, 
	                    (SELECT FL_Cadastrar FROM [dbo].[TB_GRUPO_PERMISSAO] WHERE ID_MENU = M.ID_MENUS AND ID_TIPO_USUARIO = @Grupo) As Cadastrar, 
	                    (SELECT FL_Atualizar FROM [dbo].[TB_GRUPO_PERMISSAO] WHERE ID_MENU = M.ID_MENUS AND ID_TIPO_USUARIO = @Grupo) As Atualizar, 
	                    (SELECT FL_Excluir FROM [dbo].[TB_GRUPO_PERMISSAO] WHERE ID_MENU = M.ID_MENUS AND ID_TIPO_USUARIO = @Grupo) As Excluir 
                    FROM 
	                    [dbo].[TB_MENUS] M
                    ORDER BY 
	                    M.NM_MENUS">
   <SelectParameters>
    <asp:ControlParameter ControlID="cbGrupo" Name="Grupo" PropertyName = "SelectedValue" />
  </SelectParameters>

</asp:SqlDataSource>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>