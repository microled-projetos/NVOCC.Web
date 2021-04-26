<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="EmissaoBL.aspx.vb" Inherits="NVOCC.Web.EmissaoBL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>

        #DivImpressao{
            display:none
        }
        th, td {
    padding: 1px 1px;
}
        td{
  border:1px solid #000000;
  }
        @media print {
        #DivImpressao{
            display:block;
            margin-top:0
        }
        @page{
            
        }
      #DivPrincial,#imgFundo { 
display:none; 

}
        }
        
    </style>

<div ID="DivPrincial" class="row principal">
       <div class="col-lg-12 col-md-12 col-sm-12">

            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">EMISSÃO DO BL - <asp:label runat="server" ID="lblNumHBL" class="control-label" Style="font-size:17px" />
                    </h3>
                </div>
                <div class="panel-body">
                     <div class="tab-content">
                        <div class="tab-pane fade active in" id="cadastro">                  
        <asp:TextBox ID="txtID" runat="server" CssClass="form-control" Style="display:none"></asp:TextBox>                    
                <div class="row">
                       <div class="col-md-6">
                        <div class="form-group">
                            <label class="control-label">Shiper:</label>
                            <asp:TextBox ID="txtCliente" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2" ></asp:TextBox>                    
                        </div>
                    </div>
                        <div class="col-md-6">
                        <div class="form-group">
                            <label class="control-label">Consigned to order of:</label>
                            <asp:TextBox ID="txtImportador1" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2" ></asp:TextBox>                    
                        </div>
                    </div>
                </div>                                      
                <div class="row">
                   <div class="col-md-6">
                        <div class="form-group">
                            <label class="control-label">Consignor:</label>
                            <asp:TextBox ID="txtImportador2" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2" ></asp:TextBox>                    
                        </div>
                    </div>
                        <div class="col-md-6">
                        <div class="form-group">
                            <label class="control-label">For Delivery of Goods, please apply to:</label>
                            <asp:TextBox ID="txtCampoEditavel" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2" ></asp:TextBox>                    
                        </div>
                    </div>
                </div>
                <div class="row">
             <div class="col-md-6" >
                        <div class="form-group">
                            <label class="control-label">Notify address:</label>
                            <asp:TextBox ID="txtImportador3" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2" ></asp:TextBox>                    
                        </div> 
                 </div>
                    <div class="col-md-3" >
                  <div class="form-group">
                            <label class="control-label">Voy Nº:</label>
                            <asp:TextBox ID="txtViagem" runat="server" CssClass="form-control" ></asp:TextBox>                    
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label class="control-label">Ocean Vessel:</label>
                            <asp:TextBox ID="txtNavio" runat="server" CssClass="form-control" ></asp:TextBox>                    
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label class="control-label">Number of Original B/Ls:</label>
                            <asp:TextBox ID="txtCampoEditavel1" runat="server" Text="EXPRESS RELEASE" CssClass="form-control" ></asp:TextBox>                    
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label class="control-label">Freight Payable at:</label>
                            <asp:TextBox ID="txtTipoPagamento" runat="server" CssClass="form-control" ></asp:TextBox>                    
                        </div>
                    </div>
                   
         </div>
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label class="control-label">Port of Loading:</label>
                            <asp:TextBox ID="txtOrigem" runat="server" CssClass="form-control" ></asp:TextBox>                    
                        </div>
                    </div>
                        <div class="col-md-3">
                        <div class="form-group">
                            <label class="control-label">Place of Receipt:</label>
                            <asp:TextBox ID="txtCampoEditavel2" runat="server" CssClass="form-control" ></asp:TextBox>                    
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label class="control-label">Port of Discharge:</label>
                            <asp:TextBox ID="txtDestino" runat="server" CssClass="form-control" ></asp:TextBox>                    
                        </div>
                    </div>
                        <div class="col-md-3">
                        <div class="form-group">
                            <label class="control-label">Port of Delivery:</label>
                            <asp:TextBox ID="txtCampoEditavel3" runat="server" CssClass="form-control" ></asp:TextBox>                    
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label class="control-label">Mark and Number:</label>
                            <asp:TextBox ID="txtContainer" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2" ></asp:TextBox> 
                        </div>
                    </div>
                        <div class="col-md-3">
                        <div class="form-group">
                            <label class="control-label">Number of Kind of Packages:</label>
                            <asp:TextBox ID="txtQtdVolumes" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2" ></asp:TextBox>                    
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label class="control-label">Commodity:</label>
                            <asp:TextBox ID="txtCampoEditavel4" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2" ></asp:TextBox>                    
                        </div>
                    </div>
                        <div class="col-md-3">
                        <div class="form-group">
                            <label class="control-label">Description of goods:</label>
                            <asp:TextBox ID="txtCampoEditavel5" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2" ></asp:TextBox>                 <%--   onkeyUp="return CheckMaxCount(this,event,250);"--%>
                        </div>
                    </div>
                </div>
                <div class="row">
                                 <div class="col-md-6" >
                        <div class="form-group">
                            <label class="control-label">Freight and Charges:</label>
                            <asp:TextBox ID="txtFreteTaxa" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2" ></asp:TextBox>                    
                        </div> 
                 </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label class="control-label">Currency:</label>
                            <asp:TextBox ID="txtMoeda" runat="server" CssClass="form-control" ></asp:TextBox>                    
                        </div>
                    </div>
                        <div class="col-md-3">
                        <div class="form-group">
                            <label class="control-label">Freight:</label>
                            <asp:TextBox ID="txtFrete" runat="server" CssClass="form-control" ></asp:TextBox>                    
                        </div>
                    </div>
                    
                       
            </div>
                <div class="row">
<div class="col-sm-3">
<div class="form-group">
<label class="control-label">Gross Weight:</label>
<asp:TextBox ID="txtPesoBruto" runat="server" CssClass="form-control" ></asp:TextBox>                    
</div>
</div>
<div class="col-sm-3">
<div class="form-group">
<label class="control-label">Net Weight:</label>
<asp:TextBox ID="txtPesoLiquido" runat="server" CssClass="form-control" ></asp:TextBox>                    
 </div>
</div>
<div class="col-sm-3">
<div class="form-group">
<label class="control-label">Measurement:</label>
<asp:TextBox ID="txtM3" runat="server" CssClass="form-control" ></asp:TextBox>                    
</div>
</div>
           
</div>
                <div class="row">
                 <div class="col-md-6">
                        <div class="form-group">
                            <label class="control-label">As Agent For:</label>
                            <asp:TextBox ID="txtAgente" runat="server" CssClass="form-control"></asp:TextBox>                    
                        </div>
                    </div>
            <div class="col-md-3">
                        <div class="form-group">
                            <label class="control-label">Signature:</label>
                            <asp:TextBox ID="txtCampoEditavel6" runat="server" CssClass="form-control" ></asp:TextBox>                    
                        </div>
                    </div>
            <div class="col-md-3">
                        <div class="form-group">
                            <label class="control-label">CPF:</label>
                            <asp:TextBox ID="txtCPF" runat="server" CssClass="form-control"></asp:TextBox>                    
                        </div>
                    </div>
                </div>
                <div class="row">
            <div class="col-md-3">
                        <div class="form-group">
                            <label class="control-label">Impressao:</label>
                            <asp:TextBox ID="txtImpressao" runat="server" Text="EXPRESS RELEASE" CssClass="form-control" ></asp:TextBox>                    
                        </div>
                    </div>
                             <div class="col-md-3">
                        <div class="form-group">
                            <label class="control-label">Place:</label>
                            <asp:TextBox ID="txtOrigemPagamento" runat="server" CssClass="form-control" ></asp:TextBox>                    
                        </div>
                    </div>
                <div class="col-md-3">
                        <div class="form-group">
                            <label class="control-label">Date of Issue:</label>
                            <asp:TextBox ID="txtCampoEditavel7" runat="server" CssClass="form-control" ></asp:TextBox>                    
                        </div>
                    </div>
                </div>
                <div class="row">

                                <div class="col-sm-3 col-sm-offset-6">
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <asp:Button ID="btnVoltar" runat="server" CssClass="btn btn-warning btn-block" Text="Voltar"  />
                                    </div>
                                </div>

                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <label>&nbsp;</label>                               
                                                    <button type="button" id="btnCadastrarContainer" class="btn btn-primary btn-block" onclick="Imprimir()">Imprimir</button>
                                    </div>
                                </div>
                            </div>   
                            </div>
                                  
                            </div>
                        

            </div>
       </div>
    </div>
