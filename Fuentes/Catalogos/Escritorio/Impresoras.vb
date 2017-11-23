Imports System.Drawing.Printing.PrinterSettings

Public Class Impresoras

    ' Variables de objetos de entidades.
    Public configuracionImpresoras As New ALMEntidadesCatalogos.ConfiguracionImpresoras()
    ' Variables generales.
    Public nombreEstePrograma As String = String.Empty
    Public tipoImpresora As Integer = 1 ' Siempre es este valor, corresponde al area de almacenes.
    Public Shared nombreImpresoraArticulo As String = String.Empty
    Public Shared habilitarImpresoraArticulo As Boolean = False
    Public Shared margenIzquierdoArticulo As Integer = 0
    Public Shared margenSuperiorArticulo As Integer = 0

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
        tp.SetToolTip(Me.btnAyuda, "Ayuda.")
        tp.SetToolTip(Me.btnGuardar, "Guardar.")
        tp.SetToolTip(Me.btnSalir, "Salir.")

    End Sub

    Private Sub CargarTitulosDirectorio()

        Me.Text = "Programa:  " & Me.nombreEstePrograma & "  -  Directorio:  " & ALMLogicaCatalogos.Directorios.nombre & "  -  Usuario:  " & ALMLogicaCatalogos.Usuarios.nombre

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
        ' Impresora de vale.
        id = OpcionTipoEtiqueta.vale
        nombre = cbImpresorasVales.Text
        habilitar = chkImprimirVales.Checked
        margenIzquierdo = ALMLogicaCatalogos.Funciones.ValidarNumeroACero(txtMargenIzquierdoVales.Text)
        margenDerecho = ALMLogicaCatalogos.Funciones.ValidarNumeroACero(txtMargenDerechoVales.Text)
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
        ' Se carga la impresora de vale.
        configuracionImpresoras.EId = OpcionTipoEtiqueta.vale
        Dim datosImpresoraVales As New DataTable
        datosImpresoraVales = configuracionImpresoras.ObtenerListado()
        If (datosImpresoraVales.Rows.Count > 0) Then
            Impresoras.nombreImpresoraArticulo = datosImpresoraVales.Rows(0).Item("Nombre")
            Impresoras.habilitarImpresoraArticulo = datosImpresoraVales.Rows(0).Item("Habilitar")
            Impresoras.margenIzquierdoArticulo = datosImpresoraVales.Rows(0).Item("MargenIzquierdo")
            Impresoras.margenSuperiorArticulo = datosImpresoraVales.Rows(0).Item("MargenSuperior")
        End If
        If (Not soloVariables) Then
            If (datosImpresoraVales.Rows.Count > 0) Then
                chkImprimirVales.Checked = datosImpresoraVales.Rows(0).Item("Habilitar")
                txtMargenIzquierdoVales.Text = datosImpresoraVales.Rows(0).Item("MargenIzquierdo")
                txtMargenDerechoVales.Text = datosImpresoraVales.Rows(0).Item("MargenSuperior")
            End If
            cbImpresorasVales.Items.Clear()
            For indice As Integer = 0 To InstalledPrinters.Count - 1
                Dim nombre As String = InstalledPrinters.Item(indice)
                cbImpresorasVales.Items.Add(nombre)
                If (datosImpresoraVales.Rows.Count > 0) Then
                    If (datosImpresoraVales.Rows(0).Item("Nombre").ToString.ToUpper.Equals(nombre.ToUpper)) Then
                        cbImpresorasVales.SelectedIndex = indice
                    End If
                End If
            Next
        End If

    End Sub

#End Region

#Region "Enumeraciones"

    Enum OpcionTipoEtiqueta

        vale = 1

    End Enum

#End Region

End Class