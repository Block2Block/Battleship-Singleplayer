Public Class Form7

    Private Sub Form7_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged
        If FlowLayoutPanel1.Height > 102 Then
            FlowLayoutPanel1.Height = Me.Height - 102
        End If

        FlowLayoutPanel1.Left = (Me.Width / 2) - 230
        Label1.Left = (Me.Width / 2) - 94
    End Sub
End Class