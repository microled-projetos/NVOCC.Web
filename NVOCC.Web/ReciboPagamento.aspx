<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ReciboPagamento.aspx.vb" Inherits="NVOCC.Web.ReciboPagamento" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>ReciboPagamento</title>
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
        <div id="DivImpressao" class="DivImpressao table-content" style="font-size: 10px; margin-bottom: 10px;margin-top: 30px;max-width:770px">
       
                    <table>
                        <tr>
                            <td>
                                <img src="Content/imagens/Logo_FCA_Deitado1.png" /><br />
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td>
                                <div style="text-align: center; font-size: 20px"><strong>RECIBO</strong></div>
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td>
                                <strong><asp:Label ID="lblRazaoFCA" runat="server" /></strong><br />
                                <div>
                                   <asp:Label ID="lblEnderecoFCA1" runat="server" /><br />
                                   <asp:Label ID="lblEnderecoFCA2" runat="server" /><br />
                                    FONE:<asp:Label ID="lblTelefoneFCA" runat="server" /><br />
                                    CNPJ:<asp:Label ID="lblCNPJFCA" runat="server" />  &nbsp;&nbsp;&nbsp;I.E:<asp:Label ID="lblIEFCA" runat="server" />
                                </div>

                                <%--<strong>FCA COMERCIO EXTERIOR E LOGISTICA LTDA.</strong><br />
                                <div>
                                    R QUINZE DE NOVEMBRO, 46/48 - CENTRO<br />
                                    SANTOS - SÃO PAULO - BRASIL - CEP:11010150<br />
                                    FONE:+55 13 3797-7850 - FAX:<br />
                                    CNPJ:00.639.367/0003-11 &nbsp;&nbsp;&nbsp;I.E: 633.672.235.110
                                </div>--%>
                            </td>
                            <td>
                                <strong>Nº:</strong>&nbsp;<asp:Label ID="Label1" runat="server" /><asp:Label ID="lblNumeroRecibo" runat="server" /><br />
                                <strong>EMISSÃO:</strong>&nbsp;<asp:Label ID="Label3" runat="server" /><asp:Label ID="lblEmissao" runat="server" /><br />
                            </td>
                        </tr>
                    </table>

                    <table style="border-style: solid; border-width: thin;">
                        <tr>
                            <td>RECEBEMOS DE <asp:Label ID="lblEmpresa" runat="server" /><br />
                                O VALOR REFERENTE AO(S) PROCESSO(S):
                    <br />
                            </td>
                            <td>CNPJ/CPF:&nbsp;<asp:Label ID="lblCNPJCPF" runat="server" /></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>N/REFERÊNCIA:&nbsp;<asp:Label ID="lblNRef" runat="server" />
                                <br />
                            </td>
                            <td>S/REFERÊNCIA:&nbsp;<asp:Label ID="lblSRef" runat="server" />
                                <br />
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>MBL:&nbsp;<asp:Label ID="lblMBL" runat="server" />
                                <br />
                            </td>
                            <td>HBL:&nbsp;<asp:Label ID="lblHBL" runat="server" />
                                <br />
                            </td>
                            <td></td>
                        </tr>
                    </table>

                    <br />
                    <br />
                    <div id="divConteudoDinamico" runat="server" style="border-style: solid; border-width: thin;">
                        TABELA DE DESPESAS
            
            
                    </div>
                    <br />
                    <br />
                    <div id="div1" runat="server" style="border-style: solid; border-width: thin; padding: 10px;">
                        <table>
                        <tr>
                            <td style="float: right;">                        <strong>VALOR LIQUIDO:</strong>&nbsp;<asp:Label ID="lblValor" runat="server" /><br />

                            </td>
                            </tr><tr>
                            <td style="float: left;">                        <strong>VALOR POR EXTENSO:</strong>&nbsp;<asp:Label ID="lblValorExtenso" runat="server" /><br />

                            </td>
                        </tr>
                    </table>
                    </div>
                    <br />
                    <br />
                    <div id="div2" runat="server" style="float: right;"> <br /> <br />
                        <strong>_____________________________________________________________________________________________________________________________</strong><br />
                        <strong>FCA COMERCIO EXTERIOR E LOGISTICA LTDA.</strong>

                    </div>
                    <br />
                    <br />
                
    </div>
    </form>
</body>
</html>
