Namespace CADS

    Public Class Structures

        Public Enum TelephoneType
            Mobile = 2
            Fixed = 4
            Pager = 5
            Fax = 10
            OfficeExtension = 11
            Home = 12
        End Enum

        Public Structure ExtensionRange
            Dim StartNumber As String
            Dim FinishNumber As String
        End Structure

        Public Structure StreetAddress
            Dim Number As String
            Dim Street As String
            Dim Suburb As String
            Dim State As String
            Dim Postcode As String
            Dim Country As String
        End Structure

        Public Structure CostCentre
            Dim ID As Int32
            Dim Code As String
            Dim Manager As String
        End Structure

        Public Structure PhysicalLocation
            Dim ID As Int32
            Dim Name As String
            Dim Description As String
            Dim Address As StreetAddress
            Dim Manager As String
            Dim PABX As PABXInformation()
            Dim CostCentre As CostCentre
        End Structure

        Public Structure PABXInformation
            Dim ExtensionRange As ExtensionRange()
            Dim IndialRange As ExtensionRange()
            Dim Costcode As String
            Dim SiteAccessCode As String
        End Structure

        Public Structure Phone
            Dim Number As String
            Dim Manager As String
            Dim Department As String
            Dim Location As String
            Dim DirectoryPath As String
            Dim User As String
            Dim PhoneType As TelephoneType
        End Structure

    End Class

End Namespace
