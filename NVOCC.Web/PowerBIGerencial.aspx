<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PowerBIGerencial.aspx.cs" Inherits="ABAINFRA.Web.PowerBIGerencial" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row principal">
        <div class="col-lg-12 col-md-12 col-sm-12">
            <br />
            <iframe title="Relatorio FCA" width="1140" height="541.25" src=https://app.powerbi.com/reportEmbed?reportId=2b296c23-e3ca-47f5-9a7b-32bab89ba8df&autoAuth=true&ctid=33bbba68-20ed-48f6-a2c0-6d76b197357c frameborder="0" allowFullScreen="true" style="width: 100%; height: calc(97vh - 60px - 20px);"></iframe>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
