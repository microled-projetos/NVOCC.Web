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
    '''Controle lkFatura.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents lkFatura As Global.System.Web.UI.WebControls.LinkButton

    '''<summary>
    '''Controle lkDesmosntrativos.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents lkDesmosntrativos As Global.System.Web.UI.WebControls.LinkButton

    '''<summary>
    '''Controle lkRPS.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents lkRPS As Global.System.Web.UI.WebControls.LinkButton

    '''<summary>
    '''Controle lkNotasFiscais.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents lkNotasFiscais As Global.System.Web.UI.WebControls.LinkButton

    '''<summary>
    '''Controle lkOpcoesBoletos.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents lkOpcoesBoletos As Global.System.Web.UI.WebControls.LinkButton

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
    '''Controle ModalPopupExtender11.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents ModalPopupExtender11 As Global.AjaxControlToolkit.ModalPopupExtender

    '''<summary>
    '''Controle pnlOpcoesBoletos.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents pnlOpcoesBoletos As Global.System.Web.UI.WebControls.Panel

    '''<summary>
    '''Controle lkBoleto.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents lkBoleto As Global.System.Web.UI.WebControls.LinkButton

    '''<summary>
    '''Controle lkBoletoRemessa.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents lkBoletoRemessa As Global.System.Web.UI.WebControls.LinkButton

    '''<summary>
    '''Controle btnFecharOpcoesBoletos.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents btnFecharOpcoesBoletos As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Controle ModalPopupExtender7.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents ModalPopupExtender7 As Global.AjaxControlToolkit.ModalPopupExtender

    '''<summary>
    '''Controle pnlFatura.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents pnlFatura As Global.System.Web.UI.WebControls.Panel

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
    '''Controle btnFecharFatura.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents btnFecharFatura As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Controle ModalPopupExtender8.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents ModalPopupExtender8 As Global.AjaxControlToolkit.ModalPopupExtender

    '''<summary>
    '''Controle pnlRPS.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents pnlRPS As Global.System.Web.UI.WebControls.Panel

    '''<summary>
    '''Controle lkGerarRPS.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents lkGerarRPS As Global.System.Web.UI.WebControls.LinkButton

    '''<summary>
    '''Controle lkReenviarRPS.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents lkReenviarRPS As Global.System.Web.UI.WebControls.LinkButton

    '''<summary>
    '''Controle btnFecharRPS.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents btnFecharRPS As Global.System.Web.UI.WebControls.Button

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
    '''Controle lkConsultaNotas.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents lkConsultaNotas As Global.System.Web.UI.WebControls.LinkButton

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
    '''Controle ModalPopupExtender6.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents ModalPopupExtender6 As Global.AjaxControlToolkit.ModalPopupExtender

    '''<summary>
    '''Controle pnlBanco.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents pnlBanco As Global.System.Web.UI.WebControls.Panel

    '''<summary>
    '''Controle ddlBanco.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents ddlBanco As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Controle btnImprimirBoleto.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents btnImprimirBoleto As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Controle btnFecharBoleto.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents btnFecharBoleto As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Controle ModalPopupExtender9.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents ModalPopupExtender9 As Global.AjaxControlToolkit.ModalPopupExtender

    '''<summary>
    '''Controle UpdatePanel2.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents UpdatePanel2 As Global.System.Web.UI.UpdatePanel

    '''<summary>
    '''Controle pnlConsultaNota.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents pnlConsultaNota As Global.System.Web.UI.WebControls.Panel

    '''<summary>
    '''Controle divErroConsultasNotas.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents divErroConsultasNotas As Global.System.Web.UI.HtmlControls.HtmlGenericControl

    '''<summary>
    '''Controle lblErroConsultasNotas.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents lblErroConsultasNotas As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Controle txtConsultaNotaInicio.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents txtConsultaNotaInicio As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Controle txtConsultaNotaFim.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents txtConsultaNotaFim As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Controle txtConsultaRPSInicio.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents txtConsultaRPSInicio As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Controle txtConsultaRPSFim.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents txtConsultaRPSFim As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Controle txtConsultaVencimentoInicio.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents txtConsultaVencimentoInicio As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Controle txtConsultaVencimentoFim.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents txtConsultaVencimentoFim As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Controle txtConsultaPagamentoInicio.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents txtConsultaPagamentoInicio As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Controle txtConsultaPagamentoFim.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents txtConsultaPagamentoFim As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Controle ddlStatusConsultaNotas.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents ddlStatusConsultaNotas As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Controle ddlCliente.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents ddlCliente As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Controle btnConsultaNotas.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents btnConsultaNotas As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Controle btnFecharConsulta.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents btnFecharConsulta As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Controle TextBox2.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents TextBox2 As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Controle ModalPopupExtender10.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents ModalPopupExtender10 As Global.AjaxControlToolkit.ModalPopupExtender

    '''<summary>
    '''Controle pnlOBSRPS.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents pnlOBSRPS As Global.System.Web.UI.WebControls.Panel

    '''<summary>
    '''Controle txtOBSRPS.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents txtOBSRPS As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Controle btnProsseguir.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents btnProsseguir As Global.System.Web.UI.WebControls.Button

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

    '''<summary>
    '''Controle dsBanco.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents dsBanco As Global.System.Web.UI.WebControls.SqlDataSource

    '''<summary>
    '''Controle dsClientes.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Modificar a declaração do campo de movimento do arquivo de designer para o arquivo code-behind.
    '''</remarks>
    Protected WithEvents dsClientes As Global.System.Web.UI.WebControls.SqlDataSource
End Class
