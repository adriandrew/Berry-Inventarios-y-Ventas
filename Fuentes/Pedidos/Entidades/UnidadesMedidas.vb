﻿Imports System.Data.SqlClient

Public Class UnidadesMedidas

    Private id As Integer
    Private nombre As String

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
     
    Public Function ObtenerListado() As List(Of UnidadesMedidas)

        Try
            Dim lista As New List(Of UnidadesMedidas)
            Dim comando As New SqlCommand()
            comando.Connection = BaseDatos.conexionCatalogo
            comando.CommandText = String.Format("SELECT Id, Nombre FROM {0}UnidadesMedidas WHERE Id=@id ORDER BY Id ASC", ALMLogicaPedidos.Programas.prefijoBaseDatosAlmacen)
            comando.Parameters.AddWithValue("@id", Me.EId)
            BaseDatos.conexionCatalogo.Open()
            Dim lectorDatos As SqlDataReader
            lectorDatos = comando.ExecuteReader()
            Dim tabla As UnidadesMedidas
            While lectorDatos.Read()
                tabla = New UnidadesMedidas()
                tabla.id = Convert.ToInt32(lectorDatos("Id").ToString())
                tabla.nombre = lectorDatos("Nombre").ToString()
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
