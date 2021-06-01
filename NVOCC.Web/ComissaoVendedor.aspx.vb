Public Class ComissaoVendedor
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Logado") = "False" Or Session("Logado") = Nothing Then

            Response.Redirect("Login.aspx")

        End If

        Dim Con As New Conexao_sql
        Con.Conectar()
        If Not Page.IsPostBack Then
            Session("ConsultaGrid") = dsComissao.SelectCommand
        End If
        Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 2029 AND FL_ACESSAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
        If ds.Tables(0).Rows(0).Item("QTD") = 0 Then

            Response.Redirect("Default.aspx")
        Else
            If Not Page.IsPostBack Then
                txtCompetencia.Text = Now.Year & "/0" & Month(Now.AddMonths(-1))
                Session("ConsultaGrid") = dsComissao.SelectCommand
            End If

        End If
        Con.Fechar()
    End Sub

    Private Sub dgvComissoes_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgvComissoes.RowCommand
        divSuccess.Visible = False
        divErro.Visible = False


        If e.CommandName = "Selecionar" Then
            If txtlinha.Text <> "" Then
                dgvComissoes.Rows(txtlinha.Text).CssClass = "Normal"

            End If
            Dim ID As String = e.CommandArgument


            txtID.Text = ID.Substring(0, ID.IndexOf("|"))

            txtlinha.Text = ID.Substring(ID.IndexOf("|"))
            txtlinha.Text = txtlinha.Text.Replace("|", "")


            For i As Integer = 0 To dgvComissoes.Rows.Count - 1
                dgvComissoes.Rows(txtlinha.Text).CssClass = "Normal"

            Next

            dgvComissoes.Rows(txtlinha.Text).CssClass = "selected1"



        End If
    End Sub

    Private Sub btnPesquisar_Click(sender As Object, e As EventArgs) Handles btnPesquisar.Click

        txtID.Text = ""
        txtlinha.Text = ""

        Dim filtro As String = ""

        If ddlFiltro.SelectedValue = 1 Then
            filtro &= " AND PARCEIRO_VENDEDOR LIKE '%" & txtPesquisa.Text & "%'"

        ElseIf ddlFiltro.SelectedValue = 2 Then
            filtro &= " AND NR_PROCESSO LIKE '%" & txtPesquisa.Text & "%'"

        End If
        Dim COMPETENCIA As String = txtCompetencia.Text.Replace("/", "")

        dsComissao.SelectCommand = "SELECT * FROM [dbo].[View_Comissao_Vendedor] WHERE DT_COMPETENCIA = '" & COMPETENCIA & "' " & filtro & " ORDER BY PARCEIRO_VENDEDOR,NR_PROCESSO"
        dgvComissoes.DataBind()
        Session("ConsultaGrid") = dsComissao.SelectCommand
        ddlFiltro.SelectedValue = 0
        txtPesquisa.Text = ""
    End Sub

    Private Sub dgvTabelaComissao_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgvTabelaComissao.RowCommand
        DivExcluir.Visible = False
        divInfo.Visible = False
        Dim ID As String = e.CommandArgument
        Dim Con As New Conexao_sql
        Con.Conectar()
        If e.CommandName = "Excluir" Then

            Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 2029 AND FL_EXCLUIR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
            If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                lblErroExcluir.Text = "Usuário não tem permissão para realizar exclusões"
                DivExcluir.Visible = True
            Else
                Con.ExecutarQuery("DELETE FROM [dbo].[TB_TAXA_COMISSAO_VENDEDOR] WHERE ID_TAXA_COMISSAO_VENDEDORES =" & ID)
                dgvTabelaComissao.DataBind()
                divInfo.Visible = True
                lblInfo.Text = "Taxa excluída com sucesso"
                ModalPopupExtender1.Show()
            End If

        ElseIf e.CommandName = "Editar" Then

            Dim ds As DataSet = Con.ExecutarQuery("SELECT ID_TAXA_COMISSAO_VENDEDORES,DT_VALIDADE_INICIAL,VL_TAXA_LCL,VL_TAXA_FCL,VL_TAXA_INSIDE_SALES FROM TB_TAXA_COMISSAO_VENDEDOR WHERE ID_TAXA_COMISSAO_VENDEDORES = " & ID)
            If ds.Tables(0).Rows.Count > 0 Then
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_TAXA_COMISSAO_VENDEDORES")) Then
                    txtIDTabelaTaxa.Text = ds.Tables(0).Rows(0).Item("ID_TAXA_COMISSAO_VENDEDORES").ToString()
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("DT_VALIDADE_INICIAL")) Then
                    txtValidade.Text = ds.Tables(0).Rows(0).Item("DT_VALIDADE_INICIAL").ToString()
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_TAXA_LCL")) Then
                    txtLCL.Text = ds.Tables(0).Rows(0).Item("VL_TAXA_LCL").ToString()
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_TAXA_FCL")) Then
                    txtFCL.Text = ds.Tables(0).Rows(0).Item("VL_TAXA_FCL").ToString()
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_TAXA_INSIDE_SALES")) Then
                    txtInsides.Text = ds.Tables(0).Rows(0).Item("VL_TAXA_INSIDE_SALES").ToString()
                End If


            End If

            ModalPopupExtender1.Show()
        End If
        Con.Fechar()
    End Sub

    Private Sub btnGravaTaxaTabela_Click(sender As Object, e As EventArgs) Handles btnGravaTaxaTabela.Click
        divInfo.Visible = False

        txtLCL.Text = txtLCL.Text.Replace(".", "")
        txtLCL.Text = txtLCL.Text.Replace(",", ".")

        txtFCL.Text = txtFCL.Text.Replace(".", "")
        txtFCL.Text = txtFCL.Text.Replace(",", ".")

        txtInsides.Text = txtInsides.Text.Replace(".", "")
        txtInsides.Text = txtInsides.Text.Replace(",", ".")

        Dim Con As New Conexao_sql
        Con.Conectar()
        If txtIDTabelaTaxa.Text = "" Then
            Con.ExecutarQuery("INSERT INTO TB_TAXA_COMISSAO_VENDEDOR (DT_VALIDADE_INICIAL,VL_TAXA_LCL,VL_TAXA_FCL,VL_TAXA_INSIDE_SALES) VALUES (CONVERT(DATE,'" & txtValidade.Text & "',103)," & txtLCL.Text & ", " & txtFCL.Text & "," & txtInsides.Text & " ) ")
            divInfo.Visible = True
            lblInfo.Text = "Taxa inserida com sucesso"
            dgvTabelaComissao.DataBind()
            ModalPopupExtender1.Show()

        Else
            Con.ExecutarQuery("UPDATE TB_TAXA_COMISSAO_VENDEDOR SET DT_VALIDADE_INICIAL = CONVERT(DATE,'" & txtValidade.Text & "',103),VL_TAXA_LCL = " & txtLCL.Text & ",VL_TAXA_FCL = " & txtFCL.Text & ",VL_TAXA_INSIDE_SALES = " & txtInsides.Text & " WHERE ID_TAXA_COMISSAO_VENDEDORES = " & txtIDTabelaTaxa.Text)
            divInfo.Visible = True
            lblInfo.Text = "Taxa alterada com sucesso"
            dgvTabelaComissao.DataBind()
            ModalPopupExtender1.Show()

        End If
    End Sub

    Private Sub btnFecharTabela_Click(sender As Object, e As EventArgs) Handles btnFecharTabela.Click
        txtIDTabelaTaxa.Text = ""
        txtValidade.Text = ""
        txtLCL.Text = ""
        txtFCL.Text = ""
        txtInsides.Text = ""
        divInfo.Visible = False

    End Sub

    Private Sub lkCSV_Click(sender As Object, e As EventArgs) Handles lkCSV.Click
        Dim SQL As String = Session("ConsultaGrid")
        Classes.Excel.exportaExcel(SQL, "NVOCC", "ComissaoVendedor")
    End Sub

    Private Sub txtLiquidacaoInicial_TextChanged(sender As Object, e As EventArgs) Handles txtLiquidacaoInicial.TextChanged
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet = Con.ExecutarQuery("SELECT TOP 1 ID_TAXA_COMISSAO_VENDEDORES,VL_TAXA_FCL,VL_TAXA_LCL,VL_TAXA_INSIDE_SALES FROM TB_TAXA_COMISSAO_VENDEDOR WHERE DT_VALIDADE_INICIAL = CONVERT(DATETIME,'" & txtLiquidacaoInicial.Text & "',103)")
        If ds.Tables(0).Rows.Count > 0 Then
            If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_TAXA_FCL")) Then
                lblFCL.Text = ds.Tables(0).Rows(0).Item("VL_TAXA_FCL")
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_TAXA_LCL")) Then
                lblLCL.Text = ds.Tables(0).Rows(0).Item("VL_TAXA_LCL")
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_TAXA_INSIDE_SALES")) Then
                lblInside.Text = ds.Tables(0).Rows(0).Item("VL_TAXA_INSIDE_SALES")
            End If
        End If
        ModalPopupExtender3.Show()
    End Sub

    Private Sub lkRelPorVendedor_Click(sender As Object, e As EventArgs) Handles lkRelPorVendedor.Click
        divErro.Visible = False
        divSuccess.Visible = False

        If txtID.Text = "" Then
            divErro.Visible = True
            lblmsgErro.Text = "Selecione um registro"
        Else
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "text", "RelPorVendedor()", True)

        End If

    End Sub

    Private Sub btnGerarComissao_Click(sender As Object, e As EventArgs) Handles btnGerarComissao.Click
        Dim Con As New Conexao_sql
        Con.Conectar()


        Dim ds As DataSet = Con.ExecutarQuery("SELECT DT_COMPETENCIA FROM TB_CABECALHO_COMISSAO_VENDEDOR WHERE DT_COMPETENCIA = '" & txtNovaCompetencia.Text & "' AND DT_EXPORTACAO IS NULL")
        If ds.Tables(0).Rows.Count > 0 Then
            divInfoGerarComissao.Visible = True
            lblInfoGerarComissao.Text = "Competência imediatamente anterior não exportada para a conta corrente do processo."
        Else
            divSuccessGerarComissao.Visible = False
            divErroGerarComissao.Visible = False
            divInfoGerarComissao.Visible = False

        End If

        ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 2029 AND FL_CADASTRAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
        If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
            lblErroExcluir.Text = "Usuário não tem permissão!"
            DivExcluir.Visible = True
        Else
            Dim dsInsert As DataSet = Con.ExecutarQuery("INSERT INTO TB_CABECALHO_COMISSAO_VENDEDOR (DT_COMPETENCIA,ID_USUARIO_GERACAO,DT_GERACAO) VALUES(" & txtNovaCompetencia.Text & "," & Session("ID_USUARIO") & ", getdate() ) Select SCOPE_IDENTITY() as ID_CABECALHO_COMISSAO_VENDEDOR ")
            Dim cabecalho As String = dsInsert.Tables(0).Rows(0).Item("ID_CABECALHO_COMISSAO_VENDEDOR")

            Con.ExecutarQuery("INSERT INTO TB_DETALHE_COMISSAO_VENDEDOR (NR_PROCESSO,NR_NOTAS_FISCAL,DT_NOTA_FISCAL,ID_SERVICO,ID_PARCEIRO_VENDEDOR,ID_PARCEIRO_CLIENTE,ID_TIPO_ESTUFAGEM,QT_CNTR,QT_BL,VL_COMISSAO_BASE,VL_COMISSAO_TOTAL,DT_LIQUIDACAO) VALUES() ")

        End If


    End Sub

    Private Sub txtNovaCompetencia_TextChanged(sender As Object, e As EventArgs) Handles txtNovaCompetencia.TextChanged
        Dim Con As New Conexao_sql
        Con.Conectar()

        Dim ds As DataSet = Con.ExecutarQuery("Select DT_COMPETENCIA FROM TB_CABECALHO_COMISSAO_VENDEDOR WHERE DT_COMPETENCIA = '" & txtNovaCompetencia.Text & "'")
        If ds.Tables(0).Rows.Count > 0 Then

            divInfoGerarComissao.Visible = True
            lblInfoGerarComissao.Text = "COMPETENCIA JÁ EXISTE!<br/> Prosseguir com esta ação ocasionará a sobreposição dos dados."
        Else
            divSuccessGerarComissao.Visible = False
            divErroGerarComissao.Visible = False
            divInfoGerarComissao.Visible = False

        End If
    End Sub
End Class