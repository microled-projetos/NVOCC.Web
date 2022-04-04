<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="CadastrarEquipe.aspx.vb" Inherits="NVOCC.Web.CadastrarEquipe" %>
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
                                        <asp:GridView ID="gdvLideres" DataKeyNames="ID" DataSourceID="dsLideres" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" EmptyDataText="Nenhum registro encontrado.">
                                            <Columns>
                                                <asp:TemplateField HeaderText="ID" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="NM_EQUIPE" HeaderText="NOME EQUIPE" SortExpression="NM_EQUIPE" />
                                                <asp:BoundField DataField="NOME" HeaderText="NOME LIDER" SortExpression="NOME" />
                                                <asp:BoundField DataField="TAXA_LIDER" HeaderText="TAXA LIDER" SortExpression="TAXA_LIDER" />
                                                <asp:BoundField DataField="TAXA_EQUIPE" HeaderText="TAXA EQUIPE" SortExpression="TAXA_EQUIPE" />
                                              <asp:TemplateField HeaderText="">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="btnEditar" title="Editar" runat="server" CssClass="btn btn-info btn-sm" CommandName="Editar"
                                                                            CommandArgument='<%# Eval("ID") %>' Autopostback="true"><span class="glyphicon glyphicon-edit" style="font-size: large"></span></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                                </asp:TemplateField>
                                                   <asp:TemplateField HeaderText="">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="btnExcluir" title="Excluir" runat="server" CssClass="btn btn-danger btn-sm" CommandName="Excluir"
                                                                            OnClientClick="javascript:return confirm('Deseja realmente excluir este registro?');" CommandArgument='<%# Eval("ID") %>' Autopostback="true"><span class="glyphicon glyphicon-trash"  style="font-size:large"></span></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle CssClass="headerStyle" />
                                        </asp:GridView>
                                    </div>

                               

                             

                                                                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="pnlMontarEquipe" TargetControlID="lkCadastrarEquipe" CancelControlID="TextBox1"></ajaxToolkit:ModalPopupExtender>
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
<asp:TextBox ID="txtIDEdicao" runat="server" CssClass="form-control" style="display:none" ></asp:TextBox>
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
                                         <asp:DropDownList ID="ddlLider" runat="server" CssClass="form-control" Font-Size="11px"  DataValueField="ID_USUARIO" DataTextField="NOME" DataSourceID="dsLider" >
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
                                                <asp:TextBox ID="txtTaxaLider" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                                                 <div class="col-sm-2">
                                            <div class="form-group">
                                                <label class="control-label">Taxa Equipe:</label>
                                                <asp:TextBox ID="txtTaxaEquipe" runat="server" CssClass="form-control"></asp:TextBox>
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
                                                         <br />     <br /> 
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
                                                <asp:TextBox ID="txtNomeMembro" runat="server" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                                            </div>
                                        </div>
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="control-label">Membro:</label></label><label runat="server" style="color:red" >*</label>
                                       <asp:DropDownList ID="ddlMembro" runat="server" CssClass="form-control" Font-Size="11px"  DataValueField="ID_USUARIO" DataTextField="NOME" DataSourceID="dsMembros" >
                                        </asp:DropDownList>
                                    </div>
                                </div>                             
                                <div class="col-sm-3">
                                    <div class="form-group">
                                         <label class="control-label" style="color:white">Membro:</label><br />
                                         <asp:Button runat="server" CssClass="btn btn-primary" ID="btnAdicionarMembro" text="Adicionar Membro" />
                                    </div>
                                </div>
                                <br />     <br /> 
                            <div class="row">
                            
                                <div class="col-sm-12">
                                    <div class="form-group">
                                      <div class="table-responsive tableFixHead" style="margin:10px">
                                        <asp:GridView ID="gdvEquipeLider" DataKeyNames="ID" DataSourceID="dsEquipeLider" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" Style="max-height: 400px; overflow: auto;"
                                            AutoGenerateColumns="false" EmptyDataText="Nenhum registro encontrado.">
                                            <Columns>
                                                <asp:BoundField DataField="ID_USUARIO" Visible="False" HeaderText="ID_USUARIO" SortExpression="ID_USUARIO" />
                                                <asp:BoundField DataField="NOME" HeaderText="NOME" SortExpression="NOME" />                                                
                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                                 <asp:linkButton ID="btnExcluir" title="Excluir" runat="server"  CssClass="btn btn-danger btn-sm" CommandName="Excluir"
                                OnClientClick="javascript:return confirm('Deseja realmente excluir este usuario da equipe?');"  CommandArgument='<%# Eval("ID") %>' Autopostback="true" ><span class="glyphicon glyphicon-trash" style="font-size:medium"></span></asp:linkButton>
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
                                        <asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="gdvEquipeLider" />
                                        <asp:AsyncPostBackTrigger ControlID="txtNomeLider" />
                                        <asp:AsyncPostBackTrigger ControlID="txtNomeMembro" />
                                    </Triggers>
                                </asp:UpdatePanel>








                               

                            </ContentTemplate>
                            <Triggers>
                                
                                <asp:AsyncPostBackTrigger ControlID="btnPesquisar" />
                               <asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="gdvEquipeLider" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>

                </div>


            </div>
        </div>

    </div>
    <asp:TextBox ID="TextBox1" Style="display: none" runat="server"></asp:TextBox>
    <asp:SqlDataSource ID="dsMembros" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="select ID_USUARIO,NOME from TB_USUARIO WHERE (NOME  like '%' + @NOME + '%' or ID_USUARIO =  @ID_USUARIO) UNION SELECT 0, '   Selecione' ORDER BY NOME">
        <SelectParameters>
           <asp:ControlParameter Name="NOME" Type="String" ControlID="txtNomeMembro"  DefaultValue ="NULL"  />
            <asp:ControlParameter Name="ID_USUARIO" Type="Int32" ControlID="txtCodMembro" DefaultValue ="0" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="dsLider" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="select ID_USUARIO,NOME from TB_USUARIO WHERE (NOME  like '%' + @NOME + '%' or ID_USUARIO =  @ID_USUARIO) UNION SELECT 0, '   Selecione' ORDER BY NOME">
        <SelectParameters>
           <asp:ControlParameter Name="NOME" Type="String" ControlID="txtNomeLider"  DefaultValue ="NULL"  />
            <asp:ControlParameter Name="ID_USUARIO" Type="Int32" ControlID="txtIDLider" DefaultValue ="0" />
        </SelectParameters>
    </asp:SqlDataSource>


    <asp:SqlDataSource ID="dsEquipeLider" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="select ID,ID_USUARIO,NOME from TB_USUARIO A
INNER JOIN TB_EQUIPE_MEMBROS B ON A.ID_USUARIO = B.ID_USUARIO_MEMBRO_EQUIPE  
WHERE ID_USUARIO_LIDER = @ID_USUARIO_LIDER ORDER BY NOME">
          <SelectParameters>
            <asp:ControlParameter Name="ID_USUARIO_LIDER" Type="Int32" ControlID="txtIDLider" />
        </SelectParameters>
    </asp:SqlDataSource>

     <asp:SqlDataSource ID="dsLideres" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="select ID,ID_USUARIO,NOME,NM_EQUIPE,TAXA_LIDER,TAXA_EQUIPE from TB_USUARIO A
INNER JOIN TB_EQUIPE_LIDER B ON A.ID_USUARIO = B.ID_USUARIO_LIDER
ORDER BY NOME">
    </asp:SqlDataSource>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
