<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="DebitNote.aspx.vb" Inherits="NVOCC.Web.DebitNote" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>DebitNote</title>
    <script src="Content/js/jquery.min.js"></script>
    <script>             
    $(window).load(function () {
        window.print();
    });
    </script>
    <style>
        table{
            width:770px;
        }
            td{
                padding-left:10px;
                padding-right:10px;
            }
            th{
                font-weight:bold;
                text-align:left;
                padding-left:10px;
            }
            #DivImpressao{
                display: none;
            }
            
            @media print {

            @page {
            }

           
            #DivImpressao{
                display: block;
                 font-family:Arial;
            }
           
        }
    </style> 
</head>
<body>
    <form id="form1" runat="server">
        <div>
                 <div style="display:none">
    <asp:Label ID="lblIDINVOICE"  runat="server"/>
        <asp:Label ID="lblID_BL"  runat="server"/>
        <asp:Label ID="lblID_BL_MASTER"  runat="server"/>
        <asp:Label ID="lblGrau"  runat="server"/>
        </div>
    <div id="DivImpressao" class="DivImpressao table-content" style="font-size: 10px; margin-bottom: 10px;margin-top: 30px;max-width:770px">
                
        <table border="1">
            <tr>
                <td>
                    <center>
    <strong>FCA COMERCIO EXTERIOR E LOGISTICA LTDA.</strong><br /></center>
                    <div style="text-align: center;">
                        <%--R QUINZE DE NOVEMBRO, 46/48 - CENTRO<br />
                        SANTOS - SÃO PAULO - BRASIL - CEP:11010150<br />
                        FONE:+55 13 3797-7850 - FAX:<br />
                        CNPJ:00.639.367/0003-11  &nbsp;&nbsp;&nbsp;I.E: 633.672.235.110--%>
                        <asp:Label ID="lblEnderecoFCA" runat="server" /><br />
                        <asp:Label ID="lblEnderecoFCA2" runat="server" /><br />
                        <asp:Label ID="lblContatoFCA" runat="server" /><br />
                        <asp:Label ID="lblDocFCA" runat="server" /><br />
                    </div>
                </td>
                <td>
                    <center>
    <img src="Content/imagens/Logo_FCA_Deitado1.png" /><br />
    <strong>FCA COMERCIO EXTERIOR E LOGISTICA LTDA.</strong>&nbsp;<br /></center>

                </td>
            </tr>
        </table>
        <table border="1">

            <tr>
                <td>
                    <div style="text-align: center;">DEBIT NOTE</div>
                </td>
            </tr>
        </table>
        <table  style="border-style:solid;border-width: thin;">
            <tr>
                <td>
                    <strong>For Account of</strong>&nbsp;<asp:Label ID="lblEmpresa" runat="server" />
                    <br />
                    <br />
                    <br /><strong>PHONE:</strong>&nbsp;<asp:Label ID="lblTelefone" runat="server" />
                    <br />
                </td>
                <td>
                    <strong>INVOICE No:</strong>&nbsp;<asp:Label ID="lblNumeroInvoice" runat="server" />
                    <br /><strong>INVOICE DATE:</strong>&nbsp;<asp:Label ID="lblDataInvoice" runat="server" />
                    <br /><strong>DUE DATE:</strong>&nbsp;<asp:Label ID="lblDataVencimento" runat="server" />
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
                    <br /><strong>FROM:</strong>&nbsp;<asp:Label ID="lblOrigem" runat="server" />
                    <br /><strong>BY:</strong>&nbsp;<asp:Label ID="lblTransportador" runat="server" />
                    <br /><strong>DATE:</strong>&nbsp;<asp:Label ID="lblEmbarque" runat="server" />
                    <br /><strong>QUANTITY:</strong>&nbsp;<asp:Label ID="lblQtdVolumes" runat="server" />
                    
                </td>
                <td>
                    <strong>REF No:</strong>&nbsp;<asp:Label ID="lblReferencias" runat="server" />
                    <br /><strong>HBL:</strong>&nbsp;<asp:Label ID="lblHBL" runat="server" />
                    <br /><strong>VOYAGE:</strong>&nbsp;<asp:Label ID="lblViagem" runat="server" />
                    <br /><strong>TO:</strong>&nbsp;<asp:Label ID="lblDestino" runat="server" />
                    <br /><strong>GROSS WEIGHT:</strong>&nbsp;<asp:Label ID="lblPesoBruto" runat="server" />
                    <br /><strong>CHARGE WEIGHT:</strong>&nbsp;<asp:Label ID="lblPesoTaxado" runat="server" />

                </td>
            </tr>
                     
        </table>
           <br />
        <div id="divConteudoDinamico" runat="server" style="border-style:solid;border-width: thin;" >
        </div>
                                    <table >
            <tr>
                <td>
                                       <%-- <strong>CORRESPONDENT BANK:</strong>&nbsp;<asp:Label ID="lblCorrespondenteBank" runat="server" /><br/>
                                        <strong>STANDART:</strong>&nbsp;<asp:Label ID="Label2" runat="server" /><br/>
                                        <strong>SWIFT:</strong>&nbsp;<asp:Label ID="Label4" runat="server" /><br/>
                                        <strong>ACCOUNT:</strong>&nbsp;<asp:Label ID="Label3" runat="server" /><br/>--%>

                </td>
                <td>
<%--<strong>BANCO:</strong> SANTANDER<br/>
<strong>SWIFT:</strong> BSCHBRSPXXX<br/>
<strong>AGENCY:</strong> 3297<br/>
<strong>ACCOUNT:</strong> 130010712<br/>
<strong>NAME:</strong> FCA COMERCIO EXTERIOR E LOGISTICA LTDA<br/>
<strong>IBAN-BR:</strong> BR7890400888032970130010712C1<br/>--%>
                    <br/>
                                        <strong>BENEFICIARY NAME:</strong>&nbsp;<asp:Label ID="lblName" runat="server" /><br/>
                                        <strong>BANK NAME:</strong>&nbsp;<asp:Label ID="lblBanco" runat="server" /><br/>
                                        <strong>BANK ADRESS:</strong>&nbsp;<asp:Label ID="lblEnderecoBanco" runat="server" /><br/>
                                        <strong>ACCOUNT:</strong>&nbsp;<asp:Label ID="lblAccount" runat="server" /><br/>                                       
                                        <strong>SWIFT CODE:</strong>&nbsp;<asp:Label ID="lblSwift" runat="server" /><br/>
                                        <strong>IBAN-BR:</strong>&nbsp;<asp:Label ID="lblIban" runat="server" /><br/>
                </td>
            </tr>
        </table> <br />
            <br />
            <div style="float:right;font-size:10px"><strong><asp:Label ID="lblUsuario" runat="server" /></strong></div><br />

          


    </div>
        </div>
    </form>
</body>
</html>
