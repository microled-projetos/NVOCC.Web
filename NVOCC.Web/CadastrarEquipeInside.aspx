<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="CadastrarEquipeInside.aspx.vb" Inherits="NVOCC.Web.CadastrarEquipeInside" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        td, th {
            padding: 0;
            padding-top: 5px;
            margin: 0;
        }

        .btnn {
            background-color: #d5d8db;
            margin: 5px;
            margin: 5px;
            font-size: 13px
        }

        .selected1 {
            color: black;
            font-family: verdana;
            font-size: 8pt;
            background-color: #e6c3a5;
        }
    </style>
    <div class="row principal">
        <div class="col-lg-8 col-lg-offset-2 col-md-12 col-sm-12">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">EQUIPE INSIDE
                    </h3>
                </div>

                <div class="panel-body">
                    <div class="tab-pane fade active in" id="consulta">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="always" ChildrenAsTriggers="True">
                            <ContentTemplate>

                                <div class="alert alert-success" id="divSuccess" runat="server" visible="false">
                                    <asp:Label ID="lblSuccess" runat="server"></asp:Label>
                                </div>
                                <div class="alert alert-danger" id="divErro" runat="server" visible="false">
                                    <asp:Label ID="lblmsgErro" runat="server"></asp:Label>
                                </div>

                                <div class="row linhabotao text-center" style="margin-left: 0px; border: ridge 1px; padding-top: 20px; padding-bottom: 20px; margin-right: 5px;">
                                    <div class="col-sm-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label2" runat="server">Filtro</asp:Label><br />

                                            <asp:DropDownList ID="ddlFiltro" AutoPostBack="true" runat="server" CssClass="form-control" Font-Size="15px">
                                                <asp:ListItem Value="0" Selected="True">Todos os registros</asp:ListItem>
                                                <asp:ListItem Value="1">Nome Lider</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="col-sm-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label4" Style="padding-left: 35px" runat="server"></asp:Label>
                                            <asp:TextBox ID="txtPesquisa" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>


                                    <div class="col-sm-1">
                                        <div class="form-group">
                                            <asp:Label ID="Label3" runat="server"></asp:Label><br />
                                            <asp:Button runat="server" Text="Pesquisar" ID="btnPesquisar" CssClass="btn btn-success" />

                                        </div>
                                    </div>

                                    <div class="col-sm-offset-1 col-sm-3">
                                        <asp:Label ID="Label1" runat="server"></asp:Label><br />
                                        <asp:LinkButton ID="lkCadastrarEquipe" runat="server" CssClass="btn btnn btn-default btn-sm" Style="font-size: 15px">Cadastrar Equipe</asp:LinkButton>
                                    </div>
                                </div>
                                <div runat="server" id="divAuxiliar" style="display: none">
                                    <asp:TextBox ID="txtID" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:TextBox ID="txtlinha" runat="server" CssClass="form-control"></asp:TextBox>

                                </div>
                                <div class="table-responsive tableFixHead DivGrid" id="DivGrid">
                                    <asp:GridView ID="gdvEquipesCadastradas" DataKeyNames="ID_EQUIPE" DataSourceID="dsEquipesCadastradas" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" EmptyDataText="Nenhum registro encontrado.">
                                        <Columns>
                                            <asp:TemplateField HeaderText="ID" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID_EQUIPE") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="NM_EQUIPE" HeaderText="NOME EQUIPE" SortExpression="NM_EQUIPE" />
                                            <asp:BoundField DataField="NOME" HeaderText="NOME LIDER" SortExpression="NOME" />
                                            <asp:BoundField DataField="TAXA_LIDER" HeaderText="TAXA LIDER" SortExpression="TAXA_LIDER" />
                                            <asp:BoundField DataField="TAXA_EQUIPE" HeaderText="TAXA EQUIPE" SortExpression="TAXA_EQUIPE" />
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnEditar" title="Editar" runat="server" CssClass="btn btn-info btn-sm" CommandName="Editar"
                                                        CommandArgument='<%# Eval("ID_EQUIPE") %>' Autopostback="true"><span class="glyphicon glyphicon-edit" style="font-size: large"></span></asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnExcluir" title="Excluir" runat="server" CssClass="btn btn-danger btn-sm" CommandName="Excluir"
                                                        OnClientClick="javascript:return confirm('Deseja realmente excluir este registro?');" CommandArgument='<%# Eval("ID_EQUIPE") %>' Autopostback="true"><span class="glyphicon glyphicon-trash"  style="font-size:large"></span></asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle CssClass="headerStyle" />
                                    </asp:GridView>
                                </div>


                                <ajaxToolkit:ModalPopupExtender ID="mpeMontarEquipe" runat="server" PopupControlID="pnlMontarEquipe" TargetControlID="lkCadastrarEquipe" CancelControlID="TextBox1"></ajaxToolkit:ModalPopupExtender>
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                    <ContentTemplate>
                                        <asp:Panel ID="pnlMontarEquipe" runat="server" CssClass="modalPopup" Style="display: none;">
                                            <center>     <div class=" modal-dialog modal-dialog-centered modal-lg" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title">MONTAR EQUIPE</h5>
                                                        </div>
                                                        <div class="modal-body">                                       
                                 <div class="alert alert-success" id="divSuccessMontarEquipe" runat="server" visible="false">
                                    <asp:Label ID="lblSuccessMontarEquipe" runat="server"></asp:Label>
                                </div>
                                <div class="alert alert-danger" id="divErroMontarEquipe" runat="server" visible="false">
                                    <asp:Label ID="lblErroMontarEquipe" runat="server"></asp:Label>
                                </div>
