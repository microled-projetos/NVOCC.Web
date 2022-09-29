<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="VisualizarUpload.aspx.vb" Inherits="NVOCC.Web.VisualizarUpload" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
             <% If Not String.IsNullOrEmpty(imagemBase64Retorno) Then %>
                            <img id="wrapper" src="<%= String.Format("data:image/gif;base64,{0}", imagemBase64Retorno)  %>" alt="Alternate Text" style="width: 100%;" />
                            <% End if %>

        </div>
    </form>
</body>
</html>
