Imports System.IO
Imports System.ComponentModel
Imports System.Threading

Public Class Principal

    ' Variables de objetos de entidades.
    Public usuarios As New ALMEntidadesDevoluciones.Usuarios()
    Public devoluciones As New ALMEntidadesDevoluciones.Devoluciones()
    Public ventas As New ALMEntidadesDevoluciones.Ventas()
    Public vales As New ALMEntidadesDevoluciones.Vales()
    Public valesDetalle As New ALMEntidadesDevoluciones.ValesDetalle()
    Public entradas As New ALMEntidadesDevoluciones.Entradas()
    Public almacenes As New ALMEntidadesDevoluciones.Almacenes()
    Public familias As New ALMEntidadesDevoluciones.Familias()
    Public subFamilias As New ALMEntidadesDevoluciones.SubFamilias()
    Public articulos As New ALMEntidadesDevoluciones.Articulos()
    Public unidadesMedidas As New ALMEntidadesDevoluciones.UnidadesMedidas()
    Public clientes As New ALMEntidadesDevoluciones.Clientes()
    Public empresas As New ALMEntidadesDevoluciones.Empresas()
    ' Variables de tipos de datos de spread.
    Public tipoTexto As New FarPoint.Win.Spread.CellType.TextCellType()
    Public tipoTextoContrasena As New FarPoint.Win.Spread.CellType.TextCellType()
    Public tipoEntero As New FarPoint.Win.Spread.CellType.NumberCellType()
    Public tipoDoble As New FarPoint.Win.Spread.CellType.NumberCellType()
    Public tipoPorcentaje As New FarPoint.Win.Spread.CellType.PercentCellType()
    Public tipoHora As New FarPoint.Win.Spread.CellType.DateTimeCellType()
    Public tipoFecha As New FarPoint.Win.Spread.CellType.DateTimeCellType()
    Public tipoBooleano As New FarPoint.Win.Spread.CellType.CheckBoxCellType()
    Public tipoMoneda As New FarPoint.Win.Spread.CellType.CurrencyCellType()
    ' Variables de tamaños y posiciones de spreads.
    Public anchoTotal As Integer = 0 : Public altoTotal As Integer = 0
    Public anchoMitad As Integer = 0 : Public altoMitad As Integer = 0
    Public anchoTercio As Integer = 0 : Public altoTercio As Integer = 0 : Public altoCuarto As Integer = 0
    Public izquierda As Integer = 0 : Public arriba As Integer = 0
    ' Variables de formatos de spread.
    Public Shared tipoLetraSpread As String = "Microsoft Sans Serif" : Public Shared tamañoLetraSpread As Integer = 8
    Public Shared alturaFilasEncabezadosGrandesSpread As Integer = 35 : Public Shared alturaFilasEncabezadosMedianosSpread As Integer = 28
    Public Shared alturaFilasEncabezadosChicosSpread As Integer = 22 : Public Shared alturaFilasSpread As Integer = 20
    ' Variables de estilos.
    Public Shared colorSpreadAreaGris As Color = Color.FromArgb(245, 245, 245) : Public Shared colorSpreadTotal As Color = Color.White
    Public Shared colorCaptura As Color = Color.White : Public Shared colorCapturaBloqueada As Color = Color.FromArgb(235, 255, 255)
    Public Shared colorAdvertencia As Color = Color.Orange
    Public Shared colorTemaAzul As Color = Color.FromArgb(99, 160, 162)
    ' Variables generales.
    Public nombreEstePrograma As String = String.Empty
    Public estaCerrando As Boolean = False
    Public estaMostrado As Boolean = False
    Public ejecutarProgramaPrincipal As New ProcessStartInfo()
    Public prefijoBaseDatosAlmacen As String = "ALM" & "_"
    Public cantidadFilas As Integer = 1
    Public opcionCatalogoSeleccionada As Integer = 0
    Public esGuardadoValido As Boolean = True
    Public esIzquierda As Boolean = False
    Public datosCatalogo As New DataTable
    Public opcionTipoSeleccionada As Integer = -1
    ' Variables de Impresión.
    Public datosValesParaImprimir As New DataTable
    Public contadorValesParaImprimir As Integer = 0 : Public listaValesParaImprimir As New List(Of System.Data.DataRow)
    Public estaImprimiendo As Boolean = False
    ' Variables fijas.
    Public idOrigen As Integer = 4 ' Siempre será 4 para devoluciones.
    ' Hilos para carga rápida.
    Public hiloCentrar As New Thread(AddressOf Centrar)
    Public hiloNombrePrograma As New Thread(AddressOf CargarNombrePrograma)
    Public hiloEncabezadosTitulos As New Thread(AddressOf CargarEncabezadosTitulos)
    ' Variable de desarrollo.
    Public esDesarrollo As Boolean = False

