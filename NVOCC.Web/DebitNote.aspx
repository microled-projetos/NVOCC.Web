<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="DebitNote.aspx.vb" Inherits="NVOCC.Web.DebitNote" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        #DivImpressao, #imgFundo {
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
                padding:5px;
                margin:0;
            }
        }
    </style>
     <div style="display:none">
    <asp:Label ID="lblIDINVOICE"  runat="server"/>
        <asp:Label ID="lblID_BL"  runat="server"/>
        <asp:Label ID="lblID_BL_MASTER"  runat="server"/>
        <asp:Label ID="lblGrau"  runat="server"/>
        </div>
    <div id="DivImpressao" class="DivImpressao table-content" style="font-size: 10px; margin-bottom: 10px;">
                <table border="1">
                    <tr>
                        <td>
        <table border="1">
            <tr>
                <td>
                    <center>
    <strong>FCA COMERCIO EXTERIOR E LOGISTICA LTDA.</strong><br /></center>
                    <div style="text-align: center;">
                        R QUINZE DE NOVEMBRO, 46/48 - CENTRO<br />
                        SANTOS - SÃO PAULO - BRASIL - CEP:11010150<br />
                        FONE:+55 13 3797-7850 - FAX:<br />
                        CNPJ:00.693.367/0003-11  &nbsp;&nbsp;&nbsp;I.E: 633.672.235.110
                    </div>
                </td>
                <td>
                    <center>
    <img src="Content/imagens/FCA-Log - Copia.png" /><br />
    <strong>FCA COMERCIO EXTERIOR E LOGISTICA LTDA.</strong>&nbsp;<br /></center>

                </td>
            </tr>
        </table>
        <table border="1">

            <tr>
                <td>
                    <div style="text-align: center;">DÉBIT NOTE</div>
                </td>
            </tr>
        </table>
        <table  style="border-style:solid;border-width: thin;">
            <tr>
                <td>
                    <strong>For Account of</strong>&nbsp;<asp:Label ID="lblEmpresa" runat="server" />
                    <br />
                    <br /><strong>PHONE:</strong>&nbsp;<asp:Label ID="lblTelefone" runat="server" />
                    <br />
                </td>
                <td>
                    <strong>INVOICE No:</strong>&nbsp;<asp:Label ID="lblNumeroInvoice" runat="server" />
                    <br /><strong>INVOICE DATE:</strong>&nbsp;<asp:Label ID="lblDataInvoice" runat="server" />
                    <br /><strong>FILE No:</strong>&nbsp;<asp:Label ID="lblProcesso" runat="server" />
                    <br />
                </td>
            </tr>
            
        </table>
        <table style="border-style:solid;border-width: thin;">
            <tr>
                <td>
                    <strong>SHIPPER</strong>&nbsp;<asp:Label ID="lblCliente" runat="server" />
                    <br /><strong>MBL:</strong>&nbsp;<asp:Label ID="lblMBL" runat="server" /><asp:Label ID="Label7" runat="server" />
                    <br /><strong>CONSIGNEE:</strong>&nbsp;<asp:Label ID="lblImportador" runat="server" />
                    <br /><strong>BY:</strong>&nbsp;<asp:Label ID="lblOrigem" runat="server" />
                    <br /><strong>QUANTITY:</strong>&nbsp;<asp:Label ID="lblQtdVolumes" runat="server" />
                    
                </td>
                <td>
                    <strong>REF No:</strong>&nbsp;<asp:Label ID="lblReferencias" runat="server" />
                    <br /><strong>HBL:</strong>&nbsp;<asp:Label ID="lblHBL" runat="server" />
                    <br /><strong>VOYAGE:</strong>&nbsp;<asp:Label ID="lblViagem" runat="server" />
                    <br /><strong>TO:</strong>&nbsp;<asp:Label ID="lblDestino" runat="server" />
                    <br /><strong>GROSS WEIGHT:</strong>&nbsp;<asp:Label ID="lblPesoBruto" runat="server" />
                    <br />
                </td>
            </tr>
                     
        </table>
           <br />
        <div id="divConteudoDinamico" runat="server" style="border-style:solid;border-width: thin;" >
        </div>
                                    <table >
            <tr>
                <td>
                                        <strong>CORRESPONDENT BANK:</strong>&nbsp;<asp:Label ID="lblCorrespondenteBank" runat="server" /><br/>
                                        <strong>STANDART:</strong>&nbsp;<asp:Label ID="Label2" runat="server" /><br/>
                                        <strong>SWIFT:</strong>&nbsp;<asp:Label ID="Label4" runat="server" /><br/>
                                        <strong>ACCOUNT:</strong>&nbsp;<asp:Label ID="Label3" runat="server" /><br/>

                </td>
                <td>
                                        <strong>BENEFICIARY BANK:</strong>&nbsp;<asp:Label ID="Label1" runat="server" /><br/>
                                        <strong>CORRESPONDENT BANK:</strong>&nbsp;<asp:Label ID="Label5" runat="server" /><br/>
                                        <strong>AGENCY:</strong>&nbsp;<asp:Label ID="Label6" runat="server" /><br/>
                                        <strong>SWIFT:</strong>&nbsp;<asp:Label ID="Label8" runat="server" /><br/>
                                        <strong>ACCOUNT:</strong>&nbsp;<asp:Label ID="Label9" runat="server" /><br/>
                                        <strong>NAME:</strong>&nbsp;<asp:Label ID="Label10" runat="server" /><br/>
                                        <strong>IBAN-BR:</strong>&nbsp;<asp:Label ID="Label11" runat="server" /><br/>
                </td>
            </tr>
        </table>

           <br />
            <br />
</td>
                    </tr>
</table>

    </div>


    <asp:SqlDataSource ID="dsNotas" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="SELECT * FROM [dbo].[View_Contas_Receber] WHERE (CD_PR = 'R')"></asp:SqlDataSource>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">
    <script>
        $(window).load(function () {
            window.print();
        });
    </script>
</asp:Content>