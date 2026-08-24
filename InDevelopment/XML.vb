Imports System.Xml

Public Class XML

    Private m_Filename As String = ""
    Private m_LastErrorDescription As String = ""

    Public Sub New(ByVal Filename As String)
        m_Filename = Filename
    End Sub

    Public Property Filename()
        Get
            Return m_Filename
        End Get
        Set(ByVal value)
            m_Filename = value
        End Set
    End Property

    Public ReadOnly Property LastErrorDescription() As String
        Get
            Return m_LastErrorDescription
        End Get
    End Property

    ''' <summary>
    ''' Load configuration setting
    ''' </summary>
    ''' <param name="Name">Name of the Setting</param>
    ''' <param name="DefaultValue">Default Value of the Setting</param>
    ''' <returns>Setting value</returns>
    ''' <remarks></remarks>
    Public Function LoadSetting(ByVal Name As String, Optional ByVal DefaultValue As String = "") As String
        Dim ReturnValue As String = DefaultValue
        Dim oDoc As XmlDocument = New XmlDocument()

        Try

            oDoc.LoadXml(m_Filename)


        Catch ex As Exception
            m_LastErrorDescription = ex.Message
        End Try

        Return ReturnValue
    End Function

End Class
