Imports System.Drawing.Drawing2D
Imports System.ComponentModel

'###################################################
'#                                                 #
'#              COLOR BY Mavamaarten               #
'#                                                 #
'#              THEME & CODE BY Patak              #  
'#                                                 #
'###################################################

Public Class DesignForm
#Region " Gestion du type de Control "

    Inherits ContainerControl

#End Region

#Region " Initialisation de variable "

    Private Locked As Boolean = False
    Private Position As Point = Nothing

    Dim Couleur_Ecriture As Color = Color.FromArgb(70, 70, 70)
    Dim Couleur_BackColor As Color = Color.FromArgb(25, 25, 25)

    Dim Couleur_Degrade1 As Color = Color.FromArgb(35, 35, 35)
    Dim Couleur_Degrade2 As Color = Color.FromArgb(25, 25, 25)

    Dim Couleur_BordureExt As Color = Color.Black

    Dim Couleur_BordureInt1_Haut As Color = Color.FromArgb(74, 74, 74)
    Dim Couleur_BordureInt1_Bas As Color = Color.FromArgb(39, 39, 39)
    Dim Couleur_BordureInt2 As Color = Color.FromArgb(43, 43, 43)

    Dim Couleur_Separation1 As Color = Color.Black
    Dim Couleur_Separation2 As Color = Color.FromArgb(26, 128, 154)
    Dim Couleur_Separation3 As Color = Color.Black

    Dim Hauteur_Barre As Integer = 33

#End Region

#Region " Gestion du New "

    Sub New()
        Dock = DockStyle.Fill
        Font = New Font("Verdana", 10, FontStyle.Regular)
        SendToBack()
        DoubleBuffered = True
        Padding = New Padding(2, 36, 2, 2)

    End Sub

#End Region

#Region " Propriétés "

    Public Overrides Property Text As String
        Get
            Return MyBase.Text
        End Get
        Set(ByVal Value As String)
            MyBase.Text = Value
            Invalidate()
        End Set
    End Property

#End Region

#Region " Gestion et création du design "

    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        MyBase.OnPaint(e)
        e.Graphics.SmoothingMode = SmoothingMode.Default

        'BACKGROUND

        e.Graphics.Clear(Couleur_BackColor)

        'HAUT

        Dim Rectangle_Haut As New Rectangle(0, 0, Width, Hauteur_Barre)
        Dim Haut_Brush As New LinearGradientBrush(Rectangle_Haut, Couleur_Degrade1, Couleur_Degrade2, LinearGradientMode.Vertical)
        e.Graphics.FillRectangle(Haut_Brush, Rectangle_Haut)
        Haut_Brush.Dispose()

        'ECRITURE

        Dim StringFormat As New StringFormat
        StringFormat.LineAlignment = StringAlignment.Center
        StringFormat.Alignment = StringAlignment.Near

        Dim Rectangle_Ecriture As New Rectangle(15, 0, Width - 15, Hauteur_Barre)

        e.Graphics.DrawString(Text, Font, New SolidBrush(Couleur_Ecriture), Rectangle_Ecriture, StringFormat)
        StringFormat.Dispose()

        'BORDURE INT1

        Dim Rectangle_BordureInt1 As New Rectangle(1, 1, Width - 3, Hauteur_Barre - 4)
        Dim Rectangle_BordureInt1_AvecBordure As New Rectangle(0, 0, Width - 1, Hauteur_Barre - 2)

        Dim Brush_BordureInt1 As New LinearGradientBrush(Rectangle_BordureInt1_AvecBordure, Couleur_BordureInt1_Haut, Couleur_BordureInt1_Bas, LinearGradientMode.Vertical)
        e.Graphics.DrawRectangle(New Pen(Brush_BordureInt1, 1), Rectangle_BordureInt1)

        'BORDURE INT2

        Dim Rectangle_BordureInt2 As New Rectangle(1, Hauteur_Barre + 2, Width - 3, Height - Hauteur_Barre - 4)
        e.Graphics.DrawRectangle(New Pen(Couleur_BordureInt2, 1), Rectangle_BordureInt2)

        'SEPARATION

        Dim Separation_Point1 As New Point(0, Hauteur_Barre)
        Dim Separation_Point2 As New Point(Width, Hauteur_Barre)

        Dim Separation_Brush As New LinearGradientBrush(Separation_Point1, Separation_Point2, Color.Black, Color.Black)

        Dim ColorBlend As New ColorBlend
        ColorBlend.Colors = New Color() {Couleur_Separation1, Couleur_Separation2, Couleur_Separation3}
        ColorBlend.Positions = New Single() {0, 0.5, 1}

        Separation_Brush.InterpolationColors = ColorBlend

        e.Graphics.DrawLine(New Pen(Separation_Brush, 2), Separation_Point1, Separation_Point2)

        Separation_Brush.Dispose()

        'BORDURE EXT1

        Dim Rectangle_BordureExt1 As New Rectangle(0, 0, Width - 1, Hauteur_Barre - 2)
        e.Graphics.DrawRectangle(New Pen(Couleur_BordureExt, 1), Rectangle_BordureExt1)

        'BORDURE EXT2

        Dim Rectangle_BordureExt2 As New Rectangle(0, Hauteur_Barre + 1, Width - 1, Height - Hauteur_Barre - 2)
        e.Graphics.DrawRectangle(New Pen(Couleur_BordureExt, 1), Rectangle_BordureExt2)

    End Sub

