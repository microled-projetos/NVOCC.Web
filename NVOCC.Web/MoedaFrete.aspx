<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="MoedaFrete.aspx.vb" Inherits="NVOCC.Web.MoedaFrete" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
      <div class="row principal">
        <div class="col-lg-6 col-lg-offset-3 col-md-12 col-sm-12">

            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">TAXA CÂMBIO FCA
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

                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <label class="control-label">Código:</label>
                                        <asp:TextBox ID="txtIDMoedaFrete" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>

                            </div>

                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Valor TX Oficial:</label>
                                        <asp:TextBox ID="txtTxOficial" runat="server" CssClass="form-control" MaxLength="15" onkeypress="return nomeFuncao( this , event ) ;"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <label class="control-label">Data Câmbio:</label>
                                        <asp:TextBox ID="txtDataCambio" runat="server" CssClass="form-control data" MaxLength="100"></asp:TextBox>
                                    </div>
                                </div>
                  
                                <div class="col-sm-4">
                                    <div class="form-group">
                                       <label class="control-label">Moeda:</label>
                                       <asp:DropDownList ID="ddlMoeda" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_MOEDA" DataSourceID="dsMoeda" DataValueField="ID_MOEDA">
                                       </asp:DropDownList>
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
                           
                                 <asp:UpdatePanel ID="updPainel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True">
    <ContentTemplate>
        <div class="row" style="padding-left:20px" runat="server" id="divPesquisa" >                        
                               <div class="row">                               <asp:label ID="Label1" style="padding-left:18px" runat="server">Filtro:</asp:label> 
                                   <div>
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <asp:DropDownList ID="ddlConsultas" AutoPostBack="true" runat="server" CssClass="form-control" Font-Size="11px" >
                                             <asp:ListItem Value="0" Text="Selecione"></asp:ListItem>
                                            <asp:ListItem Value="1">Moeda</asp:ListItem>
                                            <asp:ListItem Value="2">Data de Câmbio</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                   <div class="col-sm-4">
                                    <div class="form-group">
                                        <asp:TextBox ID="txtPesquisa" runat="server" CssClass="form-control txs" MaxLength="100"></asp:TextBox>
                                    </div>
                                </div>
                                       <div class="col-sm-4">
                                    <div class="form-group">
                                        <asp:Button ID="btnPesquisa" runat="server" CssClass="btn btn-success" Text="Pesquisar" />
                                    </div>
                                </div>
                                   </div>
                               </div></div>
                <div id="divInfo" runat="server" visible="false" class="alert alert-success">
                              <asp:label ID="lblInfo" Text="" runat="server" /></div> 
        <div class="table-responsive">
            <br />
                                <asp:GridView ID="dgvMoedaFrete" DataKeyNames="Id" DataSourceID="dsMoedaFrete"  CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" AutoGenerateColumns="false"  style="max-height:400px; overflow:auto;" AllowSorting="true" OnSorting="dgvMoedaFrete_Sorting"  EmptyDataText="Nenhum registro encontrado." >
                                    <Columns>
                                        <asp:BoundField DataField="Id" HeaderText="#"  SortExpression="Id" />
                                        <asp:BoundField DataField="NM_MOEDA" HeaderText="Moeda"  SortExpression="NM_MOEDA"/>
                                         <asp:BoundField DataField="DT_CAMBIO" HeaderText="Data Câmbio"  SortExpression="DT_CAMBIO" DataFormatString="{0:dd/MM/yyyy}"/>
                                        <asp:BoundField DataField="VL_TXOFICIAL" HeaderText="Valor TX Oficial"  SortExpression="VL_TXOFICIAL"/>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <a href="MoedaFrete.aspx?id=<%# Eval("Id") %>" class="btn btn-info btn-sm" data-toggle="tooltip" data-placement="top" title="Editar"><span class="glyphicon glyphicon-edit"  style="font-size:medium"></span></a>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>                    
                                   <ItemTemplate>                          
                            <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="False" CommandName="Excluir" CommandArgument='<%# Eval("Id") %>'
                                Text="Excluir" OnClientClick="return confirm('Tem certeza que deseja excluir esse registro?')"  CssClass="btn btn-danger btn-sm" ><span class="glyphicon glyphicon-trash"  style="font-size:medium"></span></asp:LinkButton>
                                   </ItemTemplate>        
                                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />

                    </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle CssClass="headerStyle" />
                                </asp:GridView></div>
         </ContentTemplate>
    <Triggers>
       <asp:AsyncPostBackTrigger EventName="Sorting" ControlID="dgvMoedaFrete" />
               <asp:AsyncPostBackTrigger ControlID="ddlConsultas" />
                       <asp:AsyncPostBackTrigger ControlID="btnPesquisa" />
    </Triggers>
</asp:UpdatePanel>
                            
                        </div>
                    </div>

                </div>
            </div>
        </div>

    </div>

    
    <asp:SqlDataSource ID="dsMoedaFrete" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT TOP 300 A.ID_MOEDA_FRETE as Id, A.ID_MOEDA,B.NM_MOEDA, A.DT_CAMBIO, A.VL_TXOFICIAL 
FROM [dbo].[TB_MOEDA_FRETE] A 
LEFT JOIN TB_MOEDA B ON B.ID_MOEDA = A.ID_MOEDA order by A.DT_CAMBIO DESC" 
        DeleteCommand="DELETE FROM [dbo].[TB_MOEDA_FRETE] WHERE ID_MOEDA_FRETE = @Id">  
        <DeleteParameters>
                    <asp:Parameter Name="ID" Type="Int32" />
                </DeleteParameters>
        </asp:SqlDataSource>

     <asp:SqlDataSource ID="dsMoeda" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_MOEDA, NM_MOEDA FROM [dbo].[TB_MOEDA] union SELECT  0, 'Selecione'  ORDER BY ID_MOEDA" >
        <DeleteParameters>
                    <asp:Parameter Name="ID" Type="Int32" />
                </DeleteParameters>
        </asp:SqlDataSource>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">

     <script type="text/javascript">
         function nomeFuncao(obj, e) {
             var tecla = (window.event) ? e.keyCode : e.which;
             if (tecla == 8 || tecla == 0)
                 return true;
             if (tecla != 44 && tecla < 48 || tecla > 57)
                 return false;
         }
     </script>
</asp:Content>
