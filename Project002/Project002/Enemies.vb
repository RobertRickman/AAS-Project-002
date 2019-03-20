Public Class Enemies

    Private enemy As PictureBox
    Private spritesrc As Image
    Private bstyle As BorderStyle
    Private bcolor As Color
    Private w As Integer
    Private h As Integer
    Private y As Integer
    Private x As Integer

    Public Property enem As PictureBox
        Get
            Return enemy
        End Get
        Set(value As PictureBox)
            enemy = value
        End Set
    End Property

    Public Property sprite As Image
        Get
            Return spritesrc
        End Get
        Set(value As Image)
            spritesrc = value
        End Set
    End Property

    Public Property bstl As BorderStyle
        Get
            Return bstyle
        End Get
        Set(value As BorderStyle)
            bstyle = value
        End Set
    End Property

    Public Property brdcol As Color
        Get
            Return bcolor
        End Get
        Set(value As Color)
            bcolor = value
        End Set
    End Property

    Public Property width As Integer
        Get
            Return w
        End Get
        Set(value As Integer)
            w = value
        End Set
    End Property

    Public Property height As Integer
        Get
            Return h
        End Get
        Set(value As Integer)
            h = value
        End Set
    End Property

    Public Property yPos As Integer
        Get
            Return y
        End Get
        Set(value As Integer)
            y = value
        End Set
    End Property

    Public Property xPos As Integer
        Get
            Return x
        End Get
        Set(value As Integer)
            x = value
        End Set
    End Property

    Public Function createEnemies() As PictureBox
        enem.Image = sprite

        enem.Width = width
        enem.Height = height
        enem.BorderStyle = bstl
        enem.BackColor = brdcol
        enem.Top = y
        enem.Left = x
        enem.BringToFront()
        enem.Visible = True

        Return enem

    End Function

    Public Shared maxEnemyNum = 0
    Public Shared enemySpd As Double = 1

    Public Sub enemyMovement()

        Try

            Dim i As Integer
            Dim rand As Double

            For i = 0 To maxEnemyNum

                enem.Top += enemySpd

                If enem.Top > Parachute.HEIGHT Then
                    Parachute.Timer.Stop()
                    Parachute.enemyTimer.Stop()
                    Parachute.scoreTimer.Stop()
                    MsgBox("Game Over")
                    Parachute.Visible = False
                    Parachute.Close()
                End If

                rand = Rnd()

                If rand > 0.66 Then
                    enem.Left += 5
                ElseIf rand < 0.33 Then
                    enem.Left -= 5
                End If

                If enem.Left < 5 Then
                    enem.Left += 10
                End If

                If enem.Left > (Parachute.WIDTH - 40) Then
                    enem.Left -= 10
                End If

            Next

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
End Class
