Public Class BaixarComissao
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Logado") = "False" Or Session("Logado") = Nothing Then

            Response.Redirect("Login.aspx")

        End If

        Dim Con As New Conexao_sql
        Con.Conectar()

        Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 2029 AND FL_ACESSAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
        If ds.Tables(0).Rows(0).Item("QTD") = 0 Then

            Response.Redirect("Default.aspx")

        End If
        Con.Fechar()
    End Sub

    Private Sub btnPesquisar_Click(sender As Object, e As EventArgs) Handles btnPesquisar.Click
        txtID.Text = ""
        txtLinha.Text = ""
        divErro.Visible = False

        If txtCompetencia.Text = "" Then
            lblErro.Text = "É necessario informar a competência."
            divErro.Visible = True
        Else
            Dim filtro As String = " AND DT_COMPETENCIA = '" & txtCompetencia.Text & "'"

            If ckStatus.Items.FindByValue(1).Selected Then
                filtro &= " AND DT_LIQUIDACAO IS NULL AND DT_CANCELAMENTO IS NULL"
            End If

            If ckStatus.Items.FindByValue(2).Selected Then
                filtro &= " AND DT_LIQUIDACAO IS NOT NULL AND DT_CANCELAMENTO IS NULL"
            End If


            dsPagar.SelectCommand = "SELECT * FROM [dbo].[View_Baixas_Cancelamentos] WHERE CD_PR =  'P' AND TP_EXPORTACAO = 'CINT' " & filtro & " ORDER BY DT_VENCIMENTO DESC, NR_FATURA_FORNECEDOR"
            dgvTaxasPagar.DataBind()
            gridPagar.Visible = True


        End If
    End Sub

    Private Sub btnSalvarBaixa_Click(sender As Object, e As EventArgs) Handles btnSalvarBaixa.Click
        divErro.Visible = False
        divSuccess.Visible = False


        Dim Con As New Conexao_sql
        Con.Conectar()
        If txtCompetencia.Text = "" Then
            lblErro.Text = "É necessário informar a data para efetuar a baixa!"
            divErro.Visible = True
            Exit Sub

        Else


            For Each linha As GridViewRow In dgvTaxasPagar.Rows
                Dim check As CheckBox = linha.FindControl("ckbSelecionar")
                If check.Checked Then
                    Dim ID As String = CType(linha.FindControl("lblID"), Label).Text
                    Con.ExecutarQuery("UPDATE [dbo].[TB_CONTA_PAGAR_RECEBER] SET [DT_LIQUIDACAO] = CONVERT(DATE,'" & txtCompetencia.Text & "',103), ID_USUARIO_LIQUIDACAO = " & Session("ID_USUARIO") & " WHERE ID_CONTA_PAGAR_RECEBER =" & ID)
                End If
            Next





            dgvTaxasPagar.DataBind()



            Con.Fechar()
            lblSuccess.Text = "Baixa realizada com sucesso!"
            divSuccess.Visible = True
            txtCompetencia.Text = ""


        End If
    End Sub

    Private Sub dgvTaxasPagar_Load(sender As Object, e As EventArgs) Handles dgvTaxasPagar.Load
        Dim Con As New Conexao_sql

        lblFaturaBaixa.Text = ""
        lblProcessoBaixa.Text = ""
        lblClienteBaixa.Text = ""
        For Each linha As GridViewRow In dgvTaxasPagar.Rows
            Dim ID As String = CType(linha.FindControl("lblID"), Label).Text
            Dim check As CheckBox = linha.FindControl("ckbSelecionar")
            Dim Competencia As String = CType(linha.FindControl("lblCompetencia"), Label).Text
            Dim Indicador As String = CType(linha.FindControl("lblIndicador"), Label).Text

            If check.Checked Then



                lblFaturaBaixa.Text &= "Competencia: " & Competencia & "<br/>"
                lblClienteBaixa.Text &= "Indicador: " & Indicador & "<br/>"
            End If
        Next
    End Sub
End Class