#End Region

#Region " Gestion des actions "

    Protected Overrides Sub OnHandleCreated(ByVal e As System.EventArgs)
        MyBase.OnHandleCreated(e)
        FindForm.FormBorderStyle = FormBorderStyle.None
    End Sub

    Protected Overrides Sub OnMouseDown(ByVal e As System.Windows.Forms.MouseEventArgs)
        MyBase.OnMouseDown(e)
        Dim Rectangle As New Rectangle(0, 0, Width, Hauteur_Barre)
        If (Rectangle.Contains(e.Location)) Then
            Locked = True
            Position = e.Location
        End If
    End Sub

    Protected Overrides Sub OnMouseUp(ByVal e As System.Windows.Forms.MouseEventArgs)
        MyBase.OnMouseUp(e)
        Locked = False
    End Sub

    Protected Overrides Sub OnMouseMove(ByVal e As System.Windows.Forms.MouseEventArgs)
        MyBase.OnMouseMove(e)
        If (Locked) Then
            FindForm.Location = Cursor.Position - Position
        End If
    End Sub

    Protected Overrides Sub OnResize(ByVal e As System.EventArgs)
        MyBase.OnResize(e)
        Invalidate()
    End Sub

#End Region
End Class

Public Class DesignControlBox
#Region " Gestion du type de Control "

    Inherits Control

#End Region

#Region " Initialisation de variable "

    Dim Couleur_Ecriture_None As Color = Color.FromArgb(70, 70, 70)
    Dim Couleur_Ecriture_Over As Color = Color.FromArgb(150, 150, 150)
    Dim Couleur_Ecriture_Down As Color = Color.FromArgb(200, 200, 200)

    Dim Etat_Bouton As Etat = Etat.None
    Dim Position As New Point

#End Region

#Region " Initialisation des Enum "

    Enum Etat As Integer
        None
        Over
        Down
    End Enum

#End Region

#Region " Gestion du New "

    Sub New()
        Me.Size = New Size(40, 20)
        Me.SetStyle(ControlStyles.SupportsTransparentBackColor, True)
        Me.BackColor = Color.Transparent
        Me.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        DoubleBuffered = True
    End Sub

#End Region

