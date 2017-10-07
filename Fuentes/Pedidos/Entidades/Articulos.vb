Imports System.Data.SqlClient

Public Class Articulos

    Private idAlmacen As Integer
    Private idFamilia As Integer
    Private idSubFamilia As Integer
    Private id As Integer
    Private nombre As String
    Private nombreComercial As String
    Private idUnidadMedida As Integer
    Private cantidadMinima As Integer
    Private cantidadMaxima As Integer 
    Private precio As Double
    Private seccion As String
    Private estante As String
    Private nivel As String
    Private idProveedor As Integer

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
    Public Property EId() As Integer
        Get
            Return id
        End Get
        Set(value As Integer)
            id = value
        End Set
    End Property
    Public Property ENombre() As String
        Get
            Return nombre
        End Get
        Set(value As String)
            nombre = value
        End Set
    End Property
    Public Property ENombreComercial() As String
        Get
            Return nombreComercial
        End Get
        Set(value As String)
            nombreComercial = value
        End Set
    End Property
    Public Property EIdUnidadMedida() As Integer
        Get
            Return idUnidadMedida
        End Get
        Set(value As Integer)
            idUnidadMedida = value
        End Set
    End Property
    Public Property ECantidadMinima() As Integer
        Get
            Return cantidadMinima
        End Get
        Set(value As Integer)
            cantidadMinima = value
        End Set
    End Property
    Public Property ECantidadMaxima() As Integer
        Get
            Return cantidadMaxima
        End Get
        Set(value As Integer)
            cantidadMaxima = value
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
    Public Property ESeccion() As String
        Get
            Return seccion
        End Get
        Set(value As String)
            seccion = value
        End Set
    End Property
    Public Property EEstante() As String
        Get
            Return estante
        End Get
        Set(value As String)
            estante = value
        End Set
    End Property
    Public Property ENivel() As String
        Get
            Return nivel
        End Get
        Set(value As String)
            nivel = value
        End Set
    End Property
    Public Property EIdProveedor() As Integer
        Get
            Return idProveedor
        End Get
        Set(value As Integer)
            idProveedor = value
        End Set
    End Property

    Public Function ObtenerListadoReporteCatalogo() As DataTable

        Try
            Dim datos As New DataTable
            Dim comando As New SqlCommand()
            comando.Connection = BaseDatos.conexionCatalogo
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
            If (Me.EId > 0) Then
                condicion &= " AND Id=@id"
            End If
            If (Me.EIdProveedor > 0) Then
                condicion &= " AND IdProveedor=@idProveedor"
            End If
            comando.CommandText = String.Format("SELECT A.Id, A.Nombre, UM.Nombre, A.Codigo, A.Pagina, A.Color, A.Talla, A.Modelo, A.CodigoInternet, A.Precio FROM {0}Articulos AS A " & _
            " LEFT JOIN {0}UnidadesMedidas AS UM ON A.IdUnidadMedida=UM.Id " & _
            " WHERE 0=0 {1} " & _
            " ORDER BY A.IdAlmacen, A.IdFamilia, A.IdSubFamilia, A.Id ASC", ALMLogicaPedidos.Programas.prefijoBaseDatosAlmacen, condicion)
            comando.Parameters.AddWithValue("@idAlmacen", Me.EIdAlmacen)
            comando.Parameters.AddWithValue("@idFamilia", Me.EIdFamilia)
            comando.Parameters.AddWithValue("@idSubFamilia", Me.EIdSubFamilia)
            comando.Parameters.AddWithValue("@id", Me.EId)
            comando.Parameters.AddWithValue("@idProveedor", Me.EIdProveedor)
            BaseDatos.conexionCatalogo.Open()
            Dim lectorDatos As SqlDataReader
            lectorDatos = comando.ExecuteReader()
            datos.Load(lectorDatos)
            BaseDatos.conexionCatalogo.Close()
            Return datos
        Catch ex As Exception
            Throw ex
        Finally
            BaseDatos.conexionCatalogo.Close()
        End Try

    End Function

    Public Function ObtenerListado() As List(Of Articulos)

        Try
            Dim lista As New List(Of Articulos)
            Dim comando As New SqlCommand()
            comando.Connection = BaseDatos.conexionCatalogo
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
            If (Me.EId > 0) Then
                condicion &= " AND Id=@id"
            End If
            comando.CommandText = String.Format("SELECT IdAlmacen, IdFamilia, IdSubFamilia, Id, Nombre, NombreComercial, IdUnidadMedida, CantidadMinima, CantidadMaxima, Precio, Seccion, Estante, Nivel, IdProveedor FROM {0}Articulos WHERE 0=0 {1}", ALMLogicaPedidos.Programas.prefijoBaseDatosAlmacen, condicion)
            comando.Parameters.AddWithValue("@idAlmacen", Me.EIdAlmacen)
            comando.Parameters.AddWithValue("@idFamilia", Me.EIdFamilia)
            comando.Parameters.AddWithValue("@idSubFamilia", Me.EIdSubFamilia)
            comando.Parameters.AddWithValue("@id", Me.EId)
            BaseDatos.conexionCatalogo.Open()
            Dim lectorDatos As SqlDataReader = comando.ExecuteReader()
            Dim tabla As Articulos
            While lectorDatos.Read()
                tabla = New Articulos()
                tabla.idAlmacen = Convert.ToInt32(lectorDatos("IdAlmacen").ToString())
                tabla.idFamilia = Convert.ToInt32(lectorDatos("idFamilia").ToString())
                tabla.idSubFamilia = Convert.ToInt32(lectorDatos("idSubFamilia").ToString())
                tabla.id = Convert.ToInt32(lectorDatos("Id").ToString())
                tabla.nombre = lectorDatos("Nombre").ToString()
                tabla.nombreComercial = lectorDatos("NombreComercial").ToString()
                tabla.idUnidadMedida = Convert.ToInt32(lectorDatos("IdUnidadMedida").ToString())
                tabla.cantidadMinima = Convert.ToInt32(lectorDatos("CantidadMinima").ToString())
                tabla.cantidadMaxima = Convert.ToInt32(lectorDatos("CantidadMaxima").ToString())
                tabla.precio = Convert.ToDouble(lectorDatos("Precio").ToString())
                tabla.seccion = lectorDatos("Seccion").ToString()
                tabla.estante = lectorDatos("Estante").ToString()
                tabla.nivel = lectorDatos("Nivel").ToString()
                tabla.idProveedor = lectorDatos("IdProveedor").ToString()
                lista.Add(tabla)
            End While
            BaseDatos.conexionCatalogo.Close()
            Return lista
        Catch ex As Exception
            Throw ex
        Finally
            BaseDatos.conexionCatalogo.Close()
        End Try

    End Function
     
    Public Function ObtenerListadoReporteCombos() As DataTable

        Try
            Dim datos As New DataTable
            Dim comando As New SqlCommand()
            comando.Connection = BaseDatos.conexionCatalogo
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
            If (Me.EId > 0) Then
                condicion &= " AND Id=@id"
            End If
            comando.CommandText = String.Format("SELECT Id, Nombre, (CAST(Id AS Varchar)+' - '+ Nombre) AS IdNombre FROM {0}Articulos WHERE 0=0 {1}" & _
            " UNION SELECT -1 AS Id, NULL AS Nombre, NULL AS IdNombre FROM {0}Articulos " & _
            " ORDER BY Id ASC", ALMLogicaPedidos.Programas.prefijoBaseDatosAlmacen, condicion)
            comando.Parameters.AddWithValue("@idAlmacen", Me.EIdAlmacen)
            comando.Parameters.AddWithValue("@idFamilia", Me.EIdFamilia)
            comando.Parameters.AddWithValue("@idSubFamilia", Me.EIdSubFamilia)
            comando.Parameters.AddWithValue("@id", Me.EId)
            BaseDatos.conexionCatalogo.Open()
            Dim lectorDatos As SqlDataReader
            lectorDatos = comando.ExecuteReader()
            datos.Load(lectorDatos)
            BaseDatos.conexionCatalogo.Close()
            Return datos
        Catch ex As Exception
            Throw ex
        Finally
            BaseDatos.conexionCatalogo.Close()
        End Try

    End Function

End Class
