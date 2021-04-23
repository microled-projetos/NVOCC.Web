<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="CadastrarUsuario.aspx.vb" Inherits="NVOCC.Web.CadastrarUsuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <div class="row principal">
        <div class="col-lg-8 col-lg-offset-2 col-md-12 col-sm-12">

            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">USUÁRIOS
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


                                    <div class="alert alert-success" id="divmsg" runat="server" visible="false">
                                        Registro cadastrado/atualizado com sucesso!
                                    </div>
                                    <div class="alert alert-danger" id="diverro" runat="server" visible="false">
                                        <asp:Label ID="lblerro" runat="server" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">

                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <label class="control-label">Código:</label>
                                        <asp:TextBox ID="txtID" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-5">
                                    <div class="form-group">
                                        <label class="control-label">Nome:</label>
                                        <asp:TextBox ID="txtNome" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-5">
                                    <div class="form-group">
                                        <label class="control-label">Email:</label>
                                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">CPF:</label>
                                        <asp:TextBox ID="txtCPF" runat="server" CssClass="form-control cpf"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Telefone:</label>
                                        <asp:TextBox ID="txtTelefone" runat="server" CssClass="form-control telefone"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Celular:</label>
                                        <asp:TextBox ID="txtCelular" runat="server" CssClass="form-control celular"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Login:</label>
                                        <asp:TextBox ID="txtLogin" runat="server" MaxLength="50" Style="text-transform: none !important" CssClass="form-control senha"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Senha:</label>
                                        <asp:TextBox ID="txtSenha" runat="server" CssClass="form-control senha" MaxLength="128" Style="text-transform: none !important" TextMode="Password"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Confirmar Senha:</label>
                                        <asp:TextBox ID="txtConfirmaSenha" runat="server" CssClass="form-control senha" Style="text-transform: none !important" MaxLength="128" TextMode="Password"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="linha-colorida">Permissões Especiais</div>

                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <label class="control-label"></label>

                                        <asp:CheckBox ID="ckbAtivo" runat="server" CssClass="form-control" Text="&nbsp;&nbsp;Ativo" />
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <label class="control-label"></label>

                                        <asp:CheckBox ID="ckbExterno" runat="server" CssClass="form-control" Text="&nbsp;&nbsp;Usuário Externo" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <asp:CheckBox ID="ckbHouseBasico" runat="server" CssClass="form-control" Text="&nbsp;&nbsp;Básico - HOUSE" />
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <asp:CheckBox ID="ckbHouseCarga" runat="server" CssClass="form-control" Text="&nbsp;&nbsp;Carga - HOUSE" />
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <asp:CheckBox ID="ckbHouseTaxas" runat="server" CssClass="form-control" Text="&nbsp;&nbsp;Taxas - HOUSE" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <asp:CheckBox ID="ckbMasterBasico" runat="server" CssClass="form-control" Text="&nbsp;&nbsp;Básico - MASTER" />
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <asp:CheckBox ID="ckbMasterCNTR" runat="server" CssClass="form-control" Text="&nbsp;&nbsp;CNTR - MASTER" />
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <asp:CheckBox ID="ckbMasterTaxas" runat="server" CssClass="form-control" Text="&nbsp;&nbsp;Taxas - MASTER" />
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <asp:CheckBox ID="ckbMasterVinculo" runat="server" CssClass="form-control" Text="&nbsp;&nbsp;Vínculos - MASTER" />
                                    </div>
                                </div>
                            </div>
                            <div class="row" runat="server" id="divTipoUsuario" style="display: none">
                                <div class="linha-colorida">Tipo Usuário (Grupo)</div>
                                <br />
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <asp:DropDownList ID="cbTipoUsuario" runat="server" CssClass="form-control" Font-Size="11px" DataValueField="Id" DataTextField="Descricao" DataSourceID="dsGruposUsuario">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <asp:ImageButton ID="ImageButton1" src="Content/imagens/plus.png" runat="server" />
                                    </div>
                                </div>
                            </div>
                            <div class="row" runat="server" id="div1">
                                <br />
                                <div class="col-sm-12">
                                    <div class="form-group">
                                        <asp:GridView ID="gdvTipoUsuario" DataKeyNames="ID_VINCULO,ID_TIPO_USUARIO" DataSourceID="dsTipoUsuario" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" Style="max-height: 400px; overflow: auto;"
                                            AutoGenerateColumns="false">
                                            <Columns>             <asp:BoundField DataField="ID_VINCULO" Visible="False" HeaderText="ID_VINCULO" SortExpression="ID_VINCULO" />
                                                <asp:BoundField DataField="ID_TIPO_USUARIO" HeaderText="#" SortExpression="ID_TIPO_USUARIO" />
                                                <asp:BoundField DataField="NM_TIPO_USUARIO" HeaderText="TIPO USUARIO" SortExpression="NM_TIPO_USUARIO" />
                                                <asp:BoundField DataField="Empresa" HeaderText="Empresa" SortExpression="Empresa" Visible="false" />
                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                                 <asp:linkButton ID="btnExcluir" title="Excluir" runat="server"  CssClass="btn btn-danger btn-sm" CommandName="Excluir"
                                OnClientClick="javascript:return confirm('Deseja realmente excluir este parceiro?');"  CommandArgument='<%# Eval("ID_VINCULO") %>' Autopostback="true" ><span class="glyphicon glyphicon-trash" style="font-size:medium"></span></asp:linkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle CssClass="headerStyle" />
                                        </asp:GridView>
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
                            <div class="table-responsive tableFixHead">
                                <asp:UpdatePanel ID="updPainel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                    <ContentTemplate>
                                        <asp:GridView ID="dgvUsuarios" DataKeyNames="Id" DataSourceID="dsUsuario" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" Style="max-height: 400px; overflow: auto;" AllowSorting="true" OnSorting="dgvUsuarios_Sorting"
                                            AutoGenerateColumns="false">
                                            <Columns>
                                                <asp:BoundField DataField="Id" HeaderText="#" SortExpression="Id" />
                                                <asp:BoundField DataField="Login" HeaderText="Login" SortExpression="Login" />
                                                <asp:BoundField DataField="Nome" HeaderText="Nome" SortExpression="Nome" />
                                                <asp:BoundField DataField="Email" HeaderText="Email" Visible="false" SortExpression="Email" />
                                                <asp:BoundField DataField="CPF" HeaderText="CPF" SortExpression="CPF" />
                                                <asp:BoundField DataField="TipoUsuarioDescricao" HeaderText="Grupo" SortExpression="TipoUsuarioDescricao" />
                                                <asp:BoundField DataField="Ativo" HeaderText="Ativo" SortExpression="Ativo" />
                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <a href="CadastrarUsuario.aspx?id=<%# Eval("Id") %>" class="btn btn-info btn-sm" data-toggle="tooltip" data-placement="top" title="Editar"><span class="glyphicon glyphicon-edit" style="font-size: large"></a>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                </asp:TemplateField>
                                                <%--    <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <a href="CadastrarUsuario.aspx?id=<%# Eval("Id") %>" class="btn btn-danger btn-sm" data-toggle="tooltip" data-placement="top" title="Excluir"><i class="fa fa-trash"></i></a>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                        </asp:TemplateField>--%>
                                            </Columns>
                                            <HeaderStyle CssClass="headerStyle" />
                                        </asp:GridView>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger EventName="Sorting" ControlID="dgvUsuarios" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>

                        </div>

                    </div>
            </div>
        </div>
    </div>

    </div>

    <div class="modal fade" id="modal-exclusao" tabindex="-1" role="dialog" aria-labelledby="modal-exclusao-label" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">

                <div class="modal-body">
                    <strong>Atenção:</strong> Confirma a Exclusão do Registro? Essa operação não pode ser desfeita!
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Fechar</button>
                    <button type="button" class="btn btn-danger" onclick="confirmarExclusao()">Confirmar Exclusão</button>
                </div>
            </div>
        </div>
    </div>
    <%--<asp:SqlDataSource ID="dsUsuario" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT USU.ID_USUARIO as Id, USU.LOGIN, USU.NOME, USU.EMAIL,USU.CPF, TIPO.NM_TIPO_USUARIO as TipoUsuarioDescricao,