#Region " Gestion et création du design "

    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        MyBase.OnPaint(e)
        e.Graphics.SmoothingMode = SmoothingMode.Default

        Dim Point_Min As New Point(2, 4)
        Dim Point_Close As New Point(22, 5)

        'GESTION DES ETATS

        Dim Couleur_Ecriture As Color

        Select Case Etat_Bouton
            Case Etat.None
                Couleur_Ecriture = Couleur_Ecriture_None
            Case Etat.Over
                Couleur_Ecriture = Couleur_Ecriture_Over
            Case Etat.Down
                Couleur_Ecriture = Couleur_Ecriture_Down
        End Select

        'ECRITURE

        If (Position.X <= 20) Then
            e.Graphics.DrawString("0", New Font("Marlett", 8), New SolidBrush(Couleur_Ecriture), Point_Min)
            e.Graphics.DrawString("r", New Font("Marlett", 8), New SolidBrush(Couleur_Ecriture_None), Point_Close)
        Else
            e.Graphics.DrawString("0", New Font("Marlett", 8), New SolidBrush(Couleur_Ecriture_None), Point_Min)
            e.Graphics.DrawString("r", New Font("Marlett", 8), New SolidBrush(Couleur_Ecriture), Point_Close)
        End If
    End Sub

#End Region

#Region " Gestion des actions "

    Protected Overrides Sub OnMouseMove(e As System.Windows.Forms.MouseEventArgs)
        MyBase.OnMouseMove(e)
        Position = e.Location
        Invalidate()
    End Sub

    Protected Overrides Sub OnClick(e As System.EventArgs)
        MyBase.OnClick(e)
        If (Position.X <= 20) Then
            FindForm.WindowState = FormWindowState.Minimized
        Else
            FindForm.Close()
        End If
    End Sub

    Protected Overrides Sub OnMouseEnter(ByVal e As System.EventArgs)
        MyBase.OnMouseEnter(e)
        Etat_Bouton = Etat.Over
        Invalidate()
    End Sub

    Protected Overrides Sub OnMouseLeave(ByVal e As System.EventArgs)
        MyBase.OnMouseLeave(e)
        Etat_Bouton = Etat.None
        Invalidate()
    End Sub

    Protected Overrides Sub OnMouseDown(ByVal e As System.Windows.Forms.MouseEventArgs)
        MyBase.OnMouseDown(e)
        Etat_Bouton = Etat.Down
        Invalidate()
    End Sub

    Protected Overrides Sub OnMouseUp(ByVal e As System.Windows.Forms.MouseEventArgs)
        MyBase.OnMouseUp(e)
        Etat_Bouton = Etat.Over
        Invalidate()
    End Sub

    Protected Overrides Sub OnResize(ByVal e As System.EventArgs)
        MyBase.OnResize(e)
        Invalidate()
    End Sub

#End Region
End Class

Public Class DesignGroupBox
#Region " Gestion du type de Control "

    Inherits ContainerControl

#End Region

#Region " Initialisation de variable "

    Dim Couleur_Ecriture As Color = Color.DarkGray
    Dim Couleur_BackColor As Color = Color.FromArgb(27, 27, 27)

    Dim Couleur_Degrade1 As Color = Color.FromArgb(39, 39, 39)
    Dim Couleur_Degrade2 As Color = Color.FromArgb(24, 24, 24)

    Dim Couleur_BordureExt As Color = Color.Black

    Dim Couleur_BordureInt1_Haut As Color = Color.FromArgb(52, 52, 52)
    Dim Couleur_BordureInt1_Bas As Color = Color.FromArgb(37, 37, 37)
    Dim Couleur_BordureInt2 As Color = Color.FromArgb(43, 43, 43)

    Dim Couleur_Separation As Color = Color.Black

    Dim Hauteur_Barre As Integer = 25

#End Region

#Region " Gestion du New "

    Sub New()
        Me.Size = New Size(172, 108)
        Font = New Font("Verdana", 8.25, FontStyle.Regular)
        DoubleBuffered = True
        Padding = New Padding(2, 27, 2, 2)
    End Sub

#End Region

#Region " Propriétés "

    Public Overrides Property Text As String
        Get
            Return MyBase.Text
        End Get
        Set(ByVal Value As String)
            MyBase.Text = Value
            Invalidate()
        End Set
    End Property

#End Region

