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


Partial Public Class CalcularRecebimento

    '''<summary>
    '''Controle lblMBL.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents lblMBL As Global.System.Web.UI.WebControls.Label

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
    '''Controle ddlFornecedor.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents ddlFornecedor As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Controle lblCidade.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents lblCidade As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Controle txtCambio.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents txtCambio As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Controle txtVencimento.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents txtVencimento As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Controle lblTipoFaturamento.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents lblTipoFaturamento As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Controle lblDiasFaturamento.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents lblDiasFaturamento As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Controle lblAcordo.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents lblAcordo As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Controle lblSpread.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents lblSpread As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Controle divConteudo.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents divConteudo As Global.System.Web.UI.HtmlControls.HtmlGenericControl

    '''<summary>
    '''Controle dgvTaxas.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents dgvTaxas As Global.System.Web.UI.WebControls.GridView

    '''<summary>
    '''Controle dgvMoedaFreteArmador.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents dgvMoedaFreteArmador As Global.System.Web.UI.WebControls.GridView

    '''<summary>
    '''Controle dgvMoedaFrete.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents dgvMoedaFrete As Global.System.Web.UI.WebControls.GridView

    '''<summary>
    '''Controle btnCalcularRecebimento.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents btnCalcularRecebimento As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Controle btnCancelar.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents btnCancelar As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Controle dsTaxas.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents dsTaxas As Global.System.Web.UI.WebControls.SqlDataSource

    '''<summary>
    '''Controle dsMoedaFreteArmador.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents dsMoedaFreteArmador As Global.System.Web.UI.WebControls.SqlDataSource

    '''<summary>
    '''Controle dsMoedaFrete.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents dsMoedaFrete As Global.System.Web.UI.WebControls.SqlDataSource

    '''<summary>
    '''Controle dsFornecedor.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents dsFornecedor As Global.System.Web.UI.WebControls.SqlDataSource
End Class
