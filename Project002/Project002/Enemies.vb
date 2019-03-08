Public Class Enemies
    Dim spriteRect As Rectangle
    Dim spriteBmp As Bitmap
    Dim moveSpd As Integer = 1
    Dim spriteType As Integer = 0

    'Critter Position
    Public enemyX As Integer = 0
    Public enemyY As Integer = 0

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
        enemyY += moveSpd
    End Sub

    Public Sub Update()
        'update enemy AI 
    End Sub


    Private Sub tmrUpdate_Tick(ByVal sender As Object, ByVal e As System.Timers.ElapsedEventArgs) Handles tmrUpdate.Elapsed
        moveEnemy()

    End Sub
End Class