</div>
    <div id="DivImpressao" class="DivImpressao">
       <center><h3>BILL OF LADING</h3></center>
        <div id="DivVariavel" class="DivVariavel" style="font-size:9px">
            <table border="1">
<tr>
<td>
    <table >
<tr>
<td><strong>SHIPPER/EXPORTER</strong><br/>
    <asp:Label ID="lblCliente" class="lblCliente" runat="server"></asp:Label>

</td>
</tr>
<tr>
<td><strong>CONSIGNED TO ORDER OF</strong><br/>
    <asp:Label ID="lblImportador1" class="lblImportador1" runat="server"></asp:Label>
</td>
</tr>
<tr>
<td><strong>NOTIFY ADDRESS</strong><br/>
        <asp:Label ID="lblImportador3" class="lblImportador3" runat="server"></asp:Label>
</td>
</tr>
</table>
</td>
<td><center>H/BL: <asp:Label ID="lblNumeroBLImpressao" class="lblNumeroBLImpressao" runat="server"></asp:Label><br />
    <img src="Content/imagens/FCA-Log - Copia.png" /><br />
    <strong>FCA COMERCIO EXTERIOR E LOGISTICA LTDA.</strong><br /></center>
   <div style="font-size:10px;text-align: center;"> R QUINZE DE NOVEMBRO, 46/48 - ANDAR 1 SALA 01 - CENTRO<br />
    SANTOS - SP - BRASIL - CEP:11010150<br />
    Phone:+55 13 3797-7850 - Fax: +55</div>
</td>
</tr>
<tr>
<td><table>
<tr>
<td><strong>OCEAN VESSEL:</strong><br/>
        <asp:Label ID="lblNavio" class="lblNavio" runat="server"></asp:Label><br/></td>
<td><strong> PLACE OF RECEIPT</strong><br/>
        <asp:Label ID="lblCampoEditavel2" class="lblCampoEditavel2" runat="server"></asp:Label></td>
</tr>
<tr>
<td>
    <strong>VG:</strong><br/>
        <asp:Label ID="lblViagem" class="lblViagem" runat="server"></asp:Label></td>
<td><strong>PORT OF LOADING</strong><br/>
        <asp:Label ID="lblOrigem" class="lblOrigem" runat="server"></asp:Label></td>
</tr>
    <tr>
<td><strong>PORT OF DISCHARGE</strong><br/>
        <asp:Label ID="lblDestino" class="lblDestino" runat="server"></asp:Label></td>
<td><strong>PORT OF DELIVERY</strong><br/>
        <asp:Label ID="lblCampoEditavel3" class="lblCampoEditavel3" runat="server"></asp:Label></td>
</tr>
</table></td>
<td>
    <table >
<tr>
<td style="width:75%;border:none !important;"><center><strong><asp:Label ID="lblImpressao" class="lblImpressao" runat="server"></asp:Label></strong></center>
        </td>
</tr>
<tr>
<td><strong>FREIGH PAYABLE</strong><br/>
        <asp:Label ID="lblTipoPagamento" class="lblTipoPagamento" runat="server"></asp:Label> </td>
<td style="font-size:8px"><strong>NUMBER OF ORIGINAL B/LS</strong><br/>
        <asp:Label ID="lblCampoEditavel1" class="lblCampoEditavel1" runat="server"></asp:Label></td>
</tr>
</table>
</td>
</tr>
<tr>
<td><table >
<tr>
<td style="width:75%"><strong>MARKS AND NUMBERS</strong><br/>
        <asp:Label ID="lblContainer" class="lblContainer" runat="server"></asp:Label></td>
<td style="width:15%;font-size:8px"><div style="top:0"><strong>NUMBER OF KIND OF PACKAGES</strong></div>
        <asp:Label ID="lblQtdVolumes" class="lblQtdVolumes" runat="server"></asp:Label><br/><br/><br />
    <strong>GROSS WEIGHT:</strong><asp:Label ID="lblPesoBruto" class="lblPesoBruto" runat="server"></asp:Label>
    <br/>
     <strong>NET WEIGHT:</strong><asp:Label ID="lblPesoLiquido" class="lblPesoLiquido" runat="server"></asp:Label>
   <br/>
   <strong>MEASUREMENT:</strong><asp:Label ID="lblM3" class="lblM3" runat="server"></asp:Label>
</td>
</tr>
</table></td>
<td>
<table>
<tr>
<td><strong>DESCRIPTION OF GOODS</strong><br/>
        <asp:Label ID="lblCampoEditavel5" class="lblCampoEditavel5" runat="server"></asp:Label></td>
</tr>
</table>

<tr>
<td><strong>FOR DELIVERY OF GOODS, PLEASE APPLY TO</strong><br/>
    <asp:Label ID="lblCampoEditavel" class="lblCampoEditavel" runat="server"></asp:Label>
    <br/><br />
      <div style="font-size:8px"> Received by the Carrier from the Shipper in apparent good order and condition unless otherwise<br />
indicated herein, the Goods, or the Container(s), or package(s) said to contain the cargo herein<br />
mentioned, to be carried subject to all the terms and conditions appearing on the face and back of this<br />
Bill of Lading by the vessel named herein or any substitute at the Carrier's option and/or other means<br />
of transport, from the Place of Receipt or the Port of Loading to the Port of Discharge or the Place of<br />
Delivery shown herein and there to be delivered unto order or assigns.<br />
This Bill of Lading duly endorsed must be surrendered in exchange for the Goods or delivery order.<br />
In accepting this Bill of Lading, the Merchant agrees to be bound by all the stipulations, exceptions,<br />
terms and conditions on the face and back hereof and of the Carrier?s applicable tariff, whether<br />
written, typed, stamped or printed, as fully as if signed by the Merchant, any local custom or privilege<br />
to the contrary notwithstanding, agrees that all agreements or freight engagement for and in<br />
connection with the Carriage of the Goods are superseded by this Bill of Lading.<br />
Particulars furnished by Merchant. All descriptions contained herein considered unknown to the<br />
Carrier.<br />
Merchant's Declared Value...............................(see Clause 7.3):<br />
Note:<br />
The Merchant's attention is called to the fact that according to Clauses 18 & 23 of this Bill of Lading,<br />
the liability of the Carrier is, in most cases, limited in respect of loss of or damage to the Goods.<br />
In witness whereof, the undersigned has signed the number of Bill(s) of Lading stated herein, all of<br />
this tenor and date, one of which being accomplished, the others to stand void.<br />
As Carrier<br />
An enlarged copy of back clauses is available from the Carrier upon request.</div><br />
    The goods ans instructions are accepted and dealt with subject to the Standard
</td>
<td><table >
<tr>
<td><strong>FREIGHT AND CHARGES</strong><br/>
    <asp:Label ID="lblFreteTaxa" class="lblFreteTaxa" runat="server"></asp:Label></td>
</tr>
<tr>
<td><strong>PLACE AND DATE OF ISSUE</strong><br/>
     <asp:Label ID="lblOrigemPagamento" class="lblOrigemPagamento" runat="server"></asp:Label>, <asp:Label ID="lblCampoEditavel7" class="lblCampoEditavel7" runat="server"></asp:Label><br/><br/><br/><br/>
   <strong>FCA COMERCIO EXTERIOR E LOGISTICA LTDA.</strong>
</td>
</tr>
</table></td>
</tr>
</table>
        </div>

        <div style='break-after:page'></div>
         <div id="DivFixa" class="DivFixa" style="font-size:4.5px;margin-top:5px" >
             <div style="border: ridge 1px;margin: 0 auto; text-align: center; width: 100%;">
<div style="float: left; margin: 0px 2px 0px 5px; text-align: justify; width: 30%;margin-top:5px">
             <div > <div style="font-size:7px;text-align:center;margin-top:10px">FCA LOG PORT TO PORT OR MULTIMODAL BILL OF LADING</div>
                 <div style="font-size:7px;text-align:center;margin-top:10px">1. APPLICABILITY & NEGOTIABILITY</div>
