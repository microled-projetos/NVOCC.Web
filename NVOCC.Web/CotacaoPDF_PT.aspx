<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CotacaoPDF_PT.aspx.vb" Inherits="NVOCC.Web.CotacaoPDF_PT" EnableEventValidation="false" %>

<!DOCTYPE html>

<html>
<head runat="server">

    <title>COTA&Ccedil;&Atilde;O </title>
    <style>
        .linha-colorida1 {
            margin-top: 10px;
            background-color: #cccccc;
            width: 100%;
            margin-right: 15px;
            padding: 4px;
            font-weight: bold;
        }

        .linha-colorida2 {
            margin-top: 10px;
            background-color: #000000;
            width: 100%;
            margin-right: 15px;
            padding: 4px;
            font-weight: bold;
            color: white;
        }

        body {
            margin: 0;
        }
    </style>
</head>

<body style="margin: 0;">
    <form id="form" runat="server" style="font-family: Arial; font-size: 10px;">
        <div class="interna" style="z-index: 2; position: absolute;">
            <div style="font-weight: bold;">
                <table style="font-family: Arial; font-size: 10px; font-weight: bold">
                    <tr>
                        <td>
                            <p>
                                FCA COM&Eacute;RCIO EXTERIOR E LOG&Iacute;STICA LTDA
                                <br />
                                <br />
                                <asp:Label runat="server" ID="lblEnderecoFCA" class="control-label" /><br />
                                <asp:Label runat="server" ID="lblCidadeFCA" class="control-label" />
                                - SP - BRASIL<br />
                                Fone:<asp:Label runat="server" ID="lblTelefoneFCA" class="control-label" /><br />
                                CNPJ:<asp:Label runat="server" ID="lblCNPJFCA" class="control-label" />
                            </p>
                        </td>
                        <td>
                            <p style="margin-left: 55px;">
                                <asp:Label runat="server" ID="lblTitulo" class="control-label" /><br />
                                N&uacute;mero:<asp:Label runat="server" ID="lblNumeroCotacao" class="control-label" /><br />
                                Data emiss&atilde;o:<asp:Label runat="server" ID="lblDataAtual" class="control-label" />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="Label1" class="control-label" /><br />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="Label2" class="control-label" /><br />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="Label3" class="control-label" /><br />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="Label4" class="control-label" />
                            </p>
                        </td>
                    </tr>
                </table>
                <br />
                &nbsp;Cliente:
                    <asp:Label runat="server" ID="lblCliente" class="control-label" /><br />
                &nbsp;CNPJ:<asp:Label runat="server" ID="lblCnpjCliente" class="control-label" />
                <br />
                <br />
                &nbsp;Prezado (a), 
                    <asp:Label runat="server" ID="lblNome" class="control-label" />
                Segue sua proposta visando uma oportunidade de embarque.<br />
                <br />
            </div>

            <div id="detalhesCarga" runat="server">
                <div class="linha-colorida2">Detalhes da carga</div>
                <strong>&nbsp;Peso bruto:</strong>
                <asp:Label runat="server" ID="lblPesoBruto" class="control-label" /><br />
                <asp:Label runat="server" ID="lblM3" class="control-label" />
                <asp:Label runat="server" ID="lblPesoTaxado" class="control-label" />
                <asp:Label runat="server" ID="lblTipoCargaLCL" class="control-label" /><br />
                <div id="divCargaLCL" style="font-family: Arial; font-size: 10px;" runat="server"></div>
            </div>
            <div id="divMedidasAereo" style="font-family: Arial; font-size: 10px;" runat="server"></div>

            <div class="linha-colorida2">
                INCOTERM:
                    <asp:Label runat="server" ID="lblINCOTERM" class="control-label" />
            </div>
            <strong>Origem: </strong>
            <asp:Label runat="server" ID="lblOrigem" class="control-label" /><br />
            <strong>Destino: </strong>
            <asp:Label runat="server" ID="lblDestino" class="control-label" /><br />
            <asp:Label runat="server" ID="lblCiaAerea" class="control-label" />
            <strong>Via: </strong>
            <asp:Label runat="server" ID="lblVia" class="control-label" /><asp:Label runat="server" ID="lblEscalas" class="control-label" /><br />
            <strong>Transit Time: </strong>
            <asp:Label runat="server" ID="lblTTime" class="control-label" /><asp:Label runat="server" ID="lblTTimeAereo" class="control-label" /><br />
            <strong>Data de validade:</strong>
            <asp:Label runat="server" ID="lblValidade" class="control-label" /><br />
            <strong>Frequencia:</strong>
            <asp:Label runat="server" ID="lblFrequencia" class="control-label" /><asp:Label runat="server" ID="lblValorFrequencia" class="control-label" />
            <asp:Label runat="server" ID="lblTipoCargaFCL" class="control-label" /><br />
            <div id="divCargaFCL" style="font-family: Arial; font-size: 10px;" runat="server"></div>
            <div id="divConteudoDinamico" style="font-family: Arial; font-size: 10px;" runat="server">
            </div>
            <br />
            <div class="linha-colorida2">Detalhes da Cota&ccedil;&atilde;o</div>
            <div class="row linha-colorida1">Frete</div>
            <div id="divConteudofrete" style="font-family: Arial; font-size: 10px;" runat="server">
            </div>
            <div class="col-sm-4">
                <asp:Label runat="server" Style="text-align: left" ID="lblfretesCalc" class="control-label" />
            </div>
            <br />
            <div visible="false" runat="server" style="text-align: right; font-weight: bold">
                Sub Total:
                    <asp:Label runat="server" ID="lblTotalFrete" class="control-label" />
            </div>
            <div class="linha-colorida1">Taxas de origem</div>
            <div id="divTaxaOrigem" style="font-family: Arial; font-size: 10px;" runat="server">
            </div>
            <br />
            <div style="text-align: right; font-weight: bold">
                Sub Total:
                    <asp:Label runat="server" ID="lblTotalTaxasOrigem" class="control-label" />
            </div>

            <div class="linha-colorida1">Taxas de destino</div>
            <div id="divTaxaDestino" style="font-family: Arial; font-size: 10px;" runat="server">
            </div>
            <div style="text-align: right; font-weight: bold">
                Sub Total:
                    <asp:Label runat="server" ID="lblTotalTaxasDestino" class="control-label moeda" />
            </div>
            <div class="linha-colorida2">CUSTOS TOTAIS</div>
            <div>
                <strong>TOTAL TAXAS:</strong>
                <asp:Label runat="server" ID="lblTotalFinalTaxas" class="control-label" />
            </div>
            <div>
                <strong>TOTAL FRETE:</strong>
                <asp:Label runat="server" ID="lblTotalFinalFrete" class="control-label" />
            </div>
            <br />
            <div style="color: red; font-weight: bold">
                TOTAL FINAL:
                    <asp:Label runat="server" ID="lblTotalFinal" class="control-label" />
            </div>
            <br />
            <div class="linha-colorida2">Observa&ccedil;&otilde;es</div>
            <div style="color: red">
                "Com rela&ccedil;&atilde;o ao Imposto sobre Opera&ccedil;&otilde;es Financeiras, informamos que o mesmo ser&aacute; atualizado de acordo com a taxa de convers&atilde;o do dia
do faturamento. Essa poder&aacute; divergir da taxa de convers&atilde;o do dia da proposta."
            </div>

            <asp:Label runat="server" ID="lblObsCliente" class="control-label" />
            <div id="divTexto" runat="server" style="font-family: Arial; font-size: 9px;"></div>
            <br />
            <asp:Label runat="server" ID="lblAnalista" class="control-label" /><br />
            FCA COM&Eacute;RCIO EXTERIOR E LOG&Iacute;STICA LTDA<br />
            Tel No.<asp:Label runat="server" ID="lblTelefoneAnalista" class="control-label" />
            <br />
            www.fcalog.com.br
        </div>
    </form>

</body>
</html>
