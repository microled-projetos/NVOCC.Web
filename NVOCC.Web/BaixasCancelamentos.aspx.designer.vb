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


Partial Public Class BaixasCancelamentos

    '''<summary>
    '''Controle lblTipo.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents lblTipo As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Controle UpdatePanel5.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents UpdatePanel5 As Global.System.Web.UI.UpdatePanel

    '''<summary>
    '''Controle txtID_BL.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents txtID_BL As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Controle txtLinhaBL.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents txtLinhaBL As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Controle divSuccess.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents divSuccess As Global.System.Web.UI.HtmlControls.HtmlGenericControl

    '''<summary>
    '''Controle lblSuccess.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents lblSuccess As Global.System.Web.UI.WebControls.Label

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
    '''Controle txtData.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents txtData As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Controle botoesPagamento.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents botoesPagamento As Global.System.Web.UI.HtmlControls.HtmlGenericControl

    '''<summary>
    '''Controle btnBaixarPagamento.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents btnBaixarPagamento As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Controle btnCancelarPagamento.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents btnCancelarPagamento As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Controle botoesRecebimento.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents botoesRecebimento As Global.System.Web.UI.HtmlControls.HtmlGenericControl

    '''<summary>
    '''Controle btnBaixarRecebimento.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents btnBaixarRecebimento As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Controle btnCancelarRecebimento.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents btnCancelarRecebimento As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Controle gridPagar.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents gridPagar As Global.System.Web.UI.HtmlControls.HtmlGenericControl

    '''<summary>
    '''Controle dgvTaxasPagar.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents dgvTaxasPagar As Global.System.Web.UI.WebControls.GridView

    '''<summary>
    '''Controle gridReceber.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents gridReceber As Global.System.Web.UI.HtmlControls.HtmlGenericControl

    '''<summary>
    '''Controle dgvTaxasReceber.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents dgvTaxasReceber As Global.System.Web.UI.WebControls.GridView

    '''<summary>
    '''Controle dsPagar.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents dsPagar As Global.System.Web.UI.WebControls.SqlDataSource

    '''<summary>
    '''Controle dsReceber.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents dsReceber As Global.System.Web.UI.WebControls.SqlDataSource
End Class
