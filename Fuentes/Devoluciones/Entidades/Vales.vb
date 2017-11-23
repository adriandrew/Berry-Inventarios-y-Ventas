Imports System.Data.SqlClient

Public Class Vales

    Private idOrigen As Integer
    Private id As Integer
    Private idCliente As Integer
    Private fecha As Date
    Private total As Double
    Private fechaVencimiento As Date
    Private estaUtilizado As Boolean
    Private idVenta As Integer

    Public Property EIdOrigen() As Integer
        Get
            Return idOrigen
        End Get
        Set(value As Integer)
            idOrigen = value
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
    Public Property ETotal() As Double
        Get
            Return total
        End Get
        Set(value As Double)
            total = value
        End Set
    End Property
    Public Property EFechaVencimiento() As Date
        Get
            Return fechaVencimiento
        End Get
        Set(value As Date)
            fechaVencimiento = value
        End Set
    End Property
    Public Property EEstaUtilizado() As Boolean
        Get
            Return estaUtilizado
        End Get
        Set(value As Boolean)
            estaUtilizado = value
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
            comando.CommandText = String.Format("INSERT INTO Vales (IdOrigen, Id, IdCliente, Fecha, Total, FechaVencimiento, EstaUtilizado, IdVenta) VALUES (@idOrigen, @id, @idCliente, @fecha, @total, @fechaVencimiento, @estaUtilizado, @idVenta)")
            comando.Parameters.AddWithValue("@idOrigen", Me.EIdOrigen)
            comando.Parameters.AddWithValue("@id", Me.EId)
            comando.Parameters.AddWithValue("@idCliente", Me.EIdCliente)
            comando.Parameters.AddWithValue("@fecha", Me.EFecha)
            comando.Parameters.AddWithValue("@total", Me.ETotal)
            comando.Parameters.AddWithValue("@fechaVencimiento", Me.EFechaVencimiento)
            comando.Parameters.AddWithValue("@estaUtilizado", Me.EEstaUtilizado)
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
            If (Me.EIdOrigen > 0) Then
                condicion &= " AND IdOrigen=@idOrigen"
            End If
            If (Me.EId > 0) Then
                condicion &= " AND Id=@id"
            End If
            comando.CommandText = String.Format("DELETE FROM Vales WHERE 0=0 {0}", condicion)
            comando.Parameters.AddWithValue("@idOrigen", Me.EIdOrigen)
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
     
    Public Function ObtenerListadoImpresionVales() As DataTable

        Try
            Dim datos As New DataTable
            Dim comando As New SqlCommand()
            comando.Connection = BaseDatos.conexionAlmacen
            Dim condicion As String = String.Empty
            If (Me.EIdOrigen > 0) Then
                condicion &= " AND V.IdOrigen=@idOrigen"
            End If
            If (Me.EId > 0) Then
                condicion &= " AND V.Id=@id"
            End If
            comando.CommandText = String.Format("SELECT V.IdOrigen, V.Id, VD.IdAlmacen, A.Nombre AS NombreAlmacen, VD.IdFamilia, F.Nombre AS NombreFamilia, VD.IdSubFamilia, SF.Nombre AS NombreSubFamilia, VD.IdArticulo, A2.Nombre AS NombreArticulo, UM.Nombre AS NombreUnidad, A2.Codigo, A2.Pagina, A2.Color, A2.Talla, A2.Modelo, A2.CodigoInternet, V.IdCliente, C.Nombre AS NombreCliente, V.Fecha, VD.Cantidad, VD.Precio, VD.Total, V.FechaVencimiento " & _
            " FROM Vales AS V " & _
            " LEFT JOIN ValesDetalle AS VD ON V.IdOrigen = VD.IdOrigen AND V.Id = VD.Id " & _
            " LEFT JOIN {0}Almacenes AS A ON VD.IdAlmacen = A.Id " & _
            " LEFT JOIN {0}Familias AS F ON VD.IdFamilia = F.Id AND VD.IdAlmacen = F.IdAlmacen " & _
            " LEFT JOIN {0}SubFamilias AS SF ON VD.IdSubFamilia = SF.Id AND VD.IdFamilia = SF.IdFamilia AND VD.IdAlmacen = SF.IdAlmacen " & _
            " LEFT JOIN {0}Articulos AS A2 ON VD.IdArticulo = A2.Id AND VD.IdSubFamilia = A2.IdSubFamilia AND VD.IdFamilia = A2.IdFamilia AND VD.IdAlmacen = A2.IdAlmacen " & _
            " LEFT JOIN {0}UnidadesMedidas AS UM ON A2.IdUnidadMedida = UM.Id " & _
            " LEFT JOIN {0}Clientes AS C ON V.IdCliente = C.Id " & _
            " WHERE 0=0 {1} ORDER BY VD.Orden ASC", ALMLogicaDevoluciones.Programas.bdCatalogo & ".dbo." & ALMLogicaDevoluciones.Programas.prefijoBaseDatosAlmacen, condicion)
            comando.Parameters.AddWithValue("@idOrigen", Me.EIdOrigen)
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