In this Bill of Lading, unless the context otherwise requires:<br />
"Carrier" means Fca Comércio Exterior e Logística Ltda., a company organized under the laws of Brazil and on whose behalf this bill of lading has been issued.<br />
"Carriage" means the whole or any part of the operations and services undertaken by Carrier in respect of the Goods covered by this Bill of Lading.<br />
"Charges" includes freight, all expenses, costs, detention, demurrage and any other money obligations incurred and payable by the Merchant and all collection costs for freight and other amounts due from the Merchant including attorneys' fees and court costs.<br />
"Container" includes any container, trailer, transportable tank, lift van, flat, pallet, skid, platform and similar article of transport used to consolidate or transport goods and any ancillary or associated equipment.<br />
"Goods" means the whole or any part of the cargo provided by Merchant for Carriage and includes any Container, packing or equipment not supplied by or on behalf of Carrier.<br />
"Hague Rules" means the provisions of the International Convention for the Unification of Certain Rules relating to Bills of Lading signed at Brussels on 25 August 1924.<br />
"Hague-Visby Rules" means the Hague Rules as amended by the Protocol signed at Brussels on 23 February 1968.<br />
"MTO Law" means the Multimodal Transport Law of Brazil, Law 9.611 of 16 February of 1998 and Decree 3.411 of 12 April 2000.<br />
"Multimodal Transport" arises if the Place of Receipt and/or the Place of Delivery are indicated in the relevant boxes on the face hereof.<br />
"Port to Port Transport" arises where the Carriage called for by this Bill of Lading is not Multimodal Transport.<br />
"Merchant" includes any person who is or at any time has been or becomes the Shipper, the Consignee, the receiver of the Goods, the Holder of this Bill of Lading, any person owning or entitled to the possession of or otherwise having any interest in the Goods or this Bill of Lading, and any person acting on behalf of any of the persons aforesaid.<br />
"Rights and Defences" includes rights, defences, exemptions, limitations of liability, liberties, immunities and benefits of whatsoever nature and howsoever acquired.
"Services" means the whole or any part of the loading, packing, stuffing, transporting, carriage, unloading, unpacking, de-stuffing, storage, warehousing and handling of the Goods, any value added services and any other operations and services of whatsoever nature undertaken by or performed by or on behalf of the Carrier in relation to the Goods and related documentary, customs and information technology processes.<br />
"Shipper" means the party indicated in the "Shipper" box on the face hereof.<br />
"Shipping Unit" includes freight unit and the term "unit" as used in the Hague Rules, Hague-Visby Rules and MTO Law.<br />
"Sub-Contractors" includes the owners, charterers and operators of any Vessel, stevedores, terminal operators, forwarders, groupage operators, consolidators, warehouse operators, road, rail and air transport operators, and other independent contractors employed by or for or taking
instructions from Carrier directly or indirectly in the performance of any of Carrier's obligations hereunder, and including sub-contractors of any degree.
"US COGSA" means the Carriage of Goods by Sea Act 1936 of the United States of America. "Liabilities" includes claims, losses, damages, liabilities, fines, penalties, costs and expenses (including legal costs and expenses) of whatsoever nature and howsoever arising.<br />
"Vessel" includes the vessel named on the face hereof and any substitute vessel, feeder, lighter or other watercraft used in the performance of the Carriage, whether owned or chartered or operated or controlled by Carrier or any Sub-Contractor or any other person.<br />
References to Clauses are to clauses of these Terms and Conditions. Clause headings and sub-headings are for convenience only and do not affect the construction of these Terms and Conditions.
   
             </div>
                 <div  >  <div style="font-size:7px;text-align:center">2. INTERPRETATION AND GENERAL </div>
2.1 If any terms of this Bill of Lading are held to be invalid or unenforceable, or if they are held to be repugnant to the Hague Rules, Hague-Visby Rules, US COGSA, MTO Law or any other compulsorily applicable legislation, then such provision shall be null and void without invalidating the remaining provisions hereof. Unless otherwise specifically agreed in writing between the Merchant and the Carrier, the Terms and Conditions of this Bill of Lading supersede any prior agreements between the Merchant and Carrier.<br />
2.2 Subject to Clause 2.1, provisions in these Terms and Conditions which exempt, exclude, relieve or limit the liability of Carrier, its servants, agents or Sub-Contractors shall be operative and effective notwithstanding (i) any act, omission (whether negligent, deliberate or otherwise) of Carrier, its servants, agents or Sub-Contractors, or (ii) the circumstances or cause of any loss or damage (to which such provisions relate) be unknown or unexplained, or (iii) any other matters or causes whatsoever.<br />
2.3 Any right or remedy herein conferred on Carrier under this Bill of Lading is in addition to and without prejudice to all other rights and remedies available to it.<br />
2.4 Notwithstanding anything to the contrary herein provided, nothing herein shall be construed to contractually apply the Hague-Visby Rules to this Bill of Lading.
 
             </div>
                 <div  >  <div style="font-size:7px;text-align:center">3. CARRIER'S TARIFFS </div>
The terms and conditions of Carrier's applicable tariffs, including without limitation provisions relating to Container demurrage and detention are incorporated herein. Copies of the applicable tariffs may be obtained from Carrier upon request. In the case of inconsistency between this Bill of Lading and the applicable tariffs, the terms of this Bill of Lading shall prevail.
   
                                              


             </div>
                 <div  >  <div style="font-size:7px;text-align:center">4. SUB-CONTRACTING </div>
4.1 Carrier shall be entitled to sub-contract the whole or any part of the duties undertaken by the Carrier in this Bill of Lading in relation to the Goods or Carriage or both, directly or indirectly on any terms whatsoever consistent with any applicable law.<br />
4.2 Merchant warrants that no claim or demand shall be made against any person undertaking or performing such duties (including Carrier's servants, agents and Sub-Contractors) other than Carrier, which imposes or attempts to impose on any such person or any vessel owned or operated or controlled by any such person, any liability whatsoever in connection with the Goods or the Carriage or this Bill of Lading, whether or not arising out of negligence on the part of such person. If any such claim or demand should nevertheless be made, Merchant shall indemnify Carrier against all consequences thereof.<br />
4.3 Without prejudice to the Merchant's indemnity obligations herein, the Sub-Contractors or each of them, shall have the benefit of all Rights and Defences herein provided for the benefit of or otherwise available to Carrier as if the same were expressly made also for such person's benefit. For the foregoing purposes, Carrier contracts for itself and as agent or trustee for all the aforesaid persons. For the purpose of this Clause 4, the Vessel and all its Sub-Contractors shall be deemed to be parties to the contract evidenced by this Bill of Lading.<br />
4.4 If loss or damage to the Goods is known to have occurred during a period when the Goods were in the custody of Sub-Contractors then the Carrier shall have the benefit of any and all Rights and Defences contained in or incorporated by or compulsorily applicable to the Sub-Contractors' tariff(s) or contract(s) with the Carrier (in addition to all Rights and Defences contained in this Bill of Lading and the Carrier's tariff) and for this purpose such Rights and Defences shall be deemed to be incorporated herein, and copies are obtainable from the Carrier upon request.
   
             </div>
                 <div  >  <div style="font-size:7px;text-align:center">5. NEGOTIABILITY AND TITLE TO THE GOODS </div>
5.1 This bill of lading is not a negotiable document of title unless consigned "to order", to the order of a named person, or "to bearer".<br />
5.2 Request for substitute bills may only be made by the lawful holder of an original bill of lading who at the material time holds the full set of original bills of lading. The Carrier will only issue substitute bills of lading at its sole discretion and subject to the person making the request providing the Carrier with (i) the full set of the original bills of lading and (ii) a full indemnity issued by a first class bank acceptable to the Carrier for all and any liability and expenses arising out of the request for substitute bills.<br />
5.3 This Bill of Lading shall be prima facie evidence of the taking in charge by the Carrier of the Goods as described on the face hereof, unless a contrary indication such as ?shipper?s weight, load and count?, ?shipper-packed container?, ?said to contain? or similar expressions has been made on the face hereof. However, proof to the contrary shall not be admissible when this Bill of Lading has been negotiated or transferred to a third party acting in good faith.
   
             </div>
                 <div  >  <div style="font-size:7px;text-align:center">6. CARRIER'S LIABILITY </div>
