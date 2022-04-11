Imports System.IO
Public Class FaturarRecebimento
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Logado") = "False" Or Session("Logado") = Nothing Then

            Response.Redirect("Login.aspx")

        End If

        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 2027 AND FL_ACESSAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
        If ds.Tables(0).Rows(0).Item("QTD") = 0 Then

            Response.Redirect("Default.aspx")
        End If
        Con.Fechar()

    End Sub

    Private Sub dgvContasReceber_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgvContasReceber.RowCommand
        dgvContasReceber.Columns(9).Visible = False
        divSuccess.Visible = False
        divErro.Visible = False
        Dim Con As New Conexao_sql
        Dim ds As DataSet
        Con.Conectar()

        If e.CommandName = "Selecionar" Then
            Dim ID As String = e.CommandArgument
            ds = Con.ExecutarQuery("SELECT COUNT(ID_CONTA_PAGAR_RECEBER)QTD FROM [TB_CONTA_PAGAR_RECEBER] WHERE DT_CANCELAMENTO IS NULL AND ID_CONTA_PAGAR_RECEBER = " & ID)
            If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                divErro.Visible = True
                lblmsgErro.Text = "Não é possivel enviar este recebimento!"
            Else
                ds = Con.ExecutarQuery("SELECT COUNT(ID_CONTA_PAGAR_RECEBER)QTD FROM [TB_FATURAMENTO] WHERE DT_CANCELAMENTO IS NULL AND ID_CONTA_PAGAR_RECEBER = " & ID)
                If ds.Tables(0).Rows(0).Item("QTD") > 0 Then
                    divErro.Visible = True
                    lblmsgErro.Text = "Recebimento já enviado!"
                Else
                    txtID.Text = ID
                    ModalPopupExtender1.Show()
                End If
            End If

        End If


    End Sub

    Sub CarregaGrid()
        Dim filtro As String = ""

        If ddlFiltro.SelectedValue = 1 Then

            filtro &= "AND NR_PROCESSO LIKE '%" & txtPesquisa.Text & "%'"

        ElseIf ddlFiltro.SelectedValue = 2 Then
            filtro &= "AND NR_BL_MASTER LIKE '%" & txtPesquisa.Text & "%'"


        ElseIf ddlFiltro.SelectedValue = 3 Then
            filtro &= "AND PARCEIRO_EMPRESA LIKE '%" & txtPesquisa.Text & "%'"

        ElseIf ddlFiltro.SelectedValue = 4 Then
            filtro &= "AND REFERENCIA_CLIENTE LIKE '%" & txtPesquisa.Text & "%'"

        End If


        If rdStatus.SelectedValue = 0 Then
            'aberto
            filtro &= " AND DT_CANCELAMENTO IS NULL and DT_ENVIO_FATURAMENTO IS NULL"
        ElseIf rdStatus.SelectedValue = 1 Then
            'cancelado
            filtro &= " AND DT_CANCELAMENTO IS NOT NULL "
        ElseIf rdStatus.SelectedValue = 2 Then
            'enviado
            filtro &= " AND DT_ENVIO_FATURAMENTO IS NOT NULL"
        End If

        dsContasReceber.SelectCommand = "SELECT * FROM [dbo].[View_Contas_Receber] WHERE (CD_PR = 'R') AND ISNULL(TP_EXPORTACAO,'') = '' " & filtro
        dgvContasReceber.DataBind()

        If rdStatus.SelectedValue = 0 Then
            'aberto
            dgvContasReceber.Columns(12).Visible = True
        Else
            dgvContasReceber.Columns(12).Visible = False
        End If

        ddlFiltro.SelectedValue = 0
        txtPesquisa.Text = ""
    End Sub

    Private Sub btnPesquisa_Click(sender As Object, e As EventArgs) Handles btnPesquisa.Click
        CarregaGrid()
    End Sub

    Private Sub btnProsseguir_Click(sender As Object, e As EventArgs) Handles btnProsseguir.Click
        divSuccess.Visible = False
        divErro.Visible = False
        Dim Con As New Conexao_sql
        Dim ds As DataSet
        Con.Conectar()



        Dim ID As String = txtID.Text
        ds = Con.ExecutarQuery("SELECT COUNT(ID_CONTA_PAGAR_RECEBER)QTD FROM [TB_CONTA_PAGAR_RECEBER] WHERE DT_CANCELAMENTO IS NULL AND ID_CONTA_PAGAR_RECEBER = " & ID)
        If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
            divErro.Visible = True
            lblmsgErro.Text = "Não é possivel enviar este recebimento!"
        Else
            ds = Con.ExecutarQuery("SELECT COUNT(ID_CONTA_PAGAR_RECEBER)QTD FROM [TB_FATURAMENTO] WHERE DT_CANCELAMENTO IS NULL AND ID_CONTA_PAGAR_RECEBER = " & ID)
            If ds.Tables(0).Rows(0).Item("QTD") > 0 Then
                divErro.Visible = True
                lblmsgErro.Text = "Recebimento já enviado!"
            Else

                'Con.ExecutarQuery("UPDATE TB_CONTA_PAGAR_RECEBER SET DT_ENVIO_FATURAMENTO = GETDATE() WHERE ID_CONTA_PAGAR_RECEBER = " & ID)

                'Dim dsFaturamento As DataSet = Con.ExecutarQuery("INSERT INTO TB_FATURAMENTO (ID_CONTA_PAGAR_RECEBER,VL_NOTA) SELECT ID_CONTA_PAGAR_RECEBER, (SELECT SUM(ISNULL(VL_LIQUIDO,0)) FROM TB_CONTA_PAGAR_RECEBER_ITENS WHERE ID_CONTA_PAGAR_RECEBER = A.ID_CONTA_PAGAR_RECEBER AND ID_ITEM_DESPESA IN (SELECT ID_ITEM_DESPESA FROM TB_ITEM_DESPESA WHERE FL_RECEITA = 1)) VL_NOTA FROM TB_CONTA_PAGAR_RECEBER A WHERE ID_CONTA_PAGAR_RECEBER = " & ID & " ; Select SCOPE_IDENTITY() as ID_FATURAMENTO  ")


                Dim ID_FATURAMENTO As String = 0 'dsFaturamento.Tables(0).Rows(0).Item("ID_FATURAMENTO")

                'Dim dsParceiro As DataSet = Con.ExecutarQuery("SELECT ID_PARCEIRO,NM_RAZAO,CNPJ,INSCR_ESTADUAL,INSCR_MUNICIPAL,ENDERECO,NR_ENDERECO,COMPL_ENDERECO,BAIRRO,CEP,(SELECT NM_CIDADE FROM TB_CIDADE WHERE ID_CIDADE = A.ID_CIDADE)CIDADE,(SELECT NM_ESTADO FROM TB_ESTADO WHERE ID_ESTADO = (SELECT ID_ESTADO FROM TB_CIDADE WHERE ID_CIDADE = A.ID_CIDADE))ESTADO,VL_ALIQUOTA_ISS FROM TB_PARCEIRO A
                'WHERE ID_PARCEIRO = (SELECT TOP 1 ID_PARCEIRO_EMPRESA FROM TB_CONTA_PAGAR_RECEBER_ITENS WHERE ID_CONTA_PAGAR_RECEBER= " & ID & ")")
                'If dsParceiro.Tables(0).Rows.Count > 0 Then

                '    Con.ExecutarQuery("UPDATE [dbo].[TB_FATURAMENTO] SET ID_PARCEIRO_CLIENTE = " & dsParceiro.Tables(0).Rows(0).Item("ID_PARCEIRO").ToString & ",NM_CLIENTE = '" & dsParceiro.Tables(0).Rows(0).Item("NM_RAZAO").ToString & "',CNPJ = '" & dsParceiro.Tables(0).Rows(0).Item("CNPJ").ToString & "',INSCR_ESTADUAL ='" & dsParceiro.Tables(0).Rows(0).Item("INSCR_ESTADUAL").ToString & "',INSCR_MUNICIPAL ='" & dsParceiro.Tables(0).Rows(0).Item("INSCR_MUNICIPAL").ToString & "',ENDERECO='" & dsParceiro.Tables(0).Rows(0).Item("ENDERECO").ToString & "',NR_ENDERECO='" & dsParceiro.Tables(0).Rows(0).Item("NR_ENDERECO").ToString & "',COMPL_ENDERECO='" & dsParceiro.Tables(0).Rows(0).Item("COMPL_ENDERECO").ToString & "',BAIRRO='" & dsParceiro.Tables(0).Rows(0).Item("BAIRRO").ToString & "',CEP ='" & dsParceiro.Tables(0).Rows(0).Item("CEP").ToString & "',CIDADE ='" & dsParceiro.Tables(0).Rows(0).Item("CIDADE").ToString & "',ESTADO ='" & dsParceiro.Tables(0).Rows(0).Item("ESTADO").ToString & "' WHERE ID_FATURAMENTO =" & ID_FATURAMENTO)
                'End If



                If FileUpload1.HasFile Then
                    Dim nomeArquivo As String = Path.GetFileName(FileUpload1.PostedFile.FileName)
                    Dim fileExtensio As String = Path.GetExtension(FileUpload1.PostedFile.FileName.ToString())
                    Dim tamanhoArquivo As Long = FileUpload1.PostedFile.ContentLength
                    Dim diretorio_arquivos As String = Server.MapPath("~/Content/Arquivos/Faturamento") & ID_FATURAMENTO

                    If Not Directory.Exists(diretorio_arquivos) Then
                        System.IO.Directory.CreateDirectory(diretorio_arquivos)
                    End If

                    Dim nomeArquivofinal As String = ""
                    If nomeArquivo.Length > 150 Then
                        nomeArquivofinal = nomeArquivo.Substring(0, 150) & fileExtensio
                    Else
                        nomeArquivofinal = nomeArquivo
                    End If
                    FileUpload1.PostedFile.SaveAs(diretorio_arquivos & "/" & nomeArquivofinal)
                    divSuccess.Visible = True

                End If


                txtID.Text = ""

                divSuccess.Visible = True
                lblmsgSuccess.Text = "Enviado com sucesso!"
            End If
        End If

        dgvContasReceber.DataBind()


        dgvContasReceber.Columns(9).Visible = True

        CarregaGrid()
    End Sub

    Private Sub btnFechar_Click(sender As Object, e As EventArgs) Handles btnFechar.Click
        txtID.Text = ""
        ModalPopupExtender1.Hide()
        dgvContasReceber.DataBind()
        dgvContasReceber.Columns(9).Visible = True
        CarregaGrid()
    End Sub

End Class