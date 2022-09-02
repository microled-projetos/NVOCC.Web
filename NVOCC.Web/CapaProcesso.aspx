<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="CapaProcesso.aspx.vb" Inherits="NVOCC.Web.CapaProcesso" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        #imgFundo { 
display:none; 

}
  
        @media print {
       
        @page{
        }
     #imgFundo { 
display:none; 

}
   th{
       font-size: 10px
   }
     
        }
        
    </style>
    <div id="DivImpressao" class="DivImpressao" style="font-size: 10px">
        <div id="divFCL" class="divFCL" visible="false" runat="server">
            <table>
                <tr>

                    <td style="padding-bottom:20px">
                        <center>
    <img src="Content/imagens/Logo_FCA_Deitado1.png" /><br />
    <strong>FCA COMERCIO EXTERIOR E LOGISTICA LTDA.</strong>&nbsp;<br /></center>

                    </td>
                    <td style="padding-bottom:20px">
                        <strong>Nº do Processo:</strong>&nbsp;<asp:Label ID="lblProcesso_FCL"  runat="server"/>
                    </td>
                </tr> </table> <br /><br /><br />
            <table>
                <tr>
                    <td>
                        <strong>Consignee:</strong>&nbsp;<asp:Label ID="lblImportador_FCL"  runat="server"/>
                        <br />
                    </td>
                     <tr> </tr>
                    <td>
                        <strong>Ref. CNEE:</strong>&nbsp;<asp:Label ID="lblRefCliente_FCL"  runat="server"/>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <strong>Navio/Viagem:</strong>&nbsp;<asp:Label ID="lblNavioViagem_FCL"  runat="server"/>
                        <br />
                    </td>
                    
                </tr>
                <tr>
                    <td>
                        <strong>ETD:</strong>&nbsp;<asp:Label ID="lblDataEmbarque_FCL"  runat="server"/>
                        <br />
                    </td>
                    <td>
                        <strong>ETA:</strong>&nbsp;<asp:Label ID="lblDataChegada_FCL"  runat="server"/>
                        <br />
                    </td>
                    </tr>
                <tr>
                    <td>
                        <strong>Navio Transbordo:</strong>&nbsp;<asp:Label ID="lblNavioTransb_FCL"  runat="server"/>
                        <br />
                    </td>                   
                </tr>  
                <tr>
                     <td>
                        <strong>Porto:</strong>&nbsp;<asp:Label ID="lblPorto_FCL"  runat="server"/>
                        <br />
                    </td>
                    <td>
                       <strong>ETD:</strong>&nbsp;<asp:Label ID="lblDataTransb_FCL"  runat="server"/>
                        <br />
                    </td>
                </tr>
                <tr>                   
                    <td>
                        <strong>Agente:</strong>&nbsp;<asp:Label ID="lblAgente_FCL"  runat="server"/>
                    </td>
                </tr>
 
                <tr>
                    <td>
                        <strong>Origem:</strong>&nbsp;<asp:Label ID="lblOrigem_FCL"  runat="server"/>
                        <br />
                    </td>
                    <td>
                        <strong>Destino:</strong>&nbsp;<asp:Label ID="lblDestino_FCL"  runat="server"/>
                        <br />
                    </td>
                </tr>
                <tr>                   
                    <td>
                        <strong>MBL Nº:</strong>&nbsp;<asp:Label ID="lblMaster_FCL"  runat="server"/>
                        <br />
                    </td>
                    <td>
                        <strong>HBL Nº:</strong>&nbsp;<asp:Label ID="lblHouse_FCL"  runat="server"/>
                        <br />
                    </td>
                    </tr>
                <tr>
                    <td>
                        <strong>Modalidade:</strong>&nbsp;<asp:Label ID="lblEstufagem_FCL"  runat="server"/><asp:Label ID="lblServico_FCL"  runat="server"/>
                        <br />
                    </td>
                     <td>
                        <strong>Peso Bruto:</strong>&nbsp;<asp:Label ID="lblPesoBruto_FCL"  runat="server"/><asp:Label ID="Label2"  runat="server"/>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <strong>Qtd. Volumes:</strong>&nbsp;<asp:Label ID="lblQtdVolume_FCL"  runat="server"/><asp:Label ID="Label4"  runat="server"/>
                        <br />
                    </td>
                     <td>
                        <strong>Desc. Mercadoria:</strong>&nbsp;<asp:Label ID="lblDescMercadoria_FCL"  runat="server"/><asp:Label ID="Label6"  runat="server"/>
                        <br />
                    </td>
                </tr>
            </table>
        </div>
                                <div id="divLCL" class="divLCL" visible="false" runat="server">
                                     <table>
                <tr>

                    <td style="padding-bottom:20px">
                        <center>
    <img src="Content/imagens/Logo_FCA_Deitado1.png" /><br />
    <strong>FCA COMERCIO EXTERIOR E LOGISTICA LTDA.</strong>&nbsp;<br /></center>

                    </td>
                    <td style="padding-bottom:20px">
                        <strong>Nº WEEK:</strong>&nbsp;<asp:Label ID="lblWEEK_LCL"  runat="server"/>
                    </td>
                </tr> </table> <br /><br /><br />
            <table>                
                <tr>
                    <td>
                        <strong>Navio/Viagem:</strong>&nbsp;<asp:Label ID="lblNavioViagem_LCL"  runat="server"/>
                        <br />
                    </td>
                    </tr>
                <tr>
                    <td>
                        <strong>ETD:</strong>&nbsp;<asp:Label ID="lblDataEmbarque_LCL"  runat="server"/>
                        <br />
                    </td>
                     <td>
                        <strong>ETA:</strong>&nbsp;<asp:Label ID="lblDataChegada_LCL"  runat="server"/>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <strong>Origem:</strong>&nbsp;<asp:Label ID="lblOrigem_LCL"  runat="server"/>
                        <br />
                    </td>
                    <td>
                        <strong>Destino:</strong>&nbsp;<asp:Label ID="lblDestino_LCL"  runat="server"/>
                        <br />
                    </td>
                    
                </tr>
                   <tr>
                <td>
                        <strong>Agente:</strong>&nbsp;<asp:Label ID="lblAgente_LCL"  runat="server"/>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <strong>MBL Nº:</strong>&nbsp;<asp:Label ID="lblMaster_LCL"  runat="server"/>
                        <br />
                    </td>
                    <td>
                        <strong>Modalidade:</strong>&nbsp;<asp:Label ID="lblEstufagem_LCL"  runat="server"/><asp:Label ID="lblServico_LCL"  runat="server"/>
                        <br />
                    </td>
                     </tr>
                <tr>
                    <td>
                        <strong>Armador:</strong>&nbsp;<asp:Label ID="lblArmador_LCL"  runat="server"/>
                        <br />
                    </td>
                </tr>
            </table>
                                  <br /> <br /> <br />   <div id="divConteudoDinamico" runat="server">
        </div>  
                           </div>
       </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">
    <script>
        $(window).load(function () {
            window.print();
        });
    </script>
</asp:Content>
