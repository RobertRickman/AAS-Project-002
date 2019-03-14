Public Class Enemies
    Private health As Integer
    Public Shared maxEnemyNum = 1
    Public Shared enemyAry(maxEnemyNum) As PictureBox
    Public Shared enemyOnScrn(maxEnemyNum) As Boolean
    Public Shared enemySpd As Integer = 5

    Public Sub New()
        health = 100
    End Sub

    Public Property hp() As Integer
        Get
            Return health
        End Get
        Set(value As Integer)
            health = value
        End Set
    End Property

    Public Sub New(ByVal hp As Integer)
        health = hp
    End Sub

    Public Sub Attack(ByRef baseHealth As Integer)
        baseHealth -= 10
    End Sub

    Public Shared Sub enemyMovement()
        Dim i As Integer
        Dim rand As Double

        For i = 0 To maxEnemyNum

            enemyAry(i).Top += enemySpd

            If enemyAry(i).Top > Parachute.HEIGHT Then
                Parachute.Timer.Stop()
                Parachute.enemyTimer.Stop()
                Parachute.scoreTimer.Stop()
                MsgBox("Game Over")
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
            If enemyAry(i).Left > (Parachute.WIDTH - 40) Then
                enemyAry(i).Left -= 10
            End If
        Next
    End Sub
End Class
