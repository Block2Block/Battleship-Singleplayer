﻿Public Class Form6

    Dim letters() As Char = {"A"c, "B"c, "C"c, "D"c, "E"c, "F"c, "G"c, "H"c, "I"c, "J"c}

    Private Sub A1_Click(sender As Object, e As EventArgs) Handles A1.Click, A2.Click, A3.Click, A4.Click, A5.Click, A6.Click, A7.Click, A8.Click, A9.Click, A10.Click, B1.Click, B2.Click, B3.Click, B4.Click, B5.Click, B6.Click, B7.Click, B8.Click, B9.Click, B10.Click, C1.Click, C2.Click, C3.Click, C4.Click, C5.Click, C6.Click, C7.Click, C8.Click, C9.Click, C10.Click, D1.Click, D2.Click, D3.Click, D4.Click, D5.Click, D6.Click, D7.Click, D8.Click, D9.Click, D10.Click, E1.Click, E2.Click, E3.Click, E4.Click, E5.Click, E6.Click, E7.Click, E8.Click, E9.Click, E10.Click, F1.Click, F2.Click, F3.Click, F4.Click, F5.Click, F6.Click, F7.Click, F8.Click, F9.Click, F10.Click, G1.Click, G2.Click, G3.Click, G4.Click, G5.Click, G6.Click, G7.Click, G8.Click, G9.Click, G10.Click, H1.Click, H2.Click, H3.Click, H4.Click, H5.Click, H6.Click, H7.Click, H8.Click, H9.Click, H10.Click, I1.Click, I2.Click, I3.Click, I4.Click, I5.Click, I6.Click, I7.Click, I8.Click, I9.Click, I10.Click, J1.Click, J2.Click, J3.Click, J4.Click, J5.Click, J6.Click, J7.Click, J8.Click, J9.Click, J10.Click
        Dim btn As Button = CType(sender, Button)
        btn.Enabled = False
        For Each x In gv.shipLocationsAI
            If x.Contains(btn.Name) Then
                btn.Text = "X"
                gv.shipLocationsAI.Remove(x)
                x.Remove(btn.Name)
                If x.Count > 0 Then
                    gv.shipLocationsAI.Add(x)
                    MsgBox("Hit!")
                    Exit Sub
                End If
                If gv.shipLocationsAI.Count = 0 Then
                    MsgBox("Congratulations, you win!")
                    Form1.Activate()
                    Form1.Show()
                    Me.Close()
                    Exit Sub
                End If
                MsgBox("Ship Sunk!")
                Exit Sub
            End If
        Next
        btn.Text = "O"
        lblTurn.Text = "AI's Turn"
        MsgBox("Miss!")
        AI_Turn()
    End Sub

    Private Sub AI_Turn()
        ' :'( Now I have to do AI :'(
        If gv.lastRoundHit And Not gv.lastRoundSunk Then

        Else
            Dim number As Integer
            Dim number2 As Integer
            Randomize()
            number = Int(Rnd() * 10) + 1
            number2 = Int(Rnd() * 9) + 1

            Dim ship As List(Of String) = {""}.ToList
            Dim space As String = letters(number2) & number
            Dim button As Button = DirectCast(My.Forms.Form6.Controls.Find(space & "Player", True)(0), Button)

            If button.Text = "-" Or button.Text = "" Then
                For Each x In gv.shipLocations
                    If x.Contains(space) Then
                        MsgBox("The AI Hit!")
                    End If
                Next
            Else
                AI_Turn()
            End If
        End If
    End Sub
End Class