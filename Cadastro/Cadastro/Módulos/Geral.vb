Imports Touchless.Vision.Camera
Imports System.IO
Imports System.Text.RegularExpressions
Imports DgvFilterPopup

Module Geral

    Public wAtendimento As Boolean
    Public SavedFilters As New Dictionary(Of String, String)
    Public Cod_Usuario As Integer
    Public Usuario_Sistema As String
    Public Cod_Empresa As Integer

    Public Function NNull(ByVal Valor As String, ByVal Tipo As Integer) As String
        If Valor <> Nothing Then
            If Tipo = 0 Then
                If Valor.ToString() = "" Then Valor = "0"
            ElseIf Tipo = 1 Then
                If Valor.ToString() = "" Then Valor = ""
            ElseIf Tipo = 2 Then
                If Not IsDate(Valor.ToString) Then Valor = ""
            End If
        Else
            If Tipo = 0 Then Valor = 0
            If Tipo = 1 Or Tipo = 2 Then Valor = ""

        End If
        Return Valor
    End Function

    Public Function DirectoryExists(ByVal sDirectory As String, ByRef sError As String, Optional ByRef fActuallyDoesntExist As Boolean = False) As Boolean

        If Not IO.Directory.Exists(sDirectory) Then
            Try
                Dim dtCreated As Date
                dtCreated = Directory.GetCreationTime(sDirectory)
                fActuallyDoesntExist = True
                sError = "Diretório inexistente."
            Catch Ex As Exception
                sError = Ex.Message
            End Try

            Return False
        Else
            Return True
        End If
    End Function

    Public Sub LoadFilters(ByVal Filtro As DgvFilterManager)
        If Not SavedFilters Is Nothing Then
            For Each pair As KeyValuePair(Of String, String) In SavedFilters
                Filtro(pair.Key.ToString()).FilterExpression = pair.Value.ToString()
                Filtro(pair.Key.ToString()).FilterCaption = pair.Value.ToString()
                Filtro(pair.Key.ToString()).Active = True
                Filtro(pair.Key.ToString()).FilterApplySoon = True
                Filtro.RebuildFilter()
            Next
        End If
    End Sub

    Public Function RemoverCaracterEspecial(ByVal Valor As String) As String
        Return Regex.Replace(Valor, "'", "")
    End Function

    Public Function ValidarFormatoEmails(ByVal Valor As String) As String
        Return Regex.IsMatch(Valor, "^[_a-z0-9-]+(.[_a-z0-9-]+)*@[a-z0-9-]+(.[a-z0-9-]+)*(.[a-z]{2,4})$")
    End Function

    Public Function AjustaNumeroTelefone(ByVal Valor As String) As String

        If Valor.Length > 1 Then

            Valor = RemoverCaracterEspecial(Valor)

            If IsNumeric(Valor) Then
                If Valor.Trim() <> String.Empty Then
                    If Mid(Valor.ToString(), 1, 1) = 0 Then
                        Valor = Valor.Remove(0, 1)
                    End If
                End If
            End If

            Return Valor.ToUpper().Replace("-", "").Replace("X", "").Replace(" ", "")

        End If

        Return String.Empty

    End Function

    Public Sub FundoTextBox(ByRef Tela As Control, Optional NaoValidar As String() = Nothing)

        Dim Objeto As Control

        For Each Objeto In Tela.Controls

            If TypeOf Objeto Is System.Windows.Forms.GroupBox Or
                    TypeOf Objeto Is System.Windows.Forms.TabPage Or
                    TypeOf Objeto Is System.Windows.Forms.Panel Then
                FundoTextBox(Objeto)
            End If

            If TypeOf Objeto Is System.Windows.Forms.TextBox Then
                If Objeto.BackColor.ToArgb = -1 Or Objeto.BackColor = Color.White Then
                    AddHandler Objeto.GotFocus, AddressOf Text_GotFocus
                    AddHandler Objeto.LostFocus, AddressOf Text_LostFocus
                End If
            End If

            If Objeto.HasChildren Then
                FundoTextBox(Objeto)
            End If

        Next

    End Sub

    Private Sub Text_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim Text As TextBox = sender
        Text.BackColor = Color.LightYellow
    End Sub

    Private Sub Text_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim txt As TextBox = sender
        txt.BackColor = Color.White

        If txt.Text <> String.Empty Then
            txt.Text = RemoverCaracterEspecial(txt.Text)
        End If

    End Sub

    Public Sub CampoNumerico(ByVal o As TextBox)

        Dim Objeto As TextBox = o
        AddHandler Objeto.KeyPress, AddressOf Text_Numeric

    End Sub

    Private Sub Text_Numeric(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

        If Not Char.IsNumber(e.KeyChar) And Not e.KeyChar = vbBack Then
            e.Handled = True
        End If

    End Sub

    Public Sub CampoMoeda(ByRef _TextBox As TextBox)


        Dim Texto As String = String.Empty
        Dim Valor As Double = 0

        Try
            Texto = _TextBox.Text.Replace(",", "").Replace(".", "")
            If Texto.Equals("") Then Texto = "000"
            Texto = Texto.PadLeft(3, "0")
            If Texto.Length > 3 And Texto.Substring(0, 1) = "0" Then Texto = Texto.Substring(1, Texto.Length - 1)
            Valor = Convert.ToDouble(Texto) / 100
            _TextBox.Text = String.Format("{0:N}", Valor)
            _TextBox.SelectionStart = _TextBox.Text.Length
        Catch ex As Exception
            _TextBox.Text = 0
        End Try

    End Sub

    Public Sub CampoMoedaCom3CasasDecimais(ByRef _TextBox As TextBox)

        Dim Texto As String = String.Empty
        Dim Valor As Double = 0

        Try
            Texto = _TextBox.Text.Replace(",", "").Replace(".", "")
            If Texto.Equals("") Then Texto = "0000"
            Texto = Texto.PadLeft(4, "0")
            If Texto.Length > 4 And Texto.Substring(0, 1) = "0" Then Texto = Texto.Substring(1, Texto.Length - 1)
            Valor = Convert.ToDouble(Texto) / 1000
            _TextBox.Text = String.Format("{0:N3}", Valor)
            _TextBox.SelectionStart = _TextBox.Text.Length
        Catch ex As Exception
            MessageBox.Show("Campo Inválido")
            _TextBox.Text = ""
        End Try

    End Sub

    Public Sub CampoDecimal(ByVal sender As Object, ByVal e As KeyPressEventArgs)

        If Not Char.IsNumber(e.KeyChar) And Not e.KeyChar = vbBack And Not e.KeyChar = "," And Not e.KeyChar = "." Then
            e.Handled = True
        End If

    End Sub

    Public Sub HabilitarCampos(ByRef Tela As Control, ByVal Habilita As Boolean)

        For Each ctr As Control In Tela.Controls
            If TypeOf ctr Is TextBox Then
                TryCast(ctr, TextBox).Enabled = Habilita
            End If
            If TypeOf ctr Is ComboBox Then
                TryCast(ctr, ComboBox).Enabled = Habilita
            End If
            If TypeOf ctr Is CheckBox Then
                TryCast(ctr, CheckBox).Enabled = Habilita
            End If
            If TypeOf ctr Is RadioButton Then
                TryCast(ctr, RadioButton).Enabled = Habilita
            End If
            If TypeOf ctr Is DateTimePicker Then
                TryCast(ctr, DateTimePicker).Enabled = Habilita
            End If
            If TypeOf ctr Is RichTextBox Then
                TryCast(ctr, RichTextBox).Enabled = Habilita
            End If
            If TypeOf ctr Is ListBox Then
                TryCast(ctr, ListBox).Enabled = Habilita
            End If
            If TypeOf ctr Is MaskedTextBox Then
                TryCast(ctr, MaskedTextBox).Enabled = Habilita
            End If
            If TypeOf ctr Is GroupBox Then
                TryCast(ctr, GroupBox).Enabled = Habilita
            End If
            If TypeOf ctr Is LinkLabel Then
                TryCast(ctr, LinkLabel).Enabled = Habilita
            End If
            If ctr.HasChildren Then
                HabilitarCampos(ctr, Habilita)
            End If
        Next

    End Sub

    Public Sub LimparCampos(ByRef Tela As Control)

        For Each ctr As Control In Tela.Controls
            If TypeOf ctr Is TextBox Then
                TryCast(ctr, TextBox).Text = String.Empty
                TryCast(ctr, TextBox).BackColor = Color.White
            End If
            If TypeOf ctr Is ComboBox Then
                TryCast(ctr, ComboBox).SelectedIndex = -1
                TryCast(ctr, ComboBox).BackColor = Color.White
            End If
            If TypeOf ctr Is CheckBox Then
                TryCast(ctr, CheckBox).Checked = False
            End If
            If TypeOf ctr Is RadioButton Then
                TryCast(ctr, RadioButton).Checked = False
            End If
            If TypeOf ctr Is DateTimePicker Then
                TryCast(ctr, DateTimePicker).Text = String.Empty
                TryCast(ctr, DateTimePicker).BackColor = Color.White
            End If
            If TypeOf ctr Is RichTextBox Then
                TryCast(ctr, RichTextBox).Text = String.Empty
                TryCast(ctr, RichTextBox).BackColor = Color.White
            End If
            If TypeOf ctr Is MaskedTextBox Then
                TryCast(ctr, MaskedTextBox).Text = String.Empty
                TryCast(ctr, MaskedTextBox).BackColor = Color.White
            End If
            If TypeOf ctr Is DataGridView Then
                TryCast(ctr, DataGridView).ClearSelection()
            End If
            If ctr.HasChildren Then
                LimparCampos(ctr)
            End If
        Next

    End Sub

    Public Function ValidarCampos(ByRef Tela As Control) As Boolean

        Dim Erros As Boolean = False

        For Each ctr As Control In Tela.Controls
            If TypeOf ctr Is TextBox Then
                If TryCast(ctr, TextBox).Tag = "requerido" Then
                    If TryCast(ctr, TextBox).Text = String.Empty Then
                        TryCast(ctr, TextBox).BackColor = Color.MistyRose
                        Erros = True
                    Else
                        TryCast(ctr, TextBox).BackColor = Color.White
                    End If
                End If
            End If
            If TypeOf ctr Is ComboBox Then
                If TryCast(ctr, ComboBox).Tag = "requerido" Then
                    If TryCast(ctr, ComboBox).Text = String.Empty Then
                        TryCast(ctr, ComboBox).BackColor = Color.MistyRose
                        Erros = True
                    Else
                        TryCast(ctr, ComboBox).BackColor = Color.White
                    End If
                End If
            End If
            If TypeOf ctr Is MaskedTextBox Then
                If TryCast(ctr, MaskedTextBox).Tag = "requerido" Then
                    If TryCast(ctr, MaskedTextBox).MaskFull = False Or TryCast(ctr, MaskedTextBox).MaskFull = False Then
                        TryCast(ctr, MaskedTextBox).BackColor = Color.MistyRose
                        Erros = True
                    Else
                        TryCast(ctr, MaskedTextBox).BackColor = Color.White
                    End If
                End If
            End If

            If ctr.HasChildren Then
                If Not ValidarCampos(ctr) Then
                    Return False
                End If
            End If

        Next

        If Not Erros Then
            Return True
        End If

        If Erros Then
            MessageBox.Show("Atenção: Os campos destacados deverão ser preenchidos corretamente.", Tela.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

        Return False

    End Function

    Public Function Mensagens(ByVal Tela As Control, ByVal Tipo As Integer, Optional ByVal Custom As String = "") As String

        Select Case Tipo
            Case 1
                Return MessageBox.Show("Registro Incluído com sucesso! " & Custom, Tela.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Case 2
                Return MessageBox.Show("Registro Atualizado com sucesso! " & Custom, Tela.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Case 3
                Return MessageBox.Show("Registro Excluído com sucesso! " & Custom, Tela.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Case 4
                Return MessageBox.Show("Erro durante a Inclusão do registro. Tente novamente. " & Custom, Tela.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Case 5
                Return MessageBox.Show("Erro durante a Atualização do registro. Tente novamente. " & Custom, Tela.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Case 6
                Return MessageBox.Show("Erro durante a Exclusão do registro. Tente novamente. " & Custom, Tela.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Case 7
                Return MessageBox.Show("Atenção: Já existe outro registro cadastrado no sistema com os mesmos dados informados. " & Custom, Tela.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Select

        Return String.Empty

    End Function

    Public Function VerificarDadosRepetidos(ByVal DataGrid As DataGridView, ByVal Coluna As Integer, ByVal Expressao As String) As Boolean

        For Each Linha As DataGridViewRow In DataGrid.Rows
            If IsNumeric(Expressao) And IsNumeric(Linha.Cells(Coluna).Value.ToString()) Then
                If Convert.ToInt64(Linha.Cells(Coluna).Value.ToString()) = Convert.ToInt64(Expressao) Then
                    Return True
                End If
            Else
                If Linha.Cells(Coluna).Value.Equals(Expressao) Then
                    Return True
                End If
            End If
        Next

        Return False

    End Function

    Private _Arquivo As String

    Public Property Arquivo() As String
        Get
            Return _Arquivo
        End Get
        Set(ByVal value As String)
            _Arquivo = value
        End Set
    End Property

    Public Function LerImagemDoArquivo() As Bitmap

        Dim Tamanho As Long = 0
        Dim Bytes As Byte() = Nothing

        Dim Imagem As New System.IO.FileInfo(Arquivo)
        Tamanho = Imagem.Length
        Bytes = New Byte(Convert.ToInt32(Tamanho) - 1) {}
        Dim fs As New System.IO.FileStream(Arquivo, IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.Read)
        fs.Read(Bytes, 0, Convert.ToInt32(Tamanho))
        fs.Close()
        Return New Bitmap(New IO.MemoryStream(Bytes))

        Return Nothing

    End Function

    Public Function ImagemParaBytes(ByVal Arquivo As String) As Byte()

        Dim Tamanho As Long = 0
        Dim Bytes As Byte() = Nothing

        Dim Imagem As New System.IO.FileInfo(Arquivo)
        Tamanho = Imagem.Length
        Bytes = New Byte(Convert.ToInt32(Tamanho) - 1) {}
        Dim fs As New System.IO.FileStream(Arquivo, IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.Read)
        fs.Read(Bytes, 0, Convert.ToInt32(Tamanho))
        fs.Close()
        Return Bytes

    End Function

    Public Sub LimparDataGridView(ByVal Ds As DataTable, ByVal Dgv As DataGridView, ByVal Pb As PictureBox)

        Ds.Clear()
        Dgv.Refresh()

        If Pb IsNot Nothing Then
            Pb.Visible = True
        End If

    End Sub

    Public Sub AjustarLarguraComboBox(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim senderComboBox As ComboBox = DirectCast(sender, ComboBox)
        Dim width As Integer = senderComboBox.DropDownWidth
        Dim g As Graphics = senderComboBox.CreateGraphics()
        Dim font As Font = senderComboBox.Font
        Dim vertScrollBarWidth As Integer = If((senderComboBox.Items.Count > senderComboBox.MaxDropDownItems), SystemInformation.VerticalScrollBarWidth, 0)

        Dim newWidth As Integer

        For Each item As Object In DirectCast(sender, ComboBox).Items
            Dim s As String = senderComboBox.GetItemText(item)
            newWidth = CInt(g.MeasureString(s, font).Width) + vertScrollBarWidth
            If width < newWidth Then
                width = newWidth
            End If
        Next

        senderComboBox.DropDownWidth = width

    End Sub

    Public Function ValidarEmail(ByVal email As String) As Boolean

        Dim validEmail As Boolean = False
        Dim indexArr As Integer = email.IndexOf("@"c)

        If indexArr > 0 Then
            Dim indexDot As Integer = email.IndexOf("."c, indexArr)
            If indexDot - 1 > indexArr Then
                If indexDot + 1 < email.Length Then
                    Dim indexDot2 As String = email.Substring(indexDot + 1, 1)
                    If indexDot2 <> "." Then
                        validEmail = True
                    End If
                End If
            End If
        End If

        Return validEmail

    End Function

    Public Sub ComboNull(ByRef Combo As ComboBox, ByVal Valor As Object)

        If Valor IsNot Nothing Then

            Combo.SelectedIndex = -1
            Dim valorItem As Object = Nothing

            For Each item As Object In Combo.Items
                valorItem = DirectCast(item, KeyValuePair(Of String, String)).Key
                If valorItem = Valor.ToString() Then
                    Combo.SelectedValue = valorItem
                End If
            Next

            If Combo.SelectedValue Is Nothing Then
                For Each item As Object In Combo.Items
                    valorItem = DirectCast(item, KeyValuePair(Of String, String)).Value
                    If valorItem.ToString() = Valor.ToString() Then
                        Combo.Text = valorItem
                    End If
                Next
            End If

        End If

    End Sub

    'Public Function Nnull(ByVal Par As Object, ByVal Tipo As Integer) As String

    '    If Par IsNot Nothing Then
    '        If Not String.IsNullOrEmpty(Par.ToString()) Then
    '            Return Par.ToString()
    '        End If
    '    End If

    '    If Tipo = 1 Then
    '        Return String.Empty
    '    End If

    '    Return "0"

    'End Function

    Public Function RemoveCaracteres(TEXTO As String) As String

        TEXTO = Replace(TEXTO, "-", String.Empty)
        TEXTO = Replace(TEXTO, ".", String.Empty)
        TEXTO = Replace(TEXTO, ",", String.Empty)
        TEXTO = Replace(TEXTO, "/", String.Empty)
        TEXTO = Replace(TEXTO, "\", String.Empty)
        TEXTO = Replace(TEXTO, "_", " ")

        Return TEXTO

    End Function

    Public Function PPonto(ByVal Valor As String) As String
        Return Replace(Replace(NNull(Valor, 0), ".", ""), ",", ".")
    End Function

    Public Function Clip(TEXTO) As String
        If TEXTO <> "" Then
            Clip = Replace(Replace(Replace(Replace(TEXTO, "-", ""), "_", ""), ".", ""), "/", "")
        Else
            Clip = ""
        End If
    End Function

    Public Function VirgulaPonto(ByVal Valor As String) As String
        Return Replace(Replace(Valor, ".", ""), ",", ".")
    End Function

    Public Function PRSet(ByRef ObjetoTabela As ADODB.Recordset, ByVal Instrução As String, Optional ByVal Modo As Integer = 0, Optional ByVal Carregar As Boolean = False, Optional ByVal LadoCliente As Boolean = True) As Boolean

        Dim Con As New ADODB.Connection

        Dim sSTR As String

        If Not ObjetoTabela Is Nothing Then  'Encerra-se o recordset em caso de erros
            If Not ObjetoTabela.State = 0 Then ObjetoTabela.Close()
            ObjetoTabela = Nothing
        End If

        If Con IsNot Nothing Then
            If Con.State = 0 Then
                Con.ConnectionString = Banco.ConnectionString
                Con.Open()
            End If
        End If

        ObjetoTabela = New ADODB.Recordset
        ObjetoTabela.ActiveConnection = Con

        If LadoCliente = True Then
            ObjetoTabela.CursorLocation = ADODB.CursorLocationEnum.adUseClient
        Else
            ObjetoTabela.CursorLocation = ADODB.CursorLocationEnum.adUseServer
        End If

        If Modo = 0 Then
            ObjetoTabela.CursorType = ADODB.CursorTypeEnum.adOpenForwardOnly
            ObjetoTabela.LockType = ADODB.LockTypeEnum.adLockReadOnly
        Else
            ObjetoTabela.CursorType = ADODB.CursorTypeEnum.adOpenDynamic
            ObjetoTabela.LockType = ADODB.LockTypeEnum.adLockBatchOptimistic
        End If

        If ObjetoTabela.State = 1 Then ObjetoTabela.Close()

        If Not Instrução = String.Empty Then
            ObjetoTabela.Open(Instrução)
        End If

        If Carregar = True Then ObjetoTabela.MoveLast()

        If Modo = 0 Then
            If Not ObjetoTabela.CursorLocation = ADODB.CursorLocationEnum.adUseServer Then ObjetoTabela.ActiveConnection = Nothing
        End If

        PRSet = True

    End Function

    Dim Cabe1 As String
    Dim Cabe2 As String
    Dim Cabe3 As String
    Dim Cabe4 As String
    Dim Cabe5 As String
    Dim LinhaRelServicos As Integer
    Dim TEXTO As String
    Dim Tbhs As New ADODB.Recordset
    Dim Tbhg As New ADODB.Recordset
    Dim tbhg1 As New ADODB.Recordset
    Dim TBFORMA As New ADODB.Recordset
    Dim TBLOCALATRAC As New ADODB.Recordset
    Dim TBSERVPER As New ADODB.Recordset
    Dim LPeriodo As Boolean
    Dim tbMin As New ADODB.Recordset
    Dim Rs As New ADODB.Recordset
    Dim TBV As New ADODB.Recordset
    Dim sql As String
    Dim tcarga1 As String
    Dim antbl As Integer
    Dim TCarga As String
    Dim pis, cofins, iss, lotes As String
    Dim Lista_Preco As Long

    Public Sub Gera_Rel_Servicos(ByVal _Lista_Preco As Long, Optional ByVal Modo As Integer = 0)

        Cabe1 = ""
        Cabe2 = ""
        Cabe3 = ""
        Cabe4 = ""
        Cabe5 = ""
        LinhaRelServicos = 0
        TEXTO = ""
        LPeriodo = False
        sql = ""
        tcarga1 = ""
        antbl = 0
        TCarga = ""
        pis = ""
        cofins = ""
        iss = ""
        lotes = ""
        Lista_Preco = 0

        Lista_Preco = _Lista_Preco

        LPeriodo = (MsgBox("Serviços Período listados em primeiro?", vbYesNo + vbQuestion, "Listagens - Tabela de Preços") = vbYes)

        Banco.Execute("DELETE FROM " & Banco.BancoSGIPA & "TEMP_REL_SERVICOS WHERE USUARIO=" & Banco.UsuarioSistema)

        PRSet(Tbhs, "SELECT A.OBS, A.Autonum, a.descr as Tabela,A.Flag_Liberada,  A.importador,a.dias_apos_gr,a.forma_pagamento, A.Data_Inicio, B.Razao as Nome_Parceiro,c.razao as Despachante,d.razao as Captador,e.descr as td, A.PROPOSTA FROM " & Banco.BancoSGIPA & "TB_LISTAS_PRECOS A, " & Banco.BancoSGIPA & "TB_CAD_PARCEIROS B," & Banco.BancoSGIPA & "TB_CAD_PARCEIROS c," & Banco.BancoSGIPA & "TB_CAD_PARCEIROS d," & Banco.BancoSGIPA & "TB_TIPOS_DOCUMENTOS e Where A.Autonum = " & Val(Lista_Preco) & "" & " And A.importador = B.Autonum (+) And A.despachante = c.Autonum (+) And A.captador = d.Autonum (+) and a.tipo_documento=e.code(+) order by A.descr", 0)
        Select Case Modo
            Case Is = 0
                'a.servico = b.autonum
                sql = "SELECT A.* FROM (  "
                sql = sql & " Select A.GRUPO_ATRACACAO, B.Descr as Servico, A.Tipo_Carga, A.Variante_Local, A.Base_Calculo, D.Descricao, A.Preco_Unitario, A.Preco_Minimo, A.Valor_Acrescimo, 'FIXOS' AS TIPOTABELA, VALOR_ACRESC_PESO,PESO_LIMITE,PRECO_MINIMO_DESOVA,TIPO_OPER,E.FANTASIA ,A.Autonum, b.autonum as numsv from " & Banco.BancoSGIPA & "TB_LISTA_PRECO_SERVICOS_FIXOS A, " & Banco.BancoSGIPA & "TB_SERVICOS_IPA  B, " & Banco.BancoSGIPA & "TB_CAD_MOEDAS D ," & Banco.BancoSGIPA & "TB_CAD_PARCEIROS E Where A.Lista  = '" & Lista_Preco & "' And A.Servico = B.Autonum(+) and A.Moeda = D.Autonum(+) AND A.PORTO_HUBPORT=E.AUTONUM(+) "
                sql = sql & " UNION ALL "
                sql = sql & " Select A.GRUPO_ATRACACAO, B.Descr as Servico, A.Tipo_Carga, A.Variante_Local, A.Base_Calculo, D.Descricao, A.Preco_Unitario, A.Preco_Minimo, A.Valor_Acrescimo, 'PERIODO' AS TIPOTABELA, VALOR_ACRESC_PESO,PESO_LIMITE,0,'','',A.Autonum, b.autonum as numsv from " & Banco.BancoSGIPA & "TB_LISTA_P_S_PERIODO A, " & Banco.BancoSGIPA & "TB_SERVICOS_IPA  B, " & Banco.BancoSGIPA & "TB_CAD_MOEDAS D Where A.Lista  = '" & Lista_Preco & "' And A.Servico = B.Autonum(+) and A.Moeda = D.Autonum(+) ) A WHERE TIPOTABELA = 'FIXOS' ORDER BY A.TIPOTABELA, A.SERVICO,A.Tipo_Carga"
                PRSet(tbhg1, sql, 0)
            Case Is = 1
                PRSet(tbhg1, "Select A.GRUPO_ATRACACAO, B.Descr as Servico, A.Tipo_Carga, A.Variante_Local, A.Base_Calculo, D.Descricao, A.Preco_Unitario, A.Preco_Minimo, A.Valor_Acrescimo,   VALOR_ACRESC_PESO,PESO_LIMITE,PRECO_MINIMO_DESOVA,TIPO_OPER,E.FANTASIA ,A.Autonum, b.autonum as numsv from " & Banco.BancoSGIPA & "TB_LISTA_PRECO_SERVICOS_FIXOS A, " & Banco.BancoSGIPA & "TB_SERVICOS_IPA  B, " & Banco.BancoSGIPA & "TB_CAD_MOEDAS D ," & Banco.BancoSGIPA & "TB_CAD_PARCEIROS E Where A.Lista  = " & Lista_Preco & "" & " And A.Servico = B.Autonum(+) and A.Moeda = D.Autonum(+) AND A.PORTO_HUBPORT=E.AUTONUM(+) and (a.autonum_vinculado is null or a.autonum_vinculado = 0) order by B.Descr,A.Tipo_Carga", 0)
            Case Is = 2
                PRSet(tbhg1, "Select A.GRUPO_ATRACACAO, B.Descr as Servico, A.Tipo_Carga, A.Variante_Local, A.Base_Calculo, D.Descricao, A.Preco_Unitario, A.Preco_Minimo, A.Valor_Acrescimo,  VALOR_ACRESC_PESO,PESO_LIMITE,PRECO_MINIMO_DESOVA,TIPO_OPER,E.FANTASIA ,A.Autonum, b.autonum as numsv from " & Banco.BancoSGIPA & "TB_LISTA_PRECO_SERVICOS_FIXOS A, " & Banco.BancoSGIPA & "TB_SERVICOS_IPA  B, " & Banco.BancoSGIPA & "TB_CAD_MOEDAS D ," & Banco.BancoSGIPA & "TB_CAD_PARCEIROS E Where A.Lista  = " & Lista_Preco & "" & " And A.Servico = B.Autonum(+) and A.Moeda = D.Autonum(+) AND A.PORTO_HUBPORT=E.AUTONUM(+) and not a.autonum_vinculado is null and not a.autonum_vinculado = 0 order by B.Descr,A.Tipo_Carga", 0)
        End Select
        If Not tbhg1.EOF Then
            Lista_Preco = Val(Tbhs.Fields("Autonum").Value.ToString())
            Cabecalho()
            If LPeriodo = True Then
                '----------------- Inicio Periodo ------------------------
                Select Case Modo
                    Case Is = 0 : PRSet(TBSERVPER, "Select  a.grupo_atracacao, B.Descr as Servico, A.N_Periodo, A.Qtde_Dias, A.Tipo_Carga, A.Variante_Local, A.Base_Calculo, D.Descricao, A.Preco_Unitario, A.Preco_Minimo, A.Valor_Acrescimo,decode(a.flag_prorata,0,'N',1,'S') as pr,  A.Autonum, a.servico as sipa from " & Banco.BancoSGIPA & "TB_LISTA_P_S_PERIODO A, " & Banco.BancoSGIPA & "TB_SERVICOS_IPA  B, " & Banco.BancoSGIPA & "TB_CAD_MOEDAS D Where A.Lista  = " & Lista_Preco & "" & " And A.Servico = B.Autonum(+) and A.Moeda = D.Autonum(+) order by b.descr,A.Tipo_Carga,a.n_periodo", 0)
                    Case Is = 1 : PRSet(TBSERVPER, "Select  a.grupo_atracacao, B.Descr as Servico, A.N_Periodo, A.Qtde_Dias, A.Tipo_Carga, A.Variante_Local, A.Base_Calculo, D.Descricao, A.Preco_Unitario, A.Preco_Minimo, A.Valor_Acrescimo,decode(a.flag_prorata,0,'N',1,'S') as pr,  A.Autonum, a.servico as sipa from " & Banco.BancoSGIPA & "TB_LISTA_P_S_PERIODO A, " & Banco.BancoSGIPA & "TB_SERVICOS_IPA  B, " & Banco.BancoSGIPA & "TB_CAD_MOEDAS D Where A.Lista  = " & Lista_Preco & "" & " And A.Servico = B.Autonum(+) and A.Moeda = D.Autonum(+) and (a.autonum_vinculado is null or a.autonum_vinculado = 0) order by b.descr,A.Tipo_Carga,a.n_periodo", 0)
                    Case Is = 2 : PRSet(TBSERVPER, "Select  a.grupo_atracacao, B.Descr as Servico, A.N_Periodo, A.Qtde_Dias, A.Tipo_Carga, A.Variante_Local, A.Base_Calculo, D.Descricao, A.Preco_Unitario, A.Preco_Minimo, A.Valor_Acrescimo,decode(a.flag_prorata,0,'N',1,'S') as pr,  A.Autonum, a.servico as sipa from " & Banco.BancoSGIPA & "TB_LISTA_P_S_PERIODO A, " & Banco.BancoSGIPA & "TB_SERVICOS_IPA  B, " & Banco.BancoSGIPA & "TB_CAD_MOEDAS D Where A.Lista  = " & Lista_Preco & "" & " And A.Servico = B.Autonum(+) and A.Moeda = D.Autonum(+) and not a.autonum_vinculado is null and not a.autonum_vinculado = 0 order by b.descr,A.Tipo_Carga,a.n_periodo", 0)
                End Select
                If Not TBSERVPER.EOF Then
                    Lista_Preco = Val(Tbhs.Fields("autonum").Value.ToString())
                    Cabecalho()

                    TEXTO = "--------------------------------------------------------------------------------------------------------------------------------------------"
                    Cabe1 = TEXTO
                    Call Acrescenta_Linha_Relatorio_Servicos(Lista_Preco, LinhaRelServicos, TEXTO, Banco.UsuarioSistema)
                    Cabecalho()

                    TEXTO = Microsoft.VisualBasic.Left("Serviços Período " & s(18), 18)
                    Cabe2 = TEXTO
                    Call Acrescenta_Linha_Relatorio_Servicos(Lista_Preco, LinhaRelServicos, TEXTO, Banco.UsuarioSistema)
                    Cabecalho()

                    TEXTO = "--------------------------------------------------------------------------------------------------------------------------------------------"
                    Cabe3 = TEXTO
                    Call Acrescenta_Linha_Relatorio_Servicos(Lista_Preco, LinhaRelServicos, TEXTO, Banco.UsuarioSistema)



                    TEXTO = Microsoft.VisualBasic.Left("Serviço " & s(28), 28)
                    TEXTO = TEXTO & Microsoft.VisualBasic.Left("Período " & s(8), 8)
                    TEXTO = TEXTO & Microsoft.VisualBasic.Left("Dias " & s(6), 6)
                    TEXTO = TEXTO & Microsoft.VisualBasic.Left("T.Carga " & s(8), 8)
                    TEXTO = TEXTO & Microsoft.VisualBasic.Left("Variante " & s(9), 9)
                    TEXTO = TEXTO & Microsoft.VisualBasic.Left("B.Cálculo " & s(10), 10)
                    TEXTO = TEXTO & Microsoft.VisualBasic.Left("Moeda " & s(7), 7)
                    TEXTO = TEXTO & Microsoft.VisualBasic.Left("Preço Unit. " & s(12), 12)
                    TEXTO = TEXTO & Microsoft.VisualBasic.Left("Preço Min. " & s(11), 11)
                    TEXTO = TEXTO & Microsoft.VisualBasic.Left("Perigosa % " & s(11), 11)
                    TEXTO = TEXTO & Microsoft.VisualBasic.Left("Acr.Peso " & s(9), 9)
                    TEXTO = TEXTO & Microsoft.VisualBasic.Left("Limite   " & s(7), 7)
                    TEXTO = TEXTO & Microsoft.VisualBasic.Left("ProRata" & s(8), 8)
                    'Colunas de Impostos
                    TEXTO = TEXTO & Microsoft.VisualBasic.Left("ISS" & s(5), 5)
                    TEXTO = TEXTO & Microsoft.VisualBasic.Left("PIS" & s(5), 5)
                    TEXTO = TEXTO & Microsoft.VisualBasic.Left("COFINS" & s(7), 7)

                    Cabe4 = TEXTO
                    Call Acrescenta_Linha_Relatorio_Servicos(Lista_Preco, LinhaRelServicos, TEXTO, Banco.UsuarioSistema)
                    Cabecalho()

                    TEXTO = Microsoft.VisualBasic.Left("============================================================================================================================================" & s(150), 150)
                    Cabe5 = TEXTO
                    Call Acrescenta_Linha_Relatorio_Servicos(Lista_Preco, LinhaRelServicos, TEXTO, Banco.UsuarioSistema)
                    tcarga1 = ""
                    Do While Not TBSERVPER.EOF() = True
                        If tcarga1 <> Mid$(TBSERVPER.Fields("TIPO_CARGA").Value.ToString(), 1, 3) And tcarga1 <> "" Then
                            Cabecalho()
                            TEXTO = Microsoft.VisualBasic.Left("============================================================================================================================================" & s(150), 150)
                            Call Acrescenta_Linha_Relatorio_Servicos(Lista_Preco, LinhaRelServicos, TEXTO, Banco.UsuarioSistema)
                        End If
                        tcarga1 = Mid$(TBSERVPER.Fields("TIPO_CARGA").Value.ToString(), 1, 3)
                        Cabecalho()

                        TEXTO = Microsoft.VisualBasic.Left(TBSERVPER.Fields("Servico").Value.ToString() & s(28), 28)
                        TEXTO = TEXTO & Microsoft.VisualBasic.Left(TBSERVPER.Fields("N_Periodo").Value.ToString() & s(8), 8)
                        TEXTO = TEXTO & Microsoft.VisualBasic.Left(TBSERVPER.Fields("qtde_dias").Value.ToString() & s(6), 6)
                        TEXTO = TEXTO & Microsoft.VisualBasic.Left(TBSERVPER.Fields("TIPO_CARGA").Value.ToString() & s(8), 8)
                        TEXTO = TEXTO & Microsoft.VisualBasic.Left(TBSERVPER.Fields("VARIANTE_LOCAL").Value.ToString() & s(9), 9)
                        TEXTO = TEXTO & Microsoft.VisualBasic.Left(TBSERVPER.Fields("BASE_CALCULO").Value.ToString() & s(10), 10)
                        TEXTO = TEXTO & Microsoft.VisualBasic.Left(TBSERVPER.Fields("Descricao").Value.ToString() & s(7), 7)
                        TEXTO = TEXTO & Microsoft.VisualBasic.Left(String.Format("{0:C2}", Convert.ToDecimal(TBSERVPER.Fields("preco_unitario").Value.ToString())) & s(12), 12)
                        TEXTO = TEXTO & Microsoft.VisualBasic.Left(String.Format("{0:C2}", Convert.ToDecimal(TBSERVPER.Fields("Preco_Minimo").Value.ToString())) & s(11), 11)
                        TEXTO = TEXTO & Microsoft.VisualBasic.Left(String.Format("{0:C2}", Convert.ToDecimal(TBSERVPER.Fields("VALOR_ACRESCIMO").Value.ToString())) & s(11), 11)
                        TEXTO = TEXTO & Microsoft.VisualBasic.Left(String.Format("{0:C2}", Convert.ToDecimal(tbhg1.Fields("Valor_Acresc_PESO").Value.ToString())) & s(9), 9)
                        TEXTO = TEXTO & Microsoft.VisualBasic.Left(tbhg1.Fields("PESO_LIMITE").Value.ToString() & s(7), 7)
                        TEXTO = TEXTO & s(4)
                        TEXTO = TEXTO & NNull(TBSERVPER.Fields("pr").Value.ToString(), 1) & s(4)
                        If NNull(TBSERVPER.Fields("GRUPO_ATRACACAO").Value, 0) <> 0 Then
                            PRSet(TBLOCALATRAC, "SELECT DESCRICAO DESCR FROM " & Banco.BancoSGIPA & "TB_GRUPOS_ATRACACAO WHERE ID = " & NNull(TBSERVPER.Fields("GRUPO_ATRACACAO"), 0) & " ", 0)
                            TEXTO = TEXTO & Microsoft.VisualBasic.Left(NNull(TBLOCALATRAC.Fields(0).Value, 1) & s(17), 17)
                            TBLOCALATRAC.Close()
                        Else
                            TEXTO = TEXTO & ""
                        End If
                        'Seleciona os Impostos Relacionados ao serviço
                        sql = " select c.descricao, B.calcular  "
                        sql = sql & " from " & Banco.BancoSGIPA & "tb_servicos_ipa a, TB_lp_servicos_impostos b, tb_cad_impostos c"
                        sql = sql & " where a.autonum =" & TBSERVPER.Fields("sipa").Value.ToString() & " and b.id_tabela =" & Val(Lista_Preco) & " and a.autonum = b.id_servico  and b.id_imposto = c.autonum "
                        sql = sql & " order by a.descr"
                        pis = "N"
                        cofins = "N"
                        iss = "N"
                        PRSet(tbMin, sql)
                        If Not tbMin.EOF Then
                            Do While Not tbMin.EOF() = True
                                If tbMin.Fields("Descricao").Value.ToString() = "COFINS" Then
                                    If tbMin.Fields("calcular").Value.ToString() = 0 Then
                                        cofins = "N"
                                    Else
                                        cofins = "S"
                                    End If
                                End If
                                If tbMin.Fields("Descricao").Value.ToString() = "ISS" Then
                                    If tbMin.Fields("calcular").Value.ToString() = 0 Then
                                        iss = "N"
                                    Else
                                        iss = "S"
                                    End If
                                End If
                                If tbMin.Fields("Descricao").Value.ToString() = "PIS" Then
                                    If tbMin.Fields("calcular").Value.ToString() = 0 Then
                                        pis = "N"
                                    Else
                                        pis = "S"
                                    End If
                                End If
                                tbMin.MoveNext()
                            Loop
                        End If
                        'Exibe as Informações S ou N para os Impostos
                        TEXTO = TEXTO & Microsoft.VisualBasic.Left(iss & s(5), 5)
                        TEXTO = TEXTO & Microsoft.VisualBasic.Left(pis & s(5), 5)
                        TEXTO = TEXTO & Microsoft.VisualBasic.Left(cofins & s(7), 7)
                        Call Acrescenta_Linha_Relatorio_Servicos(Lista_Preco, LinhaRelServicos, TEXTO, Banco.UsuarioSistema)

                        PRSet(tbMin, "SELECT nbls, valorminimo,percmulta,tipo,autonumsv,autonum  FROM TB_LISTA_CFG_VALORMINIMO where autonumsv =" & TBSERVPER.Fields("Autonum").Value.ToString() & " order by tipo,nbls")
                        If Not tbMin.EOF Then
                            Cabecalho()
                            TEXTO = "----------"
                            TEXTO = TEXTO & Microsoft.VisualBasic.Left("N. BLS " & s(21), 21)
                            TEXTO = TEXTO & Microsoft.VisualBasic.Left("Valor Minimo " & s(15), 15)
                            TEXTO = TEXTO & Microsoft.VisualBasic.Left("Multa % " & s(15), 15)
                            TEXTO = TEXTO & Microsoft.VisualBasic.Left("T.Carga " & s(15), 15)
                            Call Acrescenta_Linha_Relatorio_Servicos(Lista_Preco, LinhaRelServicos, TEXTO, Banco.UsuarioSistema)
                            antbl = 0
                            TCarga = ""
                            Do While Not tbMin.EOF() = True
                                If TCarga <> Mid$(tbMin.Fields("Tipo").Value.ToString(), 1, 3) And TCarga <> "" Then
                                    Cabecalho()
                                    TEXTO = Microsoft.VisualBasic.Left("============================================================================================================================================" & s(150), 150)
                                    Call Acrescenta_Linha_Relatorio_Servicos(Lista_Preco, LinhaRelServicos, TEXTO, Banco.UsuarioSistema)
                                End If
                                TCarga = Mid$(tbMin.Fields("Tipo").Value.ToString(), 1, 3)
                                Cabecalho()
                                TEXTO = "----------"
                                If antbl > Val(NNull(tbMin.Fields("nBLs").Value.ToString(), 0)) Then antbl = 0
                                TEXTO = TEXTO & "DE  " & Microsoft.VisualBasic.Left(antbl + 1 & s(10), 10)
                                antbl = Val(NNull(tbMin.Fields("nBLs").Value.ToString(), 0))
                                TEXTO = TEXTO & "ATÉ " & Microsoft.VisualBasic.Left(NNull(tbMin.Fields("nBLs").Value.ToString(), 0) & s(11), 11)
                                TEXTO = TEXTO & Microsoft.VisualBasic.Left(tbMin.Fields("ValorMINIMO").Value.ToString() & s(15), 15)
                                TEXTO = TEXTO & Microsoft.VisualBasic.Left(tbMin.Fields("PercMulta").Value.ToString() & s(15), 15)
                                TEXTO = TEXTO & Microsoft.VisualBasic.Left(tbMin.Fields("Tipo").Value.ToString() & s(15), 15)
                                Call Acrescenta_Linha_Relatorio_Servicos(Lista_Preco, LinhaRelServicos, TEXTO, Banco.UsuarioSistema)
                                tbMin.MoveNext()
                            Loop
                            Cabecalho()
                            TEXTO = Microsoft.VisualBasic.Left("============================================================================================================================================" & s(150), 150)
                            Call Acrescenta_Linha_Relatorio_Servicos(Lista_Preco, LinhaRelServicos, TEXTO, Banco.UsuarioSistema)
                        End If


                        PRSet(tbMin, "SELECT VALORINICIAL, valorfinal,percentual ,Minimo FROM TB_LISTA_P_S_FAIXASCIF_PER where autonumsv =" & TBSERVPER.Fields("Autonum").Value.ToString() & " order by VALORINICIAL")
                        If Not tbMin.EOF Then
                            Cabecalho()
                            TEXTO = "----------"
                            TEXTO = TEXTO & Microsoft.VisualBasic.Left("Valor Minimo " & s(15), 15)
                            TEXTO = TEXTO & Microsoft.VisualBasic.Left("Valor Maximo " & s(15), 15)
                            TEXTO = TEXTO & Microsoft.VisualBasic.Left("Percentual % " & s(15), 15)
                            TEXTO = TEXTO & Microsoft.VisualBasic.Left("Minimo       " & s(15), 15)
                            Call Acrescenta_Linha_Relatorio_Servicos(Lista_Preco, LinhaRelServicos, TEXTO, Banco.UsuarioSistema)
                            Do While Not tbMin.EOF() = True
                                Cabecalho()
                                TEXTO = "----------"
                                TEXTO = TEXTO & Microsoft.VisualBasic.Left(tbMin.Fields("ValorInicial").Value.ToString() & s(15), 15)
                                TEXTO = TEXTO & Microsoft.VisualBasic.Left(tbMin.Fields("ValorFinal").Value.ToString() & s(15), 15)
                                TEXTO = TEXTO & Microsoft.VisualBasic.Left(tbMin.Fields("Percentual").Value.ToString() & s(15), 15)
                                TEXTO = TEXTO & Microsoft.VisualBasic.Left(tbMin.Fields("Minimo").Value.ToString() & s(15), 15)
                                Call Acrescenta_Linha_Relatorio_Servicos(Lista_Preco, LinhaRelServicos, TEXTO, Banco.UsuarioSistema)
                                tbMin.MoveNext()
                            Loop
                            Cabecalho()
                            TEXTO = Microsoft.VisualBasic.Left("============================================================================================================================================" & s(150), 150)
                            Call Acrescenta_Linha_Relatorio_Servicos(Lista_Preco, LinhaRelServicos, TEXTO, Banco.UsuarioSistema)
                        End If
                        TBSERVPER.MoveNext()
                    Loop
                End If
                TBSERVPER.Close()
                TBSERVPER = Nothing
            End If
            Cabecalho()

            TEXTO = "--------------------------------------------------------------------------------------------------------------------------------------------"
            Call Acrescenta_Linha_Relatorio_Servicos(Lista_Preco, LinhaRelServicos, TEXTO, Banco.UsuarioSistema)
            Cabecalho()

            TEXTO = Microsoft.VisualBasic.Left("Serviços Fixo" & s(14), 14)
            Cabe2 = TEXTO
            Call Acrescenta_Linha_Relatorio_Servicos(Lista_Preco, LinhaRelServicos, TEXTO, Banco.UsuarioSistema)
            Cabecalho()

            TEXTO = "--------------------------------------------------------------------------------------------------------------------------------------------"
            Call Acrescenta_Linha_Relatorio_Servicos(Lista_Preco, LinhaRelServicos, TEXTO, Banco.UsuarioSistema)
            Cabecalho()

            TEXTO = Microsoft.VisualBasic.Left("Serviço " & s(31), 31)
            TEXTO = TEXTO & Microsoft.VisualBasic.Left("T.Carga " & s(8), 8)
            TEXTO = TEXTO & Microsoft.VisualBasic.Left("Margem  " & s(8), 8)
            TEXTO = TEXTO & Microsoft.VisualBasic.Left("B.Calc. " & s(9), 9)
            TEXTO = TEXTO & Microsoft.VisualBasic.Left("Moeda " & s(9), 9)
            TEXTO = TEXTO & Microsoft.VisualBasic.Left("Pr.Unit. " & s(9), 9)
            TEXTO = TEXTO & Microsoft.VisualBasic.Left("Pr.Min.  " & s(9), 9)
            TEXTO = TEXTO & Microsoft.VisualBasic.Left("Perig.%  " & s(9), 9)
            TEXTO = TEXTO & Microsoft.VisualBasic.Left("Lc.Atrac." & s(10), 10)
            TEXTO = TEXTO & Microsoft.VisualBasic.Left("Acr.Peso " & s(9), 9)
            TEXTO = TEXTO & Microsoft.VisualBasic.Left("Limite   " & s(9), 9)
            TEXTO = TEXTO & Microsoft.VisualBasic.Left("Mn.Desova" & s(10), 10)
            TEXTO = TEXTO & Microsoft.VisualBasic.Left("Tp.Desova" & s(10), 10)
            TEXTO = TEXTO & Microsoft.VisualBasic.Left("Hub Port " & s(10), 10)
            Cabe4 = TEXTO
            Call Acrescenta_Linha_Relatorio_Servicos(Lista_Preco, LinhaRelServicos, TEXTO, Banco.UsuarioSistema)
            Cabecalho()
            TEXTO = Microsoft.VisualBasic.Left("============================================================================================================================================" & s(150), 150)
            Call Acrescenta_Linha_Relatorio_Servicos(Lista_Preco, LinhaRelServicos, TEXTO, Banco.UsuarioSistema)
            Do While Not tbhg1.EOF()
                Cabecalho()

                TEXTO = Microsoft.VisualBasic.Left(tbhg1.Fields("Servico").Value.ToString() & s(31), 31)
                TEXTO = TEXTO & Microsoft.VisualBasic.Left(tbhg1.Fields("TIPO_CARGA").Value.ToString() & s(8), 8)
                TEXTO = TEXTO & Microsoft.VisualBasic.Left(tbhg1.Fields("VARIANTE_LOCAL").Value.ToString() & s(8), 8)
                TEXTO = TEXTO & Microsoft.VisualBasic.Left(tbhg1.Fields("BASE_CALCULO").Value.ToString() & s(9), 9)
                TEXTO = TEXTO & Microsoft.VisualBasic.Left(tbhg1.Fields("Descricao").Value.ToString() & s(8), 8) & " "
                TEXTO = TEXTO & Microsoft.VisualBasic.Left(String.Format("{0:C2}", Convert.ToDecimal(tbhg1.Fields("preco_unitario").Value.ToString())) & s(9), 9)
                TEXTO = TEXTO & Microsoft.VisualBasic.Left(String.Format("{0:C2}", Convert.ToDecimal(tbhg1.Fields("Preco_Minimo").Value.ToString())) & s(9), 9)
                TEXTO = TEXTO & Microsoft.VisualBasic.Left(String.Format("{0:C2}", Convert.ToDecimal(tbhg1.Fields("VALOR_ACRESCIMO").Value.ToString())) & s(9), 9)

                If NNull(tbhg1.Fields("GRUPO_ATRACACAO").Value.ToString(), 0) <> 0 Then
                    PRSet(TBLOCALATRAC, "SELECT DESCRicao descr FROM " & Banco.BancoSGIPA & "tb_grupos_atracacao WHERE id = " & NNull(tbhg1.Fields("GRUPO_ATRACACAO").Value.ToString(), 0) & " ", 0)
                    TEXTO = TEXTO & Microsoft.VisualBasic.Left(NNull(TBLOCALATRAC.Fields(0).Value.ToString(), 1) & s(10), 10)
                    TBLOCALATRAC.Close()
                Else
                    TEXTO = TEXTO & s(10)
                End If

                TEXTO = TEXTO & Microsoft.VisualBasic.Left(String.Format("{0:C2}", Convert.ToDecimal(NNull(tbhg1.Fields("Valor_Acresc_PESO").Value.ToString(), 0))) & s(9), 9)
                TEXTO = TEXTO & Microsoft.VisualBasic.Left(tbhg1.Fields("PESO_LIMITE").Value.ToString() & s(9), 9)
                TEXTO = TEXTO & Microsoft.VisualBasic.Left(String.Format("{0:C2}", Convert.ToDecimal(NNull(tbhg1.Fields("Preco_Minimo_Desova").Value.ToString(), 0))) & s(10), 10)
                TEXTO = TEXTO & Microsoft.VisualBasic.Left(tbhg1.Fields("Tipo_Oper").Value.ToString() & s(10), 10)
                TEXTO = TEXTO & Microsoft.VisualBasic.Left(NNull(tbhg1.Fields("Fantasia").Value.ToString(), 1) & s(10), 10)
                Call Acrescenta_Linha_Relatorio_Servicos(Lista_Preco, LinhaRelServicos, TEXTO, Banco.UsuarioSistema)


                'FAIXA DE PESO - HUBPORT
                PRSet(tbMin, "SELECT pesoinicial, pesofinal, preco FROM TB_LISTA_FAIXA_PESO where autonumsv =" & tbhg1.Fields("Autonum").Value.ToString() & " and tipo = 'H' order by PESOINICIAL")
                If Not tbMin.EOF Then
                    Cabecalho()
                    TEXTO = "TABELA HUBPORT - PESO ----------"
                    TEXTO = TEXTO & Microsoft.VisualBasic.Left("Peso Inicial " & s(15), 15)
                    TEXTO = TEXTO & Microsoft.VisualBasic.Left("Peso Final " & s(15), 15)
                    TEXTO = TEXTO & Microsoft.VisualBasic.Left("Preço  " & s(15), 15)
                    Call Acrescenta_Linha_Relatorio_Servicos(Lista_Preco, LinhaRelServicos, TEXTO, Banco.UsuarioSistema)
                    Do While Not tbMin.EOF() = True
                        Cabecalho()
                        TEXTO = "--------------------------------"
                        TEXTO = TEXTO & Microsoft.VisualBasic.Left(tbMin.Fields("PESOINICIAL").Value.ToString() & s(15), 15)
                        TEXTO = TEXTO & Microsoft.VisualBasic.Left(tbMin.Fields("PESOFINAL").Value.ToString() & s(15), 15)
                        TEXTO = TEXTO & Microsoft.VisualBasic.Left(tbMin.Fields("Preco").Value.ToString() & s(15), 15)
                        Call Acrescenta_Linha_Relatorio_Servicos(Lista_Preco, LinhaRelServicos, TEXTO, Banco.UsuarioSistema)
                        tbMin.MoveNext()
                    Loop
                    Cabecalho()
                    TEXTO = Microsoft.VisualBasic.Left("============================================================================================================================================" & s(150), 150)
                    Call Acrescenta_Linha_Relatorio_Servicos(Lista_Preco, LinhaRelServicos, TEXTO, Banco.UsuarioSistema)
                End If

                'FAIXA DE VOLUME - HUBPORT
                PRSet(tbMin, "SELECT volumeinicial, volumefinal, preco FROM TB_LISTA_FAIXA_VOLUME where autonumsv =" & tbhg1.Fields("Autonum").Value.ToString() & " and tipo = 'H' order by volumeinicial")
                If Not tbMin.EOF Then
                    Cabecalho()
                    TEXTO = "TABELA HUBPORT - VOLUME---------"
                    TEXTO = TEXTO & Microsoft.VisualBasic.Left("Volume Inicial " & s(15), 15)
                    TEXTO = TEXTO & Microsoft.VisualBasic.Left("Volume Final " & s(15), 15)
                    TEXTO = TEXTO & Microsoft.VisualBasic.Left("Preço  " & s(15), 15)
                    Call Acrescenta_Linha_Relatorio_Servicos(Lista_Preco, LinhaRelServicos, TEXTO, Banco.UsuarioSistema)
                    Do While Not tbMin.EOF() = True
                        Cabecalho()
                        TEXTO = "--------------------------------"
                        TEXTO = TEXTO & Microsoft.VisualBasic.Left(tbMin.Fields("VOLUMEINICIAL").Value.ToString() & s(15), 15)
                        TEXTO = TEXTO & Microsoft.VisualBasic.Left(tbMin.Fields("VOLUMEFINAL").Value.ToString() & s(15), 15)
                        TEXTO = TEXTO & Microsoft.VisualBasic.Left(tbMin.Fields("Preco").Value.ToString() & s(15), 15)
                        Call Acrescenta_Linha_Relatorio_Servicos(Lista_Preco, LinhaRelServicos, TEXTO, Banco.UsuarioSistema)
                        tbMin.MoveNext()
                    Loop
                    Cabecalho()
                    TEXTO = Microsoft.VisualBasic.Left("============================================================================================================================================" & s(150), 150)
                    Call Acrescenta_Linha_Relatorio_Servicos(Lista_Preco, LinhaRelServicos, TEXTO, Banco.UsuarioSistema)
                End If

                'FAIXA DE PESO - CARGA SOLTA
                PRSet(tbMin, "SELECT pesoinicial, pesofinal, preco FROM TB_LISTA_FAIXA_PESO where autonumsv =" & tbhg1.Fields("Autonum").Value.ToString() & " and tipo = 'C' order by PESOINICIAL")
                If Not tbMin.EOF Then
                    Cabecalho()
                    TEXTO = "TABELA CARGA SOLTA - PESO ------"
                    TEXTO = TEXTO & Microsoft.VisualBasic.Left("Peso Inicial " & s(15), 15)
                    TEXTO = TEXTO & Microsoft.VisualBasic.Left("Peso Final " & s(15), 15)
                    TEXTO = TEXTO & Microsoft.VisualBasic.Left("Preço  " & s(15), 15)
                    Call Acrescenta_Linha_Relatorio_Servicos(Lista_Preco, LinhaRelServicos, TEXTO, Banco.UsuarioSistema)
                    Do While Not tbMin.EOF() = True
                        Cabecalho()
                        TEXTO = "--------------------------------"
                        TEXTO = TEXTO & Microsoft.VisualBasic.Left(tbMin.Fields("PESOINICIAL").Value.ToString() & s(15), 15)
                        TEXTO = TEXTO & Microsoft.VisualBasic.Left(tbMin.Fields("PESOFINAL").Value.ToString() & s(15), 15)
                        TEXTO = TEXTO & Microsoft.VisualBasic.Left(tbMin.Fields("Preco").Value.ToString() & s(15), 15)
                        Call Acrescenta_Linha_Relatorio_Servicos(Lista_Preco, LinhaRelServicos, TEXTO, Banco.UsuarioSistema)
                        tbMin.MoveNext()
                    Loop
                    Cabecalho()
                    TEXTO = Microsoft.VisualBasic.Left("============================================================================================================================================" & s(150), 150)
                    Call Acrescenta_Linha_Relatorio_Servicos(Lista_Preco, LinhaRelServicos, TEXTO, Banco.UsuarioSistema)
                End If

                'FAIXA DE VOLUME - CARGA SOLTA
                PRSet(tbMin, "SELECT volumeinicial, volumefinal, preco FROM TB_LISTA_FAIXA_VOLUME where autonumsv =" & tbhg1.Fields("Autonum").Value.ToString() & " and tipo = 'C' order by volumeinicial")
                If Not tbMin.EOF Then
                    Cabecalho()
                    TEXTO = "TABELA CARGA SOLTA - VOLUME-----"
                    TEXTO = TEXTO & Microsoft.VisualBasic.Left("Volume Inicial " & s(15), 15)
                    TEXTO = TEXTO & Microsoft.VisualBasic.Left("Volume Final " & s(15), 15)
                    TEXTO = TEXTO & Microsoft.VisualBasic.Left("Adicional %  " & s(15), 15)
                    Call Acrescenta_Linha_Relatorio_Servicos(Lista_Preco, LinhaRelServicos, TEXTO, Banco.UsuarioSistema)
                    Do While Not tbMin.EOF() = True
                        Cabecalho()
                        TEXTO = "--------------------------------"
                        TEXTO = TEXTO & Microsoft.VisualBasic.Left(tbMin.Fields("VOLUMEINICIAL").Value.ToString() & s(15), 15)
                        TEXTO = TEXTO & Microsoft.VisualBasic.Left(tbMin.Fields("VOLUMEFINAL").Value.ToString() & s(15), 15)
                        TEXTO = TEXTO & Microsoft.VisualBasic.Left(tbMin.Fields("Preco").Value.ToString() & s(15), 15)
                        Call Acrescenta_Linha_Relatorio_Servicos(Lista_Preco, LinhaRelServicos, TEXTO, Banco.UsuarioSistema)
                        tbMin.MoveNext()
                    Loop
                    Cabecalho()
                    TEXTO = Microsoft.VisualBasic.Left("============================================================================================================================================" & s(150), 150)
                    Call Acrescenta_Linha_Relatorio_Servicos(Lista_Preco, LinhaRelServicos, TEXTO, Banco.UsuarioSistema)
                End If


                PRSet(tbMin, "SELECT VALORINICIAL, valorfinal,percentual  ,Minimo FROM TB_LISTA_P_S_FAIXASCIF_PER where autonumsv =" & tbhg1.Fields("Autonum").Value.ToString() & " order by VALORINICIAL")
                If Not tbMin.EOF Then
                    Cabecalho()
                    TEXTO = "----------"
                    TEXTO = TEXTO & Microsoft.VisualBasic.Left("Valor Minimo " & s(15), 15)
                    TEXTO = TEXTO & Microsoft.VisualBasic.Left("Valor Maximo " & s(15), 15)
                    TEXTO = TEXTO & Microsoft.VisualBasic.Left("Percentual % " & s(15), 15)
                    TEXTO = TEXTO & Microsoft.VisualBasic.Left("Minimo       " & s(15), 15)
                    Call Acrescenta_Linha_Relatorio_Servicos(Lista_Preco, LinhaRelServicos, TEXTO, Banco.UsuarioSistema)
                    Do While Not tbMin.EOF() = True
                        Cabecalho()
                        TEXTO = "----------"
                        TEXTO = TEXTO & Microsoft.VisualBasic.Left(tbMin.Fields("ValorInicial").Value.ToString() & s(15), 15)
                        TEXTO = TEXTO & Microsoft.VisualBasic.Left(tbMin.Fields("ValorFinal").Value.ToString() & s(15), 15)
                        TEXTO = TEXTO & Microsoft.VisualBasic.Left(tbMin.Fields("Percentual").Value.ToString() & s(15), 15)
                        TEXTO = TEXTO & Microsoft.VisualBasic.Left(tbMin.Fields("Minimo").Value.ToString() & s(15), 15)
                        Call Acrescenta_Linha_Relatorio_Servicos(Lista_Preco, LinhaRelServicos, TEXTO, Banco.UsuarioSistema)
                        tbMin.MoveNext()
                    Loop
                    Cabecalho()
                    TEXTO = Microsoft.VisualBasic.Left("============================================================================================================================================" & s(150), 150)
                    Call Acrescenta_Linha_Relatorio_Servicos(Lista_Preco, LinhaRelServicos, TEXTO, Banco.UsuarioSistema)
                End If
                tbhg1.MoveNext()
            Loop
            If Not LPeriodo = True Then
                '----------------- Inicio Periodo ------------------------
                PRSet(TBSERVPER, "Select  a.grupo_atracacao, B.Descr as Servico, A.N_Periodo, A.Qtde_Dias, A.Tipo_Carga, A.Variante_Local, A.Base_Calculo, D.Descricao, A.Preco_Unitario, A.Preco_Minimo, A.Valor_Acrescimo,  decode(a.flag_prorata,0,'N',1,'S') as pr, A.Autonum from " & Banco.BancoSGIPA & "TB_LISTA_P_S_PERIODO A, " & Banco.BancoSGIPA & "TB_SERVICOS_IPA  B, " & Banco.BancoSGIPA & "TB_CAD_MOEDAS D Where A.Lista  = " & Lista_Preco & "" & " And A.Servico = B.Autonum(+) and A.Moeda = D.Autonum(+) order by b.descr", 0)
                If Not TBSERVPER.EOF Then
                    Lista_Preco = Val(Tbhs.Fields("Autonum").Value.ToString())
                    Cabecalho()

                    TEXTO = "--------------------------------------------------------------------------------------------------------------------------------------------"
                    Cabe1 = TEXTO
                    Call Acrescenta_Linha_Relatorio_Servicos(Lista_Preco, LinhaRelServicos, TEXTO, Banco.UsuarioSistema)
                    Cabecalho()

                    TEXTO = Microsoft.VisualBasic.Left("Serviços Período " & s(18), 18)
                    Cabe2 = TEXTO
                    Call Acrescenta_Linha_Relatorio_Servicos(Lista_Preco, LinhaRelServicos, TEXTO, Banco.UsuarioSistema)
                    Cabecalho()

                    TEXTO = "--------------------------------------------------------------------------------------------------------------------------------------------"
                    Cabe3 = TEXTO
                    Call Acrescenta_Linha_Relatorio_Servicos(Lista_Preco, LinhaRelServicos, TEXTO, Banco.UsuarioSistema)
                    Cabecalho()

                    TEXTO = Microsoft.VisualBasic.Left("Serviço " & s(31), 31)
                    TEXTO = TEXTO & Microsoft.VisualBasic.Left("Período " & s(8), 8)
                    TEXTO = TEXTO & Microsoft.VisualBasic.Left("Dias " & s(6), 6)
                    TEXTO = TEXTO & Microsoft.VisualBasic.Left("T.Carga " & s(8), 8)
                    TEXTO = TEXTO & Microsoft.VisualBasic.Left("Variante " & s(10), 10)
                    TEXTO = TEXTO & Microsoft.VisualBasic.Left("B.Cálculo " & s(10), 10)
                    TEXTO = TEXTO & Microsoft.VisualBasic.Left("Moeda " & s(11), 11)
                    TEXTO = TEXTO & Microsoft.VisualBasic.Left("Preço Unit. " & s(12), 12)
                    TEXTO = TEXTO & Microsoft.VisualBasic.Left("Preço Min. " & s(12), 12)
                    TEXTO = TEXTO & Microsoft.VisualBasic.Left("Perigosa % " & s(12), 12)
                    TEXTO = TEXTO & Microsoft.VisualBasic.Left("Acr.Peso " & s(9), 9)
                    TEXTO = TEXTO & Microsoft.VisualBasic.Left("Limite   " & s(9), 9)
                    TEXTO = TEXTO & Microsoft.VisualBasic.Left("ProRata" & s(7), 7)
                    Cabe4 = TEXTO
                    Call Acrescenta_Linha_Relatorio_Servicos(Lista_Preco, LinhaRelServicos, TEXTO, Banco.UsuarioSistema)
                    Cabecalho()

                    TEXTO = Microsoft.VisualBasic.Left("============================================================================================================================================" & s(150), 150)
                    Cabe5 = TEXTO
                    Call Acrescenta_Linha_Relatorio_Servicos(Lista_Preco, LinhaRelServicos, TEXTO, Banco.UsuarioSistema)
                    tcarga1 = 1
                    Do While Not TBSERVPER.EOF()
                        If tcarga1 <> Mid$(TBSERVPER.Fields("TIPO_CARGA").Value.ToString(), 1, 3) And tcarga1 <> "" Then
                            Cabecalho()
                            TEXTO = Microsoft.VisualBasic.Left("============================================================================================================================================" & s(150), 150)
                            Call Acrescenta_Linha_Relatorio_Servicos(Lista_Preco, LinhaRelServicos, TEXTO, Banco.UsuarioSistema)
                        End If
                        tcarga1 = Mid$(TBSERVPER.Fields("TIPO_CARGA").Value.ToString(), 1, 3)
                        Cabecalho()

                        TEXTO = Microsoft.VisualBasic.Left(TBSERVPER.Fields("Servico").Value.ToString() & s(33), 33)
                        TEXTO = TEXTO & Microsoft.VisualBasic.Left(TBSERVPER.Fields("N_Periodo").Value.ToString() & s(9), 9)
                        TEXTO = TEXTO & Microsoft.VisualBasic.Left(TBSERVPER.Fields("qtde_dias").Value.ToString() & s(8), 8)
                        TEXTO = TEXTO & Microsoft.VisualBasic.Left(TBSERVPER.Fields("TIPO_CARGA").Value.ToString() & s(9), 9)
                        TEXTO = TEXTO & Microsoft.VisualBasic.Left(TBSERVPER.Fields("VARIANTE_LOCAL").Value.ToString() & s(11), 11)
                        TEXTO = TEXTO & Microsoft.VisualBasic.Left(TBSERVPER.Fields("BASE_CALCULO").Value.ToString() & s(8), 8)
                        TEXTO = TEXTO & Microsoft.VisualBasic.Left(TBSERVPER.Fields("Descricao").Value.ToString() & s(16), 16)

                        TEXTO = TEXTO & Microsoft.VisualBasic.Left(String.Format("{0:C2}", Convert.ToDecimal(TBSERVPER.Fields("preco_unitario").Value.ToString())) & s(13), 13)
                        TEXTO = TEXTO & Microsoft.VisualBasic.Left(String.Format("{0:C2}", Convert.ToDecimal(TBSERVPER.Fields("Preco_Minimo").Value.ToString())) & s(12), 12)
                        TEXTO = TEXTO & Microsoft.VisualBasic.Left(String.Format("{0:C2}", Convert.ToDecimal(TBSERVPER.Fields("VALOR_ACRESCIMO").Value.ToString())) & s(13), 13)

                        TEXTO = TEXTO & s(3)
                        TEXTO = TEXTO & NNull(TBSERVPER.Fields("pr").Value.ToString(), 1)

                        If NNull(TBSERVPER.Fields("grupo_ATRACACAO").Value.ToString(), 0) <> 0 Then
                            PRSet(TBLOCALATRAC, "SELECT DESCRicao descr FROM " & Banco.BancoSGIPA & "tb_grupos_atracacao WHERE id = " & NNull(TBSERVPER.Fields("grupo_ATRACACAO").Value.ToString(), 0) & " ", 0)
                            TEXTO = TEXTO & Microsoft.VisualBasic.Left(NNull(TBLOCALATRAC.Fields(0).Value.ToString(), 1) & s(17), 17)
                            TBLOCALATRAC.Close()
                        End If
                        Call Acrescenta_Linha_Relatorio_Servicos(Lista_Preco, LinhaRelServicos, TEXTO, Banco.UsuarioSistema)
                        PRSet(tbMin, "SELECT nbls, valorminimo,percmulta,tipo,autonumsv,autonum  FROM TB_LISTA_CFG_VALORMINIMO where autonumsv =" & TBSERVPER.Fields("Autonum").Value.ToString() & " order by tipo,nbls")
                        If Not tbMin.EOF Then
                            Cabecalho()
                            TEXTO = "----------"
                            TEXTO = TEXTO & Microsoft.VisualBasic.Left("N. BLS " & s(21), 21)
                            TEXTO = TEXTO & Microsoft.VisualBasic.Left("Valor Minimo " & s(15), 15)
                            TEXTO = TEXTO & Microsoft.VisualBasic.Left("Multa % " & s(15), 15)
                            TEXTO = TEXTO & Microsoft.VisualBasic.Left("T.Carga " & s(15), 15)
                            Call Acrescenta_Linha_Relatorio_Servicos(Lista_Preco, LinhaRelServicos, TEXTO, Banco.UsuarioSistema)
                            antbl = 0
                            TCarga = ""
                            Do While Not tbMin.EOF() = True
                                If TCarga <> Mid$(tbMin.Fields("Tipo").Value.ToString(), 1, 3) And TCarga <> "" Then
                                    Cabecalho()
                                    TEXTO = Microsoft.VisualBasic.Left("============================================================================================================================================" & s(150), 150)
                                    Call Acrescenta_Linha_Relatorio_Servicos(Lista_Preco, LinhaRelServicos, TEXTO, Banco.UsuarioSistema)
                                End If
                                TCarga = Mid$(tbMin.Fields("Tipo").Value.ToString(), 1, 3)
                                Cabecalho()
                                TEXTO = "----------"
                                If antbl > Val(NNull(tbMin.Fields("nBLs").Value.ToString(), 0)) Then antbl = 0
                                TEXTO = TEXTO & "DE  " & Microsoft.VisualBasic.Left(antbl + 1 & s(10), 10)
                                antbl = Val(NNull(tbMin.Fields("nBLs").Value.ToString(), 0))
                                TEXTO = TEXTO & "ATÉ " & Microsoft.VisualBasic.Left(NNull(tbMin.Fields("nBLs").Value.ToString(), 0) & s(11), 11)
                                TEXTO = TEXTO & Microsoft.VisualBasic.Left(tbMin.Fields("ValorMINIMO").Value.ToString() & s(15), 15)
                                TEXTO = TEXTO & Microsoft.VisualBasic.Left(tbMin.Fields("PercMulta").Value.ToString() & s(15), 15)
                                TEXTO = TEXTO & Microsoft.VisualBasic.Left(tbMin.Fields("Tipo").Value.ToString() & s(15), 15)
                                Call Acrescenta_Linha_Relatorio_Servicos(Lista_Preco, LinhaRelServicos, TEXTO, Banco.UsuarioSistema)
                                tbMin.MoveNext()
                            Loop
                            Cabecalho()
                            TEXTO = Microsoft.VisualBasic.Left("============================================================================================================================================" & s(150), 150)
                            Call Acrescenta_Linha_Relatorio_Servicos(Lista_Preco, LinhaRelServicos, TEXTO, Banco.UsuarioSistema)
                        End If
                        PRSet(tbMin, "SELECT VALORINICIAL, valorfinal,percentual ,Minimo FROM TB_LISTA_P_S_FAIXASCIF_PER where autonumsv =" & TBSERVPER.Fields("Autonum").Value.ToString() & " order by VALORINICIAL")
                        If Not tbMin.EOF Then
                            Cabecalho()
                            TEXTO = "----------"
                            TEXTO = TEXTO & Microsoft.VisualBasic.Left("Valor Minimo " & s(15), 15)
                            TEXTO = TEXTO & Microsoft.VisualBasic.Left("Valor Maximo " & s(15), 15)
                            TEXTO = TEXTO & Microsoft.VisualBasic.Left("Percentual % " & s(15), 15)
                            TEXTO = TEXTO & Microsoft.VisualBasic.Left("Minimo       " & s(15), 15)
                            Call Acrescenta_Linha_Relatorio_Servicos(Lista_Preco, LinhaRelServicos, TEXTO, Banco.UsuarioSistema)
                            Do While Not tbMin.EOF() = True
                                Cabecalho()
                                TEXTO = "----------"
                                TEXTO = TEXTO & Microsoft.VisualBasic.Left(tbMin.Fields("ValorInicial").Value.ToString() & s(15), 15)
                                TEXTO = TEXTO & Microsoft.VisualBasic.Left(tbMin.Fields("ValorFinal").Value.ToString() & s(15), 15)
                                TEXTO = TEXTO & Microsoft.VisualBasic.Left(tbMin.Fields("Percentual").Value.ToString() & s(15), 15)
                                TEXTO = TEXTO & Microsoft.VisualBasic.Left(tbMin.Fields("Minimo").Value.ToString() & s(15), 15)
                                Call Acrescenta_Linha_Relatorio_Servicos(Lista_Preco, LinhaRelServicos, TEXTO, Banco.UsuarioSistema)
                                tbMin.MoveNext()
                            Loop
                            Cabecalho()
                            TEXTO = Microsoft.VisualBasic.Left("============================================================================================================================================" & s(150), 150)
                            Call Acrescenta_Linha_Relatorio_Servicos(Lista_Preco, LinhaRelServicos, TEXTO, Banco.UsuarioSistema)
                        End If
                        TBSERVPER.MoveNext()
                    Loop
                End If
                TBSERVPER.Close()
                TBSERVPER = Nothing
            End If
        Else
            Lista_Preco = Val(Tbhs.Fields("Autonum").Value.ToString())
            LinhaRelServicos = 0
            Cabecalho()
            '----------------- Inicio Periodo ------------------------
            PRSet(TBSERVPER, "Select  a.grupo_atracacao, B.Descr as Servico, A.N_Periodo, A.Qtde_Dias, A.Tipo_Carga, A.Variante_Local, A.Base_Calculo, D.Descricao, A.Preco_Unitario, A.Preco_Minimo, A.Valor_Acrescimo,  decode(a.flag_prorata,0,'N',1,'S') as pr, A.Autonum from " & Banco.BancoSGIPA & "TB_LISTA_P_S_PERIODO A, " & Banco.BancoSGIPA & "TB_SERVICOS_IPA  B, " & Banco.BancoSGIPA & "TB_CAD_MOEDAS D Where A.Lista  = " & Lista_Preco & "" & " And A.Servico = B.Autonum(+) and A.Moeda = D.Autonum(+) order by b.descr", 0)
            If Not TBSERVPER.EOF Then
                Lista_Preco = Val(Tbhs.Fields("Autonum").Value.ToString())
                Cabecalho()

                TEXTO = "--------------------------------------------------------------------------------------------------------------------------------------------"
                Cabe1 = TEXTO
                Call Acrescenta_Linha_Relatorio_Servicos(Lista_Preco, LinhaRelServicos, TEXTO, Banco.UsuarioSistema)
                Cabecalho()

                TEXTO = Microsoft.VisualBasic.Left("Serviços Período " & s(18), 18)
                Cabe2 = TEXTO
                Call Acrescenta_Linha_Relatorio_Servicos(Lista_Preco, LinhaRelServicos, TEXTO, Banco.UsuarioSistema)
                Cabecalho()

                TEXTO = "--------------------------------------------------------------------------------------------------------------------------------------------"
                Cabe3 = TEXTO
                Call Acrescenta_Linha_Relatorio_Servicos(Lista_Preco, LinhaRelServicos, TEXTO, Banco.UsuarioSistema)
                Cabecalho()

                TEXTO = Microsoft.VisualBasic.Left("Serviço " & s(31), 31)
                TEXTO = TEXTO & Microsoft.VisualBasic.Left("Período " & s(8), 8)
                TEXTO = TEXTO & Microsoft.VisualBasic.Left("Dias " & s(6), 6)
                TEXTO = TEXTO & Microsoft.VisualBasic.Left("T.Carga " & s(8), 8)
                TEXTO = TEXTO & Microsoft.VisualBasic.Left("Variante " & s(10), 10)
                TEXTO = TEXTO & Microsoft.VisualBasic.Left("B.Cálculo " & s(10), 10)
                TEXTO = TEXTO & Microsoft.VisualBasic.Left("Moeda " & s(11), 11)
                TEXTO = TEXTO & Microsoft.VisualBasic.Left("Preço Unit. " & s(12), 12)
                TEXTO = TEXTO & Microsoft.VisualBasic.Left("Preço Min. " & s(12), 12)
                TEXTO = TEXTO & Microsoft.VisualBasic.Left("Perigosa % " & s(12), 12)
                TEXTO = TEXTO & Microsoft.VisualBasic.Left("Acr.Peso " & s(9), 9)
                TEXTO = TEXTO & Microsoft.VisualBasic.Left("Limite   " & s(9), 9)
                TEXTO = TEXTO & Microsoft.VisualBasic.Left("ProRata" & s(7), 7)
                Cabe4 = TEXTO
                Call Acrescenta_Linha_Relatorio_Servicos(Lista_Preco, LinhaRelServicos, TEXTO, Banco.UsuarioSistema)
                Cabecalho()

                TEXTO = Microsoft.VisualBasic.Left("============================================================================================================================================" & s(150), 150)
                Cabe5 = TEXTO
                Call Acrescenta_Linha_Relatorio_Servicos(Lista_Preco, LinhaRelServicos, TEXTO, Banco.UsuarioSistema)
                tcarga1 = ""
                Do While Not TBSERVPER.EOF()
                    If TCarga <> Mid$(TBSERVPER.Fields("TIPO_CARGA").Value.ToString(), 1, 3) And TCarga <> "" Then
                        Cabecalho()
                        TEXTO = Microsoft.VisualBasic.Left("============================================================================================================================================" & s(150), 150)
                        Call Acrescenta_Linha_Relatorio_Servicos(Lista_Preco, LinhaRelServicos, TEXTO, Banco.UsuarioSistema)
                    End If
                    TCarga = Mid$(TBSERVPER.Fields("TIPO_CARGA").Value.ToString(), 1, 3)
                    Cabecalho()

                    TEXTO = Microsoft.VisualBasic.Left(TBSERVPER.Fields("Servico").Value.ToString() & s(33), 33)
                    TEXTO = TEXTO & Microsoft.VisualBasic.Left(TBSERVPER.Fields("N_Periodo").Value.ToString() & s(9), 9)
                    TEXTO = TEXTO & Microsoft.VisualBasic.Left(TBSERVPER.Fields("qtde_dias").Value.ToString() & s(8), 8)
                    TEXTO = TEXTO & Microsoft.VisualBasic.Left(TBSERVPER.Fields("TIPO_CARGA").Value.ToString() & s(9), 9)
                    TEXTO = TEXTO & Microsoft.VisualBasic.Left(TBSERVPER.Fields("VARIANTE_LOCAL").Value.ToString() & s(11), 11)
                    TEXTO = TEXTO & Microsoft.VisualBasic.Left(TBSERVPER.Fields("BASE_CALCULO").Value.ToString() & s(8), 8)
                    TEXTO = TEXTO & Microsoft.VisualBasic.Left(TBSERVPER.Fields("Descricao").Value.ToString() & s(16), 16)

                    TEXTO = TEXTO & Microsoft.VisualBasic.Left(String.Format("{0:C2}", Convert.ToDecimal(TBSERVPER.Fields("preco_unitario").Value.ToString())) & s(13), 13)
                    TEXTO = TEXTO & Microsoft.VisualBasic.Left(String.Format("{0:C2}", Convert.ToDecimal(TBSERVPER.Fields("Preco_Minimo").Value.ToString())) & s(12), 12)
                    TEXTO = TEXTO & Microsoft.VisualBasic.Left(String.Format("{0:C2}", Convert.ToDecimal(TBSERVPER.Fields("VALOR_ACRESCIMO").Value.ToString())) & s(13), 13)

                    TEXTO = TEXTO & s(3)
                    TEXTO = TEXTO & NNull(TBSERVPER.Fields("pr").Value.ToString(), 1)
                    PRSet(TBLOCALATRAC, "SELECT DESCRICAO DESCR FROM " & Banco.BancoSGIPA & "tb_grupos_atracacao WHERE ID = " & NNull(TBSERVPER.Fields("grupo_ATRACACAO"), 0) & " ", 0)
                    TEXTO = TEXTO & Microsoft.VisualBasic.Left(NNull(TBLOCALATRAC.Fields(0), 1) & s(17), 17)
                    TBLOCALATRAC.Close()
                    Call Acrescenta_Linha_Relatorio_Servicos(Lista_Preco, LinhaRelServicos, TEXTO, Banco.UsuarioSistema)
                    PRSet(tbMin, "SELECT nbls, valorminimo,percmulta,tipo,autonumsv,autonum  FROM TB_LISTA_CFG_VALORMINIMO where autonumsv =" & TBSERVPER.Fields("Autonum").Value.ToString() & " order by tipo,nbls")
                    If Not tbMin.EOF Then
                        Cabecalho()
                        TEXTO = "----------"
                        TEXTO = TEXTO & Microsoft.VisualBasic.Left("N. BLS " & s(21), 21)
                        TEXTO = TEXTO & Microsoft.VisualBasic.Left("Valor Minimo " & s(15), 15)
                        TEXTO = TEXTO & Microsoft.VisualBasic.Left("Multa % " & s(15), 15)
                        TEXTO = TEXTO & Microsoft.VisualBasic.Left("T.Carga " & s(15), 15)
                        Call Acrescenta_Linha_Relatorio_Servicos(Lista_Preco, LinhaRelServicos, TEXTO, Banco.UsuarioSistema)
                        antbl = 0
                        TCarga = ""
                        Do While Not tbMin.EOF() = True
                            If TCarga <> Mid$(tbMin.Fields("Tipo").Value.ToString(), 1, 3) And TCarga <> "" Then
                                Cabecalho()
                                TEXTO = Microsoft.VisualBasic.Left("============================================================================================================================================" & s(150), 150)
                                Call Acrescenta_Linha_Relatorio_Servicos(Lista_Preco, LinhaRelServicos, TEXTO, Banco.UsuarioSistema)
                            End If
                            TCarga = Mid$(tbMin.Fields("Tipo").Value.ToString(), 1, 3)
                            Cabecalho()
                            TEXTO = "----------"
                            If antbl > Val(NNull(tbMin.Fields("nBLs").Value.ToString(), 0)) Then antbl = 0
                            TEXTO = TEXTO & "DE  " & Microsoft.VisualBasic.Left(antbl + 1 & s(10), 10)
                            antbl = Val(NNull(tbMin.Fields("nBLs").Value.ToString(), 0))
                            TEXTO = TEXTO & "ATÉ " & Microsoft.VisualBasic.Left(NNull(tbMin.Fields("nBLs").Value.ToString(), 0) & s(11), 11)
                            TEXTO = TEXTO & Microsoft.VisualBasic.Left(tbMin.Fields("ValorMINIMO").Value.ToString() & s(15), 15)
                            TEXTO = TEXTO & Microsoft.VisualBasic.Left(tbMin.Fields("PercMulta").Value.ToString() & s(15), 15)
                            TEXTO = TEXTO & Microsoft.VisualBasic.Left(tbMin.Fields("Tipo").Value.ToString() & s(15), 15)
                            Call Acrescenta_Linha_Relatorio_Servicos(Lista_Preco, LinhaRelServicos, TEXTO, Banco.UsuarioSistema)
                            tbMin.MoveNext()
                        Loop
                        Cabecalho()
                        TEXTO = Microsoft.VisualBasic.Left("============================================================================================================================================" & s(150), 150)
                        Call Acrescenta_Linha_Relatorio_Servicos(Lista_Preco, LinhaRelServicos, TEXTO, Banco.UsuarioSistema)
                    End If
                    PRSet(tbMin, "SELECT VALORINICIAL, valorfinal,percentual  ,Minimo FROM TB_LISTA_P_S_FAIXASCIF_PER where autonumsv =" & TBSERVPER.Fields("Autonum").Value.ToString() & " order by VALORINICIAL")
                    If Not tbMin.EOF Then
                        Cabecalho()
                        TEXTO = "----------"
                        TEXTO = TEXTO & Microsoft.VisualBasic.Left("Valor Minimo " & s(15), 15)
                        TEXTO = TEXTO & Microsoft.VisualBasic.Left("Valor Maximo " & s(15), 15)
                        TEXTO = TEXTO & Microsoft.VisualBasic.Left("Percentual % " & s(15), 15)
                        TEXTO = TEXTO & Microsoft.VisualBasic.Left("Minimo       " & s(15), 15)
                        Call Acrescenta_Linha_Relatorio_Servicos(Lista_Preco, LinhaRelServicos, TEXTO, Banco.UsuarioSistema)
                        Do While Not tbMin.EOF() = True
                            Cabecalho()
                            TEXTO = "----------"
                            TEXTO = TEXTO & Microsoft.VisualBasic.Left(tbMin.Fields("ValorInicial").Value.ToString() & s(15), 15)
                            TEXTO = TEXTO & Microsoft.VisualBasic.Left(tbMin.Fields("ValorFinal").Value.ToString() & s(15), 15)
                            TEXTO = TEXTO & Microsoft.VisualBasic.Left(tbMin.Fields("Percentual").Value.ToString() & s(15), 15)
                            TEXTO = TEXTO & Microsoft.VisualBasic.Left(tbMin.Fields("Minimo").Value.ToString() & s(15), 15)
                            Call Acrescenta_Linha_Relatorio_Servicos(Lista_Preco, LinhaRelServicos, TEXTO, Banco.UsuarioSistema)
                            tbMin.MoveNext()
                        Loop
                        Cabecalho()
                        TEXTO = Microsoft.VisualBasic.Left("============================================================================================================================================" & s(150), 150)
                        Call Acrescenta_Linha_Relatorio_Servicos(Lista_Preco, LinhaRelServicos, TEXTO, Banco.UsuarioSistema)
                    End If
                    TBSERVPER.MoveNext()
                Loop
            End If
            TBSERVPER.Close()
            TBSERVPER = Nothing
        End If

    End Sub

    Private Sub Cabecalho()

        Dim TBFORMA As New ADODB.Recordset
        Dim TBV As New ADODB.Recordset
        Dim sql As String
        ''LinhaRelServicos += 1
        If LinhaRelServicos Mod 35 = 0 Or LinhaRelServicos = 0 Then

            If LinhaRelServicos > 1 Then
                TEXTO = "-"
                Call Acrescenta_Linha_Relatorio_Servicos(Lista_Preco, LinhaRelServicos, TEXTO, Banco.UsuarioSistema)

            End If
            TEXTO = "Id: " & Microsoft.VisualBasic.Left(Tbhs.Fields("Autonum").Value.ToString() & s(6), 6) & "  Descrição Tabela: " & NNull(Tbhs.Fields("Tabela").Value.ToString(), 1)
            Call Acrescenta_Linha_Relatorio_Servicos(Lista_Preco, LinhaRelServicos, TEXTO, Banco.UsuarioSistema)
            If Trim(NNull(Tbhs.Fields("Obs").Value.ToString(), 1)) <> String.Empty Then

                TEXTO = "Obs: " & Mid$(NNull(Tbhs.Fields("Obs").Value.ToString(), 1) + Space(200), 1, 140)
                TEXTO = Replace(TEXTO, Chr(10), " ")
                TEXTO = Replace(TEXTO, Chr(13), " ")
                Call Acrescenta_Linha_Relatorio_Servicos(Lista_Preco, LinhaRelServicos, TEXTO, Banco.UsuarioSistema)
            End If
            If Trim(Mid$(Trim(NNull(Tbhs.Fields("Obs").Value.ToString(), 1)) + Space(400), 141, 140)) <> String.Empty Then

                TEXTO = "Obs: " & Mid$(NNull(Tbhs.Fields("Obs").Value.ToString(), 1) + Space(400), 141, 140)
                TEXTO = Replace(TEXTO, Chr(10), " ")
                TEXTO = Replace(TEXTO, Chr(13), " ")
                Call Acrescenta_Linha_Relatorio_Servicos(Lista_Preco, LinhaRelServicos, TEXTO, Banco.UsuarioSistema)
            End If
            If Trim(Mid$(Trim(NNull(Tbhs.Fields("Obs").Value.ToString(), 1)) + Space(400), 281, 140)) <> String.Empty Then

                TEXTO = "Obs: " & Mid$(NNull(Tbhs.Fields("Obs").Value.ToString(), 1) + Space(4200), 281, 140)
                TEXTO = Replace(TEXTO, Chr(10), " ")
                TEXTO = Replace(TEXTO, Chr(13), " ")
                Call Acrescenta_Linha_Relatorio_Servicos(Lista_Preco, LinhaRelServicos, TEXTO, Banco.UsuarioSistema)
            End If
            If LinhaRelServicos < 31 Then

                TEXTO = "Importador: " & Microsoft.VisualBasic.Left(Tbhs.Fields("Nome_Parceiro").Value.ToString() & s(50), 50)
                Call Acrescenta_Linha_Relatorio_Servicos(Lista_Preco, LinhaRelServicos, TEXTO, Banco.UsuarioSistema)

                TEXTO = "Despachante: " & Microsoft.VisualBasic.Left(Tbhs.Fields("Despachante").Value.ToString() & s(50), 50)
                Call Acrescenta_Linha_Relatorio_Servicos(Lista_Preco, LinhaRelServicos, TEXTO, Banco.UsuarioSistema)

                TEXTO = "NVOCC   : " & Microsoft.VisualBasic.Left(Tbhs.Fields("Captador").Value.ToString() & s(50), 50)
                Call Acrescenta_Linha_Relatorio_Servicos(Lista_Preco, LinhaRelServicos, TEXTO, Banco.UsuarioSistema)

                TEXTO = "Prazo de retirada após GR em dias: " & Microsoft.VisualBasic.Left(NNull(Tbhs.Fields("DIAS_APOS_GR").Value.ToString(), 0) & s(50), 50)
                Call Acrescenta_Linha_Relatorio_Servicos(Lista_Preco, LinhaRelServicos, TEXTO, Banco.UsuarioSistema)
                PRSet(TBFORMA, "Select Descr from tb_formas_pagamento where autonum=" & Tbhs.Fields("Forma_Pagamento").Value.ToString(), 0)

                TEXTO = "Forma de Pagamento: " & Microsoft.VisualBasic.Left(NNull(TBFORMA.Fields("Descr").Value.ToString(), 1) & s(50), 50)
                Call Acrescenta_Linha_Relatorio_Servicos(Lista_Preco, LinhaRelServicos, TEXTO, Banco.UsuarioSistema)
                TBFORMA.Close()

                TEXTO = String.Empty
                If Tbhs.Fields("FLAG_LIBERADA").Value.ToString() = 1 Then TEXTO = TEXTO & Microsoft.VisualBasic.Left("Liberada" & s(10), 10)
                TEXTO = TEXTO & Microsoft.VisualBasic.Left("Data Inicio: " & Convert.ToDateTime(Tbhs.Fields("DATA_INICIO").Value.ToString()).ToString("dd/mm/yyyy") & s(23), 23)
                Call Acrescenta_Linha_Relatorio_Servicos(Lista_Preco, LinhaRelServicos, TEXTO, Banco.UsuarioSistema)

                TEXTO = String.Empty
                TEXTO = "Proposta : " & NNull(Tbhs.Fields("Proposta").Value.ToString(), 1) & "    Vendedor(es) : "
                PRSet(TBV, "Select a.razao from " & Banco.BancoSGIPA & "tb_cad_parceiros a, " & Banco.BancoSGIPA & "tb_listas_precos_vendedores b where b.vendedor = a.autonum and b.lista = " & Tbhs.Fields("Autonum").Value.ToString())
                Do While Not TBV.EOF
                    TEXTO = TEXTO & NNull(TBV.Fields("Razao").Value.ToString(), 1) & "  -  "
                    TBV.MoveNext()
                Loop
                TBV.Close()
                Call Acrescenta_Linha_Relatorio_Servicos(Lista_Preco, LinhaRelServicos, TEXTO, Banco.UsuarioSistema)
                sql = "select lote from " & Banco.BancoSGIPA & "tb_lp_lotes where autonum_lista=" & Tbhs.Fields("Autonum").Value.ToString() & " order by lote"
                PRSet(TBV, sql)
                TEXTO = ""
                Do While Not TBV.EOF
                    TEXTO = TEXTO & TBV.Fields("Lote").Value.ToString() & "  /  "
                    TBV.MoveNext()
                Loop
                TBV.Close()
                If TEXTO <> String.Empty Then
                    TEXTO = "Lotes Vinculados : " & TEXTO

                    Call Acrescenta_Linha_Relatorio_Servicos(Lista_Preco, LinhaRelServicos, TEXTO, Banco.UsuarioSistema)
                End If
            End If

            TEXTO = "============================================================================================================================================"
            Call Acrescenta_Linha_Relatorio_Servicos(Lista_Preco, LinhaRelServicos, TEXTO, Banco.UsuarioSistema)
            If Cabe1 <> "" Then

                Call Acrescenta_Linha_Relatorio_Servicos(Lista_Preco, LinhaRelServicos, Cabe1, Banco.UsuarioSistema)

                Call Acrescenta_Linha_Relatorio_Servicos(Lista_Preco, LinhaRelServicos, Cabe2, Banco.UsuarioSistema)

                Call Acrescenta_Linha_Relatorio_Servicos(Lista_Preco, LinhaRelServicos, Cabe3, Banco.UsuarioSistema)

                Call Acrescenta_Linha_Relatorio_Servicos(Lista_Preco, LinhaRelServicos, Cabe4, Banco.UsuarioSistema)

                Call Acrescenta_Linha_Relatorio_Servicos(Lista_Preco, LinhaRelServicos, Cabe5, Banco.UsuarioSistema)
            End If

        End If

    End Sub

    Public Sub Acrescenta_Linha_Relatorio_Servicos(ByVal Campo1 As Long, ByVal Campo2 As Integer, ByVal Campo3 As String, ByVal Campo4 As Long)

        Dim sql As String

        sql = "INSERT INTO " & Banco.BancoSGIPA & "TEMP_REL_SERVICOS (Lista_Preco, linha, texto,usuario) values "
        sql = sql & "(" & NNull(Campo1, 0) & ""
        sql = sql & "," & NNull(Campo2, 0) & ""
        sql = sql & ",'" & NNull(Campo3, 1) & "'"
        sql = sql & "," & NNull(Campo4, 1) & ")"
        Banco.Execute(sql)
        LinhaRelServicos += 1

    End Sub

    Function s(ByVal TAM As Integer)
        s = Microsoft.VisualBasic.Left("                                                                            ", TAM)
    End Function

End Module
