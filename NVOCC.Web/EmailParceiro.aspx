<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="EmailParceiro.aspx.vb" Inherits="NVOCC.Web.EmailParceiro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
      <br />
<div class="row principal">
    <style>
        .larguraMinima{
     min-width: 140px;
} 
    </style>
       <div class="col-lg-6 col-lg-offset-3 col-md-12 col-sm-12">

            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">EMAIL PARCEIRO
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
                    <asp:UpdatePanel ID="updPainel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True">
    <ContentTemplate>
                  <div class="alert alert-success" id="divmsg" runat="server" visible="false">                                       
                      <asp:label ID="lblmsg" runat="server"  /> 
                  </div>
                    <div class="alert alert-danger" id="divmsg2" runat="server" visible="false">                                       
                      <asp:label ID="lblerro" runat="server"  /> 
                  </div>
                <asp:ValidationSummary ID="Validacoes" runat="server" ShowModelStateErrors="true" CssClass="alert alert-danger" />
                    <div class="row"> 
                        <div class="col-sm-3">
                                    <div class="form-group">
                                        <label class="control-label">Código:</label>
                                        <asp:TextBox ID="txtID" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                              </div>  
                    </div>

                <div class="row"> 

                  <div class="col-sm-9">
                    <div class="form-group">
                         <label class="control-label">Empresa:</label>
                                <asp:DropDownList ID="ddlEmpresa" runat="server" CssClass="form-control" Font-Size="11px" AutoPostBack="True" DataTextField="NM_RAZAO" DataSourceID="dsEmpresas" DataValueField="ID_PARCEIRO">
                                </asp:DropDownList>
                        </div>
                    </div></div>
                <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <asp:Checkbox ID="ckbClientes" runat="server" autopostback="True" CssClass="form-control" text="&nbsp;&nbsp;Clientes" Checked="true" ></asp:Checkbox>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <asp:Checkbox ID="ckbTerminais" runat="server" autopostback="True" CssClass="form-control" text="&nbsp;&nbsp;Terminais" Checked="true" ></asp:Checkbox>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <asp:Checkbox ID="ckbParceiros" runat="server" autopostback="True" CssClass="form-control" text="&nbsp;&nbsp;Parceiros" Checked="true" ></asp:Checkbox>
                                    </div>
                                </div>
           </div>    
                <br />
                <br />           
                <div class="row">
                        <div class="col-sm-4">
                             <div class="form-group">
                                <asp:label ID="lblRazaoSocial" runat="server" />
                             </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:label ID="lblNomeFantasia" runat="server" />
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:label ID="lblCNPJ" runat="server" />
                            </div>
                        </div>
                </div>                                      
                <br />
                <br />
                <div class="row">
                   <div class="col-sm-6">
                    <div class="form-group">
                         <label class="control-label">Evento:</label>
                                <asp:DropDownList ID="ddlEvento" runat="server" CssClass="form-control" Font-Size="11px" AutoPostBack="True" DataTextField="NMTIPOAVISO" DataSourceID="dsEventos" DataValueField="IDTIPOAVISO">
                                </asp:DropDownList>
                   </div>
                 </div>
                    <div class="col-sm-6">
                    <div class="form-group">
                         <label class="control-label">Porto:</label>
                                <asp:DropDownList ID="ddlPorto" runat="server" CssClass="form-control" autopostback="True" Font-Size="11px" DataTextField="NM_PORTO" DataSourceID="dsPorto" DataValueField="ID_PORTO">
                                </asp:DropDownList>
                   </div>
                 </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div class="form-group">
                            <label class="control-label">Endereços de Email:</label><label runat="server" style="color:red" >*</label> 
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" Style="max-width: 800px;" TextMode="MultiLine" Rows="2" onkeyUp="return CheckMaxCount(this,event,500);"></asp:TextBox><small style="color:gray">(Informe 1 ou mais endereços de eMail's separados por ponto e vírgula)</small>                       
                        </div>
                    </div>
                </div>
         <div class="row">
            <div class="col-sm-12">
                <div class="form-group">
                    <asp:Checkbox ID="ckbReplica" runat="server" CssClass="form-control" text="&nbsp;&nbsp;Desejo replicar estes emails para todos os eventos" ></asp:Checkbox>
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
                                        <asp:Button ID="btnGravar" runat="server" CssClass="btn btn-primary btn-block" Text="Gravar"  />
                                    </div>
                                </div>
                            </div>   
                      </ContentTemplate>
    <Triggers>
       <asp:AsyncPostBackTrigger ControlID="ddlEvento" />
               <asp:AsyncPostBackTrigger ControlID="btnGravar" />
                       <asp:AsyncPostBackTrigger ControlID="txtEmail" />

    </Triggers>
