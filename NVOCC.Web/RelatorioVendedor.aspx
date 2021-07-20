<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="RelatorioVendedor.aspx.vb" Inherits="NVOCC.Web.RelatorioVendedor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <style>
        #DivImpressao {
            display: BLOCK;
        }
        @media print {

            @page {
            }

            #imgFundo {
                display: none;
            }
            #DivImpressao{
                display: block;
            }
        }
    </style> 
    <div id="DivImpressao" class="DivImpressao table-content">

          <div id="divConteudoDinamico" runat="server">
        </div>
                                             

        </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">
     <script>             
        $(window).load(function () {
            window.print();
        });
     </script>
</asp:Content>
