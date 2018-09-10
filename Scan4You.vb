Imports System.Collections.Specialized
Imports System.IO
Imports System.Text
Imports System.Text.RegularExpressions
Imports Crypter.Utility.Http

Namespace Service
    Public Class Scan4You
        Implements IDisposable

        Private Http As Utility.Http = Nothing
        Private Session As UploadSession = Nothing

#Region "Structures"
        Public Structure Result
            Dim Success As Boolean
            Dim ProxyError As Boolean
            Dim FailMessage As String
            Dim UploadUrl As String
        End Structure

        Private Structure UploadSession
            Dim UploadKey As String
            Dim User As String
            Dim Config As String
        End Structure
#End Region

#Region "Events"
        Public Event Notify(ByVal Text As String)
#End Region

#Region "Properties"
        Private _Account As String() = Nothing
        Public Property Account As String()
            Get
                Return _Account
            End Get
            Set(ByVal value As String())
                _Account = value
            End Set
        End Property

        Private _Online As Boolean = False
        Public Property Online As Boolean
            Get
                Return _Online
            End Get
            Set(ByVal value As Boolean)
                _Online = value
            End Set
        End Property
        Private _AVDetections As New Dictionary(Of String, String)
        Public Property AVDetections As Dictionary(Of String, String)
            Get
                Return _AVDetections
            End Get
            Set(ByVal value As Dictionary(Of String, String))
                _AVDetections = value
            End Set
        End Property

        Private _Rate As String = Nothing
        Public Property Rate As String
            Get
                Return _Rate
            End Get
            Set(ByVal value As String)
                _Rate = value
            End Set
        End Property

        Private _BBCode As String = Nothing
        Public Property BBCode As String
            Get
                Return _BBCode
            End Get
            Set(ByVal value As String)
                _BBCode = value
            End Set
        End Property

        Private _Link As String = Nothing
        Public Property Link As String
            Get
                Return _Link
            End Get
            Set(ByVal value As String)
                _Link = value
            End Set
        End Property
#End Region

#Region "Constructor"
        Public Sub New(ByVal Acc As String())
            Account = Acc
        End Sub
#End Region

#Region "Deconstructor"
        Private disposedValue As Boolean
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    If Not IsNothing(Http) Then Http.Dispose()
                End If
            End If
            Me.disposedValue = True
        End Sub
        Public Sub Dispose() Implements IDisposable.Dispose
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region

#Region "Methods"
        Private Sub NewSession()
            If Not IsNothing(Http) Then Http.Dispose()
            Http = New Utility.Http
            Online = False
        End Sub
#End Region

#Region "API"
        Public Sub Login()
            Try
                NewSession()
                With Http
                    .TimeOut = 10000
                    .DebugMode = True
                    .AutoRedirect = True
                    RaiseEvent Notify("Attempting to log in.")

                    Dim hr As HttpResponse = .GetResponse(Verb._GET, "http://scan4you.net")
                    If Not hr.Html.Contains("z6112' value='") Then
                        RaiseEvent Notify("Could not retrieve log in form. Missing Element.")
                        Exit Try
                    End If

                    Dim PostData As New StringBuilder()
                    PostData.Append("l_ogin=" & .UrlEncode(Account(0)))
                    PostData.Append("&passw=" & .UrlEncode(Account(1)))
                    PostData.Append("&act=Login&action=L_ogi_n&z6112=" & .ParseBetween(hr.Html, "name='z6112' value='", "'></div>", "name='z6112' value='".Length))

                    .ContentType = "application/x-www-form-urlencoded"
                    .Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8"
                    .Referer = "http://scan4you.net/"

                    hr = .GetResponse(Verb._POST, "http://scan4you.net/login.php", PostData.ToString)

                    If hr.Html.Contains("('document.location=""check.php"";',1000);") Then
                        RaiseEvent Notify("Succesfully logged in!")
                        Online = True
                    Else
                        RaiseEvent Notify("Could not login!")
                        Online = False
                    End If
                End With
            Catch ex As Exception
                RaiseEvent Notify("Could not login: " & ex.Message)
            End Try
        End Sub
        Public Sub Upload(ByVal Path As String)
            Try
                With Http
                    .TimeOut = 600000 'Long time out to give Scan4You long enough to receive and give results.
                    .DebugMode = True
                    .AutoRedirect = True
                    Dim fi As New FileInfo(Path)
                    RaiseEvent Notify(String.Format("Attempting to upload file '{0}'.", fi.Name))

                    Dim hr As HttpResponse

                    Dim data As New UploadData(File.ReadAllBytes(Path), fi.Name, "uppload") 'Use idb's handy UploadData class to make our lives easier.

                    .Referer = "http://scan4you.net/check3.php"

                    Dim fields As New NameValueCollection 'A lot of unnecessary fields at the end of the request.
                    fields.Add("action", "Check File")
                    fields.Add("url", "http://")
                    fields.Add("domen", "")
                    fields.Add("pack", "http://")
                    fields.Add("pereodic", "0:")
                    fields.Add("notify", "")
                    fields.Add("notify_param", "")
                    fields.Add("notify_virus_only", "on")
                    fields.Add("notify_stop_on_virus", "on")

                    hr = .GetResponse(Verb._POST, "http://scan4you.net/check3.php", Nothing, fields, data)
                    If hr.Html.Contains("/35") Then
                        RaiseEvent Notify("Parsing results...")
                        'BBCode is in some kind of javascript window thing.
                        _BBCode = .ParseBetween(hr.Html, """if(window.fCopyToClipboard){if(window.fCopyToClipboard(this));}"" >", "</textarea></div></div>", """if(window.fCopyToClipboard){if(window.fCopyToClipboard(this));}"" >".Length)
                        'Parse result link
                        Dim lin As String = .ParseBetween(hr.Html, "<div id='job'><table width=400 align=center><tr><td colspan=2><font size=4><a href='", "'>", "<div id='job'><table width=400 align=center><tr><td colspan=2><font size=4><a href='".Length)


                        For i As Integer = 0 To 5   'Scan4You likes to fuck up and give 503 errors on newly scanned items, so let's keep trying until it works.
                            hr = .GetResponse(Verb._GET, lin)
                            If Not hr.Html.Contains("503 Service Temporarily Unavailable") Then Exit For
                        Next

                        Dim results As String = .ParseBetween(hr.Html, "RESULTS:</b></td><td><b>", "</td></tr></table><table width=400 align=center><tr><td>", "RESULTS:</b></td><td><b>".Length)
                        _Rate = .ParseBetween(results, "", "</b></td></tr><tr><td>", "".Length)

                        _Link = lin
                        results = results.Replace("</td><td>", "~~").Replace("</td></tr><tr><td>", Environment.NewLine).Replace("</b></td>", String.Empty).Replace("</td><td class=""failed"">", "~~")

                        For Each d As String In Regex.Split(results, Environment.NewLine)
                            If Not d.Contains("/35") Then
                                Dim avData As String() = Regex.Split(d, "~~")
                                AVDetections.Add(avData(0), avData(1))
                            End If
                        Next
                        RaiseEvent Notify("ScanComplete") 'Tell form to put data into appropriate controls.
                        RaiseEvent Notify("Scanning completed!") 'Add to event log.
                    Else
                        RaiseEvent Notify("Scanning failed, please try again.")
                    End If

                End With
            Catch ex As Exception
                MessageBox.Show(ex.ToString)
            End Try
        End Sub
#End Region

    End Class
End Namespace