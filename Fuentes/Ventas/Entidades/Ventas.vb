Imports System.Data.SqlClient

Public Class Ventas
     
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
            comando.CommandText = String.Format("INSERT INTO Ventas (IdAlmacen, IdFamilia, IdSubFamilia, IdArticulo, Id, IdCliente, Fecha, Cantidad, Precio, SubTotal, Descuento, Total, Orden, Observaciones) VALUES (@idAlmacen, @idFamilia, @idSubFamilia, @idArticulo, @id, @idCliente, @fecha, @cantidad, @precio, @subtotal, @descuento, @total, @orden, @observaciones)")
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

    Public Function ObtenerListadoReporte() As DataTable

        Try
            Dim datos As New DataTable
            Dim comando As New SqlCommand()
            comando.Connection = BaseDatos.conexionAlmacen
            Dim condicion As String = String.Empty
            If (Me.EId > 0) Then
                condicion &= " AND V.Id=@id"
            End If
            comando.CommandText = String.Format("SELECT 'TRUE', V.IdAlmacen, A.Nombre, V.IdFamilia, F.Nombre, V.IdSubFamilia, SF.Nombre, V.IdArticulo, A2.Nombre, UM.Nombre, A2.Codigo, A2.Pagina, A2.Color, A2.Talla, A2.Modelo, A2.CodigoInternet, V.Cantidad, V.Precio, V.SubTotal, V.Descuento, V.Total " & _
            " FROM Ventas AS V " & _
            " LEFT JOIN {0}Almacenes AS A ON V.IdAlmacen = A.Id" & _
            " LEFT JOIN {0}Familias AS F ON V.IdFamilia = F.Id AND V.IdAlmacen = F.IdAlmacen" & _
            " LEFT JOIN {0}SubFamilias AS SF ON V.IdSubFamilia = SF.Id AND V.IdFamilia = SF.IdFamilia AND V.IdAlmacen = SF.IdAlmacen" & _
            " LEFT JOIN {0}Articulos AS A2 ON V.IdArticulo = A2.Id AND V.IdSubFamilia = A2.IdSubFamilia AND V.IdFamilia = A2.IdFamilia AND V.IdAlmacen = A2.IdAlmacen" & _
            " LEFT JOIN {0}UnidadesMedidas AS UM ON A2.IdUnidadMedida = UM.Id" & _
            " WHERE 0=0 {1} ORDER BY V.Orden ASC", ALMLogicaVentas.Programas.bdCatalogo & ".dbo." & ALMLogicaVentas.Programas.prefijoBaseDatosAlmacen, condicion)
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

    Public Function ObtenerListado() As DataTable

        Try
            Dim datos As New DataTable
            Dim comando As New SqlCommand()
            comando.Connection = BaseDatos.conexionAlmacen
            Dim condicion As String = String.Empty
            If (Me.EId > 0) Then
                condicion &= " AND Id=@id"
            End If
            comando.CommandText = String.Format("SELECT * FROM Ventas WHERE 0=0 {0} ORDER BY Orden ASC", condicion)
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

    Public Function ObtenerSaldos() As DataTable

        Try
            Dim datos As New DataTable
            Dim comando As New SqlCommand()
            comando.Connection = BaseDatos.conexionAlmacen
            Dim condicion As String = String.Empty
            If (Me.EIdAlmacen > 0) Then
                condicion &= " AND Saldos.IdAlmacen=@idAlmacen"
            End If
            If (Me.EIdFamilia > 0) Then
                condicion &= " AND Saldos.IdFamilia=@idFamilia"
            End If
            If (Me.EIdSubFamilia > 0) Then
                condicion &= " AND Saldos.IdSubFamilia=@idSubFamilia"
            End If
            If (Me.EIdArticulo > 0) Then
                condicion &= " AND Saldos.IdArticulo=@idArticulo"
            End If
            comando.CommandText = String.Format("SELECT * FROM " & _
            "( " & _
                " SELECT E.IdAlmacen, E.IdFamilia, E.IdSubFamilia, E.IdArticulo, SUM(ISNULL(E.Cantidad, 0)) - SUM(ISNULL(S.Cantidad, 0)) AS Cantidad " & _
                " FROM " & _
                " ( " & _
                " SELECT IdAlmacen, IdFamilia, IdSubFamilia, IdArticulo, SUM(ISNULL(Cantidad, 0)) AS Cantidad " & _
                " FROM Entradas " & _
                " GROUP BY IdAlmacen, IdFamilia, IdSubFamilia, IdArticulo " & _
                " ) AS E LEFT JOIN " & _
                " ( " & _
                " SELECT IdAlmacen, IdFamilia, IdSubFamilia, IdArticulo, SUM(ISNULL(Cantidad, 0)) AS Cantidad " & _
                " FROM Ventas " & _
                " GROUP BY IdAlmacen, IdFamilia, IdSubFamilia, IdArticulo " & _
                " ) AS S ON E.IdAlmacen = S.IdAlmacen  AND E.IdFamilia = S.IdFamilia AND E.IdSubFamilia = S.IdSubFamilia AND E.IdArticulo = S.IdArticulo " & _
            " GROUP BY E.IdAlmacen, E.IdFamilia, E.IdSubFamilia, E.IdArticulo " & _
            " ) AS Saldos " & _
            " WHERE 0=0 {0}", condicion)
            comando.Parameters.AddWithValue("@idAlmacen", Me.EIdAlmacen)
            comando.Parameters.AddWithValue("@idFamilia", Me.EIdFamilia)
            comando.Parameters.AddWithValue("@idSubFamilia", Me.EIdSubFamilia)
            comando.Parameters.AddWithValue("@idArticulo", Me.EIdArticulo)
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

    Public Function ValidarFechasPosteriores() As DataTable

        Try
            Dim datos As New DataTable
            Dim comando As New SqlCommand()
            comando.Connection = BaseDatos.conexionAlmacen
            Dim condicion As String = String.Empty
            If (Me.EIdAlmacen > 0) Then
                condicion &= " AND IdAlmacen=@idAlmacen"
            End If
            If (Me.EIdFamilia > 0) Then
                condicion &= " AND IdFamilia=@idFamilia"
            End If
            If (Me.EIdSubFamilia > 0) Then
                condicion &= " AND IdSubFamilia=@idSubFamilia"
            End If
            If (Me.EIdArticulo > 0) Then
                condicion &= " AND IdArticulo=@idArticulo"
            End If
            condicion &= " AND Fecha>@fecha"
            comando.CommandText = String.Format(" SELECT Id, Fecha FROM Ventas WHERE 0=0 {0}", condicion)
            comando.Parameters.AddWithValue("@idAlmacen", Me.EIdAlmacen)
            comando.Parameters.AddWithValue("@idFamilia", Me.EIdFamilia)
            comando.Parameters.AddWithValue("@idSubFamilia", Me.EIdSubFamilia)
            comando.Parameters.AddWithValue("@idArticulo", Me.EIdArticulo)
            comando.Parameters.AddWithValue("@fecha", ALMLogicaVentas.Funciones.ValidarFechaAEstandar(Me.EFecha))
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
