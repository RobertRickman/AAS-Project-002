Public Class Enemies

    Public Shared Sub createEnemies(ByVal num As Integer)

        For i = 0 To num
            Dim enemy As New PictureBox
            enemy.Image = Image.FromFile("C:\Users\gamep\Documents\COS\Spring 2019\AAS Project 002\Project002\Project002\My Project\Goblinp50.png")

            Parachute.Controls.Add(enemy)
            enemy.Width = 50
            enemy.Height = 50
            enemy.BackColor = Color.Red
            enemy.Top = 50
            enemy.Left = i * 90
            enemy.BringToFront()
            Parachute.enemyAry(i) = enemy
            Parachute.enemyAry(i).Visible = True
            Parachute.enemyOnScrn(i) = True

        Next
    End Sub
End Class
