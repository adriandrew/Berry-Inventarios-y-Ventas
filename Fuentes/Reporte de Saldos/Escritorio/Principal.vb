﻿Imports System.IO
Imports FarPoint.Win.Spread
Imports System.Reflection
Imports System.Threading

Public Class Principal

    ' Variables de objetos de entidades.
    Public saldos As New ALMEntidadesReporteSaldos.Saldos
    Public usuarios As New ALMEntidadesReporteSaldos.Usuarios
    Public almacenes As New ALMEntidadesReporteSaldos.Almacenes()
    Public familias As New ALMEntidadesReporteSaldos.Familias()
    Public subFamilias As New ALMEntidadesReporteSaldos.SubFamilias()
    Public articulos As New ALMEntidadesReporteSaldos.Articulos()
    Public empresas As New ALMEntidadesReporteSaldos.Empresas()
    ' Variables de tipos de datos de spread.
    Public tipoTexto As New FarPoint.Win.Spread.CellType.TextCellType()
    Public tipoEntero As New FarPoint.Win.Spread.CellType.NumberCellType()
    Public tipoDoble As New FarPoint.Win.Spread.CellType.NumberCellType()
    Public tipoPorcentaje As New FarPoint.Win.Spread.CellType.PercentCellType()
    Public tipoHora As New FarPoint.Win.Spread.CellType.DateTimeCellType()
    Public tipoFecha As New FarPoint.Win.Spread.CellType.DateTimeCellType()
    Public tipoBooleano As New FarPoint.Win.Spread.CellType.CheckBoxCellType()
    ' Variables de formatos de spread.
    Public Shared tipoLetraSpread As String = "Microsoft Sans Serif" : Public Shared tamañoLetraSpread As Integer = 11
    Public Shared alturaFilasEncabezadosGrandesSpread As Integer = 35 : Public Shared alturaFilasEncabezadosMedianosSpread As Integer = 28
    Public Shared alturaFilasEncabezadosChicosSpread As Integer = 22 : Public Shared alturaFilasSpread As Integer = 20
    Public Shared colorAreaGris = Color.White
    ' Variables generales.
    Public nombreEstePrograma As String = String.Empty
    Public opcionSeleccionada As Integer = 0
    Public estaMostrado As Boolean = False
    Public ejecutarProgramaPrincipal As New ProcessStartInfo()
    Public rutaTemporal As String = CurDir() & "\ArchivosTemporales"
    Public estaCerrando As Boolean = False
    Public prefijoBaseDatosAlmacen As String = "ALM" & "_"
    Public colorFiltros As Color
    ' Hilos para carga rapida. 
    Public hiloCentrar As New Thread(AddressOf Centrar)
    Public hiloNombrePrograma As New Thread(AddressOf CargarNombrePrograma)
    Public hiloTooltips As New Thread(AddressOf AsignarTooltips)
    Public hiloEncabezadosTitulos As New Thread(AddressOf CargarEncabezadosTitulos)
    Public hiloColor As New Thread(AddressOf CargarValorColor)
    Public hiloTiposDatos As New Thread(AddressOf CargarTiposDeDatos)
    ' Variable de desarrollo.
    Public esDesarrollo As Boolean = False

#Region "Eventos"

    Private Sub Principal_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.Cursor = Cursors.WaitCursor
        MostrarCargando(True) 
        ConfigurarConexiones()
        IniciarHilosCarga() 
        Me.Cursor = Cursors.Default

    End Sub

    Private Sub Principal_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown

        Me.Cursor = Cursors.WaitCursor 
        CargarComboAlmacenes()
        Me.estaMostrado = True
        AsignarFoco(dtpFecha)
        MostrarCargando(False)
        Me.Cursor = Cursors.Default

    End Sub

    Private Sub Principal_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed

        Me.Cursor = Cursors.WaitCursor
        EliminarArchivosTemporales()
        Dim nombrePrograma As String = "PrincipalBerry"
        AbrirPrograma(nombrePrograma, True)
        System.Threading.Thread.Sleep(5000)
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

        Application.Exit()

    End Sub

    Private Sub btnGuardar_MouseEnter(sender As Object, e As EventArgs)

        AsignarTooltips("Guardar.")

    End Sub

    Private Sub btnSalir_MouseEnter(sender As Object, e As EventArgs) Handles btnSalir.MouseEnter

        AsignarTooltips("Salir.")

    End Sub

    Private Sub cbAlmacen_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbAlmacen.SelectedIndexChanged

        If (Me.estaMostrado) Then
            If (cbAlmacen.Items.Count > 1) Then
                If (cbAlmacen.SelectedValue > 0) Then
                    Me.opcionSeleccionada = OpcionNivel.almacen
                    CargarComboFamilias()
                Else
                    cbFamilia.DataSource = Nothing
                    cbFamilia.Enabled = False
                    cbSubFamilia.DataSource = Nothing
                    cbSubFamilia.Enabled = False
                    cbArticulo.DataSource = Nothing
                    cbArticulo.Enabled = False
                End If
            End If
        End If

    End Sub

    Private Sub cbFamilia_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbFamilia.SelectedIndexChanged

        If (Me.estaMostrado) Then
            If (cbFamilia.Items.Count > 1) Then
                If (cbFamilia.SelectedValue > 0) Then
                    Me.opcionSeleccionada = OpcionNivel.familia
                    CargarComboSubFamilias()
                Else
                    cbSubFamilia.DataSource = Nothing
                    cbSubFamilia.Enabled = False
                    cbArticulo.DataSource = Nothing
                    cbArticulo.Enabled = False
                End If
            End If
        End If

    End Sub

    Private Sub cbSubFamilia_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbSubFamilia.SelectedIndexChanged

        If (Me.estaMostrado) Then
            If (cbSubFamilia.Items.Count > 1) Then
                If (cbSubFamilia.SelectedValue > 0) Then
                    Me.opcionSeleccionada = OpcionNivel.subFamilia
                    CargarComboArticulos()
                Else
                    cbArticulo.DataSource = Nothing
                    cbArticulo.Enabled = False
                End If
            End If
        End If

    End Sub

    Private Sub cbArticulo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbArticulo.SelectedIndexChanged

        Me.opcionSeleccionada = OpcionNivel.articulo

    End Sub

    Private Sub pnlCuerpo_MouseEnter(sender As Object, e As EventArgs) Handles pnlPie.MouseEnter, pnlEncabezado.MouseEnter, pnlCuerpo.MouseEnter

        AsignarTooltips(String.Empty)

    End Sub

    Private Sub btnGenerar_Click(sender As Object, e As EventArgs) Handles btnGenerar.Click

        GenerarReporte()

    End Sub

    Private Sub btnGenerar_MouseEnter(sender As Object, e As EventArgs) Handles btnGenerar.MouseEnter

        AsignarTooltips("Generar Reporte.")

    End Sub

    Private Sub pnlFiltros_MouseHover(sender As Object, e As EventArgs) Handles pnlFiltros.MouseHover, gbFechas.MouseHover, gbNiveles.MouseHover, chkFecha.MouseHover, cbAlmacen.MouseHover, cbFamilia.MouseHover, cbSubFamilia.MouseHover, cbArticulo.MouseHover

        AlinearFiltrosNormal()
        AsignarTooltips("Filtros para Generar el Reporte.")

    End Sub

    Private Sub spActividades_MouseHover(sender As Object, e As EventArgs) Handles spReporte.MouseHover

        AlinearFiltrosIzquierda()
        AsignarTooltips("Reporte Generado.")

    End Sub

    Private Sub temporizador_Tick(sender As Object, e As EventArgs) Handles temporizador.Tick

        If (Me.estaCerrando) Then
            Desvanecer()
        Else
            AlinearFiltrosIzquierda()
        End If

    End Sub

    Private Sub btnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click

        Imprimir(False)

    End Sub

    Private Sub btnExportarPdf_Click(sender As Object, e As EventArgs) Handles btnExportarPdf.Click

        Imprimir(True)

    End Sub

    Private Sub btnExportarExcel_Click(sender As Object, e As EventArgs) Handles btnExportarExcel.Click

        ExportarExcel()

    End Sub

    Private Sub btnImprimir_MouseEnter(sender As Object, e As EventArgs) Handles btnImprimir.MouseEnter

        AsignarTooltips("Imprimir.")

    End Sub

    Private Sub btnExportarExcel_MouseEnter(sender As Object, e As EventArgs) Handles btnExportarExcel.MouseEnter

        AsignarTooltips("Exportar a Excel.")

    End Sub

    Private Sub btnExportarPdf_MouseEnter(sender As Object, e As EventArgs) Handles btnExportarPdf.MouseEnter

        AsignarTooltips("Exportar a Pdf.")

    End Sub

    Private Sub btnAyuda_Click(sender As Object, e As EventArgs) Handles btnAyuda.Click

        MostrarAyuda()

    End Sub

    Private Sub btnAyuda_MouseEnter(sender As Object, e As EventArgs) Handles btnAyuda.MouseEnter

        AsignarTooltips("Ayuda.")

    End Sub

    Private Sub chkFecha_CheckedChanged(sender As Object, e As EventArgs) Handles chkFecha.CheckedChanged

        If (chkFecha.Checked) Then
            chkFecha.Text = "SI"
        Else
            chkFecha.Text = "NO"
        End If

    End Sub

    Private Sub dtpFecha_KeyDown(sender As Object, e As KeyEventArgs) Handles dtpFecha.KeyDown

        If (e.KeyCode = Keys.Enter) Then
            AsignarFoco(dtpFechaFinal)
        End If

    End Sub

    Private Sub dtpFechaFinal_KeyDown(sender As Object, e As KeyEventArgs) Handles dtpFechaFinal.KeyDown

        If (e.KeyCode = Keys.Enter) Then
            AsignarFoco(cbAlmacen)
        ElseIf (e.KeyCode = Keys.Escape) Then
            AsignarFoco(dtpFecha)
        End If

    End Sub

    Private Sub cbAlmacen_KeyDown(sender As Object, e As KeyEventArgs) Handles cbAlmacen.KeyDown

        If (e.KeyCode = Keys.Enter) Then
            If (cbAlmacen.SelectedValue <= 0) Then
                AsignarFoco(btnGenerar)
            Else
                AsignarFoco(cbFamilia)
            End If
        ElseIf (e.KeyCode = Keys.Escape) Then
            AsignarFoco(dtpFechaFinal)
        End If

    End Sub

    Private Sub cbFamilia_KeyDown(sender As Object, e As KeyEventArgs) Handles cbFamilia.KeyDown

        If (e.KeyCode = Keys.Enter) Then
            If (cbFamilia.SelectedValue <= 0) Then
                AsignarFoco(btnGenerar)
            Else
                AsignarFoco(cbSubFamilia)
            End If
        ElseIf (e.KeyCode = Keys.Escape) Then
            AsignarFoco(cbAlmacen)
        End If

    End Sub

    Private Sub cbSubFamilia_KeyDown(sender As Object, e As KeyEventArgs) Handles cbSubFamilia.KeyDown

        If (e.KeyCode = Keys.Enter) Then
            If (cbSubFamilia.SelectedValue <= 0) Then
                AsignarFoco(btnGenerar)
            Else
                AsignarFoco(cbArticulo)
            End If
        ElseIf (e.KeyCode = Keys.Escape) Then
            AsignarFoco(cbFamilia)
        End If

    End Sub

    Private Sub cbArticulo_KeyDown(sender As Object, e As KeyEventArgs) Handles cbArticulo.KeyDown

        If (e.KeyCode = Keys.Enter) Then
            If (cbArticulo.SelectedValue <= 0) Then
                AsignarFoco(btnGenerar)
            Else
                AsignarFoco(btnGenerar)
            End If
        ElseIf (e.KeyCode = Keys.Escape) Then
            AsignarFoco(cbSubFamilia)
        End If

    End Sub

    Private Sub btnGenerar_KeyDown(sender As Object, e As KeyEventArgs) Handles btnGenerar.KeyDown

        If (e.KeyCode = Keys.Escape) Then
            If (cbArticulo.Enabled) Then
                AsignarFoco(cbArticulo)
            ElseIf (cbSubFamilia.Enabled) Then
                AsignarFoco(cbSubFamilia)
            ElseIf (cbFamilia.Enabled) Then
                AsignarFoco(cbFamilia)
            ElseIf (cbAlmacen.Enabled) Then
                AsignarFoco(cbAlmacen)
            End If
        End If

    End Sub

