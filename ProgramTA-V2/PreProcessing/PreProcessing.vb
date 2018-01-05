Imports System.IO
Imports AForge
Imports AForge.Video
Imports AForge.Video.DirectShow
Imports AForge.Imaging.Filters
Imports AForge.Imaging
Imports AForge.Math.Geometry

Module PreProcessing
    Sub StartPreprocessing(ByVal Gambar As Bitmap, idx As Integer) 'Digunakan untuk grayscale->threshold->Filtering
        Dim Hasil(3) As Bitmap
        Dim grayImage, thresholdImage As Bitmap
        'Median Filter
        Dim image As Bitmap = Gambar
        image = New Median().Apply(image)
        Hasil(0) = image

        'Grayscale
        grayImage = Grayscale.CommonAlgorithms.BT709.Apply(image)
        Hasil(1) = New Bitmap(grayImage)

        'Melakukan threshold pada image
        Dim thresholdFilter As New Threshold(120)
        thresholdFilter.ApplyInPlace(grayImage)
        thresholdImage = grayImage.Clone
        Hasil(2) = New Bitmap(grayImage)

        'Melalukan filter blob dengan ukuran 39x39
        Dim filter As New BlobsFiltering
        filter.CoupledSizeFiltering = True
        filter.MinWidth = 39
        filter.MinHeight = 39
        filter.ApplyInPlace(grayImage)
        Hasil(3) = New Bitmap(grayImage)

        SplitGambar(Hasil(3), idx)
        SimpanHasilPreProc(Hasil, idx)
        'ArrGambarPreProc.Add(Hasil)
    End Sub

    Sub SplitGambar(ByVal myImage As Bitmap, idx As Integer) 'Digunakan untuk memotong gambar menjadi 4 bagian
        Dim bit As Bitmap = New Bitmap(myImage)
        Dim hasil(3) As Bitmap 'Untuk menampuk 4 bagian gambar
        Dim g As Graphics
        'Resize Image ke (800x400)
        Dim NewImg = New Bitmap(800, 400)
        g = Graphics.FromImage(NewImg)
        g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
        g.PixelOffsetMode = Drawing2D.PixelOffsetMode.HighQuality
        g.CompositingQuality = Drawing2D.CompositingQuality.HighQuality
        g.DrawImage(bit, 0, 0, NewImg.Width, NewImg.Height)
        '====================
        Dim NewCrop = New Bitmap(400, 180)
        Dim rect As New Rectangle(0, 0, 400, 180)
        g = Graphics.FromImage(NewCrop)
        g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
        g.PixelOffsetMode = Drawing2D.PixelOffsetMode.HighQuality
        g.CompositingQuality = Drawing2D.CompositingQuality.HighQuality
        g.DrawImage(NewImg, New Rectangle(0, 0, rect.Width, rect.Height), rect, GraphicsUnit.Pixel)
        hasil(0) = NewCrop

        NewCrop = New Bitmap(400, 180)
        rect = New Rectangle(400, 0, 400, 180)
        g = Graphics.FromImage(NewCrop)
        g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
        g.PixelOffsetMode = Drawing2D.PixelOffsetMode.HighQuality
        g.CompositingQuality = Drawing2D.CompositingQuality.HighQuality
        g.DrawImage(NewImg, New Rectangle(0, 0, rect.Width, rect.Height), rect, GraphicsUnit.Pixel)
        hasil(1) = NewCrop

        NewCrop = New Bitmap(400, 220)
        rect = New Rectangle(0, 180, 800, 380)
        g = Graphics.FromImage(NewCrop)
        g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
        g.PixelOffsetMode = Drawing2D.PixelOffsetMode.HighQuality
        g.CompositingQuality = Drawing2D.CompositingQuality.HighQuality
        g.DrawImage(NewImg, New Rectangle(0, 0, rect.Width, rect.Height), rect, GraphicsUnit.Pixel)
        hasil(2) = NewCrop

        NewCrop = New Bitmap(400, 220)
        rect = New Rectangle(400, 180, 800, 380)
        g = Graphics.FromImage(NewCrop)
        g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
        g.PixelOffsetMode = Drawing2D.PixelOffsetMode.HighQuality
        g.CompositingQuality = Drawing2D.CompositingQuality.HighQuality
        g.DrawImage(NewImg, New Rectangle(0, 0, rect.Width, rect.Height), rect, GraphicsUnit.Pixel)
        hasil(3) = NewCrop
        SimpanHasilSplit(hasil, idx)
        'ArrGambarSplit.Add(hasil)
    End Sub

    Sub ClearFolder()
        Dim temp As String = ""
        Dim di As DirectoryInfo
        Dim fiArr As FileInfo()
        Dim fri As FileInfo

        temp &= MainPath & "Output\"
        di = New DirectoryInfo(temp & "Preprocessing\")
        fiArr = di.GetFiles()
        For Each fri In fiArr
            File.Delete(temp & "Preprocessing\" & fri.Name)
        Next fri

        di = New DirectoryInfo(temp & "Split\")
        fiArr = di.GetFiles()
        For Each fri In fiArr
            File.Delete(temp & "Split\" & fri.Name)
        Next fri
    End Sub

    Sub SimpanHasilPreProc(gambar As Bitmap(), i As Integer) 'Digunakan untuk menyimpan gambar hasil preprocessing sebelum di split
        Dim temp As String = MainPath & "Output\Preprocessing\"
        gambar(0).Save(temp & i & " - 1.png", System.Drawing.Imaging.ImageFormat.Png)
        gambar(1).Save(temp & i & " - 2.png", System.Drawing.Imaging.ImageFormat.Png)
        gambar(2).Save(temp & i & " - 3.png", System.Drawing.Imaging.ImageFormat.Png)
        gambar(3).Save(temp & i & " - 4.png", System.Drawing.Imaging.ImageFormat.Png)
    End Sub

    Sub SimpanHasilSplit(gambar As Bitmap(), i As Integer) 'Digunakan untuk menyimpan gambar hasil preprocessing sebelum di split
        Dim temp As String = MainPath & "Output\Split\"
        gambar(0).Save(temp & i & " - 1.png", System.Drawing.Imaging.ImageFormat.Png)
        gambar(1).Save(temp & i & " - 2.png", System.Drawing.Imaging.ImageFormat.Png)
        gambar(2).Save(temp & i & " - 3.png", System.Drawing.Imaging.ImageFormat.Png)
        gambar(3).Save(temp & i & " - 4.png", System.Drawing.Imaging.ImageFormat.Png)
    End Sub
End Module