#Region "Eventos"

    Private Sub Principal_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.Cursor = Cursors.WaitCursor
        MostrarCargando(True)
        ConfigurarConexiones()
        IniciarHilosCarga()
        AsignarTooltips()
        CargarMedidas()
        Me.Cursor = Cursors.Default

    End Sub

    Private Sub Principal_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        Me.Cursor = Cursors.WaitCursor
        'If (Not ValidarAccesoTotal()) Then
        '    Salir()
        'End If 
        FormatearSpread()
        FormatearSpreadDevoluciones()
        CargarComboClientes()
        CargarOpcionesImpresion()
        CargarIdConsecutivo()
        AsignarFoco(txtId)
        CargarEstilos()
        Me.estaMostrado = True
        MostrarCargando(False)
        Me.Cursor = Cursors.Default

    End Sub

    Private Sub Principal_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed

        Me.Cursor = Cursors.WaitCursor
        Dim nombrePrograma As String = "PrincipalBerry"
        AbrirPrograma(nombrePrograma, True)
        System.Threading.Thread.Sleep(3000)
        Me.Cursor = Cursors.Default

    End Sub

    Private Sub Principal_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing

        Me.Cursor = Cursors.WaitCursor
        Me.estaCerrando = True
        MostrarCargando(True)
        Desvanecer()
        Me.Cursor = Cursors.Default

    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click

        Salir()

    End Sub

    Private Sub spDevoluciones_DialogKey(sender As Object, e As FarPoint.Win.Spread.DialogKeyEventArgs) Handles spDevoluciones.DialogKey

        If (e.KeyData = Keys.Enter) Then
            ControlarSpreadEnter(spDevoluciones)
        End If

    End Sub

    Private Sub spDevoluciones_KeyDown(sender As Object, e As KeyEventArgs) Handles spDevoluciones.KeyDown

        Me.Cursor = Cursors.WaitCursor
        If (e.KeyData = Keys.F6) Then ' Eliminar un registro.
            If (MessageBox.Show("Confirmas que deseas eliminar el registro seleccionado?", "Confirmación.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes) Then
                EliminarRegistroDeSpread(spDevoluciones)
            End If
        ElseIf (e.KeyData = Keys.Enter) Then ' Validar registros.
            ControlarSpreadEnter(spDevoluciones)
        ElseIf (e.KeyData = Keys.F5) Then ' Abrir catalogos.
            CargarCatalogoEnSpread()
        ElseIf (e.KeyData = Keys.Escape) Then
            spDevoluciones.ActiveSheet.SetActiveCell(0, 0)
            AsignarFoco(cbClientes)
        End If
        Me.Cursor = Cursors.Default

    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click

        Me.Cursor = Cursors.WaitCursor
        ValidarGuardadoDevoluciones()
        If (Me.esGuardadoValido) Then
            GuardarEditarDevoluciones()
        End If
        Me.Cursor = Cursors.Default

    End Sub

    Private Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click

        Me.Cursor = Cursors.WaitCursor
        EliminarDevoluciones(True)
        Me.Cursor = Cursors.Default

    End Sub

    Private Sub btnGuardar_MouseEnter(sender As Object, e As EventArgs) Handles btnGuardar.MouseEnter

        AsignarTooltips("Guardar")

    End Sub

    Private Sub btnEliminar_MouseEnter(sender As Object, e As EventArgs) Handles btnEliminar.MouseEnter

        AsignarTooltips("Eliminar")

    End Sub

    Private Sub btnSalir_MouseEnter(sender As Object, e As EventArgs) Handles btnSalir.MouseEnter

        AsignarTooltips("Salir")

    End Sub

    Private Sub pnlEncabezado_MouseEnter(sender As Object, e As EventArgs) Handles pnlEncabezado.MouseEnter, pnlCuerpo.MouseEnter

        AsignarTooltips(String.Empty)

    End Sub

    Private Sub spCatalogos_CellClick(sender As Object, e As FarPoint.Win.Spread.CellClickEventArgs) Handles spCatalogos.CellClick

        Dim fila As Integer = e.Row
        If (Me.opcionCatalogoSeleccionada = OpcionCatalogo.cliente) Then
            CargarDatosEnOtrosDeCatalogos(fila)
        Else
            CargarDatosEnSpreadDeCatalogos(fila)
        End If

    End Sub

    Private Sub spCatalogos_CellDoubleClick(sender As Object, e As FarPoint.Win.Spread.CellClickEventArgs) Handles spCatalogos.CellDoubleClick

        VolverFocoDeCatalogos()

    End Sub

    Private Sub spCatalogos_KeyDown(sender As Object, e As KeyEventArgs) Handles spCatalogos.KeyDown


        If (e.KeyCode = Keys.Enter) Then
            Dim fila As Integer = spCatalogos.ActiveSheet.ActiveRowIndex
            If (Me.opcionCatalogoSeleccionada = OpcionCatalogo.almacen Or Me.opcionCatalogoSeleccionada = OpcionCatalogo.cliente Or Me.opcionCatalogoSeleccionada = OpcionCatalogo.moneda Or Me.opcionCatalogoSeleccionada = OpcionCatalogo.tipoSalida) Then
                CargarDatosEnOtrosDeCatalogos(fila)
            Else
                CargarDatosEnSpreadDeCatalogos(fila)
            End If
        ElseIf (e.KeyCode = Keys.Escape) Then
            VolverFocoDeCatalogos()
        End If

    End Sub

    Private Sub temporizador_Tick(sender As Object, e As EventArgs) Handles temporizador.Tick

        If (Me.estaCerrando) Then
            Desvanecer()
        End If

    End Sub

    Private Sub btnAyuda_Click(sender As Object, e As EventArgs) Handles btnAyuda.Click

        Me.Cursor = Cursors.WaitCursor
        MostrarAyuda()
        Me.Cursor = Cursors.Default

    End Sub

    Private Sub btnAyuda_MouseEnter(sender As Object, e As EventArgs) Handles btnAyuda.MouseEnter

        AsignarTooltips("Ayuda")

    End Sub

    Private Sub txtId_KeyDown(sender As Object, e As KeyEventArgs) Handles txtId.KeyDown

        If (e.KeyData = Keys.Enter) Then
            e.SuppressKeyPress = True
            If (IsNumeric(txtId.Text)) Then
                e.SuppressKeyPress = True
                CargarDevoluciones()
            Else
                txtId.Clear()
                LimpiarPantalla()
            End If
        End If

    End Sub

    Private Sub dtpFecha_KeyDown(sender As Object, e As KeyEventArgs) Handles dtpFecha.KeyDown

        If (e.KeyData = Keys.Enter) Then
            e.SuppressKeyPress = True
            AsignarFoco(cbClientes)
        ElseIf (e.KeyData = Keys.Escape) Then
            e.SuppressKeyPress = True
            AsignarFoco(txtId)
        End If

    End Sub

    Private Sub cbClientes_KeyDown(sender As Object, e As KeyEventArgs) Handles cbClientes.KeyDown

        If (e.KeyData = Keys.Enter) Then
            e.SuppressKeyPress = True
            If (cbClientes.SelectedValue > 0) Then
                AsignarFoco(spDevoluciones)
                spDevoluciones.ActiveSheet.SetActiveCell(0, 1)
            Else
                cbClientes.SelectedIndex = 0
            End If
        ElseIf (e.KeyData = Keys.Escape) Then
            e.SuppressKeyPress = True
            AsignarFoco(spDevoluciones)
        ElseIf (e.KeyData = Keys.F5) Then ' Abrir catalogos.
            Me.opcionCatalogoSeleccionada = OpcionCatalogo.cliente
            CargarCatalogoEnOtros()
        End If

    End Sub

    Private Sub btnIdAnterior_Click(sender As Object, e As EventArgs) Handles btnIdAnterior.Click

        If (ALMLogicaDevoluciones.Funciones.ValidarNumeroACero(txtId.Text) > 1) Then
            txtId.Text -= 1
            CargarDevoluciones()
        End If

    End Sub

    Private Sub btnIdSiguiente_Click(sender As Object, e As EventArgs) Handles btnIdSiguiente.Click

        If (ALMLogicaDevoluciones.Funciones.ValidarNumeroACero(txtId.Text) >= 1) Then
            txtId.Text += 1
            CargarDevoluciones()
        End If

    End Sub

    Private Sub btnMostrarOcultar_Click(sender As Object, e As EventArgs) Handles btnMostrarOcultar.Click

        MostrarOcultar()

    End Sub

    Private Sub btnMostrarOcultar_MouseEnter(sender As Object, e As EventArgs) Handles btnMostrarOcultar.MouseEnter

        If (Me.esIzquierda) Then
            AsignarTooltips("Mostrar")
        Else
            AsignarTooltips("Ocultar")
        End If

    End Sub

    Private Sub txtBuscarCatalogo_TextChanged(sender As Object, e As EventArgs) Handles txtBuscarCatalogo.TextChanged

        Me.Cursor = Cursors.WaitCursor
        If (Me.opcionCatalogoSeleccionada = OpcionCatalogo.articulo) Then
            BuscarCatalogosRapidoArticulos()
        Else
            BuscarCatalogos()
        End If
        Me.Cursor = Cursors.Default

    End Sub

    Private Sub txtBuscarCatalogo_KeyDown(sender As Object, e As KeyEventArgs) Handles txtBuscarCatalogo.KeyDown

        If (e.KeyCode = Keys.Enter) Then
            AsignarFoco(spCatalogos)
        ElseIf (e.KeyCode = Keys.Escape) Then
            VolverFocoDeCatalogos()
        End If

    End Sub

    Private Sub chkMostrarDetallado_CheckedChanged(sender As Object, e As EventArgs) Handles chkMostrarDetallado.CheckedChanged

        Me.Cursor = Cursors.WaitCursor
        If (Me.estaMostrado) Then
            If (chkMostrarDetallado.Checked) Then
                MostrarOcultarDetalle(True)
            Else
                MostrarOcultarDetalle(False)
            End If
        End If
        Me.Cursor = Cursors.Default

    End Sub

    Private Sub btnConfigurarImpresion_Click(sender As Object, e As EventArgs) Handles btnConfigurarImpresion.Click

        Me.Cursor = Cursors.WaitCursor
        Impresoras.Show()
        Impresoras.BringToFront()
        Me.Enabled = False
        Me.Cursor = Cursors.Default

    End Sub

    Private Sub pdVale_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles pdVale.PrintPage

        CrearFormatoVale(e)

    End Sub

    Private Sub btnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click

        Me.Cursor = Cursors.WaitCursor
        ImprimirManualmente()
        Me.Cursor = Cursors.Default

    End Sub

    Private Sub btnImprimir_MouseEnter(sender As Object, e As EventArgs) Handles btnImprimir.MouseEnter

        AsignarTooltips("Imprimir")

    End Sub

    Private Sub btnConfigurarImpresion_MouseEnter(sender As Object, e As EventArgs) Handles btnConfigurarImpresion.MouseEnter

        AsignarTooltips("Configurar Impresoras")

    End Sub

    Private Sub pbMarca_MouseEnter(sender As Object, e As EventArgs) Handles pbMarca.MouseEnter

        AsignarTooltips("Producido por Berry")

    End Sub

    Private Sub pnlCapturaSuperior_MouseEnter(sender As Object, e As EventArgs) Handles pnlCapturaSuperior.MouseEnter

        AsignarTooltips("Capturar Datos Generales")

    End Sub

    Private Sub spDevoluciones_MouseEnter(sender As Object, e As EventArgs) Handles spDevoluciones.MouseEnter

        AsignarTooltips("Capturar Datos Detallados")

    End Sub

    Private Sub pnlPie_MouseEnter(sender As Object, e As EventArgs) Handles pnlPie.MouseEnter

        AsignarTooltips("Opciones")

    End Sub

#End Region

#Region "Métodos"

#Region "Básicos"

    Private Sub CargarEstilos()

        pnlCapturaSuperior.BackColor = Principal.colorSpreadAreaGris
        pnlTotales.BackColor = Principal.colorSpreadAreaGris
        pnlPie.BackColor = Principal.colorSpreadAreaGris
        spDevoluciones.ActiveSheet.GrayAreaBackColor = Principal.colorSpreadAreaGris

    End Sub

    Private Sub BuscarCatalogos()

        Dim valorBuscado As String = txtBuscarCatalogo.Text.Replace("á", "a").Replace("é", "e").Replace("í", "i").Replace("ó", "o").Replace("ú", "u")
        For fila = 0 To spCatalogos.ActiveSheet.Rows.Count - 1
            Dim valorSpread As String = ALMLogicaDevoluciones.Funciones.ValidarLetra(spCatalogos.ActiveSheet.Cells(fila, spCatalogos.ActiveSheet.Columns("id").Index).Text & spCatalogos.ActiveSheet.Cells(fila, spCatalogos.ActiveSheet.Columns("nombre").Index).Text).Replace("á", "a").Replace("é", "e").Replace("í", "i").Replace("ó", "o").Replace("ú", "u")
            If (valorSpread.ToUpper.Contains(valorBuscado.ToUpper)) Then
                spCatalogos.ActiveSheet.Rows(fila).Visible = True
            Else
                spCatalogos.ActiveSheet.Rows(fila).Visible = False
            End If
        Next

    End Sub

    Private Sub BuscarCatalogosRapidoArticulos()

        If (Me.datosCatalogo.Rows.Count > 0) Then
            Dim valorBuscado As String = txtBuscarCatalogo.Text.Replace("á", "a").Replace("é", "e").Replace("í", "i").Replace("ó", "o").Replace("ú", "u")
            Dim consulta As String = "Id + Nombre + Codigo + Pagina + Color + Talla + Modelo + CodigoInternet + Precio LIKE '*" & valorBuscado & "*'"
            Dim ordenamiento As String = "Id ASC"
            Dim filasResultado As DataRow()
            filasResultado = Me.datosCatalogo.[Select](consulta, ordenamiento)
            If (filasResultado.Count > 0) Then
                spCatalogos.ActiveSheet.DataSource = filasResultado.CopyToDataTable
            Else
                spCatalogos.ActiveSheet.DataSource = Nothing
                spCatalogos.ActiveSheet.Rows.Count = 0
            End If
            FormatearSpreadAnchoColumnasArticulos()
        End If

    End Sub

    Private Sub MostrarCargando(ByVal mostrar As Boolean)

        Dim pnlCargando As New Panel
        Dim lblCargando As New Label
        Dim crear As Boolean = False
        If (Me.Controls.Find("pnlCargando", True).Count = 0) Then ' Si no existe, se crea. 
            crear = True
        Else ' Si existe, se obtiene.
            pnlCargando = Me.Controls.Find("pnlCargando", False)(0)
            crear = False
        End If
        If (crear And mostrar) Then ' Si se tiene que crear y mostrar.
            ' Imagen de fondo.
            Try
                pnlCargando.BackgroundImage = Image.FromFile(String.Format("{0}\{1}\{2}", IIf(Me.esDesarrollo, "W:", Application.StartupPath), "Imagenes", "cargando.png"))
            Catch
                pnlCargando.BackgroundImage = Image.FromFile(String.Format("{0}\{1}\{2}", IIf(Me.esDesarrollo, "W:", Application.StartupPath), "Imagenes", "logoBerry.png"))
            End Try
            pnlCargando.BackgroundImageLayout = ImageLayout.Center
            pnlCargando.BackColor = Color.DarkSlateGray
            pnlCargando.Width = Me.Width
            pnlCargando.Height = Me.Height
            pnlCargando.Location = New Point(Me.Location)
            pnlCargando.Name = "pnlCargando"
            pnlCargando.Visible = True
            Me.Controls.Add(pnlCargando)
            ' Etiqueta de cargando.
            lblCargando.Text = "¡cargando!"
            lblCargando.BackColor = pnlCargando.BackColor
            lblCargando.ForeColor = Color.White
            lblCargando.AutoSize = False
            lblCargando.Width = Me.Width
            lblCargando.Height = 75
            lblCargando.TextAlign = ContentAlignment.TopCenter
            lblCargando.Font = New Font(Principal.tipoLetraSpread, 40, FontStyle.Regular)
            lblCargando.Location = New Point(lblCargando.Location.X, (Me.Height / 2) + 140)
            pnlCargando.Controls.Add(lblCargando)
            pnlCargando.BringToFront()
            pnlCargando.Focus()
        ElseIf (Not crear) Then ' Si ya existe, se checa si se muestra o no.
            If (mostrar) Then ' Se muestra.
                pnlCargando.Visible = True
                pnlCargando.BringToFront()
            Else ' No se muestra.
                pnlCargando.Visible = False
                pnlCargando.SendToBack()
            End If
        End If
        Application.DoEvents()

    End Sub

    Private Sub MostrarOcultar()

        Dim anchoMenor As Integer = btnMostrarOcultar.Width
        Dim espacio As Integer = 1
        If (Not Me.esIzquierda) Then
            pnlCapturaSuperior.Left = -pnlCapturaSuperior.Width + anchoMenor
            spDevoluciones.Left = anchoMenor + espacio
            spDevoluciones.Width = Me.anchoTotal - anchoMenor - espacio
            Me.esIzquierda = True
        Else
            pnlCapturaSuperior.Left = 0
            spDevoluciones.Left = pnlCapturaSuperior.Width + espacio
            spDevoluciones.Width = Me.anchoTotal - pnlCapturaSuperior.Width - espacio
            Me.esIzquierda = False
        End If

    End Sub

    Public Sub IniciarHilosCarga()

        CheckForIllegalCrossThreadCalls = False
        hiloNombrePrograma.Start()
        hiloCentrar.Start()
        hiloEncabezadosTitulos.Start()

    End Sub

    Private Sub Salir()

        Application.Exit()

    End Sub

    Private Sub MostrarAyuda()

        Dim pnlAyuda As New Panel()
        Dim txtAyuda As New TextBox()
        If (pnlContenido.Controls.Find("pnlAyuda", True).Count = 0) Then
            pnlAyuda.Name = "pnlAyuda"
            pnlAyuda.Visible = False
            pnlContenido.Controls.Add(pnlAyuda)
            txtAyuda.Name = "txtAyuda"
            pnlAyuda.Controls.Add(txtAyuda)
        Else
            pnlAyuda = pnlContenido.Controls.Find("pnlAyuda", False)(0)
            txtAyuda = pnlAyuda.Controls.Find("txtAyuda", False)(0)
        End If
        If (Not pnlAyuda.Visible) Then
            pnlCuerpo.Visible = False
            pnlAyuda.Visible = True
            pnlAyuda.Size = pnlCuerpo.Size
            pnlAyuda.Location = pnlCuerpo.Location
            pnlContenido.Controls.Add(pnlAyuda)
            txtAyuda.ScrollBars = ScrollBars.Both
            txtAyuda.Multiline = True
            txtAyuda.Width = pnlAyuda.Width - 10
            txtAyuda.Height = pnlAyuda.Height - 10
            txtAyuda.Location = New Point(5, 5)
            txtAyuda.Text = "Sección de Ayuda: " & vbNewLine & vbNewLine & "* Teclas básicas: " & vbNewLine & "F5 sirve para mostrar catálogos. " & vbNewLine & "F6 sirve para eliminar un registro únicamente. " & vbNewLine & "F7 sirve para mostrar listados." & vbNewLine & "Escape sirve para ocultar catálogos o listados que se encuentren desplegados. " & vbNewLine & vbNewLine & "* Catálogos o listados desplegados: " & vbNewLine & "Cuando se muestra algún catálogo o listado, al seleccionar alguna opción de este, se va mostrando en tiempo real en la captura de donde se originó. Cuando se le da doble clic en alguna opción o a la tecla escape se oculta dicho catálogo o listado. " & vbNewLine & vbNewLine & "* Datos obligatorios:" & vbNewLine & "Todos los que tengan el simbolo * son estrictamente obligatorios." & vbNewLine & vbNewLine & "* Captura:" & vbNewLine & "* Parte superior/izquierda: " & vbNewLine & "En esta parte se capturarán todos los datos que son generales, tal cual como el número de devolución, fecha, cliente, etc." & vbNewLine & "* Parte inferior/derecha: " & vbNewLine & "En esta parte se capturarán todos los artículos correspondientes a esa devolución y otros datos extra que llevará, por ejemplo, cantidad, precio, porcentaje de descuento, etc." & vbNewLine & vbNewLine & "* Opciones:" & vbNewLine & "Existe un botón para configurar impresoras y otro botón para imprimir/reimprimir directamente. " & vbNewLine & vbNewLine & "* Existen los botones de guardar/editar y eliminar todo dependiendo lo que se necesite hacer. "
            pnlAyuda.Controls.Add(txtAyuda)
        Else
            pnlCuerpo.Visible = True
            pnlAyuda.Visible = False
        End If
        Application.DoEvents()

    End Sub

    Private Sub Desvanecer()

        temporizador.Interval = 10
        temporizador.Enabled = True
        temporizador.Start()
        If (Me.Opacity > 0) Then
            Me.Opacity -= 0.25 : Application.DoEvents()
        Else
            temporizador.Enabled = False
            temporizador.Stop()
        End If

    End Sub

    Private Function ValidarAccesoTotal() As Boolean

        If ((Not ALMLogicaDevoluciones.Usuarios.accesoTotal) Or (ALMLogicaDevoluciones.Usuarios.accesoTotal = 0) Or (ALMLogicaDevoluciones.Usuarios.accesoTotal = False)) Then
            MsgBox("No tienes permisos suficientes para acceder a este programa.", MsgBoxStyle.Information, "Permisos insuficientes.")
            Return False
        Else
            Return True
        End If

    End Function

    Private Sub Centrar()

        Me.CenterToScreen()
        Me.Opacity = 0.98
        Me.Location = Screen.PrimaryScreen.WorkingArea.Location
        Me.Size = Screen.PrimaryScreen.WorkingArea.Size
        hiloCentrar.Abort()

    End Sub

    Private Sub CargarNombrePrograma()

        Me.nombreEstePrograma = Me.Text
        hiloNombrePrograma.Abort()

    End Sub

    Private Sub AsignarTooltips()

        Dim tp As New ToolTip()
        tp.AutoPopDelay = 5000
        tp.InitialDelay = 0
        tp.ReshowDelay = 100
        tp.ShowAlways = True
        tp.SetToolTip(Me.pnlEncabezado, "Datos Principales")
        tp.SetToolTip(Me.btnAyuda, "Ayuda")
        tp.SetToolTip(Me.btnSalir, "Salir")
        tp.SetToolTip(Me.btnGuardar, "Guardar")
        tp.SetToolTip(Me.btnEliminar, "Eliminar")
        tp.SetToolTip(Me.btnConfigurarImpresion, "Configurar Impresoras")
        tp.SetToolTip(Me.btnImprimir, "Imprimir")
        tp.SetToolTip(Me.pbMarca, "Producido por Berry")

    End Sub

    Private Sub AsignarTooltips(ByVal texto As String)

        lblDescripcionTooltip.Text = texto

    End Sub

    Private Sub ConfigurarConexiones()

        If (Me.esDesarrollo) Then
            ALMLogicaDevoluciones.Directorios.id = 2
            ALMLogicaDevoluciones.Directorios.instanciaSql = "BERRY1-DELL\SQLEXPRESS2008"
            ALMLogicaDevoluciones.Directorios.usuarioSql = "AdminBerry"
            ALMLogicaDevoluciones.Directorios.contrasenaSql = "@berry2017"
            pnlEncabezado.BackColor = Color.DarkRed
        Else
            ALMLogicaDevoluciones.Directorios.ObtenerParametros()
            ALMLogicaDevoluciones.Usuarios.ObtenerParametros()
        End If
        ALMLogicaDevoluciones.Programas.bdCatalogo = "Catalogo" & ALMLogicaDevoluciones.Directorios.id
        ALMLogicaDevoluciones.Programas.bdConfiguracion = "Configuracion" & ALMLogicaDevoluciones.Directorios.id
        ALMLogicaDevoluciones.Programas.bdAlmacen = "Almacen" & ALMLogicaDevoluciones.Directorios.id
        ALMEntidadesDevoluciones.BaseDatos.ECadenaConexionCatalogo = ALMLogicaDevoluciones.Programas.bdCatalogo
        ALMEntidadesDevoluciones.BaseDatos.ECadenaConexionConfiguracion = ALMLogicaDevoluciones.Programas.bdConfiguracion
        ALMEntidadesDevoluciones.BaseDatos.ECadenaConexionAlmacen = ALMLogicaDevoluciones.Programas.bdAlmacen
        ALMEntidadesDevoluciones.BaseDatos.AbrirConexionCatalogo()
        ALMEntidadesDevoluciones.BaseDatos.AbrirConexionConfiguracion()
        ALMEntidadesDevoluciones.BaseDatos.AbrirConexionAlmacen()
        ConsultarInformacionUsuario()
        CargarPrefijoBaseDatosAlmacen()

    End Sub

    Private Sub CargarPrefijoBaseDatosAlmacen()

        ALMLogicaDevoluciones.Programas.prefijoBaseDatosAlmacen = Me.prefijoBaseDatosAlmacen

    End Sub

    Private Sub ConsultarInformacionUsuario()

        Dim lista As New List(Of ALMEntidadesDevoluciones.Usuarios)
        usuarios.EId = ALMLogicaDevoluciones.Usuarios.id
        lista = usuarios.ObtenerListado()
        If (lista.Count > 0) Then
            ALMLogicaDevoluciones.Usuarios.id = lista(0).EId
            ALMLogicaDevoluciones.Usuarios.nombre = lista(0).ENombre
            ALMLogicaDevoluciones.Usuarios.contrasena = lista(0).EContrasena
            ALMLogicaDevoluciones.Usuarios.nivel = lista(0).ENivel
            ALMLogicaDevoluciones.Usuarios.accesoTotal = lista(0).EAccesoTotal
        End If

    End Sub

    Private Sub CargarEncabezadosTitulos()

        lblEncabezadoPrograma.Text = "Programa: " & Me.Text
        lblEncabezadoEmpresa.Text = "Directorio: " & ALMLogicaDevoluciones.Directorios.nombre
        lblEncabezadoUsuario.Text = "Usuario: " & ALMLogicaDevoluciones.Usuarios.nombre
        Me.Text = "Programa:  " & Me.nombreEstePrograma & "              Directorio:  " & ALMLogicaDevoluciones.Directorios.nombre & "              Usuario:  " & ALMLogicaDevoluciones.Usuarios.nombre
        hiloEncabezadosTitulos.Abort()

    End Sub

    Private Sub AbrirPrograma(nombre As String, salir As Boolean)

        If (Me.esDesarrollo) Then
            Exit Sub
        End If
        ejecutarProgramaPrincipal.UseShellExecute = True
        ejecutarProgramaPrincipal.FileName = nombre & Convert.ToString(".exe")
        ejecutarProgramaPrincipal.WorkingDirectory = Application.StartupPath
        ejecutarProgramaPrincipal.Arguments = ALMLogicaDevoluciones.Directorios.id.ToString().Trim().Replace(" ", "|") & " " & ALMLogicaDevoluciones.Directorios.nombre.ToString().Trim().Replace(" ", "|") & " " & ALMLogicaDevoluciones.Directorios.descripcion.ToString().Trim().Replace(" ", "|") & " " & ALMLogicaDevoluciones.Directorios.rutaLogo.ToString().Trim().Replace(" ", "|") & " " & ALMLogicaDevoluciones.Directorios.esPredeterminado.ToString().Trim().Replace(" ", "|") & " " & ALMLogicaDevoluciones.Directorios.instanciaSql.ToString().Trim().Replace(" ", "|") & " " & ALMLogicaDevoluciones.Directorios.usuarioSql.ToString().Trim().Replace(" ", "|") & " " & ALMLogicaDevoluciones.Directorios.contrasenaSql.ToString().Trim().Replace(" ", "|") & " " & "Aquí terminan los de directorios, indice 9 ;)".Replace(" ", "|") & " " & ALMLogicaDevoluciones.Usuarios.id.ToString().Trim().Replace(" ", "|") & " " & "Aquí terminan los de usuario, indice 11 ;)".Replace(" ", "|")
        Try
            Dim proceso = Process.Start(ejecutarProgramaPrincipal)
            proceso.WaitForInputIdle()
            If (salir) Then
                If (Me.ShowIcon) Then
                    Me.ShowIcon = False
                End If
                Application.Exit()
            End If
        Catch ex As Exception
            MessageBox.Show((Convert.ToString("No se puede abrir el programa principal en la ruta : " & ejecutarProgramaPrincipal.WorkingDirectory & "\") & nombre) & Environment.NewLine & Environment.NewLine & ex.Message, "Error.", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub AsignarFoco(ByVal c As Control)

        c.Focus()

    End Sub

    Public Sub ControlarSpreadEnterASiguienteColumna(ByVal spread As FarPoint.Win.Spread.FpSpread)

        Dim valor1 As FarPoint.Win.Spread.InputMap
        Dim valor2 As FarPoint.Win.Spread.InputMap
        valor1 = spread.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenAncestorOfFocused)
        valor1.Put(New FarPoint.Win.Spread.Keystroke(Keys.Enter, Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextColumnWrap)
        valor1 = spread.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenFocused)
        valor1.Put(New FarPoint.Win.Spread.Keystroke(Keys.Enter, Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextColumnWrap)
        valor2 = spread.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenFocused)
        valor2.Put(New FarPoint.Win.Spread.Keystroke(Keys.Escape, Keys.None), FarPoint.Win.Spread.SpreadActions.None)
        valor2 = spread.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenAncestorOfFocused)
        valor2.Put(New FarPoint.Win.Spread.Keystroke(Keys.Escape, Keys.None), FarPoint.Win.Spread.SpreadActions.None)

    End Sub

    Private Sub CargarTiposDeDatos()

        tipoDoble.DecimalPlaces = 2
        tipoDoble.DecimalSeparator = "."
        tipoDoble.Separator = ","
        tipoDoble.ShowSeparator = True
        tipoEntero.DecimalPlaces = 0
        tipoEntero.Separator = ","
        tipoEntero.ShowSeparator = True
        tipoTextoContrasena.PasswordChar = "*"
        tipoMoneda.MinimumValue = 0
        tipoMoneda.DecimalPlaces = 8
        tipoMoneda.Separator = ","
        tipoMoneda.DecimalSeparator = "."
        tipoMoneda.ShowCurrencySymbol = True
        tipoMoneda.ShowSeparator = True

    End Sub

    Private Sub CargarMedidas()

        Me.izquierda = 0
        Me.arriba = spDevoluciones.Top
        Me.anchoTotal = pnlCuerpo.Width
        Me.altoTotal = pnlCuerpo.Height
        Me.anchoMitad = Me.anchoTotal / 2
        Me.altoMitad = Me.altoTotal / 2
        Me.anchoTercio = Me.anchoTotal / 3
        Me.altoTercio = Me.altoTotal / 3
        Me.altoCuarto = Me.altoTotal / 4

    End Sub

#End Region

#Region "Todos los demás"

    Private Sub LimpiarPantalla()

        For Each c As Control In pnlCapturaSuperior.Controls
            If ((Not TypeOf c Is Button) AndAlso (Not TypeOf c Is Label) AndAlso (c.Name <> txtIdVale.Name)) Then
                c.BackColor = Principal.colorCaptura
            End If
        Next
        For fila = 0 To spDevoluciones.ActiveSheet.Rows.Count - 1
            For columna = 0 To spDevoluciones.ActiveSheet.Columns.Count - 1
                spDevoluciones.ActiveSheet.Cells(fila, columna).BackColor = Principal.colorCaptura
            Next
        Next
        If (Not chkConservarDatos.Checked) Then
            dtpFecha.Value = Today
            cbClientes.SelectedIndex = 0
        End If
        txtSubTotal.Text = String.Empty
        txtDescuento.Text = String.Empty
        txtTotal.Text = String.Empty
        spDevoluciones.ActiveSheet.DataSource = Nothing
        spDevoluciones.ActiveSheet.Rows.Count = 1
        spDevoluciones.ActiveSheet.SetActiveCell(0, 0)
        LimpiarSpread(spDevoluciones)

    End Sub

    Private Sub LimpiarSpread(ByVal spread As FarPoint.Win.Spread.FpSpread)

        spread.ActiveSheet.ClearRange(0, 0, spread.ActiveSheet.Rows.Count, spread.ActiveSheet.Columns.Count, True)

    End Sub

    Private Sub CargarComboClientes()

        cbClientes.DataSource = clientes.ObtenerListadoReporte()
        cbClientes.DisplayMember = "IdNombre"
        cbClientes.ValueMember = "Id"

    End Sub

    Private Sub FormatearSpread()

        ' Se cargan tipos de datos de spread.
        CargarTiposDeDatos()
        ' Se cargan las opciones generales.
        pnlCatalogos.Visible = False
        spDevoluciones.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Seashell
        spCatalogos.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Midnight
        spDevoluciones.ActiveSheet.GrayAreaBackColor = Principal.colorSpreadAreaGris
        spDevoluciones.Font = New Font(Principal.tipoLetraSpread, Principal.tamañoLetraSpread, FontStyle.Regular)
        spCatalogos.Font = New Font(Principal.tipoLetraSpread, Principal.tamañoLetraSpread, FontStyle.Regular)
        spDevoluciones.ActiveSheet.ColumnHeader.Rows(0).Height = Principal.alturaFilasEncabezadosGrandesSpread
        spCatalogos.ActiveSheet.ColumnHeader.Rows(0).Height = Principal.alturaFilasEncabezadosGrandesSpread
        spDevoluciones.ActiveSheet.Rows(-1).Height = Principal.alturaFilasSpread
        spCatalogos.ActiveSheet.Rows(-1).Height = Principal.alturaFilasSpread
        spDevoluciones.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        spDevoluciones.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        spCatalogos.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never
        spCatalogos.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Always
        'spDevoluciones.EditModePermanent = True
        spDevoluciones.EditModeReplace = True

    End Sub

    Private Sub EliminarRegistroDeSpread(ByVal spread As FarPoint.Win.Spread.FpSpread)

        spread.ActiveSheet.Rows.Remove(spread.ActiveSheet.ActiveRowIndex, 1)
        spread.ActiveSheet.Rows.Count += 1

    End Sub

    Private Sub ControlarSpreadEnter(ByVal spread As FarPoint.Win.Spread.FpSpread)

        Dim columnaActiva As Integer = spread.ActiveSheet.ActiveColumnIndex
        If (columnaActiva = spread.ActiveSheet.Columns.Count - 1) Then
            spread.ActiveSheet.Rows.Count += 1
        End If
        If (spread.Name = spDevoluciones.Name) Then
            Dim fila As Integer = 0
            If (columnaActiva = spDevoluciones.ActiveSheet.Columns("idVenta").Index) Then
                fila = spDevoluciones.ActiveSheet.ActiveRowIndex
                Dim idVenta As Integer = ALMLogicaDevoluciones.Funciones.ValidarNumeroACero(spDevoluciones.ActiveSheet.Cells(fila, spDevoluciones.ActiveSheet.Columns("idVenta").Index).Value)
                Dim idCliente As Integer = ALMLogicaDevoluciones.Funciones.ValidarNumeroACero(cbClientes.SelectedValue)
                Dim idAlmacen As Integer = ALMLogicaDevoluciones.Funciones.ValidarNumeroACero(spDevoluciones.ActiveSheet.Cells(fila, spDevoluciones.ActiveSheet.Columns("idAlmacen").Index).Value)
                Dim idFamilia As Integer = ALMLogicaDevoluciones.Funciones.ValidarNumeroACero(spDevoluciones.ActiveSheet.Cells(fila, spDevoluciones.ActiveSheet.Columns("idFamilia").Index).Value)
                Dim idSubFamilia As Integer = ALMLogicaDevoluciones.Funciones.ValidarNumeroACero(spDevoluciones.ActiveSheet.Cells(fila, spDevoluciones.ActiveSheet.Columns("idSubFamilia").Index).Value)
                Dim idArticulo As Integer = ALMLogicaDevoluciones.Funciones.ValidarNumeroACero(spDevoluciones.ActiveSheet.Cells(fila, spDevoluciones.ActiveSheet.Columns("idArticulo").Index).Value)
                If (idVenta > 0 AndAlso idCliente > 0 AndAlso idAlmacen > 0 AndAlso idFamilia > 0 AndAlso idSubFamilia > 0 AndAlso idArticulo > 0) Then
                    ventas.EId = idVenta
                    ventas.EIdCliente = idCliente
                    ventas.EIdAlmacen = idAlmacen
                    ventas.EIdFamilia = idFamilia
                    ventas.EIdSubFamilia = idSubFamilia
                    ventas.EIdArticulo = idArticulo
                    Dim datos As New DataTable
                    datos = ventas.ObtenerListadoGeneral()
                    If (datos.Rows.Count > 0) Then
                        spDevoluciones.ActiveSheet.Cells(fila, spDevoluciones.ActiveSheet.Columns("porcentajeDescuento").Index).Value = ALMLogicaDevoluciones.Funciones.ValidarNumeroACero(datos.Rows(0).Item("PorcentajeDescuento"))
                        spDevoluciones.ActiveSheet.Cells(fila, spDevoluciones.ActiveSheet.Columns("descuento").Index).Value = ALMLogicaDevoluciones.Funciones.ValidarNumeroACero(datos.Rows(0).Item("Descuento"))
                    Else
                        spDevoluciones.ActiveSheet.Cells(fila, spDevoluciones.ActiveSheet.Columns("idVenta").Index).Value = 0
                        spDevoluciones.ActiveSheet.Cells(fila, spDevoluciones.ActiveSheet.Columns("descuento").Index).Value = 0
                        spDevoluciones.ActiveSheet.ClearSelection()
                    End If
                Else
                    spDevoluciones.ActiveSheet.Cells(fila, spDevoluciones.ActiveSheet.Columns("idVenta").Index).Value = 0
                    spDevoluciones.ActiveSheet.Cells(fila, spDevoluciones.ActiveSheet.Columns("descuento").Index).Value = 0
                    spDevoluciones.ActiveSheet.ClearSelection()
                End If
            ElseIf (columnaActiva = spDevoluciones.ActiveSheet.Columns("idFamilia").Index) Then
                fila = spDevoluciones.ActiveSheet.ActiveRowIndex
                Dim idAlmacen As Integer = ALMLogicaDevoluciones.Funciones.ValidarNumeroACero(spDevoluciones.ActiveSheet.Cells(fila, spDevoluciones.ActiveSheet.Columns("idAlmacen").Index).Value)
                Dim idFamilia As Integer = ALMLogicaDevoluciones.Funciones.ValidarNumeroACero(spDevoluciones.ActiveSheet.Cells(fila, spDevoluciones.ActiveSheet.Columns("idFamilia").Index).Value)
                familias.EIdAlmacen = idAlmacen
                familias.EId = idFamilia
                If (idAlmacen > 0 And idFamilia > 0) Then
                    Dim datos As New DataTable
                    datos = familias.ObtenerListado()
                    If (datos.Rows.Count > 0) Then
                        spDevoluciones.ActiveSheet.Cells(fila, spDevoluciones.ActiveSheet.Columns("nombreFamilia").Index).Value = datos.Rows(0).Item("Nombre")
                        spDevoluciones.ActiveSheet.SetActiveCell(fila, spDevoluciones.ActiveSheet.ActiveColumnIndex + 1)
                    Else
                        spDevoluciones.ActiveSheet.Cells(fila, spDevoluciones.ActiveSheet.Columns("idFamilia").Index, fila, spDevoluciones.ActiveSheet.Columns("nombreFamilia").Index).Value = String.Empty
                        spDevoluciones.ActiveSheet.ClearSelection()
                        spDevoluciones.ActiveSheet.SetActiveCell(fila, spDevoluciones.ActiveSheet.ActiveColumnIndex - 1)
                    End If
                Else
                    spDevoluciones.ActiveSheet.Cells(fila, spDevoluciones.ActiveSheet.Columns("idFamilia").Index, fila, spDevoluciones.ActiveSheet.Columns("nombreFamilia").Index).Value = String.Empty
                    spDevoluciones.ActiveSheet.ClearSelection()
                    spDevoluciones.ActiveSheet.SetActiveCell(fila, spDevoluciones.ActiveSheet.ActiveColumnIndex - 1)
                End If
            ElseIf (columnaActiva = spDevoluciones.ActiveSheet.Columns("idSubFamilia").Index) Then
                fila = spDevoluciones.ActiveSheet.ActiveRowIndex
                Dim idAlmacen As Integer = ALMLogicaDevoluciones.Funciones.ValidarNumeroACero(spDevoluciones.ActiveSheet.Cells(fila, spDevoluciones.ActiveSheet.Columns("idAlmacen").Index).Value)
                Dim idFamilia As Integer = ALMLogicaDevoluciones.Funciones.ValidarNumeroACero(spDevoluciones.ActiveSheet.Cells(fila, spDevoluciones.ActiveSheet.Columns("idFamilia").Index).Value)
                Dim idSubFamilia As Integer = ALMLogicaDevoluciones.Funciones.ValidarNumeroACero(spDevoluciones.ActiveSheet.Cells(fila, spDevoluciones.ActiveSheet.Columns("idSubFamilia").Index).Value)
                subFamilias.EIdAlmacen = idAlmacen
                subFamilias.EIdFamilia = idFamilia
                subFamilias.EId = idSubFamilia
                If (idAlmacen > 0 And idFamilia > 0 And idSubFamilia > 0) Then
                    Dim datos As New DataTable
                    datos = subFamilias.ObtenerListado()
                    If (datos.Rows.Count > 0) Then
                        spDevoluciones.ActiveSheet.Cells(fila, spDevoluciones.ActiveSheet.Columns("nombreSubFamilia").Index).Value = datos.Rows(0).Item("Nombre")
                        spDevoluciones.ActiveSheet.SetActiveCell(fila, spDevoluciones.ActiveSheet.ActiveColumnIndex + 1)
                    Else
                        spDevoluciones.ActiveSheet.Cells(fila, spDevoluciones.ActiveSheet.Columns("idSubFamilia").Index, fila, spDevoluciones.ActiveSheet.Columns("nombreSubFamilia").Index).Value = String.Empty
                        spDevoluciones.ActiveSheet.ClearSelection()
                        spDevoluciones.ActiveSheet.SetActiveCell(fila, spDevoluciones.ActiveSheet.ActiveColumnIndex - 1)
                    End If
                Else
                    spDevoluciones.ActiveSheet.Cells(fila, spDevoluciones.ActiveSheet.Columns("idSubFamilia").Index, fila, spDevoluciones.ActiveSheet.Columns("nombreSubFamilia").Index).Value = String.Empty
                    spDevoluciones.ActiveSheet.ClearSelection()
                    spDevoluciones.ActiveSheet.SetActiveCell(fila, spDevoluciones.ActiveSheet.ActiveColumnIndex - 1)
                End If
            ElseIf (columnaActiva = spDevoluciones.ActiveSheet.Columns("idArticulo").Index) Then
                fila = spDevoluciones.ActiveSheet.ActiveRowIndex
                Dim idAlmacen As Integer = ALMLogicaDevoluciones.Funciones.ValidarNumeroACero(spDevoluciones.ActiveSheet.Cells(fila, spDevoluciones.ActiveSheet.Columns("idAlmacen").Index).Value)
                Dim idFamilia As Integer = ALMLogicaDevoluciones.Funciones.ValidarNumeroACero(spDevoluciones.ActiveSheet.Cells(fila, spDevoluciones.ActiveSheet.Columns("idFamilia").Index).Value)
                Dim idSubFamilia As Integer = ALMLogicaDevoluciones.Funciones.ValidarNumeroACero(spDevoluciones.ActiveSheet.Cells(fila, spDevoluciones.ActiveSheet.Columns("idSubFamilia").Index).Value)
                Dim idArticulo As Integer = ALMLogicaDevoluciones.Funciones.ValidarNumeroACero(spDevoluciones.ActiveSheet.Cells(fila, spDevoluciones.ActiveSheet.Columns("idArticulo").Index).Value)
                If (idAlmacen > 0 And idFamilia > 0 And idSubFamilia > 0 And idArticulo > 0) Then
                    articulos.EIdAlmacen = idAlmacen
                    articulos.EIdFamilia = idFamilia
                    articulos.EIdSubFamilia = idSubFamilia
                    articulos.EId = idArticulo
                    For indice = 0 To spDevoluciones.ActiveSheet.Rows.Count - 1 ' Se valida que no se repitan los articulos.
                        Dim idArticuloLocal As Integer = ALMLogicaDevoluciones.Funciones.ValidarNumeroACero(spDevoluciones.ActiveSheet.Cells(indice, spDevoluciones.ActiveSheet.Columns("idArticulo").Index).Text)
                        Dim idSubFamiliaLocal As Integer = ALMLogicaDevoluciones.Funciones.ValidarNumeroACero(spDevoluciones.ActiveSheet.Cells(indice, spDevoluciones.ActiveSheet.Columns("idSubFamilia").Index).Text)
                        Dim idFamiliaLocal As Integer = ALMLogicaDevoluciones.Funciones.ValidarNumeroACero(spDevoluciones.ActiveSheet.Cells(indice, spDevoluciones.ActiveSheet.Columns("idFamilia").Index).Text)
                        If (idArticuloLocal > 0 And idFamiliaLocal > 0 And idSubFamiliaLocal > 0) Then
                            If (idArticuloLocal = idArticulo And idSubFamiliaLocal = idSubFamilia And idFamiliaLocal = idFamilia And indice <> fila) Then
                                spDevoluciones.ActiveSheet.Cells(fila, spDevoluciones.ActiveSheet.Columns("idArticulo").Index).Text = String.Empty
                                spDevoluciones.ActiveSheet.ClearRange(fila, spDevoluciones.ActiveSheet.Columns("idArticulo").Index, 1, spDevoluciones.ActiveSheet.Columns.Count - 1, True)
                                spDevoluciones.ActiveSheet.ClearSelection()
                                spDevoluciones.ActiveSheet.SetActiveCell(fila, spDevoluciones.ActiveSheet.ActiveColumnIndex - 1)
                                Exit Sub
                            End If
                        End If
                    Next
                    Dim datos As New DataTable
                    datos = articulos.ObtenerListado()
                    If (datos.Rows.Count > 0) Then ' Se carga nombre de artículo.
                        spDevoluciones.ActiveSheet.Cells(fila, spDevoluciones.ActiveSheet.Columns("nombreArticulo").Index).Value = datos.Rows(0).Item("Nombre")
                        spDevoluciones.ActiveSheet.Cells(fila, spDevoluciones.ActiveSheet.Columns("codigo").Index).Value = datos.Rows(0).Item("Codigo")
                        spDevoluciones.ActiveSheet.Cells(fila, spDevoluciones.ActiveSheet.Columns("pagina").Index).Value = datos.Rows(0).Item("Pagina")
                        spDevoluciones.ActiveSheet.Cells(fila, spDevoluciones.ActiveSheet.Columns("color").Index).Value = datos.Rows(0).Item("Color")
                        spDevoluciones.ActiveSheet.Cells(fila, spDevoluciones.ActiveSheet.Columns("talla").Index).Value = datos.Rows(0).Item("Talla")
                        spDevoluciones.ActiveSheet.Cells(fila, spDevoluciones.ActiveSheet.Columns("modelo").Index).Value = datos.Rows(0).Item("Modelo")
                        spDevoluciones.ActiveSheet.Cells(fila, spDevoluciones.ActiveSheet.Columns("codigoInternet").Index).Value = datos.Rows(0).Item("CodigoInternet")
                        spDevoluciones.ActiveSheet.Cells(fila, spDevoluciones.ActiveSheet.Columns("precio").Index).Value = datos.Rows(0).Item("Precio")
                        Dim datos2 As New DataTable
                        unidadesMedidas.EId = datos.Rows(0).Item("IdUnidadMedida")
                        datos2 = unidadesMedidas.ObtenerListado()
                        If (datos2.Rows.Count > 0) Then ' Se carga nombre de unidad.
                            spDevoluciones.ActiveSheet.Cells(fila, spDevoluciones.ActiveSheet.Columns("nombreUnidadMedida").Index).Value = datos2.Rows(0).Item("Nombre")
                        End If
                        spDevoluciones.ActiveSheet.ClearSelection()
                        spDevoluciones.ActiveSheet.SetActiveCell(fila, spDevoluciones.ActiveSheet.ActiveColumnIndex + 8)
                    Else
                        spDevoluciones.ActiveSheet.Cells(fila, spDevoluciones.ActiveSheet.Columns("idArticulo").Index, fila, spDevoluciones.ActiveSheet.Columns("nombreUnidadMedida").Index).Value = String.Empty
                        spDevoluciones.ActiveSheet.ClearSelection()
                        spDevoluciones.ActiveSheet.SetActiveCell(fila, spDevoluciones.ActiveSheet.ActiveColumnIndex - 1)
                    End If
                Else
                    spDevoluciones.ActiveSheet.Cells(fila, spDevoluciones.ActiveSheet.Columns("idArticulo").Index, fila, spDevoluciones.ActiveSheet.Columns("nombreUnidadMedida").Index).Value = String.Empty
                    spDevoluciones.ActiveSheet.ClearSelection()
                    spDevoluciones.ActiveSheet.SetActiveCell(fila, spDevoluciones.ActiveSheet.ActiveColumnIndex - 1)
                End If
            ElseIf (columnaActiva = spDevoluciones.ActiveSheet.Columns("cantidad").Index) Then
                fila = spDevoluciones.ActiveSheet.ActiveRowIndex
                Dim cantidad As Integer = ALMLogicaDevoluciones.Funciones.ValidarNumeroACero(spDevoluciones.ActiveSheet.Cells(fila, spDevoluciones.ActiveSheet.Columns("cantidad").Index).Value)
                If (cantidad > 0) Then
                    Dim precio As Double = ALMLogicaDevoluciones.Funciones.ValidarNumeroACero(spDevoluciones.ActiveSheet.Cells(fila, spDevoluciones.ActiveSheet.Columns("precio").Index).Text)
                    spDevoluciones.ActiveSheet.Cells(fila, spDevoluciones.ActiveSheet.Columns("subtotal").Index).Value = cantidad * precio
                    Dim descuento As Double = ALMLogicaDevoluciones.Funciones.ValidarNumeroACero(spDevoluciones.ActiveSheet.Cells(fila, spDevoluciones.ActiveSheet.Columns("descuento").Index).Text)
                    spDevoluciones.ActiveSheet.Cells(fila, spDevoluciones.ActiveSheet.Columns("total").Index).Value = (cantidad * precio) - descuento
                Else
                    spDevoluciones.ActiveSheet.Cells(fila, spDevoluciones.ActiveSheet.Columns("cantidad").Index).Value = 0
                    spDevoluciones.ActiveSheet.ClearSelection()
                    spDevoluciones.ActiveSheet.SetActiveCell(fila, spDevoluciones.ActiveSheet.ActiveColumnIndex - 1)
                End If
            ElseIf (columnaActiva = spDevoluciones.ActiveSheet.Columns("precio").Index) Then
                fila = spDevoluciones.ActiveSheet.ActiveRowIndex
                Dim cantidad As Integer = ALMLogicaDevoluciones.Funciones.ValidarNumeroACero(spDevoluciones.ActiveSheet.Cells(fila, spDevoluciones.ActiveSheet.Columns("cantidad").Index).Value)
                Dim precio As Double = ALMLogicaDevoluciones.Funciones.ValidarNumeroACero(spDevoluciones.ActiveSheet.Cells(fila, spDevoluciones.ActiveSheet.Columns("precio").Index).Value)
                Dim descuento As Double = ALMLogicaDevoluciones.Funciones.ValidarNumeroACero(spDevoluciones.ActiveSheet.Cells(fila, spDevoluciones.ActiveSheet.Columns("descuento").Index).Value)
                If (cantidad > 0) Then
                    spDevoluciones.ActiveSheet.Cells(fila, spDevoluciones.ActiveSheet.Columns("subtotal").Index).Value = cantidad * precio
                    spDevoluciones.ActiveSheet.Cells(fila, spDevoluciones.ActiveSheet.Columns("total").Index).Value = (cantidad * precio) - descuento
                ElseIf (precio = 0) Then
                    spDevoluciones.ActiveSheet.Cells(fila, spDevoluciones.ActiveSheet.Columns("precio").Index).Value = 0
                Else
                    spDevoluciones.ActiveSheet.Cells(fila, spDevoluciones.ActiveSheet.Columns("precio").Index).Value = 0
                    spDevoluciones.ActiveSheet.ClearSelection()
                    spDevoluciones.ActiveSheet.SetActiveCell(fila, spDevoluciones.ActiveSheet.ActiveColumnIndex - 1)
                End If
            ElseIf (columnaActiva = spDevoluciones.ActiveSheet.Columns("subtotal").Index) Then
                fila = spDevoluciones.ActiveSheet.ActiveRowIndex
                Dim cantidad As Integer = ALMLogicaDevoluciones.Funciones.ValidarNumeroACero(spDevoluciones.ActiveSheet.Cells(fila, spDevoluciones.ActiveSheet.Columns("cantidad").Index).Value)
                Dim subtotal As Double = ALMLogicaDevoluciones.Funciones.ValidarNumeroACero(spDevoluciones.ActiveSheet.Cells(fila, spDevoluciones.ActiveSheet.Columns("subtotal").Index).Value)
                If (cantidad > 0) Then
                    spDevoluciones.ActiveSheet.Cells(fila, spDevoluciones.ActiveSheet.Columns("precio").Index).Value = subtotal / cantidad
                Else
                    spDevoluciones.ActiveSheet.Cells(fila, spDevoluciones.ActiveSheet.Columns("subtotal").Index).Value = 0
                    spDevoluciones.ActiveSheet.ClearSelection()
                    spDevoluciones.ActiveSheet.SetActiveCell(fila, spDevoluciones.ActiveSheet.ActiveColumnIndex - 1)
                End If
            ElseIf (columnaActiva = spDevoluciones.ActiveSheet.Columns("porcentajeDescuento").Index) Then
                fila = spDevoluciones.ActiveSheet.ActiveRowIndex
                Dim porcentajeDescuento As Double = ALMLogicaDevoluciones.Funciones.ValidarNumeroACero(spDevoluciones.ActiveSheet.Cells(fila, spDevoluciones.ActiveSheet.Columns("porcentajeDescuento").Index).Value)
                Dim subtotal As Double = ALMLogicaDevoluciones.Funciones.ValidarNumeroACero(spDevoluciones.ActiveSheet.Cells(fila, spDevoluciones.ActiveSheet.Columns("subtotal").Index).Value)
                If (porcentajeDescuento >= 0 And porcentajeDescuento <= 100) Then
                    If (porcentajeDescuento = 0) Then
                        spDevoluciones.ActiveSheet.Cells(fila, spDevoluciones.ActiveSheet.Columns("porcentajeDescuento").Index).Value = 0
                    End If
                    Dim descuento As Double = (subtotal / 100) * porcentajeDescuento
                    Dim total As Double = subtotal - descuento
                    If (total >= 0) Then
                        spDevoluciones.ActiveSheet.Cells(fila, spDevoluciones.ActiveSheet.Columns("descuento").Index).Value = descuento
                        spDevoluciones.ActiveSheet.Cells(fila, spDevoluciones.ActiveSheet.Columns("total").Index).Value = total
                    Else
                        spDevoluciones.ActiveSheet.Cells(fila, spDevoluciones.ActiveSheet.Columns("porcentajeDescuento").Index).Value = 0
                        spDevoluciones.ActiveSheet.SetActiveCell(fila, spDevoluciones.ActiveSheet.ActiveColumnIndex - 1)
                    End If
                Else
                    spDevoluciones.ActiveSheet.Cells(fila, spDevoluciones.ActiveSheet.Columns("porcentajeDescuento").Index).Value = 0
                End If
            ElseIf (columnaActiva = spDevoluciones.ActiveSheet.Columns("descuento").Index) Then
                fila = spDevoluciones.ActiveSheet.ActiveRowIndex
                Dim descuento As Double = ALMLogicaDevoluciones.Funciones.ValidarNumeroACero(spDevoluciones.ActiveSheet.Cells(fila, spDevoluciones.ActiveSheet.Columns("descuento").Index).Value)
                Dim subtotal As Double = ALMLogicaDevoluciones.Funciones.ValidarNumeroACero(spDevoluciones.ActiveSheet.Cells(fila, spDevoluciones.ActiveSheet.Columns("subtotal").Index).Value)
                If (descuento > 0) Then
                    Dim porcentajeDescuento As Double = (descuento / subtotal) * 100
                    Dim total As Double = subtotal - descuento
                    If (total >= 0) Then
                        spDevoluciones.ActiveSheet.Cells(fila, spDevoluciones.ActiveSheet.Columns("porcentajeDescuento").Index).Value = porcentajeDescuento
                        spDevoluciones.ActiveSheet.Cells(fila, spDevoluciones.ActiveSheet.Columns("total").Index).Value = total
                    Else
                        spDevoluciones.ActiveSheet.Cells(fila, spDevoluciones.ActiveSheet.Columns("descuento").Index).Value = 0
                        spDevoluciones.ActiveSheet.ClearSelection()
                        spDevoluciones.ActiveSheet.SetActiveCell(fila, spDevoluciones.ActiveSheet.ActiveColumnIndex - 1)
                    End If
                Else
                    spDevoluciones.ActiveSheet.Cells(fila, spDevoluciones.ActiveSheet.Columns("descuento").Index).Value = 0
                End If
            End If
            CalcularTotales()
        End If

    End Sub

    Private Sub CalcularTotales()

        Dim subTotal As Double = 0
        Dim descuento As Double = 0
        Dim total As Double = 0
        For fila = 0 To spDevoluciones.ActiveSheet.Rows.Count - 1
            subTotal += ALMLogicaDevoluciones.Funciones.ValidarNumeroACero(spDevoluciones.ActiveSheet.Cells(fila, spDevoluciones.ActiveSheet.Columns("subtotal").Index).Value)
            descuento += ALMLogicaDevoluciones.Funciones.ValidarNumeroACero(spDevoluciones.ActiveSheet.Cells(fila, spDevoluciones.ActiveSheet.Columns("descuento").Index).Value)
            total += ALMLogicaDevoluciones.Funciones.ValidarNumeroACero(spDevoluciones.ActiveSheet.Cells(fila, spDevoluciones.ActiveSheet.Columns("total").Index).Value)
        Next
        txtSubTotal.Text = subTotal.ToString("###,###.00")
        txtDescuento.Text = descuento.ToString("###,###.00")
        txtTotal.Text = total.ToString("###,###.00")

    End Sub

    Private Sub CargarIdConsecutivo()

        Dim idMaximo As Integer = devoluciones.ObtenerMaximoId()
        txtId.Text = idMaximo

    End Sub

    Private Sub CargarIdVale()

        txtIdVale.Text = Me.idOrigen.ToString().PadLeft(2, "0") & ALMLogicaDevoluciones.Funciones.ValidarNumeroACero(txtId.Text).ToString().PadLeft(6, "0")

    End Sub

    Private Sub CargarDatosEnSpreadDeCatalogos(ByVal filaCatalogos As Integer)

        If (spDevoluciones.ActiveSheet.ActiveColumnIndex = spDevoluciones.ActiveSheet.Columns("idAlmacen").Index Or spDevoluciones.ActiveSheet.ActiveColumnIndex = spDevoluciones.ActiveSheet.Columns("nombreAlmacen").Index) Then
            spDevoluciones.ActiveSheet.Cells(spDevoluciones.ActiveSheet.ActiveRowIndex, spDevoluciones.ActiveSheet.Columns("idAlmacen").Index).Text = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("id").Index).Text
            spDevoluciones.ActiveSheet.Cells(spDevoluciones.ActiveSheet.ActiveRowIndex, spDevoluciones.ActiveSheet.Columns("nombreAlmacen").Index).Text = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("nombre").Index).Text
        ElseIf (spDevoluciones.ActiveSheet.ActiveColumnIndex = spDevoluciones.ActiveSheet.Columns("idFamilia").Index Or spDevoluciones.ActiveSheet.ActiveColumnIndex = spDevoluciones.ActiveSheet.Columns("nombreFamilia").Index) Then
            spDevoluciones.ActiveSheet.Cells(spDevoluciones.ActiveSheet.ActiveRowIndex, spDevoluciones.ActiveSheet.Columns("idFamilia").Index).Text = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("id").Index).Text
            spDevoluciones.ActiveSheet.Cells(spDevoluciones.ActiveSheet.ActiveRowIndex, spDevoluciones.ActiveSheet.Columns("nombreFamilia").Index).Text = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("nombre").Index).Text
        ElseIf (spDevoluciones.ActiveSheet.ActiveColumnIndex = spDevoluciones.ActiveSheet.Columns("idSubFamilia").Index Or spDevoluciones.ActiveSheet.ActiveColumnIndex = spDevoluciones.ActiveSheet.Columns("nombreSubFamilia").Index) Then
            spDevoluciones.ActiveSheet.Cells(spDevoluciones.ActiveSheet.ActiveRowIndex, spDevoluciones.ActiveSheet.Columns("idSubFamilia").Index).Text = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("id").Index).Text
            spDevoluciones.ActiveSheet.Cells(spDevoluciones.ActiveSheet.ActiveRowIndex, spDevoluciones.ActiveSheet.Columns("nombreSubFamilia").Index).Text = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("nombre").Index).Text
        ElseIf (spDevoluciones.ActiveSheet.ActiveColumnIndex = spDevoluciones.ActiveSheet.Columns("idArticulo").Index Or spDevoluciones.ActiveSheet.ActiveColumnIndex = spDevoluciones.ActiveSheet.Columns("nombreArticulo").Index Or spDevoluciones.ActiveSheet.ActiveColumnIndex = spDevoluciones.ActiveSheet.Columns("nombreUnidadMedida").Index Or spDevoluciones.ActiveSheet.ActiveColumnIndex = spDevoluciones.ActiveSheet.Columns("codigo").Index Or spDevoluciones.ActiveSheet.ActiveColumnIndex = spDevoluciones.ActiveSheet.Columns("pagina").Index Or spDevoluciones.ActiveSheet.ActiveColumnIndex = spDevoluciones.ActiveSheet.Columns("color").Index Or spDevoluciones.ActiveSheet.ActiveColumnIndex = spDevoluciones.ActiveSheet.Columns("talla").Index Or spDevoluciones.ActiveSheet.ActiveColumnIndex = spDevoluciones.ActiveSheet.Columns("modelo").Index Or spDevoluciones.ActiveSheet.ActiveColumnIndex = spDevoluciones.ActiveSheet.Columns("codigoInternet").Index) Then
            spDevoluciones.ActiveSheet.Cells(spDevoluciones.ActiveSheet.ActiveRowIndex, spDevoluciones.ActiveSheet.Columns("idAlmacen").Index).Text = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("idAlmacen").Index).Text
            spDevoluciones.ActiveSheet.Cells(spDevoluciones.ActiveSheet.ActiveRowIndex, spDevoluciones.ActiveSheet.Columns("nombreAlmacen").Index).Text = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("nombreAlmacen").Index).Text
            spDevoluciones.ActiveSheet.Cells(spDevoluciones.ActiveSheet.ActiveRowIndex, spDevoluciones.ActiveSheet.Columns("idFamilia").Index).Text = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("idFamilia").Index).Text
            spDevoluciones.ActiveSheet.Cells(spDevoluciones.ActiveSheet.ActiveRowIndex, spDevoluciones.ActiveSheet.Columns("nombreFamilia").Index).Text = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("nombreFamilia").Index).Text
            spDevoluciones.ActiveSheet.Cells(spDevoluciones.ActiveSheet.ActiveRowIndex, spDevoluciones.ActiveSheet.Columns("idSubFamilia").Index).Text = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("idSubFamilia").Index).Text
            spDevoluciones.ActiveSheet.Cells(spDevoluciones.ActiveSheet.ActiveRowIndex, spDevoluciones.ActiveSheet.Columns("nombreSubFamilia").Index).Text = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("nombreSubFamilia").Index).Text
            spDevoluciones.ActiveSheet.Cells(spDevoluciones.ActiveSheet.ActiveRowIndex, spDevoluciones.ActiveSheet.Columns("idArticulo").Index).Text = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("id").Index).Text
            spDevoluciones.ActiveSheet.Cells(spDevoluciones.ActiveSheet.ActiveRowIndex, spDevoluciones.ActiveSheet.Columns("nombreArticulo").Index).Text = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("nombre").Index).Text
            spDevoluciones.ActiveSheet.Cells(spDevoluciones.ActiveSheet.ActiveRowIndex, spDevoluciones.ActiveSheet.Columns("nombreUnidadMedida").Index).Text = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("unidadMedida").Index).Text
            spDevoluciones.ActiveSheet.Cells(spDevoluciones.ActiveSheet.ActiveRowIndex, spDevoluciones.ActiveSheet.Columns("codigo").Index).Text = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("codigo").Index).Text
            spDevoluciones.ActiveSheet.Cells(spDevoluciones.ActiveSheet.ActiveRowIndex, spDevoluciones.ActiveSheet.Columns("pagina").Index).Text = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("pagina").Index).Text
            spDevoluciones.ActiveSheet.Cells(spDevoluciones.ActiveSheet.ActiveRowIndex, spDevoluciones.ActiveSheet.Columns("color").Index).Text = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("color").Index).Text
            spDevoluciones.ActiveSheet.Cells(spDevoluciones.ActiveSheet.ActiveRowIndex, spDevoluciones.ActiveSheet.Columns("talla").Index).Text = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("talla").Index).Text
            spDevoluciones.ActiveSheet.Cells(spDevoluciones.ActiveSheet.ActiveRowIndex, spDevoluciones.ActiveSheet.Columns("modelo").Index).Text = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("modelo").Index).Text
            spDevoluciones.ActiveSheet.Cells(spDevoluciones.ActiveSheet.ActiveRowIndex, spDevoluciones.ActiveSheet.Columns("codigoInternet").Index).Text = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("codigoInternet").Index).Text
            spDevoluciones.ActiveSheet.Cells(spDevoluciones.ActiveSheet.ActiveRowIndex, spDevoluciones.ActiveSheet.Columns("precio").Index).Text = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("precio").Index).Text
        End If

    End Sub

    Private Sub CargarDatosEnOtrosDeCatalogos(ByVal filaCatalogos As Integer)

        If (Me.opcionCatalogoSeleccionada = OpcionCatalogo.cliente) Then
            cbClientes.SelectedValue = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("id").Index).Text
        End If

    End Sub

    Private Sub CargarCatalogoEnSpread()

        spDevoluciones.Enabled = False
        Dim columna As Integer = spDevoluciones.ActiveSheet.ActiveColumnIndex
        If ((columna = spDevoluciones.ActiveSheet.Columns("idAlmacen").Index) Or (columna = spDevoluciones.ActiveSheet.Columns("nombreAlmacen").Index)) Then
            Me.opcionCatalogoSeleccionada = OpcionCatalogo.almacen
            almacenes.EId = 0
            Dim datos As New DataTable
            datos = almacenes.ObtenerListadoCatalogos()
            If (datos.Rows.Count > 0) Then
                spCatalogos.ActiveSheet.DataSource = datos
            Else
                spCatalogos.ActiveSheet.DataSource = Nothing
                spCatalogos.ActiveSheet.Rows.Count = 0
                spDevoluciones.Enabled = True
            End If
            FormatearSpreadCatalogos(OpcionPosicion.derecha)
        ElseIf ((columna = spDevoluciones.ActiveSheet.Columns("idFamilia").Index) Or (columna = spDevoluciones.ActiveSheet.Columns("nombreFamilia").Index)) Then
            Me.opcionCatalogoSeleccionada = OpcionCatalogo.familia
            Dim idAlmacen As Integer = ALMLogicaDevoluciones.Funciones.ValidarNumeroACero(spDevoluciones.ActiveSheet.Cells(spDevoluciones.ActiveSheet.ActiveRowIndex, spDevoluciones.ActiveSheet.Columns("idAlmacen").Index).Text)
            If (idAlmacen > 0) Then
                familias.EIdAlmacen = idAlmacen
                familias.EId = 0
                Dim datos As New DataTable
                datos = familias.ObtenerListadoReporte()
                If (datos.Rows.Count > 0) Then
                    spCatalogos.ActiveSheet.DataSource = datos
                Else
                    spCatalogos.ActiveSheet.DataSource = Nothing
                    spCatalogos.ActiveSheet.Rows.Count = 0
                    spDevoluciones.Enabled = True
                End If
            Else
                spCatalogos.ActiveSheet.DataSource = Nothing
                spCatalogos.ActiveSheet.Rows.Count = 0
                spDevoluciones.Enabled = True
            End If
            FormatearSpreadCatalogos(OpcionPosicion.derecha)
        ElseIf ((columna = spDevoluciones.ActiveSheet.Columns("idSubFamilia").Index) Or (columna = spDevoluciones.ActiveSheet.Columns("nombreSubFamilia").Index)) Then
            Me.opcionCatalogoSeleccionada = OpcionCatalogo.subfamilia
            Dim idAlmacen As Integer = ALMLogicaDevoluciones.Funciones.ValidarNumeroACero(spDevoluciones.ActiveSheet.Cells(spDevoluciones.ActiveSheet.ActiveRowIndex, spDevoluciones.ActiveSheet.Columns("idAlmacen").Index).Text)
            Dim idFamilia As Integer = ALMLogicaDevoluciones.Funciones.ValidarNumeroACero(spDevoluciones.ActiveSheet.Cells(spDevoluciones.ActiveSheet.ActiveRowIndex, spDevoluciones.ActiveSheet.Columns("idFamilia").Index).Text)
            If (idAlmacen > 0 And idFamilia > 0) Then
                subFamilias.EIdAlmacen = idAlmacen
                subFamilias.EIdFamilia = idFamilia
                subFamilias.EId = 0
                Dim datos As New DataTable
                datos = subFamilias.ObtenerListadoReporte()
                If (datos.Rows.Count > 0) Then
                    spCatalogos.ActiveSheet.DataSource = datos
                Else
                    spCatalogos.ActiveSheet.DataSource = Nothing
                    spCatalogos.ActiveSheet.Rows.Count = 0
                    spDevoluciones.Enabled = True
                End If
            Else
                spCatalogos.ActiveSheet.DataSource = Nothing
                spCatalogos.ActiveSheet.Rows.Count = 0
                spDevoluciones.Enabled = True
            End If
            FormatearSpreadCatalogos(OpcionPosicion.derecha)
        ElseIf ((columna = spDevoluciones.ActiveSheet.Columns("idArticulo").Index) Or (columna = spDevoluciones.ActiveSheet.Columns("nombreArticulo").Index) Or (columna = spDevoluciones.ActiveSheet.Columns("nombreUnidadMedida").Index) Or (columna = spDevoluciones.ActiveSheet.Columns("codigo").Index) Or (columna = spDevoluciones.ActiveSheet.Columns("pagina").Index) Or (columna = spDevoluciones.ActiveSheet.Columns("color").Index) Or (columna = spDevoluciones.ActiveSheet.Columns("talla").Index) Or (columna = spDevoluciones.ActiveSheet.Columns("modelo").Index) Or (columna = spDevoluciones.ActiveSheet.Columns("codigoInternet").Index)) Then
            Me.opcionCatalogoSeleccionada = OpcionCatalogo.articulo
            Dim idAlmacen As Integer = ALMLogicaDevoluciones.Funciones.ValidarNumeroACero(spDevoluciones.ActiveSheet.Cells(spDevoluciones.ActiveSheet.ActiveRowIndex, spDevoluciones.ActiveSheet.Columns("idAlmacen").Index).Text)
            Dim idFamilia As Integer = ALMLogicaDevoluciones.Funciones.ValidarNumeroACero(spDevoluciones.ActiveSheet.Cells(spDevoluciones.ActiveSheet.ActiveRowIndex, spDevoluciones.ActiveSheet.Columns("idFamilia").Index).Text)
            Dim idSubFamilia As Integer = ALMLogicaDevoluciones.Funciones.ValidarNumeroACero(spDevoluciones.ActiveSheet.Cells(spDevoluciones.ActiveSheet.ActiveRowIndex, spDevoluciones.ActiveSheet.Columns("idSubFamilia").Index).Text)
            articulos.EIdAlmacen = idAlmacen
            articulos.EIdFamilia = idFamilia
            articulos.EIdSubFamilia = idSubFamilia
            articulos.EId = 0
            Dim datos As New DataTable
            datos = articulos.ObtenerListadoCatalogos()
            Me.datosCatalogo = datos
            If (datos.Rows.Count > 0) Then
                spCatalogos.ActiveSheet.DataSource = datos
            Else
                spCatalogos.ActiveSheet.DataSource = Nothing
                spCatalogos.ActiveSheet.Rows.Count = 0
                spDevoluciones.Enabled = True
            End If
            FormatearSpreadCatalogos(OpcionPosicion.derecha)
        Else
            spDevoluciones.Enabled = True
        End If
        AsignarFoco(txtBuscarCatalogo)

    End Sub

    Private Sub CargarCatalogoEnOtros()

        pnlCapturaSuperior.Enabled = False
        If (Me.opcionCatalogoSeleccionada = OpcionCatalogo.almacen) Then
            almacenes.EId = 0
            Dim datos As New DataTable
            datos = almacenes.ObtenerListadoCatalogos()
            If (datos.Rows.Count > 0) Then
                spCatalogos.ActiveSheet.DataSource = datos
            Else
                spCatalogos.ActiveSheet.DataSource = Nothing
                spCatalogos.ActiveSheet.Rows.Count = 0
                pnlCapturaSuperior.Enabled = True
            End If
            FormatearSpreadCatalogos(OpcionPosicion.centro)
        ElseIf (Me.opcionCatalogoSeleccionada = OpcionCatalogo.cliente) Then
            clientes.EId = 0
            Dim datos As New DataTable
            datos = clientes.ObtenerListadoReporteCatalogo()
            If (datos.Rows.Count > 0) Then
                spCatalogos.ActiveSheet.DataSource = datos
            Else
                spCatalogos.ActiveSheet.DataSource = Nothing
                spCatalogos.ActiveSheet.Rows.Count = 0
                pnlCapturaSuperior.Enabled = True
            End If
            FormatearSpreadCatalogos(OpcionPosicion.centro)
        End If
        AsignarFoco(txtBuscarCatalogo)

    End Sub

    Private Sub FormatearSpreadCatalogos(ByVal posicion As Integer)

        If (Me.opcionCatalogoSeleccionada = OpcionCatalogo.articulo) Then
            spCatalogos.Width = 1190
            spCatalogos.ActiveSheet.Columns.Count = 18
        Else
            spCatalogos.Width = 520
            spCatalogos.ActiveSheet.Columns.Count = 2
        End If
        If (posicion = OpcionPosicion.izquierda) Then ' Izquierda.
            pnlCatalogos.Location = New Point(Me.izquierda, Me.arriba)
        ElseIf (posicion = OpcionPosicion.centro) Then ' Centrar.
            pnlCatalogos.Location = New Point(Me.anchoMitad - (spCatalogos.Width / 2), Me.arriba)
        ElseIf (posicion = OpcionPosicion.derecha) Then ' Derecha.
            pnlCatalogos.Location = New Point(Me.anchoTotal - (spCatalogos.Width), Me.arriba)
        End If
        spCatalogos.ActiveSheet.ColumnHeader.Rows(0).Font = New Font(Principal.tipoLetraSpread, Principal.tamañoLetraSpread, FontStyle.Bold)
        If (Me.opcionCatalogoSeleccionada = OpcionCatalogo.articulo) Then
            spCatalogos.ActiveSheet.ColumnHeader.Rows(0).Height = Principal.alturaFilasEncabezadosGrandesSpread
        Else
            spCatalogos.ActiveSheet.ColumnHeader.Rows(0).Height = Principal.alturaFilasEncabezadosMedianosSpread
        End If
        spCatalogos.ActiveSheet.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect
        Dim numeracion As Integer = 0
        If (Me.opcionCatalogoSeleccionada = OpcionCatalogo.articulo) Then
            spCatalogos.ActiveSheet.Columns(numeracion).Tag = "idAlmacen" : numeracion += 1
            spCatalogos.ActiveSheet.Columns(numeracion).Tag = "nombreAlmacen" : numeracion += 1
            spCatalogos.ActiveSheet.Columns(numeracion).Tag = "idFamilia" : numeracion += 1
            spCatalogos.ActiveSheet.Columns(numeracion).Tag = "nombreFamilia" : numeracion += 1
            spCatalogos.ActiveSheet.Columns(numeracion).Tag = "idSubFamilia" : numeracion += 1
            spCatalogos.ActiveSheet.Columns(numeracion).Tag = "nombreSubFamilia" : numeracion += 1
        End If
        spCatalogos.ActiveSheet.Columns(numeracion).Tag = "id" : numeracion += 1
        spCatalogos.ActiveSheet.Columns(numeracion).Tag = "nombre" : numeracion += 1
        If (Me.opcionCatalogoSeleccionada = OpcionCatalogo.articulo) Then
            spCatalogos.ActiveSheet.Columns(numeracion).Tag = "idProveedor" : numeracion += 1
            spCatalogos.ActiveSheet.Columns(numeracion).Tag = "nombreProveedor" : numeracion += 1
            spCatalogos.ActiveSheet.Columns(numeracion).Tag = "unidadMedida" : numeracion += 1
            spCatalogos.ActiveSheet.Columns(numeracion).Tag = "codigo" : numeracion += 1
            spCatalogos.ActiveSheet.Columns(numeracion).Tag = "pagina" : numeracion += 1
            spCatalogos.ActiveSheet.Columns(numeracion).Tag = "color" : numeracion += 1
            spCatalogos.ActiveSheet.Columns(numeracion).Tag = "talla" : numeracion += 1
            spCatalogos.ActiveSheet.Columns(numeracion).Tag = "modelo" : numeracion += 1
            spCatalogos.ActiveSheet.Columns(numeracion).Tag = "codigoInternet" : numeracion += 1
            spCatalogos.ActiveSheet.Columns(numeracion).Tag = "precio" : numeracion += 1
        End If
        spCatalogos.ActiveSheet.Columns("id").Width = 70
        spCatalogos.ActiveSheet.Columns("nombre").Width = 380
        If (Me.opcionCatalogoSeleccionada = OpcionCatalogo.articulo) Then
            FormatearSpreadAnchoColumnasArticulos()
        End If
        spCatalogos.ActiveSheet.ColumnHeader.Cells(0, spCatalogos.ActiveSheet.Columns("id").Index).Value = "No.".ToUpper
        spCatalogos.ActiveSheet.ColumnHeader.Cells(0, spCatalogos.ActiveSheet.Columns("nombre").Index).Value = "Nombre".ToUpper
        If (Me.opcionCatalogoSeleccionada = OpcionCatalogo.articulo) Then
            spCatalogos.ActiveSheet.ColumnHeader.Cells(0, spCatalogos.ActiveSheet.Columns("idAlmacen").Index).Value = "No.".ToUpper
            spCatalogos.ActiveSheet.ColumnHeader.Cells(0, spCatalogos.ActiveSheet.Columns("nombreAlmacen").Index).Value = "Nombre Almacén".ToUpper
            spCatalogos.ActiveSheet.ColumnHeader.Cells(0, spCatalogos.ActiveSheet.Columns("idFamilia").Index).Value = "No.".ToUpper
            spCatalogos.ActiveSheet.ColumnHeader.Cells(0, spCatalogos.ActiveSheet.Columns("nombreFamilia").Index).Value = "Nombre Familia".ToUpper
            spCatalogos.ActiveSheet.ColumnHeader.Cells(0, spCatalogos.ActiveSheet.Columns("idSubFamilia").Index).Value = "No.".ToUpper
            spCatalogos.ActiveSheet.ColumnHeader.Cells(0, spCatalogos.ActiveSheet.Columns("nombreSubFamilia").Index).Value = "Nombre SubFamilia".ToUpper
            spCatalogos.ActiveSheet.ColumnHeader.Cells(0, spCatalogos.ActiveSheet.Columns("idProveedor").Index).Value = "No.".ToUpper
            spCatalogos.ActiveSheet.ColumnHeader.Cells(0, spCatalogos.ActiveSheet.Columns("nombreProveedor").Index).Value = "Proveedor".ToUpper
            spCatalogos.ActiveSheet.ColumnHeader.Cells(0, spCatalogos.ActiveSheet.Columns("unidadMedida").Index).Value = "Unidad".ToUpper
            spCatalogos.ActiveSheet.ColumnHeader.Cells(0, spCatalogos.ActiveSheet.Columns("codigo").Index).Value = "Código".ToUpper
            spCatalogos.ActiveSheet.ColumnHeader.Cells(0, spCatalogos.ActiveSheet.Columns("pagina").Index).Value = "Página".ToUpper
            spCatalogos.ActiveSheet.ColumnHeader.Cells(0, spCatalogos.ActiveSheet.Columns("color").Index).Value = "Color".ToUpper
            spCatalogos.ActiveSheet.ColumnHeader.Cells(0, spCatalogos.ActiveSheet.Columns("talla").Index).Value = "Talla".ToUpper
            spCatalogos.ActiveSheet.ColumnHeader.Cells(0, spCatalogos.ActiveSheet.Columns("modelo").Index).Value = "Modelo".ToUpper
            spCatalogos.ActiveSheet.ColumnHeader.Cells(0, spCatalogos.ActiveSheet.Columns("codigoInternet").Index).Value = "Codigo Internet".ToUpper
            spCatalogos.ActiveSheet.ColumnHeader.Cells(0, spCatalogos.ActiveSheet.Columns("precio").Index).Value = "Precio".ToUpper
            spCatalogos.ActiveSheet.Columns(spCatalogos.ActiveSheet.Columns("idAlmacen").Index, spCatalogos.ActiveSheet.Columns("nombreSubFamilia").Index).Visible = False
            spCatalogos.ActiveSheet.Columns("unidadMedida").Visible = False
            spCatalogos.ActiveSheet.Columns("idProveedor").Visible = False
        End If
        spCatalogos.ActiveSheet.Columns(0, spCatalogos.ActiveSheet.Columns.Count - 1).AllowAutoFilter = True
        If (Me.opcionCatalogoSeleccionada = OpcionCatalogo.articulo) Then
            spCatalogos.ActiveSheet.Columns("id").AllowAutoFilter = False
            spCatalogos.ActiveSheet.Columns("nombre").AllowAutoFilter = False
            spCatalogos.ActiveSheet.Columns("codigo").AllowAutoFilter = False
        End If
        spCatalogos.ActiveSheet.Columns(0, spCatalogos.ActiveSheet.Columns.Count - 1).AllowAutoSort = True
        pnlCatalogos.Height = spDevoluciones.Height
        pnlCatalogos.Width = spCatalogos.Width
        spCatalogos.Width = pnlCatalogos.Width
        spCatalogos.Height = pnlCatalogos.Height - txtBuscarCatalogo.Height - 5
        pnlCatalogos.BringToFront()
        pnlCatalogos.Visible = True
        pnlCatalogos.Refresh()

    End Sub

    Private Sub FormatearSpreadAnchoColumnasArticulos()

        spCatalogos.ActiveSheet.Columns("idFamilia").Width = 70
        spCatalogos.ActiveSheet.Columns("nombreFamilia").Width = 200
        spCatalogos.ActiveSheet.Columns("idSubFamilia").Width = 70
        spCatalogos.ActiveSheet.Columns("nombreSubFamilia").Width = 200
        spCatalogos.ActiveSheet.Columns("id").Width = 70
        spCatalogos.ActiveSheet.Columns("nombre").Width = 150
        spCatalogos.ActiveSheet.Columns("idProveedor").Width = 70
        spCatalogos.ActiveSheet.Columns("nombreProveedor").Width = 125
        spCatalogos.ActiveSheet.Columns("unidadMedida").Width = 80
        spCatalogos.ActiveSheet.Columns("codigo").Width = 140
        spCatalogos.ActiveSheet.Columns("pagina").Width = 100
        spCatalogos.ActiveSheet.Columns("color").Width = 100
        spCatalogos.ActiveSheet.Columns("talla").Width = 90
        spCatalogos.ActiveSheet.Columns("modelo").Width = 110
        spCatalogos.ActiveSheet.Columns("codigoInternet").Width = 120
        spCatalogos.ActiveSheet.Columns("precio").Width = 100

    End Sub

    Private Sub VolverFocoDeCatalogos()

        pnlCapturaSuperior.Enabled = True
        spDevoluciones.Enabled = True
        If (Me.opcionCatalogoSeleccionada = OpcionCatalogo.cliente) Then
            AsignarFoco(cbClientes)
        Else
            AsignarFoco(spDevoluciones)
        End If
        txtBuscarCatalogo.Clear()
        pnlCatalogos.Visible = False

    End Sub

    Private Sub CargarDevoluciones()

        Me.Cursor = Cursors.WaitCursor
        CargarIdVale()
        devoluciones.EId = ALMLogicaDevoluciones.Funciones.ValidarNumeroACero(txtId.Text)
        If (devoluciones.EId > 0) Then
            Dim datos As New DataTable
            datos = devoluciones.ObtenerListadoGeneral()
            If (datos.Rows.Count > 0) Then
                dtpFecha.Value = datos.Rows(0).Item("Fecha")
                cbClientes.SelectedValue = datos.Rows(0).Item("IdCliente")
                spDevoluciones.ActiveSheet.DataSource = devoluciones.ObtenerListadoDetallado()
                Me.cantidadFilas = spDevoluciones.ActiveSheet.Rows.Count + 1
                FormatearSpreadDevoluciones()
                CalcularTotales()
                btnImprimir.Enabled = True
            Else
                LimpiarPantalla()
                btnImprimir.Enabled = False
            End If
            chkMostrarDetallado.Checked = False
        End If
        AsignarFoco(dtpFecha)
        Me.Cursor = Cursors.Default

    End Sub

    Private Sub FormatearSpreadDevoluciones()

        spDevoluciones.ActiveSheet.ColumnHeader.RowCount = 2
        spDevoluciones.ActiveSheet.ColumnHeader.Rows(0, spDevoluciones.ActiveSheet.ColumnHeader.Rows.Count - 1).Font = New Font(Principal.tipoLetraSpread, Principal.tamañoLetraSpread, FontStyle.Bold)
        spDevoluciones.ActiveSheet.ColumnHeader.Rows(0).Height = Principal.alturaFilasEncabezadosChicosSpread
        spDevoluciones.ActiveSheet.ColumnHeader.Rows(1).Height = Principal.alturaFilasEncabezadosMedianosSpread
        spDevoluciones.ActiveSheet.OperationMode = FarPoint.Win.Spread.OperationMode.Normal
        spDevoluciones.ActiveSheet.Rows.Count = Me.cantidadFilas
        ControlarSpreadEnterASiguienteColumna(spDevoluciones)
        Dim numeracion As Integer = 0
        spDevoluciones.ActiveSheet.Columns(numeracion).Tag = "esCapturado" : numeracion += 1
        spDevoluciones.ActiveSheet.Columns(numeracion).Tag = "idAlmacen" : numeracion += 1
        spDevoluciones.ActiveSheet.Columns(numeracion).Tag = "nombreAlmacen" : numeracion += 1
        spDevoluciones.ActiveSheet.Columns(numeracion).Tag = "idFamilia" : numeracion += 1
        spDevoluciones.ActiveSheet.Columns(numeracion).Tag = "nombreFamilia" : numeracion += 1
        spDevoluciones.ActiveSheet.Columns(numeracion).Tag = "idSubFamilia" : numeracion += 1
        spDevoluciones.ActiveSheet.Columns(numeracion).Tag = "nombreSubFamilia" : numeracion += 1
        spDevoluciones.ActiveSheet.Columns(numeracion).Tag = "idArticulo" : numeracion += 1
        spDevoluciones.ActiveSheet.Columns(numeracion).Tag = "nombreArticulo" : numeracion += 1
        spDevoluciones.ActiveSheet.Columns(numeracion).Tag = "nombreUnidadMedida" : numeracion += 1
        spDevoluciones.ActiveSheet.Columns(numeracion).Tag = "codigo" : numeracion += 1
        spDevoluciones.ActiveSheet.Columns(numeracion).Tag = "pagina" : numeracion += 1
        spDevoluciones.ActiveSheet.Columns(numeracion).Tag = "color" : numeracion += 1
        spDevoluciones.ActiveSheet.Columns(numeracion).Tag = "talla" : numeracion += 1
        spDevoluciones.ActiveSheet.Columns(numeracion).Tag = "modelo" : numeracion += 1
        spDevoluciones.ActiveSheet.Columns(numeracion).Tag = "codigoInternet" : numeracion += 1
        spDevoluciones.ActiveSheet.Columns(numeracion).Tag = "idVenta" : numeracion += 1
        spDevoluciones.ActiveSheet.Columns(numeracion).Tag = "cantidad" : numeracion += 1
        spDevoluciones.ActiveSheet.Columns(numeracion).Tag = "precio" : numeracion += 1
        spDevoluciones.ActiveSheet.Columns(numeracion).Tag = "subtotal" : numeracion += 1
        spDevoluciones.ActiveSheet.Columns(numeracion).Tag = "porcentajeDescuento" : numeracion += 1
        spDevoluciones.ActiveSheet.Columns(numeracion).Tag = "descuento" : numeracion += 1
        spDevoluciones.ActiveSheet.Columns(numeracion).Tag = "total" : numeracion += 1
        spDevoluciones.ActiveSheet.Columns.Count = numeracion
        spDevoluciones.ActiveSheet.Columns("idAlmacen").Width = 50
        spDevoluciones.ActiveSheet.Columns("nombreAlmacen").Width = 130
        spDevoluciones.ActiveSheet.Columns("idFamilia").Width = 50
        spDevoluciones.ActiveSheet.Columns("nombreFamilia").Width = 130
        spDevoluciones.ActiveSheet.Columns("idSubFamilia").Width = 50
        spDevoluciones.ActiveSheet.Columns("nombreSubFamilia").Width = 130
        spDevoluciones.ActiveSheet.Columns("idArticulo").Width = 50
        spDevoluciones.ActiveSheet.Columns("nombreArticulo").Width = 100
        spDevoluciones.ActiveSheet.Columns("nombreUnidadMedida").Width = 70
        spDevoluciones.ActiveSheet.Columns("codigo").Width = 105
        spDevoluciones.ActiveSheet.Columns("pagina").Width = 70
        spDevoluciones.ActiveSheet.Columns("color").Width = 90
        spDevoluciones.ActiveSheet.Columns("talla").Width = 60
        spDevoluciones.ActiveSheet.Columns("modelo").Width = 70
        spDevoluciones.ActiveSheet.Columns("codigoInternet").Width = 80
        spDevoluciones.ActiveSheet.Columns("idVenta").Width = 50
        spDevoluciones.ActiveSheet.Columns("cantidad").Width = 85
        spDevoluciones.ActiveSheet.Columns("precio").Width = 70
        spDevoluciones.ActiveSheet.Columns("subtotal").Width = 90
        spDevoluciones.ActiveSheet.Columns("porcentajeDescuento").Width = 100
        spDevoluciones.ActiveSheet.Columns("descuento").Width = 100
        spDevoluciones.ActiveSheet.Columns("total").Width = 75
        spDevoluciones.ActiveSheet.Columns("idAlmacen").CellType = tipoEntero
        spDevoluciones.ActiveSheet.Columns("nombreAlmacen").CellType = tipoTexto
        spDevoluciones.ActiveSheet.Columns("idFamilia").CellType = tipoEntero
        spDevoluciones.ActiveSheet.Columns("nombreFamilia").CellType = tipoTexto
        spDevoluciones.ActiveSheet.Columns("idSubFamilia").CellType = tipoEntero
        spDevoluciones.ActiveSheet.Columns("nombreSubFamilia").CellType = tipoTexto
        spDevoluciones.ActiveSheet.Columns("idArticulo").CellType = tipoEntero
        spDevoluciones.ActiveSheet.Columns("nombreArticulo").CellType = tipoTexto
        spDevoluciones.ActiveSheet.Columns("nombreUnidadMedida").CellType = tipoTexto
        spDevoluciones.ActiveSheet.Columns("codigo").CellType = tipoTexto
        spDevoluciones.ActiveSheet.Columns("pagina").CellType = tipoEntero
        spDevoluciones.ActiveSheet.Columns("color").CellType = tipoTexto
        spDevoluciones.ActiveSheet.Columns("talla").CellType = tipoTexto
        spDevoluciones.ActiveSheet.Columns("modelo").CellType = tipoTexto
        spDevoluciones.ActiveSheet.Columns("codigoInternet").CellType = tipoTexto
        spDevoluciones.ActiveSheet.Columns("idVenta").CellType = tipoEntero
        spDevoluciones.ActiveSheet.Columns("cantidad").CellType = tipoEntero
        spDevoluciones.ActiveSheet.Columns("precio").CellType = tipoDoble
        spDevoluciones.ActiveSheet.Columns("subtotal").CellType = tipoDoble
        spDevoluciones.ActiveSheet.Columns("porcentajeDescuento").CellType = tipoDoble
        spDevoluciones.ActiveSheet.Columns("descuento").CellType = tipoDoble
        spDevoluciones.ActiveSheet.Columns("total").CellType = tipoDoble
        spDevoluciones.ActiveSheet.AddColumnHeaderSpanCell(0, spDevoluciones.ActiveSheet.Columns("idAlmacen").Index, 1, 2)
        spDevoluciones.ActiveSheet.ColumnHeader.Cells(0, spDevoluciones.ActiveSheet.Columns("idAlmacen").Index).Value = "A l m a c é n".ToUpper()
        spDevoluciones.ActiveSheet.ColumnHeader.Cells(1, spDevoluciones.ActiveSheet.Columns("idAlmacen").Index).Value = "No. *".ToUpper()
        spDevoluciones.ActiveSheet.ColumnHeader.Cells(1, spDevoluciones.ActiveSheet.Columns("nombreAlmacen").Index).Value = "Nombre *".ToUpper()
        spDevoluciones.ActiveSheet.AddColumnHeaderSpanCell(0, spDevoluciones.ActiveSheet.Columns("idFamilia").Index, 1, 2)
        spDevoluciones.ActiveSheet.ColumnHeader.Cells(0, spDevoluciones.ActiveSheet.Columns("idFamilia").Index).Value = "F a m i l i a".ToUpper()
        spDevoluciones.ActiveSheet.ColumnHeader.Cells(1, spDevoluciones.ActiveSheet.Columns("idFamilia").Index).Value = "No. *".ToUpper()
        spDevoluciones.ActiveSheet.ColumnHeader.Cells(1, spDevoluciones.ActiveSheet.Columns("nombreFamilia").Index).Value = "Nombre *".ToUpper()
        spDevoluciones.ActiveSheet.AddColumnHeaderSpanCell(0, spDevoluciones.ActiveSheet.Columns("idSubFamilia").Index, 1, 2)
        spDevoluciones.ActiveSheet.ColumnHeader.Cells(0, spDevoluciones.ActiveSheet.Columns("idSubFamilia").Index).Value = "S u b F a m i l i a".ToUpper()
        spDevoluciones.ActiveSheet.ColumnHeader.Cells(1, spDevoluciones.ActiveSheet.Columns("idSubFamilia").Index).Value = "No. *".ToUpper()
        spDevoluciones.ActiveSheet.ColumnHeader.Cells(1, spDevoluciones.ActiveSheet.Columns("nombreSubFamilia").Index).Value = "Nombre *".ToUpper()
        spDevoluciones.ActiveSheet.AddColumnHeaderSpanCell(0, spDevoluciones.ActiveSheet.Columns("idArticulo").Index, 1, 9)
        spDevoluciones.ActiveSheet.ColumnHeader.Cells(0, spDevoluciones.ActiveSheet.Columns("idArticulo").Index).Value = "A r t í c u l o".ToUpper()
        spDevoluciones.ActiveSheet.ColumnHeader.Cells(1, spDevoluciones.ActiveSheet.Columns("idArticulo").Index).Value = "No. *".ToUpper()
        spDevoluciones.ActiveSheet.ColumnHeader.Cells(1, spDevoluciones.ActiveSheet.Columns("nombreArticulo").Index).Value = "Nombre *".ToUpper()
        spDevoluciones.ActiveSheet.ColumnHeader.Cells(1, spDevoluciones.ActiveSheet.Columns("nombreUnidadMedida").Index).Value = "Unidad *".ToUpper()
        spDevoluciones.ActiveSheet.ColumnHeader.Cells(1, spDevoluciones.ActiveSheet.Columns("codigo").Index).Value = "Código *".ToUpper()
        spDevoluciones.ActiveSheet.ColumnHeader.Cells(1, spDevoluciones.ActiveSheet.Columns("pagina").Index).Value = "Página *".ToUpper()
        spDevoluciones.ActiveSheet.ColumnHeader.Cells(1, spDevoluciones.ActiveSheet.Columns("color").Index).Value = "Color *".ToUpper()
        spDevoluciones.ActiveSheet.ColumnHeader.Cells(1, spDevoluciones.ActiveSheet.Columns("talla").Index).Value = "Talla *".ToUpper()
        spDevoluciones.ActiveSheet.ColumnHeader.Cells(1, spDevoluciones.ActiveSheet.Columns("modelo").Index).Value = "Modelo *".ToUpper()
        spDevoluciones.ActiveSheet.ColumnHeader.Cells(1, spDevoluciones.ActiveSheet.Columns("codigoInternet").Index).Value = "Codigo Internet *".ToUpper()
        spDevoluciones.ActiveSheet.AddColumnHeaderSpanCell(0, spDevoluciones.ActiveSheet.Columns("idVenta").Index, 2, 1)
        spDevoluciones.ActiveSheet.ColumnHeader.Cells(0, spDevoluciones.ActiveSheet.Columns("idVenta").Index).Value = "No. Venta".ToUpper()
        spDevoluciones.ActiveSheet.AddColumnHeaderSpanCell(0, spDevoluciones.ActiveSheet.Columns("cantidad").Index, 2, 1)
        spDevoluciones.ActiveSheet.ColumnHeader.Cells(0, spDevoluciones.ActiveSheet.Columns("cantidad").Index).Value = "Cantidad *".ToUpper()
        spDevoluciones.ActiveSheet.AddColumnHeaderSpanCell(0, spDevoluciones.ActiveSheet.Columns("precio").Index, 2, 1)
        spDevoluciones.ActiveSheet.ColumnHeader.Cells(0, spDevoluciones.ActiveSheet.Columns("precio").Index).Value = "Precio *".ToUpper()
        spDevoluciones.ActiveSheet.AddColumnHeaderSpanCell(0, spDevoluciones.ActiveSheet.Columns("subtotal").Index, 2, 1)
        spDevoluciones.ActiveSheet.ColumnHeader.Cells(0, spDevoluciones.ActiveSheet.Columns("subtotal").Index).Value = "SubTotal *".ToUpper()
        spDevoluciones.ActiveSheet.AddColumnHeaderSpanCell(0, spDevoluciones.ActiveSheet.Columns("porcentajeDescuento").Index, 2, 1)
        spDevoluciones.ActiveSheet.ColumnHeader.Cells(0, spDevoluciones.ActiveSheet.Columns("porcentajeDescuento").Index).Value = "Porcentaje Descuento *".ToUpper()
        spDevoluciones.ActiveSheet.AddColumnHeaderSpanCell(0, spDevoluciones.ActiveSheet.Columns("descuento").Index, 2, 1)
        spDevoluciones.ActiveSheet.ColumnHeader.Cells(0, spDevoluciones.ActiveSheet.Columns("descuento").Index).Value = "Descuento *".ToUpper()
        spDevoluciones.ActiveSheet.AddColumnHeaderSpanCell(0, spDevoluciones.ActiveSheet.Columns("total").Index, 2, 1)
        spDevoluciones.ActiveSheet.ColumnHeader.Cells(0, spDevoluciones.ActiveSheet.Columns("total").Index).Value = "Total *".ToUpper()
        spDevoluciones.ActiveSheet.Columns(spDevoluciones.ActiveSheet.Columns("esCapturado").Index).Visible = False
        MostrarOcultarDetalle(False)
        spDevoluciones.Refresh()

    End Sub

    Private Sub MostrarOcultarDetalle(ByVal valor As Boolean)

        spDevoluciones.ActiveSheet.Columns(spDevoluciones.ActiveSheet.Columns("idAlmacen").Index, spDevoluciones.ActiveSheet.Columns("nombreSubFamilia").Index).Visible = valor
        'spDevoluciones.ActiveSheet.Columns(spDevoluciones.ActiveSheet.Columns("nombreArticulo").Index).Visible = valor
        spDevoluciones.ActiveSheet.Columns(spDevoluciones.ActiveSheet.Columns("nombreUnidadMedida").Index).Visible = valor
        spDevoluciones.ActiveSheet.Columns(spDevoluciones.ActiveSheet.Columns("descuento").Index).Visible = valor

    End Sub

    Private Sub ValidarGuardadoDevoluciones()

        Me.Cursor = Cursors.WaitCursor
        Me.esGuardadoValido = True
        ' Parte superior. 
        Dim id As Integer = ALMLogicaDevoluciones.Funciones.ValidarNumeroACero(txtId.Text)
        If (id <= 0) Then
            txtId.BackColor = Principal.colorAdvertencia
            Me.esGuardadoValido = False
        End If
        Dim idCliente As Integer = ALMLogicaDevoluciones.Funciones.ValidarNumeroACero(cbClientes.SelectedValue)
        If (idCliente <= 0) Then
            cbClientes.BackColor = Principal.colorAdvertencia
            Me.esGuardadoValido = False
        End If
        ' Parte inferior.
        For fila As Integer = 0 To spDevoluciones.ActiveSheet.Rows.Count - 1
            Dim idAlmacen As Integer = ALMLogicaDevoluciones.Funciones.ValidarNumeroACero(spDevoluciones.ActiveSheet.Cells(fila, spDevoluciones.ActiveSheet.Columns("idFamilia").Index).Text)
            Dim idFamilia As Integer = ALMLogicaDevoluciones.Funciones.ValidarNumeroACero(spDevoluciones.ActiveSheet.Cells(fila, spDevoluciones.ActiveSheet.Columns("idFamilia").Index).Text)
            Dim idSubFamilia As Integer = ALMLogicaDevoluciones.Funciones.ValidarNumeroACero(spDevoluciones.ActiveSheet.Cells(fila, spDevoluciones.ActiveSheet.Columns("idSubFamilia").Index).Text)
            Dim idArticulo As Integer = ALMLogicaDevoluciones.Funciones.ValidarNumeroACero(spDevoluciones.ActiveSheet.Cells(fila, spDevoluciones.ActiveSheet.Columns("idArticulo").Index).Text)
            If (idAlmacen > 0 AndAlso idFamilia > 0 AndAlso idSubFamilia > 0 AndAlso idArticulo > 0) Then
                Dim cantidad As Integer = ALMLogicaDevoluciones.Funciones.ValidarNumeroACero(spDevoluciones.ActiveSheet.Cells(fila, spDevoluciones.ActiveSheet.Columns("cantidad").Index).Text)
                If (cantidad <= 0) Then
                    spDevoluciones.ActiveSheet.Cells(fila, spDevoluciones.ActiveSheet.Columns("cantidad").Index).BackColor = Principal.colorAdvertencia
                    Me.esGuardadoValido = False
                End If
                Dim precio As String = spDevoluciones.ActiveSheet.Cells(fila, spDevoluciones.ActiveSheet.Columns("precio").Index).Text
                Dim precio2 As Double = ALMLogicaDevoluciones.Funciones.ValidarNumeroACero(spDevoluciones.ActiveSheet.Cells(fila, spDevoluciones.ActiveSheet.Columns("precio").Index).Text)
                If (String.IsNullOrEmpty(precio) Or precio2 < 0) Then
                    spDevoluciones.ActiveSheet.Cells(fila, spDevoluciones.ActiveSheet.Columns("precio").Index).BackColor = Principal.colorAdvertencia
                    Me.esGuardadoValido = False
                End If
                Dim subtotal As String = spDevoluciones.ActiveSheet.Cells(fila, spDevoluciones.ActiveSheet.Columns("subtotal").Index).Text
                Dim subtotal2 As Double = ALMLogicaDevoluciones.Funciones.ValidarNumeroACero(spDevoluciones.ActiveSheet.Cells(fila, spDevoluciones.ActiveSheet.Columns("subtotal").Index).Text)
                If (String.IsNullOrEmpty(subtotal) Or subtotal2 < 0) Then
                    spDevoluciones.ActiveSheet.Cells(fila, spDevoluciones.ActiveSheet.Columns("subtotal").Index).BackColor = Principal.colorAdvertencia
                    Me.esGuardadoValido = False
                End If
                Dim porcentajeDescuento As String = spDevoluciones.ActiveSheet.Cells(fila, spDevoluciones.ActiveSheet.Columns("porcentajeDescuento").Index).Text
                Dim porcentajeDescuento2 As Double = ALMLogicaDevoluciones.Funciones.ValidarNumeroACero(spDevoluciones.ActiveSheet.Cells(fila, spDevoluciones.ActiveSheet.Columns("porcentajeDescuento").Index).Text)
                If (String.IsNullOrEmpty(porcentajeDescuento) Or porcentajeDescuento2 < 0) Then
                    spDevoluciones.ActiveSheet.Cells(fila, spDevoluciones.ActiveSheet.Columns("porcentajeDescuento").Index).BackColor = Principal.colorAdvertencia
                    Me.esGuardadoValido = False
                End If
                Dim descuento As String = spDevoluciones.ActiveSheet.Cells(fila, spDevoluciones.ActiveSheet.Columns("descuento").Index).Text
                Dim descuento2 As Double = ALMLogicaDevoluciones.Funciones.ValidarNumeroACero(spDevoluciones.ActiveSheet.Cells(fila, spDevoluciones.ActiveSheet.Columns("descuento").Index).Text)
                If (String.IsNullOrEmpty(descuento) Or descuento2 < 0) Then
                    spDevoluciones.ActiveSheet.Cells(fila, spDevoluciones.ActiveSheet.Columns("descuento").Index).BackColor = Principal.colorAdvertencia
                    Me.esGuardadoValido = False
                End If
                Dim total As String = spDevoluciones.ActiveSheet.Cells(fila, spDevoluciones.ActiveSheet.Columns("total").Index).Text
                Dim total2 As Double = ALMLogicaDevoluciones.Funciones.ValidarNumeroACero(spDevoluciones.ActiveSheet.Cells(fila, spDevoluciones.ActiveSheet.Columns("total").Index).Text)
                If (String.IsNullOrEmpty(total) Or total2 < 0) Then
                    spDevoluciones.ActiveSheet.Cells(fila, spDevoluciones.ActiveSheet.Columns("total").Index).BackColor = Principal.colorAdvertencia
                    Me.esGuardadoValido = False
                End If
            End If
        Next
        Me.Cursor = Cursors.Default

    End Sub

    Private Sub GuardarEditarDevoluciones()

        Dim hiloImpresion As New Thread(AddressOf MandarImprimir)
        EliminarDevoluciones(False)
        ' Parte superior.
        Dim id As Integer = ALMLogicaDevoluciones.Funciones.ValidarNumeroACero(txtId.Text)
        Dim fecha As Date = dtpFecha.Value
        Dim idCliente As Integer = ALMLogicaDevoluciones.Funciones.ValidarNumeroACero(cbClientes.SelectedValue)
        ' Parte inferior.
        For fila As Integer = 0 To spDevoluciones.ActiveSheet.Rows.Count - 1
            Dim idAlmacen As Integer = ALMLogicaDevoluciones.Funciones.ValidarNumeroACero(spDevoluciones.ActiveSheet.Cells(fila, spDevoluciones.ActiveSheet.Columns("idAlmacen").Index).Text)
            Dim idFamilia As Integer = ALMLogicaDevoluciones.Funciones.ValidarNumeroACero(spDevoluciones.ActiveSheet.Cells(fila, spDevoluciones.ActiveSheet.Columns("idFamilia").Index).Text)
            Dim idSubFamilia As Integer = ALMLogicaDevoluciones.Funciones.ValidarNumeroACero(spDevoluciones.ActiveSheet.Cells(fila, spDevoluciones.ActiveSheet.Columns("idSubFamilia").Index).Text)
            Dim idArticulo As Integer = ALMLogicaDevoluciones.Funciones.ValidarNumeroACero(spDevoluciones.ActiveSheet.Cells(fila, spDevoluciones.ActiveSheet.Columns("idArticulo").Index).Text)
            Dim cantidad As Integer = ALMLogicaDevoluciones.Funciones.ValidarNumeroACero(spDevoluciones.ActiveSheet.Cells(fila, spDevoluciones.ActiveSheet.Columns("cantidad").Index).Text)
            Dim precio As Double = ALMLogicaDevoluciones.Funciones.ValidarNumeroACero(spDevoluciones.ActiveSheet.Cells(fila, spDevoluciones.ActiveSheet.Columns("precio").Index).Text)
            Dim subtotal As Double = ALMLogicaDevoluciones.Funciones.ValidarNumeroACero(spDevoluciones.ActiveSheet.Cells(fila, spDevoluciones.ActiveSheet.Columns("subtotal").Index).Text)
            Dim porcentajeDescuento As Double = ALMLogicaDevoluciones.Funciones.ValidarNumeroACero(spDevoluciones.ActiveSheet.Cells(fila, spDevoluciones.ActiveSheet.Columns("porcentajeDescuento").Index).Text)
            Dim descuento As Double = ALMLogicaDevoluciones.Funciones.ValidarNumeroACero(spDevoluciones.ActiveSheet.Cells(fila, spDevoluciones.ActiveSheet.Columns("descuento").Index).Text)
            Dim total As Double = ALMLogicaDevoluciones.Funciones.ValidarNumeroACero(spDevoluciones.ActiveSheet.Cells(fila, spDevoluciones.ActiveSheet.Columns("total").Index).Text)
            Dim idVenta As Integer = ALMLogicaDevoluciones.Funciones.ValidarNumeroACero(spDevoluciones.ActiveSheet.Cells(fila, spDevoluciones.ActiveSheet.Columns("idVenta").Index).Text)
            Dim orden As Integer = fila
            Dim observaciones As String = String.Empty
            If (id > 0 AndAlso idAlmacen > 0 AndAlso idFamilia > 0 AndAlso idSubFamilia > 0 AndAlso idArticulo > 0 AndAlso idCliente > 0) Then
                devoluciones.EIdAlmacen = idAlmacen
                devoluciones.EId = id
                devoluciones.EIdFamilia = idFamilia
                devoluciones.EIdSubFamilia = idSubFamilia
                devoluciones.EIdArticulo = idArticulo
                devoluciones.EIdCliente = idCliente
                devoluciones.EFecha = fecha
                devoluciones.ECantidad = cantidad
                devoluciones.EPrecio = precio
                devoluciones.ESubTotal = subtotal
                devoluciones.EPorcentajeDescuento = porcentajeDescuento
                devoluciones.EDescuento = descuento
                devoluciones.ETotal = total
                devoluciones.EOrden = orden
                devoluciones.EObservaciones = observaciones
                devoluciones.EIdVenta = idVenta
                devoluciones.Guardar()
            End If
        Next
        GuardarEntradasAlmacen()
        GuardarVales()
        Me.opcionTipoSeleccionada = OpcionTipoEtiqueta.vale
        PrepararImpresion(id)
        hiloImpresion.Start()
        MessageBox.Show("Guardado finalizado.", "Finalizado.", MessageBoxButtons.OK)
        LimpiarPantalla()
        CargarIdConsecutivo()
        AsignarFoco(txtId)
        txtId.SelectAll()
        If (Not Me.estaImprimiendo) Then
            hiloImpresion.Abort()
        End If

    End Sub

    Private Sub GuardarEntradasAlmacen()

        ' No capturables por el usuario.
        Dim idExterno As String = String.Empty
        Dim idMoneda As Integer = 1 ' Pesos
        Dim tipoCambio As Double = 1 ' 1
        Dim idTipoEntrada As Integer = 2 ' Devolución.
        Dim factura As String = String.Empty
        Dim id As Integer = ALMLogicaDevoluciones.Funciones.ValidarNumeroACero(txtId.Text)
        ' Se eliminan todas las entradas de acuerdo al idorigen, idalmacen y idsalida.
        EliminarEntradasAlmacen()
        Dim datos As New DataTable
        datos = devoluciones.ObtenerListadoGeneral()
        ' Parte inferior.
        For fila As Integer = 0 To datos.Rows.Count - 1
            Dim idAlmacen As Integer = datos.Rows(fila).Item("IdAlmacen")
            Dim idFamilia As Integer = datos.Rows(fila).Item("IdFamilia")
            Dim idSubFamilia As Integer = datos.Rows(fila).Item("IdSubFamilia")
            Dim idArticulo As Integer = datos.Rows(fila).Item("IdArticulo")
            Dim fecha As Date = datos.Rows(fila).Item("Fecha")
            Dim idProveedor As Integer = 10 'TODO. Verificar que sea público en general. datos.Rows(fila).Item("IdProveedor")
            Dim cantidad As Integer = datos.Rows(fila).Item("Cantidad")
            Dim precio As Double = datos.Rows(fila).Item("Precio")
            Dim total As Double = datos.Rows(fila).Item("Total")
            Dim totalPesos As Double = total
            Dim orden As Integer = fila
            Dim observaciones As String = datos.Rows(fila).Item("Observaciones")
            If (Me.idOrigen > 0 AndAlso id > 0 AndAlso idAlmacen > 0 AndAlso idFamilia > 0 AndAlso idSubFamilia > 0 AndAlso idArticulo > 0 AndAlso idMoneda > 0 AndAlso idTipoEntrada > 0 AndAlso idProveedor > 0) Then
                entradas.EIdOrigen = Me.idOrigen
                entradas.EIdAlmacen = idAlmacen
                entradas.EId = id
                entradas.EIdFamilia = idFamilia
                entradas.EIdSubFamilia = idSubFamilia
                entradas.EIdArticulo = idArticulo
                entradas.EIdExterno = idExterno
                entradas.EIdTipoEntrada = idTipoEntrada
                entradas.EIdProveedor = idProveedor
                entradas.EIdMoneda = idMoneda
                entradas.ETipoCambio = tipoCambio
                entradas.EFecha = fecha
                entradas.ECantidad = cantidad
                entradas.EPrecio = precio
                entradas.ETotal = total
                entradas.ETotalPesos = totalPesos
                entradas.EOrden = orden
                entradas.EObservaciones = observaciones
                entradas.EFactura = factura
                entradas.Guardar()
            End If
        Next

    End Sub

    Private Sub EliminarEntradasAlmacen()

        Dim datos As New DataTable
        Dim id As Integer = ALMLogicaDevoluciones.Funciones.ValidarNumeroACero(txtId.Text)
        devoluciones.EId = id
        datos = devoluciones.ObtenerListadoParaAlmacen()
        entradas.EIdOrigen = Me.idOrigen
        For fila = 0 To datos.Rows.Count - 1
            Dim idAlmacen As Integer = datos.Rows(fila).Item("IdAlmacen")
            entradas.EIdAlmacen = idAlmacen
            entradas.EId = id
            entradas.Eliminar()
        Next

    End Sub

    Private Sub GuardarVales()

        Dim id As Integer = ALMLogicaDevoluciones.Funciones.ValidarNumeroACero(txtId.Text)
        ' Se eliminan todos los vales de acuerdo al idorigen y id.
        EliminarVales(False)
        Dim datos As New DataTable
        datos = devoluciones.ObtenerListadoGeneral()
        If (datos.Rows.Count > 0) Then
            ' Parte superior.
            Dim idCliente As Integer = datos.Rows(0).Item("IdCliente")
            Dim fecha As Date = datos.Rows(0).Item("Fecha")
            Dim fechaVencimiento As Date = fecha.AddDays(30)
            Dim estaUtilizado As Boolean = False
            Dim idVenta As Integer = 0
            ' Parte inferior.
            Dim sumaTotal As Double = 0
            For fila As Integer = 0 To datos.Rows.Count - 1
                Dim idAlmacen As Integer = datos.Rows(fila).Item("IdAlmacen")
                Dim idFamilia As Integer = datos.Rows(fila).Item("IdFamilia")
                Dim idSubFamilia As Integer = datos.Rows(fila).Item("IdSubFamilia")
                Dim idArticulo As Integer = datos.Rows(fila).Item("IdArticulo")
                Dim cantidad As Integer = datos.Rows(fila).Item("Cantidad")
                Dim precio As Double = datos.Rows(fila).Item("Precio")
                Dim total As Double = datos.Rows(fila).Item("Total")
                sumaTotal += total
                Dim orden As Integer = fila
                If (Me.idOrigen > 0 AndAlso id > 0 AndAlso cantidad > 0) Then
                    valesDetalle.EIdOrigen = Me.idOrigen
                    valesDetalle.EId = id
                    valesDetalle.EIdAlmacen = idAlmacen
                    valesDetalle.EIdFamilia = idFamilia
                    valesDetalle.EIdSubFamilia = idSubFamilia
                    valesDetalle.EIdArticulo = idArticulo
                    valesDetalle.ECantidad = cantidad
                    valesDetalle.EPrecio = precio
                    valesDetalle.ETotal = total
                    valesDetalle.EOrden = orden
                    valesDetalle.Guardar()
                End If
            Next
            If (Me.idOrigen > 0 AndAlso id > 0) Then
                vales.EIdOrigen = Me.idOrigen
                vales.EId = id
                vales.EIdCliente = idCliente
                vales.EFecha = fecha
                vales.ETotal = sumaTotal
                vales.EFechaVencimiento = fechaVencimiento
                vales.EEstaUtilizado = estaUtilizado
                vales.EIdVenta = idVenta
                vales.Guardar()
            End If
        End If

    End Sub

    Private Sub EliminarVales(ByVal conMensaje As Boolean)

        Dim respuestaSi As Boolean = False
        If (conMensaje) Then
            If (MessageBox.Show("Confirmas que deseas eliminar este vale?", "Confirmación.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes) Then
                respuestaSi = True
            End If
        End If
        If ((respuestaSi) Or (Not conMensaje)) Then
            Dim id As Integer = ALMLogicaDevoluciones.Funciones.ValidarNumeroACero(txtId.Text)
            valesDetalle.EIdOrigen = Me.idOrigen
            valesDetalle.EId = id
            valesDetalle.Eliminar()
            vales.EIdOrigen = Me.idOrigen
            vales.EId = id
            vales.Eliminar()
        End If
        If (conMensaje And respuestaSi) Then
            MessageBox.Show("Eliminado finalizado.", "Finalizado.", MessageBoxButtons.OK)
            LimpiarPantalla()
            CargarIdConsecutivo()
            AsignarFoco(txtId)
        End If

    End Sub

    Private Sub ImprimirManualmente()

        Dim id As Integer = ALMLogicaDevoluciones.Funciones.ValidarNumeroACero(txtId.Text)
        Me.opcionTipoSeleccionada = OpcionTipoEtiqueta.vale
        PrepararImpresion(id)
        MandarImprimir()

    End Sub

    Private Sub CargarOpcionesImpresion()

        Impresoras.CargarImpresoras(True)

    End Sub

    Private Sub PrepararImpresion(ByVal id As Integer)

        vales.EIdOrigen = Me.idOrigen
        vales.EId = id
        If (Me.opcionTipoSeleccionada = OpcionTipoEtiqueta.vale) Then ' Aquí se obtienen datos del vale.
            Me.datosValesParaImprimir = vales.ObtenerListadoImpresionVales()
            For fila As Integer = 0 To Me.datosValesParaImprimir.Rows.Count - 1
                ' Se agrega al listado.
                Me.listaValesParaImprimir.Add(Me.datosValesParaImprimir.Rows(fila))
            Next
        End If

    End Sub

    Private Sub MandarImprimir()

        Me.estaImprimiendo = True
        ' Impresión de vales.
        ' Si hay datos para imprimir.
        If (Me.listaValesParaImprimir.Count > 0) Then
            pdVale.PrinterSettings.PrinterName = Impresoras.nombreImpresoraVale
            If (Impresoras.habilitarImpresoraVale) Then
                pdVale.DocumentName = "Vale" & Me.idOrigen.ToString.PadLeft(2, "0") & txtId.Text.PadLeft(6, "0")
                Try
                    pdVale.Print()
                Catch ex As Exception
                    MsgBox("Hay un error al imprimir. " & ex.Message, MsgBoxStyle.Critical, "Error.")
                End Try
            End If
        End If
        ' Se reinician los valores para la próxima impresión.
        Me.contadorValesParaImprimir = 0
        Me.listaValesParaImprimir.Clear()
        Me.datosValesParaImprimir.Clear()
        Me.estaImprimiendo = False

    End Sub

    Private Sub CrearFormatoVale(ByRef e As System.Drawing.Printing.PrintPageEventArgs)

        Dim fuenteDescripcion8 As New Font(Principal.tipoLetraSpread, 8, FontStyle.Bold)
        Dim imagen As System.Drawing.Image = Nothing
        Dim margenIzquierdoVale As Integer = Impresoras.margenIzquierdoVale : Dim margenSuperiorVale As Integer = Impresoras.margenSuperiorVale
        Dim altura As Integer = 0
        ' Se obtienen los datos generales.
        ' Información de la empresa.
        Dim empresa As String = String.Empty
        Dim representante As String = String.Empty
        Dim rfc As String = String.Empty
        Dim domicilio As String = String.Empty
        Dim telefono As String = String.Empty
        Dim municipio As String = String.Empty
        Dim estado As String = String.Empty
        Dim pais As String = String.Empty
        Dim datos As New DataTable
        empresas.EId = 0 ' Se busca la primer empresa.
        datos = empresas.ObtenerListado(True)
        If (datos.Rows.Count > 0) Then
            empresa = ALMLogicaDevoluciones.Funciones.ValidarLetra(datos.Rows(0).Item("Nombre").ToString())
            representante = ALMLogicaDevoluciones.Funciones.ValidarLetra(datos.Rows(0).Item("RepresentanteLegal").ToString())
            rfc = ALMLogicaDevoluciones.Funciones.ValidarLetra(datos.Rows(0).Item("Rfc").ToString())
            domicilio = ALMLogicaDevoluciones.Funciones.ValidarLetra(datos.Rows(0).Item("Domicilio").ToString())
            telefono = ALMLogicaDevoluciones.Funciones.ValidarLetra(datos.Rows(0).Item("Telefono").ToString())
            municipio = ALMLogicaDevoluciones.Funciones.ValidarLetra(datos.Rows(0).Item("Municipio").ToString())
            estado = ALMLogicaDevoluciones.Funciones.ValidarLetra(datos.Rows(0).Item("Estado").ToString())
            pais = ALMLogicaDevoluciones.Funciones.ValidarLetra(datos.Rows(0).Item("Pais").ToString())
        End If
        ' Información de vale.
        Dim idOrigen As String = ALMLogicaDevoluciones.Funciones.ValidarLetra(Me.listaValesParaImprimir.Item(Me.contadorValesParaImprimir).Item("IdOrigen").ToString()).PadLeft(2, "0").ToString()
        Dim id As String = ALMLogicaDevoluciones.Funciones.ValidarLetra(Me.listaValesParaImprimir.Item(Me.contadorValesParaImprimir).Item("Id").ToString()).PadLeft(6, "0").ToString()
        Dim fecha As String = ALMLogicaDevoluciones.Funciones.ValidarLetra(Me.listaValesParaImprimir.Item(Me.contadorValesParaImprimir).Item("Fecha"))
        Dim hora As String = Now.ToShortTimeString
        Dim nombreCliente As String = ALMLogicaDevoluciones.Funciones.ValidarLetra(Me.listaValesParaImprimir.Item(Me.contadorValesParaImprimir).Item("NombreCliente").ToString())
        altura = 5
        e.Graphics.DrawString(empresa, fuenteDescripcion8, Brushes.Black, margenIzquierdoVale + 0, margenSuperiorVale + altura) ' Empresa.
        altura += 12
        e.Graphics.DrawString(representante, fuenteDescripcion8, Brushes.Black, margenIzquierdoVale + 0, margenSuperiorVale + altura) ' Representante.
        altura += 12
        e.Graphics.DrawString(rfc, fuenteDescripcion8, Brushes.Black, margenIzquierdoVale + 0, margenSuperiorVale + altura) ' Rfc.
        altura += 12
        e.Graphics.DrawString(domicilio, fuenteDescripcion8, Brushes.Black, margenIzquierdoVale + 0, margenSuperiorVale + altura) ' Domicilio.
        altura += 12
        e.Graphics.DrawString(telefono, fuenteDescripcion8, Brushes.Black, margenIzquierdoVale + 0, margenSuperiorVale + altura) ' Telefono.
        altura += 12
        e.Graphics.DrawString(municipio & ", " & estado & ", " & pais, fuenteDescripcion8, Brushes.Black, margenIzquierdoVale + 0, margenSuperiorVale + altura) ' Localidad.
        altura += 20
        e.Graphics.DrawString("Vale: " & idOrigen & id, fuenteDescripcion8, Brushes.Black, margenIzquierdoVale + 0, margenSuperiorVale + altura) ' Id.
        e.Graphics.DrawString(fecha & " " & hora, fuenteDescripcion8, Brushes.Black, margenIzquierdoVale + 125, margenSuperiorVale + altura) ' Fecha y Hora.
        altura += 20
        e.Graphics.DrawString("Cliente: ", fuenteDescripcion8, Brushes.Black, margenIzquierdoVale + 0, margenSuperiorVale + altura) ' Cliente.
        altura += 12
        e.Graphics.DrawString(nombreCliente, fuenteDescripcion8, Brushes.Black, margenIzquierdoVale + 0, margenSuperiorVale + altura) ' Cliente.
        altura += 20
        e.Graphics.DrawLine(Pens.Black, margenIzquierdoVale + 0, margenSuperiorVale + altura, margenIzquierdoVale + 300, margenSuperiorVale + altura) ' Linea.
        e.Graphics.DrawString("Cant.", fuenteDescripcion8, Brushes.Black, margenIzquierdoVale + 0, margenSuperiorVale + altura) ' Cantidad.
        e.Graphics.DrawString("Modelo", fuenteDescripcion8, Brushes.Black, margenIzquierdoVale + 45, margenSuperiorVale + altura) ' Modelo.
        e.Graphics.DrawString("Color", fuenteDescripcion8, Brushes.Black, margenIzquierdoVale + 95, margenSuperiorVale + altura) ' Color.
        e.Graphics.DrawString("Núm.", fuenteDescripcion8, Brushes.Black, margenIzquierdoVale + 150, margenSuperiorVale + altura) ' Talla.
        e.Graphics.DrawString("Precio", fuenteDescripcion8, Brushes.Black, margenIzquierdoVale + 195, margenSuperiorVale + altura) ' Precio.
        altura += 12
        Dim total As Double = 0
        For indice = 0 To Me.listaValesParaImprimir.Count - 1
            Dim cantidad As String = ALMLogicaDevoluciones.Funciones.ValidarLetra(Me.listaValesParaImprimir.Item(indice).Item("Cantidad").ToString())
            Dim modelo As String = ALMLogicaDevoluciones.Funciones.ValidarLetra(Me.listaValesParaImprimir.Item(indice).Item("Modelo").ToString())
            Dim color As String = ALMLogicaDevoluciones.Funciones.ValidarLetra(Me.listaValesParaImprimir.Item(indice).Item("Color").ToString())
            Dim talla As String = ALMLogicaDevoluciones.Funciones.ValidarLetra(Me.listaValesParaImprimir.Item(indice).Item("Talla").ToString())
            Dim precio As Double = ALMLogicaDevoluciones.Funciones.ValidarNumeroACero(Me.listaValesParaImprimir.Item(indice).Item("Precio").ToString())
            total += ALMLogicaDevoluciones.Funciones.ValidarNumeroACero(Me.listaValesParaImprimir.Item(indice).Item("Total").ToString())
            e.Graphics.DrawString(cantidad, fuenteDescripcion8, Brushes.Black, margenIzquierdoVale + 0, margenSuperiorVale + altura) ' Cantidad.
            e.Graphics.DrawString(modelo, fuenteDescripcion8, Brushes.Black, margenIzquierdoVale + 45, margenSuperiorVale + altura) ' Modelo.
            e.Graphics.DrawString(color, fuenteDescripcion8, Brushes.Black, margenIzquierdoVale + 95, margenSuperiorVale + altura) ' Color.
            e.Graphics.DrawString(talla, fuenteDescripcion8, Brushes.Black, margenIzquierdoVale + 150, margenSuperiorVale + altura) ' Talla.
            e.Graphics.DrawString(precio.ToString("C"), fuenteDescripcion8, Brushes.Black, margenIzquierdoVale + 195, margenSuperiorVale + altura) ' Precio.
            altura += 12
        Next
        e.Graphics.DrawLine(Pens.Black, margenIzquierdoVale + 0, margenSuperiorVale + altura, margenIzquierdoVale + 300, margenSuperiorVale + altura) ' Linea. 
        e.Graphics.DrawString("Total", fuenteDescripcion8, Brushes.Black, margenIzquierdoVale + 95, margenSuperiorVale + altura) ' Total.
        e.Graphics.DrawString(total.ToString("C"), fuenteDescripcion8, Brushes.Black, margenIzquierdoVale + 195, margenSuperiorVale + altura) ' Total.
        altura += 20
        e.Graphics.DrawString("Le atendió:", fuenteDescripcion8, Brushes.Black, margenIzquierdoVale + 0, margenSuperiorVale + altura) ' Atendio.
        altura += 12
        e.Graphics.DrawString(ALMLogicaDevoluciones.Usuarios.nombre, fuenteDescripcion8, Brushes.Black, margenIzquierdoVale + 0, margenSuperiorVale + altura) ' Atendio.
        altura += 20
        e.Graphics.DrawString("¡Gracias por su preferencia!", fuenteDescripcion8, Brushes.Black, margenIzquierdoVale + 0, margenSuperiorVale + altura) ' Gracias.
        altura += 20
        e.Graphics.DrawString("Válido hasta:", fuenteDescripcion8, Brushes.Black, margenIzquierdoVale + 0, margenSuperiorVale + altura) ' Garantia.
        altura += 12
        e.Graphics.DrawString(Convert.ToDateTime(fecha).AddDays(30), fuenteDescripcion8, Brushes.Black, margenIzquierdoVale + 0, margenSuperiorVale + altura) ' Garantia. 

    End Sub

    Private Sub EliminarDevoluciones(ByVal conMensaje As Boolean)

        Dim respuestaSi As Boolean = False
        If (conMensaje) Then
            If (MessageBox.Show("Confirmas que deseas eliminar esta devolución?", "Confirmación.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes) Then
                respuestaSi = True
            End If
        End If
        If ((respuestaSi) Or (Not conMensaje)) Then
            EliminarVales(False)
            EliminarEntradasAlmacen()
            devoluciones.EId = ALMLogicaDevoluciones.Funciones.ValidarNumeroACero(txtId.Text)
            devoluciones.Eliminar()
        End If
        If (conMensaje And respuestaSi) Then
            MessageBox.Show("Eliminado finalizado.", "Finalizado.", MessageBoxButtons.OK)
            LimpiarPantalla()
            CargarIdConsecutivo()
            AsignarFoco(txtId)
            txtId.SelectAll()
        End If

    End Sub

#End Region

#End Region

#Region "Enumeraciones"

    Enum OpcionPosicion

        izquierda = 1
        centro = 2
        derecha = 3

    End Enum

    Enum OpcionCatalogo

        almacen = 1
        familia = 2
        subfamilia = 3
        articulo = 4
        cliente = 5
        moneda = 6
        tipoSalida = 7

    End Enum

    Enum OpcionTipoEtiqueta

        ' Este listado debe corresponder a la tabla TiposEtiquetas (no es configurable por el usuario).
        vale = 1

    End Enum

#End Region

End Class