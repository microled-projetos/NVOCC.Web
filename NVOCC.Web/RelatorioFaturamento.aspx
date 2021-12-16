<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="RelatorioFaturamento.aspx.vb" Inherits="NVOCC.Web.RelatorioFaturamento" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Relatorio Faturamento</title> 
        <link href="Content/css/bootstrap.min.css" rel="stylesheet" />

     <script src="Content/js/jquery.min.js"></script>
    <script>             
    $(window).load(function () {
        window.print();
    });
    </script>
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
                    font-family:Arial;
            }
           td{
               font-size:8px !important;
               padding-left:5px !important;
                padding-right:5px !important;
           }
           
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">

        <asp:Label ID="lblIDs"  runat="server" style="display:none"/>
        <div id="DivImpressao" class="DivImpressao" style="font-size: 12px;font-family:Arial;">
            <table >
            <tr>               
                
                <td>
          <br /><strong>FCA COMERCIO EXTERIOR E LOGISTICA LTDA.</strong>&nbsp;<br />         
    <asp:Label ID="lblPesquisa"  runat="server"/>

                </td>
            </tr>
        </table>

 <center><h3>Relatório Faturamento</h3></center>        
    <div id="divConteudoDinamico" class="table-content" runat="server" style="margin-left:10px;margin-right:10px" >
        </div><br /><br /> <br />
           <div style="float:right;font-size:10px"><strong>Usuário:</strong>&nbsp;<asp:Label ID="lblUsuario" runat="server" />
                    <br /><strong>Data Impressão:</strong>&nbsp;<asp:Label ID="lblDataImpressao" runat="server" /> <br /> </div>

        </div>
    </form>
</body>
</html>
