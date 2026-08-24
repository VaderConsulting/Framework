#Region " Imports "

Imports System
Imports System.Configuration
Imports System.Xml
Imports System.Xml.XPath
Imports System.IO
'Imports Utility.CADS.Functions
'Imports Utility.CADS.Functions.ERROR_CONFIG
'Imports Utility.CADS.Functions.ERROR_CRYPT
'Imports Utility.CADS.Functions.ERROR_FILESYSTEM
'Imports Utility.CADS.Functions.ERROR_SECURITY
'Imports Utility.CADS.Functions.ERRORTYPE

#End Region

' External configuration files must be in the following format:
'
'   <configuration>
'    <Setting1>Text</Setting1>
'    <Setting2>Other Text</Setting2>
'    <Setting3>NewValue</Setting3>
'  </configuration>
'

''' <summary>
''' Provides methods to read and write settings to\from configuration files external to your project
''' </summary>
''' <remarks>V1.0  D. Robinson</remarks>
Public Class Config

#Region " Private variables "

    Private m_Filename As String
    Private m_ConfigDoc As New ConfigXmlDocument
    'Private m_Variables As New CADS.Functions

#End Region

#Region " Public Properties "

    ''' <summary>
    ''' The filename of the external configuration file
    ''' </summary>
    ''' <value>The filename to use</value>
    ''' <returns>The filename in use</returns>
    ''' <remarks>V1.0  D. Robinson</remarks>
    Public Property Filename() As String
        Get
            Return m_Filename
        End Get
        Set(ByVal value As String)
            m_Filename = value
        End Set
    End Property

#End Region

