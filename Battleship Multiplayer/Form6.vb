﻿Public Class Form6

    Dim letters() As Char = {"A"c, "B"c, "C"c, "D"c, "E"c, "F"c, "G"c, "H"c, "I"c, "J"c}
    Dim spaces As List(Of String) = {""}.ToList

    Dim Connection As New OleDb.OleDbConnection("Provider=Microsoft.JET.OLEDB.4.0;Data Source=C:\Program Files\Battleship Singleplayer\UserData.mdb")

    Dim tb As New DataTable
    Dim dataAdapter As New OleDb.OleDbDataAdapter

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
                    Dim turn As New UserControl1
                    turn.turn.Text = "Users's Turn"
                    turn.Label3.Text = btn.Name
                    turn.Label4.Text = "Hit"

                    My.Forms.Form7.FlowLayoutPanel1.Controls.Add(turn)
                    Exit Sub
                End If
                If gv.shipLocationsAI.Count = 0 Then
                    MsgBox("Congratulations, you win!")

                    Connection.Open()
                    Dim sqlString As String = "UPDATE" & gv.username & "SET Wins = Wins + 1 WHERE ID=1"
                    dataAdapter = New OleDb.OleDbDataAdapter(sqlString, Connection)
                    Connection.Close()

                    Form1.Activate()
                    Form1.Show()
                    Me.Close()
                    Exit Sub
                End If
                MsgBox("Ship Sunk!")
                Dim turn2 As New UserControl1
                turn2.turn.Text = "Users's Turn"
                turn2.Label3.Text = btn.Name
                turn2.Label4.Text = "Sunk"

                My.Forms.Form7.FlowLayoutPanel1.Controls.Add(turn2)
                Exit Sub
            End If
        Next
        btn.Text = "O"
        lblTurn.Text = "AI's Turn"
        MsgBox("Miss!")
        Dim hit As New UserControl1
        hit.turn.Text = "Users's Turn"
        hit.Label3.Text = btn.Name
        hit.Label4.Text = "Miss"

        My.Forms.Form7.FlowLayoutPanel1.Controls.Add(hit)
        AI_Turn()
    End Sub

    Private Sub AI_Turn()
        Try
            ' :'( Now I have to do AI :'(

            If spaces.Count < 50 Then
                Console.WriteLine("The number of player spaces is less than 50, deprecating spaces.")
                DeprecateSpaces()
            End If
            If gv.lastRoundFoundShip Then
                If gv.lastRoundHit Then
                    If gv.lastRoundShipInitialLocation = gv.lastRoundSpace Then
                        'Go to the right and see if it's a hit
                        If gv.lastRoundSpace.Contains("10") Then
                            'go to the left, not right
                            Dim space As String = gv.lastRoundSpace.ToCharArray()(0) & "9"
                            gv.lastRoundDirection = "Left"
                            gv.lastRoundSpace = space
                            gv.lastRoundHit = False
                            For Each row In spaces
                                If Not row.Contains(space) Then
                                    AI_Turn()
                                    Exit Sub
                                End If
                            Next


                            Dim button As Button = DirectCast(My.Forms.Form6.Controls.Find(space & "Player", True)(0), Button)

                            If button.Text = "-" Or button.Text = "" Then
                                For Each row In spaces
                                    If row.Contains(space) Then
                                        spaces.Remove(row)
                                        row.Remove(space)
                                        If row.Length > 0 Then
                                            spaces.Add(row)
                                        End If
                                    End If
                                Next
                                For Each x In gv.shipLocations
                                    If x.Contains(space) Then
                                        gv.shipLocations.Remove(x)
                                        x.Remove(space)
                                        If x.Count > 0 Then
                                            gv.shipLocations.Add(x)
                                            Turn(space, "Hit")
                                            gv.lastRoundHit = True
                                            gv.lastRoundSunk = False
                                            gv.lastRoundSpace = space
                                            button.Text = "X"
                                            gv.lastRoundHitsInDirection = True
                                            Console.WriteLine("[Debug] Hit = " & gv.lastRoundHit & ", Sunk = " & gv.lastRoundSunk & ", Space = " & gv.lastRoundSpace & ", FoundShip = " & gv.lastRoundFoundShip & ", ShipInitialLocation = " & gv.lastRoundShipInitialLocation & ", Direction = " & gv.lastRoundDirection & ", HitsInDirection = " & gv.lastRoundHitsInDirection)
                                            AI_Turn()
                                            Exit Sub
                                        End If
                                        Turn(space, "Sunk")
                                        gv.lastRoundHit = True
                                        gv.lastRoundSunk = True
                                        gv.lastRoundSpace = space
                                        gv.lastRoundFoundShip = False
                                        gv.lastRoundHitsInDirection = False
                                        button.Text = "X"
                                        If gv.shipLocations.Count = 0 Then
                                            MsgBox("You Lost! The AI sunk all of your ships!")
                                            Form1.Activate()
                                            Form1.Show()
                                            Me.Close()
                                            Exit Sub
                                        End If
                                        Console.WriteLine("[Debug] Hit = " & gv.lastRoundHit & ", Sunk = " & gv.lastRoundSunk & ", Space = " & gv.lastRoundSpace & ", FoundShip = " & gv.lastRoundFoundShip & ", ShipInitialLocation = " & gv.lastRoundShipInitialLocation & ", Direction = " & gv.lastRoundDirection & ", HitsInDirection = " & gv.lastRoundHitsInDirection)
                                        AI_Turn()
                                        Exit Sub
                                    End If
                                Next
                                Turn(space, "Miss")
                                gv.lastRoundHit = False
                                gv.lastRoundSunk = False
                                gv.lastRoundSpace = space
                                lblTurn.Text = "Your Turn"
                                button.Text = "O"
                            Else
                                AI_Turn()
                            End If
                        Else
                            'go to the right
                            Dim space As String = gv.lastRoundSpace.ToCharArray()(0) & (Byte.Parse(gv.lastRoundSpace.ToCharArray()(1).ToString) + 1)
                            gv.lastRoundDirection = "Right"
                            gv.lastRoundSpace = space
                            gv.lastRoundHit = False
                            For Each row In spaces
                                If Not row.Contains(space) Then
                                    AI_Turn()
                                    Exit Sub
                                End If
                            Next

                            Dim button As Button = DirectCast(My.Forms.Form6.Controls.Find(space & "Player", True)(0), Button)

                            If button.Text = "-" Or button.Text = "" Then
                                For Each row In spaces
                                    If row.Contains(space) Then
                                        spaces.Remove(row)
                                        row.Remove(space)
                                        If row.Length > 0 Then
                                            spaces.Add(row)
                                        End If
                                    End If
                                Next
                                For Each x In gv.shipLocations
                                    If x.Contains(space) Then
                                        gv.shipLocations.Remove(x)
                                        x.Remove(space)
                                        If x.Count > 0 Then
                                            gv.shipLocations.Add(x)
                                            Turn(space, "Hit")
                                            gv.lastRoundHit = True
                                            gv.lastRoundSunk = False
                                            gv.lastRoundSpace = space
                                            button.Text = "X"
                                            gv.lastRoundHitsInDirection = True
                                            Console.WriteLine("[Debug] Hit = " & gv.lastRoundHit & ", Sunk = " & gv.lastRoundSunk & ", Space = " & gv.lastRoundSpace & ", FoundShip = " & gv.lastRoundFoundShip & ", ShipInitialLocation = " & gv.lastRoundShipInitialLocation & ", Direction = " & gv.lastRoundDirection & ", HitsInDirection = " & gv.lastRoundHitsInDirection)
                                            AI_Turn()
                                            Exit Sub
                                        End If
                                        Turn(space, "Sunk")
                                        gv.lastRoundHit = True
                                        gv.lastRoundSunk = True
                                        gv.lastRoundSpace = space
                                        gv.lastRoundFoundShip = False
                                        gv.lastRoundHitsInDirection = False
                                        button.Text = "X"
                                        If gv.shipLocations.Count = 0 Then
                                            Turn(space, "Sunk")
                                            Form1.Activate()
                                            Form1.Show()
                                            Me.Close()
                                            Exit Sub
                                        End If
                                        Console.WriteLine("[Debug] Hit = " & gv.lastRoundHit & ", Sunk = " & gv.lastRoundSunk & ", Space = " & gv.lastRoundSpace & ", FoundShip = " & gv.lastRoundFoundShip & ", ShipInitialLocation = " & gv.lastRoundShipInitialLocation & ", Direction = " & gv.lastRoundDirection & ", HitsInDirection = " & gv.lastRoundHitsInDirection)
                                        AI_Turn()
                                        Exit Sub
                                    End If
                                Next
                                Turn(space, "Miss")
                                gv.lastRoundHit = False
                                gv.lastRoundSunk = False
                                gv.lastRoundSpace = space
                                lblTurn.Text = "Your Turn"
                                button.Text = "O"
                            Else
                                AI_Turn()
                            End If
                        End If
                    Else
                        'Continue to go in the direction it went in last time
                        Select Case gv.lastRoundDirection
                            Case "Left"
                                If gv.lastRoundSpace.Contains("1") Then
                                    gv.lastRoundDirection = "Right"
                                    gv.lastRoundSpace = gv.lastRoundSpace.ToCharArray()(0) & (Integer.Parse(gv.lastRoundSpace.ToCharArray()(1)) + 1)
                                    Dim space As String = gv.lastRoundSpace

                                    Dim button As Button = DirectCast(My.Forms.Form6.Controls.Find(space & "Player", True)(0), Button)

                                    If button.Text = "-" Or button.Text = "" Then
                                        For Each x In gv.shipLocations
                                            If x.Contains(space) Then
                                                gv.shipLocations.Remove(x)
                                                x.Remove(space)
                                                If x.Count > 0 Then
                                                    gv.shipLocations.Add(x)
                                                    Turn(space, "Hit")
                                                    gv.lastRoundHit = True
                                                    gv.lastRoundSunk = False
                                                    gv.lastRoundSpace = space
                                                    button.Text = "X"
                                                    gv.lastRoundHitsInDirection = True
                                                    Console.WriteLine("[Debug] Hit = " & gv.lastRoundHit & ", Sunk = " & gv.lastRoundSunk & ", Space = " & gv.lastRoundSpace & ", FoundShip = " & gv.lastRoundFoundShip & ", ShipInitialLocation = " & gv.lastRoundShipInitialLocation & ", Direction = " & gv.lastRoundDirection & ", HitsInDirection = " & gv.lastRoundHitsInDirection)
                                                    AI_Turn()
                                                    Exit Sub
                                                End If
                                                Turn(space, "Sunk")
                                                gv.lastRoundHit = True
                                                gv.lastRoundSunk = True
                                                gv.lastRoundSpace = space
                                                gv.lastRoundFoundShip = False
                                                gv.lastRoundHitsInDirection = False
                                                button.Text = "X"
                                                If gv.shipLocations.Count = 0 Then
                                                    MsgBox("You Lost! The AI sunk all of your ships!")
                                                    Form1.Activate()
                                                    Form1.Show()
                                                    Me.Close()
                                                    Exit Sub
                                                End If
                                                Console.WriteLine("[Debug] Hit = " & gv.lastRoundHit & ", Sunk = " & gv.lastRoundSunk & ", Space = " & gv.lastRoundSpace & ", FoundShip = " & gv.lastRoundFoundShip & ", ShipInitialLocation = " & gv.lastRoundShipInitialLocation & ", Direction = " & gv.lastRoundDirection & ", HitsInDirection = " & gv.lastRoundHitsInDirection)
                                                AI_Turn()
                                                Exit Sub
                                            End If
                                        Next
                                        Turn(space, "Miss")
                                        gv.lastRoundHit = False
                                        gv.lastRoundSunk = False
                                        gv.lastRoundSpace = space
                                        lblTurn.Text = "Your Turn"
                                        button.Text = "O"
                                    Else
                                        Console.WriteLine("[Debug] Hit = " & gv.lastRoundHit & ", Sunk = " & gv.lastRoundSunk & ", Space = " & gv.lastRoundSpace & ", FoundShip = " & gv.lastRoundFoundShip & ", ShipInitialLocation = " & gv.lastRoundShipInitialLocation & ", Direction = " & gv.lastRoundDirection & ", HitsInDirection = " & gv.lastRoundHitsInDirection)
                                        AI_Turn()
                                    End If
                                Else
                                    gv.lastRoundSpace = gv.lastRoundSpace.ToCharArray()(0) & (Integer.Parse(gv.lastRoundSpace.ToCharArray()(1)) - 1)
                                    Dim space As String = gv.lastRoundSpace

                                    Dim button As Button = DirectCast(My.Forms.Form6.Controls.Find(space & "Player", True)(0), Button)

                                    If button.Text = "-" Or button.Text = "" Then
                                        For Each x In gv.shipLocations
                                            If x.Contains(space) Then
                                                gv.shipLocations.Remove(x)
                                                x.Remove(space)
                                                If x.Count > 0 Then
                                                    gv.shipLocations.Add(x)
                                                    Turn(space, "Hit")
                                                    gv.lastRoundHit = True
                                                    gv.lastRoundSunk = False
                                                    gv.lastRoundSpace = space
                                                    button.Text = "X"
                                                    gv.lastRoundHitsInDirection = True
                                                    Console.WriteLine("[Debug] Hit = " & gv.lastRoundHit & ", Sunk = " & gv.lastRoundSunk & ", Space = " & gv.lastRoundSpace & ", FoundShip = " & gv.lastRoundFoundShip & ", ShipInitialLocation = " & gv.lastRoundShipInitialLocation & ", Direction = " & gv.lastRoundDirection & ", HitsInDirection = " & gv.lastRoundHitsInDirection)
                                                    AI_Turn()
                                                    Exit Sub
                                                End If
                                                Turn(space, "Sunk")
                                                gv.lastRoundHit = True
                                                gv.lastRoundSunk = True
                                                gv.lastRoundSpace = space
                                                gv.lastRoundFoundShip = False
                                                gv.lastRoundHitsInDirection = False
                                                button.Text = "X"
                                                If gv.shipLocations.Count = 0 Then
                                                    MsgBox("You Lost! The AI sunk all of your ships!")
                                                    Form1.Activate()
                                                    Form1.Show()
                                                    Me.Close()
                                                    Exit Sub
                                                End If
                                                Console.WriteLine("[Debug] Hit = " & gv.lastRoundHit & ", Sunk = " & gv.lastRoundSunk & ", Space = " & gv.lastRoundSpace & ", FoundShip = " & gv.lastRoundFoundShip & ", ShipInitialLocation = " & gv.lastRoundShipInitialLocation & ", Direction = " & gv.lastRoundDirection & ", HitsInDirection = " & gv.lastRoundHitsInDirection)
                                                AI_Turn()
                                                Exit Sub
                                            End If
                                        Next
                                        Turn(space, "Miss")
                                        gv.lastRoundHit = False
                                        gv.lastRoundSunk = False
                                        gv.lastRoundSpace = space
                                        lblTurn.Text = "Your Turn"
                                        button.Text = "O"
                                    Else
                                        Console.WriteLine("[Debug] Hit = " & gv.lastRoundHit & ", Sunk = " & gv.lastRoundSunk & ", Space = " & gv.lastRoundSpace & ", FoundShip = " & gv.lastRoundFoundShip & ", ShipInitialLocation = " & gv.lastRoundShipInitialLocation & ", Direction = " & gv.lastRoundDirection & ", HitsInDirection = " & gv.lastRoundHitsInDirection)
                                        AI_Turn()
                                    End If
                                End If
                            Case "Right"
                                If gv.lastRoundSpace.Contains("10") Then
                                    gv.lastRoundDirection = "Left"
                                    gv.lastRoundSpace = gv.lastRoundSpace.ToCharArray()(0) & (Integer.Parse(gv.lastRoundShipInitialLocation.ToCharArray()(1)) - 1)
                                    Dim space As String = gv.lastRoundSpace

                                    Dim button As Button = DirectCast(My.Forms.Form6.Controls.Find(space & "Player", True)(0), Button)

                                    If button.Text = "-" Or button.Text = "" Then
                                        For Each x In gv.shipLocations
                                            If x.Contains(space) Then
                                                gv.shipLocations.Remove(x)
                                                x.Remove(space)
                                                If x.Count > 0 Then
                                                    gv.shipLocations.Add(x)
                                                    Turn(space, "Hit")
                                                    gv.lastRoundHit = True
                                                    gv.lastRoundSunk = False
                                                    gv.lastRoundSpace = space
                                                    button.Text = "X"
                                                    gv.lastRoundHitsInDirection = True
                                                    Console.WriteLine("[Debug] Hit = " & gv.lastRoundHit & ", Sunk = " & gv.lastRoundSunk & ", Space = " & gv.lastRoundSpace & ", FoundShip = " & gv.lastRoundFoundShip & ", ShipInitialLocation = " & gv.lastRoundShipInitialLocation & ", Direction = " & gv.lastRoundDirection & ", HitsInDirection = " & gv.lastRoundHitsInDirection)
                                                    AI_Turn()
                                                    Exit Sub
                                                End If
                                                Turn(space, "Sunk")
                                                gv.lastRoundHit = True
                                                gv.lastRoundSunk = True
                                                gv.lastRoundSpace = space
                                                gv.lastRoundFoundShip = False
                                                gv.lastRoundHitsInDirection = False
                                                button.Text = "X"
                                                If gv.shipLocations.Count = 0 Then
                                                    MsgBox("You Lost! The AI sunk all of your ships!")
                                                    Form1.Activate()
                                                    Form1.Show()
                                                    Me.Close()
                                                    Exit Sub
                                                End If
                                                Console.WriteLine("[Debug] Hit = " & gv.lastRoundHit & ", Sunk = " & gv.lastRoundSunk & ", Space = " & gv.lastRoundSpace & ", FoundShip = " & gv.lastRoundFoundShip & ", ShipInitialLocation = " & gv.lastRoundShipInitialLocation & ", Direction = " & gv.lastRoundDirection & ", HitsInDirection = " & gv.lastRoundHitsInDirection)
                                                AI_Turn()
                                                Exit Sub
                                            End If
                                        Next
                                        Turn(space, "Miss")
                                        gv.lastRoundHit = False
                                        gv.lastRoundSunk = False
                                        gv.lastRoundSpace = space
                                        lblTurn.Text = "Your Turn"
                                        button.Text = "O"
                                    Else
                                        Console.WriteLine("[Debug] Hit = " & gv.lastRoundHit & ", Sunk = " & gv.lastRoundSunk & ", Space = " & gv.lastRoundSpace & ", FoundShip = " & gv.lastRoundFoundShip & ", ShipInitialLocation = " & gv.lastRoundShipInitialLocation & ", Direction = " & gv.lastRoundDirection & ", HitsInDirection = " & gv.lastRoundHitsInDirection)
                                        AI_Turn()
                                    End If
                                Else
                                    gv.lastRoundSpace = gv.lastRoundSpace.ToCharArray()(0) & (Integer.Parse(gv.lastRoundSpace.ToCharArray()(1)) + 1)
                                    Dim space As String = gv.lastRoundSpace

                                    Dim button As Button = DirectCast(My.Forms.Form6.Controls.Find(space & "Player", True)(0), Button)

                                    If button.Text = "-" Or button.Text = "" Then
                                        For Each x In gv.shipLocations
                                            If x.Contains(space) Then
                                                gv.shipLocations.Remove(x)
                                                x.Remove(space)
                                                If x.Count > 0 Then
                                                    gv.shipLocations.Add(x)
                                                    Turn(space, "Hit")
                                                    gv.lastRoundHit = True
                                                    gv.lastRoundSunk = False
                                                    gv.lastRoundSpace = space
                                                    button.Text = "X"
                                                    gv.lastRoundHitsInDirection = True
                                                    Console.WriteLine("[Debug] Hit = " & gv.lastRoundHit & ", Sunk = " & gv.lastRoundSunk & ", Space = " & gv.lastRoundSpace & ", FoundShip = " & gv.lastRoundFoundShip & ", ShipInitialLocation = " & gv.lastRoundShipInitialLocation & ", Direction = " & gv.lastRoundDirection & ", HitsInDirection = " & gv.lastRoundHitsInDirection)
                                                    AI_Turn()
                                                    Exit Sub
                                                End If
                                                Turn(space, "Sunk")
                                                gv.lastRoundHit = True
                                                gv.lastRoundSunk = True
                                                gv.lastRoundSpace = space
                                                gv.lastRoundFoundShip = False
                                                gv.lastRoundHitsInDirection = False
                                                button.Text = "X"
                                                If gv.shipLocations.Count = 0 Then
                                                    MsgBox("You Lost! The AI sunk all of your ships!")
                                                    Form1.Activate()
                                                    Form1.Show()
                                                    Me.Close()
                                                    Exit Sub
                                                End If
                                                Console.WriteLine("[Debug] Hit = " & gv.lastRoundHit & ", Sunk = " & gv.lastRoundSunk & ", Space = " & gv.lastRoundSpace & ", FoundShip = " & gv.lastRoundFoundShip & ", ShipInitialLocation = " & gv.lastRoundShipInitialLocation & ", Direction = " & gv.lastRoundDirection & ", HitsInDirection = " & gv.lastRoundHitsInDirection)
                                                AI_Turn()
                                                Exit Sub
                                            End If
                                        Next
                                        Turn(space, "Miss")
                                        gv.lastRoundHit = False
                                        gv.lastRoundSunk = False
                                        gv.lastRoundSpace = space
                                        lblTurn.Text = "Your Turn"
                                        button.Text = "O"
                                    Else
                                        Console.WriteLine("[Debug] Hit = " & gv.lastRoundHit & ", Sunk = " & gv.lastRoundSunk & ", Space = " & gv.lastRoundSpace & ", FoundShip = " & gv.lastRoundFoundShip & ", ShipInitialLocation = " & gv.lastRoundShipInitialLocation & ", Direction = " & gv.lastRoundDirection & ", HitsInDirection = " & gv.lastRoundHitsInDirection)
                                        AI_Turn()
                                    End If
                                End If
                            Case "Up"
                                If gv.lastRoundSpace.Contains("A") Then
                                    gv.lastRoundDirection = "Down"
                                    If gv.lastRoundSpace.Contains("10") Then
                                        gv.lastRoundSpace = letters(Array.IndexOf(letters, gv.lastRoundSpace.ToCharArray()(0)) + 1) & (Integer.Parse(10))
                                    Else
                                        gv.lastRoundSpace = letters(Array.IndexOf(letters, gv.lastRoundSpace.ToCharArray()(0)) + 1) & (Integer.Parse(gv.lastRoundSpace.ToCharArray()(1)))
                                    End If
                                    Dim space As String = gv.lastRoundSpace

                                    Dim button As Button = DirectCast(My.Forms.Form6.Controls.Find(space & "Player", True)(0), Button)

                                    If button.Text = "-" Or button.Text = "" Then
                                        For Each x In gv.shipLocations
                                            If x.Contains(space) Then
                                                gv.shipLocations.Remove(x)
                                                x.Remove(space)
                                                If x.Count > 0 Then
                                                    gv.shipLocations.Add(x)
                                                    Turn(space, "Hit")
                                                    gv.lastRoundHit = True
                                                    gv.lastRoundSunk = False
                                                    gv.lastRoundSpace = space
                                                    button.Text = "X"
                                                    gv.lastRoundHitsInDirection = True
                                                    Console.WriteLine("[Debug] Hit = " & gv.lastRoundHit & ", Sunk = " & gv.lastRoundSunk & ", Space = " & gv.lastRoundSpace & ", FoundShip = " & gv.lastRoundFoundShip & ", ShipInitialLocation = " & gv.lastRoundShipInitialLocation & ", Direction = " & gv.lastRoundDirection & ", HitsInDirection = " & gv.lastRoundHitsInDirection)
                                                    AI_Turn()
                                                    Exit Sub
                                                End If
                                                Turn(space, "Sunk")
                                                gv.lastRoundHit = True
                                                gv.lastRoundSunk = True
                                                gv.lastRoundSpace = space
                                                gv.lastRoundFoundShip = False
                                                gv.lastRoundHitsInDirection = False
                                                button.Text = "X"
                                                If gv.shipLocations.Count = 0 Then
                                                    MsgBox("You Lost! The AI sunk all of your ships!")
                                                    Form1.Activate()
                                                    Form1.Show()
                                                    Me.Close()
                                                    Exit Sub
                                                End If
                                                Console.WriteLine("[Debug] Hit = " & gv.lastRoundHit & ", Sunk = " & gv.lastRoundSunk & ", Space = " & gv.lastRoundSpace & ", FoundShip = " & gv.lastRoundFoundShip & ", ShipInitialLocation = " & gv.lastRoundShipInitialLocation & ", Direction = " & gv.lastRoundDirection & ", HitsInDirection = " & gv.lastRoundHitsInDirection)
                                                AI_Turn()
                                                Exit Sub
                                            End If
                                        Next
                                        Turn(space, "Miss")
                                        gv.lastRoundHit = False
                                        gv.lastRoundSunk = False
                                        gv.lastRoundSpace = space
                                        lblTurn.Text = "Your Turn"
                                        button.Text = "O"
                                    Else
                                        Console.WriteLine("[Debug] Hit = " & gv.lastRoundHit & ", Sunk = " & gv.lastRoundSunk & ", Space = " & gv.lastRoundSpace & ", FoundShip = " & gv.lastRoundFoundShip & ", ShipInitialLocation = " & gv.lastRoundShipInitialLocation & ", Direction = " & gv.lastRoundDirection & ", HitsInDirection = " & gv.lastRoundHitsInDirection)
                                        AI_Turn()
                                    End If
                                Else
                                    gv.lastRoundDirection = "Up"
                                    If gv.lastRoundSpace.Contains("10") Then
                                        gv.lastRoundSpace = letters(Array.IndexOf(letters, gv.lastRoundSpace.ToCharArray()(0)) - 1) & (Integer.Parse(10))
                                    Else
                                        gv.lastRoundSpace = letters(Array.IndexOf(letters, gv.lastRoundSpace.ToCharArray()(0)) - 1) & (Integer.Parse(gv.lastRoundSpace.ToCharArray()(1)))
                                    End If
                                    Dim space As String = gv.lastRoundSpace

                                    Console.WriteLine("Selected Space: " & space)

                                    Dim button As Button = DirectCast(My.Forms.Form6.Controls.Find(space & "Player", True)(0), Button)

                                    If button.Text = "-" Or button.Text = "" Then
                                        For Each x In gv.shipLocations
                                            If x.Contains(space) Then
                                                gv.shipLocations.Remove(x)
                                                x.Remove(space)
                                                If x.Count > 0 Then
                                                    gv.shipLocations.Add(x)
                                                    Turn(space, "Hit")
                                                    gv.lastRoundHit = True
                                                    gv.lastRoundSunk = False
                                                    gv.lastRoundSpace = space
                                                    button.Text = "X"
                                                    gv.lastRoundHitsInDirection = True
                                                    Console.WriteLine("[Debug] Hit = " & gv.lastRoundHit & ", Sunk = " & gv.lastRoundSunk & ", Space = " & gv.lastRoundSpace & ", FoundShip = " & gv.lastRoundFoundShip & ", ShipInitialLocation = " & gv.lastRoundShipInitialLocation & ", Direction = " & gv.lastRoundDirection & ", HitsInDirection = " & gv.lastRoundHitsInDirection)
                                                    AI_Turn()
                                                    Exit Sub
                                                End If
                                                Turn(space, "Sunk")
                                                gv.lastRoundHit = True
                                                gv.lastRoundSunk = True
                                                gv.lastRoundSpace = space
                                                gv.lastRoundFoundShip = False
                                                gv.lastRoundHitsInDirection = False
                                                button.Text = "X"
                                                If gv.shipLocations.Count = 0 Then
                                                    MsgBox("You Lost! The AI sunk all of your ships!")
                                                    Form1.Activate()
                                                    Form1.Show()
                                                    Me.Close()
                                                    Exit Sub
                                                End If
                                                Console.WriteLine("[Debug] Hit = " & gv.lastRoundHit & ", Sunk = " & gv.lastRoundSunk & ", Space = " & gv.lastRoundSpace & ", FoundShip = " & gv.lastRoundFoundShip & ", ShipInitialLocation = " & gv.lastRoundShipInitialLocation & ", Direction = " & gv.lastRoundDirection & ", HitsInDirection = " & gv.lastRoundHitsInDirection)
                                                AI_Turn()
                                                Exit Sub
                                            End If
                                        Next
                                        Turn(space, "Miss")
                                        gv.lastRoundHit = False
                                        gv.lastRoundSunk = False
                                        gv.lastRoundSpace = space
                                        lblTurn.Text = "Your Turn"
                                        button.Text = "O"
                                    Else
                                        Console.WriteLine("[Debug] Hit = " & gv.lastRoundHit & ", Sunk = " & gv.lastRoundSunk & ", Space = " & gv.lastRoundSpace & ", FoundShip = " & gv.lastRoundFoundShip & ", ShipInitialLocation = " & gv.lastRoundShipInitialLocation & ", Direction = " & gv.lastRoundDirection & ", HitsInDirection = " & gv.lastRoundHitsInDirection)
                                        AI_Turn()
                                    End If
                                End If
                            Case "Down"
                                If gv.lastRoundSpace.Contains("J") Then
                                    gv.lastRoundDirection = "Up"
                                    If gv.lastRoundSpace.Contains("10") Then
                                        gv.lastRoundSpace = letters(Array.IndexOf(letters, gv.lastRoundSpace.ToCharArray()(0)) - 1) & (Integer.Parse(10))
                                    Else
                                        gv.lastRoundSpace = letters(Array.IndexOf(letters, gv.lastRoundSpace.ToCharArray()(0)) - 1) & (Integer.Parse(gv.lastRoundSpace.ToCharArray()(1)))
                                    End If
                                    Dim space As String = gv.lastRoundSpace

                                    Dim button As Button = DirectCast(My.Forms.Form6.Controls.Find(space & "Player", True)(0), Button)

                                    If button.Text = "-" Or button.Text = "" Then
                                        For Each x In gv.shipLocations
                                            If x.Contains(space) Then
                                                gv.shipLocations.Remove(x)
                                                x.Remove(space)
                                                If x.Count > 0 Then
                                                    gv.shipLocations.Add(x)
                                                    Turn(space, "Hit")
                                                    gv.lastRoundHit = True
                                                    gv.lastRoundSunk = False
                                                    gv.lastRoundSpace = space
                                                    button.Text = "X"
                                                    gv.lastRoundHitsInDirection = True
                                                    Console.WriteLine("[Debug] Hit = " & gv.lastRoundHit & ", Sunk = " & gv.lastRoundSunk & ", Space = " & gv.lastRoundSpace & ", FoundShip = " & gv.lastRoundFoundShip & ", ShipInitialLocation = " & gv.lastRoundShipInitialLocation & ", Direction = " & gv.lastRoundDirection & ", HitsInDirection = " & gv.lastRoundHitsInDirection)
                                                    AI_Turn()
                                                    Exit Sub
                                                End If
                                                Turn(space, "Sunk")
                                                gv.lastRoundHit = True
                                                gv.lastRoundSunk = True
                                                gv.lastRoundSpace = space
                                                gv.lastRoundFoundShip = False
                                                gv.lastRoundHitsInDirection = False
                                                button.Text = "X"
                                                If gv.shipLocations.Count = 0 Then
                                                    MsgBox("You Lost! The AI sunk all of your ships!")
                                                    Form1.Activate()
                                                    Form1.Show()
                                                    Me.Close()
                                                    Exit Sub
                                                End If
                                                Console.WriteLine("[Debug] Hit = " & gv.lastRoundHit & ", Sunk = " & gv.lastRoundSunk & ", Space = " & gv.lastRoundSpace & ", FoundShip = " & gv.lastRoundFoundShip & ", ShipInitialLocation = " & gv.lastRoundShipInitialLocation & ", Direction = " & gv.lastRoundDirection & ", HitsInDirection = " & gv.lastRoundHitsInDirection)
                                                AI_Turn()
                                                Exit Sub
                                            End If
                                        Next
                                        Turn(space, "Miss")
                                        gv.lastRoundHit = False
                                        gv.lastRoundSunk = False
                                        gv.lastRoundSpace = space
                                        lblTurn.Text = "Your Turn"
                                        button.Text = "O"
                                    Else
                                        Console.WriteLine("[Debug] Hit = " & gv.lastRoundHit & ", Sunk = " & gv.lastRoundSunk & ", Space = " & gv.lastRoundSpace & ", FoundShip = " & gv.lastRoundFoundShip & ", ShipInitialLocation = " & gv.lastRoundShipInitialLocation & ", Direction = " & gv.lastRoundDirection & ", HitsInDirection = " & gv.lastRoundHitsInDirection)
                                        AI_Turn()
                                    End If
                                Else
                                    If gv.lastRoundSpace.Contains("10") Then
                                        gv.lastRoundSpace = letters(Array.IndexOf(letters, gv.lastRoundSpace.ToCharArray()(0)) + 1) & (Integer.Parse(10))
                                    Else
                                        gv.lastRoundSpace = letters(Array.IndexOf(letters, gv.lastRoundSpace.ToCharArray()(0)) + 1) & (Integer.Parse(gv.lastRoundSpace.ToCharArray()(1)))
                                    End If
                                    Dim space As String = gv.lastRoundSpace

                                    Dim button As Button = DirectCast(My.Forms.Form6.Controls.Find(space & "Player", True)(0), Button)

                                    If button.Text = "-" Or button.Text = "" Then
                                        For Each x In gv.shipLocations
                                            If x.Contains(space) Then
                                                gv.shipLocations.Remove(x)
                                                x.Remove(space)
                                                If x.Count > 0 Then
                                                    gv.shipLocations.Add(x)
                                                    Turn(space, "Hit")
                                                    gv.lastRoundHit = True
                                                    gv.lastRoundSunk = False
                                                    gv.lastRoundSpace = space
                                                    button.Text = "X"
                                                    gv.lastRoundHitsInDirection = True
                                                    Console.WriteLine("[Debug] Hit = " & gv.lastRoundHit & ", Sunk = " & gv.lastRoundSunk & ", Space = " & gv.lastRoundSpace & ", FoundShip = " & gv.lastRoundFoundShip & ", ShipInitialLocation = " & gv.lastRoundShipInitialLocation & ", Direction = " & gv.lastRoundDirection & ", HitsInDirection = " & gv.lastRoundHitsInDirection)
                                                    AI_Turn()
                                                    Exit Sub
                                                End If
                                                Turn(space, "Sunk")
                                                gv.lastRoundHit = True
                                                gv.lastRoundSunk = True
                                                gv.lastRoundSpace = space
                                                gv.lastRoundFoundShip = False
                                                gv.lastRoundHitsInDirection = False
                                                button.Text = "X"
                                                If gv.shipLocations.Count = 0 Then
                                                    MsgBox("You Lost! The AI sunk all of your ships!")
                                                    Form1.Activate()
                                                    Form1.Show()
                                                    Me.Close()
                                                    Exit Sub
                                                End If
                                                Console.WriteLine("[Debug] Hit = " & gv.lastRoundHit & ", Sunk = " & gv.lastRoundSunk & ", Space = " & gv.lastRoundSpace & ", FoundShip = " & gv.lastRoundFoundShip & ", ShipInitialLocation = " & gv.lastRoundShipInitialLocation & ", Direction = " & gv.lastRoundDirection & ", HitsInDirection = " & gv.lastRoundHitsInDirection)
                                                AI_Turn()
                                                Exit Sub
                                            End If
                                        Next
                                        Turn(space, "Miss")
                                        gv.lastRoundHit = False
                                        gv.lastRoundSunk = False
                                        gv.lastRoundSpace = space
                                        lblTurn.Text = "Your Turn"
                                        button.Text = "O"
                                    Else
                                        Console.WriteLine("[Debug] Hit = " & gv.lastRoundHit & ", Sunk = " & gv.lastRoundSunk & ", Space = " & gv.lastRoundSpace & ", FoundShip = " & gv.lastRoundFoundShip & ", ShipInitialLocation = " & gv.lastRoundShipInitialLocation & ", Direction = " & gv.lastRoundDirection & ", HitsInDirection = " & gv.lastRoundHitsInDirection)
                                        AI_Turn()
                                    End If
                                End If
                        End Select
                    End If
                Else
                    'Find out if the old direction had ANY hits, then go in another direction, or opposite direction if there were hits
                    Try
                        Select Case gv.lastRoundDirection
                            Case "Left"
                                If gv.lastRoundHitsInDirection Then
                                    gv.lastRoundDirection = "Right"
                                    gv.lastRoundInfinateLoop = True
                                    gv.lastRoundSpace = gv.lastRoundShipInitialLocation.ToCharArray()(0) & (Integer.Parse(gv.lastRoundShipInitialLocation.ToCharArray()(1)) + 1)
                                    Dim space As String = gv.lastRoundSpace
                                    Console.WriteLine("Selected Space: " & space)

                                    Dim button As Button = DirectCast(My.Forms.Form6.Controls.Find(space & "Player", True)(0), Button)

                                    If button.Text = "-" Or button.Text = "" Then
                                        For Each x In gv.shipLocations
                                            If x.Contains(space) Then
                                                gv.shipLocations.Remove(x)
                                                x.Remove(space)
                                                If x.Count > 0 Then
                                                    gv.shipLocations.Add(x)
                                                    Turn(space, "Hit")
                                                    gv.lastRoundHit = True
                                                    gv.lastRoundSunk = False
                                                    gv.lastRoundSpace = space
                                                    button.Text = "X"
                                                    gv.lastRoundHitsInDirection = True
                                                    gv.lastRoundInfinateLoop = False
                                                    Console.WriteLine("[Debug] Hit = " & gv.lastRoundHit & ", Sunk = " & gv.lastRoundSunk & ", Space = " & gv.lastRoundSpace & ", FoundShip = " & gv.lastRoundFoundShip & ", ShipInitialLocation = " & gv.lastRoundShipInitialLocation & ", Direction = " & gv.lastRoundDirection & ", HitsInDirection = " & gv.lastRoundHitsInDirection)
                                                    AI_Turn()
                                                    Exit Sub
                                                End If
                                                Turn(space, "Sunk")
                                                gv.lastRoundHit = True
                                                gv.lastRoundSunk = True
                                                gv.lastRoundSpace = space
                                                gv.lastRoundFoundShip = False
                                                gv.lastRoundHitsInDirection = False
                                                gv.lastRoundInfinateLoop = False
                                                button.Text = "X"
                                                If gv.shipLocations.Count = 0 Then
                                                    MsgBox("You Lost! The AI sunk all of your ships!")
                                                    Form1.Activate()
                                                    Form1.Show()
                                                    Me.Close()
                                                    Exit Sub
                                                End If
                                                Console.WriteLine("[Debug] Hit = " & gv.lastRoundHit & ", Sunk = " & gv.lastRoundSunk & ", Space = " & gv.lastRoundSpace & ", FoundShip = " & gv.lastRoundFoundShip & ", ShipInitialLocation = " & gv.lastRoundShipInitialLocation & ", Direction = " & gv.lastRoundDirection & ", HitsInDirection = " & gv.lastRoundHitsInDirection)
                                                gv.lastRoundInfinateLoop = False
                                                AI_Turn()
                                                Exit Sub
                                            End If
                                        Next
                                        Turn(space, "Miss")
                                        gv.lastRoundHit = False
                                        gv.lastRoundSunk = False
                                        gv.lastRoundSpace = space
                                        lblTurn.Text = "Your Turn"
                                        button.Text = "O"
                                    Else
                                        Console.WriteLine("[Debug] Hit = " & gv.lastRoundHit & ", Sunk = " & gv.lastRoundSunk & ", Space = " & gv.lastRoundSpace & ", FoundShip = " & gv.lastRoundFoundShip & ", ShipInitialLocation = " & gv.lastRoundShipInitialLocation & ", Direction = " & gv.lastRoundDirection & ", HitsInDirection = " & gv.lastRoundHitsInDirection)
                                        AI_Turn()
                                    End If
                                Else
                                    If gv.lastRoundShipInitialLocation.Contains("A") Then
                                        gv.lastRoundDirection = "Down"
                                        If gv.lastRoundShipInitialLocation.Contains("10") Then
                                            gv.lastRoundSpace = letters(Array.IndexOf(letters, gv.lastRoundShipInitialLocation.ToCharArray()(0)) + 1) & (Integer.Parse(10))
                                        Else
                                            gv.lastRoundSpace = letters(Array.IndexOf(letters, gv.lastRoundShipInitialLocation.ToCharArray()(0)) + 1) & (Integer.Parse(gv.lastRoundShipInitialLocation.ToCharArray()(1)))
                                        End If
                                        Dim space As String = gv.lastRoundSpace

                                        Console.WriteLine("Selected Space: " & space)

                                        Dim button As Button = DirectCast(My.Forms.Form6.Controls.Find(space & "Player", True)(0), Button)

                                        If button.Text = "-" Or button.Text = "" Then
                                            For Each x In gv.shipLocations
                                                If x.Contains(space) Then
                                                    gv.shipLocations.Remove(x)
                                                    x.Remove(space)
                                                    If x.Count > 0 Then
                                                        gv.shipLocations.Add(x)
                                                        Turn(space, "Hit")
                                                        gv.lastRoundHit = True
                                                        gv.lastRoundSunk = False
                                                        gv.lastRoundSpace = space
                                                        button.Text = "X"
                                                        gv.lastRoundHitsInDirection = True
                                                        Console.WriteLine("[Debug] Hit = " & gv.lastRoundHit & ", Sunk = " & gv.lastRoundSunk & ", Space = " & gv.lastRoundSpace & ", FoundShip = " & gv.lastRoundFoundShip & ", ShipInitialLocation = " & gv.lastRoundShipInitialLocation & ", Direction = " & gv.lastRoundDirection & ", HitsInDirection = " & gv.lastRoundHitsInDirection)
                                                        AI_Turn()
                                                        Exit Sub
                                                    End If
                                                    Turn(space, "Sunk")
                                                    gv.lastRoundHit = True
                                                    gv.lastRoundSunk = True
                                                    gv.lastRoundSpace = space
                                                    gv.lastRoundFoundShip = False
                                                    gv.lastRoundHitsInDirection = False
                                                    button.Text = "X"
                                                    If gv.shipLocations.Count = 0 Then
                                                        MsgBox("You Lost! The AI sunk all of your ships!")
                                                        Form1.Activate()
                                                        Form1.Show()
                                                        Me.Close()
                                                        Exit Sub
                                                    End If
                                                    Console.WriteLine("[Debug] Hit = " & gv.lastRoundHit & ", Sunk = " & gv.lastRoundSunk & ", Space = " & gv.lastRoundSpace & ", FoundShip = " & gv.lastRoundFoundShip & ", ShipInitialLocation = " & gv.lastRoundShipInitialLocation & ", Direction = " & gv.lastRoundDirection & ", HitsInDirection = " & gv.lastRoundHitsInDirection)
                                                    AI_Turn()
                                                    Exit Sub
                                                End If
                                            Next
                                            Turn(space, "Miss")
                                            gv.lastRoundHit = False
                                            gv.lastRoundSunk = False
                                            gv.lastRoundSpace = space
                                            lblTurn.Text = "Your Turn"
                                            button.Text = "O"
                                        Else
                                            Console.WriteLine("[Debug] Hit = " & gv.lastRoundHit & ", Sunk = " & gv.lastRoundSunk & ", Space = " & gv.lastRoundSpace & ", FoundShip = " & gv.lastRoundFoundShip & ", ShipInitialLocation = " & gv.lastRoundShipInitialLocation & ", Direction = " & gv.lastRoundDirection & ", HitsInDirection = " & gv.lastRoundHitsInDirection)
                                            AI_Turn()
                                        End If
                                    Else
                                        gv.lastRoundDirection = "Up"
                                        If gv.lastRoundShipInitialLocation.Contains("10") Then
                                            gv.lastRoundSpace = letters(Array.IndexOf(letters, gv.lastRoundShipInitialLocation.ToCharArray()(0)) - 1) & (Integer.Parse(10))
                                        Else
                                            gv.lastRoundSpace = letters(Array.IndexOf(letters, gv.lastRoundShipInitialLocation.ToCharArray()(0)) - 1) & (Integer.Parse(gv.lastRoundShipInitialLocation.ToCharArray()(1)))
                                        End If
                                        Dim space As String = gv.lastRoundSpace

                                        Console.WriteLine("Selected Space: " & space)

                                        Dim button As Button = DirectCast(My.Forms.Form6.Controls.Find(space & "Player", True)(0), Button)

                                        If button.Text = "-" Or button.Text = "" Then
                                            For Each x In gv.shipLocations
                                                If x.Contains(space) Then
                                                    gv.shipLocations.Remove(x)
                                                    x.Remove(space)
                                                    If x.Count > 0 Then
                                                        gv.shipLocations.Add(x)
                                                        Turn(space, "Hit")
                                                        gv.lastRoundHit = True
                                                        gv.lastRoundSunk = False
                                                        gv.lastRoundSpace = space
                                                        button.Text = "X"
                                                        gv.lastRoundHitsInDirection = True
                                                        Console.WriteLine("[Debug] Hit = " & gv.lastRoundHit & ", Sunk = " & gv.lastRoundSunk & ", Space = " & gv.lastRoundSpace & ", FoundShip = " & gv.lastRoundFoundShip & ", ShipInitialLocation = " & gv.lastRoundShipInitialLocation & ", Direction = " & gv.lastRoundDirection & ", HitsInDirection = " & gv.lastRoundHitsInDirection)
                                                        AI_Turn()
                                                        Exit Sub
                                                    End If
                                                    Turn(space, "Sunk")
                                                    gv.lastRoundHit = True
                                                    gv.lastRoundSunk = True
                                                    gv.lastRoundSpace = space
                                                    gv.lastRoundFoundShip = False
                                                    gv.lastRoundHitsInDirection = False
                                                    button.Text = "X"
                                                    If gv.shipLocations.Count = 0 Then
                                                        MsgBox("You Lost! The AI sunk all of your ships!")
                                                        Form1.Activate()
                                                        Form1.Show()
                                                        Me.Close()
                                                        Exit Sub
                                                    End If
                                                    Console.WriteLine("[Debug] Hit = " & gv.lastRoundHit & ", Sunk = " & gv.lastRoundSunk & ", Space = " & gv.lastRoundSpace & ", FoundShip = " & gv.lastRoundFoundShip & ", ShipInitialLocation = " & gv.lastRoundShipInitialLocation & ", Direction = " & gv.lastRoundDirection & ", HitsInDirection = " & gv.lastRoundHitsInDirection)
                                                    AI_Turn()
                                                    Exit Sub
                                                End If
                                            Next
                                            Turn(space, "Miss")
                                            gv.lastRoundHit = False
                                            gv.lastRoundSunk = False
                                            gv.lastRoundSpace = space
                                            lblTurn.Text = "Your Turn"
                                            button.Text = "O"
                                        Else
                                            Console.WriteLine("[Debug] Hit = " & gv.lastRoundHit & ", Sunk = " & gv.lastRoundSunk & ", Space = " & gv.lastRoundSpace & ", FoundShip = " & gv.lastRoundFoundShip & ", ShipInitialLocation = " & gv.lastRoundShipInitialLocation & ", Direction = " & gv.lastRoundDirection & ", HitsInDirection = " & gv.lastRoundHitsInDirection)
                                            AI_Turn()
                                        End If
                                    End If
                                End If
                            Case "Right"
                                If gv.lastRoundHitsInDirection Then
                                    gv.lastRoundDirection = "Left"
                                    gv.lastRoundSpace = gv.lastRoundShipInitialLocation.ToCharArray()(0) & (Integer.Parse(gv.lastRoundShipInitialLocation.ToCharArray()(1)) - 1)
                                    Dim space As String = gv.lastRoundSpace

                                    Console.WriteLine("Selected Space: " & space)
                                    Dim button As Button
                                    Try
                                        button = DirectCast(My.Forms.Form6.Controls.Find(space & "Player", True)(0), Button)
                                    Catch ex As Exception
                                        If gv.lastRoundShipInitialLocation.Contains("10") Then
                                            gv.lastRoundDirection = "Down"
                                            gv.lastRoundSpace = letters(Array.IndexOf(letters, gv.lastRoundShipInitialLocation.ToCharArray()(0)) - 1) & (Integer.Parse(10))
                                            space = gv.lastRoundSpace
                                            button = DirectCast(My.Forms.Form6.Controls.Find(space & "Player", True)(0), Button)
                                        Else
                                            gv.lastRoundDirection = "Up"
                                            gv.lastRoundSpace = letters(Array.IndexOf(letters, gv.lastRoundShipInitialLocation.ToCharArray()(0)) - 1) & (Integer.Parse(gv.lastRoundShipInitialLocation.ToCharArray()(1)))
                                            space = gv.lastRoundSpace
                                            button = DirectCast(My.Forms.Form6.Controls.Find(space & "Player", True)(0), Button)
                                        End If
                                    End Try


                                    If button.Text = "-" Or button.Text = "" Then
                                        For Each x In gv.shipLocations
                                            If x.Contains(space) Then
                                                gv.shipLocations.Remove(x)
                                                x.Remove(space)
                                                If x.Count > 0 Then
                                                    gv.shipLocations.Add(x)
                                                    Turn(space, "Hit")
                                                    gv.lastRoundHit = True
                                                    gv.lastRoundSunk = False
                                                    gv.lastRoundSpace = space
                                                    button.Text = "X"
                                                    gv.lastRoundHitsInDirection = True
                                                    Console.WriteLine("[Debug] Hit = " & gv.lastRoundHit & ", Sunk = " & gv.lastRoundSunk & ", Space = " & gv.lastRoundSpace & ", FoundShip = " & gv.lastRoundFoundShip & ", ShipInitialLocation = " & gv.lastRoundShipInitialLocation & ", Direction = " & gv.lastRoundDirection & ", HitsInDirection = " & gv.lastRoundHitsInDirection)
                                                    AI_Turn()
                                                    Exit Sub
                                                End If
                                                Turn(space, "Sunk")
                                                gv.lastRoundHit = True
                                                gv.lastRoundSunk = True
                                                gv.lastRoundSpace = space
                                                gv.lastRoundFoundShip = False
                                                gv.lastRoundHitsInDirection = False
                                                button.Text = "X"
                                                If gv.shipLocations.Count = 0 Then
                                                    MsgBox("You Lost! The AI sunk all of your ships!")
                                                    Form1.Activate()
                                                    Form1.Show()
                                                    Me.Close()
                                                    Exit Sub
                                                End If
                                                AI_Turn()
                                                Console.WriteLine("[Debug] Hit = " & gv.lastRoundHit & ", Sunk = " & gv.lastRoundSunk & ", Space = " & gv.lastRoundSpace & ", FoundShip = " & gv.lastRoundFoundShip & ", ShipInitialLocation = " & gv.lastRoundShipInitialLocation & ", Direction = " & gv.lastRoundDirection & ", HitsInDirection = " & gv.lastRoundHitsInDirection)
                                                Exit Sub
                                            End If
                                        Next
                                        Turn(space, "Miss")
                                        gv.lastRoundHit = False
                                        gv.lastRoundSunk = False
                                        gv.lastRoundSpace = space
                                        lblTurn.Text = "Your Turn"
                                        button.Text = "O"
                                    Else
                                        Console.WriteLine("[Debug] Hit = " & gv.lastRoundHit & ", Sunk = " & gv.lastRoundSunk & ", Space = " & gv.lastRoundSpace & ", FoundShip = " & gv.lastRoundFoundShip & ", ShipInitialLocation = " & gv.lastRoundShipInitialLocation & ", Direction = " & gv.lastRoundDirection & ", HitsInDirection = " & gv.lastRoundHitsInDirection)
                                        If gv.lastRoundInfinateLoop Then
                                            gv.lastRoundInfinateLoop = False
                                            gv.lastRoundDirection = "Down"
                                            gv.lastRoundDoubleInfinateLoop = True
                                        End If
                                        AI_Turn()
                                    End If
                                Else
                                    If gv.lastRoundShipInitialLocation.Contains("1") Then
                                        gv.lastRoundDirection = "Up"
                                        If gv.lastRoundShipInitialLocation.Contains("10") Then
                                            gv.lastRoundSpace = letters(Array.IndexOf(letters, gv.lastRoundShipInitialLocation.ToCharArray()(0)) - 1) & (Integer.Parse(10))
                                        Else
                                            gv.lastRoundSpace = letters(Array.IndexOf(letters, gv.lastRoundShipInitialLocation.ToCharArray()(0)) - 1) & (Integer.Parse(gv.lastRoundShipInitialLocation.ToCharArray()(1)))
                                        End If
                                        Dim space As String = gv.lastRoundSpace

                                        Console.WriteLine("Selected Space: " & space)

                                        Dim button As Button = DirectCast(My.Forms.Form6.Controls.Find(space & "Player", True)(0), Button)

                                        If button.Text = "-" Or button.Text = "" Then
                                            For Each x In gv.shipLocations
                                                If x.Contains(space) Then
                                                    gv.shipLocations.Remove(x)
                                                    x.Remove(space)
                                                    If x.Count > 0 Then
                                                        gv.shipLocations.Add(x)
                                                        Turn(space, "Hit")
                                                        gv.lastRoundHit = True
                                                        gv.lastRoundSunk = False
                                                        gv.lastRoundSpace = space
                                                        button.Text = "X"
                                                        gv.lastRoundHitsInDirection = True
                                                        Console.WriteLine("[Debug] Hit = " & gv.lastRoundHit & ", Sunk = " & gv.lastRoundSunk & ", Space = " & gv.lastRoundSpace & ", FoundShip = " & gv.lastRoundFoundShip & ", ShipInitialLocation = " & gv.lastRoundShipInitialLocation & ", Direction = " & gv.lastRoundDirection & ", HitsInDirection = " & gv.lastRoundHitsInDirection)
                                                        AI_Turn()
                                                        Exit Sub
                                                    End If
                                                    Turn(space, "Sunk")
                                                    gv.lastRoundHit = True
                                                    gv.lastRoundSunk = True
                                                    gv.lastRoundSpace = space
                                                    gv.lastRoundFoundShip = False
                                                    gv.lastRoundHitsInDirection = False
                                                    button.Text = "X"
                                                    If gv.shipLocations.Count = 0 Then
                                                        MsgBox("You Lost! The AI sunk all of your ships!")
                                                        Form1.Activate()
                                                        Form1.Show()
                                                        Me.Close()
                                                        Exit Sub
                                                    End If
                                                    Console.WriteLine("[Debug] Hit = " & gv.lastRoundHit & ", Sunk = " & gv.lastRoundSunk & ", Space = " & gv.lastRoundSpace & ", FoundShip = " & gv.lastRoundFoundShip & ", ShipInitialLocation = " & gv.lastRoundShipInitialLocation & ", Direction = " & gv.lastRoundDirection & ", HitsInDirection = " & gv.lastRoundHitsInDirection)
                                                    AI_Turn()
                                                    Exit Sub
                                                End If
                                            Next
                                            Turn(space, "Miss")
                                            gv.lastRoundHit = False
                                            gv.lastRoundSunk = False
                                            gv.lastRoundSpace = space
                                            lblTurn.Text = "Your Turn"
                                            button.Text = "O"
                                        Else
                                            Console.WriteLine("[Debug] Hit = " & gv.lastRoundHit & ", Sunk = " & gv.lastRoundSunk & ", Space = " & gv.lastRoundSpace & ", FoundShip = " & gv.lastRoundFoundShip & ", ShipInitialLocation = " & gv.lastRoundShipInitialLocation & ", Direction = " & gv.lastRoundDirection & ", HitsInDirection = " & gv.lastRoundHitsInDirection)
                                            AI_Turn()
                                        End If
                                    Else
                                        gv.lastRoundDirection = "Left"
                                        gv.lastRoundSpace = gv.lastRoundShipInitialLocation.ToCharArray()(0) & (Integer.Parse(gv.lastRoundShipInitialLocation.ToCharArray()(1)) - 1)
                                        Dim space As String = gv.lastRoundSpace

                                        Console.WriteLine("Selected Space: " & space)

                                        Dim button As Button = DirectCast(My.Forms.Form6.Controls.Find(space & "Player", True)(0), Button)

                                        If button.Text = "-" Or button.Text = "" Then
                                            For Each x In gv.shipLocations
                                                If x.Contains(space) Then
                                                    gv.shipLocations.Remove(x)
                                                    x.Remove(space)
                                                    If x.Count > 0 Then
                                                        gv.shipLocations.Add(x)
                                                        Turn(space, "Hit")
                                                        gv.lastRoundHit = True
                                                        gv.lastRoundSunk = False
                                                        gv.lastRoundSpace = space
                                                        button.Text = "X"
                                                        gv.lastRoundHitsInDirection = True
                                                        Console.WriteLine("[Debug] Hit = " & gv.lastRoundHit & ", Sunk = " & gv.lastRoundSunk & ", Space = " & gv.lastRoundSpace & ", FoundShip = " & gv.lastRoundFoundShip & ", ShipInitialLocation = " & gv.lastRoundShipInitialLocation & ", Direction = " & gv.lastRoundDirection & ", HitsInDirection = " & gv.lastRoundHitsInDirection)
                                                        AI_Turn()
                                                        Exit Sub
                                                    End If
                                                    Turn(space, "Sunk")
                                                    gv.lastRoundHit = True
                                                    gv.lastRoundSunk = True
                                                    gv.lastRoundSpace = space
                                                    gv.lastRoundFoundShip = False
                                                    gv.lastRoundHitsInDirection = False
                                                    button.Text = "X"
                                                    If gv.shipLocations.Count = 0 Then
                                                        MsgBox("You Lost! The AI sunk all of your ships!")
                                                        Form1.Activate()
                                                        Form1.Show()
                                                        Me.Close()
                                                        Exit Sub
                                                    End If
                                                    Console.WriteLine("[Debug] Hit = " & gv.lastRoundHit & ", Sunk = " & gv.lastRoundSunk & ", Space = " & gv.lastRoundSpace & ", FoundShip = " & gv.lastRoundFoundShip & ", ShipInitialLocation = " & gv.lastRoundShipInitialLocation & ", Direction = " & gv.lastRoundDirection & ", HitsInDirection = " & gv.lastRoundHitsInDirection)
                                                    AI_Turn()
                                                    Exit Sub
                                                End If
                                            Next
                                            Turn(space, "Miss")
                                            gv.lastRoundHit = False
                                            gv.lastRoundSunk = False
                                            gv.lastRoundSpace = space
                                            lblTurn.Text = "Your Turn"
                                            button.Text = "O"
                                        Else
                                            Console.WriteLine("[Debug] Hit = " & gv.lastRoundHit & ", Sunk = " & gv.lastRoundSunk & ", Space = " & gv.lastRoundSpace & ", FoundShip = " & gv.lastRoundFoundShip & ", ShipInitialLocation = " & gv.lastRoundShipInitialLocation & ", Direction = " & gv.lastRoundDirection & ", HitsInDirection = " & gv.lastRoundHitsInDirection)
                                            AI_Turn()
                                        End If
                                    End If
                                End If
                            Case "Up"
                                If gv.lastRoundHitsInDirection Then
                                    gv.lastRoundDirection = "Down"
                                    If gv.lastRoundShipInitialLocation.Contains("10") Then
                                        gv.lastRoundSpace = letters(Array.IndexOf(letters, gv.lastRoundShipInitialLocation.ToCharArray()(0)) + 1) & (Integer.Parse(10))
                                    Else
                                        gv.lastRoundSpace = letters(Array.IndexOf(letters, gv.lastRoundShipInitialLocation.ToCharArray()(0)) + 1) & (Integer.Parse(gv.lastRoundShipInitialLocation.ToCharArray()(1)))
                                    End If
                                    Dim space As String = gv.lastRoundSpace
                                    gv.lastRoundInfinateLoopVertical = True

                                    Console.WriteLine("Selected Space: " & space)

                                    Dim button As Button = DirectCast(My.Forms.Form6.Controls.Find(space & "Player", True)(0), Button)

                                    If button.Text = "-" Or button.Text = "" Then
                                        For Each x In gv.shipLocations
                                            If x.Contains(space) Then
                                                gv.shipLocations.Remove(x)
                                                x.Remove(space)
                                                If x.Count > 0 Then
                                                    gv.shipLocations.Add(x)
                                                    Turn(space, "Hit")
                                                    gv.lastRoundHit = True
                                                    gv.lastRoundSunk = False
                                                    gv.lastRoundSpace = space
                                                    button.Text = "X"
                                                    gv.lastRoundHitsInDirection = True
                                                    Console.WriteLine("[Debug] Hit = " & gv.lastRoundHit & ", Sunk = " & gv.lastRoundSunk & ", Space = " & gv.lastRoundSpace & ", FoundShip = " & gv.lastRoundFoundShip & ", ShipInitialLocation = " & gv.lastRoundShipInitialLocation & ", Direction = " & gv.lastRoundDirection & ", HitsInDirection = " & gv.lastRoundHitsInDirection)
                                                    gv.lastRoundInfinateLoopVertical = False
                                                    AI_Turn()
                                                    Exit Sub
                                                End If
                                                Turn(space, "Sunk")
                                                gv.lastRoundHit = True
                                                gv.lastRoundSunk = True
                                                gv.lastRoundSpace = space
                                                gv.lastRoundFoundShip = False
                                                gv.lastRoundHitsInDirection = False
                                                button.Text = "X"
                                                If gv.shipLocations.Count = 0 Then
                                                    MsgBox("You Lost! The AI sunk all of your ships!")
                                                    Form1.Activate()
                                                    Form1.Show()
                                                    Me.Close()
                                                    Exit Sub
                                                End If
                                                Console.WriteLine("[Debug] Hit = " & gv.lastRoundHit & ", Sunk = " & gv.lastRoundSunk & ", Space = " & gv.lastRoundSpace & ", FoundShip = " & gv.lastRoundFoundShip & ", ShipInitialLocation = " & gv.lastRoundShipInitialLocation & ", Direction = " & gv.lastRoundDirection & ", HitsInDirection = " & gv.lastRoundHitsInDirection)
                                                gv.lastRoundInfinateLoopVertical = False
                                                AI_Turn()
                                                Exit Sub
                                            End If
                                        Next
                                        Turn(space, "Miss")
                                        gv.lastRoundHit = False
                                        gv.lastRoundSunk = False
                                        gv.lastRoundSpace = space
                                        lblTurn.Text = "Your Turn"
                                        button.Text = "O"
                                    Else
                                        Console.WriteLine("[Debug] Hit = " & gv.lastRoundHit & ", Sunk = " & gv.lastRoundSunk & ", Space = " & gv.lastRoundSpace & ", FoundShip = " & gv.lastRoundFoundShip & ", ShipInitialLocation = " & gv.lastRoundShipInitialLocation & ", Direction = " & gv.lastRoundDirection & ", HitsInDirection = " & gv.lastRoundHitsInDirection)
                                        AI_Turn()
                                    End If
                                Else
                                    If gv.lastRoundShipInitialLocation.Contains("J") Then
                                        gv.lastRoundDirection = "Right"
                                        gv.lastRoundSpace = gv.lastRoundShipInitialLocation.ToCharArray()(0) & (Integer.Parse(gv.lastRoundShipInitialLocation.ToCharArray()(1)) + 1)
                                        Dim space As String = gv.lastRoundSpace

                                        Console.WriteLine("Selected Space: " & space)

                                        Dim button As Button = DirectCast(My.Forms.Form6.Controls.Find(space & "Player", True)(0), Button)

                                        If button.Text = "-" Or button.Text = "" Then
                                            For Each x In gv.shipLocations
                                                If x.Contains(space) Then
                                                    gv.shipLocations.Remove(x)
                                                    x.Remove(space)
                                                    If x.Count > 0 Then
                                                        gv.shipLocations.Add(x)
                                                        Turn(space, "Hit")
                                                        gv.lastRoundHit = True
                                                        gv.lastRoundSunk = False
                                                        gv.lastRoundSpace = space
                                                        button.Text = "X"
                                                        gv.lastRoundHitsInDirection = True
                                                        Console.WriteLine("[Debug] Hit = " & gv.lastRoundHit & ", Sunk = " & gv.lastRoundSunk & ", Space = " & gv.lastRoundSpace & ", FoundShip = " & gv.lastRoundFoundShip & ", ShipInitialLocation = " & gv.lastRoundShipInitialLocation & ", Direction = " & gv.lastRoundDirection & ", HitsInDirection = " & gv.lastRoundHitsInDirection)
                                                        AI_Turn()
                                                        Exit Sub
                                                    End If
                                                    Turn(space, "Sunk")
                                                    gv.lastRoundHit = True
                                                    gv.lastRoundSunk = True
                                                    gv.lastRoundSpace = space
                                                    gv.lastRoundFoundShip = False
                                                    gv.lastRoundHitsInDirection = False
                                                    button.Text = "X"
                                                    If gv.shipLocations.Count = 0 Then
                                                        MsgBox("You Lost! The AI sunk all of your ships!")
                                                        Form1.Activate()
                                                        Form1.Show()
                                                        Me.Close()
                                                        Exit Sub
                                                    End If
                                                    Console.WriteLine("[Debug] Hit = " & gv.lastRoundHit & ", Sunk = " & gv.lastRoundSunk & ", Space = " & gv.lastRoundSpace & ", FoundShip = " & gv.lastRoundFoundShip & ", ShipInitialLocation = " & gv.lastRoundShipInitialLocation & ", Direction = " & gv.lastRoundDirection & ", HitsInDirection = " & gv.lastRoundHitsInDirection)
                                                    AI_Turn()
                                                    Exit Sub
                                                End If
                                            Next
                                            Turn(space, "Miss")
                                            gv.lastRoundHit = False
                                            gv.lastRoundSunk = False
                                            gv.lastRoundSpace = space
                                            lblTurn.Text = "Your Turn"
                                            button.Text = "O"
                                        Else
                                            Console.WriteLine("[Debug] Hit = " & gv.lastRoundHit & ", Sunk = " & gv.lastRoundSunk & ", Space = " & gv.lastRoundSpace & ", FoundShip = " & gv.lastRoundFoundShip & ", ShipInitialLocation = " & gv.lastRoundShipInitialLocation & ", Direction = " & gv.lastRoundDirection & ", HitsInDirection = " & gv.lastRoundHitsInDirection)
                                            AI_Turn()
                                        End If
                                    Else
                                        gv.lastRoundDirection = "Down"
                                        If gv.lastRoundShipInitialLocation.Contains("10") Then
                                            gv.lastRoundSpace = letters(Array.IndexOf(letters, gv.lastRoundShipInitialLocation.ToCharArray()(0)) + 1) & (Integer.Parse(10))
                                        Else
                                            gv.lastRoundSpace = letters(Array.IndexOf(letters, gv.lastRoundShipInitialLocation.ToCharArray()(0)) + 1) & (Integer.Parse(gv.lastRoundSpace.ToCharArray()(1)))
                                        End If
                                        Dim space As String = gv.lastRoundSpace

                                        Console.WriteLine("Selected Space: " & space)

                                        Dim button As Button = DirectCast(My.Forms.Form6.Controls.Find(space & "Player", True)(0), Button)

                                        If button.Text = "-" Or button.Text = "" Then
                                            For Each x In gv.shipLocations
                                                If x.Contains(space) Then
                                                    gv.shipLocations.Remove(x)
                                                    x.Remove(space)
                                                    If x.Count > 0 Then
                                                        gv.shipLocations.Add(x)
                                                        Turn(space, "Hit")
                                                        gv.lastRoundHit = True
                                                        gv.lastRoundSunk = False
                                                        gv.lastRoundSpace = space
                                                        button.Text = "X"
                                                        gv.lastRoundHitsInDirection = True
                                                        Console.WriteLine("[Debug] Hit = " & gv.lastRoundHit & ", Sunk = " & gv.lastRoundSunk & ", Space = " & gv.lastRoundSpace & ", FoundShip = " & gv.lastRoundFoundShip & ", ShipInitialLocation = " & gv.lastRoundShipInitialLocation & ", Direction = " & gv.lastRoundDirection & ", HitsInDirection = " & gv.lastRoundHitsInDirection)
                                                        AI_Turn()
                                                        Exit Sub
                                                    End If
                                                    Turn(space, "Sunk")
                                                    gv.lastRoundHit = True
                                                    gv.lastRoundSunk = True
                                                    gv.lastRoundSpace = space
                                                    gv.lastRoundFoundShip = False
                                                    gv.lastRoundHitsInDirection = False
                                                    button.Text = "X"
                                                    If gv.shipLocations.Count = 0 Then
                                                        MsgBox("You Lost! The AI sunk all of your ships!")
                                                        Form1.Activate()
                                                        Form1.Show()
                                                        Me.Close()
                                                        Exit Sub
                                                    End If
                                                    Console.WriteLine("[Debug] Hit = " & gv.lastRoundHit & ", Sunk = " & gv.lastRoundSunk & ", Space = " & gv.lastRoundSpace & ", FoundShip = " & gv.lastRoundFoundShip & ", ShipInitialLocation = " & gv.lastRoundShipInitialLocation & ", Direction = " & gv.lastRoundDirection & ", HitsInDirection = " & gv.lastRoundHitsInDirection)
                                                    AI_Turn()
                                                    Exit Sub
                                                End If
                                            Next
                                            Turn(space, "Miss")
                                            gv.lastRoundHit = False
                                            gv.lastRoundSunk = False
                                            gv.lastRoundSpace = space
                                            lblTurn.Text = "Your Turn"
                                            button.Text = "O"
                                        Else
                                            Console.WriteLine("[Debug] Hit = " & gv.lastRoundHit & ", Sunk = " & gv.lastRoundSunk & ", Space = " & gv.lastRoundSpace & ", FoundShip = " & gv.lastRoundFoundShip & ", ShipInitialLocation = " & gv.lastRoundShipInitialLocation & ", Direction = " & gv.lastRoundDirection & ", HitsInDirection = " & gv.lastRoundHitsInDirection)
                                            AI_Turn()
                                        End If
                                    End If
                                End If
                            Case "Down"
                                If gv.lastRoundHitsInDirection Then
                                    gv.lastRoundDirection = "Up"
                                    If gv.lastRoundShipInitialLocation.Contains("10") Then
                                        gv.lastRoundSpace = letters(Array.IndexOf(letters, gv.lastRoundShipInitialLocation.ToCharArray()(0)) - 1) & (Integer.Parse(10))
                                    Else
                                        gv.lastRoundSpace = letters(Array.IndexOf(letters, gv.lastRoundShipInitialLocation.ToCharArray()(0)) - 1) & (Integer.Parse(gv.lastRoundSpace.ToCharArray()(1)))
                                    End If
                                    Dim space As String = gv.lastRoundSpace

                                    Console.WriteLine("Selected Space: " & space)

                                    Dim button As Button = DirectCast(My.Forms.Form6.Controls.Find(space & "Player", True)(0), Button)

                                    If button.Text = "-" Or button.Text = "" Then
                                        For Each x In gv.shipLocations
                                            If x.Contains(space) Then
                                                gv.shipLocations.Remove(x)
                                                x.Remove(space)
                                                If x.Count > 0 Then
                                                    gv.shipLocations.Add(x)
                                                    Turn(space, "Hit")
                                                    gv.lastRoundHit = True
                                                    gv.lastRoundSunk = False
                                                    gv.lastRoundSpace = space
                                                    button.Text = "X"
                                                    gv.lastRoundHitsInDirection = True
                                                    Console.WriteLine("[Debug] Hit = " & gv.lastRoundHit & ", Sunk = " & gv.lastRoundSunk & ", Space = " & gv.lastRoundSpace & ", FoundShip = " & gv.lastRoundFoundShip & ", ShipInitialLocation = " & gv.lastRoundShipInitialLocation & ", Direction = " & gv.lastRoundDirection & ", HitsInDirection = " & gv.lastRoundHitsInDirection)
                                                    AI_Turn()
                                                    Exit Sub
                                                End If
                                                Turn(space, "Sunk")
                                                gv.lastRoundHit = True
                                                gv.lastRoundSunk = True
                                                gv.lastRoundSpace = space
                                                gv.lastRoundFoundShip = False
                                                gv.lastRoundHitsInDirection = False
                                                button.Text = "X"
                                                If gv.shipLocations.Count = 0 Then
                                                    MsgBox("The AI chose " & space & ". You Lost! The AI sunk all of your ships!")
                                                    Form1.Activate()
                                                    Form1.Show()
                                                    Me.Close()
                                                    Exit Sub
                                                End If
                                                Console.WriteLine("[Debug] Hit = " & gv.lastRoundHit & ", Sunk = " & gv.lastRoundSunk & ", Space = " & gv.lastRoundSpace & ", FoundShip = " & gv.lastRoundFoundShip & ", ShipInitialLocation = " & gv.lastRoundShipInitialLocation & ", Direction = " & gv.lastRoundDirection & ", HitsInDirection = " & gv.lastRoundHitsInDirection)
                                                AI_Turn()
                                                Exit Sub
                                            End If
                                        Next
                                        MsgBox("The AI Missed!")
                                        gv.lastRoundHit = False
                                        gv.lastRoundSunk = False
                                        gv.lastRoundSpace = space
                                        lblTurn.Text = "Your Turn"
                                        button.Text = "O"
                                    Else
                                        Console.WriteLine("[Debug] Hit = " & gv.lastRoundHit & ", Sunk = " & gv.lastRoundSunk & ", Space = " & gv.lastRoundSpace & ", FoundShip = " & gv.lastRoundFoundShip & ", ShipInitialLocation = " & gv.lastRoundShipInitialLocation & ", Direction = " & gv.lastRoundDirection & ", HitsInDirection = " & gv.lastRoundHitsInDirection)
                                        If gv.lastRoundInfinateLoopVertical Then
                                            gv.lastRoundInfinateLoopVertical = False
                                            gv.lastRoundDirection = "Left"
                                            If gv.lastRoundDoubleInfinateLoop Then
                                                gv.lastRoundFoundShip = False
                                            End If
                                        End If
                                        AI_Turn()
                                    End If
                                Else
                                    gv.lastRoundDirection = "Right"
                                    gv.lastRoundSpace = gv.lastRoundShipInitialLocation.ToCharArray()(0) & (Integer.Parse(gv.lastRoundShipInitialLocation.ToCharArray()(1)) + 1)
                                    Dim space As String = gv.lastRoundSpace

                                    Console.WriteLine("Selected Space: " & space)

                                    Dim button As Button = DirectCast(My.Forms.Form6.Controls.Find(space & "Player", True)(0), Button)

                                    If button.Text = "-" Or button.Text = "" Then
                                        For Each x In gv.shipLocations
                                            If x.Contains(space) Then
                                                gv.shipLocations.Remove(x)
                                                x.Remove(space)
                                                If x.Count > 0 Then
                                                    gv.shipLocations.Add(x)
                                                    Turn(space, "Hit")
                                                    gv.lastRoundHit = True
                                                    gv.lastRoundSunk = False
                                                    gv.lastRoundSpace = space
                                                    button.Text = "X"
                                                    gv.lastRoundHitsInDirection = True
                                                    Console.WriteLine("[Debug] Hit = " & gv.lastRoundHit & ", Sunk = " & gv.lastRoundSunk & ", Space = " & gv.lastRoundSpace & ", FoundShip = " & gv.lastRoundFoundShip & ", ShipInitialLocation = " & gv.lastRoundShipInitialLocation & ", Direction = " & gv.lastRoundDirection & ", HitsInDirection = " & gv.lastRoundHitsInDirection)
                                                    AI_Turn()
                                                    Exit Sub
                                                End If
                                                Turn(space, "Sunk")
                                                gv.lastRoundHit = True
                                                gv.lastRoundSunk = True
                                                gv.lastRoundSpace = space
                                                gv.lastRoundFoundShip = False
                                                gv.lastRoundHitsInDirection = False
                                                button.Text = "X"
                                                If gv.shipLocations.Count = 0 Then
                                                    MsgBox("You Lost! The AI sunk all of your ships!")
                                                    Form1.Activate()
                                                    Form1.Show()
                                                    Me.Close()
                                                    Exit Sub
                                                End If
                                                Console.WriteLine("[Debug] Hit = " & gv.lastRoundHit & ", Sunk = " & gv.lastRoundSunk & ", Space = " & gv.lastRoundSpace & ", FoundShip = " & gv.lastRoundFoundShip & ", ShipInitialLocation = " & gv.lastRoundShipInitialLocation & ", Direction = " & gv.lastRoundDirection & ", HitsInDirection = " & gv.lastRoundHitsInDirection)
                                                AI_Turn()
                                                Exit Sub
                                            End If
                                        Next
                                        Turn(space, "Miss")
                                        gv.lastRoundHit = False
                                        gv.lastRoundSunk = False
                                        gv.lastRoundSpace = space
                                        lblTurn.Text = "Your Turn"
                                        button.Text = "O"
                                    Else
                                        Console.WriteLine("[Debug] Hit = " & gv.lastRoundHit & ", Sunk = " & gv.lastRoundSunk & ", Space = " & gv.lastRoundSpace & ", FoundShip = " & gv.lastRoundFoundShip & ", ShipInitialLocation = " & gv.lastRoundShipInitialLocation & ", Direction = " & gv.lastRoundDirection & ", HitsInDirection = " & gv.lastRoundHitsInDirection)
                                        AI_Turn()
                                    End If
                                End If
                        End Select
                    Catch ex As StackOverflowException
                        gv.lastRoundHitsInDirection = "Up"
                        If gv.lastRoundShipInitialLocation.Contains("10") Then
                            gv.lastRoundSpace = letters(Array.IndexOf(letters, gv.lastRoundShipInitialLocation.ToCharArray()(0)) - 1) & (Integer.Parse(10))
                        Else
                            gv.lastRoundSpace = letters(Array.IndexOf(letters, gv.lastRoundShipInitialLocation.ToCharArray()(0)) - 1) & (Integer.Parse(gv.lastRoundSpace.ToCharArray()(1)))
                        End If
                        Dim space As String = gv.lastRoundSpace

                        Console.WriteLine("Selected Space: " & space)

                        Dim button As Button = DirectCast(My.Forms.Form6.Controls.Find(space & "Player", True)(0), Button)

                        If button.Text = "-" Or button.Text = "" Then
                            For Each x In gv.shipLocations
                                If x.Contains(space) Then
                                    gv.shipLocations.Remove(x)
                                    x.Remove(space)
                                    If x.Count > 0 Then
                                        gv.shipLocations.Add(x)
                                        Turn(space, "Hit")
                                        gv.lastRoundHit = True
                                        gv.lastRoundSunk = False
                                        gv.lastRoundSpace = space
                                        button.Text = "X"
                                        gv.lastRoundHitsInDirection = True
                                        Console.WriteLine("[Debug] Hit = " & gv.lastRoundHit & ", Sunk = " & gv.lastRoundSunk & ", Space = " & gv.lastRoundSpace & ", FoundShip = " & gv.lastRoundFoundShip & ", ShipInitialLocation = " & gv.lastRoundShipInitialLocation & ", Direction = " & gv.lastRoundDirection & ", HitsInDirection = " & gv.lastRoundHitsInDirection)
                                        AI_Turn()
                                        Exit Sub
                                    End If
                                    Turn(space, "Sunk")
                                    gv.lastRoundHit = True
                                    gv.lastRoundSunk = True
                                    gv.lastRoundSpace = space
                                    gv.lastRoundFoundShip = False
                                    gv.lastRoundHitsInDirection = False
                                    button.Text = "X"
                                    If gv.shipLocations.Count = 0 Then
                                        MsgBox("The AI chose " & space & ". You Lost! The AI sunk all of your ships!")
                                        Form1.Activate()
                                        Form1.Show()
                                        Me.Close()
                                        Exit Sub
                                    End If
                                    Console.WriteLine("[Debug] Hit = " & gv.lastRoundHit & ", Sunk = " & gv.lastRoundSunk & ", Space = " & gv.lastRoundSpace & ", FoundShip = " & gv.lastRoundFoundShip & ", ShipInitialLocation = " & gv.lastRoundShipInitialLocation & ", Direction = " & gv.lastRoundDirection & ", HitsInDirection = " & gv.lastRoundHitsInDirection)
                                    AI_Turn()
                                    Exit Sub
                                Else
                                    gv.lastRoundDirection = "Down"
                                    If gv.lastRoundShipInitialLocation.Contains("10") Then
                                        gv.lastRoundSpace = letters(Array.IndexOf(letters, gv.lastRoundShipInitialLocation.ToCharArray()(0)) + 1) & (Integer.Parse(10))
                                    Else
                                        gv.lastRoundSpace = letters(Array.IndexOf(letters, gv.lastRoundShipInitialLocation.ToCharArray()(0)) + 1) & (Integer.Parse(gv.lastRoundSpace.ToCharArray()(1)))
                                    End If
                                    Dim space2 As String = gv.lastRoundSpace

                                    Console.WriteLine("Selected Space: " & space2)

                                    Dim button2 As Button = DirectCast(My.Forms.Form6.Controls.Find(space2 & "Player", True)(0), Button)

                                    If button2.Text = "-" Or button2.Text = "" Then
                                        For Each y In gv.shipLocations
                                            If y.Contains(space2) Then
                                                gv.shipLocations.Remove(y)
                                                y.Remove(space2)
                                                If y.Count > 0 Then
                                                    gv.shipLocations.Add(y)
                                                    MsgBox("The AI chose " & space2 & ". The AI Hit!")
                                                    gv.lastRoundHit = True
                                                    gv.lastRoundSunk = False
                                                    gv.lastRoundSpace = space2
                                                    button2.Text = "X"
                                                    gv.lastRoundHitsInDirection = True
                                                    Console.WriteLine("[Debug] Hit = " & gv.lastRoundHit & ", Sunk = " & gv.lastRoundSunk & ", Space = " & gv.lastRoundSpace & ", FoundShip = " & gv.lastRoundFoundShip & ", ShipInitialLocation = " & gv.lastRoundShipInitialLocation & ", Direction = " & gv.lastRoundDirection & ", HitsInDirection = " & gv.lastRoundHitsInDirection)
                                                    AI_Turn()
                                                    Exit Sub
                                                End If
                                                Turn(space, "Sunk")
                                                gv.lastRoundHit = True
                                                gv.lastRoundSunk = True
                                                gv.lastRoundSpace = space2
                                                gv.lastRoundFoundShip = False
                                                gv.lastRoundHitsInDirection = False
                                                button2.Text = "X"
                                                If gv.shipLocations.Count = 0 Then
                                                    MsgBox("You Lost! The AI sunk all of your ships!")
                                                    Form1.Activate()
                                                    Form1.Show()
                                                    Me.Close()
                                                    Exit Sub
                                                End If
                                                Console.WriteLine("[Debug] Hit = " & gv.lastRoundHit & ", Sunk = " & gv.lastRoundSunk & ", Space = " & gv.lastRoundSpace & ", FoundShip = " & gv.lastRoundFoundShip & ", ShipInitialLocation = " & gv.lastRoundShipInitialLocation & ", Direction = " & gv.lastRoundDirection & ", HitsInDirection = " & gv.lastRoundHitsInDirection)
                                                AI_Turn()
                                                Exit Sub
                                            End If
                                        Next
                                        Turn(space, "Miss")
                                        gv.lastRoundHit = False
                                        gv.lastRoundSunk = False
                                        gv.lastRoundSpace = space
                                        lblTurn.Text = "Your Turn"
                                        button2.Text = "O"
                                    End If
                                End If
                            Next
                            MsgBox("The AI Missed!")
                            gv.lastRoundHit = False
                            gv.lastRoundSunk = False
                            gv.lastRoundSpace = space
                            lblTurn.Text = "Your Turn"
                            button.Text = "O"
                        End If
                    End Try
                End If
            Else
                Dim number As Integer
                Dim number2 As Integer
                Randomize()
                number = Int(Rnd() * 10) + 1
                number2 = Int(Rnd() * 9) + 1

                Dim ship As List(Of String) = {""}.ToList
                Dim space As String = letters(number2) & number
                If Not spaces.Contains(space) Then
                    Threading.Thread.Sleep(TimeSpan.FromSeconds(1))
                    AI_Turn()
                    Exit Sub
                End If
                Dim button As Button = DirectCast(My.Forms.Form6.Controls.Find(space & "Player", True)(0), Button)

                If button.Text = "-" Or button.Text = "" Then
                    spaces.Remove(space)
                    For Each x In gv.shipLocations
                        If x.Contains(space) Then
                            gv.shipLocations.Remove(x)
                            x.Remove(space)
                            If x.Count > 0 Then
                                gv.shipLocations.Add(x)
                                Turn(space, "Hit")
                                gv.lastRoundHit = True
                                gv.lastRoundSunk = False
                                gv.lastRoundSpace = space
                                gv.lastRoundFoundShip = True
                                gv.lastRoundShipInitialLocation = space
                                Console.WriteLine("Ship found! Located at: " & space)
                                button.Text = "X"
                                Console.WriteLine("[Debug] Hit = " & gv.lastRoundHit & ", Sunk = " & gv.lastRoundSunk & ", Space = " & gv.lastRoundSpace & ", FoundShip = " & gv.lastRoundFoundShip & ", ShipInitialLocation = " & gv.lastRoundShipInitialLocation & ", Direction = " & gv.lastRoundDirection & ", HitsInDirection = " & gv.lastRoundHitsInDirection)
                                AI_Turn()
                                Exit Sub
                            End If
                            Turn(space, "Sunk")
                            gv.lastRoundHit = True
                            gv.lastRoundSunk = True
                            gv.lastRoundSpace = space
                            gv.lastRoundFoundShip = False
                            button.Text = "X"
                            If gv.shipLocations.Count = 0 Then
                                MsgBox("You Lost! The AI sunk all of your ships!")
                                Form1.Activate()
                                Form1.Show()
                                Me.Close()
                                Exit Sub
                            End If
                            Console.WriteLine("[Debug] Hit = " & gv.lastRoundHit & ", Sunk = " & gv.lastRoundSunk & ", Space = " & gv.lastRoundSpace & ", FoundShip = " & gv.lastRoundFoundShip & ", ShipInitialLocation = " & gv.lastRoundShipInitialLocation & ", Direction = " & gv.lastRoundDirection & ", HitsInDirection = " & gv.lastRoundHitsInDirection)
                            AI_Turn()
                            Exit Sub
                        End If
                    Next
                    Turn(space, "Miss")
                    gv.lastRoundHit = False
                    gv.lastRoundSunk = False
                    gv.lastRoundSpace = space
                    lblTurn.Text = "Your Turn"
                    button.Text = "O"
                Else
                    Console.WriteLine("[Debug] Hit = " & gv.lastRoundHit & ", Sunk = " & gv.lastRoundSunk & ", Space = " & gv.lastRoundSpace & ", FoundShip = " & gv.lastRoundFoundShip & ", ShipInitialLocation = " & gv.lastRoundShipInitialLocation & ", Direction = " & gv.lastRoundDirection & ", HitsInDirection = " & gv.lastRoundHitsInDirection)
                    Threading.Thread.Sleep(TimeSpan.FromSeconds(1))
                    AI_Turn()
                End If
            End If
        Catch ex As StackOverflowException
            gv.lastRoundHitsInDirection = "Up"
            If gv.lastRoundShipInitialLocation.Contains("10") Then
                gv.lastRoundSpace = letters(Array.IndexOf(letters, gv.lastRoundShipInitialLocation.ToCharArray()(0)) - 1) & (Integer.Parse(10))
            Else
                gv.lastRoundSpace = letters(Array.IndexOf(letters, gv.lastRoundShipInitialLocation.ToCharArray()(0)) - 1) & (Integer.Parse(gv.lastRoundSpace.ToCharArray()(1)))
            End If
            Dim space As String = gv.lastRoundSpace

            Console.WriteLine("Selected Space: " & space)

            Dim button As Button = DirectCast(My.Forms.Form6.Controls.Find(space & "Player", True)(0), Button)

            If button.Text = "-" Or button.Text = "" Then
                For Each x In gv.shipLocations
                    If x.Contains(space) Then
                        gv.shipLocations.Remove(x)
                        x.Remove(space)
                        If x.Count > 0 Then
                            gv.shipLocations.Add(x)
                            Turn(space, "Hit")
                            gv.lastRoundHit = True
                            gv.lastRoundSunk = False
                            gv.lastRoundSpace = space
                            button.Text = "X"
                            gv.lastRoundHitsInDirection = True
                            Console.WriteLine("[Debug] Hit = " & gv.lastRoundHit & ", Sunk = " & gv.lastRoundSunk & ", Space = " & gv.lastRoundSpace & ", FoundShip = " & gv.lastRoundFoundShip & ", ShipInitialLocation = " & gv.lastRoundShipInitialLocation & ", Direction = " & gv.lastRoundDirection & ", HitsInDirection = " & gv.lastRoundHitsInDirection)
                            AI_Turn()
                            Exit Sub
                        End If
                        Turn(space, "Sunk")
                        gv.lastRoundHit = True
                        gv.lastRoundSunk = True
                        gv.lastRoundSpace = space
                        gv.lastRoundFoundShip = False
                        gv.lastRoundHitsInDirection = False
                        button.Text = "X"
                        If gv.shipLocations.Count = 0 Then
                            MsgBox("The AI chose " & space & ". You Lost! The AI sunk all of your ships!")
                            Form1.Activate()
                            Form1.Show()
                            Me.Close()
                            Exit Sub
                        End If
                        Console.WriteLine("[Debug] Hit = " & gv.lastRoundHit & ", Sunk = " & gv.lastRoundSunk & ", Space = " & gv.lastRoundSpace & ", FoundShip = " & gv.lastRoundFoundShip & ", ShipInitialLocation = " & gv.lastRoundShipInitialLocation & ", Direction = " & gv.lastRoundDirection & ", HitsInDirection = " & gv.lastRoundHitsInDirection)
                        AI_Turn()
                        Exit Sub
                    Else
                        gv.lastRoundDirection = "Down"
                        If gv.lastRoundShipInitialLocation.Contains("10") Then
                            gv.lastRoundSpace = letters(Array.IndexOf(letters, gv.lastRoundShipInitialLocation.ToCharArray()(0)) + 1) & (Integer.Parse(10))
                        Else
                            gv.lastRoundSpace = letters(Array.IndexOf(letters, gv.lastRoundShipInitialLocation.ToCharArray()(0)) + 1) & (Integer.Parse(gv.lastRoundSpace.ToCharArray()(1)))
                        End If
                        Dim space2 As String = gv.lastRoundSpace

                        Console.WriteLine("Selected Space: " & space2)

                        Dim button2 As Button = DirectCast(My.Forms.Form6.Controls.Find(space2 & "Player", True)(0), Button)

                        If button2.Text = "-" Or button2.Text = "" Then
                            For Each y In gv.shipLocations
                                If y.Contains(space2) Then
                                    gv.shipLocations.Remove(y)
                                    y.Remove(space2)
                                    If y.Count > 0 Then
                                        gv.shipLocations.Add(y)
                                        MsgBox("The AI chose " & space2 & ". The AI Hit!")
                                        gv.lastRoundHit = True
                                        gv.lastRoundSunk = False
                                        gv.lastRoundSpace = space2
                                        button2.Text = "X"
                                        gv.lastRoundHitsInDirection = True
                                        Console.WriteLine("[Debug] Hit = " & gv.lastRoundHit & ", Sunk = " & gv.lastRoundSunk & ", Space = " & gv.lastRoundSpace & ", FoundShip = " & gv.lastRoundFoundShip & ", ShipInitialLocation = " & gv.lastRoundShipInitialLocation & ", Direction = " & gv.lastRoundDirection & ", HitsInDirection = " & gv.lastRoundHitsInDirection)
                                        AI_Turn()
                                        Exit Sub
                                    End If
                                    Turn(space, "Sunk")
                                    gv.lastRoundHit = True
                                    gv.lastRoundSunk = True
                                    gv.lastRoundSpace = space2
                                    gv.lastRoundFoundShip = False
                                    gv.lastRoundHitsInDirection = False
                                    button2.Text = "X"
                                    If gv.shipLocations.Count = 0 Then
                                        MsgBox("You Lost! The AI sunk all of your ships!")
                                        Form1.Activate()
                                        Form1.Show()
                                        Me.Close()
                                        Exit Sub
                                    End If
                                    Console.WriteLine("[Debug] Hit = " & gv.lastRoundHit & ", Sunk = " & gv.lastRoundSunk & ", Space = " & gv.lastRoundSpace & ", FoundShip = " & gv.lastRoundFoundShip & ", ShipInitialLocation = " & gv.lastRoundShipInitialLocation & ", Direction = " & gv.lastRoundDirection & ", HitsInDirection = " & gv.lastRoundHitsInDirection)
                                    AI_Turn()
                                    Exit Sub
                                End If
                            Next
                            Turn(space, "Miss")
                            gv.lastRoundHit = False
                            gv.lastRoundSunk = False
                            gv.lastRoundSpace = space
                            lblTurn.Text = "Your Turn"
                            button2.Text = "O"
                        End If
                    End If
                Next
                MsgBox("The AI Missed!")
                gv.lastRoundHit = False
                gv.lastRoundSunk = False
                gv.lastRoundSpace = space
                lblTurn.Text = "Your Turn"
                button.Text = "O"
            End If
        End Try
    End Sub

    Private Sub DeprecateSpaces()
        Console.WriteLine("Deprecating invalid spaces.")
        Dim lowest As Byte = 5
        For Each x In spaces
            If x.Length < lowest Then
                lowest = x.Length
            End If
        Next
        For Each x In spaces
            Console.WriteLine("Checking space " & x)
            'checking vertical
            Dim level As Char = x.ToCharArray()(0)
            Dim vertical As Byte = 0

            Dim z As Byte
            Try
                If Byte.Parse(x.ToCharArray()(2)) = "0" Then
                    z = 10
                Else
                    z = Byte.Parse(x.ToCharArray()(1))
                End If
            Catch ex As Exception
                z = Byte.Parse(x.ToCharArray()(1))
            End Try
            Dim originalNumber As Byte = z

            Do While True
                If z = 0 Then
                    Exit Do
                End If
                If DirectCast(My.Forms.Form6.Controls.Find((level & z.ToString & "Player"), True)(0), Button).Text = "-" Then
                    Exit Do
                End If
                vertical += 1
                z = z - 1
            Loop
            z = originalNumber

            Do While True
                If z = 11 Then
                    Exit Do
                End If
                If DirectCast(My.Forms.Form6.Controls.Find((level & z.ToString & "Player"), True)(0), Button).Text = "-" Then
                    Exit Do
                End If
                vertical += 1
                z = z + 1
            Loop

            If vertical >= lowest Then
                Continue For
            End If

            'check horizonal
            'checking vertical
            Try
                If Byte.Parse(x.ToCharArray()(2)) = "0" Then
                    z = 10
                Else
                    z = Byte.Parse(x.ToCharArray()(1))
                End If
            Catch ex As Exception
                z = Byte.Parse(x.ToCharArray()(1))
            End Try
            Dim hlevel As Byte = z
            Dim horizonal As Byte = 0

            Dim originalRow As Byte = Array.IndexOf(letters, x.ToCharArray()(0))
            z = originalRow

            Do While True
                If z = 0 Then
                    Exit Do
                End If
                If DirectCast(My.Forms.Form6.Controls.Find(hlevel & letters(z - 1) & "Player", True)(0), Button).Text = "-" Then
                    Exit Do
                End If
                horizonal += 1
                z = z - 1
            Loop
            z = originalRow

            Do While True
                If z = 9 Then
                    Exit Do
                End If
                If DirectCast(My.Forms.Form6.Controls.Find((hlevel & letters(z - 1) & "Player"), True)(0), Button).Text = "-" Then
                    Exit Do
                End If
                horizonal += 1
                z = z + 1
            Loop

            If horizonal >= lowest Then
                Continue For
            End If

            'the space is invalid, remove it.
            Console.WriteLine("The space " & x & "is invalid. Removing from space roster.")
            spaces.Remove(x)
        Next
    End Sub

    Private Sub Form6_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        spaces.Clear()

        For x = 0 To 9
            For y = 1 To 10
                spaces.Add(letters(x) & y)
            Next
        Next
        For Each x In spaces
            Console.WriteLine(x)
        Next
        Console.WriteLine(spaces.Count)

        My.Forms.Form7.Show()
        My.Forms.Form7.FlowLayoutPanel1.Controls.Add(New UserControl2)

        Connection.Open()

        Dim sqlString As String = "UPDATE" & gv.username & "SET Games = Games + 1 WHERE ID=1"
        dataAdapter = New OleDb.OleDbDataAdapter(sqlString, Connection)

        Connection.Close()
    End Sub

    Private Sub Turn(ByRef space As String, ByRef result As String)

        Dim hit As New UserControl1
        hit.turn.Text = "AI's Turn"
        hit.Label3.Text = space
        hit.Label4.Text = result

        My.Forms.Form7.FlowLayoutPanel1.Controls.Add(hit)
        If result = "Miss" Then
            lblTurn.Text = "Your Turn"
            MsgBox("The AI tried space" & space & " and missed! Your turn!")
        Else
            MsgBox("The AI tried space" & space & " and hit!")
        End If
    End Sub
End Class