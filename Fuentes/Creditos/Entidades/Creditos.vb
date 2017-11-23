Imports System.Data.SqlClient

Public Class Creditos

    Private id As Integer
    Private idCliente As Integer
    Private fecha As Date
    Private fecha2 As Date
    Private subtotal As Double
    Private descuento As Double
    Private total As Double
    Private idMetodoPago As Integer
    Private estaPagado As Boolean

    Public Property EId() As Integer
        Get
            Return id
        End Get
        Set(value As Integer)
            id = value
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
    Public Property EFecha() As Date
        Get
            Return fecha
        End Get
        Set(value As Date)
            fecha = value
        End Set
    End Property
    Public Property EFecha2() As Date
        Get
            Return fecha2
        End Get
        Set(value As Date)
            fecha2 = value
        End Set
    End Property
    Public Property ESubtotal() As Integer
        Get
            Return subtotal
        End Get
        Set(value As Integer)
            subtotal = value
        End Set
    End Property
    Public Property EDescuento() As Double
        Get
            Return descuento
        End Get
        Set(value As Double)
            descuento = value
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
    Public Property EIdMetodoPago() As Integer
        Get
            Return idMetodoPago
        End Get
        Set(value As Integer)
            idMetodoPago = value
        End Set
    End Property
    Public Property EEstaPagado() As Boolean
        Get
            Return estaPagado
        End Get
        Set(value As Boolean)
            estaPagado = value
        End Set
    End Property

    Public Sub Guardar()

        Try
            Dim comando As New SqlCommand()
            comando.Connection = BaseDatos.conexionAlmacen
            comando.CommandText = String.Format("INSERT INTO Creditos (Id, IdCliente, Fecha, Subtotal, Descuento, Total, IdMetodoPago, EstaPagado) VALUES (@id, @idCliente, @fecha, @subtotal, @descuento, @total, @idMetodoPago, @estaPagado)")
            comando.Parameters.AddWithValue("@id", Me.EId)
            comando.Parameters.AddWithValue("@idCliente", Me.EIdCliente)
            comando.Parameters.AddWithValue("@fecha", Me.EFecha) 
            comando.Parameters.AddWithValue("@subtotal", Me.ESubtotal)
            comando.Parameters.AddWithValue("@descuento", Me.EDescuento)
            comando.Parameters.AddWithValue("@total", Me.ETotal)
            comando.Parameters.AddWithValue("@idMetodoPago", Me.EIdMetodoPago)
            comando.Parameters.AddWithValue("@estaPagado", Me.EEstaPagado)
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
            If (Me.EId > 0) Then
                condicion &= " AND Id=@id"
            End If
            comando.CommandText = String.Format("DELETE FROM Creditos WHERE 0=0 {0}", condicion)
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

    Public Function ObtenerMaximoId() As Integer

        Try
            Dim comando As New SqlCommand()
            comando.Connection = BaseDatos.conexionAlmacen 
            comando.CommandText = String.Format("SELECT MAX(CAST (Id AS Int)) AS IdMaximo FROM Creditos")
            BaseDatos.conexionAlmacen.Open()
            Dim lectorDatos As SqlDataReader = comando.ExecuteReader()
            Dim valor As Integer = 0
            While lectorDatos.Read()
                valor = ALMLogicaCreditos.Funciones.ValidarNumeroACero(lectorDatos("IdMaximo").ToString()) + 1
            End While
            BaseDatos.conexionAlmacen.Close()
            Return valor
        Catch ex As Exception
            Throw ex
        Finally
            BaseDatos.conexionAlmacen.Close()
        End Try

    End Function

    Public Function ObtenerListadoGeneral() As DataTable

        Try
            Dim datos As New DataTable
            Dim comando As New SqlCommand()
            comando.Connection = BaseDatos.conexionAlmacen
            Dim condicion As String = String.Empty
            If (Me.EId > 0) Then
                condicion &= " AND Id=@id"
            End If
            comando.CommandText = String.Format("SELECT * FROM Creditos WHERE 0=0 {0}", condicion)
            comando.Parameters.AddWithValue("@id", Me.EId)
            BaseDatos.conexionAlmacen.Open()
            Dim lectorDatos As SqlDataReader
            lectorDatos = comando.ExecuteReader()
            datos.Load(lectorDatos)
            BaseDatos.conexionAlmacen.Close()
            Return datos
        Catch ex As Exception
            Throw ex
        Finally
            BaseDatos.conexionAlmacen.Close()
        End Try

    End Function

    Public Function ObtenerListadoActualizarSeleccionar(ByVal aplicaFecha As Boolean) As DataTable

        Try
            Dim datos As New DataTable
            Dim comando As New SqlCommand()
            comando.Connection = BaseDatos.conexionAlmacen
            Dim condicion As String = String.Empty
            If (Me.EIdCliente > 0) Then
                condicion &= " AND C.IdCliente=@idCliente"
            End If
            If (aplicaFecha) Then
                condicion &= " AND C.Fecha=@fechaLlegada "
            End If
            comando.CommandText = String.Format("SELECT C.Id, C.Fecha, C.IdCliente, C2.Nombre, C.IdMetodoPago, MP.Nombre, C.SubTotal, C.Descuento, C.Total, C.EstaPagado " & _
            " FROM Creditos AS C " & _
            " LEFT JOIN {0}Clientes AS C2 ON C.IdCliente = C2.Id " & _
            " LEFT JOIN {0}MetodosPagos AS MP ON C.IdMetodoPago = MP.Id " & _
            " WHERE EstaPagado='FALSE' {1}", ALMLogicaCreditos.Programas.bdCatalogo & ".dbo." & ALMLogicaCreditos.Programas.prefijoBaseDatosAlmacen, condicion)
            comando.Parameters.AddWithValue("@idCliente", Me.EIdCliente)
            If (aplicaFecha) Then
                comando.Parameters.AddWithValue("@fechaLlegada", Me.EFecha)
            End If
            BaseDatos.conexionAlmacen.Open()
            Dim lectorDatos As SqlDataReader
            lectorDatos = comando.ExecuteReader()
            datos.Load(lectorDatos)
            BaseDatos.conexionAlmacen.Close()
            Return datos
        Catch ex As Exception
            Throw ex
        Finally
            BaseDatos.conexionAlmacen.Close()
        End Try

    End Function

End Class