#End Region

#Region "Métodos"

#Region "Básicos"

    Public Sub IniciarHilosCarga()

        CheckForIllegalCrossThreadCalls = False
        hiloNombrePrograma.Start()
        hiloCentrar.Start()
        hiloTooltips.Start()
        hiloEncabezadosTitulos.Start()
        hiloColor.Start()
        hiloTiposDatos.Start()

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
            pnlCargando.BackgroundImage = Global.ReporteSaldos.My.Resources.bienvenida
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

    Private Sub AsignarFoco(ByVal c As Control)

        c.Focus()

    End Sub

    Private Sub MostrarAyuda()

        Dim pnlAyuda As New Panel()
        Dim txtAyuda As New TextBox()
        If (pnlContenido.Controls.Find("pnlAyuda", True).Count = 0) Then
            pnlAyuda.Name = "pnlAyuda" : Application.DoEvents()
            pnlAyuda.Visible = False : Application.DoEvents()
            pnlContenido.Controls.Add(pnlAyuda) : Application.DoEvents()
            txtAyuda.Name = "txtAyuda" : Application.DoEvents()
            pnlAyuda.Controls.Add(txtAyuda) : Application.DoEvents()
        Else
            pnlAyuda = pnlContenido.Controls.Find("pnlAyuda", False)(0) : Application.DoEvents()
            txtAyuda = pnlAyuda.Controls.Find("txtAyuda", False)(0) : Application.DoEvents()
        End If
        If (Not pnlAyuda.Visible) Then
            pnlCuerpo.Visible = False : Application.DoEvents()
            pnlAyuda.Visible = True : Application.DoEvents()
            pnlAyuda.Size = pnlCuerpo.Size : Application.DoEvents()
            pnlAyuda.Location = pnlCuerpo.Location : Application.DoEvents()
            pnlContenido.Controls.Add(pnlAyuda) : Application.DoEvents()
            txtAyuda.ScrollBars = ScrollBars.Both : Application.DoEvents()
            txtAyuda.Multiline = True : Application.DoEvents()
            txtAyuda.Width = pnlAyuda.Width - 10 : Application.DoEvents()
            txtAyuda.Height = pnlAyuda.Height - 10 : Application.DoEvents()
            txtAyuda.Location = New Point(5, 5) : Application.DoEvents()
            txtAyuda.Text = "Sección de Ayuda: " & vbNewLine & vbNewLine & "* Reporte: " & vbNewLine & "En esta pantalla se desplegará el reporte de acuerdo a los filtros que se hayan seleccionado. " & vbNewLine & "En la parte izquierda se puede agregar cualquiera de los filtros. Existen unos botones que se encuentran en las fechas que contienen la palabra si o no, si la palabra mostrada es si, el rango de fecha correspondiente se incluirá como filtro para el reporte, esto aplica para todas las opciones de fechas. Posteriormente se procede a generar el reporte con los criterios seleccionados. Cuando se termine de generar dicho reporte, se habilitarán las opciones de imprimir, exportar a excel o exportar a pdf, en estas dos últimas el usuario puede guardarlos directamente desde el archivo que se muestra en pantalla si así lo desea, mas no desde el sistema directamente." : Application.DoEvents()
            pnlAyuda.Controls.Add(txtAyuda) : Application.DoEvents()
        Else
            pnlCuerpo.Visible = True : Application.DoEvents()
            pnlAyuda.Visible = False : Application.DoEvents()
        End If

    End Sub

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
        tp.SetToolTip(Me.btnSalir, "Salir.")
        tp.SetToolTip(Me.btnImprimir, "Imprimir.")
        tp.SetToolTip(Me.btnExportarExcel, "Exportar a Excel.")
        tp.SetToolTip(Me.btnExportarPdf, "Exportar a Pdf.")
        tp.SetToolTip(Me.btnGenerar, "Generar Reporte.")
        tp.SetToolTip(Me.pnlFiltros, "Filtros para Generar el Reporte.")
        tp.SetToolTip(Me.spReporte, "Datos del Reporte.")
        hiloTooltips.Abort()

    End Sub

    Private Sub AsignarTooltips(ByVal texto As String)

        lblDescripcionTooltip.Text = texto

    End Sub

    Public Sub ControlarSpreadEnter(ByVal spread As FarPoint.Win.Spread.FpSpread)

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
        hiloTiposDatos.Abort()

    End Sub

    Private Sub ConfigurarConexiones()

        If (Me.esDesarrollo) Then
            ALMLogicaReporteSaldos.Directorios.id = 1
            ALMLogicaReporteSaldos.Directorios.instanciaSql = "BERRY1-DELL\SQLEXPRESS2008"
            ALMLogicaReporteSaldos.Directorios.usuarioSql = "AdminBerry"
            ALMLogicaReporteSaldos.Directorios.contrasenaSql = "@berry2017"
            pnlEncabezado.BackColor = Color.DarkRed
            pnlPie.BackColor = Color.DarkRed
        Else
            ALMLogicaReporteSaldos.Directorios.ObtenerParametros()
            ALMLogicaReporteSaldos.Usuarios.ObtenerParametros()
        End If
        ALMLogicaReporteSaldos.Programas.bdCatalogo = "Catalogo" & ALMLogicaReporteSaldos.Directorios.id
        ALMLogicaReporteSaldos.Programas.bdConfiguracion = "Configuracion" & ALMLogicaReporteSaldos.Directorios.id
        ALMLogicaReporteSaldos.Programas.bdAlmacen = "Almacen" & ALMLogicaReporteSaldos.Directorios.id
        ALMEntidadesReporteSaldos.BaseDatos.ECadenaConexionCatalogo = ALMLogicaReporteSaldos.Programas.bdCatalogo
        ALMEntidadesReporteSaldos.BaseDatos.ECadenaConexionConfiguracion = ALMLogicaReporteSaldos.Programas.bdConfiguracion
        ALMEntidadesReporteSaldos.BaseDatos.ECadenaConexionAlmacen = ALMLogicaReporteSaldos.Programas.bdAlmacen
        ALMEntidadesReporteSaldos.BaseDatos.AbrirConexionCatalogo()
        ALMEntidadesReporteSaldos.BaseDatos.AbrirConexionConfiguracion()
        ALMEntidadesReporteSaldos.BaseDatos.AbrirConexionAlmacen()
        ConsultarInformacionUsuario()
        CargarPrefijoBaseDatosAlmacen()

    End Sub

    Private Sub ConsultarInformacionUsuario()

        Dim lista As New List(Of ALMEntidadesReporteSaldos.Usuarios)
        usuarios.EId = ALMLogicaReporteSaldos.Usuarios.id
        lista = usuarios.ObtenerListado()
        If (lista.Count = 1) Then
            ALMLogicaReporteSaldos.Usuarios.id = lista(0).EId
            ALMLogicaReporteSaldos.Usuarios.nombre = lista(0).ENombre
            ALMLogicaReporteSaldos.Usuarios.contrasena = lista(0).EContrasena
            ALMLogicaReporteSaldos.Usuarios.nivel = lista(0).ENivel
            ALMLogicaReporteSaldos.Usuarios.accesoTotal = lista(0).EAccesoTotal
        End If

    End Sub

    Private Sub CargarPrefijoBaseDatosAlmacen()

        ALMLogicaReporteSaldos.Programas.prefijoBaseDatosAlmacen = Me.prefijoBaseDatosAlmacen

    End Sub
     
    Private Sub CargarEncabezadosTitulos()

        lblEncabezadoPrograma.Text = "Programa: " + Me.Text
        lblEncabezadoEmpresa.Text = "Directorio: " + ALMLogicaReporteSaldos.Directorios.nombre
        lblEncabezadoUsuario.Text = "Usuario: " + ALMLogicaReporteSaldos.Usuarios.nombre
        Me.Text = "Programa:  " + Me.nombreEstePrograma + "              Directorio:  " + ALMLogicaReporteSaldos.Directorios.nombre + "              Usuario:  " + ALMLogicaReporteSaldos.Usuarios.nombre
        hiloEncabezadosTitulos.Abort()

    End Sub

    Private Sub CargarValorColor()

        Me.colorFiltros = pnlFiltros.BackColor
        hiloColor.Abort()

    End Sub

    Private Sub PonerFocoEnControl(ByVal c As Control)

        c.Focus()

    End Sub

    Private Sub AbrirPrograma(nombre As String, salir As Boolean)

        If (Me.esDesarrollo) Then
            Exit Sub
        End If
        ejecutarProgramaPrincipal.UseShellExecute = True
        ejecutarProgramaPrincipal.FileName = nombre & Convert.ToString(".exe")
        ejecutarProgramaPrincipal.WorkingDirectory = Application.StartupPath
        ejecutarProgramaPrincipal.Arguments = ALMLogicaReporteSaldos.Directorios.id.ToString().Trim().Replace(" ", "|") & " " & ALMLogicaReporteSaldos.Directorios.nombre.ToString().Trim().Replace(" ", "|") & " " & ALMLogicaReporteSaldos.Directorios.descripcion.ToString().Trim().Replace(" ", "|") & " " & ALMLogicaReporteSaldos.Directorios.rutaLogo.ToString().Trim().Replace(" ", "|") & " " & ALMLogicaReporteSaldos.Directorios.esPredeterminado.ToString().Trim().Replace(" ", "|") & " " & ALMLogicaReporteSaldos.Directorios.instanciaSql.ToString().Trim().Replace(" ", "|") & " " & ALMLogicaReporteSaldos.Directorios.usuarioSql.ToString().Trim().Replace(" ", "|") & " " & ALMLogicaReporteSaldos.Directorios.contrasenaSql.ToString().Trim().Replace(" ", "|") & " " & "Aquí terminan los de directorios, indice 9 ;)".Replace(" ", "|") & " " & ALMLogicaReporteSaldos.Usuarios.id.ToString().Trim().Replace(" ", "|") & " " & "Aquí terminan los de usuario, indice 11 ;)".Replace(" ", "|")
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

