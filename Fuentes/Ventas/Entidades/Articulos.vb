﻿Imports System.Data.SqlClient

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
            comando.CommandText = String.Format("SELECT A.IdAlmacen, A2.Nombre, A.IdFamilia, F.Nombre, A.IdSubFamilia, SF.Nombre, A.Id, A.Nombre, A.IdProveedor, P.Nombre,UM.Nombre, A.Codigo, A.Pagina, A.Color, A.Talla, A.Modelo, A.CodigoInternet, A.Precio FROM {0}Articulos AS A " & _
            " LEFT JOIN {0}Almacenes AS A2 ON A.IdAlmacen=A2.Id " & _
            " LEFT JOIN {0}Familias AS F ON A.IdAlmacen=F.IdAlmacen AND A.IdFamilia=F.Id " & _
            " LEFT JOIN {0}SubFamilias AS SF ON A.IdAlmacen=SF.IdAlmacen AND A.IdFamilia=SF.IdFamilia AND A.IdSubFamilia=SF.Id " & _
            " LEFT JOIN {0}UnidadesMedidas AS UM ON A.IdUnidadMedida=UM.Id " & _
            " LEFT JOIN {0}Proveedores AS P ON A.IdProveedor=P.Id " & _
            " WHERE 0=0 {1} ORDER BY A.IdAlmacen, A.IdFamilia, A.IdSubFamilia, A.Id ASC", ALMLogicaVentas.Programas.prefijoBaseDatosAlmacen, condicion)
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

    Public Function ObtenerListado() As DataTable

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
            comando.CommandText = String.Format("SELECT IdAlmacen, IdFamilia, IdSubFamilia, Id, Nombre, NombreComercial, IdUnidadMedida, CantidadMinima, CantidadMaxima, Precio, Seccion, Estante, Nivel FROM {0}Articulos WHERE 0=0 {1}", ALMLogicaVentas.Programas.prefijoBaseDatosAlmacen, condicion)
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
