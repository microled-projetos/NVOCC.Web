<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CadastroMetas.aspx.vb" Inherits="NVOCC.Web.CadastroMetas" MasterPageFile="~/Site.Master" %>

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
            font-size: 13px
        }

        .selected1 {
            color: black;
            font-family: verdana;
            font-size: 8pt;
            background-color: #e6c3a5;
        }
        .modal-xxl {
            width: 100%;            
            max-width: 2000px;
        }
    </style>
    <div class="row">
        <div class="col-lg-12 col-md-12 col-sm-12">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">TABELA CADASTRO DE META
                    </h3>
                </div>
                <div class="panel-body">
                    <asp:UpdatePanel ID="pnlCadastroMeta" runat="server" UpdateMode="always" ChildrenAsTriggers="True">
                            <ContentTemplate>
                                    <div class="row linhabotao text-center" style="margin-left: 0px; border: ridge 1px; padding-top: 20px; padding-bottom: 20px; margin-right: 5px;">
                                        <div class="col-sm-1">
                                            <asp:TextBox ID="txtValidade" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:DropDownList ID="cbTipoProduto" runat="server" CssClass="form-control font-size-11">
                                                <asp:ListItem Value="0">Selecione</asp:ListItem>
                                            </asp:DropDownList>                                        
                                       </div>
                                    </div>
                            </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
           </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server"></asp:Content>