#End Region

#Region "Todos"

    Private Sub Imprimir(ByVal esPdf As Boolean)

        Me.Cursor = Cursors.WaitCursor
        ' Se carga la información de la empresa.
        Dim lista As New List(Of ALMEntidadesReporteSaldos.Empresas)
        empresas.EId = 0 ' Se busca la primer empresa.
        lista = empresas.Obtener(True)
        If (lista.Count = 0) Then
            MsgBox("No existen datos de la empresa para encabezados de impresión. Se cancelará la impresión.", MsgBoxStyle.Information, "Faltan datos.")
            Exit Sub
        End If
        Dim nombrePdf As String = "\Temporal.pdf"
        Dim fuente7 As Integer = 7 : Dim fuente8 As Integer = 8
        Dim encabezadoPuntoPago As String = String.Empty
        Dim informacionImpresion As New FarPoint.Win.Spread.PrintInfo
        impresor.AllowSelection = True
        impresor.AllowSomePages = True
        impresor.AllowCurrentPage = True
        informacionImpresion.Orientation = PrintOrientation.Landscape
        informacionImpresion.Margin.Top = 20
        informacionImpresion.Margin.Left = 20
        informacionImpresion.Margin.Right = 20
        informacionImpresion.Margin.Bottom = 20
        informacionImpresion.ShowBorder = False
        informacionImpresion.ShowGrid = False
        informacionImpresion.ZoomFactor = 0.6
        informacionImpresion.Printer = impresor.PrinterSettings.PrinterName
        informacionImpresion.Centering = FarPoint.Win.Spread.Centering.Horizontal
        informacionImpresion.ShowRowHeader = FarPoint.Win.Spread.PrintHeader.Hide
        informacionImpresion.ShowColumnHeader = FarPoint.Win.Spread.PrintHeader.Show
        Dim encabezado1 As String = String.Empty
        Dim encabezado2 As String = String.Empty
        Dim encabezado3 As String = String.Empty
        encabezado1 = "/l/fz""" & fuente7 & """" & "Rfc " & lista(0).ERfc & "/c/fz""" & fuente7 & """" & lista(0).ENombre
        encabezado1 &= "/r/fz""" & fuente7 & """" & "Página /p de /pc"
        encabezado1 = encabezado1.ToUpper
        encabezado2 = "/l/fz""" & fuente7 & """" & lista(0).EDomicilio & "/c/fb1/fz""" & fuente8 & """" & lista(0).EDescripcion & "/r/fz""" & fuente7 & """" & "Fecha: " & Today.ToShortDateString
        encabezado2 = encabezado2.ToUpper
        encabezado3 = "/l/fz""" & fuente7 & """" & lista(0).ELocalidad & "/c/fb1/fz""" & fuente8 & """" & spReporte.ActiveSheet.SheetName & "/r/fz""" & fuente7 & """" & "Hora: " & Now.ToShortTimeString
        encabezado3 = encabezado3.ToUpper
        If esPdf Then
            Dim bandera As Boolean = True
            Dim obtenerRandom As System.Random = New System.Random()
            Try
                If (Not Directory.Exists(rutaTemporal)) Then
                    Directory.CreateDirectory(rutaTemporal)
                End If
            Catch ex As Exception
            End Try
            While bandera
                nombrePdf = "\" & obtenerRandom.Next(0, 99999).ToString.PadLeft(5, "0") & ".pdf"
                If Not File.Exists(rutaTemporal & nombrePdf) Then
                    bandera = False
                End If
            End While
            informacionImpresion.PdfWriteTo = PdfWriteTo.File
            informacionImpresion.PdfFileName = rutaTemporal & nombrePdf
            informacionImpresion.PrintToPdf = True
        End If
        informacionImpresion.Header = encabezado1 & "/n" & encabezado2 & "/n" & encabezado3
        informacionImpresion.Footer = "Creado por: Software Berry"
        For indice = 0 To spReporte.Sheets.Count - 1
            spReporte.Sheets(indice).PrintInfo = informacionImpresion
        Next
        If Not esPdf Then
            If impresor.ShowDialog = Windows.Forms.DialogResult.OK Then
                spReporte.PrintSheet(-1)
            End If
        Else
            spReporte.PrintSheet(-1)
            Try
                System.Diagnostics.Process.Start(nombrePdf)
                System.Diagnostics.Process.Start(rutaTemporal & nombrePdf)
            Catch
                System.Diagnostics.Process.Start(rutaTemporal & nombrePdf)
            End Try
        End If
        Me.Cursor = Cursors.Default
        Application.DoEvents()

    End Sub

    Private Sub ExportarExcel()

        Me.Cursor = Cursors.WaitCursor
        spParaClonar.Sheets.Clear()
        spParaClonar = ClonarSpread(spParaClonar)
        Dim bandera As Boolean = True
        Dim nombreExcel As String = "\Temporal.xls"
        Dim obtenerRandom As System.Random = New System.Random()
        FormatearExcel()
        Application.DoEvents()
        Try
            If (Not Directory.Exists(rutaTemporal)) Then
                Directory.CreateDirectory(rutaTemporal)
            End If
        Catch ex As Exception
        End Try
        While bandera
            nombreExcel = "\" & obtenerRandom.Next(0, 99999).ToString.PadLeft(5, "0") & ".xls"
            If Not File.Exists(rutaTemporal & nombreExcel) Then
                bandera = False
            End If
        End While
        spParaClonar.SaveExcel(rutaTemporal & nombreExcel, FarPoint.Win.Spread.Model.IncludeHeaders.ColumnHeadersCustomOnly)
        System.Diagnostics.Process.Start(rutaTemporal & nombreExcel)
        Me.Cursor = Cursors.Default

    End Sub

    Private Function ClonarSpread(baseObject As FpSpread) As FpSpread

        ' Copying to a memory stream
        Dim ms As New System.IO.MemoryStream()
        FarPoint.Win.Spread.Model.SpreadSerializer.SaveXml(spReporte, ms, False)
        ms = New System.IO.MemoryStream(ms.ToArray())
        ' Copying from memory stream to clone spread object
        Dim newSpread As New FarPoint.Win.Spread.FpSpread()
        FarPoint.Win.Spread.Model.SpreadSerializer.OpenXml(newSpread, ms)
        Dim fInfo As FieldInfo() = GetType(FarPoint.Win.Spread.FpSpread).GetFields(BindingFlags.Instance Or BindingFlags.[Public] Or BindingFlags.NonPublic Or BindingFlags.[Static])
        For Each field As FieldInfo In fInfo
            If field IsNot Nothing Then
                Dim del As [Delegate] = Nothing
                If field.FieldType.Name.Contains("EventHandler") Then
                    del = DirectCast(field.GetValue(baseObject), [Delegate])
                End If

                If del IsNot Nothing Then
                    Dim eInfo As EventInfo = GetType(FarPoint.Win.Spread.FpSpread).GetEvent(del.Method.Name.Substring(del.Method.Name.IndexOf("_"c) + 1))
                    If eInfo IsNot Nothing Then
                        eInfo.AddEventHandler(newSpread, del)
                    End If
                End If
            End If
        Next
        Return newSpread

    End Function

    Private Sub FormatearExcel()

        ' Se carga la información de la empresa.
        Dim lista As New List(Of ALMEntidadesReporteSaldos.Empresas)
        empresas.EId = 0 ' Se busca la primer empresa.
        lista = empresas.Obtener(True)
        If (lista.Count = 0) Then
            MsgBox("No existen datos de la empresa para encabezados de impresión. Se cancelará la impresión.", MsgBoxStyle.Information, "Faltan datos.")
            Exit Sub
        End If
        Dim fuente6 As Integer = 6
        Dim fuente7 As Integer = 7
        Dim fuente8 As Integer = 8
        Dim encabezado1I As String = String.Empty
        Dim encabezado1C As String = String.Empty
        Dim encabezado2I As String = String.Empty
        Dim encabezado2C As String = String.Empty
        Dim encabezado2D As String = String.Empty
        Dim encabezado3I As String = String.Empty
        Dim encabezado3C As String = String.Empty
        Dim encabezado3D As String = String.Empty
        encabezado1I = "RFC " & lista(0).ERfc : encabezado1I = encabezado1I.ToUpper
        encabezado1C = lista(0).ENombre : encabezado1C = encabezado1C.ToUpper
        encabezado2I = lista(0).EDomicilio : encabezado2I = encabezado2I.ToUpper
        encabezado2C = lista(0).EDescripcion : encabezado2C = encabezado2C.ToUpper
        encabezado2D = "Fecha: " & Today.ToShortDateString : encabezado2D = encabezado2D.ToUpper
        encabezado3I = lista(0).ELocalidad : encabezado3I = encabezado3I.ToUpper
        encabezado3C = spReporte.ActiveSheet.SheetName : encabezado3C = encabezado3C.ToUpper
        encabezado3D = "Hora: " & Now.ToShortTimeString : encabezado3D = encabezado3D.ToUpper
        For indice = 0 To spParaClonar.Sheets.Count - 1
            spParaClonar.Sheets(indice).Columns.Count = spReporte.Sheets(indice).Columns.Count + 10
            spParaClonar.Sheets(indice).Protect = False
            spParaClonar.Sheets(indice).ColumnHeader.Rows.Add(0, 6)
            spParaClonar.Sheets(indice).AddColumnHeaderSpanCell(0, 0, 1, 3) 'spParaClonar.Sheets(i).ColumnCount 
            spParaClonar.Sheets(indice).AddColumnHeaderSpanCell(0, 3, 1, 5)
            spParaClonar.Sheets(indice).AddColumnHeaderSpanCell(0, 8, 1, 2)
            spParaClonar.Sheets(indice).AddColumnHeaderSpanCell(1, 0, 1, 3)
            spParaClonar.Sheets(indice).AddColumnHeaderSpanCell(1, 3, 1, 5)
            spParaClonar.Sheets(indice).AddColumnHeaderSpanCell(1, 8, 1, 2)
            spParaClonar.Sheets(indice).AddColumnHeaderSpanCell(2, 0, 1, 3)
            spParaClonar.Sheets(indice).AddColumnHeaderSpanCell(2, 3, 1, 5)
            spParaClonar.Sheets(indice).AddColumnHeaderSpanCell(2, 8, 1, 2)
            spParaClonar.Sheets(indice).AddColumnHeaderSpanCell(3, 0, 1, 3)
            spParaClonar.Sheets(indice).AddColumnHeaderSpanCell(3, 3, 1, 5)
            spParaClonar.Sheets(indice).AddColumnHeaderSpanCell(4, 0, 1, spParaClonar.Sheets(indice).ColumnCount)
            spParaClonar.Sheets(indice).ColumnHeader.Cells(0, 0).Text = encabezado1I
            spParaClonar.Sheets(indice).ColumnHeader.Cells(0, 3).Text = encabezado1C
            spParaClonar.Sheets(indice).ColumnHeader.Cells(1, 0).Text = encabezado2I
            spParaClonar.Sheets(indice).ColumnHeader.Cells(1, 3).Text = encabezado2C
            spParaClonar.Sheets(indice).ColumnHeader.Cells(1, 8).Text = encabezado2D
            spParaClonar.Sheets(indice).ColumnHeader.Cells(2, 0).Text = encabezado3I
            spParaClonar.Sheets(indice).ColumnHeader.Cells(2, 3).Text = encabezado3C
            spParaClonar.Sheets(indice).ColumnHeader.Cells(2, 8).Text = encabezado3D
            spParaClonar.Sheets(indice).ColumnHeader.Cells(4, 0).Border = New FarPoint.Win.LineBorder(Color.Black, 1, False, True, False, False)
            spParaClonar.Sheets(indice).ColumnHeader.Cells(0, 0).Font = New Font("microsoft sans serif", fuente7, FontStyle.Bold)
            spParaClonar.Sheets(indice).ColumnHeader.Cells(0, 3).Font = New Font("microsoft sans serif", fuente8, FontStyle.Bold)
            spParaClonar.Sheets(indice).ColumnHeader.Cells(0, 8).Font = New Font("microsoft sans serif", fuente7, FontStyle.Bold)
            spParaClonar.Sheets(indice).ColumnHeader.Cells(1, 0).Font = New Font("microsoft sans serif", fuente7, FontStyle.Bold)
            spParaClonar.Sheets(indice).ColumnHeader.Cells(1, 3).Font = New Font("microsoft sans serif", fuente8, FontStyle.Bold)
            spParaClonar.Sheets(indice).ColumnHeader.Cells(1, 8).Font = New Font("microsoft sans serif", fuente7, FontStyle.Bold)
            spParaClonar.Sheets(indice).ColumnHeader.Cells(2, 0).Font = New Font("microsoft sans serif", fuente7, FontStyle.Bold)
            spParaClonar.Sheets(indice).ColumnHeader.Cells(2, 3).Font = New Font("microsoft sans serif", fuente8, FontStyle.Bold)
            spParaClonar.Sheets(indice).ColumnHeader.Cells(2, 8).Font = New Font("microsoft sans serif", fuente7, FontStyle.Bold)
            spParaClonar.Sheets(indice).ColumnHeader.Cells(3, 0).Font = New Font("microsoft sans serif", fuente7, FontStyle.Bold)
            spParaClonar.Sheets(indice).ColumnHeader.Cells(3, 3).Font = New Font("microsoft sans serif", fuente8, FontStyle.Bold)
            spParaClonar.Sheets(indice).ColumnHeader.Cells(3, 8).Font = New Font("microsoft sans serif", fuente7, FontStyle.Bold)
            spParaClonar.Sheets(indice).ColumnHeader.Cells(0, 0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            spParaClonar.Sheets(indice).ColumnHeader.Cells(1, 0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            spParaClonar.Sheets(indice).ColumnHeader.Cells(1, 3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
            spParaClonar.Sheets(indice).ColumnHeader.Cells(1, 8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right
            spParaClonar.Sheets(indice).ColumnHeader.Cells(2, 0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            spParaClonar.Sheets(indice).ColumnHeader.Cells(2, 3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
            spParaClonar.Sheets(indice).ColumnHeader.Cells(2, 8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right
            spParaClonar.Sheets(indice).ColumnHeader.Cells(3, 0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
            spParaClonar.Sheets(indice).ColumnHeader.Cells(3, 3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
            spParaClonar.Sheets(indice).ColumnHeader.Cells(3, 8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right
        Next
        spParaClonar.ActiveSheet.Protect = False
        spParaClonar.ActiveSheet.Rows.Count += 20 ' Se aumenta la cantidad de filas debido a un bug del spread al exportar a excel.

    End Sub

    Private Sub EliminarArchivosTemporales()

        Try
            If Directory.Exists(rutaTemporal) Then
                Directory.Delete(rutaTemporal, True)
                Directory.CreateDirectory(rutaTemporal)
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub AlinearFiltrosIzquierda()

        temporizador.Interval = 1
        temporizador.Enabled = True
        temporizador.Start()
        Dim ancho As Integer = -(pnlFiltros.Width - (pnlFiltros.Width / 3))
        If (pnlFiltros.Location.X > ancho) Then
            pnlFiltros.Location = New Point(pnlFiltros.Location.X - (pnlFiltros.Width / 5), pnlFiltros.Location.Y)
            spReporte.Location = New Point(spReporte.Location.X - (pnlFiltros.Width / 5), spReporte.Location.Y)
            Application.DoEvents()
        Else
            temporizador.Enabled = False
            temporizador.Stop()
            AlinearFiltrosIzquierda2()
        End If

    End Sub

    Private Sub AlinearFiltrosIzquierda2()

        pnlFiltros.BackColor = Color.Gray
        btnGenerar.Enabled = False
        spReporte.Width = pnlCuerpo.Width - (pnlFiltros.Width / 5) - 5
        Application.DoEvents()

    End Sub

    Private Sub AlinearFiltrosNormal()

        pnlFiltros.Left = 0
        pnlFiltros.BackColor = Me.colorFiltros
        btnGenerar.Enabled = True
        System.Threading.Thread.Sleep(250)
        spReporte.Width = pnlCuerpo.Width - pnlFiltros.Width - 5
        spReporte.Location = New Point(pnlFiltros.Location.X + pnlFiltros.Width + 5, pnlFiltros.Location.Y)
        Application.DoEvents()

    End Sub

    Private Sub GenerarReporte()

        Me.Cursor = Cursors.WaitCursor
        FormatearSpread()
        Dim datos As New DataTable
        If (Me.estaMostrado) Then
            saldos.EIdAlmacen = cbAlmacen.SelectedValue
            saldos.EIdFamilia = cbFamilia.SelectedValue
            saldos.EIdSubFamilia = cbSubFamilia.SelectedValue
            saldos.EIdArticulo = cbArticulo.SelectedValue
        End If
        Dim fecha As Date = dtpFecha.Value.ToShortDateString : Dim fecha2 As Date = dtpFechaFinal.Value.ToShortDateString
        Dim aplicaFecha As Boolean = False
        If (chkFecha.Checked) Then
            aplicaFecha = True
            saldos.EFecha = fecha : saldos.EFecha2 = fecha2
        Else
            aplicaFecha = False
        End If
        datos = saldos.ObtenerListadoReporte(aplicaFecha)
        spReporte.ActiveSheet.DataSource = datos
        FormatearSpreadReporte(spReporte.ActiveSheet.Columns.Count)
        CalcularTotales(0, "Total", 8, spReporte.ActiveSheet.Columns("saldoAnterior").Index, spReporte.ActiveSheet.Columns.Count, 0, spReporte.ActiveSheet.Rows.Count)
        GenerarReporteGraficoCircular()
        GenerarReporteGraficoBarras()
        AlinearFiltrosIzquierda()
        btnImprimir.Enabled = True
        btnExportarExcel.Enabled = True
        btnExportarPdf.Enabled = True
        AsignarFoco(dtpFecha)
        Me.Cursor = Cursors.Default

    End Sub

    Private Sub CalcularTotales(ByVal columnaConceptoTotal As Integer, ByVal valorColumnaConceptoTotal As String, ByVal cantidadColumnasSpan As Integer, ByVal columnaInicial As Integer, ByVal columnaFinal As Integer, ByVal filaInicial As Integer, ByVal filaFinal As Integer)

        If (filaFinal > 0) Then
            Dim numeroColumnas As Integer = spReporte.ActiveSheet.Columns.Count
            spReporte.ActiveSheet.AddUnboundRows(filaFinal, 1)
            spReporte.ActiveSheet.AddSpanCell(filaFinal, columnaConceptoTotal, 1, cantidadColumnasSpan)
            spReporte.ActiveSheet.Cells(filaFinal, columnaConceptoTotal).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right
            spReporte.ActiveSheet.Cells(filaFinal, columnaConceptoTotal).CellType = tipoTexto
            spReporte.ActiveSheet.Cells(filaFinal, columnaConceptoTotal).Text = valorColumnaConceptoTotal.ToUpper
            spReporte.ActiveSheet.Cells(filaFinal, 0, filaFinal, numeroColumnas - 1).BackColor = Color.Gainsboro
            For columna = columnaInicial To columnaFinal - 1
                Dim contador As Double = 0
                For fila = filaInicial To filaFinal - 1
                    If Not String.IsNullOrEmpty(spReporte.ActiveSheet.Cells(fila, 0).Text) Then
                        Dim valor As String = spReporte.ActiveSheet.Cells(fila, columna).Text
                        If IsNumeric(valor) Then
                            contador += spReporte.ActiveSheet.Cells(fila, columna).Text
                        End If
                    End If
                Next
                spReporte.ActiveSheet.Cells(filaFinal, columna).Text = contador
                spReporte.ActiveSheet.Cells(filaFinal, columna).CellType = tipoDoble
            Next
        End If

    End Sub

    Private Sub GenerarReporteGraficoCircular()

        Me.Cursor = Cursors.WaitCursor
        ' Se agrega una hoja.
        spReporte.Sheets.Count += 1
        spReporte.ActiveSheetIndex = spReporte.Sheets.Count - 1
        FormatearSpreadReporteGrafico()
        spReporte.ActiveSheet.SheetName = "Reporte de Saldos Gráficos Circular"
        ' Se crea la gráfica circular.
        Dim rango As New FarPoint.Win.Chart.PieSeries()
        rango.SeriesName = "Cantidad"
        spReporte.ActiveSheetIndex = 0 ' Se toma del reporte principal.
        For columna = 0 To spReporte.ActiveSheet.Columns.Count - 1
            If (columna = spReporte.ActiveSheet.Columns("saldoActual").Index) Then
                For fila = 0 To spReporte.ActiveSheet.Rows.Count - 2 ' Se excluye uno mas por los totales.
                    rango.Values.Add(spReporte.ActiveSheet.Cells(fila, columna).Text) ' Valor del elemento de la gráfica, en este caso son artículos.
                    rango.CategoryNames.Item(fila) = spReporte.ActiveSheet.Cells(fila, spReporte.ActiveSheet.Columns("nombreArticulo").Index).Text & " (" & spReporte.ActiveSheet.Cells(fila, spReporte.ActiveSheet.Columns("saldoActual").Index).Text & ")" ' Nombre de elemento o categoría, en este caso son artículos. 
                Next
            End If
        Next
        spReporte.ActiveSheetIndex = spReporte.Sheets.Count - 1 ' Se regresa al reporte actual.
        Dim veces As Integer = Math.Ceiling(rango.Count / 50) ' Se dividen los elementos entre 50, que son los que se ven bien.
        If (veces > 2) Then
            veces = 2
        End If
        spReporte.ActiveSheet.Rows.Count = 50 * veces ' Se generan las filas por la cantidad de veces.
        Dim alto As Double = 1000 * veces
        spReporte.ActiveSheet.Columns.Count = 26 * veces ' Se generan las columnas por la cantidad de veces.
        Dim ancho As Double = 1550 * veces
        Dim plano As New FarPoint.Win.Chart.PiePlotArea()
        plano.Location = New PointF(0.0F, 0.1F)
        plano.Size = New SizeF(0.6F, 0.6F)
        plano.Series.Add(rango)
        Dim etiqueta As New FarPoint.Win.Chart.LabelArea()
        etiqueta.Text = "Saldos Actuales"
        etiqueta.Location = New PointF(0.5F, 0.02F)
        etiqueta.AlignmentX = 0.5F
        etiqueta.AlignmentY = 0.0F
        etiqueta.TextFont = New Font(Principal.tipoLetraSpread, Principal.tamañoLetraSpread + 10)
        Dim leyenda As New FarPoint.Win.Chart.LegendArea()
        leyenda.Location = New PointF(0.7F, 0.5F)
        leyenda.AlignmentX = 1.0F
        leyenda.AlignmentY = 0.5F
        Dim modelo As New FarPoint.Win.Chart.ChartModel()
        modelo.LabelAreas.Add(etiqueta)
        If (rango.Count <= 100) Then ' Se le agrega solo si son menos de 100, para que no pierdan visibilidad.
            modelo.LegendAreas.Add(leyenda)
        End If
        modelo.PlotAreas.Add(plano)
        Dim grafico As New FarPoint.Win.Spread.Chart.SpreadChart()
        grafico.Size = New Size(ancho, alto)
        grafico.Location = New Point(1, 1)
        grafico.Model = modelo
        spReporte.ActiveSheet.Charts.Add(grafico)
        ' Se regresa al reporte principal.
        spReporte.ActiveSheetIndex = 0
        Application.DoEvents()
        Me.Cursor = Cursors.Default

    End Sub

    Private Sub GenerarReporteGraficoBarras()

        Me.Cursor = Cursors.WaitCursor
        ' Se agrega una hoja.
        spReporte.Sheets.Count += 1
        spReporte.ActiveSheetIndex = spReporte.Sheets.Count - 1
        FormatearSpreadReporteGrafico()
        spReporte.ActiveSheet.SheetName = "Reporte de Saldos Gráficos Barras"
        ' Se crea la gráfica de barras.
        Dim rango2 As New FarPoint.Win.Chart.BarSeries()
        rango2.SeriesName = "Cantidad"
        spReporte.ActiveSheetIndex = 0 ' Se toma del reporte principal.
        For columna = 0 To spReporte.ActiveSheet.Columns.Count - 1
            If (columna = spReporte.ActiveSheet.Columns("saldoActual").Index) Then
                For fila = 0 To spReporte.ActiveSheet.Rows.Count - 2 ' Se excluye uno mas por los totales.
                    rango2.Values.Add(spReporte.ActiveSheet.Cells(fila, columna).Text) ' Valor del elemento de la gráfica, en este caso son artículos. 
                    Dim nombresArticulo As String() = spReporte.ActiveSheet.Cells(fila, spReporte.ActiveSheet.Columns("nombreArticulo").Index).Text.Split(" ")
                    Dim nombreArticulo As String = String.Empty
                    For indice = 0 To nombresArticulo.Length - 1
                        If (Not String.IsNullOrEmpty(nombresArticulo(indice).ToString.Trim)) Then
                            nombreArticulo &= nombresArticulo(indice) & vbNewLine
                        End If
                    Next
                    rango2.CategoryNames.Item(fila) = nombreArticulo & " (" & spReporte.ActiveSheet.Cells(fila, spReporte.ActiveSheet.Columns("saldoActual").Index).Text & ")" ' Nombre de elemento o categoría, en este caso son artículos. 
                Next
            End If
        Next
        spReporte.ActiveSheetIndex = spReporte.Sheets.Count - 1 ' Se regresa al reporte actual.
        Dim veces As Integer = Math.Ceiling(rango2.Count / 20) ' Se dividen los elementos entre 20, que son los que se ven bien.
        spReporte.ActiveSheet.Columns.Count = 26 * veces ' Se generan las columnas por la cantidad de veces.
        Dim ancho As Double = 1550 * veces
        rango2.VaryColors = True
        Dim plano2 As New FarPoint.Win.Chart.YPlotArea()
        plano2.Location = New PointF(0.05F, 0.2F)
        plano2.Size = New SizeF(0.9F, 0.6F)
        plano2.Series.Add(rango2)
        Dim etiqueta2 As New FarPoint.Win.Chart.LabelArea()
        etiqueta2.Text = "Saldos Actuales"
        etiqueta2.Location = New PointF(0.5F, 0.02F)
        etiqueta2.AlignmentX = 0.5F
        etiqueta2.AlignmentY = 0.0F
        etiqueta2.TextFont = New Font(Principal.tipoLetraSpread, Principal.tamañoLetraSpread + 10)
        'Dim leyenda2 As New FarPoint.Win.Chart.LegendArea()
        'leyenda2.Location = New PointF(0.5F, 0.92F)
        'leyenda2.AlignmentX = 0.1F
        'leyenda2.AlignmentY = 0.5F
        'leyenda2.Vertical = False 
        Dim modelo2 As New FarPoint.Win.Chart.ChartModel()
        modelo2.LabelAreas.Add(etiqueta2)
        'modelo2.LegendAreas.Add(leyenda2) ' No se le agrega, ya que tienen los nombres cada barra.
        modelo2.PlotAreas.Add(plano2)
        Dim grafico2 As New FarPoint.Win.Spread.Chart.SpreadChart()
        grafico2.Size = New Size(ancho, 1000)
        grafico2.Location = New Point(1, 1)
        grafico2.Model = modelo2
        spReporte.ActiveSheet.Charts.Add(grafico2)
        ' Se regresa al reporte principal.
        spReporte.ActiveSheetIndex = 0
        Application.DoEvents()
        Me.Cursor = Cursors.Default

    End Sub

    Private Sub FormatearSpreadReporteGrafico()

        ' Estilo blanco totalmente, nombre de hoja, etc.
        spReporte.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        spReporte.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        spReporte.ActiveSheet.GrayAreaBackColor = Color.White
        spReporte.ActiveSheet.SheetName = "Reporte de Saldos Gráficos"
        spReporte.ActiveSheet.HorizontalGridLine = New FarPoint.Win.Spread.GridLine(Nothing)
        spReporte.ActiveSheet.VerticalGridLine = New FarPoint.Win.Spread.GridLine(Nothing)
        ' Se ocultan datos. 
        spReporte.ActiveSheet.ColumnHeader.Visible = False
        spReporte.ActiveSheet.RowHeader.Visible = False
        ' Se agrega una trampa de celdas para contener los gráficos.
        spReporte.ActiveSheet.Rows.Count = 50
        spReporte.ActiveSheet.Columns.Count = 26

    End Sub

    Public Function CopiarHoja(hoja As FarPoint.Win.Spread.SheetView) As FarPoint.Win.Spread.SheetView

        Dim nuevaHoja As FarPoint.Win.Spread.SheetView = Nothing
        If Not IsNothing(hoja) Then
            nuevaHoja = FarPoint.Win.Serializer.LoadObjectXml(GetType(FarPoint.Win.Spread.SheetView), FarPoint.Win.Serializer.GetObjectXml(hoja, "CopySheet"), "CopySheet")
        End If
        nuevaHoja.SheetName = "Nueva hoja"
        Return nuevaHoja

    End Function

    Private Sub FormatearSpread()

        spReporte.Reset()
        spReporte.Visible = False
        spReporte.ActiveSheet.SheetName = "Reporte"
        spReporte.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Seashell
        spReporte.Font = New Font(Principal.tipoLetraSpread, Principal.tamañoLetraSpread, FontStyle.Regular)
        spReporte.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        spReporte.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        spReporte.ActiveSheet.Rows(-1).Height = Principal.alturaFilasSpread
        Application.DoEvents()

    End Sub

    Private Sub FormatearSpreadReporte(ByVal cantidadColumnas As Integer)

        spReporte.Visible = True
        spReporte.ActiveSheet.SheetName = "Reporte de Saldos"
        spReporte.ActiveSheet.GrayAreaBackColor = Principal.colorAreaGris
        spReporte.ActiveSheet.ColumnHeader.RowCount = 2
        spReporte.ActiveSheet.ColumnHeader.Rows(0).Height = Principal.alturaFilasEncabezadosChicosSpread
        spReporte.ActiveSheet.ColumnHeader.Rows(1).Height = Principal.alturaFilasEncabezadosMedianosSpread
        spReporte.ActiveSheet.ColumnHeader.Rows(0, spReporte.ActiveSheet.ColumnHeader.Rows.Count - 1).Font = New Font(Principal.tipoLetraSpread, Principal.tamañoLetraSpread, FontStyle.Bold)
        Dim numeracion As Integer = 0
        spReporte.ActiveSheet.Columns.Count = cantidadColumnas
        spReporte.ActiveSheet.Columns(numeracion).Tag = "idAlmacen" : numeracion += 1
        spReporte.ActiveSheet.Columns(numeracion).Tag = "nombreAlmacen" : numeracion += 1
        spReporte.ActiveSheet.Columns(numeracion).Tag = "idFamilia" : numeracion += 1
        spReporte.ActiveSheet.Columns(numeracion).Tag = "nombreFamilia" : numeracion += 1
        spReporte.ActiveSheet.Columns(numeracion).Tag = "idSubFamilia" : numeracion += 1
        spReporte.ActiveSheet.Columns(numeracion).Tag = "nombreSubFamilia" : numeracion += 1
        spReporte.ActiveSheet.Columns(numeracion).Tag = "idArticulo" : numeracion += 1
        spReporte.ActiveSheet.Columns(numeracion).Tag = "nombreArticulo" : numeracion += 1
        spReporte.ActiveSheet.Columns(numeracion).Tag = "saldoAnterior" : numeracion += 1
        spReporte.ActiveSheet.Columns(numeracion).Tag = "costoAnterior" : numeracion += 1
        spReporte.ActiveSheet.Columns(numeracion).Tag = "saldoEntradasRango" : numeracion += 1
        spReporte.ActiveSheet.Columns(numeracion).Tag = "costoEntradasRango" : numeracion += 1
        spReporte.ActiveSheet.Columns(numeracion).Tag = "saldoSalidasRango" : numeracion += 1
        spReporte.ActiveSheet.Columns(numeracion).Tag = "costoSalidasRango" : numeracion += 1
        spReporte.ActiveSheet.Columns(numeracion).Tag = "saldoActual" : numeracion += 1
        spReporte.ActiveSheet.Columns(numeracion).Tag = "costoActual" : numeracion += 1
        spReporte.ActiveSheet.Columns("idAlmacen").Width = 50
        spReporte.ActiveSheet.Columns("nombreAlmacen").Width = 170
        spReporte.ActiveSheet.Columns("idFamilia").Width = 50
        spReporte.ActiveSheet.Columns("nombreFamilia").Width = 170
        spReporte.ActiveSheet.Columns("idSubFamilia").Width = 50
        spReporte.ActiveSheet.Columns("nombreSubFamilia").Width = 170
        spReporte.ActiveSheet.Columns("idArticulo").Width = 50
        spReporte.ActiveSheet.Columns("nombreArticulo").Width = 170
        spReporte.ActiveSheet.Columns("saldoAnterior").Width = 100
        spReporte.ActiveSheet.Columns("costoAnterior").Width = 100
        spReporte.ActiveSheet.Columns("saldoEntradasRango").Width = 100
        spReporte.ActiveSheet.Columns("costoEntradasRango").Width = 100
        spReporte.ActiveSheet.Columns("saldoSalidasRango").Width = 100
        spReporte.ActiveSheet.Columns("costoSalidasRango").Width = 100
        spReporte.ActiveSheet.Columns("saldoActual").Width = 100
        spReporte.ActiveSheet.Columns("costoActual").Width = 100
        spReporte.ActiveSheet.Columns("saldoAnterior").CellType = tipoEntero
        spReporte.ActiveSheet.Columns("costoAnterior").CellType = tipoDoble
        spReporte.ActiveSheet.Columns("saldoEntradasRango").CellType = tipoEntero
        spReporte.ActiveSheet.Columns("costoEntradasRango").CellType = tipoDoble
        spReporte.ActiveSheet.Columns("saldoSalidasRango").CellType = tipoEntero
        spReporte.ActiveSheet.Columns("costoSalidasRango").CellType = tipoDoble
        spReporte.ActiveSheet.Columns("saldoActual").CellType = tipoEntero
        spReporte.ActiveSheet.Columns("costoActual").CellType = tipoDoble
        spReporte.ActiveSheet.AddColumnHeaderSpanCell(0, spReporte.ActiveSheet.Columns("idAlmacen").Index, 1, 2)
        spReporte.ActiveSheet.ColumnHeader.Cells(0, spReporte.ActiveSheet.Columns("idAlmacen").Index).Value = "Almacén".ToUpper
        spReporte.ActiveSheet.ColumnHeader.Cells(1, spReporte.ActiveSheet.Columns("idAlmacen").Index).Value = "No.".ToUpper
        spReporte.ActiveSheet.ColumnHeader.Cells(1, spReporte.ActiveSheet.Columns("nombreAlmacen").Index).Value = "Nombre".ToUpper
        spReporte.ActiveSheet.AddColumnHeaderSpanCell(0, spReporte.ActiveSheet.Columns("idFamilia").Index, 1, 2)
        spReporte.ActiveSheet.ColumnHeader.Cells(0, spReporte.ActiveSheet.Columns("idFamilia").Index).Value = "Familia".ToUpper
        spReporte.ActiveSheet.ColumnHeader.Cells(1, spReporte.ActiveSheet.Columns("idFamilia").Index).Value = "No.".ToUpper
        spReporte.ActiveSheet.ColumnHeader.Cells(1, spReporte.ActiveSheet.Columns("nombreFamilia").Index).Value = "Nombre".ToUpper
        spReporte.ActiveSheet.AddColumnHeaderSpanCell(0, spReporte.ActiveSheet.Columns("idSubFamilia").Index, 1, 2)
        spReporte.ActiveSheet.ColumnHeader.Cells(0, spReporte.ActiveSheet.Columns("idSubFamilia").Index).Value = "SubFamilia".ToUpper
        spReporte.ActiveSheet.ColumnHeader.Cells(1, spReporte.ActiveSheet.Columns("idSubFamilia").Index).Value = "No.".ToUpper
        spReporte.ActiveSheet.ColumnHeader.Cells(1, spReporte.ActiveSheet.Columns("nombreSubFamilia").Index).Value = "Nombre".ToUpper
        spReporte.ActiveSheet.AddColumnHeaderSpanCell(0, spReporte.ActiveSheet.Columns("idArticulo").Index, 1, 2)
        spReporte.ActiveSheet.ColumnHeader.Cells(0, spReporte.ActiveSheet.Columns("idArticulo").Index).Value = "Artículo".ToUpper
        spReporte.ActiveSheet.ColumnHeader.Cells(1, spReporte.ActiveSheet.Columns("idArticulo").Index).Value = "No.".ToUpper
        spReporte.ActiveSheet.ColumnHeader.Cells(1, spReporte.ActiveSheet.Columns("nombreArticulo").Index).Value = "Nombre".ToUpper
        spReporte.ActiveSheet.AddColumnHeaderSpanCell(0, spReporte.ActiveSheet.Columns("saldoAnterior").Index, 1, 2)
        spReporte.ActiveSheet.ColumnHeader.Cells(0, spReporte.ActiveSheet.Columns("saldoAnterior").Index).Value = "Saldo Anterior".ToUpper
        spReporte.ActiveSheet.ColumnHeader.Cells(1, spReporte.ActiveSheet.Columns("saldoAnterior").Index).Value = "Cantidad".ToUpper
        spReporte.ActiveSheet.ColumnHeader.Cells(1, spReporte.ActiveSheet.Columns("costoAnterior").Index).Value = "Costo".ToUpper
        spReporte.ActiveSheet.AddColumnHeaderSpanCell(0, spReporte.ActiveSheet.Columns("saldoEntradasRango").Index, 1, 2)
        spReporte.ActiveSheet.ColumnHeader.Cells(0, spReporte.ActiveSheet.Columns("saldoEntradasRango").Index).Value = "Entradas".ToUpper
        spReporte.ActiveSheet.ColumnHeader.Cells(1, spReporte.ActiveSheet.Columns("saldoEntradasRango").Index).Value = "Cantidad".ToUpper
        spReporte.ActiveSheet.ColumnHeader.Cells(1, spReporte.ActiveSheet.Columns("costoEntradasRango").Index).Value = "Costo".ToUpper
        spReporte.ActiveSheet.AddColumnHeaderSpanCell(0, spReporte.ActiveSheet.Columns("saldoSalidasRango").Index, 1, 2)
        spReporte.ActiveSheet.ColumnHeader.Cells(0, spReporte.ActiveSheet.Columns("saldoSalidasRango").Index).Value = "Salidas".ToUpper
        spReporte.ActiveSheet.ColumnHeader.Cells(1, spReporte.ActiveSheet.Columns("saldoSalidasRango").Index).Value = "Cantidad".ToUpper
        spReporte.ActiveSheet.ColumnHeader.Cells(1, spReporte.ActiveSheet.Columns("costoSalidasRango").Index).Value = "Costo".ToUpper
        spReporte.ActiveSheet.AddColumnHeaderSpanCell(0, spReporte.ActiveSheet.Columns("saldoActual").Index, 1, 2)
        spReporte.ActiveSheet.ColumnHeader.Cells(0, spReporte.ActiveSheet.Columns("saldoActual").Index).Value = "Saldo Actual".ToUpper
        spReporte.ActiveSheet.ColumnHeader.Cells(1, spReporte.ActiveSheet.Columns("saldoActual").Index).Value = "Cantidad".ToUpper
        spReporte.ActiveSheet.ColumnHeader.Cells(1, spReporte.ActiveSheet.Columns("costoActual").Index).Value = "Costo".ToUpper
        spReporte.ActiveSheet.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect
        If (Not chkFecha.Checked) Then
            spReporte.ActiveSheet.Columns(spReporte.ActiveSheet.Columns("saldoAnterior").Index, spReporte.ActiveSheet.Columns("saldoActual").Index - 1).Visible = False
        End If
        Application.DoEvents()

    End Sub

    Private Sub CargarComboAlmacenes()

        almacenes.EId = 0
        cbAlmacen.ValueMember = "Id"
        cbAlmacen.DisplayMember = "Nombre"
        cbAlmacen.DataSource = almacenes.ObtenerListadoReporte()
        cbAlmacen.Enabled = True
        cbFamilia.DataSource = Nothing
        cbFamilia.Enabled = False
        cbSubFamilia.DataSource = Nothing
        cbSubFamilia.Enabled = False
        cbArticulo.DataSource = Nothing
        cbArticulo.Enabled = False

    End Sub

    Private Sub CargarComboFamilias()

        Dim idAlmacen As Integer = cbAlmacen.SelectedValue()
        If (idAlmacen > 0) Then
            familias.EIdAlmacen = idAlmacen
            familias.EId = 0
            cbFamilia.ValueMember = "Id"
            cbFamilia.DisplayMember = "Nombre"
            cbFamilia.DataSource = familias.ObtenerListadoReporte()
            cbFamilia.Enabled = True
            cbSubFamilia.DataSource = Nothing
            cbSubFamilia.Enabled = False
            cbArticulo.DataSource = Nothing
            cbArticulo.Enabled = False
        End If

    End Sub

    Private Sub CargarComboSubFamilias()

        Dim idAlmacen As Integer = cbAlmacen.SelectedValue()
        Dim idFamilia As Integer = cbFamilia.SelectedValue()
        If (idAlmacen > 0 And idFamilia > 0) Then
            subFamilias.EIdAlmacen = idAlmacen
            subFamilias.EIdFamilia = idFamilia
            subFamilias.EId = 0
            cbSubFamilia.ValueMember = "Id"
            cbSubFamilia.DisplayMember = "Nombre"
            cbSubFamilia.DataSource = subFamilias.ObtenerListadoReporte()
            cbSubFamilia.Enabled = True
            cbArticulo.DataSource = Nothing
            cbArticulo.Enabled = False
        End If

    End Sub

    Private Sub CargarComboArticulos()

        Dim idAlmacen As Integer = cbAlmacen.SelectedValue()
        Dim idFamilia As Integer = cbFamilia.SelectedValue()
        Dim idSubFamilia As Integer = cbSubFamilia.SelectedValue()
        If (idAlmacen > 0 And idFamilia > 0 And idSubFamilia > 0) Then
            articulos.EIdAlmacen = idAlmacen
            articulos.EIdFamilia = idFamilia
            articulos.EIdSubFamilia = idSubFamilia
            articulos.EId = 0
            cbArticulo.ValueMember = "Id"
            cbArticulo.DisplayMember = "Nombre"
            cbArticulo.DataSource = articulos.ObtenerListadoReporte()
            cbArticulo.Enabled = True
        End If

    End Sub

#End Region

#End Region

#Region "Enumeraciones"

    Enum OpcionNivel

        almacen = 0
        familia = 1
        subFamilia = 2
        articulo = 3

    End Enum

#End Region

End Class