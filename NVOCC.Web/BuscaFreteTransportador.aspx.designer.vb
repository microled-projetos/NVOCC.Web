'------------------------------------------------------------------------------
' <gerado automaticamente>
'     Este código foi gerado por uma ferramenta.
'
'     As alterações ao arquivo poderão causar comportamento incorreto e serão perdidas se
'     o código for recriado
' </gerado automaticamente>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On


Partial Public Class BuscaFreteTransportador

    '''<summary>
    '''Controle updPainel1.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents updPainel1 As Global.System.Web.UI.UpdatePanel

    '''<summary>
    '''Controle divPesquisa.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents divPesquisa As Global.System.Web.UI.HtmlControls.HtmlGenericControl

    '''<summary>
    '''Controle ddlConsultas.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents ddlConsultas As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Controle ocean.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents ocean As Global.System.Web.UI.HtmlControls.HtmlGenericControl

    '''<summary>
    '''Controle ddlDestinoOcena.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents ddlDestinoOcena As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Controle ddlTransportadorOcean.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents ddlTransportadorOcean As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Controle txtDataInicial.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents txtDataInicial As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Controle txtDataFinal.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents txtDataFinal As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Controle bntPesquisarOcean.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents bntPesquisarOcean As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Controle locais.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents locais As Global.System.Web.UI.HtmlControls.HtmlGenericControl

    '''<summary>
    '''Controle ddlDestinoLocais.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents ddlDestinoLocais As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Controle ddlTransportadorLocais.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents ddlTransportadorLocais As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Controle bntPesquisarLocais.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents bntPesquisarLocais As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Controle divsuccess.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents divsuccess As Global.System.Web.UI.HtmlControls.HtmlGenericControl

    '''<summary>
    '''Controle lblmsgSuccess.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents lblmsgSuccess As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Controle diverro.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents diverro As Global.System.Web.UI.HtmlControls.HtmlGenericControl

    '''<summary>
    '''Controle lblmsgErro.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents lblmsgErro As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Controle DivGrid.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents DivGrid As Global.System.Web.UI.HtmlControls.HtmlGenericControl

    '''<summary>
    '''Controle dgvTaxas.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents dgvTaxas As Global.System.Web.UI.WebControls.GridView

    '''<summary>
    '''Controle dsParceiros.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents dsParceiros As Global.System.Web.UI.WebControls.SqlDataSource

    '''<summary>
    '''Controle dsPorto.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents dsPorto As Global.System.Web.UI.WebControls.SqlDataSource

    '''<summary>
    '''Controle dsTransportador.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents dsTransportador As Global.System.Web.UI.WebControls.SqlDataSource

    '''<summary>
    '''Controle dsContainer.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents dsContainer As Global.System.Web.UI.WebControls.SqlDataSource

    '''<summary>
    '''Controle dsTaxas.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents dsTaxas As Global.System.Web.UI.WebControls.SqlDataSource
End Class
