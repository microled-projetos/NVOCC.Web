<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ProcessosPeriodo.aspx.vb" Inherits="NVOCC.Web.ProcessosPeriodo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        #imgFundo {
            display: none;
        }

        td {
            font-size: 10px !important;
            padding-left: 5px !important;
            padding-right: 5px !important;
        }
        
        .divTitulo{
            border:solid;
            border-width:thin;
            border-color:black !important;
            margin-left:15%;
            margin-right:15%;
            margin-top: 0px !important;
            margin-bottom: 0px !important;
        }

         .tabelaDinamica{
            border:solid;
            border-width:thin;
            border-color:black !important;        
            font-size:12px;
        }

        @media print {

            @page {
            }

            #imgFundo {
                display: none;
            }

            #DivImpressao {
                display: block;
                font-size: 11px !important
            }

            td {
                font-size: 12px !important;
                padding-left: 5px !important;
                padding-right: 5px !important;
            }

            input[type=submit] {
                display: none;
            }

        }
    </style>
    <div style="margin-bottom: 40px;">

        <div style="display: none">
        </div>
        <table>
            <tr>
                <td>
                    <center><div style="float:left;font-size:13px"><img src="Content/imagens/Logo_FCA_Deitado2.png" /><br />
    <strong>FCA COMÉRCIO EXTERIOR E LOGISTICA LTDA.</strong>&nbsp;<br /></div></center>
                </td>
                <td></td>
                <td></td>
            </tr>
        </table>
        <div id="DivImpressao" class="DivImpressao table-content" style="font-size: 10px; margin-bottom: 10px;">
            <br /><br /><br />
            <div class="divTitulo">
               
                        <center><h5>PROCESSOS POR PERIODO <asp:Label ID="lblAgente"  runat="server"/></h5></center>
   
            </div> <br /><br /><br />
            <div id="divConteudoDinamico" runat="server">
            </div>

            <br />
            <div style="float: right; font-size: 12px">Financeiro/Account/Processos Por Periodo <%= DateTime.Now %> </div>
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
