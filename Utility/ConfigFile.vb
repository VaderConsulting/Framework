Imports System.IO
Imports System.Xml.XPath
Imports System.Configuration
Imports Utility.Constants
Imports Utility.Types
Imports Utility.Types.ERRORTYPE
Imports Utility.Types.ERROR_CONFIG
Imports Utility.Types.ERROR_DIRECTORY
Imports Utility.Types.ERROR_FILESYSTEM

' External configuration files must be in the following format:
'
'<configuration>
'    <section1>   
'        <Setting1>Text</Setting1>
'        <Setting2>Other Text</Setting2>
'        <Setting3>New Value</Setting3>
'    </section1>
'    <section2>
'        <Setting1>Text</Setting1>
'        <Setting2>More Text</Setting2>
'        <Setting3>Yet another Value</Setting3>
'    </section2>
'</configuration>
'
' Section and setting names are arbitary
'

Public Class ConfigFile

    Private m_FilenameAndPath As String               ' Required
    Public DisplayName As String                      ' Optional
    Public Description As String                      ' Optional
    Public Settings As ConfigurationSettings          ' The actual settings
    Public Contents As New Collections.SortedList     ' Holds the list of settings
    Private ConfigDocument As New ConfigXmlDocument
    Private m_Functions As New Functions

    '''' <summary>
    '''' Instantiate new object, connecting to the given filename
    '''' </summary>
    '''' <param name="Filename">The External configuration file to open</param>
    '''' <remarks>V1.0  D. Robinson</remarks>
    'Public Sub New(ByVal Filename As String)
    '    m_FilenameAndPath = Filename
    'End Sub

#Region " Properties "

    ''' <summary>
    ''' The filename of the external configuration file
    ''' </summary>
    ''' <value>The filename to use</value>
    ''' <returns>The filename in use</returns>
    ''' <remarks>V1.0  D. Robinson</remarks>
    Public Property FilenameAndPath() As String
        Get
            Return m_FilenameAndPath
        End Get
        Set(ByVal value As String)
            m_FilenameAndPath = value
        End Set
    End Property

    'Public Property ApplicationStatus() As Status
    '    Get
    '        Return m_ApplicationStatus
    '    End Get
    '    Set(ByVal value As Status)
    '        m_ApplicationStatus = value
    '        SetStatus(ApplicationStatus)
    '    End Set
    'End Property

#End Region

#Region " Public Methods "

    Public Sub GetSettings(ByVal SectionName As String, Optional ByVal NameFilter As String = "*")
        Dim FileOpenResult As ERRORTYPE
        Dim ConfigNode As XPathNavigator = Nothing
        Dim SettingComplete As Boolean = False

        OpenExternalConfigurationFile(ConfigNode)

        FileOpenResult = Utility.GlobalSettings.LastStatus.CADS_ERRORCODE

        If FileOpenResult = SUCCESS Then
            If ConfigNode.HasChildren Then
                ConfigNode.MoveToFirstChild()
                Do
                    Debug.Print("Reading " & ConfigNode.Name)
                    If ConfigNode.Name Like SectionName Then
                        ConfigNode.MoveToFirstChild()
                        Do
                            If ConfigNode.Name Like (NameFilter) Then
                                Contents.Add(ConfigNode.Name, ConfigNode.Value)
                            End If
                        Loop Until ConfigNode.MoveToNext = False
                        SettingComplete = True
                    End If
                Loop Until (ConfigNode.MoveToNext = False Or SettingComplete = True) ' <---------------- TEST!!!!!
            End If
        Else
            ' LastStatus now contains the necessary information for us to determine the cause of the problem.
        End If

    End Sub

    ''' <summary>
    ''' Adds the given setting
    ''' </summary>
    ''' <param name="SettingName">The setting to add</param>
    ''' <param name="Value">An optional defualt value to write</param>
    ''' <remarks>V1.0  D. Robinson</remarks>
    Public Sub AddString(ByVal SectionName As String, ByVal SettingName As String, Optional ByVal Value As String = "")
        Try
            Dim ConfigNode As XPathNavigator = Nothing
            Dim FileOpenResult As Int32

            OpenExternalConfigurationFile(ConfigNode)

            FileOpenResult = GlobalSettings.LastStatus.CADS_ERRORCODE

            If FileOpenResult = SUCCESS Then

                ConfigNode.AppendChildElement("", SettingName, Nothing, Value)
                ConfigDocument.Save(m_FilenameAndPath)

                ConfigNode = Nothing
                LastStatus.Clear()
                LastStatus.CADS_ERRORCODE = SUCCESS
            Else
                LastStatus.Clear()
                LastStatus.CADS_ERRORCODE = FileOpenResult
            End If

        Catch ex As Exception
            LastStatus.Clear()
            LastStatus.CADS_ERRORCODE = m_Functions.ErrorResult(ERRORTYPE.CONFIG, ERROR_CONFIG.SETTING_CREATE_ERROR)
            LastStatus.LastException = ex
        End Try

    End Sub

    ''' <summary>
    ''' Reads the given String value
    ''' </summary>
    ''' <param name="SettingName">The Setting name</param>
    ''' <param name="DefaultValue">The default value to use</param>
    ''' <returns>The value</returns>
    ''' <remarks>V1.0  D. Robinson</remarks>
    Public Function ReadString(ByVal SectionName As String, ByVal SettingName As String, Optional ByVal DefaultValue As String = DEFAULTSTRINGVALUE) As String
        Dim ReturnValue As String = DefaultValue

        Try
            Dim ConfigNode As XPathNavigator = Nothing
            Dim FileOpenResult As Int32
            Dim StatusOfGet As Int32 = 0

            OpenExternalConfigurationFile(ConfigNode)

            FileOpenResult = GlobalSettings.LastStatus.CADS_ERRORCODE

            If FileOpenResult = SUCCESS Then
                Dim SettingValue As String = ""
                SettingValue = GetValue(ConfigNode, SettingName)
                StatusOfGet = LastStatus.CADS_ERRORCODE

                If StatusOfGet = SUCCESS Then
                    ConfigNode = Nothing

                    ReturnValue = SettingValue
                    LastStatus.Clear()
                    LastStatus.CADS_ERRORCODE = SUCCESS
                    Return ReturnValue
                Else
                    ReturnValue = DefaultValue
                    LastStatus.CADS_ERRORCODE = StatusOfGet
                    Return ReturnValue
                End If

            Else
                LastStatus.CADS_ERRORCODE = FileOpenResult
                Return "" ' Error opening External Configuration file
            End If
        Catch ex As Exception
            LastStatus.LastException = ex
            LastStatus.CADS_ERRORCODE = m_Functions.ErrorResult(ERRORTYPE.CONFIG, STRING_READ_ERROR)     ' Error Reading Configuration information of type string
            Return ""
        End Try

    End Function

    ''' <summary>
    ''' Reads the given Integer value
    ''' </summary>
    ''' <param name="SettingName">The Setting name</param>
    ''' <param name="DefaultValue">The default value to use</param>
    ''' <returns>The Value</returns>
    ''' <remarks>V1.0  D. Robinson</remarks>
    Public Function ReadInteger(ByVal SectionName As String, ByVal SettingName As String, Optional ByVal DefaultValue As Integer = DEFAULTINTEGERVALUE) As Integer
        Dim ReturnValue As Integer
        Dim ReturnResult As String

        Dim ConvertedReturnValue As String = Convert.ToString(ReturnValue)
        Dim ConvertedDefaultValue As String = Convert.ToString(DefaultValue)

        Try
            ReturnResult = ReadString(SettingName, ConvertedDefaultValue)

            If GlobalSettings.LastStatus.CADS_ERRORCODE = SUCCESS Then
                Try
                    ReturnValue = Convert.ToInt32(ConvertedReturnValue)
                    LastStatus.CADS_ERRORCODE = SUCCESS
                    Return ReturnValue
                Catch ex As Exception
                    LastStatus.CADS_ERRORCODE = m_Functions.ErrorResult(ERRORTYPE.CONFIG, INTEGER_EXPECTED_ERROR) ' Error Reading Configuration.  Integer expected but not found
                    LastStatus.LastException = ex
                    Return DEFAULTINTEGERVALUE
                End Try
            Else
                ReturnValue = DefaultValue
                Return ReturnResult ' Error Reading Configuration.  Setting not found
            End If
        Catch
            ReturnValue = DefaultValue
            LastStatus.CADS_ERRORCODE = m_Functions.ErrorResult(ERRORTYPE.CONFIG, INTEGER_READ_ERROR) ' Error Reading Configuration information of type integer
            Return ReturnValue
        End Try

    End Function

    ''' <summary>
    ''' Writes the given String value
    ''' </summary>
    ''' <param name="SettingName">The Setting name</param>
    ''' <remarks>V1.0  D. Robinson</remarks>
    Public Sub WriteString(ByVal SectionName As String, ByVal SettingName As String, ByVal Value As String)
        Try
            Dim ConfigNode As XPathNavigator = Nothing
            Dim FileOpenResult As Int32
            Dim StatusOfSet As Int32 = 0

            OpenExternalConfigurationFile(ConfigNode)
            FileOpenResult = GlobalSettings.LastStatus.CADS_ERRORCODE

            If FileOpenResult = SUCCESS Then
                SetValue(ConfigNode, SettingName, Value)
                StatusOfSet = LastStatus.CADS_ERRORCODE

                If StatusOfSet = SUCCESS Then
                    ConfigNode = Nothing
                    LastStatus.CADS_ERRORCODE = SUCCESS
                Else
                    LastStatus.CADS_ERRORCODE = StatusOfSet
                End If
            Else
                LastStatus.CADS_ERRORCODE = FileOpenResult ' Error opening External Configuration file
            End If
        Catch ex As Exception
            LastStatus.LastException = ex
            LastStatus.CADS_ERRORCODE = m_Functions.ErrorResult(ERRORTYPE.CONFIG, STRING_WRITE_ERROR)     ' Error Writing Configuration information of type string
        End Try

    End Sub

    ''' <summary>
    ''' Writes the given Integer value
    ''' </summary>
    ''' <param name="SettingName">The Setting name</param>
    ''' <param name="Value">The value</param>
    ''' <remarks>V1.0  D. Robinson</remarks>
    Public Sub WriteInteger(ByVal SectionName As String, ByVal SettingName As String, ByVal Value As Int32)
        Try
            Dim ConfigNode As XPathNavigator = Nothing
            Dim FileOpenResult As Int32
            Dim StatusOfSet As Int32 = 0

            OpenExternalConfigurationFile(ConfigNode)

            FileOpenResult = GlobalSettings.LastStatus.CADS_ERRORCODE

            If FileOpenResult = SUCCESS Then
                SetValue(ConfigNode, SettingName, Value.ToString)
                StatusOfSet = LastStatus.CADS_ERRORCODE

                If StatusOfSet = SUCCESS Then
                    ConfigNode = Nothing
                    LastStatus.CADS_ERRORCODE = SUCCESS
                Else
                    LastStatus.CADS_ERRORCODE = StatusOfSet
                End If

            Else
                LastStatus.CADS_ERRORCODE = FileOpenResult ' Error opening External Configuration file
            End If
        Catch ex As Exception
            LastStatus.LastException = ex
            LastStatus.CADS_ERRORCODE = m_Functions.ErrorResult(ERRORTYPE.CONFIG, INTEGER_WRITE_ERROR)     ' Error Writing Configuration information of type Integer
        End Try

    End Sub

    ''' <summary>
    ''' Deletes the given setting
    ''' </summary>
    ''' <param name="SettingName">The Setting name</param>
    ''' <remarks>V1.0  D. Robinson</remarks>
    Public Sub DeleteString(ByVal SectionName As String, ByVal SettingName As String)
        Try
            Dim ConfigNode As XPathNavigator = Nothing
            Dim FileOpenResult As Int32
            Dim StatusOfGet As Int32 = 0

            OpenExternalConfigurationFile(ConfigNode)

            FileOpenResult = GlobalSettings.LastStatus.CADS_ERRORCODE

            If FileOpenResult = SUCCESS Then
                Dim SettingValue As String = ""
                SettingValue = GetValue(ConfigNode, SettingName)
                StatusOfGet = LastStatus.CADS_ERRORCODE

                If StatusOfGet = SUCCESS Then
                    ConfigNode.SelectSingleNode(SettingName)
                    ConfigNode.DeleteSelf()
                    LastStatus.CADS_ERRORCODE = SUCCESS
                Else
                    LastStatus.CADS_ERRORCODE = StatusOfGet
                End If
            Else
                LastStatus.CADS_ERRORCODE = FileOpenResult ' Error opening External Configuration file
            End If
        Catch ex As Exception
            LastStatus.LastException = ex
            LastStatus.CADS_ERRORCODE = m_Functions.ErrorResult(ERRORTYPE.CONFIG, SETTING_DELETE_ERROR)     ' Error Reading Configuration information of type string
        End Try

    End Sub

#End Region

#Region " Private Methods "

    ''' <summary>
    ''' Opens the External Configuration file
    ''' </summary>
    ''' <param name="Navigator">XPathNavigator</param>
    ''' <remarks>V1.0  D. Robinson</remarks>
    Private Sub OpenExternalConfigurationFile(ByRef Navigator As XPathNavigator)
        If IO.File.Exists(m_FilenameAndPath) Then
            Try
                ConfigDocument.Load(m_FilenameAndPath)

                Dim Nav As XPathNavigator = ConfigDocument.CreateNavigator()
                Dim ConfigNode As XPathNavigator = Nav.SelectSingleNode("/configuration")

                Navigator = ConfigNode.Clone

                ConfigNode = Nothing
                Nav = Nothing

                LastStatus.CADS_ERRORCODE = SUCCESS
            Catch ex As DirectoryNotFoundException
                LastStatus.LastException = ex
                LastStatus.CADS_ERRORCODE = m_Functions.ErrorResult(ERRORTYPE.FILESYSTEM, DIRECTORY_NOT_FOUND) ' Directory not found Error opening External Configuration file
            Catch ex As FileNotFoundException
                LastStatus.LastException = ex
                LastStatus.CADS_ERRORCODE = m_Functions.ErrorResult(ERRORTYPE.FILESYSTEM, FILE_NOT_FOUND) ' File not found Error opening External Configuration file
            End Try
        Else
            LastStatus.CADS_ERRORCODE = m_Functions.ErrorResult(ERRORTYPE.FILESYSTEM, FILE_NOT_FOUND) ' File not found Error opening External Configuration file
        End If
    End Sub

    ''' <summary>
    ''' Reads the value from the configuration file
    ''' </summary>
    ''' <param name="Node">XPathNavigator</param>
    ''' <param name="Name">The Setting name</param>
    ''' <returns>The Value</returns>
    ''' <remarks>V1.0  D. Robinson</remarks>
    Private Function GetValue(ByVal Node As XPathNavigator, ByVal Name As String) As String
        Dim ResultOfMove As Boolean = False
        Dim Result As String

        Node.SelectSingleNode(Name)
        ResultOfMove = Node.MoveToChild(Name, "")
        If ResultOfMove = True Then
            Result = Node.Value
            LastStatus.CADS_ERRORCODE = SUCCESS
        Else  ' Unsuccessful
            Result = ""
            LastStatus.CADS_ERRORCODE = m_Functions.ErrorResult(ERRORTYPE.CONFIG, SETTING_NOT_FOUND) ' Error Reading Configuration.  Setting not found
        End If

        Return Result
    End Function

    ''' <summary>
    ''' Writes the value to the configuration file
    ''' </summary>
    ''' <param name="Node">XPathNavigator</param>
    ''' <param name="Name">The Setting name</param>
    ''' <param name="Value">The value</param>
    ''' <remarks>V1.0  D. Robinson</remarks>
    Private Sub SetValue(ByVal Node As XPathNavigator, ByVal Name As String, ByVal Value As String)
        Dim ResultOfMove As Boolean = False

        Node.SelectSingleNode(Name)
        ResultOfMove = Node.MoveToChild(Name, "")
        If ResultOfMove = True Then
            ' Save the value to this node
            Try
                Node.SetValue(Value)
                ConfigDocument.Save(m_FilenameAndPath)
                LastStatus.CADS_ERRORCODE = SUCCESS
            Catch ex As Exception
                LastStatus.CADS_ERRORCODE = m_Functions.ErrorResult(ERRORTYPE.CONFIG, WRITE_NOT_SUPPORTED) ' Error Writing Configuration.  Writes not supported
            End Try
        Else  ' Unsuccessful
            Try
                Node.AppendChild(Name)
                Try
                    ResultOfMove = Node.MoveToChild(Name, "")
                    Node.SetValue(Value)
                Catch ex As Exception
                    LastStatus.LastException = ex
                    LastStatus.CADS_ERRORCODE = m_Functions.ErrorResult(ERRORTYPE.CONFIG, WRITE_NOT_PERFORMED) ' Error Writing Configuration.  Could not save new value
                End Try
            Catch ex As Exception
                LastStatus.LastException = ex
                LastStatus.CADS_ERRORCODE = m_Functions.ErrorResult(ERRORTYPE.CONFIG, WRITE_ERROR) ' Error Writing Configuration.  Could not append setting
            End Try
        End If

    End Sub

#End Region

End Class

