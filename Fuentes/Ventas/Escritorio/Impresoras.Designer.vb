<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Impresoras
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
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

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Impresoras))
        Me.pnlConfiguracion = New System.Windows.Forms.Panel()
        Me.gbEtiquetasCajas = New System.Windows.Forms.GroupBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtMargenDerechoVales = New System.Windows.Forms.TextBox()
        Me.chkImprimirVales = New System.Windows.Forms.CheckBox()
        Me.cbImpresorasVales = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtMargenIzquierdoVales = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.gbEtiquetasTarimas = New System.Windows.Forms.GroupBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtMargenDerechoRecibos = New System.Windows.Forms.TextBox()
        Me.cbImpresorasRecibos = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.chkImprimirRecibos = New System.Windows.Forms.CheckBox()
        Me.txtMargenIzquierdoRecibos = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btnGuardar = New System.Windows.Forms.Button()
        Me.pnlPie = New System.Windows.Forms.Panel()
        Me.btnAyuda = New System.Windows.Forms.Button()
        Me.lblDescripcionTooltip = New System.Windows.Forms.Label()
        Me.btnSalir = New System.Windows.Forms.Button()
        Me.pnlConfiguracion.SuspendLayout()
        Me.gbEtiquetasCajas.SuspendLayout()
        Me.gbEtiquetasTarimas.SuspendLayout()
        Me.pnlPie.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlConfiguracion
        '
        Me.pnlConfiguracion.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlConfiguracion.BackColor = System.Drawing.Color.White
        Me.pnlConfiguracion.Controls.Add(Me.gbEtiquetasCajas)
        Me.pnlConfiguracion.Controls.Add(Me.gbEtiquetasTarimas)
        Me.pnlConfiguracion.Location = New System.Drawing.Point(3, 5)
        Me.pnlConfiguracion.Name = "pnlConfiguracion"
        Me.pnlConfiguracion.Size = New System.Drawing.Size(586, 341)
        Me.pnlConfiguracion.TabIndex = 24
        '
        'gbEtiquetasCajas
        '
        Me.gbEtiquetasCajas.BackColor = System.Drawing.Color.Transparent
        Me.gbEtiquetasCajas.Controls.Add(Me.Label2)
        Me.gbEtiquetasCajas.Controls.Add(Me.txtMargenDerechoVales)
        Me.gbEtiquetasCajas.Controls.Add(Me.chkImprimirVales)
        Me.gbEtiquetasCajas.Controls.Add(Me.cbImpresorasVales)
        Me.gbEtiquetasCajas.Controls.Add(Me.Label3)
        Me.gbEtiquetasCajas.Controls.Add(Me.txtMargenIzquierdoVales)
        Me.gbEtiquetasCajas.Controls.Add(Me.Label5)
        Me.gbEtiquetasCajas.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbEtiquetasCajas.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.gbEtiquetasCajas.Location = New System.Drawing.Point(3, 180)
        Me.gbEtiquetasCajas.Name = "gbEtiquetasCajas"
        Me.gbEtiquetasCajas.Size = New System.Drawing.Size(579, 150)
        Me.gbEtiquetasCajas.TabIndex = 38
        Me.gbEtiquetasCajas.TabStop = False
        Me.gbEtiquetasCajas.Text = "RECIBOS DE VALES"
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(5, 48)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(381, 20)
        Me.Label2.TabIndex = 28
        Me.Label2.Text = "IMPRESORA *"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtMargenDerechoVales
        '
        Me.txtMargenDerechoVales.BackColor = System.Drawing.Color.White
        Me.txtMargenDerechoVales.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMargenDerechoVales.ForeColor = System.Drawing.Color.Black
        Me.txtMargenDerechoVales.Location = New System.Drawing.Point(514, 69)
        Me.txtMargenDerechoVales.MaxLength = 2
        Me.txtMargenDerechoVales.Name = "txtMargenDerechoVales"
        Me.txtMargenDerechoVales.Size = New System.Drawing.Size(30, 20)
        Me.txtMargenDerechoVales.TabIndex = 35
        Me.txtMargenDerechoVales.Text = "0"
        Me.txtMargenDerechoVales.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'chkImprimirVales
        '
        Me.chkImprimirVales.AutoSize = True
        Me.chkImprimirVales.Cursor = System.Windows.Forms.Cursors.Hand
        Me.chkImprimirVales.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkImprimirVales.ForeColor = System.Drawing.Color.Black
        Me.chkImprimirVales.Location = New System.Drawing.Point(5, 103)
        Me.chkImprimirVales.Name = "chkImprimirVales"
        Me.chkImprimirVales.Size = New System.Drawing.Size(93, 17)
        Me.chkImprimirVales.TabIndex = 30
        Me.chkImprimirVales.Text = "IMPRIMIR *"
        Me.chkImprimirVales.UseVisualStyleBackColor = True
        '
        'cbImpresorasVales
        '
        Me.cbImpresorasVales.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cbImpresorasVales.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cbImpresorasVales.BackColor = System.Drawing.Color.White
        Me.cbImpresorasVales.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cbImpresorasVales.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbImpresorasVales.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbImpresorasVales.ForeColor = System.Drawing.Color.Black
        Me.cbImpresorasVales.FormattingEnabled = True
        Me.cbImpresorasVales.Location = New System.Drawing.Point(5, 69)
        Me.cbImpresorasVales.Name = "cbImpresorasVales"
        Me.cbImpresorasVales.Size = New System.Drawing.Size(381, 21)
        Me.cbImpresorasVales.TabIndex = 29
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(487, 48)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(80, 13)
        Me.Label3.TabIndex = 34
        Me.Label3.Text = "SUPERIOR *"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtMargenIzquierdoVales
        '
        Me.txtMargenIzquierdoVales.BackColor = System.Drawing.Color.White
        Me.txtMargenIzquierdoVales.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMargenIzquierdoVales.ForeColor = System.Drawing.Color.Black
        Me.txtMargenIzquierdoVales.Location = New System.Drawing.Point(420, 69)
        Me.txtMargenIzquierdoVales.MaxLength = 2
        Me.txtMargenIzquierdoVales.Name = "txtMargenIzquierdoVales"
        Me.txtMargenIzquierdoVales.Size = New System.Drawing.Size(30, 20)
        Me.txtMargenIzquierdoVales.TabIndex = 33
        Me.txtMargenIzquierdoVales.Text = "0"
        Me.txtMargenIzquierdoVales.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(391, 48)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(85, 13)
        Me.Label5.TabIndex = 32
        Me.Label5.Text = "IZQUIERDO *"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'gbEtiquetasTarimas
        '
        Me.gbEtiquetasTarimas.BackColor = System.Drawing.Color.Transparent
        Me.gbEtiquetasTarimas.Controls.Add(Me.Label8)
        Me.gbEtiquetasTarimas.Controls.Add(Me.txtMargenDerechoRecibos)
        Me.gbEtiquetasTarimas.Controls.Add(Me.cbImpresorasRecibos)
        Me.gbEtiquetasTarimas.Controls.Add(Me.Label1)
        Me.gbEtiquetasTarimas.Controls.Add(Me.chkImprimirRecibos)
        Me.gbEtiquetasTarimas.Controls.Add(Me.txtMargenIzquierdoRecibos)
        Me.gbEtiquetasTarimas.Controls.Add(Me.Label4)
        Me.gbEtiquetasTarimas.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbEtiquetasTarimas.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.gbEtiquetasTarimas.Location = New System.Drawing.Point(3, 9)
        Me.gbEtiquetasTarimas.Name = "gbEtiquetasTarimas"
        Me.gbEtiquetasTarimas.Size = New System.Drawing.Size(579, 150)
        Me.gbEtiquetasTarimas.TabIndex = 37
        Me.gbEtiquetasTarimas.TabStop = False
        Me.gbEtiquetasTarimas.Text = "RECIBOS DE VENTAS"
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Black
        Me.Label8.Location = New System.Drawing.Point(5, 44)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(381, 20)
        Me.Label8.TabIndex = 28
        Me.Label8.Text = "IMPRESORA *"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtMargenDerechoRecibos
        '
        Me.txtMargenDerechoRecibos.BackColor = System.Drawing.Color.White
        Me.txtMargenDerechoRecibos.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMargenDerechoRecibos.ForeColor = System.Drawing.Color.Black
        Me.txtMargenDerechoRecibos.Location = New System.Drawing.Point(514, 65)
        Me.txtMargenDerechoRecibos.MaxLength = 2
        Me.txtMargenDerechoRecibos.Name = "txtMargenDerechoRecibos"
        Me.txtMargenDerechoRecibos.Size = New System.Drawing.Size(30, 20)
        Me.txtMargenDerechoRecibos.TabIndex = 35
        Me.txtMargenDerechoRecibos.Text = "0"
        Me.txtMargenDerechoRecibos.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'cbImpresorasRecibos
        '
        Me.cbImpresorasRecibos.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cbImpresorasRecibos.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cbImpresorasRecibos.BackColor = System.Drawing.Color.White
        Me.cbImpresorasRecibos.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cbImpresorasRecibos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbImpresorasRecibos.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbImpresorasRecibos.ForeColor = System.Drawing.Color.Black
        Me.cbImpresorasRecibos.FormattingEnabled = True
        Me.cbImpresorasRecibos.Location = New System.Drawing.Point(5, 65)
        Me.cbImpresorasRecibos.Name = "cbImpresorasRecibos"
        Me.cbImpresorasRecibos.Size = New System.Drawing.Size(381, 21)
        Me.cbImpresorasRecibos.TabIndex = 29
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(487, 44)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(80, 13)
        Me.Label1.TabIndex = 34
        Me.Label1.Text = "SUPERIOR *"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'chkImprimirRecibos
        '
        Me.chkImprimirRecibos.AutoSize = True
        Me.chkImprimirRecibos.Cursor = System.Windows.Forms.Cursors.Hand
        Me.chkImprimirRecibos.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkImprimirRecibos.ForeColor = System.Drawing.Color.Black
        Me.chkImprimirRecibos.Location = New System.Drawing.Point(8, 95)
        Me.chkImprimirRecibos.Name = "chkImprimirRecibos"
        Me.chkImprimirRecibos.Size = New System.Drawing.Size(93, 17)
        Me.chkImprimirRecibos.TabIndex = 31
        Me.chkImprimirRecibos.Text = "IMPRIMIR *"
        Me.chkImprimirRecibos.UseVisualStyleBackColor = True
        '
        'txtMargenIzquierdoRecibos
        '
        Me.txtMargenIzquierdoRecibos.BackColor = System.Drawing.Color.White
        Me.txtMargenIzquierdoRecibos.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMargenIzquierdoRecibos.ForeColor = System.Drawing.Color.Black
        Me.txtMargenIzquierdoRecibos.Location = New System.Drawing.Point(420, 65)
        Me.txtMargenIzquierdoRecibos.MaxLength = 2
        Me.txtMargenIzquierdoRecibos.Name = "txtMargenIzquierdoRecibos"
        Me.txtMargenIzquierdoRecibos.Size = New System.Drawing.Size(30, 20)
        Me.txtMargenIzquierdoRecibos.TabIndex = 33
        Me.txtMargenIzquierdoRecibos.Text = "0"
        Me.txtMargenIzquierdoRecibos.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(391, 44)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(85, 13)
        Me.Label4.TabIndex = 32
        Me.Label4.Text = "IZQUIERDO *"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnGuardar
        '
        Me.btnGuardar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnGuardar.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btnGuardar.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnGuardar.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.btnGuardar.FlatAppearance.BorderSize = 3
        Me.btnGuardar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.MediumAquamarine
        Me.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnGuardar.ForeColor = System.Drawing.Color.Black
        Me.btnGuardar.Image = CType(resources.GetObject("btnGuardar.Image"), System.Drawing.Image)
        Me.btnGuardar.Location = New System.Drawing.Point(468, 0)
        Me.btnGuardar.Margin = New System.Windows.Forms.Padding(0)
        Me.btnGuardar.Name = "btnGuardar"
        Me.btnGuardar.Size = New System.Drawing.Size(60, 60)
        Me.btnGuardar.TabIndex = 18
        Me.btnGuardar.UseVisualStyleBackColor = False
        '
        'pnlPie
        '
        Me.pnlPie.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlPie.BackColor = System.Drawing.Color.White
        Me.pnlPie.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlPie.Controls.Add(Me.btnGuardar)
        Me.pnlPie.Controls.Add(Me.btnAyuda)
        Me.pnlPie.Controls.Add(Me.lblDescripcionTooltip)
        Me.pnlPie.Controls.Add(Me.btnSalir)
        Me.pnlPie.ForeColor = System.Drawing.Color.Black
        Me.pnlPie.Location = New System.Drawing.Point(0, 348)
        Me.pnlPie.Name = "pnlPie"
        Me.pnlPie.Size = New System.Drawing.Size(593, 60)
        Me.pnlPie.TabIndex = 25
        '
        'btnAyuda
        '
        Me.btnAyuda.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnAyuda.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnAyuda.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.btnAyuda.FlatAppearance.BorderSize = 3
        Me.btnAyuda.FlatAppearance.MouseOverBackColor = System.Drawing.Color.MediumAquamarine
        Me.btnAyuda.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAyuda.ForeColor = System.Drawing.Color.Black
        Me.btnAyuda.Image = CType(resources.GetObject("btnAyuda.Image"), System.Drawing.Image)
        Me.btnAyuda.Location = New System.Drawing.Point(0, 0)
        Me.btnAyuda.Margin = New System.Windows.Forms.Padding(0)
        Me.btnAyuda.Name = "btnAyuda"
        Me.btnAyuda.Size = New System.Drawing.Size(60, 60)
        Me.btnAyuda.TabIndex = 5
        Me.btnAyuda.UseVisualStyleBackColor = False
        Me.btnAyuda.Visible = False
        '
        'lblDescripcionTooltip
        '
        Me.lblDescripcionTooltip.AutoSize = True
        Me.lblDescripcionTooltip.BackColor = System.Drawing.Color.FromArgb(CType(CType(99, Byte), Integer), CType(CType(160, Byte), Integer), CType(CType(162, Byte), Integer))
        Me.lblDescripcionTooltip.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDescripcionTooltip.ForeColor = System.Drawing.Color.Black
        Me.lblDescripcionTooltip.Location = New System.Drawing.Point(100, 17)
        Me.lblDescripcionTooltip.Name = "lblDescripcionTooltip"
        Me.lblDescripcionTooltip.Size = New System.Drawing.Size(0, 31)
        Me.lblDescripcionTooltip.TabIndex = 4
        '
        'btnSalir
        '
        Me.btnSalir.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSalir.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnSalir.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnSalir.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.btnSalir.FlatAppearance.BorderSize = 3
        Me.btnSalir.FlatAppearance.MouseOverBackColor = System.Drawing.Color.MediumAquamarine
        Me.btnSalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSalir.ForeColor = System.Drawing.Color.Black
        Me.btnSalir.Image = CType(resources.GetObject("btnSalir.Image"), System.Drawing.Image)
        Me.btnSalir.Location = New System.Drawing.Point(531, 0)
        Me.btnSalir.Margin = New System.Windows.Forms.Padding(0)
        Me.btnSalir.Name = "btnSalir"
        Me.btnSalir.Size = New System.Drawing.Size(60, 60)
        Me.btnSalir.TabIndex = 2
        Me.btnSalir.UseVisualStyleBackColor = False
        '
        'Impresoras
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(592, 408)
        Me.Controls.Add(Me.pnlPie)
        Me.Controls.Add(Me.pnlConfiguracion)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "Impresoras"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Configurar Impresoras"
        Me.pnlConfiguracion.ResumeLayout(False)
        Me.gbEtiquetasCajas.ResumeLayout(False)
        Me.gbEtiquetasCajas.PerformLayout()
        Me.gbEtiquetasTarimas.ResumeLayout(False)
        Me.gbEtiquetasTarimas.PerformLayout()
        Me.pnlPie.ResumeLayout(False)
        Me.pnlPie.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlConfiguracion As System.Windows.Forms.Panel
    Friend WithEvents txtMargenIzquierdoRecibos As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents chkImprimirRecibos As System.Windows.Forms.CheckBox
    Friend WithEvents cbImpresorasRecibos As System.Windows.Forms.ComboBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtMargenDerechoRecibos As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents gbEtiquetasTarimas As System.Windows.Forms.GroupBox
    Private WithEvents pnlPie As System.Windows.Forms.Panel
    Private WithEvents btnAyuda As System.Windows.Forms.Button
    Friend WithEvents lblDescripcionTooltip As System.Windows.Forms.Label
    Private WithEvents btnSalir As System.Windows.Forms.Button
    Private WithEvents btnGuardar As System.Windows.Forms.Button
    Friend WithEvents gbEtiquetasCajas As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtMargenDerechoVales As System.Windows.Forms.TextBox
    Friend WithEvents chkImprimirVales As System.Windows.Forms.CheckBox
    Friend WithEvents cbImpresorasVales As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtMargenIzquierdoVales As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
End Class
