Imports System.IO
Imports System.Net.Mail

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
Imports Utility.Types.LOGGING_LEVEL

''' <summary>
''' Provides logging functionality to email, logfile and the Application Eventlog
''' </summary>
''' <remarks></remarks>
Public Class Logging

#Region " Constructor "

    ''' <summary>
    ''' Creates a new instance, writing to the EventLog
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        If (m_LogType And LOGGING_TYPE.COMPUTER_EVENT_LOG) Then ' If configured to use the Event Log then ensure an Event Source has been created.
            CreateEventSource()
        End If
    End Sub

    ''' <summary>
    ''' Creates a new instance, writing to the specified Log type
    ''' </summary>
    ''' <param name="LogType"></param>
    ''' <remarks></remarks>
    Public Sub New(ByVal LogType As LOGGING_TYPE)
        m_LogType = LogType

        If m_LogType = LOGFILE Then
            If Not IO.Directory.Exists(m_AppDataPath & "\Logfiles") Then
                Try
                    IO.Directory.CreateDirectory(m_AppDataPath & "\Logfiles")
                Catch
                End Try

            End If
        End If

        If LogType = LOGGING_TYPE.COMPUTER_EVENT_LOG Then CreateEventSource() ' If configured to use the Event Log then ensure an Event Source has been created.
    End Sub

    ''' <summary>
    ''' Creates a new instance, writing to a file
    ''' </summary>
    ''' <param name="Filename"></param>
    ''' <remarks></remarks>
    Public Sub New(ByVal Filename As String)
        m_LogType = LOGGING_TYPE.LogFile
        m_LogFilename = Filename

        If System.IO.File.Exists(Filename) Then

        End If
    End Sub

    ''' <summary>
    ''' Creates a new instance, sending output to an email
    ''' </summary>
    ''' <param name="EmailTo">The SMTP address of the recipient</param>
    ''' <param name="EmailFrom">The SMTP address of the sender</param>
    ''' <param name="EmailSubject">The Email subject</param>
    ''' <param name="SMTPServerIPorName">The IP Address or Name of the SMTP Server</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal EmailTo As String, ByVal EmailFrom As String, ByVal EmailSubject As String, ByVal SMTPServerIPorName As String)
        m_EmailFrom = EmailFrom
        m_EmailTo = EmailTo
        m_EmailSubject = EmailSubject
        m_SMTPServerIPorName = SMTPServerIPorName
    End Sub

#End Region

#Region " Private Variables "

    Private m_EventLog As EventLog = Nothing
    Private m_EmailSubject As String = _
            My.Application.Info.ProductName & _
            " Logged message"
    Private m_EmailTo As String = ""                                  ' My.Settings.EmailTo
    Private m_SMTPServerIPorName As String = ""                       ' My.Settings.SMTPServerIPorName
    Private m_EmailFrom As String = ""                                ' My.Settings.EmailFrom
    Private m_LogLevel As LOGGING_LEVEL = LOGGING_LEVEL.ERRORS        ' LoggingLevel.Errors + LoggingLevel.Warning
    Private m_LogType As LOGGING_TYPE = LOGGING_TYPE.LogFile          ' LoggingType.Email | LoggingType.EventLog | LoggingType.File
    Private m_AppDataPath As String = My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData
    Private m_LogFilename As String = _
            m_AppDataPath & "\" & _
            Date.Now.Year.ToString.PadLeft(4, "0") & "-" & _
            Date.Now.Month.ToString.PadLeft(2, "0") & "-" & _
            Date.Now.Day.ToString.PadLeft(2, "0") & _
            "-ApplicationLog.txt"


#End Region

#Region " Public Properties "

    ''' <summary>
    ''' The Log Type
    ''' </summary>
    ''' <value>LoggingType.Email | LoggingType.EventLog | LoggingType.File</value>
    ''' <returns>Integer</returns>
    ''' <remarks>V1.0 D. Robinson</remarks>
    Public Property LogType() As Integer
        Get
            Return m_LogType
        End Get
        Set(ByVal value As Integer)
            m_LogType = value
        End Set
    End Property

    ''' <summary>
    ''' The LogLevel of the message
    ''' </summary>
    ''' <value>The LogLevel to use</value>
    ''' <returns>The LogLevel in use</returns>
    ''' <remarks>V1.0 D. Robinson</remarks>
    Public Property LogLevel() As LOGGING_LEVEL
        Get
            Return m_LogLevel
        End Get
        Set(ByVal value As LOGGING_LEVEL)
            m_LogLevel = value
        End Set
    End Property

    ''' <summary>
    ''' The subject of the Email
    ''' </summary>
    ''' <value>The subject to use</value>
    ''' <returns>The subject in use</returns>
    ''' <remarks>V1.0 D. Robinson</remarks>
    Public Property EmailSubject() As String
        Get
            Return m_EmailSubject
        End Get
        Set(ByVal value As String)
            m_EmailSubject = value
        End Set
    End Property

    ''' <summary>
    ''' The email address of the sender
    ''' </summary>
    ''' <value>The email address to use</value>
    ''' <returns>The email address in use</returns>
    ''' <remarks>V1.0 D. Robinson</remarks>
    Public Property EmailFrom() As String
        Get
            Return m_EmailFrom
        End Get
        Set(ByVal value As String)
            m_EmailFrom = value
        End Set
    End Property

    ''' <summary>
    ''' The email address of the recipient
    ''' </summary>
    ''' <value>The email address to use</value>
    ''' <returns>The email address in use</returns>
    ''' <remarks>V1.0 D. Robinson</remarks>
    Public Property EmailTo() As String
        Get
            Return m_EmailTo
        End Get
        Set(ByVal value As String)
            m_EmailTo = value
        End Set
    End Property

    ''' <summary>
    ''' The SMTP Server name or IP Address
    ''' </summary>
    ''' <value>The SMTP Server or IP Address to use</value>
    ''' <returns>The SMTP Server or IP Address in use</returns>
    ''' <remarks>V1.0 D. Robinson</remarks>
    Public Property SMTPServerIPorName() As String
        Get
            Return m_SMTPServerIPorName
        End Get
        Set(ByVal value As String)
            m_SMTPServerIPorName = value
        End Set
    End Property

    ''' <summary>
    ''' The filename of the log
    ''' </summary>
    ''' <value>The Filename to use</value>
    ''' <returns>The Filename in use</returns>
    ''' <remarks>V1.0 D. Robinson</remarks>
    Public ReadOnly Property Filename() As String
        Get
            Return m_LogFilename
        End Get
    End Property

#End Region

#Region " Public Methods "

    ''' <summary>
    ''' Set the logging filename
    ''' </summary>
    ''' <param name="LogFilenameAndPath">The filename to use</param>
    ''' <remarks>V1.0 D. Robinson</remarks>
    Public Sub SetFilename(ByVal LogFilenameAndPath As String)
        If LogFilenameAndPath.Length > 0 Then
            m_LogFilename = LogFilenameAndPath
        End If
    End Sub

    ''' <summary>
    ''' Write an Information Event
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <returns>Boolean indicating Success (True) or Failure (False)</returns>
    ''' <remarks>V1.0 D. Robinson</remarks>
    Public Function WriteInformationEvent(ByVal Message As String) As Boolean
        Return WriteEvent(Message, INFORMATION)
    End Function

    ''' <summary>
    ''' Write a Warning Event
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <returns>Boolean indicating Success (True) or Failure (False)</returns>
    ''' <remarks></remarks>
    Public Function WriteWarningEvent(ByVal Message As String) As Boolean
        Return WriteEvent(Message, WARNING)
    End Function

    ''' <summary>
    ''' Write an Error Event
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <returns>Boolean indicating Success (True) or Failure (False)</returns>
    ''' <remarks></remarks>
    Public Function WriteErrorEvent(ByVal Message As String) As Boolean
        Return WriteEvent(Message, ERRORS)
    End Function

    ''' <summary>
    ''' Write a Debug Event
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <returns>Boolean indicating Success (True) or Failure (False)</returns>
    ''' <remarks></remarks>
    Public Function WriteDebugEvent(ByVal Message As String) As Boolean
        Return WriteEvent(Message, DEBUGGING)
    End Function

    ''' <summary>
    ''' Write an Event of the specified EventLogEntryType
    ''' </summary>
    ''' <param name="Message">The event text</param>
    ''' <param name="Category">The EventLogEntryType</param>
    ''' <returns>Boolean indicating Success (True) or Failure (False)</returns>
    ''' <remarks></remarks>
    Public Function WriteEvent(ByVal Message As String, ByVal Category As LOGGING_LEVEL) As Boolean
        Dim OKToSubmitEvent As Boolean = False

        Try
            ' First determine if we are configured to 'write' this event
            'Select Case Category
            '    Case Errors
            '        If (m_LogLevel And Errors) Or (m_LogLevel And Debugging) Then OKToSubmitEvent = True
            '    Case Information
            '        If (m_LogLevel And Information) Or (m_LogLevel And Debugging) Then OKToSubmitEvent = True
            '    Case Warning
            '        If (m_LogLevel And WARNING) Or (m_LogLevel And DEBUGGING) Then OKToSubmitEvent = True
            '    Case DEBUGGING
            '        If Command.ToString.ToLower.Contains("debug") Then OKToSubmitEvent = True
            'End Select

            If Category <= m_LogLevel Then OKToSubmitEvent = True
            'If m_LogLevel = Then

            If OKToSubmitEvent Then ' Yes, ok to 'write' the event
                If (m_LogType And LOGGING_TYPE.EMAIL) Then ' Send Email
                    SendEmail(Message, Category)
                End If

                If (m_LogType And LOGGING_TYPE.COMPUTER_EVENT_LOG) Then ' Add event to Event Log
                    WriteEventLogEntry(Message, Category)
                End If

                If (m_LogType And LOGGING_TYPE.LOGFILE) Then ' Log to a file
                    WriteToLogFile(Message, Category)
                End If
            End If

            Return True
        Catch
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Write to the Application logfile
    ''' </summary>
    ''' <param name="Message">The event text</param>
    ''' <param name="Category">The EventLogEntryType</param>
    ''' <remarks></remarks>
    Public Sub WriteToLogFile(ByVal Message As String, ByVal Category As LOGGING_LEVEL)
        Dim TimeString As String = Date.Now.Hour.ToString.PadLeft(2, "0") & ":" & Date.Now.Minute.ToString.PadLeft(2, "0") & ":" & Date.Now.Second.ToString.PadLeft(2, "0")

        Dim ApplicationName As String = My.Application.Info.AssemblyName.PadRight(20)

        Try
            Dim oStream As New StreamWriter(m_LogFilename, True)

            Select Case Category
                Case Errors
                    oStream.Write(TimeString & " !!!!! ERROR " & ApplicationName & " :")
                Case Information
                    oStream.Write(TimeString & " INFORMATION " & ApplicationName & " :")
                Case Warning
                    oStream.Write(TimeString & " ??? WARNING " & ApplicationName & " :")
                Case DEBUGGING
                    oStream.Write(TimeString & " ***** DEBUG " & ApplicationName & " :")
            End Select

#If DEBUG Then
                Debug.Print("WriteToLogFile: " & Message)
#End If

            oStream.Write(Message & vbCrLf)
            oStream.Close()
        Catch ex As Exception
            ' Damn!  If we get an error in here, we can't use this message logging type
            WriteEventLogEntry("Error writing logfile.  The message was: " & ex.Message, EventLogEntryType.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Send an Email
    ''' </summary>
    ''' <param name="Body">The event text</param>
    ''' <param name="Category">The EventLogEntryType</param>
    ''' <remarks></remarks>
    Public Sub SendEmail(ByVal Body As String, ByVal Category As LOGGING_LEVEL)
        ' If mandatory settings aren't specified, exit silently
        If m_SMTPServerIPorName = "" Or m_EmailFrom = "" Or Body = "" Then Exit Sub

        Dim TimeString As String = String.Format(Date.Now.Hour, "00") & ":" & String.Format(Date.Now.Minute, "00") & ":" & String.Format(Date.Now.Second, "00")
        Dim CategoryString As String = ""

        Select Case Category
            Case Errors
                CategoryString = "Error message generated by " & My.Application.Info.ProductName.ToString & " at " & TimeString
            Case Information
                CategoryString = "Information message generated by " & My.Application.Info.ProductName.ToString & " at " & TimeString
            Case Warning
                CategoryString = "Warning message generated by " & My.Application.Info.ProductName.ToString & " at " & TimeString
        End Select

        Body = CategoryString & vbCrLf & vbCrLf & Body

        Dim MailClient As New SmtpClient(m_SMTPServerIPorName)
        Dim Message As New MailMessage(m_EmailFrom, m_EmailTo, m_EmailSubject, Body)

        Try
            MailClient.Send(Message)
        Catch ex As Exception
            ' Damn!  If we get an error in here, we can't use this message logging type
            WriteToLogFile("Error sending Email.  The error message was: " & ex.Message, EventLogEntryType.Error)
        End Try

        Message = Nothing
        MailClient = Nothing

    End Sub

#End Region

#Region "Private Methods "

    Private Sub CreateEventSource()
        m_EventLog = New EventLog("Application")
        If Not EventLog.SourceExists(My.Application.Info.ProductName) Then
            Try
                EventLog.CreateEventSource(My.Application.Info.ProductName.ToString, m_EventLog.LogDisplayName)
            Catch ex As Exception
                ' Error whilst creating Event Source.  Do we have permissions to create the Event Source?
                WriteToLogFile("Error creating Event Source.  The error message was: " & ex.Message, EventLogEntryType.Error)
            End Try
        End If
    End Sub

    ''' <summary>
    ''' Write to the Application Event Log
    ''' </summary>
    ''' <param name="Message">The event text</param>
    ''' <param name="Category">The EventLogEntryType</param>
    ''' <remarks></remarks>
    Private Sub WriteEventLogEntry(ByVal Message As String, ByVal Category As EventLogEntryType)
        EventLog.WriteEntry(My.Application.Info.ProductName.ToString, Message, Category)
    End Sub

#End Region

End Class
