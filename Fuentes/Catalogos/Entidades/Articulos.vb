Imports System.Data.SqlClient

Public Class Articulos

    Private idAlmacen As Integer
    Private idFamilia As Integer
    Private idSubFamilia As Integer
    Private id As Long
    Private nombre As String
    Private nombreComercial As String
    Private idUnidadMedida As Integer
    Private cantidadMinima As Integer
    Private cantidadMaxima As Integer 
    Private precio As Double
    Private seccion As String
    Private estante As String
    Private nivel As String
    Private pagina As Integer
    Private codigo As String
    Private color As String
    Private talla As String
    Private modelo As String
    Private codigoInternet As String
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
    Public Property EId() As Long
        Get
            Return id
        End Get
        Set(value As Long)
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
    Public Property EPagina() As Integer
        Get
            Return pagina
        End Get
        Set(value As Integer)
            pagina = value
        End Set
    End Property 
    Public Property ECodigo() As String
        Get
            Return codigo
        End Get
        Set(value As String)
            codigo = value
        End Set
    End Property 
    Public Property EColor() As String
        Get
            Return color
        End Get
        Set(value As String)
            color = value
        End Set
    End Property 
    Public Property ETalla() As String
        Get
            Return talla
        End Get
        Set(value As String)
            talla = value
        End Set
    End Property 
    Public Property EModelo() As String
        Get
            Return modelo
        End Get
        Set(value As String)
            modelo = value
        End Set
    End Property

    Public Property ECodigoInternet() As String
        Get
            Return codigoInternet
        End Get
        Set(value As String)
            codigoInternet = value
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

    Public Sub Guardar()

        Try
            Dim comando As New SqlCommand()
            comando.Connection = BaseDatos.conexionCatalogo
            comando.CommandText = String.Format("INSERT INTO {0}Articulos (IdAlmacen, IdFamilia, IdSubFamilia, Id, Nombre, NombreComercial, IdUnidadMedida, CantidadMinima, CantidadMaxima, Precio, Seccion, Estante, Nivel, Pagina, Codigo, Color, Talla, Modelo, CodigoInternet, IdProveedor) VALUES (@idAlmacen, @idFamilia, @idSubFamilia, @id, @nombre, @nombreComercial, @idUnidadMedida, @cantidadMinima, @cantidadMaxima, @precio, @seccion, @estante, @nivel, @pagina, @codigo, @color, @talla, @modelo, @codigoInternet, @idProveedor)", ALMLogicaCatalogos.Programas.prefijoBaseDatosAlmacen)
            comando.Parameters.AddWithValue("@idAlmacen", Me.EIdAlmacen)
            comando.Parameters.AddWithValue("@idFamilia", Me.EIdFamilia)
            comando.Parameters.AddWithValue("@idSubFamilia", Me.EIdSubFamilia)
            comando.Parameters.AddWithValue("@id", Me.EId)
            comando.Parameters.AddWithValue("@nombre", Me.ENombre)
            comando.Parameters.AddWithValue("@nombreComercial", Me.ENombreComercial)
            comando.Parameters.AddWithValue("@idUnidadMedida", Me.EIdUnidadMedida)
            comando.Parameters.AddWithValue("@cantidadMinima", Me.ECantidadMinima)
            comando.Parameters.AddWithValue("@cantidadMaxima", Me.ECantidadMaxima)
            comando.Parameters.AddWithValue("@precio", Me.EPrecio)
            comando.Parameters.AddWithValue("@seccion", Me.ESeccion)
            comando.Parameters.AddWithValue("@estante", Me.EEstante)
            comando.Parameters.AddWithValue("@nivel", Me.ENivel)
            comando.Parameters.AddWithValue("@pagina", Me.EPagina)
            comando.Parameters.AddWithValue("@codigo", Me.ECodigo)
            comando.Parameters.AddWithValue("@color", Me.EColor)
            comando.Parameters.AddWithValue("@talla", Me.ETalla)
            comando.Parameters.AddWithValue("@modelo", Me.EModelo)
            comando.Parameters.AddWithValue("@codigoInternet", Me.ECodigoInternet)
            comando.Parameters.AddWithValue("@idProveedor", Me.EIdProveedor)
            BaseDatos.conexionCatalogo.Open()
            comando.ExecuteNonQuery()
            BaseDatos.conexionCatalogo.Close()
        Catch ex As Exception
            Throw ex
        Finally
            BaseDatos.conexionCatalogo.Close()
        End Try

    End Sub

    Public Sub Eliminar()

        Try
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
            comando.CommandText = String.Format("DELETE FROM {0}Articulos WHERE 0=0 {1}", ALMLogicaCatalogos.Programas.prefijoBaseDatosAlmacen, condicion)
            comando.Parameters.AddWithValue("@idAlmacen", Me.EIdAlmacen)
            comando.Parameters.AddWithValue("@idFamilia", Me.EIdFamilia)
            comando.Parameters.AddWithValue("@idSubFamilia", Me.EIdSubFamilia)
            comando.Parameters.AddWithValue("@id", Me.EId)
            BaseDatos.conexionCatalogo.Open()
            comando.ExecuteNonQuery()
            BaseDatos.conexionCatalogo.Close()
        Catch ex As Exception
            Throw ex
        Finally
            BaseDatos.conexionCatalogo.Close()
        End Try

    End Sub

    Public Function ObtenerListadoReporte() As DataTable

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
            comando.CommandText = String.Format("SELECT A.Id, A.Nombre, A.NombreComercial, A.IdUnidadMedida, UM.Nombre, A.CantidadMinima, A.CantidadMaxima, A.Precio, A.Seccion, A.Estante, A.Nivel, A.Pagina, A.Codigo, A.Color, A.Talla, A.Modelo, A.CodigoInternet  " & _
            " FROM {0}Articulos AS A " & _
            " LEFT JOIN {0}UnidadesMedidas AS UM ON A.IdUnidadMedida=UM.Id " & _
            " WHERE 0=0 {1} ORDER BY A.IdAlmacen, A.IdFamilia, A.IdSubFamilia, A.Id ASC", ALMLogicaCatalogos.Programas.prefijoBaseDatosAlmacen, condicion)
            comando.Parameters.AddWithValue("@idAlmacen", Me.EIdAlmacen)
            comando.Parameters.AddWithValue("@idFamilia", Me.EIdFamilia)
            comando.Parameters.AddWithValue("@idSubFamilia", Me.EIdSubFamilia)
            comando.Parameters.AddWithValue("@id", Me.EId)
            BaseDatos.conexionCatalogo.Open()
            Dim dataReader As SqlDataReader
            dataReader = comando.ExecuteReader()
            datos.Load(dataReader)
            BaseDatos.conexionCatalogo.Close()
            Return datos
        Catch ex As Exception
            Throw ex
        Finally
            BaseDatos.conexionCatalogo.Close()
        End Try

    End Function

    Public Sub EliminarArticulosProveedor()

        Try
            Dim comando As New SqlCommand()
            comando.Connection = BaseDatos.conexionCatalogo 
            comando.CommandText = String.Format("DELETE FROM {0}Articulos WHERE IdProveedor=@idProveedor", ALMLogicaCatalogos.Programas.prefijoBaseDatosAlmacen)
            comando.Parameters.AddWithValue("@idProveedor", Me.EIdProveedor)
            BaseDatos.conexionCatalogo.Open()
            comando.ExecuteNonQuery()
            BaseDatos.conexionCatalogo.Close()
        Catch ex As Exception
            Throw ex
        Finally
            BaseDatos.conexionCatalogo.Close()
        End Try

    End Sub

End Class
