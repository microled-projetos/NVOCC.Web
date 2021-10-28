Imports DGVPrinterHelper
Imports System.Drawing.Text
Imports DGVPrinterHelper.DGVPrinter

Public Class FrmImpressao

    Dim Imprimir As New DGVPrinter
    Dim DGV As DataGridView
    Dim Titulo As String

    Private Sub CarregarColunas(ByVal _DGV As DataGridView)

        Me.lstColunas.Items.Clear()

        For Each Coluna As DataGridViewColumn In _DGV.Columns
            If Coluna.Visible Then
                Me.lstColunas.Items.Add(Coluna.HeaderText, True)
            End If
        Next

    End Sub

    Private Sub CarregarFontes()

        Using Fontes As New InstalledFontCollection
            For Each Fonte As FontFamily In Fontes.Families
                cbFonteTitulo.Items.Add(Fonte.Name)
                cbFonteSubTitulo.Items.Add(Fonte.Name)
                cbFonteRodape.Items.Add(Fonte.Name)
            Next
        End Using

        If cbFonteTitulo.Items.Count > 0 Then
            If cbFonteTitulo.FindString("Arial") Then
                cbFonteTitulo.SelectedIndex = cbFonteTitulo.FindString("Arial")
            End If
        End If

        If cbFonteSubTitulo.Items.Count > 0 Then
            If cbFonteSubTitulo.FindString("Arial") Then
                cbFonteSubTitulo.SelectedIndex = cbFonteSubTitulo.FindString("Arial")
            End If
        End If

        If cbFonteRodape.Items.Count > 0 Then
            If cbFonteRodape.FindString("Arial") Then
                cbFonteRodape.SelectedIndex = cbFonteRodape.FindString("Arial")
            End If
        End If

    End Sub

    Private Function CopyDataGridView(dgv_org As DataGridView) As DataGridView

        Dim dgv_copy As New DataGridView()
        Try
            If dgv_copy.Columns.Count = 0 Then
                For Each dgvc As DataGridViewColumn In dgv_org.Columns
                    dgv_copy.Columns.Add(TryCast(dgvc.Clone(), DataGridViewColumn))
                Next
            End If

            Dim row As New DataGridViewRow()

            For i As Integer = 0 To dgv_org.Rows.Count - 1
                row = DirectCast(dgv_org.Rows(i).Clone(), DataGridViewRow)
                Dim intColIndex As Integer = 0
                For Each cell As DataGridViewCell In dgv_org.Rows(i).Cells
                    row.Cells(intColIndex).Value = cell.Value
                    intColIndex += 1
                Next
                dgv_copy.Rows.Add(row)
            Next
            dgv_copy.AllowUserToAddRows = False

            dgv_copy.Refresh()
        Catch ex As Exception
        End Try
        Return dgv_copy
    End Function


    Public Sub New(ByVal _Titulo As String, ByVal _DGV As DataGridView, ByVal QuemChamou As Form)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        CarregarColunas(_DGV)
        CarregarFontes()

        Me.DGV = CopyDataGridView(_DGV)
        Me.Titulo = _Titulo
        Me.txtTitulo.Text = _Titulo

    End Sub

    Private Sub btnVisualizarImpressao_Click(sender As System.Object, e As System.EventArgs) Handles btnVisualizarImpressao.Click

        Dim Logo As New DGVPrinter.ImbeddedImage

        Dim Result As Object = Nothing
        Result = Banco.ObterImagemEmpresa()

        If Result IsNot Nothing Then

            Logo.theImage = Result

            Dim Imagem As New List(Of DGVPrinterHelper.DGVPrinter.ImbeddedImage)
            Imagem.Add(Logo)

            Imprimir.ImbeddedImageList = Imagem
            Imprimir.ImbeddedImageList(0).ImageX = 60

        End If

        Imprimir.Title = txtTitulo.Text
        Imprimir.SubTitle = txtSubTitulo.Text
        Imprimir.Footer = txtRodape.Text

        Imprimir.SubTitleSpacing = 60

        If cbFonteTitulo.SelectedIndex >= 0 And txtTamTitulo.Value > 0 Then
            Imprimir.TitleFont = New Font(cbFonteTitulo.Text, txtTamTitulo.Value)
        End If

        If cbFonteSubTitulo.SelectedIndex >= 0 And txtTamSubTitulo.Value > 0 Then
            Imprimir.SubTitleFont = New Font(cbFonteSubTitulo.Text, txtTamSubTitulo.Value)
        End If

        If cbFonteRodape.SelectedIndex >= 0 And txtTamRodape.Value > 0 Then
            Imprimir.FooterFont = New Font(cbFonteRodape.Text, txtTamRodape.Value)
        End If

        Imprimir.TitleColor = txtTitulo.ForeColor
        Imprimir.SubTitleColor = txtSubTitulo.ForeColor
        Imprimir.FooterColor = txtRodape.ForeColor

        If txtTitulo.TextAlign = HorizontalAlignment.Center Then
            Imprimir.TitleAlignment = StringAlignment.Center
        End If

        If txtTitulo.TextAlign = HorizontalAlignment.Left Then
            Imprimir.TitleAlignment = StringAlignment.Near
        End If

        If txtTitulo.TextAlign = HorizontalAlignment.Right Then
            Imprimir.TitleAlignment = StringAlignment.Far
        End If

        If txtSubTitulo.TextAlign = HorizontalAlignment.Center Then
            Imprimir.SubTitleAlignment = StringAlignment.Center
        End If

        If txtSubTitulo.TextAlign = HorizontalAlignment.Left Then
            Imprimir.SubTitleAlignment = StringAlignment.Near
        End If

        If txtSubTitulo.TextAlign = HorizontalAlignment.Right Then
            Imprimir.SubTitleAlignment = StringAlignment.Far
        End If

        If txtRodape.TextAlign = HorizontalAlignment.Center Then
            Imprimir.FooterAlignment = StringAlignment.Center
        End If

        If txtRodape.TextAlign = HorizontalAlignment.Left Then
            Imprimir.FooterAlignment = StringAlignment.Near
        End If

        If txtRodape.TextAlign = HorizontalAlignment.Right Then
            Imprimir.FooterAlignment = StringAlignment.Far
        End If

        Imprimir.SubTitleFormatFlags = StringFormatFlags.LineLimit
        Imprimir.PageNumbers = chkMostrarNum.CheckState
        Imprimir.PageNumberInHeader = chkMostrarNumCabecalho.CheckState
        Imprimir.PorportionalColumns = chkAjustarColunas.CheckState
        Imprimir.HeaderCellAlignment = StringAlignment.Near

        Dim Count As Integer = Me.lstColunas.Items.Count - 1

        For i As Integer = 0 To Count
            If Me.lstColunas.GetItemChecked(i) Then
                Me.DGV.Columns(i).Visible = True
            Else
                Me.DGV.Columns(i).Visible = False
            End If
        Next

        Me.DGV.Refresh()

        'For Each Coluna As DataGridViewColumn In Me.DGV.Columns
        '    If lstColunas.Items(Coluna.Index).ToString().Equals(Coluna.HeaderText) Then
        '        If lstColunas.GetItemChecked(Coluna.Index) Then
        '            Coluna.Visible = True
        '        Else
        '            Coluna.Visible = False
        '        End If
        '    End If
        'Next

        Imprimir.RowHeight = RowHeightSetting.CellHeight
        Imprimir.PageSettings.Landscape = rbPaisagem.Checked
        Imprimir.PrintPreviewNoDisplay(DGV)

        Me.Hide()

    End Sub

    Private Sub pbCorTitulo_Click(sender As System.Object, e As System.EventArgs) Handles pbCorTitulo.Click

        Dim cDialog As New ColorDialog()

        If cDialog.ShowDialog() = DialogResult.OK Then
            txtTitulo.ForeColor = cDialog.Color
        End If

    End Sub

    Private Sub pbCorSubTitulo_Click(sender As System.Object, e As System.EventArgs) Handles pbCorSubTitulo.Click

        Dim cDialog As New ColorDialog()

        If cDialog.ShowDialog() = DialogResult.OK Then
            txtSubTitulo.ForeColor = cDialog.Color
        End If

    End Sub

    Private Sub pbCorRodape_Click(sender As System.Object, e As System.EventArgs) Handles pbCorRodape.Click

        Dim cDialog As New ColorDialog()

        If cDialog.ShowDialog() = DialogResult.OK Then
            txtRodape.ForeColor = cDialog.Color
        End If

    End Sub

    Private Sub pbTituloEsq_Click(sender As System.Object, e As System.EventArgs) Handles pbTituloEsq.Click
        txtTitulo.TextAlign = HorizontalAlignment.Left
        pbTituloEsq.BackColor = Color.Maroon
        pnTituloCen.BackColor = Color.Gray
        pbTituloDir.BackColor = Color.Gray
    End Sub

    Private Sub pnTituloCen_Click(sender As System.Object, e As System.EventArgs) Handles pnTituloCen.Click
        txtTitulo.TextAlign = HorizontalAlignment.Center
        pbTituloEsq.BackColor = Color.Gray
        pnTituloCen.BackColor = Color.Maroon
        pbTituloDir.BackColor = Color.Gray
    End Sub

    Private Sub pbTituloDir_Click(sender As System.Object, e As System.EventArgs) Handles pbTituloDir.Click
        txtTitulo.TextAlign = HorizontalAlignment.Right
        pbTituloEsq.BackColor = Color.Gray
        pnTituloCen.BackColor = Color.Gray
        pbTituloDir.BackColor = Color.Maroon
    End Sub

    Private Sub pbStDir_Click(sender As System.Object, e As System.EventArgs) Handles pbStDir.Click
        txtSubTitulo.TextAlign = HorizontalAlignment.Right
        pbStEsq.BackColor = Color.Gray
        pbStCen.BackColor = Color.Gray
        pbStDir.BackColor = Color.Maroon
    End Sub

    Private Sub pbStCen_Click(sender As System.Object, e As System.EventArgs) Handles pbStCen.Click
        txtSubTitulo.TextAlign = HorizontalAlignment.Center
        pbStEsq.BackColor = Color.Gray
        pbStCen.BackColor = Color.Maroon
        pbStDir.BackColor = Color.Gray
    End Sub

    Private Sub pbStEsq_Click(sender As System.Object, e As System.EventArgs) Handles pbStEsq.Click
        txtSubTitulo.TextAlign = HorizontalAlignment.Left
        pbStEsq.BackColor = Color.Maroon
        pbStCen.BackColor = Color.Gray
        pbStDir.BackColor = Color.Gray
    End Sub

    Private Sub pbRodDir_Click(sender As System.Object, e As System.EventArgs) Handles pbRodDir.Click
        txtRodape.TextAlign = HorizontalAlignment.Right
        pbRodEsq.BackColor = Color.Gray
        pbRodCen.BackColor = Color.Gray
        pbRodDir.BackColor = Color.Maroon
    End Sub

    Private Sub pbRodCen_Click(sender As System.Object, e As System.EventArgs) Handles pbRodCen.Click
        txtRodape.TextAlign = HorizontalAlignment.Center
        pbRodEsq.BackColor = Color.Gray
        pbRodCen.BackColor = Color.Maroon
        pbRodDir.BackColor = Color.Gray
    End Sub

    Private Sub pbRodEsq_Click(sender As System.Object, e As System.EventArgs) Handles pbRodEsq.Click
        txtRodape.TextAlign = HorizontalAlignment.Left
        pbRodEsq.BackColor = Color.Maroon
        pbRodCen.BackColor = Color.Gray
        pbRodDir.BackColor = Color.Gray
    End Sub

    Private Sub btnRestaurarPadroes_Click(sender As System.Object, e As System.EventArgs) Handles btnRestaurarPadroes.Click

        Me.txtTitulo.Text = Titulo
        Me.txtTamTitulo.Value = 14
        Me.txtTamSubTitulo.Value = 12
        Me.txtTamRodape.Value = 12

        Me.txtTitulo.ForeColor = Color.Black
        Me.txtSubTitulo.ForeColor = Color.Black
        Me.txtRodape.ForeColor = Color.Black

        Me.txtTitulo.TextAlign = HorizontalAlignment.Left
        Me.txtSubTitulo.TextAlign = HorizontalAlignment.Left
        Me.txtRodape.TextAlign = HorizontalAlignment.Left

        CarregarFontes()
        CarregarColunas(DGV)

        pbTituloEsq.BackColor = Color.Maroon
        pnTituloCen.BackColor = Color.Gray
        pbTituloDir.BackColor = Color.Gray

        pbStEsq.BackColor = Color.Maroon
        pbStCen.BackColor = Color.Gray
        pbStDir.BackColor = Color.Gray

        pbRodEsq.BackColor = Color.Maroon
        pbRodCen.BackColor = Color.Gray
        pbRodDir.BackColor = Color.Gray

        chkAjustarColunas.Checked = True
        chkMostrarNum.Checked = True
        chkMostrarNumCabecalho.Checked = False

    End Sub

    Private Sub btnSair_Click(sender As System.Object, e As System.EventArgs) Handles btnSair.Click
        Me.Hide()
    End Sub

    Private Sub FrmImpressao_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If

    End Sub

End Class
