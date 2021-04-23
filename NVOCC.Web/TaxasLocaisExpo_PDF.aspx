<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="TaxasLocaisExpo_PDF.aspx.vb" Inherits="NVOCC.Web.TaxasLocaisExpo_PDF" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
 <title>Taxas Locais Exportação</title>
 <style>
        .teste{
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
    .titulo{
        background-color:#bddea0;
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