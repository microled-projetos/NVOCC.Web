Imports Newtonsoft.Json

Public Class RemessaBoletos
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Logado") = "False" Or Session("Logado") = Nothing Then

            Response.Redirect("Login.aspx")

        End If

        Dim Con As New Conexao_sql
        Con.Conectar()

        Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 2028 AND FL_ACESSAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
        If ds.Tables(0).Rows(0).Item("QTD") = 0 Then

            Response.Redirect("Default.aspx")

        End If
        Con.Fechar()
    End Sub

    Private Sub btnPesquisar_Click(sender As Object, e As EventArgs) Handles btnPesquisar.Click
        Dim filtro As String = ""

        If ddlCliente.SelectedValue <> 0 Then
            Dim nome As String = ddlCliente.SelectedItem.Text
            filtro &= " AND NM_CLIENTE = '" & nome & "' "

        End If

        If ddlBanco.SelectedValue <> 0 Then
            filtro &= " AND COD_BANCO = '" & ddlBanco.SelectedItem.Text & "' "
        Else
            divErro.Visible = True
            lblmsgErro.Text = "É necessário informar o banco para prosseguir com a consulta!"
            Exit Sub
        End If

        If txtConsultaNotaInicio.Text <> "" Then
            If txtConsultaNotaFim.Text <> "" Then
                'filtro
                filtro &= " AND NR_NOTA_FISCAL BETWEEN '" & txtConsultaNotaInicio.Text & "' AND '" & txtConsultaNotaFim.Text & "' "
            Else
                'msg erro
                divErro.Visible = True
                lblmsgErro.Text = "É necessário informar inicio e fim para prosseguir com a consulta!"
                Exit Sub
            End If
        End If


        If txtConsultaVencimentoInicio.Text <> "" Then
            If txtConsultaVencimentoFim.Text <> "" Then
                'filtro
                filtro &= " AND  CONVERT(DATE,DT_VENCIMENTO,103)  BETWEEN CONVERT(DATE,'" & txtConsultaVencimentoInicio.Text & "',103) AND CONVERT(DATE,'" & txtConsultaVencimentoFim.Text & "',103) "

            Else
                'msg erro
                divErro.Visible = True
                lblmsgErro.Text = "É necessário informar data de inicio e fim para prosseguir com a consulta!"
                Exit Sub

            End If
        End If

        If txtConsultaEmissaoInicio.Text <> "" Then
            If txtConsultaEmissaoFim.Text <> "" Then
                'filtro
                filtro &= " AND CONVERT(DATE,DT_NOTA_FISCAL,103)  BETWEEN CONVERT(DATE,'" & txtConsultaEmissaoInicio.Text & "',103) AND CONVERT(DATE,'" & txtConsultaEmissaoFim.Text & "',103) "

            Else
                'msg erro
                divErro.Visible = True
                lblmsgErro.Text = "É necessário informar data de inicio e fim para prosseguir com a consulta!"
                Exit Sub

            End If
        End If


        Dim sql As String = "SELECT * FROM [dbo].[View_Boletos_Remessa] WHERE FL_ENVIADO_REM = 0  " & filtro & " ORDER BY DT_VENCIMENTO,NR_PROCESSO"
        dsFaturamento.SelectCommand = sql
        dgvFaturamento.DataBind()
        dgvFaturamento.Visible = True
        divBotoes.Visible = True

        ddlCliente.SelectedValue = 0

        txtConsultaNotaInicio.Text = ""
        txtConsultaNotaFim.Text = ""


        txtConsultaVencimentoInicio.Text = ""
        txtConsultaVencimentoFim.Text = ""

        txtConsultaEmissaoInicio.Text = ""
        txtConsultaEmissaoFim.Text = ""

    End Sub

    Private Sub btnDesmarcar_Click(sender As Object, e As EventArgs) Handles btnDesmarcar.Click
        For i As Integer = 0 To Me.dgvFaturamento.Rows.Count - 1
            Dim ckbSelecionar = CType(Me.dgvFaturamento.Rows(i).FindControl("ckbSelecionar"), CheckBox)
            ckbSelecionar.Checked = False
        Next
        divErro.Visible = False
    End Sub

    Private Sub btnMarcar_Click(sender As Object, e As EventArgs) Handles btnMarcar.Click
        For i As Integer = 0 To Me.dgvFaturamento.Rows.Count - 1
            Dim ckbSelecionar = CType(Me.dgvFaturamento.Rows(i).FindControl("ckbSelecionar"), CheckBox)
            ckbSelecionar.Checked = True
        Next
    End Sub

    Private Sub btnEnviarRemessa_Click(sender As Object, e As EventArgs) Handles btnEnviarRemessa.Click

        Dim Remessa As New Remessa()
        Remessa.Banco = ddlBanco.SelectedItem.Text
        Remessa.Id = New List(Of String)
        For Each linha As GridViewRow In dgvFaturamento.Rows
            Dim check As CheckBox = linha.FindControl("ckbSelecionar")
            Dim ID As String = CType(linha.FindControl("lblID"), Label).Text
            If check.Checked Then
                Remessa.Id.Add(ID)
            End If
        Next
        ' Label1.Text = JsonConvert.SerializeObject(Remessa)
        JsonConvert.SerializeObject(Remessa)
    End Sub
End Class