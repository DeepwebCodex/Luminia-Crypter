<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Scanner
    Inherits System.Windows.Forms.Form

    'Form remplace la méthode Dispose pour nettoyer la liste des composants.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requise par le Concepteur Windows Form
    Private components As System.ComponentModel.IContainer

    'REMARQUE : la procédure suivante est requise par le Concepteur Windows Form
    'Elle peut être modifiée à l'aide du Concepteur Windows Form.  
    'Ne la modifiez pas à l'aide de l'éditeur de code.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.DesignForm1 = New Crypter.DesignForm()
        Me.EventLog = New System.Windows.Forms.TextBox()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.DesignButton1 = New Crypter.DesignButton()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.DesignControlBox1 = New Crypter.DesignControlBox()
        Me.DesignForm1.SuspendLayout()
        Me.SuspendLayout()
        '
        'DesignForm1
        '
        Me.DesignForm1.Controls.Add(Me.EventLog)
        Me.DesignForm1.Controls.Add(Me.TextBox2)
        Me.DesignForm1.Controls.Add(Me.Label1)
        Me.DesignForm1.Controls.Add(Me.DesignButton1)
        Me.DesignForm1.Controls.Add(Me.TextBox1)
        Me.DesignForm1.Controls.Add(Me.Label18)
        Me.DesignForm1.Controls.Add(Me.ListView1)
        Me.DesignForm1.Controls.Add(Me.DesignControlBox1)
        Me.DesignForm1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DesignForm1.Font = New System.Drawing.Font("Verdana", 10.0!)
        Me.DesignForm1.Location = New System.Drawing.Point(0, 0)
        Me.DesignForm1.Name = "DesignForm1"
        Me.DesignForm1.Padding = New System.Windows.Forms.Padding(2, 36, 2, 2)
        Me.DesignForm1.Size = New System.Drawing.Size(322, 456)
        Me.DesignForm1.TabIndex = 0
        Me.DesignForm1.Text = "Multi-Av Scanner"
        '
        'EventLog
        '
        Me.EventLog.BackColor = System.Drawing.Color.FromArgb(CType(CType(27, Byte), Integer), CType(CType(27, Byte), Integer), CType(CType(27, Byte), Integer))
        Me.EventLog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.EventLog.ForeColor = System.Drawing.Color.DarkGray
        Me.EventLog.Location = New System.Drawing.Point(9, 350)
        Me.EventLog.Multiline = True
        Me.EventLog.Name = "EventLog"
        Me.EventLog.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.EventLog.Size = New System.Drawing.Size(305, 94)
        Me.EventLog.TabIndex = 24
        '
        'TextBox2
        '
        Me.TextBox2.BackColor = System.Drawing.Color.FromArgb(CType(CType(27, Byte), Integer), CType(CType(27, Byte), Integer), CType(CType(27, Byte), Integer))
        Me.TextBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox2.ForeColor = System.Drawing.Color.DarkGray
        Me.TextBox2.Location = New System.Drawing.Point(58, 320)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.ReadOnly = True
        Me.TextBox2.Size = New System.Drawing.Size(256, 24)
        Me.TextBox2.TabIndex = 23
        Me.TextBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.ForeColor = System.Drawing.Color.DarkGray
        Me.Label1.Location = New System.Drawing.Point(10, 322)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(42, 17)
        Me.Label1.TabIndex = 22
        Me.Label1.Text = "Link:"
        '
        'DesignButton1
        '
        Me.DesignButton1.Font = New System.Drawing.Font("Verdana", 9.0!)
        Me.DesignButton1.Location = New System.Drawing.Point(144, 290)
        Me.DesignButton1.Name = "DesignButton1"
        Me.DesignButton1.Size = New System.Drawing.Size(170, 24)
        Me.DesignButton1.TabIndex = 21
        Me.DesignButton1.Text = "Browse 'n' Scan"
        '
        'TextBox1
        '
        Me.TextBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(27, Byte), Integer), CType(CType(27, Byte), Integer), CType(CType(27, Byte), Integer))
        Me.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox1.ForeColor = System.Drawing.Color.DarkGray
        Me.TextBox1.Location = New System.Drawing.Point(58, 290)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ReadOnly = True
        Me.TextBox1.Size = New System.Drawing.Size(80, 24)
        Me.TextBox1.TabIndex = 20
        Me.TextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.Color.Transparent
        Me.Label18.ForeColor = System.Drawing.Color.DarkGray
        Me.Label18.Location = New System.Drawing.Point(6, 292)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(46, 17)
        Me.Label18.TabIndex = 19
        Me.Label18.Text = "Rate:"
        '
        'ListView1
        '
        Me.ListView1.BackColor = System.Drawing.Color.FromArgb(CType(CType(27, Byte), Integer), CType(CType(27, Byte), Integer), CType(CType(27, Byte), Integer))
        Me.ListView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2})
        Me.ListView1.ForeColor = System.Drawing.Color.DarkGray
        Me.ListView1.GridLines = True
        Me.ListView1.Location = New System.Drawing.Point(9, 37)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(305, 247)
        Me.ListView1.TabIndex = 1
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Antivirus"
        Me.ColumnHeader1.Width = 162
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Detection"
        Me.ColumnHeader2.Width = 140
        '
        'DesignControlBox1
        '
        Me.DesignControlBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DesignControlBox1.BackColor = System.Drawing.Color.Transparent
        Me.DesignControlBox1.Location = New System.Drawing.Point(279, 3)
        Me.DesignControlBox1.Name = "DesignControlBox1"
        Me.DesignControlBox1.Size = New System.Drawing.Size(40, 20)
        Me.DesignControlBox1.TabIndex = 0
        Me.DesignControlBox1.Text = "DesignControlBox1"
        '
        'Scanner
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(322, 456)
        Me.Controls.Add(Me.DesignForm1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Scanner"
        Me.Opacity = 0.95R
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Scanner"
        Me.DesignForm1.ResumeLayout(False)
        Me.DesignForm1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DesignForm1 As Crypter.DesignForm
    Friend WithEvents DesignControlBox1 As Crypter.DesignControlBox
    Friend WithEvents EventLog As System.Windows.Forms.TextBox
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents DesignButton1 As Crypter.DesignButton
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents ListView1 As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
End Class
