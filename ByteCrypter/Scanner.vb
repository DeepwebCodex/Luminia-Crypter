Imports System.Runtime.InteropServices
Imports System.Threading
Imports System.Text
Public Class Scanner
    Public Shared scan As Service.Scan4You = Nothing
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            scan = New Service.Scan4You(New String() {"Mephobia", "zman13"})
            AddHandler scan.Notify, AddressOf OnNotify
            Threading.ThreadPool.QueueUserWorkItem(AddressOf scan.Login)
            AddHandler scan.Notify, AddressOf OnNotifyScan4You
        Catch ex As Exception

        End Try
    End Sub
    Private Sub DesignButton1_Click(sender As Object, e As EventArgs) Handles DesignButton1.Click
        scan.AVDetections.Clear()
        If Not scan.Online Then Return
        Using ofd As New OpenFileDialog()
            If ofd.ShowDialog = Windows.Forms.DialogResult.OK Then
                With scan
                    ListView1.Items.Clear()
                    TextBox1.Text = Nothing : TextBox2.Text = Nothing
                    ThreadPool.QueueUserWorkItem(New WaitCallback(AddressOf scan.Upload), ofd.FileName)
                End With
            End If
        End Using
    End Sub
    Private Delegate Sub OnNotifyDelegate(ByVal text As String)
    Public Sub OnNotify(ByVal text As String)
        If InvokeRequired Then
            Invoke(New OnNotifyDelegate(AddressOf OnNotify), text)
            Exit Sub
        End If
        If text = "Succesfully logged in!" Then

        ElseIf text = "Could not login!" Then
            MessageBox.Show("Could not login!")
        End If
    End Sub
    Private Delegate Sub _OnNotify(ByVal Text As String)
    Private Sub OnNotifyScan4You(ByVal Text As String)
        Try
            If Me.InvokeRequired Then
                Me.Invoke(New _OnNotify(AddressOf OnNotifyScan4You), Text)
            Else
                If Text = "ScanComplete" Then
                    For Each Str As KeyValuePair(Of String, String) In scan.AVDetections
                        Dim lItem As New ListViewItem(Str.Key)
                        lItem.SubItems.Add(Str.Value)
                        lItem.UseItemStyleForSubItems = False
                        If Str.Value = "OK" Then
                            lItem.SubItems(1).ForeColor = Color.Green
                        Else
                            lItem.SubItems(1).ForeColor = Color.Red
                        End If
                        ListView1.Items.Add(lItem)
                    Next
                    'TextBox4.Text = scan.BBCode
                    TextBox1.Text = scan.Rate
                    TextBox2.Text = scan.Link
                    Exit Sub
                End If
                EventLog.AppendText(String.Format("[{0}] {1}{2}", Now.ToString("hh:mm:ss tt"), Text, vbCrLf))
            End If
        Catch ex As Exception

        End Try  
    End Sub
    Private Sub Form_Close() Handles MyBase.FormClosing
        Form1.Show()
    End Sub

    Private Sub DesignForm1_Click(sender As Object, e As EventArgs)

    End Sub
End Class
Public Class ListView
    Inherits System.Windows.Forms.ListView

    <DllImport("uxtheme.dll", CharSet:=CharSet.Unicode)> _
    Private Shared Function SetWindowTheme(ByVal hWnd As IntPtr, ByVal pszSubAppName As String, ByVal pszSubIdList As String) As Integer
    End Function
    Protected Overrides Sub CreateHandle()
        MyBase.CreateHandle()
        SetWindowTheme(Me.Handle, "explorer", Nothing)
    End Sub
    Public Sub New()
        Me.DoubleBuffered = True
        Me.View = System.Windows.Forms.View.Details
        Me.FullRowSelect = True
        Me.GridLines = True
    End Sub
End Class