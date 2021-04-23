<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="CadastroNavios.aspx.vb" Inherits="NVOCC.Web.CadastroNavios" %>
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

                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <label class="control-label">Código:</label>
                                        <asp:TextBox ID="txtID" runat="server" required="true" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>

                            </div>

                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="form-group">
                                        <label class="control-label">NOME:</label>
                                        <asp:TextBox ID="txtNM_Navio" runat="server" required="true" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                             <div class="row">
                                <div class="col-sm-12">
                                    <div class="form-group">
                                        <label class="control-label">CD_LOYD:</label>
                                        <asp:TextBox ID="txtCD_Loyd" runat="server" required="true" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">                               
                                <div class="col-sm-12">
                    <div class="form-group">
                         <label class="control-label">Pais:</label>
                                <asp:DropDownList ID="ddlPais" runat="server" CssClass="form-control" Font-Size="11px" AutoPostBack="True" DataTextField="NM_PAIS" DataSourceID="dsPais" DataValueField="ID_PAIS">
                                </asp:DropDownList>
                   </div>
                 </div>

                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="form-group">
                                        <asp:CheckBox ID="ckbAtivo" runat="server" Text="Ativo" />
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
                                <asp:GridView ID="dgvNavios" DataKeyNames="Id" DataSourceID="dsNavios"  CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="10">
                                    <Columns>
                                        <asp:BoundField DataField="Id" HeaderText="#" />
                                        <asp:BoundField DataField="Descricao" HeaderText="Descrição" />
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <a href="CadastrarUsuarioGrupo.aspx?id=<%# Eval("Id") %>" class="btn btn-info btn-sm" data-toggle="tooltip" data-placement="top" title="Editar"><span class="glyphicon glyphicon-edit" style="font-size:medium"></a>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>                    
                                   <ItemTemplate>                          
                            <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="False" CommandName="Delete"
                                Text="Excluir" OnClientClick="return confirm('Tem certeza que deseja excluir esse registro?')"  CssClass="btn btn-danger btn-sm" ><span class="glyphicon glyphicon-trash"  style="font-size:medium"></span></asp:LinkButton>
                                   </ItemTemplate>                                        
                    </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle CssClass="headerStyle" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>

    </div>
     <asp:SqlDataSource ID="dsPais" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_PAIS, NM_PAIS FROM [dbo].[TB_PAIS]" >
        </asp:SqlDataSource>

        <asp:SqlDataSource ID="dsNavios" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_NAVIO as Id, NM_NAVIO as Descricao FROM [dbo].[TB_NAVIO] ORDER BY " 
        DeleteCommand="DELETE FROM [dbo].[TB_NAVIO] WHERE ID_NAVIO = @Id">  
        <DeleteParameters>
                    <asp:Parameter Name="ID" Type="Int32" />
                </DeleteParameters>
        </asp:SqlDataSource>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">
        <script src="Content/js/jquery.dataTables.min.js"></script>

    <script>
        $(document).ready(function () {

            $('#<%= dgvNavios.ClientID %>').DataTable({
                "language": {
                    "url": "Content/js/pt-br.json"
                }
            });
        });

       
    </script>
</asp:Content>
