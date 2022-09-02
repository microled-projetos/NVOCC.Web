<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ProcessosPeriodo.aspx.vb" Inherits="NVOCC.Web.ProcessosPeriodo" %>
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
    </style>    <div Style="margin-bottom: 40px;">

    <asp:Button runat="server" Text="Gerar CSV" id="btnCSV" CssClass="btnCSV btn btn-success"  Style="float:right" />
        <div style="display:none">
        </div>
            <table >
            <tr>
                <td >
                         <center><div style="float:left;font-size:13px"><img src="Content/imagens/Logo_FCA_Deitado2.png" /><br />
    <strong>FCA COMÉRCIO EXTERIOR E LOGISTICA LTDA.</strong>&nbsp;<br /></div></center>
                </td>
                <td>
                           

                </td>
                <td>
                    
  
                </td>
            </tr>
        </table>
          <div id="DivImpressao" class="DivImpressao table-content" style="font-size: 10px; margin-bottom: 10px;">   
              <center><h3>Processos Por Periodo <asp:Label ID="lblAgente"  runat="server"/></h3></center>                                   
    <div id="divConteudoDinamico" runat="server"  >
        </div>
          
          <br />  <div style="float:right; font-size:12px">Financeiro/Account/Processos Por Periodo <%= DateTime.Now %> </div>
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
