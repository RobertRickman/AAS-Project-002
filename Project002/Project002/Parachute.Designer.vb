<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Parachute
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
        Me.components = New System.ComponentModel.Container()
        Me.Timer = New System.Windows.Forms.Timer(Me.components)
        Me.enemyTimer = New System.Windows.Forms.Timer(Me.components)
        Me.Score = New System.Windows.Forms.Label()
        Me.scoreTimer = New System.Windows.Forms.Timer(Me.components)
        Me.SuspendLayout()
        '
        'Timer
        '
        Me.Timer.Interval = 20
        '
        'enemyTimer
        '
        Me.enemyTimer.Interval = 200
        '
        'Score
        '
        Me.Score.AutoSize = True
        Me.Score.BackColor = System.Drawing.Color.LimeGreen
        Me.Score.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Score.Location = New System.Drawing.Point(391, 9)
        Me.Score.Name = "Score"
        Me.Score.Size = New System.Drawing.Size(61, 20)
        Me.Score.TabIndex = 0
        Me.Score.Text = "Score:"
        '
        'scoreTimer
        '
        Me.scoreTimer.Interval = 1000
        '
        'Parachute
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(499, 500)
        Me.Controls.Add(Me.Score)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(515, 539)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(515, 539)
        Me.Name = "Parachute"
        Me.Text = "Parachute"
        Me.TransparencyKey = System.Drawing.Color.Transparent
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Timer As Timer
    Friend WithEvents enemyTimer As Timer
    Friend WithEvents Score As Label
    Friend WithEvents scoreTimer As Timer
End Class
