' ----------------------------------------------------------------------------------------
' Author:                    Dave Robinson
' Company:                   Stratatel
' Assembly version:          1.0.0.0
' Date:                      11/06/2008
' Purpose:                   Provides logging functionality
' ----------------------------------------------------------------------------------------

#Region " Imports "

Imports System.Net.Mail
Imports System.IO

#End Region

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
        If (m_LogType And LoggingType.EventLog) Then ' If configured to use the Event Log then ensure an Event Source has been created.
            CreateEventSource()
        End If
    End Sub

    ''' <summary>
    ''' Creates a new instance, writing to the specified Log type
    ''' </summary>
    ''' <param name="LogType"></param>
    ''' <remarks></remarks>
    Public Sub New(ByVal LogType As Logging.LoggingType)
        m_LogType = LogType

        If LogType = LoggingType.EventLog Then CreateEventSource() ' If configured to use the Event Log then ensure an Event Source has been created.
    End Sub

    ''' <summary>
    ''' Creates a new instance, writing to a file
    ''' </summary>
    ''' <param name="Filename"></param>
    ''' <remarks></remarks>
    Public Sub New(ByVal Filename As String)
        m_LogType = LoggingType.File
        m_LogFilename = Filename
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

#Region " Public Enums "

    ''' <summary>
    ''' Indicates the type of logging to employ
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum LoggingType
        File = 1
        Email = 2
        EventLog = 4
    End Enum

    ''' <summary>
    ''' Indicates the type of information to log
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum LoggingLevel
        Errors = 1        ' Will always be logged
        Warning = 2
        Information = 4
        Debugging = 255
    End Enum

#End Region

#Region " Private Variables "

    Private m_EventLog As EventLog = Nothing
    Private m_EmailSubject As String = My.Application.Info.ProductName & " Logged message"
    Private m_EmailTo As String = ""                                ' My.Settings.EmailTo
    Private m_SMTPServerIPorName As String = ""                     ' My.Settings.SMTPServerIPorName
    Private m_EmailFrom As String = ""                              ' My.Settings.EmailFrom
    Private m_LogLevel As LoggingLevel = LoggingLevel.Debugging     ' LoggingLevel.Errors + LoggingLevel.Warning
    Private m_LogType As LoggingType = LoggingType.File             ' LoggingType.Email | LoggingType.EventLog | LoggingType.File
    Private m_LogFilename As String = My.Application.Info.DirectoryPath & "\" & Format(Date.Now.Day, "00") & "-" & Format(Date.Now.Month, "00") & "-" & Format(Date.Now.Year, "00") & "-ApplicationLog.txt"

#End Region

#Region " Public Properties "

    ''' <summary>
    ''' The Log Type
    ''' </summary>
    ''' <value>LoggingType.Email | LoggingType.EventLog | LoggingType.File</value>
    ''' <returns>Integer</returns>
    ''' <remarks></remarks>
    Public Property LogType() As Integer
        Get
            Return m_LogType
        End Get
        Set(ByVal value As Integer)
            m_LogType = value
        End Set
    End Property

    Public Property LogLevel() As LoggingLevel
        Get
            Return m_LogLevel
        End Get
        Set(ByVal value As LoggingLevel)
            m_LogLevel = value
        End Set
    End Property

    ''' <summary>
    ''' The subject of the Email
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>String</returns>
    ''' <remarks></remarks>
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
    ''' <value>String</value>
    ''' <returns>String</returns>
    ''' <remarks></remarks>
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
    ''' <value>String</value>
    ''' <returns>String</returns>
    ''' <remarks></remarks>
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
    ''' <value>String</value>
    ''' <returns>String</returns>
    ''' <remarks></remarks>
    Public Property SMTPServerIPorName() As String
        Get
            Return m_SMTPServerIPorName
        End Get
        Set(ByVal value As String)
            m_SMTPServerIPorName = value
        End Set
    End Property

    Public ReadOnly Property Filename() As String
        Get
            Return m_LogFilename
        End Get
    End Property

#End Region

