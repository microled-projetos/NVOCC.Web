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


Partial Public Class Faturamento

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
    '''Controle btnPesquisar.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents btnPesquisar As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Controle Label6.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents Label6 As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Controle lkBaixarFatura.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents lkBaixarFatura As Global.System.Web.UI.WebControls.LinkButton

    '''<summary>
    '''Controle lkCancelamento.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents lkCancelamento As Global.System.Web.UI.WebControls.LinkButton

    '''<summary>
    '''Controle lkDesmosntrativos.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents lkDesmosntrativos As Global.System.Web.UI.WebControls.LinkButton

    '''<summary>
    '''Controle lkGerarRPS.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents lkGerarRPS As Global.System.Web.UI.WebControls.LinkButton

    '''<summary>
    '''Controle lkNotasFiscais.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents lkNotasFiscais As Global.System.Web.UI.WebControls.LinkButton

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
    '''Controle lblContador.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents lblContador As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Controle dgvFaturamento.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents dgvFaturamento As Global.System.Web.UI.WebControls.GridView

    '''<summary>
    '''Controle ModalPopupExtender1.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents ModalPopupExtender1 As Global.AjaxControlToolkit.ModalPopupExtender

    '''<summary>
    '''Controle pnlBaixarFatura.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents pnlBaixarFatura As Global.System.Web.UI.WebControls.Panel

    '''<summary>
    '''Controle txtData.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents txtData As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Controle lblProcessoBaixa.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents lblProcessoBaixa As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Controle lblClienteBaixa.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents lblClienteBaixa As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Controle btnBaixarFatura.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents btnBaixarFatura As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Controle btnFecharBaixa.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents btnFecharBaixa As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Controle ModalPopupExtender3.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents ModalPopupExtender3 As Global.AjaxControlToolkit.ModalPopupExtender

    '''<summary>
    '''Controle pnlCancelamento.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents pnlCancelamento As Global.System.Web.UI.WebControls.Panel

    '''<summary>
    '''Controle divInfo.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents divInfo As Global.System.Web.UI.HtmlControls.HtmlGenericControl

    '''<summary>
    '''Controle lblmsgInfo.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents lblmsgInfo As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Controle lblProcessoCancelamento.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents lblProcessoCancelamento As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Controle lblClienteCancelamento.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents lblClienteCancelamento As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Controle txtObs.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents txtObs As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Controle btnSalvarCancelamento.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents btnSalvarCancelamento As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Controle btnFecharCancelamento.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents btnFecharCancelamento As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Controle ModalPopupExtender4.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents ModalPopupExtender4 As Global.AjaxControlToolkit.ModalPopupExtender

    '''<summary>
    '''Controle pnlDesmosntrativos.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents pnlDesmosntrativos As Global.System.Web.UI.WebControls.Panel

    '''<summary>
    '''Controle lkNotaDebito.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents lkNotaDebito As Global.System.Web.UI.WebControls.LinkButton

    '''<summary>
    '''Controle lkReciboServico.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents lkReciboServico As Global.System.Web.UI.WebControls.LinkButton

    '''<summary>
    '''Controle lkReciboPagamento.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents lkReciboPagamento As Global.System.Web.UI.WebControls.LinkButton

    '''<summary>
    '''Controle btnFecharDesmosntrativos.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents btnFecharDesmosntrativos As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Controle ModalPopupExtender2.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents ModalPopupExtender2 As Global.AjaxControlToolkit.ModalPopupExtender

    '''<summary>
    '''Controle pnlNotasFiscais.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents pnlNotasFiscais As Global.System.Web.UI.WebControls.Panel

    '''<summary>
    '''Controle lkCancelarNota.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents lkCancelarNota As Global.System.Web.UI.WebControls.LinkButton

    '''<summary>
    '''Controle lkSubstituirNota.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents lkSubstituirNota As Global.System.Web.UI.WebControls.LinkButton

    '''<summary>
    '''Controle lkVisualizarNota.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents lkVisualizarNota As Global.System.Web.UI.WebControls.LinkButton

    '''<summary>
    '''Controle btnFecharNotas.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents btnFecharNotas As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Controle ModalPopupExtender5.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents ModalPopupExtender5 As Global.AjaxControlToolkit.ModalPopupExtender

    '''<summary>
    '''Controle pnlSubstituirNota.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents pnlSubstituirNota As Global.System.Web.UI.WebControls.Panel

    '''<summary>
    '''Controle divErroSubstituir.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents divErroSubstituir As Global.System.Web.UI.HtmlControls.HtmlGenericControl

    '''<summary>
    '''Controle lblErroSubstituir.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents lblErroSubstituir As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Controle lblProcessoSubs.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents lblProcessoSubs As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Controle lblClienteSubs.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents lblClienteSubs As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Controle lblNumeroNota.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents lblNumeroNota As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Controle lblDataEmissao.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents lblDataEmissao As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Controle txtNovoNumeroNota.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents txtNovoNumeroNota As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Controle txtNovaEmissaoNota.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents txtNovaEmissaoNota As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Controle btnSubstituir.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents btnSubstituir As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Controle btnFecharSubstituicao.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents btnFecharSubstituicao As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Controle txtResultado.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents txtResultado As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Controle TextBox1.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents TextBox1 As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Controle dsFaturamento.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents dsFaturamento As Global.System.Web.UI.WebControls.SqlDataSource

    '''<summary>
    '''Controle dsParceiros.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents dsParceiros As Global.System.Web.UI.WebControls.SqlDataSource
End Class
