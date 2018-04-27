Public Class gv
    Public Shared selection As String
    Public Shared ship As String
    Public Shared shipLocations As List(Of List(Of String)) = {{""}.ToList}.ToList
    Public Shared shipLocationsAI As List(Of List(Of String)) = {{""}.ToList}.ToList
    Public Shared destroyerCount As Byte = 1
    Public Shared cruiseCount As Byte = 2
    Public Shared battleshipCount As Byte = 1
    Public Shared carrierCount As Byte = 1
    Public Shared lastRoundHit As Boolean = False
    Public Shared lastRoundSpace As String
    Public Shared lastRoundSunk As Boolean = False
End Class