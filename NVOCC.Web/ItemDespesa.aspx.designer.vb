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


Partial Public Class ItemDespesa

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
    '''Controle txtIDItemDespesa.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents txtIDItemDespesa As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Controle ckbAtivo.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents ckbAtivo As Global.System.Web.UI.WebControls.CheckBox

    '''<summary>
    '''Controle ckbIntegraPA.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents ckbIntegraPA As Global.System.Web.UI.WebControls.CheckBox

    '''<summary>
    '''Controle txtNome.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents txtNome As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Controle ddlTipoItemDespesa.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents ddlTipoItemDespesa As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Controle txtNatureza.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents txtNatureza As Global.System.Web.UI.WebControls.TextBox

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
    '''Controle ddlConsulta.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents ddlConsulta As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Controle divPesquisa.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents divPesquisa As Global.System.Web.UI.HtmlControls.HtmlGenericControl

    '''<summary>
    '''Controle txtConsulta.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents txtConsulta As Global.System.Web.UI.WebControls.TextBox

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
    '''Controle dgvItemDespesa.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents dgvItemDespesa As Global.System.Web.UI.WebControls.GridView

    '''<summary>
    '''Controle dsItemDespesa.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents dsItemDespesa As Global.System.Web.UI.WebControls.SqlDataSource

    '''<summary>
    '''Controle dsTipoItemDespesa.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents dsTipoItemDespesa As Global.System.Web.UI.WebControls.SqlDataSource
End Class
