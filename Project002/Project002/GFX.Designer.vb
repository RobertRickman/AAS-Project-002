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
        Me.pbChar = New System.Windows.Forms.PictureBox()
        Me.pbGFX = New System.Windows.Forms.PictureBox()
        CType(Me.pbChar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbGFX, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pbChar
        '
        Me.pbChar.BackColor = System.Drawing.Color.Transparent
        Me.pbChar.Image = Global.Project002.My.Resources.Resources.EntitiesSpriteSheet
        Me.pbChar.Location = New System.Drawing.Point(12, 128)
        Me.pbChar.Name = "pbChar"
        Me.pbChar.Size = New System.Drawing.Size(113, 75)
        Me.pbChar.TabIndex = 1
        Me.pbChar.TabStop = False
        '
        'pbGFX
        '
        Me.pbGFX.BackColor = System.Drawing.Color.Transparent
        Me.pbGFX.Image = Global.Project002.My.Resources.Resources.MapSpriteSheet
        Me.pbGFX.Location = New System.Drawing.Point(12, 12)
        Me.pbGFX.Name = "pbGFX"
        Me.pbGFX.Size = New System.Drawing.Size(160, 110)
        Me.pbGFX.TabIndex = 0
        Me.pbGFX.TabStop = False
        '
        'GFX
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(587, 450)
        Me.Controls.Add(Me.pbChar)
        Me.Controls.Add(Me.pbGFX)
        Me.Name = "GFX"
        Me.Text = "GFX"
        CType(Me.pbChar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbGFX, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pbGFX As PictureBox
    Friend WithEvents pbChar As PictureBox
End Class
