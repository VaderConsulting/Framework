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

Public Class ADUser

    Private Structure TemporaryField
        Dim Data As String
    End Structure

    Private m_DisplayName As String
    Private m_Name As String
    Private m_CommonName As String
    Private m_DistinguishedName As String
    Private m_Manager As String
    Private m_OfficeTelephoneNumber As String
    Private m_Title As String
    Private m_Suburb As String
    Private m_FacsimileTelephoneNumber As String
    Private m_MobileTelephoneNumber As String
    Private m_Surname As String
    Private m_GivenName As String
    Private m_Department As String
    Private m_EmployeeNumber As String
    Private m_TelephoneNumber As String
    Private m_Description As String
    Private m_TemporaryFields() As TemporaryField


    Public Property DisplayName() As String
        Get
            Return m_DisplayName
        End Get
        Set(ByVal value As String)
            m_DisplayName = value
        End Set
    End Property

    Public Property Name() As String
        Get
            Return m_Name
        End Get
        Set(ByVal value As String)
            m_Name = value
        End Set
    End Property

    Public Property DistinguishedName() As String
        Get
            Return m_DistinguishedName
        End Get
        Set(ByVal value As String)
            m_DistinguishedName = value
        End Set
    End Property

    Public Property Manager() As String
        Get
            Return m_Manager
        End Get
        Set(ByVal value As String)
            m_Manager = value
        End Set
    End Property

    Public Property CommonName() As String
        Get
            Return m_CommonName
        End Get
        Set(ByVal value As String)
            m_CommonName = value
        End Set
    End Property

#Region " Properties "



#End Region

#Region " Functions "

    Public Function Move() As Int32
        Try
            Return SUCCESS
        Catch ex As Exception

        End Try
    End Function

    Public Function Create() As Int32
        Try
            Return SUCCESS
        Catch ex As Exception

        End Try

    End Function

    Public Function Write() As Int32
        Try
            Return SUCCESS
        Catch ex As Exception

        End Try
    End Function

#End Region

    Public Property OfficeTelephoneNumber() As String
        Get
            Return m_OfficeTelephoneNumber
        End Get
        Set(ByVal value As String)
            m_OfficeTelephoneNumber = value
        End Set
    End Property

End Class
