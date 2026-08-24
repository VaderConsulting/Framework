Imports System
Imports System.IO
Imports System.ServiceModel
Imports System.ServiceModel.Web
'Imports System.ServiceModel.Description
'Imports System.Runtime.Serialization
'Imports System.Runtime.Serialization.Configuration
'Imports System.Runtime.Serialization.DataContractAttribute
'Imports System.Runtime.Serialization.DataContractSerializer

<ServiceContract()> _
Public Interface IService

    <OperationContract()> _
    <WebInvoke(BodyStyle:=WebMessageBodyStyle.Wrapped)> _
    Sub SetConfigFilenameAndPath(ByVal FilenameAndPath As String)

    <OperationContract()> _
    <WebInvoke(BodyStyle:=WebMessageBodyStyle.Wrapped)> _
    Function LoadConfigSetting(ByVal SectionName As String, ByVal SettingName As String) As String

    <OperationContract()> _
    <WebInvoke(BodyStyle:=WebMessageBodyStyle.Wrapped)> _
    Sub SaveConfigSetting(ByVal SectionName As String, ByVal SettingName As String, ByVal SettingValue As String)

    <OperationContract()> _
    <WebInvoke(BodyStyle:=WebMessageBodyStyle.Wrapped)> _
    Function LoadConfigSection(ByVal SectionName As String) As Collections.Specialized.OrderedDictionary

    <OperationContract()> _
    <WebInvoke(BodyStyle:=WebMessageBodyStyle.Wrapped)> _
    Function MySettings(ByVal Username As String) As Collections.Specialized.OrderedDictionary

    <OperationContract()> _
    <WebInvoke(BodyStyle:=WebMessageBodyStyle.Wrapped)> _
    Function MySection(ByVal Username As String, ByVal SectionName As String) As Collections.Specialized.OrderedDictionary

    <OperationContract()> _
    <WebInvoke(BodyStyle:=WebMessageBodyStyle.Wrapped)> _
    Function PerformCADSAuthentication(Optional ByVal Username As String = "[Empty]", _
                                       Optional ByVal Password As String = "[Empty]", _
                                       Optional ByVal Version As String = "4.0", _
                                       Optional ByVal AutoLogon As Boolean = True) _
                                       As String

    <OperationContract()> _
    <WebInvoke(BodyStyle:=WebMessageBodyStyle.Wrapped)> _
    Function Logon(ByVal Username As String, ByVal Password As String) As String

    <OperationContract()> _
    <WebInvoke(BodyStyle:=WebMessageBodyStyle.Wrapped)> _
    Function LoadConfigSettingAttribute(ByVal SectionName As String, ByVal SettingName As String, ByVal AttributeName As String) As String

    <OperationContract()> _
    <WebInvoke(BodyStyle:=WebMessageBodyStyle.Wrapped)> _
    Function LoadConfigSettingAttributes(ByVal SectionName As String, ByVal SettingName As String) As Collections.Specialized.OrderedDictionary

End Interface