6.1 The Carrier's liability in respect of any loss of or damage to the Goods or delay in the performance of the Services shall be determined and limited in accordance with the provisions of this clause unless:<br />
6.1.1 in the case of US Carriage, an international convention or national law (including US COGSA) compulsorily applies (US Compulsory Legislation), in which case the liability of the Carrier will be determined and limited in accordance with the provisions of such US Compulsory Legislation;<br />
6.1.2 in the case of Non US Carriage an international convention or national law applies compulsorily to any element of the Services (Non US Compulsory Legislation), in which case the liability of the Carrier in relation to that element of the Services will be determined and limited in accordance with the provisions of such Non US Compulsory Legislation;<br />
6.1.3 and US Compulsory Legislation and Non US Compulsory Legislation are hereinafter referred to as Compulsory Legislation.<br />
6.2. Liability for Goods lost or damaged where no Compulsory Legislation applies<br />
6.2.1 Port to Port Shipment. For carriage which is between the Port of loading and the Port of discharge only, the Carrier shall have no responsibility for loss or damage to the Goods until they are loaded on board the Vessel and it shall cease to have any responsibility for any loss or damage to the Goods once they have been discharged from the Vessel, and, save as is otherwise provided for in this Bill of Lading, the Liabilities incurred by Carrier for loss or damage to the Goods shall be in accordance with this Clause 6.2.<br />
6.2.2 Multimodal Transport. If the Carriage is Multimodal Transport, the Carrier undertakes to perform and/or in its own name to procure performance of the Carriage from the Place of Receipt or the Port of Loading (whichever is applicable) to the Port of Discharge or the Place of Delivery (whichever is applicable) and, save as is otherwise provided for in this Bill of Lading, the Liabilities incurred by Carrier for loss or damage to the Goods shall be in accordance with this Clause 6.2.v
6.2.3 The carrier shall not be liable for loss or damage arising or resulting from unseaworthiness unless caused by want of due diligence on the part of the Carrier to make the Vessel seaworthy and to secure that the Vessel is properly manned, equipped and supplied, and to make the holds, refrigerating and cool chambers and all other parts of the Vessel in which goods are carried fit and safe for their reception, carriage and preservation. Whenever loss or damage has resulted from unseaworthiness the burden of proving the exercise of due diligence shall be on the Carrier.<br />
6.2.4 The carrier shall not be responsible for loss or damage arising or resulting from:<br />
(a) an Act, neglect, or default of the master, mariner, pilot, or the servants of the Carrier in the navigation or in the management of the Vessel;<br />
(b) Fire;<br />
(c) Perils, dangers and accidents of the sea or other navigable waters;<br />
(d) Acts of God, force majeure, act of war or acts of public enemies, thieves, pirates, assailing thieves, hijacking;<br />
(e) Arrest or restraint of princes, rulers or people, or seizure under legal process;<br />
(f) Embargo or Quarantine restrictions;<br />
(g) Act or omission of the Merchant;<br />
(h) Strikes or lockouts or stoppage or restraint of labour from whatever cause, whether partial or general;<br />
(i) Riots and civil commotions;<br />
(j) Saving or attempting to save life or property at sea;<br />
(k) Inherent defect, quality or vice of the goods;<br />
(l) Insufficiency or defective condition of packing or marking;<br />
(m) Latent defects not discoverable by due diligence;<br />
(n) handling, loading, stowage or unloading of the Goods by the Merchant;<br />
(o) a nuclear incident;<br />
(p) breach of any of the provisions of this bill of lading by the Merchant;<br />
(q) any other cause or event without the actual fault or privity of the Carrier or which the Carrier could not avoid and the consequences of whereof it could not prevent by the exercise of reasonable diligence.<br />
6.2.5 When the Carrier establishes that, in the circumstances of the case, the loss or damage could be attributed to one or more of the causes or events specified in clause 6.2.4 it shall be presumed that it was so caused. The Merchant shall, however, be entitled to prove that the loss or damage was not, in fact, caused wholly or partly by one or more of these causes or events.<br />
6.2.6 The perils listed in clause 6.2.4 (a), (c) and (j) will only apply to the carriage of Goods by sea or inland waterways.
             </div>
                 <div  >  <div style="font-size:7px;text-align:center">7. LIMITS OF LIABILITY </div>
7.1 When the Carrier is liable for compensation in respect of any loss of or damage to the Goods, it is agreed with the Merchant that such compensation shall be calculated by reference to the value of the Goods at the place and time they are delivered to the Merchant, or at the place and time they should have been delivered. For the purpose of determining the extent of the Carrier's liability for loss of or damage to the Goods, the sound value of the Goods is presumed to be the Merchant's invoice value of the Goods plus freight, charges and insurance, if paid.<br />
7.2 The Carrier shall in no event be or become liable for any loss of or damage, whatsoever and howsoever arising, to the Goods in an amount exceeding the equivalent of 666.67 Units of Account per package or unit or 2 Units of Account per kilogram of gross weight of the Goods lost or damaged, whichever is the higher. For US Carriage, the limits shall be US$500 per Package or per the freight unit billed for Goods not packaged.<br />
7.3 Higher compensation may be claimed only when, with the consent of the Carrier, the value of the Goods declared by the Shipper prior to the commencement of the Carriage, which exceeds the limits laid down in this Clause, has been inserted on the face hereof in the space provided and extra freight or ad valorem charge is paid, in which case such declared value shall be the limit and any partial loss or damage shall be adjusted pro rata on the basis of such declared value.
   
             </div>
             </div>
              
                <div style="float: left; margin: 0px 2px 0px 0px; text-align: justify; width: 30%;margin-top:10px">
                    
7.4 The Units of Account mentioned in clause 7.2 is the Special Drawing Right (SDR) as defined by the International Monetary Fund and the amount shall be converted into national currency on the basis of the value of that currency on a date to be determined by the law of the court seized of the case.<br />
7.5 When the Goods have been packed into a Container by or on behalf of the Merchant, and when the number of packages or units packed into the Container is not enumerated on the face hereof, each Container including the entire contents thereof shall be considered as one package for the purpose of application of the Carrier's limitation of liability.
                    <div  >  <div style="font-size:7px;text-align:center">8. GENERAL PROVISIONS </div>
8.1 Limitation statutes. Nothing in this bill of lading shall operate to limit or deprive the Carrier of any statutory protection, defence, exception or limitation of liability authorised by any applicable laws, statutes or regulations of any country. The Carrier shall have the full benefit of the all laws, statutes or regulations as if it were the owner of any carrying Vessel.<br />
8.2 Application of defences, limits and exclusions of liability. The defences, limits and exclusions of liability provided for in this bill of lading shall apply in any action against the Carrier arising out in connection with this bill of lading (including loss or damage to Goods and delay) and whether the action be founded in contract, bailment, tort, breach of express or implied warranty or otherwise and even if the loss, damage or delay arose as a result of unseaworthiness, negligence, wilful misconduct or fundamental breach of contract.<br />
8.3. Insurance. Carrier will not arrange for insurance on the Goods except upon express written instructions from Merchant and then only at Merchant?s expense and presentation of a declaration of value for insurance purposes prior handing over the Goods.<br />
8.4 Delay. Carrier does not undertake that the Goods or any documents relating thereto shall arrive at the Port of Discharge, Place of Delivery or at any place at any particular time or to meet any particular market or use. In no circumstances shall the Carrier be liable for any loss or damage caused by alleged delay or any other cause whatsoever and howsoever caused. Without prejudice to the foregoing, if the Carrier is found liable for delay, liability shall be limited to the Freight applicable to the relevant stage of transport. It is hereby agreed that ad valorem arrangements pursuant to Clause 7.3 shall have no application to any claim for delay.<br />
8.5 Consequential Loss.. Carrier shall in no circumstances whatsoever be liable for any loss of profits, loss of market, loss of contract, loss of revenue or use, loss of goodwill or reputation, moral damages, third party claims or any other indirect, special or consequential loss, howsoever caused. For the avoidance of doubt, ad valorem arrangements pursuant to Clause 7.3 shall have no application in relation to consequential loss, which forms the subject matter of this Clause 8.5.<br />
8.6 Heavy Lift. The weight of a single piece or package exceeding 1,000 kilograms gross weight must be declared by the Merchant in writing before receipt by the Carrier and must be marked clearly and durably on the outside of the piece or package in letters and numbers not less than five centimeters high. In case of the Merchant's failure in the abovementioned obligation, the Carrier shall not be responsible for any loss of or damage to the Goods and the Merchant shall be responsible for loss of or damage to any property or for personal injury or death arising as a result of the Merchant's said failure and shall indemnify the Carrier against loss or liability suffered or incurred by the Carrier as a result of such failure.<br />
8.7 Notice of Claim. The Carrier shall be deemed prima facie to have delivered the Goods undamaged and in full unless notice of loss of or damage to the Goods, indicating the general nature of such loss or damage, shall have been given in writing to the Carrier or to its representative at the place of delivery before or at the time of removal of the Goods into the custody of the person entitled to delivery thereof under this bill of lading or, if the loss or damage is not apparent, within three consecutive days thereafter;<br />
8.8. Time-bar. The Carrier shall be discharged of all liability whatsoever in respect of the Goods unless suit is brought in the proper forum and written notice thereof received by the Carrier within nine months after delivery of the Goods or if the Goods are not delivered the date when the Goods should have been delivered. <br />Notwithstanding the above, where the Hague Rules or Hague Visby Rules or COGSA apply whether by incorporation in this Bill of Lading or by compulsorily applicable law, the Carrier shall be discharged of all liability whatsoever in respect
of the Goods unless such is brought in the proper forum within one year of their delivery or of the date when they should have been delivered.<br />
8.9. The Rights and Defences of Carrier provided for in this Bill of Lading shall apply in any action or claim against Carrier whether founded in contract, tort, bailment, trust or breach of express or implied warranty or otherwise and notwithstanding any negligence, unseaworthiness, deviation, non-delivery, mis-delivery (including mis-delivery arising from the delivery of the Goods without the presentation of this Bill of Lading) or any other breach by the Carrier of the contract evidenced by this Bill of Lading.

                 </div>
                     <div  >  <div style="font-size:7px;text-align:center">9. DESCRIPTION OF GOODS </div>