#Region " Gestion et création du design "

    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        MyBase.OnPaint(e)
        e.Graphics.SmoothingMode = SmoothingMode.Default

        'BACKGROUND

        e.Graphics.Clear(Couleur_BackColor)

        'HAUT

        Dim Rectangle_Haut As New Rectangle(0, 0, Width, Hauteur_Barre)
        Dim Haut_Brush As New LinearGradientBrush(Rectangle_Haut, Couleur_Degrade1, Couleur_Degrade2, LinearGradientMode.Vertical)
        e.Graphics.FillRectangle(Haut_Brush, Rectangle_Haut)
        Haut_Brush.Dispose()

        'ECRITURE

        Dim StringFormat As New StringFormat
        StringFormat.LineAlignment = StringAlignment.Center
        StringFormat.Alignment = StringAlignment.Near

        Dim Rectangle_Ecriture As New Rectangle(9, 2, Width - 9, Hauteur_Barre - 2)

        e.Graphics.DrawString(Text, Font, New SolidBrush(Couleur_Ecriture), Rectangle_Ecriture, StringFormat)
        StringFormat.Dispose()

        'BORDURE INT1

        Dim Rectangle_BordureInt1 As New Rectangle(1, 1, Width - 3, Hauteur_Barre - 2)
        Dim Rectangle_BordureInt1_AvecBordure As New Rectangle(0, 0, Width - 1, Hauteur_Barre)

        Dim Brush_BordureInt1 As New LinearGradientBrush(Rectangle_BordureInt1_AvecBordure, Couleur_BordureInt1_Haut, Couleur_BordureInt1_Bas, LinearGradientMode.Vertical)
        e.Graphics.DrawRectangle(New Pen(Brush_BordureInt1, 1), Rectangle_BordureInt1)

        'BORDURE INT2

        Dim Rectangle_BordureInt2 As New Rectangle(1, Hauteur_Barre, Width - 3, Height - Hauteur_Barre - 2)
        e.Graphics.DrawRectangle(New Pen(Couleur_BordureInt2, 1), Rectangle_BordureInt2)

        'SEPARATION

        Dim Separation_Point1 As New Point(0, Hauteur_Barre)
        Dim Separation_Point2 As New Point(Width, Hauteur_Barre)

        e.Graphics.DrawLine(New Pen(Couleur_Separation, 1), Separation_Point1, Separation_Point2)

        'BORDURE EXT

        Dim Rectangle_BordureExt As New Rectangle(0, 0, Width - 1, Height - 1)
        e.Graphics.DrawRectangle(New Pen(Couleur_BordureExt, 1), Rectangle_BordureExt)

    End Sub

#End Region

#Region " Gestion des actions "

    Protected Overrides Sub OnResize(ByVal e As System.EventArgs)
        MyBase.OnResize(e)
        Invalidate()
    End Sub

#End Region
End Class

Public Class DesignButton
#Region " Gestion du type de Control "

    Inherits Control

#End Region

#Region " Initialisation de variable "

    Dim Couleur_Ecriture As Color = Color.FromArgb(170, 170, 170)

    Dim Couleur_Degrade1_None As Color = Color.FromArgb(40, 40, 40)
    Dim Couleur_Degrade2_None As Color = Color.FromArgb(20, 20, 20)

    Dim Couleur_Degrade1_Over As Color = Color.FromArgb(46, 46, 46)
    Dim Couleur_Degrade2_Over As Color = Color.FromArgb(26, 26, 26)

    Dim Couleur_Degrade1_Down As Color = Color.FromArgb(37, 37, 37)
    Dim Couleur_Degrade2_Down As Color = Color.FromArgb(18, 18, 18)

    Dim Couleur_BordureExt As Color = Color.Black

    Dim Couleur_BordureInt_Haut_None As Color = Color.FromArgb(53, 53, 53)
    Dim Couleur_BordureInt_Bas_None As Color = Color.FromArgb(34, 34, 34)

    Dim Couleur_BordureInt_Haut_Over As Color = Color.FromArgb(58, 58, 58)
    Dim Couleur_BordureInt_Bas_Over As Color = Color.FromArgb(39, 39, 39)

    Dim Couleur_BordureInt_Haut_Down As Color = Color.FromArgb(50, 50, 50)
    Dim Couleur_BordureInt_Bas_Down As Color = Color.FromArgb(32, 32, 32)

    Dim Etat_Bouton As Etat = Etat.None

