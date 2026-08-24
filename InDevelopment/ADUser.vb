Imports System.DirectoryServices

Public Class ADUser

    Public Structure Properties
        Public Username As String
        ''' <summary>
        ''' Active Directory Property 'aDSPath'
        ''' </summary>
        ''' <remarks></remarks>
        Public Path As String
        ''' <summary>
        ''' Active Directory Property 'sn'
        ''' </summary>
        ''' <remarks></remarks>
        Public Surname As String
        ''' <summary>
        ''' Active Directory Property 'givenName'
        ''' </summary>
        ''' <remarks></remarks>
        Public FirstName As String
        ''' <summary>
        ''' Active Directory Property 'l'
        ''' </summary>
        ''' <remarks></remarks>
        Public City As String
        ''' <summary>
        ''' Active Directory Property 'department'
        ''' </summary>
        ''' <remarks></remarks>
        Public Department As String
        ''' <summary>
        ''' Active Directory Property 'mobile'
        ''' </summary>
        ''' <remarks></remarks>
        Public MobilePhoneNumber As String
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <remarks></remarks>
        Public OfficePhoneNumber As String
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <remarks></remarks>
        Public Address As String
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <remarks></remarks>
        Public UID As String

        Shared Sub New()
            MsgBox("TODO: Finish AD.User.Properties")
        End Sub
    End Structure

    Private m_User As New DirectoryEntry()

#Region " Constructors "

    ''' <summary>
    ''' Default constructor
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        MsgBox("TODO: Finish New()")
    End Sub

    ''' <summary>
    ''' Creates an instance of User with the provided Username (sAMAccountName)
    ''' </summary>
    ''' <param name="Username">The Username (sAMAccountName) to create</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal Username As String)
        MsgBox("TODO: Finish New(Username)")
    End Sub

    ''' <summary>
    ''' Creates an instance of User with the specified Username (sAMAccountName) and at the specified Path
    ''' </summary>
    ''' <param name="Username">The username (sAMAccountName) to create</param>
    ''' <param name="Path">The location of the user in the underlying Directory</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal Username As String, ByVal Path As String)
        MsgBox("TODO: Finish New(Username,Path)")
    End Sub

    ''' <summary>
    ''' Creates an instance of User given another User as the source
    ''' </summary>
    ''' <param name="User"></param>
    ''' <remarks></remarks>
    Public Sub New(ByVal User As ADUser)
        MsgBox("TODO: Finish New(User)")
    End Sub

#End Region

    Public Function WriteToDirectory() As Boolean
        MsgBox("TODO: Finish WriteToDirectory")
        Return True
    End Function

    Public Function Create() As Boolean
        MsgBox("TODO: Finish Create")
        Return True
    End Function

    Public Function Clone() As ADUser
        Dim NewUser As ADUser = Nothing

        MsgBox("TODO: Finish Clone")

        Return NewUser
    End Function

End Class
