Public Class Form2
    Dim letters() As Char = {"A"c, "B"c, "C"c, "D"c, "E"c, "F"c, "G"c, "H"c, "I"c, "J"c}
    Dim ship As List(Of String) = {""}.ToList
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

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim number As Integer
        Dim number2 As Integer
        Randomize()
        'this is where i cry because i need to make ai to choose spaces. :'(
        number = Int(Rnd() * 10) + 1
        number2 = Int(Rnd() * 9) + 1

        Dim space As String = letters(number2) & number
        Dim x As Integer
        If checkRight(space) Then
            Try
                If Byte.Parse(space.ToCharArray()(2)) = "0" Then
                    x = 10
                Else
                    x = Byte.Parse(space.ToCharArray()(1))
                End If
            Catch ex As Exception
                x = Byte.Parse(space.ToCharArray()(1))
            End Try

            ship.Clear()

            Dim y As Integer = 5

            Do While y > 0
                DirectCast(My.Forms.Form5.Controls.Find((space.ToCharArray()(0) & x.ToString).ToString, True)(0), Button).Text = "-"
                ship.Add(space.ToCharArray()(0) & x.ToString)
                y = y - 1
                If y > 0 Then
                    x = x + 1
                End If
            Loop
        End If


    End Sub

    Private Function checkRight(ByVal space As String) As Boolean
        Select Case space.ToCharArray()(1)
            Case "1"c
                Try
                    If space.ToCharArray()(2) = "0"c Then
                        Return False
                    End If
                Catch ex As Exception
                End Try
            Case "7"c
                Return False
            Case "8"c
                Return False
            Case "9"c
                Return False
        End Select
        Dim x As Byte = 0
        Dim y As Byte = 5

        x = Array.IndexOf(letters, space.ToCharArray()(0))

        Do While y > 0
            Try
                If DirectCast(My.Forms.Form5.Controls.Find((letters(x) & space.ToCharArray()(1)).ToString, True)(0), Button).Text = "-" Then
                    Return False
                    Exit Do
                End If
            Catch ex As Exception

            End Try
            y = y - 1
            If x > 0 Then
                x = x - 1
            End If
        Loop

        Return True
    End Function
End Class