#Region " Public Methods "

    ''' <summary>
    ''' Write an Information Event
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function WriteInformationEvent(ByVal Message As String) As Boolean
        Return WriteEvent(Message, EventLogEntryType.Information)
    End Function

    ''' <summary>
    ''' Write a Warning Event
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function WriteWarningEvent(ByVal Message As String) As Boolean
        Return WriteEvent(Message, EventLogEntryType.Warning)
    End Function

    ''' <summary>
    ''' Write an Error Event
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function WriteErrorEvent(ByVal Message As String) As Boolean
        Return WriteEvent(Message, EventLogEntryType.Error)
    End Function

    ''' <summary>
    ''' Write an Event of the specified EventLogEntryType
    ''' </summary>
    ''' <param name="Message">The event text</param>
    ''' <param name="Category">The EventLogEntryType</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function WriteEvent(ByVal Message As String, ByVal Category As EventLogEntryType) As Boolean
        Dim OKToSubmitEvent As Boolean

        Try
            ' First determine if we are configured to 'write' this event
            Select Case Category
                Case EventLogEntryType.Error
                    If (m_LogLevel And LoggingLevel.Errors) Or (m_LogLevel And LoggingLevel.Debugging) Then OKToSubmitEvent = True
                Case EventLogEntryType.Information
                    If (m_LogLevel And LoggingLevel.Information) Or (m_LogLevel And LoggingLevel.Debugging) Then OKToSubmitEvent = True
                Case EventLogEntryType.Warning
                    If (m_LogLevel And LoggingLevel.Warning) Or (m_LogLevel And LoggingLevel.Debugging) Then OKToSubmitEvent = True
            End Select

            If OKToSubmitEvent Then ' Yes, ok to 'write' the event
                If (m_LogType And LoggingType.Email) Then ' Send Email
                    SendEmail(Message, Category)
                End If

                If (m_LogType And LoggingType.EventLog) Then ' Add event to Event Log
                    WriteEventLogEntry(Message, Category)
                End If

                If (m_LogType And LoggingType.File) Then ' Log to a file
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
    ''' <param name="Entry">The event text</param>
    ''' <param name="Category">The EventLogEntryType</param>
    ''' <remarks></remarks>
    Public Sub WriteToLogFile(ByVal Entry As String, ByVal Category As EventLogEntryType)
        Dim TimeString As String = Format(Date.Now.Hour, "00") & ":" & Format(Date.Now.Minute, "00") & ":" & Format(Date.Now.Second, "00")

        Try
            Dim oStream As New StreamWriter(m_LogFilename, True)

            Select Case Category
                Case EventLogEntryType.Error
                    oStream.Write("***** ERROR (" & TimeString & ") ")
                Case EventLogEntryType.Information
                    oStream.Write("INFORMATION (" & TimeString & ") ")
                Case EventLogEntryType.Warning
                    oStream.Write("??? WARNING (" & TimeString & ") ")
            End Select

            oStream.Write(Entry & vbCrLf)
            oStream.Close()
        Catch ex As Exception
            ' Damn!  If we get an error in here, we can't use this message logging type
            WriteEventLogEntry("Error writing logfile.  The message was: " & ex.Message, EventLogEntryType.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Write to the Application Event Log
    ''' </summary>
    ''' <param name="Message">The event text</param>
    ''' <param name="Category">The EventLogEntryType</param>
    ''' <remarks></remarks>
    Public Sub WriteEventLogEntry(ByVal Message As String, ByVal Category As EventLogEntryType)
        EventLog.WriteEntry(My.Application.Info.ProductName.ToString, Message, Category)
    End Sub

    ''' <summary>
    ''' Send an Email
    ''' </summary>
    ''' <param name="Body">The event text</param>
    ''' <param name="Category">The EventLogEntryType</param>
    ''' <remarks></remarks>
    Public Sub SendEmail(ByVal Body As String, ByVal Category As EventLogEntryType)
        ' If mandatory settings aren't specified, exit silently
        If m_SMTPServerIPorName = "" Or m_EmailFrom = "" Or Body = "" Then Exit Sub

        Dim TimeString As String = Format(Date.Now.Hour, "00") & ":" & Format(Date.Now.Minute, "00") & ":" & Format(Date.Now.Second, "00")
        Dim CategoryString As String = ""

        Select Case Category
            Case EventLogEntryType.Error
                CategoryString = "Error message generated by " & My.Application.Info.ProductName.ToString & " at " & TimeString
            Case EventLogEntryType.Information
                CategoryString = "Information message generated by " & My.Application.Info.ProductName.ToString & " at " & TimeString
            Case EventLogEntryType.Warning
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

#End Region

End Class

