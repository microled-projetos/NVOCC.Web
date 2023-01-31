<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="ABAINFRA.Web.HomePage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="main">
       
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">
    <script>
        $(document).ready(function () {
            HomePage();
        });


        function HomePage() {
            $.ajax({
                type: "POST",
                url: "DemurrageService.asmx/getHomePage", 
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (dado) {
                    var dado = dado.d;
                    dado = $.parseJSON(dado);
                    if (dado != null) {
                        result = "<iframe title='Embarques - Operacional' src='" + dado[0]["DS_LINK_ACESSO"] + "' frameborder='0' allowFullScreen='true' style='width: 100%; height: calc(97vh - 60px - 20px);'></iframe>";
                        $("#main").append(result);
                    }
                }
            })
        }
    </script>
</asp:Content>
