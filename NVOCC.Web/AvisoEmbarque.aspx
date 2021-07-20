<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="AvisoEmbarque.aspx.vb" Inherits="NVOCC.Web.AvisoEmbarque" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
        <style>
        #DivImpressao, #imgFundo {
            display: block;
        }

            #imgFundo {
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
                font-size:8px !important;
            }
        }
    </style> 
    <div id="DivImpressao">
        <div style="display:none">
    <asp:Label ID="lblIDINVOICE"  runat="server"/>
        <asp:Label ID="lblID_BL"  runat="server"/>
        <asp:Label ID="lblID_BL_MASTER"  runat="server"/>
        <asp:Label ID="lblGrau"  runat="server"/>
        </div>
        <h5> <asp:Label ID="lblAgente"  runat="server"/></h5>
         <br /><br /><br /> 
        <asp:GridView ID="dgvBLInvoiceMBL" DataKeyNames="ID_BL" DataSourceID="dsBLInvoice" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" EmptyDataText="Nenhum registro encontrado." Visible="false">
                                            <Columns>
                                                <asp:TemplateField HeaderText="ID" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID_BL") %>'  />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                              <asp:BoundField DataField="NR_INVOICE" HeaderText="INVOICE" SortExpression="NR_INVOICE" />
                                                <asp:BoundField DataField="NR_PROCESSO" HeaderText="PROCESSO" SortExpression="NR_PROCESSO" />
                                                <asp:BoundField DataField="HBL" HeaderText="HBL" SortExpression="HBL" />
                                                <asp:BoundField DataField="MBL" HeaderText="MBL" SortExpression="MBL" />
                                                <asp:BoundField DataField="PARCEIRO_CLIENTE" HeaderText="CLIENTE" SortExpression="PARCEIRO_CLIENTE" />     
                                                <asp:BoundField DataField="ORIGEM" HeaderText="ORIGEM" SortExpression="ORIGEM" />
                                                 <asp:BoundField DataField="DESTINO" HeaderText="DESTINO" SortExpression="DESTINO" />
                                                                                                <asp:BoundField DataField="PARCEIRO_TRANSPORTADOR" HeaderText="TRANSPORTADOR" SortExpression="PARCEIRO_TRANSPORTADOR" />     

                                                 <asp:BoundField DataField="DT_PREVISAO_EMBARQUE" HeaderText="PREVISAO DE EMBARQUE" SortExpression="DT_PREVISAO_EMBARQUE" />
                                                 <asp:BoundField DataField="DT_EMBARQUE" HeaderText="EMBARQUE" SortExpression="DT_EMBARQUE" />
                                                 <asp:BoundField DataField="DT_PREVISAO_CHEGADA" HeaderText="PREVISAO DE CHEGADA" SortExpression="DT_PREVISAO_CHEGADA" />
                                                 <asp:BoundField DataField="DT_CHEGADA" HeaderText="CHEGADA" SortExpression="DT_CHEGADA" />
                                       
                                            </Columns>
                                            <HeaderStyle CssClass="headerStyle" />
                                        </asp:GridView>
                <asp:GridView ID="dgvBLInvoiceHBL" DataKeyNames="ID_BL" DataSourceID="dsBLInvoice" CssClass="table table-hover table-sm grdViewTable" GridLines="None" CellSpacing="-1" runat="server" AutoGenerateColumns="false" Style="max-height: 400px; overflow: auto;" AllowSorting="true" EmptyDataText="Nenhum registro encontrado." Visible="false">
                                            <Columns>
                                                <asp:TemplateField HeaderText="ID" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID_BL") %>'  />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                                                                <asp:BoundField DataField="NR_INVOICE" HeaderText="INVOICE" SortExpression="NR_INVOICE" />

                                                <asp:BoundField DataField="NR_PROCESSO" HeaderText="PROCESSO" SortExpression="NR_PROCESSO" />
                                                <asp:BoundField DataField="HBL" HeaderText="HBL" SortExpression="HBL" />
                                                <asp:BoundField DataField="MBL" HeaderText="MBL" SortExpression="MBL" />
                                                <asp:BoundField DataField="PARCEIRO_CLIENTE" HeaderText="CLIENTE" SortExpression="PARCEIRO_CLIENTE" />     
                                                <asp:BoundField DataField="ORIGEM" HeaderText="ORIGEM" SortExpression="ORIGEM" />
                                                 <asp:BoundField DataField="DESTINO" HeaderText="DESTINO" SortExpression="DESTINO" />
                                                                                                <asp:BoundField DataField="PARCEIRO_TRANSPORTADOR" HeaderText="TRANSPORTADOR" SortExpression="PARCEIRO_TRANSPORTADOR" />     

                                                 <asp:BoundField DataField="DT_PREVISAO_EMBARQUE_MASTER" HeaderText="PREVISAO DE EMBARQUE" SortExpression="DT_PREVISAO_EMBARQUE_MASTER" />
                                                 <asp:BoundField DataField="DT_EMBARQUE_MASTER" HeaderText="EMBARQUE" SortExpression="DT_EMBARQUE_MASTER" />
                                                 <asp:BoundField DataField="DT_PREVISAO_CHEGADA_MASTER" HeaderText="PREVISAO DE CHEGADA" SortExpression="DT_PREVISAO_CHEGADA_MASTER" />
                                                 <asp:BoundField DataField="DT_CHEGADA_MASTER" HeaderText="CHEGADA" SortExpression="DT_CHEGADA_MASTER" />
                                       
                                            </Columns>
                                            <HeaderStyle CssClass="headerStyle" />
                                        </asp:GridView>

        </div>
     <asp:SqlDataSource ID="dsBLInvoice" runat="server" ConnectionString="<%$ ConnectionStrings:NVOCC %>"
        SelectCommand="">
    </asp:SqlDataSource>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">
    <script>
        $(window).load(function () {
            window.print();
        });
    </script>
</asp:Content>