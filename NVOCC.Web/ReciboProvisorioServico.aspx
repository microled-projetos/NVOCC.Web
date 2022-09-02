<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ReciboProvisorioServico.aspx.vb" Inherits="NVOCC.Web.ReciboProvisorioServico" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>ReciboProvisorioServico</title>
     <script src="Content/js/jquery.min.js"></script>
    <script>             
    $(window).load(function () {
        window.print();
    });
    </script>
    <style>
        table{
            width:780px;
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
        <div id="DivImpressao" class="DivImpressao table-content" style="font-size: 10px; margin-bottom: 10px;margin-top: 30px;max-width:780px">
                
        <table  style="border-style:solid;border-width: thin;">
            <tr>               
                <td>
                    <center>
    <img src="Content/imagens/Logo_FCA_Deitado1.png" /><br />
                </td>
                <td>
                    <center>
    <strong>FCA COMERCIO EXTERIOR E LOGISTICA LTDA.</strong><br /></center>
                    <div style="text-align: center;">
                        R QUINZE DE NOVEMBRO, 46/48 - CENTRO<br />
                        SANTOS - SÃO PAULO - BRASIL - CEP:11010150<br />
                        FONE:+55 13 3797-7850 - FAX:<br />
                        CNPJ:00.639.367/0003-11  &nbsp;&nbsp;&nbsp;I.E: 633.672.235.110
                    </div>
                </td>
                <td>
                    <strong>RPS Nº:</strong>&nbsp;<asp:Label ID="Label1" runat="server" /><asp:Label ID="lblNumeroRPS" runat="server" /><br/>
                        <strong>Data Emissão:</strong>&nbsp;<asp:Label ID="Label3" runat="server" /><asp:Label ID="lblEmissao" runat="server" /><br />
                        <strong>Data Vencimento:</strong>&nbsp;<asp:Label ID="Label5" runat="server" /><asp:Label ID="lblVencimento" runat="server" />
                </td>
            </tr>
        </table>
        <table border="1">
            <tr>
                <td>
                    <div style="text-align: center;">RECIBO PROVISÓRIO DE SERVIÇOS</div>
                </td>
            </tr>
        </table>
        <table  style="border-style:solid;border-width: thin;">
            <tr>
                <td>
                    <strong>CLIENTE</strong>&nbsp;<asp:Label ID="lblEmpresa" runat="server" />
                    <br />
                </td>
                <td>
                    <strong>CNPJ:</strong>&nbsp;<asp:Label ID="lblCNPJ" runat="server" />
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                    <strong>ENDEREÇO:</strong>&nbsp;<asp:Label ID="lblEndereco" runat="server" /><asp:Label ID="lblNumero" runat="server" />
                    <br />
                </td>
                <td>
                    <strong>CEP:</strong>&nbsp;<asp:Label ID="lblCEP" runat="server" />
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                    <strong>BAIRRO:</strong>&nbsp;<asp:Label ID="lblBairro" runat="server" />
                    <br />
                </td>
                <td>
                    <strong>CIDADE:</strong>&nbsp;<asp:Label ID="lblCidade" runat="server" />
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                    <strong>INSCR. ESTADUAL:</strong>&nbsp;<asp:Label ID="lblInscrEstadual" runat="server" />
                    <br />
                </td>
                <td>                   
                </td>
            </tr>
        </table>
        <table style="border-style:solid;border-width: thin;">
            <tr>
                <td>
                    <strong>N/REFERÊNCIA:</strong>&nbsp;<asp:Label ID="lblReferencia" runat="server" />
                    <br />
                </td>
                <td>                    
                </td>
                <td>                    
                </td>
            </tr>
            <tr>
                <td>
                    <strong>REF. CLIENTE:</strong>&nbsp;<asp:Label ID="lblRefCliente" runat="server" />
                    <br />
                </td>
                  <td>                    
                </td>
                <td>                    
                </td>
            </tr>
            <tr>
                <td>
                    <strong>SERVICO:</strong>&nbsp;<asp:Label ID="lblServico" runat="server" />
                    <br />
                </td>
                  <td>                    
                </td>
                <td>                    
                </td>
            </tr>
            <tr>
                <td>
                    <strong>CHEGADA:</strong>&nbsp;<asp:Label ID="lblChegada" runat="server" />
                    <br />
                </td>
                <td>
                    <strong>HOUSE:</strong>&nbsp;<asp:Label ID="lblHouse" runat="server" />
                    <br />
                </td>
                <td>
                   <strong>MASTER:</strong>&nbsp;<asp:Label ID="lblMaster" runat="server" />
 <br />
                </td>
            </tr>
            
            
          
        </table>
        <table border="1">
            <tr>
                <td>
                    <div style="text-align: center;">DISCRIMINAÇÃO DE SERVIÇOS TRIBUTÁVEIS</div>
                </td>
            </tr>
        </table>
                            <br />
        <div id="divConteudoDinamico" runat="server" >
        </div>
            <div id="div1" runat="server" style="border-style:solid;border-width: thin;padding:10px;" >
                 <strong>OBSERVAÇÕES:</strong>&nbsp;<asp:Label ID="lblObs" runat="server" /><br/>
        </div>
            <div id="div2" runat="server" style="border-style:solid;border-width: thin;padding:10px;padding-top:50px;" >
                    <br />            
                <br />
                <strong>_____________________________________________________________________________________________________________________________</strong><br />
                    <strong>FCA COMERCIO EXTERIOR E LOGISTICA LTDA.</strong>

        </div>
           <br />
            <br />


    </div>
    </form>
</body>
</html>
