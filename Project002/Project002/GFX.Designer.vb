<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class GFX
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.goblin = New System.Windows.Forms.PictureBox()
        Me.missle = New System.Windows.Forms.PictureBox()
        Me.Canon = New System.Windows.Forms.PictureBox()
        Me.pbChar = New System.Windows.Forms.PictureBox()
        Me.pbGFX = New System.Windows.Forms.PictureBox()
        CType(Me.goblin, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.missle, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Canon, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbChar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbGFX, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'goblin
        '
        Me.goblin.BackColor = System.Drawing.SystemColors.Control
        Me.goblin.Image = Global.Project002.My.Resources.Resources.Goblinp50
        Me.goblin.Location = New System.Drawing.Point(12, 269)
        Me.goblin.Name = "goblin"
        Me.goblin.Size = New System.Drawing.Size(50, 50)
        Me.goblin.TabIndex = 4
        Me.goblin.TabStop = False
        '
        'missle
        '
        Me.missle.BackColor = System.Drawing.SystemColors.Control
        Me.missle.Image = Global.Project002.My.Resources.Resources.RedLaserBeam
        Me.missle.Location = New System.Drawing.Point(12, 235)
        Me.missle.Name = "missle"
        Me.missle.Size = New System.Drawing.Size(9, 19)
        Me.missle.TabIndex = 3
        Me.missle.TabStop = False
        '
        'Canon
        '
        Me.Canon.BackColor = System.Drawing.SystemColors.Control
        Me.Canon.Image = Global.Project002.My.Resources.Resources.Canonp50
        Me.Canon.Location = New System.Drawing.Point(12, 179)
        Me.Canon.Name = "Canon"
        Me.Canon.Size = New System.Drawing.Size(50, 50)
        Me.Canon.TabIndex = 2
        Me.Canon.TabStop = False
        '
        'pbChar
        '
        Me.pbChar.Image = Global.Project002.My.Resources.Resources.EntitiesSpriteSheet
        Me.pbChar.Location = New System.Drawing.Point(12, 123)
        Me.pbChar.Name = "pbChar"
        Me.pbChar.Size = New System.Drawing.Size(100, 50)
        Me.pbChar.TabIndex = 1
        Me.pbChar.TabStop = False
        '
        'pbGFX
        '
        Me.pbGFX.Image = Global.Project002.My.Resources.Resources.MapSpriteSheet
        Me.pbGFX.Location = New System.Drawing.Point(12, 12)
        Me.pbGFX.Name = "pbGFX"
        Me.pbGFX.Size = New System.Drawing.Size(157, 105)
        Me.pbGFX.TabIndex = 0
        Me.pbGFX.TabStop = False
        '
        'GFX
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(512, 450)
        Me.Controls.Add(Me.goblin)
        Me.Controls.Add(Me.missle)
        Me.Controls.Add(Me.Canon)
        Me.Controls.Add(Me.pbChar)
        Me.Controls.Add(Me.pbGFX)
        Me.Name = "GFX"
        Me.Text = "GFX"
        CType(Me.goblin, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.missle, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Canon, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbChar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbGFX, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pbGFX As PictureBox
    Friend WithEvents pbChar As PictureBox
    Friend WithEvents Canon As PictureBox
    Friend WithEvents goblin As PictureBox
    Friend WithEvents missle As PictureBox
End Class
