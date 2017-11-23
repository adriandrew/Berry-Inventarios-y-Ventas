Imports System.Data.SqlClient

Public Class VentasDetalle

    Private idAlmacen As Integer
    Private idFamilia As Integer
    Private idSubFamilia As Integer
    Private idArticulo As Integer
    Private id As Integer
    Private cantidad As Integer
    Private precio As Double
    Private subtotal As Double
    Private porcentajeDescuento As Double
    Private descuento As Double
    Private total As Double
    Private orden As Integer
    Private observaciones As String

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

    Public Sub Guardar()

        Try
            Dim comando As New SqlCommand()
            comando.Connection = BaseDatos.conexionAlmacen
            comando.CommandText = String.Format("INSERT INTO VentasDetalle (IdAlmacen, IdFamilia, IdSubFamilia, IdArticulo, Id, Cantidad, Precio, SubTotal, PorcentajeDescuento, Descuento, Total, Orden, Observaciones) VALUES (@idAlmacen, @idFamilia, @idSubFamilia, @idArticulo, @id, @cantidad, @precio, @subtotal, @porcentajeDescuento, @descuento, @total, @orden, @observaciones)")
            comando.Parameters.AddWithValue("@idAlmacen", Me.EIdAlmacen)
            comando.Parameters.AddWithValue("@idFamilia", Me.EIdFamilia)
            comando.Parameters.AddWithValue("@idSubFamilia", Me.EIdSubFamilia)
            comando.Parameters.AddWithValue("@idArticulo", Me.EIdArticulo)
            comando.Parameters.AddWithValue("@id", Me.EId)
            comando.Parameters.AddWithValue("@cantidad", Me.ECantidad)
            comando.Parameters.AddWithValue("@precio", Me.EPrecio)
            comando.Parameters.AddWithValue("@subtotal", Me.ESubTotal)
            comando.Parameters.AddWithValue("@porcentajeDescuento", Me.EPorcentajeDescuento)
            comando.Parameters.AddWithValue("@descuento", Me.EDescuento)
            comando.Parameters.AddWithValue("@total", Me.ETotal)
            comando.Parameters.AddWithValue("@orden", Me.EOrden)
            comando.Parameters.AddWithValue("@observaciones", Me.EObservaciones)
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
            comando.CommandText = String.Format("DELETE FROM VentasDetalle WHERE 0=0 {0}", condicion)
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

    Public Function ObtenerListadoDetallado() As DataTable

        Try
            Dim datos As New DataTable
            Dim comando As New SqlCommand()
            comando.Connection = BaseDatos.conexionAlmacen
            Dim condicion As String = String.Empty
            If (Me.EId > 0) Then
                condicion &= " AND VD.Id=@id"
            End If
            comando.CommandText = String.Format("SELECT 'TRUE', VD.IdAlmacen, A.Nombre, VD.IdFamilia, F.Nombre, VD.IdSubFamilia, SF.Nombre, VD.IdArticulo, A2.Nombre, UM.Nombre, A2.Codigo, A2.Pagina, A2.Color, A2.Talla, A2.Modelo, A2.CodigoInternet, VD.Cantidad, VD.Precio, VD.SubTotal, VD.PorcentajeDescuento, VD.Descuento, VD.Total, VD.Observaciones " & _
            " FROM Ventas AS V " & _
            " LEFT JOIN VentasDetalle AS VD ON V.Id = VD.Id " & _
            " LEFT JOIN {0}Almacenes AS A ON VD.IdAlmacen = A.Id " & _
            " LEFT JOIN {0}Familias AS F ON VD.IdFamilia = F.Id AND VD.IdAlmacen = F.IdAlmacen " & _
            " LEFT JOIN {0}SubFamilias AS SF ON VD.IdSubFamilia = SF.Id AND VD.IdFamilia = SF.IdFamilia AND VD.IdAlmacen = SF.IdAlmacen " & _
            " LEFT JOIN {0}Articulos AS A2 ON VD.IdArticulo = A2.Id AND VD.IdSubFamilia = A2.IdSubFamilia AND VD.IdFamilia = A2.IdFamilia AND VD.IdAlmacen = A2.IdAlmacen " & _
            " LEFT JOIN {0}UnidadesMedidas AS UM ON A2.IdUnidadMedida = UM.Id " & _
            " WHERE 0=0 {1} ORDER BY VD.Orden ASC", ALMLogicaVentas.Programas.bdCatalogo & ".dbo." & ALMLogicaVentas.Programas.prefijoBaseDatosAlmacen, condicion)
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

    Public Function ObtenerListadoImpresionRecibos() As DataTable

        Try
            Dim datos As New DataTable
            Dim comando As New SqlCommand()
            comando.Connection = BaseDatos.conexionAlmacen
            Dim condicion As String = String.Empty
            If (Me.EId > 0) Then
                condicion &= " AND VD.Id=@id"
            End If
            comando.CommandText = String.Format("SELECT V.Id, VD.IdAlmacen, A.Nombre AS NombreAlmacen, VD.IdFamilia, F.Nombre AS NombreFamilia, VD.IdSubFamilia, SF.Nombre AS NombreSubFamilia, VD.IdArticulo, A2.Nombre AS NombreArticulo, UM.Nombre AS NombreUnidad, A2.Codigo, A2.Pagina, A2.Color, A2.Talla, A2.Modelo, A2.CodigoInternet, V.Fecha, VD.Cantidad, VD.Precio, VD.SubTotal, VD.PorcentajeDescuento, VD.Descuento, VD.Total, V.IdCliente, C.Nombre AS NombreCliente, V.IdMetodoPago, MP.Nombre AS NombreMetodoPago " & _
            " FROM Ventas AS V " & _
            " LEFT JOIN VentasDetalle AS VD ON V.Id = VD.Id " & _
            " LEFT JOIN {0}Almacenes AS A ON VD.IdAlmacen = A.Id " & _
            " LEFT JOIN {0}Familias AS F ON VD.IdFamilia = F.Id AND VD.IdAlmacen = F.IdAlmacen " & _
            " LEFT JOIN {0}SubFamilias AS SF ON VD.IdSubFamilia = SF.Id AND VD.IdFamilia = SF.IdFamilia AND VD.IdAlmacen = SF.IdAlmacen " & _
            " LEFT JOIN {0}Articulos AS A2 ON VD.IdArticulo = A2.Id AND VD.IdSubFamilia = A2.IdSubFamilia AND VD.IdFamilia = A2.IdFamilia AND VD.IdAlmacen = A2.IdAlmacen " & _
            " LEFT JOIN {0}UnidadesMedidas AS UM ON A2.IdUnidadMedida = UM.Id " & _
            " LEFT JOIN {0}Clientes AS C ON V.IdCliente = C.Id " & _
            " LEFT JOIN {0}MetodosPagos AS MP ON V.IdMetodoPago = MP.Id " & _
            " WHERE 0=0 {1} ORDER BY VD.Orden ASC", ALMLogicaVentas.Programas.bdCatalogo & ".dbo." & ALMLogicaVentas.Programas.prefijoBaseDatosAlmacen, condicion)
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
            comando.CommandText = String.Format("SELECT IdAlmacen, Id FROM VentasDetalle WHERE 0=0 {0} GROUP BY IdAlmacen, Id", condicion)
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
