Imports System.Data.SqlClient

Public Class Devoluciones

    Private idAlmacen As Integer
    Private idFamilia As Integer
    Private idSubFamilia As Integer
    Private idArticulo As Integer
    Private id As Integer
    Private idCliente As Integer
    Private fecha As Date
    Private cantidad As Integer
    Private precio As Double
    Private subtotal As Double
    Private porcentajeDescuento As Double
    Private descuento As Double
    Private total As Double
    Private orden As Integer
    Private observaciones As String
    Private idVenta As Integer

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
    Public Property ECantidad() As Integer
        Get
            Return cantidad
        End Get
        Set(value As Integer)
            cantidad = value
        End Set
    End Property
    Public Property EPrecio() As Double
        Get
            Return precio
        End Get
        Set(value As Double)
            precio = value
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
    Public Property EPorcentajeDescuento() As Double
        Get
            Return porcentajeDescuento
        End Get
        Set(value As Double)
            porcentajeDescuento = value
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
    Public Property EIdVenta() As Integer
        Get
            Return idVenta
        End Get
        Set(value As Integer)
            idVenta = value
        End Set
    End Property

    Public Sub Guardar()

        Try
            Dim comando As New SqlCommand()
            comando.Connection = BaseDatos.conexionAlmacen
            comando.CommandText = String.Format("INSERT INTO Devoluciones (IdAlmacen, IdFamilia, IdSubFamilia, IdArticulo, Id, IdCliente, Fecha, Cantidad, Precio, SubTotal, PorcentajeDescuento, Descuento, Total, Orden, Observaciones, IdVenta) VALUES (@idAlmacen, @idFamilia, @idSubFamilia, @idArticulo, @id, @idCliente, @fecha, @cantidad, @precio, @subtotal, @porcentajeDescuento, @descuento, @total, @orden, @observaciones, @idVenta)")
            comando.Parameters.AddWithValue("@idAlmacen", Me.EIdAlmacen)
            comando.Parameters.AddWithValue("@idFamilia", Me.EIdFamilia)
            comando.Parameters.AddWithValue("@idSubFamilia", Me.EIdSubFamilia)
            comando.Parameters.AddWithValue("@idArticulo", Me.EIdArticulo)
            comando.Parameters.AddWithValue("@id", Me.EId)
            comando.Parameters.AddWithValue("@idCliente", Me.EIdCliente)
            comando.Parameters.AddWithValue("@fecha", Me.EFecha)
            comando.Parameters.AddWithValue("@cantidad", Me.ECantidad)
            comando.Parameters.AddWithValue("@precio", Me.EPrecio)
            comando.Parameters.AddWithValue("@subtotal", Me.ESubTotal)
            comando.Parameters.AddWithValue("@porcentajeDescuento", Me.EPorcentajeDescuento)
            comando.Parameters.AddWithValue("@descuento", Me.EDescuento)
            comando.Parameters.AddWithValue("@total", Me.ETotal)
            comando.Parameters.AddWithValue("@orden", Me.EOrden)
            comando.Parameters.AddWithValue("@observaciones", Me.EObservaciones)
            comando.Parameters.AddWithValue("@idVenta", Me.EIdVenta)
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
            comando.CommandText = String.Format("DELETE FROM Devoluciones WHERE 0=0 {0}", condicion)
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
            comando.CommandText = String.Format("SELECT MAX(CAST (Id AS Int)) AS IdMaximo FROM Devoluciones WHERE 0=0 {0}", condicion)
            BaseDatos.conexionAlmacen.Open()
            Dim lectorDatos As SqlDataReader = comando.ExecuteReader()
            Dim valor As Integer = 0
            While lectorDatos.Read()
                valor = ALMLogicaDevoluciones.Funciones.ValidarNumeroACero(lectorDatos("IdMaximo").ToString()) + 1
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
            comando.CommandText = String.Format("SELECT * FROM Devoluciones WHERE 0=0 {0} ORDER BY Orden ASC", condicion)
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

    Public Function ObtenerListadoDetallado() As DataTable

        Try
            Dim datos As New DataTable
            Dim comando As New SqlCommand()
            comando.Connection = BaseDatos.conexionAlmacen
            Dim condicion As String = String.Empty
            If (Me.EId > 0) Then
                condicion &= " AND D.Id=@id"
            End If
            comando.CommandText = String.Format("SELECT 'TRUE', D.IdAlmacen, A.Nombre, D.IdFamilia, F.Nombre, D.IdSubFamilia, SF.Nombre, D.IdArticulo, A2.Nombre, UM.Nombre, A2.Codigo, A2.Pagina, A2.Color, A2.Talla, A2.Modelo, A2.CodigoInternet, D.IdVenta, D.Cantidad, D.Precio, D.SubTotal, D.PorcentajeDescuento, D.Descuento, D.Total " & _
            " FROM Devoluciones AS D " & _
            " LEFT JOIN {0}Almacenes AS A ON D.IdAlmacen = A.Id" & _
            " LEFT JOIN {0}Familias AS F ON D.IdFamilia = F.Id AND D.IdAlmacen = F.IdAlmacen" & _
            " LEFT JOIN {0}SubFamilias AS SF ON D.IdSubFamilia = SF.Id AND D.IdFamilia = SF.IdFamilia AND D.IdAlmacen = SF.IdAlmacen" & _
            " LEFT JOIN {0}Articulos AS A2 ON D.IdArticulo = A2.Id AND D.IdSubFamilia = A2.IdSubFamilia AND D.IdFamilia = A2.IdFamilia AND D.IdAlmacen = A2.IdAlmacen" & _
            " LEFT JOIN {0}UnidadesMedidas AS UM ON A2.IdUnidadMedida = UM.Id" & _
            " WHERE 0=0 {1} ORDER BY D.Orden ASC", ALMLogicaDevoluciones.Programas.bdCatalogo & ".dbo." & ALMLogicaDevoluciones.Programas.prefijoBaseDatosAlmacen, condicion)
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

    Public Function ObtenerListadoParaAlmacen() As DataTable

        Try
            Dim datos As New DataTable
            Dim comando As New SqlCommand()
            comando.Connection = BaseDatos.conexionAlmacen
            Dim condicion As String = String.Empty
            If (Me.EId > 0) Then
                condicion &= " AND Id=@id"
            End If
            comando.CommandText = String.Format("SELECT IdAlmacen, Id FROM Devoluciones WHERE 0=0 {0} GROUP BY IdAlmacen, Id", condicion)
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
