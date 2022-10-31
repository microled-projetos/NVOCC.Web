<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ProcessosPeriodo.aspx.vb" Inherits="NVOCC.Web.ProcessosPeriodo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        #imgFundo {
            display: none;
        }
       
        .logo{
            width:6cm;
            height:2cm
        }

        td {
            padding-left: 4px !important;
            padding-right: 4px !important;
        }
        
        .divTitulo{
            font-size: 20pt;          
        }

         .tabelaDinamica{
            border:solid;
            border-width:thin;
            border-color:black !important;        
            font-size:8pt;
            text-align:center;
        }      
      
    </style>
    <div style="margin-bottom: 40px;">
        <table>
            <tr>
                <td>
                    <center><div style="float:left;font-size:11px"><img id="logo" class="logo" src="Content/imagens/Logo_FCA_Deitado2.png" /><br />
    <strong>FCA COMÉRCIO EXTERIOR E LOGISTICA LTDA.</strong>&nbsp;<br /></div></center>
                </td>
                <td></td>
                <td></td>
            </tr>
        </table>
           <br/><br/> <div class="divTitulo"><center>Processos Por Periodo <asp:Label ID="lblAgente"  runat="server"/></center></div><br/>
            <div id="divConteudoDinamico" runat="server"></div><br/>
            <div style="float: right; font-size: 12px">Financeiro/Account/Processos Por Periodo <%= DateTime.Now %> </div>
        </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">
    <script>
        $(window).load(function () {
            window.print();
        });
    </script>
</asp:Content>