</asp:UpdatePanel>
                            </div>
                                   <div class="tab-pane fade" id="consulta">
                            <br />
                            
                                 <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True">
    <ContentTemplate>
        <div class="alert alert-success" id="divMsgExcluir" runat="server" visible="false">                                       
                      <asp:label ID="lblMsgExcluir" runat="server"  /> 
                  </div>
         <div class="alert alert-danger" id="divMsgErro" runat="server" visible="false">                                       
                      <asp:label ID="lblMsgErro" runat="server"  /> 
                  </div>
        <div class="table-responsive tableFixHead">

                                <asp:GridView ID="dgvEmail" DataKeyNames="Id" DataSourceID="dsListaEmail" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" style="max-height:400px; overflow:auto;" AllowSorting="true" OnSorting="dsListaEmail_Sorting" 
 AutoGenerateColumns="false" EmptyDataText="Nenhum registro encontrado.">
                                    <Columns>
                                        <asp:BoundField DataField="Id" HeaderText="#" SortExpression="Id" />
                                        <asp:BoundField DataField="NMTIPOAVISO" HeaderText="Tipo de Evento" SortExpression="NMTIPOAVISO" />
                                        <asp:BoundField DataField="NM_RAZAO" HeaderText="Parceiro" SortExpression="NM_RAZAO" />
                                        <asp:BoundField DataField="NM_PORTO" HeaderText="Porto" SortExpression="NM_PORTO" />
                                        <asp:BoundField DataField="TIPO" HeaderText="Tipo" SortExpression="TIPO" />                                                       
                                        <asp:BoundField DataField="TIPO_PESSOA" HeaderText="Tipo Pessoa" SortExpression="TIPO_PESSOA" />
                                        <asp:BoundField DataField="ENDERECOS" Visible="false" HeaderText="ENDERECOS" SortExpression="ENDERECOS" />                                       
                                        <asp:TemplateField HeaderText="" >
                                            <ItemTemplate>
                                                <a href="EmailParceiro.aspx?id=<%# Eval("Id") %>" class="btn btn-info btn-sm" data-toggle="tooltip" data-placement="top" title="Editar"><span class="glyphicon glyphicon-edit"  style="font-size:medium"></span></a>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="False" CommandName="Excluir" CommandArgument='<%# Eval("Id") %>' 
                                Text="Excluir" OnClientClick="return confirm('Tem certeza que deseja excluir esse registro?')"  CssClass="btn btn-danger btn-sm" ><span class="glyphicon glyphicon-trash"  style="font-size:medium"></span></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="campo-acao" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle CssClass="headerStyle" />
                                </asp:GridView>
                                     </ContentTemplate>
    <Triggers>
       <asp:AsyncPostBackTrigger EventName="Sorting" ControlID="dgvEmail" />
               <asp:AsyncPostBackTrigger ControlID="ddlempresa" />
                       <asp:AsyncPostBackTrigger ControlID="btnGravar" />


    </Triggers>
</asp:UpdatePanel>
                            </div>
                        
                        </div>

            </div>
       </div>
    </div>
</div>
     <asp:SqlDataSource ID="dsEmpresas" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT ID_PARCEIRO, NM_RAZAO FROM [dbo].[TB_PARCEIRO] WHERE FL_ATIVO = 1 /*FILTRO*/  union SELECT 0 , '   Selecione'  FROM [dbo].[TB_PARCEIRO]
Order by NM_RAZAO">
</asp:SqlDataSource>
    <asp:SqlDataSource ID="dsEventos" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="SELECT [IDTIPOAVISO],[NMTIPOAVISO] FROM [dbo].[TB_TIPOAVISO] union SELECT 0 , 'Selecione'  FROM [dbo].[TB_TIPOAVISO]
Order by [IDTIPOAVISO]">
</asp:SqlDataSource>
    <asp:SqlDataSource ID="dsPorto" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="select ID_PORTO, NM_PORTO FROM [dbo].[TB_PORTO] union SELECT 0 , 'Selecione'  FROM [dbo].[TB_PORTO] ORDER BY NM_PORTO ">
</asp:SqlDataSource>
        <asp:SqlDataSource ID="dsListaEmail" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        selectcommand="select 
ID as Id, ID_EVENTO,A.ID_PESSOA,D.NM_RAZAO,B.NMTIPOAVISO,ID_TERMINAL,C.NM_PORTO,TIPO,TIPO_PESSOA,ENDERECOS
from [dbo].[TB_AMR_PESSOA_EVENTO] A
LEFT JOIN TB_TIPOAVISO B ON A.ID_EVENTO = B.IDTIPOAVISO
LEFT JOIN TB_PORTO C ON C.ID_PORTO = A.ID_TERMINAL
LEFT JOIN TB_PARCEIRO D ON D.ID_PARCEIRO = A.ID_PESSOA
WHERE ID_PESSOA = @ID_PARCEIRO">
<SelectParameters>
 <asp:ControlParameter ControlID="ddlEmpresa" Name="ID_PARCEIRO" PropertyName="SelectedValue" DefaultValue="0" />
   <%-- <asp:Parameter Name="ID_PARCEIRO" Type="Int32" />--%>
</SelectParameters>
</asp:SqlDataSource>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
