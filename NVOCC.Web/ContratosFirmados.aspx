<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ContratosFirmados.aspx.vb" Inherits="NVOCC.Web.ContratosFirmados" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
            #imgFundo {
                display: none;
            }
             td{
                padding-left:10px;
                padding-right:10px;

            }
        @media print {

            @page {
            }

             #imgFundo {
                display: none;
            }
            #DivImpressao{
                display: block;
                font-size:8px !important;
            }
             td{
                padding-left:10px;
                padding-right:10px;

            }
        }
    </style> 

    <div id="divConteudoDinamico" runat="server" style="border-style:solid;border-width: thin;" >
        </div>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>