#End Region

#Region " Initialisation des Enum "

    Enum Etat As Integer
        None
        Over
        Down
    End Enum

#End Region

#Region " Gestion du New "

    Sub New()
        Me.Size = New Size(130, 40)
        Font = New Font("Verdana", 9, FontStyle.Regular)
        DoubleBuffered = True
    End Sub

#End Region

#Region " Propriétés "

    Public Overrides Property Text As String
        Get
            Return MyBase.Text
        End Get
        Set(ByVal Value As String)
            MyBase.Text = Value
            Invalidate()
        End Set
    End Property

#End Region

#Region " Gestion et création du design "

    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        MyBase.OnPaint(e)
        e.Graphics.SmoothingMode = SmoothingMode.Default

        'GESTION DES ETATS

        Dim Couleur_Degrade1 As New Color
        Dim Couleur_Degrade2 As New Color

        Dim Couleur_BordureInt_Haut As New Color
        Dim Couleur_BordureInt_Bas As New Color

        Select Case Etat_Bouton
            Case Etat.None
                Couleur_Degrade1 = Couleur_Degrade1_None
                Couleur_Degrade2 = Couleur_Degrade2_None

                Couleur_BordureInt_Haut = Couleur_BordureInt_Haut_None
                Couleur_BordureInt_Bas = Couleur_BordureInt_Bas_None
            Case Etat.Over
                Couleur_Degrade1 = Couleur_Degrade1_Over
                Couleur_Degrade2 = Couleur_Degrade2_Over

                Couleur_BordureInt_Haut = Couleur_BordureInt_Haut_Over
                Couleur_BordureInt_Bas = Couleur_BordureInt_Bas_Over
            Case Etat.Down
                Couleur_Degrade1 = Couleur_Degrade1_Down
                Couleur_Degrade2 = Couleur_Degrade2_Down

                Couleur_BordureInt_Haut = Couleur_BordureInt_Haut_Down
                Couleur_BordureInt_Bas = Couleur_BordureInt_Bas_Down
        End Select

        'BACKGROUND

        Dim Rectangle_Haut As New Rectangle(0, 0, Width, Height)

        Dim Haut_Brush As LinearGradientBrush = New LinearGradientBrush(Rectangle_Haut, Couleur_Degrade1, Couleur_Degrade2, LinearGradientMode.Vertical)
        e.Graphics.FillRectangle(Haut_Brush, Rectangle_Haut)
        Haut_Brush.Dispose()

        'ECRITURE

        Dim StringFormat As New StringFormat
        StringFormat.LineAlignment = StringAlignment.Center
        StringFormat.Alignment = StringAlignment.Center

        Dim Rectangle_Ecriture As New Rectangle(0, 0, Width, Height)

        e.Graphics.DrawString(Text, Font, New SolidBrush(Couleur_Ecriture), Rectangle_Ecriture, StringFormat)

        'BORDURE INT

        Dim Rectangle_BordureInt As New Rectangle(1, 1, Width - 3, Height - 3)
        Dim Rectangle_BordureInt_AvecBordure As New Rectangle(0, 0, Width - 1, Height - 1)

        Dim Brush_BordureInt As New LinearGradientBrush(Rectangle_BordureInt_AvecBordure, Couleur_BordureInt_Haut, Couleur_BordureInt_Bas, LinearGradientMode.Vertical)
        e.Graphics.DrawRectangle(New Pen(Brush_BordureInt, 1), Rectangle_BordureInt)

        'BORDURE EXT

        Dim Rectangle_BordureExt As New Rectangle(0, 0, Width - 1, Height - 1)
        e.Graphics.DrawRectangle(New Pen(Couleur_BordureExt, 1), Rectangle_BordureExt)
    End Sub

#End Region

