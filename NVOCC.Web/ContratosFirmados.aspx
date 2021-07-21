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
                 font-size:9px !important;
            }
        }
    </style> 
     <div id="DivImpressao" runat="server" >
        
    <asp:Label ID="lblDatas" runat="server" />
    <center><h5>RELAÇÃO DOS FECHAMENTOS DE CÂMBIO</h5> </center>
   
    <div id="divConteudoDinamico" runat="server" >
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