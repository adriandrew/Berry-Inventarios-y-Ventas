Imports System.Data.SqlClient

Public Class CreditosDetalle

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
            comando.CommandText = String.Format("INSERT INTO CreditosDetalle (IdAlmacen, IdFamilia, IdSubFamilia, IdArticulo, Id, Cantidad, Precio, SubTotal, PorcentajeDescuento, Descuento, Total, Orden, Observaciones) VALUES (@idAlmacen, @idFamilia, @idSubFamilia, @idArticulo, @id, @cantidad, @precio, @subtotal, @porcentajeDescuento, @descuento, @total, @orden, @observaciones)")
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
            comando.CommandText = String.Format("DELETE FROM CreditosDetalle WHERE 0=0 {0}", condicion) 
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
                condicion &= " AND CD.Id=@id"
            End If
            comando.CommandText = String.Format("SELECT A.IdProveedor, P2.Nombre, CD.IdFamilia, F.Nombre, CD.IdSubFamilia, SF.Nombre, CD.IdArticulo, A.Nombre, UM.Nombre, A.Codigo, A.Pagina, A.Color, A.Talla, A.Modelo, A.CodigoInternet, CD.Cantidad, A.Precio, CD.Subtotal, CD.PorcentajeDescuento, CD.Descuento, CD.Total, CD.Observaciones " & _
            " FROM CreditosDetalle AS CD " & _
            " LEFT JOIN {0}Familias AS F ON CD.IdFamilia = F.Id AND CD.IdAlmacen = F.IdAlmacen" & _
            " LEFT JOIN {0}SubFamilias AS SF ON CD.IdSubFamilia = SF.Id AND CD.IdFamilia = SF.IdFamilia AND CD.IdAlmacen = SF.IdAlmacen" & _
            " LEFT JOIN {0}Articulos AS A ON CD.IdArticulo = A.Id AND CD.IdSubFamilia = A.IdSubFamilia AND CD.IdFamilia = A.IdFamilia AND CD.IdAlmacen = A.IdAlmacen" & _
            " LEFT JOIN {0}UnidadesMedidas AS UM ON A.IdUnidadMedida = UM.Id" & _
            " LEFT JOIN {0}Proveedores AS P2 ON A.IdProveedor = P2.Id" & _
            " WHERE 0=0 {1} ORDER BY CD.Orden ASC", ALMLogicaCreditos.Programas.bdCatalogo & ".dbo." & ALMLogicaCreditos.Programas.prefijoBaseDatosAlmacen, condicion) 
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