<asp:TextBox ID="txtIDEquipe" runat="server" CssClass="form-control" style="display:none" ></asp:TextBox>
                           <div class="row" runat="server">
                                <br />
                               <div class="col-sm-6">
                                            <div class="form-group">
                                                <label class="control-label">Busca Lider:</label>
                                                <asp:TextBox ID="txtNomeLider" runat="server" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                                            </div>
                                        </div>
                                <div class="col-sm-6">
                                    <div class="form-group">
                                         <label class="control-label">Lider:</label> 
                                         <asp:DropDownList ID="ddlLider" runat="server" CssClass="form-control" Font-Size="11px"  DataValueField="ID_USUARIO" DataTextField="NOME" DataSourceID="dsBuscaLider" >
                                        </asp:DropDownList>
                                        <asp:TextBox ID="txtIDLider" runat="server" CssClass="form-control" style="display:none" ></asp:TextBox>
                                    </div>
                                </div>
                               
                               </div>
                                                             <div class="row">
                                                                 <div class="col-sm-6">
                                            <div class="form-group">
                                                <label class="control-label">Nome Equipe:</label>
                                                <asp:TextBox ID="txtNomeEquipe" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                                                 <div class="col-sm-2">
                                            <div class="form-group">
                                                <label class="control-label">Taxa Lider:</label>
                                                <asp:TextBox ID="txtTaxaLider" runat="server" CssClass="form-control valores"></asp:TextBox>
                                            </div>
                                        </div>
                                                                 <div class="col-sm-2">
                                            <div class="form-group">
                                                <label class="control-label">Taxa Equipe:</label>
                                                <asp:TextBox ID="txtTaxaEquipe" runat="server" CssClass="form-control valores"></asp:TextBox>
                                            </div>
                                        </div>
                               <div class="col-sm-2">
                                    <div class="form-group">
                                         <label class="control-label" style="color:white">Lider:</label><br />
                               <asp:Button runat="server" CssClass="btn btn-success" ID="btnCadastrarLider" text="Cadastrar Lider" visible="true" />
                               <asp:Button runat="server" CssClass="btn btn-success" ID="btnSalvarEdicao" text="Salvar Edição" visible="false" />
                           </div>

                               </div>

                           </div>
                                <div class="row" id="divEquipe" runat="server" visible="false" >
                                 <div class="col-sm-1" style="display:none" >
                                            <div class="form-group">
                                                <label class="control-label">Cód Membro:</label>
                                                <asp:TextBox ID="txtCodMembro" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                <div class="col-sm-3">
                                            <div class="form-group">
                                                <label class="control-label">Busca Membro:</label>
                                                <asp:TextBox ID="txtBuscaMembros" runat="server" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                                            </div>
                                        </div>
                                <div class="col-sm-5">
                                    <div class="form-group">
                                        <label class="control-label">Membro:</label></label><label runat="server" style="color:red" >*</label>
                                       <asp:DropDownList ID="ddlMembro" runat="server" CssClass="form-control" Font-Size="11px"  DataValueField="ID_USUARIO" DataTextField="NOME" DataSourceID="dsBuscaMembrosEquipes" >
                                        </asp:DropDownList>
                                    </div>
                                </div>                             
                                <div class="col-sm-2">
                                    <div class="form-group">
                                         <label class="control-label" style="color:white">Membro:</label><br />
                                         <asp:Button runat="server" CssClass="btn btn-primary" ID="btnAdicionarMembro" text="Adicionar Membro" />
                                    </div>
                                </div>
                                    <div class="col-sm-2">
                                    <div class="form-group">
                                         <label class="control-label" style="color:white">Membro:</label><br />
                                         <asp:Button runat="server" CssClass="btn btn-primary" ID="btnAdicionarTime" text="Adicionar Time" />
                                    </div>
                                </div>
                                <br />     <br /> 
                            <div class="row">
                            
                                <div class="col-sm-12">
                                    <div class="form-group">
                                      <div class="table-responsive tableFixHead" style="margin:10px;max-height: 300px;">
                                        <asp:GridView ID="gdvMembrosEquipesCadastradas" DataKeyNames="ID_EQUIPE_MEMBROS" DataSourceID="dsMembrosEquipesCadastradas" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" Style="max-height: 300px; overflow: auto;"
                                            AutoGenerateColumns="false" EmptyDataText="Nenhum registro encontrado.">
                                            <Columns>
                                                <asp:BoundField DataField="ID_USUARIO" Visible="False" HeaderText="ID_USUARIO" SortExpression="ID_USUARIO" />
                                                <asp:BoundField DataField="NOME" HeaderText="NOME" SortExpression="NOME" />                                                
                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                                 <asp:linkButton CommandName="Editar" Visible='<%#Eval("ID_TIME") <> 0 %>'  CommandArgument='<%# Eval("ID_TIME") %>' runat="server"  CssClass="btn btn-info btn-sm" data-toggle="tooltip" data-placement="top" title="Editar"><span class="glyphicon glyphicon-edit" style="font-size:medium"></span></span></asp:linkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                                 <asp:linkButton ID="btnExcluir" title="Excluir" runat="server"  CssClass="btn btn-danger btn-sm" CommandName="Excluir"
                                OnClientClick="javascript:return confirm('Deseja realmente excluir este usuario da equipe?');"  CommandArgument='<%# Eval("ID_EQUIPE_MEMBROS") %>' Autopostback="true" ><span class="glyphicon glyphicon-trash" style="font-size:medium"></span></asp:linkButton>
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
                          
                                                                </div> </div>
                               <div class="modal-footer"> 

                                         <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFecharMontarEquipe" text="Close" />
                                                                 

                                                        </div>                                                    
                                            
                                       </div>     </center>
                                        </asp:Panel>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnCadastrarLider" />
                                        <asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="gdvMembrosEquipesCadastradas" />
                                        <asp:AsyncPostBackTrigger ControlID="txtNomeLider" />
                                        <asp:AsyncPostBackTrigger ControlID="txtBuscaMembros" />
                                    </Triggers>
                                </asp:UpdatePanel>



                                  <ajaxToolkit:ModalPopupExtender ID="mpeMontarTime" runat="server" PopupControlID="pnlMontarTime" TargetControlID="TextBox2" CancelControlID="TextBox1"></ajaxToolkit:ModalPopupExtender>
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                    <ContentTemplate>
                                        <asp:Panel ID="pnlMontarTime" runat="server" CssClass="modalPopup" Style="display: none;">
                                            <center>     <div class=" modal-dialog modal-dialog-centered modal-lg" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title">MONTAR TIME</h5>
                                                        </div>
                                                        <div class="modal-body">                                       
                                 <div class="alert alert-success" id="divTimeSuccess" runat="server" visible="false">
                                    <asp:Label ID="lblSuccessTime" runat="server"></asp:Label>
                                </div>
                                <div class="alert alert-danger" id="divTimeErro" runat="server" visible="false">
                                    <asp:Label ID="lblErroTime" runat="server"></asp:Label>
                                </div>
                            <div class="row" runat="server">
                                <div class="col-sm-8">
                                            <div class="form-group">
                                                <label class="control-label">Nome Time:</label>
                                                <asp:TextBox ID="txtIDTime" runat="server" CssClass="form-control" style="display:none" ></asp:TextBox>
                                                <asp:TextBox ID="txtNomeTime" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                         <div class="col-sm-2">
                                            <div class="form-group">
                                                <label class="control-label">Qtd. Membros:</label>
                                                <asp:TextBox ID="txtQtdMembrosTime" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                   
                               <div class="col-sm-2">
                                    <div class="form-group">
                                          <label class="control-label" style="color:white">:</label><br />
                                <asp:Button runat="server" CssClass="btn btn-success" ID="btnSalvarTime" text="Salvar" />
                           </div>

                               </div>

                           </div>
                                <div class="row" id="divMembroTime" runat="server" visible="false" >
                                 <div class="col-sm-1" style="display:none" >
                                            <div class="form-group">
                                                <label class="control-label">Cód Membro:</label>
                                                <asp:TextBox ID="txtCodMembrosTime" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                <div class="col-sm-3">
                                            <div class="form-group">
                                                <label class="control-label">Busca Membro:</label>
                                                <asp:TextBox ID="txtBuscaMembrosTime" runat="server" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                                            </div>
                                        </div>
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="control-label">Membro:</label></label><label runat="server" style="color:red" >*</label>
                                       <asp:DropDownList ID="ddlMembroTime" runat="server" CssClass="form-control" Font-Size="11px"  DataValueField="ID_USUARIO" DataTextField="NOME" DataSourceID="dsBuscaMembrosTime" >
                                        </asp:DropDownList>
                                    </div>
                                </div>                             
                                <div class="col-sm-3">
                                    <div class="form-group">
                                         <label class="control-label" style="color:white">Membro:</label><br />
                                         <asp:Button runat="server" CssClass="btn btn-primary" ID="btnAdicionarMembroTime" text="Adicionar Membro" />
                                    </div>
                                </div>
                                <br />     <br /> 
                            <div class="row">
                            
                                <div class="col-sm-12">
                                    <div class="form-group">
                                      <div class="table-responsive tableFixHead" style="margin:10px;max-height: 300px;">
                                        <asp:GridView ID="dgvMembrosTime" DataKeyNames="ID_TIME" DataSourceID="dsMembrosTime" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" Style="max-height: 300px; overflow: auto;"
                                            AutoGenerateColumns="false" EmptyDataText="Nenhum registro encontrado.">
                                            <Columns>
                                                <asp:BoundField DataField="ID_USUARIO" Visible="False" HeaderText="ID_USUARIO" SortExpression="ID_USUARIO" />
                                                <asp:BoundField DataField="NOME" HeaderText="NOME" SortExpression="NOME" />                                                
                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                                 <asp:linkButton ID="btnExcluir" title="Excluir" runat="server"  CssClass="btn btn-danger btn-sm" CommandName="Excluir"
                                OnClientClick="javascript:return confirm('Deseja realmente excluir este usuario do Time?');"  CommandArgument='<%# Eval("ID_TIME") %>' Autopostback="true" ><span class="glyphicon glyphicon-trash" style="font-size:medium"></span></asp:linkButton>
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
                          
                                                                </div> 

                                                        </div>
                               <div class="modal-footer"> 

                                         <asp:Button runat="server" CssClass="btn btn-secondary" ID="btnFecharMontaTime" text="Close" />
                                                                 

                                                        </div>                                                    
                                            
                                       </div>     </center>
                                        </asp:Panel>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnSalvarTime" />
                                        <asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="dgvMembrosTime" />
                                        <asp:AsyncPostBackTrigger ControlID="txtBuscaMembrosTime" />
                                        <asp:AsyncPostBackTrigger ControlID="btnFecharMontaTime" />
                                    </Triggers>
                                </asp:UpdatePanel>





                                    <asp:TextBox ID="TextBox2" Style="display: none" runat="server"></asp:TextBox>
                                    <asp:TextBox ID="TextBox1" Style="display: none" runat="server"></asp:TextBox>
                            </ContentTemplate>
                            <Triggers>

                                <asp:AsyncPostBackTrigger ControlID="btnPesquisar" />
                                <asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="gdvMembrosEquipesCadastradas" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>

                </div>


            </div>
        </div>


    </div><asp:SqlDataSource ID="dsBuscaLider" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="select ID_USUARIO,NOME from TB_USUARIO WHERE (NOME  like '%' + @NOME + '%' or ID_USUARIO =  @ID_USUARIO) UNION SELECT 0, '   Selecione' ORDER BY NOME">
        <SelectParameters>
            <asp:ControlParameter Name="NOME" Type="String" ControlID="txtNomeLider" DefaultValue="NULL" />
            <asp:ControlParameter Name="ID_USUARIO" Type="Int32" ControlID="txtIDLider" DefaultValue="0" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="dsBuscaMembrosEquipes" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="select ID_USUARIO,NOME from TB_USUARIO WHERE (NOME  like '%' + @NOME + '%' or ID_USUARIO =  @ID_USUARIO) UNION SELECT 0, '   Selecione' ORDER BY NOME">
        <SelectParameters>
            <asp:ControlParameter Name="NOME" Type="String" ControlID="txtBuscaMembros" DefaultValue="NULL" />
            <asp:ControlParameter Name="ID_USUARIO" Type="Int32" ControlID="txtCodMembro" DefaultValue="0" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="dsMembrosEquipesCadastradas" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT M.ID_EQUIPE_MEMBROS,M.ID_EQUIPE,U.ID_USUARIO,ISNULL(M.ID_TIME,0)ID_TIME, CASE WHEN M.ID_TIME IS NULL THEN U.NOME ELSE T.NM_TIME END NOME
