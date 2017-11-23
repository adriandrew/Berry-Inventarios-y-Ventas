Imports System.IO
Imports System.ComponentModel
Imports System.Threading

Public Class Principal

    ' Variables de objetos de entidades.
    Public usuarios As New ALMEntidadesVentas.Usuarios()
    Public ventas As New ALMEntidadesVentas.Ventas()
    Public ventasDetalle As New ALMEntidadesVentas.VentasDetalle()
    Public vales As New ALMEntidadesVentas.Vales()
    Public salidas As New ALMEntidadesVentas.Salidas()
    Public almacenes As New ALMEntidadesVentas.Almacenes()
    Public familias As New ALMEntidadesVentas.Familias()
    Public subFamilias As New ALMEntidadesVentas.SubFamilias()
    Public articulos As New ALMEntidadesVentas.Articulos()
    Public unidadesMedidas As New ALMEntidadesVentas.UnidadesMedidas()
    Public clientes As New ALMEntidadesVentas.Clientes()
    Public metodosPagos As New ALMEntidadesVentas.MetodosPagos()
    Public empresas As New ALMEntidadesVentas.Empresas()
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
    Public opcionEtiquetaTarimaSeleccionada As Integer = -1 : Public opcionEtiquetaCajaSeleccionada As Integer = 0
    Public datosRecibosParaImprimir As New DataTable
    Public contadorRecibosParaImprimir As Integer = 0 : Public listaRecibosParaImprimir As New List(Of System.Data.DataRow)
    Public datosValesParaImprimir As New DataTable
    Public contadorValesParaImprimir As Integer = 0 : Public listaValesParaImprimir As New List(Of System.Data.DataRow)
    Public estaImprimiendo As Boolean = False
    ' Variables fijas.
    Public idOrigen As Integer = 3 ' Siempre será 3 para ventas.
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
        FormatearSpreadVentas()
        FormatearSpreadVales()
        CargarComboClientes()
        CargarComboMetodosPago()
        CargarOpcionesImpresion()
        CargarIdConsecutivo()
        AsignarFoco(txtId)
        Me.estaMostrado = True
        CargarEstilos()
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

    Private Sub spVentas_DialogKey(sender As Object, e As FarPoint.Win.Spread.DialogKeyEventArgs) Handles spVentas.DialogKey

        If (e.KeyData = Keys.Enter) Then
            ControlarSpreadEnter(spVentas)
        End If

    End Sub

    Private Sub spVentas_KeyDown(sender As Object, e As KeyEventArgs) Handles spVentas.KeyDown

        Me.Cursor = Cursors.WaitCursor
        If (e.KeyData = Keys.F6) Then ' Eliminar un registro.
            If (MessageBox.Show("Confirmas que deseas eliminar el registro seleccionado?", "Confirmación.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes) Then
                EliminarRegistroDeSpread(spVentas)
            End If
        ElseIf (e.KeyData = Keys.Enter) Then ' Validar registros.
            ControlarSpreadEnter(spVentas)
        ElseIf (e.KeyData = Keys.F5) Then ' Abrir catalogos.
            CargarCatalogoEnSpread()
        ElseIf (e.KeyData = Keys.Escape) Then
            spVentas.ActiveSheet.SetActiveCell(0, 0)
            AsignarFoco(cbClientes)
        End If
        Me.Cursor = Cursors.Default

    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click

        Me.Cursor = Cursors.WaitCursor
        ValidarGuardadoVentas()
        If (Me.esGuardadoValido) Then
            GuardarEditarVentas()
        End If
        Me.Cursor = Cursors.Default

    End Sub

    Private Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click

        Me.Cursor = Cursors.WaitCursor
        EliminarVentas(True)
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
                CargarVentas()
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
                AsignarFoco(spVentas)
                spVentas.ActiveSheet.SetActiveCell(0, 1)
            Else
                cbClientes.SelectedIndex = 0
            End If
        ElseIf (e.KeyData = Keys.Escape) Then
            e.SuppressKeyPress = True
            AsignarFoco(spVentas)
        ElseIf (e.KeyData = Keys.F5) Then ' Abrir catalogos.
            Me.opcionCatalogoSeleccionada = OpcionCatalogo.cliente
            CargarCatalogoEnOtros()
        End If

    End Sub

    Private Sub btnIdAnterior_Click(sender As Object, e As EventArgs) Handles btnIdAnterior.Click

        If (ALMLogicaVentas.Funciones.ValidarNumeroACero(txtId.Text) > 1) Then
            txtId.Text -= 1
            CargarVentas()
        End If

    End Sub

    Private Sub btnIdSiguiente_Click(sender As Object, e As EventArgs) Handles btnIdSiguiente.Click

        If (ALMLogicaVentas.Funciones.ValidarNumeroACero(txtId.Text) >= 1) Then
            txtId.Text += 1
            CargarVentas()
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
                MostrarOcultarColumnasDetalle(True)
            Else
                MostrarOcultarColumnasDetalle(False)
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

    Private Sub pdRecibo_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles pdRecibo.PrintPage

        CrearRecibo(e)

    End Sub

    Private Sub btnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click

        Me.Cursor = Cursors.WaitCursor
        ImprimirManualmente()
        Me.Cursor = Cursors.Default

    End Sub

    Private Sub cbMetodosPagos_KeyDown(sender As Object, e As KeyEventArgs) Handles cbMetodosPagos.KeyDown

        If (e.KeyData = Keys.Enter) Then
            e.SuppressKeyPress = True
            If (cbMetodosPagos.SelectedValue > 0) Then
                AsignarFoco(txtImportePagado)
            Else
                cbMetodosPagos.SelectedIndex = 0
            End If
        ElseIf (e.KeyData = Keys.Escape) Then
            e.SuppressKeyPress = True
            AsignarFoco(spVentas)
            'ElseIf (e.KeyData = Keys.F5) Then ' Abrir catalogos.
            '    Me.opcionCatalogoSeleccionada = OpcionCatalogo.metodosPagos
            '    CargarCatalogoEnOtros()
        End If

    End Sub

    Private Sub txtImportePagado_KeyDown(sender As Object, e As KeyEventArgs) Handles txtImportePagado.KeyDown

        If (e.KeyData = Keys.Enter) Then
            e.SuppressKeyPress = True
            CalcularCambio()
        ElseIf (e.KeyData = Keys.Escape) Then
            e.SuppressKeyPress = True
            AsignarFoco(cbMetodosPagos)
        End If

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

    Private Sub spVales_DialogKey(sender As Object, e As FarPoint.Win.Spread.DialogKeyEventArgs) Handles spVales.DialogKey

        If (e.KeyData = Keys.Enter) Then ' Validar registros.
            ControlarSpreadEnter(spVales)
        End If

    End Sub

    Private Sub spVales_KeyDown(sender As Object, e As KeyEventArgs) Handles spVales.KeyDown

        If (e.KeyData = Keys.F6) Then ' Eliminar un registro.
            If (MessageBox.Show("Confirmas que deseas eliminar el registro seleccionado?", "Confirmación.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes) Then
                EliminarRegistroDeSpread(spVales)
                spVales.ActiveSheet.Rows.Count = 2
            End If
        ElseIf (e.KeyData = Keys.Enter) Then ' Validar registros.
            ControlarSpreadEnter(spVales)
        ElseIf (e.KeyData = Keys.F7) Then ' Abrir catalogos.
            CargarListadosOtros()
        End If

    End Sub

    Private Sub pdVale_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles pdVale.PrintPage

        CrearVale(e)

    End Sub

    Private Sub pnlCapturaSuperior_MouseEnter(sender As Object, e As EventArgs) Handles pnlCapturaSuperior.MouseEnter

        AsignarTooltips("Capturar Datos Generales")

    End Sub

    Private Sub spVentas_MouseEnter(sender As Object, e As EventArgs) Handles spVentas.MouseEnter

        AsignarTooltips("Capturar Datos Detallados")

    End Sub

    Private Sub spVales_MouseEnter(sender As Object, e As EventArgs) Handles spVales.MouseEnter

        AsignarTooltips("Capturar Datos Detallados")

    End Sub

    Private Sub pnlTotales_MouseEnter(sender As Object, e As EventArgs) Handles pnlTotales.MouseEnter

        AsignarTooltips("Totales")

    End Sub

    Private Sub pnlPie_MouseEnter(sender As Object, e As EventArgs) Handles pnlPie.MouseEnter

        AsignarTooltips("Opciones")

    End Sub

    Private Sub spListadosOtros_CellClick(sender As Object, e As FarPoint.Win.Spread.CellClickEventArgs) Handles spListadosOtros.CellClick

        Dim fila As Integer = e.Row
        CargarDatosDeListadosOtros(fila)

    End Sub

    Private Sub spListadosOtros_CellDoubleClick(sender As Object, e As FarPoint.Win.Spread.CellClickEventArgs) Handles spListadosOtros.CellDoubleClick

        VolverFocoDeListadosOtros()

    End Sub

    Private Sub spListadosOtros_KeyDown(sender As Object, e As KeyEventArgs) Handles spListadosOtros.KeyDown

        If (e.KeyCode = Keys.Escape) Then
            VolverFocoDeListadosOtros()
        End If

    End Sub

#End Region

#Region "Métodos"

#Region "Básicos"

    Private Sub CargarEstilos()

        pnlCapturaSuperior.BackColor = Principal.colorSpreadAreaGris
        pnlTotales.BackColor = Principal.colorSpreadAreaGris
        pnlPie.BackColor = Principal.colorSpreadAreaGris
        spVentas.ActiveSheet.GrayAreaBackColor = Principal.colorSpreadAreaGris
        spVales.ActiveSheet.GrayAreaBackColor = Principal.colorSpreadAreaGris

    End Sub

    Private Sub BuscarCatalogos()

        Dim valorBuscado As String = txtBuscarCatalogo.Text.Replace("á", "a").Replace("é", "e").Replace("í", "i").Replace("ó", "o").Replace("ú", "u")
        For fila = 0 To spCatalogos.ActiveSheet.Rows.Count - 1
            Dim valorSpread As String = ALMLogicaVentas.Funciones.ValidarLetra(spCatalogos.ActiveSheet.Cells(fila, spCatalogos.ActiveSheet.Columns("id").Index).Text & spCatalogos.ActiveSheet.Cells(fila, spCatalogos.ActiveSheet.Columns("nombre").Index).Text).Replace("á", "a").Replace("é", "e").Replace("í", "i").Replace("ó", "o").Replace("ú", "u")
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
            spVentas.Left = anchoMenor + espacio
            spVentas.Width = Me.anchoTotal - anchoMenor - espacio
            Me.esIzquierda = True
        Else
            pnlCapturaSuperior.Left = 0
            spVentas.Left = pnlCapturaSuperior.Width + espacio
            spVentas.Width = Me.anchoTotal - pnlCapturaSuperior.Width - espacio
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
            txtAyuda.Text = "Sección de Ayuda: " & vbNewLine & vbNewLine & "* Teclas básicas: " & vbNewLine & "F5 sirve para mostrar catálogos. " & vbNewLine & "F6 sirve para eliminar un registro únicamente. " & vbNewLine & "F7 sirve para mostrar listados." & vbNewLine & "Escape sirve para ocultar catálogos o listados que se encuentren desplegados. " & vbNewLine & vbNewLine & "* Catálogos o listados desplegados: " & vbNewLine & "Cuando se muestra algún catálogo o listado, al seleccionar alguna opción de este, se va mostrando en tiempo real en la captura de donde se originó. Cuando se le da doble clic en alguna opción o a la tecla escape se oculta dicho catálogo o listado. " & vbNewLine & vbNewLine & "* Datos obligatorios:" & vbNewLine & "Todos los que tengan el simbolo * son estrictamente obligatorios." & vbNewLine & vbNewLine & "* Captura:" & vbNewLine & "* Parte superior/izquierda: " & vbNewLine & "En esta parte se capturarán todos los datos que son generales, tal cual como el número de venta, fecha, cliente, método de pago, etc." & vbNewLine & "* Parte inferior/derecha: " & vbNewLine & "En esta parte se capturarán todos los artículos correspondientes a esa venta y otros datos extra que llevará, por ejemplo, cantidad, precio, porcentaje de descuento, etc." & vbNewLine & vbNewLine & "* Opciones:" & vbNewLine & "Existe un botón para configurar impresoras y otro botón para imprimir/reimprimir directamente. " & vbNewLine & vbNewLine & "* Existen los botones de guardar/editar y eliminar todo dependiendo lo que se necesite hacer. "
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

        If ((Not ALMLogicaVentas.Usuarios.accesoTotal) Or (ALMLogicaVentas.Usuarios.accesoTotal = 0) Or (ALMLogicaVentas.Usuarios.accesoTotal = False)) Then
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
            ALMLogicaVentas.Directorios.id = 2
            ALMLogicaVentas.Directorios.instanciaSql = "BERRY1-DELL\SQLEXPRESS2008"
            ALMLogicaVentas.Directorios.usuarioSql = "AdminBerry"
            ALMLogicaVentas.Directorios.contrasenaSql = "@berry2017"
            pnlEncabezado.BackColor = Color.DarkRed
        Else
            ALMLogicaVentas.Directorios.ObtenerParametros()
            ALMLogicaVentas.Usuarios.ObtenerParametros()
        End If
        ALMLogicaVentas.Programas.bdCatalogo = "Catalogo" & ALMLogicaVentas.Directorios.id
        ALMLogicaVentas.Programas.bdConfiguracion = "Configuracion" & ALMLogicaVentas.Directorios.id
        ALMLogicaVentas.Programas.bdAlmacen = "Almacen" & ALMLogicaVentas.Directorios.id
        ALMEntidadesVentas.BaseDatos.ECadenaConexionCatalogo = ALMLogicaVentas.Programas.bdCatalogo
        ALMEntidadesVentas.BaseDatos.ECadenaConexionConfiguracion = ALMLogicaVentas.Programas.bdConfiguracion
        ALMEntidadesVentas.BaseDatos.ECadenaConexionAlmacen = ALMLogicaVentas.Programas.bdAlmacen
        ALMEntidadesVentas.BaseDatos.AbrirConexionCatalogo()
        ALMEntidadesVentas.BaseDatos.AbrirConexionConfiguracion()
        ALMEntidadesVentas.BaseDatos.AbrirConexionAlmacen()
        ConsultarInformacionUsuario()
        CargarPrefijoBaseDatosAlmacen()

    End Sub

    Private Sub CargarPrefijoBaseDatosAlmacen()

        ALMLogicaVentas.Programas.prefijoBaseDatosAlmacen = Me.prefijoBaseDatosAlmacen

    End Sub

    Private Sub ConsultarInformacionUsuario()

        Dim lista As New List(Of ALMEntidadesVentas.Usuarios)
        usuarios.EId = ALMLogicaVentas.Usuarios.id
        lista = usuarios.ObtenerListado()
        If (lista.Count > 0) Then
            ALMLogicaVentas.Usuarios.id = lista(0).EId
            ALMLogicaVentas.Usuarios.nombre = lista(0).ENombre
            ALMLogicaVentas.Usuarios.contrasena = lista(0).EContrasena
            ALMLogicaVentas.Usuarios.nivel = lista(0).ENivel
            ALMLogicaVentas.Usuarios.accesoTotal = lista(0).EAccesoTotal
        End If

    End Sub

    Private Sub CargarEncabezadosTitulos()

        lblEncabezadoPrograma.Text = "Programa: " & Me.Text
        lblEncabezadoEmpresa.Text = "Directorio: " & ALMLogicaVentas.Directorios.nombre
        lblEncabezadoUsuario.Text = "Usuario: " & ALMLogicaVentas.Usuarios.nombre
        Me.Text = "Programa:  " & Me.nombreEstePrograma & "              Directorio:  " & ALMLogicaVentas.Directorios.nombre & "              Usuario:  " & ALMLogicaVentas.Usuarios.nombre
        hiloEncabezadosTitulos.Abort()

    End Sub

    Private Sub AbrirPrograma(nombre As String, salir As Boolean)

        If (Me.esDesarrollo) Then
            Exit Sub
        End If
        ejecutarProgramaPrincipal.UseShellExecute = True
        ejecutarProgramaPrincipal.FileName = nombre & Convert.ToString(".exe")
        ejecutarProgramaPrincipal.WorkingDirectory = Application.StartupPath
        ejecutarProgramaPrincipal.Arguments = ALMLogicaVentas.Directorios.id.ToString().Trim().Replace(" ", "|") & " " & ALMLogicaVentas.Directorios.nombre.ToString().Trim().Replace(" ", "|") & " " & ALMLogicaVentas.Directorios.descripcion.ToString().Trim().Replace(" ", "|") & " " & ALMLogicaVentas.Directorios.rutaLogo.ToString().Trim().Replace(" ", "|") & " " & ALMLogicaVentas.Directorios.esPredeterminado.ToString().Trim().Replace(" ", "|") & " " & ALMLogicaVentas.Directorios.instanciaSql.ToString().Trim().Replace(" ", "|") & " " & ALMLogicaVentas.Directorios.usuarioSql.ToString().Trim().Replace(" ", "|") & " " & ALMLogicaVentas.Directorios.contrasenaSql.ToString().Trim().Replace(" ", "|") & " " & "Aquí terminan los de directorios, indice 9 ;)".Replace(" ", "|") & " " & ALMLogicaVentas.Usuarios.id.ToString().Trim().Replace(" ", "|") & " " & "Aquí terminan los de usuario, indice 11 ;)".Replace(" ", "|")
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
        Me.arriba = spVentas.Top
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
            If ((Not TypeOf c Is Button) AndAlso (Not TypeOf c Is Label)) Then
                c.BackColor = Principal.colorCaptura
            End If
        Next
        For fila = 0 To spVentas.ActiveSheet.Rows.Count - 1
            For columna = 0 To spVentas.ActiveSheet.Columns.Count - 1
                spVentas.ActiveSheet.Cells(fila, columna).BackColor = Principal.colorCaptura
            Next
        Next
        If (Not chkConservarDatos.Checked) Then
            dtpFecha.Value = Today
        End If
        cbClientes.SelectedIndex = 0
        cbMetodosPagos.SelectedIndex = 0
        txtSubTotal.Text = String.Empty
        txtDescuento.Text = String.Empty
        txtTotal.Text = String.Empty
        txtImporteVale1.Text = String.Empty
        txtImporteVale2.Text = String.Empty
        txtImportePagado.Text = String.Empty
        txtImporteCambio.Text = String.Empty
        spVentas.ActiveSheet.DataSource = Nothing
        spVentas.ActiveSheet.Rows.Count = 1
        spVentas.ActiveSheet.SetActiveCell(0, 0)
        LimpiarSpread(spVentas)
        spVales.ActiveSheet.DataSource = Nothing
        LimpiarSpread(spVales)

    End Sub

    Private Sub LimpiarSpread(ByVal spread As FarPoint.Win.Spread.FpSpread)

        spread.ActiveSheet.ClearRange(0, 0, spread.ActiveSheet.Rows.Count, spread.ActiveSheet.Columns.Count, True)

    End Sub

    Private Sub CargarComboClientes()

        cbClientes.DataSource = clientes.ObtenerListadoCombos()
        cbClientes.DisplayMember = "IdNombre"
        cbClientes.ValueMember = "Id"

    End Sub

    Private Sub CargarComboMetodosPago()

        cbMetodosPagos.DataSource = metodosPagos.ObtenerListadoCombos()
        cbMetodosPagos.DisplayMember = "IdNombre"
        cbMetodosPagos.ValueMember = "Id"

    End Sub

    Private Sub FormatearSpread()

        ' Se cargan tipos de datos de spread.
        CargarTiposDeDatos()
        ' Se cargan las opciones generales.
        pnlCatalogos.Visible = False
        spVentas.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Seashell
        spVales.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Seashell
        spCatalogos.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Midnight
        spListadosOtros.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Rose
        spVentas.ActiveSheet.GrayAreaBackColor = Principal.colorSpreadAreaGris
        spVales.ActiveSheet.GrayAreaBackColor = Principal.colorSpreadAreaGris
        spListadosOtros.ActiveSheet.GrayAreaBackColor = Color.FromArgb(255, 230, 230)
        spVentas.Font = New Font(Principal.tipoLetraSpread, Principal.tamañoLetraSpread, FontStyle.Regular)
        spVales.Font = New Font(Principal.tipoLetraSpread, Principal.tamañoLetraSpread, FontStyle.Regular)
        spCatalogos.Font = New Font(Principal.tipoLetraSpread, Principal.tamañoLetraSpread, FontStyle.Regular)
        spListadosOtros.Font = New Font(Principal.tipoLetraSpread, Principal.tamañoLetraSpread, FontStyle.Regular)
        spVentas.ActiveSheet.ColumnHeader.Rows(0).Height = Principal.alturaFilasEncabezadosGrandesSpread
        spVales.ActiveSheet.ColumnHeader.Rows(0).Height = Principal.alturaFilasEncabezadosGrandesSpread
        spCatalogos.ActiveSheet.ColumnHeader.Rows(0).Height = Principal.alturaFilasEncabezadosGrandesSpread
        spListadosOtros.ActiveSheet.ColumnHeader.Rows(0).Height = Principal.alturaFilasEncabezadosGrandesSpread
        spVentas.ActiveSheet.Rows(-1).Height = Principal.alturaFilasSpread
        spVales.ActiveSheet.Rows(-1).Height = Principal.alturaFilasSpread
        spCatalogos.ActiveSheet.Rows(-1).Height = Principal.alturaFilasSpread
        spListadosOtros.ActiveSheet.Rows(-1).Height = Principal.alturaFilasSpread
        spVentas.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        spVentas.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        spVales.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        spVales.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        spCatalogos.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never
        spCatalogos.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Always
        spListadosOtros.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never
        spListadosOtros.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Always
        spVentas.EditModeReplace = True
        spVales.EditModeReplace = True

    End Sub

    Private Sub EliminarRegistroDeSpread(ByVal spread As FarPoint.Win.Spread.FpSpread)

        spread.ActiveSheet.Rows.Remove(spread.ActiveSheet.ActiveRowIndex, 1)
        spread.ActiveSheet.Rows.Count += 1

    End Sub

    Private Sub ControlarSpreadEnter(ByVal spread As FarPoint.Win.Spread.FpSpread)

        Dim columnaActiva As Integer = spread.ActiveSheet.ActiveColumnIndex
        If (spread.Name = spVentas.Name) Then
            Dim fila As Integer = 0
            If (columnaActiva = spread.ActiveSheet.Columns.Count - 1) Then
                spread.ActiveSheet.Rows.Count += 1
            End If
            If (columnaActiva = spVentas.ActiveSheet.Columns("idAlmacen").Index) Then
                fila = spVentas.ActiveSheet.ActiveRowIndex
                Dim idAlmacen As Integer = ALMLogicaVentas.Funciones.ValidarNumeroACero(spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("idAlmacen").Index).Value)
                If (idAlmacen > 0) Then
                    almacenes.EId = idAlmacen
                    Dim datos As New DataTable
                    datos = almacenes.ObtenerListado()
                    If (datos.Rows.Count > 0) Then
                        spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("nombreAlmacen").Index).Value = datos.Rows(0).Item("Nombre")
                        spVentas.ActiveSheet.SetActiveCell(fila, spVentas.ActiveSheet.ActiveColumnIndex + 1)
                    Else
                        spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("idAlmacen").Index, fila, spVentas.ActiveSheet.Columns("nombreAlmacen").Index).Value = String.Empty
                        spVentas.ActiveSheet.SetActiveCell(fila, spVentas.ActiveSheet.ActiveColumnIndex - 1)
                    End If
                Else
                    spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("idAlmacen").Index, fila, spVentas.ActiveSheet.Columns("nombreAlmacen").Index).Value = String.Empty
                    spVentas.ActiveSheet.ClearSelection()
                    spVentas.ActiveSheet.SetActiveCell(fila, 0)
                End If
            ElseIf (columnaActiva = spVentas.ActiveSheet.Columns("idFamilia").Index) Then
                fila = spVentas.ActiveSheet.ActiveRowIndex
                Dim idAlmacen As Integer = ALMLogicaVentas.Funciones.ValidarNumeroACero(spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("idAlmacen").Index).Value)
                Dim idFamilia As Integer = ALMLogicaVentas.Funciones.ValidarNumeroACero(spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("idFamilia").Index).Value)
                If (idAlmacen > 0 And idFamilia > 0) Then
                    familias.EIdAlmacen = idAlmacen
                    familias.EId = idFamilia
                    Dim datos As New DataTable
                    datos = familias.ObtenerListado()
                    If (datos.Rows.Count > 0) Then
                        spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("nombreFamilia").Index).Value = datos.Rows(0).Item("Nombre")
                        spVentas.ActiveSheet.SetActiveCell(fila, spVentas.ActiveSheet.ActiveColumnIndex + 1)
                    Else
                        spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("idFamilia").Index, fila, spVentas.ActiveSheet.Columns("nombreFamilia").Index).Value = String.Empty
                        spVentas.ActiveSheet.SetActiveCell(fila, spVentas.ActiveSheet.ActiveColumnIndex - 1)
                    End If
                Else
                    spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("idFamilia").Index, fila, spVentas.ActiveSheet.Columns("nombreFamilia").Index).Value = String.Empty
                    spVentas.ActiveSheet.ClearSelection()
                    spVentas.ActiveSheet.SetActiveCell(fila, spVentas.ActiveSheet.ActiveColumnIndex - 1)
                End If
            ElseIf (columnaActiva = spVentas.ActiveSheet.Columns("idSubFamilia").Index) Then
                fila = spVentas.ActiveSheet.ActiveRowIndex
                Dim idAlmacen As Integer = ALMLogicaVentas.Funciones.ValidarNumeroACero(spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("idAlmacen").Index).Value)
                Dim idFamilia As Integer = ALMLogicaVentas.Funciones.ValidarNumeroACero(spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("idFamilia").Index).Value)
                Dim idSubFamilia As Integer = ALMLogicaVentas.Funciones.ValidarNumeroACero(spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("idSubFamilia").Index).Value)
                If (idAlmacen > 0 And idFamilia > 0 And idSubFamilia > 0) Then
                    subFamilias.EIdAlmacen = idAlmacen
                    subFamilias.EIdFamilia = idFamilia
                    subFamilias.EId = idSubFamilia
                    Dim datos As New DataTable
                    datos = subFamilias.ObtenerListado()
                    If (datos.Rows.Count > 0) Then
                        spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("nombreSubFamilia").Index).Value = datos.Rows(0).Item("Nombre")
                        spVentas.ActiveSheet.SetActiveCell(fila, spVentas.ActiveSheet.ActiveColumnIndex + 1)
                    Else
                        spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("idSubFamilia").Index, fila, spVentas.ActiveSheet.Columns("nombreSubFamilia").Index).Value = String.Empty
                        spVentas.ActiveSheet.SetActiveCell(fila, spVentas.ActiveSheet.ActiveColumnIndex - 1)
                    End If
                Else
                    spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("idSubFamilia").Index, fila, spVentas.ActiveSheet.Columns("nombreSubFamilia").Index).Value = String.Empty
                    spVentas.ActiveSheet.SetActiveCell(fila, spVentas.ActiveSheet.ActiveColumnIndex - 1)
                End If
            ElseIf (columnaActiva = spVentas.ActiveSheet.Columns("idArticulo").Index) Then
                fila = spVentas.ActiveSheet.ActiveRowIndex
                Dim idAlmacen As Integer = ALMLogicaVentas.Funciones.ValidarNumeroACero(spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("idAlmacen").Index).Value)
                Dim idFamilia As Integer = ALMLogicaVentas.Funciones.ValidarNumeroACero(spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("idFamilia").Index).Value)
                Dim idSubFamilia As Integer = ALMLogicaVentas.Funciones.ValidarNumeroACero(spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("idSubFamilia").Index).Value)
                Dim idArticulo As Integer = ALMLogicaVentas.Funciones.ValidarNumeroACero(spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("idArticulo").Index).Value)
                If (idAlmacen > 0 And idFamilia > 0 And idSubFamilia > 0 And idArticulo > 0) Then
                    articulos.EIdAlmacen = idAlmacen
                    articulos.EIdFamilia = idFamilia
                    articulos.EIdSubFamilia = idSubFamilia
                    articulos.EId = idArticulo
                    For indice = 0 To spVentas.ActiveSheet.Rows.Count - 1 ' Se valida que no se repitan los articulos.
                        Dim idArticuloLocal As Integer = ALMLogicaVentas.Funciones.ValidarNumeroACero(spVentas.ActiveSheet.Cells(indice, spVentas.ActiveSheet.Columns("idArticulo").Index).Text)
                        Dim idSubFamiliaLocal As Integer = ALMLogicaVentas.Funciones.ValidarNumeroACero(spVentas.ActiveSheet.Cells(indice, spVentas.ActiveSheet.Columns("idSubFamilia").Index).Text)
                        Dim idFamiliaLocal As Integer = ALMLogicaVentas.Funciones.ValidarNumeroACero(spVentas.ActiveSheet.Cells(indice, spVentas.ActiveSheet.Columns("idFamilia").Index).Text)
                        If (idArticuloLocal > 0 And idFamiliaLocal > 0 And idSubFamiliaLocal > 0) Then
                            If (idArticuloLocal = idArticulo And idSubFamiliaLocal = idSubFamilia And idFamiliaLocal = idFamilia And indice <> fila) Then
                                spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("idArticulo").Index).Text = String.Empty
                                spVentas.ActiveSheet.ClearRange(fila, spVentas.ActiveSheet.Columns("idArticulo").Index, 1, spVentas.ActiveSheet.Columns.Count - 1, True)
                                spVentas.ActiveSheet.SetActiveCell(fila, spVentas.ActiveSheet.ActiveColumnIndex - 1)
                                Exit Sub
                            End If
                        End If
                    Next
                    Dim datos As New DataTable
                    datos = articulos.ObtenerListado()
                    If (datos.Rows.Count > 0) Then ' Se carga nombre de artículo.
                        spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("nombreArticulo").Index).Value = datos.Rows(0).Item("Nombre")
                        spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("codigo").Index).Value = datos.Rows(0).Item("Codigo")
                        spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("pagina").Index).Value = datos.Rows(0).Item("Pagina")
                        spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("color").Index).Value = datos.Rows(0).Item("Color")
                        spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("talla").Index).Value = datos.Rows(0).Item("Talla")
                        spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("modelo").Index).Value = datos.Rows(0).Item("Modelo")
                        spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("codigoInternet").Index).Value = datos.Rows(0).Item("CodigoInternet")
                        spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("precio").Index).Value = datos.Rows(0).Item("Precio")
                        Dim datos2 As New DataTable
                        unidadesMedidas.EId = datos.Rows(0).Item("IdUnidadMedida")
                        datos2 = unidadesMedidas.ObtenerListado()
                        If (datos2.Rows.Count > 0) Then ' Se carga nombre de unidad.
                            spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("nombreUnidadMedida").Index).Value = datos2.Rows(0).Item("Nombre")
                        End If
                        spVentas.ActiveSheet.SetActiveCell(fila, spVentas.ActiveSheet.ActiveColumnIndex + 2)
                    Else
                        spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("idArticulo").Index, fila, spVentas.ActiveSheet.Columns("nombreUnidadMedida").Index).Value = String.Empty
                        spVentas.ActiveSheet.SetActiveCell(fila, spVentas.ActiveSheet.ActiveColumnIndex - 1)
                    End If
                Else
                    spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("idArticulo").Index, fila, spVentas.ActiveSheet.Columns("nombreUnidadMedida").Index).Value = String.Empty
                    spVentas.ActiveSheet.SetActiveCell(fila, spVentas.ActiveSheet.ActiveColumnIndex - 1)
                End If
            ElseIf (columnaActiva = spVentas.ActiveSheet.Columns("cantidad").Index) Then
                fila = spVentas.ActiveSheet.ActiveRowIndex
                Dim cantidad As Integer = ALMLogicaVentas.Funciones.ValidarNumeroACero(spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("cantidad").Index).Value)
                If (cantidad > 0) Then
                    Dim precio As Double = ALMLogicaVentas.Funciones.ValidarNumeroACero(spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("precio").Index).Text)
                    spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("subtotal").Index).Value = cantidad * precio
                    Dim descuento As Double = ALMLogicaVentas.Funciones.ValidarNumeroACero(spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("descuento").Index).Text)
                    spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("total").Index).Value = (cantidad * precio) - descuento
                Else
                    spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("cantidad").Index).Value = 0
                    spVentas.ActiveSheet.SetActiveCell(fila, spVentas.ActiveSheet.ActiveColumnIndex - 1)
                End If
            ElseIf (columnaActiva = spVentas.ActiveSheet.Columns("precio").Index) Then
                fila = spVentas.ActiveSheet.ActiveRowIndex
                Dim cantidad As Integer = ALMLogicaVentas.Funciones.ValidarNumeroACero(spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("cantidad").Index).Value)
                Dim precio As Double = ALMLogicaVentas.Funciones.ValidarNumeroACero(spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("precio").Index).Value)
                Dim descuento As Double = ALMLogicaVentas.Funciones.ValidarNumeroACero(spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("descuento").Index).Value)
                If (cantidad > 0) Then
                    spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("subtotal").Index).Value = cantidad * precio
                    spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("total").Index).Value = (cantidad * precio) - descuento
                ElseIf (precio = 0) Then
                    spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("precio").Index).Value = 0
                Else
                    spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("precio").Index).Value = 0
                    spVentas.ActiveSheet.SetActiveCell(fila, spVentas.ActiveSheet.ActiveColumnIndex - 1)
                End If
            ElseIf (columnaActiva = spVentas.ActiveSheet.Columns("subtotal").Index) Then
                fila = spVentas.ActiveSheet.ActiveRowIndex
                Dim cantidad As Integer = ALMLogicaVentas.Funciones.ValidarNumeroACero(spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("cantidad").Index).Value)
                Dim subtotal As Double = ALMLogicaVentas.Funciones.ValidarNumeroACero(spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("subtotal").Index).Value)
                If (cantidad > 0) Then
                    spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("precio").Index).Value = subtotal / cantidad
                Else
                    spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("subtotal").Index).Value = 0
                    spVentas.ActiveSheet.SetActiveCell(fila, spVentas.ActiveSheet.ActiveColumnIndex - 1)
                End If
            ElseIf (columnaActiva = spVentas.ActiveSheet.Columns("porcentajeDescuento").Index) Then
                fila = spVentas.ActiveSheet.ActiveRowIndex
                Dim porcentajeDescuento As Double = ALMLogicaVentas.Funciones.ValidarNumeroACero(spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("porcentajeDescuento").Index).Value)
                Dim subtotal As Double = ALMLogicaVentas.Funciones.ValidarNumeroACero(spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("subtotal").Index).Value)
                If (porcentajeDescuento >= 0 And porcentajeDescuento <= 100) Then
                    If (porcentajeDescuento = 0) Then
                        spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("porcentajeDescuento").Index).Value = 0
                    End If
                    Dim descuento As Double = (subtotal / 100) * porcentajeDescuento
                    Dim total As Double = subtotal - descuento
                    If (total >= 0) Then
                        spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("descuento").Index).Value = descuento
                        spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("total").Index).Value = total
                    Else
                        spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("porcentajeDescuento").Index).Value = 0
                        spVentas.ActiveSheet.SetActiveCell(fila, spVentas.ActiveSheet.ActiveColumnIndex - 1)
                    End If
                Else
                    spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("porcentajeDescuento").Index).Value = 0
                End If
            ElseIf (columnaActiva = spVentas.ActiveSheet.Columns("descuento").Index) Then
                fila = spVentas.ActiveSheet.ActiveRowIndex
                Dim descuento As Double = ALMLogicaVentas.Funciones.ValidarNumeroACero(spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("descuento").Index).Value)
                Dim subtotal As Double = ALMLogicaVentas.Funciones.ValidarNumeroACero(spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("subtotal").Index).Value)
                If (descuento > 0) Then
                    Dim porcentajeDescuento As Double = (descuento / subtotal) * 100
                    Dim total As Double = subtotal - descuento
                    If (total >= 0) Then
                        spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("porcentajeDescuento").Index).Value = porcentajeDescuento
                        spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("total").Index).Value = total
                    Else
                        spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("descuento").Index).Value = 0
                        spVentas.ActiveSheet.SetActiveCell(fila, spVentas.ActiveSheet.ActiveColumnIndex - 1)
                    End If
                Else
                    spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("descuento").Index).Value = 0
                End If
            End If
            CalcularTotalesVentas()
        ElseIf (spread.Name = spVales.Name) Then
            Dim fila As Integer = 0
            If (columnaActiva = spVales.ActiveSheet.Columns("id").Index) Then
                fila = spVales.ActiveSheet.ActiveRowIndex
                Dim idOrigen As Integer = ALMLogicaVentas.Funciones.ValidarNumeroACero(spVales.ActiveSheet.Cells(fila, spVales.ActiveSheet.Columns("idOrigen").Index).Value)
                Dim id As Integer = ALMLogicaVentas.Funciones.ValidarNumeroACero(spVales.ActiveSheet.Cells(fila, spVales.ActiveSheet.Columns("id").Index).Value)
                Dim idCliente As Integer = ALMLogicaVentas.Funciones.ValidarNumeroACero(cbClientes.SelectedValue)
                If (idOrigen > 0 AndAlso id > 0 AndAlso idCliente > 0) Then
                    vales.EIdOrigen = idOrigen
                    vales.EId = id
                    vales.EIdCliente = idCliente
                    Dim datos As New DataTable
                    datos = vales.ObtenerListado()
                    If (datos.Rows.Count > 0) Then
                        Dim total As Double = ALMLogicaVentas.Funciones.ValidarNumeroACero(datos.Rows(0).Item("Total").ToString())
                        If (total > 0) Then
                            spVales.ActiveSheet.Cells(fila, spVales.ActiveSheet.Columns("total").Index).Value = total
                            Dim sumaTotalVales As Double = CalcularTotalesVales(False)
                            Dim sumaTotalVenta As Double = ALMLogicaVentas.Funciones.ValidarNumeroACero(txtTotal.Text)
                            Dim diferencia As Double = 0
                            If (sumaTotalVales > sumaTotalVenta) Then ' Si la suma de los vales es mayor a la suma de la venta.
                                diferencia = sumaTotalVales - sumaTotalVenta
                                spVales.ActiveSheet.Cells(fila, spVales.ActiveSheet.Columns("totalVenta").Index).Value = total - diferencia
                            Else ' Si es menor o igual.
                                spVales.ActiveSheet.Cells(fila, spVales.ActiveSheet.Columns("totalVenta").Index).Value = total
                            End If
                            CalcularTotalesVales(True)
                            CalcularCambio()
                            spVales.ActiveSheet.SetActiveCell(fila, spVales.ActiveSheet.ActiveColumnIndex + 1)
                        Else
                            Try
                                spVales.ActiveSheet.Cells(fila, spVales.ActiveSheet.Columns("idOrigen").Index, fila, spVales.ActiveSheet.Columns("total").Index).Value = String.Empty
                            Catch
                            End Try
                            spVales.ActiveSheet.SetActiveCell(fila, spVales.ActiveSheet.ActiveColumnIndex - 1)
                        End If
                    Else
                        spVales.ActiveSheet.Cells(fila, spVales.ActiveSheet.Columns("idOrigen").Index, fila, spVales.ActiveSheet.Columns("total").Index).Value = String.Empty
                        spVales.ActiveSheet.SetActiveCell(fila, spVales.ActiveSheet.ActiveColumnIndex - 1)
                    End If
                Else
                    spVales.ActiveSheet.Cells(fila, spVales.ActiveSheet.Columns("idOrigen").Index, fila, spVales.ActiveSheet.Columns("total").Index).Value = String.Empty
                    spVales.ActiveSheet.ClearSelection()
                    spVales.ActiveSheet.SetActiveCell(fila, spVales.ActiveSheet.ActiveColumnIndex - 1)
                End If
            End If
        End If

    End Sub

    Private Function CalcularTotalesVales(ByVal esTotalVenta As Boolean) As Double

        Dim total As Double = 0
        For fila = 0 To spVales.ActiveSheet.Rows.Count - 1
            If (Not esTotalVenta) Then
                total += ALMLogicaVentas.Funciones.ValidarNumeroACero(spVales.ActiveSheet.Cells(fila, spVales.ActiveSheet.Columns("total").Index).Value)
            Else
                Dim totalVenta As Double = ALMLogicaVentas.Funciones.ValidarNumeroACero(spVales.ActiveSheet.Cells(fila, spVales.ActiveSheet.Columns("totalVenta").Index).Value)
                If (fila = 0) Then
                    txtImporteVale1.Text = totalVenta.ToString("###,###.00")
                ElseIf (fila = 1) Then
                    txtImporteVale2.Text = totalVenta.ToString("###,###.00")
                End If
                total += totalVenta
            End If
        Next
        Return total

    End Function

    Private Sub CalcularTotalesVentas()

        Dim subTotal As Double = 0
        Dim descuento As Double = 0
        Dim total As Double = 0
        Dim importeVale1 As Double = ALMLogicaVentas.Funciones.ValidarNumeroACero(txtImporteVale1.Text)
        Dim importeVale2 As Double = ALMLogicaVentas.Funciones.ValidarNumeroACero(txtImporteVale2.Text)
        Dim importePagado As Double = ALMLogicaVentas.Funciones.ValidarNumeroACero(txtImportePagado.Text)
        For fila = 0 To spVentas.ActiveSheet.Rows.Count - 1
            subTotal += ALMLogicaVentas.Funciones.ValidarNumeroACero(spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("subtotal").Index).Value)
            descuento += ALMLogicaVentas.Funciones.ValidarNumeroACero(spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("descuento").Index).Value)
            total += ALMLogicaVentas.Funciones.ValidarNumeroACero(spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("total").Index).Value)
        Next
        txtSubTotal.Text = subTotal.ToString("###,###.00")
        txtDescuento.Text = descuento.ToString("###,###.00")
        txtTotal.Text = total.ToString("###,###.00")
        txtImporteCambio.Text = ((importeVale1 + importeVale2 + importePagado) - total).ToString("###,###.00")

    End Sub

    Private Sub CalcularCambio()

        Dim importeVale1 As Double = ALMLogicaVentas.Funciones.ValidarNumeroACero(txtImporteVale1.Text)
        Dim importeVale2 As Double = ALMLogicaVentas.Funciones.ValidarNumeroACero(txtImporteVale2.Text)
        Dim importePagado As Double = ALMLogicaVentas.Funciones.ValidarNumeroACero(txtImportePagado.Text)
        Dim total As Double = ALMLogicaVentas.Funciones.ValidarNumeroACero(txtTotal.Text)
        If (importePagado > 0) Then
            txtImportePagado.Text = importePagado.ToString("###,###.00")
        Else
            txtImportePagado.Text = Math.Abs(((importeVale1 + importeVale2) - total)).ToString("###,###.00")
            importePagado = ALMLogicaVentas.Funciones.ValidarNumeroACero(txtImportePagado.Text)
        End If
        Dim importeCambio As Double = (importeVale1 + importeVale2 + importePagado) - total
        txtImporteCambio.Text = importeCambio.ToString("###,###.00")
        If (importeCambio < 0) Then ' No es válido.
            txtImporteCambio.BackColor = Principal.colorAdvertencia
        End If

    End Sub

    Private Sub CargarIdConsecutivo()

        Dim idMaximo As Integer = ventas.ObtenerMaximoId()
        txtId.Text = idMaximo

    End Sub

    Private Sub CargarDatosEnSpreadDeCatalogos(ByVal filaCatalogos As Integer)

        If (spVentas.ActiveSheet.ActiveColumnIndex = spVentas.ActiveSheet.Columns("idAlmacen").Index Or spVentas.ActiveSheet.ActiveColumnIndex = spVentas.ActiveSheet.Columns("nombreAlmacen").Index) Then
            spVentas.ActiveSheet.Cells(spVentas.ActiveSheet.ActiveRowIndex, spVentas.ActiveSheet.Columns("idAlmacen").Index).Text = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("id").Index).Text
            spVentas.ActiveSheet.Cells(spVentas.ActiveSheet.ActiveRowIndex, spVentas.ActiveSheet.Columns("nombreAlmacen").Index).Text = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("nombre").Index).Text
        ElseIf (spVentas.ActiveSheet.ActiveColumnIndex = spVentas.ActiveSheet.Columns("idFamilia").Index Or spVentas.ActiveSheet.ActiveColumnIndex = spVentas.ActiveSheet.Columns("nombreFamilia").Index) Then
            spVentas.ActiveSheet.Cells(spVentas.ActiveSheet.ActiveRowIndex, spVentas.ActiveSheet.Columns("idFamilia").Index).Text = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("id").Index).Text
            spVentas.ActiveSheet.Cells(spVentas.ActiveSheet.ActiveRowIndex, spVentas.ActiveSheet.Columns("nombreFamilia").Index).Text = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("nombre").Index).Text
        ElseIf (spVentas.ActiveSheet.ActiveColumnIndex = spVentas.ActiveSheet.Columns("idSubFamilia").Index Or spVentas.ActiveSheet.ActiveColumnIndex = spVentas.ActiveSheet.Columns("nombreSubFamilia").Index) Then
            spVentas.ActiveSheet.Cells(spVentas.ActiveSheet.ActiveRowIndex, spVentas.ActiveSheet.Columns("idSubFamilia").Index).Text = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("id").Index).Text
            spVentas.ActiveSheet.Cells(spVentas.ActiveSheet.ActiveRowIndex, spVentas.ActiveSheet.Columns("nombreSubFamilia").Index).Text = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("nombre").Index).Text
        ElseIf (spVentas.ActiveSheet.ActiveColumnIndex = spVentas.ActiveSheet.Columns("idArticulo").Index Or spVentas.ActiveSheet.ActiveColumnIndex = spVentas.ActiveSheet.Columns("nombreArticulo").Index Or spVentas.ActiveSheet.ActiveColumnIndex = spVentas.ActiveSheet.Columns("nombreUnidadMedida").Index Or spVentas.ActiveSheet.ActiveColumnIndex = spVentas.ActiveSheet.Columns("codigo").Index Or spVentas.ActiveSheet.ActiveColumnIndex = spVentas.ActiveSheet.Columns("pagina").Index Or spVentas.ActiveSheet.ActiveColumnIndex = spVentas.ActiveSheet.Columns("color").Index Or spVentas.ActiveSheet.ActiveColumnIndex = spVentas.ActiveSheet.Columns("talla").Index Or spVentas.ActiveSheet.ActiveColumnIndex = spVentas.ActiveSheet.Columns("modelo").Index Or spVentas.ActiveSheet.ActiveColumnIndex = spVentas.ActiveSheet.Columns("codigoInternet").Index) Then
            spVentas.ActiveSheet.Cells(spVentas.ActiveSheet.ActiveRowIndex, spVentas.ActiveSheet.Columns("idAlmacen").Index).Text = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("idAlmacen").Index).Text
            spVentas.ActiveSheet.Cells(spVentas.ActiveSheet.ActiveRowIndex, spVentas.ActiveSheet.Columns("nombreAlmacen").Index).Text = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("nombreAlmacen").Index).Text
            spVentas.ActiveSheet.Cells(spVentas.ActiveSheet.ActiveRowIndex, spVentas.ActiveSheet.Columns("idFamilia").Index).Text = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("idFamilia").Index).Text
            spVentas.ActiveSheet.Cells(spVentas.ActiveSheet.ActiveRowIndex, spVentas.ActiveSheet.Columns("nombreFamilia").Index).Text = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("nombreFamilia").Index).Text
            spVentas.ActiveSheet.Cells(spVentas.ActiveSheet.ActiveRowIndex, spVentas.ActiveSheet.Columns("idSubFamilia").Index).Text = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("idSubFamilia").Index).Text
            spVentas.ActiveSheet.Cells(spVentas.ActiveSheet.ActiveRowIndex, spVentas.ActiveSheet.Columns("nombreSubFamilia").Index).Text = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("nombreSubFamilia").Index).Text
            spVentas.ActiveSheet.Cells(spVentas.ActiveSheet.ActiveRowIndex, spVentas.ActiveSheet.Columns("idArticulo").Index).Text = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("id").Index).Text
            spVentas.ActiveSheet.Cells(spVentas.ActiveSheet.ActiveRowIndex, spVentas.ActiveSheet.Columns("nombreArticulo").Index).Text = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("nombre").Index).Text
            spVentas.ActiveSheet.Cells(spVentas.ActiveSheet.ActiveRowIndex, spVentas.ActiveSheet.Columns("nombreUnidadMedida").Index).Text = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("unidadMedida").Index).Text
            spVentas.ActiveSheet.Cells(spVentas.ActiveSheet.ActiveRowIndex, spVentas.ActiveSheet.Columns("codigo").Index).Text = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("codigo").Index).Text
            spVentas.ActiveSheet.Cells(spVentas.ActiveSheet.ActiveRowIndex, spVentas.ActiveSheet.Columns("pagina").Index).Text = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("pagina").Index).Text
            spVentas.ActiveSheet.Cells(spVentas.ActiveSheet.ActiveRowIndex, spVentas.ActiveSheet.Columns("color").Index).Text = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("color").Index).Text
            spVentas.ActiveSheet.Cells(spVentas.ActiveSheet.ActiveRowIndex, spVentas.ActiveSheet.Columns("talla").Index).Text = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("talla").Index).Text
            spVentas.ActiveSheet.Cells(spVentas.ActiveSheet.ActiveRowIndex, spVentas.ActiveSheet.Columns("modelo").Index).Text = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("modelo").Index).Text
            spVentas.ActiveSheet.Cells(spVentas.ActiveSheet.ActiveRowIndex, spVentas.ActiveSheet.Columns("codigoInternet").Index).Text = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("codigoInternet").Index).Text
            spVentas.ActiveSheet.Cells(spVentas.ActiveSheet.ActiveRowIndex, spVentas.ActiveSheet.Columns("precio").Index).Text = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("precio").Index).Text
        End If

    End Sub

    Private Sub CargarDatosEnOtrosDeCatalogos(ByVal filaCatalogos As Integer)

        If (Me.opcionCatalogoSeleccionada = OpcionCatalogo.cliente) Then
            cbClientes.SelectedValue = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("id").Index).Text
        End If

    End Sub

    Private Sub CargarCatalogoEnSpread()

        spVentas.Enabled = False
        Dim columna As Integer = spVentas.ActiveSheet.ActiveColumnIndex
        If ((columna = spVentas.ActiveSheet.Columns("idAlmacen").Index) Or (columna = spVentas.ActiveSheet.Columns("nombreAlmacen").Index)) Then
            Me.opcionCatalogoSeleccionada = OpcionCatalogo.almacen
            almacenes.EId = 0
            Dim datos As New DataTable
            datos = almacenes.ObtenerListadoCatalogos()
            If (datos.Rows.Count > 0) Then
                'spCatalogos.Reset() ' TODO. Pendiente ver porqué de repente desaparecen los datos.
                spCatalogos.ActiveSheet.DataSource = datos
            Else
                spCatalogos.ActiveSheet.DataSource = Nothing
                spCatalogos.ActiveSheet.Rows.Count = 0
                spVentas.Enabled = True
            End If
            FormatearSpreadCatalogos(OpcionPosicion.derecha)
        ElseIf ((columna = spVentas.ActiveSheet.Columns("idFamilia").Index) Or (columna = spVentas.ActiveSheet.Columns("nombreFamilia").Index)) Then
            Me.opcionCatalogoSeleccionada = OpcionCatalogo.familia
            Dim idAlmacen As Integer = ALMLogicaVentas.Funciones.ValidarNumeroACero(spVentas.ActiveSheet.Cells(spVentas.ActiveSheet.ActiveRowIndex, spVentas.ActiveSheet.Columns("idAlmacen").Index).Text)
            If (idAlmacen > 0) Then
                familias.EIdAlmacen = idAlmacen
                familias.EId = 0
                Dim datos As New DataTable
                datos = familias.ObtenerListadoCatalogos()
                If (datos.Rows.Count > 0) Then
                    spCatalogos.ActiveSheet.DataSource = datos
                Else
                    spCatalogos.ActiveSheet.DataSource = Nothing
                    spCatalogos.ActiveSheet.Rows.Count = 0
                    spVentas.Enabled = True
                End If
            Else
                spCatalogos.ActiveSheet.DataSource = Nothing
                spCatalogos.ActiveSheet.Rows.Count = 0
                spVentas.Enabled = True
            End If
            FormatearSpreadCatalogos(OpcionPosicion.derecha)
        ElseIf ((columna = spVentas.ActiveSheet.Columns("idSubFamilia").Index) Or (columna = spVentas.ActiveSheet.Columns("nombreSubFamilia").Index)) Then
            Me.opcionCatalogoSeleccionada = OpcionCatalogo.subfamilia
            Dim idAlmacen As Integer = ALMLogicaVentas.Funciones.ValidarNumeroACero(spVentas.ActiveSheet.Cells(spVentas.ActiveSheet.ActiveRowIndex, spVentas.ActiveSheet.Columns("idAlmacen").Index).Text)
            Dim idFamilia As Integer = ALMLogicaVentas.Funciones.ValidarNumeroACero(spVentas.ActiveSheet.Cells(spVentas.ActiveSheet.ActiveRowIndex, spVentas.ActiveSheet.Columns("idFamilia").Index).Text)
            If (idAlmacen > 0 And idFamilia > 0) Then
                subFamilias.EIdAlmacen = idAlmacen
                subFamilias.EIdFamilia = idFamilia
                subFamilias.EId = 0
                Dim datos As New DataTable
                datos = subFamilias.ObtenerListadoCatalogos()
                If (datos.Rows.Count > 0) Then
                    spCatalogos.ActiveSheet.DataSource = datos
                Else
                    spCatalogos.ActiveSheet.DataSource = Nothing
                    spCatalogos.ActiveSheet.Rows.Count = 0
                    spVentas.Enabled = True
                End If
            Else
                spCatalogos.ActiveSheet.DataSource = Nothing
                spCatalogos.ActiveSheet.Rows.Count = 0
                spVentas.Enabled = True
            End If
            FormatearSpreadCatalogos(OpcionPosicion.derecha)
        ElseIf ((columna = spVentas.ActiveSheet.Columns("idArticulo").Index) Or (columna = spVentas.ActiveSheet.Columns("nombreArticulo").Index) Or (columna = spVentas.ActiveSheet.Columns("nombreUnidadMedida").Index) Or (columna = spVentas.ActiveSheet.Columns("codigo").Index) Or (columna = spVentas.ActiveSheet.Columns("pagina").Index) Or (columna = spVentas.ActiveSheet.Columns("color").Index) Or (columna = spVentas.ActiveSheet.Columns("talla").Index) Or (columna = spVentas.ActiveSheet.Columns("modelo").Index) Or (columna = spVentas.ActiveSheet.Columns("codigoInternet").Index)) Then
            Me.opcionCatalogoSeleccionada = OpcionCatalogo.articulo
            Dim idAlmacen As Integer = ALMLogicaVentas.Funciones.ValidarNumeroACero(spVentas.ActiveSheet.Cells(spVentas.ActiveSheet.ActiveRowIndex, spVentas.ActiveSheet.Columns("idAlmacen").Index).Text)
            Dim idFamilia As Integer = ALMLogicaVentas.Funciones.ValidarNumeroACero(spVentas.ActiveSheet.Cells(spVentas.ActiveSheet.ActiveRowIndex, spVentas.ActiveSheet.Columns("idFamilia").Index).Text)
            Dim idSubFamilia As Integer = ALMLogicaVentas.Funciones.ValidarNumeroACero(spVentas.ActiveSheet.Cells(spVentas.ActiveSheet.ActiveRowIndex, spVentas.ActiveSheet.Columns("idSubFamilia").Index).Text)
            'If (idAlmacen > 0 And idFamilia > 0 And idSubFamilia > 0) Then
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
                spVentas.Enabled = True
            End If
            'Else
            '    spCatalogos.ActiveSheet.DataSource = Nothing
            '    spCatalogos.ActiveSheet.Rows.Count = 0
            '    spVentas.Enabled = True
            'End If
            FormatearSpreadCatalogos(OpcionPosicion.derecha)
        Else
            spVentas.Enabled = True
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
        pnlCatalogos.Height = Me.altoTotal
        pnlCatalogos.Width = spCatalogos.Width
        spCatalogos.Width = pnlCatalogos.Width
        spCatalogos.Height = pnlCatalogos.Height - txtBuscarCatalogo.Height - 5
        pnlCatalogos.BringToFront()
        pnlCatalogos.Visible = True
        pnlCatalogos.Refresh()

    End Sub

    Private Sub FormatearSpreadAnchoColumnasArticulos()

        spCatalogos.ActiveSheet.Columns("idAlmacen").Width = 70
        spCatalogos.ActiveSheet.Columns("nombreAlmacen").Width = 200
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
        spVentas.Enabled = True
        If (Me.opcionCatalogoSeleccionada = OpcionCatalogo.cliente) Then
            AsignarFoco(cbClientes)
        Else
            AsignarFoco(spVentas)
        End If
        txtBuscarCatalogo.Clear()
        pnlCatalogos.Visible = False

    End Sub

    Private Sub CargarVentas()

        Me.Cursor = Cursors.WaitCursor
        Dim id As Integer = ALMLogicaVentas.Funciones.ValidarNumeroACero(txtId.Text)
        If (id > 0) Then
            ventas.EId = id
            ventasDetalle.EId = id
            Dim datos As New DataTable
            datos = ventas.ObtenerListadoGeneral()
            If (datos.Rows.Count > 0) Then
                dtpFecha.Value = datos.Rows(0).Item("Fecha")
                cbClientes.SelectedValue = datos.Rows(0).Item("IdCliente")
                cbMetodosPagos.SelectedValue = datos.Rows(0).Item("IdMetodoPago")
                txtImportePagado.Text = ALMLogicaVentas.Funciones.ValidarNumeroACero(datos.Rows(0).Item("ImportePagado").ToString).ToString("###,###.00")
                txtImporteCambio.Text = ALMLogicaVentas.Funciones.ValidarNumeroACero(datos.Rows(0).Item("ImporteCambio").ToString).ToString("###,###.00")
                spVentas.ActiveSheet.DataSource = ventasDetalle.ObtenerListadoDetallado()
                Me.cantidadFilas = spVentas.ActiveSheet.Rows.Count + 1
                FormatearSpreadVentas()
                CalcularTotalesVentas()
                CargarVales()
                CalcularTotalesVales(True)
                CalcularCambio()
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

    Private Sub FormatearSpreadVentas()

        spVentas.ActiveSheet.ColumnHeader.RowCount = 2
        spVentas.ActiveSheet.ColumnHeader.Rows(0, spVentas.ActiveSheet.ColumnHeader.Rows.Count - 1).Font = New Font(Principal.tipoLetraSpread, Principal.tamañoLetraSpread, FontStyle.Bold)
        spVentas.ActiveSheet.ColumnHeader.Rows(0).Height = Principal.alturaFilasEncabezadosChicosSpread
        spVentas.ActiveSheet.ColumnHeader.Rows(1).Height = Principal.alturaFilasEncabezadosMedianosSpread
        spVentas.ActiveSheet.OperationMode = FarPoint.Win.Spread.OperationMode.Normal
        spVentas.ActiveSheet.Rows.Count = Me.cantidadFilas
        ControlarSpreadEnterASiguienteColumna(spVentas)
        Dim numeracion As Integer = 0
        spVentas.ActiveSheet.Columns(numeracion).Tag = "esCapturado" : numeracion += 1
        spVentas.ActiveSheet.Columns(numeracion).Tag = "idAlmacen" : numeracion += 1
        spVentas.ActiveSheet.Columns(numeracion).Tag = "nombreAlmacen" : numeracion += 1
        spVentas.ActiveSheet.Columns(numeracion).Tag = "idFamilia" : numeracion += 1
        spVentas.ActiveSheet.Columns(numeracion).Tag = "nombreFamilia" : numeracion += 1
        spVentas.ActiveSheet.Columns(numeracion).Tag = "idSubFamilia" : numeracion += 1
        spVentas.ActiveSheet.Columns(numeracion).Tag = "nombreSubFamilia" : numeracion += 1
        spVentas.ActiveSheet.Columns(numeracion).Tag = "idArticulo" : numeracion += 1
        spVentas.ActiveSheet.Columns(numeracion).Tag = "nombreArticulo" : numeracion += 1
        spVentas.ActiveSheet.Columns(numeracion).Tag = "nombreUnidadMedida" : numeracion += 1
        spVentas.ActiveSheet.Columns(numeracion).Tag = "codigo" : numeracion += 1
        spVentas.ActiveSheet.Columns(numeracion).Tag = "pagina" : numeracion += 1
        spVentas.ActiveSheet.Columns(numeracion).Tag = "color" : numeracion += 1
        spVentas.ActiveSheet.Columns(numeracion).Tag = "talla" : numeracion += 1
        spVentas.ActiveSheet.Columns(numeracion).Tag = "modelo" : numeracion += 1
        spVentas.ActiveSheet.Columns(numeracion).Tag = "codigoInternet" : numeracion += 1
        spVentas.ActiveSheet.Columns(numeracion).Tag = "cantidad" : numeracion += 1
        spVentas.ActiveSheet.Columns(numeracion).Tag = "precio" : numeracion += 1
        spVentas.ActiveSheet.Columns(numeracion).Tag = "subtotal" : numeracion += 1
        spVentas.ActiveSheet.Columns(numeracion).Tag = "porcentajeDescuento" : numeracion += 1
        spVentas.ActiveSheet.Columns(numeracion).Tag = "descuento" : numeracion += 1
        spVentas.ActiveSheet.Columns(numeracion).Tag = "total" : numeracion += 1
        spVentas.ActiveSheet.Columns.Count = numeracion
        spVentas.ActiveSheet.Columns("idAlmacen").Width = 50
        spVentas.ActiveSheet.Columns("nombreAlmacen").Width = 140
        spVentas.ActiveSheet.Columns("idFamilia").Width = 50
        spVentas.ActiveSheet.Columns("nombreFamilia").Width = 140
        spVentas.ActiveSheet.Columns("idSubFamilia").Width = 50
        spVentas.ActiveSheet.Columns("nombreSubFamilia").Width = 140
        spVentas.ActiveSheet.Columns("idArticulo").Width = 50
        spVentas.ActiveSheet.Columns("nombreArticulo").Width = 150
        spVentas.ActiveSheet.Columns("nombreUnidadMedida").Width = 70
        spVentas.ActiveSheet.Columns("codigo").Width = 110
        spVentas.ActiveSheet.Columns("pagina").Width = 70
        spVentas.ActiveSheet.Columns("color").Width = 100
        spVentas.ActiveSheet.Columns("talla").Width = 70
        spVentas.ActiveSheet.Columns("modelo").Width = 80
        spVentas.ActiveSheet.Columns("codigoInternet").Width = 80
        spVentas.ActiveSheet.Columns("cantidad").Width = 85
        spVentas.ActiveSheet.Columns("precio").Width = 70
        spVentas.ActiveSheet.Columns("subtotal").Width = 90
        spVentas.ActiveSheet.Columns("porcentajeDescuento").Width = 100
        spVentas.ActiveSheet.Columns("descuento").Width = 100
        spVentas.ActiveSheet.Columns("total").Width = 75
        spVentas.ActiveSheet.Columns("idAlmacen").CellType = tipoEntero
        spVentas.ActiveSheet.Columns("nombreAlmacen").CellType = tipoTexto
        spVentas.ActiveSheet.Columns("idFamilia").CellType = tipoEntero
        spVentas.ActiveSheet.Columns("nombreFamilia").CellType = tipoTexto
        spVentas.ActiveSheet.Columns("idSubFamilia").CellType = tipoEntero
        spVentas.ActiveSheet.Columns("nombreSubFamilia").CellType = tipoTexto
        spVentas.ActiveSheet.Columns("idArticulo").CellType = tipoEntero
        spVentas.ActiveSheet.Columns("nombreArticulo").CellType = tipoTexto
        spVentas.ActiveSheet.Columns("nombreUnidadMedida").CellType = tipoTexto
        spVentas.ActiveSheet.Columns("codigo").CellType = tipoTexto
        spVentas.ActiveSheet.Columns("pagina").CellType = tipoEntero
        spVentas.ActiveSheet.Columns("color").CellType = tipoTexto
        spVentas.ActiveSheet.Columns("talla").CellType = tipoTexto
        spVentas.ActiveSheet.Columns("modelo").CellType = tipoTexto
        spVentas.ActiveSheet.Columns("codigoInternet").CellType = tipoTexto
        spVentas.ActiveSheet.Columns("cantidad").CellType = tipoEntero
        spVentas.ActiveSheet.Columns("precio").CellType = tipoDoble
        spVentas.ActiveSheet.Columns("subtotal").CellType = tipoDoble
        spVentas.ActiveSheet.Columns("porcentajeDescuento").CellType = tipoDoble
        spVentas.ActiveSheet.Columns("descuento").CellType = tipoDoble
        spVentas.ActiveSheet.Columns("total").CellType = tipoDoble
        spVentas.ActiveSheet.AddColumnHeaderSpanCell(0, spVentas.ActiveSheet.Columns("idAlmacen").Index, 1, 2)
        spVentas.ActiveSheet.ColumnHeader.Cells(0, spVentas.ActiveSheet.Columns("idAlmacen").Index).Value = "A l m a c é n".ToUpper()
        spVentas.ActiveSheet.ColumnHeader.Cells(1, spVentas.ActiveSheet.Columns("idAlmacen").Index).Value = "No. *".ToUpper()
        spVentas.ActiveSheet.ColumnHeader.Cells(1, spVentas.ActiveSheet.Columns("nombreAlmacen").Index).Value = "Nombre *".ToUpper()
        spVentas.ActiveSheet.AddColumnHeaderSpanCell(0, spVentas.ActiveSheet.Columns("idFamilia").Index, 1, 2)
        spVentas.ActiveSheet.ColumnHeader.Cells(0, spVentas.ActiveSheet.Columns("idFamilia").Index).Value = "F a m i l i a".ToUpper()
        spVentas.ActiveSheet.ColumnHeader.Cells(1, spVentas.ActiveSheet.Columns("idFamilia").Index).Value = "No. *".ToUpper()
        spVentas.ActiveSheet.ColumnHeader.Cells(1, spVentas.ActiveSheet.Columns("nombreFamilia").Index).Value = "Nombre *".ToUpper()
        spVentas.ActiveSheet.AddColumnHeaderSpanCell(0, spVentas.ActiveSheet.Columns("idSubFamilia").Index, 1, 2)
        spVentas.ActiveSheet.ColumnHeader.Cells(0, spVentas.ActiveSheet.Columns("idSubFamilia").Index).Value = "S u b F a m i l i a".ToUpper()
        spVentas.ActiveSheet.ColumnHeader.Cells(1, spVentas.ActiveSheet.Columns("idSubFamilia").Index).Value = "No. *".ToUpper()
        spVentas.ActiveSheet.ColumnHeader.Cells(1, spVentas.ActiveSheet.Columns("nombreSubFamilia").Index).Value = "Nombre *".ToUpper()
        spVentas.ActiveSheet.AddColumnHeaderSpanCell(0, spVentas.ActiveSheet.Columns("idArticulo").Index, 1, 9)
        spVentas.ActiveSheet.ColumnHeader.Cells(0, spVentas.ActiveSheet.Columns("idArticulo").Index).Value = "A r t í c u l o".ToUpper()
        spVentas.ActiveSheet.ColumnHeader.Cells(1, spVentas.ActiveSheet.Columns("idArticulo").Index).Value = "No. *".ToUpper()
        spVentas.ActiveSheet.ColumnHeader.Cells(1, spVentas.ActiveSheet.Columns("nombreArticulo").Index).Value = "Nombre *".ToUpper()
        spVentas.ActiveSheet.ColumnHeader.Cells(1, spVentas.ActiveSheet.Columns("nombreUnidadMedida").Index).Value = "Unidad *".ToUpper()
        spVentas.ActiveSheet.ColumnHeader.Cells(1, spVentas.ActiveSheet.Columns("codigo").Index).Value = "Código *".ToUpper()
        spVentas.ActiveSheet.ColumnHeader.Cells(1, spVentas.ActiveSheet.Columns("pagina").Index).Value = "Página *".ToUpper()
        spVentas.ActiveSheet.ColumnHeader.Cells(1, spVentas.ActiveSheet.Columns("color").Index).Value = "Color *".ToUpper()
        spVentas.ActiveSheet.ColumnHeader.Cells(1, spVentas.ActiveSheet.Columns("talla").Index).Value = "Talla *".ToUpper()
        spVentas.ActiveSheet.ColumnHeader.Cells(1, spVentas.ActiveSheet.Columns("modelo").Index).Value = "Modelo *".ToUpper()
        spVentas.ActiveSheet.ColumnHeader.Cells(1, spVentas.ActiveSheet.Columns("codigoInternet").Index).Value = "Codigo Internet *".ToUpper()
        spVentas.ActiveSheet.AddColumnHeaderSpanCell(0, spVentas.ActiveSheet.Columns("cantidad").Index, 2, 1)
        spVentas.ActiveSheet.ColumnHeader.Cells(0, spVentas.ActiveSheet.Columns("cantidad").Index).Value = "Cantidad *".ToUpper()
        spVentas.ActiveSheet.AddColumnHeaderSpanCell(0, spVentas.ActiveSheet.Columns("precio").Index, 2, 1)
        spVentas.ActiveSheet.ColumnHeader.Cells(0, spVentas.ActiveSheet.Columns("precio").Index).Value = "Precio *".ToUpper()
        spVentas.ActiveSheet.AddColumnHeaderSpanCell(0, spVentas.ActiveSheet.Columns("subtotal").Index, 2, 1)
        spVentas.ActiveSheet.ColumnHeader.Cells(0, spVentas.ActiveSheet.Columns("subtotal").Index).Value = "SubTotal *".ToUpper()
        spVentas.ActiveSheet.AddColumnHeaderSpanCell(0, spVentas.ActiveSheet.Columns("porcentajeDescuento").Index, 2, 1)
        spVentas.ActiveSheet.ColumnHeader.Cells(0, spVentas.ActiveSheet.Columns("porcentajeDescuento").Index).Value = "Porcentaje Descuento *".ToUpper()
        spVentas.ActiveSheet.AddColumnHeaderSpanCell(0, spVentas.ActiveSheet.Columns("descuento").Index, 2, 1)
        spVentas.ActiveSheet.ColumnHeader.Cells(0, spVentas.ActiveSheet.Columns("descuento").Index).Value = "Descuento *".ToUpper()
        spVentas.ActiveSheet.AddColumnHeaderSpanCell(0, spVentas.ActiveSheet.Columns("total").Index, 2, 1)
        spVentas.ActiveSheet.ColumnHeader.Cells(0, spVentas.ActiveSheet.Columns("total").Index).Value = "Total *".ToUpper()
        spVentas.ActiveSheet.Columns(spVentas.ActiveSheet.Columns("esCapturado").Index).Visible = False
        MostrarOcultarColumnasDetalle(False)
        spVentas.Refresh()

    End Sub

    Private Sub MostrarOcultarColumnasDetalle(ByVal valor As Boolean)

        spVentas.ActiveSheet.Columns(spVentas.ActiveSheet.Columns("idAlmacen").Index, spVentas.ActiveSheet.Columns("nombreSubFamilia").Index).Visible = valor
        spVentas.ActiveSheet.Columns(spVentas.ActiveSheet.Columns("nombreUnidadMedida").Index).Visible = valor
        spVentas.ActiveSheet.Columns(spVentas.ActiveSheet.Columns("descuento").Index).Visible = valor

    End Sub

    Private Sub CargarVales()

        Dim id As Integer = ALMLogicaVentas.Funciones.ValidarNumeroACero(txtId.Text)
        If (id > 0) Then
            vales.EIdVenta = id
            spVales.ActiveSheet.DataSource = vales.ObtenerListadoDetallado()
            FormatearSpreadVales()
        End If

    End Sub

    Private Sub FormatearSpreadVales()

        spVales.ActiveSheet.ColumnHeader.RowCount = 2
        spVales.ActiveSheet.ColumnHeader.Rows(0, spVales.ActiveSheet.ColumnHeader.Rows.Count - 1).Font = New Font(Principal.tipoLetraSpread, Principal.tamañoLetraSpread, FontStyle.Bold)
        spVales.ActiveSheet.ColumnHeader.Rows(0).Height = Principal.alturaFilasEncabezadosChicosSpread
        spVales.ActiveSheet.ColumnHeader.Rows(1).Height = Principal.alturaFilasEncabezadosMedianosSpread
        spVales.ActiveSheet.OperationMode = FarPoint.Win.Spread.OperationMode.Normal
        spVales.ActiveSheet.Rows.Count = 2
        ControlarSpreadEnterASiguienteColumna(spVales)
        Dim numeracion As Integer = 0
        spVales.ActiveSheet.Columns(numeracion).Tag = "idOrigen" : numeracion += 1
        spVales.ActiveSheet.Columns(numeracion).Tag = "id" : numeracion += 1
        spVales.ActiveSheet.Columns(numeracion).Tag = "total" : numeracion += 1
        spVales.ActiveSheet.Columns(numeracion).Tag = "totalVenta" : numeracion += 1
        spVales.ActiveSheet.Columns.Count = numeracion
        spVales.ActiveSheet.Columns("idOrigen").Width = 100
        spVales.ActiveSheet.Columns("id").Width = 100
        spVales.ActiveSheet.Columns("total").Width = 120
        spVales.ActiveSheet.Columns("totalVenta").Width = 120
        spVales.ActiveSheet.Columns("idOrigen").CellType = tipoEntero
        spVales.ActiveSheet.Columns("id").CellType = tipoEntero
        spVales.ActiveSheet.Columns("total").CellType = tipoDoble
        spVales.ActiveSheet.Columns("totalVenta").CellType = tipoDoble
        spVales.ActiveSheet.AddColumnHeaderSpanCell(0, spVales.ActiveSheet.Columns("idOrigen").Index, 1, 4)
        spVales.ActiveSheet.ColumnHeader.Cells(0, spVales.ActiveSheet.Columns("idOrigen").Index).Value = "V a l e s".ToUpper()
        spVales.ActiveSheet.ColumnHeader.Cells(1, spVales.ActiveSheet.Columns("idOrigen").Index).Value = "No. Origen *".ToUpper()
        spVales.ActiveSheet.ColumnHeader.Cells(1, spVales.ActiveSheet.Columns("id").Index).Value = "No. *".ToUpper()
        spVales.ActiveSheet.ColumnHeader.Cells(1, spVales.ActiveSheet.Columns("total").Index).Value = "Total".ToUpper()
        spVales.ActiveSheet.ColumnHeader.Cells(1, spVales.ActiveSheet.Columns("totalVenta").Index).Value = "Total Venta *".ToUpper()
        spVales.ActiveSheet.Columns("total").Locked = True
        spVales.ActiveSheet.Columns("totalVenta").Locked = True
        spVales.ActiveSheet.LockBackColor = Principal.colorCapturaBloqueada
        spVales.Refresh()

    End Sub

    Private Sub GuardarValesExtras()

        Dim id As Integer = ALMLogicaVentas.Funciones.ValidarNumeroACero(txtId.Text)
        ' Se eliminan todos los vales de acuerdo al idorigen y id.
        'EliminarValesExtras(False)
        Dim datos As New DataTable
        Dim datos2 As New DataTable
        ventasDetalle.EId = id
        ventas.EId = id
        datos = ventasDetalle.ObtenerListadoDetallado()
        datos2 = ventas.ObtenerListadoGeneral()
        If (datos.Rows.Count > 0) Then
            ' Parte superior.
            Dim idCliente As Integer = datos2.Rows(0).Item("IdCliente")
            Dim fecha As Date = datos2.Rows(0).Item("Fecha")
            Dim fechaVencimiento As Date = fecha.AddDays(30)
            Dim estaUtilizado As Boolean = False
            Dim sumaTotalVales As Double = CalcularTotalesVales(False)
            Dim sumaTotalVenta As Double = 0
            Dim idVenta As Integer = 0
            Dim totalVenta As Double = 0
            ' Parte inferior.
            For fila As Integer = 0 To datos.Rows.Count - 1
                Dim idAlmacen As Integer = datos.Rows(fila).Item("IdAlmacen")
                Dim idFamilia As Integer = datos.Rows(fila).Item("IdFamilia")
                Dim idSubFamilia As Integer = datos.Rows(fila).Item("IdSubFamilia")
                Dim idArticulo As Integer = datos.Rows(fila).Item("IdArticulo")
                Dim cantidad As Integer = datos.Rows(fila).Item("Cantidad")
                Dim precio As Double = datos.Rows(fila).Item("Precio")
                Dim total As Double = datos.Rows(fila).Item("Total")
                sumaTotalVenta += total
                Dim orden As Integer = fila
            Next
            If (Me.idOrigen > 0 AndAlso id > 0) Then
                vales.EIdOrigen = Me.idOrigen
                vales.EId = id
                vales.EIdCliente = idCliente
                vales.EFecha = fecha
                vales.ETotal = sumaTotalVales - sumaTotalVenta
                vales.EFechaVencimiento = fechaVencimiento
                vales.EEstaUtilizado = estaUtilizado
                vales.EIdVenta = idVenta
                vales.ETotalVenta = totalVenta
                vales.Guardar()
            End If
        End If

    End Sub

    Private Sub EliminarValesExtras(ByVal conMensaje As Boolean)

        Dim respuestaSi As Boolean = False
        If (conMensaje) Then
            If (MessageBox.Show("Confirmas que deseas eliminar este vale?", "Confirmación.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes) Then
                respuestaSi = True
            End If
        End If
        If ((respuestaSi) Or (Not conMensaje)) Then
            Dim id As Integer = ALMLogicaVentas.Funciones.ValidarNumeroACero(txtId.Text)
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

    Private Sub EditarVales()

        Dim idVenta As Integer = ALMLogicaVentas.Funciones.ValidarNumeroACero(txtId.Text)
        vales.EIdVenta = idVenta
        vales.EditarParaEliminar()
        For fila = 0 To spVales.ActiveSheet.Rows.Count - 1
            Dim idOrigen As Integer = ALMLogicaVentas.Funciones.ValidarNumeroACero(spVales.ActiveSheet.Cells(fila, spVales.ActiveSheet.Columns("idOrigen").Index).Value)
            Dim id As Integer = ALMLogicaVentas.Funciones.ValidarNumeroACero(spVales.ActiveSheet.Cells(fila, spVales.ActiveSheet.Columns("id").Index).Value)
            Dim totalVenta As Double = ALMLogicaVentas.Funciones.ValidarNumeroACero(spVales.ActiveSheet.Cells(fila, spVales.ActiveSheet.Columns("totalVenta").Index).Value)
            If (idOrigen > 0 AndAlso id > 0 AndAlso idVenta > 0 AndAlso totalVenta > 0) Then
                vales.EIdOrigen = idOrigen
                vales.EId = id
                vales.ETotalVenta = totalVenta
                vales.Editar()
            End If
        Next

    End Sub

    Private Sub ValidarGuardadoVentas()

        Me.esGuardadoValido = True
        CalcularTotalesVentas()
        CalcularCambio()
        ' Parte superior. 
        Dim id As Integer = ALMLogicaVentas.Funciones.ValidarNumeroACero(txtId.Text)
        If (id <= 0) Then
            txtId.BackColor = Principal.colorAdvertencia
            Me.esGuardadoValido = False
        End If
        Dim idCliente As Integer = ALMLogicaVentas.Funciones.ValidarNumeroACero(cbClientes.SelectedValue)
        If (idCliente <= 0) Then
            cbClientes.BackColor = Principal.colorAdvertencia
            Me.esGuardadoValido = False
        End If
        Dim idMetodoPago As Integer = ALMLogicaVentas.Funciones.ValidarNumeroACero(cbMetodosPagos.SelectedValue)
        If (idMetodoPago <= 0) Then
            cbMetodosPagos.BackColor = Principal.colorAdvertencia
            Me.esGuardadoValido = False
        End If
        Dim importeCambio As Double = ALMLogicaVentas.Funciones.ValidarNumeroACero(txtImporteCambio.Text)
        If (importeCambio < 0) Then
            txtImportePagado.BackColor = Principal.colorAdvertencia
            txtImporteCambio.BackColor = Principal.colorAdvertencia
            Me.esGuardadoValido = False
        End If
        ' Parte inferior.
        For fila As Integer = 0 To spVentas.ActiveSheet.Rows.Count - 1
            Dim idAlmacen As Integer = ALMLogicaVentas.Funciones.ValidarNumeroACero(spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("idAlmacen").Index).Text)
            Dim idFamilia As Integer = ALMLogicaVentas.Funciones.ValidarNumeroACero(spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("idFamilia").Index).Text)
            Dim idSubFamilia As Integer = ALMLogicaVentas.Funciones.ValidarNumeroACero(spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("idSubFamilia").Index).Text)
            Dim idArticulo As Integer = ALMLogicaVentas.Funciones.ValidarNumeroACero(spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("idArticulo").Index).Text)
            If (idAlmacen > 0 AndAlso idFamilia > 0 AndAlso idSubFamilia > 0 AndAlso idArticulo > 0) Then
                Dim cantidad As Integer = ALMLogicaVentas.Funciones.ValidarNumeroACero(spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("cantidad").Index).Text)
                If (cantidad <= 0) Then
                    spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("cantidad").Index).BackColor = Principal.colorAdvertencia
                    Me.esGuardadoValido = False
                End If
                Dim precio As String = spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("precio").Index).Text
                Dim precio2 As Double = ALMLogicaVentas.Funciones.ValidarNumeroACero(spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("precio").Index).Text)
                If (String.IsNullOrEmpty(precio) Or precio2 < 0) Then
                    spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("precio").Index).BackColor = Principal.colorAdvertencia
                    Me.esGuardadoValido = False
                End If
                Dim subtotal As String = spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("subtotal").Index).Text
                Dim subtotal2 As Double = ALMLogicaVentas.Funciones.ValidarNumeroACero(spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("subtotal").Index).Text)
                If (String.IsNullOrEmpty(subtotal) Or subtotal2 < 0) Then
                    spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("subtotal").Index).BackColor = Principal.colorAdvertencia
                    Me.esGuardadoValido = False
                End If
                Dim porcentajeDescuento As String = spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("porcentajeDescuento").Index).Text
                Dim porcentajeDescuento2 As Double = ALMLogicaVentas.Funciones.ValidarNumeroACero(spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("porcentajeDescuento").Index).Text)
                If (String.IsNullOrEmpty(porcentajeDescuento) Or porcentajeDescuento2 < 0) Then
                    spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("porcentajeDescuento").Index).BackColor = Principal.colorAdvertencia
                    Me.esGuardadoValido = False
                End If
                Dim descuento As String = spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("descuento").Index).Text
                Dim descuento2 As Double = ALMLogicaVentas.Funciones.ValidarNumeroACero(spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("descuento").Index).Text)
                If (String.IsNullOrEmpty(descuento) Or descuento2 < 0) Then
                    spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("descuento").Index).BackColor = Principal.colorAdvertencia
                    Me.esGuardadoValido = False
                End If
                Dim total As String = spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("total").Index).Text
                Dim total2 As Double = ALMLogicaVentas.Funciones.ValidarNumeroACero(spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("total").Index).Text)
                If (String.IsNullOrEmpty(total) Or total2 < 0) Then
                    spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("total").Index).BackColor = Principal.colorAdvertencia
                    Me.esGuardadoValido = False
                End If
            End If
        Next
        ' Parte inferior.
        For fila As Integer = 0 To spVales.ActiveSheet.Rows.Count - 1
            Dim idOrigenVale As Integer = ALMLogicaVentas.Funciones.ValidarNumeroACero(spVales.ActiveSheet.Cells(fila, spVales.ActiveSheet.Columns("idOrigen").Index).Text)
            Dim idVale As Integer = ALMLogicaVentas.Funciones.ValidarNumeroACero(spVales.ActiveSheet.Cells(fila, spVales.ActiveSheet.Columns("id").Index).Text)
            Dim totalVale As Integer = ALMLogicaVentas.Funciones.ValidarNumeroACero(spVales.ActiveSheet.Cells(fila, spVales.ActiveSheet.Columns("total").Index).Text)
            If (idOrigenVale > 0 AndAlso idVale > 0 AndAlso totalVale > 0) Then
                Dim totalVentaVale As String = spVales.ActiveSheet.Cells(fila, spVales.ActiveSheet.Columns("totalVenta").Index).Text
                Dim totalVentaVale2 As Double = ALMLogicaVentas.Funciones.ValidarNumeroACero(spVales.ActiveSheet.Cells(fila, spVales.ActiveSheet.Columns("totalVenta").Index).Text)
                If (String.IsNullOrEmpty(totalVentaVale) Or totalVentaVale2 < 0) Then
                    spVales.ActiveSheet.Cells(fila, spVales.ActiveSheet.Columns("totalVenta").Index).BackColor = Principal.colorAdvertencia
                    Me.esGuardadoValido = False
                End If
            End If
        Next

    End Sub

    Private Sub GuardarEditarVentas()

        Dim hiloImpresion As New Thread(AddressOf MandarImprimir)
        EliminarVentas(False)
        EliminarValesExtras(False) ' Siempre se eliminan los vales al volver a guardar.
        ' Parte superior.
        Dim id As Integer = ALMLogicaVentas.Funciones.ValidarNumeroACero(txtId.Text)
        Dim fecha As Date = dtpFecha.Value
        Dim idCliente As Integer = ALMLogicaVentas.Funciones.ValidarNumeroACero(cbClientes.SelectedValue)
        Dim idMetodoPago As Integer = ALMLogicaVentas.Funciones.ValidarNumeroACero(cbMetodosPagos.SelectedValue)
        Dim importePagado As Double = ALMLogicaVentas.Funciones.ValidarNumeroACero(txtImportePagado.Text)
        Dim importeCambio As Double = ALMLogicaVentas.Funciones.ValidarNumeroACero(txtImporteCambio.Text)
        Dim sumaSubtotal As Double = 0
        Dim sumaDescuento As Double = 0
        Dim sumaTotal As Double = 0
        ' Parte inferior.
        ' Se guarda la información de detalle.
        For fila As Integer = 0 To spVentas.ActiveSheet.Rows.Count - 1
            Dim idAlmacen As Integer = ALMLogicaVentas.Funciones.ValidarNumeroACero(spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("idAlmacen").Index).Text)
            Dim idFamilia As Integer = ALMLogicaVentas.Funciones.ValidarNumeroACero(spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("idFamilia").Index).Text)
            Dim idSubFamilia As Integer = ALMLogicaVentas.Funciones.ValidarNumeroACero(spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("idSubFamilia").Index).Text)
            Dim idArticulo As Integer = ALMLogicaVentas.Funciones.ValidarNumeroACero(spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("idArticulo").Index).Text)
            Dim cantidad As Integer = ALMLogicaVentas.Funciones.ValidarNumeroACero(spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("cantidad").Index).Text)
            Dim precio As Double = ALMLogicaVentas.Funciones.ValidarNumeroACero(spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("precio").Index).Text)
            Dim subtotal As Double = ALMLogicaVentas.Funciones.ValidarNumeroACero(spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("subtotal").Index).Text)
            sumaSubtotal += subtotal
            Dim porcentajeDescuento As Double = ALMLogicaVentas.Funciones.ValidarNumeroACero(spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("porcentajeDescuento").Index).Text)
            Dim descuento As Double = ALMLogicaVentas.Funciones.ValidarNumeroACero(spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("descuento").Index).Text)
            sumaDescuento += descuento
            Dim total As Double = ALMLogicaVentas.Funciones.ValidarNumeroACero(spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("total").Index).Text)
            sumaTotal += total
            Dim orden As Integer = fila
            Dim observaciones As String = String.Empty
            If (id > 0 AndAlso idAlmacen > 0 AndAlso idFamilia > 0 AndAlso idSubFamilia > 0 AndAlso idArticulo > 0 AndAlso idCliente > 0) Then
                ventasDetalle.EId = id
                ventasDetalle.EIdAlmacen = idAlmacen
                ventasDetalle.EIdFamilia = idFamilia
                ventasDetalle.EIdSubFamilia = idSubFamilia
                ventasDetalle.EIdArticulo = idArticulo
                ventasDetalle.ECantidad = cantidad
                ventasDetalle.EPrecio = precio
                ventasDetalle.ESubTotal = subtotal
                ventasDetalle.EPorcentajeDescuento = porcentajeDescuento
                ventasDetalle.EDescuento = descuento
                ventasDetalle.ETotal = total
                ventasDetalle.EOrden = orden
                ventasDetalle.EObservaciones = observaciones
                ventasDetalle.Guardar()
            End If
        Next
        ' Se guarda la información general.
        If (id > 0 AndAlso idCliente > 0) Then
            ventas.EId = id
            ventas.EIdCliente = idCliente
            ventas.EFecha = fecha
            ventas.ESubTotal = sumaSubtotal
            ventas.EDescuento = sumaDescuento
            ventas.ETotal = sumaTotal
            ventas.EIdMetodoPago = idMetodoPago
            ventas.EImportePagado = importePagado
            ventas.EImporteCambio = importeCambio
            ventas.Guardar()
        End If
        EditarVales()
        Dim sumaTotalVales As Double = CalcularTotalesVales(False)
        Dim sumaTotalVenta As Double = ALMLogicaVentas.Funciones.ValidarNumeroACero(txtTotal.Text)
        Dim aplicaImpresionVale As Boolean = False
        If (sumaTotalVales > sumaTotalVenta) Then ' Si la suma de los vales es mayor a la suma de la venta. 
            aplicaImpresionVale = True
            GuardarValesExtras() ' Se crea un vale nuevo.
        End If
        GuardarSalidasAlmacen()
        Me.opcionTipoSeleccionada = OpcionTipoEtiqueta.recibo
        PrepararImpresion(id)
        If (aplicaImpresionVale) Then
            Me.opcionTipoSeleccionada = OpcionTipoEtiqueta.vale
            PrepararImpresion(id)
        End If
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

    Private Sub GuardarSalidasAlmacen()

        ' No capturables por el usuario.
        Dim idExterno As String = String.Empty
        Dim idMoneda As Integer = 1 ' Pesos
        Dim tipoCambio As Double = 1 ' 1
        Dim idTipoSalida As Integer = 1 ' Normal o venta.
        Dim factura As String = String.Empty
        Dim id As Integer = ALMLogicaVentas.Funciones.ValidarNumeroACero(txtId.Text)
        ' Se eliminan todas las salidas de acuerdo al idorigen, idalmacen y idsalida.
        EliminarSalidasAlmacen()
        Dim datos As New DataTable
        Dim datos2 As New DataTable
        ventasDetalle.EId = id
        ventas.EId = id
        datos = ventasDetalle.ObtenerListadoDetallado()
        datos2 = ventas.ObtenerListadoGeneral()
        ' Parte inferior.
        For fila As Integer = 0 To datos.Rows.Count - 1
            Dim idAlmacen As Integer = datos.Rows(fila).Item("IdAlmacen")
            Dim idFamilia As Integer = datos.Rows(fila).Item("IdFamilia")
            Dim idSubFamilia As Integer = datos.Rows(fila).Item("IdSubFamilia")
            Dim idArticulo As Integer = datos.Rows(fila).Item("IdArticulo")
            Dim fecha As Date = datos2.Rows(0).Item("Fecha")
            Dim idCliente As Integer = datos2.Rows(0).Item("IdCliente")
            Dim cantidad As Integer = datos.Rows(fila).Item("Cantidad")
            Dim precio As Double = datos.Rows(fila).Item("Precio")
            Dim total As Double = datos.Rows(fila).Item("Total")
            Dim totalPesos As Double = total
            Dim orden As Integer = fila
            Dim observaciones As String = datos.Rows(fila).Item("Observaciones")
            If (Me.idOrigen > 0 AndAlso id > 0 AndAlso idAlmacen > 0 AndAlso idFamilia > 0 AndAlso idSubFamilia > 0 AndAlso idArticulo > 0 AndAlso idMoneda > 0 AndAlso idTipoSalida > 0 AndAlso idCliente > 0) Then
                salidas.EIdOrigen = Me.idOrigen
                salidas.EIdAlmacen = idAlmacen
                salidas.EId = id
                salidas.EIdFamilia = idFamilia
                salidas.EIdSubFamilia = idSubFamilia
                salidas.EIdArticulo = idArticulo
                salidas.EIdExterno = idExterno
                salidas.EIdTipoSalida = idTipoSalida
                salidas.EIdCliente = idCliente
                salidas.EIdMoneda = idMoneda
                salidas.ETipoCambio = tipoCambio
                salidas.EFecha = fecha
                salidas.ECantidad = cantidad
                salidas.EPrecio = precio
                salidas.ETotal = total
                salidas.ETotalPesos = totalPesos
                salidas.EOrden = orden
                salidas.EObservaciones = observaciones
                salidas.EFactura = factura
                salidas.Guardar()
            End If
        Next

    End Sub

    Private Sub EliminarSalidasAlmacen()

        Dim datos As New DataTable
        Dim id As Integer = ALMLogicaVentas.Funciones.ValidarNumeroACero(txtId.Text)
        ventasDetalle.EId = id
        datos = ventasDetalle.ObtenerListadoParaAlmacen()
        salidas.EIdOrigen = Me.idOrigen
        For fila = 0 To datos.Rows.Count - 1
            Dim idAlmacen As Integer = datos.Rows(fila).Item("IdAlmacen")
            salidas.EIdAlmacen = idAlmacen
            salidas.EId = id
            salidas.Eliminar()
        Next

    End Sub

    Private Sub ImprimirManualmente()

        Dim id As Integer = ALMLogicaVentas.Funciones.ValidarNumeroACero(txtId.Text)
        Me.opcionTipoSeleccionada = OpcionTipoEtiqueta.recibo
        PrepararImpresion(id)
        Me.opcionTipoSeleccionada = OpcionTipoEtiqueta.vale
        PrepararImpresion(id)
        MandarImprimir()

    End Sub

    Private Sub CargarOpcionesImpresion()

        Impresoras.CargarImpresoras(True)

    End Sub

    Private Sub PrepararImpresion(ByVal id As Integer)

        ventasDetalle.EId = id
        If (Me.opcionTipoSeleccionada = OpcionTipoEtiqueta.recibo) Then ' Aquí se obtienen datos del recibo.
            Me.datosRecibosParaImprimir = ventasDetalle.ObtenerListadoImpresionRecibos()
            For fila As Integer = 0 To Me.datosRecibosParaImprimir.Rows.Count - 1
                ' Se agrega al listado.
                Me.listaRecibosParaImprimir.Add(Me.datosRecibosParaImprimir.Rows(fila))
            Next
        ElseIf (Me.opcionTipoSeleccionada = OpcionTipoEtiqueta.vale) Then ' Aquí se obtienen datos del vale.
            vales.EIdOrigen = Me.idOrigen
            vales.EId = id
            vales.EIdVenta = id
            Me.datosValesParaImprimir = vales.ObtenerListadoImpresionVales()
            For fila As Integer = 0 To Me.datosValesParaImprimir.Rows.Count - 1
                ' Se agrega al listado.
                Me.listaValesParaImprimir.Add(Me.datosValesParaImprimir.Rows(fila))
            Next
        End If

    End Sub

    Private Sub MandarImprimir()

        Me.estaImprimiendo = True
        ' Impresión de etiquetas de recibo.
        ' Si hay datos para imprimir.
        If (Me.listaRecibosParaImprimir.Count > 0) Then
            pdRecibo.PrinterSettings.PrinterName = Impresoras.nombreImpresoraRecibo
            If (Impresoras.habilitarImpresoraRecibo) Then
                Try
                    pdRecibo.DocumentName = "Recibo" & txtId.Text.PadLeft(6, "0")
                    pdRecibo.Print()
                Catch ex As Exception
                    MsgBox("Hay un error al imprimir. " & ex.Message, MsgBoxStyle.Critical, "Error.")
                End Try
            End If
        End If
        ' Se reinician los valores para la próxima impresión.
        Me.contadorRecibosParaImprimir = 0
        Me.listaRecibosParaImprimir.Clear()
        Me.datosRecibosParaImprimir.Clear()
        ' Impresion de etiquetas de vale. 
        ' Si hay datos para imprimir.
        If (Me.listaValesParaImprimir.Count > 0) Then
            pdVale.PrinterSettings.PrinterName = Impresoras.nombreImpresoraVale
            If (Impresoras.habilitarImpresoraVale) Then
                Try
                    pdVale.DocumentName = "Vale" & Me.idOrigen.ToString.PadLeft(2, "0") & txtId.Text.PadLeft(6, "0")
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

    Private Sub CrearRecibo(ByRef e As System.Drawing.Printing.PrintPageEventArgs)

        Dim fuente7 As New Font(Principal.tipoLetraSpread, 7, FontStyle.Bold)
        Dim fuente8 As New Font(Principal.tipoLetraSpread, 8, FontStyle.Bold)
        Dim imagen As System.Drawing.Image = Nothing
        Dim margenIzquierdoRecibo As Integer = Impresoras.margenIzquierdoRecibo : Dim margenSuperiorRecibo As Integer = Impresoras.margenSuperiorRecibo
        Dim formato As New StringFormat()
        formato.Alignment = StringAlignment.Center
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
            empresa = ALMLogicaVentas.Funciones.ValidarLetra(datos.Rows(0).Item("Nombre").ToString())
            representante = ALMLogicaVentas.Funciones.ValidarLetra(datos.Rows(0).Item("RepresentanteLegal").ToString())
            rfc = ALMLogicaVentas.Funciones.ValidarLetra(datos.Rows(0).Item("Rfc").ToString())
            domicilio = ALMLogicaVentas.Funciones.ValidarLetra(datos.Rows(0).Item("Domicilio").ToString())
            telefono = ALMLogicaVentas.Funciones.ValidarLetra(datos.Rows(0).Item("Telefono").ToString())
            municipio = ALMLogicaVentas.Funciones.ValidarLetra(datos.Rows(0).Item("Municipio").ToString())
            estado = ALMLogicaVentas.Funciones.ValidarLetra(datos.Rows(0).Item("Estado").ToString())
            pais = ALMLogicaVentas.Funciones.ValidarLetra(datos.Rows(0).Item("Pais").ToString())
        End If
        ' Información de recibo de venta.
        Dim id As String = ALMLogicaVentas.Funciones.ValidarLetra(Me.listaRecibosParaImprimir.Item(Me.contadorRecibosParaImprimir).Item("Id").ToString()).PadLeft(6, "0").ToString()
        Dim fecha As String = ALMLogicaVentas.Funciones.ValidarLetra(Me.listaRecibosParaImprimir.Item(Me.contadorRecibosParaImprimir).Item("Fecha"))
        Dim hora As String = Now.ToShortTimeString
        Dim nombreCliente As String = ALMLogicaVentas.Funciones.ValidarLetra(Me.listaRecibosParaImprimir.Item(Me.contadorRecibosParaImprimir).Item("NombreCliente").ToString())
        Dim metodoPago As String = "Indefinido"
        metodoPago = ALMLogicaVentas.Funciones.ValidarLetra(Me.listaRecibosParaImprimir.Item(Me.contadorRecibosParaImprimir).Item("NombreMetodoPago").ToString())
        ' Formato de recibo.
        altura = 5
        e.Graphics.DrawString(empresa, fuente8, Brushes.Black, margenIzquierdoRecibo + 0, margenSuperiorRecibo + altura) ' Empresa.
        altura += 12
        e.Graphics.DrawString(representante, fuente8, Brushes.Black, margenIzquierdoRecibo + 0, margenSuperiorRecibo + altura) ' Representante.
        altura += 12
        e.Graphics.DrawString(rfc, fuente8, Brushes.Black, margenIzquierdoRecibo + 0, margenSuperiorRecibo + altura) ' Rfc.
        altura += 12
        e.Graphics.DrawString(domicilio, fuente8, Brushes.Black, margenIzquierdoRecibo + 0, margenSuperiorRecibo + altura) ' Domicilio.
        altura += 12
        e.Graphics.DrawString(telefono, fuente8, Brushes.Black, margenIzquierdoRecibo + 0, margenSuperiorRecibo + altura) ' Telefono.
        altura += 12
        e.Graphics.DrawString(municipio & ", " & estado & ", " & pais, fuente8, Brushes.Black, margenIzquierdoRecibo + 0, margenSuperiorRecibo + altura) ' Localidad.
        altura += 20
        e.Graphics.DrawString(id, fuente8, Brushes.Black, margenIzquierdoRecibo + 0, margenSuperiorRecibo + altura) ' Id.
        e.Graphics.DrawString(fecha & " " & hora, fuente8, Brushes.Black, margenIzquierdoRecibo + 125, margenSuperiorRecibo + altura) ' Fecha y Hora.
        altura += 20
        e.Graphics.DrawString("Cliente: ", fuente8, Brushes.Black, margenIzquierdoRecibo + 0, margenSuperiorRecibo + altura) ' Cliente.
        altura += 12
        e.Graphics.DrawString(nombreCliente, fuente8, Brushes.Black, margenIzquierdoRecibo + 0, margenSuperiorRecibo + altura) ' Cliente.
        altura += 20
        e.Graphics.DrawLine(Pens.Black, margenIzquierdoRecibo + 0, margenSuperiorRecibo + altura, margenIzquierdoRecibo + 300, margenSuperiorRecibo + altura) ' Linea.
        e.Graphics.DrawString("Cant.", fuente8, Brushes.Black, margenIzquierdoRecibo + 0, margenSuperiorRecibo + altura) ' Cantidad.
        e.Graphics.DrawString("Modelo", fuente8, Brushes.Black, margenIzquierdoRecibo + 45, margenSuperiorRecibo + altura) ' Modelo.
        e.Graphics.DrawString("Color", fuente8, Brushes.Black, margenIzquierdoRecibo + 95, margenSuperiorRecibo + altura) ' Color.
        e.Graphics.DrawString("Núm.", fuente8, Brushes.Black, margenIzquierdoRecibo + 150, margenSuperiorRecibo + altura) ' Talla.
        e.Graphics.DrawString("Precio", fuente8, Brushes.Black, margenIzquierdoRecibo + 195, margenSuperiorRecibo + altura) ' Precio.
        altura += 12
        Dim total As Double = 0
        For indice = 0 To Me.listaRecibosParaImprimir.Count - 1
            Dim idAlmacen As Integer = ALMLogicaVentas.Funciones.ValidarNumeroACero(Me.listaRecibosParaImprimir.Item(indice).Item("IdAlmacen").ToString())
            Dim cantidad As String = ALMLogicaVentas.Funciones.ValidarLetra(Me.listaRecibosParaImprimir.Item(indice).Item("Cantidad").ToString())
            Dim nombreArticulo As String = ALMLogicaVentas.Funciones.ValidarLetra(Me.listaRecibosParaImprimir.Item(indice).Item("NombreArticulo").ToString())
            Dim modelo As String = ALMLogicaVentas.Funciones.ValidarLetra(Me.listaRecibosParaImprimir.Item(indice).Item("Modelo").ToString())
            Dim color As String = ALMLogicaVentas.Funciones.ValidarLetra(Me.listaRecibosParaImprimir.Item(indice).Item("Color").ToString())
            Dim talla As String = ALMLogicaVentas.Funciones.ValidarLetra(Me.listaRecibosParaImprimir.Item(indice).Item("Talla").ToString())
            Dim precio As Double = ALMLogicaVentas.Funciones.ValidarNumeroACero(Me.listaRecibosParaImprimir.Item(indice).Item("Precio").ToString())
            Dim porcentajeDescuento As Double = ALMLogicaVentas.Funciones.ValidarNumeroACero(Me.listaRecibosParaImprimir.Item(indice).Item("PorcentajeDescuento").ToString())
            Dim descuento As Double = ALMLogicaVentas.Funciones.ValidarNumeroACero(Me.listaRecibosParaImprimir.Item(indice).Item("Descuento").ToString())
            total += ALMLogicaVentas.Funciones.ValidarNumeroACero(Me.listaRecibosParaImprimir.Item(indice).Item("Total").ToString())
            e.Graphics.DrawString(cantidad, fuente7, Brushes.Black, margenIzquierdoRecibo + 0, margenSuperiorRecibo + altura) ' Cantidad.
            e.Graphics.DrawString(modelo, fuente7, Brushes.Black, margenIzquierdoRecibo + 45, margenSuperiorRecibo + altura) ' Modelo.
            e.Graphics.DrawString(IIf(idAlmacen = 1, color, nombreArticulo), fuente7, Brushes.Black, margenIzquierdoRecibo + 95, margenSuperiorRecibo + altura) ' Color.
            e.Graphics.DrawString(talla, fuente7, Brushes.Black, margenIzquierdoRecibo + 150, margenSuperiorRecibo + altura) ' Talla.
            e.Graphics.DrawString(precio.ToString("C"), fuente7, Brushes.Black, margenIzquierdoRecibo + 195, margenSuperiorRecibo + altura) ' Precio.
            altura += 12
            If (descuento > 0) Then
                e.Graphics.DrawString("Descto.", fuente7, Brushes.Black, margenIzquierdoRecibo + 95, margenSuperiorRecibo + altura) ' Descuento.
                e.Graphics.DrawString(porcentajeDescuento & "%", fuente7, Brushes.Black, margenIzquierdoRecibo + 150, margenSuperiorRecibo + altura) ' Porcentaje Descuento.
                e.Graphics.DrawString(descuento.ToString("C"), fuente7, Brushes.Black, margenIzquierdoRecibo + 195, margenSuperiorRecibo + altura) ' Descuento.
                altura += 12
            End If
        Next
        e.Graphics.DrawLine(Pens.Black, margenIzquierdoRecibo + 0, margenSuperiorRecibo + altura, margenIzquierdoRecibo + 300, margenSuperiorRecibo + altura) ' Linea. 
        e.Graphics.DrawString("Total", fuente7, Brushes.Black, margenIzquierdoRecibo + 95, margenSuperiorRecibo + altura) ' Total.
        e.Graphics.DrawString(total.ToString("C"), fuente7, Brushes.Black, margenIzquierdoRecibo + 195, margenSuperiorRecibo + altura) ' Total.
        altura += 20
        Dim datosVales As New DataTable
        vales.EIdVenta = id
        datosVales = vales.ObtenerListadoDetallado()
        Dim idOrigenVale1 As String = String.Empty
        Dim idVale1 As String = String.Empty
        Dim idsVale1 As String = "NA"
        Dim precioVale1 As Double = 0
        Dim idOrigenVale2 As String = String.Empty
        Dim idVale2 As String = String.Empty
        Dim idsVale2 As String = "NA"
        Dim precioVale2 As Double = 0
        If (datosVales.Rows.Count >= 1 AndAlso datosVales.Rows.Count <= 2) Then
            idOrigenVale1 = ALMLogicaVentas.Funciones.ValidarNumeroACero(datosVales.Rows(0).Item("IdOrigen").ToString).ToString.PadLeft(2, "0")
            idVale1 = ALMLogicaVentas.Funciones.ValidarNumeroACero(datosVales.Rows(0).Item("Id").ToString).ToString.PadLeft(6, "0")
            idsVale1 = idOrigenVale1 & idVale1
            If (String.IsNullOrEmpty(idsVale1)) Then
                idsVale1 = "NA"
            End If
            precioVale1 = ALMLogicaVentas.Funciones.ValidarNumeroACero(datosVales.Rows(0).Item("TotalVenta").ToString)
        End If
        If (datosVales.Rows.Count = 2) Then
            idOrigenVale2 = ALMLogicaVentas.Funciones.ValidarNumeroACero(datosVales.Rows(1).Item("IdOrigen").ToString).ToString.PadLeft(2, "0")
            idVale2 = ALMLogicaVentas.Funciones.ValidarNumeroACero(datosVales.Rows(1).Item("Id").ToString).ToString.PadLeft(6, "0")
            idsVale2 = idOrigenVale2 & idVale2
            If (String.IsNullOrEmpty(idsVale2)) Then
                idsVale2 = "NA"
            End If
            precioVale2 = ALMLogicaVentas.Funciones.ValidarNumeroACero(datosVales.Rows(1).Item("TotalVenta").ToString)
        End If
        Dim diferencia As Double = total - precioVale1 - precioVale2
        e.Graphics.DrawString("Vale No.", fuente7, Brushes.Black, margenIzquierdoRecibo + 45, margenSuperiorRecibo + altura) ' Vale. 
        e.Graphics.DrawString(idsVale1, fuente7, Brushes.Black, margenIzquierdoRecibo + 95, margenSuperiorRecibo + altura) ' Vale 1.
        e.Graphics.DrawString(precioVale1.ToString("C"), fuente7, Brushes.Black, margenIzquierdoRecibo + 195, margenSuperiorRecibo + altura) ' Vale 1.
        altura += 12
        e.Graphics.DrawString(idsVale2, fuente7, Brushes.Black, margenIzquierdoRecibo + 95, margenSuperiorRecibo + altura) ' Vale 2.
        e.Graphics.DrawString(precioVale2.ToString("C"), fuente7, Brushes.Black, margenIzquierdoRecibo + 195, margenSuperiorRecibo + altura) ' Vale 2.
        altura += 12
        e.Graphics.DrawString("Diferencia", fuente7, Brushes.Black, margenIzquierdoRecibo + 95, margenSuperiorRecibo + altura) ' Diferencia.
        e.Graphics.DrawString(diferencia.ToString("C"), fuente7, Brushes.Black, margenIzquierdoRecibo + 195, margenSuperiorRecibo + altura) ' Diferencia.
        altura += 20
        e.Graphics.DrawString("Método de pago:", fuente8, Brushes.Black, margenIzquierdoRecibo + 0, margenSuperiorRecibo + altura) ' Metodo pago.
        altura += 12
        e.Graphics.DrawString(metodoPago, fuente8, Brushes.Black, margenIzquierdoRecibo + 0, margenSuperiorRecibo + altura) ' Metodo pago.
        altura += 20
        e.Graphics.DrawString("Le atendió:", fuente8, Brushes.Black, margenIzquierdoRecibo + 0, margenSuperiorRecibo + altura) ' Atendio.
        altura += 12
        e.Graphics.DrawString(ALMLogicaVentas.Usuarios.nombre, fuente8, Brushes.Black, margenIzquierdoRecibo + 0, margenSuperiorRecibo + altura) ' Atendio.
        altura += 20
        e.Graphics.DrawString("¡Gracias por su preferencia!", fuente8, Brushes.Black, margenIzquierdoRecibo + 0, margenSuperiorRecibo + altura) ' Gracias.
        altura += 20
        e.Graphics.DrawString("Garantía válida hasta:", fuente8, Brushes.Black, margenIzquierdoRecibo + 0, margenSuperiorRecibo + altura) ' Garantia.
        altura += 12
        e.Graphics.DrawString(Convert.ToDateTime(fecha).AddDays(15), fuente8, Brushes.Black, margenIzquierdoRecibo + 0, margenSuperiorRecibo + altura) ' Garantia.

    End Sub

    Private Sub CrearVale(ByRef e As System.Drawing.Printing.PrintPageEventArgs)

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
            empresa = ALMLogicaVentas.Funciones.ValidarLetra(datos.Rows(0).Item("Nombre").ToString())
            representante = ALMLogicaVentas.Funciones.ValidarLetra(datos.Rows(0).Item("RepresentanteLegal").ToString())
            rfc = ALMLogicaVentas.Funciones.ValidarLetra(datos.Rows(0).Item("Rfc").ToString())
            domicilio = ALMLogicaVentas.Funciones.ValidarLetra(datos.Rows(0).Item("Domicilio").ToString())
            telefono = ALMLogicaVentas.Funciones.ValidarLetra(datos.Rows(0).Item("Telefono").ToString())
            municipio = ALMLogicaVentas.Funciones.ValidarLetra(datos.Rows(0).Item("Municipio").ToString())
            estado = ALMLogicaVentas.Funciones.ValidarLetra(datos.Rows(0).Item("Estado").ToString())
            pais = ALMLogicaVentas.Funciones.ValidarLetra(datos.Rows(0).Item("Pais").ToString())
        End If
        ' Información de vale.
        Dim idOrigen As String = ALMLogicaVentas.Funciones.ValidarLetra(Me.listaValesParaImprimir.Item(Me.contadorValesParaImprimir).Item("IdOrigen").ToString()).PadLeft(2, "0").ToString()
        Dim id As String = ALMLogicaVentas.Funciones.ValidarLetra(Me.listaValesParaImprimir.Item(Me.contadorValesParaImprimir).Item("Id").ToString()).PadLeft(6, "0").ToString()
        Dim fecha As String = ALMLogicaVentas.Funciones.ValidarLetra(Me.listaValesParaImprimir.Item(Me.contadorValesParaImprimir).Item("Fecha"))
        Dim hora As String = Now.ToShortTimeString
        Dim nombreCliente As String = ALMLogicaVentas.Funciones.ValidarLetra(Me.listaValesParaImprimir.Item(Me.contadorValesParaImprimir).Item("NombreCliente").ToString())
        Dim sumaTotal As Double = ALMLogicaVentas.Funciones.ValidarNumeroACero(Me.listaValesParaImprimir.Item(Me.contadorValesParaImprimir).Item("Total").ToString())
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
        'e.Graphics.DrawLine(Pens.Black, margenIzquierdoVale + 0, margenSuperiorVale + altura, margenIzquierdoVale + 300, margenSuperiorVale + altura) ' Linea.
        'e.Graphics.DrawString("Cant.", fuenteDescripcion8, Brushes.Black, margenIzquierdoVale + 0, margenSuperiorVale + altura) ' Cantidad.
        'e.Graphics.DrawString("Modelo", fuenteDescripcion8, Brushes.Black, margenIzquierdoVale + 45, margenSuperiorVale + altura) ' Modelo.
        'e.Graphics.DrawString("Color", fuenteDescripcion8, Brushes.Black, margenIzquierdoVale + 95, margenSuperiorVale + altura) ' Color.
        'e.Graphics.DrawString("Núm.", fuenteDescripcion8, Brushes.Black, margenIzquierdoVale + 150, margenSuperiorVale + altura) ' Talla.
        'e.Graphics.DrawString("Precio", fuenteDescripcion8, Brushes.Black, margenIzquierdoVale + 195, margenSuperiorVale + altura) ' Precio.
        'altura += 12
        'Dim total As Double = 0
        'For indice = 0 To Me.listaValesParaImprimir.Count - 1
        '    Dim cantidad As String = ALMLogicaVentas.Funciones.ValidarLetra(Me.listaValesParaImprimir.Item(indice).Item("Cantidad").ToString())
        '    Dim modelo As String = ALMLogicaVentas.Funciones.ValidarLetra(Me.listaValesParaImprimir.Item(indice).Item("Modelo").ToString())
        '    Dim color As String = ALMLogicaVentas.Funciones.ValidarLetra(Me.listaValesParaImprimir.Item(indice).Item("Color").ToString())
        '    Dim talla As String = ALMLogicaVentas.Funciones.ValidarLetra(Me.listaValesParaImprimir.Item(indice).Item("Talla").ToString())
        '    Dim precio As Double = ALMLogicaVentas.Funciones.ValidarNumeroACero(Me.listaValesParaImprimir.Item(indice).Item("Precio").ToString())
        '    total += ALMLogicaVentas.Funciones.ValidarNumeroACero(Me.listaValesParaImprimir.Item(indice).Item("Total").ToString())
        '    e.Graphics.DrawString(cantidad, fuenteDescripcion8, Brushes.Black, margenIzquierdoVale + 0, margenSuperiorVale + altura) ' Cantidad.
        '    e.Graphics.DrawString(modelo, fuenteDescripcion8, Brushes.Black, margenIzquierdoVale + 45, margenSuperiorVale + altura) ' Modelo.
        '    e.Graphics.DrawString(color, fuenteDescripcion8, Brushes.Black, margenIzquierdoVale + 95, margenSuperiorVale + altura) ' Color.
        '    e.Graphics.DrawString(talla, fuenteDescripcion8, Brushes.Black, margenIzquierdoVale + 150, margenSuperiorVale + altura) ' Talla.
        '    e.Graphics.DrawString(precio.ToString("C"), fuenteDescripcion8, Brushes.Black, margenIzquierdoVale + 195, margenSuperiorVale + altura) ' Precio.
        '    altura += 12
        'Next
        e.Graphics.DrawLine(Pens.Black, margenIzquierdoVale + 0, margenSuperiorVale + altura, margenIzquierdoVale + 300, margenSuperiorVale + altura) ' Linea. 
        e.Graphics.DrawString("Total", fuenteDescripcion8, Brushes.Black, margenIzquierdoVale + 95, margenSuperiorVale + altura) ' Total.
        e.Graphics.DrawString(sumaTotal.ToString("C"), fuenteDescripcion8, Brushes.Black, margenIzquierdoVale + 195, margenSuperiorVale + altura) ' Total.
        altura += 20
        e.Graphics.DrawString("Le atendió:", fuenteDescripcion8, Brushes.Black, margenIzquierdoVale + 0, margenSuperiorVale + altura) ' Atendio.
        altura += 12
        e.Graphics.DrawString(ALMLogicaVentas.Usuarios.nombre, fuenteDescripcion8, Brushes.Black, margenIzquierdoVale + 0, margenSuperiorVale + altura) ' Atendio.
        altura += 20
        e.Graphics.DrawString("¡Gracias por su preferencia!", fuenteDescripcion8, Brushes.Black, margenIzquierdoVale + 0, margenSuperiorVale + altura) ' Gracias.
        altura += 20
        e.Graphics.DrawString("Válido hasta:", fuenteDescripcion8, Brushes.Black, margenIzquierdoVale + 0, margenSuperiorVale + altura) ' Garantia.
        altura += 12
        e.Graphics.DrawString(Convert.ToDateTime(fecha).AddDays(30), fuenteDescripcion8, Brushes.Black, margenIzquierdoVale + 0, margenSuperiorVale + altura) ' Garantia. 

    End Sub

    Private Sub EliminarVentas(ByVal conMensaje As Boolean)

        Dim respuestaSi As Boolean = False
        If (conMensaje) Then
            If (MessageBox.Show("Confirmas que deseas eliminar esta venta?", "Confirmación.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes) Then
                respuestaSi = True
            End If
        End If
        If ((respuestaSi) Or (Not conMensaje)) Then
            EliminarSalidasAlmacen()
            Dim id As Integer = ALMLogicaVentas.Funciones.ValidarNumeroACero(txtId.Text)
            ventas.EId = id
            ventas.Eliminar()
            ventasDetalle.EId = id
            ventasDetalle.Eliminar()
        End If
        If (conMensaje And respuestaSi) Then
            MessageBox.Show("Eliminado finalizado.", "Finalizado.", MessageBoxButtons.OK)
            LimpiarPantalla()
            CargarIdConsecutivo()
            AsignarFoco(txtId)
            txtId.SelectAll()
        End If

    End Sub

    Private Sub FormatearSpreadListadosOtros(ByVal posicion As Integer)

        spListadosOtros.Width = 440
        If (posicion = OpcionPosicion.izquierda) Then ' Izquierda.
            spListadosOtros.Location = New Point(Me.izquierda, Me.arriba)
        ElseIf (posicion = OpcionPosicion.centro) Then ' Centrar.
            spListadosOtros.Location = New Point(Me.anchoMitad - (spListadosOtros.Width / 2), Me.arriba)
        ElseIf (posicion = OpcionPosicion.derecha) Then ' Derecha.
            spListadosOtros.Location = New Point(Me.anchoTotal - spListadosOtros.Width, Me.arriba)
        End If
        spListadosOtros.ActiveSheet.Columns.Count = 4
        spListadosOtros.ActiveSheet.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect
        Dim numeracion As Integer = 0
        spListadosOtros.ActiveSheet.Columns(numeracion).Tag = "idOrigen" : numeracion += 1
        spListadosOtros.ActiveSheet.Columns(numeracion).Tag = "id" : numeracion += 1
        spListadosOtros.ActiveSheet.Columns(numeracion).Tag = "fecha" : numeracion += 1
        spListadosOtros.ActiveSheet.Columns(numeracion).Tag = "total" : numeracion += 1
        spListadosOtros.ActiveSheet.ColumnHeader.Rows(0).Font = New Font(Principal.tipoLetraSpread, Principal.tamañoLetraSpread, FontStyle.Bold)
        spListadosOtros.ActiveSheet.ColumnHeader.Rows(0).Height = Principal.alturaFilasEncabezadosMedianosSpread
        spListadosOtros.ActiveSheet.ColumnHeader.Cells(0, spListadosOtros.ActiveSheet.Columns("idOrigen").Index).Value = "No. Origen".ToUpper
        spListadosOtros.ActiveSheet.ColumnHeader.Cells(0, spListadosOtros.ActiveSheet.Columns("id").Index).Value = "No.".ToUpper
        spListadosOtros.ActiveSheet.ColumnHeader.Cells(0, spListadosOtros.ActiveSheet.Columns("fecha").Index).Value = "Fecha".ToUpper
        spListadosOtros.ActiveSheet.ColumnHeader.Cells(0, spListadosOtros.ActiveSheet.Columns("total").Index).Value = "Total".ToUpper
        spListadosOtros.ActiveSheet.Columns("idOrigen").Width = 100
        spListadosOtros.ActiveSheet.Columns("id").Width = 70
        spListadosOtros.ActiveSheet.Columns("fecha").Width = 100
        spListadosOtros.ActiveSheet.Columns("total").Width = 100
        spListadosOtros.ActiveSheet.Columns(0, spListadosOtros.ActiveSheet.Columns.Count - 1).AllowAutoFilter = True
        spListadosOtros.ActiveSheet.Columns(0, spListadosOtros.ActiveSheet.Columns.Count - 1).AllowAutoSort = True
        spListadosOtros.Height = spVentas.Height
        spListadosOtros.BringToFront()
        spListadosOtros.Visible = True
        spListadosOtros.Refresh()

    End Sub

    Private Sub CargarListadosOtros()

        If (spListadosOtros.Visible) Then
            spListadosOtros.Visible = False
            spVentas.Enabled = True
        Else
            spVentas.Enabled = False
            vales.EIdOrigen = 0
            vales.EId = 0
            vales.EIdCliente = cbClientes.SelectedValue
            Dim datos As New DataTable
            datos = vales.ObtenerListado()
            If (datos.Rows.Count > 0) Then
                spListadosOtros.ActiveSheet.DataSource = datos
            Else
                spListadosOtros.ActiveSheet.DataSource = Nothing
                spListadosOtros.ActiveSheet.Rows.Count = 0
                spVentas.Enabled = True
            End If
            FormatearSpreadListadosOtros(OpcionPosicion.centro)
            AsignarFoco(spListadosOtros)
        End If

    End Sub

    Private Sub CargarDatosDeListadosOtros(ByVal filaCatalogos As Integer)

        spVales.ActiveSheet.Cells(spVales.ActiveSheet.ActiveRowIndex, spVales.ActiveSheet.Columns("idOrigen").Index).Text = spListadosOtros.ActiveSheet.Cells(filaCatalogos, spListadosOtros.ActiveSheet.Columns("idOrigen").Index).Text
        spVales.ActiveSheet.Cells(spVales.ActiveSheet.ActiveRowIndex, spVales.ActiveSheet.Columns("id").Index).Text = spListadosOtros.ActiveSheet.Cells(filaCatalogos, spListadosOtros.ActiveSheet.Columns("id").Index).Text
        spVales.ActiveSheet.Cells(spVales.ActiveSheet.ActiveRowIndex, spVales.ActiveSheet.Columns("total").Index).Text = spListadosOtros.ActiveSheet.Cells(filaCatalogos, spListadosOtros.ActiveSheet.Columns("total").Index).Text
        spVales.ActiveSheet.SetActiveCell(spVales.ActiveSheet.ActiveRowIndex, spVales.ActiveSheet.Columns("id").Index)
        ControlarSpreadEnter(spVales)

    End Sub

    Private Sub VolverFocoDeListadosOtros()

        pnlCapturaSuperior.Enabled = True
        spVentas.Enabled = True
        AsignarFoco(spVales)
        spListadosOtros.Visible = False

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
        recibo = 1
        vale = 2

    End Enum

#End Region

End Class