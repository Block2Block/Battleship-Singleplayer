Imports ADOX

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

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim cat As Catalog = New Catalog()

        If Not System.IO.File.Exists("C:\Program Files\Battleship Singleplayer\UserData.mdb") Then

            My.Computer.FileSystem.CreateDirectory("C:\Program Files\Battleship Singleplayer")

            cat.Create("Provider=Microsoft.Jet.OLEDB.4.0;" &
                 "Data Source=C:\Program Files\Battleship Singleplayer\UserData.mdb;" &
                 "Jet OLEDB:Engine Type=5")
            Console.WriteLine("Database Created Successfully")
            cat = Nothing

            Dim Connection As New OleDb.OleDbConnection("Provider=Microsoft.JET.OLEDB.4.0;Data Source=C:\Program Files\Battleship Singleplayer\UserData.mdb")
            Connection.Close()
            Connection.Open()

            Dim dt As New DataTable

            Dim DataAdapter As New OleDb.OleDbDataAdapter

            Dim sqlString As String = "CREATE TABLE userDetails (Username varchar(255),Password varchar(255));"
            DataAdapter = New OleDb.OleDbDataAdapter(sqlString, Connection)
            Connection.Close()
        End If

        My.Forms.Form7.Close()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        My.Forms.Form8.Show()
    End Sub
End Class
