Public Class Form3
    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Button1.Enabled = True
        Button2.Enabled = True
        Button3.Enabled = True
        Button4.Enabled = True

        If gv.carrierCount = 0 Then
            Button1.Enabled = False
        End If
        If gv.battleshipCount = 0 Then
            Button2.Enabled = False
        End If
        If gv.cruiseCount = 0 Then
            Button3.Enabled = False
        End If
        If gv.destroyerCount = 0 Then
            Button4.Enabled = False
        End If

        Console.WriteLine(gv.selection)

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        My.Forms.Form2.Show()
        Me.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click, Button2.Click, Button3.Click, Button4.Click
        Dim btn As Button = CType(sender, Button)
        gv.ship = btn.Text
        My.Forms.Form4.Activate()
        My.Forms.Form4.Show()
        Me.Close()
    End Sub
End Class