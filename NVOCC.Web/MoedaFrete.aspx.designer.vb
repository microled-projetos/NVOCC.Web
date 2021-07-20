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


Partial Public Class MoedaFrete

    '''<summary>
    '''Controle Validacoes.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents Validacoes As Global.System.Web.UI.WebControls.ValidationSummary

    '''<summary>
    '''Controle divmsg.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents divmsg As Global.System.Web.UI.HtmlControls.HtmlGenericControl

    '''<summary>
    '''Controle divErro.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents divErro As Global.System.Web.UI.HtmlControls.HtmlGenericControl

    '''<summary>
    '''Controle lblErro.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents lblErro As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Controle txtIDMoedaFrete.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents txtIDMoedaFrete As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Controle txtTxOficial.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents txtTxOficial As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Controle txtDataCambio.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents txtDataCambio As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Controle ddlMoeda.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents ddlMoeda As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Controle btnLimpar.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents btnLimpar As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Controle btnGravar.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents btnGravar As Global.System.Web.UI.WebControls.Button

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
    '''Controle Label1.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents Label1 As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Controle ddlConsultas.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents ddlConsultas As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Controle txtPesquisa.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents txtPesquisa As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Controle btnPesquisa.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents btnPesquisa As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Controle divInfo.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents divInfo As Global.System.Web.UI.HtmlControls.HtmlGenericControl

    '''<summary>
    '''Controle lblInfo.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents lblInfo As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Controle dgvMoedaFrete.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents dgvMoedaFrete As Global.System.Web.UI.WebControls.GridView

    '''<summary>
    '''Controle dsMoedaFrete.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents dsMoedaFrete As Global.System.Web.UI.WebControls.SqlDataSource

    '''<summary>
    '''Controle dsMoeda.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents dsMoeda As Global.System.Web.UI.WebControls.SqlDataSource
End Class
