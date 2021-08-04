<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="RelatorioVendedor.aspx.vb" Inherits="NVOCC.Web.RelatorioVendedor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <style>
        #DivImpressao {
            display: block;
        }
         #imgFundo {
                display: none;
            }
        @media print {

            @page {
            }

            #imgFundo,#DivMsg {
                display: none;
            }
            #DivImpressao{
                display: block;
            }
            td,th{
               font-size:8px !important;
               padding-left:5px !important;
                padding-right:5px !important;
           }
        }
    </style>      
    <div id="DivMsg">
    <div class="alert alert-success" id="divSuccess" runat="server" visible="false">
                                    <asp:Label ID="lblmsgSuccess" Text="Email's enviados!" runat="server"></asp:Label>
                                </div>
        <div class="alert alert-danger" id="divErro" runat="server" visible="false">
                                    <asp:Label ID="lblmsgErro" Text="Erro ao enviar email's" runat="server"></asp:Label>
                                </div></div>
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
