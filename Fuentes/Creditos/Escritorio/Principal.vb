Imports System.IO
Imports System.ComponentModel
Imports System.Threading

Public Class Principal

    ' Variables de objetos de entidades.
    Public usuarios As New ALMEntidadesCreditos.Usuarios()
    Public creditos As New ALMEntidadesCreditos.Creditos()
    Public creditosDetalle As New ALMEntidadesCreditos.CreditosDetalle()
    Public creditosAbonos As New ALMEntidadesCreditos.CreditosAbonos()
    Public almacenes As New ALMEntidadesCreditos.Almacenes()
    Public familias As New ALMEntidadesCreditos.Familias()
    Public subFamilias As New ALMEntidadesCreditos.SubFamilias()
    Public articulos As New ALMEntidadesCreditos.Articulos()
    Public unidadesMedidas As New ALMEntidadesCreditos.UnidadesMedidas()
    Public proveedores As New ALMEntidadesCreditos.Proveedores()
    Public clientes As New ALMEntidadesCreditos.Clientes()
    Public metodosPagos As New ALMEntidadesCreditos.MetodosPagos()
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
    Public Shared tipoLetraSpread As String = "Microsoft Sans Serif" : Public Shared tamañoLetraSpread As Integer = 9
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
    Public opcionCatalogoSeleccionada As Integer = 0 : Public opcionPestanaSeleccionada As Integer = -1
    Public filaSeleccionadaCreditos As Integer = -1
    Public esGuardadoValido As Boolean = True
    Public esIzquierdaCapturar As Boolean = False : Public esIzquierdaActualizar As Boolean = False
    Public datosCatalogo As New DataTable
    ' Hilos para carga rapida.
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
        FormatearSpreadCreditos()
        CargarCombosCaptura()
        CargarIdConsecutivo()
        AsignarFoco(txtId)
        CargarComboClientesActualizar()
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

    Private Sub spCreditosCapturar_DialogKey(sender As Object, e As FarPoint.Win.Spread.DialogKeyEventArgs) Handles spCreditosCapturar.DialogKey

        If (e.KeyData = Keys.Enter) Then
            ControlarSpreadEnter(spCreditosCapturar)
        End If

    End Sub

    Private Sub spCreditosCapturar_KeyDown(sender As Object, e As KeyEventArgs) Handles spCreditosCapturar.KeyDown

        Me.Cursor = Cursors.WaitCursor
        If (e.KeyData = Keys.F6) Then ' Eliminar un registro.
            If (MessageBox.Show("Confirmas que deseas eliminar el registro seleccionado?", "Confirmación.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes) Then
                EliminarRegistroDeSpread(spCreditosCapturar)
            End If
        ElseIf (e.KeyData = Keys.Enter) Then ' Validar registros.
            ControlarSpreadEnter(spCreditosCapturar)
        ElseIf (e.KeyData = Keys.F5) Then ' Abrir catalogos.
            CargarCatalogoEnSpread()
        ElseIf (e.KeyData = Keys.Escape) Then
            spCreditosCapturar.ActiveSheet.SetActiveCell(0, 0)
            AsignarFoco(cbMetodosPagos)
        End If
        Me.Cursor = Cursors.Default

    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click

        Me.Cursor = Cursors.WaitCursor
        If (Me.opcionPestanaSeleccionada = OpcionPestana.capturar) Then
            ValidarGuardadoCreditos()
            If (Me.esGuardadoValido) Then
                GuardarEditarCreditos()
            End If
        Else
            ValidarGuardadoCreditosActualizar()
            If (Me.esGuardadoValido) Then
                GuardarEditarCreditosActualizar()
            End If
        End If
        Me.Cursor = Cursors.Default

    End Sub

    Private Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click

        Me.Cursor = Cursors.WaitCursor
        If (Me.opcionPestanaSeleccionada = OpcionPestana.capturar) Then
            EliminarCreditos(True)
        End If
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
        If (Me.opcionCatalogoSeleccionada = OpcionCatalogo.almacen Or Me.opcionCatalogoSeleccionada = OpcionCatalogo.cliente) Then
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
            If (Me.opcionCatalogoSeleccionada = OpcionCatalogo.almacen Or Me.opcionCatalogoSeleccionada = OpcionCatalogo.cliente) Then
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
                CargarCreditos()
            Else
                txtId.Clear()
                LimpiarPantalla()
            End If
        ElseIf (e.KeyData = Keys.Escape) Then
            e.SuppressKeyPress = True
            AsignarFoco(cbAlmacenes)
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

    Private Sub cbAlmacenes_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbAlmacenes.SelectedIndexChanged

        If (Me.estaMostrado) Then
            If (cbAlmacenes.SelectedValue > 0) Then
                CargarIdConsecutivo()
                LimpiarPantalla()
            Else
                cbAlmacenes.SelectedIndex = 0
                txtId.Clear()
            End If
        End If

    End Sub

    Private Sub cbAlmacenes_KeyDown(sender As Object, e As KeyEventArgs) Handles cbAlmacenes.KeyDown

        If (e.KeyData = Keys.Enter) Then
            e.SuppressKeyPress = True
            If (cbAlmacenes.SelectedValue > 0) Then
                CargarIdConsecutivo()
                AsignarFoco(txtId)
                txtId.SelectAll()
            Else
                cbAlmacenes.SelectedIndex = 0
            End If
        ElseIf (e.KeyData = Keys.F5) Then ' Abrir catalogos.
            Me.opcionCatalogoSeleccionada = OpcionCatalogo.almacen
            CargarCatalogoEnOtros()
        End If

    End Sub

    Private Sub cbClientes_KeyDown(sender As Object, e As KeyEventArgs) Handles cbClientes.KeyDown

        If (e.KeyData = Keys.Enter) Then
            e.SuppressKeyPress = True
            If (cbClientes.SelectedValue > 0) Then
                AsignarFoco(cbMetodosPagos)
            Else
                cbClientes.SelectedIndex = 0
            End If
        ElseIf (e.KeyData = Keys.Escape) Then
            e.SuppressKeyPress = True
            AsignarFoco(dtpFecha)
        ElseIf (e.KeyData = Keys.F5) Then ' Abrir catalogos.
            Me.opcionCatalogoSeleccionada = OpcionCatalogo.cliente
            CargarCatalogoEnOtros()
        End If

    End Sub

    Private Sub btnIdAnterior_Click(sender As Object, e As EventArgs) Handles btnIdAnterior.Click

        If (ALMLogicaCreditos.Funciones.ValidarNumeroACero(txtId.Text) > 1) Then
            txtId.Text -= 1
            CargarCreditos()
        End If

    End Sub

    Private Sub btnIdSiguiente_Click(sender As Object, e As EventArgs) Handles btnIdSiguiente.Click

        If (ALMLogicaCreditos.Funciones.ValidarNumeroACero(txtId.Text) >= 1) Then
            txtId.Text += 1
            CargarCreditos()
        End If

    End Sub

    Private Sub btnMostrarOcultar_Click(sender As Object, e As EventArgs) Handles btnMostrarOcultar.Click

        MostrarOcultar()

    End Sub

    Private Sub btnMostrarOcultar_MouseEnter(sender As Object, e As EventArgs) Handles btnMostrarOcultar.MouseEnter

        If (Me.esIzquierdaCapturar) Then
            AsignarTooltips("Mostrar")
        Else
            AsignarTooltips("Ocultar")
        End If

    End Sub

    Private Sub txtBuscarCatalogo_KeyDown(sender As Object, e As KeyEventArgs) Handles txtBuscarCatalogo.KeyDown

        If (e.KeyCode = Keys.Enter) Then
            AsignarFoco(spCatalogos)
        ElseIf (e.KeyCode = Keys.Escape) Then
            VolverFocoDeCatalogos()
        End If

    End Sub

    Private Sub txtBuscarCatalogo_TextChanged(sender As Object, e As EventArgs) Handles txtBuscarCatalogo.TextChanged

        Me.Cursor = Cursors.WaitCursor
        If (Me.opcionCatalogoSeleccionada = OpcionCatalogo.articulo) Then
            BuscarCatalogosRapidoArticulos()
        Else
            BuscarCatalogos()
        End If
        Me.Cursor = Cursors.WaitCursor

    End Sub

    Private Sub chkMostrarDatos_CheckedChanged(sender As Object, e As EventArgs) Handles chkMostrarDetallado.CheckedChanged

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

    Private Sub tcPestanas_SelectedIndexChanged(sender As Object, e As EventArgs) Handles tcPestanas.SelectedIndexChanged

        If (tcPestanas.SelectedIndex = 0) Then
            Me.opcionPestanaSeleccionada = OpcionPestana.capturar
            btnGuardar.Enabled = True
            btnEliminar.Enabled = True
        ElseIf (tcPestanas.SelectedIndex = 1) Then
            Me.opcionPestanaSeleccionada = OpcionPestana.actualizar
            btnEliminar.Enabled = False
            btnGuardar.Enabled = False
            'If (cbClientesActualizar.Items.Count = 0) Then
            '    CargarComboClientesActualizar()
            'End If
        End If

    End Sub

    Private Sub btnMostrarOcultarActualizar_Click(sender As Object, e As EventArgs) Handles btnMostrarOcultarActualizar.Click

        MostrarOcultar()

    End Sub

    Private Sub chkFecha_CheckedChanged(sender As Object, e As EventArgs) Handles chkFecha.CheckedChanged

        If (chkFecha.Checked) Then
            chkFecha.Text = "SI"
        Else
            chkFecha.Text = "NO"
        End If

    End Sub

    Private Sub btnGenerar_Click(sender As Object, e As EventArgs) Handles btnGenerar.Click

        Me.Cursor = Cursors.WaitCursor
        CargarCreditosActualizarSeleccionar()
        Me.Cursor = Cursors.Default

    End Sub

    Private Sub chkMostrarDetalladoActualizar_CheckedChanged(sender As Object, e As EventArgs) Handles chkMostrarDetalladoActualizar.CheckedChanged

        Me.Cursor = Cursors.WaitCursor
        If (Me.estaMostrado) Then
            If (chkMostrarDetalladoActualizar.Checked) Then
                MostrarOcultarColumnasDetalleActualizarSeleccionar(True)
            Else
                MostrarOcultarColumnasDetalleActualizarSeleccionar(False)
            End If
        End If
        Me.Cursor = Cursors.Default

    End Sub

    Private Sub spCreditosActualizarSeleccionar_CellClick(sender As Object, e As FarPoint.Win.Spread.CellClickEventArgs) Handles spCreditosActualizarSeleccionar.CellClick

        Me.Cursor = Cursors.WaitCursor
        If (Not e.ColumnHeader AndAlso Not e.RowHeader) Then
            Me.filaSeleccionadaCreditos = e.Row
            If (Me.filaSeleccionadaCreditos >= 0) Then
                CargarCreditosActualizarGuardar()
            End If
        End If
        Me.Cursor = Cursors.Default

    End Sub

    Private Sub pbMarca_MouseEnter(sender As Object, e As EventArgs) Handles pbMarca.MouseEnter

        AsignarTooltips("Producido por Berry")

    End Sub

    Private Sub cbMetodosPagos_KeyDown(sender As Object, e As KeyEventArgs) Handles cbMetodosPagos.KeyDown

        If (e.KeyData = Keys.Enter) Then
            e.SuppressKeyPress = True
            If (cbMetodosPagos.SelectedValue > 0) Then
                AsignarFoco(spCreditosCapturar)
            Else
                cbMetodosPagos.SelectedIndex = 0
            End If
        End If

    End Sub

    Private Sub spCreditosActualizarGuardar_KeyDown(sender As Object, e As KeyEventArgs) Handles spCreditosActualizarGuardar.KeyDown

        Me.Cursor = Cursors.WaitCursor
        If (e.KeyData = Keys.F6) Then ' Eliminar un registro.
            If (MessageBox.Show("Confirmas que deseas eliminar el registro seleccionado?", "Confirmación.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes) Then
                EliminarRegistroDeSpread(spCreditosActualizarGuardar)
            End If
        ElseIf (e.KeyData = Keys.Enter) Then ' Validar registros.
            ControlarSpreadEnter(spCreditosActualizarGuardar)
        ElseIf (e.KeyData = Keys.Escape) Then ' Regresar.
            spCreditosActualizarGuardar.ActiveSheet.SetActiveCell(0, 0)
            AsignarFoco(spCreditosActualizarSeleccionar)
        End If
        Me.Cursor = Cursors.Default

    End Sub

    Private Sub spCreditosActualizarGuardar_DialogKey(sender As Object, e As FarPoint.Win.Spread.DialogKeyEventArgs) Handles spCreditosActualizarGuardar.DialogKey

        If (e.KeyData = Keys.Enter) Then
            ControlarSpreadEnter(spCreditosActualizarGuardar)
        End If

    End Sub

    Private Sub pnlCapturaSuperior_MouseEnter(sender As Object, e As EventArgs) Handles pnlCapturaSuperior.MouseEnter

        AsignarTooltips("Capturar Datos Generales")

    End Sub

    Private Sub spCreditosCapturar_MouseEnter(sender As Object, e As EventArgs) Handles spCreditosCapturar.MouseEnter

        AsignarTooltips("Capturar Datos Detallados")

    End Sub

    Private Sub pnlPie_MouseEnter(sender As Object, e As EventArgs) Handles pnlPie.MouseEnter

        AsignarTooltips("Opciones")

    End Sub

    Private Sub spCreditosActualizarSeleccionar_MouseEnter(sender As Object, e As EventArgs) Handles spCreditosActualizarSeleccionar.MouseEnter

        AsignarTooltips("Seleccionar")

    End Sub

    Private Sub spCreditosActualizarGuardar_MouseEnter(sender As Object, e As EventArgs) Handles spCreditosActualizarGuardar.MouseEnter

        AsignarTooltips("Capturar Datos Detallados")

    End Sub

    Private Sub pnlFiltradoActualizar_MouseEnter(sender As Object, e As EventArgs) Handles pnlFiltradoActualizar.MouseEnter

        AsignarTooltips("Filtros para Generar los Datos")

    End Sub

#End Region

#Region "Métodos"

#Region "Básicos"

    Private Sub CargarEstilos()

        pnlCapturaSuperior.BackColor = Principal.colorSpreadAreaGris
        pnlTotales.BackColor = Principal.colorSpreadAreaGris
        pnlFiltradoActualizar.BackColor = Principal.colorSpreadAreaGris
        spCreditosCapturar.ActiveSheet.GrayAreaBackColor = Principal.colorSpreadAreaGris
        spCreditosActualizarSeleccionar.ActiveSheet.GrayAreaBackColor = Principal.colorSpreadAreaGris
        spCreditosActualizarGuardar.ActiveSheet.GrayAreaBackColor = Principal.colorSpreadAreaGris
        pnlPie.BackColor = Principal.colorSpreadAreaGris

    End Sub

    Private Sub BuscarCatalogos()

        Dim valorBuscado As String = txtBuscarCatalogo.Text.Replace("á", "a").Replace("é", "e").Replace("í", "i").Replace("ó", "o").Replace("ú", "u")
        For fila = 0 To spCatalogos.ActiveSheet.Rows.Count - 1
            Dim valorSpread As String = ALMLogicaCreditos.Funciones.ValidarLetra(spCatalogos.ActiveSheet.Cells(fila, spCatalogos.ActiveSheet.Columns("id").Index).Text & spCatalogos.ActiveSheet.Cells(fila, spCatalogos.ActiveSheet.Columns("nombre").Index).Text).Replace("á", "a").Replace("é", "e").Replace("í", "i").Replace("ó", "o").Replace("ú", "u")
            If (Me.opcionCatalogoSeleccionada = OpcionCatalogo.articulo) Then
                valorSpread = ALMLogicaCreditos.Funciones.ValidarLetra(spCatalogos.ActiveSheet.Cells(fila, spCatalogos.ActiveSheet.Columns("id").Index).Text & spCatalogos.ActiveSheet.Cells(fila, spCatalogos.ActiveSheet.Columns("nombre").Index).Text & spCatalogos.ActiveSheet.Cells(fila, spCatalogos.ActiveSheet.Columns("codigo").Index).Text & spCatalogos.ActiveSheet.Cells(fila, spCatalogos.ActiveSheet.Columns("pagina").Index).Text & spCatalogos.ActiveSheet.Cells(fila, spCatalogos.ActiveSheet.Columns("color").Index).Text & spCatalogos.ActiveSheet.Cells(fila, spCatalogos.ActiveSheet.Columns("talla").Index).Text & spCatalogos.ActiveSheet.Cells(fila, spCatalogos.ActiveSheet.Columns("modelo").Index).Text & spCatalogos.ActiveSheet.Cells(fila, spCatalogos.ActiveSheet.Columns("codigoInternet").Index).Text).Replace("á", "a").Replace("é", "e").Replace("í", "i").Replace("ó", "o").Replace("ú", "u")
            End If
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

        If (Me.opcionPestanaSeleccionada = OpcionPestana.capturar) Then
            Dim anchoBoton As Integer = btnMostrarOcultar.Width
            Dim espacio As Integer = 1
            If (Not Me.esIzquierdaCapturar) Then
                pnlCapturaSuperior.Left = -pnlCapturaSuperior.Width + anchoBoton
                spCreditosCapturar.Left = anchoBoton + espacio
                spCreditosCapturar.Width = Me.anchoTotal - anchoBoton - espacio - 5
                Me.esIzquierdaCapturar = True
            Else
                pnlCapturaSuperior.Left = 0
                spCreditosCapturar.Left = pnlCapturaSuperior.Width + espacio
                spCreditosCapturar.Width = Me.anchoTotal - pnlCapturaSuperior.Width - espacio - 5
                Me.esIzquierdaCapturar = False
            End If
        ElseIf (Me.opcionPestanaSeleccionada = OpcionPestana.actualizar) Then
            Dim anchoBoton As Integer = btnMostrarOcultarActualizar.Width
            Dim espacio As Integer = 1
            If (Not Me.esIzquierdaActualizar) Then
                pnlFiltradoActualizar.Left = -pnlFiltradoActualizar.Width + anchoBoton
                spCreditosActualizarSeleccionar.Left = anchoBoton + espacio
                spCreditosActualizarSeleccionar.Width = Me.anchoMitad - anchoBoton - espacio - 5
                Me.esIzquierdaActualizar = True
            Else
                pnlFiltradoActualizar.Left = 0
                spCreditosActualizarSeleccionar.Left = pnlFiltradoActualizar.Width + espacio
                spCreditosActualizarSeleccionar.Width = Me.anchoMitad - pnlFiltradoActualizar.Width - espacio - 5
                Me.esIzquierdaActualizar = False
            End If
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
            txtAyuda.Text = "Sección de Ayuda: " & vbNewLine & vbNewLine & "* Teclas básicas: " & vbNewLine & "F5 sirve para mostrar catálogos. " & vbNewLine & "F6 sirve para eliminar un registro únicamente. " & vbNewLine & "F7 sirve para mostrar listados." & vbNewLine & "Escape sirve para ocultar catálogos o listados que se encuentren desplegados. " & vbNewLine & vbNewLine & "* Catálogos o listados desplegados: " & vbNewLine & "Cuando se muestra algún catálogo o listado, al seleccionar alguna opción de este, se va mostrando en tiempo real en la captura de donde se originó. Cuando se le da doble clic en alguna opción o a la tecla escape se oculta dicho catálogo o listado. " & vbNewLine & vbNewLine & "* Datos obligatorios:" & vbNewLine & "Todos los que tengan el simbolo * son estrictamente obligatorios." & vbNewLine & vbNewLine & "* Captura:" & vbNewLine & "* Parte superior/izquierda: " & vbNewLine & "En esta parte se capturarán todos los datos que son generales, tal cual como el número de créditos, fecha, cliente, método de pago, etc." & vbNewLine & "* Parte inferior/derecha: " & vbNewLine & "En esta parte se capturarán todos los artículos correspondientes a ese crédito y otros datos extra que llevará, por ejemplo, cantidad, precio, porcentaje de descuento, etc." & vbNewLine & vbNewLine & "* Existen los botones de guardar/editar y eliminar todo dependiendo lo que se necesite hacer. "
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

        If ((Not ALMLogicaCreditos.Usuarios.accesoTotal) Or (ALMLogicaCreditos.Usuarios.accesoTotal = 0) Or (ALMLogicaCreditos.Usuarios.accesoTotal = False)) Then
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
        tp.SetToolTip(Me.pbMarca, "Producido por Berry")

    End Sub

    Private Sub AsignarTooltips(ByVal texto As String)

        lblDescripcionTooltip.Text = texto

    End Sub

    Private Sub ConfigurarConexiones()

        If (Me.esDesarrollo) Then
            ALMLogicaCreditos.Directorios.id = 2
            ALMLogicaCreditos.Directorios.instanciaSql = "BERRY1-DELL\SQLEXPRESS2008"
            ALMLogicaCreditos.Directorios.usuarioSql = "AdminBerry"
            ALMLogicaCreditos.Directorios.contrasenaSql = "@berry2017"
            pnlEncabezado.BackColor = Color.DarkRed
        Else
            ALMLogicaCreditos.Directorios.ObtenerParametros()
            ALMLogicaCreditos.Usuarios.ObtenerParametros()
        End If
        ALMLogicaCreditos.Programas.bdCatalogo = "Catalogo" & ALMLogicaCreditos.Directorios.id
        ALMLogicaCreditos.Programas.bdConfiguracion = "Configuracion" & ALMLogicaCreditos.Directorios.id
        ALMLogicaCreditos.Programas.bdAlmacen = "Almacen" & ALMLogicaCreditos.Directorios.id
        ALMEntidadesCreditos.BaseDatos.ECadenaConexionCatalogo = ALMLogicaCreditos.Programas.bdCatalogo
        ALMEntidadesCreditos.BaseDatos.ECadenaConexionConfiguracion = ALMLogicaCreditos.Programas.bdConfiguracion
        ALMEntidadesCreditos.BaseDatos.ECadenaConexionAlmacen = ALMLogicaCreditos.Programas.bdAlmacen
        ALMEntidadesCreditos.BaseDatos.AbrirConexionCatalogo()
        ALMEntidadesCreditos.BaseDatos.AbrirConexionConfiguracion()
        ALMEntidadesCreditos.BaseDatos.AbrirConexionAlmacen()
        ConsultarInformacionUsuario()
        CargarPrefijoBaseDatosAlmacen()

    End Sub

    Private Sub CargarPrefijoBaseDatosAlmacen()

        ALMLogicaCreditos.Programas.prefijoBaseDatosAlmacen = Me.prefijoBaseDatosAlmacen

    End Sub

    Private Sub ConsultarInformacionUsuario()

        Dim lista As New List(Of ALMEntidadesCreditos.Usuarios)
        usuarios.EId = ALMLogicaCreditos.Usuarios.id
        lista = usuarios.ObtenerListado()
        If (lista.Count = 1) Then
            ALMLogicaCreditos.Usuarios.id = lista(0).EId
            ALMLogicaCreditos.Usuarios.nombre = lista(0).ENombre
            ALMLogicaCreditos.Usuarios.contrasena = lista(0).EContrasena
            ALMLogicaCreditos.Usuarios.nivel = lista(0).ENivel
            ALMLogicaCreditos.Usuarios.accesoTotal = lista(0).EAccesoTotal
        End If

    End Sub

    Private Sub CargarEncabezadosTitulos()

        lblEncabezadoPrograma.Text = "Programa: " & Me.Text
        lblEncabezadoEmpresa.Text = "Directorio: " & ALMLogicaCreditos.Directorios.nombre
        lblEncabezadoUsuario.Text = "Usuario: " & ALMLogicaCreditos.Usuarios.nombre
        Me.Text = "Programa:  " & Me.nombreEstePrograma & "              Directorio:  " & ALMLogicaCreditos.Directorios.nombre & "              Usuario:  " & ALMLogicaCreditos.Usuarios.nombre
        hiloEncabezadosTitulos.Abort()

    End Sub

    Private Sub AbrirPrograma(nombre As String, salir As Boolean)

        If (Me.esDesarrollo) Then
            Exit Sub
        End If
        ejecutarProgramaPrincipal.UseShellExecute = True
        ejecutarProgramaPrincipal.FileName = nombre & Convert.ToString(".exe")
        ejecutarProgramaPrincipal.WorkingDirectory = Application.StartupPath
        ejecutarProgramaPrincipal.Arguments = ALMLogicaCreditos.Directorios.id.ToString().Trim().Replace(" ", "|") & " " & ALMLogicaCreditos.Directorios.nombre.ToString().Trim().Replace(" ", "|") & " " & ALMLogicaCreditos.Directorios.descripcion.ToString().Trim().Replace(" ", "|") & " " & ALMLogicaCreditos.Directorios.rutaLogo.ToString().Trim().Replace(" ", "|") & " " & ALMLogicaCreditos.Directorios.esPredeterminado.ToString().Trim().Replace(" ", "|") & " " & ALMLogicaCreditos.Directorios.instanciaSql.ToString().Trim().Replace(" ", "|") & " " & ALMLogicaCreditos.Directorios.usuarioSql.ToString().Trim().Replace(" ", "|") & " " & ALMLogicaCreditos.Directorios.contrasenaSql.ToString().Trim().Replace(" ", "|") & " " & "Aquí terminan los de directorios, indice 9 ;)".Replace(" ", "|") & " " & ALMLogicaCreditos.Usuarios.id.ToString().Trim().Replace(" ", "|") & " " & "Aquí terminan los de usuario, indice 11 ;)".Replace(" ", "|")
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
        Me.arriba = spCreditosCapturar.Top
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

#Region "Captura"

    Private Sub CargarCombosCaptura()

        CargarComboAlmacenes()
        CargarComboClientes()
        CargarComboMetodosPago()

    End Sub

    Private Sub LimpiarPantalla()

        If (Me.opcionPestanaSeleccionada = OpcionPestana.capturar) Then
            For Each c As Control In pnlCapturaSuperior.Controls
                If ((Not TypeOf c Is Button) AndAlso (Not TypeOf c Is Label)) Then
                    c.BackColor = Principal.colorCaptura
                End If
            Next
            For fila = 0 To spCreditosCapturar.ActiveSheet.Rows.Count - 1
                For columna = 0 To spCreditosCapturar.ActiveSheet.Columns.Count - 1
                    spCreditosCapturar.ActiveSheet.Cells(fila, columna).BackColor = Principal.colorCaptura
                Next
            Next
            If (Not chkMantenerDatos.Checked) Then
                dtpFecha.Value = Today
            End If
            cbClientes.SelectedIndex = 0
            cbMetodosPagos.SelectedIndex = 0
            spCreditosCapturar.ActiveSheet.DataSource = Nothing
            spCreditosCapturar.ActiveSheet.Rows.Count = 1
            spCreditosCapturar.ActiveSheet.SetActiveCell(0, 0)
            LimpiarSpread(spCreditosCapturar)
        ElseIf (Me.opcionPestanaSeleccionada = OpcionPestana.actualizar) Then
            For fila = 0 To spCreditosActualizarGuardar.ActiveSheet.Rows.Count - 1
                For columna = 0 To spCreditosActualizarGuardar.ActiveSheet.Columns.Count - 1
                    spCreditosActualizarGuardar.ActiveSheet.Cells(fila, columna).BackColor = Principal.colorCaptura
                Next
            Next
            For fila = 0 To spCreditosActualizarGuardar.ActiveSheet.ColumnFooter.Rows.Count - 1
                For columna = 0 To spCreditosActualizarGuardar.ActiveSheet.ColumnFooter.Columns.Count - 1
                    spCreditosActualizarGuardar.ActiveSheet.ColumnFooter.Cells(fila, columna).BackColor = Principal.colorSpreadTotal
                Next
            Next
        End If

    End Sub

    Private Sub LimpiarSpread(ByVal spread As FarPoint.Win.Spread.FpSpread)

        spread.ActiveSheet.ClearRange(0, 0, spread.ActiveSheet.Rows.Count, spread.ActiveSheet.Columns.Count, True)

    End Sub

    Private Sub CargarComboAlmacenes()

        cbAlmacenes.DataSource = almacenes.ObtenerListadoCombos()
        cbAlmacenes.DisplayMember = "IdNombre"
        cbAlmacenes.ValueMember = "Id"
        cbAlmacenes.SelectedValue = 1
        cbAlmacenes.Enabled = False

    End Sub

    Private Sub CargarComboClientes()

        Dim datos As New DataTable
        datos = clientes.ObtenerListadoCombos(False)
        cbClientes.DataSource = datos
        cbClientes.DisplayMember = "IdNombre"
        cbClientes.ValueMember = "Id"

    End Sub

    Private Sub CargarComboMetodosPago()

        cbMetodosPagos.DataSource = metodosPagos.ObtenerListadoCombos()
        cbMetodosPagos.DisplayMember = "IdNombre"
        cbMetodosPagos.ValueMember = "Id"

    End Sub

    Private Sub CargarComboClientesActualizar()

        cbClientesActualizar.DataSource = clientes.ObtenerListadoCombos(True)
        cbClientesActualizar.DisplayMember = "Nombre"
        cbClientesActualizar.ValueMember = "Id"

    End Sub

    Private Sub FormatearSpread()

        ' Se cargan tipos de datos de spread.
        CargarTiposDeDatos()
        ' Se cargan las opciones generales. 
        pnlCatalogos.Visible = False
        spCreditosCapturar.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Seashell
        spCreditosActualizarSeleccionar.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Seashell
        spCreditosActualizarGuardar.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Seashell
        spCatalogos.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Midnight
        spCreditosCapturar.ActiveSheet.GrayAreaBackColor = Principal.colorSpreadAreaGris
        spCreditosActualizarSeleccionar.ActiveSheet.GrayAreaBackColor = Principal.colorSpreadAreaGris
        spCreditosActualizarGuardar.ActiveSheet.GrayAreaBackColor = Principal.colorSpreadAreaGris
        spCreditosCapturar.Font = New Font(Principal.tipoLetraSpread, Principal.tamañoLetraSpread, FontStyle.Regular)
        spCreditosActualizarSeleccionar.Font = New Font(Principal.tipoLetraSpread, Principal.tamañoLetraSpread, FontStyle.Regular)
        spCreditosActualizarGuardar.Font = New Font(Principal.tipoLetraSpread, Principal.tamañoLetraSpread, FontStyle.Regular)
        spCatalogos.Font = New Font(Principal.tipoLetraSpread, Principal.tamañoLetraSpread, FontStyle.Regular)
        spCreditosCapturar.ActiveSheet.ColumnHeader.Rows(0).Height = Principal.alturaFilasEncabezadosGrandesSpread
        spCreditosActualizarSeleccionar.ActiveSheet.ColumnHeader.Rows(0).Height = Principal.alturaFilasEncabezadosGrandesSpread
        spCreditosActualizarGuardar.ActiveSheet.ColumnHeader.Rows(0).Height = Principal.alturaFilasEncabezadosGrandesSpread
        spCatalogos.ActiveSheet.ColumnHeader.Rows(0).Height = Principal.alturaFilasEncabezadosGrandesSpread
        spCreditosCapturar.ActiveSheet.Rows(-1).Height = Principal.alturaFilasSpread
        spCreditosActualizarSeleccionar.ActiveSheet.Rows(-1).Height = Principal.alturaFilasSpread
        spCreditosActualizarGuardar.ActiveSheet.Rows(-1).Height = Principal.alturaFilasSpread
        spCatalogos.ActiveSheet.Rows(-1).Height = Principal.alturaFilasSpread
        spCreditosCapturar.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        spCreditosCapturar.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        spCreditosActualizarSeleccionar.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        spCreditosActualizarSeleccionar.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        spCreditosActualizarGuardar.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        spCreditosActualizarGuardar.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        spCatalogos.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never
        spCatalogos.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Always
        spCreditosCapturar.EditModeReplace = True
        spCreditosActualizarGuardar.EditModeReplace = True

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
        If (spread.Name = spCreditosCapturar.Name) Then
            Dim fila As Integer = 0
            If (columnaActiva = spread.ActiveSheet.Columns("total").Index) Then
                spread.ActiveSheet.Rows.Count += 1
            End If
            If (columnaActiva = spCreditosCapturar.ActiveSheet.Columns("idProveedor").Index) Then
                fila = spCreditosCapturar.ActiveSheet.ActiveRowIndex
                Dim idProveedor As Integer = ALMLogicaCreditos.Funciones.ValidarNumeroACero(spCreditosCapturar.ActiveSheet.Cells(fila, spCreditosCapturar.ActiveSheet.Columns("idProveedor").Index).Value)
                proveedores.EId = idProveedor
                If (idProveedor > 0) Then
                    Dim datosProveedor As New DataTable
                    datosProveedor = proveedores.ObtenerListadoReporte()
                    If (datosProveedor.Rows.Count > 0) Then
                        spCreditosCapturar.ActiveSheet.Cells(fila, spCreditosCapturar.ActiveSheet.Columns("nombreProveedor").Index).Value = datosProveedor.Rows(0).Item("Nombre")
                        spCreditosCapturar.ActiveSheet.SetActiveCell(fila, spCreditosCapturar.ActiveSheet.ActiveColumnIndex + 1)
                    Else
                        spCreditosCapturar.ActiveSheet.Cells(fila, spCreditosCapturar.ActiveSheet.Columns("idProveedor").Index, fila, spCreditosCapturar.ActiveSheet.Columns("nombreProveedor").Index).Value = String.Empty
                        spCreditosCapturar.ActiveSheet.ClearSelection()
                        spCreditosCapturar.ActiveSheet.SetActiveCell(fila, spCreditosCapturar.ActiveSheet.ActiveColumnIndex - 1)
                    End If
                Else
                    spCreditosCapturar.ActiveSheet.Cells(fila, spCreditosCapturar.ActiveSheet.Columns("idProveedor").Index, fila, spCreditosCapturar.ActiveSheet.Columns("nombreProveedor").Index).Value = String.Empty
                    spCreditosCapturar.ActiveSheet.ClearSelection()
                    spCreditosCapturar.ActiveSheet.SetActiveCell(fila, spCreditosCapturar.ActiveSheet.ActiveColumnIndex - 1)
                End If
            ElseIf (columnaActiva = spCreditosCapturar.ActiveSheet.Columns("idFamilia").Index) Then
                fila = spCreditosCapturar.ActiveSheet.ActiveRowIndex
                Dim idAlmacen As Integer = ALMLogicaCreditos.Funciones.ValidarNumeroACero(ALMLogicaCreditos.Funciones.ValidarNumeroACero(cbAlmacenes.SelectedValue))
                Dim idFamilia As Integer = ALMLogicaCreditos.Funciones.ValidarNumeroACero(spCreditosCapturar.ActiveSheet.Cells(fila, spCreditosCapturar.ActiveSheet.Columns("idFamilia").Index).Value)
                familias.EIdAlmacen = idAlmacen
                familias.EId = idFamilia
                If (idAlmacen > 0 And idFamilia > 0) Then
                    Dim lista As New List(Of ALMEntidadesCreditos.Familias)
                    lista = familias.ObtenerListado()
                    If (lista.Count = 1) Then
                        spCreditosCapturar.ActiveSheet.Cells(fila, spCreditosCapturar.ActiveSheet.Columns("nombreFamilia").Index).Value = lista(0).ENombre
                        spCreditosCapturar.ActiveSheet.SetActiveCell(fila, spCreditosCapturar.ActiveSheet.ActiveColumnIndex + 1)
                    Else
                        spCreditosCapturar.ActiveSheet.Cells(fila, spCreditosCapturar.ActiveSheet.Columns("idFamilia").Index, fila, spCreditosCapturar.ActiveSheet.Columns("nombreFamilia").Index).Value = String.Empty
                        spCreditosCapturar.ActiveSheet.SetActiveCell(fila, spCreditosCapturar.ActiveSheet.ActiveColumnIndex - 1)
                    End If
                Else
                    spCreditosCapturar.ActiveSheet.Cells(fila, spCreditosCapturar.ActiveSheet.Columns("idFamilia").Index, fila, spCreditosCapturar.ActiveSheet.Columns("nombreFamilia").Index).Value = String.Empty
                    spCreditosCapturar.ActiveSheet.ClearSelection()
                    spCreditosCapturar.ActiveSheet.SetActiveCell(fila, spCreditosCapturar.ActiveSheet.ActiveColumnIndex - 1)
                End If
            ElseIf (columnaActiva = spCreditosCapturar.ActiveSheet.Columns("idSubFamilia").Index) Then
                fila = spCreditosCapturar.ActiveSheet.ActiveRowIndex
                Dim idAlmacen As Integer = ALMLogicaCreditos.Funciones.ValidarNumeroACero(cbAlmacenes.SelectedValue)
                Dim idFamilia As Integer = ALMLogicaCreditos.Funciones.ValidarNumeroACero(spCreditosCapturar.ActiveSheet.Cells(fila, spCreditosCapturar.ActiveSheet.Columns("idFamilia").Index).Value)
                Dim idSubFamilia As Integer = ALMLogicaCreditos.Funciones.ValidarNumeroACero(spCreditosCapturar.ActiveSheet.Cells(fila, spCreditosCapturar.ActiveSheet.Columns("idSubFamilia").Index).Value)
                subFamilias.EIdAlmacen = idAlmacen
                subFamilias.EIdFamilia = idFamilia
                subFamilias.EId = idSubFamilia
                If (idAlmacen > 0 And idFamilia > 0 And idSubFamilia > 0) Then
                    Dim lista As New List(Of ALMEntidadesCreditos.SubFamilias)
                    lista = subFamilias.ObtenerListado()
                    If (lista.Count = 1) Then
                        spCreditosCapturar.ActiveSheet.Cells(fila, spCreditosCapturar.ActiveSheet.Columns("nombreSubFamilia").Index).Value = lista(0).ENombre
                        spCreditosCapturar.ActiveSheet.SetActiveCell(fila, spCreditosCapturar.ActiveSheet.ActiveColumnIndex + 1)
                    Else
                        spCreditosCapturar.ActiveSheet.Cells(fila, spCreditosCapturar.ActiveSheet.Columns("idSubFamilia").Index, fila, spCreditosCapturar.ActiveSheet.Columns("nombreSubFamilia").Index).Value = String.Empty
                        spCreditosCapturar.ActiveSheet.SetActiveCell(fila, spCreditosCapturar.ActiveSheet.ActiveColumnIndex - 1)
                    End If
                Else
                    spCreditosCapturar.ActiveSheet.Cells(fila, spCreditosCapturar.ActiveSheet.Columns("idSubFamilia").Index, fila, spCreditosCapturar.ActiveSheet.Columns("nombreSubFamilia").Index).Value = String.Empty
                    spCreditosCapturar.ActiveSheet.SetActiveCell(fila, spCreditosCapturar.ActiveSheet.ActiveColumnIndex - 1)
                End If
            ElseIf (columnaActiva = spCreditosCapturar.ActiveSheet.Columns("idArticulo").Index) Then
                fila = spCreditosCapturar.ActiveSheet.ActiveRowIndex
                Dim idAlmacen As Integer = ALMLogicaCreditos.Funciones.ValidarNumeroACero(cbAlmacenes.SelectedValue)
                Dim idFamilia As Integer = ALMLogicaCreditos.Funciones.ValidarNumeroACero(spCreditosCapturar.ActiveSheet.Cells(fila, spCreditosCapturar.ActiveSheet.Columns("idFamilia").Index).Value)
                Dim idSubFamilia As Integer = ALMLogicaCreditos.Funciones.ValidarNumeroACero(spCreditosCapturar.ActiveSheet.Cells(fila, spCreditosCapturar.ActiveSheet.Columns("idSubFamilia").Index).Value)
                Dim idArticulo As Integer = ALMLogicaCreditos.Funciones.ValidarNumeroACero(spCreditosCapturar.ActiveSheet.Cells(fila, spCreditosCapturar.ActiveSheet.Columns("idArticulo").Index).Value)
                articulos.EIdAlmacen = idAlmacen
                articulos.EIdFamilia = idFamilia
                articulos.EIdSubFamilia = idSubFamilia
                articulos.EId = idArticulo
                If (idAlmacen > 0 And idFamilia > 0 And idSubFamilia > 0 And idArticulo > 0) Then
                    For indice = 0 To spCreditosCapturar.ActiveSheet.Rows.Count - 1 ' Se valida que no se repitan los articulos.
                        Dim idArticuloLocal As Integer = ALMLogicaCreditos.Funciones.ValidarNumeroACero(spCreditosCapturar.ActiveSheet.Cells(indice, spCreditosCapturar.ActiveSheet.Columns("idArticulo").Index).Text)
                        Dim idSubFamiliaLocal As Integer = ALMLogicaCreditos.Funciones.ValidarNumeroACero(spCreditosCapturar.ActiveSheet.Cells(indice, spCreditosCapturar.ActiveSheet.Columns("idSubFamilia").Index).Text)
                        Dim idFamiliaLocal As Integer = ALMLogicaCreditos.Funciones.ValidarNumeroACero(spCreditosCapturar.ActiveSheet.Cells(indice, spCreditosCapturar.ActiveSheet.Columns("idFamilia").Index).Text)
                        If (idArticuloLocal > 0 And idFamiliaLocal > 0 And idSubFamiliaLocal > 0) Then
                            If (idArticuloLocal = idArticulo And idSubFamiliaLocal = idSubFamilia And idFamiliaLocal = idFamilia And indice <> fila) Then
                                spCreditosCapturar.ActiveSheet.Cells(fila, spCreditosCapturar.ActiveSheet.Columns("idArticulo").Index).Value = String.Empty
                                spCreditosCapturar.ActiveSheet.ClearRange(fila, spCreditosCapturar.ActiveSheet.Columns("idArticulo").Index, 1, spCreditosCapturar.ActiveSheet.Columns.Count - 1, True)
                                spCreditosCapturar.ActiveSheet.SetActiveCell(fila, spCreditosCapturar.ActiveSheet.ActiveColumnIndex - 1)
                                Exit Sub
                            End If
                        End If
                    Next
                    Dim datos As New DataTable
                    datos = articulos.ObtenerListado()
                    If (datos.Rows.Count > 0) Then
                        spCreditosCapturar.ActiveSheet.Cells(fila, spCreditosCapturar.ActiveSheet.Columns("nombreArticulo").Index).Value = datos.Rows(0).Item("Nombre")
                        spCreditosCapturar.ActiveSheet.Cells(fila, spCreditosCapturar.ActiveSheet.Columns("codigo").Index).Value = datos.Rows(0).Item("Codigo")
                        spCreditosCapturar.ActiveSheet.Cells(fila, spCreditosCapturar.ActiveSheet.Columns("pagina").Index).Value = datos.Rows(0).Item("Pagina")
                        spCreditosCapturar.ActiveSheet.Cells(fila, spCreditosCapturar.ActiveSheet.Columns("color").Index).Value = datos.Rows(0).Item("Color")
                        spCreditosCapturar.ActiveSheet.Cells(fila, spCreditosCapturar.ActiveSheet.Columns("talla").Index).Value = datos.Rows(0).Item("Talla")
                        spCreditosCapturar.ActiveSheet.Cells(fila, spCreditosCapturar.ActiveSheet.Columns("modelo").Index).Value = datos.Rows(0).Item("Modelo")
                        spCreditosCapturar.ActiveSheet.Cells(fila, spCreditosCapturar.ActiveSheet.Columns("codigoInternet").Index).Value = datos.Rows(0).Item("CodigoInternet")
                        spCreditosCapturar.ActiveSheet.Cells(fila, spCreditosCapturar.ActiveSheet.Columns("precio").Index).Value = datos.Rows(0).Item("Precio")
                        spCreditosCapturar.ActiveSheet.Cells(fila, spCreditosCapturar.ActiveSheet.Columns("idProveedor").Index).Value = datos.Rows(0).Item("IdProveedor")
                        spCreditosCapturar.ActiveSheet.Cells(fila, spCreditosCapturar.ActiveSheet.Columns("nombreProveedor").Index).Value = datos.Rows(0).Item("NombreProveedor")
                        Dim datos2 As New DataTable
                        Dim idMedida As Integer = datos.Rows(0).Item("IdUnidadMedida")
                        If (idMedida > 0) Then
                            unidadesMedidas.EId = idMedida
                            datos2 = unidadesMedidas.ObtenerListado()
                            If (datos2.Rows.Count > 0) Then
                                spCreditosCapturar.ActiveSheet.Cells(fila, spCreditosCapturar.ActiveSheet.Columns("nombreUnidadMedida").Index).Value = datos2.Rows(0).Item("Nombre")
                            End If
                        End If
                        spCreditosCapturar.ActiveSheet.SetActiveCell(fila, spCreditosCapturar.ActiveSheet.ActiveColumnIndex + 2)
                    Else
                        spCreditosCapturar.ActiveSheet.Cells(fila, spCreditosCapturar.ActiveSheet.Columns("idArticulo").Index, fila, spCreditosCapturar.ActiveSheet.Columns("nombreUnidadMedida").Index).Value = String.Empty
                        spCreditosCapturar.ActiveSheet.SetActiveCell(fila, spCreditosCapturar.ActiveSheet.ActiveColumnIndex - 1)
                    End If
                Else
                    spCreditosCapturar.ActiveSheet.Cells(fila, spCreditosCapturar.ActiveSheet.Columns("idArticulo").Index, fila, spCreditosCapturar.ActiveSheet.Columns("nombreUnidadMedida").Index).Value = String.Empty
                    spCreditosCapturar.ActiveSheet.SetActiveCell(fila, spCreditosCapturar.ActiveSheet.ActiveColumnIndex - 1)
                End If
            ElseIf (columnaActiva = spCreditosCapturar.ActiveSheet.Columns("cantidad").Index) Then
                fila = spCreditosCapturar.ActiveSheet.ActiveRowIndex
                Dim cantidad As Integer = ALMLogicaCreditos.Funciones.ValidarNumeroACero(spCreditosCapturar.ActiveSheet.Cells(fila, spCreditosCapturar.ActiveSheet.Columns("cantidad").Index).Value)
                If (cantidad > 0) Then
                    Dim precio As Double = ALMLogicaCreditos.Funciones.ValidarNumeroACero(spCreditosCapturar.ActiveSheet.Cells(fila, spCreditosCapturar.ActiveSheet.Columns("precio").Index).Text)
                    spCreditosCapturar.ActiveSheet.Cells(fila, spCreditosCapturar.ActiveSheet.Columns("subtotal").Index).Value = cantidad * precio
                    Dim descuento As Double = ALMLogicaCreditos.Funciones.ValidarNumeroACero(spCreditosCapturar.ActiveSheet.Cells(fila, spCreditosCapturar.ActiveSheet.Columns("descuento").Index).Text)
                    spCreditosCapturar.ActiveSheet.Cells(fila, spCreditosCapturar.ActiveSheet.Columns("total").Index).Value = (cantidad * precio) - descuento
                Else
                    spCreditosCapturar.ActiveSheet.Cells(fila, spCreditosCapturar.ActiveSheet.Columns("cantidad").Index).Value = String.Empty
                    spCreditosCapturar.ActiveSheet.SetActiveCell(fila, spCreditosCapturar.ActiveSheet.ActiveColumnIndex - 1)
                End If
            ElseIf (columnaActiva = spCreditosCapturar.ActiveSheet.Columns("precio").Index) Then
                fila = spCreditosCapturar.ActiveSheet.ActiveRowIndex
                Dim cantidad As Integer = ALMLogicaCreditos.Funciones.ValidarNumeroACero(spCreditosCapturar.ActiveSheet.Cells(fila, spCreditosCapturar.ActiveSheet.Columns("cantidad").Index).Value)
                Dim precio As Double = ALMLogicaCreditos.Funciones.ValidarNumeroACero(spCreditosCapturar.ActiveSheet.Cells(fila, spCreditosCapturar.ActiveSheet.Columns("precio").Index).Value)
                Dim descuento As Double = ALMLogicaCreditos.Funciones.ValidarNumeroACero(spCreditosCapturar.ActiveSheet.Cells(fila, spCreditosCapturar.ActiveSheet.Columns("descuento").Index).Value)
                If (cantidad > 0) Then
                    spCreditosCapturar.ActiveSheet.Cells(fila, spCreditosCapturar.ActiveSheet.Columns("total").Index).Value = cantidad * precio
                    spCreditosCapturar.ActiveSheet.Cells(fila, spCreditosCapturar.ActiveSheet.Columns("total").Index).Value = (cantidad * precio) - descuento
                ElseIf (precio = 0) Then
                    spCreditosCapturar.ActiveSheet.Cells(fila, spCreditosCapturar.ActiveSheet.Columns("precio").Index).Value = 0
                Else
                    spCreditosCapturar.ActiveSheet.Cells(fila, spCreditosCapturar.ActiveSheet.Columns("precio").Index).Value = 0
                    spCreditosCapturar.ActiveSheet.SetActiveCell(fila, spCreditosCapturar.ActiveSheet.ActiveColumnIndex - 1)
                End If
            ElseIf (columnaActiva = spCreditosCapturar.ActiveSheet.Columns("subtotal").Index) Then
                fila = spCreditosCapturar.ActiveSheet.ActiveRowIndex
                Dim cantidad As Integer = ALMLogicaCreditos.Funciones.ValidarNumeroACero(spCreditosCapturar.ActiveSheet.Cells(fila, spCreditosCapturar.ActiveSheet.Columns("cantidad").Index).Value)
                Dim subtotal As Double = ALMLogicaCreditos.Funciones.ValidarNumeroACero(spCreditosCapturar.ActiveSheet.Cells(fila, spCreditosCapturar.ActiveSheet.Columns("subtotal").Index).Value)
                If (cantidad > 0) Then
                    spCreditosCapturar.ActiveSheet.Cells(fila, spCreditosCapturar.ActiveSheet.Columns("precio").Index).Value = subtotal / cantidad
                Else
                    spCreditosCapturar.ActiveSheet.Cells(fila, spCreditosCapturar.ActiveSheet.Columns("subtotal").Index).Value = 0
                    spCreditosCapturar.ActiveSheet.SetActiveCell(fila, spCreditosCapturar.ActiveSheet.ActiveColumnIndex - 1)
                End If
            ElseIf (columnaActiva = spCreditosCapturar.ActiveSheet.Columns("porcentajeDescuento").Index) Then
                fila = spCreditosCapturar.ActiveSheet.ActiveRowIndex
                Dim porcentajeDescuento As Double = ALMLogicaCreditos.Funciones.ValidarNumeroACero(spCreditosCapturar.ActiveSheet.Cells(fila, spCreditosCapturar.ActiveSheet.Columns("porcentajeDescuento").Index).Value)
                Dim subtotal As Double = ALMLogicaCreditos.Funciones.ValidarNumeroACero(spCreditosCapturar.ActiveSheet.Cells(fila, spCreditosCapturar.ActiveSheet.Columns("subtotal").Index).Value)
                If (porcentajeDescuento >= 0 And porcentajeDescuento <= 100) Then
                    If (porcentajeDescuento = 0) Then
                        spCreditosCapturar.ActiveSheet.Cells(fila, spCreditosCapturar.ActiveSheet.Columns("porcentajeDescuento").Index).Value = 0
                    End If
                    Dim descuento As Double = (subtotal / 100) * porcentajeDescuento
                    Dim total As Double = subtotal - descuento
                    If (total >= 0) Then
                        spCreditosCapturar.ActiveSheet.Cells(fila, spCreditosCapturar.ActiveSheet.Columns("descuento").Index).Value = descuento
                        spCreditosCapturar.ActiveSheet.Cells(fila, spCreditosCapturar.ActiveSheet.Columns("total").Index).Value = total
                    Else
                        spCreditosCapturar.ActiveSheet.Cells(fila, spCreditosCapturar.ActiveSheet.Columns("porcentajeDescuento").Index).Value = 0
                        spCreditosCapturar.ActiveSheet.SetActiveCell(fila, spCreditosCapturar.ActiveSheet.ActiveColumnIndex - 1)
                    End If
                Else
                    spCreditosCapturar.ActiveSheet.Cells(fila, spCreditosCapturar.ActiveSheet.Columns("porcentajeDescuento").Index).Value = 0
                End If
            ElseIf (columnaActiva = spCreditosCapturar.ActiveSheet.Columns("descuento").Index) Then
                fila = spCreditosCapturar.ActiveSheet.ActiveRowIndex
                Dim descuento As Double = ALMLogicaCreditos.Funciones.ValidarNumeroACero(spCreditosCapturar.ActiveSheet.Cells(fila, spCreditosCapturar.ActiveSheet.Columns("descuento").Index).Value)
                Dim subtotal As Double = ALMLogicaCreditos.Funciones.ValidarNumeroACero(spCreditosCapturar.ActiveSheet.Cells(fila, spCreditosCapturar.ActiveSheet.Columns("subtotal").Index).Value)
                If (descuento > 0) Then
                    Dim porcentajeDescuento As Double = (descuento / subtotal) * 100
                    Dim total As Double = subtotal - descuento
                    If (total >= 0) Then
                        spCreditosCapturar.ActiveSheet.Cells(fila, spCreditosCapturar.ActiveSheet.Columns("porcentajeDescuento").Index).Value = porcentajeDescuento
                        spCreditosCapturar.ActiveSheet.Cells(fila, spCreditosCapturar.ActiveSheet.Columns("total").Index).Value = total
                    Else
                        spCreditosCapturar.ActiveSheet.Cells(fila, spCreditosCapturar.ActiveSheet.Columns("descuento").Index).Value = 0
                        spCreditosCapturar.ActiveSheet.SetActiveCell(fila, spCreditosCapturar.ActiveSheet.ActiveColumnIndex - 1)
                    End If
                Else
                    spCreditosCapturar.ActiveSheet.Cells(fila, spCreditosCapturar.ActiveSheet.Columns("descuento").Index).Value = 0
                End If
            End If
            CalcularTotalesCreditos()
        ElseIf (spread.Name = spCreditosActualizarGuardar.Name) Then
            Dim fila As Integer = 0
            If (columnaActiva = spread.ActiveSheet.Columns.Count - 1) Then
                spread.ActiveSheet.Rows.Count += 1
            End If
            If (columnaActiva = spCreditosActualizarGuardar.ActiveSheet.Columns("fecha").Index) Then
                fila = spCreditosActualizarGuardar.ActiveSheet.ActiveRowIndex
                Dim fecha As String = ALMLogicaCreditos.Funciones.ValidarLetra(spCreditosActualizarGuardar.ActiveSheet.Cells(fila, spCreditosActualizarGuardar.ActiveSheet.Columns("fecha").Index).Value)
                If (String.IsNullOrEmpty(fecha)) Then
                    spCreditosActualizarGuardar.ActiveSheet.Cells(fila, spCreditosActualizarGuardar.ActiveSheet.Columns("fecha").Index).Value = Today
                End If
            End If
            CalcularTotalesCreditosActualizar()
        End If

    End Sub

    Private Sub CalcularTotalesCreditos()

        Dim subTotal As Double = 0
        Dim descuento As Double = 0
        Dim total As Double = 0
        For fila = 0 To spCreditosCapturar.ActiveSheet.Rows.Count - 1
            subTotal += ALMLogicaCreditos.Funciones.ValidarNumeroACero(spCreditosCapturar.ActiveSheet.Cells(fila, spCreditosCapturar.ActiveSheet.Columns("subtotal").Index).Value)
            descuento += ALMLogicaCreditos.Funciones.ValidarNumeroACero(spCreditosCapturar.ActiveSheet.Cells(fila, spCreditosCapturar.ActiveSheet.Columns("descuento").Index).Value)
            total += ALMLogicaCreditos.Funciones.ValidarNumeroACero(spCreditosCapturar.ActiveSheet.Cells(fila, spCreditosCapturar.ActiveSheet.Columns("total").Index).Value)
        Next
        txtSubTotal.Text = subTotal.ToString("###,###.00")
        txtDescuento.Text = descuento.ToString("###,###.00")
        txtTotal.Text = total.ToString("###,###.00")

    End Sub

    Private Sub CargarIdConsecutivo()

        Dim idMaximo As Integer = creditos.ObtenerMaximoId()
        txtId.Text = idMaximo
        Me.opcionPestanaSeleccionada = OpcionPestana.capturar

    End Sub

    Private Sub CargarDatosEnSpreadDeCatalogos(ByVal filaCatalogos As Integer)

        If (spCreditosCapturar.ActiveSheet.ActiveColumnIndex = spCreditosCapturar.ActiveSheet.Columns("idProveedor").Index Or spCreditosCapturar.ActiveSheet.ActiveColumnIndex = spCreditosCapturar.ActiveSheet.Columns("nombreProveedor").Index) Then
            spCreditosCapturar.ActiveSheet.Cells(spCreditosCapturar.ActiveSheet.ActiveRowIndex, spCreditosCapturar.ActiveSheet.Columns("idProveedor").Index).Text = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("id").Index).Text
            spCreditosCapturar.ActiveSheet.Cells(spCreditosCapturar.ActiveSheet.ActiveRowIndex, spCreditosCapturar.ActiveSheet.Columns("nombreProveedor").Index).Text = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("nombre").Index).Text
        ElseIf (spCreditosCapturar.ActiveSheet.ActiveColumnIndex = spCreditosCapturar.ActiveSheet.Columns("idFamilia").Index Or spCreditosCapturar.ActiveSheet.ActiveColumnIndex = spCreditosCapturar.ActiveSheet.Columns("nombreFamilia").Index) Then
            spCreditosCapturar.ActiveSheet.Cells(spCreditosCapturar.ActiveSheet.ActiveRowIndex, spCreditosCapturar.ActiveSheet.Columns("idFamilia").Index).Text = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("id").Index).Text
            spCreditosCapturar.ActiveSheet.Cells(spCreditosCapturar.ActiveSheet.ActiveRowIndex, spCreditosCapturar.ActiveSheet.Columns("nombreFamilia").Index).Text = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("nombre").Index).Text
        ElseIf (spCreditosCapturar.ActiveSheet.ActiveColumnIndex = spCreditosCapturar.ActiveSheet.Columns("idSubFamilia").Index Or spCreditosCapturar.ActiveSheet.ActiveColumnIndex = spCreditosCapturar.ActiveSheet.Columns("nombreSubFamilia").Index) Then
            spCreditosCapturar.ActiveSheet.Cells(spCreditosCapturar.ActiveSheet.ActiveRowIndex, spCreditosCapturar.ActiveSheet.Columns("idSubFamilia").Index).Text = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("id").Index).Text
            spCreditosCapturar.ActiveSheet.Cells(spCreditosCapturar.ActiveSheet.ActiveRowIndex, spCreditosCapturar.ActiveSheet.Columns("nombreSubFamilia").Index).Text = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("nombre").Index).Text
        ElseIf (spCreditosCapturar.ActiveSheet.ActiveColumnIndex = spCreditosCapturar.ActiveSheet.Columns("idArticulo").Index Or spCreditosCapturar.ActiveSheet.ActiveColumnIndex = spCreditosCapturar.ActiveSheet.Columns("nombreArticulo").Index Or spCreditosCapturar.ActiveSheet.ActiveColumnIndex = spCreditosCapturar.ActiveSheet.Columns("nombreUnidadMedida").Index Or spCreditosCapturar.ActiveSheet.ActiveColumnIndex = spCreditosCapturar.ActiveSheet.Columns("codigo").Index Or spCreditosCapturar.ActiveSheet.ActiveColumnIndex = spCreditosCapturar.ActiveSheet.Columns("pagina").Index Or spCreditosCapturar.ActiveSheet.ActiveColumnIndex = spCreditosCapturar.ActiveSheet.Columns("color").Index Or spCreditosCapturar.ActiveSheet.ActiveColumnIndex = spCreditosCapturar.ActiveSheet.Columns("talla").Index Or spCreditosCapturar.ActiveSheet.ActiveColumnIndex = spCreditosCapturar.ActiveSheet.Columns("modelo").Index Or spCreditosCapturar.ActiveSheet.ActiveColumnIndex = spCreditosCapturar.ActiveSheet.Columns("codigoInternet").Index Or spCreditosCapturar.ActiveSheet.ActiveColumnIndex = spCreditosCapturar.ActiveSheet.Columns("precio").Index) Then
            spCreditosCapturar.ActiveSheet.Cells(spCreditosCapturar.ActiveSheet.ActiveRowIndex, spCreditosCapturar.ActiveSheet.Columns("idProveedor").Index).Text = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("idProveedor").Index).Text
            spCreditosCapturar.ActiveSheet.Cells(spCreditosCapturar.ActiveSheet.ActiveRowIndex, spCreditosCapturar.ActiveSheet.Columns("nombreProveedor").Index).Text = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("nombreProveedor").Index).Text
            spCreditosCapturar.ActiveSheet.Cells(spCreditosCapturar.ActiveSheet.ActiveRowIndex, spCreditosCapturar.ActiveSheet.Columns("idFamilia").Index).Text = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("idFamilia").Index).Text
            spCreditosCapturar.ActiveSheet.Cells(spCreditosCapturar.ActiveSheet.ActiveRowIndex, spCreditosCapturar.ActiveSheet.Columns("nombreFamilia").Index).Text = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("nombreFamilia").Index).Text
            spCreditosCapturar.ActiveSheet.Cells(spCreditosCapturar.ActiveSheet.ActiveRowIndex, spCreditosCapturar.ActiveSheet.Columns("idSubFamilia").Index).Text = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("idSubFamilia").Index).Text
            spCreditosCapturar.ActiveSheet.Cells(spCreditosCapturar.ActiveSheet.ActiveRowIndex, spCreditosCapturar.ActiveSheet.Columns("nombreSubFamilia").Index).Text = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("nombreSubFamilia").Index).Text
            spCreditosCapturar.ActiveSheet.Cells(spCreditosCapturar.ActiveSheet.ActiveRowIndex, spCreditosCapturar.ActiveSheet.Columns("idArticulo").Index).Text = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("id").Index).Text
            spCreditosCapturar.ActiveSheet.Cells(spCreditosCapturar.ActiveSheet.ActiveRowIndex, spCreditosCapturar.ActiveSheet.Columns("nombreArticulo").Index).Text = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("nombre").Index).Text
            spCreditosCapturar.ActiveSheet.Cells(spCreditosCapturar.ActiveSheet.ActiveRowIndex, spCreditosCapturar.ActiveSheet.Columns("nombreUnidadMedida").Index).Text = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("unidadMedida").Index).Text
            spCreditosCapturar.ActiveSheet.Cells(spCreditosCapturar.ActiveSheet.ActiveRowIndex, spCreditosCapturar.ActiveSheet.Columns("codigo").Index).Text = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("codigo").Index).Text
            spCreditosCapturar.ActiveSheet.Cells(spCreditosCapturar.ActiveSheet.ActiveRowIndex, spCreditosCapturar.ActiveSheet.Columns("pagina").Index).Text = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("pagina").Index).Text
            spCreditosCapturar.ActiveSheet.Cells(spCreditosCapturar.ActiveSheet.ActiveRowIndex, spCreditosCapturar.ActiveSheet.Columns("color").Index).Text = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("color").Index).Text
            spCreditosCapturar.ActiveSheet.Cells(spCreditosCapturar.ActiveSheet.ActiveRowIndex, spCreditosCapturar.ActiveSheet.Columns("talla").Index).Text = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("talla").Index).Text
            spCreditosCapturar.ActiveSheet.Cells(spCreditosCapturar.ActiveSheet.ActiveRowIndex, spCreditosCapturar.ActiveSheet.Columns("modelo").Index).Text = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("modelo").Index).Text
            spCreditosCapturar.ActiveSheet.Cells(spCreditosCapturar.ActiveSheet.ActiveRowIndex, spCreditosCapturar.ActiveSheet.Columns("codigoInternet").Index).Text = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("codigoInternet").Index).Text
            spCreditosCapturar.ActiveSheet.Cells(spCreditosCapturar.ActiveSheet.ActiveRowIndex, spCreditosCapturar.ActiveSheet.Columns("precio").Index).Text = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("precio").Index).Text
        End If

    End Sub

    Private Sub CargarDatosEnOtrosDeCatalogos(ByVal filaCatalogos As Integer)

        If (Me.opcionCatalogoSeleccionada = OpcionCatalogo.almacen) Then
            cbAlmacenes.SelectedValue = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("id").Index).Text
        ElseIf (Me.opcionCatalogoSeleccionada = OpcionCatalogo.cliente) Then
            cbClientes.SelectedValue = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("id").Index).Text
        End If

    End Sub

    Private Sub CargarCatalogoEnSpread()

        spCreditosCapturar.Enabled = False
        Dim columna As Integer = spCreditosCapturar.ActiveSheet.ActiveColumnIndex
        If ((columna = spCreditosCapturar.ActiveSheet.Columns("idProveedor").Index) Or (columna = spCreditosCapturar.ActiveSheet.Columns("nombreProveedor").Index)) Then
            Me.opcionCatalogoSeleccionada = OpcionCatalogo.proveedor
            proveedores.EId = 0
            Dim datos As New DataTable
            datos = proveedores.ObtenerListadoReporteCatalogo()
            If (datos.Rows.Count > 0) Then
                spCatalogos.ActiveSheet.DataSource = datos
            Else
                spCatalogos.ActiveSheet.DataSource = Nothing
                spCatalogos.ActiveSheet.Rows.Count = 0
                spCreditosCapturar.Enabled = True
            End If
            FormatearSpreadCatalogos(OpcionPosicion.derecha)
        ElseIf ((columna = spCreditosCapturar.ActiveSheet.Columns("idFamilia").Index) Or (columna = spCreditosCapturar.ActiveSheet.Columns("nombreFamilia").Index)) Then
            Me.opcionCatalogoSeleccionada = OpcionCatalogo.familia
            Dim idAlmacen As Integer = ALMLogicaCreditos.Funciones.ValidarNumeroACero(cbAlmacenes.SelectedValue)
            If (idAlmacen > 0) Then
                familias.EIdAlmacen = idAlmacen
                familias.EId = 0
                Dim datos As New DataTable
                datos = familias.ObtenerListadoReporteCatalogo()
                If (datos.Rows.Count > 0) Then
                    spCatalogos.ActiveSheet.DataSource = datos
                Else
                    spCatalogos.ActiveSheet.DataSource = Nothing
                    spCatalogos.ActiveSheet.Rows.Count = 0
                    spCreditosCapturar.Enabled = True
                End If
            Else
                spCatalogos.ActiveSheet.DataSource = Nothing
                spCatalogos.ActiveSheet.Rows.Count = 0
                spCreditosCapturar.Enabled = True
            End If
            FormatearSpreadCatalogos(OpcionPosicion.derecha)
        ElseIf ((columna = spCreditosCapturar.ActiveSheet.Columns("idSubFamilia").Index) Or (columna = spCreditosCapturar.ActiveSheet.Columns("nombreSubFamilia").Index)) Then
            Me.opcionCatalogoSeleccionada = OpcionCatalogo.subfamilia
            Dim idAlmacen As Integer = ALMLogicaCreditos.Funciones.ValidarNumeroACero(cbAlmacenes.SelectedValue)
            Dim idFamilia As Integer = ALMLogicaCreditos.Funciones.ValidarNumeroACero(spCreditosCapturar.ActiveSheet.Cells(spCreditosCapturar.ActiveSheet.ActiveRowIndex, spCreditosCapturar.ActiveSheet.Columns("idFamilia").Index).Text)
            If (idAlmacen > 0 And idFamilia > 0) Then
                subFamilias.EIdAlmacen = idAlmacen
                subFamilias.EIdFamilia = idFamilia
                subFamilias.EId = 0
                Dim datos As New DataTable
                datos = subFamilias.ObtenerListadoReporteCatalogo()
                If (datos.Rows.Count > 0) Then
                    spCatalogos.ActiveSheet.DataSource = datos
                Else
                    spCatalogos.ActiveSheet.DataSource = Nothing
                    spCatalogos.ActiveSheet.Rows.Count = 0
                    spCreditosCapturar.Enabled = True
                End If
            Else
                spCatalogos.ActiveSheet.DataSource = Nothing
                spCatalogos.ActiveSheet.Rows.Count = 0
                spCreditosCapturar.Enabled = True
            End If
            FormatearSpreadCatalogos(OpcionPosicion.derecha)
        ElseIf ((columna = spCreditosCapturar.ActiveSheet.Columns("idArticulo").Index) Or (columna = spCreditosCapturar.ActiveSheet.Columns("nombreArticulo").Index) Or (columna = spCreditosCapturar.ActiveSheet.Columns("nombreUnidadMedida").Index) Or (columna = spCreditosCapturar.ActiveSheet.Columns("codigo").Index) Or (columna = spCreditosCapturar.ActiveSheet.Columns("pagina").Index) Or (columna = spCreditosCapturar.ActiveSheet.Columns("color").Index) Or (columna = spCreditosCapturar.ActiveSheet.Columns("talla").Index) Or (columna = spCreditosCapturar.ActiveSheet.Columns("modelo").Index) Or (columna = spCreditosCapturar.ActiveSheet.Columns("codigoInternet").Index) Or (columna = spCreditosCapturar.ActiveSheet.Columns("precio").Index)) Then
            Me.opcionCatalogoSeleccionada = OpcionCatalogo.articulo
            Dim idAlmacen As Integer = ALMLogicaCreditos.Funciones.ValidarNumeroACero(cbAlmacenes.SelectedValue)
            Dim idFamilia As Integer = ALMLogicaCreditos.Funciones.ValidarNumeroACero(spCreditosCapturar.ActiveSheet.Cells(spCreditosCapturar.ActiveSheet.ActiveRowIndex, spCreditosCapturar.ActiveSheet.Columns("idFamilia").Index).Text)
            Dim idSubFamilia As Integer = ALMLogicaCreditos.Funciones.ValidarNumeroACero(spCreditosCapturar.ActiveSheet.Cells(spCreditosCapturar.ActiveSheet.ActiveRowIndex, spCreditosCapturar.ActiveSheet.Columns("idSubFamilia").Index).Text)
            Dim idProveedor As Integer = ALMLogicaCreditos.Funciones.ValidarNumeroACero(spCreditosCapturar.ActiveSheet.Cells(spCreditosCapturar.ActiveSheet.ActiveRowIndex, spCreditosCapturar.ActiveSheet.Columns("idProveedor").Index).Text)
            articulos.EIdAlmacen = idAlmacen
            articulos.EIdFamilia = idFamilia
            articulos.EIdSubFamilia = idSubFamilia
            articulos.EIdProveedor = idProveedor
            articulos.EId = 0
            Dim datos As New DataTable
            datos = articulos.ObtenerListadoCatalogo()
            Me.datosCatalogo = datos
            If (datos.Rows.Count > 0) Then
                spCatalogos.ActiveSheet.DataSource = datos
            Else
                spCatalogos.ActiveSheet.DataSource = Nothing
                spCatalogos.ActiveSheet.Rows.Count = 0
                spCreditosCapturar.Enabled = True
            End If
            FormatearSpreadCatalogos(OpcionPosicion.derecha)
        Else
            spCreditosCapturar.Enabled = True
        End If
        AsignarFoco(txtBuscarCatalogo)

    End Sub

    Private Sub CargarCatalogoEnOtros()

        pnlCapturaSuperior.Enabled = False
        If (Me.opcionCatalogoSeleccionada = OpcionCatalogo.almacen) Then
            almacenes.EId = 0
            Dim datos As New DataTable
            datos = almacenes.ObtenerListadoReporteCatalogo()
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
            spCatalogos.Width = 1040
            spCatalogos.ActiveSheet.Columns.Count = 16
        Else
            spCatalogos.Width = 500
            spCatalogos.ActiveSheet.Columns.Count = 2
        End If
        If (posicion = OpcionPosicion.izquierda) Then ' Izquierda.
            pnlCatalogos.Location = New Point(Me.izquierda, Me.arriba)
        ElseIf (posicion = OpcionPosicion.centro) Then ' Centrar.
            pnlCatalogos.Location = New Point(Me.anchoMitad - (spCatalogos.Width / 2), Me.arriba)
        ElseIf (posicion = OpcionPosicion.derecha) Then ' Derecha.
            pnlCatalogos.Location = New Point(Me.anchoTotal - 5 - (spCatalogos.Width), Me.arriba)
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
            spCatalogos.ActiveSheet.Columns(spCatalogos.ActiveSheet.Columns("idFamilia").Index, spCatalogos.ActiveSheet.Columns("nombreSubFamilia").Index).Visible = False
            spCatalogos.ActiveSheet.Columns("unidadMedida").Visible = False
            spCatalogos.ActiveSheet.Columns("nombre").Visible = False
            spCatalogos.ActiveSheet.Columns("idProveedor").Visible = False
        End If
        spCatalogos.ActiveSheet.Columns(0, spCatalogos.ActiveSheet.Columns.Count - 1).AllowAutoFilter = True
        spCatalogos.ActiveSheet.Columns(0, spCatalogos.ActiveSheet.Columns.Count - 1).AllowAutoSort = True
        If (Me.opcionCatalogoSeleccionada = OpcionCatalogo.articulo) Then
            spCatalogos.ActiveSheet.Columns("id").AllowAutoFilter = False
            spCatalogos.ActiveSheet.Columns("nombre").AllowAutoFilter = False
            spCatalogos.ActiveSheet.Columns("codigo").AllowAutoFilter = False
        End If
        pnlCatalogos.Height = spCreditosCapturar.Height
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
        spCatalogos.ActiveSheet.Columns("nombre").Width = 200
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
        spCreditosCapturar.Enabled = True
        If (Me.opcionCatalogoSeleccionada = OpcionCatalogo.almacen) Then
            AsignarFoco(cbAlmacenes)
        ElseIf (Me.opcionCatalogoSeleccionada = OpcionCatalogo.cliente) Then
            AsignarFoco(cbClientes)
        Else
            AsignarFoco(spCreditosCapturar)
        End If
        txtBuscarCatalogo.Clear()
        pnlCatalogos.Visible = False

    End Sub

    Private Sub CargarCreditos()

        Me.Cursor = Cursors.WaitCursor
        Dim id As Integer = ALMLogicaCreditos.Funciones.ValidarNumeroACero(txtId.Text)
        If (id > 0) Then
            creditos.EId = id
            creditosDetalle.EId = id
            Dim datos As New DataTable
            datos = creditos.ObtenerListadoGeneral()
            If (datos.Rows.Count > 0) Then
                dtpFecha.Value = datos.Rows(0).Item("Fecha")
                cbClientes.SelectedValue = datos.Rows(0).Item("IdCliente")
                cbMetodosPagos.SelectedValue = datos.Rows(0).Item("IdMetodoPago")
                spCreditosCapturar.ActiveSheet.DataSource = creditosDetalle.ObtenerListadoDetallado()
                Me.cantidadFilas = spCreditosCapturar.ActiveSheet.Rows.Count + 1
                FormatearSpreadCreditos()
                CalcularTotalesCreditos()
            Else
                LimpiarPantalla()
            End If
            chkMostrarDetallado.Checked = False
        End If
        AsignarFoco(dtpFecha)
        Me.Cursor = Cursors.Default

    End Sub

    Private Sub FormatearSpreadCreditos()

        spCreditosCapturar.ActiveSheet.ColumnHeader.RowCount = 2
        spCreditosCapturar.ActiveSheet.ColumnHeader.Rows(0, spCreditosCapturar.ActiveSheet.ColumnHeader.Rows.Count - 1).Font = New Font(Principal.tipoLetraSpread, Principal.tamañoLetraSpread, FontStyle.Bold)
        spCreditosCapturar.ActiveSheet.ColumnHeader.Rows(0).Height = Principal.alturaFilasEncabezadosChicosSpread
        spCreditosCapturar.ActiveSheet.ColumnHeader.Rows(1).Height = Principal.alturaFilasEncabezadosMedianosSpread
        spCreditosCapturar.ActiveSheet.OperationMode = FarPoint.Win.Spread.OperationMode.Normal
        spCreditosCapturar.ActiveSheet.Rows.Count = Me.cantidadFilas
        ControlarSpreadEnterASiguienteColumna(spCreditosCapturar)
        Dim numeracion As Integer = 0
        spCreditosCapturar.ActiveSheet.Columns(numeracion).Tag = "idProveedor" : numeracion += 1
        spCreditosCapturar.ActiveSheet.Columns(numeracion).Tag = "nombreProveedor" : numeracion += 1
        spCreditosCapturar.ActiveSheet.Columns(numeracion).Tag = "idFamilia" : numeracion += 1
        spCreditosCapturar.ActiveSheet.Columns(numeracion).Tag = "nombreFamilia" : numeracion += 1
        spCreditosCapturar.ActiveSheet.Columns(numeracion).Tag = "idSubFamilia" : numeracion += 1
        spCreditosCapturar.ActiveSheet.Columns(numeracion).Tag = "nombreSubFamilia" : numeracion += 1
        spCreditosCapturar.ActiveSheet.Columns(numeracion).Tag = "idArticulo" : numeracion += 1
        spCreditosCapturar.ActiveSheet.Columns(numeracion).Tag = "nombreArticulo" : numeracion += 1
        spCreditosCapturar.ActiveSheet.Columns(numeracion).Tag = "nombreUnidadMedida" : numeracion += 1
        spCreditosCapturar.ActiveSheet.Columns(numeracion).Tag = "codigo" : numeracion += 1
        spCreditosCapturar.ActiveSheet.Columns(numeracion).Tag = "pagina" : numeracion += 1
        spCreditosCapturar.ActiveSheet.Columns(numeracion).Tag = "color" : numeracion += 1
        spCreditosCapturar.ActiveSheet.Columns(numeracion).Tag = "talla" : numeracion += 1
        spCreditosCapturar.ActiveSheet.Columns(numeracion).Tag = "modelo" : numeracion += 1
        spCreditosCapturar.ActiveSheet.Columns(numeracion).Tag = "codigoInternet" : numeracion += 1
        spCreditosCapturar.ActiveSheet.Columns(numeracion).Tag = "cantidad" : numeracion += 1
        spCreditosCapturar.ActiveSheet.Columns(numeracion).Tag = "precio" : numeracion += 1
        spCreditosCapturar.ActiveSheet.Columns(numeracion).Tag = "subtotal" : numeracion += 1
        spCreditosCapturar.ActiveSheet.Columns(numeracion).Tag = "porcentajeDescuento" : numeracion += 1
        spCreditosCapturar.ActiveSheet.Columns(numeracion).Tag = "descuento" : numeracion += 1
        spCreditosCapturar.ActiveSheet.Columns(numeracion).Tag = "total" : numeracion += 1
        spCreditosCapturar.ActiveSheet.Columns(numeracion).Tag = "observaciones" : numeracion += 1
        spCreditosCapturar.ActiveSheet.Columns.Count = numeracion
        spCreditosCapturar.ActiveSheet.Columns("idProveedor").Width = 50
        spCreditosCapturar.ActiveSheet.Columns("nombreProveedor").Width = 90
        spCreditosCapturar.ActiveSheet.Columns("idFamilia").Width = 50
        spCreditosCapturar.ActiveSheet.Columns("nombreFamilia").Width = 100
        spCreditosCapturar.ActiveSheet.Columns("idSubFamilia").Width = 50
        spCreditosCapturar.ActiveSheet.Columns("nombreSubFamilia").Width = 100
        spCreditosCapturar.ActiveSheet.Columns("idArticulo").Width = 50
        spCreditosCapturar.ActiveSheet.Columns("nombreArticulo").Width = 200
        spCreditosCapturar.ActiveSheet.Columns("nombreUnidadMedida").Width = 90
        spCreditosCapturar.ActiveSheet.Columns("codigo").Width = 125
        spCreditosCapturar.ActiveSheet.Columns("pagina").Width = 70
        spCreditosCapturar.ActiveSheet.Columns("color").Width = 100
        spCreditosCapturar.ActiveSheet.Columns("talla").Width = 70
        spCreditosCapturar.ActiveSheet.Columns("modelo").Width = 80
        spCreditosCapturar.ActiveSheet.Columns("codigoInternet").Width = 85
        spCreditosCapturar.ActiveSheet.Columns("cantidad").Width = 90
        spCreditosCapturar.ActiveSheet.Columns("precio").Width = 70
        spCreditosCapturar.ActiveSheet.Columns("subtotal").Width = 90
        spCreditosCapturar.ActiveSheet.Columns("porcentajeDescuento").Width = 100
        spCreditosCapturar.ActiveSheet.Columns("descuento").Width = 100
        spCreditosCapturar.ActiveSheet.Columns("total").Width = 85
        spCreditosCapturar.ActiveSheet.Columns("observaciones").Width = 200
        spCreditosCapturar.ActiveSheet.Columns("idProveedor").CellType = tipoEntero
        spCreditosCapturar.ActiveSheet.Columns("nombreProveedor").CellType = tipoTexto
        spCreditosCapturar.ActiveSheet.Columns("idFamilia").CellType = tipoEntero
        spCreditosCapturar.ActiveSheet.Columns("nombreFamilia").CellType = tipoTexto
        spCreditosCapturar.ActiveSheet.Columns("idSubFamilia").CellType = tipoEntero
        spCreditosCapturar.ActiveSheet.Columns("nombreSubFamilia").CellType = tipoTexto
        spCreditosCapturar.ActiveSheet.Columns("idArticulo").CellType = tipoEntero
        spCreditosCapturar.ActiveSheet.Columns("nombreArticulo").CellType = tipoTexto
        spCreditosCapturar.ActiveSheet.Columns("nombreUnidadMedida").CellType = tipoTexto
        spCreditosCapturar.ActiveSheet.Columns("codigo").CellType = tipoTexto
        spCreditosCapturar.ActiveSheet.Columns("pagina").CellType = tipoEntero
        spCreditosCapturar.ActiveSheet.Columns("color").CellType = tipoTexto
        spCreditosCapturar.ActiveSheet.Columns("talla").CellType = tipoTexto
        spCreditosCapturar.ActiveSheet.Columns("modelo").CellType = tipoTexto
        spCreditosCapturar.ActiveSheet.Columns("codigoInternet").CellType = tipoTexto
        spCreditosCapturar.ActiveSheet.Columns("cantidad").CellType = tipoEntero
        spCreditosCapturar.ActiveSheet.Columns("precio").CellType = tipoDoble
        spCreditosCapturar.ActiveSheet.Columns("subtotal").CellType = tipoDoble
        spCreditosCapturar.ActiveSheet.Columns("porcentajeDescuento").CellType = tipoDoble
        spCreditosCapturar.ActiveSheet.Columns("descuento").CellType = tipoDoble
        spCreditosCapturar.ActiveSheet.Columns("total").CellType = tipoDoble
        spCreditosCapturar.ActiveSheet.Columns("observaciones").CellType = tipoTexto
        spCreditosCapturar.ActiveSheet.AddColumnHeaderSpanCell(0, spCreditosCapturar.ActiveSheet.Columns("idProveedor").Index, 1, 2)
        spCreditosCapturar.ActiveSheet.ColumnHeader.Cells(0, spCreditosCapturar.ActiveSheet.Columns("idProveedor").Index).Value = "P r o v e e d o r".ToUpper()
        spCreditosCapturar.ActiveSheet.ColumnHeader.Cells(1, spCreditosCapturar.ActiveSheet.Columns("idProveedor").Index).Value = "No. *".ToUpper()
        spCreditosCapturar.ActiveSheet.ColumnHeader.Cells(1, spCreditosCapturar.ActiveSheet.Columns("nombreProveedor").Index).Value = "Nombre *".ToUpper()
        spCreditosCapturar.ActiveSheet.AddColumnHeaderSpanCell(0, spCreditosCapturar.ActiveSheet.Columns("idFamilia").Index, 1, 2)
        spCreditosCapturar.ActiveSheet.ColumnHeader.Cells(0, spCreditosCapturar.ActiveSheet.Columns("idFamilia").Index).Value = "F a m i l i a".ToUpper()
        spCreditosCapturar.ActiveSheet.ColumnHeader.Cells(1, spCreditosCapturar.ActiveSheet.Columns("idFamilia").Index).Value = "No. *".ToUpper()
        spCreditosCapturar.ActiveSheet.ColumnHeader.Cells(1, spCreditosCapturar.ActiveSheet.Columns("nombreFamilia").Index).Value = "Nombre *".ToUpper()
        spCreditosCapturar.ActiveSheet.AddColumnHeaderSpanCell(0, spCreditosCapturar.ActiveSheet.Columns("idSubFamilia").Index, 1, 2)
        spCreditosCapturar.ActiveSheet.ColumnHeader.Cells(0, spCreditosCapturar.ActiveSheet.Columns("idSubFamilia").Index).Value = "S u b F a m i l i a".ToUpper()
        spCreditosCapturar.ActiveSheet.ColumnHeader.Cells(1, spCreditosCapturar.ActiveSheet.Columns("idSubFamilia").Index).Value = "No. *".ToUpper()
        spCreditosCapturar.ActiveSheet.ColumnHeader.Cells(1, spCreditosCapturar.ActiveSheet.Columns("nombreSubFamilia").Index).Value = "Nombre *".ToUpper()
        spCreditosCapturar.ActiveSheet.AddColumnHeaderSpanCell(0, spCreditosCapturar.ActiveSheet.Columns("idArticulo").Index, 1, 9)
        spCreditosCapturar.ActiveSheet.ColumnHeader.Cells(0, spCreditosCapturar.ActiveSheet.Columns("idArticulo").Index).Value = "A r t í c u l o".ToUpper()
        spCreditosCapturar.ActiveSheet.ColumnHeader.Cells(1, spCreditosCapturar.ActiveSheet.Columns("idArticulo").Index).Value = "No. *".ToUpper()
        spCreditosCapturar.ActiveSheet.ColumnHeader.Cells(1, spCreditosCapturar.ActiveSheet.Columns("nombreArticulo").Index).Value = "Nombre *".ToUpper()
        spCreditosCapturar.ActiveSheet.ColumnHeader.Cells(1, spCreditosCapturar.ActiveSheet.Columns("nombreUnidadMedida").Index).Value = "Unidad *".ToUpper()
        spCreditosCapturar.ActiveSheet.ColumnHeader.Cells(1, spCreditosCapturar.ActiveSheet.Columns("codigo").Index).Value = "Código *".ToUpper()
        spCreditosCapturar.ActiveSheet.ColumnHeader.Cells(1, spCreditosCapturar.ActiveSheet.Columns("pagina").Index).Value = "Página *".ToUpper()
        spCreditosCapturar.ActiveSheet.ColumnHeader.Cells(1, spCreditosCapturar.ActiveSheet.Columns("color").Index).Value = "Color *".ToUpper()
        spCreditosCapturar.ActiveSheet.ColumnHeader.Cells(1, spCreditosCapturar.ActiveSheet.Columns("talla").Index).Value = "Talla *".ToUpper()
        spCreditosCapturar.ActiveSheet.ColumnHeader.Cells(1, spCreditosCapturar.ActiveSheet.Columns("modelo").Index).Value = "Modelo *".ToUpper()
        spCreditosCapturar.ActiveSheet.ColumnHeader.Cells(1, spCreditosCapturar.ActiveSheet.Columns("codigoInternet").Index).Value = "Codigo Internet *".ToUpper()
        spCreditosCapturar.ActiveSheet.AddColumnHeaderSpanCell(0, spCreditosCapturar.ActiveSheet.Columns("cantidad").Index, 2, 1)
        spCreditosCapturar.ActiveSheet.ColumnHeader.Cells(0, spCreditosCapturar.ActiveSheet.Columns("cantidad").Index).Value = "Cantidad *".ToUpper()
        spCreditosCapturar.ActiveSheet.AddColumnHeaderSpanCell(0, spCreditosCapturar.ActiveSheet.Columns("precio").Index, 2, 1)
        spCreditosCapturar.ActiveSheet.ColumnHeader.Cells(0, spCreditosCapturar.ActiveSheet.Columns("precio").Index).Value = "Precio *".ToUpper()
        spCreditosCapturar.ActiveSheet.AddColumnHeaderSpanCell(0, spCreditosCapturar.ActiveSheet.Columns("subtotal").Index, 2, 1)
        spCreditosCapturar.ActiveSheet.ColumnHeader.Cells(0, spCreditosCapturar.ActiveSheet.Columns("subtotal").Index).Value = "SubTotal *".ToUpper()
        spCreditosCapturar.ActiveSheet.AddColumnHeaderSpanCell(0, spCreditosCapturar.ActiveSheet.Columns("porcentajeDescuento").Index, 2, 1)
        spCreditosCapturar.ActiveSheet.ColumnHeader.Cells(0, spCreditosCapturar.ActiveSheet.Columns("porcentajeDescuento").Index).Value = "Porcentaje Descuento *".ToUpper()
        spCreditosCapturar.ActiveSheet.AddColumnHeaderSpanCell(0, spCreditosCapturar.ActiveSheet.Columns("descuento").Index, 2, 1)
        spCreditosCapturar.ActiveSheet.ColumnHeader.Cells(0, spCreditosCapturar.ActiveSheet.Columns("descuento").Index).Value = "Descuento *".ToUpper()
        spCreditosCapturar.ActiveSheet.AddColumnHeaderSpanCell(0, spCreditosCapturar.ActiveSheet.Columns("total").Index, 2, 1)
        spCreditosCapturar.ActiveSheet.ColumnHeader.Cells(0, spCreditosCapturar.ActiveSheet.Columns("total").Index).Value = "Total *".ToUpper()
        spCreditosCapturar.ActiveSheet.AddColumnHeaderSpanCell(0, spCreditosCapturar.ActiveSheet.Columns("observaciones").Index, 2, 1)
        spCreditosCapturar.ActiveSheet.ColumnHeader.Cells(0, spCreditosCapturar.ActiveSheet.Columns("observaciones").Index).Value = "Observaciones".ToUpper()
        MostrarOcultarColumnasDetalle(False)
        spCreditosCapturar.Refresh()

    End Sub

    Private Sub MostrarOcultarColumnasDetalle(ByVal valor As Boolean)

        spCreditosCapturar.ActiveSheet.Columns(spCreditosCapturar.ActiveSheet.Columns("idProveedor").Index, spCreditosCapturar.ActiveSheet.Columns("nombreSubFamilia").Index).Visible = valor
        spCreditosCapturar.ActiveSheet.Columns("nombreArticulo").Visible = valor
        spCreditosCapturar.ActiveSheet.Columns("nombreUnidadMedida").Visible = valor
        spCreditosCapturar.ActiveSheet.Columns("observaciones").Visible = valor
        spCreditosCapturar.ActiveSheet.Columns(spCreditosCapturar.ActiveSheet.Columns("descuento").Index).Visible = valor

    End Sub

    Private Sub ValidarGuardadoCreditos()

        CalcularTotalesCreditos()
        Me.esGuardadoValido = True
        ' Parte superior.
        Dim idAlmacen As Integer = ALMLogicaCreditos.Funciones.ValidarNumeroACero(cbAlmacenes.SelectedValue)
        If (idAlmacen <= 0) Then
            cbAlmacenes.BackColor = Principal.colorAdvertencia
            Me.esGuardadoValido = False
        End If
        Dim id As Integer = ALMLogicaCreditos.Funciones.ValidarNumeroACero(txtId.Text)
        If (id <= 0) Then
            txtId.BackColor = Principal.colorAdvertencia
            Me.esGuardadoValido = False
        End If
        Dim idCliente As Integer = ALMLogicaCreditos.Funciones.ValidarNumeroACero(cbClientes.SelectedValue)
        If (idCliente <= 0) Then
            cbClientes.BackColor = Principal.colorAdvertencia
            Me.esGuardadoValido = False
        End If
        Dim idMetodoPago As Integer = ALMLogicaCreditos.Funciones.ValidarNumeroACero(cbMetodosPagos.SelectedValue)
        If (idMetodoPago <= 0) Then
            cbMetodosPagos.BackColor = Principal.colorAdvertencia
            Me.esGuardadoValido = False
        End If
        ' Parte inferior.
        For fila As Integer = 0 To spCreditosCapturar.ActiveSheet.Rows.Count - 1
            Dim idProveedor As Integer = ALMLogicaCreditos.Funciones.ValidarNumeroACero(spCreditosCapturar.ActiveSheet.Cells(fila, spCreditosCapturar.ActiveSheet.Columns("idProveedor").Index).Text)
            Dim idFamilia As Integer = ALMLogicaCreditos.Funciones.ValidarNumeroACero(spCreditosCapturar.ActiveSheet.Cells(fila, spCreditosCapturar.ActiveSheet.Columns("idFamilia").Index).Text)
            Dim idSubFamilia As Integer = ALMLogicaCreditos.Funciones.ValidarNumeroACero(spCreditosCapturar.ActiveSheet.Cells(fila, spCreditosCapturar.ActiveSheet.Columns("idSubFamilia").Index).Text)
            Dim idArticulo As Integer = ALMLogicaCreditos.Funciones.ValidarNumeroACero(spCreditosCapturar.ActiveSheet.Cells(fila, spCreditosCapturar.ActiveSheet.Columns("idArticulo").Index).Text)
            If (idFamilia > 0 And idSubFamilia > 0 And idArticulo > 0) Then
                Dim cantidad As Integer = ALMLogicaCreditos.Funciones.ValidarNumeroACero(spCreditosCapturar.ActiveSheet.Cells(fila, spCreditosCapturar.ActiveSheet.Columns("cantidad").Index).Text)
                If (cantidad <= 0) Then
                    spCreditosCapturar.ActiveSheet.Cells(fila, spCreditosCapturar.ActiveSheet.Columns("cantidad").Index).BackColor = Principal.colorAdvertencia
                    Me.esGuardadoValido = False
                End If
                Dim precio As String = spCreditosCapturar.ActiveSheet.Cells(fila, spCreditosCapturar.ActiveSheet.Columns("precio").Index).Text
                Dim precio2 As Double = ALMLogicaCreditos.Funciones.ValidarNumeroACero(spCreditosCapturar.ActiveSheet.Cells(fila, spCreditosCapturar.ActiveSheet.Columns("precio").Index).Text)
                If (String.IsNullOrEmpty(precio) Or precio2 < 0) Then
                    spCreditosCapturar.ActiveSheet.Cells(fila, spCreditosCapturar.ActiveSheet.Columns("precio").Index).BackColor = Principal.colorAdvertencia
                    Me.esGuardadoValido = False
                End If
                Dim subtotal As String = spCreditosCapturar.ActiveSheet.Cells(fila, spCreditosCapturar.ActiveSheet.Columns("subtotal").Index).Text
                Dim subtotal2 As Double = ALMLogicaCreditos.Funciones.ValidarNumeroACero(spCreditosCapturar.ActiveSheet.Cells(fila, spCreditosCapturar.ActiveSheet.Columns("subtotal").Index).Text)
                If (String.IsNullOrEmpty(subtotal) Or subtotal2 < 0) Then
                    spCreditosCapturar.ActiveSheet.Cells(fila, spCreditosCapturar.ActiveSheet.Columns("subtotal").Index).BackColor = Principal.colorAdvertencia
                    Me.esGuardadoValido = False
                End If
                Dim porcentajeDescuento As String = spCreditosCapturar.ActiveSheet.Cells(fila, spCreditosCapturar.ActiveSheet.Columns("porcentajeDescuento").Index).Text
                Dim porcentajeDescuento2 As Double = ALMLogicaCreditos.Funciones.ValidarNumeroACero(spCreditosCapturar.ActiveSheet.Cells(fila, spCreditosCapturar.ActiveSheet.Columns("porcentajeDescuento").Index).Text)
                If (String.IsNullOrEmpty(porcentajeDescuento) Or porcentajeDescuento2 < 0) Then
                    spCreditosCapturar.ActiveSheet.Cells(fila, spCreditosCapturar.ActiveSheet.Columns("porcentajeDescuento").Index).BackColor = Principal.colorAdvertencia
                    Me.esGuardadoValido = False
                End If
                Dim descuento As String = spCreditosCapturar.ActiveSheet.Cells(fila, spCreditosCapturar.ActiveSheet.Columns("descuento").Index).Text
                Dim descuento2 As Double = ALMLogicaCreditos.Funciones.ValidarNumeroACero(spCreditosCapturar.ActiveSheet.Cells(fila, spCreditosCapturar.ActiveSheet.Columns("descuento").Index).Text)
                If (String.IsNullOrEmpty(descuento) Or descuento2 < 0) Then
                    spCreditosCapturar.ActiveSheet.Cells(fila, spCreditosCapturar.ActiveSheet.Columns("descuento").Index).BackColor = Principal.colorAdvertencia
                    Me.esGuardadoValido = False
                End If
                Dim total As String = spCreditosCapturar.ActiveSheet.Cells(fila, spCreditosCapturar.ActiveSheet.Columns("total").Index).Text
                Dim total2 As Double = ALMLogicaCreditos.Funciones.ValidarNumeroACero(spCreditosCapturar.ActiveSheet.Cells(fila, spCreditosCapturar.ActiveSheet.Columns("total").Index).Text)
                If (String.IsNullOrEmpty(total) Or total2 < 0) Then
                    spCreditosCapturar.ActiveSheet.Cells(fila, spCreditosCapturar.ActiveSheet.Columns("total").Index).BackColor = Principal.colorAdvertencia
                    Me.esGuardadoValido = False
                End If
            End If
        Next

    End Sub

    Private Sub GuardarEditarCreditos()

        EliminarCreditos(False)
        ' Parte superior.
        Dim idAlmacen As Integer = ALMLogicaCreditos.Funciones.ValidarNumeroACero(cbAlmacenes.SelectedValue)
        Dim id As Integer = ALMLogicaCreditos.Funciones.ValidarNumeroACero(txtId.Text)
        Dim fecha As Date = dtpFecha.Value
        Dim idCliente As Integer = ALMLogicaCreditos.Funciones.ValidarNumeroACero(cbClientes.SelectedValue)
        Dim idMetodoPago As Integer = ALMLogicaCreditos.Funciones.ValidarNumeroACero(cbMetodosPagos.SelectedValue)
        Dim sumaSubtotal As Double = ALMLogicaCreditos.Funciones.ValidarNumeroACero(txtSubTotal.Text)
        Dim sumaDescuento As Double = ALMLogicaCreditos.Funciones.ValidarNumeroACero(txtDescuento.Text)
        Dim sumaTotal As Double = ALMLogicaCreditos.Funciones.ValidarNumeroACero(txtTotal.Text)
        ' Parte inferior.
        ' Se guarda la información de detalle.
        For fila As Integer = 0 To spCreditosCapturar.ActiveSheet.Rows.Count - 1
            Dim idProveedor As Integer = ALMLogicaCreditos.Funciones.ValidarNumeroACero(spCreditosCapturar.ActiveSheet.Cells(fila, spCreditosCapturar.ActiveSheet.Columns("idProveedor").Index).Text)
            Dim idFamilia As Integer = ALMLogicaCreditos.Funciones.ValidarNumeroACero(spCreditosCapturar.ActiveSheet.Cells(fila, spCreditosCapturar.ActiveSheet.Columns("idFamilia").Index).Text)
            Dim idSubFamilia As Integer = ALMLogicaCreditos.Funciones.ValidarNumeroACero(spCreditosCapturar.ActiveSheet.Cells(fila, spCreditosCapturar.ActiveSheet.Columns("idSubFamilia").Index).Text)
            Dim idArticulo As Integer = ALMLogicaCreditos.Funciones.ValidarNumeroACero(spCreditosCapturar.ActiveSheet.Cells(fila, spCreditosCapturar.ActiveSheet.Columns("idArticulo").Index).Text)
            Dim cantidad As Integer = ALMLogicaCreditos.Funciones.ValidarNumeroACero(spCreditosCapturar.ActiveSheet.Cells(fila, spCreditosCapturar.ActiveSheet.Columns("cantidad").Index).Text)
            Dim precio As Double = ALMLogicaCreditos.Funciones.ValidarNumeroACero(spCreditosCapturar.ActiveSheet.Cells(fila, spCreditosCapturar.ActiveSheet.Columns("precio").Index).Text)
            Dim subtotal As Double = ALMLogicaCreditos.Funciones.ValidarNumeroACero(spCreditosCapturar.ActiveSheet.Cells(fila, spCreditosCapturar.ActiveSheet.Columns("subtotal").Index).Text)
            Dim porcentajeDescuento As Double = ALMLogicaCreditos.Funciones.ValidarNumeroACero(spCreditosCapturar.ActiveSheet.Cells(fila, spCreditosCapturar.ActiveSheet.Columns("porcentajeDescuento").Index).Text)
            Dim descuento As Double = ALMLogicaCreditos.Funciones.ValidarNumeroACero(spCreditosCapturar.ActiveSheet.Cells(fila, spCreditosCapturar.ActiveSheet.Columns("descuento").Index).Text)
            Dim total As Double = ALMLogicaCreditos.Funciones.ValidarNumeroACero(spCreditosCapturar.ActiveSheet.Cells(fila, spCreditosCapturar.ActiveSheet.Columns("total").Index).Text)
            Dim orden As Integer = fila
            Dim observaciones As String = spCreditosCapturar.ActiveSheet.Cells(fila, spCreditosCapturar.ActiveSheet.Columns("observaciones").Index).Text
            Dim estaActualizado As Boolean = False
            If (id > 0 AndAlso idAlmacen > 0 AndAlso idFamilia > 0 AndAlso idSubFamilia > 0 AndAlso idArticulo > 0 AndAlso idCliente > 0) Then
                creditosDetalle.EId = id
                creditosDetalle.EIdAlmacen = idAlmacen
                creditosDetalle.EIdFamilia = idFamilia
                creditosDetalle.EIdSubFamilia = idSubFamilia
                creditosDetalle.EIdArticulo = idArticulo
                creditosDetalle.ECantidad = cantidad
                creditosDetalle.EPrecio = precio
                creditosDetalle.ESubTotal = subtotal
                creditosDetalle.EPorcentajeDescuento = porcentajeDescuento
                creditosDetalle.EDescuento = descuento
                creditosDetalle.ETotal = total
                creditosDetalle.EOrden = orden
                creditosDetalle.EObservaciones = observaciones
                creditosDetalle.Guardar()
            End If
        Next
        ' Se guarda la información general.
        If (id > 0 AndAlso idCliente > 0) Then
            creditos.EId = id
            creditos.EIdCliente = idCliente
            creditos.EFecha = fecha
            creditos.ESubtotal = sumaSubtotal
            creditos.EDescuento = sumaDescuento
            creditos.ETotal = sumaTotal
            creditos.EIdMetodoPago = idMetodoPago
            creditos.Guardar()
        End If
        MessageBox.Show("Guardado finalizado.", "Finalizado.", MessageBoxButtons.OK)
        LimpiarPantalla()
        CargarIdConsecutivo()
        AsignarFoco(txtId)

    End Sub

    Private Sub EliminarCreditos(ByVal conMensaje As Boolean)

        Me.Cursor = Cursors.WaitCursor
        Dim respuestaSi As Boolean = False
        If (conMensaje) Then
            If (MessageBox.Show("Confirmas que deseas eliminar esta captura?", "Confirmación.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes) Then
                respuestaSi = True
            End If
        End If
        If ((respuestaSi) Or (Not conMensaje)) Then
            Dim id As Integer = ALMLogicaCreditos.Funciones.ValidarNumeroACero(txtId.Text)
            creditos.EId = id
            creditos.Eliminar()
            creditosDetalle.EId = id
            creditosDetalle.Eliminar()
        End If
        If (conMensaje And respuestaSi) Then
            MessageBox.Show("Eliminado finalizado.", "Finalizado.", MessageBoxButtons.OK)
            LimpiarPantalla()
            CargarIdConsecutivo()
            AsignarFoco(txtId)
        End If
        Me.Cursor = Cursors.Default

    End Sub

#End Region

#Region "Actualizacion"

    Private Sub CargarCreditosActualizarSeleccionar()

        creditos.EId = 0
        Dim fecha As Date = dtpFechaInicial.Value.ToShortDateString : Dim fecha2 As Date = dtpFechaFinal.Value.ToShortDateString
        Dim aplicaFecha As Boolean = False
        If (chkFecha.Checked) Then
            aplicaFecha = True
            creditos.EFecha = fecha : creditos.EFecha2 = fecha2
        Else
            aplicaFecha = False
        End If
        Dim idCliente As Integer = ALMLogicaCreditos.Funciones.ValidarNumeroACero(cbClientesActualizar.SelectedValue)
        creditos.EIdCliente = idCliente
        Dim datos As New DataTable
        datos = creditos.ObtenerListadoActualizarSeleccionar(aplicaFecha)
        spCreditosActualizarSeleccionar.ActiveSheet.DataSource = datos
        FormatearSpreadCreditosActualizarSeleccionar(datos.Rows.Count)
        chkMostrarDetalladoActualizar.Checked = False
        chkMostrarDetalladoActualizar.Enabled = True
        btnGuardar.Enabled = True
        MostrarOcultar()
        AsignarFoco(spCreditosActualizarSeleccionar)

    End Sub

    Private Sub FormatearSpreadCreditosActualizarSeleccionar(ByVal cantidadFilas As Integer)

        spCreditosActualizarSeleccionar.ActiveSheet.Rows.Count = cantidadFilas
        spCreditosActualizarSeleccionar.ActiveSheet.ColumnHeader.RowCount = 2
        spCreditosActualizarSeleccionar.ActiveSheet.ColumnHeader.Rows(0, spCreditosActualizarSeleccionar.ActiveSheet.ColumnHeader.Rows.Count - 1).Font = New Font(Principal.tipoLetraSpread, Principal.tamañoLetraSpread, FontStyle.Bold)
        spCreditosActualizarSeleccionar.ActiveSheet.ColumnHeader.Rows(0).Height = Principal.alturaFilasEncabezadosChicosSpread
        spCreditosActualizarSeleccionar.ActiveSheet.ColumnHeader.Rows(1).Height = Principal.alturaFilasEncabezadosMedianosSpread
        spCreditosActualizarSeleccionar.ActiveSheet.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect
        ControlarSpreadEnterASiguienteColumna(spCreditosActualizarSeleccionar)
        Dim numeracion As Integer = 0
        spCreditosActualizarSeleccionar.ActiveSheet.Columns.Count += 10
        spCreditosActualizarSeleccionar.ActiveSheet.Columns(numeracion).Tag = "id" : numeracion += 1
        spCreditosActualizarSeleccionar.ActiveSheet.Columns(numeracion).Tag = "fecha" : numeracion += 1
        spCreditosActualizarSeleccionar.ActiveSheet.Columns(numeracion).Tag = "idCliente" : numeracion += 1
        spCreditosActualizarSeleccionar.ActiveSheet.Columns(numeracion).Tag = "nombreCliente" : numeracion += 1
        spCreditosActualizarSeleccionar.ActiveSheet.Columns(numeracion).Tag = "idMetodoPago" : numeracion += 1
        spCreditosActualizarSeleccionar.ActiveSheet.Columns(numeracion).Tag = "nombreMetodoPago" : numeracion += 1
        spCreditosActualizarSeleccionar.ActiveSheet.Columns(numeracion).Tag = "subtotal" : numeracion += 1
        spCreditosActualizarSeleccionar.ActiveSheet.Columns(numeracion).Tag = "descuento" : numeracion += 1
        spCreditosActualizarSeleccionar.ActiveSheet.Columns(numeracion).Tag = "total" : numeracion += 1
        spCreditosActualizarSeleccionar.ActiveSheet.Columns(numeracion).Tag = "estaPagado" : numeracion += 1
        spCreditosActualizarSeleccionar.ActiveSheet.Columns.Count = numeracion
        spCreditosActualizarSeleccionar.ActiveSheet.Columns("id").Width = 50
        spCreditosActualizarSeleccionar.ActiveSheet.Columns("fecha").Width = 80
        spCreditosActualizarSeleccionar.ActiveSheet.Columns("idCliente").Width = 50
        spCreditosActualizarSeleccionar.ActiveSheet.Columns("nombreCliente").Width = 210
        spCreditosActualizarSeleccionar.ActiveSheet.Columns("idMetodoPago").Width = 50
        spCreditosActualizarSeleccionar.ActiveSheet.Columns("nombreMetodoPago").Width = 120
        spCreditosActualizarSeleccionar.ActiveSheet.Columns("subtotal").Width = 100
        spCreditosActualizarSeleccionar.ActiveSheet.Columns("descuento").Width = 110
        spCreditosActualizarSeleccionar.ActiveSheet.Columns("total").Width = 85
        spCreditosActualizarSeleccionar.ActiveSheet.Columns("estaPagado").Width = 100
        For columna = 0 To spCreditosActualizarSeleccionar.ActiveSheet.Columns.Count - 1
            spCreditosActualizarSeleccionar.ActiveSheet.Columns(columna).Width += 10
        Next
        spCreditosActualizarSeleccionar.ActiveSheet.Columns("id").CellType = tipoEntero
        spCreditosActualizarSeleccionar.ActiveSheet.Columns("fecha").CellType = tipoFecha
        spCreditosActualizarSeleccionar.ActiveSheet.Columns("idCliente").CellType = tipoEntero
        spCreditosActualizarSeleccionar.ActiveSheet.Columns("nombreCliente").CellType = tipoTexto
        spCreditosActualizarSeleccionar.ActiveSheet.Columns("idMetodoPago").CellType = tipoEntero
        spCreditosActualizarSeleccionar.ActiveSheet.Columns("nombreMetodoPago").CellType = tipoTexto
        spCreditosActualizarSeleccionar.ActiveSheet.Columns("subtotal").CellType = tipoDoble
        spCreditosActualizarSeleccionar.ActiveSheet.Columns("descuento").CellType = tipoDoble
        spCreditosActualizarSeleccionar.ActiveSheet.Columns("total").CellType = tipoDoble
        spCreditosActualizarSeleccionar.ActiveSheet.Columns("estaPagado").CellType = tipoBooleano
        spCreditosActualizarSeleccionar.ActiveSheet.AddColumnHeaderSpanCell(0, spCreditosActualizarSeleccionar.ActiveSheet.Columns("id").Index, 2, 1)
        spCreditosActualizarSeleccionar.ActiveSheet.ColumnHeader.Cells(0, spCreditosActualizarSeleccionar.ActiveSheet.Columns("id").Index).Value = "No.".ToUpper()
        spCreditosActualizarSeleccionar.ActiveSheet.AddColumnHeaderSpanCell(0, spCreditosActualizarSeleccionar.ActiveSheet.Columns("fecha").Index, 2, 1)
        spCreditosActualizarSeleccionar.ActiveSheet.ColumnHeader.Cells(0, spCreditosActualizarSeleccionar.ActiveSheet.Columns("fecha").Index).Value = "Fecha".ToUpper()
        spCreditosActualizarSeleccionar.ActiveSheet.AddColumnHeaderSpanCell(0, spCreditosActualizarSeleccionar.ActiveSheet.Columns("idCliente").Index, 1, 2)
        spCreditosActualizarSeleccionar.ActiveSheet.ColumnHeader.Cells(0, spCreditosActualizarSeleccionar.ActiveSheet.Columns("idCliente").Index).Value = "C l i e n t e".ToUpper()
        spCreditosActualizarSeleccionar.ActiveSheet.ColumnHeader.Cells(1, spCreditosActualizarSeleccionar.ActiveSheet.Columns("idCliente").Index).Value = "No.".ToUpper()
        spCreditosActualizarSeleccionar.ActiveSheet.ColumnHeader.Cells(1, spCreditosActualizarSeleccionar.ActiveSheet.Columns("nombreCliente").Index).Value = "Nombre".ToUpper()
        spCreditosActualizarSeleccionar.ActiveSheet.AddColumnHeaderSpanCell(0, spCreditosActualizarSeleccionar.ActiveSheet.Columns("idMetodoPago").Index, 1, 2)
        spCreditosActualizarSeleccionar.ActiveSheet.ColumnHeader.Cells(0, spCreditosActualizarSeleccionar.ActiveSheet.Columns("idMetodoPago").Index).Value = "M é t o d o   d e   P a g o".ToUpper()
        spCreditosActualizarSeleccionar.ActiveSheet.ColumnHeader.Cells(1, spCreditosActualizarSeleccionar.ActiveSheet.Columns("idMetodoPago").Index).Value = "No.".ToUpper()
        spCreditosActualizarSeleccionar.ActiveSheet.ColumnHeader.Cells(1, spCreditosActualizarSeleccionar.ActiveSheet.Columns("nombreMetodoPago").Index).Value = "Nombre".ToUpper()
        spCreditosActualizarSeleccionar.ActiveSheet.AddColumnHeaderSpanCell(0, spCreditosActualizarSeleccionar.ActiveSheet.Columns("subtotal").Index, 2, 1)
        spCreditosActualizarSeleccionar.ActiveSheet.ColumnHeader.Cells(0, spCreditosActualizarSeleccionar.ActiveSheet.Columns("subtotal").Index).Value = "SubTotal".ToUpper()
        spCreditosActualizarSeleccionar.ActiveSheet.AddColumnHeaderSpanCell(0, spCreditosActualizarSeleccionar.ActiveSheet.Columns("descuento").Index, 2, 1)
        spCreditosActualizarSeleccionar.ActiveSheet.ColumnHeader.Cells(0, spCreditosActualizarSeleccionar.ActiveSheet.Columns("descuento").Index).Value = "Descuento".ToUpper()
        spCreditosActualizarSeleccionar.ActiveSheet.AddColumnHeaderSpanCell(0, spCreditosActualizarSeleccionar.ActiveSheet.Columns("total").Index, 2, 1)
        spCreditosActualizarSeleccionar.ActiveSheet.ColumnHeader.Cells(0, spCreditosActualizarSeleccionar.ActiveSheet.Columns("total").Index).Value = "Total".ToUpper()
        spCreditosActualizarSeleccionar.ActiveSheet.AddColumnHeaderSpanCell(0, spCreditosActualizarSeleccionar.ActiveSheet.Columns("estaPagado").Index, 2, 1)
        spCreditosActualizarSeleccionar.ActiveSheet.ColumnHeader.Cells(0, spCreditosActualizarSeleccionar.ActiveSheet.Columns("estaPagado").Index).Value = "Esta Pagado".ToUpper()
        spCreditosActualizarSeleccionar.ActiveSheet.Columns(0, spCreditosActualizarSeleccionar.ActiveSheet.Columns.Count - 1).AllowAutoFilter = True
        spCreditosActualizarSeleccionar.ActiveSheet.Columns(0, spCreditosActualizarSeleccionar.ActiveSheet.Columns.Count - 1).AllowAutoSort = True
        spCreditosActualizarSeleccionar.ActiveSheet.Columns(0, spCreditosActualizarSeleccionar.ActiveSheet.Columns.Count - 1).Locked = True
        MostrarOcultarColumnasDetalleActualizarSeleccionar(False)
        spCreditosActualizarSeleccionar.ActiveSheet.LockBackColor = Principal.colorCapturaBloqueada
        spCreditosActualizarSeleccionar.Visible = True
        spCreditosActualizarGuardar.Visible = False
        spCreditosActualizarSeleccionar.Refresh()

    End Sub

    Private Sub MostrarOcultarColumnasDetalleActualizarSeleccionar(ByVal valor As Boolean)

        spCreditosActualizarSeleccionar.ActiveSheet.Columns("idMetodoPago").Visible = valor
        spCreditosActualizarSeleccionar.ActiveSheet.Columns("nombreMetodoPago").Visible = valor
        spCreditosActualizarSeleccionar.ActiveSheet.Columns("subtotal").Visible = valor
        spCreditosActualizarSeleccionar.ActiveSheet.Columns("descuento").Visible = valor
        spCreditosActualizarSeleccionar.ActiveSheet.Columns("estaPagado").Visible = valor

    End Sub

    Private Sub CargarCreditosActualizarGuardar()

        If (Me.filaSeleccionadaCreditos >= 0) Then
            Dim id As Integer = ALMLogicaCreditos.Funciones.ValidarNumeroACero(spCreditosActualizarSeleccionar.ActiveSheet.Cells(Me.filaSeleccionadaCreditos, spCreditosActualizarSeleccionar.ActiveSheet.Columns("id").Index).Value)
            creditosAbonos.EId = id
            Dim datos As New DataTable
            datos = creditosAbonos.ObtenerListadoDetallado()
            spCreditosActualizarGuardar.ActiveSheet.DataSource = datos
            FormatearSpreadCreditosActualizarGuardar(datos.Rows.Count + 1)
            CalcularTotalesCreditosActualizar()
            btnGuardar.Enabled = True
            AsignarFoco(spCreditosActualizarGuardar)
        End If

    End Sub

    Private Sub FormatearSpreadCreditosActualizarGuardar(ByVal cantidadFilas As Integer)

        'spRecepcion.ActiveSheet.ColumnHeader.Columns(0, spRecepcion.ActiveSheet.ColumnCount - 1).BackColor = Principal.colorCaptura
        'spRecepcion.ActiveSheet.ColumnHeader.Cells(0, 0, 0, spRecepcion.ActiveSheet.Columns.Count - 1).Border = New FarPoint.Win.LineBorder(Color.Black, 2, False, True, False, False)
        'spRecepcion.ActiveSheet.ColumnHeader.Cells(spRecepcion.ActiveSheet.ColumnHeader.Rows.Count - 1, 0, spRecepcion.ActiveSheet.ColumnHeader.Rows.Count - 1, spRecepcion.ActiveSheet.Columns.Count - 1).Border = New FarPoint.Win.LineBorder(Color.Black, 2, False, False, False, True)
        spCreditosActualizarGuardar.ActiveSheet.Rows.Count = cantidadFilas
        spCreditosActualizarGuardar.ActiveSheet.ColumnHeader.RowCount = 2
        spCreditosActualizarGuardar.ActiveSheet.ColumnHeader.Rows(0, spCreditosActualizarGuardar.ActiveSheet.ColumnHeader.Rows.Count - 1).Font = New Font(Principal.tipoLetraSpread, Principal.tamañoLetraSpread, FontStyle.Bold)
        spCreditosActualizarGuardar.ActiveSheet.ColumnHeader.Rows(0).Height = Principal.alturaFilasEncabezadosChicosSpread
        spCreditosActualizarGuardar.ActiveSheet.ColumnHeader.Rows(1).Height = Principal.alturaFilasEncabezadosMedianosSpread
        spCreditosActualizarGuardar.ActiveSheet.OperationMode = FarPoint.Win.Spread.OperationMode.Normal
        ControlarSpreadEnterASiguienteColumna(spCreditosActualizarGuardar)
        Dim numeracion As Integer = 0
        spCreditosActualizarGuardar.ActiveSheet.Columns.Count += 10
        spCreditosActualizarGuardar.ActiveSheet.Columns(numeracion).Tag = "fecha" : numeracion += 1
        spCreditosActualizarGuardar.ActiveSheet.Columns(numeracion).Tag = "total" : numeracion += 1
        spCreditosActualizarGuardar.ActiveSheet.Columns.Count = numeracion
        spCreditosActualizarGuardar.ActiveSheet.Columns("fecha").Width = 100
        spCreditosActualizarGuardar.ActiveSheet.Columns("total").Width = 100
        For columna = 0 To spCreditosActualizarGuardar.ActiveSheet.Columns.Count - 1
            spCreditosActualizarGuardar.ActiveSheet.Columns(columna).Width += 10
        Next
        spCreditosActualizarGuardar.ActiveSheet.Columns("fecha").CellType = tipoFecha
        spCreditosActualizarGuardar.ActiveSheet.Columns("total").CellType = tipoDoble
        spCreditosActualizarGuardar.ActiveSheet.Columns(0, spCreditosActualizarGuardar.ActiveSheet.Columns.Count - 1).AllowAutoSort = True
        spCreditosActualizarGuardar.ActiveSheet.AddColumnHeaderSpanCell(0, 0, 1, spCreditosActualizarGuardar.ActiveSheet.Columns.Count)
        spCreditosActualizarGuardar.ActiveSheet.ColumnHeader.Cells(0, spCreditosActualizarGuardar.ActiveSheet.Columns("fecha").Index).Value = "A b o n o s".ToUpper()
        spCreditosActualizarGuardar.ActiveSheet.ColumnHeader.Cells(1, spCreditosActualizarGuardar.ActiveSheet.Columns("fecha").Index).Value = "Fecha".ToUpper()
        spCreditosActualizarGuardar.ActiveSheet.ColumnHeader.Cells(1, spCreditosActualizarGuardar.ActiveSheet.Columns("total").Index).Value = "Total".ToUpper()
        spCreditosActualizarGuardar.ActiveSheet.ColumnFooter.Visible = True
        spCreditosActualizarGuardar.ActiveSheet.ColumnFooter.Cells(0, spCreditosActualizarGuardar.ActiveSheet.Columns("fecha").Index).Value = "Total".ToUpper()
        spCreditosActualizarGuardar.ActiveSheet.ColumnFooter.Columns("fecha").CellType = tipoTexto
        spCreditosActualizarGuardar.ActiveSheet.ColumnFooter.Columns("total").CellType = tipoDoble
        spCreditosActualizarGuardar.ActiveSheet.ColumnFooter.Columns(0, spCreditosActualizarGuardar.ActiveSheet.Columns.Count - 1).BackColor = Principal.colorSpreadTotal
        spCreditosActualizarGuardar.ActiveSheet.ColumnFooter.Columns(0, spCreditosActualizarGuardar.ActiveSheet.Columns.Count - 1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right
        spCreditosActualizarGuardar.ActiveSheet.LockBackColor = Principal.colorCapturaBloqueada
        spCreditosActualizarGuardar.Left = Me.anchoMitad
        spCreditosActualizarGuardar.Width = Me.anchoMitad - 5 '- btnMostrarOcultarActualizar.Width - espacio 
        spCreditosActualizarGuardar.Visible = True
        spCreditosActualizarGuardar.Refresh()

    End Sub

    Private Sub ValidarGuardadoCreditosActualizar()

        CalcularTotalesCreditosActualizar()
        Me.esGuardadoValido = True
        ' Parte izquierda.
        Dim id As Integer = ALMLogicaCreditos.Funciones.ValidarNumeroACero(spCreditosActualizarSeleccionar.ActiveSheet.Cells(spCreditosActualizarSeleccionar.ActiveSheet.ActiveRowIndex, spCreditosActualizarSeleccionar.ActiveSheet.Columns("id").Index).Text)
        If (id <= 0) Then
            spCreditosActualizarSeleccionar.ActiveSheet.Cells(spCreditosActualizarSeleccionar.ActiveSheet.ActiveRowIndex, spCreditosActualizarSeleccionar.ActiveSheet.Columns("id").Index).BackColor = Principal.colorAdvertencia
            Me.esGuardadoValido = False
        End If
        ' Parte derecha.
        For fila As Integer = 0 To spCreditosActualizarGuardar.ActiveSheet.Rows.Count - 1
            Dim fecha As String = spCreditosActualizarGuardar.ActiveSheet.Cells(fila, spCreditosActualizarGuardar.ActiveSheet.Columns("fecha").Index).Text
            If (Not String.IsNullOrEmpty(fecha) AndAlso IsDate(fecha)) Then
                Dim total As String = spCreditosActualizarGuardar.ActiveSheet.Cells(fila, spCreditosActualizarGuardar.ActiveSheet.Columns("total").Index).Text
                Dim total2 As Double = ALMLogicaCreditos.Funciones.ValidarNumeroACero(spCreditosActualizarGuardar.ActiveSheet.Cells(fila, spCreditosActualizarGuardar.ActiveSheet.Columns("total").Index).Text)
                If (String.IsNullOrEmpty(total) Or total2 <= 0) Then
                    spCreditosActualizarGuardar.ActiveSheet.Cells(fila, spCreditosActualizarGuardar.ActiveSheet.Columns("total").Index).BackColor = Principal.colorAdvertencia
                    Me.esGuardadoValido = False
                End If
            End If
        Next
        Dim totalCredito As Double = ALMLogicaCreditos.Funciones.ValidarNumeroACero(spCreditosActualizarSeleccionar.ActiveSheet.Cells(Me.filaSeleccionadaCreditos, spCreditosActualizarSeleccionar.ActiveSheet.Columns("total").Index).Text)
        Dim totalAbonos As Double = ALMLogicaCreditos.Funciones.ValidarNumeroACero(spCreditosActualizarGuardar.ActiveSheet.ColumnFooter.Cells(0, spCreditosActualizarGuardar.ActiveSheet.Columns("total").Index).Value)
        If (totalAbonos > totalCredito) Then
            spCreditosActualizarSeleccionar.ActiveSheet.Cells(Me.filaSeleccionadaCreditos, spCreditosActualizarSeleccionar.ActiveSheet.Columns("total").Index).BackColor = Principal.colorAdvertencia
            spCreditosActualizarGuardar.ActiveSheet.ColumnFooter.Cells(0, spCreditosActualizarGuardar.ActiveSheet.Columns("total").Index).BackColor = Principal.colorAdvertencia
            Me.esGuardadoValido = False
        End If

    End Sub

    Private Sub GuardarEditarCreditosActualizar()

        EliminarCreditosActualizar(False)
        ' No capturables por el usuario.
        Dim estaPagado As Boolean = False
        Dim id As Integer = ALMLogicaCreditos.Funciones.ValidarNumeroACero(spCreditosActualizarSeleccionar.ActiveSheet.Cells(spCreditosActualizarSeleccionar.ActiveSheet.ActiveRowIndex, spCreditosActualizarSeleccionar.ActiveSheet.Columns("id").Index).Text)
        ' Parte inferior.
        For fila As Integer = 0 To spCreditosActualizarGuardar.ActiveSheet.Rows.Count - 1
            Dim fecha As Date = spCreditosActualizarGuardar.ActiveSheet.Cells(fila, spCreditosActualizarGuardar.ActiveSheet.Columns("fecha").Index).Value
            Dim total As Double = ALMLogicaCreditos.Funciones.ValidarNumeroACero(spCreditosActualizarGuardar.ActiveSheet.Cells(fila, spCreditosActualizarGuardar.ActiveSheet.Columns("total").Index).Value)
            Dim orden As Integer = fila
            If (id > 0 And total > 0) Then
                creditosAbonos.EId = id
                creditosAbonos.EFecha = fecha
                creditosAbonos.ETotal = total
                creditosAbonos.EOrden = orden
                creditosAbonos.Guardar()
            End If
        Next
        MessageBox.Show("Guardado finalizado.", "Finalizado.", MessageBoxButtons.OK)
        CargarCreditosActualizarGuardar()
        LimpiarPantalla()
        AsignarFoco(spCreditosActualizarSeleccionar)

    End Sub

    Private Sub EliminarCreditosActualizar(ByVal conMensaje As Boolean)

        Me.Cursor = Cursors.WaitCursor
        Dim respuestaSi As Boolean = False
        If (conMensaje) Then
            If (MessageBox.Show("Confirmas que deseas eliminar esta captura?", "Confirmación.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes) Then
                respuestaSi = True
            End If
        End If
        If ((respuestaSi) Or (Not conMensaje)) Then
            Dim id As Integer = ALMLogicaCreditos.Funciones.ValidarNumeroACero(spCreditosActualizarSeleccionar.ActiveSheet.Cells(spCreditosActualizarSeleccionar.ActiveSheet.ActiveRowIndex, spCreditosActualizarSeleccionar.ActiveSheet.Columns("id").Index).Text)
            creditosAbonos.EId = id
            creditosAbonos.Eliminar()
        End If
        If (conMensaje And respuestaSi) Then
            MessageBox.Show("Eliminado finalizado.", "Finalizado.", MessageBoxButtons.OK)
            LimpiarPantalla()
            CargarIdConsecutivo()
            AsignarFoco(txtId)
        End If
        Me.Cursor = Cursors.Default

    End Sub

    Private Sub CalcularTotalesCreditosActualizar()

        Dim total As Double = 0
        For fila = 0 To spCreditosActualizarGuardar.ActiveSheet.Rows.Count - 1
            total += ALMLogicaCreditos.Funciones.ValidarNumeroACero(spCreditosActualizarGuardar.ActiveSheet.Cells(fila, spCreditosActualizarGuardar.ActiveSheet.Columns("total").Index).Value)
        Next
        spCreditosActualizarGuardar.ActiveSheet.ColumnFooter.Cells(0, spCreditosActualizarGuardar.ActiveSheet.Columns("total").Index).Value = total

    End Sub

#End Region

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
        proveedor = 5
        cliente = 7

    End Enum

    Enum OpcionPestana

        capturar = 1
        actualizar = 2

    End Enum
     
#End Region

End Class