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

        <div runat="server" id="DivIngles">
          
             <div class="interna" style="z-index: 2; position: absolute;" >
                 <div style="font-weight:bold;">
            FCA COM&Eacute;RCIO EXTERIOR E LOG&Iacute;STICA LTDA            <br/>
            <br/>

XV de Novembro Street, 46/48 - Centro<br/>
Santos - SP - BRASIL<br/>
Phone:<br/>
CNPJ:<br/><br/>
Customer: <asp:label runat="server" ID="lblClienteIngles" class="control-label" /><br/>
CNPJ:<asp:label runat="server" ID="lblCnpjClienteIngles" class="control-label" /><br/><br/>
Dear,  <asp:label runat="server" ID="lblNomeIngles" class="control-label" />
Follows our Quotation for business opportunity.<br /><br/>
<asp:label runat="server" ID="lblTituloIngles" class="control-label" /><br />
Number:<asp:label runat="server" ID="lblNumeroCotacaoIngles" class="control-label" /><br/>
Issuance date: <asp:label runat="server" ID="lblDataAtualIngles" class="control-label" /><br/></div>
<br/>
<div class="linha-colorida2">Cargo Details</div>
<strong>Gross weight:</strong> <asp:label runat="server" ID="lblPesoBrutoIngles" class="control-label" /><br />
<strong>Measurement CBM:</strong> <asp:label runat="server" ID="lblM3Ingles" class="control-label" /><br /> 
<strong>Taxed Weight:</strong> <asp:label runat="server" ID="lblPesoTaxadoIngles" class="control-label" /><br /> 
<br/>
<div class="linha-colorida2">INCOTERM: <asp:label runat="server" ID="lblINCOTERMIngles" class="control-label" /></div>
<strong>Origin: </strong><asp:label runat="server" ID="lblOrigemIngles" class="control-label" /><br />
<strong>Destination: </strong><asp:label runat="server" ID="lblDestinoIngles" class="control-label" /><br />
<strong>Via: </strong><asp:label runat="server" ID="lblViaIngles" class="control-label" /><br />
<strong>Transit Time: </strong><asp:label runat="server" ID="lblTTimeIngles" class="control-label" /><br />
<strong>Expiration date:</strong> <asp:label runat="server" ID="lblValidadeIngles" class="control-label" /><br />
<br/>
<div id="divConteudoDinamico" style="font-family:Arial;font-size:10px;" runat="server">
</div><br />
<br />
<div class="linha-colorida2">Quotation Details</div>
<div class="row linha-colorida1">Freight</div>
<div id="divConteudofrete" style="font-family:Arial;font-size:10px;" runat="server">
</div><div class="col-sm-4"><asp:label runat="server" Style="text-align:left"   ID="lblfretesCalcIngles" class="control-label" /></div>
<br /><br /><div Visible="false" runat="server" style="text-align:right;font-weight:bold">Sub Total: <asp:label runat="server" ID="lblTotalFreteIngles" class="control-label" /></div>

<div class="linha-colorida1">Origin charges</div>
<div id="divTaxaOrigem" style="font-family:Arial;font-size:10px;" runat="server">
</div> 
<br /><br /><div style="text-align:right;font-weight:bold">Sub Total: <asp:label runat="server" ID="lblTotalTaxasOrigemIngles" class="control-label" /></div>

<div class="linha-colorida1">Destination charges</div>
<div id="divTaxaDestino" style="font-family:Arial;font-size:10px;" runat="server">
</div> 
<br /><br /><div style="text-align:right;font-weight:bold">Sub Total: <asp:label runat="server" ID="lblTotalTaxasDestinoIngles" class="control-label" /></div>
<div class="linha-colorida2">TOTAL CHARGES</div>
                                  <br />
<div><strong>TOTAL CHARGES:</strong> <asp:label runat="server" ID="lblTotalFinalTaxas" class="control-label" /></div>
<div><strong>TOTAL FREIGHT:</strong> <asp:label runat="server" ID="lblTotalFinalFrete" class="control-label" /></div>
<div style="color:red;text-align:right;font-weight:bold">TOTAL FINAL: <asp:label runat="server" ID="lblTotalFinalGeral" class="control-label" /></div>

<div class="linha-colorida2">Remarks</div>
<div style="color:red">"Com rela&ccedil;&atilde;o ao Imposto sobre Opera&ccedil;&otilde;es Financeiras, informamos que o mesmo ser&aacute; atualizado de acordo com a taxa de convers&atilde;o do dia
do faturamento. Essa poder&aacute; divergir da taxa de convers&atilde;o do dia da proposta."</div>
<br/><br/>
<asp:label runat="server" ID="lblAnalistaIngles" class="control-label" /><br/>
FCA COM&Eacute;RCIO EXTERIOR E LOG&Iacute;STICA LTDA<br/>
Tel No.-+55 13 3213-6670<br/>
<asp:label runat="server" ID="Label1" Text="www.fcalog.com.br" class="control-label" /><br/>
</div>
             
            </div>
        
    </form>
    
</body>
</html>