9.1 This Bill of Lading shall be prima facie evidence of the receipt by Carrier from the Shipper in apparent good order and condition, except as otherwise noted, of the total number of Containers or other packages or units indicated on the face hereof as "TOTAL NUMBER OF CONTAINERS OR OTHER PACKAGES OR UNITS RECEIVED BY THE CARRIER".<br />
9.2 Carrier makes no representation or acknowledgement and assumes no responsibility whatsoever as to any weight, measure, quantity, quality, content, description, marks, numbers, place of origin, value or condition of the Goods (all of which are unknown to it).<br />
9.3 Any information on the face of this Bill of Lading relating to any invoice, export or import license, documentary credit, order, contract, or like matters is included solely at the request of Merchant and is not verified by Carrier. Any such information shall not constitute any declaration of value of the Goods and shall in no way increase Carrier's liability hereunder.
             </div>
                     <div  >  <div style="font-size:7px;text-align:center">10. MERCHANT'S WARRANTIES AND IMDEMNIFICATION </div>
10.1 All persons coming within the definition of Merchant in Clause 1 shall be jointly and severally liable to the Carrier for the fulfillment of all obligations, responsibilities and warranties undertaken by the Merchant either in the Bill of Lading, or as required by law and shall remain so liable throughout the transportation and to pay Freight due under it without deduction or set off notwithstanding their having transferred this Bill of Lading and/or title to the Goods to another party.<br />
10.2 The Merchant warrants that in accepting this Bill of Lading, the Merchant agrees to be bound by all stipulations, exceptions, terms and conditions on the face and back thereof, whether written, typed, stamped or printed, as fully as if signed by Merchant.<br />
10.3 The Merchant in accepting these Terms and Conditions is either the person or the authorized agent of the person who owns or is entitled to the possession of the Goods and/or this Bill of Lading, and accepts these Terms and Conditions for itself as well as for such person and any other person who may hereafter have any interest in the Goods and/or this Bill of Lading and/or the Carriage.<br />
10.4 The Merchant warrants that the description and particulars of the Goods set out on the face hereof are furnished by the Merchant and have been checked by Merchant on receipt of this Bill of Lading, and that such particulars and all other information whether relating to the Goods or otherwise provided by Merchant are complete, accurate and true. The Merchant shall indemnify the Carrier against all Liabilities arising or resulting from in accuracies or inadequacy of such particulars.<br />
10.5 The Merchant shall comply with all applicable laws, regulations and requirements (including but not limited to any imposed at any time before or during the Carrier relating to anti-terrorism measures) of customs, port and other authorities and shall bear and pay all duties, taxes, fines, imposts, expenses and losses (including without prejudice to the generality of the foregoing, Freight for any additional Carriage undertaken) incurred or suffered by reason thereof or by reason of any illegal, incorrect or insufficient marking, numbering or addressing of the Goods.<br />
10.6 The Merchant further warrants, represents and agrees that (i) the Goods and any Container loaded by the Merchant are packed and secured in such a manner as to be handled in the ordinary course of the transportation without damage to the Goods, Vessel, Containers or other property or persons; (ii) any Goods placed by the Merchant in Containers are compatible and suitable for transportation in Containers; and (iii) if the Container is not supplied by or on behalf of the Carrier, that the Container meets all ISO and/or other applicable national or international safety standards and is fit in all respects for Carriage by the Carrier.<br />
10.7 The Merchant warrants that the Goods are lawful goods and contain no contraband or prohibited items.<br />
10.8 The Merchant shall not tender for transportation any Goods which require refrigeration or other stable temperature conditions without giving written notice of their nature and the required temperature setting of the thermostatic controls before receipt of the Goods by the Carrier.<br />
10.9 Any Container released into the care of the Merchant for packing, unpacking or any other purpose whatsoever shall be at the sole risk of the Merchant until proper redelivery to the Carrier at the time and place prescribed by the Carrier. If the Merchant fails to deliver the Container at such prescribed time and place, the Merchant shall pay the Carrier the applicable demurrage or detention charges arising therefrom. The Merchant is responsible for returning the empty Container, with interiors brushed and clean, to the point or place designated by the Carrier, his servants or agents. The Merchant shall be liable for any charges, loss or any other expenses arising therefrom. The Merchant shall be responsible for any loss and/or damage to, and any Liabilities caused or incurred by such Container whilst in its custody and/or control.<br />
10.10 The Merchant warrants that when the Goods are for Carriage to or through or from the United States of America, all information relating to the Goods is complete, accurate and true and in all respects in conformity and compliance with cargo declaration requirements of the U.S. Customs Regulations and other related laws, rules and regulations.
   
             </div>
                     <div  >  <div style="font-size:7px;text-align:center">11. CONTAINERS </div>
11.1 If a Container has not been filled, packed, stuffed or loaded by the Carrier, the Carrier shall not be liable for loss of or damage to the contents and the Merchant shall indemnify the Carrier against all Liabilities incurred by the Carrier, if such Liabilities has been caused by:<br />
(i) the manner in which any Container has been filled, packed, stuffed or loaded;<br />
(ii) the unsuitability for Carriage of the contents of any Container;<br />
(iii) if Container was not supplied by Carrier, the unsuitability, defective condition or the incorrect setting of temperature controls of the Container actually used;
(iv) if Container was supplied by Carrier, the unsuitability, defective condition or incorrect setting of temperature controls of the Container which could have been discovered upon reasonable inspection by Merchant at or prior to the time the Container was filled, packed, stuffed or loaded; or
(v) the packing in any Container of temperature controlled Goods that are not at the correct temperature for Carriage.<br />
11.2 Where the Carrier is instructed to provide a Container in the absence of a written request to the contrary, the Carrier is not under an obligation to provide a Container of any particular type of quality. The Merchant shall inspect the Container prior to stuffing and use of such Container shall be deemed to be acceptance by the Merchant of it being sound and suitable for use. Unless otherwise agreed with the Carrier, the Merchant is responsible for returning the empty Containers with clean interiors to the point or place designated by the Carrier or its agents within the time prescribed in the Carrier's applicable Tariff. In such case if the Containers are not returned within the time prescribed in the Carrier's applicable Tariff, the Merchant shall be liable for detention and demurrage at the rates specified in the Carrier's applicable Tariff together with any other losses and expenses arising from such non-return including those incurred by Carrier or its agents in seeking the return of the Containers. In the event that the Carrier assumes responsibility to return the empty Containers, the Merchant agrees to indemnify the Carrier in respect of any charges, costs and expenses of whatsoever nature incurred by the Carrier in returning the empty Containers caused by any act, omission and/or delay on the Merchant's part.<br />
11.3 The Merchant is responsible for the packing and sealing of all Merchant-packed Containers. Delivery of a Merchant-packed Container by Carrier with its original seal intact shall be deemed to be a full and complete delivery under this Bill of Lading. Carrier shall not be liable for any shortage of Goods on delivery nor for any condensation or humidity loss or damage. If a claim for shortage is made against the Carrier, the Merchant agrees to indemnify the Carrier against all and any Liabilities of whatsoever nature suffered and/or incurred in connection with such claim (including, without limitation, legal costs).<br />
11.4 It is the sole responsibility of the Merchant to ascertain the applicability of and ensure compliance in sufficient time with the requirements as to the verified gross mass ("VGM") of Containers to be loaded by the Carrier on board the Vessel contained in SOLAS Chapter VI (the "SOLAS VGM Requirements") for any Goods provided by the Merchant to the Carrier. Where the Merchant is unable, refuses, fails or is reasonably anticipated to be unable to or fail to comply in full with the SOLAS VGM Requirements or with such other reasonable requests of the Carrier in connection thereto, the Carrier may, at its sole discretion but without any obligation to do so, reject the Goods for shipment or undertake and comply with that obligation on the Merchant's behalf as the Merchant's agent at the Merchant's risk and expense. All consequences of the Merchant's inability, refusal or failure, including delay and Liabilities suffered by any party, shall be solely for the Merchant to bear and the Merchant shall keep the Carrier indemnified from the same. The Merchant represents that the Carrier is entitled to rely on the accuracy of the VGM information and to counter-sign, endorse or otherwise provide its own certified weight on behalf of the Merchant to the Sub-Contractors or any other relevant parties. The Merchant agrees that it shall indemnify and hold the Carrier harmless from any and all claims, losses, penalties or other costs resulting from any statements of the VGM provided by Merchant or its agent or contractor or by the Carrier on the Merchant's behalf.<br /><br/>
   </div>
                     <div  >  <div style="font-size:7px;text-align:center">12. INSPECTION </div>
