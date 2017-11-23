Imports System.Data.SqlClient

Public Class Pedidos

    Private idAlmacen As Integer
    Private idFamilia As Integer
    Private idSubFamilia As Integer
    Private idArticulo As Integer
    Private id As Integer 
    Private idCliente As Integer
    Private fechaEnvio As Date
    Private cantidad As Integer
    Private precio As Double
    Private total As Double
    Private orden As Integer
    Private observaciones As String
    Private fechaEnvio2 As Date
    Private estaActualizado As Boolean
    Private estatusConfirmado As Boolean
    Private estatusCancelado As Boolean
    Private estatusRecibido As Boolean
    Private estatusEntregado As Boolean
    Private fechaLlegada As Date
    Private observaciones2 As String
    Private estaIntegradoAlmacen As Boolean

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
    Public Property EFechaEnvio() As Date
        Get
            Return fechaEnvio
        End Get
        Set(value As Date)
            fechaEnvio = value
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
    Public Property EFechaEnvio2() As Date
        Get
            Return fechaEnvio2
        End Get
        Set(value As Date)
            fechaEnvio2 = value
        End Set
    End Property
    Public Property EEstaActualizado() As Boolean
        Get
            Return estaActualizado
        End Get
        Set(value As Boolean)
            estaActualizado = value
        End Set
    End Property
    Public Property EEstatusConfirmado() As Boolean
        Get
            Return estatusConfirmado
        End Get
        Set(value As Boolean)
            estatusConfirmado = value
        End Set
    End Property
    Public Property EEstatusCancelado() As Boolean
        Get
            Return estatusCancelado
        End Get
        Set(value As Boolean)
            estatusCancelado = value
        End Set
    End Property
    Public Property EEstatusRecibido() As Boolean
        Get
            Return estatusRecibido
        End Get
        Set(value As Boolean)
            estatusRecibido = value
        End Set
    End Property
    Public Property EEstatusEntregado() As Boolean
        Get
            Return estatusEntregado
        End Get
        Set(value As Boolean)
            estatusEntregado = value
        End Set
    End Property
    Public Property EFechaLlegada() As Date
        Get
            Return fechaLlegada
        End Get
        Set(value As Date)
            fechaLlegada = value
        End Set
    End Property
    Public Property EObservaciones2() As String
        Get
            Return observaciones2
        End Get
        Set(value As String)
            observaciones2 = value
        End Set
    End Property
    Public Property EEstaIntegradoAlmacen() As Boolean
        Get
            Return estaIntegradoAlmacen
        End Get
        Set(value As Boolean)
            estaIntegradoAlmacen = value
        End Set
    End Property

    Public Sub Guardar()

        Try
            Dim comando As New SqlCommand()
            comando.Connection = BaseDatos.conexionAlmacen
            comando.CommandText = String.Format("INSERT INTO Pedidos (IdAlmacen, IdFamilia, IdSubFamilia, IdArticulo, Id, IdCliente, FechaEnvio, Cantidad, Precio, Total, Orden, Observaciones, EstaActualizado, EstaIntegradoAlmacen) VALUES (@idAlmacen, @idFamilia, @idSubFamilia, @idArticulo, @id, @idCliente, @fechaEnvio, @cantidad, @precio, @total, @orden, @observaciones, @estaActualizado, @estaIntegradoAlmacen)")
            comando.Parameters.AddWithValue("@idAlmacen", Me.EIdAlmacen)
            comando.Parameters.AddWithValue("@idFamilia", Me.EIdFamilia)
            comando.Parameters.AddWithValue("@idSubFamilia", Me.EIdSubFamilia)
            comando.Parameters.AddWithValue("@idArticulo", Me.EIdArticulo)
            comando.Parameters.AddWithValue("@id", Me.EId)
            comando.Parameters.AddWithValue("@idCliente", Me.EIdCliente)
            comando.Parameters.AddWithValue("@fechaEnvio", Me.EFechaEnvio)
            comando.Parameters.AddWithValue("@cantidad", Me.ECantidad)
            comando.Parameters.AddWithValue("@precio", Me.EPrecio)
            comando.Parameters.AddWithValue("@total", Me.ETotal)
            comando.Parameters.AddWithValue("@orden", Me.EOrden)
            comando.Parameters.AddWithValue("@observaciones", Me.EObservaciones)
            comando.Parameters.AddWithValue("@estaActualizado", Me.EEstaActualizado)
            comando.Parameters.AddWithValue("@estaIntegradoAlmacen", Me.EEstaIntegradoAlmacen)
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
            If (Me.EIdAlmacen > 0) Then
                condicion &= " AND IdAlmacen=@idAlmacen"
            End If
            If (Me.EId > 0) Then
                condicion &= " AND Id=@id"
            End If
            comando.CommandText = String.Format("DELETE FROM Pedidos WHERE 0=0 {0}", condicion)
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

    Public Function ObtenerMaximoId() As Integer

        Try
            Dim comando As New SqlCommand()
            comando.Connection = BaseDatos.conexionAlmacen
            Dim condicion As String = String.Empty
            If (Me.EIdAlmacen > 0) Then
                condicion &= " WHERE IdAlmacen=@idAlmacen"
            End If
            comando.CommandText = String.Format("SELECT MAX(CAST (Id AS Int)) AS IdMaximo FROM Pedidos {0}", condicion)
            comando.Parameters.AddWithValue("@idAlmacen", Me.EIdAlmacen)
            BaseDatos.conexionAlmacen.Open()
            Dim lectorDatos As SqlDataReader = comando.ExecuteReader()
            Dim valor As Integer = 0
            While lectorDatos.Read()
                valor = ALMLogicaPedidos.Funciones.ValidarNumeroACero(lectorDatos("IdMaximo").ToString()) + 1
            End While
            BaseDatos.conexionAlmacen.Close()
            Return valor
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
            If (Me.EIdAlmacen > 0) Then
                condicion &= " AND P.IdAlmacen=@idAlmacen"
            End If
            If (Me.EId > 0) Then
                condicion &= " AND P.Id=@id"
            End If
            comando.CommandText = String.Format("SELECT A.IdProveedor, P2.Nombre, P.IdFamilia, F.Nombre, P.IdSubFamilia, SF.Nombre, P.IdArticulo, A.Nombre, UM.Nombre, A.Codigo, A.Pagina, A.Color, A.Talla, A.Modelo, A.CodigoInternet, A.Precio, P.Cantidad, P.Total, P.Observaciones " & _
            " FROM Pedidos AS P " & _
            " LEFT JOIN {0}Familias AS F ON P.IdFamilia = F.Id AND P.IdAlmacen = F.IdAlmacen" & _
            " LEFT JOIN {0}SubFamilias AS SF ON P.IdSubFamilia = SF.Id AND P.IdFamilia = SF.IdFamilia AND P.IdAlmacen = SF.IdAlmacen" & _
            " LEFT JOIN {0}Articulos AS A ON P.IdArticulo = A.Id AND P.IdSubFamilia = A.IdSubFamilia AND P.IdFamilia = A.IdFamilia AND P.IdAlmacen = A.IdAlmacen" & _
            " LEFT JOIN {0}UnidadesMedidas AS UM ON A.IdUnidadMedida = UM.Id" & _
            " LEFT JOIN {0}Proveedores AS P2 ON A.IdProveedor = P2.Id" & _
            " WHERE 0=0 {1} ORDER BY P.Orden ASC", ALMLogicaPedidos.Programas.bdCatalogo & ".dbo." & ALMLogicaPedidos.Programas.prefijoBaseDatosAlmacen, condicion)
            comando.Parameters.AddWithValue("@idAlmacen", Me.EIdAlmacen)
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

    Public Function ObtenerListadoGeneral() As List(Of Pedidos)

        Try
            Dim lista As New List(Of Pedidos)
            Dim comando As New SqlCommand()
            comando.Connection = BaseDatos.conexionAlmacen
            Dim condicion As String = String.Empty
            If (Me.EIdAlmacen > 0) Then
                condicion &= " AND IdAlmacen=@idAlmacen"
            End If
            If (Me.EId > 0) Then
                condicion &= " AND Id=@id"
            End If
            comando.CommandText = String.Format("SELECT * FROM Pedidos WHERE 0=0 {0} ORDER BY Orden ASC", condicion)
            comando.Parameters.AddWithValue("@idAlmacen", Me.EIdAlmacen)
            comando.Parameters.AddWithValue("@id", Me.EId)
            BaseDatos.conexionAlmacen.Open()
            Dim lectorDatos As SqlDataReader = comando.ExecuteReader()
            Dim tabla As Pedidos
            While lectorDatos.Read()
                tabla = New Pedidos()
                tabla.idAlmacen = Convert.ToInt32(lectorDatos("IdAlmacen").ToString())
                tabla.idFamilia = Convert.ToInt32(lectorDatos("IdFamilia").ToString())
                tabla.idSubFamilia = Convert.ToInt32(lectorDatos("IdSubFamilia").ToString())
                tabla.idArticulo = Convert.ToInt32(lectorDatos("IdArticulo").ToString())
                tabla.id = Convert.ToInt32(lectorDatos("Id").ToString())
                tabla.idCliente = lectorDatos("IdCliente").ToString()
                tabla.fechaEnvio = Convert.ToDateTime(lectorDatos("FechaEnvio").ToString())
                tabla.cantidad = Convert.ToInt32(lectorDatos("Cantidad").ToString())
                tabla.precio = Convert.ToDouble(lectorDatos("Precio").ToString())
                tabla.total = Convert.ToDouble(lectorDatos("Total").ToString())
                tabla.orden = Convert.ToInt32(lectorDatos("Orden").ToString())
                tabla.observaciones = lectorDatos("Observaciones").ToString()
                lista.Add(tabla)
            End While
            BaseDatos.conexionAlmacen.Close()
            Return lista
        Catch ex As Exception
            Throw ex
        Finally
            BaseDatos.conexionAlmacen.Close()
        End Try

    End Function

    Public Sub Actualizar(ByVal aplicaFecha As Boolean)

        Try
            Dim comando As New SqlCommand()
            comando.Connection = BaseDatos.conexionAlmacen
            Dim condicion As String = String.Empty
            If (aplicaFecha) Then
                condicion &= " FechaLlegada=@fechaLlegada, "
            End If
            comando.CommandText = String.Format("UPDATE Pedidos " & _
            " SET EstaActualizado=@estaActualizado, EstatusConfirmado=@estatusConfirmado, EstatusCancelado=@estatusCancelado, EstatusRecibido=@estatusRecibido, EstatusEntregado=@estatusEntregado, {0} Observaciones2=@observaciones2, EstaIntegradoAlmacen=@estaIntegradoAlmacen " & _
            " WHERE IdAlmacen=@idAlmacen AND Id=@id AND IdFamilia=@idFamilia AND IdSubFamilia=@idSubFamilia AND IdArticulo=@idArticulo", condicion)
            comando.Parameters.AddWithValue("@idAlmacen", Me.EIdAlmacen)
            comando.Parameters.AddWithValue("@idFamilia", Me.EIdFamilia)
            comando.Parameters.AddWithValue("@idSubFamilia", Me.EIdSubFamilia)
            comando.Parameters.AddWithValue("@idArticulo", Me.EIdArticulo)
            comando.Parameters.AddWithValue("@id", Me.EId)
            comando.Parameters.AddWithValue("@estaActualizado", Me.EEstaActualizado)
            comando.Parameters.AddWithValue("@estatusConfirmado", Me.EEstatusConfirmado)
            comando.Parameters.AddWithValue("@estatusCancelado", Me.EEstatusCancelado)
            comando.Parameters.AddWithValue("@estatusRecibido", Me.EEstatusRecibido)
            comando.Parameters.AddWithValue("@estatusEntregado", Me.EEstatusEntregado)
            If (aplicaFecha) Then
                comando.Parameters.AddWithValue("@fechaLlegada", Me.EFechaLlegada)
            End If
            comando.Parameters.AddWithValue("@observaciones2", Me.EObservaciones2)
            comando.Parameters.AddWithValue("@estaIntegradoAlmacen", Me.EEstaIntegradoAlmacen)
            BaseDatos.conexionAlmacen.Open()
            comando.ExecuteNonQuery()
            BaseDatos.conexionAlmacen.Close()
        Catch ex As Exception
            Throw ex
        Finally
            BaseDatos.conexionAlmacen.Close()
        End Try

    End Sub

    Public Function ObtenerListadoReporteActualizar(ByVal aplicaFecha As Boolean) As DataTable

        Try
            Dim datos As New DataTable
            Dim comando As New SqlCommand()
            comando.Connection = BaseDatos.conexionAlmacen
            Dim condicion As String = String.Empty
            Dim condicionFechaRango As String = String.Empty
            If (Me.EIdAlmacen > 0) Then
                condicion &= " AND P.IdAlmacen=@idAlmacen"
            End If
            If (Me.EId > 0) Then
                condicion &= " AND P.Id=@id"
            End If
            If (aplicaFecha) Then
                condicionFechaRango &= " AND FechaEnvio BETWEEN @fechaEnvio AND @fechaEnvio2 "
            End If
            comando.CommandText = String.Format("SELECT P.FechaEnvio, P.Id, P.IdCliente, C.Nombre, A2.IdProveedor, P2.Nombre, P.IdAlmacen, A.Nombre, P.IdFamilia, F.Nombre, P.IdSubFamilia, SF.Nombre, P.IdArticulo, A2.Nombre, UM.Nombre, A2.Codigo, A2.Pagina, A2.Color, A2.Talla, A2.Modelo, A2.CodigoInternet, A2.Precio, P.Cantidad, P.Total, P.Observaciones, EstatusConfirmado, EstatusCancelado, EstatusRecibido, EstatusEntregado, FechaLlegada, Observaciones2 " & _
            " FROM Pedidos AS P " & _
            " LEFT JOIN {0}Almacenes AS A ON P.IdAlmacen = A.Id" & _
            " LEFT JOIN {0}Familias AS F ON P.IdFamilia = F.Id AND P.IdAlmacen = F.IdAlmacen" & _
            " LEFT JOIN {0}SubFamilias AS SF ON P.IdSubFamilia = SF.Id AND P.IdFamilia = SF.IdFamilia AND P.IdAlmacen = SF.IdAlmacen" & _
            " LEFT JOIN {0}Articulos AS A2 ON P.IdArticulo = A2.Id AND P.IdSubFamilia = A2.IdSubFamilia AND P.IdFamilia = A2.IdFamilia AND P.IdAlmacen = A2.IdAlmacen" & _
            " LEFT JOIN {0}UnidadesMedidas AS UM ON A2.IdUnidadMedida = UM.Id" & _
            " LEFT JOIN {0}Proveedores AS P2 ON A2.IdProveedor = P2.Id" & _
            " LEFT JOIN {0}Clientes AS C ON P.IdCliente = C.Id" & _
            " WHERE 0=0 {1} ORDER BY P.FechaEnvio, P.Id, P.Orden ASC", ALMLogicaPedidos.Programas.bdCatalogo & ".dbo." & ALMLogicaPedidos.Programas.prefijoBaseDatosAlmacen, condicion & condicionFechaRango)
            comando.Parameters.AddWithValue("@idAlmacen", Me.EIdAlmacen)
            comando.Parameters.AddWithValue("@id", Me.EId)
            comando.Parameters.AddWithValue("@fechaEnvio", ALMLogicaPedidos.Funciones.ValidarFechaAEstandar(Me.EFechaEnvio))
            comando.Parameters.AddWithValue("@fechaEnvio2", ALMLogicaPedidos.Funciones.ValidarFechaAEstandar(Me.EFechaEnvio2))
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
