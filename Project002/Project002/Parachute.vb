Imports System.Drawing

Public Class Parachute

    'View port variables
    Public tileSize As Integer = 50
    Public HEIGHT As Integer = 539
    Public WIDTH As Integer = 515

    'Map variables
    Public Map(100, 100, 10) As Integer
    Public MapX As Integer = 20
    Public MapY As Integer = 20

    'Graphic variables
    Public G As Graphics
    Public BckGrdG As Graphics
    Public BckBuf As Bitmap
    Public bmpTile As Bitmap
    Public srec As Rectangle
    Public drec As Rectangle

    'Track if game is running 
    Public isRunning As Boolean = True

    'New System
    Dim moveRight As Boolean
    Dim moveLeft As Boolean
    Dim moveSpd As Integer = 10

    Dim maxMissleNum As Integer = 10
    Dim missleAry(10) As PictureBox
    Dim missleNum As Integer = 0
    Dim missleOnScrn(10) As Boolean

    Dim maxEnemyNum = 2
    Public enemyAry(maxEnemyNum) As PictureBox
    Public enemyOnScrn(maxEnemyNum) As Boolean
    Dim enemySpd As Integer = 20

    Dim scre As Integer

    Dim snd As New Media.SoundPlayer

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'PlayBackgroundSoundFile()

        Me.Show()
        Me.Focus()

        Me.Controls.Add(GFX.Canon)
        GFX.Canon.Top = 380
        GFX.Canon.Left = 250
        createMissles(maxMissleNum)

        Enemies.createEnemies(maxEnemyNum)

        Randomize()
        'Starting Timers
        Timer.Start()
        enemyTimer.Start()
        scoreTimer.Start()

        'Initialize graphic objects before setting values
        G = Me.CreateGraphics
        BckBuf = New Bitmap(WIDTH, HEIGHT)
        bmpTile = New Bitmap(GFX.pbGFX.Image)

        LoadMap()
        'Continue to draw background graphics
        gameLoop()

    End Sub

    'character and missle movement
    Private Sub Timer_Tick(sender As Object, e As EventArgs) Handles Timer.Tick
        Dim i As Integer
        Dim j As Integer

        If moveRight = True And (GFX.Canon.Left < Me.WIDTH - 40) Then
            GFX.Canon.Left += moveSpd
        End If
        If moveLeft = True And (GFX.Canon.Left > -10) Then
            GFX.Canon.Left -= moveSpd
        End If

        For i = 0 To maxMissleNum
            If missleOnScrn(i) = True Then
                missleAry(i).Top -= 10
            End If

            If missleAry(i).Top <= -19 Then
                missleOnScrn(i) = False

            End If

            'check for collision between enemies and missles
            For j = 0 To maxEnemyNum
                If Collision(missleAry(i), enemyAry(j)) Then
                    enemyAry(j).Top = 0
                    enemyAry(j).Left = CInt(Rnd() * Me.WIDTH)
                    missleAry(i).Visible = False
                    snd.SoundLocation = "oof.wav"
                    snd.Play()
                End If
            Next

        Next

    End Sub

    Private Sub enemyTimer_Tick(sender As Object, e As EventArgs) Handles enemyTimer.Tick
        Dim i As Integer
        Dim rand As Double

        For i = 0 To maxEnemyNum

            enemyAry(i).Top += enemySpd

            If enemyAry(i).Top > Me.HEIGHT Then
                Timer.Stop()
                enemyTimer.Stop()
                scoreTimer.Stop()
                MsgBox("Game Over BITCH!!")
                Application.Exit()
            End If

            rand = Rnd()
            If rand > 0.66 Then
                enemyAry(i).Left += 5
            ElseIf rand < 0.33 Then
                enemyAry(i).Left -= 5
            End If

            If enemyAry(i).Left < 5 Then
                enemyAry(i).Left += 10
            End If
            If enemyAry(i).Left > (Me.WIDTH - 40) Then
                enemyAry(i).Left -= 10
            End If
        Next

    End Sub

    Private Sub scoreTimer_Tick(sender As Object, e As EventArgs) Handles scoreTimer.Tick
        scre += 1
        Score.Text = "Score: " & scre

        If scre Mod 10 = 0 Then
            enemySpd += 5
        End If
    End Sub


    Private Function Collision(ByVal object1 As Object, ByVal object2 As Object) As Boolean
        Dim collieded As Boolean = False

        If object1.Top + object1.Height >= object2.Top And
                object2.Top + object2.Height >= object1.Top And
                object1.left + object1.width >= object2.left And
                object2.left + object2.width >= object1.left And
                object1.visible = True And
                object2.visible = True Then
            collieded = True
        End If

        Return collieded

    End Function

    Private Sub createMissles(ByVal num As Integer)

        For i = 0 To num

            Dim missle As New PictureBox
            missle.Image = Image.FromFile("C:\Users\gamep\Documents\COS\Spring 2019\AAS Project 002\Project002\Project002\My Project\RedLaserBeam.png")
            Me.Controls.Add(missle)
            missle.Width = 9
            missle.Height = 19
            missle.BorderStyle = BorderStyle.FixedSingle
            missle.BackColor = Color.Blue
            missle.Top = GFX.Canon.Top + GFX.Canon.Height / 2 - GFX.missle.Height / 2
            missle.Left = GFX.Canon.Left + GFX.Canon.Width / 2 - GFX.missle.Width / 2
            missle.BringToFront()
            missleAry(i) = missle
            missleAry(i).Visible = False
            missleOnScrn(i) = False

        Next

    End Sub

    Private Sub Parachute_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Dim i As Integer = 0
        Dim count As Integer = 1

        Select Case e.KeyValue

            Case Keys.Right
                moveRight = True

            Case Keys.Left
                moveLeft = True

            Case Keys.Space
                snd.SoundLocation = "laser.wav"
                snd.Play()

                For i = 0 To 9
                    If missleOnScrn(i) = True Then
                        count += 1
                    End If
                Next

                If count <= 9 Then
                    missleOnScrn(missleNum) = True
                    missleAry(missleNum).Visible = True

                    missleAry(missleNum).Top = GFX.Canon.Top + GFX.Canon.Height / 2 - missleAry(missleNum).Height / 2
                    missleAry(missleNum).Left = GFX.Canon.Left + GFX.Canon.Width / 2 - missleAry(missleNum).Width / 2
                    missleNum += 1

                    If missleNum = maxMissleNum Then missleNum = 0

                End If

        End Select
    End Sub

    Private Sub Parachute_KeyUp(sender As Object, e As KeyEventArgs) Handles Me.KeyUp
        Select Case e.KeyValue
            Case Keys.Right
                moveRight = False
            Case Keys.Left
                moveLeft = False
        End Select
    End Sub

    Private Sub PlayBackgroundSoundFile()
        Dim proc As New System.Diagnostics.Process()
        proc = Process.Start("C:\Users\gamep\Documents\COS\Spring 2019\AAS Project 002\Project002\Project002\bin\Debug\mortal.wav")

    End Sub

    Public Sub gameLoop()
        Do While isRunning = True
            Application.DoEvents()
            DrawGraphics()

        Loop
    End Sub

    Public Sub DrawGraphics()

        For x As Integer = -1 To 9
            For y As Integer = -1 To 9

                getSrcRect(MapX + x, MapY + y, tileSize, tileSize)

                drec = New Rectangle((x * tileSize), (y * tileSize), tileSize, tileSize)

                G.DrawImage(bmpTile, drec, srec, GraphicsUnit.Pixel)

            Next
        Next

        G = Graphics.FromImage(BckBuf)

        Try
            BckGrdG = Me.CreateGraphics()
        Catch ex As Exception
           
        End Try

        BckGrdG.DrawImage(BckBuf, 0, 0, WIDTH, HEIGHT)

        G.Clear(Color.Wheat)

    End Sub

    'getting image source for map tiles
    Public Sub getSrcRect(ByVal x As Integer, ByVal y As Integer, ByVal w As Integer, ByVal h As Integer)

        Select Case Map(x, y, 0)
            Case 0 'grass
                srec = New Rectangle(0, 0, tileSize, tileSize)
            Case 1 'wall
                srec = New Rectangle(50, 50, tileSize, tileSize)
            Case 2 'left edge
                srec = New Rectangle(50, 0, tileSize, tileSize)
                Map(x, y, 1) = 1
            Case 3 'right edge
                srec = New Rectangle(0, 50, tileSize, tileSize)
                Map(x, y, 1) = 1
            Case 4 'middle
                srec = New Rectangle(100, 0, tileSize, tileSize)
        End Select

    End Sub

    'Loading Map
    Private Sub LoadMap()
        'Wall
        For x As Integer = 20 To 29
            Map(x, 29, 0) = 1
        Next
        'Middle
        For x As Integer = 21 To 28
            Map(x, 28, 0) = 4
        Next
        'Left edge
        Map(20, 28, 0) = 2
        'Right edge
        Map(29, 28, 0) = 3

    End Sub

End Class