FROM TB_INSIDE_EQUIPE E
INNER JOIN TB_INSIDE_EQUIPE_MEMBROS M ON E.ID_EQUIPE = M.ID_EQUIPE  
LEFT JOIN TB_USUARIO U ON U.ID_USUARIO = M.ID_USUARIO_MEMBRO_EQUIPE 
LEFT JOIN TB_INSIDE_TIME T ON T.ID_TIME = M.ID_TIME
WHERE E.ID_USUARIO_LIDER = @ID_USUARIO_LIDER ORDER BY NOME">
        <SelectParameters>
            <asp:ControlParameter Name="ID_USUARIO_LIDER" Type="Int32" ControlID="txtIDLider" />
        </SelectParameters>
    </asp:SqlDataSource>

      <asp:SqlDataSource ID="dsBuscaMembrosTime" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="select ID_USUARIO,NOME from TB_USUARIO WHERE (NOME  like '%' + @NOME + '%' or ID_USUARIO =  @ID_USUARIO) UNION SELECT 0, '   Selecione' ORDER BY NOME">
        <SelectParameters>
            <asp:ControlParameter Name="NOME" Type="String" ControlID="txtBuscaMembrosTime" DefaultValue="NULL" />
            <asp:ControlParameter Name="ID_USUARIO" Type="Int32" ControlID="txtCodMembrosTime" DefaultValue="0" />
        </SelectParameters>
    </asp:SqlDataSource>

     <asp:SqlDataSource ID="dsMembrosTime" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT T.ID_TIME,U.ID_USUARIO, U.NOME
