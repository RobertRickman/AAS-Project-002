Public Class Enemies
    Property health As Integer

    Public Sub New()
        health = 100
    End Sub

    Public Sub New(ByVal hp As Integer)
        health = hp
    End Sub

    Public Function recievingDmg(ByVal dmg As Integer) As Integer
        Return (health - dmg)
    End Function

End Class
