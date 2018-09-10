Imports System.Windows.Forms
Imports System
Imports System.Drawing

Public Class Form1

    Sub New()
        InitializeComponent()

        Seal.ValidateCore = True
        Seal.Protection = RuntimeProtection.None

        Seal.RunHook = AddressOf LicenseRun
        Seal.BanHook = AddressOf LicenseBan
        Seal.RenewHook = AddressOf LicenseRenew

        Seal.Catch = True

        Seal.Initialize("00000000") 'Required
    End Sub

    Sub LicenseBan()
        MessageBox.Show("Executing BanHook code.")
    End Sub

    Sub LicenseRun()
        MessageBox.Show("Executing RunHook code.")
    End Sub

    Sub LicenseRenew()
        MessageBox.Show("Executing RenewHook code.")
        LicenseRenewEx()
    End Sub

    Sub LicenseRenewEx()
        TextBox3.Text = Seal.GlobalMessage

        Label1.Text = "Username: " & Seal.Username
        Label2.Text = "Update Available: " & Seal.UpdateAvailable.ToString()
        Label4.Text = "Expiration Date: " & Seal.ExpirationDate.ToString()
        Label5.Text = "Time Remaining: " & Seal.TimeRemaining.ToString()
        Label3.Text = "License Points: " & Seal.Points.ToString("N0")
        Label6.Text = "License Type: " & Seal.LicenseType.ToString()
        Label7.Text = "Unlimited Time: " & Seal.UnlimitedTime.ToString()
        Label9.Text = "Users Online: " & Seal.UsersOnline & " / " & Seal.UsersCount
        Label8.Text = "GUID: " & Seal.GUID

        ShowNewsEntries()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        LicenseRenew()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Seal.ShowAccount()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Seal.InstallUpdates()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Throw New ArgumentException
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Dim T As New Threading.Thread(Sub() Throw New Exception)
        T.Start()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Seal.BanCurrentUser("You are a bad user!")
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        If Seal.SpendPoints(10) Then
            LicenseRenewEx()
            MessageBox.Show("10 Points have been deducted from your account.")
        Else
            MessageBox.Show("You do not have enough points to complete this transaction.")
        End If
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        If Seal.SpendPoints(1000000) Then
            LicenseRenewEx()
            MessageBox.Show("1,000,000 Points have been deducted from your account.")
        Else
            MessageBox.Show("You do not have enough points to complete this transaction.")
        End If
    End Sub

#Region " News System "

    Private Sub ShowNewsEntries()
        ListView1.Items.Clear()

        For Each P As NewsPost In Seal.News
            Dim I As New ListViewItem(P.Time.ToString("MM.dd.yy"))
            I.SubItems.Add(P.Name)
            I.Tag = P
            ListView1.Items.Add(I)
        Next
    End Sub

    Private Sub ShowPostMessage(ByVal post As NewsPost)
        Dim Message As String = Seal.GetPostMessage(post.ID)

        If String.IsNullOrEmpty(Message) Then
            ShowNewsEntries()
        Else
            TextBox1.Text = post.Name
            TextBox2.Text = Message
        End If
    End Sub


    Private Sub ListView1_Click(sender As System.Object, e As System.EventArgs) Handles ListView1.Click
        If ListView1.SelectedIndices.Count = 0 Then Return
        ShowPostMessage(DirectCast(ListView1.SelectedItems(0).Tag, NewsPost))
    End Sub

    Private Sub ListView1_MouseMove(sender As Object, e As MouseEventArgs) Handles ListView1.MouseMove
        If ListView1.GetItemAt(e.X, e.Y) Is Nothing Then
            ListView1.Cursor = Cursors.Default
        Else
            ListView1.Cursor = Cursors.Hand
        End If
    End Sub

    Private Sub ListView1_DrawSubItem(sender As System.Object, e As System.Windows.Forms.DrawListViewSubItemEventArgs) Handles ListView1.DrawSubItem
        Dim SB As New SolidBrush(e.SubItem.ForeColor)

        e.DrawBackground()
        e.Graphics.DrawString(e.SubItem.Text, e.SubItem.Font, SB, e.Bounds, Nothing)

        SB.Dispose()
    End Sub

#End Region

End Class
