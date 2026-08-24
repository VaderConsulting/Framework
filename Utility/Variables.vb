Imports System.Runtime.Serialization
Imports System.Runtime.Serialization.Formatters
Imports System.Runtime.Serialization.Formatters.Binary
Imports System.Runtime.Serialization.Formatters.Soap
Imports Utility.CADS
Imports Utility.CADS.Variables
Imports Utility.CADS.Variables.ERROR_CONFIG
Imports Utility.CADS.Variables.ERROR_CRYPT
Imports Utility.CADS.Variables.ERROR_IO
Imports Utility.CADS.Variables.ERROR_SECURITY
Imports Utility.CADS.Variables.ERROR_UNSPECIFIED
Imports Utility.CADS.Variables.ERRORTYPE

Namespace CADS

    ''' <summary>
    ''' Provides standard variables to all projects
    ''' </summary>
    ''' <remarks>V1.0  D. Robinson</remarks>
    Public Class Variables

        Public Const DefaultStringValue As String = "[Empty]"
        Public Const DefaultIntegerValue As Int32 = -1

        ''' <summary>
        ''' CADS Specific Error types
        ''' </summary>
        ''' <remarks>V1.0  D. Robinson</remarks>
        ''' 
        Public Enum ERRORTYPE
            SUCCESS = 0
            IO = 1
            CONFIG = 2
            SECURITY = 3
            CRYPT = 4
            UNSPECIFIED = 99
        End Enum

        Public Enum ERROR_IO
            DIRECTORY_NOT_FOUND = 1
            FILE_NOT_FOUND = 2
            FILE_READ = 3
            FILE_WRITE = 4
        End Enum

        Public Enum ERROR_CONFIG
            SETTING_NOT_FOUND = 1
            STRING_READ_ERROR = 2
            INTEGER_READ_ERROR = 3
            WRITE_NOT_SUPPORTED = 4
            WRITE_ERROR = 5
            WRITE_NOT_PERFORMED = 6
            STRING_WRITE_ERROR = 7
            INTEGER_EXPECTED_ERROR = 8
            INTEGER_WRITE_ERROR = 9
        End Enum

        Public Enum ERROR_SECURITY
            COULD_NOT_CREATE_OBJECT = 1
            COULD_NOT_READ_OBJECT = 2
            COULD_NOT_WRITE_OBJECT = 3
            REFERRAL_RETURNED = 4
            AUTHENTICATION_FAILURE = 5
            DIRECTORY_ERROR = 6
            GENERIC_SECURITY_ERROR = 99
        End Enum

        Public Enum ERROR_CRYPT
            ENCRYPTION_ERROR = 1
            DECRYPTION_ERROR = 2
            GENERAL_ERROR = 3
        End Enum

        Public Enum ERROR_UNSPECIFIED
            NOT_IMPLEMENTED = 1
            OTHER = 99
        End Enum

        ''' <summary>
        ''' Translates the ERROR_IO code into the CADS_ERRORCODE
        ''' </summary>
        ''' <param name="ErrorType">One of the defined CADS_ERROR_TYPES</param>
        ''' <param name="ErrorNumber">The ERROR_IO value to translate</param>
        ''' <returns>The CADS_ERRORCODE</returns>
        ''' <remarks>V1.0  D. Robinson</remarks>
        Public Function ErrorResult(ByVal ErrorType As ERRORTYPE, ByVal ErrorNumber As ERROR_IO) As Int32
            Dim Result As Int32 = (ErrorType * (2 ^ 16)) + ErrorNumber

            Return Result
        End Function

        ''' <summary>
        ''' Translates the ERROR_CONFIG code into the CADS_ERRORCODE
        ''' </summary>
        ''' <param name="ErrorType">One of the defined CADS_ERROR_TYPES</param>
        ''' <param name="ErrorNumber">The ERROR_CONFIG value to translate</param>
        ''' <returns>The CADS_ERRORCODE</returns>
        ''' <remarks>V1.0  D. Robinson</remarks>
        Public Function ErrorResult(ByVal ErrorType As ERRORTYPE, ByVal ErrorNumber As ERROR_CONFIG) As Int32
            Dim Result As Int32 = (ErrorType * (2 ^ 16)) + ErrorNumber

            Return Result
        End Function

        ''' <summary>
        ''' Translates the ERROR_SECURITY code into the CADS_ERRORCODE
        ''' </summary>
        ''' <param name="ErrorType">One of the defined CADS_ERROR_TYPES</param>
        ''' <param name="ErrorNumber">The ERROR_SECURITY value to translate</param>
        ''' <returns>The CADS_ERRORCODE</returns>
        ''' <remarks>V1.0  D. Robinson</remarks>
        Public Function ErrorResult(ByVal ErrorType As ERRORTYPE, ByVal ErrorNumber As ERROR_SECURITY) As Int32
            Dim Result As Int32 = (ErrorType * (2 ^ 16)) + ErrorNumber

            Return Result
        End Function

        ''' <summary>
        ''' Translates the ERROR_UNSPECIFIED code into the CADS_ERRORCODE
        ''' </summary>
        ''' <param name="ErrorType">One of the defined CADS_ERROR_TYPES</param>
        ''' <param name="ErrorNumber">The ERROR_UNSPECIFIED value to translate</param>
        ''' <returns>The CADS_ERRORCODE</returns>
        ''' <remarks>V1.0  D. Robinson</remarks>
        Public Function ErrorResult(ByVal ErrorType As ERRORTYPE, ByVal ErrorNumber As ERROR_UNSPECIFIED) As Int32
            Dim Result As Int32 = (ErrorType * (2 ^ 16)) + ErrorNumber

            Return Result
        End Function

        ''' <summary>
        ''' Translates the ERROR_CRYPT code into the CADS_ERRORCODE
        ''' </summary>
        ''' <param name="ErrorType">One of the defined CADS_ERROR_TYPES</param>
        ''' <param name="ErrorNumber">The ERROR_CRYPT value to translate</param>
        ''' <returns>The CADS_ERRORCODE</returns>
        ''' <remarks>V1.0  D. Robinson</remarks>
        Public Function ErrorResult(ByVal ErrorType As ERRORTYPE, ByVal ErrorNumber As ERROR_CRYPT) As Int32
            Dim Result As Int32 = (ErrorType * (2 ^ 16)) + ErrorNumber

            Return Result
        End Function

        ''' <summary>
        ''' Saves an object to disk
        ''' </summary>
        ''' <param name="Filename">The output filename</param>
        ''' <param name="ObjectToSerialize">An object to serialize</param>
        ''' <returns>SUCCESS or CADS_ERRORCODE</returns>
        ''' <remarks>V1.0  D. Robinson</remarks>
        Public Function Serialize(ByVal Filename As String, ByVal ObjectToSerialize As Object) As Int32
            Dim fs As New IO.FileStream(Filename, System.IO.FileMode.Create)
            Dim formatter As New Formatters.Soap.SoapFormatter

            Try
                formatter.Serialize(fs, ObjectToSerialize)

                fs.Close()
                fs = Nothing
                Return SUCCESS
            Catch ex As Exception
                Return 1 ' TODO:  Should be ErrorResult(x) as appropriate
            End Try
        End Function

        ''' <summary>
        ''' Loads an object from disk
        ''' </summary>
        ''' <param name="PathAndFilename">The input filename</param>
        ''' <param name="DeserializedObject">The deserialized object</param>
        ''' <returns>SUCCESS or CADS_ERRORCODE</returns>
        ''' <remarks>V1.0  D. Robinson</remarks>
        Public Function Deserialize(ByVal PathAndFilename As String, ByRef DeserializedObject As Object) As Int32
            Dim fs As New IO.FileStream(PathAndFilename, System.IO.FileMode.Open)
            Dim formatter As New Formatters.Soap.SoapFormatter

            Try
                DeserializedObject = formatter.Deserialize(fs)

                fs.Flush()
                fs.Close()
                Return SUCCESS
            Catch ex As Exception
                Return 2 ' TODO:  Should be ErrorResult(x) as appropriate
            End Try
        End Function

        ''' <summary>
        ''' Function to return the text value based on the index provided
        ''' </summary>
        ''' <param name="ErrorCode">The value we're looking for</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetErrorTypeAndCode(ByVal ErrorCode As Int32) As String
            Dim ErrorString As String = ""
            ' The passed-in Error code has two portions:
            ' 1. Error type
            ' 2. Error code (local)

            ' Firstly, extract the type
            Dim ErrorType As Int16 = (ErrorCode / (2 ^ 16))
            ErrorString = "(" & [Enum].GetName(GetType(ERRORTYPE), ErrorType) & ") "

            ' Now determine the code
            Dim Remainder As Int16 = ErrorCode - (ErrorType * (2 ^ 16))

            ' The output will look something like the following:
            ' "(ERROR_IO) DIRECTORY_NOT_FOUND"
            Select Case ErrorType
                Case 1
                    ErrorString &= [Enum].GetName(GetType(ERROR_IO), Remainder)
                Case 2
                    ErrorString &= [Enum].GetName(GetType(ERROR_CONFIG), Remainder)
                Case 3
                    ErrorString &= [Enum].GetName(GetType(ERROR_SECURITY), Remainder)
                Case 4
                    ErrorString &= [Enum].GetName(GetType(ERROR_CRYPT), Remainder)
                Case 99
                    ErrorString &= [Enum].GetName(GetType(ERROR_UNSPECIFIED), Remainder)
                Case Else
                    ErrorString &= "UNSUPPORTED_ERROR_VALUE"
            End Select

            Return ErrorString
        End Function

        ''' <summary>
        ''' Returns a string representation of the input string collection, separated by the Separator character
        ''' </summary>
        ''' <param name="InputCollection">The Input StringCollection</param>
        ''' <param name="Separator">The input Separator to use</param>
        ''' <returns>String</returns>
        ''' <remarks>V1.0 D. Robinson</remarks>
        Public Function StringFromStringCollection(ByVal InputCollection As Specialized.StringCollection, ByVal Separator As String) As String
            Dim OutputString As String = ""

            For Each InputString As String In InputCollection
                OutputString &= InputString & Separator
            Next

            If Right(OutputString, Len(Separator)) = Separator Then ' remove last separator character(s)
                OutputString = Left(OutputString, Len(OutputString) - Len(Separator))
            End If

            Return OutputString.Trim
        End Function

    End Class

    
End Namespace
