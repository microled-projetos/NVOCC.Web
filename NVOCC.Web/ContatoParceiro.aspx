<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ContatoParceiro.aspx.vb" Inherits="NVOCC.Web.ContatoParceiro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
        <div class="row principal">
        <div class="col-lg-6 col-lg-offset-3 col-md-12 col-sm-12">

            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">CONTATO - PARCEIROS
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
 <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
    <ContentTemplate>
                    <div class="tab-content">
                        <div class="tab-pane fade active in" id="cadastro">
                            

                            <div class="row">

                                <div class="col-sm-12">
                                    <br />
                                    <asp:ValidationSummary ID="Validacoes" runat="server" ShowModelStateErrors="true" CssClass="alert alert-danger" />

                                    <div class="alert alert-success" id="divmsg" runat="server" visible="false">
                                        Registro cadastrado/atualizado com sucesso!
                                    </div>
                                    <div class="alert alert-danger" id="divmsg1" runat="server" visible="false">
                                    <asp:Label runat="server"  ID="msgErro" />
                                    </div>
                                    <div class="alert alert-warning" id="divInformativa"  visible="false" runat="server">
                                    <asp:label ID="lblInformacao" runat="server"></asp:label>
                                    </div>

                                </div>
                            </div>

                            
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <label class="control-label">Código:</label>
                                        <asp:TextBox ID="txtID" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>  
                                <div class="col-sm-9">
                                    <div class="form-group">
                                        <label class="control-label">Parceiro:</label>
                                        <asp:DropDownList ID="ddlParceiro" DataTextField="NM_RAZAO" DataSourceID="dsParceiro" DataValueField="ID_PARCEIRO" runat="server" CssClass="form-control" Font-Size="11px" AutoPostBack="True" Enabled="false">                                  
                                        </asp:DropDownList>
                                    </div>
                                </div>  
                            </div>

                            <div class="row">
                               
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="control-label">Nome:</label>
                                        <asp:TextBox ID="txtNomeContato" runat="server"  CssClass="form-control" MaxLength="50"></asp:TextBox>
                                    </div>
                                </div>
                            <div class="col-sm-3">
                                    <div class="form-group">
                                        <label class="control-label">Telefone:</label>
                                        <asp:TextBox ID="txtTelContato" runat="server"  CssClass="form-control telefone" MaxLength="50"></asp:TextBox>
                                    </div>
                                </div> 
                                 <div class="col-sm-3">
                                    <div class="form-group">
                                        <label class="control-label">Celular:</label>
                                        <asp:TextBox ID="txtCelularContato" runat="server"  CssClass="form-control celular" MaxLength="50"></asp:TextBox>
                                    </div>
                                </div>   
                            </div>
                            <div class="row">
                               
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="control-label">Email:</label>
                                        <asp:TextBox ID="txtEmailContato" runat="server"  CssClass="form-control" MaxLength="200"></asp:TextBox>
                                    </div>
                                </div>
                            <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="control-label">Departamento:</label>
                                        <asp:TextBox ID="txtDepartamento" runat="server"  CssClass="form-control" MaxLength="30"></asp:TextBox>
                                    </div>
                                </div>                                </div>
                
                                 <div class="row">

                                <div class="col-sm-3 col-sm-offset-6">
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <asp:Button ID="btnLimpar" runat="server" CssClass="btn btn-warning btn-block" Text="Limpar Campos"  />
                                    </div>
                                </div>

                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <asp:button  ID="btnGravar" OnClientClick="return confirm('Deseja realmente gravar estes dados?')"  runat="server" CssClass="btn btn-primary btn-block" Text="Gravar"  />
                                    </div>
                                </div>
                            </div>
                             
                </div>
                         <div class="tab-pane fade" id="consulta">
                              <br />
                            <div class="table-responsive">
                                  <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True">
    <ContentTemplate>
                                <div class="alert alert-success" id="divSuccesgrid" runat="server" visible="false" >
                                    <asp:Label runat="server"  ID="lblSuccesgrid" />
                                    </div>
                                    <div class="alert alert-danger" id="divErrogrid" runat="server" visible="false">
                                    <asp:Label runat="server"  ID="lblErrogrid" />
                                    </div>
                               <asp:GridView ID="dgvContato" DataKeyNames="Id" DataSourceID="dsContato" EmptyDataText="Esse parceiro não possui contato cadastrado."  CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" AllowSorting="true" OnSorting="dgvContato_Sorting" AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:BoundField DataField="Id" HeaderText="#" SortExpression="Id" />   
                                        <asp:BoundField DataField="NM_RAZAO" HeaderText="Parceiro" SortExpression="NM_RAZAO" Visible="false"/>
                                        <asp:BoundField DataField="NM_CONTATO" HeaderText="Nome" SortExpression="NM_CONTATO" />
                                        <asp:BoundField DataField="EMAIL_CONTATO" HeaderText="Email" SortExpression="EMAIL_CONTATO" />
                                        <asp:BoundField DataField="CELULAR_CONTATO" HeaderText="Celular" SortExpression="CELULAR_CONTATO" />
                         
                                         <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="False" CommandName="Delete"
                                Text="Excluir" OnClientClick="return confirm('Tem certeza que deseja excluir esse registro?')"  CssClass="btn btn-danger btn-sm" ><span class="glyphicon glyphicon-trash"  style="font-size:medium"></span></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle CssClass="headerStyle" />
                                </asp:GridView>
                                
                                     </ContentTemplate>
    <Triggers>
       <asp:AsyncPostBackTrigger EventName="Sorting" ControlID="dgvContato" />
    </Triggers>
</asp:UpdatePanel>
                            </div>
                        </div> </div>
                </ContentTemplate>
    <Triggers>
    </Triggers>
</asp:UpdatePanel>  
            </div>
        </div>
</div>
           

</div>

 <asp:SqlDataSource ID="dsParceiro" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT ID_PARCEIRO, NM_RAZAO FROM [dbo].[TB_PARCEIRO] union SELECT  0, ' Selecione' FROM [dbo].[TB_PARCEIRO] #FILTRO Order by NM_RAZAO">
</asp:SqlDataSource>
    <asp:SqlDataSource ID="dsContato" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT  A.ID_CONTATO as Id,A.ID_PARCEIRO,B.NM_RAZAO, A.NM_CONTATO,A.TELEFONE_CONTATO,A.CELULAR_CONTATO,A.NM_DEPARTAMENTO,A.EMAIL_CONTATO FROM [dbo].[TB_CONTATO] A LEFT JOIN TB_PARCEIRO B ON B.ID_PARCEIRO = A.ID_PARCEIRO WHERE A.ID_PARCEIRO = @ID ORDER BY ID_CONTATO" 
        DeleteCommand="DELETE FROM [dbo].[TB_CONTATO] WHERE ID_CONTATO = @ID">  
        <DeleteParameters>
                    <asp:Parameter Name="ID" Type="Int32" />
                </DeleteParameters>
        <SelectParameters>
          <asp:QueryStringParameter Name="ID" QueryStringField="id" />
        </SelectParameters>
</asp:SqlDataSource>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