#Region " Gestion des actions "

    Protected Overrides Sub OnMouseEnter(ByVal e As System.EventArgs)
        MyBase.OnMouseEnter(e)
        Etat_Bouton = Etat.Over
        Invalidate()
    End Sub

    Protected Overrides Sub OnMouseLeave(ByVal e As System.EventArgs)
        MyBase.OnMouseLeave(e)
        Etat_Bouton = Etat.None
        Invalidate()
    End Sub

    Protected Overrides Sub OnMouseDown(ByVal e As System.Windows.Forms.MouseEventArgs)
        MyBase.OnMouseDown(e)
        Etat_Bouton = Etat.Down
        Invalidate()
    End Sub

    Protected Overrides Sub OnMouseUp(ByVal e As System.Windows.Forms.MouseEventArgs)
        MyBase.OnMouseUp(e)
        Etat_Bouton = Etat.Over
        Invalidate()
    End Sub

    Protected Overrides Sub OnResize(ByVal e As System.EventArgs)
        MyBase.OnResize(e)
        Invalidate()
    End Sub

#End Region
End Class

<DefaultEvent("CheckedChanged")> _
Class DeumosCheckBox
    Inherits ThemeControl153

    Sub New()
        LockHeight = 16

        SetColor("Border", 26, 26, 26)
        SetColor("Gloss1", 35, Color.White)
        SetColor("Gloss2", 5, Color.White)
        SetColor("Checked1", Color.Transparent)
        SetColor("Checked2", 40, Color.White)
        SetColor("Unchecked1", 8, 8, 8)
        SetColor("Unchecked2", 16, 16, 16)
        SetColor("Glow", 5, Color.White)
        SetColor("Text", Color.White)
        SetColor("InnerOutline", Color.Black)
        SetColor("OuterOutline", Color.Black)
    End Sub

    Private C1, C2, C3, C4, C5, C6 As Color
    Private P1, P2, P3 As Pen
    Private B1, B2 As SolidBrush

    Protected Overrides Sub ColorHook()
        C1 = GetColor("Gloss1")
        C2 = GetColor("Gloss2")
        C3 = GetColor("Checked1")
        C4 = GetColor("Checked2")
        C5 = GetColor("Unchecked1")
        C6 = GetColor("Unchecked2")

        P1 = New Pen(GetColor("Border"))
        P2 = New Pen(GetColor("InnerOutline"))
        P3 = New Pen(GetColor("OuterOutline"))

        B1 = New SolidBrush(GetColor("Glow"))
        B2 = New SolidBrush(GetColor("Text"))
    End Sub

    Protected Overrides Sub PaintHook()
        G.Clear(BackColor)

        DrawBorders(P1, 0, 0, _Field, _Field, 1)
        DrawGradient(C1, C2, 0, 0, _Field, _Field \ 2)

        If _Checked Then
            DrawGradient(C3, C4, 2, 2, _Field - 4, _Field - 4)
        Else
            DrawGradient(C5, C6, 2, 2, _Field - 4, _Field - 4, 90)
        End If

        If State = MouseState.Over Then
            G.FillRectangle(B1, 0, 0, _Field, _Field)
        End If

        DrawText(B2, HorizontalAlignment.Left, _Field + 3, 0)

        DrawBorders(P2, 0, 0, _Field, _Field, 2)
        DrawBorders(P3, 0, 0, _Field, _Field)

        DrawCorners(BackColor, 0, 0, _Field, _Field)
    End Sub

    Private _Field As Integer = 16
    Property Field() As Integer
        Get
            Return _Field
        End Get
        Set(ByVal value As Integer)
            If value < 4 Then Return
            _Field = value
            LockHeight = value
            Invalidate()
        End Set
    End Property

    Private _Checked As Boolean
    Property Checked() As Boolean
        Get
            Return _Checked
        End Get
        Set(ByVal value As Boolean)
            _Checked = value
            Invalidate()
        End Set
    End Property

    Protected Overrides Sub OnMouseDown(ByVal e As System.Windows.Forms.MouseEventArgs)
        _Checked = Not _Checked
        RaiseEvent CheckedChanged(Me)
        MyBase.OnMouseDown(e)
    End Sub

    Event CheckedChanged(ByVal sender As Object)