#Region " Constructors "
    ''' <summary>
    ''' Instantiate new object, connecting to the given filename
    ''' </summary>
    ''' <param name="Filename">The External configuration file to open</param>
    ''' <remarks>V1.0  D. Robinson</remarks>
    Public Sub New(ByVal Filename As String)
        m_Filename = Filename
    End Sub

    ''' <summary>
    ''' Default constructor
    ''' </summary>
    ''' <remarks>V1.0  D. Robinson</remarks>
    Public Sub New()
        ' Don't do anything.  This is the default constructor
        MyBase.New()
    End Sub

#End Region

    '#Region " Public Methods "

    '        ''' <summary>
    '        ''' Adds the given setting
    '        ''' </summary>
    '        ''' <param name="SettingName">The setting to add</param>
    '        ''' <param name="Value">An optional defualt value to write</param>
    '        ''' <returns>SUCCESS or CADS_ERRORCODE</returns>
    '        ''' <remarks>V1.0  D. Robinson</remarks>
    '        Public Function AddString(ByVal SettingName As String, Optional ByVal Value As String = "") As Int32
    '            Try
    '                Dim ConfigNode As XPathNavigator = Nothing
    '                Dim FileOpenResult As Int32

    '                FileOpenResult = OpenExternalConfigurationFile(ConfigNode)

    '                If FileOpenResult = SUCCESS Then

    '                    ConfigNode.AppendChildElement("", SettingName, Nothing, Value)
    '                    m_ConfigDoc.Save(m_Filename)

    '                    ConfigNode = Nothing

    '                    Return SUCCESS
    '                Else
    '                    Return FileOpenResult
    '                End If

    '            Catch ex As Exception
    '                Return m_Variables.ErrorResult(ERRORTYPE.CONFIG, SETTING_CREATE_ERROR)     ' Error Reading Configuration information of type string
    '            End Try

    '        End Function

    '        ''' <summary>
    '        ''' Reads the given String value
    '        ''' </summary>
    '        ''' <param name="SettingName">The Setting name</param>
    '        ''' <param name="ReturnValue">The value</param>
    '        ''' <param name="DefaultValue">The default value to use</param>
    '        ''' <returns>SUCCESS or CADS_ERRORCODE</returns>
    '        ''' <remarks>V1.0  D. Robinson</remarks>
    '        Public Function ReadString(ByVal SettingName As String, ByRef ReturnValue As String, Optional ByVal DefaultValue As String = CADS.Functions.DefaultStringValue) As Int32
    '            Try
    '                Dim ConfigNode As XPathNavigator = Nothing
    '                Dim FileOpenResult As Int32
    '                Dim StatusOfGet As Int32 = 0

    '                FileOpenResult = OpenExternalConfigurationFile(ConfigNode)

    '                If FileOpenResult = SUCCESS Then
    '                    Dim SettingValue As String = ""
    '                    StatusOfGet = GetValue(ConfigNode, SettingName, SettingValue)

    '                    If StatusOfGet = SUCCESS Then
    '                        ConfigNode = Nothing

    '                        ReturnValue = SettingValue
    '                        Return SUCCESS
    '                    Else
    '                        ReturnValue = DefaultValue
    '                        Return StatusOfGet
    '                    End If

    '                Else
    '                    Return FileOpenResult ' Error opening External Configuration file
    '                End If
    '            Catch ex As Exception
    '                Return m_Variables.ErrorResult(ERRORTYPE.CONFIG, STRING_READ_ERROR)     ' Error Reading Configuration information of type string
    '            End Try

    '        End Function

    '        ''' <summary>
    '        ''' Reads the given Integer value
    '        ''' </summary>
    '        ''' <param name="SettingName">The Setting name</param>
    '        ''' <param name="ReturnValue">The value</param>
    '        ''' <param name="DefaultValue">The default value to use</param>
    '        ''' <returns>SUCCESS or CADS_ERRORCODE</returns>
    '        ''' <remarks>V1.0  D. Robinson</remarks>
    '        Public Function ReadInteger(ByVal SettingName As String, ByRef ReturnValue As Integer, Optional ByVal DefaultValue As Integer = CADS.Functions.DefaultIntegerValue) As Int32
    '            Dim ReturnResult As Int32

    '            Dim ConvertedReturnValue As String = Convert.ToString(ReturnValue)
    '            Dim ConvertedDefaultValue As String = Convert.ToString(DefaultValue)

    '            Try
    '                ReturnResult = ReadString(SettingName, ConvertedReturnValue, ConvertedDefaultValue)

    '                If ReturnResult = SUCCESS Then
    '                    Try
    '                        ReturnValue = Convert.ToInt32(ConvertedReturnValue)
    '                        Return SUCCESS
    '                    Catch ex As Exception
    '                        Return m_Variables.ErrorResult(ERRORTYPE.CONFIG, INTEGER_EXPECTED_ERROR) ' Error Reading Configuration.  Integer expected but not found
    '                    End Try
    '                Else
    '                    ReturnValue = DefaultValue
    '                    Return ReturnResult ' Error Reading Configuration.  Setting not found
    '                End If
    '            Catch
    '                ReturnValue = DefaultValue
    '                Return m_Variables.ErrorResult(ERRORTYPE.CONFIG, INTEGER_READ_ERROR) ' Error Reading Configuration information of type integer
    '            End Try

    '        End Function

    '        ''' <summary>
    '        ''' Writes the given String value
    '        ''' </summary>
    '        ''' <param name="SettingName">The Setting name</param>
    '        ''' <param name="Value">The value</param>
    '        ''' <returns>SUCCESS or CADS_ERRORCODE</returns>
    '        ''' <remarks>V1.0  D. Robinson</remarks>
    '        Public Function WriteString(ByVal SettingName As String, ByVal Value As String) As Int32
    '            Try
    '                Dim ConfigNode As XPathNavigator = Nothing
    '                Dim FileOpenResult As Int32
    '                Dim StatusOfSet As Int32 = 0

    '                FileOpenResult = OpenExternalConfigurationFile(ConfigNode)

    '                If FileOpenResult = SUCCESS Then
    '                    StatusOfSet = SetValue(ConfigNode, SettingName, Value)

    '                    If StatusOfSet = SUCCESS Then
    '                        ConfigNode = Nothing

    '                        Return SUCCESS
    '                    Else
    '                        Return StatusOfSet
    '                    End If

    '                Else
    '                    Return FileOpenResult ' Error opening External Configuration file
    '                End If
    '            Catch ex As Exception
    '                Return m_Variables.ErrorResult(ERRORTYPE.CONFIG, STRING_WRITE_ERROR)     ' Error Writing Configuration information of type string
    '            End Try
    '        End Function

    '        ''' <summary>
    '        ''' Writes the given Integer value
    '        ''' </summary>
    '        ''' <param name="SettingName">The Setting name</param>
    '        ''' <param name="Value">The value</param>
    '        ''' <returns>SUCCESS or CADS_ERRORCODE</returns>
    '        ''' <remarks>V1.0  D. Robinson</remarks>
    '        Public Function WriteInteger(ByVal SettingName As String, ByVal Value As Int32) As Int32
    '            Try
    '                Dim ConfigNode As XPathNavigator = Nothing
    '                Dim FileOpenResult As Int32
    '                Dim StatusOfSet As Int32 = 0

    '                FileOpenResult = OpenExternalConfigurationFile(ConfigNode)

    '                If FileOpenResult = SUCCESS Then
    '                    StatusOfSet = SetValue(ConfigNode, SettingName, Value.ToString)

    '                    If StatusOfSet = SUCCESS Then
    '                        ConfigNode = Nothing

    '                        Return SUCCESS
    '                    Else
    '                        Return StatusOfSet
    '                    End If

    '                Else
    '                    Return FileOpenResult ' Error opening External Configuration file
    '                End If
    '            Catch ex As Exception
    '                Return m_Variables.ErrorResult(ERRORTYPE.CONFIG, INTEGER_WRITE_ERROR)     ' Error Writing Configuration information of type Integer
    '            End Try
    '        End Function

    '        ''' <summary>
    '        ''' Deletes the given setting
    '        ''' </summary>
    '        ''' <param name="SettingName">The Setting name</param>
    '        ''' <returns>SUCCESS or CADS_ERRORCODE</returns>
    '        ''' <remarks>V1.0  D. Robinson</remarks>
    '        Public Function DeleteString(ByVal SettingName As String) As Int32
    '            Dim ReturnValue As Int32 = 0

    '            Try
    '                Dim ConfigNode As XPathNavigator = Nothing
    '                Dim FileOpenResult As Int32
    '                Dim StatusOfGet As Int32 = 0

    '                FileOpenResult = OpenExternalConfigurationFile(ConfigNode)

    '                If FileOpenResult = SUCCESS Then
    '                    Dim SettingValue As String = ""
    '                    StatusOfGet = GetValue(ConfigNode, SettingName, SettingValue)

    '                    If StatusOfGet = SUCCESS Then
    '                        ConfigNode.SelectSingleNode(SettingName)
    '                        ConfigNode.DeleteSelf()
    '                        Return SUCCESS
    '                    Else
    '                        Return StatusOfGet
    '                    End If
    '                Else
    '                    Return FileOpenResult ' Error opening External Configuration file
    '                End If
    '            Catch ex As Exception
    '                Return m_Variables.ErrorResult(ERRORTYPE.CONFIG, SETTING_DELETE_ERROR)     ' Error Reading Configuration information of type string
    '            End Try
    '        End Function

    '#End Region

    '#Region " Private Methods "

    '        ''' <summary>
    '        ''' Opens the External Configuration file
    '        ''' </summary>
    '        ''' <param name="Navigator">XPathNavigator</param>
    '        ''' <returns>SUCCESS or CADS_ERRORCODE</returns>
    '        ''' <remarks>V1.0  D. Robinson</remarks>
    '        Private Function OpenExternalConfigurationFile(ByRef Navigator As XPathNavigator) As Int32
    '            Try
    '                m_ConfigDoc.Load(m_Filename)

    '                Dim Nav As XPathNavigator = m_ConfigDoc.CreateNavigator()
    '                Dim ConfigNode As XPathNavigator = Nav.SelectSingleNode("/configuration")

    '                Navigator = ConfigNode.Clone

    '                ConfigNode = Nothing
    '                Nav = Nothing

    '                Return SUCCESS
    '            Catch ex As DirectoryNotFoundException
    '                Return m_Variables.ErrorResult(ERRORTYPE.FILESYSTEM, DIRECTORY_NOT_FOUND) ' Directory not found Error opening External Configuration file
    '            Catch ex As FileNotFoundException
    '                Return m_Variables.ErrorResult(ERRORTYPE.FILESYSTEM, FILE_NOT_FOUND) ' File not found Error opening External Configuration file
    '            End Try
    '        End Function

    '        ''' <summary>
    '        ''' Reads the value from the configuration file
    '        ''' </summary>
    '        ''' <param name="Node">XPathNavigator</param>
    '        ''' <param name="Name">The Setting name</param>
    '        ''' <param name="Result">The value</param>
    '        ''' <returns>SUCCESS or CADS_ERRORCODE</returns>
    '        ''' <remarks>V1.0  D. Robinson</remarks>
    '        Private Function GetValue(ByVal Node As XPathNavigator, ByVal Name As String, ByRef Result As String) As Int32
    '            Dim ReturnValue As Int32 = 0
    '            Dim ResultOfMove As Boolean = False
    '            Dim FunctionResult As Int32 = 0

    '            Node.SelectSingleNode(Name)
    '            ResultOfMove = Node.MoveToChild(Name, "")
    '            If ResultOfMove = True Then
    '                Result = Node.Value
    '                ReturnValue = SUCCESS
    '            Else  ' Unsuccessful
    '                Result = ""
    '                ReturnValue = m_Variables.ErrorResult(ERRORTYPE.CONFIG, SETTING_NOT_FOUND) ' Error Reading Configuration.  Setting not found
    '            End If

    '            Return ReturnValue
    '        End Function

    '        ''' <summary>
    '        ''' Writes the value to the configuration file
    '        ''' </summary>
    '        ''' <param name="Node">XPathNavigator</param>
    '        ''' <param name="Name">The Setting name</param>
    '        ''' <param name="Value">The value</param>
    '        ''' <returns>SUCCESS or CADS_ERRORCODE</returns>
    '        ''' <remarks>V1.0  D. Robinson</remarks>
    '        Private Function SetValue(ByVal Node As XPathNavigator, ByVal Name As String, ByVal Value As String) As Int32
    '            Dim ReturnValue As Int32 = 0
    '            Dim ResultOfMove As Boolean = False
    '            Dim FunctionResult As Int32 = 0

    '            Node.SelectSingleNode(Name)
    '            ResultOfMove = Node.MoveToChild(Name, "")
    '            If ResultOfMove = True Then
    '                ' Save the value to this node
    '                Try
    '                    Node.SetValue(Value)
    '                    m_ConfigDoc.Save(m_Filename)
    '                    ReturnValue = SUCCESS
    '                Catch ex As Exception
    '                    ReturnValue = m_Variables.ErrorResult(ERRORTYPE.CONFIG, WRITE_NOT_SUPPORTED) ' Error Writing Configuration.  Writes not supported
    '                End Try
    '            Else  ' Unsuccessful
    '                Try
    '                    Node.AppendChild(Name)
    '                    Try
    '                        ResultOfMove = Node.MoveToChild(Name, "")
    '                        Node.SetValue(Value)
    '                    Catch ex As Exception
    '                        ReturnValue = m_Variables.ErrorResult(ERRORTYPE.CONFIG, WRITE_NOT_PERFORMED) ' Error Writing Configuration.  Could not save new value
    '                    End Try
    '                Catch ex As Exception
    '                    ReturnValue = m_Variables.ErrorResult(ERRORTYPE.CONFIG, WRITE_ERROR) ' Error Writing Configuration.  Could not append setting
    '                End Try
    '            End If

    '            Return ReturnValue
    '        End Function

    '#End Region

End Class

