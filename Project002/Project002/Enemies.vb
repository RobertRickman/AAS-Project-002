Public Class Enemies
    Dim spriteRect As Rectangle
    Dim spriteBmp As Bitmap
    Dim moveSpd As Integer = 1
    Dim spriteType As Integer = 0

    'Critter Position
    Public enemyX As Integer = 50
    Public enemyY As Integer = 0

    Public targetX As Integer = (Parachute.MapX) * Parachute.tileSize

    'Update Timer
    Dim tmrSpeed As Integer = 10

    'start timer when class is called
    Dim WithEvents tmrUpdate As New System.Timers.Timer(tmrSpeed) With {.Enabled = True}

    Public Property enemiesType As Integer
        Get
            Return spriteType
        End Get
        Set(value As Integer)
            spriteType = value
            spriteSrc()
        End Set
    End Property

    Public Sub spriteSrc()

        spriteRect = New Rectangle(50, 0, 50, 50)

    End Sub

    Public Sub DrawEnemy()

        spriteBmp = GFX.pbChar.Image
        spriteBmp.MakeTransparent(Color.White)

        Parachute.G.DrawImage(spriteBmp, enemyX, enemyY, spriteRect, GraphicsUnit.Pixel)

    End Sub

    Public Sub moveEnemy()

        If enemyY <> 350 Then
            enemyY += moveSpd
        End If

        If targetX <> 1250 Then
            If guyX < 1250 Then
                enemyX += moveSpd
            ElseIf guyX > 1250 Then
                enemyX -= moveSpd
            End If
        End If
    End Sub

    Public Sub Update()
        'update enemy AI 

        moveEnemy()
        targetX = Parachute.Map(Math.Ceiling(guyX / Parachute.tileSize) - 1, (guyY / Parachute.tileSize), 1)
    End Sub


    Private Sub tmrUpdate_Tick(ByVal sender As Object, ByVal e As System.Timers.ElapsedEventArgs) Handles tmrUpdate.Elapsed
        Update()
    End Sub
End Class
