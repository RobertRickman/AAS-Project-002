﻿Imports System.Drawing
Imports System.Media

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

    Dim multiMissleMax As Integer = 1
    Dim multiMAry(multiMissleMax) As PictureBox
    Dim multiMNum As Integer = 0
    Dim multiMOnScrn(multiMissleMax) As Boolean

    Dim entities(Enemies.maxEnemyNum) As Enemies
    Dim enemyOnScrn(Enemies.maxEnemyNum) As Boolean
    Dim enemyL As New ArrayList()

    Dim scre As Integer
    Dim enscre As Integer

    Dim baseHealth As Integer = 100

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try

            'PlayBackgroundSoundFile()

            Me.Show()
            Me.Focus()

            'add character to screen plus position
            Me.Controls.Add(GFX.Canon)
            GFX.Canon.Top = 380
            GFX.Canon.Left = 250

            'creating missles and enemies
            createMissles(maxMissleNum)
            'createMultiMissle(multiMissleMax)          

            Randomize()

            'Starting Timers
            Timer.Start()
            enemyTimer.Start()
            scoreTimer.Start()

            For i As Integer = 0 To Enemies.maxEnemyNum
                entities(i) = New Enemies

                entities(i).enem = New PictureBox

                entities(i).sprite = My.Resources.Goblinp50
                entities(i).width = 50
                entities(i).height = 50
                entities(i).bstl = BorderStyle.FixedSingle
                entities(i).brdcol = Color.Red
                entities(i).yPos = 0
                entities(i).xPos = i * 90
                entities(i).enem.BringToFront()
                enemyOnScrn(i) = True
                enemyL.Add(entities(i))
                Me.Controls.Add(enemyL.Item(i).createEnemies())
            Next

            'Initialize graphic objects before setting values
            G = Me.CreateGraphics
            BckBuf = New Bitmap(WIDTH, HEIGHT)
            bmpTile = New Bitmap(GFX.pbGFX.Image)

            If Me.Visible = True Then
                'load certain images
                LoadMap()

                'Continue to draw background graphics as objects move
                gameLoop()
            End If

        Catch ex As Exception
            MsgBox("FormLoad" & ex.Message)
        End Try

    End Sub

    'character and missle movement
    Private Sub Timer_Tick(sender As Object, e As EventArgs) Handles Timer.Tick

        Try

            Dim i As Integer
            Dim j As Integer

            'moving right if not going past right boundry and moveright is true
            If moveRight = True And (GFX.Canon.Left < Me.WIDTH - 40) Then
                GFX.Canon.Left += moveSpd
            End If

            'moving left if not going past left boundry and moveleft is true
            If moveLeft = True And (GFX.Canon.Left > -10) Then
                GFX.Canon.Left -= moveSpd
            End If

            For i = 0 To maxMissleNum

                'if missle is spawned/onscreen then move it up
                If missleOnScrn(i) = True Then
                    missleAry(i).Top -= 10
                End If
                'if missle has gone out of bounds make it invisible
                If missleAry(i).Top <= -19 Then
                    missleOnScrn(i) = False
                End If


                For j = 0 To Enemies.maxEnemyNum

                    'if the missle an enemy objects are in the same area on the form then reset enemy location to the top
                    'and to the right or left randomly and then make missle invisible, play oof sound
                    If Collision(missleAry(i), entities(j).enem) Then
                        entities(j).enem.Top = 0
                        entities(j).enem.Left = CInt(Rnd() * Me.WIDTH)
                        missleAry(i).Visible = False
                        My.Computer.Audio.Play(My.Resources.oof, AudioPlayMode.Background)
                    End If

                Next

            Next

        Catch ex As Exception
            MsgBox("TimerTick" & ex.Message)
        End Try

    End Sub

    Private Sub enemyTimer_Tick(sender As Object, e As EventArgs) Handles enemyTimer.Tick

        enscre += 1
        For i As Integer = 0 To Enemies.maxEnemyNum
            entities(i).enemyMovement()
        Next

    End Sub

    Private Sub scoreTimer_Tick(sender As Object, e As EventArgs) Handles scoreTimer.Tick

        Try

            scre += 1
            Score.Text = "Score: " & scre

            If scre Mod 10 = 0 Then

                Enemies.enemySpd += 0.5

                For i As Integer = 0 To Enemies.maxEnemyNum
                    enemyL.Item(i).enem.Visible = False
                Next

                Enemies.maxEnemyNum += 1

                ReDim entities(Enemies.maxEnemyNum)

                ReDim enemyOnScrn(Enemies.maxEnemyNum)

                enemyL.Clear()

                For x As Integer = 0 To Enemies.maxEnemyNum

                    entities(x) = New Enemies
                    entities(x).enem = New PictureBox

                    entities(x).sprite = My.Resources.Goblinp50
                    entities(x).width = 50
                    entities(x).height = 50
                    entities(x).bstl = BorderStyle.FixedSingle
                    entities(x).brdcol = Color.Red
                    entities(x).yPos = 0
                    entities(x).xPos = x * 90
                    entities(x).enem.BringToFront()
                    enemyOnScrn(x) = True
                    enemyL.Add(entities(x))
                    Me.Controls.Add(enemyL.Item(x).createEnemies())

                Next
                moveSpd += 2
            End If

        Catch ex As Exception
            MsgBox("scoreTimer" & ex.Message)
        End Try

    End Sub


    Private Function Collision(ByVal object1 As Object, ByVal object2 As Object) As Boolean

        Try

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

        Catch ex As Exception
            MsgBox("colieded" & ex.Message)
        End Try

    End Function

    Private Sub createMissles(ByVal num As Integer)

        Try

            For i = 0 To num

                Dim missle As New PictureBox
                missle.Image = My.Resources.RedLaserBeam
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

        Catch ex As Exception
            MsgBox("CreateMissles" & ex.Message)
        End Try

    End Sub

    'Private Sub createMultiMissle(ByVal num As Integer)
    '    Try

    '        For i = 0 To num
    '            Dim multMissle As New PictureBox
    '            multMissle.Image = My.Resources.RedLaserBeam

    '            Me.Controls.Add(multMissle)
    '            multMissle.Width = 9
    '            multMissle.Height = 19
    '            multMissle.BorderStyle = BorderStyle.FixedSingle
    '            multMissle.BackColor = Color.Blue
    '            multMissle.Top = GFX.Canon.Top + GFX.Canon.Height / 2 - GFX.missle.Height / 2
    '            multMissle.Left = i * 90
    '            multMissle.BringToFront()
    '            multiMAry(i) = multMissle
    '            multiMAry(i).Visible = False
    '            multiMOnScrn(i) = False
    '        Next
    '    Catch ex As Exception
    '        MsgBox("MultiMissles" & ex.Message)
    '    End Try
    'End Sub

    Private Sub Parachute_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown

        Try

            Dim i As Integer = 0
            Dim count As Integer = 1

            Select Case e.KeyValue

                Case Keys.Right
                    moveRight = True

                Case Keys.Left
                    moveLeft = True

                Case Keys.Space
                    My.Computer.Audio.Play(My.Resources.laser, AudioPlayMode.Background)

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

        Catch ex As Exception
            MsgBox("keydown" & ex.Message)
        End Try

    End Sub

    Private Sub Parachute_KeyUp(sender As Object, e As KeyEventArgs) Handles Me.KeyUp

        Try

            Select Case e.KeyValue
                Case Keys.Right
                    moveRight = False
                Case Keys.Left
                    moveLeft = False
            End Select

        Catch ex As Exception
            MsgBox("keyUp" & ex.Message)
        End Try

    End Sub

    Public Sub gameLoop()

        Try

            If Me.Visible = False Then
                isRunning = False
                Environment.Exit(Environment.ExitCode)
            End If

            Do While isRunning = True
                Application.DoEvents()
                DrawGraphics()
            Loop

        Catch ex As Exception
            MsgBox("gameLoop" & ex.Message)
        End Try

    End Sub

    Public Sub DrawGraphics()

        Try

            If Me.Visible = False Then
                Environment.Exit(Environment.ExitCode)
            End If

            For x As Integer = -1 To 9
                For y As Integer = -1 To 9

                    getSrcRect(MapX + x, MapY + y, tileSize, tileSize)

                    drec = New Rectangle((x * tileSize), (y * tileSize), tileSize, tileSize)

                    G.DrawImage(bmpTile, drec, srec, GraphicsUnit.Pixel)

                Next
            Next

            G = Graphics.FromImage(BckBuf)

            BckGrdG = Me.CreateGraphics()

            BckGrdG.DrawImage(BckBuf, 0, 0, WIDTH, HEIGHT)

            G.Clear(Color.Wheat)

        Catch ex As Exception
            MsgBox("DrawG" & ex.Message)
        End Try

    End Sub

    'getting image source for map tiles
    Public Sub getSrcRect(ByVal x As Integer, ByVal y As Integer, ByVal w As Integer, ByVal h As Integer)

        Try

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

        Catch ex As Exception
            MsgBox("getSrc" & ex.Message)
        End Try

    End Sub

    'Loading Map
    Private Sub LoadMap()

        Try

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

        Catch ex As Exception
            MsgBox("LoadMap" & ex.Message)
        End Try

    End Sub

End Class