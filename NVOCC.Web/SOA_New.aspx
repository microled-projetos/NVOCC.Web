﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="SOA_New.aspx.vb" Inherits="NVOCC.Web.SOA_New" %>
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
    <div Style="margin-bottom: 40px;">
    <asp:Button runat="server" Text="Gerar CSV" id="btnCSV" CssClass="btnCSV btn btn-success"  Style="float:right" />
        <div style="display:none">
    <asp:Label ID="lblIDINVOICE"  runat="server"/>
        <asp:Label ID="lblID_BL"  runat="server"/>
        <asp:Label ID="lblGrau"  runat="server"/>
        </div>
            <table >
            <tr> <td>
                    <center>
    <div style="float:left;font-size:13px"><img src="Content/imagens/Logo_FCA_Deitado2.png" /><br />
    <strong>FCA COMERCIO EXTERIOR E LOGISTICA LTDA.</strong>&nbsp;<br /></div></center>

                </td>
                <td >   
                 <%--<strong>Date Range:</strong>&nbsp;<asp:Label ID="lblDatas" runat="server" />
                     <br/><strong>Agente Name:</strong>&nbsp;<asp:Label ID="lblAgente" runat="server" />
                    <br /> <strong>Currency:</strong>&nbsp;<asp:Label ID="lblMoeda" runat="server" />--%>

                </td>
                <td>
                

                     <br />
    <div id="divDadosBancarios" runat="server"  style="float:right"></div>
                   
                </td>
               
            </tr>
        </table>
          <div id="DivImpressao" class="DivImpressao table-content" style="font-size: 10px; margin-bottom: 10px;">   <center><h1>Statement of Account</h1></center>                                   
    <div id="divConteudoDinamico" runat="server"  >
        </div><br />
              
    <div style="float:right"> SOA <%= DateTime.Now %> </div><br />
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
