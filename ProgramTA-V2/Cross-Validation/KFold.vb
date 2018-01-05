Public Class KFold
    Private rnd As New Random()
    Dim KumpulanData As Double()()
    Dim fold As Double()()()

    Public Sub New(ByVal data As Double()(), ByVal jmlfold As Integer, ByVal isifold As Integer) 'Membuat data untuk kfold
        KumpulanData = Randomize(data) 'Data sudah dirandom
        fold = makeFold(KumpulanData, jmlfold, isifold) 'Data sudah dimasukkan kedalam beberapa fold
    End Sub

    Private Function Randomize(ByVal data As Double()()) 'Mengacak semua data yang dimasukkan
        Dim j As Int32
        Dim temp As Double()

        For n As Int32 = data.Length - 1 To 0 Step -1
            j = rnd.Next(0, n + 1)
            ' Swap them.
            temp = data(n)
            data(n) = data(j)
            data(j) = temp
        Next n
        Return data
    End Function

    Private Function makeFold(ByVal data As Double()(), ByVal jmlfold As Integer, ByVal isifold As Integer) 'Digunakan untuk membuat fold sebanyak k fold dengan masing-masing isi yang ditentukan
        Dim fold As Double()()() = New Double(jmlfold - 1)()() {}

        For i = 0 To jmlfold - 1
            Dim counter As Integer = 0
            fold(i) = New Double(isifold - 1)() {}
            For j = i * isifold To ((i + 1) * isifold) - 1
                fold(i)(counter) = data(j)
                counter += 1
            Next
        Next
        Return fold
    End Function

    Public Function getFold()
        Return fold
    End Function
End Class