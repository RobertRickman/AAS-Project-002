Public Class Character

    'Character Variables
    Dim spriteChar As Bitmap
    Dim spriteRect As Rectangle
    Dim spriteType As Integer
    Dim moveSpd As Integer = 5
    Dim moveDir As Short = 0
    Dim LastDir As Short = 2

    'Character Position Variables
    Public xPos As Integer = 0
    Public yPos As Integer = 0

    'Terrain Detection Collision
    Dim guyX As Decimal = (Parachute.MapX + 5) * Parachute.tileSize
    Dim guyY As Decimal = (Parachute.MapY + 8) * Parachute.tileSize

    'Update Timer
    'Dim tmrSpeed As Integer = 10

    'Start Timer
    'Dim WithEvents tmrUpdate As New System.Timers.Timer(tmrSpeed) With {.Enabled = True}

    Public Property characType As Integer
        Get
            Return spriteType
        End Get
        Set(value As Integer)
            spriteType = value
            getChar()
        End Set
    End Property

    'Key detection
    Dim keyPushed As Short = 0

    'Sprite Source
    Public Sub getChar()

        spriteRect = New Rectangle(0, 0, Parachute.tileSize, Parachute.tileSize)

    End Sub

    Public Sub DrawChar()
        spriteChar = GFX.pbChar.Image
        spriteChar.MakeTransparent(Color.White)

        Parachute.G.DrawImage(spriteChar, 5 * (Parachute.tileSize), 8 * (Parachute.tileSize - 4), spriteRect, GraphicsUnit.Pixel)
    End Sub

    'Key Detection
    Public Declare Function GetAsyncKeyState Lib "user32" (ByVal vkey As Integer) As Short

    Public Function GetKeyState(ByVal key1 As Integer) As Boolean

        Dim s As Short

        s = GetAsyncKeyState(key1)

        If s = 0 Then Return False
        Return True

    End Function

    'Setting sprite to direction of movement
    Public Sub setMoveDir()
        If GetKeyState(Keys.A) = True Then moveDir = 1
        If GetKeyState(Keys.D) = True Then moveDir = 2

        If GetKeyState(Keys.A) = False And
           GetKeyState(Keys.D) = False Then

            moveDir = 0

        End If

        If moveDir <> 0 Then LastDir = moveDir

    End Sub

    'Moving Character
    Public Sub moveChar(ByVal dir As Short)

        Select Case dir
            Case 1

                If isBlocked(0) = False Then

                    xPos += moveSpd
                    guyX = (guyX - moveSpd)

                    If xPos >= Parachute.tileSize Then
                        xPos = 0
                        Parachute.MapX -= 1
                    End If

                End If

            Case 2
                If isBlocked(1) = False Then

                    xPos -= moveSpd
                    guyX = (guyX + moveSpd)

                    If xPos <= Parachute.tileSize * -1 Then
                        xPos = 0
                        Parachute.MapX += 1
                    End If
                End If
        End Select

    End Sub

    'Terain Collision
    Public Function isBlocked(ByVal dir As Short) As Boolean

        Select Case dir
            Case 0 'West
                If Parachute.Map(Math.Ceiling(guyX / Parachute.tileSize) - 1, (guyY / Parachute.tileSize), 1) = 1 Then
                    Return True
                End If
            Case 1 'East
                If Parachute.Map(Math.Floor(guyX / Parachute.tileSize) + 1, (guyY / Parachute.tileSize), 1) = 1 Then
                    Return True
                End If
        End Select

        Return False

    End Function

    Public Sub Update()
        setMoveDir()
        moveChar(moveDir)
    End Sub

    'Private Sub tmrUpdate_Tick(ByVal sender As Object, ByVal e As System.Timers.ElapsedEventArgs) Handles tmrUpdate.Elapsed
    '    moveChar(moveDir)
    ' End Sub

End Class
