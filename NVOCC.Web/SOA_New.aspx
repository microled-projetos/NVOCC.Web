<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="SOA_New.aspx.vb" Inherits="NVOCC.Web.SOA_New" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        #imgFundo {
            display: none;
        }
        td{
               font-size:10px !important;
               padding-left:5px !important;
                padding-right:5px !important;
           }
        @media print {

            @page {
            }

             #imgFundo {
                display: none;
            }
            #DivImpressao{
                display: block;
                font-size:11px !important
            }
           td{
               font-size:8px !important;
               padding-left:5px !important;
                padding-right:5px !important;
           }
           
           input[type=submit] {
                display: none;
           }
           
        }
    </style>
    <asp:Button runat="server" Text="Gerar CSV" id="btnCSV" CssClass="btnCSV btn btn-success"  Style="float:right" />
        <div style="display:none">
    <asp:Label ID="lblIDINVOICE"  runat="server"/>
        <asp:Label ID="lblID_BL"  runat="server"/>
        <asp:Label ID="lblGrau"  runat="server"/>
        </div>
            <table >
            <tr>
                <td >
                           <strong>Agente Acount:</strong>&nbsp;<asp:Label ID="lblAgente" runat="server" />
                    <br /> <strong>Date Range:</strong>&nbsp;<asp:Label ID="lblDatas" runat="server" />
                    <br /> <strong>Type of Service</strong>&nbsp;<asp:Label ID="lblTipoServico" runat="server" />
                    <br />
                </td>
                <td>
                           <strong>Origin:</strong>&nbsp;<asp:Label ID="lblOrigem" runat="server" />
                    <br /> <strong>Destination:</strong>&nbsp;<asp:Label ID="lblDestino" runat="server" />
                    <br /> <strong>Costumer:</strong>&nbsp;<asp:Label ID="lblCliente" runat="server" />
                    <br /> <strong>Currency:</strong>&nbsp;<asp:Label ID="lblMoeda" runat="server" />

                </td>
                <td>
                    <center>
    <img src="Content/imagens/FCA-Log - Copia.png" /><br />
    <strong>FCA COMERCIO EXTERIOR E LOGISTICA LTDA.</strong>&nbsp;<br /></center>

                </td>
            </tr>
        </table>
          <div id="DivImpressao" class="DivImpressao table-content" style="font-size: 10px; margin-bottom: 10px;">   <center><h3>Statement of Account</h3></center>                                   
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
