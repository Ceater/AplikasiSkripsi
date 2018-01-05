Imports System.Drawing.Imaging
Imports AForge
Imports AForge.Imaging
Imports AForge.Imaging.Filters

Public Class Form1
    Dim cornerss As List(Of IntPoint)
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        PictureBox2.Image = BackgroundRemoval(PictureBox1.Image)
        PictureBox3.Image = blobx(PictureBox2.Image)
        PictureBox4.Image = QuadFinder(PictureBox3.Image)
        cornerss = cekUjung(PictureBox4.Image)
        'PictureBox5.Image = RotateGambar(PictureBox3.Image, PictureBox1.Image)
        PictureBox5.Image = FitToScreen(PictureBox4.Image)
        'MsgBox(cornerss(0).X & " " & cornerss(0).Y & " " & cornerss(1).X & " " & cornerss(1).Y & " " & cornerss(2).X & " " & cornerss(2).Y & " " & cornerss(3).X & " " & cornerss(3).Y)
    End Sub

    Function cekUjung(img As Bitmap) As List(Of IntPoint)
        Dim image As New Bitmap(img)
        Dim qf As New QuadrilateralFinder
        Dim corners As List(Of IntPoint) = qf.ProcessImage(image)
        Return corners
    End Function

    Function BackgroundRemoval(img As Bitmap) As Bitmap
        Dim bitx As Bitmap = New Bitmap(img)
        'Digunakan untuk menjadikan warna putih menjadi hitam
        Dim newImg As New Bitmap(bitx.Width, bitx.Height)
        Dim counterX, counterY As Integer
        For i = 0 To bitx.Width - 2
            counterY = 0
            For j = 0 To bitx.Height - 2
                counterY += 1
                Dim pixelColor As Color
                If bitx.GetPixel(i, j).R >= 230 And bitx.GetPixel(i, j).G >= 230 And bitx.GetPixel(i, j).B >= 230 Then
                    pixelColor = Color.Black
                Else
                    pixelColor = bitx.GetPixel(i, j)
                End If
                newImg.SetPixel(counterX, counterY, pixelColor)
            Next
            counterX += 1
        Next
        Return newImg
    End Function

    Function blobx(x As Bitmap)
        Dim newImg As Bitmap = New Bitmap(x)
        Dim filter As New BlobsFiltering
        filter.CoupledSizeFiltering = True
        filter.MinWidth = 200
        filter.MinHeight = 200
        filter.ApplyInPlace(newImg)
        Return newImg
    End Function

    Private Function QuadFinder(img As Bitmap)
        Dim image As New Bitmap(img)
        Dim qf As New QuadrilateralFinder
        Dim corners As List(Of IntPoint) = qf.ProcessImage(image)
        Dim Data As BitmapData = image.LockBits(New Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadWrite, image.PixelFormat)
        Drawing.Polygon(Data, corners, Color.Red)
        For i = 0 To corners.Count - 1
            Drawing.FillRectangle(Data, New Rectangle(corners(i).X - 2, corners(i).Y - 2, 5, 5), Color.FromArgb(i * 32 + 127 + 32, i * 64, i * 64))
        Next
        image.UnlockBits(Data)
        Return image
    End Function

    Private Function RotateGambar(img As Bitmap, img2 As Bitmap) As Bitmap
        Dim qf As New QuadrilateralFinder
        Dim corners As List(Of IntPoint) = qf.ProcessImage(img)
        Dim quadrilateralTransformation As New QuadrilateralTransformation(corners, 500, 500)
        Dim transformed As Bitmap = quadrilateralTransformation.Apply(img2)

        If cornerss(0).Y > cornerss(1).Y Then
            Dim temp As IntPoint
            temp = cornerss(0)
            cornerss(0) = cornerss(1)
            cornerss(1) = temp
        End If
        If cornerss(2).Y > cornerss(3).Y Then
            Dim temp As IntPoint
            temp = cornerss(2)
            cornerss(2) = cornerss(3)
            cornerss(3) = temp
        End If
        Dim newImg As Bitmap
        If cornerss(0).Y < cornerss(2).Y Then
            MsgBox("A")
            newImg = RotateImage(transformed, -90)
            'newImg = transformed
        Else
            MsgBox("B")
            newImg = RotateImage(transformed, 90)
        End If
        Return newImg
    End Function

    Private Function RotateImage(img As Bitmap, angle As Single) As Bitmap
        Dim retBMP As New Bitmap(img.Width, img.Height)
        retBMP.SetResolution(img.HorizontalResolution, img.VerticalResolution)
        Using g = Graphics.FromImage(retBMP)
            g.TranslateTransform(img.Width \ 2, img.Height \ 2)
            g.RotateTransform(angle)
            g.TranslateTransform(-img.Width \ 2, -img.Height \ 2)
            g.DrawImage(img, New PointF(0, 0))
            Return retBMP
        End Using
    End Function

    Function FitToScreen(img As Bitmap)
        'Digunakan untuk menghapus warna putih background
        Dim minX, maxX, minY, maxY As Integer
        minX = img.Width
        maxX = 0
        minY = img.Height
        maxY = 0
        Dim White As Integer = Color.Black.ToArgb()
        For x As Integer = 0 To img.Width - 1
            For y As Integer = 0 To img.Height - 1
                If (Not img.GetPixel(x, y).ToArgb() = White) And img.GetPixel(x, y).ToArgb() <> 0 Then
                    minX = System.Math.Min(minX, x)
                    maxX = System.Math.Max(maxX, x)
                    minY = System.Math.Min(minY, y)
                    maxY = System.Math.Max(maxY, y)
                End If
            Next
        Next
        Dim newImg As New Bitmap(maxX, maxY)
        Dim counterX, counterY As Integer
        For i = minX To maxX - 2
            counterY = 0
            For j = minY To maxY - 2
                counterY += 1
                Dim pixelColor As Color
                pixelColor = img.GetPixel(i, j)
                newImg.SetPixel(counterX, counterY, pixelColor)
            Next
            counterX += 1
        Next
        Return newImg
    End Function
End Class