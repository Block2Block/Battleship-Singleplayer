Public Class Form4
    Dim letters() As Char = {"A"c, "B"c, "C"c, "D"c, "E"c, "F"c, "G"c, "H"c, "I"c, "J"c}
    Dim ships As List(Of String) = {""}.ToList
    Private Sub Form4_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Button1.Enabled = True
        Button2.Enabled = True
        Button3.Enabled = True
        Button4.Enabled = True
        Select Case gv.ship
            Case "Carrier (5)"
                Select Case gv.selection.ToCharArray()(0)
                    Case "A"c
                        Button1.Enabled = False
                    Case "B"c
                        Button1.Enabled = False
                    Case "C"c
                        Button1.Enabled = False
                    Case "D"c
                        Button1.Enabled = False
                    Case "G"c
                        Button4.Enabled = False
                    Case "H"c
                        Button4.Enabled = False
                    Case "I"c
                        Button4.Enabled = False
                    Case "J"c
                        Button4.Enabled = False
                End Select
                Select Case gv.selection.ToCharArray()(1)
                    Case "1"c
                        Try
                            If gv.selection.ToCharArray()(2) = "0"c Then
                                Button3.Enabled = False
                            End If
                        Catch ex As Exception
                            Button2.Enabled = False
                        End Try
                    Case "2"c
                        Button2.Enabled = False
                    Case "3"c
                        Button2.Enabled = False
                    Case "4"c
                        Button2.Enabled = False
                    Case "7"c
                        Button3.Enabled = False
                    Case "8"c
                        Button3.Enabled = False
                    Case "9"c
                        Button3.Enabled = False
                End Select

                Dim x As Byte = 0
                Dim y As Byte = 5

                x = Array.IndexOf(letters, gv.selection.ToCharArray()(0))

                Do While y > 0
                    Try
                        If DirectCast(My.Forms.Form2.Controls.Find((letters(x) & gv.selection.ToCharArray()(1)).ToString, True)(0), Button).Text = "-" Then
                            Button1.Enabled = False
                            Exit Do
                        End If
                    Catch ex As Exception
                    End Try
                    y = y - 1
                    If x > 0 Then
                        x = x - 1
                    End If
                Loop
                y = 5
                x = Array.IndexOf(letters, gv.selection.ToCharArray()(0))
                Do While y > 0
                    Try
                        If DirectCast(My.Forms.Form2.Controls.Find((letters(x) & gv.selection.ToCharArray()(1)).ToString, True)(0), Button).Text = "-" Then
                            Button4.Enabled = False
                            Exit Do
                        End If
                    Catch ex As Exception
                    End Try
                    y = y - 1
                    If x > 0 Then
                        x = x + 1
                    End If
                Loop

                y = 5
                Try
                    If Byte.Parse(gv.selection.ToCharArray()(2)) = "0" Then
                        x = 10
                    Else
                        x = Byte.Parse(gv.selection.ToCharArray()(1))
                    End If
                Catch ex As Exception
                    x = Byte.Parse(gv.selection.ToCharArray()(1))
                End Try
                Do While y > 0
                    Try
                        If DirectCast(My.Forms.Form2.Controls.Find((gv.selection.ToCharArray()(0) & x.ToString).ToString, True)(0), Button).Text = "-" Then
                            Button2.Enabled = False
                            Exit Do
                        End If
                    Catch ex As Exception
                    End Try
                    y = y - 1
                    If x > 0 Then
                        x = x - 1
                    End If
                Loop


                y = 5
                Try
                    If Byte.Parse(gv.selection.ToCharArray()(2)) = "0" Then
                        x = 10
                    Else
                        x = Byte.Parse(gv.selection.ToCharArray()(1))
                    End If
                Catch ex As Exception
                    x = Byte.Parse(gv.selection.ToCharArray()(1))
                End Try

                Do While y > 0
                    Try
                        If DirectCast(My.Forms.Form2.Controls.Find((gv.selection.ToCharArray()(0) & x.ToString).ToString, True)(0), Button).Text = "-" Then
                            Button3.Enabled = False
                            Exit Do
                        End If
                    Catch ex As Exception
                    End Try
                    y = y - 1
                    If x > 0 Then
                        x = x + 1
                    End If
                Loop
            Case "Battleship (4)"
                Select Case gv.selection.ToCharArray()(0)
                    Case "A"c
                        Button1.Enabled = False
                    Case "B"c
                        Button1.Enabled = False
                    Case "C"c
                        Button1.Enabled = False
                    Case "H"c
                        Button4.Enabled = False
                    Case "I"c
                        Button4.Enabled = False
                    Case "J"c
                        Button4.Enabled = False
                End Select
                Select Case gv.selection.ToCharArray()(1)
                    Case "1"c
                        Try
                            If gv.selection.ToCharArray()(2) = "0"c Then
                                Button3.Enabled = False
                            End If
                        Catch ex As Exception
                            Button2.Enabled = False
                        End Try
                    Case "2"c
                        Button2.Enabled = False
                    Case "3"c
                        Button2.Enabled = False
                    Case "8"c
                        Button3.Enabled = False
                    Case "9"c
                        Button3.Enabled = False
                End Select

                Dim x As Byte = 0
                Dim y As Byte = 4

                x = Array.IndexOf(letters, gv.selection.ToCharArray()(0))

                Do While y > 0
                    Try
                        If DirectCast(My.Forms.Form2.Controls.Find((letters(x) & gv.selection.ToCharArray()(1)).ToString, True)(0), Button).Text = "-" Then
                            Button1.Enabled = False
                            Exit Do
                        End If
                    Catch ex As Exception
                    End Try
                    y = y - 1
                    If x > 0 Then
                        x = x - 1
                    End If
                Loop
                y = 4
                x = Array.IndexOf(letters, gv.selection.ToCharArray()(0))
                Do While y > 0
                    Try
                        If DirectCast(My.Forms.Form2.Controls.Find((letters(x) & gv.selection.ToCharArray()(1)).ToString, True)(0), Button).Text = "-" Then
                            Button4.Enabled = False
                            Exit Do
                        End If
                    Catch ex As Exception
                    End Try
                    y = y - 1
                    If x > 0 Then
                        x = x + 1
                    End If
                Loop

                y = 4
                Try
                    If Byte.Parse(gv.selection.ToCharArray()(2)) = "0" Then
                        x = 10
                    Else
                        x = Byte.Parse(gv.selection.ToCharArray()(1))
                    End If
                Catch ex As Exception
                    x = Byte.Parse(gv.selection.ToCharArray()(1))
                End Try
                Do While y > 0
                    Try
                        If DirectCast(My.Forms.Form2.Controls.Find((gv.selection.ToCharArray()(0) & x.ToString).ToString, True)(0), Button).Text = "-" Then
                            Button2.Enabled = False
                            Exit Do
                        End If
                    Catch ex As Exception
                    End Try
                    y = y - 1
                    If x > 0 Then
                        x = x - 1
                    End If
                Loop


                y = 4
                Try
                    If Byte.Parse(gv.selection.ToCharArray()(2)) = "0" Then
                        x = 10
                    Else
                        x = Byte.Parse(gv.selection.ToCharArray()(1))
                    End If
                Catch ex As Exception
                    x = Byte.Parse(gv.selection.ToCharArray()(1))
                End Try
                Do While y > 0
                    Try
                        If DirectCast(My.Forms.Form2.Controls.Find((gv.selection.ToCharArray()(0) & x.ToString).ToString, True)(0), Button).Text = "-" Then
                            Button3.Enabled = False
                            Exit Do
                        End If
                    Catch ex As Exception
                    End Try
                    y = y - 1
                    If x > 0 Then
                        x = x + 1
                    End If
                Loop
            Case "Cruise (3)"
                Select Case gv.selection.ToCharArray()(0)
                    Case "A"c
                        Button1.Enabled = False
                    Case "B"c
                        Button1.Enabled = False
                    Case "I"c
                        Button4.Enabled = False
                    Case "J"c
                        Button4.Enabled = False
                End Select
                Select Case gv.selection.ToCharArray()(1)
                    Case "1"c
                        Try
                            If gv.selection.ToCharArray()(2) = "0"c Then
                                Button3.Enabled = False
                            End If
                        Catch ex As Exception
                            Button2.Enabled = False
                        End Try
                    Case "2"c
                        Button2.Enabled = False
                    Case "9"c
                        Button3.Enabled = False
                End Select

                Dim x As Byte = 0
                Dim y As Byte = 3

                x = Array.IndexOf(letters, gv.selection.ToCharArray()(0))

                Do While y > 0
                    Try
                        If DirectCast(My.Forms.Form2.Controls.Find((letters(x) & gv.selection.ToCharArray()(1)).ToString, True)(0), Button).Text = "-" Then
                            Button1.Enabled = False
                            Exit Do
                        End If
                    Catch ex As Exception
                    End Try
                    y = y - 1
                    If x > 0 Then
                        x = x - 1
                    End If
                Loop
                y = 3
                x = Array.IndexOf(letters, gv.selection.ToCharArray()(0))
                Do While y > 0
                    Try
                        If DirectCast(My.Forms.Form2.Controls.Find((letters(x) & gv.selection.ToCharArray()(1)).ToString, True)(0), Button).Text = "-" Then
                            Button4.Enabled = False
                            Exit Do
                        End If
                    Catch ex As Exception
                    End Try
                    y = y - 1
                    If x > 0 Then
                        x = x + 1
                    End If
                Loop

                y = 3
                Try
                    If Byte.Parse(gv.selection.ToCharArray()(2)) = "0" Then
                        x = 10
                    Else
                        x = Byte.Parse(gv.selection.ToCharArray()(1))
                    End If
                Catch ex As Exception
                    x = Byte.Parse(gv.selection.ToCharArray()(1))
                End Try
                Do While y > 0
                    Try
                        If DirectCast(My.Forms.Form2.Controls.Find((gv.selection.ToCharArray()(0) & x.ToString).ToString, True)(0), Button).Text = "-" Then
                            Button2.Enabled = False
                            Exit Do
                        End If
                    Catch ex As Exception
                    End Try
                    y = y - 1
                    If x > 0 Then
                        x = x - 1
                    End If
                Loop


                y = 3
                Try
                    If Byte.Parse(gv.selection.ToCharArray()(2)) = "0" Then
                        x = 10
                    Else
                        x = Byte.Parse(gv.selection.ToCharArray()(1))
                    End If
                Catch ex As Exception
                    x = Byte.Parse(gv.selection.ToCharArray()(1))
                End Try
                Do While y > 0
                    Try
                        If DirectCast(My.Forms.Form2.Controls.Find((gv.selection.ToCharArray()(0) & x.ToString).ToString, True)(0), Button).Text = "-" Then
                            Button3.Enabled = False
                            Exit Do
                        End If
                    Catch ex As Exception
                    End Try
                    y = y - 1
                    If x > 0 Then
                        x = x + 1
                    End If
                Loop

            Case "Destroyer (2)"
                Select Case gv.selection.ToCharArray()(0)
                    Case "A"c
                        Button1.Enabled = False
                    Case "J"c
                        Button4.Enabled = False

                        Dim x As Byte = 0
                        Dim y As Byte = 2

                        x = Array.IndexOf(letters, gv.selection.ToCharArray()(0))

                        Do While y > 0
                            Try
                                If DirectCast(My.Forms.Form2.Controls.Find((letters(x) & gv.selection.ToCharArray()(1)).ToString, True)(0), Button).Text = "-" Then
                                    Button1.Enabled = False
                                    Exit Do
                                End If
                            Catch ex As Exception
                            End Try
                            y = y - 1
                            If x > 0 Then
                                x = x - 1
                            End If
                        Loop
                        y = 2
                        x = Array.IndexOf(letters, gv.selection.ToCharArray()(0))
                        Do While y > 0
                            Try
                                If DirectCast(My.Forms.Form2.Controls.Find((letters(x) & gv.selection.ToCharArray()(1)).ToString, True)(0), Button).Text = "-" Then
                                    Button4.Enabled = False
                                    Exit Do
                                End If
                            Catch ex As Exception
                            End Try
                            y = y - 1
                            If x > 0 Then
                                x = x + 1
                            End If
                        Loop

                        y = 2
                        Try
                            If Byte.Parse(gv.selection.ToCharArray()(2)) = "0" Then
                                x = 10
                            Else
                                x = Byte.Parse(gv.selection.ToCharArray()(1))
                            End If
                        Catch ex As Exception
                            x = Byte.Parse(gv.selection.ToCharArray()(1))
                        End Try
                        Do While y > 0
                            Try
                                If DirectCast(My.Forms.Form2.Controls.Find((gv.selection.ToCharArray()(0) & x.ToString).ToString, True)(0), Button).Text = "-" Then
                                    Button2.Enabled = False
                                    Exit Do
                                End If
                            Catch ex As Exception
                            End Try
                            y = y - 1
                            If x > 0 Then
                                x = x - 1
                            End If
                        Loop


                        y = 2
                        Try
                            If Byte.Parse(gv.selection.ToCharArray()(2)) = "0" Then
                                x = 10
                            Else
                                x = Byte.Parse(gv.selection.ToCharArray()(1))
                            End If
                        Catch ex As Exception
                            x = Byte.Parse(gv.selection.ToCharArray()(1))
                        End Try
                        Do While y > 0
                            Try
                                If DirectCast(My.Forms.Form2.Controls.Find((gv.selection.ToCharArray()(0) & x.ToString).ToString, True)(0), Button).Text = "-" Then
                                    Button3.Enabled = False
                                    Exit Do
                                End If
                            Catch ex As Exception
                            End Try
                            y = y - 1
                                If x > 0 Then
                                    x = x + 1
                                End If
                            Loop

                End Select
                Select Case gv.selection.ToCharArray()(1)
                    Case "1"c
                        Try
                            If gv.selection.ToCharArray()(2) = "0"c Then
                                Button3.Enabled = False
                            End If
                        Catch ex As Exception
                            Button2.Enabled = False
                        End Try
                End Select
        End Select
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        My.Forms.Form2.Show()
        Me.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim x As Byte
        Dim y As Byte = 0
        Select Case gv.ship
            Case "Carrier (5)"
                y = 5
                gv.carrierCount = gv.carrierCount - 1
                My.Forms.Form2.Carrier.Text = gv.carrierCount
            Case "Battleship (4)"
                y = 4
                gv.battleshipCount = gv.battleshipCount - 1
                My.Forms.Form2.Battleship.Text = gv.battleshipCount
            Case "Cruise (3)"
                y = 3
                gv.cruiseCount = gv.cruiseCount - 1
                My.Forms.Form2.Cruises.Text = gv.cruiseCount
            Case "Destroyer (2)"
                y = 2
                gv.destroyerCount = gv.destroyerCount - 1
                My.Forms.Form2.Destroyer.Text = gv.destroyerCount
        End Select
        Dim z As Byte = 0
        x = Array.IndexOf(letters, gv.selection.ToCharArray()(0))

        Try
            If Byte.Parse(gv.selection.ToCharArray()(2)) = "0" Then
                z = 10
            Else
                z = Byte.Parse(gv.selection.ToCharArray()(1))
            End If
        Catch ex As Exception
            z = Byte.Parse(gv.selection.ToCharArray()(1))
        End Try
        ships.Clear()

        Do While y > 0
            DirectCast(My.Forms.Form2.Controls.Find((letters(x) & z.ToString), True)(0), Button).Text = "-"
            ships.Add(letters(x) & z.ToString)
            y = y - 1
            If y > 0 Then
                x = x - 1
            End If
        Loop
        If (gv.battleshipCount = 0) And (gv.carrierCount = 0) And (gv.cruiseCount = 0) And (gv.destroyerCount = 0) Then
            My.Forms.Form2.Button2.Enabled = True
        End If
        gv.shipLocations.Add(ships)
        My.Forms.Form2.Show()
        Me.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim x As Byte
        Dim y As Byte = 0
        Select Case gv.ship
            Case "Carrier (5)"
                y = 5
                gv.carrierCount = gv.carrierCount - 1
                My.Forms.Form2.Carrier.Text = gv.carrierCount
            Case "Battleship (4)"
                y = 4
                gv.battleshipCount = gv.battleshipCount - 1
                My.Forms.Form2.Battleship.Text = gv.battleshipCount
            Case "Cruise (3)"
                y = 3
                gv.cruiseCount = gv.cruiseCount - 1
                My.Forms.Form2.Cruises.Text = gv.cruiseCount
            Case "Destroyer (2)"
                y = 2
                gv.destroyerCount = gv.destroyerCount - 1
                My.Forms.Form2.Destroyer.Text = gv.destroyerCount
        End Select

        Try
            If Byte.Parse(gv.selection.ToCharArray()(2)) = "0" Then
                x = 10
            Else
                x = Byte.Parse(gv.selection.ToCharArray()(1))
            End If
        Catch ex As Exception
            x = Byte.Parse(gv.selection.ToCharArray()(1))
        End Try
        ships.Clear()

        Do While y > 0
            Try
                DirectCast(My.Forms.Form2.Controls.Find((gv.selection.ToCharArray()(0) & x.ToString).ToString, True)(0), Button).Text = "-"
                ships.Add(gv.selection.ToCharArray()(0) & x.ToString)
            Catch ex As Exception
            End Try
            y = y - 1
            If x > 0 Then
                x = x - 1
            End If
        Loop
        If (gv.battleshipCount = 0) And (gv.carrierCount = 0) And (gv.cruiseCount = 0) And (gv.destroyerCount = 0) Then
            My.Forms.Form2.Button2.Enabled = True
        End If
        gv.shipLocations.Add(ships)
        My.Forms.Form2.Show()
        Me.Close()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim x As Byte
        Dim y As Byte = 0
        Select Case gv.ship
            Case "Carrier (5)"
                y = 5
                gv.carrierCount = gv.carrierCount - 1
                My.Forms.Form2.Carrier.Text = gv.carrierCount
            Case "Battleship (4)"
                y = 4
                gv.battleshipCount = gv.battleshipCount - 1
                My.Forms.Form2.Battleship.Text = gv.battleshipCount
            Case "Cruise (3)"
                y = 3
                gv.cruiseCount = gv.cruiseCount - 1
                My.Forms.Form2.Cruises.Text = gv.cruiseCount
            Case "Destroyer (2)"
                y = 2
                gv.destroyerCount = gv.destroyerCount - 1
                My.Forms.Form2.Destroyer.Text = gv.destroyerCount
        End Select

        Try
            If Byte.Parse(gv.selection.ToCharArray()(2)) = "0" Then
                x = 10
            Else
                x = Byte.Parse(gv.selection.ToCharArray()(1))
            End If
        Catch ex As Exception
            x = Byte.Parse(gv.selection.ToCharArray()(1))
        End Try
        ships.Clear()

        Do While y > 0
            DirectCast(My.Forms.Form2.Controls.Find((gv.selection.ToCharArray()(0) & x.ToString).ToString, True)(0), Button).Text = "-"
            ships.Add(gv.selection.ToCharArray()(0) & x.ToString)
            y = y - 1
            If y > 0 Then
                x = x + 1
            End If
        Loop
        If (gv.battleshipCount = 0) And (gv.carrierCount = 0) And (gv.cruiseCount = 0) And (gv.destroyerCount = 0) Then
            My.Forms.Form2.Button2.Enabled = True
        End If
        gv.shipLocations.Add(ships)
        My.Forms.Form2.Show()
        Me.Close()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim x As Byte
        Dim y As Byte = 0
        Select Case gv.ship
            Case "Carrier (5)"
                y = 5
                gv.carrierCount = gv.carrierCount - 1
                My.Forms.Form2.Carrier.Text = gv.carrierCount
            Case "Battleship (4)"
                y = 4
                gv.battleshipCount = gv.battleshipCount - 1
                My.Forms.Form2.Battleship.Text = gv.battleshipCount
            Case "Cruise (3)"
                y = 3
                gv.cruiseCount = gv.cruiseCount - 1
                My.Forms.Form2.Cruises.Text = gv.cruiseCount
            Case "Destroyer (2)"
                y = 2
                gv.destroyerCount = gv.destroyerCount - 1
                My.Forms.Form2.Destroyer.Text = gv.destroyerCount
        End Select

        Dim z As Byte

        Try
            If Byte.Parse(gv.selection.ToCharArray()(2)) = "0" Then
                z = 10
            Else
                z = Byte.Parse(gv.selection.ToCharArray()(1))
            End If
        Catch ex As Exception
            z = Byte.Parse(gv.selection.ToCharArray()(1))
        End Try

        x = Array.IndexOf(letters, gv.selection.ToCharArray()(0))
        ships.Clear()

        Do While y > 0
            DirectCast(My.Forms.Form2.Controls.Find((letters(x) & z.ToString).ToString, True)(0), Button).Text = "-"
            ships.Add(letters(x) & z.ToString)
            y = y - 1
            If y > 0 Then
                x = x + 1
            End If
        Loop
        If (gv.battleshipCount = 0) And (gv.carrierCount = 0) And (gv.cruiseCount = 0) And (gv.destroyerCount = 0) Then
            My.Forms.Form2.Button2.Enabled = True
        End If
        gv.shipLocations.Add(ships)
        My.Forms.Form2.Show()
        Me.Close()
    End Sub

End Class