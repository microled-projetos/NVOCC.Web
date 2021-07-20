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
           td{
               font-size:8px !important;
               padding-left:5px;
                padding-right:5px
           }
        }
    </style>
        <div style="display:none">
    <asp:Label ID="lblIDINVOICE"  runat="server"/>
        <asp:Label ID="lblID_BL"  runat="server"/>
        <asp:Label ID="lblGrau"  runat="server"/>
        </div>
          <div id="DivImpressao" class="DivImpressao table-content" style="font-size: 10px; margin-bottom: 10px;">   
              
                  <asp:Label ID="lblDatas" runat="server" />
                    <br /><asp:Label ID="lblUsuario" runat="server" />
                    <br />
                                    
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