12.1 Carrier or any person authorized by the Carrier shall be entitled, but under no obligation, to open any Container or bale, parcel, bag, bundle, crate, carton, pallet, package or other individual unit of partially or completely covered or contained cargo, whether in the Container or not, at any time without liability and notice to Merchant to inspect, examine, weigh or measure the contents thereof.<br />
12.2 If it appears at any time that the contents of the Container or any part thereof cannot safely or properly be carried or carried further, either at all, or without incurring additional expense or taking any measures in relation to the Container or its contents or any part thereof, the Carrier may at the sole risk and expense of the Merchant abandon the transportation thereof and/or take any measures and/or incur any reasonable additional expense to carry or to continue the Carriage or to store the same ashore or afloat under cover or in the open, at any place, which storage shall be deemed to constitute due delivery under this Bill of Lading. The Merchant shall indemnify the Carrier against all additional expenses resulting therefrom.<br />
12.3 The Carrier is not responsible for any damage or loss to Container or its contents resulting from inspection by customs or other authorities and Merchant shall be responsible for any expenses, costs, fines, or penalties incurred as a result of such inspection or otherwise.<br />
12.4. The Carrier in exercising the liberties contained in this Clause shall not be under any obligation to take any particular measures and shall not be liable for any loss, delay or damage howsoever arising from any action or lack of action under this Clause.
   </div>
                   

                     
            
             
             </div>
                          
<div style="float: left; margin: 0px 0px 0px 0px; text-align: justify; width: 33%;margin-top:2px">
      <div  >  <div style="font-size:7px;text-align:center">13. DANGEROUS GOODS </div>
13.1 No Goods which are or may become of a dangerous (whether or not listed in the IMDG Code), hazardous, inflammable, damaging or injurious (including radio-active materials) nature or which may become liable to cause damage to the Vessel or property or person shall be tendered by the Merchant to the Carrier for Carriage without (i) previously giving the Carrier written notice of their nature and having received the Carrier?s express consent in writing; (ii) distinctly and durably indicating and marking the Goods and the Container or other packages or units in compliance with any laws or regulations which may be applicable during the Carriage; and (iii) submitting to Carrier and the appropriate authorities all documents required by any laws or regulations or otherwise required by Carrier.<br />
13.2 Merchant warrants that all dangerous goods tendered or provided to Carrier are adequately packed in compliance with all applicable laws or regulations and requirements having regard to the nature of the Goods. If the Goods are not packed into the Container by or on behalf of Carrier, Merchant warrants that no incompatible goods are packed in the same Container.<br />
13.3 Goods which are or which, in the opinion of the Carrier, may at any time become dangerous, inflammable, radioactive or damaging may, at any time or place be unloaded, destroyed or rendered harmless by Carrier or any Sub-Contractor without compensation to the Merchant and if the Merchant has not given notice of their nature to the Carrier under Clause 13.1, the Carrier shall be under no liability to make any General Average contribution in respect of such Goods.<br />
13.4 If the requirements of Clause 13.1 are not complied with, the Merchant shall indemnify Carrier from and against any and all Liabilities of whatsoever nature arising out of the carriage of such Goods or Containers including without limitation environmental damages, direct and indirect clean up or rehabilitation expenses, legal costs, fines and penalties.<br /><br />
   </div>
                     <div  >  <div style="font-size:7px;text-align:center">14. PERISHABLE GOODS/TEMPERATURE CONTROLLED </div>
14.1 The Merchant undertakes not to tender for Carriage any Goods which require temperature control and/or specific settings in terms of humidity, CO2 levels, ventilation etc. without previously giving written notice of their nature and particular temperature range to be maintained.<br />
14.2 In case of refrigerated Containers packed by or on behalf of Merchant, the Merchant undertakes that the Goods have been properly stowed in the Container and that the Container has been properly pre-cooled and the thermostatic controls have been properly set. It is Merchant?s obligation to set and/or check that the temperature controls on the Container are at the required temperature and to properly set the vents.<br />
14.3 If the requirements of Clauses 14.1 and 14.2 are not complied with by the Merchant, the Carrier shall not be liable for any loss of or damage to the Goods caused by such non-compliance and the Merchant shall indemnify the Carrier for any resulting loss the Carrier suffers.<br />
14.4 Insofar as Carrier provides empty refrigerated Containers to Merchant for stuffing by Merchant, Carrier is not responsible for the temperature of those Containers on delivery to Merchant.<br />
14.5 Merchant acknowledges that refrigerated Containers are not designed to freeze down cargo which has not been presented for stuffing at or below its designated carrying temperature and Carrier shall not be responsible for the consequences of Goods presented at a higher temperature than that required for Carriage.
   </div>

                                           <div  >  <div style="font-size:7px;text-align:center">15. OPTIONAL STOWAGE AND DECK CARGO </div>
15.1 Goods may be packed by Carrier in any type of Containers and consolidated with other goods.<br />
15.2 Goods, whether or not packed in Containers, may be carried on deck or under deck at the sole discretion of Carrier without
notice to Merchant, and such stowage and Carriage shall not be a deviation of whatsoever nature or degree. All Goods whether
carried on deck or under deck shall participate in General Average, and (save as provided in Clause 15.3) such Goods (other than
live animals) shall be deemed to be within the definition of goods for the purposes of the Hague Rules, the Hague-Visby Rules or the
US COGSA, as the case may be.<br />
15.3 Goods which are stated on the face hereof as being carried on deck and which are so carried (and live animals whether or not
carried on deck) are carried at the sole risk of Merchant and without any responsibility on the part of Carrier for loss or damage of
whatsoever nature arising during carriage by sea, whether or not caused by unseaworthiness or negligence or any other cause
whatsoever. If the requirements of this Clause 15.3 are satisfied, the Hague Rules, Hague-Visby Rules and US COGSA shall not
apply to such Carriage.
   </div>
    <div  >  <div style="font-size:7px;text-align:center">16. NOTIFICATION AND DELIVERY </div>
