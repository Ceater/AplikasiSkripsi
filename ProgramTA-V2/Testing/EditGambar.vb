Imports System.Drawing.Imaging
Imports AForge
Imports AForge.Imaging
Imports AForge.Imaging.Filters

Public Class EditGambar
    Dim img As Bitmap
    Friend hsl(3) As Bitmap
    Dim edgePoint As List(Of IntPoint)
    Dim MinX As Integer = 0
    Dim MaxX As Integer = 0
    Dim MinY As Integer = 0
    Dim MaxY As Integer = 0

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Function TrimWarna(img As Bitmap) As Bitmap
        'Digunakan untuk menghapus warna putih background
        MinX = img.Width
        MaxX = 0
        MinY = img.Height
        MaxY = 0
        Dim White As Integer = Color.White.ToArgb()
        For x As Integer = 0 To img.Width - 1
            For y As Integer = 0 To img.Height - 1
                If Not img.GetPixel(x, y).ToArgb() = White Then
                    MinX = System.Math.Min(MinX, x)
                    MaxX = System.Math.Max(MaxX, x)
                    MinY = System.Math.Min(MinY, y)
                    MaxY = System.Math.Max(MaxY, y)
                End If
            Next
        Next
        Dim newImg As New Bitmap(MaxX, MaxY)
        Dim counterX, counterY As Integer
        For i = MinX To MaxX - 2
            counterY = 0
            For j = MinY To MaxY - 2
                counterY += 1
                Dim pixelColor As Color
                pixelColor = img.GetPixel(i, j)
                newImg.SetPixel(counterX, counterY, pixelColor)
            Next
            counterX += 1
        Next
        Return newImg
    End Function

    Private Function CFilter(img As Bitmap) As Bitmap
        'Digunakan untuk menjadikan warna putih menjadi hitam
        Dim newImg As New Bitmap(MaxX, MaxY)
        Dim counterX, counterY As Integer
        For i = MinX To MaxX - 2
            counterY = 0
            For j = MinY To MaxY - 2
                counterY += 1
                Dim pixelColor As Color
                If img.GetPixel(i, j).R >= 230 And img.GetPixel(i, j).G >= 230 And img.GetPixel(i, j).B >= 230 Then
                    pixelColor = Color.Black
                Else
                    pixelColor = img.GetPixel(i, j)
                End If
                newImg.SetPixel(counterX, counterY, pixelColor)
            Next
            counterX += 1
        Next

        Dim filter As New BlobsFiltering
        filter.CoupledSizeFiltering = True
        filter.MinWidth = 20
        filter.MinHeight = 20
        filter.ApplyInPlace(newImg)

        Return newImg
    End Function

    Private Function RotateGambar(img As Bitmap, img2 As Bitmap) As Bitmap
        'Digunakan untuk mencari pola kotak
        Dim qf As New QuadrilateralFinder
        Dim corners As List(Of IntPoint) = qf.ProcessImage(img)
        Dim quadrilateralTransformation As New QuadrilateralTransformation(corners, 500, 500)
        Dim transformed As Bitmap = quadrilateralTransformation.Apply(img2)
        Dim newImg As Bitmap = RotateImage(transformed, -90)
        'Dim newImg As Bitmap = transformed
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

    Private Sub EditGambar_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        img = GUtama.Image
        hsl(0) = TrimWarna(img)
        hsl(1) = CFilter(img)
        hsl(1) = QuadFinder(hsl(1))
        hsl(2) = RotateGambar(hsl(1), hsl(0))
        PictureBox1.Image = hsl(0)
        PictureBox2.Image = hsl(1)
        PictureBox3.Image = hsl(2)
    End Sub

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
End Class