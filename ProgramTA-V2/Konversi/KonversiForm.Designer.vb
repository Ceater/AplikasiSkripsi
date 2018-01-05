<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class KonversiForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Preview_4 = New System.Windows.Forms.PictureBox()
        Me.Preview_2 = New System.Windows.Forms.PictureBox()
        Me.Preview_3 = New System.Windows.Forms.PictureBox()
        Me.Preview_1 = New System.Windows.Forms.PictureBox()
        Me.MaxLabel = New System.Windows.Forms.Label()
        Me.PreviousImg = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.First = New System.Windows.Forms.Button()
        Me.CurrLabel = New System.Windows.Forms.Label()
        Me.NextImg = New System.Windows.Forms.Button()
        Me.Last = New System.Windows.Forms.Button()
        Me.ProgressText = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Panel1.SuspendLayout()
        CType(Me.Preview_4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Preview_2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Preview_3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Preview_1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Preview_4)
        Me.Panel1.Controls.Add(Me.Preview_2)
        Me.Panel1.Controls.Add(Me.Preview_3)
        Me.Panel1.Controls.Add(Me.Preview_1)
        Me.Panel1.Controls.Add(Me.MaxLabel)
        Me.Panel1.Controls.Add(Me.PreviousImg)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.First)
        Me.Panel1.Controls.Add(Me.CurrLabel)
        Me.Panel1.Controls.Add(Me.NextImg)
        Me.Panel1.Controls.Add(Me.Last)
        Me.Panel1.Location = New System.Drawing.Point(12, 12)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(505, 293)
        Me.Panel1.TabIndex = 7
        '
        'Preview_4
        '
        Me.Preview_4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.Preview_4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Preview_4.Location = New System.Drawing.Point(252, 141)
        Me.Preview_4.Name = "Preview_4"
        Me.Preview_4.Size = New System.Drawing.Size(250, 113)
        Me.Preview_4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.Preview_4.TabIndex = 3
        Me.Preview_4.TabStop = False
        '
        'Preview_2
        '
        Me.Preview_2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.Preview_2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Preview_2.Location = New System.Drawing.Point(252, 27)
        Me.Preview_2.Name = "Preview_2"
        Me.Preview_2.Size = New System.Drawing.Size(250, 113)
        Me.Preview_2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.Preview_2.TabIndex = 3
        Me.Preview_2.TabStop = False
        '
        'Preview_3
        '
        Me.Preview_3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.Preview_3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Preview_3.Location = New System.Drawing.Point(3, 141)
        Me.Preview_3.Name = "Preview_3"
        Me.Preview_3.Size = New System.Drawing.Size(250, 113)
        Me.Preview_3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.Preview_3.TabIndex = 3
        Me.Preview_3.TabStop = False
        '
        'Preview_1
        '
        Me.Preview_1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.Preview_1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Preview_1.Location = New System.Drawing.Point(3, 27)
        Me.Preview_1.Name = "Preview_1"
        Me.Preview_1.Size = New System.Drawing.Size(250, 113)
        Me.Preview_1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.Preview_1.TabIndex = 3
        Me.Preview_1.TabStop = False
        '
        'MaxLabel
        '
        Me.MaxLabel.AutoSize = True
        Me.MaxLabel.Location = New System.Drawing.Point(259, 265)
        Me.MaxLabel.Name = "MaxLabel"
        Me.MaxLabel.Size = New System.Drawing.Size(13, 13)
        Me.MaxLabel.TabIndex = 5
        Me.MaxLabel.Text = "0"
        '
        'PreviousImg
        '
        Me.PreviousImg.Location = New System.Drawing.Point(166, 260)
        Me.PreviousImg.Name = "PreviousImg"
        Me.PreviousImg.Size = New System.Drawing.Size(40, 23)
        Me.PreviousImg.TabIndex = 4
        Me.PreviousImg.Text = "<"
        Me.PreviousImg.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(241, 265)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(12, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "/"
        '
        'First
        '
        Me.First.Location = New System.Drawing.Point(120, 260)
        Me.First.Name = "First"
        Me.First.Size = New System.Drawing.Size(40, 23)
        Me.First.TabIndex = 4
        Me.First.Text = "<<"
        Me.First.UseVisualStyleBackColor = True
        '
        'CurrLabel
        '
        Me.CurrLabel.AutoSize = True
        Me.CurrLabel.Location = New System.Drawing.Point(217, 265)
        Me.CurrLabel.Name = "CurrLabel"
        Me.CurrLabel.Size = New System.Drawing.Size(13, 13)
        Me.CurrLabel.TabIndex = 5
        Me.CurrLabel.Text = "0"
        '
        'NextImg
        '
        Me.NextImg.Location = New System.Drawing.Point(288, 260)
        Me.NextImg.Name = "NextImg"
        Me.NextImg.Size = New System.Drawing.Size(40, 23)
        Me.NextImg.TabIndex = 4
        Me.NextImg.Text = ">"
        Me.NextImg.UseVisualStyleBackColor = True
        '
        'Last
        '
        Me.Last.Location = New System.Drawing.Point(334, 260)
        Me.Last.Name = "Last"
        Me.Last.Size = New System.Drawing.Size(40, 23)
        Me.Last.TabIndex = 4
        Me.Last.Text = ">>"
        Me.Last.UseVisualStyleBackColor = True
        '
        'ProgressText
        '
        Me.ProgressText.Location = New System.Drawing.Point(12, 311)
        Me.ProgressText.Multiline = True
        Me.ProgressText.Name = "ProgressText"
        Me.ProgressText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.ProgressText.Size = New System.Drawing.Size(177, 74)
        Me.ProgressText.TabIndex = 9
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(398, 311)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(119, 74)
        Me.Button1.TabIndex = 8
        Me.Button1.Text = "Start"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'KonversiForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(528, 395)
        Me.Controls.Add(Me.ProgressText)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "KonversiForm"
        Me.Text = "Konversi"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.Preview_4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Preview_2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Preview_3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Preview_1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents Preview_1 As PictureBox
    Friend WithEvents MaxLabel As Label
    Friend WithEvents PreviousImg As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents First As Button
    Friend WithEvents CurrLabel As Label
    Friend WithEvents NextImg As Button
    Friend WithEvents Last As Button
    Friend WithEvents Preview_4 As PictureBox
    Friend WithEvents Preview_2 As PictureBox
    Friend WithEvents Preview_3 As PictureBox
    Friend WithEvents ProgressText As TextBox
    Friend WithEvents Button1 As Button
End Class
