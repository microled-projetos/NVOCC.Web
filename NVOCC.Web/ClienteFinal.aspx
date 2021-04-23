<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ClienteFinal.aspx.vb" Inherits="NVOCC.Web.ClienteFinal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
        <div class="row principal">
        <div class="col-lg-6 col-lg-offset-3 col-md-12 col-sm-12">

            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">CLIENTE
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
                            </div>

                                <div class="row" >
                                <div class="col-sm-6"  >
                                    <div class="form-group">
                                        <label class="control-label">CNPJ:</label><label runat="server" style="color:red" >*</label>
                                        <asp:TextBox ID="txtCNPJ"  runat="server" autopostback="true" CssClass="form-control cnpj" MaxLength="18"></asp:TextBox>
                                    </div>
                                </div>                                
                          </div>
                            <div class="row">
                               
                                <div class="col-sm-9">
                                    <div class="form-group">
                                        <label class="control-label">Nome:</label>
                                        <asp:TextBox ID="txtNome" runat="server"  CssClass="form-control" MaxLength="100"></asp:TextBox>
                                    </div>
                                </div>
                                  
                                </div>
                 <div class="row"STYLE="display:NONE">
                             <div class="col-sm-3">
                                    <div class="form-group">
                                        <label class="control-label">Parceiro:</label><label runat="server" style="color:red" >*</label>
                                         <asp:DropDownList ID="ddlParceiro" DataTextField="NM_RAZAO" DataSourceID="dsParceiro" DataValueField="ID_PARCEIRO" runat="server" CssClass="form-control" Font-Size="11px" AutoPostBack="True">                                  
                                        </asp:DropDownList>
                                    </div>
                                </div>
                          </div>
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
                               <asp:GridView ID="dgvCliente" DataKeyNames="Id" DataSourceID="dsCliente" EmptyDataText="Esse parceiro não possui cliente cadastrado."  CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" AllowSorting="true" OnSorting="dgvCliente_Sorting" AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:BoundField DataField="Id" HeaderText="#" SortExpression="Id" />                                
                                        <asp:BoundField DataField="NM_CLIENTE_FINAL" HeaderText="Nome" SortExpression="NM_CLIENTE_FINAL" />
                                        <asp:BoundField DataField="NR_CNPJ" HeaderText="CNPJ" SortExpression="NR_CNPJ" />
                                        <asp:BoundField DataField="NM_RAZAO" HeaderText="Parceiro" SortExpression="NM_RAZAO"/>
                                        <%--   <asp:TemplateField HeaderText="">
                                               <ItemTemplate>
                                             <a href="ClienteFinal.aspx?id=<%# Eval("Id") %>" class="btn btn-info btn-sm" data-toggle="tooltip" data-placement="top" title="Editar"><span class="glyphicon glyphicon-edit" style="font-size:medium"></a>
                                                <asp:LinkButton ID="Editar" runat="server" CausesValidation="False" CssClass="btn btn-primary" CommandName="Alterar"
                                    Text="Editar" CommandArgument='<%# Eval("Id") %>'  ><span class="glyphicon glyphicon-edit"  style="font-size:medium"></span></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                        </asp:TemplateField>--%>
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
       <asp:AsyncPostBackTrigger EventName="Sorting" ControlID="dgvCliente" />
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
        selectcommand="SELECT ID_PARCEIRO, NM_RAZAO FROM [dbo].[TB_PARCEIRO] union SELECT  0, 'Selecione' FROM [dbo].[TB_PARCEIRO] #FILTRO Order by ID_PARCEIRO">
</asp:SqlDataSource>
    <asp:SqlDataSource ID="dsCliente" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT  A.ID_CLIENTE_FINAL as Id,A.ID_PARCEIRO,B.NM_RAZAO, A.NR_CNPJ,A.NM_CLIENTE_FINAL FROM [dbo].[TB_CLIENTE_FINAL] A LEFT JOIN TB_PARCEIRO B ON B.ID_PARCEIRO = A.ID_PARCEIRO WHERE A.ID_PARCEIRO = @ID ORDER BY ID_CLIENTE_FINAL" 
        DeleteCommand="DELETE FROM [dbo].[TB_CLIENTE_FINAL] WHERE ID_CLIENTE_FINAL = @ID">  
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
