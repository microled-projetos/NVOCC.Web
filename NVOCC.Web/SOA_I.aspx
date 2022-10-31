<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="SOA_I.aspx.vb" Inherits="NVOCC.Web.SOA_I" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        #imgFundo {
            display: none;
        }
        
        .logo{
            width:6cm;
            height:2cm
        }
       
        td{
            font-size:8pt !important;
            padding-left:5px !important;
            padding-right:5px !important;
        }
         
        .tabelaDinamica{
            border:solid;
            border-width:thin;
            border-color:black !important;        
            font-size:8pt;
            text-align:center;

        }

        .divTitulo{
            font-size: 20pt;          
        }

        .divDadosBancarios{
            font-size: 8pt;          
        }

        .divConteudoDinamico{
            font-size: 8pt;          
        }



        @media print {
           
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
    <div style="float:left;font-size:11px"><img id="logo" class="logo" src="Content/imagens/Logo_FCA_Deitado2.png" /><br />
    <strong>FCA COMERCIO EXTERIOR E LOGISTICA LTDA.</strong>&nbsp;<br /></div></center>

                </td>
                <td >   
             
                </td>
                <td>               
                     <br />
    <div id="divDadosBancarios" class="divDadosBancarios" runat="server"  style="float:right"></div>
                   
                </td>
               
            </tr>
        </table>
          <div id="divTitulo" class="divTitulo" ><center>Statement of Account</center></div>                                   
    <div id="divConteudoDinamico" runat="server"></div><br />             
    <div style="float:right;font-size:13px"> Financeiro/Account/Invoice/Rel. Statement of Account <%= DateTime.Now %> </div><br />

    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">
     <script>
        $(window).load(function () {
            window.print();
        });
     </script>
</asp:Content>
