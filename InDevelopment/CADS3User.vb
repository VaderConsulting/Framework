Public Class CADS3User

    Public Structure Properties
        Public Username As String
        ''' <summary>
        ''' CADS3 Field ''
        ''' </summary>
        ''' <remarks></remarks>
        Public Path As String
        ''' <summary>
        ''' CADS3 Field ''
        ''' </summary>
        ''' <remarks></remarks>
        Public Surname As String
        ''' <summary>
        ''' CADS3 Field ''
        ''' </summary>
        ''' <remarks></remarks>
        Public FirstName As String
        ''' <summary>
        ''' CADS3 Field ''
        ''' </summary>
        ''' <remarks></remarks>
        Public City As String
        ''' <summary>
        ''' CADS3 Field ''
        ''' </summary>
        ''' <remarks></remarks>
        Public Department As String
        ''' <summary>
        ''' CADS3 Field ''
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
            MsgBox("TODO: Finish CADS3.User.Properties")
        End Sub
    End Structure

    Private m_Username As String
    Private m_User As CADS3User

    Public Sub New()

    End Sub

    Public Sub New(ByVal Username As String)
        m_Username = Username
    End Sub

    Public Sub Import()
        'Dim ImportForm As New CADS3Import

        'ImportForm.ShowDialog()
    End Sub

End Class
