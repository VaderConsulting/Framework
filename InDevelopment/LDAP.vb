' ----------------------------------------------------------------------------------------
' Author:                    Dave Robinson
' Company:                   Stratatel
' Assembly version:          1.0.0.0
' Date:                      29/08/2008
' Purpose:                   Provides LDAP specific Utility functions
' ----------------------------------------------------------------------------------------

#Region " Imports "

Imports System.DirectoryServices

#End Region

''' <summary>
''' Provides LDAP specific Utility functions
''' </summary>
''' <remarks></remarks>
Public Class LDAP

    ''' <summary>
    ''' Check if the given LDAP path exists
    ''' </summary>
    ''' <param name="DirectoryPath">Fully qualified LDAP Path in the form 'LDAP://[Server:Port/]dc=Name,dc=COM'</param>
    ''' <returns>Boolean indicating success or failure</returns>
    ''' <remarks></remarks>
    Public Function LDAPDirectoryExists(ByVal DirectoryPath As String) As Boolean
        Dim oSearcher As New DirectorySearcher()

        Try
            With oSearcher
                .SearchRoot = New DirectoryEntry(DirectoryPath)
                .SearchScope = SearchScope.Subtree
                .Filter = "(objectClass=*)"
                .FindOne()
            End With
            Return True
        Catch ex As Exception
            ' Nothing required here.  An incorrect path will result in an exception which is ok.
        End Try
    End Function

    ''' <summary>
    ''' Check the given Username and Password against the given LDAP Server
    ''' </summary>
    ''' <param name="DirectoryPath">Fully qualified LDAP Path in the form 'LDAP://[Server:Port/]dc=Name,dc=COM'</param>
    ''' <param name="DomainAndUsername">The Username to check (including Domain Name)</param>
    ''' <param name="Password">The Password to check</param>
    ''' <returns>Boolean indicating success or failure</returns>
    ''' <remarks></remarks>
    Public Function CheckPassword(ByVal DirectoryPath As String, ByVal DomainAndUsername As String, ByVal Password As String) As Boolean
        Dim de As DirectoryEntry = New DirectoryEntry(DirectoryPath, DomainAndUsername, Password)

        Try
            'Bind to the native AdsObject to force authentication.
            Dim obj As Object = de.NativeObject
            Dim search As DirectorySearcher = New DirectorySearcher(de)

            search.Filter = "(sAMAccountName=" & DomainAndUsername.Substring(DomainAndUsername.IndexOf("\") + 1) & ")"
            search.PropertiesToLoad.Add("cn")
            Dim result As SearchResult = search.FindOne()

            If (result Is Nothing) Then
                Return False
            End If

            Return True

        Catch ex As Exception
            ' Nothing required here.  An incorrect path will result in an exception which is ok.
        End Try

    End Function

    ' ----------------------------------------------------------------------------------------
    ' Author:                    Stephen Hine
    ' Company:                   Stratatel
    ' Date:                      1/09/2008
    ' Purpose:                   Creates a Organizational unit to callers criteria
    ' ----------------------------------------------------------------------------------------
    Public Function CreateOU(ByVal adsPath As String) As Boolean
        Try
            Dim Root As DirectoryEntry     ' Binding object.
            Dim OU As DirectoryEntry       ' Organizational unit.
            Dim OUName As String           ' Organiztional unit.
            Dim Path As String             ' Binding path.

            ' Construct the binding string.
            Path = adsPath
            ' Specify Organizational Unit.
            Dim i As Integer = Path.IndexOf("//", 0)
            OUName = Path.Remove(0, i + 2)
            Dim j As Integer = OUName.IndexOf("/", 0)
            OUName = OUName.Remove(j)

            'Specify the location.
            Path = Path.Remove(i + 2, j + 1)

            Root = New DirectoryEntry(Path)
            Root.RefreshCache()
            OU = Root.Children.Add(OUName, "OrganizationalUnit")

            OU.CommitChanges()

            Return True
        Catch ex As Exception
            Return False
        End Try


    End Function 'CreateOU

End Class
