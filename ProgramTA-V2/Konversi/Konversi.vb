Imports System
Imports System.IO

Module Konversi
    Public Function StartKonversi(ByVal gambar As Bitmap, nominal As String, bagian As String)
        gambar = resizeImage(gambar)

        Dim TLine As String = ""
        For i = 0 To 31
            For j = 0 To 31
                Dim x As Double = CLng((gambar.GetPixel(j, i).R) + CLng(gambar.GetPixel(j, i).G) + CLng(gambar.GetPixel(j, i).B)) / 3
                If x <= 180 Then
                    TLine &= "0"
                Else
                    TLine &= "1"
                End If
                TLine &= ","
            Next
        Next
        TLine = TLine.Substring(0, TLine.Length - 1)

        If nominal = "1K" Then
            TLine &= ",1,0,0,0,0,0,0"
        ElseIf nominal = "2K" Then
            TLine &= ",0,1,0,0,0,0,0"
        ElseIf nominal = "5K" Then
            TLine &= ",0,0,1,0,0,0,0"
        ElseIf nominal = "10K" Then
            TLine &= ",0,0,0,1,0,0,0"
        ElseIf nominal = "20K" Then
            TLine &= ",0,0,0,0,1,0,0"
        ElseIf nominal = "50K" Then
            TLine &= ",0,0,0,0,0,1,0"
        ElseIf nominal = "100K" Then
            TLine &= ",0,0,0,0,0,0,1"
        End If
        Return TLine
    End Function

    Function resizeImage(ByVal image As Bitmap) As Bitmap
        'Mengubah ukuran image menjadi 16x16 pixel
        Dim thumbnail As New Bitmap(32, 32)
        Dim graphic As Graphics = System.Drawing.Graphics.FromImage(thumbnail)

        graphic.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
        graphic.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
        graphic.PixelOffsetMode = Drawing2D.PixelOffsetMode.HighQuality
        graphic.CompositingQuality = Drawing2D.CompositingQuality.HighQuality

        Dim ratioX As Double = CDbl(32) / CDbl(image.Width)
        Dim ratioY As Double = CDbl(32) / CDbl(image.Height)
        Dim ratio As Double = If(ratioX < ratioY, ratioX, ratioY)

        Dim newHeight As Integer = Convert.ToInt32(image.Height * ratio)
        Dim newWidth As Integer = Convert.ToInt32(image.Width * ratio)

        Dim posX As Integer = Convert.ToInt32((32 - (image.Width * ratio)) / 2)
        Dim posY As Integer = Convert.ToInt32((32 - (image.Height * ratio)) / 2)

        graphic.Clear(Color.Black)
        graphic.DrawImage(image, posX, posY, newWidth, newHeight)

        'Dispose section
        graphic.Dispose()
        Return thumbnail
    End Function

    Sub SaveKonversi(bin() As String, no As String)
        Dim fs As FileStream
        Dim objWriter As System.IO.StreamWriter
        Dim TLine As String = ""
        Dim KonversiPath As String = ""
        If no = "1" Then
            KonversiPath = MainPath & "Output\Konversi1.txt"
        ElseIf no = "2" Then
            KonversiPath = MainPath & "Output\Konversi2.txt"
        ElseIf no = "3" Then
            KonversiPath = MainPath & "Output\Konversi3.txt"
        ElseIf no = "4" Then
            KonversiPath = MainPath & "Output\Konversi4.txt"
        End If
        'Digunakan untuk mengecek apakah notepadnya sudah ada
        If System.IO.File.Exists(KonversiPath) = False Then
            fs = File.Create(KonversiPath)
            fs.Dispose()
        Else
            ''Menambahkan line baru dibawahnya
            'Using sr As New StreamReader(KonversiPath)
            '    TLine &= sr.ReadToEnd()
            'End Using
            'System.IO.File.Delete(FName)
            System.IO.File.Delete(KonversiPath)
            fs = File.Create(KonversiPath)
            fs.Dispose()
        End If
        For i = 0 To bin.Count - 2
            TLine &= bin(i)
            TLine &= vbNewLine
        Next
        TLine &= bin(bin.Count - 1)
        objWriter = New System.IO.StreamWriter(KonversiPath)
        objWriter.Write(TLine)
        objWriter.Close()
        objWriter.Dispose()
    End Sub
End Module