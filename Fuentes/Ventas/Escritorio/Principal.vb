Imports System.IO
Imports System.ComponentModel
Imports System.Threading

Public Class Principal

    ' Variables de objetos de entidades.
    Public usuarios As New ALMEntidadesVentas.Usuarios()
    Public ventas As New ALMEntidadesVentas.Ventas()
    Public almacenes As New ALMEntidadesVentas.Almacenes()
    Public familias As New ALMEntidadesVentas.Familias()
    Public subFamilias As New ALMEntidadesVentas.SubFamilias()
    Public articulos As New ALMEntidadesVentas.Articulos()
    Public unidadesMedidas As New ALMEntidadesVentas.UnidadesMedidas()
    Public clientes As New ALMEntidadesVentas.Clientes()
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
    Public opcionCatalogoSeleccionada As Integer = 0
    Public esGuardadoValido As Boolean = True
    Public esIzquierda As Boolean = False
    Public datosCatalogo As New DataTable
    ' Variables fijas.
    Public idOrigen As Integer = 1 ' Siempre será 1 para almacén.
    ' Hilos para carga rápida.
    Public hiloCentrar As New Thread(AddressOf Centrar)
    Public hiloNombrePrograma As New Thread(AddressOf CargarNombrePrograma)
    Public hiloEncabezadosTitulos As New Thread(AddressOf CargarEncabezadosTitulos)
    ' Variable de desarrollo.
    Public esDesarrollo As Boolean = True

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
        CargarClientes()
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

    Private Sub spVentas_DialogKey(sender As Object, e As FarPoint.Win.Spread.DialogKeyEventArgs) Handles spVentas.DialogKey

        If (e.KeyData = Keys.Enter) Then
            ControlarSpreadEnter(spVentas)
        End If

    End Sub

    Private Sub spVentas_KeyDown(sender As Object, e As KeyEventArgs) Handles spVentas.KeyDown

        If (e.KeyData = Keys.F6) Then ' Eliminar un registro.
            If (MessageBox.Show("Confirmas que deseas eliminar el registro seleccionado?", "Confirmación.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes) Then
                EliminarRegistroDeSpread(spVentas)
            End If
        ElseIf (e.KeyData = Keys.Enter) Then ' Validar registros.
            ControlarSpreadEnter(spVentas)
        ElseIf (e.KeyData = Keys.F5) Then ' Abrir catalogos.
            Me.Cursor = Cursors.WaitCursor
            CargarCatalogoEnSpread()
            Me.Cursor = Cursors.Default
        ElseIf (e.KeyData = Keys.Escape) Then
            spVentas.ActiveSheet.SetActiveCell(0, 0)
            AsignarFoco(cbClientes)
        End If

    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click

        ValidarGuardado()
        If (Me.esGuardadoValido) Then
            Me.Cursor = Cursors.WaitCursor
            GuardarEditarVentas()
            Me.Cursor = Cursors.Default
        End If

    End Sub

    Private Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click

        Me.Cursor = Cursors.WaitCursor
        EliminarVentas(True)
        Me.Cursor = Cursors.Default

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
            AsignarTooltips("Mostrar.")
        Else
            AsignarTooltips("Ocultar.")
        End If

    End Sub

    Private Sub txtBuscarCatalogo_TextChanged(sender As Object, e As EventArgs) Handles txtBuscarCatalogo.TextChanged

        If (Me.opcionCatalogoSeleccionada = OpcionCatalogo.articulo) Then
            BuscarCatalogosRapidoArticulos()
        Else
            BuscarCatalogos()
        End If

    End Sub

    Private Sub txtBuscarCatalogo_KeyDown(sender As Object, e As KeyEventArgs) Handles txtBuscarCatalogo.KeyDown

        If (e.KeyCode = Keys.Enter) Then
            AsignarFoco(spCatalogos)
        ElseIf (e.KeyCode = Keys.Escape) Then
            VolverFocoDeCatalogos()
        End If

    End Sub

#End Region

#Region "Métodos"

#Region "Básicos"

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
            txtAyuda.Text = "Sección de Ayuda: " & vbNewLine & vbNewLine & "* Teclas básicas: " & vbNewLine & "F5 sirve para mostrar catálogos. " & vbNewLine & "F6 sirve para eliminar un registro únicamente. " & vbNewLine & "Escape sirve para ocultar catálogos que se encuentren desplegados. " & vbNewLine & vbNewLine & "* Catálogos desplegados: " & vbNewLine & "Cuando se muestra algún catálogo, al seleccionar alguna opción de este, se va mostrando en tiempo real en la captura de donde se originó. Cuando se le da doble clic en alguna opción o a la tecla escape se oculta dicho catálogo. " & vbNewLine & vbNewLine & "* Datos obligatorios: " & vbNewLine & "Todos los que tengan el simbolo * son estrictamente obligatorios." & vbNewLine & vbNewLine & "* Captura:" & vbNewLine & "* Parte superior: " & vbNewLine & "En esta parte se capturarán todos los datos que son generales, tal cual como el número de la salida, el almacén al que corresponde, etc." & vbNewLine & "* Parte inferior: " & vbNewLine & "En esta parte se capturarán todos los datos que pueden combinarse, por ejemplo los distintos artículos de ese número de salida." & vbNewLine & vbNewLine & "* Existen los botones de guardar/editar y eliminar todo dependiendo lo que se necesite hacer. "
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
            ALMLogicaVentas.Directorios.id = 2
            ALMLogicaVentas.Directorios.instanciaSql = "BERRY1-DELL\SQLEXPRESS2008"
            ALMLogicaVentas.Directorios.usuarioSql = "AdminBerry"
            ALMLogicaVentas.Directorios.contrasenaSql = "@berry2017"
            pnlEncabezado.BackColor = Color.DarkRed
            pnlPie.BackColor = Color.DarkRed
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

        lblEncabezadoPrograma.Text = "Programa: " + Me.Text
        lblEncabezadoEmpresa.Text = "Directorio: " + ALMLogicaVentas.Directorios.nombre
        lblEncabezadoUsuario.Text = "Usuario: " + ALMLogicaVentas.Usuarios.nombre
        Me.Text = "Programa:  " + Me.nombreEstePrograma + "              Directorio:  " + ALMLogicaVentas.Directorios.nombre + "              Usuario:  " + ALMLogicaVentas.Usuarios.nombre
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
            c.BackColor = Color.White
        Next
        For fila = 0 To spVentas.ActiveSheet.Rows.Count - 1
            For columna = 0 To spVentas.ActiveSheet.Columns.Count - 1
                spVentas.ActiveSheet.Cells(fila, columna).BackColor = Color.White
            Next
        Next
        If (Not chkConservarDatos.Checked) Then
            dtpFecha.Value = Today
            cbClientes.SelectedIndex = 0
        End If
        spVentas.ActiveSheet.DataSource = Nothing
        spVentas.ActiveSheet.Rows.Count = 1
        spVentas.ActiveSheet.SetActiveCell(0, 0)
        LimpiarSpread(spVentas)

    End Sub

    Private Sub LimpiarSpread(ByVal spread As FarPoint.Win.Spread.FpSpread)

        spread.ActiveSheet.ClearRange(0, 0, spread.ActiveSheet.Rows.Count, spread.ActiveSheet.Columns.Count, True)

    End Sub

    Private Sub CargarClientes()

        cbClientes.DataSource = clientes.ObtenerListadoReporte()
        cbClientes.DisplayMember = "IdNombre"
        cbClientes.ValueMember = "Id"

    End Sub

    Private Sub FormatearSpread()

        ' Se cargan tipos de datos de spread.
        CargarTiposDeDatos()
        ' Se cargan las opciones generales. 
        pnlCatalogos.Visible = False
        spVentas.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Seashell
        spCatalogos.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Midnight
        spVentas.ActiveSheet.GrayAreaBackColor = Principal.colorAreaGris
        spVentas.Font = New Font(Principal.tipoLetraSpread, Principal.tamañoLetraSpread, FontStyle.Regular)
        spCatalogos.Font = New Font(Principal.tipoLetraSpread, Principal.tamañoLetraSpread, FontStyle.Regular)
        spVentas.ActiveSheet.ColumnHeader.Rows(0).Height = Principal.alturaFilasEncabezadosGrandesSpread
        spCatalogos.ActiveSheet.ColumnHeader.Rows(0).Height = Principal.alturaFilasEncabezadosGrandesSpread
        spVentas.ActiveSheet.Rows(-1).Height = Principal.alturaFilasSpread
        spCatalogos.ActiveSheet.Rows(-1).Height = Principal.alturaFilasSpread
        spVentas.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        spVentas.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        spCatalogos.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never
        spCatalogos.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Always
        'spVentas.EditModePermanent = True
        spVentas.EditModeReplace = True

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
        If (spread.Name = spVentas.Name) Then
            Dim fila As Integer = 0
            If (columnaActiva = spVentas.ActiveSheet.Columns("idAlmacen").Index) Then
                fila = spVentas.ActiveSheet.ActiveRowIndex
                Dim idAlmacen As Integer = ALMLogicaVentas.Funciones.ValidarNumeroACero(spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("idAlmacen").Index).Value)
                almacenes.EId = idAlmacen
                If (idAlmacen > 0 And idAlmacen > 0) Then
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
                familias.EIdAlmacen = idAlmacen
                familias.EId = idFamilia
                If (idAlmacen > 0 And idFamilia > 0) Then
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
                subFamilias.EIdAlmacen = idAlmacen
                subFamilias.EIdFamilia = idFamilia
                subFamilias.EId = idSubFamilia
                If (idAlmacen > 0 And idFamilia > 0 And idSubFamilia > 0) Then
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
                articulos.EIdAlmacen = idAlmacen
                articulos.EIdFamilia = idFamilia
                articulos.EIdSubFamilia = idSubFamilia
                articulos.EId = idArticulo
                If (idAlmacen > 0 And idFamilia > 0 And idSubFamilia > 0 And idArticulo > 0) Then
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
                        Dim datos2 As New DataTable
                        unidadesMedidas.EId = datos.Rows(0).Item("IdUnidadMedida")
                        datos2 = unidadesMedidas.ObtenerListado()
                        If (datos2.Rows.Count > 0) Then ' Se carga nombre de unidad.
                            spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("nombreUnidadMedida").Index).Value = datos2.Rows(0).Item("Nombre")
                        End If
                        spVentas.ActiveSheet.SetActiveCell(fila, spVentas.ActiveSheet.ActiveColumnIndex + 2)
                        Dim esCapturado As Boolean = spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("esCapturado").Index).Value
                        If (Not esCapturado) Then ' Si no es capturado, es decir, si es nuevo registro.
                            Dim fecha As Date = dtpFecha.Value
                            Dim listaSaldo As New List(Of String)
                            listaSaldo = ValidarFechasPosteriores(idAlmacen, idFamilia, idSubFamilia, idArticulo, fecha)
                            Dim resultado As Boolean = listaSaldo(0)
                            Dim idResultado As Integer = listaSaldo(1)
                            Dim fechaResultado As Date = listaSaldo(2)
                            If (resultado) Then ' Se validan fechas posteriores capturadas.
                                MsgBox("Este artículo tiene salida(s) con fecha posterior. La mas próxima es la no. " & idResultado & " con fecha de " & fechaResultado, MsgBoxStyle.Exclamation, "No permitido.")
                                spVentas.ActiveSheet.ClearRange(fila, spVentas.ActiveSheet.Columns("idArticulo").Index, 1, spVentas.ActiveSheet.Columns.Count - 1, True)
                                spVentas.ActiveSheet.SetActiveCell(fila, spVentas.ActiveSheet.Columns("idArticulo").Index - 1)
                                Exit Sub
                            End If
                        End If
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
                    Dim valorPrecio As String = ALMLogicaVentas.Funciones.ValidarLetra(spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("precio").Index).Text)
                    Dim idAlmacen As Integer = ALMLogicaVentas.Funciones.ValidarNumeroACero(spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("idAlmacen").Index).Value)
                    Dim idFamilia As Integer = ALMLogicaVentas.Funciones.ValidarNumeroACero(spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("idFamilia").Index).Value)
                    Dim idSubFamilia As Integer = ALMLogicaVentas.Funciones.ValidarNumeroACero(spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("idSubFamilia").Index).Value)
                    Dim idArticulo As Integer = ALMLogicaVentas.Funciones.ValidarNumeroACero(spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("idArticulo").Index).Value)
                    articulos.EIdAlmacen = idAlmacen
                    articulos.EIdFamilia = idFamilia
                    articulos.EIdSubFamilia = idSubFamilia
                    articulos.EId = idArticulo
                    'Dim esCapturado As Boolean = spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("esCapturado").Index).Value
                    'If (Not esCapturado) Then ' Si no es capturado, es decir, si es nuevo registro. 
                    '    Dim listaSaldo As New List(Of String)
                    '    listaSaldo = ValidarSaldoSuficiente(idAlmacen, idFamilia, idSubFamilia, idArticulo, cantidad)
                    '    Dim resultado As Boolean = listaSaldo(0)
                    '    Dim saldo As Integer = listaSaldo(1)
                    '    If (Not resultado) Then ' Se valida el saldo del articulo.
                    '        MsgBox("El saldo de este artículo no es suficiente para realizar la salida. Tienes " & saldo & " unidades.", MsgBoxStyle.Exclamation, "No permitido.")
                    '        spVentas.ActiveSheet.ClearRange(fila, spVentas.ActiveSheet.Columns("cantidad").Index, 1, 4, True)
                    '        spVentas.ActiveSheet.SetActiveCell(fila, spVentas.ActiveSheet.ActiveColumnIndex - 1)
                    '        Exit Sub
                    '    End If
                    'End If
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
            ElseIf (columnaActiva = spVentas.ActiveSheet.Columns("descuento").Index) Then
                fila = spVentas.ActiveSheet.ActiveRowIndex
                Dim descuento As Double = ALMLogicaVentas.Funciones.ValidarNumeroACero(spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("descuento").Index).Value)
                Dim subtotal As Double = ALMLogicaVentas.Funciones.ValidarNumeroACero(spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("subtotal").Index).Value)
                If (descuento > 0) Then
                    Dim total As Double = subtotal - descuento
                    If (total >= 0) Then
                        spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("total").Index).Value = total
                    Else
                        spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("descuento").Index).Value = 0
                        spVentas.ActiveSheet.SetActiveCell(fila, spVentas.ActiveSheet.ActiveColumnIndex - 1)
                    End If
                Else
                    spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("descuento").Index).Value = 0
                End If
                'ElseIf (columnaActiva = spVentas.ActiveSheet.Columns("total").Index) Then
                '    fila = spVentas.ActiveSheet.ActiveRowIndex
                '    Dim cantidad As Integer = ALMLogicaVentas.Funciones.ValidarNumeroACero(spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("cantidad").Index).Value)
                '    Dim total As Double = ALMLogicaVentas.Funciones.ValidarNumeroACero(spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("total").Index).Value)
                '    If (cantidad > 0) Then
                '        spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("precio").Index).Value = total / cantidad
                '    Else
                '        spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("total").Index).Value = 0
                '        spVentas.ActiveSheet.SetActiveCell(fila, spVentas.ActiveSheet.ActiveColumnIndex - 1)
                '    End If
            End If
        End If

    End Sub

    Private Sub CargarIdConsecutivo()

        Dim idMaximo As Integer = ventas.ObtenerMaximoId()
        txtId.Text = idMaximo

    End Sub

    Private Sub CargarDatosEnSpreadDeCatalogos(ByVal filaCatalogos As Integer)

        If (spVentas.ActiveSheet.ActiveColumnIndex = spVentas.ActiveSheet.Columns("idFamilia").Index Or spVentas.ActiveSheet.ActiveColumnIndex = spVentas.ActiveSheet.Columns("nombreFamilia").Index) Then
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
        If ((columna = spVentas.ActiveSheet.Columns("idFamilia").Index) Or (columna = spVentas.ActiveSheet.Columns("nombreFamilia").Index)) Then
            Me.opcionCatalogoSeleccionada = OpcionCatalogo.familia
            Dim idAlmacen As Integer = ALMLogicaVentas.Funciones.ValidarNumeroACero(spVentas.ActiveSheet.Cells(spVentas.ActiveSheet.ActiveRowIndex, spVentas.ActiveSheet.Columns("idAlmacen").Index).Text)
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
                    spVentas.Enabled = True
                End If
            Else
                spCatalogos.ActiveSheet.DataSource = Nothing
                spCatalogos.ActiveSheet.Rows.Count = 0
                spVentas.Enabled = True
            End If
            FormatearSpreadCatalogo(OpcionPosicion.derecha)
        ElseIf ((columna = spVentas.ActiveSheet.Columns("idSubFamilia").Index) Or (columna = spVentas.ActiveSheet.Columns("nombreSubFamilia").Index)) Then
            Me.opcionCatalogoSeleccionada = OpcionCatalogo.subfamilia
            Dim idAlmacen As Integer = ALMLogicaVentas.Funciones.ValidarNumeroACero(spVentas.ActiveSheet.Cells(spVentas.ActiveSheet.ActiveRowIndex, spVentas.ActiveSheet.Columns("idAlmacen").Index).Text)
            Dim idFamilia As Integer = ALMLogicaVentas.Funciones.ValidarNumeroACero(spVentas.ActiveSheet.Cells(spVentas.ActiveSheet.ActiveRowIndex, spVentas.ActiveSheet.Columns("idFamilia").Index).Text)
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
                    spVentas.Enabled = True
                End If
            Else
                spCatalogos.ActiveSheet.DataSource = Nothing
                spCatalogos.ActiveSheet.Rows.Count = 0
                spVentas.Enabled = True
            End If
            FormatearSpreadCatalogo(OpcionPosicion.derecha)
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
            datos = articulos.ObtenerListadoReporte()
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
            FormatearSpreadCatalogo(OpcionPosicion.derecha)
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
            spCatalogos.Width = 1040
            spCatalogos.ActiveSheet.Columns.Count = 18
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
            spCatalogos.ActiveSheet.Columns("nombre").Visible = False
            spCatalogos.ActiveSheet.Columns("idProveedor").Visible = False
        End If
        spCatalogos.ActiveSheet.Columns(0, spCatalogos.ActiveSheet.Columns.Count - 1).AllowAutoFilter = True
        spCatalogos.ActiveSheet.Columns(0, spCatalogos.ActiveSheet.Columns.Count - 1).AllowAutoSort = True
        pnlCatalogos.Height = spVentas.Height
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
        ventas.EId = ALMLogicaVentas.Funciones.ValidarNumeroACero(txtId.Text)
        If (ventas.EId > 0) Then
            Dim datos As New DataTable
            datos = ventas.ObtenerListado()
            If (datos.Rows.Count > 0) Then
                dtpFecha.Value = datos.Rows(0).Item("Fecha")
                cbClientes.SelectedValue = datos.Rows(0).Item("IdCliente")
                spVentas.ActiveSheet.DataSource = ventas.ObtenerListadoReporte()
                Me.cantidadFilas = spVentas.ActiveSheet.Rows.Count + 1
                FormatearSpreadVentas()
            Else
                LimpiarPantalla()
            End If
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
        spVentas.ActiveSheet.Columns("nombreArticulo").Width = 200
        spVentas.ActiveSheet.Columns("nombreUnidadMedida").Width = 80
        spVentas.ActiveSheet.Columns("codigo").Width = 140
        spVentas.ActiveSheet.Columns("pagina").Width = 70
        spVentas.ActiveSheet.Columns("color").Width = 100
        spVentas.ActiveSheet.Columns("talla").Width = 70
        spVentas.ActiveSheet.Columns("modelo").Width = 80
        spVentas.ActiveSheet.Columns("codigoInternet").Width = 90
        spVentas.ActiveSheet.Columns("cantidad").Width = 90
        spVentas.ActiveSheet.Columns("precio").Width = 70
        spVentas.ActiveSheet.Columns("subtotal").Width = 90
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
        spVentas.ActiveSheet.AddColumnHeaderSpanCell(0, spVentas.ActiveSheet.Columns("descuento").Index, 2, 1)
        spVentas.ActiveSheet.ColumnHeader.Cells(0, spVentas.ActiveSheet.Columns("descuento").Index).Value = "Descuento *".ToUpper()
        spVentas.ActiveSheet.AddColumnHeaderSpanCell(0, spVentas.ActiveSheet.Columns("total").Index, 2, 1)
        spVentas.ActiveSheet.ColumnHeader.Cells(0, spVentas.ActiveSheet.Columns("total").Index).Value = "Total *".ToUpper() 
        spVentas.ActiveSheet.Columns(spVentas.ActiveSheet.Columns("esCapturado").Index).Visible = False
        MostrarOcultarDetalleVentas(False)
        spVentas.Refresh()

    End Sub

    Private Sub MostrarOcultarDetalleVentas(ByVal valor As Boolean)

        spVentas.ActiveSheet.Columns(spVentas.ActiveSheet.Columns("idAlmacen").Index, spVentas.ActiveSheet.Columns("nombreSubFamilia").Index).Visible = valor
        spVentas.ActiveSheet.Columns(spVentas.ActiveSheet.Columns("nombreArticulo").Index).Visible = valor
        spVentas.ActiveSheet.Columns(spVentas.ActiveSheet.Columns("nombreUnidadMedida").Index).Visible = valor 

    End Sub

    Private Sub ValidarGuardado()

        Me.Cursor = Cursors.WaitCursor
        Me.esGuardadoValido = True
        ' Parte superior. 
        Dim id As Integer = ALMLogicaVentas.Funciones.ValidarNumeroACero(txtId.Text)
        If (id <= 0) Then
            txtId.BackColor = Color.Orange
            Me.esGuardadoValido = False
        End If
        Dim idCliente As Integer = ALMLogicaVentas.Funciones.ValidarNumeroACero(cbClientes.SelectedValue)
        If (idCliente <= 0) Then
            cbClientes.BackColor = Color.Orange
            Me.esGuardadoValido = False
        End If
        ' Parte inferior.
        For fila As Integer = 0 To spVentas.ActiveSheet.Rows.Count - 1
            Dim idAlmacen As Integer = ALMLogicaVentas.Funciones.ValidarNumeroACero(spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("idFamilia").Index).Text)
            Dim idFamilia As Integer = ALMLogicaVentas.Funciones.ValidarNumeroACero(spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("idFamilia").Index).Text)
            Dim idSubFamilia As Integer = ALMLogicaVentas.Funciones.ValidarNumeroACero(spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("idSubFamilia").Index).Text)
            Dim idArticulo As Integer = ALMLogicaVentas.Funciones.ValidarNumeroACero(spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("idArticulo").Index).Text)
            If (idAlmacen > 0 AndAlso idFamilia > 0 AndAlso idSubFamilia > 0 AndAlso idArticulo > 0) Then
                Dim cantidad As Integer = ALMLogicaVentas.Funciones.ValidarNumeroACero(spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("cantidad").Index).Text)
                If (cantidad <= 0) Then
                    spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("cantidad").Index).BackColor = Color.Orange
                    Me.esGuardadoValido = False
                End If
                Dim precio As String = spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("precio").Index).Text
                Dim precio2 As Double = ALMLogicaVentas.Funciones.ValidarNumeroACero(spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("precio").Index).Text)
                If (String.IsNullOrEmpty(precio) Or precio2 < 0) Then
                    spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("precio").Index).BackColor = Color.Orange
                    Me.esGuardadoValido = False
                End If
                Dim subtotal As String = spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("subtotal").Index).Text
                Dim subtotal2 As Double = ALMLogicaVentas.Funciones.ValidarNumeroACero(spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("subtotal").Index).Text)
                If (String.IsNullOrEmpty(subtotal) Or subtotal2 < 0) Then
                    spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("subtotal").Index).BackColor = Color.Orange
                    Me.esGuardadoValido = False
                End If
                Dim descuento As String = spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("descuento").Index).Text
                Dim descuento2 As Double = ALMLogicaVentas.Funciones.ValidarNumeroACero(spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("descuento").Index).Text)
                If (String.IsNullOrEmpty(descuento) Or descuento2 < 0) Then
                    spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("descuento").Index).BackColor = Color.Orange
                    Me.esGuardadoValido = False
                End If
                Dim total As String = spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("total").Index).Text
                Dim total2 As Double = ALMLogicaVentas.Funciones.ValidarNumeroACero(spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("total").Index).Text)
                If (String.IsNullOrEmpty(total) Or total2 < 0) Then
                    spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("total").Index).BackColor = Color.Orange
                    Me.esGuardadoValido = False
                End If
            End If
        Next
        Me.Cursor = Cursors.Default

    End Sub

    Private Sub GuardarEditarVentas()

        EliminarVentas(False)
        ' Parte superior.
        Dim id As Integer = ALMLogicaVentas.Funciones.ValidarNumeroACero(txtId.Text)
        Dim fecha As Date = dtpFecha.Value
        Dim idCliente As Integer = ALMLogicaVentas.Funciones.ValidarNumeroACero(cbClientes.SelectedValue)
        ' Parte inferior.
        For fila As Integer = 0 To spVentas.ActiveSheet.Rows.Count - 1
            Dim idAlmacen As Integer = ALMLogicaVentas.Funciones.ValidarNumeroACero(spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("idAlmacen").Index).Text)
            Dim idFamilia As Integer = ALMLogicaVentas.Funciones.ValidarNumeroACero(spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("idFamilia").Index).Text)
            Dim idSubFamilia As Integer = ALMLogicaVentas.Funciones.ValidarNumeroACero(spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("idSubFamilia").Index).Text)
            Dim idArticulo As Integer = ALMLogicaVentas.Funciones.ValidarNumeroACero(spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("idArticulo").Index).Text)
            Dim cantidad As Integer = ALMLogicaVentas.Funciones.ValidarNumeroACero(spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("cantidad").Index).Text)
            Dim precio As Double = ALMLogicaVentas.Funciones.ValidarNumeroACero(spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("precio").Index).Text)
            Dim subtotal As Double = ALMLogicaVentas.Funciones.ValidarNumeroACero(spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("subtotal").Index).Text)
            Dim descuento As Double = ALMLogicaVentas.Funciones.ValidarNumeroACero(spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("descuento").Index).Text)
            Dim total As Double = ALMLogicaVentas.Funciones.ValidarNumeroACero(spVentas.ActiveSheet.Cells(fila, spVentas.ActiveSheet.Columns("total").Index).Text)
            Dim orden As Integer = fila
            Dim observaciones As String = String.Empty
            If (id > 0 AndAlso idAlmacen > 0 AndAlso idFamilia > 0 AndAlso idSubFamilia > 0 AndAlso idArticulo > 0 AndAlso idCliente > 0) Then
                ventas.EIdAlmacen = idAlmacen
                ventas.EId = id
                ventas.EIdFamilia = idFamilia
                ventas.EIdSubFamilia = idSubFamilia
                ventas.EIdArticulo = idArticulo
                ventas.EIdCliente = idCliente
                ventas.EFecha = fecha
                ventas.ECantidad = cantidad
                ventas.EPrecio = precio
                ventas.ESubTotal = subtotal
                ventas.EDescuento = descuento
                ventas.ETotal = total
                ventas.EOrden = orden
                ventas.EObservaciones = observaciones
                ventas.Guardar()
            End If
        Next
        MessageBox.Show("Guardado finalizado.", "Finalizado.", MessageBoxButtons.OK)
        LimpiarPantalla()
        CargarIdConsecutivo()
        AsignarFoco(txtId)
        txtId.SelectAll()

    End Sub

    Private Sub EliminarVentas(ByVal conMensaje As Boolean)

        Dim respuestaSi As Boolean = False
        If (conMensaje) Then
            If (MessageBox.Show("Confirmas que deseas eliminar esta salida?", "Confirmación.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes) Then
                respuestaSi = True
            End If
        End If
        If ((respuestaSi) Or (Not conMensaje)) Then
            ventas.EId = ALMLogicaVentas.Funciones.ValidarNumeroACero(txtId.Text)
            ventas.Eliminar()
        End If
        If (conMensaje And respuestaSi) Then
            MessageBox.Show("Eliminado finalizado.", "Finalizado.", MessageBoxButtons.OK)
            LimpiarPantalla()
            CargarIdConsecutivo()
            AsignarFoco(txtId)
            txtId.SelectAll()
        End If

    End Sub

    Private Function ValidarSaldoSuficiente(ByVal idAlmacen As Integer, ByVal idFamilia As Integer, ByVal idSubFamilia As Integer, ByVal idArticulo As Integer, ByVal cantidad As Integer) As List(Of String)

        Dim valor As Boolean = False
        Dim saldo As Integer = 0
        ventas.EIdAlmacen = idAlmacen
        ventas.EIdFamilia = idFamilia
        ventas.EIdSubFamilia = idSubFamilia
        ventas.EIdArticulo = idArticulo
        Dim datos As New DataTable
        datos = ventas.ObtenerSaldos()
        If (datos.Rows.Count = 1) Then
            saldo = ALMLogicaVentas.Funciones.ValidarNumeroACero(datos.Rows(0).Item("Cantidad").ToString)
            If (saldo >= cantidad) Then
                valor = True
            Else
                valor = False
            End If
        Else
            valor = False
        End If
        Dim lista As New List(Of String)
        lista.Add(valor)
        lista.Add(saldo)
        Return lista

    End Function

    Private Function ValidarFechasPosteriores(ByVal idAlmacen As Integer, ByVal idFamilia As Integer, ByVal idSubFamilia As Integer, ByVal idArticulo As Integer, ByVal fecha As Date) As List(Of String)

        Dim valor As Boolean = False
        Dim id As Integer = 0
        Dim fechaLocal As Date = Now
        ventas.EIdAlmacen = idAlmacen
        ventas.EIdFamilia = idFamilia
        ventas.EIdSubFamilia = idSubFamilia
        ventas.EIdArticulo = idArticulo
        ventas.EFecha = fecha
        Dim datos As New DataTable
        datos = ventas.ValidarFechasPosteriores()
        If (datos.Rows.Count > 0) Then
            valor = True
            id = datos.Rows(0).Item("Id")
            fechaLocal = datos.Rows(0).Item("Fecha")
        Else
            valor = False
        End If
        Dim lista As New List(Of String)
        lista.Add(valor)
        lista.Add(id)
        lista.Add(fechaLocal)
        Return lista

    End Function

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

#End Region
     
    Private Sub chkMostrarDetallado_CheckedChanged(sender As Object, e As EventArgs) Handles chkMostrarDetallado.CheckedChanged

        If (Me.estaMostrado) Then
            If (chkMostrarDetallado.Checked) Then
                MostrarOcultarDetalleVentas(True)
            Else
                MostrarOcultarDetalleVentas(False)
            End If
        End If

    End Sub

End Class