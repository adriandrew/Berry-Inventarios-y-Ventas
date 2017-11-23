Imports System.Drawing.Printing.PrinterSettings

Public Class Impresoras

    ' Variables de objetos de entidades.
    Public configuracionImpresoras As New ALMEntidadesVentas.ConfiguracionImpresoras()
    ' Variables generales.
    Public nombreEstePrograma As String = String.Empty
    Public tipoImpresora As Integer = 3 ' Siempre es este valor, corresponde al area de ventas.
    Public Shared nombreImpresoraRecibo As String = String.Empty
    Public Shared habilitarImpresoraRecibo As Boolean = False
    Public Shared margenIzquierdoRecibo As Integer = 0
    Public Shared margenSuperiorRecibo As Integer = 0
    Public Shared nombreImpresoraVale As String = String.Empty
    Public Shared habilitarImpresoraVale As Boolean = False
    Public Shared margenIzquierdoVale As Integer = 0
    Public Shared margenSuperiorVale As Integer = 0

#Region "Eventos"

    Private Sub Impresion_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.Cursor = Cursors.WaitCursor
        Centrar()
        CargarNombrePrograma()
        AsignarTooltips()
        Me.Cursor = Cursors.Default

    End Sub

    Private Sub Impresion_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown

        Me.Cursor = Cursors.WaitCursor
        CargarTitulosDirectorio()
        CargarImpresoras(False)
        CargarEstilos()
        Me.Cursor = Cursors.Default

    End Sub

    Private Sub Impresion_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed

        Principal.Enabled = True

    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click

        Salir()

    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click

        Me.Cursor = Cursors.WaitCursor
        GuardarEditarImpresoras()
        Me.Cursor = Cursors.WaitCursor

    End Sub

    Private Sub btnAyuda_MouseEnter(sender As Object, e As EventArgs) Handles btnAyuda.MouseEnter

        AsignarTooltips("Ayuda")

    End Sub

    Private Sub btnGuardar_MouseEnter(sender As Object, e As EventArgs) Handles btnGuardar.MouseEnter

        AsignarTooltips("Guardar")

    End Sub

    Private Sub btnSalir_MouseEnter(sender As Object, e As EventArgs) Handles btnSalir.MouseEnter

        AsignarTooltips("Salir")

    End Sub

#End Region

