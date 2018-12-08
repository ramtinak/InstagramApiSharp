'
'  Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https :  //t.me/ramtinak ]
'
'  Github source : https :  //github.com/ramtinak/InstagramApiSharp
'  Nuget package : https :    //www.nuget.org/packages/InstagramApiSharp
'  Update Date:   08 December 2018
'  IRANIAN DEVELOPERS
'
Imports InstagramApiSharp.Classes.Models

Public Class DebugUtils
    Public Shared Function PrintMedia(ByVal header As String, ByVal media As InstaMedia) As String
        Dim content = $"{header}: {Truncate(media.Caption?.Text, 30)}, {media.Code}"
        Debug.WriteLine(content)
        Return content
    End Function

    Public Shared Function Truncate(ByVal value As String, ByVal maxChars As Integer) As String
        If value Is Nothing Then
            Return String.Empty
        End If
        If value.Length <= maxChars Then
            Return value
        End If
        Return value.Substring(0, maxChars) & "..."
    End Function
End Class