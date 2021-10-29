Imports Newtonsoft.Json
Imports System.IO

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
        Else
            If Not Page.IsPostBack Then
                ddlBanco.SelectedValue = "033"
            End If
        End If
        Con.Fechar()
    End Sub

    Sub Pesquisar()
        Dim filtro As String = ""
        divErro.Visible = False
        divSuccess.Visible = False
        If ddlCliente.SelectedValue <> 0 Then
            Dim nome As String = ddlCliente.SelectedItem.Text
            filtro &= " AND NM_CLIENTE = '" & nome & "' "

        End If

        If ddlBanco.SelectedValue <> 0 Then
            filtro &= " AND COD_BANCO = '" & ddlBanco.SelectedValue & "' "
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
                filtro &= " AND CONVERT(DATE,DT_NOTA_DEBITO,103)  BETWEEN CONVERT(DATE,'" & txtConsultaEmissaoInicio.Text & "',103) AND CONVERT(DATE,'" & txtConsultaEmissaoFim.Text & "',103) "

            Else
                'msg erro
                divErro.Visible = True
                lblmsgErro.Text = "É necessário informar data de inicio e fim para prosseguir com a consulta!"
                Exit Sub

            End If
        End If


        Dim sql As String = "SELECT * FROM View_Faturamento WHERE NOSSONUMERO IS NOT NULL AND FL_ENVIADO_REM = 0 " & filtro & " ORDER BY DT_VENCIMENTO,NR_PROCESSO"
        dsFaturamento.SelectCommand = sql
        dgvFaturamento.DataBind()
        For i As Integer = 0 To Me.dgvFaturamento.Rows.Count - 1
            Dim ckbSelecionar = CType(Me.dgvFaturamento.Rows(i).FindControl("ckSelecionar"), CheckBox)
            ckbSelecionar.Checked = True
        Next
        divBotoes.Visible = True

    End Sub
    Private Sub btnPesquisar_Click(sender As Object, e As EventArgs) Handles btnPesquisar.Click
        Pesquisar()

    End Sub

    Private Sub btnDesmarcar_Click(sender As Object, e As EventArgs) Handles btnDesmarcar.Click
        For i As Integer = 0 To Me.dgvFaturamento.Rows.Count - 1
            Dim ckbSelecionar = CType(Me.dgvFaturamento.Rows(i).FindControl("ckSelecionar"), CheckBox)
            ckbSelecionar.Checked = False
        Next
        divErro.Visible = False
    End Sub

    Private Sub btnMarcar_Click(sender As Object, e As EventArgs) Handles btnMarcar.Click
        For i As Integer = 0 To Me.dgvFaturamento.Rows.Count - 1
            Dim ckbSelecionar = CType(Me.dgvFaturamento.Rows(i).FindControl("ckSelecionar"), CheckBox)
            ckbSelecionar.Checked = True
        Next
    End Sub

    Sub ArquivoRemessa()

        divSuccess.Visible = False
        divErro.Visible = False
        Dim GeraRemessa As New GeraRemessa

        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ConOracle As New Conexao_oracle
        ConOracle.Conectar()
        Dim dsBanco As DataSet = Con.ExecutarQuery("SELECT NR_BANCO AS cod_banco, COD_MULTA,VL_MULTA,CNPJ_CPF_CEDENTE AS CNPJ_CEDENTE,NM_CEDENTE AS NOME_CEDENTE,convert(int,NR_BANCO)NR_BANCO,NR_AGENCIA,DG_AGENCIA,NR_CONTA,DG_CONTA,ENDERECO_CEDENTE,CARTEIRA,CD_CEDENTE,CD_TRASMISSAO as cod_trans ,NUMERO_END_CEDENTE, BAIRRO_END_CEDENTE, UF_END_CEDENTE, CEP_END_CEDENTE, CIDADE_END_CEDENTE, COMP_END_CEDENTE, ESPECIE_TITULO,QT_DIAS_PROTESTO,COD_PROTESTO, QT_DIAS_BAIXA, COD_BAIXA,VL_MORA, COD_MORA,COD_MOV, OBS1,OBS2,SEQ_ARQUIVO,SEQUENCIA FROM TB_CONTA_BANCARIA WHERE ID_CONTA_BANCARIA = 1") '& ddlBanco.SelectedValue)
        If dsBanco.Tables(0).Rows.Count > 0 Then
            Dim dt As DataTable = ConOracle.Consultar("SELECT SEQ_ARQUIVO from Sgipa.TB_BANCO_BOLETO WHERE AUTONUM = 1 ")
            Dim SEQ_ARQUIVO As String = ""

            If dt.Rows.Count > 0 Then
                SEQ_ARQUIVO = dt.Rows(0)("SEQ_ARQUIVO").ToString
                SEQ_ARQUIVO = SEQ_ARQUIVO + 1
            End If



            Dim strToWrite As String = ""
            Dim Stream As IO.StreamWriter = Nothing
            Try
                'limpa diretorio de remessa
                Dim di As System.IO.DirectoryInfo = New DirectoryInfo(Server.MapPath("/Content/remessa"))
                For Each file As FileInfo In di.GetFiles()
                    file.Delete()
                Next
                For Each dir As DirectoryInfo In di.GetDirectories()
                    dir.Delete(True)
                Next


                Dim NomeStream As String
                NomeStream = "arquivo_remessa_" & SEQ_ARQUIVO & ".txt"
                Stream = New IO.StreamWriter(Server.MapPath("/Content/remessa/") & NomeStream, True)

                Stream.WriteLine(strToWrite)
                Stream.Flush()
                Dim seqRem As Integer = 0
                Dim seqLote As Integer = 0

                If dsBanco.Tables(0).Rows(0).Item("cod_banco") = "033" Or dsBanco.Tables(0).Rows(0).Item("cod_banco") = "104" Or dsBanco.Tables(0).Rows(0).Item("cod_banco") = "001" Then
                    Stream.WriteLine(GeraRemessa.criaHeaderSantander(dsBanco.Tables(0).Rows(0).Item("cod_banco"), dsBanco.Tables(0).Rows(0).Item("CNPJ_CEDENTE"), dsBanco.Tables(0).Rows(0).Item("NOME_CEDENTE"), dsBanco.Tables(0).Rows(0).Item("cod_trans"), SEQ_ARQUIVO))
                    seqRem = 1
                    Stream.WriteLine(GeraRemessa.criaHeaderLoteSantander(1, dsBanco.Tables(0).Rows(0).Item("cod_banco"), dsBanco.Tables(0).Rows(0).Item("CNPJ_CEDENTE"), dsBanco.Tables(0).Rows(0).Item("NOME_CEDENTE"), dsBanco.Tables(0).Rows(0).Item("cod_trans"), dsBanco.Tables(0).Rows(0).Item("obs1"), dsBanco.Tables(0).Rows(0).Item("obs2")))
                    seqRem = seqRem + 1
                    seqLote = 1
                    For i = 0 To dgvFaturamento.Rows.Count - 1

                        Dim check As CheckBox = dgvFaturamento.Rows(i).FindControl("ckSelecionar")
                        Dim ID As String = CType(dgvFaturamento.Rows(i).FindControl("lblID"), Label).Text
                        If check.Checked = True Then


                            Dim dsFatura As DataSet = Con.ExecutarQuery("SELECT NOSSONUMERO+DIG_NOSSONUM as NOSSONUM,convert(date, DT_VENCIMENTO_BOLETO,103)DT_VENCIMENTO_BOLETO,VL_BOLETO,convert(date, DT_EMISSAO_BOLETO,103)DT_EMISSAO_BOLETO, CNPJ,upper([dbo].[fnTiraAcento](NM_CLIENTE))NM_CLIENTE,upper([dbo].[fnTiraAcento](ENDERECO))ENDERECO,upper([dbo].[fnTiraAcento](BAIRRO))BAIRRO,CEP,upper([dbo].[fnTiraAcento](CIDADE))CIDADE,(SELECT SIGLA_ESTADO FROM TB_ESTADO C WHERE C.NM_ESTADO = ESTADO) AS UF,COD_BANCO, NR_NOTA_FISCAL, 'ND ' + NR_NOTA_DEBITO AS NR_NOTA_DEBITO FROM TB_FATURAMENTO WHERE ID_FATURAMENTO = " & ID)



                            Stream.WriteLine(GeraRemessa.criaDetalhePSantander(1, seqLote, dsFatura.Tables(0).Rows(0).Item("NOSSONUM"), dsFatura.Tables(0).Rows(0).Item("NR_NOTA_DEBITO"), dsFatura.Tables(0).Rows(0).Item("DT_VENCIMENTO_BOLETO"), dsFatura.Tables(0).Rows(0).Item("DT_EMISSAO_BOLETO"), dsFatura.Tables(0).Rows(0).Item("VL_BOLETO"), dsBanco.Tables(0).Rows(0).Item("cod_banco"), dsBanco.Tables(0).Rows(0).Item("CNPJ_CEDENTE"), dsBanco.Tables(0).Rows(0).Item("NOME_CEDENTE"), dsBanco.Tables(0).Rows(0).Item("cod_trans"), dsBanco.Tables(0).Rows(0).Item("COD_MOV"), dsBanco.Tables(0).Rows(0).Item("NR_AGENCIA"), dsBanco.Tables(0).Rows(0).Item("DG_AGENCIA"), dsBanco.Tables(0).Rows(0).Item("NR_CONTA"), dsBanco.Tables(0).Rows(0).Item("DG_CONTA"), dsBanco.Tables(0).Rows(0).Item("especie_titulo"), dsBanco.Tables(0).Rows(0).Item("cod_mora"), dsBanco.Tables(0).Rows(0).Item("COD_PROTESTO"), dsBanco.Tables(0).Rows(0).Item("QT_DIAS_PROTESTO"), dsBanco.Tables(0).Rows(0).Item("COD_BAIXA"), dsBanco.Tables(0).Rows(0).Item("QT_DIAS_BAIXA"), dsBanco.Tables(0).Rows(0).Item("VL_MORA")))
                            seqLote = seqLote + 1
                            seqRem = seqRem + 1
                            Stream.WriteLine(GeraRemessa.criaDetalheQSantander(1, seqLote, dsFatura.Tables(0).Rows(0).Item("CNPJ"), dsFatura.Tables(0).Rows(0).Item("NM_CLIENTE"), dsFatura.Tables(0).Rows(0).Item("ENDERECO"), dsFatura.Tables(0).Rows(0).Item("BAIRRO"), dsFatura.Tables(0).Rows(0).Item("CEP"), dsFatura.Tables(0).Rows(0).Item("CIDADE"), dsFatura.Tables(0).Rows(0).Item("UF"), dsFatura.Tables(0).Rows(0).Item("COD_BANCO"), dsBanco.Tables(0).Rows(0).Item("COD_MOV")))
                            seqLote = seqLote + 1
                            seqRem = seqRem + 1
                            If GeraRemessa.NNull(dsBanco.Tables(0).Rows(0).Item("COD_MULTA"), 1) <> "" Then
                                Stream.WriteLine(GeraRemessa.criaDetalheRSantander(1, seqLote, dsBanco.Tables(0).Rows(0).Item("COD_BANCO"), dsBanco.Tables(0).Rows(0).Item("COD_MOV"), dsBanco.Tables(0).Rows(0).Item("COD_MULTA"), dsBanco.Tables(0).Rows(0).Item("VL_MULTA")))
                                seqLote = seqLote + 1
                                seqRem = seqRem + 1
                            End If
                            Con.ExecutarQuery("UPDATE TB_FATURAMENTO SET FL_ENVIADO_REM = 1, DT_ENVIO_REM = GETDATE(), ARQ_REM ='" & NomeStream & "', USUARIO_REM ='" & Session("ID_USUARIO") & "' WHERE ID_FATURAMENTO = " & ID)

                            ConOracle.ExecuteScalar("UPDATE TB_BANCO_BOLETO SET SEQ_ARQUIVO =  " & SEQ_ARQUIVO & "WHERE AUTONUM = 1")
                        End If
                    Next i
                    seqLote = seqLote + 1
                    seqRem = seqRem + 1
                    Stream.WriteLine(GeraRemessa.criaTrailerLoteSantander(1, seqLote, dsBanco.Tables(0).Rows(0).Item("COD_BANCO")))
                    seqRem = seqRem + 1
                    Stream.WriteLine(GeraRemessa.criaTrailerSantander(1, seqRem, dsBanco.Tables(0).Rows(0).Item("COD_BANCO")))
                    Stream.Close()


                End If



                Dim _file As System.IO.FileInfo = New System.IO.FileInfo(Server.MapPath("/Content/remessa/") & NomeStream)
                If _file.Exists Then
                    Response.Clear()
                    Response.AddHeader("Content-Disposition", "attachment; filename=" & _file.Name)
                    Response.AddHeader("Content-Length", _file.Length.ToString())
                    Response.ContentType = "application/octet-stream"
                    Response.WriteFile(Server.UrlDecode(_file.FullName))
                    Response.Flush()
                    Response.Close()
                End If

                Pesquisar()
                divSuccess.Visible = True
                lblmsgSuccess.Text = "Remessa gerada com sucesso!"

            Catch ex As Exception
                divErro.Visible = True
                lblmsgErro.Text = ex.Message

            End Try


        End If
        Con.Fechar()
        ConOracle.Desconectar()
    End Sub

    Private Sub btnEnviarRemessa_Click(sender As Object, e As EventArgs) Handles btnEnviarRemessa.Click
        ArquivoRemessa()
    End Sub
End Class