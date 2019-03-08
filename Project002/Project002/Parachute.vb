Imports System.Drawing

Public Class Parachute

    'Key detection
    Dim keyPushed As Short = 0

    Public Declare Function GetAsyncKeyState Lib "user32" (ByVal vkey As Integer) As Short

    Public Function GetKeyState(ByVal key1 As Integer) As Boolean

        Dim s As Short

        s = GetAsyncKeyState(key1)

        If s = 0 Then Return False
        Return True

    End Function

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

    'Character Variables
    Public bmpChar As Bitmap
    Public xPos As Integer = 0
    Public yPos As Integer = 0
    Dim moveSpd As Integer = 5
    Public moveDir As Short = 0
    Public LastDir As Short = 2

    'Terrain Detection Collision
    Dim guyX As Decimal = (MapX + 5) * tileSize
    Dim guyY As Decimal = (MapY + 8) * tileSize

    'Paint Brush
    Dim PaintBrush As Integer = 0


    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Show()
        Me.Focus()

        'Initialize graphic objects before setting values
        G = Me.CreateGraphics
        BckBuf = New Bitmap(WIDTH, HEIGHT)
        bmpTile = New Bitmap(GFX.pbGFX.Image)
        bmpChar = New Bitmap(GFX.pbChar.Image)

        LoadMap()

        StartGameLoop()

    End Sub

    Public Sub StartGameLoop()

        Do While isRunning = True

            'Keep application responsive
            Application.DoEvents()

            '1) check user input
            setMoveDir()
            moveChar(moveDir)
            '2) checking AI
            '3) updating object data (positions,status, etc..)
            '4) checking triggers and conditions

            '5) draw graphics
            DrawGraphics()

            '6) playing sound effects/music

            'Update Tick Counter
            ' TicCounter()

        Loop

    End Sub

    Public Sub DrawGraphics()
        'fill backbuffer
        'draw tiles
        For x As Integer = -1 To 12
            For y As Integer = -1 To 9

                getSrcRect(MapX + x, MapY + y, tileSize, tileSize)

                drec = New Rectangle((x * tileSize) + xPos, (y * tileSize) + yPos, tileSize, tileSize)

                G.DrawImage(bmpTile, drec, srec, GraphicsUnit.Pixel)

            Next
        Next



        'draw final layers

        'guys, menus, etc.
        getChar()
        bmpChar.MakeTransparent(Color.White)
        G.DrawImage(bmpChar, 5 * tileSize, 8 * tileSize, srec, GraphicsUnit.Pixel)

        G.DrawRectangle(Pens.Purple, mouseX * tileSize, mouseY * tileSize, tileSize, tileSize)

        'Display: Number of Tics, X & Y COORDS, Logical COORDS
        'G.DrawString("Ticks: " & numTics & vbCrLf &
        ' "TPS: " & maxTics & vbCrLf &
        ' "Mouse x: " & mouseX & vbCrLf &
        ' "Mouse y: " & mouseY & vbCrLf &
        ' "Mouse MapX: " & mMapX & vbCrLf &
        ' "Mouse MapY: " & mMapY & vbCrLf &
        ' "", Font, Brushes.Black, 500, 0)

        'copy backbuffer to graphics object
        G = Graphics.FromImage(BckBuf)

        'draw backbuffer to screen
        BckGrdG = CreateGraphics()
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

    Public Sub getChar()

        srec = New Rectangle(0, 0, tileSize, tileSize)

    End Sub

    Public Sub setMoveDir()
        If GetKeyState(Keys.A) = True Then moveDir = 1
        If GetKeyState(Keys.D) = True Then moveDir = 2

        If GetKeyState(Keys.A) = False And
           GetKeyState(Keys.D) = False Then

            moveDir = 0

        End If

        If moveDir <> 0 Then LastDir = moveDir

    End Sub

    Public Sub moveChar(ByVal dir As Short)

        Select Case dir
            Case 1

                If isBlocked(0) = False Then

                    xPos += moveSpd
                    guyX = (guyX - moveSpd)

                    If xPos >= tileSize Then
                        xPos = 0
                        MapX -= 1
                    End If

                End If

            Case 2
                If isBlocked(1) = False Then

                    xPos -= moveSpd
                    guyX = (guyX + moveSpd)

                    If xPos <= tileSize * -1 Then
                        xPos = 0
                        MapX += 1
                    End If
                End If
        End Select

    End Sub

    'Terain Collision
    Public Function isBlocked(ByVal dir As Short) As Boolean

        Select Case dir
            Case 0 'West
                If Map(Math.Ceiling(guyX / tileSize) - 1, (guyY / tileSize), 1) = 1 Then
                    Return True
                End If
            Case 1 'East
                If Map(Math.Floor(guyX / tileSize) + 1, (guyY / tileSize), 1) = 1 Then
                    Return True
                End If
        End Select

        Return False

    End Function

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
    ' Public tSec As Integer = TimeOfDay.Second
    'Public numTics As Integer = 0
    'Public maxTics As Integer = 0

    'Mouse movement
    'Private Sub Parachute_MouseMove(sender As Object, e As MouseEventArgs) Handles Me.MouseMove

    '        mouseX = Math.Floor(e.X / tileSize)
    '       mouseY = Math.Floor(e.Y / tileSize)

    '      mMapX = MapX + mouseX
    '     mMapY = MapY + mouseY

    'End Sub

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
    'Public Sub TicCounter()

    'If tSec = TimeOfDay.Second And isRunning = True Then

    '       numTics += 1

    'Else

    '       maxTics = numTics
    '      numTics = 0
    '     tSec = TimeOfDay.Second

    'End If

    'End Sub

End Class