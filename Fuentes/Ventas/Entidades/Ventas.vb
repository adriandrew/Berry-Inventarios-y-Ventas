Imports System.Data.SqlClient

Public Class Ventas
      
    Private id As Integer
    Private idCliente As Integer
    Private fecha As Date
    Private subtotal As Double
    Private descuento As Double
    Private total As Double
    Private idMetodoPago As Integer
    Private importePagado As Double
    Private importeCambio As Double
     
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
    Public Property ESubTotal() As Double
        Get
            Return subtotal
        End Get
        Set(value As Double)
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
    Public Property EImportePagado() As Double
        Get
            Return importePagado
        End Get
        Set(value As Double)
            importePagado = value
        End Set
    End Property
    Public Property EImporteCambio() As Double
        Get
            Return importeCambio
        End Get
        Set(value As Double)
            importeCambio = value
        End Set
    End Property

    Public Sub Guardar()

        Try
            Dim comando As New SqlCommand()
            comando.Connection = BaseDatos.conexionAlmacen
            comando.CommandText = String.Format("INSERT INTO Ventas (Id, IdCliente, Fecha, SubTotal, Descuento, Total, IdMetodoPago, ImportePagado, ImporteCambio) VALUES (@id, @idCliente, @fecha, @subtotal, @descuento, @total, @idMetodoPago, @importePagado, @importeCambio)")
            comando.Parameters.AddWithValue("@id", Me.EId)
            comando.Parameters.AddWithValue("@idCliente", Me.EIdCliente)
            comando.Parameters.AddWithValue("@fecha", Me.EFecha)
            comando.Parameters.AddWithValue("@subtotal", Me.ESubTotal)
            comando.Parameters.AddWithValue("@descuento", Me.EDescuento)
            comando.Parameters.AddWithValue("@total", Me.ETotal)
            comando.Parameters.AddWithValue("@idMetodoPago", Me.EIdMetodoPago)
            comando.Parameters.AddWithValue("@importePagado", Me.EImportePagado)
            comando.Parameters.AddWithValue("@importeCambio", Me.EImporteCambio)
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
            comando.CommandText = String.Format("DELETE FROM Ventas WHERE 0=0 {0}", condicion)
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
            Dim condicion As String = String.Empty
            comando.CommandText = String.Format("SELECT MAX(CAST (Id AS Int)) AS IdMaximo FROM Ventas WHERE 0=0 {0}", condicion)
            BaseDatos.conexionAlmacen.Open()
            Dim lectorDatos As SqlDataReader = comando.ExecuteReader()
            Dim valor As Integer = 0
            While lectorDatos.Read()
                valor = ALMLogicaVentas.Funciones.ValidarNumeroACero(lectorDatos("IdMaximo").ToString()) + 1
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
            comando.CommandText = String.Format("SELECT * FROM Ventas WHERE 0=0 {0}", condicion)
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
       
End Class
