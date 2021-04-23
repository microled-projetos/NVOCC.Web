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


Partial Public Class CadastrarUsuario

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
    '''Controle diverro.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents diverro As Global.System.Web.UI.HtmlControls.HtmlGenericControl

    '''<summary>
    '''Controle lblerro.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents lblerro As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Controle txtID.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents txtID As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Controle txtNome.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents txtNome As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Controle txtEmail.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents txtEmail As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Controle txtCPF.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents txtCPF As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Controle txtTelefone.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents txtTelefone As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Controle txtCelular.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents txtCelular As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Controle txtLogin.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents txtLogin As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Controle txtSenha.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents txtSenha As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Controle txtConfirmaSenha.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents txtConfirmaSenha As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Controle ckbAtivo.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents ckbAtivo As Global.System.Web.UI.WebControls.CheckBox

    '''<summary>
    '''Controle ckbExterno.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents ckbExterno As Global.System.Web.UI.WebControls.CheckBox

    '''<summary>
    '''Controle ckbHouseBasico.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents ckbHouseBasico As Global.System.Web.UI.WebControls.CheckBox

    '''<summary>
    '''Controle ckbHouseCarga.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents ckbHouseCarga As Global.System.Web.UI.WebControls.CheckBox

    '''<summary>
    '''Controle ckbHouseTaxas.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents ckbHouseTaxas As Global.System.Web.UI.WebControls.CheckBox

    '''<summary>
    '''Controle ckbMasterBasico.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents ckbMasterBasico As Global.System.Web.UI.WebControls.CheckBox

    '''<summary>
    '''Controle ckbMasterCNTR.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents ckbMasterCNTR As Global.System.Web.UI.WebControls.CheckBox

    '''<summary>
    '''Controle ckbMasterTaxas.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents ckbMasterTaxas As Global.System.Web.UI.WebControls.CheckBox

    '''<summary>
    '''Controle ckbMasterVinculo.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents ckbMasterVinculo As Global.System.Web.UI.WebControls.CheckBox

    '''<summary>
    '''Controle divTipoUsuario.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents divTipoUsuario As Global.System.Web.UI.HtmlControls.HtmlGenericControl

    '''<summary>
    '''Controle cbTipoUsuario.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents cbTipoUsuario As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Controle ImageButton1.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents ImageButton1 As Global.System.Web.UI.WebControls.ImageButton

    '''<summary>
    '''Controle div1.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents div1 As Global.System.Web.UI.HtmlControls.HtmlGenericControl

    '''<summary>
    '''Controle gdvTipoUsuario.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents gdvTipoUsuario As Global.System.Web.UI.WebControls.GridView

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
    '''Controle dgvUsuarios.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents dgvUsuarios As Global.System.Web.UI.WebControls.GridView

    '''<summary>
    '''Controle dsUsuario.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents dsUsuario As Global.System.Web.UI.WebControls.SqlDataSource

    '''<summary>
    '''Controle dsGruposUsuario.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents dsGruposUsuario As Global.System.Web.UI.WebControls.SqlDataSource

    '''<summary>
    '''Controle dsTipoUsuario.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents dsTipoUsuario As Global.System.Web.UI.WebControls.SqlDataSource
End Class