16.1 Any mention in this Bill of Lading of any party to be notified of the arrival of the Goods is only for Carrier's information. Failure to give such notice shall not subject Carrier to any Liabilities nor relieve Merchant of any obligation hereunder.<br />
16.2 Merchant shall take delivery of the Goods (notwithstanding any loss or damage or any other matter whatsoever) within the time and at the address for collection provided in Carrier's applicable tariffs, or otherwise notified to Merchant or the Notify Party named on the face hereof. If Merchant fails to take delivery of the Goods within the prescribed time at the prescribed place, the Goods shall be deemed to have been duly delivered to Merchant under this Bill of Lading upon expiration of such time.<br />
16.3 If the Merchant fails or refuses to take delivery of the Goods within 30 days of delivery under clause above, or such shorter time as may be provided in any law or regulation applicable at the port of discharge, Carrier shall be entitled, but under no obligation, without further notice to Merchant, to unpack the Goods (if packed in Containers) and/or store the Goods ashore, afloat, in the open or under cover at the sole risk and expense of Merchant. Thereupon, the Carrier?s liability in respect of the Goods shall cease wholly and the costs of such storage (if paid or payable by the Carrier or any agent or Sub-contractor of the Carrier) shall forthwith upon demand be paid by the Merchant to the Carrier.<br />
16.4 If after arrival of the Goods, Carrier in accordance with the applicable custom or law hands over the Goods into the custody of any customs, port or other authority, such hand-over shall be deemed to be due delivery of the Goods to Merchant under this Bill of Lading.<br />
16.5 In the event that Carrier, in its entire discretion, agrees at the request of Merchant to deliver the Goods at a port of discharge or a place of delivery other than the Port of Discharge or Place of Delivery indicated on the face hereof, Carrier shall act only as the agent of Merchant in arranging for the delivery of the Goods to the revised port of discharge or the revised place of delivery, and shall be under no liability whatsoever arising from such revised carriage.<br />
16.6 In all circumstances, the Carrier shall not be liable whatsoever for delivering or releasing the Goods in its actual or constructive possession to any person holding or presenting forged or fraudulent documents purporting to be an original Bill of Lading or other original documents entitling such person to the delivery or possession of the Goods so long as the Carrier acts innocently and does not intentionally deliver the Goods to persons known by him to have no right to possession under the Bill of Lading.
   </div>
             <div  >  <div style="font-size:7px;text-align:center">17. LIEN </div>
17.1 Carrier shall have a lien on the Goods and any document relating thereto for all monies earned, or due or payable to Carrier under this Bill of Lading and/or any other contract with the Merchant or, on account of the Goods or Carriage, storage or handling of the Goods including but not limited to General Average contributions, Freight, delivery, destination, demurrage, detention, port and/or handling charges, to whomever due and/or for the cost of recovering the same and/or any fines or penalties levied against the Carrier by reason of any acts or omissions for which the Merchant is responsible. Carrier may at its sole discretion exercise its lien at any time and in any place, whether the contractual transportation is completed or not. The lien shall survive the delivery of the Goods.<br />
17.2 For the purpose of enforcing and satisfying the lien, the Carrier shall have the right to sell at the cost and expense of Merchant the Goods by public auction or private treaty or other means, without giving any notice or incurring any liability to Merchant and without the need to obtain an order for sale from any Court and to apply the proceeds (net of expenses) thereof in or towards satisfaction of any monies due to Carrier. The Carrier shall be entitled to claim the difference in the event that the sale proceeds fail to cover the full amount due to the Carrier.
   </div>
             
                 <div  >  <div style="font-size:7px;text-align:center">18. CHARGES </div>
18.1 The Charges have been calculated on the basis of particulars furnished by or on behalf of the Merchant and the Carrier's confirmation of the basis of the calculation shall be conclusive. The Carrier shall be entitled to production of the commercial invoice for the Goods or true copy thereof and to open the Goods or the Container to inspect, reweigh, re-measure and re-value the Goods. If the particulars are found by the Carrier to be incorrect, the Merchant shall pay the Carrier the correct Charges (credit being given for the Charges charged) together with the costs incurred by the Carrier in establishing the correct particulars.<br />
18.2 Quotations as to Freight, rates of duty, insurance premiums or other charges or fees given by Carrier are for information only and are subject to changes without notice and shall not under any circumstances be binding upon Carrier.<br />
18.3 Charges shall be deemed fully earned on receipt of the Goods by the Carrier and Merchant shall remain responsible for all Charges, regardless of whether they are stated on the face of the Bill of Lading or intended to be pre-paid or collect. All Charges shall be paid in the currency named in this Bill of Lading, or, at the option of Carrier in another currency specified by Carrier.<br />
18.4 All Charges shall be paid in full and are non-returnable in any event, regardless of the condition of Goods and any loss or damage, without any set-off, deduction or counter-claim.<br />
18.5 All Charges shall be paid at or within the time stipulated in Carrier's applicable tariffs and in any event before delivery of the Goods. Late payment fees and interest rates shall be payable on any overdue amount from the date when payment is due until payment in full. All costs and expenses incurred by or on behalf of Carrier in the recovery of any monies due from Merchant including legal costs, attorney fees, recovery or collection fees and expenses shall be recoverable from Merchant as a debt.<br />
18.6 Merchant shall be liable for all Charges, dues, duties, fines, penalties, taxes, consular fees, levies on or relating to the Goods and Merchant shall reimburse Carrier for any and all advances made by Carrier in Carrier's own discretion. Merchant shall be liable for additional or return Freight on the Goods if they are refused export or import by any government body or authority or any other person having authority.
   </div>
                 <div  >  <div style="font-size:7px;text-align:center">19. METHODS AND ROUTES OF TRANSPORTATION </div>
19.1 Carrier may at any time and without notice to Merchant: (i) use any means of carriage or storage whatsoever; (ii) transfer the Goods from one conveyance to another, including without limitation transshipping the Goods or carrying them on a Vessel other than that named on the face hereof, even though transshipment or forwarding of the Goods may not have been provided for herein; (iii) unpack and remove the Goods which have been packed in a Container and forward them in another Container or otherwise; (iv) proceed by any route (whether or not the nearest or most direct or customary or advertised route) in its discretion, at any speed, and proceed to or stay at any place or port whatsoever, once or more often and in any order; (v) load or unload the Goods at any place or port and store the Goods at any such place or port; (vi) comply with any orders, directions or recommendations given by any government or authority, or any person or body acting or purporting to act as or on behalf of such government or authority, or having under the terms of any insurance on any conveyance used for the Carriage the right to give orders or directions; (vii) permit the Vessel to proceed with or without pilots, to tow or to be towed, or to be dry-docked, with or without Goods and/or Containers on board.<br />
19.2 Carrier may invoke any of the liberties under Clause 19.1 for any purpose whatsoever, whether or not connected with the Carriage of the Goods, including but not limited to loading or unloading the Goods, bunkering, undergoing repairs, adjusting instruments, towing or being towed, sailing with or without pilots, drydocking, picking up or landing any persons. Anything
done in accordance with Clause 19.1 or any delay arising therefrom shall be deemed to be within the contractual Carriage and shall not be a deviation. If, in invoking any of the liberties under Clause 19.1, any service provided by any third party is involved, Carrier may without notice to Merchant conclude a contract with such third party as agent of the Merchant, and in respect of such services Carrier shall have no liability whatsoever.
   </div>
                 <div  >  <div style="font-size:7px;text-align:center">20. MATTERS AFFECTING PERFORMANCE </div>
If at any time the performance of the contract contained in or evidenced by this Bill of Lading is or is likely to be affected by any hindrance, risk, delay, difficulty or disadvantage of any kind whatsoever and howsoever arising and which cannot be avoided by reasonable endeavours, the Carrier may (whether or not the Carriage has commenced) and without prior notice to Merchant and at its sole discretion, elect any one or more of the following: (i) treat the performance of this contract as terminated and place the Goods or any part of them at Merchant's disposal at any place or port which Carrier may reasonably deem safe and convenient, whereupon all the responsibility of Carrier under this Bill of Lading shall cease absolutely and the Goods shall be deemed to have been duly delivered by Carrier under this Bill of Lading; or (ii) acting as a Merchant?s agent only, suspend the Carriage of the Goods and store them ashore or afloat at Merchant?s expense upon the terms of this Bill of Lading and use reasonable endeavours to forward them as soon as practicable to the Port of Discharge or Place of Delivery; or (iii) carry the Goods to the contracted Port of Discharge or Place of Delivery, whichever is applicable, by an alternative route by any means in the sole discretion of Carrier.
   </div>
                 <div  >  <div style="font-size:7px;text-align:center">21. GENERAL AVERAGE </div>
General Average to be adjusted in any currency at any place selected by the Carrier and according to the York-Antwerp Rules 1974 as amended in 1990 and 1994. Any claims and/or disputes relating to General Average shall be exclusive subject to the laws and jurisdiction set out at Clause 28. Merchant shall indemnify Carrier from and against any claim of a General Average nature which may be made on Carrier, and shall provide to Carrier prior to delivery of the Goods such cash deposit or security as Carrier may consider sufficient to cover the estimated General Average contribution of the Goods and any salvage and special charges thereon. Carrier shall be under no obligation to exercise any lien or collect or procure any security for General Average contribution due to Merchant.
   </div>
                 <div  >  <div style="font-size:7px;text-align:center">22. BOTH TO BLAME COLLISION AND NEW JASON </div>
