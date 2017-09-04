﻿Imports System.Data.SqlClient

Public Class Empresas

    Private id As Integer
    Private nombre As String
    Private descripcion As String
    Private domicilio As String
    Private localidad As String
    Private rfc As String

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
    Public Property EDescripcion() As String
        Get
            Return descripcion
        End Get
        Set(value As String)
            descripcion = value
        End Set
    End Property
    Public Property EDomicilio() As String
        Get
            Return domicilio
        End Get
        Set(value As String)
            domicilio = value
        End Set
    End Property
    Public Property ELocalidad() As String
        Get
            Return localidad
        End Get
        Set(value As String)
            localidad = value
        End Set
    End Property
    Public Property ERfc() As String
        Get
            Return rfc
        End Get
        Set(value As String)
            rfc = value
        End Set
    End Property

    Public Function Obtener(ByVal primerElemento As Boolean) As List(Of Empresas)

        Try
            Dim lista As New List(Of Empresas)
            Dim comando As New SqlCommand()
            comando.Connection = BaseDatos.conexionCatalogo
            Dim consulta As String = String.Empty : Dim condicion As String = String.Empty
            If (primerElemento) Then
                consulta = "SELECT TOP 1 * FROM Empresas ORDER BY Id ASC"
            Else
                If (Me.EId > 0) Then
                    condicion &= "WHERE Id = @id"
                End If
                consulta = "SELECT * FROM Empresas " & condicion
            End If
            comando.CommandText = consulta
            comando.Parameters.AddWithValue("@id", Me.EId)
            BaseDatos.conexionCatalogo.Open()
            Dim dataReader As SqlDataReader = comando.ExecuteReader()
            Dim tabla As Empresas
            While dataReader.Read()
                tabla = New Empresas()
                tabla.id = Convert.ToInt32(dataReader("id").ToString())
                tabla.nombre = dataReader("nombre").ToString()
                tabla.descripcion = dataReader("descripcion").ToString()
                tabla.domicilio = dataReader("domicilio").ToString()
                tabla.localidad = dataReader("localidad").ToString()
                tabla.rfc = dataReader("rfc").ToString()
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

End Class
