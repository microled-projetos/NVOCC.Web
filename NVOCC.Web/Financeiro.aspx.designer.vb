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


Partial Public Class Financeiro

    '''<summary>
    '''Controle UpdatePanel1.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents UpdatePanel1 As Global.System.Web.UI.UpdatePanel

    '''<summary>
    '''Controle divSuccess.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents divSuccess As Global.System.Web.UI.HtmlControls.HtmlGenericControl

    '''<summary>
    '''Controle lblmsgSuccess.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents lblmsgSuccess As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Controle divErro.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents divErro As Global.System.Web.UI.HtmlControls.HtmlGenericControl

    '''<summary>
    '''Controle lblmsgErro.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents lblmsgErro As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Controle ddlFiltro.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents ddlFiltro As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Controle txtPesquisa.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents txtPesquisa As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Controle ckStatus.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents ckStatus As Global.System.Web.UI.WebControls.CheckBoxList

    '''<summary>
    '''Controle btnPesquisa.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents btnPesquisa As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Controle Label6.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents Label6 As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Controle lkContasPagar.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents lkContasPagar As Global.System.Web.UI.WebControls.LinkButton

    '''<summary>
    '''Controle lkContasReceber.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents lkContasReceber As Global.System.Web.UI.WebControls.LinkButton

    '''<summary>
    '''Controle lkIntegracao.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents lkIntegracao As Global.System.Web.UI.WebControls.LinkButton

    '''<summary>
    '''Controle lkIndicadores.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents lkIndicadores As Global.System.Web.UI.WebControls.LinkButton

    '''<summary>
    '''Controle Button1.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents Button1 As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Controle mpeImprimir.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents mpeImprimir As Global.AjaxControlToolkit.ModalPopupExtender

    '''<summary>
    '''Controle pnlContasPagar.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents pnlContasPagar As Global.System.Web.UI.WebControls.Panel

    '''<summary>
    '''Controle lkSolicitacaoPagamento.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents lkSolicitacaoPagamento As Global.System.Web.UI.WebControls.LinkButton

    '''<summary>
    '''Controle lkMontagemPagamento.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents lkMontagemPagamento As Global.System.Web.UI.WebControls.LinkButton

    '''<summary>
    '''Controle lkBaixaCancel_Pagar.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents lkBaixaCancel_Pagar As Global.System.Web.UI.WebControls.LinkButton

    '''<summary>
    '''Controle btnFecharContasPagar.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents btnFecharContasPagar As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Controle ModalPopupExtender1.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents ModalPopupExtender1 As Global.AjaxControlToolkit.ModalPopupExtender

    '''<summary>
    '''Controle pnlContasReceber.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents pnlContasReceber As Global.System.Web.UI.WebControls.Panel

    '''<summary>
    '''Controle lkCalcularRecebimento.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents lkCalcularRecebimento As Global.System.Web.UI.WebControls.LinkButton

    '''<summary>
    '''Controle lkEmissaoND.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents lkEmissaoND As Global.System.Web.UI.WebControls.LinkButton

    '''<summary>
    '''Controle lkBaixaCancel_Receber.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents lkBaixaCancel_Receber As Global.System.Web.UI.WebControls.LinkButton

    '''<summary>
    '''Controle lkFaturar.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents lkFaturar As Global.System.Web.UI.WebControls.LinkButton

    '''<summary>
    '''Controle btnFecharContasReceber.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents btnFecharContasReceber As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Controle ModalPopupExtender3.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents ModalPopupExtender3 As Global.AjaxControlToolkit.ModalPopupExtender

    '''<summary>
    '''Controle pnlIntegracao.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents pnlIntegracao As Global.System.Web.UI.WebControls.Panel

    '''<summary>
    '''Controle lkNotaDespesa.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents lkNotaDespesa As Global.System.Web.UI.WebControls.LinkButton

    '''<summary>
    '''Controle lkPA.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents lkPA As Global.System.Web.UI.WebControls.LinkButton

    '''<summary>
    '''Controle btnFecharIntegracao.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents btnFecharIntegracao As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Controle ModalPopupExtender2.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents ModalPopupExtender2 As Global.AjaxControlToolkit.ModalPopupExtender

    '''<summary>
    '''Controle pnlIndicadores.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents pnlIndicadores As Global.System.Web.UI.WebControls.Panel

    '''<summary>
    '''Controle lkInternacional.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents lkInternacional As Global.System.Web.UI.WebControls.LinkButton

    '''<summary>
    '''Controle lkNacional.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents lkNacional As Global.System.Web.UI.WebControls.LinkButton

    '''<summary>
    '''Controle btnFecharIndicadores.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents btnFecharIndicadores As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Controle updPainel1.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents updPainel1 As Global.System.Web.UI.UpdatePanel

    '''<summary>
    '''Controle divAuxiliar.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents divAuxiliar As Global.System.Web.UI.HtmlControls.HtmlGenericControl

    '''<summary>
    '''Controle txtID.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents txtID As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Controle txtlinha.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents txtlinha As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Controle dgvFinanceiro.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents dgvFinanceiro As Global.System.Web.UI.WebControls.GridView

    '''<summary>
    '''Controle lblAprovadas.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents lblAprovadas As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Controle lblRejeitadas.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents lblRejeitadas As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Controle dsFinanceiro.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents dsFinanceiro As Global.System.Web.UI.WebControls.SqlDataSource
End Class
