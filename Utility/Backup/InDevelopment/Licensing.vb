''' <summary>
''' Provides Stratatel Licensing functionality
''' </summary>
''' <remarks></remarks>
Public Class Licensing

    Private m_IsLicensed As Boolean = False

    Public ReadOnly Property isLicensed() As Boolean
        Get
            Return m_IsLicensed
        End Get
    End Property

    Public Sub DoMaintenanceCheck()
        ' Check to see if we are in maintenance
    End Sub

End Class
