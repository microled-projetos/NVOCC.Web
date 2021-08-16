<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CotacaoPDF_ING.aspx.vb" Inherits="NVOCC.Web.CotacaoPDF_ING" EnableEventValidation="false" %>

<!DOCTYPE html>

<html>
<head runat="server">
 <title> COTA&Ccedil;&Atilde;O </title>
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
    background-color:#000000;
    width: 100%;
    margin-right: 15px;
    padding: 4px;
    font-weight: bold;
    color:white;
}
        body{
            margin:0;
           
        }



    </style>
  <%--   <script>
         window.onload = function () {
             window.print();
         }
     </script>--%>
</head>

<body style="margin:0;">
    <form id="form" runat="server" style="font-family:Arial;font-size:10px;">  

        <div runat="server" id="Div">
          
             <div class="interna" style="z-index: 2; position: absolute;" >
                 <div style="font-weight:bold;">
            FCA COM&Eacute;RCIO EXTERIOR E LOG&Iacute;STICA LTDA            <br/>
            <br/>

XV de Novembro Street, 46/48 - Centro<br/>
Santos - SP - BRASIL<br/>
Phone:(13) 3797-7850<br/>
CNPJ:00.639.367/0003-11<br/><br/>
Customer: <asp:label runat="server" ID="lblCliente" class="control-label" /><br/>
CNPJ:<asp:label runat="server" ID="lblCnpjCliente" class="control-label" /><br/><br/>
Dear,  <asp:label runat="server" ID="lblNome" class="control-label" />
Follows our Quotation for business opportunity.<br /><br/>
<asp:label runat="server" ID="lblTitulo" class="control-label" /><br />
Number:<asp:label runat="server" ID="lblNumeroCotacao" class="control-label" /><br/>
Issuance date: <asp:label runat="server" ID="lblDataAtual" class="control-label" /><br/></div>
<br/><div id="detalhesCarga" runat="server">
<div class="linha-colorida2">Cargo Details</div>
<strong>Gross weight:</strong> <asp:label runat="server" ID="lblPesoBruto" class="control-label" /><br />
<strong>Measurement CBM:</strong> <asp:label runat="server" ID="lblM3" class="control-label" /><br /> 
<strong>Taxed Weight:</strong> <asp:label runat="server" ID="lblPesoTaxado" class="control-label" /><br /> </div>
<br/>
<div class="linha-colorida2">INCOTERM: <asp:label runat="server" ID="lblINCOTERM" class="control-label" /></div>
<strong>Origin: </strong><asp:label runat="server" ID="lblOrigem" class="control-label" /><br />
<strong>Destination: </strong><asp:label runat="server" ID="lblDestino" class="control-label" /><br />
<strong>Via: </strong><asp:label runat="server" ID="lblVia" class="control-label" /><asp:label runat="server" ID="lblEscalas" class="control-label" /><br />
<strong>Transit Time: </strong><asp:label runat="server" ID="lblTTime" class="control-label" /><br />
<strong>Expiration date:</strong> <asp:label runat="server" ID="lblValidade" class="control-label" /><br />
<strong>Frequencia:</strong> <asp:label runat="server" ID="lblFrequencia" class="control-label" /><asp:label runat="server" ID="lblValorFrequencia" class="control-label" /><br />

<br/>
<div id="divConteudoDinamico" style="font-family:Arial;font-size:10px;" runat="server">
</div><br />
<br />
<div class="linha-colorida2">Quotation Details</div>
<div class="row linha-colorida1">Freight</div>
<div id="divConteudofrete" style="font-family:Arial;font-size:10px;" runat="server">
</div><div class="col-sm-4"><asp:label runat="server" Style="text-align:left"   ID="lblfretesCalc" class="control-label" /></div>
<br /><br /><div Visible="false" runat="server" style="text-align:right;font-weight:bold">Sub Total: <asp:label runat="server" ID="lblTotalFrete" class="control-label" /></div>

<div class="linha-colorida1">Origin charges</div>
<div id="divTaxaOrigem" style="font-family:Arial;font-size:10px;" runat="server">
</div> 
<br /><br /><div style="text-align:right;font-weight:bold">Sub Total: <asp:label runat="server" ID="lblTotalTaxasOrigem" class="control-label" /></div>

<div class="linha-colorida1">Destination charges</div>
<div id="divTaxaDestino" style="font-family:Arial;font-size:10px;" runat="server">
</div> 
<br /><br /><div style="text-align:right;font-weight:bold">Sub Total: <asp:label runat="server" ID="lblTotalTaxasDestino" class="control-label" /></div>
<div class="linha-colorida2">TOTAL CHARGES</div>
                                  <br />
<div><strong>TOTAL CHARGES:</strong> <asp:label runat="server" ID="lblTotalFinalTaxas" class="control-label" /></div>
<div><strong>TOTAL FREIGHT:</strong> <asp:label runat="server" ID="lblTotalFinalFrete" class="control-label" /></div>
<div style="color:red;text-align:right;font-weight:bold">TOTAL FINAL: <asp:label runat="server" ID="lblTotalFinal" class="control-label" /></div>

<div class="linha-colorida2">Remarks</div>
<div style="color:red">"Com rela&ccedil;&atilde;o ao Imposto sobre Opera&ccedil;&otilde;es Financeiras, informamos que o mesmo ser&aacute; atualizado de acordo com a taxa de convers&atilde;o do dia
do faturamento. Essa poder&aacute; divergir da taxa de convers&atilde;o do dia da proposta."</div>
<br/><br/>
<asp:label runat="server" ID="lblAnalista" class="control-label" /><br/>
FCA COM&Eacute;RCIO EXTERIOR E LOG&Iacute;STICA LTDA<br/>
Tel No.-+55 13 3213-6670<br/>
<asp:label runat="server" ID="Label1" Text="www.fcalog.com.br" class="control-label" /><br/>
</div>
             
          
                    <br/><br/>
    <asp:label runat="server" ID="lblTexto" class="control-label" />     </div> 
    </form>
    
</body>
</html>