FROM TB_INSIDE_TIME T
INNER JOIN TB_INSIDE_TIME_MEMBROS M ON T.ID_TIME = M.ID_TIME 
LEFT JOIN TB_USUARIO U ON U.ID_USUARIO = M.ID_USUARIO_MEMBRO_TIME
WHERE T.ID_TIME = @ID_TIME ORDER BY NOME">
        <SelectParameters>
            <asp:ControlParameter Name="ID_TIME" Type="Int32" ControlID="txtIDTime" />
        </SelectParameters>
    </asp:SqlDataSource>

     <asp:SqlDataSource ID="dsEquipesCadastradas" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="select ID_EQUIPE,ID_USUARIO,NOME,NM_EQUIPE,TAXA_LIDER,TAXA_EQUIPE from TB_USUARIO A
INNER JOIN TB_INSIDE_EQUIPE B ON A.ID_USUARIO = B.ID_USUARIO_LIDER
ORDER BY NOME"></asp:SqlDataSource>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">
    <script>
        $(document).ready(function () {
            Mascara();
        });

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);

        function EndRequestHandler(sender, args) {
            Mascara();
        };

        function Mascara() {
            $(".valores").on("keypress keyup blur", function (e) {
                console.log("entrou")
                var chr = String.fromCharCode(e.which);
                if ("1234567890,".indexOf(chr) < 0)
                    return false;

            });
        };

    </script>
</asp:Content>
