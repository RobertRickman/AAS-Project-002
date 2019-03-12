<<<<<<< HEAD
﻿Imports System.Drawing

Public Class Parachute

    'View port variables
    Public tileSize As Integer = 50
    Public HEIGHT As Integer = 800
    Public WIDTH As Integer = 800

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

    'Mouse locations/movement
    Public mouseX As Integer
    Public mouseY As Integer
    Public mMapX As Integer
    Public mMapY As Integer

    'Paint Brush
    Dim PaintBrush As Integer = 0

    'Character 
    Public chara As Character

    'Enemy
    Dim gbEnemy As Enemies

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Show()
        Me.Focus()

        'Initialize graphic objects before setting values
        G = Me.CreateGraphics
        BckBuf = New Bitmap(WIDTH, HEIGHT)
        bmpTile = New Bitmap(GFX.pbGFX.Image)

        'Initialize character graphic objects 
        chara = New Character
        chara.characType = 1

        'Initialize enemy graphic objects
        gbEnemy = New Enemies
        gbEnemy.enemiesType = 1

        LoadMap()

        StartGameLoop()

    End Sub

    Public Sub StartGameLoop()
        MsgBox(gbEnemy.enemyX)
        Do While isRunning = True
            MsgBox(gbEnemy.targetX)
            'Keep application responsive
            Application.DoEvents()

            '1) check user input
            '2) checking AI
            '3) updating object data (positions,status, etc..)
            chara.Update()
            '4) checking triggers and conditions

            '5) draw graphics
            DrawGraphics()
            '6) playing sound effects/music

            'Update Tick Counter
            TicCounter()

        Loop

    End Sub

    Public Sub DrawGraphics()

        'fill backbuffer
        'draw tiles
        For x As Integer = -1 To 12
            For y As Integer = -1 To 9

                getSrcRect(MapX + x, MapY + y, tileSize, tileSize)

                drec = New Rectangle((x * tileSize) + chara.xPos, (y * tileSize) + chara.yPos, tileSize, tileSize)

                G.DrawImage(bmpTile, drec, srec, GraphicsUnit.Pixel)

            Next
        Next

        'draw final layers

        'guys, menus, etc.
        chara.DrawChar()
        gbEnemy.DrawEnemy()

        'displays mouse position
        G.DrawRectangle(Pens.Purple, mouseX * tileSize, mouseY * tileSize, tileSize, tileSize)

        'Display: Number of Tics, X & Y COORDS, Logical COORDS
        G.DrawString("Ticks: " & numTics & vbCrLf &
         "TPS: " & maxTics & vbCrLf &
         "Mouse x: " & mouseX & vbCrLf &
         "Mouse y: " & mouseY & vbCrLf &
         "Mouse MapX: " & mMapX & vbCrLf &
         "Mouse MapY: " & mMapY & vbCrLf &
         "", Font, Brushes.Black, 500, 0)

        'copy backbuffer to graphics object
        G = Graphics.FromImage(BckBuf)

        'draw backbuffer to screen
        BckGrdG = Me.CreateGraphics()
        BckGrdG.DrawImage(BckBuf, 0, 0, WIDTH, HEIGHT)

        ' fix overdraw
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

    'TPS variables
    Public tSec As Integer = TimeOfDay.Second
    Public numTics As Integer = 0
    Public maxTics As Integer = 0

    'Mouse movement
    Private Sub Parachute_MouseMove(sender As Object, e As MouseEventArgs) Handles Me.MouseMove

        mouseX = Math.Floor(e.X / tileSize)
        mouseY = Math.Floor(e.Y / tileSize)

        mMapX = MapX + mouseX
        mMapY = MapY + mouseY

    End Sub

    'Mouse Click events
    '    Private Sub Parachute_MouseClick(sender As Object, e As MouseEventArgs) Handles Me.MouseClick

    ' If mouseX = 10 And mouseY = 4 Then
    '        PaintBrush = 1
    'ElseIf mouseX = 10 And mouseY = 6 Then
    '       PaintBrush = 2
    'End If

    'Select Case PaintBrush
    'Case 0
    'Case 1 'Red
    '           Map(mMapX, mMapY, 0) = 1
    'Case 2 'Blue
    '           Map(mMapX, mMapY, 0) = 2
    'End Select

    'End Sub

    'Tic per second 
    Public Sub TicCounter()

        If tSec = TimeOfDay.Second And isRunning = True Then

            numTics += 1

        Else

            maxTics = numTics
            numTics = 0
            tSec = TimeOfDay.Second

        End If

    End Sub

=======
﻿Imports System.Drawing