#Region "Métodos"

    Private Sub CargarEstilos()

        pnlConfiguracion.BackColor = Principal.colorSpreadAreaGris
        pnlPie.BackColor = Principal.colorSpreadAreaGris

    End Sub

    Private Sub Centrar()

        Me.CenterToScreen()
        Me.Opacity = 0.98

    End Sub

    Private Sub CargarNombrePrograma()

        Me.nombreEstePrograma = Me.Text

    End Sub

    Private Sub AsignarTooltips()

        Dim tp As New ToolTip()
        tp.AutoPopDelay = 5000
        tp.InitialDelay = 0
        tp.ReshowDelay = 100
        tp.ShowAlways = True
        tp.SetToolTip(Me.btnAyuda, "Ayuda")
        tp.SetToolTip(Me.btnGuardar, "Guardar")
        tp.SetToolTip(Me.btnSalir, "Salir")

    End Sub

    Private Sub AsignarTooltips(ByVal texto As String)

        lblDescripcionTooltip.Text = texto

    End Sub

    Private Sub CargarTitulosDirectorio()

        Me.Text = "Programa:  " & Me.nombreEstePrograma & "  -  Directorio:  " & ALMLogicaVentas.Directorios.nombre & "  -  Usuario:  " & ALMLogicaVentas.Usuarios.nombre

    End Sub

    Private Sub Salir()

        Me.Close()

    End Sub

    Private Sub GuardarEditarImpresoras()

        configuracionImpresoras.EIdTipo = Me.tipoImpresora
        configuracionImpresoras.EId = 0
        configuracionImpresoras.Eliminar()
        Dim id As Integer = 0
        Dim nombre As String = String.Empty
        Dim habilitar As Boolean = False
        Dim margenIzquierdo As Double = 0
        Dim margenDerecho As Double = 0
        ' Impresora de recibo.
        id = OpcionTipoEtiqueta.recibo
        nombre = cbImpresorasRecibos.Text
        habilitar = chkImprimirRecibos.Checked
        margenIzquierdo = ALMLogicaVentas.Funciones.ValidarNumeroACero(txtMargenIzquierdoRecibos.Text)
        margenDerecho = ALMLogicaVentas.Funciones.ValidarNumeroACero(txtMargenDerechoRecibos.Text)
        configuracionImpresoras.EId = id
        configuracionImpresoras.ENombre = nombre
        configuracionImpresoras.EHabilitar = habilitar
        configuracionImpresoras.EMargenIzquierdo = margenIzquierdo
        configuracionImpresoras.EMargenSuperior = margenDerecho
        If (Not String.IsNullOrEmpty(nombre)) Then
            configuracionImpresoras.Guardar()
        End If
        ' Impresora de vale.
        id = OpcionTipoEtiqueta.vale
        nombre = cbImpresorasVales.Text
        habilitar = chkImprimirVales.Checked
        margenIzquierdo = ALMLogicaVentas.Funciones.ValidarNumeroACero(txtMargenIzquierdoVales.Text)
        margenDerecho = ALMLogicaVentas.Funciones.ValidarNumeroACero(txtMargenDerechoVales.Text)
        configuracionImpresoras.EId = id
        configuracionImpresoras.ENombre = nombre
        configuracionImpresoras.EHabilitar = habilitar
        configuracionImpresoras.EMargenIzquierdo = margenIzquierdo
        configuracionImpresoras.EMargenSuperior = margenDerecho
        If (Not String.IsNullOrEmpty(nombre)) Then
            configuracionImpresoras.Guardar()
        End If
        MessageBox.Show("Guardado finalizado.", "Finalizado.", MessageBoxButtons.OK)
        CargarImpresoras(False)

    End Sub

    Public Sub CargarImpresoras(ByVal soloVariables As Boolean)

        configuracionImpresoras.EIdTipo = Me.tipoImpresora
        ' Se carga la impresora de recibo.
        configuracionImpresoras.EId = OpcionTipoEtiqueta.recibo
        Dim datosImpresoraRecibo As New DataTable
        datosImpresoraRecibo = configuracionImpresoras.ObtenerListado()
        If (datosImpresoraRecibo.Rows.Count > 0) Then
            Impresoras.nombreImpresoraRecibo = datosImpresoraRecibo.Rows(0).Item("Nombre")
            Impresoras.habilitarImpresoraRecibo = datosImpresoraRecibo.Rows(0).Item("Habilitar")
            Impresoras.margenIzquierdoRecibo = datosImpresoraRecibo.Rows(0).Item("MargenIzquierdo")
            Impresoras.margenSuperiorRecibo = datosImpresoraRecibo.Rows(0).Item("MargenSuperior")
        End If
        If (Not soloVariables) Then
            If (datosImpresoraRecibo.Rows.Count > 0) Then
                chkImprimirRecibos.Checked = datosImpresoraRecibo.Rows(0).Item("Habilitar")
                txtMargenIzquierdoRecibos.Text = datosImpresoraRecibo.Rows(0).Item("MargenIzquierdo")
                txtMargenDerechoRecibos.Text = datosImpresoraRecibo.Rows(0).Item("MargenSuperior")
            End If
            cbImpresorasRecibos.Items.Clear()
            For indice As Integer = 0 To InstalledPrinters.Count - 1
                Dim nombre As String = InstalledPrinters.Item(indice)
                cbImpresorasRecibos.Items.Add(nombre)
                If (datosImpresoraRecibo.Rows.Count > 0) Then
                    If (datosImpresoraRecibo.Rows(0).Item("Nombre").ToString.ToUpper.Equals(nombre.ToUpper)) Then
                        cbImpresorasRecibos.SelectedIndex = indice
                    End If
                End If
            Next
        End If
        ' Se carga la impresora de vale.
        configuracionImpresoras.EId = OpcionTipoEtiqueta.vale
        Dim datosImpresoraVale As New DataTable
        datosImpresoraVale = configuracionImpresoras.ObtenerListado()
        If (datosImpresoraVale.Rows.Count > 0) Then
            Impresoras.nombreImpresoraVale = datosImpresoraVale.Rows(0).Item("Nombre")
            Impresoras.habilitarImpresoraVale = datosImpresoraVale.Rows(0).Item("Habilitar")
            Impresoras.margenIzquierdoVale = datosImpresoraVale.Rows(0).Item("MargenIzquierdo")
            Impresoras.margenSuperiorVale = datosImpresoraVale.Rows(0).Item("MargenSuperior")
        End If
        If (Not soloVariables) Then
            If (datosImpresoraVale.Rows.Count > 0) Then
                chkImprimirVales.Checked = datosImpresoraVale.Rows(0).Item("Habilitar")
                txtMargenIzquierdoVales.Text = datosImpresoraVale.Rows(0).Item("MargenIzquierdo")
                txtMargenDerechoVales.Text = datosImpresoraVale.Rows(0).Item("MargenSuperior")
            End If
            cbImpresorasVales.Items.Clear()
            For indice As Integer = 0 To InstalledPrinters.Count - 1
                Dim nombre As String = InstalledPrinters.Item(indice)
                cbImpresorasVales.Items.Add(nombre)
                If (datosImpresoraVale.Rows.Count > 0) Then
                    If (datosImpresoraVale.Rows(0).Item("Nombre").ToString.ToUpper.Equals(nombre.ToUpper)) Then
                        cbImpresorasVales.SelectedIndex = indice
                    End If
                End If
            Next
        End If

    End Sub

#End Region

#Region "Enumeraciones"

    Enum OpcionTipoEtiqueta

        recibo = 1
        vale = 2

    End Enum

#End Region

End Class