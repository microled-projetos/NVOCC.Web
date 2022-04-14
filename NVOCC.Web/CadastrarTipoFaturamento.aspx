<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="CadastrarTipoFaturamento.aspx.vb" Inherits="NVOCC.Web.CadastrarTipoFaturamento" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="col-lg-9 col-md-9 col-sm-9 col-lg-offset-1">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">TIPO DE FATURAMENTO - <asp:label ID="lblRazaoSocial" runat="server"></asp:label> 
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
                            <asp:TextBox ID="txtIDParceiro" runat="server" CssClass="form-control" Style="display: none"></asp:TextBox>
                            <asp:UpdatePanel ID="UpdatePanel15" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
                                        <ContentTemplate>

                                    <div class="alert alert-success" id="divSuccess" runat="server" visible="false">
                                        Ação realizada com sucesso!
                                    </div>
                                    <div class="alert alert-danger" id="divErro" runat="server" visible="false">
                                    <asp:Label runat="server"  ID="msgErro" />
                                    </div>
                            <div class="row">
                                 <div class="col-sm-1">
                                    <div class="form-group">
                                        <label class="control-label">ID:</label>
                                        <asp:TextBox ID="txtID" runat="server" MaxLength="100" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <label class="control-label">Tipo de Faturamento:</label>
                                       <asp:DropDownList ID="ddlTipoFaturamento" runat="server"  CssClass="form-control" Font-Size="11px"  DataTextField="NM_TIPO_FATURAMENTO" DataSourceID="dsTipoFaturamento" DataValueField="ID_TIPO_FATURAMENTO">
                                            </asp:DropDownList>
                                    </div>
                                </div>
                                     <div class="col-sm-3">
                                            <div class="form-group">
                                                <label class="control-label">Serviço:</label></label><label runat="server" style="color: red">*</label>
                                                <asp:DropDownList ID="ddlServico" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_SERVICO" DataSourceID="dsServico" DataValueField="ID_SERVICO" AutoPostBack="true">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                <div class="col-sm-3">
                                            <div class="form-group">
                                                <label class="control-label">Incoterm:</label></label><label runat="server" style="color: red">*</label>
                                                <asp:DropDownList ID="ddlIncoterm" runat="server" CssClass="form-control" Font-Size="11px" DataTextField="NM_INCOTERM" DataSourceID="dsIncoterm" DataValueField="ID_INCOTERM"></asp:DropDownList>
                                            </div>
                                        </div>
                                 <div class="col-sm-2">
                                            <div class="form-group">
                                                <label class="control-label" style="color: white">X</label>
                                                <asp:CheckBox ID="ckbFreeHand" runat="server" CssClass="form-control" Text="&nbsp;&nbsp;FREE HAND"></asp:CheckBox>
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
                                            </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnGravar" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                            </div><br />
                                     <div class="tab-pane fade" id="consulta">                   
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
                                        <ContentTemplate>
                            <div class="row">
                                 <div class="col-sm-12">
                                             <div class="table-responsive tableFixHead">
                                      <asp:TextBox ID="txtArquivoSelecionado" runat="server" style="display:none"></asp:TextBox>
                                <asp:GridView ID="gvTiposFaturamentoParceiro" runat="server" AutoGenerateColumns="false" EmptyDataText="Nenhum registro encontrado!" CssClass="table table-hover table-sm grdViewTable"  GridLines="None" CellSpacing="-1"  DataKeyNames="ID" AllowSorting="true"  DataSourceID="dsTipoFaturamentoParceiro" style="max-height:600px; overflow:auto;" allowpaging="true" PageSize="100">
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="#" Visible="false" SortExpression="ID" />
                                                    <asp:BoundField DataField="NM_TIPO_FATURAMENTO" HeaderText="TIPO DE FATURAMENTO" SortExpression="NM_TIPO_FATURAMENTO" />
                                                    <asp:BoundField DataField="NM_SERVICO" HeaderText="SERVICO" SortExpression="NM_SERVICO" />
                                                    <asp:BoundField DataField="NM_INCOTERM" HeaderText="INCOTERM" SortExpression="NM_INCOTERM" />
                                                     <asp:TemplateField HeaderText="FL_FREE_HAND" SortExpression="FREE HAND">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="ckbFL_FREE_HAND" Checked='<%# Eval("FL_FREE_HAND") %>' Enabled="false" runat="server"/>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnVisualizar" runat="server" CausesValidation="False" CommandName="visualizar" CommandArgument='<%# Eval("ID") %>'
                                                                Text="Visualizar" title="Editar" CssClass="btn btn-info btn-sm"><span class="glyphicon glyphicon-edit"  style="font-size:medium"></span></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
            </Columns>
                                     <HeaderStyle CssClass="headerStyle" />
        </asp:GridView></div>
                            </div>  </div>
  </ContentTemplate>  
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger EventName="RowCommand" ControlID="gvTiposFaturamentoParceiro" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                            </div></div>
                        
                        </div>

            </div>
       </div>
     <asp:SqlDataSource ID="dsIncoterm" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_INCOTERM, cast((CD_INCOTERM)as varchar)+ ' - '+ NM_INCOTERM as NM_INCOTERM FROM TB_INCOTERM 
union SELECT  0, 'Selecione' ORDER BY ID_INCOTERM"></asp:SqlDataSource>

        <asp:SqlDataSource ID="dsServico" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT ID_SERVICO, NM_SERVICO FROM TB_SERVICO
union SELECT  0, 'Selecione' ORDER BY ID_SERVICO"></asp:SqlDataSource>

   <asp:SqlDataSource ID="dsTipoFaturamento" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="select ID_TIPO_FATURAMENTO,NM_TIPO_FATURAMENTO FROM [dbo].[TB_TIPO_FATURAMENTO] union SELECT  0, 'Selecione' ORDER BY ID_TIPO_FATURAMENTO">
</asp:SqlDataSource>

    <asp:SqlDataSource ID="dsTipoFaturamentoParceiro" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="select A.ID,A.ID_TIPO_FATURAMENTO,B.NM_TIPO_FATURAMENTO, C.NM_SERVICO,D.NM_INCOTERM,A.FL_FREE_HAND FROM TB_TIPO_FATURAMENTO_PARCEIRO A
INNER JOIN TB_TIPO_FATURAMENTO B ON  A.ID_TIPO_FATURAMENTO =B.ID_TIPO_FATURAMENTO
INNER JOIN TB_SERVICO C ON  A.ID_SERVICO =C.ID_SERVICO
LEFT JOIN TB_INCOTERM D ON  A.ID_INCOTERM =D.ID_INCOTERM
WHERE ID_PARCEIRO =  @ID_PARCEIRO

">
        <SelectParameters>
            <asp:ControlParameter Name="ID_PARCEIRO" Type="Int32" ControlID="txtIDParceiro" />
        </SelectParameters>
    </asp:SqlDataSource>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
