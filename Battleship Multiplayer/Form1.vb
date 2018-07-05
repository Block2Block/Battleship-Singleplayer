Public Class Form1
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        MsgBox("Thanks for playing! Play again soon!")
        End
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        My.Forms.Form2.Activate()
        My.Forms.Form2.Show()
        Me.Close()


    End Sub
End Class
