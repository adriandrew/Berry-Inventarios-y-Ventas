Imports System.Data.SqlClient

Public Class Salidas

    Private idOrigen As Integer
    Private idAlmacen As Integer
    Private idFamilia As Integer
    Private idSubFamilia As Integer
    Private idArticulo As Integer
    Private id As Integer
    Private idExterno As String
    Private idTipoSalida As Integer
    Private idCliente As Integer
    Private idMoneda As Integer
    Private tipoCambio As Double
    Private fecha As Date
    Private cantidad As Integer
    Private precioUnitario As Double
    Private total As Double
    Private totalPesos As Double
    Private orden As Integer
    Private observaciones As String
    Private factura As String
    Private chofer As String
    Private camion As String
    Private noEconomico As String
    Private idLote As Integer
    Private idCultivo As Integer

    Public Property EIdOrigen() As Integer
        Get
            Return idOrigen
        End Get
        Set(value As Integer)
            idOrigen = value
        End Set
    End Property
    Public Property EIdAlmacen() As Integer
        Get
            Return idAlmacen
        End Get
        Set(value As Integer)
            idAlmacen = value
        End Set
    End Property
    Public Property EIdFamilia() As Integer
        Get
            Return idFamilia
        End Get
        Set(value As Integer)
            idFamilia = value
        End Set
    End Property
    Public Property EIdSubFamilia() As Integer
        Get
            Return idSubFamilia
        End Get
        Set(value As Integer)
            idSubFamilia = value
        End Set
    End Property
    Public Property EIdArticulo() As Integer
        Get
            Return idArticulo
        End Get
        Set(value As Integer)
            idArticulo = value
        End Set
    End Property
    Public Property EId() As Integer
        Get
            Return id
        End Get
        Set(value As Integer)
            id = value
        End Set
    End Property
    Public Property EIdExterno() As String
        Get
            Return idExterno
        End Get
        Set(value As String)
            idExterno = value
        End Set
    End Property
    Public Property EIdTipoSalida() As Integer
        Get
            Return idTipoSalida
        End Get
        Set(value As Integer)
            idTipoSalida = value
        End Set
    End Property
    Public Property EIdCliente() As Integer
        Get
            Return idCliente
        End Get
        Set(value As Integer)
            idCliente = value
        End Set
    End Property
    Public Property EIdMoneda() As Integer
        Get
            Return idMoneda
        End Get
        Set(value As Integer)
            idMoneda = value
        End Set
    End Property
    Public Property ETipoCambio() As Double
        Get
            Return tipoCambio
        End Get
        Set(value As Double)
            tipoCambio = value
        End Set
    End Property
    Public Property EFecha() As Date
        Get
            Return fecha
        End Get
        Set(value As Date)
            fecha = value
        End Set
    End Property
    Public Property ECantidad() As Integer
        Get
            Return cantidad
        End Get
        Set(value As Integer)
            cantidad = value
        End Set
    End Property
    Public Property EPrecioUnitario() As Double
        Get
            Return precioUnitario
        End Get
        Set(value As Double)
            precioUnitario = value
        End Set
    End Property
    Public Property ETotal() As Double
        Get
            Return total
        End Get
        Set(value As Double)
            total = value
        End Set
    End Property
    Public Property ETotalPesos() As Double
        Get
            Return totalPesos
        End Get
        Set(value As Double)
            totalPesos = value
        End Set
    End Property
    Public Property EOrden() As Integer
        Get
            Return orden
        End Get
        Set(value As Integer)
            orden = value
        End Set
    End Property
    Public Property EObservaciones() As String
        Get
            Return observaciones
        End Get
        Set(value As String)
            observaciones = value
        End Set
    End Property
    Public Property EFactura() As String
        Get
            Return factura
        End Get
        Set(value As String)
            factura = value
        End Set
    End Property
    Public Property EChofer() As String
        Get
            Return chofer
        End Get
        Set(value As String)
            chofer = value
        End Set
    End Property
    Public Property ECamion() As String
        Get
            Return camion
        End Get
        Set(value As String)
            camion = value
        End Set
    End Property
    Public Property ENoEconomico() As String
        Get
            Return noEconomico
        End Get
        Set(value As String)
            noEconomico = value
        End Set
    End Property
    Public Property EIdLote() As Integer
        Get
            Return idLote
        End Get
        Set(value As Integer)
            idLote = value
        End Set
    End Property
    Public Property EIdCultivo() As Integer
        Get
            Return idCultivo
        End Get
        Set(value As Integer)
            idCultivo = value
        End Set
    End Property

    Public Sub Guardar()

        Try
            Dim comando As New SqlCommand()
            comando.Connection = BaseDatos.conexionAlmacen
            comando.CommandText = String.Format("INSERT INTO Salidas (IdOrigen, IdAlmacen, IdFamilia, IdSubFamilia, IdArticulo, Id, IdExterno, IdTipoSalida, IdCliente, IdMoneda, TipoCambio, Fecha, Cantidad, PrecioUnitario, Total, TotalPesos, Orden, Observaciones, Factura, Chofer, Camion, NoEconomico, IdLote, IdCultivo) VALUES (@idOrigen, @idAlmacen, @idFamilia, @idSubFamilia, @idArticulo, @id, @idExterno, @idTipoSalida, @idCliente, @idMoneda, @tipoCambio, @fecha, @cantidad, @precioUnitario, @total, @totalPesos, @orden, @observaciones, @factura, @chofer, @camion, @noEconomico, @idLote, @idCultivo)")
            comando.Parameters.AddWithValue("@idOrigen", Me.EIdOrigen)
            comando.Parameters.AddWithValue("@idAlmacen", Me.EIdAlmacen)
            comando.Parameters.AddWithValue("@idFamilia", Me.EIdFamilia)
            comando.Parameters.AddWithValue("@idSubFamilia", Me.EIdSubFamilia)
            comando.Parameters.AddWithValue("@idArticulo", Me.EIdArticulo)
            comando.Parameters.AddWithValue("@id", Me.EId)
            comando.Parameters.AddWithValue("@idExterno", Me.EIdExterno)
            comando.Parameters.AddWithValue("@idTipoSalida", Me.EIdTipoSalida)
            comando.Parameters.AddWithValue("@idCliente", Me.EIdCliente)
            comando.Parameters.AddWithValue("@idMoneda", Me.EIdMoneda)
            comando.Parameters.AddWithValue("@tipoCambio", Me.ETipoCambio)
            comando.Parameters.AddWithValue("@fecha", Me.EFecha)
            comando.Parameters.AddWithValue("@cantidad", Me.ECantidad)
            comando.Parameters.AddWithValue("@precioUnitario", Me.EPrecioUnitario)
            comando.Parameters.AddWithValue("@total", Me.ETotal)
            comando.Parameters.AddWithValue("@totalPesos", Me.ETotalPesos)
            comando.Parameters.AddWithValue("@orden", Me.EOrden)
            comando.Parameters.AddWithValue("@observaciones", Me.EObservaciones)
            comando.Parameters.AddWithValue("@factura", Me.EFactura)
            comando.Parameters.AddWithValue("@chofer", Me.EChofer)
            comando.Parameters.AddWithValue("@camion", Me.ECamion)
            comando.Parameters.AddWithValue("@noEconomico", Me.ENoEconomico)
            comando.Parameters.AddWithValue("@idLote", Me.EIdLote)
            comando.Parameters.AddWithValue("@idCultivo", Me.EIdCultivo)
            BaseDatos.conexionAlmacen.Open()
            comando.ExecuteNonQuery()
            BaseDatos.conexionAlmacen.Close()
        Catch ex As Exception
            Throw ex
        Finally
            BaseDatos.conexionAlmacen.Close()
        End Try

    End Sub

    Public Sub Eliminar()

        Try
            Dim comando As New SqlCommand()
            comando.Connection = BaseDatos.conexionAlmacen
            Dim condicion As String = String.Empty
            If (Me.EIdOrigen > 0) Then
                condicion &= " AND IdOrigen=@idOrigen"
            End If
            If (Me.EIdAlmacen > 0) Then
                condicion &= " AND IdAlmacen=@idAlmacen"
            End If
            If (Me.EId > 0) Then
                condicion &= " AND Id=@id"
            End If
            comando.CommandText = String.Format("DELETE FROM Salidas WHERE 0=0 {0}", condicion)
            comando.Parameters.AddWithValue("@idOrigen", Me.EIdOrigen)
            comando.Parameters.AddWithValue("@idAlmacen", Me.EIdAlmacen)
            comando.Parameters.AddWithValue("@id", Me.EId)
            BaseDatos.conexionAlmacen.Open()
            comando.ExecuteNonQuery()
            BaseDatos.conexionAlmacen.Close()
        Catch ex As Exception
            Throw ex
        Finally
            BaseDatos.conexionAlmacen.Close()
        End Try

    End Sub
     
End Class
