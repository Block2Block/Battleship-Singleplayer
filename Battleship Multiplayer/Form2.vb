﻿Public Class Form2
    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Forms.Form1.Hide()

        gv.battleshipCount = 1
        gv.carrierCount = 1
        gv.cruiseCount = 2
        gv.destroyerCount = 1


    End Sub

    Private Sub A1_Click(sender As Object, e As EventArgs) Handles A1.Click, A2.Click, A3.Click, A4.Click, A5.Click, A6.Click, A7.Click, A8.Click, A9.Click, A10.Click, B1.Click, B2.Click, B3.Click, B4.Click, B5.Click, B6.Click, B7.Click, B8.Click, B9.Click, B10.Click, C1.Click, C2.Click, C3.Click, C4.Click, C5.Click, C6.Click, C7.Click, C8.Click, C9.Click, C10.Click, D1.Click, D2.Click, D3.Click, D4.Click, D5.Click, D6.Click, D7.Click, D8.Click, D9.Click, D10.Click, E1.Click, E2.Click, E3.Click, E4.Click, E5.Click, E6.Click, E7.Click, E8.Click, E9.Click, E10.Click, F1.Click, F2.Click, F3.Click, F4.Click, F5.Click, F6.Click, F7.Click, F8.Click, F9.Click, F10.Click, G1.Click, G2.Click, G3.Click, G4.Click, G5.Click, G6.Click, G7.Click, G8.Click, G9.Click, G10.Click, H1.Click, H2.Click, H3.Click, H4.Click, H5.Click, H6.Click, H7.Click, H8.Click, H9.Click, H10.Click, I1.Click, I2.Click, I3.Click, I4.Click, I5.Click, I6.Click, I7.Click, I8.Click, I9.Click, I10.Click, J1.Click, J2.Click, J3.Click, J4.Click, J5.Click, J6.Click, J7.Click, J8.Click, J9.Click, J10.Click
        Dim btn As Button = CType(sender, Button)
        If btn.Text = "" Then
            gv.selection = btn.Name
            My.Forms.Form3.Activate()
            My.Forms.Form3.Show()
            Me.Hide()
        Else
            MsgBox("That space is already occupied!")
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        My.Forms.Form1.Activate()
        My.Forms.Form1.Show()
        Me.Close()
    End Sub
End Class