﻿Imports System.Data.SqlClient

Public Class Clientes

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

    Public Function ObtenerListadoCombos(ByVal esActualizar As Boolean) As DataTable

        Try
            Dim datos As New DataTable
            Dim comando As New SqlCommand()
            comando.Connection = BaseDatos.conexionCatalogo
            Dim valor As String = String.Empty
            If (esActualizar) Then
                valor = "'TODOS'"
            Else
                valor = "NULL"
            End If
            comando.CommandText = String.Format("SELECT Id, Nombre, (CAST(Id AS Varchar)+' - '+ Nombre) AS IdNombre FROM {0}Clientes " & _
            " UNION SELECT -1 AS Id, {1} AS Nombre, {1} AS IdNombre FROM {0}Clientes " & _
            " ORDER BY Id ASC", ALMLogicaCreditos.Programas.prefijoBaseDatosAlmacen, valor)
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
     
    Public Function ObtenerListadoReporteCatalogo() As DataTable

        Try
            Dim datos As New DataTable
            Dim comando As New SqlCommand()
            comando.Connection = BaseDatos.conexionCatalogo
            comando.CommandText = String.Format("SELECT Id, Nombre FROM {0}Clientes ORDER BY Id ASC", ALMLogicaCreditos.Programas.prefijoBaseDatosAlmacen)
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