End Class

<DefaultEvent("CheckedChanged")> _
Class DeumosRadioButton
    Inherits ThemeControl153

    Sub New()
        LockHeight = 16

        SetColor("Gloss1", 38, Color.White)
        SetColor("Gloss2", 5, Color.White)
        SetColor("Checked1", Color.Transparent)
        SetColor("Checked2", 40, Color.White)
        SetColor("Unchecked1", 8, 8, 8)
        SetColor("Unchecked2", 16, 16, 16)
        SetColor("Glow", 5, Color.White)
        SetColor("Text", Color.White)
        SetColor("InnerOutline", Color.Black)
        SetColor("OuterOutline", 15, Color.White)
    End Sub

    Protected Overrides Sub ColorHook()
        C1 = GetColor("Gloss1")
        C2 = GetColor("Gloss2")
        C3 = GetColor("Checked1")
        C4 = GetColor("Checked2")
        C5 = GetColor("Unchecked1")
        C6 = GetColor("Unchecked2")

        B1 = New SolidBrush(GetColor("Glow"))
        B2 = New SolidBrush(GetColor("Text"))

        P1 = New Pen(GetColor("InnerOutline"))
        P2 = New Pen(GetColor("OuterOutline"))
    End Sub

    Private C1, C2, C3, C4, C5, C6 As Color
    Private P1, P2 As Pen
    Private B1, B2 As SolidBrush

    Private R1, R2 As Rectangle
    Private G1 As LinearGradientBrush

    Protected Overrides Sub PaintHook()
        G.Clear(BackColor)

        G.SmoothingMode = SmoothingMode.HighQuality
        R1 = New Rectangle(4, 2, _Field - 8, (_Field \ 2) - 1)
        R2 = New Rectangle(4, 2, _Field - 8, (_Field \ 2))

        G1 = New LinearGradientBrush(R2, C1, C2, 90S)
        G.FillEllipse(G1, R1)

        R1 = New Rectangle(2, 2, _Field - 4, _Field - 4)

        If _Checked Then
            G1 = New LinearGradientBrush(R1, C3, C4, 90S)
        Else
            G1 = New LinearGradientBrush(R1, C5, C6, 90S)
        End If
        G.FillEllipse(G1, R1)

        If State = MouseState.Over Then
            R1 = New Rectangle(2, 2, _Field - 4, _Field - 4)
            G.FillEllipse(B1, R1)
        End If

        DrawText(B2, HorizontalAlignment.Left, _Field + 3, 0)

        G.DrawEllipse(P1, 2, 2, _Field - 4, _Field - 4)
        G.DrawEllipse(P2, 1, 1, _Field - 2, _Field - 2)

    End Sub

    Private _Field As Integer = 16
    Property Field() As Integer
        Get
            Return _Field
        End Get
        Set(ByVal value As Integer)
            If value < 4 Then Return
            _Field = value
            LockHeight = value
            Invalidate()
        End Set
    End Property

    Private _Checked As Boolean
    Property Checked() As Boolean
        Get
            Return _Checked
        End Get
        Set(ByVal value As Boolean)
            _Checked = value
            InvalidateControls()
            RaiseEvent CheckedChanged(Me)
            Invalidate()
        End Set
    End Property

    Protected Overrides Sub OnMouseDown(ByVal e As System.Windows.Forms.MouseEventArgs)
        If Not _Checked Then Checked = True
        MyBase.OnMouseDown(e)
    End Sub

    Event CheckedChanged(ByVal sender As Object)

    Protected Overrides Sub OnCreation()
        InvalidateControls()
    End Sub

    Private Sub InvalidateControls()
        If Not IsHandleCreated OrElse Not _Checked Then Return

        For Each C As Control In Parent.Controls
            If C IsNot Me AndAlso TypeOf C Is DeumosRadioButton Then
                DirectCast(C, DeumosRadioButton).Checked = False
            End If
        Next
    End Sub

End Class