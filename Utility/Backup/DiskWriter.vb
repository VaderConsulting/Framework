Imports System.IO

Imports Utility.Functions
Imports Utility.Types
Imports Utility.Types.ERRORTYPE
Imports Utility.Types.ERROR_CONFIGURATION
Imports Utility.Types.ERROR_CRYPT
Imports Utility.Types.ERROR_DIRECTORY
Imports Utility.Constants
Imports Utility.Types.ERROR_FILESYSTEM
Imports Utility.Types.ERROR_SECURITY
Imports Utility.Types.LOGGING_TYPE

''' <summary>
''' Provides easy writing of text to the file-system
''' </summary>
''' <remarks></remarks>
Public Class DiskWriter

#Region " Constructor "

    ''' <summary>
    ''' Creates a new instance, using the provided output filename
    ''' </summary>
    ''' <param name="OutputFilename"></param>
    ''' <remarks></remarks>
    Public Sub New(ByVal OutputFilename As String)
        m_OutputFilename = OutputFilename
    End Sub

    ''' <summary>
    ''' Creates a new instance, using the default output filename
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        m_OutputFilename = String.Format(Date.Now.Day, "00") & "-" & String.Format(Date.Now.Month, "00") & "-" & String.Format(Date.Now.Year, "0000") & "-OutputFile.log"
    End Sub

#End Region

#Region " Private Variables "

    Private m_FilesystemRootPath As String = My.Application.Info.DirectoryPath & "\"
    Private m_OutputFilename As String = ""
    Private m_AppendToFile As Boolean = True

#End Region

#Region " Public Properties "

    ' ----------------------------------------------------------------------------------------
    ' Author:                        Dave Robinson
    ' Assembly version:              1.0.0.0
    ' Date:                          11/06/2008
    ' Time:                          12:17
    ' Procedure FullName:            DiskWriter.FilesystemRootPath
    ' Procedure Name:                FilesystemRootPath
    ' Function Return Type Name:     String
    ' Function Return Type FullName: System.String
    ' Function Return Type Alias:    String
    ' Purpose:                       The path to write to (not including the filename), but including the trailing backslash
    '                                Note:  Use of this property is optional as the private variable has a default value
    ' Parameters:
    ' ----------------------------------------------------------------------------------------
    Public Property FilesystemRootPath() As String
        Get
            Return m_FilesystemRootPath
        End Get
        Set(ByVal value As String)
            m_FilesystemRootPath = value
        End Set
    End Property

    ' ----------------------------------------------------------------------------------------
    ' Author:                        Dave Robinson
    ' Assembly version:              1.0.0.0
    ' Date:                          11/06/2008
    ' Time:                          12:20
    ' Procedure FullName:            DiskWriter.OutputFilename
    ' Procedure Name:                OutputFilename
    ' Function Return Type Name:     String
    ' Function Return Type FullName: System.String
    ' Function Return Type Alias:    String
    ' Purpose:                       The file to write to
    '                                Note:  Use of this property is optional as the private variable has a default value
    ' Parameters:
    ' ----------------------------------------------------------------------------------------
    Public Property OutputFilename() As String
        Get
            Return m_OutputFilename
        End Get
        Set(ByVal value As String)
            m_OutputFilename = value
        End Set
    End Property

#End Region

#Region " Public Methods "

    ''' <summary>
    ''' Write data to the output file
    ''' </summary>
    ''' <param name="Data">The string data to write</param>
    ''' <returns>Boolean indicating success or failure</returns>
    ''' <remarks></remarks>
    Public Function WriteDataToFile(ByVal Data As String, ByRef Result As Boolean) As Int32
        Try
            Dim oStream As New StreamWriter(m_FilesystemRootPath & m_OutputFilename, m_AppendToFile)
            oStream.Write(Data)
            oStream.Close()

            Result = True

        Catch ex As Exception
            Result = False
        End Try
    End Function

#End Region

End Class
