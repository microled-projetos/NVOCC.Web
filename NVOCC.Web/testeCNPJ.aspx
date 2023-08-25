<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="testeCNPJ.aspx.cs" Inherits="ABAINFRA.Web.testeCNPJ" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <input type="file" id="excelFileInput" accept=".xlsx">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/xlsx/0.13.5/xlsx.full.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/xlsx/0.13.5/jszip.js"></script>
    <script src="Content/js/papaparse.min.js"></script>    
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jspdf/1.5.3/jspdf.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/html2pdf.js/0.9.2/html2pdf.bundle.js"></script>

    <script>
        document.getElementById('excelFileInput').addEventListener('change', handleFile, false);

        function handleFile(e) {
            const file = e.target.files[0];
            const reader = new FileReader();
            reader.onload = function (e) {
                const data = new Uint8Array(e.target.result);
                const workbook = XLSX.read(data, { type: 'array' });
                const worksheet = workbook.Sheets[workbook.SheetNames[0]];
                const jsonData = XLSX.utils.sheet_to_json(worksheet, { header: 1 });
                jsonData.map(async (e) => {
                    
                });
            };
            reader.readAsArrayBuffer(file);
        }
    </script>
</asp:Content>