Public Class Form8

    Dim Connection As New OleDb.OleDbConnection("Provider=Microsoft.JET.OLEDB.4.0;Data Source=C:\Program Files\Battleship Singleplayer\UserData.mdb")

    Dim tb As New DataTable
    Dim dataAdapter As New OleDb.OleDbDataAdapter

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Connection.Close()
        Connection.Open()

        Dim sqlString As String = "SELECT Password FROM userDetails WHERE Username='" & TextBox2.Text & "';"
        Dim dataAdapter As New OleDb.OleDbDataAdapter(sqlString, Connection)

        tb.Clear()
        dataAdapter.Fill(tb)
        Connection.Close()

        If tb.Rows.Count = 0 Then
            If Not tb.Rows(0)(2) = TextBox2.Text Then
                MsgBox("Incorrect Password, please try again!")
                Exit Sub
            End If
            Connection.Open()
            sqlString = "SELECT * FROM " & TextBox2.Text & ";"
            dataAdapter = New OleDb.OleDbDataAdapter(sqlString, Connection)

            tb.Clear()
            dataAdapter.Fill(tb)
            Connection.Close()

            My.Forms.Form1.Button3.Enabled = False
            My.Forms.Form1.gp.Text = tb.Rows(0)(2)
            My.Forms.Form1.w.Text = tb.Rows(0)(1)
            MsgBox("You have successfully logged in! To log out, just restart the application!")
            gv.username = TextBox1.Text
            Me.Close()
        Else
            MsgBox("There is no user by that name!")
        End If

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If TextBox1.Text = "" & TextBox2.Text = "" Then
            MsgBox("You must type in a username and a password!")
            Exit Sub
        End If
        Connection.Close()
        Connection.Open()

        Dim sqlString As String = "SELECT Password FROM userDetails WHERE Username='" & TextBox2.Text & "';"
        Dim dataAdapter As New OleDb.OleDbDataAdapter(sqlString, Connection)

        tb.Clear()
        dataAdapter.Fill(tb)
        Connection.Close()

        If tb.Rows.Count = 0 Then
            sqlString = "INSERT INTO userDetails ([Username], [Password]) VALUES ('" & TextBox1.Text & "', '" & TextBox2.Text & "');"
            dataAdapter = New OleDb.OleDbDataAdapter(sqlString, Connection)

            sqlString = "CREATE TABLE " & TextBox1.Text & " (ID INT, Wins INT, Games INT);"
            dataAdapter = New OleDb.OleDbDataAdapter(sqlString, Connection)
            sqlString = "INSERT INTO" & TextBox1.Text & "([ID], [Wins], [Games]) VALUES (1, 0, 0);"
            dataAdapter = New OleDb.OleDbDataAdapter(sqlString, Connection)

            My.Forms.Form1.Button3.Enabled = False
            My.Forms.Form1.gp.Text = 0
            My.Forms.Form1.w.Text = 0
            MsgBox("You have successfully registered your account! To log out, just restart the application!")
            Me.Close()
        Else
            MsgBox("Someone has already used that username, please try a different one!")
        End If
    End Sub
End Class