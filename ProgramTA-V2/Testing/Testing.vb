Imports System.IO
Imports AForge.Imaging.Filters

Module Testing
    Dim nn As NeuralNetwork
    Dim outputAngka As Double()
    Dim pixelImage As Double()
    Dim weight1(30966) As Double 'Jumlah line dinotepad
    Dim weight2(30966) As Double 'Jumlah line dinotepad
    Dim weight3(30966) As Double 'Jumlah line dinotepad
    Dim weight4(30966) As Double 'Jumlah line dinotepad
    Function TestingStartPreprocessing(ByVal Gambar As Bitmap) As Bitmap()
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
        filter.MinWidth = 35
        filter.MinHeight = 35
        filter.ApplyInPlace(grayImage)
        Hasil(3) = New Bitmap(grayImage)
        Return Hasil
    End Function

    Function TestingSplitGambar(ByVal myImage As Bitmap) As Bitmap() 'Digunakan untuk memotong gambar menjadi 4 bagian
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
        Return hasil
    End Function

    Function getBobot() As Double()()
        Dim hasil(3)() As Double
        Try
            Dim idxBobot As Integer = 0
            Dim sr As StreamReader = New StreamReader(W1)
            Dim line As String = ""
            Do
                line = sr.ReadLine
                If Not line Is Nothing Then
                    weight1(idxBobot) = line
                    idxBobot = idxBobot + 1
                End If
            Loop Until line Is Nothing

            idxBobot = 0
            sr = New StreamReader(W2)
            line = ""
            Do
                line = sr.ReadLine
                If Not line Is Nothing Then
                    weight2(idxBobot) = line
                    idxBobot = idxBobot + 1
                End If
            Loop Until line Is Nothing

            idxBobot = 0
            sr = New StreamReader(W3)
            line = ""
            Do
                line = sr.ReadLine
                If Not line Is Nothing Then
                    weight3(idxBobot) = line
                    idxBobot = idxBobot + 1
                End If
            Loop Until line Is Nothing

            idxBobot = 0
            sr = New StreamReader(W4)
            line = ""
            Do
                line = sr.ReadLine
                If Not line Is Nothing Then
                    weight4(idxBobot) = line
                    idxBobot = idxBobot + 1
                End If
            Loop Until line Is Nothing
            hasil(0) = weight1
            hasil(1) = weight2
            hasil(2) = weight3
            hasil(3) = weight4
        Catch ex As Exception
            MsgBox(ex.ToString)
            MsgBox("File Access Error")
        End Try
        Return hasil
    End Function

    Function StartTesting(Gambar() As Bitmap)
        pixelImage = New Double(1023) {}
        outputAngka = New Double() {}
        'Dim hasil As Double()()
        'hasil = New Double(3)() {}
        Dim hasil As Double()()
        hasil = New Double(3)() {}
        Const numInput As Integer = 1024
        Const numHidden As Integer = 30
        Const numOutput As Integer = 7
        nn = New NeuralNetwork(numInput, numHidden, numOutput)

        '======Gambar 1======
        ToBinary(Gambar(0))
        nn.SetWeights(weight1)
        For i As Integer = 0 To pixelImage.Length - 1
            outputAngka = nn.ComputeOutputs(pixelImage)
        Next
        hasil(0) = getOutputAngka("Atas Kiri")
        '======Gambar 2======
        ToBinary(Gambar(1))
        nn.SetWeights(weight2)
        For i As Integer = 0 To pixelImage.Length - 1
            outputAngka = nn.ComputeOutputs(pixelImage)
        Next
        hasil(1) = getOutputAngka("Atas Kanan")
        '======Gambar 3======
        ToBinary(Gambar(2))
        nn.SetWeights(weight3)
        For i As Integer = 0 To pixelImage.Length - 1
            outputAngka = nn.ComputeOutputs(pixelImage)
        Next
        hasil(2) = getOutputAngka("Bawah Kiri")
        '======Gambar 4======
        ToBinary(Gambar(3))
        nn.SetWeights(weight4)
        For i As Integer = 0 To pixelImage.Length - 1
            outputAngka = nn.ComputeOutputs(pixelImage)
        Next
        hasil(3) = getOutputAngka("Bawah Kanan")
        Return hasil
    End Function

    Sub ToBinary(ByVal gambar As Bitmap)
        gambar = resizeImage(gambar)
        Dim counter As Integer = 0
        For i = 0 To 31
            For j = 0 To 31
                Dim x As Integer = CLng((gambar.GetPixel(j, i).R) + CLng(gambar.GetPixel(j, i).G) + CLng(gambar.GetPixel(j, i).B)) / 3
                If x <= 180 Then
                    pixelImage(counter) = 0
                Else
                    pixelImage(counter) = 1
                End If
                counter += 1
            Next
        Next
    End Sub

    Private Function getOutputAngka(loc As String) As Double()
        'Mengekstrak output testing menjadi angka
        Dim output1 As Double = -1
        Dim output2 As Double = -1
        Dim output3 As Double = -1
        Dim output4 As Double = -1
        Dim output5 As Double = -1
        Dim output6 As Double = -1
        Dim output7 As Double = -1
        Dim temp() As Double = outputAngka.Clone
        Dim hasil(6) As Double
        Dim x, x1, x2, x3, x4, x5, x6, x7 As Integer
        Array.Sort(temp)
        Array.Reverse(temp)
        For i As Integer = 0 To 6
            If temp(0) = outputAngka(i) Then
                output1 = outputAngka(i)
                x = i
            ElseIf temp(1) = outputAngka(i) Then
                output2 = outputAngka(i)
                x1 = i
            ElseIf temp(2) = outputAngka(i) Then
                output3 = outputAngka(i)
                x2 = i
            ElseIf temp(3) = outputAngka(i) Then
                output4 = outputAngka(i)
                x3 = i
            ElseIf temp(4) = outputAngka(i) Then
                output5 = outputAngka(i)
                x4 = i
            ElseIf temp(5) = outputAngka(i) Then
                output6 = outputAngka(i)
                x5 = i
            ElseIf temp(6) = outputAngka(i) Then
                output7 = outputAngka(i)
                x6 = i
            End If
        Next

        Dim MU1 = "", MU2 As String = "", MU3 As String = "", MU4 As String = ""
        Dim MU5 = "", MU6 As String = "", MU7 As String = ""
        MU1 = getOutputAngka_F1(x)
        MU2 = getOutputAngka_F1(x1)
        MU3 = getOutputAngka_F1(x2)
        MU4 = getOutputAngka_F1(x3)
        MU5 = getOutputAngka_F1(x4)
        MU6 = getOutputAngka_F1(x5)
        MU7 = getOutputAngka_F1(x6)

        TestingForm.MyInstance.Progress &= loc & vbNewLine
        TestingForm.MyInstance.Progress &= "Mata Uang " & MU1 & " Adalah " & Math.Round(output1, 3) * 100 & " % " & vbNewLine
        TestingForm.MyInstance.Progress &= "Mata Uang " & MU2 & " Adalah " & Math.Round(output2, 3) * 100 & " % " & vbNewLine
        TestingForm.MyInstance.Progress &= "Mata Uang " & MU3 & " Adalah " & Math.Round(output3, 3) * 100 & " % " & vbNewLine
        TestingForm.MyInstance.Progress &= "Mata Uang " & MU4 & " Adalah " & Math.Round(output4, 3) * 100 & " % " & vbNewLine
        TestingForm.MyInstance.Progress &= "Mata Uang " & MU5 & " Adalah " & Math.Round(output5, 3) * 100 & " % " & vbNewLine
        TestingForm.MyInstance.Progress &= "Mata Uang " & MU6 & " Adalah " & Math.Round(output6, 3) * 100 & " % " & vbNewLine
        TestingForm.MyInstance.Progress &= "Mata Uang " & MU7 & " Adalah " & Math.Round(output7, 3) * 100 & " % " & vbNewLine
        TestingForm.BwTest.ReportProgress(0, TestingForm.MyInstance.Progress)
        hasil(0) = x
        hasil(1) = x1
        hasil(2) = x2
        hasil(3) = x3
        hasil(4) = x4
        hasil(5) = x5
        hasil(6) = x6
        Return hasil
    End Function

    Sub HitungPersentasi(hasil As Double()())
        Dim o1 As Double
        o1 = (hasil(0)(0) + hasil(1)(0) + hasil(2)(0) + hasil(3)(0)) / 4
        Dim o2 As Double
        o2 = (hasil(0)(1) + hasil(1)(1) + hasil(2)(1) + hasil(3)(1)) / 4
        Debug.Print("Output 1: " & o1 & " Output2: " & o2)
    End Sub

    Sub HasilAkhir(hsl()() As Double)
        Dim total(6) As Integer
        For i = 0 To 3
            total = HasilAkhir_F1(hsl, i, 0, total)
            total = HasilAkhir_F1(hsl, i, 1, total)
        Next
        Dim high As Integer = 0
        For i = 0 To 6
            If total(high) < total(i) Then
                high = i
            End If
        Next
        If high = 0 Then
            TestingForm.MyInstance.Progress &= "Foto ini adalah mata uang Rp. 1.000" & vbNewLine
        ElseIf high = 1 Then
            TestingForm.MyInstance.Progress &= "Foto ini adalah mata uang Rp. 2.000" & vbNewLine
        ElseIf high = 2 Then
            TestingForm.MyInstance.Progress &= "Foto ini adalah mata uang Rp. 5.000" & vbNewLine
        ElseIf high = 3 Then
            TestingForm.MyInstance.Progress &= "Foto ini adalah mata uang Rp. 10.000" & vbNewLine
        ElseIf high = 4 Then
            TestingForm.MyInstance.Progress &= "Foto ini adalah mata uang Rp. 20.000" & vbNewLine
        ElseIf high = 5 Then
            TestingForm.MyInstance.Progress &= "Foto ini adalah mata uang Rp. 50.000" & vbNewLine
        ElseIf high = 6 Then
            TestingForm.MyInstance.Progress &= "Foto ini adalah mata uang Rp. 100.000" & vbNewLine
        End If
        TestingForm.MyInstance.BwTest.ReportProgress(0, TestingForm.MyInstance.Progress)
        PlaySound(high + 1)
    End Sub

    Function getOutputAngka_F1(x As Integer)
        Dim result As String = ""
        If x = 0 Then
            result = "Rp. 1.000"
        ElseIf x = 1 Then
            result = "Rp. 2.000"
        ElseIf x = 2 Then
            result = "Rp. 5.000"
        ElseIf x = 3 Then
            result = "Rp. 10.000"
        ElseIf x = 4 Then
            result = "Rp. 20.000"
        ElseIf x = 5 Then
            result = "Rp. 50.000"
        ElseIf x = 6 Then
            result = "Rp. 100.000"
        End If
        Return result
    End Function

    Function HasilAkhir_F1(hasil()() As Double, i As Integer, j As Integer, totalx() As Integer)
        Dim total() As Integer = totalx
        If hasil(i)(j) = 0 Then
            total(0) += 1
        ElseIf hasil(i)(j) = 1 Then
            total(1) += 1
        ElseIf hasil(i)(j) = 2 Then
            total(2) += 1
        ElseIf hasil(i)(j) = 3 Then
            total(3) += 1
        ElseIf hasil(i)(j) = 4 Then
            total(4) += 1
        ElseIf hasil(i)(j) = 5 Then
            total(5) += 1
        ElseIf hasil(i)(j) = 6 Then
            total(6) += 1
        End If
        Return total
    End Function

    Sub PlaySound(x As Integer)
        If x = 1 Then
            My.Computer.Audio.Play(MainPath & "Sound\1000.wav", AudioPlayMode.WaitToComplete)
        ElseIf x = 2 Then
            My.Computer.Audio.Play(MainPath & "Sound\2000.wav", AudioPlayMode.WaitToComplete)
        ElseIf x = 3 Then
            My.Computer.Audio.Play(MainPath & "Sound\5000.wav", AudioPlayMode.WaitToComplete)
        ElseIf x = 4 Then
            My.Computer.Audio.Play(MainPath & "Sound\10000.wav", AudioPlayMode.WaitToComplete)
        ElseIf x = 5 Then
            My.Computer.Audio.Play(MainPath & "Sound\20000.wav", AudioPlayMode.WaitToComplete)
        ElseIf x = 6 Then
            My.Computer.Audio.Play(MainPath & "Sound\50000.wav", AudioPlayMode.WaitToComplete)
        ElseIf x = 7 Then
            My.Computer.Audio.Play(MainPath & "Sound\100000.wav", AudioPlayMode.WaitToComplete)
        End If
    End Sub
End Module