Public Class Parachute

    'View port variables
    Public tileSize As Integer = 50
    Public HEIGHT As Integer = 800
    Public WIDTH As Integer = 800

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

    'Mouse locations/movement
    Public mouseX As Integer
    Public mouseY As Integer
    Public mMapX As Integer
    Public mMapY As Integer

    'Add bullet
    ' Private src As BitmapImage
    Private imgBullet As Image

    'Bullet related
    Private bulletSpeed As Integer    Private isFireReady As Boolean    Private bulletTimer As DispatcherTimer    Private newBulletPosition As Double    Private offsetBullet As Double    Private bullet As Image


    'Paint Brush
    Dim PaintBrush As Integer = 0

    'Character 
    Dim chara As Character

    'Enemy
    Dim gbEnemy As Enemies
    ' Sub New()

    'Set image bmp
    'src = New BitmapImage()
    'src.BeginInit()
    'src.UriSource = New Uri("images/bullet.png", UriKind.RelativeOrAbsolute)
    ''src.CacheOption = BitmapCacheOption.OnLoad
    'src.EndInit()


    'End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Show()
        Me.Focus()

        'Initialize graphic objects before setting values
        G = Me.CreateGraphics
        BckBuf = New Bitmap(WIDTH, HEIGHT)
        bmpTile = New Bitmap(GFX.pbGFX.Image)

        'Initialize character graphic objects 
        chara = New Character
        chara.characType = 1

        'Initialize enemy graphic objects
        gbEnemy = New Enemies
        gbEnemy.enemiesType = 1

        LoadMap()

        StartGameLoop()

    End Sub

    Public Sub StartGameLoop()

        Do While isRunning = True

            'Keep application responsive
            Application.DoEvents()

            '1) check user input
            '2) checking AI
            '3) updating object data (positions,status, etc..)
            chara.Update()
            '4) checking triggers and conditions

            '5) draw graphics
            DrawGraphics()
            '6) playing sound effects/music

            'Update Tick Counter
            TicCounter()

        Loop

    End Sub

    Public Sub DrawGraphics()

        'fill backbuffer
        'draw tiles
        For x As Integer = -1 To 12
            For y As Integer = -1 To 9

                getSrcRect(MapX + x, MapY + y, tileSize, tileSize)

                drec = New Rectangle((x * tileSize) + chara.xPos, (y * tileSize) + chara.yPos, tileSize, tileSize)

                G.DrawImage(bmpTile, drec, srec, GraphicsUnit.Pixel)

            Next
        Next

        'draw final layers

        'guys, menus, etc.
        chara.DrawChar()
        gbEnemy.DrawEnemy()

        'displays mouse position
        G.DrawRectangle(Pens.Purple, mouseX * tileSize, mouseY * tileSize, tileSize, tileSize)

        'Display: Number of Tics, X & Y COORDS, Logical COORDS
        G.DrawString("Ticks: " & numTics & vbCrLf &
         "TPS: " & maxTics & vbCrLf &
         "Mouse x: " & mouseX & vbCrLf &
         "Mouse y: " & mouseY & vbCrLf &
         "Mouse MapX: " & mMapX & vbCrLf &
         "Mouse MapY: " & mMapY & vbCrLf &
         "", Font, Brushes.Black, 500, 0)

        'copy backbuffer to graphics object
        G = Graphics.FromImage(BckBuf)

        'draw backbuffer to screen
        BckGrdG = Me.CreateGraphics()
        BckGrdG.DrawImage(BckBuf, 0, 0, WIDTH, HEIGHT)

        ' fix overdraw
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

    'TPS variables
    Public tSec As Integer = TimeOfDay.Second
    Public numTics As Integer = 0
    Public maxTics As Integer = 0

    'Mouse movement
    Private Sub Parachute_MouseMove(sender As Object, e As MouseEventArgs) Handles Me.MouseMove

        mouseX = Math.Floor(e.X / tileSize)
        mouseY = Math.Floor(e.Y / tileSize)

        mMapX = MapX + mouseX
        mMapY = MapY + mouseY

    End Sub

    'Mouse Click events
    '    Private Sub Parachute_MouseClick(sender As Object, e As MouseEventArgs) Handles Me.MouseClick

    ' If mouseX = 10 And mouseY = 4 Then
    '        PaintBrush = 1
    'ElseIf mouseX = 10 And mouseY = 6 Then
    '       PaintBrush = 2
    'End If

    'Select Case PaintBrush
    'Case 0
    'Case 1 'Red
    '           Map(mMapX, mMapY, 0) = 1
    'Case 2 'Blue
    '           Map(mMapX, mMapY, 0) = 2
    'End Select

    'End Sub

    'Tic per second 
    Public Sub TicCounter()

        If tSec = TimeOfDay.Second And isRunning = True Then

            numTics += 1

        Else

            maxTics = numTics
            numTics = 0
            tSec = TimeOfDay.Second

        End If

    End Sub

    'Case Keys.Space
    'If isFireReady Then
    '                ' makeBullet(imgShip, bullet)
    '                ' FireBullet()
    '                isFireReady = False
    '            End If

    'Case Else

    'robert added this 
    'Private Sub makeBullet(startObject As Image, laser As Image)
    '    bullet.Source = src
    '    bullet.Stretch = Stretch.Uniform

    '    bullet.Width = 40
    '    bullet.Height = 40

    '    Canvas.SetLeft(bullet, Canvas.GetLeft(startObject) + ((startObject.Width / 2) - offsetBullet))
    '    Canvas.SetTop(bullet, Canvas.GetTop(startObject))

    '    mainCanvas.Children.Add(bullet)
    'End Sub

    'Private Sub FireBullet()

    '    bulletTimer = New DispatcherTimer()
    '    AddHandler bulletTimer.Tick, AddressOf BulletTimer_Tick
    '    ' bulletTimer.Interval = TimeSpan.FromMilliseconds(speed)

    '    'bulletTimer.Start()

    'End Sub
    'Private Sub BulletTimer_Tick(sender As Object, e As EventArgs)
    '    newBulletPosition = (Canvas.GetTop(bullet) - bulletSpeed)
    '    Canvas.SetTop(bullet, newBulletPosition)

    '    If newBulletPosition < (0 - imgShip.Height) Then
    '        Console.WriteLine("done")
    '        bulletTimer.Stop()


    'mainCanvas.Children.Remove(bullet)
    '        isFireReady = True
    '    Else

    '        Console.WriteLine("running")
    '    End If

    'End Sub


>>>>>>> 957b002175d8e9ed2bbb6392ebbc93ac17f4cf55
End Class