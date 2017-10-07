Imports System.IO
Imports System.ComponentModel
Imports System.Threading

Public Class Principal

    ' Variables de objetos de entidades.
    Public usuarios As New ALMEntidadesPedidos.Usuarios()
    Public pedidos As New ALMEntidadesPedidos.Pedidos()
    Public entradas As New ALMEntidadesPedidos.Entradas()
    Public salidas As New ALMEntidadesPedidos.Salidas()
    Public almacenes As New ALMEntidadesPedidos.Almacenes()
    Public familias As New ALMEntidadesPedidos.Familias()
    Public subFamilias As New ALMEntidadesPedidos.SubFamilias()
    Public articulos As New ALMEntidadesPedidos.Articulos()
    Public unidadesMedidas As New ALMEntidadesPedidos.UnidadesMedidas()
    Public proveedores As New ALMEntidadesPedidos.Proveedores()
    Public clientes As New ALMEntidadesPedidos.Clientes()
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
    Public Shared colorAreaGris = Color.White 
    ' Variables generales.
    Public nombreEstePrograma As String = String.Empty
    Public estaCerrando As Boolean = False
    Public estaMostrado As Boolean = False
    Public ejecutarProgramaPrincipal As New ProcessStartInfo()
    Public prefijoBaseDatosAlmacen As String = "ALM" & "_"
    Public cantidadFilas As Integer = 1
    Public opcionCatalogoSeleccionada As Integer = 0 : Public opcionPestanaSeleccionada As Integer = -1
    Public esGuardadoValido As Boolean = True
    Public esIzquierdaCapturar As Boolean = False : Public esIzquierdaActualizar As Boolean = False
    Public datosCatalogo As New DataTable
    ' Variables fijas.
    Public idAlmacenPredeterminado As Integer = 1 : Public idFamiliaPredeterminado As Integer = 1 : Public idSubFamiliaPredeterminado As Integer = 1
    Public idOrigen As Integer = 2 ' El 2 es para compras.
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
        FormatearSpreadPedidos()
        CargarCombosCaptura()
        CargarIdConsecutivo()
        AsignarFoco(txtId)
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

    Private Sub spCapturarPedidos_DialogKey(sender As Object, e As FarPoint.Win.Spread.DialogKeyEventArgs) Handles spPedidosCapturar.DialogKey

        If (e.KeyData = Keys.Enter) Then
            ControlarSpreadEnter(spPedidosCapturar)
        End If

    End Sub

    Private Sub spPedidos_KeyDown(sender As Object, e As KeyEventArgs) Handles spPedidosCapturar.KeyDown

        If (e.KeyData = Keys.F6) Then ' Eliminar un registro.
            If (MessageBox.Show("Confirmas que deseas eliminar el registro seleccionado?", "Confirmación.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes) Then
                EliminarRegistroDeSpread(spPedidosCapturar)
            End If
        ElseIf (e.KeyData = Keys.Enter) Then ' Validar registros.
            ControlarSpreadEnter(spPedidosCapturar)
        ElseIf (e.KeyData = Keys.F5) Then ' Abrir catalogos. 
            CargarCatalogoEnSpread()
        ElseIf (e.KeyData = Keys.Escape) Then
            spPedidosCapturar.ActiveSheet.SetActiveCell(0, 0)
            AsignarFoco(cbClientes)
        End If

    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click

        If (Me.opcionPestanaSeleccionada = OpcionPestana.capturar) Then
            ValidarGuardado()
            If (Me.esGuardadoValido) Then
                GuardarEditarPedidos()
            End If
        Else
            Me.Cursor = Cursors.WaitCursor
            ActualizarPedidosActualizar()
            Me.Cursor = Cursors.Default
        End If

    End Sub

    Private Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click

        If (Me.opcionPestanaSeleccionada = OpcionPestana.capturar) Then
            EliminarPedidos(True)
        Else

        End If

    End Sub

    Private Sub btnGuardar_MouseEnter(sender As Object, e As EventArgs) Handles btnGuardar.MouseEnter

        AsignarTooltips("Guardar.")

    End Sub

    Private Sub btnEliminar_MouseEnter(sender As Object, e As EventArgs) Handles btnEliminar.MouseEnter

        AsignarTooltips("Eliminar.")

    End Sub

    Private Sub btnSalir_MouseEnter(sender As Object, e As EventArgs) Handles btnSalir.MouseEnter

        AsignarTooltips("Salir.")

    End Sub

    Private Sub pnlEncabezado_MouseEnter(sender As Object, e As EventArgs) Handles pnlPie.MouseEnter, pnlEncabezado.MouseEnter, pnlCuerpo.MouseEnter

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

        MostrarAyuda()

    End Sub

    Private Sub btnAyuda_MouseEnter(sender As Object, e As EventArgs) Handles btnAyuda.MouseEnter

        AsignarTooltips("Ayuda.")

    End Sub

    Private Sub txtId_KeyDown(sender As Object, e As KeyEventArgs) Handles txtId.KeyDown

        If (e.KeyData = Keys.Enter) Then
            e.SuppressKeyPress = True
            If (IsNumeric(txtId.Text)) Then
                e.SuppressKeyPress = True
                CargarPedidos()
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

    Private Sub cbClientes_KeyDown(sender As Object, e As KeyEventArgs) Handles cbClientes.KeyDown

        If (e.KeyData = Keys.Enter) Then
            e.SuppressKeyPress = True
            If (cbClientes.SelectedValue > 0) Then
                AsignarFoco(spPedidosCapturar)
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

    Private Sub btnIdAnterior_Click(sender As Object, e As EventArgs) Handles btnIdAnterior.Click

        If (ALMLogicaPedidos.Funciones.ValidarNumeroACero(txtId.Text) > 1) Then
            txtId.Text -= 1
            CargarPedidos()
        End If

    End Sub

    Private Sub btnIdSiguiente_Click(sender As Object, e As EventArgs) Handles btnIdSiguiente.Click

        If (ALMLogicaPedidos.Funciones.ValidarNumeroACero(txtId.Text) >= 1) Then
            txtId.Text += 1
            CargarPedidos()
        End If

    End Sub

    Private Sub btnMostrarOcultar_Click(sender As Object, e As EventArgs) Handles btnMostrarOcultar.Click

        MostrarOcultar()

    End Sub

    Private Sub btnMostrarOcultar_MouseEnter(sender As Object, e As EventArgs) Handles btnMostrarOcultar.MouseEnter

        If (Me.esIzquierdaCapturar) Then
            AsignarTooltips("Mostrar.")
        Else
            AsignarTooltips("Ocultar.")
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

        If (Me.opcionCatalogoSeleccionada = OpcionCatalogo.articulo) Then
            BuscarCatalogosRapidoArticulos()
        Else
            BuscarCatalogos()
        End If

    End Sub

    Private Sub chkMostrarDatos_CheckedChanged(sender As Object, e As EventArgs) Handles chkMostrarDetallado.CheckedChanged

        If (Me.estaMostrado) Then
            If (chkMostrarDetallado.Checked) Then
                spPedidosCapturar.ActiveSheet.Columns(spPedidosCapturar.ActiveSheet.Columns("idFamilia").Index, spPedidosCapturar.ActiveSheet.Columns("nombreSubFamilia").Index).Visible = True
                spPedidosCapturar.ActiveSheet.Columns("nombreArticulo").Visible = True
            Else
                spPedidosCapturar.ActiveSheet.Columns(spPedidosCapturar.ActiveSheet.Columns("idFamilia").Index, spPedidosCapturar.ActiveSheet.Columns("nombreSubFamilia").Index).Visible = False
                spPedidosCapturar.ActiveSheet.Columns("nombreArticulo").Visible = False
            End If
        End If

    End Sub

    Private Sub tcPestanas_SelectedIndexChanged(sender As Object, e As EventArgs) Handles tcPestanas.SelectedIndexChanged

        If (tcPestanas.SelectedIndex = 0) Then
            Me.opcionPestanaSeleccionada = OpcionPestana.capturar
            btnEliminar.Enabled = True
        ElseIf (tcPestanas.SelectedIndex = 1) Then
            Me.opcionPestanaSeleccionada = OpcionPestana.actualizar
            btnEliminar.Enabled = False
            btnGuardar.Enabled = False
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
        CargarPedidosActualizar()
        Me.Cursor = Cursors.Default

    End Sub

    Private Sub chkMostrarDetalladoActualizar_CheckedChanged(sender As Object, e As EventArgs) Handles chkMostrarDetalladoActualizar.CheckedChanged

        If (Me.estaMostrado) Then
            If (chkMostrarDetalladoActualizar.Checked) Then
                spPedidosActualizar.ActiveSheet.Columns("idCliente").Visible = True
                spPedidosActualizar.ActiveSheet.Columns("idProveedor").Visible = True
                spPedidosActualizar.ActiveSheet.Columns("idProveedor").Visible = True
                spPedidosActualizar.ActiveSheet.Columns("idArticulo").Visible = True
                spPedidosActualizar.ActiveSheet.Columns("nombreArticulo").Visible = True
                spPedidosActualizar.ActiveSheet.Columns(spPedidosActualizar.ActiveSheet.Columns("idAlmacen").Index, spPedidosActualizar.ActiveSheet.Columns("nombreSubFamilia").Index).Visible = True
                spPedidosActualizar.ActiveSheet.Columns("observaciones").Visible = True
            Else
                spPedidosActualizar.ActiveSheet.Columns("idCliente").Visible = False
                spPedidosActualizar.ActiveSheet.Columns("idProveedor").Visible = False
                spPedidosActualizar.ActiveSheet.Columns("idProveedor").Visible = False
                spPedidosActualizar.ActiveSheet.Columns("idArticulo").Visible = False
                spPedidosActualizar.ActiveSheet.Columns("nombreArticulo").Visible = False
                spPedidosActualizar.ActiveSheet.Columns(spPedidosActualizar.ActiveSheet.Columns("idAlmacen").Index, spPedidosActualizar.ActiveSheet.Columns("nombreSubFamilia").Index).Visible = False
                spPedidosActualizar.ActiveSheet.Columns("observaciones").Visible = False
            End If
        End If

    End Sub

    Private Sub spPedidosActualizar_CellClick(sender As Object, e As FarPoint.Win.Spread.CellClickEventArgs) Handles spPedidosActualizar.CellClick

        ' Se valida que no se repita ningun estatus.
        Dim fila As Integer = e.Row
        Dim columna As Integer = e.Column
        If (columna = spPedidosActualizar.ActiveSheet.Columns("confirmado").Index) Then
            spPedidosActualizar.ActiveSheet.Cells(fila, spPedidosActualizar.ActiveSheet.Columns("cancelado").Index).Value = False
            spPedidosActualizar.ActiveSheet.Cells(fila, spPedidosActualizar.ActiveSheet.Columns("recibido").Index).Value = False
            spPedidosActualizar.ActiveSheet.Cells(fila, spPedidosActualizar.ActiveSheet.Columns("entregado").Index).Value = False
        ElseIf (columna = spPedidosActualizar.ActiveSheet.Columns("cancelado").Index) Then
            spPedidosActualizar.ActiveSheet.Cells(fila, spPedidosActualizar.ActiveSheet.Columns("confirmado").Index).Value = False
            spPedidosActualizar.ActiveSheet.Cells(fila, spPedidosActualizar.ActiveSheet.Columns("recibido").Index).Value = False
            spPedidosActualizar.ActiveSheet.Cells(fila, spPedidosActualizar.ActiveSheet.Columns("entregado").Index).Value = False
        ElseIf (columna = spPedidosActualizar.ActiveSheet.Columns("recibido").Index) Then
            spPedidosActualizar.ActiveSheet.Cells(fila, spPedidosActualizar.ActiveSheet.Columns("confirmado").Index).Value = False
            spPedidosActualizar.ActiveSheet.Cells(fila, spPedidosActualizar.ActiveSheet.Columns("cancelado").Index).Value = False
            spPedidosActualizar.ActiveSheet.Cells(fila, spPedidosActualizar.ActiveSheet.Columns("entregado").Index).Value = False
        ElseIf (columna = spPedidosActualizar.ActiveSheet.Columns("entregado").Index) Then
            spPedidosActualizar.ActiveSheet.Cells(fila, spPedidosActualizar.ActiveSheet.Columns("confirmado").Index).Value = False
            spPedidosActualizar.ActiveSheet.Cells(fila, spPedidosActualizar.ActiveSheet.Columns("cancelado").Index).Value = False
            spPedidosActualizar.ActiveSheet.Cells(fila, spPedidosActualizar.ActiveSheet.Columns("recibido").Index).Value = False
        End If

    End Sub

#End Region

#Region "Métodos"

#Region "Básicos"

    Private Sub BuscarCatalogos()

        Dim valorBuscado As String = txtBuscarCatalogo.Text.Replace("á", "a").Replace("é", "e").Replace("í", "i").Replace("ó", "o").Replace("ú", "u")
        For fila = 0 To spCatalogos.ActiveSheet.Rows.Count - 1
            Dim valorSpread As String = ALMLogicaPedidos.Funciones.ValidarLetra(spCatalogos.ActiveSheet.Cells(fila, spCatalogos.ActiveSheet.Columns("id").Index).Text & spCatalogos.ActiveSheet.Cells(fila, spCatalogos.ActiveSheet.Columns("nombre").Index).Text).Replace("á", "a").Replace("é", "e").Replace("í", "i").Replace("ó", "o").Replace("ú", "u")
            If (Me.opcionCatalogoSeleccionada = OpcionCatalogo.articulo) Then
                valorSpread = ALMLogicaPedidos.Funciones.ValidarLetra(spCatalogos.ActiveSheet.Cells(fila, spCatalogos.ActiveSheet.Columns("id").Index).Text & spCatalogos.ActiveSheet.Cells(fila, spCatalogos.ActiveSheet.Columns("nombre").Index).Text & spCatalogos.ActiveSheet.Cells(fila, spCatalogos.ActiveSheet.Columns("codigo").Index).Text & spCatalogos.ActiveSheet.Cells(fila, spCatalogos.ActiveSheet.Columns("pagina").Index).Text & spCatalogos.ActiveSheet.Cells(fila, spCatalogos.ActiveSheet.Columns("color").Index).Text & spCatalogos.ActiveSheet.Cells(fila, spCatalogos.ActiveSheet.Columns("talla").Index).Text & spCatalogos.ActiveSheet.Cells(fila, spCatalogos.ActiveSheet.Columns("modelo").Index).Text & spCatalogos.ActiveSheet.Cells(fila, spCatalogos.ActiveSheet.Columns("codigoInternet").Index).Text).Replace("á", "a").Replace("é", "e").Replace("í", "i").Replace("ó", "o").Replace("ú", "u")
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
            Dim anchoMenor As Integer = btnMostrarOcultar.Width
            Dim espacio As Integer = 1
            If (Not Me.esIzquierdaCapturar) Then
                pnlCapturaSuperior.Left = -pnlCapturaSuperior.Width + anchoMenor
                spPedidosCapturar.Left = anchoMenor + espacio
                spPedidosCapturar.Width = Me.anchoTotal - anchoMenor - espacio - 5
                Me.esIzquierdaCapturar = True
            Else
                pnlCapturaSuperior.Left = 0
                spPedidosCapturar.Left = pnlCapturaSuperior.Width + espacio
                spPedidosCapturar.Width = Me.anchoTotal - pnlCapturaSuperior.Width - espacio - 5
                Me.esIzquierdaCapturar = False
            End If
        Else
            Dim anchoMenor As Integer = btnMostrarOcultarActualizar.Width
            Dim espacio As Integer = 1
            If (Not Me.esIzquierdaActualizar) Then
                pnlFiltrado.Left = -pnlFiltrado.Width + anchoMenor
                spPedidosActualizar.Left = anchoMenor + espacio
                spPedidosActualizar.Width = Me.anchoTotal - anchoMenor - espacio - 5
                Me.esIzquierdaActualizar = True
            Else
                pnlFiltrado.Left = 0
                spPedidosActualizar.Left = pnlFiltrado.Width + espacio
                spPedidosActualizar.Width = Me.anchoTotal - pnlFiltrado.Width - espacio - 5
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
            txtAyuda.Text = "Sección de Ayuda: " & vbNewLine & vbNewLine & "* Teclas básicas: " & vbNewLine & "F5 sirve para mostrar catálogos. " & vbNewLine & "F6 sirve para eliminar un registro únicamente. " & vbNewLine & "Escape sirve para ocultar catálogos que se encuentren desplegados. " & vbNewLine & vbNewLine & "* Catálogos desplegados: " & vbNewLine & "Cuando se muestra algún catálogo, al seleccionar alguna opción de este, se va mostrando en tiempo real en la captura de donde se originó. Cuando se le da doble clic en alguna opción o a la tecla escape se oculta dicho catálogo. " & vbNewLine & vbNewLine & "* Datos obligatorios: " & vbNewLine & "Todos los que tengan el simbolo * son estrictamente obligatorios." & vbNewLine & vbNewLine & "* Captura:" & vbNewLine & "* Parte superior: " & vbNewLine & "En esta parte se capturarán todos los datos que son generales, tal cual como el número de la entrada, el almacén al que corresponde, etc." & vbNewLine & "* Parte inferior: " & vbNewLine & "En esta parte se capturarán todos los datos que pueden combinarse, por ejemplo los distintos artículos de ese número de entrada." & vbNewLine & vbNewLine & "* Existen los botones de guardar/editar y eliminar todo dependiendo lo que se necesite hacer. "
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

        If ((Not ALMLogicaPedidos.Usuarios.accesoTotal) Or (ALMLogicaPedidos.Usuarios.accesoTotal = 0) Or (ALMLogicaPedidos.Usuarios.accesoTotal = False)) Then
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
        tp.SetToolTip(Me.pnlEncabezado, "Datos Principales.")
        tp.SetToolTip(Me.btnAyuda, "Ayuda.")
        tp.SetToolTip(Me.btnSalir, "Salir.")
        tp.SetToolTip(Me.btnGuardar, "Guardar.")
        tp.SetToolTip(Me.btnEliminar, "Eliminar.")

    End Sub

    Private Sub AsignarTooltips(ByVal texto As String)

        lblDescripcionTooltip.Text = texto

    End Sub

    Private Sub ConfigurarConexiones()

        If (Me.esDesarrollo) Then
            ALMLogicaPedidos.Directorios.id = 2
            ALMLogicaPedidos.Directorios.instanciaSql = "BERRY1-DELL\SQLEXPRESS2008"
            ALMLogicaPedidos.Directorios.usuarioSql = "AdminBerry"
            ALMLogicaPedidos.Directorios.contrasenaSql = "@berry2017"
            pnlEncabezado.BackColor = Color.DarkRed
            pnlPie.BackColor = Color.DarkRed
        Else
            ALMLogicaPedidos.Directorios.ObtenerParametros()
            ALMLogicaPedidos.Usuarios.ObtenerParametros()
        End If
        ALMLogicaPedidos.Programas.bdCatalogo = "Catalogo" & ALMLogicaPedidos.Directorios.id
        ALMLogicaPedidos.Programas.bdConfiguracion = "Configuracion" & ALMLogicaPedidos.Directorios.id
        ALMLogicaPedidos.Programas.bdAlmacen = "Almacen" & ALMLogicaPedidos.Directorios.id
        ALMEntidadesPedidos.BaseDatos.ECadenaConexionCatalogo = ALMLogicaPedidos.Programas.bdCatalogo
        ALMEntidadesPedidos.BaseDatos.ECadenaConexionConfiguracion = ALMLogicaPedidos.Programas.bdConfiguracion
        ALMEntidadesPedidos.BaseDatos.ECadenaConexionAlmacen = ALMLogicaPedidos.Programas.bdAlmacen
        ALMEntidadesPedidos.BaseDatos.AbrirConexionCatalogo()
        ALMEntidadesPedidos.BaseDatos.AbrirConexionConfiguracion()
        ALMEntidadesPedidos.BaseDatos.AbrirConexionAlmacen()
        ConsultarInformacionUsuario()
        CargarPrefijoBaseDatosAlmacen()

    End Sub

    Private Sub CargarPrefijoBaseDatosAlmacen()

        ALMLogicaPedidos.Programas.prefijoBaseDatosAlmacen = Me.prefijoBaseDatosAlmacen

    End Sub

    Private Sub ConsultarInformacionUsuario()

        Dim lista As New List(Of ALMEntidadesPedidos.Usuarios)
        usuarios.EId = ALMLogicaPedidos.Usuarios.id
        lista = usuarios.ObtenerListado()
        If (lista.Count = 1) Then
            ALMLogicaPedidos.Usuarios.id = lista(0).EId
            ALMLogicaPedidos.Usuarios.nombre = lista(0).ENombre
            ALMLogicaPedidos.Usuarios.contrasena = lista(0).EContrasena
            ALMLogicaPedidos.Usuarios.nivel = lista(0).ENivel
            ALMLogicaPedidos.Usuarios.accesoTotal = lista(0).EAccesoTotal
        End If

    End Sub

    Private Sub CargarEncabezadosTitulos()

        lblEncabezadoPrograma.Text = "Programa: " + Me.Text
        lblEncabezadoEmpresa.Text = "Directorio: " + ALMLogicaPedidos.Directorios.nombre
        lblEncabezadoUsuario.Text = "Usuario: " + ALMLogicaPedidos.Usuarios.nombre
        Me.Text = "Programa:  " + Me.nombreEstePrograma + "              Directorio:  " + ALMLogicaPedidos.Directorios.nombre + "              Usuario:  " + ALMLogicaPedidos.Usuarios.nombre
        hiloEncabezadosTitulos.Abort()

    End Sub

    Private Sub AbrirPrograma(nombre As String, salir As Boolean)

        If (Me.esDesarrollo) Then
            Exit Sub
        End If
        ejecutarProgramaPrincipal.UseShellExecute = True
        ejecutarProgramaPrincipal.FileName = nombre & Convert.ToString(".exe")
        ejecutarProgramaPrincipal.WorkingDirectory = Application.StartupPath
        ejecutarProgramaPrincipal.Arguments = ALMLogicaPedidos.Directorios.id.ToString().Trim().Replace(" ", "|") & " " & ALMLogicaPedidos.Directorios.nombre.ToString().Trim().Replace(" ", "|") & " " & ALMLogicaPedidos.Directorios.descripcion.ToString().Trim().Replace(" ", "|") & " " & ALMLogicaPedidos.Directorios.rutaLogo.ToString().Trim().Replace(" ", "|") & " " & ALMLogicaPedidos.Directorios.esPredeterminado.ToString().Trim().Replace(" ", "|") & " " & ALMLogicaPedidos.Directorios.instanciaSql.ToString().Trim().Replace(" ", "|") & " " & ALMLogicaPedidos.Directorios.usuarioSql.ToString().Trim().Replace(" ", "|") & " " & ALMLogicaPedidos.Directorios.contrasenaSql.ToString().Trim().Replace(" ", "|") & " " & "Aquí terminan los de directorios, indice 9 ;)".Replace(" ", "|") & " " & ALMLogicaPedidos.Usuarios.id.ToString().Trim().Replace(" ", "|") & " " & "Aquí terminan los de usuario, indice 11 ;)".Replace(" ", "|")
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
        Me.arriba = spPedidosCapturar.Top
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

        CargarAlmacenes()
        CargarClientes()

    End Sub

    Private Sub LimpiarPantalla()

        If (Me.opcionPestanaSeleccionada = OpcionPestana.capturar) Then
            For Each c As Control In pnlCapturaSuperior.Controls
                c.BackColor = Color.White
            Next
            For fila = 0 To spPedidosCapturar.ActiveSheet.Rows.Count - 1
                For columna = 0 To spPedidosCapturar.ActiveSheet.Columns.Count - 1
                    spPedidosCapturar.ActiveSheet.Cells(fila, columna).BackColor = Color.White
                Next
            Next
            If (Not chkMantenerDatos.Checked) Then
                dtpFecha.Value = Today
                cbClientes.SelectedIndex = 0
            End If
            spPedidosCapturar.ActiveSheet.DataSource = Nothing
            spPedidosCapturar.ActiveSheet.Rows.Count = 1
            spPedidosCapturar.ActiveSheet.SetActiveCell(0, 0)
            LimpiarSpread(spPedidosCapturar)
        ElseIf (Me.opcionPestanaSeleccionada = OpcionPestana.actualizar) Then
            'spActualizarPedidos.ActiveSheet.DataSource = Nothing
            'spActualizarPedidos.ActiveSheet.Rows.Count = 1
            'spActualizarPedidos.ActiveSheet.SetActiveCell(0, 0)
            'LimpiarSpread(spActualizarPedidos)
        End If

    End Sub

    Private Sub LimpiarSpread(ByVal spread As FarPoint.Win.Spread.FpSpread)

        spread.ActiveSheet.ClearRange(0, 0, spread.ActiveSheet.Rows.Count, spread.ActiveSheet.Columns.Count, True)

    End Sub

    Private Sub CargarClientes()

        cbClientes.DataSource = clientes.ObtenerListadoReporteCombos()
        cbClientes.DisplayMember = "IdNombre"
        cbClientes.ValueMember = "Id"

    End Sub

    Private Sub CargarAlmacenes()

        cbAlmacenes.DataSource = almacenes.ObtenerListadoReporte()
        cbAlmacenes.DisplayMember = "IdNombre"
        cbAlmacenes.ValueMember = "Id"
        cbAlmacenes.SelectedValue = 1
        cbAlmacenes.Enabled = False

    End Sub

    Private Sub FormatearSpread()

        ' Se cargan tipos de datos de spread.
        CargarTiposDeDatos()
        ' Se cargan las opciones generales. 
        pnlCatalogos.Visible = False
        spPedidosCapturar.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Seashell
        spPedidosActualizar.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Seashell
        spCatalogos.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Midnight
        spPedidosCapturar.ActiveSheet.GrayAreaBackColor = Principal.colorAreaGris
        spPedidosActualizar.ActiveSheet.GrayAreaBackColor = Principal.colorAreaGris
        spPedidosCapturar.Font = New Font(Principal.tipoLetraSpread, Principal.tamañoLetraSpread, FontStyle.Regular)
        spPedidosActualizar.Font = New Font(Principal.tipoLetraSpread, Principal.tamañoLetraSpread, FontStyle.Regular)
        spCatalogos.Font = New Font(Principal.tipoLetraSpread, Principal.tamañoLetraSpread, FontStyle.Regular)
        spPedidosCapturar.ActiveSheet.ColumnHeader.Rows(0).Height = Principal.alturaFilasEncabezadosGrandesSpread
        spPedidosActualizar.ActiveSheet.ColumnHeader.Rows(0).Height = Principal.alturaFilasEncabezadosGrandesSpread
        spCatalogos.ActiveSheet.ColumnHeader.Rows(0).Height = Principal.alturaFilasEncabezadosGrandesSpread
        spPedidosCapturar.ActiveSheet.Rows(-1).Height = Principal.alturaFilasSpread
        spPedidosActualizar.ActiveSheet.Rows(-1).Height = Principal.alturaFilasSpread
        spCatalogos.ActiveSheet.Rows(-1).Height = Principal.alturaFilasSpread
        spPedidosCapturar.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        spPedidosCapturar.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        spPedidosActualizar.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        spPedidosActualizar.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        spCatalogos.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never
        spCatalogos.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Always
        spPedidosCapturar.EditModeReplace = True

    End Sub

    Private Sub EliminarRegistroDeSpread(ByVal spread As FarPoint.Win.Spread.FpSpread)

        If (spread.ActiveSheet.ActiveRowIndex > 0) Then
            spread.ActiveSheet.Rows.Remove(spread.ActiveSheet.ActiveRowIndex, 1)
        Else
            spread.ActiveSheet.ClearRange(spread.ActiveSheet.ActiveRowIndex, 0, 1, spread.ActiveSheet.Columns.Count, False)
            spread.ActiveSheet.SetActiveCell(spread.ActiveSheet.ActiveRowIndex, 0)
        End If

    End Sub

    Private Sub ControlarSpreadEnter(ByVal spread As FarPoint.Win.Spread.FpSpread)

        Dim columnaActiva As Integer = spread.ActiveSheet.ActiveColumnIndex
        If (columnaActiva = spread.ActiveSheet.Columns.Count - 1) Then
            spread.ActiveSheet.Rows.Count += 1
        End If
        If (spread.Name = spPedidosCapturar.Name) Then
            Dim fila As Integer = 0
            If (columnaActiva = spPedidosCapturar.ActiveSheet.Columns("idProveedor").Index) Then
                fila = spPedidosCapturar.ActiveSheet.ActiveRowIndex
                Dim idProveedor As Integer = ALMLogicaPedidos.Funciones.ValidarNumeroACero(spPedidosCapturar.ActiveSheet.Cells(fila, spPedidosCapturar.ActiveSheet.Columns("idProveedor").Index).Value)
                proveedores.EId = idProveedor
                If (idProveedor > 0) Then
                    Dim datosProveedor As New DataTable
                    datosProveedor = proveedores.ObtenerListadoReporte()
                    If (datosProveedor.Rows.Count > 0) Then
                        spPedidosCapturar.ActiveSheet.Cells(fila, spPedidosCapturar.ActiveSheet.Columns("nombreProveedor").Index).Value = datosProveedor.Rows(0).Item("Nombre")
                        spPedidosCapturar.ActiveSheet.Cells(fila, spPedidosCapturar.ActiveSheet.Columns("idFamilia").Index).Value = 1 ' Se cargan automaticamente.
                        spPedidosCapturar.ActiveSheet.Cells(fila, spPedidosCapturar.ActiveSheet.Columns("idSubFamilia").Index).Value = 1 ' Se cargan automaticamente.
                        spPedidosCapturar.ActiveSheet.SetActiveCell(fila, spPedidosCapturar.ActiveSheet.ActiveColumnIndex + 1)
                    Else
                        spPedidosCapturar.ActiveSheet.Cells(fila, spPedidosCapturar.ActiveSheet.Columns("idProveedor").Index, fila, spPedidosCapturar.ActiveSheet.Columns("nombreProveedor").Index).Value = String.Empty
                        spPedidosCapturar.ActiveSheet.SetActiveCell(fila, spPedidosCapturar.ActiveSheet.ActiveColumnIndex - 1)
                    End If
                Else
                    spPedidosCapturar.ActiveSheet.Cells(fila, spPedidosCapturar.ActiveSheet.Columns("idFamilia").Index, fila, spPedidosCapturar.ActiveSheet.Columns("nombreFamilia").Index).Value = String.Empty
                    spPedidosCapturar.ActiveSheet.ClearSelection()
                    spPedidosCapturar.ActiveSheet.SetActiveCell(fila, 0)
                End If
            ElseIf (columnaActiva = spPedidosCapturar.ActiveSheet.Columns("idFamilia").Index) Then
                fila = spPedidosCapturar.ActiveSheet.ActiveRowIndex
                Dim idAlmacen As Integer = ALMLogicaPedidos.Funciones.ValidarNumeroACero(ALMLogicaPedidos.Funciones.ValidarNumeroACero(cbAlmacenes.SelectedValue))
                Dim idFamilia As Integer = ALMLogicaPedidos.Funciones.ValidarNumeroACero(spPedidosCapturar.ActiveSheet.Cells(fila, spPedidosCapturar.ActiveSheet.Columns("idFamilia").Index).Value)
                familias.EIdAlmacen = idAlmacen
                familias.EId = idFamilia
                If (idAlmacen > 0 And idFamilia > 0) Then
                    Dim lista As New List(Of ALMEntidadesPedidos.Familias)
                    lista = familias.ObtenerListado()
                    If (lista.Count = 1) Then
                        spPedidosCapturar.ActiveSheet.Cells(fila, spPedidosCapturar.ActiveSheet.Columns("nombreFamilia").Index).Value = lista(0).ENombre
                        spPedidosCapturar.ActiveSheet.SetActiveCell(fila, spPedidosCapturar.ActiveSheet.ActiveColumnIndex + 1)
                    Else
                        spPedidosCapturar.ActiveSheet.Cells(fila, spPedidosCapturar.ActiveSheet.Columns("idFamilia").Index, fila, spPedidosCapturar.ActiveSheet.Columns("nombreFamilia").Index).Value = String.Empty
                        spPedidosCapturar.ActiveSheet.SetActiveCell(fila, spPedidosCapturar.ActiveSheet.ActiveColumnIndex - 1)
                    End If
                Else
                    spPedidosCapturar.ActiveSheet.Cells(fila, spPedidosCapturar.ActiveSheet.Columns("idFamilia").Index, fila, spPedidosCapturar.ActiveSheet.Columns("nombreFamilia").Index).Value = String.Empty
                    spPedidosCapturar.ActiveSheet.ClearSelection()
                    spPedidosCapturar.ActiveSheet.SetActiveCell(fila, 0)
                End If
            ElseIf (columnaActiva = spPedidosCapturar.ActiveSheet.Columns("idSubFamilia").Index) Then
                fila = spPedidosCapturar.ActiveSheet.ActiveRowIndex
                Dim idAlmacen As Integer = ALMLogicaPedidos.Funciones.ValidarNumeroACero(cbAlmacenes.SelectedValue)
                Dim idFamilia As Integer = ALMLogicaPedidos.Funciones.ValidarNumeroACero(spPedidosCapturar.ActiveSheet.Cells(fila, spPedidosCapturar.ActiveSheet.Columns("idFamilia").Index).Value)
                Dim idSubFamilia As Integer = ALMLogicaPedidos.Funciones.ValidarNumeroACero(spPedidosCapturar.ActiveSheet.Cells(fila, spPedidosCapturar.ActiveSheet.Columns("idSubFamilia").Index).Value)
                subFamilias.EIdAlmacen = idAlmacen
                subFamilias.EIdFamilia = idFamilia
                subFamilias.EId = idSubFamilia
                If (idAlmacen > 0 And idFamilia > 0 And idSubFamilia > 0) Then
                    Dim lista As New List(Of ALMEntidadesPedidos.SubFamilias)
                    lista = subFamilias.ObtenerListado()
                    If (lista.Count = 1) Then
                        spPedidosCapturar.ActiveSheet.Cells(fila, spPedidosCapturar.ActiveSheet.Columns("nombreSubFamilia").Index).Value = lista(0).ENombre
                        spPedidosCapturar.ActiveSheet.SetActiveCell(fila, spPedidosCapturar.ActiveSheet.ActiveColumnIndex + 1)
                    Else
                        spPedidosCapturar.ActiveSheet.Cells(fila, spPedidosCapturar.ActiveSheet.Columns("idSubFamilia").Index, fila, spPedidosCapturar.ActiveSheet.Columns("nombreSubFamilia").Index).Value = String.Empty
                        spPedidosCapturar.ActiveSheet.SetActiveCell(fila, spPedidosCapturar.ActiveSheet.ActiveColumnIndex - 1)
                    End If
                Else
                    spPedidosCapturar.ActiveSheet.Cells(fila, spPedidosCapturar.ActiveSheet.Columns("idSubFamilia").Index, fila, spPedidosCapturar.ActiveSheet.Columns("nombreSubFamilia").Index).Value = String.Empty
                    spPedidosCapturar.ActiveSheet.SetActiveCell(fila, spPedidosCapturar.ActiveSheet.ActiveColumnIndex - 1)
                End If
            ElseIf (columnaActiva = spPedidosCapturar.ActiveSheet.Columns("idArticulo").Index) Then
                fila = spPedidosCapturar.ActiveSheet.ActiveRowIndex
                Dim idAlmacen As Integer = ALMLogicaPedidos.Funciones.ValidarNumeroACero(cbAlmacenes.SelectedValue)
                Dim idFamilia As Integer = ALMLogicaPedidos.Funciones.ValidarNumeroACero(spPedidosCapturar.ActiveSheet.Cells(fila, spPedidosCapturar.ActiveSheet.Columns("idFamilia").Index).Value)
                Dim idSubFamilia As Integer = ALMLogicaPedidos.Funciones.ValidarNumeroACero(spPedidosCapturar.ActiveSheet.Cells(fila, spPedidosCapturar.ActiveSheet.Columns("idSubFamilia").Index).Value)
                Dim idArticulo As Integer = ALMLogicaPedidos.Funciones.ValidarNumeroACero(spPedidosCapturar.ActiveSheet.Cells(fila, spPedidosCapturar.ActiveSheet.Columns("idArticulo").Index).Value)
                articulos.EIdAlmacen = idAlmacen
                articulos.EIdFamilia = idFamilia
                articulos.EIdSubFamilia = idSubFamilia
                articulos.EId = idArticulo
                If (idAlmacen > 0 And idFamilia > 0 And idSubFamilia > 0 And idArticulo > 0) Then
                    For indice = 0 To spPedidosCapturar.ActiveSheet.Rows.Count - 1 ' Se valida que no se repitan los articulos.
                        Dim idArticuloLocal As Integer = ALMLogicaPedidos.Funciones.ValidarNumeroACero(spPedidosCapturar.ActiveSheet.Cells(indice, spPedidosCapturar.ActiveSheet.Columns("idArticulo").Index).Text)
                        Dim idSubFamiliaLocal As Integer = ALMLogicaPedidos.Funciones.ValidarNumeroACero(spPedidosCapturar.ActiveSheet.Cells(indice, spPedidosCapturar.ActiveSheet.Columns("idSubFamilia").Index).Text)
                        Dim idFamiliaLocal As Integer = ALMLogicaPedidos.Funciones.ValidarNumeroACero(spPedidosCapturar.ActiveSheet.Cells(indice, spPedidosCapturar.ActiveSheet.Columns("idFamilia").Index).Text)
                        If (idArticuloLocal > 0 And idFamiliaLocal > 0 And idSubFamiliaLocal > 0) Then
                            If (idArticuloLocal = idArticulo And idSubFamiliaLocal = idSubFamilia And idFamiliaLocal = idFamilia And indice <> fila) Then
                                spPedidosCapturar.ActiveSheet.Cells(fila, spPedidosCapturar.ActiveSheet.Columns("idArticulo").Index).Value = String.Empty
                                spPedidosCapturar.ActiveSheet.ClearRange(fila, spPedidosCapturar.ActiveSheet.Columns("idArticulo").Index, 1, spPedidosCapturar.ActiveSheet.Columns.Count - 1, True)
                                spPedidosCapturar.ActiveSheet.SetActiveCell(fila, spPedidosCapturar.ActiveSheet.ActiveColumnIndex - 1)
                                Exit Sub
                            End If
                        End If
                    Next
                    Dim lista As New List(Of ALMEntidadesPedidos.Articulos)
                    lista = articulos.ObtenerListado()
                    If (lista.Count = 1) Then
                        spPedidosCapturar.ActiveSheet.Cells(fila, spPedidosCapturar.ActiveSheet.Columns("nombreArticulo").Index).Value = lista(0).ENombre
                        spPedidosCapturar.ActiveSheet.Cells(fila, spPedidosCapturar.ActiveSheet.Columns("cantidad").Index).Value = 1 ' Por defecto. 
                        spPedidosCapturar.ActiveSheet.Cells(fila, spPedidosCapturar.ActiveSheet.Columns("precioUnitario").Index).Value = lista(0).EPrecio ' Por defecto. 
                        spPedidosCapturar.ActiveSheet.Cells(fila, spPedidosCapturar.ActiveSheet.Columns("total").Index).Value = lista(0).EPrecio ' Por defecto.
                        Dim lista2 As New List(Of ALMEntidadesPedidos.UnidadesMedidas)
                        Dim idMedida As Integer = lista(0).EIdUnidadMedida
                        If (idMedida > 0) Then
                            unidadesMedidas.EId = idMedida
                            lista2 = unidadesMedidas.ObtenerListado()
                            If (lista2.Count = 1) Then
                                spPedidosCapturar.ActiveSheet.Cells(fila, spPedidosCapturar.ActiveSheet.Columns("nombreUnidadMedida").Index).Value = lista2(0).ENombre
                            End If
                        End If
                        spPedidosCapturar.ActiveSheet.SetActiveCell(fila, spPedidosCapturar.ActiveSheet.ActiveColumnIndex + 2)
                    Else
                        spPedidosCapturar.ActiveSheet.Cells(fila, spPedidosCapturar.ActiveSheet.Columns("idArticulo").Index, fila, spPedidosCapturar.ActiveSheet.Columns("nombreUnidadMedida").Index).Value = String.Empty
                        spPedidosCapturar.ActiveSheet.SetActiveCell(fila, spPedidosCapturar.ActiveSheet.ActiveColumnIndex - 1)
                    End If
                Else
                    spPedidosCapturar.ActiveSheet.Cells(fila, spPedidosCapturar.ActiveSheet.Columns("idArticulo").Index, fila, spPedidosCapturar.ActiveSheet.Columns("nombreUnidadMedida").Index).Value = String.Empty
                    spPedidosCapturar.ActiveSheet.SetActiveCell(fila, spPedidosCapturar.ActiveSheet.ActiveColumnIndex - 1)
                End If
            ElseIf (columnaActiva = spPedidosCapturar.ActiveSheet.Columns("cantidad").Index) Then
                fila = spPedidosCapturar.ActiveSheet.ActiveRowIndex
                Dim cantidad As Integer = ALMLogicaPedidos.Funciones.ValidarNumeroACero(spPedidosCapturar.ActiveSheet.Cells(fila, spPedidosCapturar.ActiveSheet.Columns("cantidad").Index).Value)
                If (cantidad > 0) Then
                    Dim valorPrecio As String = ALMLogicaPedidos.Funciones.ValidarLetra(spPedidosCapturar.ActiveSheet.Cells(fila, spPedidosCapturar.ActiveSheet.Columns("precioUnitario").Index).Text)
                    If (String.IsNullOrEmpty(valorPrecio)) Then
                        Dim idAlmacen As Integer = ALMLogicaPedidos.Funciones.ValidarNumeroACero(cbAlmacenes.SelectedValue)
                        Dim idFamilia As Integer = ALMLogicaPedidos.Funciones.ValidarNumeroACero(spPedidosCapturar.ActiveSheet.Cells(fila, spPedidosCapturar.ActiveSheet.Columns("idFamilia").Index).Value)
                        Dim idSubFamilia As Integer = ALMLogicaPedidos.Funciones.ValidarNumeroACero(spPedidosCapturar.ActiveSheet.Cells(fila, spPedidosCapturar.ActiveSheet.Columns("idSubFamilia").Index).Value)
                        Dim idArticulo As Integer = ALMLogicaPedidos.Funciones.ValidarNumeroACero(spPedidosCapturar.ActiveSheet.Cells(fila, spPedidosCapturar.ActiveSheet.Columns("idArticulo").Index).Value)
                        Dim lista As New List(Of ALMEntidadesPedidos.Articulos)
                        articulos.EIdAlmacen = idAlmacen
                        articulos.EIdFamilia = idFamilia
                        articulos.EIdSubFamilia = idSubFamilia
                        articulos.EId = idArticulo
                        lista = articulos.ObtenerListado()
                        If (lista.Count = 1) Then
                            Dim precio As Double = lista(0).EPrecio
                            spPedidosCapturar.ActiveSheet.Cells(fila, spPedidosCapturar.ActiveSheet.Columns("precioUnitario").Index).Value = precio
                            spPedidosCapturar.ActiveSheet.Cells(fila, spPedidosCapturar.ActiveSheet.Columns("total").Index).Value = cantidad * precio
                        End If
                    End If
                Else
                    spPedidosCapturar.ActiveSheet.Cells(fila, spPedidosCapturar.ActiveSheet.Columns("cantidad").Index).Value = String.Empty
                    spPedidosCapturar.ActiveSheet.SetActiveCell(fila, spPedidosCapturar.ActiveSheet.ActiveColumnIndex - 1)
                End If
            ElseIf (columnaActiva = spPedidosCapturar.ActiveSheet.Columns("precioUnitario").Index) Then
                fila = spPedidosCapturar.ActiveSheet.ActiveRowIndex
                Dim cantidad As Integer = ALMLogicaPedidos.Funciones.ValidarNumeroACero(spPedidosCapturar.ActiveSheet.Cells(fila, spPedidosCapturar.ActiveSheet.Columns("cantidad").Index).Value)
                Dim precio As Double = ALMLogicaPedidos.Funciones.ValidarNumeroACero(spPedidosCapturar.ActiveSheet.Cells(fila, spPedidosCapturar.ActiveSheet.Columns("precioUnitario").Index).Value)
                If (cantidad > 0) Then
                    spPedidosCapturar.ActiveSheet.Cells(fila, spPedidosCapturar.ActiveSheet.Columns("total").Index).Value = cantidad * precio
                ElseIf (precio = 0) Then
                    spPedidosCapturar.ActiveSheet.Cells(fila, spPedidosCapturar.ActiveSheet.Columns("precioUnitario").Index).Value = 0
                Else
                    spPedidosCapturar.ActiveSheet.Cells(fila, spPedidosCapturar.ActiveSheet.Columns("precioUnitario").Index).Value = String.Empty
                    spPedidosCapturar.ActiveSheet.SetActiveCell(fila, spPedidosCapturar.ActiveSheet.ActiveColumnIndex - 1)
                End If
            ElseIf (columnaActiva = spPedidosCapturar.ActiveSheet.Columns("total").Index) Then
                fila = spPedidosCapturar.ActiveSheet.ActiveRowIndex
                Dim cantidad As Integer = ALMLogicaPedidos.Funciones.ValidarNumeroACero(spPedidosCapturar.ActiveSheet.Cells(fila, spPedidosCapturar.ActiveSheet.Columns("cantidad").Index).Value)
                Dim total As Double = ALMLogicaPedidos.Funciones.ValidarNumeroACero(spPedidosCapturar.ActiveSheet.Cells(fila, spPedidosCapturar.ActiveSheet.Columns("total").Index).Value)
                If (cantidad > 0) Then
                    spPedidosCapturar.ActiveSheet.Cells(fila, spPedidosCapturar.ActiveSheet.Columns("precioUnitario").Index).Value = total / cantidad
                Else
                    spPedidosCapturar.ActiveSheet.Cells(fila, spPedidosCapturar.ActiveSheet.Columns("total").Index).Value = String.Empty
                    spPedidosCapturar.ActiveSheet.SetActiveCell(fila, spPedidosCapturar.ActiveSheet.ActiveColumnIndex - 1)
                End If
            End If
        End If

    End Sub

    Private Sub CargarIdConsecutivo()

        pedidos.EIdAlmacen = ALMLogicaPedidos.Funciones.ValidarNumeroACero(cbAlmacenes.SelectedValue)
        Dim idMaximo As Integer = pedidos.ObtenerMaximoId()
        txtId.Text = idMaximo
        Me.opcionPestanaSeleccionada = OpcionPestana.capturar

    End Sub

    Private Sub CargarDatosEnSpreadDeCatalogos(ByVal filaCatalogos As Integer)

        If (spPedidosCapturar.ActiveSheet.ActiveColumnIndex = spPedidosCapturar.ActiveSheet.Columns("idProveedor").Index Or spPedidosCapturar.ActiveSheet.ActiveColumnIndex = spPedidosCapturar.ActiveSheet.Columns("nombreProveedor").Index) Then
            spPedidosCapturar.ActiveSheet.Cells(spPedidosCapturar.ActiveSheet.ActiveRowIndex, spPedidosCapturar.ActiveSheet.Columns("idProveedor").Index).Text = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("id").Index).Text
            spPedidosCapturar.ActiveSheet.Cells(spPedidosCapturar.ActiveSheet.ActiveRowIndex, spPedidosCapturar.ActiveSheet.Columns("nombreProveedor").Index).Text = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("nombre").Index).Text
        ElseIf (spPedidosCapturar.ActiveSheet.ActiveColumnIndex = spPedidosCapturar.ActiveSheet.Columns("idFamilia").Index Or spPedidosCapturar.ActiveSheet.ActiveColumnIndex = spPedidosCapturar.ActiveSheet.Columns("nombreFamilia").Index) Then
            spPedidosCapturar.ActiveSheet.Cells(spPedidosCapturar.ActiveSheet.ActiveRowIndex, spPedidosCapturar.ActiveSheet.Columns("idFamilia").Index).Text = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("id").Index).Text
            spPedidosCapturar.ActiveSheet.Cells(spPedidosCapturar.ActiveSheet.ActiveRowIndex, spPedidosCapturar.ActiveSheet.Columns("nombreFamilia").Index).Text = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("nombre").Index).Text
        ElseIf (spPedidosCapturar.ActiveSheet.ActiveColumnIndex = spPedidosCapturar.ActiveSheet.Columns("idSubFamilia").Index Or spPedidosCapturar.ActiveSheet.ActiveColumnIndex = spPedidosCapturar.ActiveSheet.Columns("nombreSubFamilia").Index) Then
            spPedidosCapturar.ActiveSheet.Cells(spPedidosCapturar.ActiveSheet.ActiveRowIndex, spPedidosCapturar.ActiveSheet.Columns("idSubFamilia").Index).Text = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("id").Index).Text
            spPedidosCapturar.ActiveSheet.Cells(spPedidosCapturar.ActiveSheet.ActiveRowIndex, spPedidosCapturar.ActiveSheet.Columns("nombreSubFamilia").Index).Text = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("nombre").Index).Text
        ElseIf (spPedidosCapturar.ActiveSheet.ActiveColumnIndex = spPedidosCapturar.ActiveSheet.Columns("idArticulo").Index Or spPedidosCapturar.ActiveSheet.ActiveColumnIndex = spPedidosCapturar.ActiveSheet.Columns("nombreArticulo").Index Or spPedidosCapturar.ActiveSheet.ActiveColumnIndex = spPedidosCapturar.ActiveSheet.Columns("nombreUnidadMedida").Index Or spPedidosCapturar.ActiveSheet.ActiveColumnIndex = spPedidosCapturar.ActiveSheet.Columns("codigo").Index Or spPedidosCapturar.ActiveSheet.ActiveColumnIndex = spPedidosCapturar.ActiveSheet.Columns("pagina").Index Or spPedidosCapturar.ActiveSheet.ActiveColumnIndex = spPedidosCapturar.ActiveSheet.Columns("color").Index Or spPedidosCapturar.ActiveSheet.ActiveColumnIndex = spPedidosCapturar.ActiveSheet.Columns("talla").Index Or spPedidosCapturar.ActiveSheet.ActiveColumnIndex = spPedidosCapturar.ActiveSheet.Columns("modelo").Index Or spPedidosCapturar.ActiveSheet.ActiveColumnIndex = spPedidosCapturar.ActiveSheet.Columns("codigoInternet").Index Or spPedidosCapturar.ActiveSheet.ActiveColumnIndex = spPedidosCapturar.ActiveSheet.Columns("precioUnitario").Index) Then
            spPedidosCapturar.ActiveSheet.Cells(spPedidosCapturar.ActiveSheet.ActiveRowIndex, spPedidosCapturar.ActiveSheet.Columns("idArticulo").Index).Text = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("id").Index).Text
            spPedidosCapturar.ActiveSheet.Cells(spPedidosCapturar.ActiveSheet.ActiveRowIndex, spPedidosCapturar.ActiveSheet.Columns("nombreArticulo").Index).Text = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("nombre").Index).Text
            spPedidosCapturar.ActiveSheet.Cells(spPedidosCapturar.ActiveSheet.ActiveRowIndex, spPedidosCapturar.ActiveSheet.Columns("nombreUnidadMedida").Index).Text = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("unidadMedida").Index).Text
            spPedidosCapturar.ActiveSheet.Cells(spPedidosCapturar.ActiveSheet.ActiveRowIndex, spPedidosCapturar.ActiveSheet.Columns("codigo").Index).Text = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("codigo").Index).Text
            spPedidosCapturar.ActiveSheet.Cells(spPedidosCapturar.ActiveSheet.ActiveRowIndex, spPedidosCapturar.ActiveSheet.Columns("pagina").Index).Text = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("pagina").Index).Text
            spPedidosCapturar.ActiveSheet.Cells(spPedidosCapturar.ActiveSheet.ActiveRowIndex, spPedidosCapturar.ActiveSheet.Columns("color").Index).Text = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("color").Index).Text
            spPedidosCapturar.ActiveSheet.Cells(spPedidosCapturar.ActiveSheet.ActiveRowIndex, spPedidosCapturar.ActiveSheet.Columns("talla").Index).Text = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("talla").Index).Text
            spPedidosCapturar.ActiveSheet.Cells(spPedidosCapturar.ActiveSheet.ActiveRowIndex, spPedidosCapturar.ActiveSheet.Columns("modelo").Index).Text = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("modelo").Index).Text
            spPedidosCapturar.ActiveSheet.Cells(spPedidosCapturar.ActiveSheet.ActiveRowIndex, spPedidosCapturar.ActiveSheet.Columns("codigoInternet").Index).Text = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("codigoInternet").Index).Text
            spPedidosCapturar.ActiveSheet.Cells(spPedidosCapturar.ActiveSheet.ActiveRowIndex, spPedidosCapturar.ActiveSheet.Columns("precioUnitario").Index).Text = spCatalogos.ActiveSheet.Cells(filaCatalogos, spCatalogos.ActiveSheet.Columns("precio").Index).Text
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

        spPedidosCapturar.Enabled = False
        Dim columna As Integer = spPedidosCapturar.ActiveSheet.ActiveColumnIndex
        If ((columna = spPedidosCapturar.ActiveSheet.Columns("idProveedor").Index) Or (columna = spPedidosCapturar.ActiveSheet.Columns("nombreProveedor").Index)) Then
            Me.opcionCatalogoSeleccionada = OpcionCatalogo.proveedor
            proveedores.EId = 0
            Dim datos As New DataTable
            datos = proveedores.ObtenerListadoReporteCatalogo()
            If (datos.Rows.Count > 0) Then
                spCatalogos.ActiveSheet.DataSource = datos
            Else
                spCatalogos.ActiveSheet.DataSource = Nothing
                spCatalogos.ActiveSheet.Rows.Count = 0
                spPedidosCapturar.Enabled = True
            End If
            FormatearSpreadCatalogo(OpcionPosicion.derecha)
        ElseIf ((columna = spPedidosCapturar.ActiveSheet.Columns("idFamilia").Index) Or (columna = spPedidosCapturar.ActiveSheet.Columns("nombreFamilia").Index)) Then
            Me.opcionCatalogoSeleccionada = OpcionCatalogo.familia
            Dim idAlmacen As Integer = ALMLogicaPedidos.Funciones.ValidarNumeroACero(cbAlmacenes.SelectedValue)
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
                    spPedidosCapturar.Enabled = True
                End If
            Else
                spCatalogos.ActiveSheet.DataSource = Nothing
                spCatalogos.ActiveSheet.Rows.Count = 0
                spPedidosCapturar.Enabled = True
            End If
            FormatearSpreadCatalogo(OpcionPosicion.derecha)
        ElseIf ((columna = spPedidosCapturar.ActiveSheet.Columns("idSubFamilia").Index) Or (columna = spPedidosCapturar.ActiveSheet.Columns("nombreSubFamilia").Index)) Then
            Me.opcionCatalogoSeleccionada = OpcionCatalogo.subfamilia
            Dim idAlmacen As Integer = ALMLogicaPedidos.Funciones.ValidarNumeroACero(cbAlmacenes.SelectedValue)
            Dim idFamilia As Integer = ALMLogicaPedidos.Funciones.ValidarNumeroACero(spPedidosCapturar.ActiveSheet.Cells(spPedidosCapturar.ActiveSheet.ActiveRowIndex, spPedidosCapturar.ActiveSheet.Columns("idFamilia").Index).Text)
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
                    spPedidosCapturar.Enabled = True
                End If
            Else
                spCatalogos.ActiveSheet.DataSource = Nothing
                spCatalogos.ActiveSheet.Rows.Count = 0
                spPedidosCapturar.Enabled = True
            End If
            FormatearSpreadCatalogo(OpcionPosicion.derecha)
        ElseIf ((columna = spPedidosCapturar.ActiveSheet.Columns("idArticulo").Index) Or (columna = spPedidosCapturar.ActiveSheet.Columns("nombreArticulo").Index) Or (columna = spPedidosCapturar.ActiveSheet.Columns("nombreUnidadMedida").Index) Or (columna = spPedidosCapturar.ActiveSheet.Columns("codigo").Index) Or (columna = spPedidosCapturar.ActiveSheet.Columns("pagina").Index) Or (columna = spPedidosCapturar.ActiveSheet.Columns("color").Index) Or (columna = spPedidosCapturar.ActiveSheet.Columns("talla").Index) Or (columna = spPedidosCapturar.ActiveSheet.Columns("modelo").Index) Or (columna = spPedidosCapturar.ActiveSheet.Columns("codigoInternet").Index) Or (columna = spPedidosCapturar.ActiveSheet.Columns("precioUnitario").Index)) Then
            Me.opcionCatalogoSeleccionada = OpcionCatalogo.articulo
            Dim idAlmacen As Integer = ALMLogicaPedidos.Funciones.ValidarNumeroACero(cbAlmacenes.SelectedValue)
            Dim idFamilia As Integer = ALMLogicaPedidos.Funciones.ValidarNumeroACero(spPedidosCapturar.ActiveSheet.Cells(spPedidosCapturar.ActiveSheet.ActiveRowIndex, spPedidosCapturar.ActiveSheet.Columns("idFamilia").Index).Text)
            Dim idSubFamilia As Integer = ALMLogicaPedidos.Funciones.ValidarNumeroACero(spPedidosCapturar.ActiveSheet.Cells(spPedidosCapturar.ActiveSheet.ActiveRowIndex, spPedidosCapturar.ActiveSheet.Columns("idSubFamilia").Index).Text)
            Dim idProveedor As Integer = ALMLogicaPedidos.Funciones.ValidarNumeroACero(spPedidosCapturar.ActiveSheet.Cells(spPedidosCapturar.ActiveSheet.ActiveRowIndex, spPedidosCapturar.ActiveSheet.Columns("idProveedor").Index).Text)
            If (idAlmacen > 0 And idFamilia > 0 And idSubFamilia > 0 And idProveedor > 0) Then
                articulos.EIdAlmacen = idAlmacen
                articulos.EIdFamilia = idFamilia
                articulos.EIdSubFamilia = idSubFamilia
                articulos.EIdProveedor = idProveedor
                articulos.EId = 0
                Dim datos As New DataTable
                datos = articulos.ObtenerListadoReporteCatalogo()
                Me.datosCatalogo = datos
                If (datos.Rows.Count > 0) Then
                    spCatalogos.ActiveSheet.DataSource = datos
                Else
                    spCatalogos.ActiveSheet.DataSource = Nothing
                    spCatalogos.ActiveSheet.Rows.Count = 0
                    spPedidosCapturar.Enabled = True
                End If
            Else
                spCatalogos.ActiveSheet.DataSource = Nothing
                spCatalogos.ActiveSheet.Rows.Count = 0
                spPedidosCapturar.Enabled = True
            End If
            FormatearSpreadCatalogo(OpcionPosicion.derecha)
        Else
            spPedidosCapturar.Enabled = True
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
            FormatearSpreadCatalogo(OpcionPosicion.centro)
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
            FormatearSpreadCatalogo(OpcionPosicion.centro)
        End If
        AsignarFoco(txtBuscarCatalogo)

    End Sub

    Private Sub FormatearSpreadCatalogo(ByVal posicion As Integer)

        If (Me.opcionCatalogoSeleccionada = OpcionCatalogo.articulo) Then
            spCatalogos.Width = 1110
            spCatalogos.ActiveSheet.Columns.Count = 10
        Else
            spCatalogos.Width = 500
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
        spCatalogos.ActiveSheet.Columns(numeracion).Tag = "id" : numeracion += 1
        spCatalogos.ActiveSheet.Columns(numeracion).Tag = "nombre" : numeracion += 1
        If (Me.opcionCatalogoSeleccionada = OpcionCatalogo.articulo) Then
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
            spCatalogos.ActiveSheet.ColumnHeader.Cells(0, spCatalogos.ActiveSheet.Columns("unidadMedida").Index).Value = "Unidad".ToUpper
            spCatalogos.ActiveSheet.ColumnHeader.Cells(0, spCatalogos.ActiveSheet.Columns("codigo").Index).Value = "Código".ToUpper
            spCatalogos.ActiveSheet.ColumnHeader.Cells(0, spCatalogos.ActiveSheet.Columns("pagina").Index).Value = "Página".ToUpper
            spCatalogos.ActiveSheet.ColumnHeader.Cells(0, spCatalogos.ActiveSheet.Columns("color").Index).Value = "Color".ToUpper
            spCatalogos.ActiveSheet.ColumnHeader.Cells(0, spCatalogos.ActiveSheet.Columns("talla").Index).Value = "Talla".ToUpper
            spCatalogos.ActiveSheet.ColumnHeader.Cells(0, spCatalogos.ActiveSheet.Columns("modelo").Index).Value = "Modelo".ToUpper
            spCatalogos.ActiveSheet.ColumnHeader.Cells(0, spCatalogos.ActiveSheet.Columns("codigoInternet").Index).Value = "Codigo Internet".ToUpper
            spCatalogos.ActiveSheet.ColumnHeader.Cells(0, spCatalogos.ActiveSheet.Columns("precio").Index).Value = "Precio".ToUpper
            spCatalogos.ActiveSheet.Columns("unidadMedida").Visible = False
        End If
        spCatalogos.ActiveSheet.Columns(0, spCatalogos.ActiveSheet.Columns.Count - 1).AllowAutoFilter = True
        spCatalogos.ActiveSheet.Columns(0, spCatalogos.ActiveSheet.Columns.Count - 1).AllowAutoSort = True
        pnlCatalogos.Height = spPedidosCapturar.Height
        pnlCatalogos.Width = spCatalogos.Width
        spCatalogos.Width = pnlCatalogos.Width
        spCatalogos.Height = pnlCatalogos.Height - txtBuscarCatalogo.Height - 5
        pnlCatalogos.BringToFront()
        pnlCatalogos.Visible = True
        pnlCatalogos.Refresh()

    End Sub

    Private Sub FormatearSpreadAnchoColumnasArticulos()

        spCatalogos.ActiveSheet.Columns("id").Width = 70
        spCatalogos.ActiveSheet.Columns("nombre").Width = 200
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
        spPedidosCapturar.Enabled = True
        If (Me.opcionCatalogoSeleccionada = OpcionCatalogo.almacen) Then
            AsignarFoco(cbAlmacenes)
        ElseIf (Me.opcionCatalogoSeleccionada = OpcionCatalogo.cliente) Then
            AsignarFoco(cbClientes)
        Else
            AsignarFoco(spPedidosCapturar)
        End If
        txtBuscarCatalogo.Clear()
        pnlCatalogos.Visible = False

    End Sub

    Private Sub CargarPedidos()

        Me.Cursor = Cursors.WaitCursor
        pedidos.EIdAlmacen = ALMLogicaPedidos.Funciones.ValidarNumeroACero(cbAlmacenes.SelectedValue)
        pedidos.EId = ALMLogicaPedidos.Funciones.ValidarNumeroACero(txtId.Text)
        If (pedidos.EId > 0) Then
            Dim lista As New List(Of ALMEntidadesPedidos.Pedidos)
            lista = pedidos.ObtenerListado()
            If (lista.Count > 0) Then
                dtpFecha.Value = lista(0).EFechaEnvio
                cbClientes.SelectedValue = lista(0).EIdCliente
                spPedidosCapturar.ActiveSheet.DataSource = pedidos.ObtenerListadoReporte()
                Me.cantidadFilas = spPedidosCapturar.ActiveSheet.Rows.Count + 1
                FormatearSpreadPedidos()
            Else
                LimpiarPantalla()
            End If
        End If
        AsignarFoco(dtpFecha)
        Me.Cursor = Cursors.Default

    End Sub

    Private Sub FormatearSpreadPedidos()

        spPedidosCapturar.ActiveSheet.ColumnHeader.RowCount = 2
        spPedidosCapturar.ActiveSheet.ColumnHeader.Rows(0, spPedidosCapturar.ActiveSheet.ColumnHeader.Rows.Count - 1).Font = New Font(Principal.tipoLetraSpread, Principal.tamañoLetraSpread, FontStyle.Bold)
        spPedidosCapturar.ActiveSheet.ColumnHeader.Rows(0).Height = Principal.alturaFilasEncabezadosChicosSpread
        spPedidosCapturar.ActiveSheet.ColumnHeader.Rows(1).Height = Principal.alturaFilasEncabezadosMedianosSpread
        spPedidosCapturar.ActiveSheet.OperationMode = FarPoint.Win.Spread.OperationMode.Normal
        spPedidosCapturar.ActiveSheet.Rows.Count = Me.cantidadFilas
        ControlarSpreadEnterASiguienteColumna(spPedidosCapturar)
        Dim numeracion As Integer = 0
        spPedidosCapturar.ActiveSheet.Columns(numeracion).Tag = "idProveedor" : numeracion += 1
        spPedidosCapturar.ActiveSheet.Columns(numeracion).Tag = "nombreProveedor" : numeracion += 1
        spPedidosCapturar.ActiveSheet.Columns(numeracion).Tag = "idFamilia" : numeracion += 1
        spPedidosCapturar.ActiveSheet.Columns(numeracion).Tag = "nombreFamilia" : numeracion += 1
        spPedidosCapturar.ActiveSheet.Columns(numeracion).Tag = "idSubFamilia" : numeracion += 1
        spPedidosCapturar.ActiveSheet.Columns(numeracion).Tag = "nombreSubFamilia" : numeracion += 1
        spPedidosCapturar.ActiveSheet.Columns(numeracion).Tag = "idArticulo" : numeracion += 1
        spPedidosCapturar.ActiveSheet.Columns(numeracion).Tag = "nombreArticulo" : numeracion += 1
        spPedidosCapturar.ActiveSheet.Columns(numeracion).Tag = "nombreUnidadMedida" : numeracion += 1
        spPedidosCapturar.ActiveSheet.Columns(numeracion).Tag = "codigo" : numeracion += 1
        spPedidosCapturar.ActiveSheet.Columns(numeracion).Tag = "pagina" : numeracion += 1
        spPedidosCapturar.ActiveSheet.Columns(numeracion).Tag = "color" : numeracion += 1
        spPedidosCapturar.ActiveSheet.Columns(numeracion).Tag = "talla" : numeracion += 1
        spPedidosCapturar.ActiveSheet.Columns(numeracion).Tag = "modelo" : numeracion += 1
        spPedidosCapturar.ActiveSheet.Columns(numeracion).Tag = "codigoInternet" : numeracion += 1
        spPedidosCapturar.ActiveSheet.Columns(numeracion).Tag = "precioUnitario" : numeracion += 1
        spPedidosCapturar.ActiveSheet.Columns(numeracion).Tag = "cantidad" : numeracion += 1
        spPedidosCapturar.ActiveSheet.Columns(numeracion).Tag = "total" : numeracion += 1
        spPedidosCapturar.ActiveSheet.Columns(numeracion).Tag = "observaciones" : numeracion += 1
        spPedidosCapturar.ActiveSheet.Columns.Count = numeracion
        spPedidosCapturar.ActiveSheet.Columns("idProveedor").Width = 50
        spPedidosCapturar.ActiveSheet.Columns("nombreProveedor").Width = 100
        spPedidosCapturar.ActiveSheet.Columns("idFamilia").Width = 50
        spPedidosCapturar.ActiveSheet.Columns("nombreFamilia").Width = 100
        spPedidosCapturar.ActiveSheet.Columns("idSubFamilia").Width = 50
        spPedidosCapturar.ActiveSheet.Columns("nombreSubFamilia").Width = 100
        spPedidosCapturar.ActiveSheet.Columns("idArticulo").Width = 70
        spPedidosCapturar.ActiveSheet.Columns("nombreArticulo").Width = 200
        spPedidosCapturar.ActiveSheet.Columns("nombreUnidadMedida").Width = 90
        spPedidosCapturar.ActiveSheet.Columns("codigo").Width = 150
        spPedidosCapturar.ActiveSheet.Columns("pagina").Width = 90
        spPedidosCapturar.ActiveSheet.Columns("color").Width = 100
        spPedidosCapturar.ActiveSheet.Columns("talla").Width = 80
        spPedidosCapturar.ActiveSheet.Columns("modelo").Width = 100
        spPedidosCapturar.ActiveSheet.Columns("codigoInternet").Width = 100
        spPedidosCapturar.ActiveSheet.Columns("precioUnitario").Width = 90
        spPedidosCapturar.ActiveSheet.Columns("cantidad").Width = 110
        spPedidosCapturar.ActiveSheet.Columns("total").Width = 110
        spPedidosCapturar.ActiveSheet.Columns("observaciones").Width = 200
        spPedidosCapturar.ActiveSheet.Columns("idProveedor").CellType = tipoEntero
        spPedidosCapturar.ActiveSheet.Columns("nombreProveedor").CellType = tipoTexto
        spPedidosCapturar.ActiveSheet.Columns("idFamilia").CellType = tipoEntero
        spPedidosCapturar.ActiveSheet.Columns("nombreFamilia").CellType = tipoTexto
        spPedidosCapturar.ActiveSheet.Columns("idSubFamilia").CellType = tipoEntero
        spPedidosCapturar.ActiveSheet.Columns("nombreSubFamilia").CellType = tipoTexto
        spPedidosCapturar.ActiveSheet.Columns("idArticulo").CellType = tipoEntero
        spPedidosCapturar.ActiveSheet.Columns("nombreArticulo").CellType = tipoTexto
        spPedidosCapturar.ActiveSheet.Columns("nombreUnidadMedida").CellType = tipoTexto
        spPedidosCapturar.ActiveSheet.Columns("codigo").CellType = tipoTexto
        spPedidosCapturar.ActiveSheet.Columns("pagina").CellType = tipoEntero
        spPedidosCapturar.ActiveSheet.Columns("color").CellType = tipoTexto
        spPedidosCapturar.ActiveSheet.Columns("talla").CellType = tipoTexto
        spPedidosCapturar.ActiveSheet.Columns("modelo").CellType = tipoTexto
        spPedidosCapturar.ActiveSheet.Columns("codigoInternet").CellType = tipoTexto
        spPedidosCapturar.ActiveSheet.Columns("precioUnitario").CellType = tipoDoble
        spPedidosCapturar.ActiveSheet.Columns("cantidad").CellType = tipoEntero
        spPedidosCapturar.ActiveSheet.Columns("total").CellType = tipoDoble
        spPedidosCapturar.ActiveSheet.Columns("observaciones").CellType = tipoTexto
        spPedidosCapturar.ActiveSheet.AddColumnHeaderSpanCell(0, spPedidosCapturar.ActiveSheet.Columns("idProveedor").Index, 1, 2)
        spPedidosCapturar.ActiveSheet.ColumnHeader.Cells(0, spPedidosCapturar.ActiveSheet.Columns("idProveedor").Index).Value = "P r o v e e d o r".ToUpper()
        spPedidosCapturar.ActiveSheet.ColumnHeader.Cells(1, spPedidosCapturar.ActiveSheet.Columns("idProveedor").Index).Value = "No. *".ToUpper()
        spPedidosCapturar.ActiveSheet.ColumnHeader.Cells(1, spPedidosCapturar.ActiveSheet.Columns("nombreProveedor").Index).Value = "Nombre *".ToUpper()
        spPedidosCapturar.ActiveSheet.AddColumnHeaderSpanCell(0, spPedidosCapturar.ActiveSheet.Columns("idFamilia").Index, 1, 2)
        spPedidosCapturar.ActiveSheet.ColumnHeader.Cells(0, spPedidosCapturar.ActiveSheet.Columns("idFamilia").Index).Value = "F a m i l i a".ToUpper()
        spPedidosCapturar.ActiveSheet.ColumnHeader.Cells(1, spPedidosCapturar.ActiveSheet.Columns("idFamilia").Index).Value = "No. *".ToUpper()
        spPedidosCapturar.ActiveSheet.ColumnHeader.Cells(1, spPedidosCapturar.ActiveSheet.Columns("nombreFamilia").Index).Value = "Nombre *".ToUpper()
        spPedidosCapturar.ActiveSheet.AddColumnHeaderSpanCell(0, spPedidosCapturar.ActiveSheet.Columns("idSubFamilia").Index, 1, 2)
        spPedidosCapturar.ActiveSheet.ColumnHeader.Cells(0, spPedidosCapturar.ActiveSheet.Columns("idSubFamilia").Index).Value = "S u b F a m i l i a".ToUpper()
        spPedidosCapturar.ActiveSheet.ColumnHeader.Cells(1, spPedidosCapturar.ActiveSheet.Columns("idSubFamilia").Index).Value = "No. *".ToUpper()
        spPedidosCapturar.ActiveSheet.ColumnHeader.Cells(1, spPedidosCapturar.ActiveSheet.Columns("nombreSubFamilia").Index).Value = "Nombre *".ToUpper()
        spPedidosCapturar.ActiveSheet.AddColumnHeaderSpanCell(0, spPedidosCapturar.ActiveSheet.Columns("idArticulo").Index, 1, 10)
        spPedidosCapturar.ActiveSheet.ColumnHeader.Cells(0, spPedidosCapturar.ActiveSheet.Columns("idArticulo").Index).Value = "A r t í c u l o".ToUpper()
        spPedidosCapturar.ActiveSheet.ColumnHeader.Cells(1, spPedidosCapturar.ActiveSheet.Columns("idArticulo").Index).Value = "No. *".ToUpper()
        spPedidosCapturar.ActiveSheet.ColumnHeader.Cells(1, spPedidosCapturar.ActiveSheet.Columns("nombreArticulo").Index).Value = "Nombre *".ToUpper()
        spPedidosCapturar.ActiveSheet.ColumnHeader.Cells(1, spPedidosCapturar.ActiveSheet.Columns("nombreUnidadMedida").Index).Value = "Unidad *".ToUpper()
        spPedidosCapturar.ActiveSheet.ColumnHeader.Cells(1, spPedidosCapturar.ActiveSheet.Columns("codigo").Index).Value = "Código *".ToUpper()
        spPedidosCapturar.ActiveSheet.ColumnHeader.Cells(1, spPedidosCapturar.ActiveSheet.Columns("pagina").Index).Value = "Página *".ToUpper()
        spPedidosCapturar.ActiveSheet.ColumnHeader.Cells(1, spPedidosCapturar.ActiveSheet.Columns("color").Index).Value = "Color *".ToUpper()
        spPedidosCapturar.ActiveSheet.ColumnHeader.Cells(1, spPedidosCapturar.ActiveSheet.Columns("talla").Index).Value = "Talla *".ToUpper()
        spPedidosCapturar.ActiveSheet.ColumnHeader.Cells(1, spPedidosCapturar.ActiveSheet.Columns("modelo").Index).Value = "Modelo *".ToUpper()
        spPedidosCapturar.ActiveSheet.ColumnHeader.Cells(1, spPedidosCapturar.ActiveSheet.Columns("codigoInternet").Index).Value = "Codigo Internet *".ToUpper()
        spPedidosCapturar.ActiveSheet.ColumnHeader.Cells(1, spPedidosCapturar.ActiveSheet.Columns("precioUnitario").Index).Value = "Precio *".ToUpper()
        spPedidosCapturar.ActiveSheet.AddColumnHeaderSpanCell(0, spPedidosCapturar.ActiveSheet.Columns("cantidad").Index, 2, 1)
        spPedidosCapturar.ActiveSheet.ColumnHeader.Cells(0, spPedidosCapturar.ActiveSheet.Columns("cantidad").Index).Value = "Cantidad *".ToUpper()
        spPedidosCapturar.ActiveSheet.AddColumnHeaderSpanCell(0, spPedidosCapturar.ActiveSheet.Columns("total").Index, 2, 1)
        spPedidosCapturar.ActiveSheet.ColumnHeader.Cells(0, spPedidosCapturar.ActiveSheet.Columns("total").Index).Value = "Total *".ToUpper()
        spPedidosCapturar.ActiveSheet.AddColumnHeaderSpanCell(0, spPedidosCapturar.ActiveSheet.Columns("observaciones").Index, 2, 1)
        spPedidosCapturar.ActiveSheet.ColumnHeader.Cells(0, spPedidosCapturar.ActiveSheet.Columns("observaciones").Index).Value = "Observaciones".ToUpper()
        spPedidosCapturar.ActiveSheet.Columns(spPedidosCapturar.ActiveSheet.Columns("idFamilia").Index, spPedidosCapturar.ActiveSheet.Columns("nombreSubFamilia").Index).Visible = False
        spPedidosCapturar.ActiveSheet.Columns("nombreArticulo").Visible = False
        spPedidosCapturar.ActiveSheet.Columns("nombreUnidadMedida").Visible = False
        spPedidosCapturar.ActiveSheet.Columns("cantidad").Visible = False
        spPedidosCapturar.ActiveSheet.Columns("total").Visible = False
        spPedidosCapturar.Refresh()

    End Sub

    Private Sub ValidarGuardado()

        Me.Cursor = Cursors.WaitCursor
        Me.esGuardadoValido = True
        ' Parte superior.
        Dim idAlmacen As Integer = ALMLogicaPedidos.Funciones.ValidarNumeroACero(cbAlmacenes.SelectedValue)
        If (idAlmacen <= 0) Then
            cbAlmacenes.BackColor = Color.Orange
            Me.esGuardadoValido = False
        End If
        Dim id As Integer = ALMLogicaPedidos.Funciones.ValidarNumeroACero(txtId.Text)
        If (id <= 0) Then
            txtId.BackColor = Color.Orange
            Me.esGuardadoValido = False
        End If
        Dim idCliente As Integer = ALMLogicaPedidos.Funciones.ValidarNumeroACero(cbClientes.SelectedValue)
        If (idCliente <= 0) Then
            cbClientes.BackColor = Color.Orange
            Me.esGuardadoValido = False
        End If
        ' Parte inferior.
        For fila As Integer = 0 To spPedidosCapturar.ActiveSheet.Rows.Count - 1
            Dim idProveedor As Integer = ALMLogicaPedidos.Funciones.ValidarNumeroACero(spPedidosCapturar.ActiveSheet.Cells(fila, spPedidosCapturar.ActiveSheet.Columns("idProveedor").Index).Text)
            Dim idFamilia As Integer = ALMLogicaPedidos.Funciones.ValidarNumeroACero(spPedidosCapturar.ActiveSheet.Cells(fila, spPedidosCapturar.ActiveSheet.Columns("idFamilia").Index).Text)
            Dim idSubFamilia As Integer = ALMLogicaPedidos.Funciones.ValidarNumeroACero(spPedidosCapturar.ActiveSheet.Cells(fila, spPedidosCapturar.ActiveSheet.Columns("idSubFamilia").Index).Text)
            Dim idArticulo As Integer = ALMLogicaPedidos.Funciones.ValidarNumeroACero(spPedidosCapturar.ActiveSheet.Cells(fila, spPedidosCapturar.ActiveSheet.Columns("idArticulo").Index).Text)
            If (idFamilia > 0 And idSubFamilia > 0 And idArticulo > 0 And idProveedor > 0) Then
                Dim cantidad As Integer = ALMLogicaPedidos.Funciones.ValidarNumeroACero(spPedidosCapturar.ActiveSheet.Cells(fila, spPedidosCapturar.ActiveSheet.Columns("cantidad").Index).Text)
                If (cantidad <= 0) Then
                    spPedidosCapturar.ActiveSheet.Cells(fila, spPedidosCapturar.ActiveSheet.Columns("cantidad").Index).BackColor = Color.Orange
                    Me.esGuardadoValido = False
                End If
                Dim precioUnitario As String = spPedidosCapturar.ActiveSheet.Cells(fila, spPedidosCapturar.ActiveSheet.Columns("precioUnitario").Index).Text
                Dim precioUnitario2 As Double = ALMLogicaPedidos.Funciones.ValidarNumeroACero(spPedidosCapturar.ActiveSheet.Cells(fila, spPedidosCapturar.ActiveSheet.Columns("precioUnitario").Index).Text)
                If (String.IsNullOrEmpty(precioUnitario) Or precioUnitario2 < 0) Then
                    spPedidosCapturar.ActiveSheet.Cells(fila, spPedidosCapturar.ActiveSheet.Columns("precioUnitario").Index).BackColor = Color.Orange
                    Me.esGuardadoValido = False
                End If
                Dim total As String = spPedidosCapturar.ActiveSheet.Cells(fila, spPedidosCapturar.ActiveSheet.Columns("total").Index).Text
                Dim total2 As Double = ALMLogicaPedidos.Funciones.ValidarNumeroACero(spPedidosCapturar.ActiveSheet.Cells(fila, spPedidosCapturar.ActiveSheet.Columns("total").Index).Text)
                If (String.IsNullOrEmpty(total) Or total2 < 0) Then
                    spPedidosCapturar.ActiveSheet.Cells(fila, spPedidosCapturar.ActiveSheet.Columns("total").Index).BackColor = Color.Orange
                    Me.esGuardadoValido = False
                End If
            End If
        Next
        Me.Cursor = Cursors.Default

    End Sub

    Private Sub GuardarEditarPedidos()

        Me.Cursor = Cursors.WaitCursor
        EliminarPedidos(False)
        ' Parte superior.
        Dim idAlmacen As Integer = ALMLogicaPedidos.Funciones.ValidarNumeroACero(cbAlmacenes.SelectedValue)
        Dim id As Integer = ALMLogicaPedidos.Funciones.ValidarNumeroACero(txtId.Text)
        Dim fechaEnvio As Date = dtpFecha.Value
        Dim idCliente As Integer = ALMLogicaPedidos.Funciones.ValidarNumeroACero(cbClientes.SelectedValue)
        ' Parte inferior.
        For fila As Integer = 0 To spPedidosCapturar.ActiveSheet.Rows.Count - 1
            Dim idProveedor As Integer = ALMLogicaPedidos.Funciones.ValidarNumeroACero(spPedidosCapturar.ActiveSheet.Cells(fila, spPedidosCapturar.ActiveSheet.Columns("idProveedor").Index).Text)
            Dim idFamilia As Integer = ALMLogicaPedidos.Funciones.ValidarNumeroACero(spPedidosCapturar.ActiveSheet.Cells(fila, spPedidosCapturar.ActiveSheet.Columns("idFamilia").Index).Text)
            Dim idSubFamilia As Integer = ALMLogicaPedidos.Funciones.ValidarNumeroACero(spPedidosCapturar.ActiveSheet.Cells(fila, spPedidosCapturar.ActiveSheet.Columns("idSubFamilia").Index).Text)
            Dim idArticulo As Integer = ALMLogicaPedidos.Funciones.ValidarNumeroACero(spPedidosCapturar.ActiveSheet.Cells(fila, spPedidosCapturar.ActiveSheet.Columns("idArticulo").Index).Text)
            Dim cantidad As Integer = 1 'ALMLogicaPedidos.Funciones.ValidarNumeroACero(spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("cantidad").Index).Text)
            Dim precioUnitario As Double = ALMLogicaPedidos.Funciones.ValidarNumeroACero(spPedidosCapturar.ActiveSheet.Cells(fila, spPedidosCapturar.ActiveSheet.Columns("precioUnitario").Index).Text)
            Dim total As Double = precioUnitario * cantidad 'ALMLogicaPedidos.Funciones.ValidarNumeroACero(spEntradas.ActiveSheet.Cells(fila, spEntradas.ActiveSheet.Columns("total").Index).Text)
            Dim orden As Integer = fila
            Dim observaciones As String = spPedidosCapturar.ActiveSheet.Cells(fila, spPedidosCapturar.ActiveSheet.Columns("observaciones").Index).Text
            Dim estaActualizado As Boolean = False
            If (id > 0 AndAlso idAlmacen > 0 AndAlso idFamilia > 0 AndAlso idSubFamilia > 0 AndAlso idArticulo > 0 AndAlso idCliente > 0 AndAlso idProveedor > 0) Then
                pedidos.EIdAlmacen = idAlmacen
                pedidos.EId = id
                pedidos.EIdFamilia = idFamilia
                pedidos.EIdSubFamilia = idSubFamilia
                pedidos.EIdArticulo = idArticulo
                pedidos.EIdCliente = idCliente
                pedidos.EFechaEnvio = fechaEnvio
                pedidos.ECantidad = cantidad
                pedidos.EPrecioUnitario = precioUnitario
                pedidos.ETotal = total
                pedidos.EOrden = orden
                pedidos.EObservaciones = observaciones
                pedidos.EEstaActualizado = False
                pedidos.Guardar()
            End If
        Next
        MessageBox.Show("Guardado finalizado.", "Finalizado.", MessageBoxButtons.OK)
        LimpiarPantalla()
        CargarIdConsecutivo()
        AsignarFoco(txtId)
        Me.Cursor = Cursors.Default

    End Sub

    Private Sub EliminarPedidos(ByVal conMensaje As Boolean)

        Me.Cursor = Cursors.WaitCursor
        Dim respuestaSi As Boolean = False
        If (conMensaje) Then
            If (MessageBox.Show("Confirmas que deseas eliminar esta entrada?", "Confirmación.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes) Then
                respuestaSi = True
            End If
        End If
        If ((respuestaSi) Or (Not conMensaje)) Then
            pedidos.EIdAlmacen = ALMLogicaPedidos.Funciones.ValidarNumeroACero(cbAlmacenes.SelectedValue)
            pedidos.EId = ALMLogicaPedidos.Funciones.ValidarNumeroACero(txtId.Text)
            pedidos.Eliminar()
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

    Private Sub CargarPedidosActualizar()

        Dim fecha As Date = dtpFecha.Value.ToShortDateString : Dim fecha2 As Date = dtpFechaFinal.Value.ToShortDateString
        Dim aplicaFecha As Boolean = False
        If (chkFecha.Checked) Then
            aplicaFecha = True
            pedidos.EFechaEnvio = fecha : pedidos.EFechaEnvio2 = fecha2
        Else
            aplicaFecha = False
        End If
        pedidos.EIdAlmacen = Me.idAlmacenPredeterminado
        pedidos.EId = 0
        spPedidosActualizar.ActiveSheet.DataSource = pedidos.ObtenerListadoReporteActualizar(aplicaFecha)
        FormatearSpreadPedidosActualizar()
        chkMostrarDetalladoActualizar.Enabled = True
        btnGuardar.Enabled = True
        MostrarOcultar()
        AsignarFoco(spPedidosActualizar)

    End Sub

    Private Sub FormatearSpreadPedidosActualizar()

        spPedidosActualizar.ActiveSheet.ColumnHeader.RowCount = 2
        spPedidosActualizar.ActiveSheet.ColumnHeader.Rows(0, spPedidosActualizar.ActiveSheet.ColumnHeader.Rows.Count - 1).Font = New Font(Principal.tipoLetraSpread, Principal.tamañoLetraSpread, FontStyle.Bold)
        spPedidosActualizar.ActiveSheet.ColumnHeader.Rows(0).Height = Principal.alturaFilasEncabezadosMedianosSpread
        spPedidosActualizar.ActiveSheet.ColumnHeader.Rows(1).Height = Principal.alturaFilasEncabezadosGrandesSpread
        spPedidosActualizar.ActiveSheet.OperationMode = FarPoint.Win.Spread.OperationMode.Normal
        'spPedidosActualizar.ActiveSheet.Rows.Count = cantidadFilas
        ControlarSpreadEnterASiguienteColumna(spPedidosActualizar)
        Dim numeracion As Integer = 0
        spPedidosActualizar.ActiveSheet.Columns.Count += 10
        spPedidosActualizar.ActiveSheet.Columns(numeracion).Tag = "fechaEnvio" : numeracion += 1
        spPedidosActualizar.ActiveSheet.Columns(numeracion).Tag = "id" : numeracion += 1
        spPedidosActualizar.ActiveSheet.Columns(numeracion).Tag = "idCliente" : numeracion += 1
        spPedidosActualizar.ActiveSheet.Columns(numeracion).Tag = "nombreCliente" : numeracion += 1
        spPedidosActualizar.ActiveSheet.Columns(numeracion).Tag = "idProveedor" : numeracion += 1
        spPedidosActualizar.ActiveSheet.Columns(numeracion).Tag = "nombreProveedor" : numeracion += 1
        spPedidosActualizar.ActiveSheet.Columns(numeracion).Tag = "idAlmacen" : numeracion += 1
        spPedidosActualizar.ActiveSheet.Columns(numeracion).Tag = "nombreAlmacen" : numeracion += 1
        spPedidosActualizar.ActiveSheet.Columns(numeracion).Tag = "idFamilia" : numeracion += 1
        spPedidosActualizar.ActiveSheet.Columns(numeracion).Tag = "nombreFamilia" : numeracion += 1
        spPedidosActualizar.ActiveSheet.Columns(numeracion).Tag = "idSubFamilia" : numeracion += 1
        spPedidosActualizar.ActiveSheet.Columns(numeracion).Tag = "nombreSubFamilia" : numeracion += 1
        spPedidosActualizar.ActiveSheet.Columns(numeracion).Tag = "idArticulo" : numeracion += 1
        spPedidosActualizar.ActiveSheet.Columns(numeracion).Tag = "nombreArticulo" : numeracion += 1
        spPedidosActualizar.ActiveSheet.Columns(numeracion).Tag = "nombreUnidadMedida" : numeracion += 1
        spPedidosActualizar.ActiveSheet.Columns(numeracion).Tag = "codigo" : numeracion += 1
        spPedidosActualizar.ActiveSheet.Columns(numeracion).Tag = "pagina" : numeracion += 1
        spPedidosActualizar.ActiveSheet.Columns(numeracion).Tag = "color" : numeracion += 1
        spPedidosActualizar.ActiveSheet.Columns(numeracion).Tag = "talla" : numeracion += 1
        spPedidosActualizar.ActiveSheet.Columns(numeracion).Tag = "modelo" : numeracion += 1
        spPedidosActualizar.ActiveSheet.Columns(numeracion).Tag = "codigoInternet" : numeracion += 1
        spPedidosActualizar.ActiveSheet.Columns(numeracion).Tag = "precioUnitario" : numeracion += 1
        spPedidosActualizar.ActiveSheet.Columns(numeracion).Tag = "cantidad" : numeracion += 1
        spPedidosActualizar.ActiveSheet.Columns(numeracion).Tag = "total" : numeracion += 1
        spPedidosActualizar.ActiveSheet.Columns(numeracion).Tag = "observaciones" : numeracion += 1
        spPedidosActualizar.ActiveSheet.Columns(numeracion).Tag = "confirmado" : numeracion += 1
        spPedidosActualizar.ActiveSheet.Columns(numeracion).Tag = "cancelado" : numeracion += 1
        spPedidosActualizar.ActiveSheet.Columns(numeracion).Tag = "recibido" : numeracion += 1
        spPedidosActualizar.ActiveSheet.Columns(numeracion).Tag = "entregado" : numeracion += 1
        spPedidosActualizar.ActiveSheet.Columns(numeracion).Tag = "fechaLlegada" : numeracion += 1
        spPedidosActualizar.ActiveSheet.Columns(numeracion).Tag = "observaciones2" : numeracion += 1
        spPedidosActualizar.ActiveSheet.Columns.Count = numeracion
        spPedidosActualizar.ActiveSheet.Columns("fechaEnvio").Width = 80
        spPedidosActualizar.ActiveSheet.Columns("id").Width = 50
        spPedidosActualizar.ActiveSheet.Columns("idCliente").Width = 50
        spPedidosActualizar.ActiveSheet.Columns("nombreCliente").Width = 150
        spPedidosActualizar.ActiveSheet.Columns("idProveedor").Width = 50
        spPedidosActualizar.ActiveSheet.Columns("nombreProveedor").Width = 90
        spPedidosActualizar.ActiveSheet.Columns("idAlmacen").Width = 50
        spPedidosActualizar.ActiveSheet.Columns("nombreAlmacen").Width = 150
        spPedidosActualizar.ActiveSheet.Columns("idFamilia").Width = 50
        spPedidosActualizar.ActiveSheet.Columns("nombreFamilia").Width = 150
        spPedidosActualizar.ActiveSheet.Columns("idSubFamilia").Width = 50
        spPedidosActualizar.ActiveSheet.Columns("nombreSubFamilia").Width = 150
        spPedidosActualizar.ActiveSheet.Columns("idArticulo").Width = 70
        spPedidosActualizar.ActiveSheet.Columns("nombreArticulo").Width = 200
        spPedidosActualizar.ActiveSheet.Columns("nombreUnidadMedida").Width = 90
        spPedidosActualizar.ActiveSheet.Columns("codigo").Width = 150
        spPedidosActualizar.ActiveSheet.Columns("pagina").Width = 80
        spPedidosActualizar.ActiveSheet.Columns("color").Width = 100
        spPedidosActualizar.ActiveSheet.Columns("talla").Width = 75
        spPedidosActualizar.ActiveSheet.Columns("modelo").Width = 90
        spPedidosActualizar.ActiveSheet.Columns("codigoInternet").Width = 100
        spPedidosActualizar.ActiveSheet.Columns("precioUnitario").Width = 80
        spPedidosActualizar.ActiveSheet.Columns("cantidad").Width = 110
        spPedidosActualizar.ActiveSheet.Columns("total").Width = 110
        spPedidosActualizar.ActiveSheet.Columns("observaciones").Width = 200
        spPedidosActualizar.ActiveSheet.Columns("confirmado").Width = 110
        spPedidosActualizar.ActiveSheet.Columns("cancelado").Width = 100
        spPedidosActualizar.ActiveSheet.Columns("recibido").Width = 85
        spPedidosActualizar.ActiveSheet.Columns("entregado").Width = 100
        spPedidosActualizar.ActiveSheet.Columns("fechaLlegada").Width = 80
        spPedidosActualizar.ActiveSheet.Columns("observaciones2").Width = 200
        spPedidosActualizar.ActiveSheet.Columns("fechaEnvio").CellType = tipoFecha
        spPedidosActualizar.ActiveSheet.Columns("id").CellType = tipoEntero
        spPedidosActualizar.ActiveSheet.Columns("idCliente").CellType = tipoEntero
        spPedidosActualizar.ActiveSheet.Columns("nombreCliente").CellType = tipoTexto
        spPedidosActualizar.ActiveSheet.Columns("idProveedor").CellType = tipoEntero
        spPedidosActualizar.ActiveSheet.Columns("nombreProveedor").CellType = tipoTexto
        spPedidosActualizar.ActiveSheet.Columns("idAlmacen").CellType = tipoEntero
        spPedidosActualizar.ActiveSheet.Columns("nombreAlmacen").CellType = tipoTexto
        spPedidosActualizar.ActiveSheet.Columns("idFamilia").CellType = tipoEntero
        spPedidosActualizar.ActiveSheet.Columns("nombreFamilia").CellType = tipoTexto
        spPedidosActualizar.ActiveSheet.Columns("idSubFamilia").CellType = tipoEntero
        spPedidosActualizar.ActiveSheet.Columns("nombreSubFamilia").CellType = tipoTexto
        spPedidosActualizar.ActiveSheet.Columns("idArticulo").CellType = tipoEntero
        spPedidosActualizar.ActiveSheet.Columns("nombreArticulo").CellType = tipoTexto
        spPedidosActualizar.ActiveSheet.Columns("nombreUnidadMedida").CellType = tipoTexto
        spPedidosActualizar.ActiveSheet.Columns("codigo").CellType = tipoTexto
        spPedidosActualizar.ActiveSheet.Columns("pagina").CellType = tipoEntero
        spPedidosActualizar.ActiveSheet.Columns("color").CellType = tipoTexto
        spPedidosActualizar.ActiveSheet.Columns("talla").CellType = tipoTexto
        spPedidosActualizar.ActiveSheet.Columns("modelo").CellType = tipoTexto
        spPedidosActualizar.ActiveSheet.Columns("codigoInternet").CellType = tipoTexto
        spPedidosActualizar.ActiveSheet.Columns("precioUnitario").CellType = tipoDoble
        spPedidosActualizar.ActiveSheet.Columns("cantidad").CellType = tipoEntero
        spPedidosActualizar.ActiveSheet.Columns("total").CellType = tipoDoble
        spPedidosActualizar.ActiveSheet.Columns("observaciones").CellType = tipoTexto
        spPedidosActualizar.ActiveSheet.Columns("confirmado").CellType = tipoBooleano
        spPedidosActualizar.ActiveSheet.Columns("cancelado").CellType = tipoBooleano
        spPedidosActualizar.ActiveSheet.Columns("recibido").CellType = tipoBooleano
        spPedidosActualizar.ActiveSheet.Columns("entregado").CellType = tipoBooleano
        spPedidosActualizar.ActiveSheet.Columns("fechaLlegada").CellType = tipoFecha
        spPedidosActualizar.ActiveSheet.Columns("observaciones2").CellType = tipoTexto
        spPedidosActualizar.ActiveSheet.AddColumnHeaderSpanCell(0, spPedidosActualizar.ActiveSheet.Columns("fechaEnvio").Index, 2, 1)
        spPedidosActualizar.ActiveSheet.ColumnHeader.Cells(0, spPedidosActualizar.ActiveSheet.Columns("fechaEnvio").Index).Value = "Fecha".ToUpper()
        spPedidosActualizar.ActiveSheet.AddColumnHeaderSpanCell(0, spPedidosActualizar.ActiveSheet.Columns("id").Index, 2, 1)
        spPedidosActualizar.ActiveSheet.ColumnHeader.Cells(0, spPedidosActualizar.ActiveSheet.Columns("id").Index).Value = "No.".ToUpper()
        spPedidosActualizar.ActiveSheet.AddColumnHeaderSpanCell(0, spPedidosActualizar.ActiveSheet.Columns("idCliente").Index, 1, 2)
        spPedidosActualizar.ActiveSheet.ColumnHeader.Cells(0, spPedidosActualizar.ActiveSheet.Columns("idCliente").Index).Value = "C l i e n t e".ToUpper()
        spPedidosActualizar.ActiveSheet.ColumnHeader.Cells(1, spPedidosActualizar.ActiveSheet.Columns("idCliente").Index).Value = "No.".ToUpper()
        spPedidosActualizar.ActiveSheet.ColumnHeader.Cells(1, spPedidosActualizar.ActiveSheet.Columns("nombreCliente").Index).Value = "Nombre".ToUpper()
        spPedidosActualizar.ActiveSheet.AddColumnHeaderSpanCell(0, spPedidosActualizar.ActiveSheet.Columns("idProveedor").Index, 1, 2)
        spPedidosActualizar.ActiveSheet.ColumnHeader.Cells(0, spPedidosActualizar.ActiveSheet.Columns("idProveedor").Index).Value = "P r o v e e d o r".ToUpper()
        spPedidosActualizar.ActiveSheet.ColumnHeader.Cells(1, spPedidosActualizar.ActiveSheet.Columns("idProveedor").Index).Value = "No.".ToUpper()
        spPedidosActualizar.ActiveSheet.ColumnHeader.Cells(1, spPedidosActualizar.ActiveSheet.Columns("nombreProveedor").Index).Value = "Nombre".ToUpper()
        spPedidosActualizar.ActiveSheet.AddColumnHeaderSpanCell(0, spPedidosActualizar.ActiveSheet.Columns("idAlmacen").Index, 1, 2)
        spPedidosActualizar.ActiveSheet.ColumnHeader.Cells(0, spPedidosActualizar.ActiveSheet.Columns("idAlmacen").Index).Value = "A l m a c é n".ToUpper()
        spPedidosActualizar.ActiveSheet.ColumnHeader.Cells(1, spPedidosActualizar.ActiveSheet.Columns("idAlmacen").Index).Value = "No.".ToUpper()
        spPedidosActualizar.ActiveSheet.ColumnHeader.Cells(1, spPedidosActualizar.ActiveSheet.Columns("nombreAlmacen").Index).Value = "Nombre".ToUpper()
        spPedidosActualizar.ActiveSheet.AddColumnHeaderSpanCell(0, spPedidosActualizar.ActiveSheet.Columns("idFamilia").Index, 1, 2)
        spPedidosActualizar.ActiveSheet.ColumnHeader.Cells(0, spPedidosActualizar.ActiveSheet.Columns("idFamilia").Index).Value = "F a m i l i a".ToUpper()
        spPedidosActualizar.ActiveSheet.ColumnHeader.Cells(1, spPedidosActualizar.ActiveSheet.Columns("idFamilia").Index).Value = "No.".ToUpper()
        spPedidosActualizar.ActiveSheet.ColumnHeader.Cells(1, spPedidosActualizar.ActiveSheet.Columns("nombreFamilia").Index).Value = "Nombre".ToUpper()
        spPedidosActualizar.ActiveSheet.AddColumnHeaderSpanCell(0, spPedidosActualizar.ActiveSheet.Columns("idSubFamilia").Index, 1, 2)
        spPedidosActualizar.ActiveSheet.ColumnHeader.Cells(0, spPedidosActualizar.ActiveSheet.Columns("idSubFamilia").Index).Value = "S u b F a m i l i a".ToUpper()
        spPedidosActualizar.ActiveSheet.ColumnHeader.Cells(1, spPedidosActualizar.ActiveSheet.Columns("idSubFamilia").Index).Value = "No.".ToUpper()
        spPedidosActualizar.ActiveSheet.ColumnHeader.Cells(1, spPedidosActualizar.ActiveSheet.Columns("nombreSubFamilia").Index).Value = "Nombre".ToUpper()
        spPedidosActualizar.ActiveSheet.AddColumnHeaderSpanCell(0, spPedidosActualizar.ActiveSheet.Columns("idArticulo").Index, 1, 10)
        spPedidosActualizar.ActiveSheet.ColumnHeader.Cells(0, spPedidosActualizar.ActiveSheet.Columns("idArticulo").Index).Value = "A r t í c u l o".ToUpper()
        spPedidosActualizar.ActiveSheet.ColumnHeader.Cells(1, spPedidosActualizar.ActiveSheet.Columns("idArticulo").Index).Value = "No.".ToUpper()
        spPedidosActualizar.ActiveSheet.ColumnHeader.Cells(1, spPedidosActualizar.ActiveSheet.Columns("nombreArticulo").Index).Value = "Nombre".ToUpper()
        spPedidosActualizar.ActiveSheet.ColumnHeader.Cells(1, spPedidosActualizar.ActiveSheet.Columns("nombreUnidadMedida").Index).Value = "Unidad".ToUpper()
        spPedidosActualizar.ActiveSheet.ColumnHeader.Cells(1, spPedidosActualizar.ActiveSheet.Columns("codigo").Index).Value = "Código".ToUpper()
        spPedidosActualizar.ActiveSheet.ColumnHeader.Cells(1, spPedidosActualizar.ActiveSheet.Columns("pagina").Index).Value = "Página".ToUpper()
        spPedidosActualizar.ActiveSheet.ColumnHeader.Cells(1, spPedidosActualizar.ActiveSheet.Columns("color").Index).Value = "Color".ToUpper()
        spPedidosActualizar.ActiveSheet.ColumnHeader.Cells(1, spPedidosActualizar.ActiveSheet.Columns("talla").Index).Value = "Talla".ToUpper()
        spPedidosActualizar.ActiveSheet.ColumnHeader.Cells(1, spPedidosActualizar.ActiveSheet.Columns("modelo").Index).Value = "Modelo".ToUpper()
        spPedidosActualizar.ActiveSheet.ColumnHeader.Cells(1, spPedidosActualizar.ActiveSheet.Columns("codigoInternet").Index).Value = "Codigo Internet".ToUpper()
        spPedidosActualizar.ActiveSheet.ColumnHeader.Cells(1, spPedidosActualizar.ActiveSheet.Columns("precioUnitario").Index).Value = "Precio".ToUpper()
        spPedidosActualizar.ActiveSheet.AddColumnHeaderSpanCell(0, spPedidosActualizar.ActiveSheet.Columns("cantidad").Index, 2, 1)
        spPedidosActualizar.ActiveSheet.ColumnHeader.Cells(0, spPedidosActualizar.ActiveSheet.Columns("cantidad").Index).Value = "Cantidad".ToUpper()
        spPedidosActualizar.ActiveSheet.AddColumnHeaderSpanCell(0, spPedidosActualizar.ActiveSheet.Columns("total").Index, 2, 1)
        spPedidosActualizar.ActiveSheet.ColumnHeader.Cells(0, spPedidosActualizar.ActiveSheet.Columns("total").Index).Value = "Total".ToUpper()
        spPedidosActualizar.ActiveSheet.AddColumnHeaderSpanCell(0, spPedidosActualizar.ActiveSheet.Columns("observaciones").Index, 2, 1)
        spPedidosActualizar.ActiveSheet.ColumnHeader.Cells(0, spPedidosActualizar.ActiveSheet.Columns("observaciones").Index).Value = "Observaciones".ToUpper()
        spPedidosActualizar.ActiveSheet.AddColumnHeaderSpanCell(0, spPedidosActualizar.ActiveSheet.Columns("confirmado").Index, 1, 4)
        spPedidosActualizar.ActiveSheet.ColumnHeader.Cells(0, spPedidosActualizar.ActiveSheet.Columns("confirmado").Index).Value = "E s t a t u s".ToUpper()
        spPedidosActualizar.ActiveSheet.ColumnHeader.Cells(1, spPedidosActualizar.ActiveSheet.Columns("confirmado").Index).Value = "Confirmado".ToUpper()
        spPedidosActualizar.ActiveSheet.ColumnHeader.Cells(1, spPedidosActualizar.ActiveSheet.Columns("cancelado").Index).Value = "Cancelado".ToUpper()
        spPedidosActualizar.ActiveSheet.ColumnHeader.Cells(1, spPedidosActualizar.ActiveSheet.Columns("recibido").Index).Value = "Recibido".ToUpper()
        spPedidosActualizar.ActiveSheet.ColumnHeader.Cells(1, spPedidosActualizar.ActiveSheet.Columns("entregado").Index).Value = "Entregado".ToUpper()
        spPedidosActualizar.ActiveSheet.AddColumnHeaderSpanCell(0, spPedidosActualizar.ActiveSheet.Columns("fechaLlegada").Index, 2, 1)
        spPedidosActualizar.ActiveSheet.ColumnHeader.Cells(0, spPedidosActualizar.ActiveSheet.Columns("fechaLlegada").Index).Value = "Fecha Llegada".ToUpper()
        spPedidosActualizar.ActiveSheet.AddColumnHeaderSpanCell(0, spPedidosActualizar.ActiveSheet.Columns("observaciones2").Index, 2, 1)
        spPedidosActualizar.ActiveSheet.ColumnHeader.Cells(0, spPedidosActualizar.ActiveSheet.Columns("observaciones2").Index).Value = "Observaciones 2".ToUpper()
        spPedidosActualizar.ActiveSheet.Columns("idCliente").Visible = False
        spPedidosActualizar.ActiveSheet.Columns("idProveedor").Visible = False
        spPedidosActualizar.ActiveSheet.Columns("idProveedor").Visible = False
        spPedidosActualizar.ActiveSheet.Columns("idArticulo").Visible = False
        spPedidosActualizar.ActiveSheet.Columns("nombreArticulo").Visible = False
        spPedidosActualizar.ActiveSheet.Columns(spPedidosActualizar.ActiveSheet.Columns("idAlmacen").Index, spPedidosActualizar.ActiveSheet.Columns("nombreSubFamilia").Index).Visible = False
        spPedidosActualizar.ActiveSheet.Columns("nombreUnidadMedida").Visible = False
        spPedidosActualizar.ActiveSheet.Columns("cantidad").Visible = False
        spPedidosActualizar.ActiveSheet.Columns("total").Visible = False
        spPedidosActualizar.ActiveSheet.Columns("observaciones").Visible = False
        spPedidosActualizar.ActiveSheet.Columns(0, spPedidosActualizar.ActiveSheet.Columns("observaciones").Index).BackColor = Color.FromArgb(240, 240, 240)
        spPedidosActualizar.ActiveSheet.Columns(0, spPedidosActualizar.ActiveSheet.Columns("observaciones").Index).AllowAutoFilter = True
        spPedidosActualizar.ActiveSheet.Columns(0, spPedidosActualizar.ActiveSheet.Columns("observaciones").Index).Locked = True
        spPedidosActualizar.Visible = True
        spPedidosActualizar.Refresh()

    End Sub

    Private Sub ActualizarPedidosActualizar()

        GuardarEditarEntradasPedidosActualizar()
        GuardarEditarSalidasPedidosActualizar()
        ' No capturables por el usuario.
        Dim estaActualizado As Boolean = True
        ' Parte inferior.
        For fila As Integer = 0 To spPedidosActualizar.ActiveSheet.Rows.Count - 1
            Dim idAlmacen As Integer = ALMLogicaPedidos.Funciones.ValidarNumeroACero(spPedidosActualizar.ActiveSheet.Cells(fila, spPedidosActualizar.ActiveSheet.Columns("idAlmacen").Index).Text)
            Dim id As Integer = ALMLogicaPedidos.Funciones.ValidarNumeroACero(spPedidosActualizar.ActiveSheet.Cells(fila, spPedidosActualizar.ActiveSheet.Columns("id").Index).Text)
            Dim idFamilia As Integer = ALMLogicaPedidos.Funciones.ValidarNumeroACero(spPedidosActualizar.ActiveSheet.Cells(fila, spPedidosActualizar.ActiveSheet.Columns("idFamilia").Index).Text)
            Dim idSubFamilia As Integer = ALMLogicaPedidos.Funciones.ValidarNumeroACero(spPedidosActualizar.ActiveSheet.Cells(fila, spPedidosActualizar.ActiveSheet.Columns("idSubFamilia").Index).Text)
            Dim idArticulo As Integer = ALMLogicaPedidos.Funciones.ValidarNumeroACero(spPedidosActualizar.ActiveSheet.Cells(fila, spPedidosActualizar.ActiveSheet.Columns("idArticulo").Index).Text)
            Dim estatusConfirmado As Boolean = spPedidosActualizar.ActiveSheet.Cells(fila, spPedidosActualizar.ActiveSheet.Columns("confirmado").Index).Value
            Dim estatusCancelado As Boolean = spPedidosActualizar.ActiveSheet.Cells(fila, spPedidosActualizar.ActiveSheet.Columns("cancelado").Index).Value
            Dim estatusRecibido As Boolean = spPedidosActualizar.ActiveSheet.Cells(fila, spPedidosActualizar.ActiveSheet.Columns("recibido").Index).Value
            Dim estatusEntregado As Boolean = spPedidosActualizar.ActiveSheet.Cells(fila, spPedidosActualizar.ActiveSheet.Columns("entregado").Index).Value
            Dim fechaLlegadaCopia As String = spPedidosActualizar.ActiveSheet.Cells(fila, spPedidosActualizar.ActiveSheet.Columns("fechaLlegada").Index).Text
            Dim fechaLlegada As Date
            If (IsDate(fechaLlegadaCopia)) Then
                fechaLlegada = spPedidosActualizar.ActiveSheet.Cells(fila, spPedidosActualizar.ActiveSheet.Columns("fechaLlegada").Index).Value
            End If
            Dim observaciones2 As String = spPedidosActualizar.ActiveSheet.Cells(fila, spPedidosActualizar.ActiveSheet.Columns("observaciones2").Index).Text
            Dim aplicaFecha As Boolean = False
            If (id > 0 AndAlso idAlmacen > 0 AndAlso idFamilia > 0 AndAlso idSubFamilia > 0 AndAlso idArticulo > 0) Then
                pedidos.EIdAlmacen = idAlmacen
                pedidos.EId = id
                pedidos.EIdFamilia = idFamilia
                pedidos.EIdSubFamilia = idSubFamilia
                pedidos.EIdArticulo = idArticulo
                pedidos.EEstaActualizado = estaActualizado
                pedidos.EEstatusConfirmado = estatusConfirmado
                pedidos.EEstatusCancelado = estatusCancelado
                pedidos.EEstatusRecibido = estatusRecibido
                pedidos.EEstatusEntregado = estatusEntregado
                If (IsDate(fechaLlegadaCopia)) Then
                    pedidos.EFechaLlegada = fechaLlegada
                    aplicaFecha = True
                End If
                pedidos.EObservaciones2 = observaciones2
                pedidos.Actualizar(aplicaFecha)
            End If
        Next
        MessageBox.Show("Guardado finalizado.", "Finalizado.", MessageBoxButtons.OK)
        'LimpiarPantalla() 

    End Sub

    Private Sub GuardarEditarEntradasPedidosActualizar()

        ' No capturables por el usuario.
        Dim idExterno As String = String.Empty
        Dim idMoneda As Integer = 1 ' Pesos
        Dim tipoCambio As Double = 1 ' 1
        Dim idTipoEntrada As Integer = 1 ' Normal o compra.
        Dim factura As String = String.Empty
        Dim chofer As String = String.Empty
        Dim camion As String = String.Empty
        Dim noEconomico As String = String.Empty
        Dim idAlmacenCopia As Integer = -1
        Dim idCopia As Integer = -1
        ' Parte inferior.
        For fila As Integer = 0 To spPedidosActualizar.ActiveSheet.Rows.Count - 1
            Dim recibido As Boolean = spPedidosActualizar.ActiveSheet.Cells(fila, spPedidosActualizar.ActiveSheet.Columns("recibido").Index).Value
            Dim idAlmacen As Integer = ALMLogicaPedidos.Funciones.ValidarNumeroACero(spPedidosActualizar.ActiveSheet.Cells(fila, spPedidosActualizar.ActiveSheet.Columns("idAlmacen").Index).Text)
            Dim id As Integer = ALMLogicaPedidos.Funciones.ValidarNumeroACero(spPedidosActualizar.ActiveSheet.Cells(fila, spPedidosActualizar.ActiveSheet.Columns("id").Index).Text)
            If (idAlmacen <> idAlmacenCopia AndAlso id <> idCopia AndAlso recibido) Then
                EliminarEntradasPedidosActualizar(False, fila)
                idAlmacenCopia = idAlmacen
                idCopia = id
            End If
            Dim idFamilia As Integer = ALMLogicaPedidos.Funciones.ValidarNumeroACero(spPedidosActualizar.ActiveSheet.Cells(fila, spPedidosActualizar.ActiveSheet.Columns("idFamilia").Index).Text)
            Dim idSubFamilia As Integer = ALMLogicaPedidos.Funciones.ValidarNumeroACero(spPedidosActualizar.ActiveSheet.Cells(fila, spPedidosActualizar.ActiveSheet.Columns("idSubFamilia").Index).Text)
            Dim idArticulo As Integer = ALMLogicaPedidos.Funciones.ValidarNumeroACero(spPedidosActualizar.ActiveSheet.Cells(fila, spPedidosActualizar.ActiveSheet.Columns("idArticulo").Index).Text)
            Dim fechaLLegadaCopia As String = String.Empty
            Dim fechaLlegada As Date
            fechaLLegadaCopia = spPedidosActualizar.ActiveSheet.Cells(fila, spPedidosActualizar.ActiveSheet.Columns("fechaLlegada").Index).Value
            If (Not String.IsNullOrEmpty(fechaLLegadaCopia)) Then
                fechaLlegada = fechaLLegadaCopia
                spPedidosActualizar.ActiveSheet.Cells(fila, spPedidosActualizar.ActiveSheet.Columns("fechaLlegada").Index).BackColor = Color.White
            Else
                If (recibido) Then
                    spPedidosActualizar.ActiveSheet.Cells(fila, spPedidosActualizar.ActiveSheet.Columns("fechaLlegada").Index).BackColor = Color.Orange
                Else
                    spPedidosActualizar.ActiveSheet.Cells(fila, spPedidosActualizar.ActiveSheet.Columns("fechaLlegada").Index).BackColor = Color.White
                End If
            End If
            Dim idProveedor As Integer = ALMLogicaPedidos.Funciones.ValidarNumeroACero(spPedidosActualizar.ActiveSheet.Cells(fila, spPedidosActualizar.ActiveSheet.Columns("idProveedor").Index).Text)
            Dim cantidad As Integer = ALMLogicaPedidos.Funciones.ValidarNumeroACero(spPedidosActualizar.ActiveSheet.Cells(fila, spPedidosActualizar.ActiveSheet.Columns("cantidad").Index).Text)
            Dim precioUnitario As Double = ALMLogicaPedidos.Funciones.ValidarNumeroACero(spPedidosActualizar.ActiveSheet.Cells(fila, spPedidosActualizar.ActiveSheet.Columns("precioUnitario").Index).Text)
            Dim total As Double = ALMLogicaPedidos.Funciones.ValidarNumeroACero(spPedidosActualizar.ActiveSheet.Cells(fila, spPedidosActualizar.ActiveSheet.Columns("total").Index).Text)
            Dim totalPesos As Double = total
            Dim orden As Integer = fila
            Dim observaciones As String = spPedidosActualizar.ActiveSheet.Cells(fila, spPedidosActualizar.ActiveSheet.Columns("observaciones2").Index).Text
            If (Me.idOrigen > 0 AndAlso id > 0 AndAlso idAlmacen > 0 AndAlso idFamilia > 0 AndAlso idSubFamilia > 0 AndAlso idArticulo > 0 AndAlso idMoneda > 0 AndAlso idTipoEntrada > 0 AndAlso idProveedor > 0 AndAlso recibido AndAlso (Not String.IsNullOrEmpty(fechaLLegadaCopia))) Then
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
                entradas.EFecha = fechaLlegada
                entradas.ECantidad = cantidad
                entradas.EPrecioUnitario = precioUnitario
                entradas.ETotal = total
                entradas.ETotalPesos = totalPesos
                entradas.EOrden = orden
                entradas.EObservaciones = observaciones
                entradas.EFactura = factura
                entradas.EChofer = chofer
                entradas.ECamion = camion
                entradas.ENoEconomico = noEconomico
                entradas.Guardar()
            End If
        Next
        'MessageBox.Show("Guardado finalizado.", "Finalizado.", MessageBoxButtons.OK)

    End Sub

    Private Sub EliminarEntradasPedidosActualizar(ByVal conMensaje As Boolean, ByVal fila As Integer)

        Dim respuestaSi As Boolean = False
        If (conMensaje) Then
            If (MessageBox.Show("Confirmas que deseas eliminar esta entrada?", "Confirmación.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes) Then
                respuestaSi = True
            End If
        End If
        If ((respuestaSi) Or (Not conMensaje)) Then
            Dim idAlmacen As Integer = ALMLogicaPedidos.Funciones.ValidarNumeroACero(spPedidosActualizar.ActiveSheet.Cells(fila, spPedidosActualizar.ActiveSheet.Columns("idAlmacen").Index).Text)
            Dim id As Integer = ALMLogicaPedidos.Funciones.ValidarNumeroACero(spPedidosActualizar.ActiveSheet.Cells(fila, spPedidosActualizar.ActiveSheet.Columns("id").Index).Text)
            entradas.EIdOrigen = Me.idOrigen
            entradas.EIdAlmacen = idAlmacen
            entradas.EId = id
            entradas.Eliminar()
        End If
        If (conMensaje And respuestaSi) Then
            MessageBox.Show("Eliminado finalizado.", "Finalizado.", MessageBoxButtons.OK)
            LimpiarPantalla()
            CargarIdConsecutivo()
            AsignarFoco(txtId)
        End If

    End Sub

    Private Sub GuardarEditarSalidasPedidosActualizar()

        ' No capturables por el usuario.
        Dim idExterno As String = String.Empty
        Dim idMoneda As Integer = 1 ' Pesos
        Dim tipoCambio As Double = 1 ' 1
        Dim idTipoSalida As Integer = 1 ' Normal o venta.
        Dim factura As String = String.Empty
        Dim chofer As String = String.Empty
        Dim camion As String = String.Empty
        Dim noEconomico As String = String.Empty
        Dim idAlmacenCopia As Integer = -1
        Dim idCopia As Integer = -1
        ' Parte inferior.
        For fila As Integer = 0 To spPedidosActualizar.ActiveSheet.Rows.Count - 1
            Dim entregado As Boolean = spPedidosActualizar.ActiveSheet.Cells(fila, spPedidosActualizar.ActiveSheet.Columns("entregado").Index).Value
            Dim idAlmacen As Integer = ALMLogicaPedidos.Funciones.ValidarNumeroACero(spPedidosActualizar.ActiveSheet.Cells(fila, spPedidosActualizar.ActiveSheet.Columns("idAlmacen").Index).Text)
            Dim id As Integer = ALMLogicaPedidos.Funciones.ValidarNumeroACero(spPedidosActualizar.ActiveSheet.Cells(fila, spPedidosActualizar.ActiveSheet.Columns("id").Index).Text)
            If (idAlmacen <> idAlmacenCopia AndAlso id <> idCopia AndAlso entregado) Then
                EliminarSalidasPedidosActualizar(False, fila)
                idAlmacenCopia = idAlmacen
                idCopia = id
            End If
            Dim idFamilia As Integer = ALMLogicaPedidos.Funciones.ValidarNumeroACero(spPedidosActualizar.ActiveSheet.Cells(fila, spPedidosActualizar.ActiveSheet.Columns("idFamilia").Index).Text)
            Dim idSubFamilia As Integer = ALMLogicaPedidos.Funciones.ValidarNumeroACero(spPedidosActualizar.ActiveSheet.Cells(fila, spPedidosActualizar.ActiveSheet.Columns("idSubFamilia").Index).Text)
            Dim idArticulo As Integer = ALMLogicaPedidos.Funciones.ValidarNumeroACero(spPedidosActualizar.ActiveSheet.Cells(fila, spPedidosActualizar.ActiveSheet.Columns("idArticulo").Index).Text)
            Dim fechaLLegadaCopia As String = String.Empty
            Dim fechaLlegada As Date
            fechaLLegadaCopia = spPedidosActualizar.ActiveSheet.Cells(fila, spPedidosActualizar.ActiveSheet.Columns("fechaLlegada").Index).Value
            If (Not String.IsNullOrEmpty(fechaLLegadaCopia)) Then
                fechaLlegada = fechaLLegadaCopia
                spPedidosActualizar.ActiveSheet.Cells(fila, spPedidosActualizar.ActiveSheet.Columns("fechaLlegada").Index).BackColor = Color.White
            Else
                If (entregado) Then
                    spPedidosActualizar.ActiveSheet.Cells(fila, spPedidosActualizar.ActiveSheet.Columns("fechaLlegada").Index).BackColor = Color.Orange
                Else
                    spPedidosActualizar.ActiveSheet.Cells(fila, spPedidosActualizar.ActiveSheet.Columns("fechaLlegada").Index).BackColor = Color.White
                End If
            End If
            Dim idCliente As Integer = ALMLogicaPedidos.Funciones.ValidarNumeroACero(spPedidosActualizar.ActiveSheet.Cells(fila, spPedidosActualizar.ActiveSheet.Columns("idCliente").Index).Text)
            Dim cantidad As Integer = ALMLogicaPedidos.Funciones.ValidarNumeroACero(spPedidosActualizar.ActiveSheet.Cells(fila, spPedidosActualizar.ActiveSheet.Columns("cantidad").Index).Text)
            Dim precioUnitario As Double = ALMLogicaPedidos.Funciones.ValidarNumeroACero(spPedidosActualizar.ActiveSheet.Cells(fila, spPedidosActualizar.ActiveSheet.Columns("precioUnitario").Index).Text)
            Dim total As Double = ALMLogicaPedidos.Funciones.ValidarNumeroACero(spPedidosActualizar.ActiveSheet.Cells(fila, spPedidosActualizar.ActiveSheet.Columns("total").Index).Text)
            Dim totalPesos As Double = total
            Dim orden As Integer = fila
            Dim observaciones As String = spPedidosActualizar.ActiveSheet.Cells(fila, spPedidosActualizar.ActiveSheet.Columns("observaciones2").Index).Text
            If (Me.idOrigen > 0 AndAlso id > 0 AndAlso idAlmacen > 0 AndAlso idFamilia > 0 AndAlso idSubFamilia > 0 AndAlso idArticulo > 0 AndAlso idMoneda > 0 AndAlso idTipoSalida > 0 AndAlso idCliente > 0 AndAlso entregado AndAlso (Not String.IsNullOrEmpty(fechaLLegadaCopia))) Then
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
                salidas.EFecha = fechaLlegada
                salidas.ECantidad = cantidad
                salidas.EPrecioUnitario = precioUnitario
                salidas.ETotal = total
                salidas.ETotalPesos = totalPesos
                salidas.EOrden = orden
                salidas.EObservaciones = observaciones
                salidas.EFactura = factura
                salidas.EChofer = chofer
                salidas.ECamion = camion
                salidas.ENoEconomico = noEconomico
                salidas.Guardar()
            End If
        Next
        'MessageBox.Show("Guardado finalizado.", "Finalizado.", MessageBoxButtons.OK)

    End Sub

    Private Sub EliminarSalidasPedidosActualizar(ByVal conMensaje As Boolean, ByVal fila As Integer)

        Dim respuestaSi As Boolean = False
        If (conMensaje) Then
            If (MessageBox.Show("Confirmas que deseas eliminar esta salida?", "Confirmación.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes) Then
                respuestaSi = True
            End If
        End If
        If ((respuestaSi) Or (Not conMensaje)) Then
            Dim idAlmacen As Integer = ALMLogicaPedidos.Funciones.ValidarNumeroACero(spPedidosActualizar.ActiveSheet.Cells(fila, spPedidosActualizar.ActiveSheet.Columns("idAlmacen").Index).Text)
            Dim id As Integer = ALMLogicaPedidos.Funciones.ValidarNumeroACero(spPedidosActualizar.ActiveSheet.Cells(fila, spPedidosActualizar.ActiveSheet.Columns("id").Index).Text)
            salidas.EIdOrigen = Me.idOrigen
            salidas.EIdAlmacen = idAlmacen
            salidas.EId = id
            salidas.Eliminar()
        End If
        If (conMensaje And respuestaSi) Then
            MessageBox.Show("Eliminado finalizado.", "Finalizado.", MessageBoxButtons.OK)
            LimpiarPantalla()
            CargarIdConsecutivo()
            AsignarFoco(txtId)
        End If

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