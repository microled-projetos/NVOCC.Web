<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="TaxasLocaisImpo_PDF.aspx.vb" Inherits="NVOCC.Web.TaxasLocaisImpo_PDF" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
 <title>Taxas Locais Importação</title>
    <style>
        .porto{
         float:left;
         margin: 20px;
        }
        .subtotal{
            width:250px;
        }
        body{
            font-size:12px;
			display:none;
        }
		
		@media print {
    body { display:block;
	    }

		}
    </style>
     <script>
        window.onload = function () {
            window.print();
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div class="col-lg-12 col-md-12 col-sm-12">
        <div id="divConteudoDinamico" style="width:595px;height:842px" runat="server">
        </div>         
</div>
    </form>
</body>
</html>