The Both-to-Blame Collision and New Jason clauses published and/or approved by BIMCO and obtainable from Carrier or its agent upon request are incorporated herein.
   </div>
                 <div  >  <div style="font-size:7px;text-align:center">23. NOTICE OF ENDORSEE AND/OR HOLDER AND/OR </div>
By taking up this Bill of Lading, whether by endorsement and/or becoming a holder and/or by transfer hereof and/or by presenting this Bill of Lading to obtain delivery of the Goods herein and/or otherwise, the endorsee, holder, transferee and the Carrier agree that the holder, endorsee, transferee thereupon become a party to a contract of carriage with the Carrier on the basis herein.
   </div>
                 <div  >  <div style="font-size:7px;text-align:center">24. VARIATION OF CONTRACT </div>
Merchant agrees that this Bill of Lading constitutes the entire agreement between the parties. No servant or agent of the Carrier shall have power to waive or vary any of the terms hereof unless such waiver or variation is in writing and is specifically authorized by the Carrier.
   </div>
                 <div  >  <div style="font-size:7px;text-align:center">25. APPLICABLE LAW AND JURISDICTION </div>
The contract evidenced by or contained in this Bill of Lading, and the rights and obligations of all parties concerned in connection with the Carriage of the Goods hereunder shall be governed by and construed in accordance with the laws of the Federative Republic of Brazil. Any claims, suits, proceedings or disputes howsoever arising in connection with this Bill of Lading and/or the contract contained or evidenced by this Bill of Lading against Carrier shall be determined exclusively by the Civil Courts of Santos, in the State of Sao Paulo, Brazil to which jurisdiction Merchant irrevocably submits. Carrier shall be entitled to bring any claim, suit, proceeding or dispute against Merchant in the Civil Courts of Santos, in the State of Sao Paulo, Brazil or any other Court of competent jurisdiction.
         </div>
                 </div>


                    </div>
          </div>
</div>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">
    <script>

      
        function Imprimir() {
            var Cliente = document.getElementById('<%= txtCliente.ClientID %>').value;
            var Importador1 = document.getElementById('<%= txtImportador1.ClientID %>').value;
            var Importador2 = document.getElementById('<%= txtImportador2.ClientID %>').value;
            var CampoEditavel = document.getElementById('<%= txtCampoEditavel.ClientID %>').value;
            var Importador3 = document.getElementById('<%= txtImportador3.ClientID %>').value;
            var Viagem = document.getElementById('<%= txtViagem.ClientID %>').value;
            var Navio = document.getElementById('<%= txtNavio.ClientID %>').value;
            var CampoEditavel1 = document.getElementById('<%= txtCampoEditavel1.ClientID %>').value;
            var TipoPagamento = document.getElementById('<%= txtTipoPagamento.ClientID %>').value;
            var Origem = document.getElementById('<%= txtOrigem.ClientID %>').value;
            var CampoEditavel2 = document.getElementById('<%= txtCampoEditavel2.ClientID %>').value;
            var Destino = document.getElementById('<%= txtDestino.ClientID %>').value;
            var CampoEditavel3 = document.getElementById('<%= txtCampoEditavel3.ClientID %>').value;
            var Container = document.getElementById('<%= txtContainer.ClientID %>').value;
            var QtdVolumes = document.getElementById('<%= txtQtdVolumes.ClientID %>').value;
            var CampoEditavel4 = document.getElementById('<%= txtCampoEditavel4.ClientID %>').value;
            var CampoEditavel5 = document.getElementById('<%= txtCampoEditavel5.ClientID %>').value;
            var Moeda = document.getElementById('<%= txtMoeda.ClientID %>').value;
			var Frete = document.getElementById('<%= txtFrete.ClientID %>').value;
            var OrigemPagamento = document.getElementById('<%= txtOrigemPagamento.ClientID %>').value;
            var PesoBruto = document.getElementById('<%= txtPesoBruto.ClientID %>').value;
            var PesoLiquido = document.getElementById('<%= txtPesoLiquido.ClientID %>').value;
            var M3 = document.getElementById('<%= txtM3.ClientID %>').value;
            var Agente = document.getElementById('<%= txtAgente.ClientID %>').value;
			var CampoEditavel6 = document.getElementById('<%= txtCampoEditavel6.ClientID %>').value;
            var CPF = document.getElementById('<%= txtCPF.ClientID %>').value;
            var Impressao = document.getElementById('<%= txtImpressao.ClientID %>').value;
            var CampoEditavel7 = document.getElementById('<%= txtCampoEditavel7.ClientID %>').value;
            var FreteTaxa = document.getElementById('<%= txtFreteTaxa.ClientID %>').value;

            Container = Container.replace(/\n/g, "<br/>");
            FreteTaxa = FreteTaxa.replace(/\n/g, "<br/>");
            Cliente = Cliente.replace(/\n/g, "<br/>");
            Importador1 = Importador1.replace(/\n/g, "<br/>");
            Importador2 = Importador2.replace(/\n/g, "<br/>");
            Importador3 = Importador3.replace(/\n/g, "<br/>");
            CampoEditavel5 = CampoEditavel5.replace(/\n/g, "<br/>");
            CampoEditavel4 = CampoEditavel4.replace(/\n/g, "<br/>");
            CampoEditavel = CampoEditavel.replace(/\n/g, "<br/>");
            QtdVolumes = QtdVolumes.replace(/\n/g, "<br/>");


            document.getElementById('<%= lblCliente.ClientID %>').innerHTML = Cliente;
            document.getElementById('<%= lblImportador1.ClientID %>').innerHTML = Importador1;
<%--            document.getElementById('<%= lblImportador2.ClientID %>').innerHTML = Importador2;--%>
            document.getElementById('<%= lblCampoEditavel.ClientID %>').innerHTML = CampoEditavel;
            document.getElementById('<%= lblImportador3.ClientID %>').innerHTML = Importador3;
            document.getElementById('<%= lblViagem.ClientID %>').innerHTML = Viagem;
            document.getElementById('<%= lblNavio.ClientID %>').innerHTML = Navio;
            document.getElementById('<%= lblCampoEditavel1.ClientID %>').innerHTML = CampoEditavel1;
            document.getElementById('<%= lblTipoPagamento.ClientID %>').innerHTML = TipoPagamento;
            document.getElementById('<%= lblOrigem.ClientID %>').innerHTML = Origem;
            document.getElementById('<%= lblCampoEditavel2.ClientID %>').innerHTML = CampoEditavel2;
            document.getElementById('<%= lblDestino.ClientID %>').innerHTML = Destino;
            document.getElementById('<%= lblContainer.ClientID %>').innerHTML = Container;
            document.getElementById('<%= lblQtdVolumes.ClientID %>').innerHTML = QtdVolumes;
<%--            document.getElementById('<%= lblCampoEditavel4.ClientID %>').innerHTML = CampoEditavel4;--%>
            document.getElementById('<%= lblCampoEditavel5.ClientID %>').innerHTML = CampoEditavel5;
<%--            document.getElementById('<%= lblMoeda.ClientID %>').innerHTML = Moeda;--%>
<%--            document.getElementById('<%= lblFrete.ClientID %>').innerHTML = Frete;--%>
            document.getElementById('<%= lblOrigemPagamento.ClientID %>').innerHTML = OrigemPagamento;
            document.getElementById('<%= lblPesoBruto.ClientID %>').innerHTML = PesoBruto;
            document.getElementById('<%= lblPesoLiquido.ClientID %>').innerHTML = PesoLiquido;
            document.getElementById('<%= lblM3.ClientID %>').innerHTML = M3;
<%--            document.getElementById('<%= lblAgente.ClientID %>').innerHTML = Agente;
            document.getElementById('<%= lblCampoEditavel6.ClientID %>').innerHTML = CampoEditavel6;
            document.getElementById('<%= lblCPF.ClientID %>').innerHTML = CPF;--%>
            document.getElementById('<%= lblImpressao.ClientID %>').innerHTML = Impressao;
            document.getElementById('<%= lblCampoEditavel7.ClientID %>').innerHTML = CampoEditavel7;
            document.getElementById('<%= lblFreteTaxa.ClientID %>').innerHTML = FreteTaxa;

            window.print();
        }
        
    </script>
</asp:Content>