case when FL_Ativo = 1 then 'Sim' else 'Não' end as Ativo,FL_Ativo FROM [dbo].[TB_USUARIO] USU LEFT JOIN TB_TIPO_USUARIO TIPO ON TIPO.ID_TIPO_USUARIO = USU.ID_TIPO_USUARIO"
        >
</asp:SqlDataSource>--%>
    <asp:SqlDataSource ID="dsUsuario" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT USU.ID_USUARIO as Id, USU.LOGIN, USU.NOME, USU.EMAIL,USU.CPF,
case when FL_Ativo = 1 then 'Sim' else 'Não' end as Ativo,FL_Ativo,
(SELECT TOP 1 NM_TIPO_USUARIO FROM TB_TIPO_USUARIO  
 WHERE ID_TIPO_USUARIO = (SELECT TOP 1 ID_TIPO_USUARIO FROM TB_VINCULO_USUARIO WHERE ID_USUARIO = USU.ID_USUARIO))TipoUsuarioDescricao
FROM [dbo].[TB_USUARIO] USU"></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsGruposUsuario" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_TIPO_USUARIO as Id, NM_TIPO_USUARIO as Descricao FROM [dbo].[TB_TIPO_USUARIO] union SELECT 0 as Id, 'Selecione' as Descricao FROM [dbo].[TB_TIPO_USUARIO]
order by ID"></asp:SqlDataSource>

    <asp:SqlDataSource ID="dsTipoUsuario" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>" SelectCommand="SELECT ID_VINCULO,C.NM_TIPO_USUARIO,A.ID_TIPO_USUARIO FROM TB_VINCULO_USUARIO A 
LEFT JOIN TB_TIPO_USUARIO C ON C.ID_TIPO_USUARIO = A.ID_TIPO_USUARIO
WHERE A.ID_USUARIO = @ID_USUARIO">

        <SelectParameters>
            <asp:ControlParameter Name="ID_USUARIO" Type="Int32" ControlID="txtID" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">

    <script src="Content/js/jquery.dataTables.min.js"></script>

    <script>
        $(document).ready(function () {

            $('#<%= dgvUsuarios.ClientID %>').DataTable({
                "language": {
                    "url": "Content/js/pt-br.json"
                }
            });
        });

        function divexpandcollapse(div1) {

            var div = document.getElementById(div1);
            var img = document.getElementById('imgdiv1');

            if (div.style.display === "none") {
                div.style.display = "inline";
                img.src = "Content/imagens/minus.png";
            } else {
                div.style.display = "none";
                img.src = "Content/imagens/plus.png";
            }
        }
    </script>
</asp:Content>
