<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="SOA_II.aspx.vb" Inherits="NVOCC.Web.SOA_II" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        #imgFundo {
            display: none;
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
        <div style="display:none">
    <asp:Label ID="lblIDINVOICE"  runat="server"/>
        <asp:Label ID="lblID_BL"  runat="server"/>
        <asp:Label ID="lblGrau"  runat="server"/>
        </div>
          <div id="DivImpressao" class="DivImpressao table-content" style="font-size: 10px; margin-bottom: 10px;">   <center><h3>DEBIT/CREDIT NOTE</h3></center>                                   
    <div id="divConteudoDinamico" runat="server"  >
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
