<%@ Page Title="FCA-Log - Agenciamento" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.vb" Inherits="NVOCC.Web._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        table {
    border: 1px solid black;
}
th {
    border: 1px solid black;
    padding: 5px;
    background-color:#ffb914;
    color: black;
}
td {
    border: 1px solid black;
    padding: 5px;
}
    </style>    
    <div runat="server" visible="false">

<input type="button" id="btnExport" value=" Export Table data into Excel " />

<div id="dvData">
    <br/><br /><br />

        <table>
        <tr>
            <th>Column One</th>
            <th>Column Two</th>
            <th></th>
            <th>Column One</th>
            <th>Column Two</th>
            <th></th>
            <th>Column One</th>
            <th>Column Two</th>
            <th></th>
            <th>Column One</th>
            <th>Column Two</th>

        </tr>
        <tr>
            <td>row1 Col1</td>
            <td>row1 Col2</td>
            <td></td>
            <td>row1 Col1</td>
            <td>row1 Col2</td>   
            <td></td>
            <td>row1 Col1</td>
            <td>row1 Col2</td>   
            <td></td>
            <td>row1 Col1</td>
            <td>row1 Col2</td>
        </tr>
        <tr>
            <td>row2 Col1</td>
            <td>row2 Col2</td>
            <td></td>
            <td>row2 Col1</td>
            <td>row2 Col2</td>
            <td></td>
            <td>row2 Col1</td>
            <td>row2 Col2</td>
            <td></td>
            <td>row2 Col1</td>
            <td>row2 Col2</td>
        </tr>
        <tr>
            <td>row3 Col1</td>
            <td>row3 Col2</td>
            <td></td>
            <td>row3 Col1</td>
            <td>row3 Col2</td>
            <td></td>
            <td>row3 Col1</td>
            <td>row3 Col2</td>  
            <td></td>
            <td>row3 Col1</td>
            <td>row3 Col2</td>
        </tr>

       
    </table>
     <br/><br /><br />

        <table>
        <tr>
            <th>Column One</th>
            <th>Column Two</th>
            <th></th>
            <th>Column One</th>
            <th>Column Two</th>
            <th></th>
            <th>Column One</th>
            <th>Column Two</th>
            <th></th>
            <th>Column One</th>
            <th>Column Two</th>

        </tr>
        <tr>
            <td>row1 Col1</td>
            <td>row1 Col2</td>
            <td></td>
            <td>row1 Col1</td>
            <td>row1 Col2</td>   
            <td></td>
            <td>row1 Col1</td>
            <td>row1 Col2</td>   
            <td></td>
            <td>row1 Col1</td>
            <td>row1 Col2</td>
        </tr>
        <tr>
            <td>row2 Col1</td>
            <td>row2 Col2</td>
            <td></td>
            <td>row2 Col1</td>
            <td>row2 Col2</td>
            <td></td>
            <td>row2 Col1</td>
            <td>row2 Col2</td>
            <td></td>
            <td>row2 Col1</td>
            <td>row2 Col2</td>
        </tr>
        <tr>
            <td>row3 Col1</td>
            <td>row3 Col2</td>
            <td></td>
            <td>row3 Col1</td>
            <td>row3 Col2</td>
            <td></td>
            <td>row3 Col1</td>
            <td>row3 Col2</td>  
            <td></td>
            <td>row3 Col1</td>
            <td>row3 Col2</td>
        </tr>

       
    </table>
</div>
</div>

        <script src="Content/js/jquery.min.js"></script>

    <script>
        $("#btnExport").click(function (e) {
            window.open('data:application/vnd.ms-excel,' + $('#dvData').html());
            e.preventDefault();
        });
    </script>
</asp:Content>
