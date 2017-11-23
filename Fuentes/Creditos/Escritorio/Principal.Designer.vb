<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Principal
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
        Me.components = New System.ComponentModel.Container()
        Dim EnhancedScrollBarRenderer8 As FarPoint.Win.Spread.EnhancedScrollBarRenderer = New FarPoint.Win.Spread.EnhancedScrollBarRenderer()
        Dim NamedStyle7 As FarPoint.Win.Spread.NamedStyle = New FarPoint.Win.Spread.NamedStyle("Style1")
        Dim NamedStyle8 As FarPoint.Win.Spread.NamedStyle = New FarPoint.Win.Spread.NamedStyle("DataAreaGrayscale")
        Dim GeneralCellType3 As FarPoint.Win.Spread.CellType.GeneralCellType = New FarPoint.Win.Spread.CellType.GeneralCellType()
        Dim NamedStyle9 As FarPoint.Win.Spread.NamedStyle = New FarPoint.Win.Spread.NamedStyle("ColumnHeaderMidnight")
        Dim NamedStyle10 As FarPoint.Win.Spread.NamedStyle = New FarPoint.Win.Spread.NamedStyle("RowHeaderMidnight")
        Dim NamedStyle11 As FarPoint.Win.Spread.NamedStyle = New FarPoint.Win.Spread.NamedStyle("CornerMidnight")
        Dim EnhancedCornerRenderer2 As FarPoint.Win.Spread.CellType.EnhancedCornerRenderer = New FarPoint.Win.Spread.CellType.EnhancedCornerRenderer()
        Dim NamedStyle12 As FarPoint.Win.Spread.NamedStyle = New FarPoint.Win.Spread.NamedStyle("DataAreaMidnght")
        Dim GeneralCellType4 As FarPoint.Win.Spread.CellType.GeneralCellType = New FarPoint.Win.Spread.CellType.GeneralCellType()
        Dim SpreadSkin2 As FarPoint.Win.Spread.SpreadSkin = New FarPoint.Win.Spread.SpreadSkin()
        Dim EnhancedFocusIndicatorRenderer2 As FarPoint.Win.Spread.EnhancedFocusIndicatorRenderer = New FarPoint.Win.Spread.EnhancedFocusIndicatorRenderer()
        Dim EnhancedInterfaceRenderer2 As FarPoint.Win.Spread.EnhancedInterfaceRenderer = New FarPoint.Win.Spread.EnhancedInterfaceRenderer()
        Dim EnhancedScrollBarRenderer9 As FarPoint.Win.Spread.EnhancedScrollBarRenderer = New FarPoint.Win.Spread.EnhancedScrollBarRenderer()
        Dim EnhancedScrollBarRenderer12 As FarPoint.Win.Spread.EnhancedScrollBarRenderer = New FarPoint.Win.Spread.EnhancedScrollBarRenderer()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Principal))
        Dim EnhancedScrollBarRenderer1 As FarPoint.Win.Spread.EnhancedScrollBarRenderer = New FarPoint.Win.Spread.EnhancedScrollBarRenderer()
        Dim EnhancedScrollBarRenderer2 As FarPoint.Win.Spread.EnhancedScrollBarRenderer = New FarPoint.Win.Spread.EnhancedScrollBarRenderer()
        Dim EnhancedScrollBarRenderer10 As FarPoint.Win.Spread.EnhancedScrollBarRenderer = New FarPoint.Win.Spread.EnhancedScrollBarRenderer()
        Dim EnhancedScrollBarRenderer11 As FarPoint.Win.Spread.EnhancedScrollBarRenderer = New FarPoint.Win.Spread.EnhancedScrollBarRenderer()
        Dim EnhancedScrollBarRenderer6 As FarPoint.Win.Spread.EnhancedScrollBarRenderer = New FarPoint.Win.Spread.EnhancedScrollBarRenderer()
        Dim EnhancedScrollBarRenderer7 As FarPoint.Win.Spread.EnhancedScrollBarRenderer = New FarPoint.Win.Spread.EnhancedScrollBarRenderer()
        Me.pnlContenido = New System.Windows.Forms.Panel()
        Me.pnlCuerpo = New System.Windows.Forms.Panel()
        Me.tcPestanas = New System.Windows.Forms.TabControl()
        Me.tpCapturar = New System.Windows.Forms.TabPage()
        Me.pnlTotales = New System.Windows.Forms.Panel()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtTotal = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtDescuento = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtSubTotal = New System.Windows.Forms.TextBox()
        Me.pnlCatalogos = New System.Windows.Forms.Panel()
        Me.txtBuscarCatalogo = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.spCatalogos = New FarPoint.Win.Spread.FpSpread()
        Me.spCatalogos_Sheet1 = New FarPoint.Win.Spread.SheetView()
        Me.pnlCapturaSuperior = New System.Windows.Forms.Panel()
        Me.cbMetodosPagos = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.chkMostrarDetallado = New System.Windows.Forms.CheckBox()
        Me.btnMostrarOcultar = New System.Windows.Forms.Button()
        Me.btnIdSiguiente = New System.Windows.Forms.Button()
        Me.btnIdAnterior = New System.Windows.Forms.Button()
        Me.cbAlmacenes = New System.Windows.Forms.ComboBox()
        Me.chkMantenerDatos = New System.Windows.Forms.CheckBox()
        Me.cbClientes = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtId = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.dtpFecha = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.spCreditosCapturar = New FarPoint.Win.Spread.FpSpread()
        Me.spCreditosCapturar_Sheet1 = New FarPoint.Win.Spread.SheetView()
        Me.tpActualizar = New System.Windows.Forms.TabPage()
        Me.pnlFiltradoActualizar = New System.Windows.Forms.Panel()
        Me.cbClientesActualizar = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.chkMostrarDetalladoActualizar = New System.Windows.Forms.CheckBox()
        Me.btnGenerar = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.chkFecha = New System.Windows.Forms.CheckBox()
        Me.dtpFechaFinal = New System.Windows.Forms.DateTimePicker()
        Me.dtpFechaInicial = New System.Windows.Forms.DateTimePicker()
        Me.btnMostrarOcultarActualizar = New System.Windows.Forms.Button()
        Me.spCreditosActualizarSeleccionar = New FarPoint.Win.Spread.FpSpread()
        Me.spCreditosActualizarSeleccionar_Sheet1 = New FarPoint.Win.Spread.SheetView()
        Me.spCreditosActualizarGuardar = New FarPoint.Win.Spread.FpSpread()
        Me.spCreditosActualizarGuardar_Sheet1 = New FarPoint.Win.Spread.SheetView()
        Me.pnlPie = New System.Windows.Forms.Panel()
        Me.btnAyuda = New System.Windows.Forms.Button()
        Me.lblDescripcionTooltip = New System.Windows.Forms.Label()
        Me.btnEliminar = New System.Windows.Forms.Button()
        Me.btnSalir = New System.Windows.Forms.Button()
        Me.btnGuardar = New System.Windows.Forms.Button()
        Me.pnlEncabezado = New System.Windows.Forms.Panel()
        Me.pbMarca = New System.Windows.Forms.PictureBox()
        Me.lblEncabezadoArea = New System.Windows.Forms.Label()
        Me.lblEncabezadoUsuario = New System.Windows.Forms.Label()
        Me.lblEncabezadoEmpresa = New System.Windows.Forms.Label()
        Me.lblEncabezadoPrograma = New System.Windows.Forms.Label()
        Me.temporizador = New System.Windows.Forms.Timer(Me.components)
        Me.pnlContenido.SuspendLayout()
        Me.pnlCuerpo.SuspendLayout()
        Me.tcPestanas.SuspendLayout()
        Me.tpCapturar.SuspendLayout()
        Me.pnlTotales.SuspendLayout()
        Me.pnlCatalogos.SuspendLayout()
        CType(Me.spCatalogos, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.spCatalogos_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlCapturaSuperior.SuspendLayout()
        CType(Me.spCreditosCapturar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.spCreditosCapturar_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpActualizar.SuspendLayout()
        Me.pnlFiltradoActualizar.SuspendLayout()
        CType(Me.spCreditosActualizarSeleccionar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.spCreditosActualizarSeleccionar_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.spCreditosActualizarGuardar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.spCreditosActualizarGuardar_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlPie.SuspendLayout()
        Me.pnlEncabezado.SuspendLayout()
        CType(Me.pbMarca, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlContenido
        '
        Me.pnlContenido.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlContenido.BackColor = System.Drawing.Color.DarkSlateGray
        Me.pnlContenido.BackgroundImage = Global.ALMCreditos.My.Resources.Resources.Logo3
        Me.pnlContenido.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.pnlContenido.Controls.Add(Me.pnlCuerpo)
        Me.pnlContenido.Controls.Add(Me.pnlPie)
        Me.pnlContenido.Controls.Add(Me.pnlEncabezado)
        Me.pnlContenido.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlContenido.Location = New System.Drawing.Point(0, 0)
        Me.pnlContenido.Name = "pnlContenido"
        Me.pnlContenido.Size = New System.Drawing.Size(1039, 661)
        Me.pnlContenido.TabIndex = 2
        '
        'pnlCuerpo
        '
        Me.pnlCuerpo.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlCuerpo.AutoScroll = True
        Me.pnlCuerpo.BackColor = System.Drawing.Color.White
        Me.pnlCuerpo.Controls.Add(Me.tcPestanas)
        Me.pnlCuerpo.Location = New System.Drawing.Point(0, 77)
        Me.pnlCuerpo.Name = "pnlCuerpo"
        Me.pnlCuerpo.Size = New System.Drawing.Size(1039, 521)
        Me.pnlCuerpo.TabIndex = 9
        '
        'tcPestanas
        '
        Me.tcPestanas.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tcPestanas.Controls.Add(Me.tpCapturar)
        Me.tcPestanas.Controls.Add(Me.tpActualizar)
        Me.tcPestanas.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tcPestanas.Location = New System.Drawing.Point(0, 0)
        Me.tcPestanas.Name = "tcPestanas"
        Me.tcPestanas.SelectedIndex = 0
        Me.tcPestanas.Size = New System.Drawing.Size(1039, 521)
        Me.tcPestanas.TabIndex = 79
        '
        'tpCapturar
        '
        Me.tpCapturar.BackColor = System.Drawing.Color.White
        Me.tpCapturar.Controls.Add(Me.pnlTotales)
        Me.tpCapturar.Controls.Add(Me.pnlCatalogos)
        Me.tpCapturar.Controls.Add(Me.pnlCapturaSuperior)
        Me.tpCapturar.Controls.Add(Me.spCreditosCapturar)
        Me.tpCapturar.Location = New System.Drawing.Point(4, 27)
        Me.tpCapturar.Name = "tpCapturar"
        Me.tpCapturar.Padding = New System.Windows.Forms.Padding(3)
        Me.tpCapturar.Size = New System.Drawing.Size(1031, 490)
        Me.tpCapturar.TabIndex = 0
        Me.tpCapturar.Text = "CAPTURAR"
        '
        'pnlTotales
        '
        Me.pnlTotales.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlTotales.BackColor = System.Drawing.Color.White
        Me.pnlTotales.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlTotales.Controls.Add(Me.Label12)
        Me.pnlTotales.Controls.Add(Me.txtTotal)
        Me.pnlTotales.Controls.Add(Me.Label11)
        Me.pnlTotales.Controls.Add(Me.txtDescuento)
        Me.pnlTotales.Controls.Add(Me.Label9)
        Me.pnlTotales.Controls.Add(Me.txtSubTotal)
        Me.pnlTotales.Location = New System.Drawing.Point(378, 456)
        Me.pnlTotales.Name = "pnlTotales"
        Me.pnlTotales.Size = New System.Drawing.Size(653, 34)
        Me.pnlTotales.TabIndex = 26
        '
        'Label12
        '
        Me.Label12.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(467, 9)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(53, 15)
        Me.Label12.TabIndex = 92
        Me.Label12.Text = "TOTAL:"
        '
        'txtTotal
        '
        Me.txtTotal.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtTotal.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtTotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotal.ForeColor = System.Drawing.Color.Black
        Me.txtTotal.Location = New System.Drawing.Point(522, 5)
        Me.txtTotal.Name = "txtTotal"
        Me.txtTotal.ReadOnly = True
        Me.txtTotal.Size = New System.Drawing.Size(123, 22)
        Me.txtTotal.TabIndex = 91
        Me.txtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label11
        '
        Me.Label11.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(229, 10)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(95, 15)
        Me.Label11.TabIndex = 90
        Me.Label11.Text = "DESCUENTO:"
        '
        'txtDescuento
        '
        Me.txtDescuento.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDescuento.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtDescuento.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDescuento.ForeColor = System.Drawing.Color.Black
        Me.txtDescuento.Location = New System.Drawing.Point(326, 6)
        Me.txtDescuento.Name = "txtDescuento"
        Me.txtDescuento.ReadOnly = True
        Me.txtDescuento.Size = New System.Drawing.Size(123, 22)
        Me.txtDescuento.TabIndex = 89
        Me.txtDescuento.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label9
        '
        Me.Label9.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(6, 9)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(81, 15)
        Me.Label9.TabIndex = 88
        Me.Label9.Text = "SUBTOTAL:"
        '
        'txtSubTotal
        '
        Me.txtSubTotal.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSubTotal.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSubTotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSubTotal.ForeColor = System.Drawing.Color.Black
        Me.txtSubTotal.Location = New System.Drawing.Point(89, 5)
        Me.txtSubTotal.Name = "txtSubTotal"
        Me.txtSubTotal.ReadOnly = True
        Me.txtSubTotal.Size = New System.Drawing.Size(123, 22)
        Me.txtSubTotal.TabIndex = 87
        Me.txtSubTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'pnlCatalogos
        '
        Me.pnlCatalogos.BackColor = System.Drawing.Color.Indigo
        Me.pnlCatalogos.Controls.Add(Me.txtBuscarCatalogo)
        Me.pnlCatalogos.Controls.Add(Me.Label10)
        Me.pnlCatalogos.Controls.Add(Me.spCatalogos)
        Me.pnlCatalogos.Location = New System.Drawing.Point(378, 0)
        Me.pnlCatalogos.Name = "pnlCatalogos"
        Me.pnlCatalogos.Size = New System.Drawing.Size(260, 150)
        Me.pnlCatalogos.TabIndex = 24
        Me.pnlCatalogos.Visible = False
        '
        'txtBuscarCatalogo
        '
        Me.txtBuscarCatalogo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtBuscarCatalogo.BackColor = System.Drawing.Color.White
        Me.txtBuscarCatalogo.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBuscarCatalogo.ForeColor = System.Drawing.Color.Black
        Me.txtBuscarCatalogo.Location = New System.Drawing.Point(79, 121)
        Me.txtBuscarCatalogo.MaxLength = 300
        Me.txtBuscarCatalogo.Name = "txtBuscarCatalogo"
        Me.txtBuscarCatalogo.Size = New System.Drawing.Size(178, 26)
        Me.txtBuscarCatalogo.TabIndex = 55
        '
        'Label10
        '
        Me.Label10.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.White
        Me.Label10.Location = New System.Drawing.Point(0, 125)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(81, 18)
        Me.Label10.TabIndex = 54
        Me.Label10.Text = "BUSCAR:"
        '
        'spCatalogos
        '
        Me.spCatalogos.AccessibleDescription = ""
        Me.spCatalogos.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.spCatalogos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.spCatalogos.HorizontalScrollBar.Buttons = New FarPoint.Win.Spread.FpScrollBarButtonCollection("BackwardLineButton,ThumbTrack,ForwardLineButton")
        Me.spCatalogos.HorizontalScrollBar.Name = ""
        EnhancedScrollBarRenderer8.ArrowColor = System.Drawing.Color.MidnightBlue
        EnhancedScrollBarRenderer8.ArrowHoveredColor = System.Drawing.Color.MidnightBlue
        EnhancedScrollBarRenderer8.ArrowSelectedColor = System.Drawing.Color.MidnightBlue
        EnhancedScrollBarRenderer8.ButtonBackgroundColor = System.Drawing.Color.DarkSlateBlue
        EnhancedScrollBarRenderer8.ButtonBorderColor = System.Drawing.Color.MidnightBlue
        EnhancedScrollBarRenderer8.ButtonHoveredBackgroundColor = System.Drawing.Color.MidnightBlue
        EnhancedScrollBarRenderer8.ButtonHoveredBorderColor = System.Drawing.Color.Black
        EnhancedScrollBarRenderer8.ButtonSelectedBackgroundColor = System.Drawing.Color.SteelBlue
        EnhancedScrollBarRenderer8.ButtonSelectedBorderColor = System.Drawing.Color.DarkSlateBlue
        EnhancedScrollBarRenderer8.TrackBarBackgroundColor = System.Drawing.Color.SteelBlue
        EnhancedScrollBarRenderer8.TrackBarSelectedBackgroundColor = System.Drawing.Color.DarkSlateBlue
        Me.spCatalogos.HorizontalScrollBar.Renderer = EnhancedScrollBarRenderer8
        Me.spCatalogos.HorizontalScrollBar.TabIndex = 10
        Me.spCatalogos.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never
        Me.spCatalogos.Location = New System.Drawing.Point(0, 0)
        Me.spCatalogos.Name = "spCatalogos"
        NamedStyle7.ForeColor = System.Drawing.Color.White
        NamedStyle7.HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
        NamedStyle7.Locked = False
        NamedStyle7.NoteIndicatorColor = System.Drawing.Color.Red
        NamedStyle7.VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
        NamedStyle8.BackColor = System.Drawing.Color.Gainsboro
        NamedStyle8.CellType = GeneralCellType3
        NamedStyle8.ForeColor = System.Drawing.SystemColors.WindowText
        NamedStyle8.Locked = False
        NamedStyle8.NoteIndicatorColor = System.Drawing.Color.Red
        NamedStyle8.Renderer = GeneralCellType3
        NamedStyle9.BackColor = System.Drawing.Color.DarkSlateBlue
        NamedStyle9.ForeColor = System.Drawing.SystemColors.ControlText
        NamedStyle9.HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
        NamedStyle9.NoteIndicatorColor = System.Drawing.Color.Red
        NamedStyle9.VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
        NamedStyle10.BackColor = System.Drawing.Color.DarkSlateBlue
        NamedStyle10.ForeColor = System.Drawing.SystemColors.ControlText
        NamedStyle10.HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
        NamedStyle10.NoteIndicatorColor = System.Drawing.Color.Red
        NamedStyle10.VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
        NamedStyle11.BackColor = System.Drawing.Color.MidnightBlue
        NamedStyle11.ForeColor = System.Drawing.SystemColors.ControlText
        NamedStyle11.HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
        NamedStyle11.NoteIndicatorColor = System.Drawing.Color.Red
        EnhancedCornerRenderer2.ActiveBackgroundColor = System.Drawing.Color.DarkSlateBlue
        EnhancedCornerRenderer2.GridLineColor = System.Drawing.Color.Empty
        EnhancedCornerRenderer2.NormalBackgroundColor = System.Drawing.Color.MidnightBlue
        NamedStyle11.Renderer = EnhancedCornerRenderer2
        NamedStyle11.VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
        NamedStyle12.BackColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(230, Byte), Integer), CType(CType(255, Byte), Integer))
        NamedStyle12.CellType = GeneralCellType4
        NamedStyle12.ForeColor = System.Drawing.SystemColors.WindowText
        NamedStyle12.Locked = False
        NamedStyle12.NoteIndicatorColor = System.Drawing.Color.Red
        NamedStyle12.Renderer = GeneralCellType4
        Me.spCatalogos.NamedStyles.AddRange(New FarPoint.Win.Spread.NamedStyle() {NamedStyle7, NamedStyle8, NamedStyle9, NamedStyle10, NamedStyle11, NamedStyle12})
        Me.spCatalogos.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.spCatalogos_Sheet1})
        Me.spCatalogos.Size = New System.Drawing.Size(260, 120)
        SpreadSkin2.ColumnFooterDefaultStyle = NamedStyle9
        SpreadSkin2.ColumnHeaderDefaultStyle = NamedStyle9
        SpreadSkin2.CornerDefaultStyle = NamedStyle11
        SpreadSkin2.DefaultStyle = NamedStyle12
        SpreadSkin2.FocusRenderer = EnhancedFocusIndicatorRenderer2
        EnhancedInterfaceRenderer2.ArrowColorEnabled = System.Drawing.Color.MidnightBlue
        EnhancedInterfaceRenderer2.GrayAreaColor = System.Drawing.Color.LightSlateGray
        EnhancedInterfaceRenderer2.RangeGroupBackgroundColor = System.Drawing.Color.DarkSlateBlue
        EnhancedInterfaceRenderer2.RangeGroupButtonBorderColor = System.Drawing.Color.MidnightBlue
        EnhancedInterfaceRenderer2.RangeGroupLineColor = System.Drawing.Color.MidnightBlue
        EnhancedInterfaceRenderer2.ScrollBoxBackgroundColor = System.Drawing.Color.SteelBlue
        EnhancedInterfaceRenderer2.SheetTabBorderColor = System.Drawing.Color.MidnightBlue
        EnhancedInterfaceRenderer2.SheetTabLowerActiveColor = System.Drawing.Color.DarkGray
        EnhancedInterfaceRenderer2.SheetTabLowerNormalColor = System.Drawing.Color.MidnightBlue
        EnhancedInterfaceRenderer2.SheetTabUpperActiveColor = System.Drawing.Color.LightGray
        EnhancedInterfaceRenderer2.SheetTabUpperNormalColor = System.Drawing.Color.DarkSlateBlue
        EnhancedInterfaceRenderer2.SplitBarBackgroundColor = System.Drawing.Color.DarkSlateBlue
        EnhancedInterfaceRenderer2.SplitBarDarkColor = System.Drawing.Color.MidnightBlue
        EnhancedInterfaceRenderer2.SplitBarLightColor = System.Drawing.Color.DarkGray
        EnhancedInterfaceRenderer2.SplitBoxBackgroundColor = System.Drawing.Color.DarkSlateBlue
        EnhancedInterfaceRenderer2.SplitBoxBorderColor = System.Drawing.Color.MidnightBlue
        EnhancedInterfaceRenderer2.TabStripBackgroundColor = System.Drawing.Color.SteelBlue
        EnhancedInterfaceRenderer2.TabStripButtonBorderColor = System.Drawing.Color.MidnightBlue
        EnhancedInterfaceRenderer2.TabStripButtonFlatStyle = System.Windows.Forms.FlatStyle.Flat
        EnhancedInterfaceRenderer2.TabStripButtonLowerActiveColor = System.Drawing.Color.DarkSlateBlue
        EnhancedInterfaceRenderer2.TabStripButtonLowerNormalColor = System.Drawing.Color.MidnightBlue
        EnhancedInterfaceRenderer2.TabStripButtonLowerPressedColor = System.Drawing.Color.DimGray
        EnhancedInterfaceRenderer2.TabStripButtonUpperActiveColor = System.Drawing.Color.DarkGray
        EnhancedInterfaceRenderer2.TabStripButtonUpperNormalColor = System.Drawing.Color.SlateBlue
        EnhancedInterfaceRenderer2.TabStripButtonUpperPressedColor = System.Drawing.Color.DarkSlateBlue
        SpreadSkin2.InterfaceRenderer = EnhancedInterfaceRenderer2
        SpreadSkin2.Name = "MidnightPersonalizado"
        SpreadSkin2.RowHeaderDefaultStyle = NamedStyle10
        EnhancedScrollBarRenderer9.ArrowColor = System.Drawing.Color.MidnightBlue
        EnhancedScrollBarRenderer9.ArrowHoveredColor = System.Drawing.Color.MidnightBlue
        EnhancedScrollBarRenderer9.ArrowSelectedColor = System.Drawing.Color.MidnightBlue
        EnhancedScrollBarRenderer9.ButtonBackgroundColor = System.Drawing.Color.DarkSlateBlue
        EnhancedScrollBarRenderer9.ButtonBorderColor = System.Drawing.Color.MidnightBlue
        EnhancedScrollBarRenderer9.ButtonHoveredBackgroundColor = System.Drawing.Color.MidnightBlue
        EnhancedScrollBarRenderer9.ButtonHoveredBorderColor = System.Drawing.Color.Black
        EnhancedScrollBarRenderer9.ButtonSelectedBackgroundColor = System.Drawing.Color.SteelBlue
        EnhancedScrollBarRenderer9.ButtonSelectedBorderColor = System.Drawing.Color.DarkSlateBlue
        EnhancedScrollBarRenderer9.TrackBarBackgroundColor = System.Drawing.Color.SteelBlue
        EnhancedScrollBarRenderer9.TrackBarSelectedBackgroundColor = System.Drawing.Color.DarkSlateBlue
        SpreadSkin2.ScrollBarRenderer = EnhancedScrollBarRenderer9
        SpreadSkin2.SelectionRenderer = New FarPoint.Win.Spread.GradientSelectionRenderer(System.Drawing.Color.MidnightBlue, System.Drawing.Color.MidnightBlue, System.Drawing.Drawing2D.LinearGradientMode.Horizontal, 80)
        Me.spCatalogos.Skin = SpreadSkin2
        Me.spCatalogos.TabIndex = 22
        Me.spCatalogos.VerticalScrollBar.Buttons = New FarPoint.Win.Spread.FpScrollBarButtonCollection("BackwardLineButton,ThumbTrack,ForwardLineButton")
        Me.spCatalogos.VerticalScrollBar.Name = ""
        EnhancedScrollBarRenderer12.ArrowColor = System.Drawing.Color.MidnightBlue
        EnhancedScrollBarRenderer12.ArrowHoveredColor = System.Drawing.Color.MidnightBlue
        EnhancedScrollBarRenderer12.ArrowSelectedColor = System.Drawing.Color.MidnightBlue
        EnhancedScrollBarRenderer12.ButtonBackgroundColor = System.Drawing.Color.DarkSlateBlue
        EnhancedScrollBarRenderer12.ButtonBorderColor = System.Drawing.Color.MidnightBlue
        EnhancedScrollBarRenderer12.ButtonHoveredBackgroundColor = System.Drawing.Color.MidnightBlue
        EnhancedScrollBarRenderer12.ButtonHoveredBorderColor = System.Drawing.Color.Black
        EnhancedScrollBarRenderer12.ButtonSelectedBackgroundColor = System.Drawing.Color.SteelBlue
        EnhancedScrollBarRenderer12.ButtonSelectedBorderColor = System.Drawing.Color.DarkSlateBlue
        EnhancedScrollBarRenderer12.TrackBarBackgroundColor = System.Drawing.Color.SteelBlue
        EnhancedScrollBarRenderer12.TrackBarSelectedBackgroundColor = System.Drawing.Color.DarkSlateBlue
        Me.spCatalogos.VerticalScrollBar.Renderer = EnhancedScrollBarRenderer12
        Me.spCatalogos.VerticalScrollBar.TabIndex = 11
        '
        'spCatalogos_Sheet1
        '
        Me.spCatalogos_Sheet1.Reset()
        spCatalogos_Sheet1.SheetName = "Sheet1"
        'Formulas and custom names must be loaded with R1C1 reference style
        Me.spCatalogos_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
        Me.spCatalogos_Sheet1.ColumnFooter.DefaultStyle.NoteIndicatorColor = System.Drawing.Color.Red
        Me.spCatalogos_Sheet1.ColumnFooter.DefaultStyle.Parent = "ColumnHeaderMidnight"
        Me.spCatalogos_Sheet1.ColumnFooterSheetCornerStyle.NoteIndicatorColor = System.Drawing.Color.Red
        Me.spCatalogos_Sheet1.ColumnFooterSheetCornerStyle.Parent = "CornerMidnight"
        Me.spCatalogos_Sheet1.ColumnHeader.DefaultStyle.NoteIndicatorColor = System.Drawing.Color.Red
        Me.spCatalogos_Sheet1.ColumnHeader.DefaultStyle.Parent = "ColumnHeaderMidnight"
        Me.spCatalogos_Sheet1.DefaultStyle.NoteIndicatorColor = System.Drawing.Color.Red
        Me.spCatalogos_Sheet1.DefaultStyle.Parent = "DataAreaMidnght"
        Me.spCatalogos_Sheet1.RowHeader.DefaultStyle.NoteIndicatorColor = System.Drawing.Color.Red
        Me.spCatalogos_Sheet1.RowHeader.DefaultStyle.Parent = "RowHeaderMidnight"
        Me.spCatalogos_Sheet1.SheetCornerStyle.NoteIndicatorColor = System.Drawing.Color.Red
        Me.spCatalogos_Sheet1.SheetCornerStyle.Parent = "CornerMidnight"
        Me.spCatalogos_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
        '
        'pnlCapturaSuperior
        '
        Me.pnlCapturaSuperior.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.pnlCapturaSuperior.AutoScroll = True
        Me.pnlCapturaSuperior.BackColor = System.Drawing.Color.White
        Me.pnlCapturaSuperior.Controls.Add(Me.cbMetodosPagos)
        Me.pnlCapturaSuperior.Controls.Add(Me.Label8)
        Me.pnlCapturaSuperior.Controls.Add(Me.chkMostrarDetallado)
        Me.pnlCapturaSuperior.Controls.Add(Me.btnMostrarOcultar)
        Me.pnlCapturaSuperior.Controls.Add(Me.btnIdSiguiente)
        Me.pnlCapturaSuperior.Controls.Add(Me.btnIdAnterior)
        Me.pnlCapturaSuperior.Controls.Add(Me.cbAlmacenes)
        Me.pnlCapturaSuperior.Controls.Add(Me.chkMantenerDatos)
        Me.pnlCapturaSuperior.Controls.Add(Me.cbClientes)
        Me.pnlCapturaSuperior.Controls.Add(Me.Label5)
        Me.pnlCapturaSuperior.Controls.Add(Me.txtId)
        Me.pnlCapturaSuperior.Controls.Add(Me.Label3)
        Me.pnlCapturaSuperior.Controls.Add(Me.dtpFecha)
        Me.pnlCapturaSuperior.Controls.Add(Me.Label2)
        Me.pnlCapturaSuperior.Controls.Add(Me.Label1)
        Me.pnlCapturaSuperior.Location = New System.Drawing.Point(0, 0)
        Me.pnlCapturaSuperior.Name = "pnlCapturaSuperior"
        Me.pnlCapturaSuperior.Size = New System.Drawing.Size(378, 490)
        Me.pnlCapturaSuperior.TabIndex = 23
        '
        'cbMetodosPagos
        '
        Me.cbMetodosPagos.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cbMetodosPagos.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cbMetodosPagos.BackColor = System.Drawing.Color.White
        Me.cbMetodosPagos.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbMetodosPagos.ForeColor = System.Drawing.Color.Black
        Me.cbMetodosPagos.FormattingEnabled = True
        Me.cbMetodosPagos.Location = New System.Drawing.Point(122, 126)
        Me.cbMetodosPagos.Name = "cbMetodosPagos"
        Me.cbMetodosPagos.Size = New System.Drawing.Size(209, 24)
        Me.cbMetodosPagos.TabIndex = 94
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(-2, 130)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(121, 15)
        Me.Label8.TabIndex = 93
        Me.Label8.Text = "MÉTODO PAGO: *"
        '
        'chkMostrarDetallado
        '
        Me.chkMostrarDetallado.AutoSize = True
        Me.chkMostrarDetallado.Cursor = System.Windows.Forms.Cursors.Hand
        Me.chkMostrarDetallado.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMostrarDetallado.ForeColor = System.Drawing.Color.Black
        Me.chkMostrarDetallado.Location = New System.Drawing.Point(86, 179)
        Me.chkMostrarDetallado.Name = "chkMostrarDetallado"
        Me.chkMostrarDetallado.Size = New System.Drawing.Size(151, 20)
        Me.chkMostrarDetallado.TabIndex = 78
        Me.chkMostrarDetallado.Text = "Mostrar Detallado"
        Me.chkMostrarDetallado.UseVisualStyleBackColor = True
        '
        'btnMostrarOcultar
        '
        Me.btnMostrarOcultar.BackColor = System.Drawing.Color.Transparent
        Me.btnMostrarOcultar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnMostrarOcultar.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnMostrarOcultar.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.btnMostrarOcultar.FlatAppearance.BorderSize = 0
        Me.btnMostrarOcultar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.MediumAquamarine
        Me.btnMostrarOcultar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnMostrarOcultar.ForeColor = System.Drawing.Color.Black
        Me.btnMostrarOcultar.Image = CType(resources.GetObject("btnMostrarOcultar.Image"), System.Drawing.Image)
        Me.btnMostrarOcultar.Location = New System.Drawing.Point(334, 0)
        Me.btnMostrarOcultar.Margin = New System.Windows.Forms.Padding(0)
        Me.btnMostrarOcultar.Name = "btnMostrarOcultar"
        Me.btnMostrarOcultar.Size = New System.Drawing.Size(40, 38)
        Me.btnMostrarOcultar.TabIndex = 77
        Me.btnMostrarOcultar.UseVisualStyleBackColor = False
        '
        'btnIdSiguiente
        '
        Me.btnIdSiguiente.BackColor = System.Drawing.Color.FromArgb(CType(CType(99, Byte), Integer), CType(CType(160, Byte), Integer), CType(CType(162, Byte), Integer))
        Me.btnIdSiguiente.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnIdSiguiente.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnIdSiguiente.Location = New System.Drawing.Point(161, 36)
        Me.btnIdSiguiente.Name = "btnIdSiguiente"
        Me.btnIdSiguiente.Size = New System.Drawing.Size(25, 28)
        Me.btnIdSiguiente.TabIndex = 21
        Me.btnIdSiguiente.Text = ">"
        Me.btnIdSiguiente.UseVisualStyleBackColor = False
        '
        'btnIdAnterior
        '
        Me.btnIdAnterior.BackColor = System.Drawing.Color.FromArgb(CType(CType(99, Byte), Integer), CType(CType(160, Byte), Integer), CType(CType(162, Byte), Integer))
        Me.btnIdAnterior.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnIdAnterior.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnIdAnterior.Location = New System.Drawing.Point(137, 36)
        Me.btnIdAnterior.Name = "btnIdAnterior"
        Me.btnIdAnterior.Size = New System.Drawing.Size(25, 28)
        Me.btnIdAnterior.TabIndex = 20
        Me.btnIdAnterior.Text = "<"
        Me.btnIdAnterior.UseVisualStyleBackColor = False
        '
        'cbAlmacenes
        '
        Me.cbAlmacenes.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cbAlmacenes.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cbAlmacenes.BackColor = System.Drawing.Color.White
        Me.cbAlmacenes.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbAlmacenes.ForeColor = System.Drawing.Color.Black
        Me.cbAlmacenes.FormattingEnabled = True
        Me.cbAlmacenes.Location = New System.Drawing.Point(86, 7)
        Me.cbAlmacenes.Name = "cbAlmacenes"
        Me.cbAlmacenes.Size = New System.Drawing.Size(245, 24)
        Me.cbAlmacenes.TabIndex = 19
        '
        'chkMantenerDatos
        '
        Me.chkMantenerDatos.AutoSize = True
        Me.chkMantenerDatos.Checked = True
        Me.chkMantenerDatos.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkMantenerDatos.Cursor = System.Windows.Forms.Cursors.Hand
        Me.chkMantenerDatos.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMantenerDatos.ForeColor = System.Drawing.Color.Black
        Me.chkMantenerDatos.Location = New System.Drawing.Point(86, 156)
        Me.chkMantenerDatos.Name = "chkMantenerDatos"
        Me.chkMantenerDatos.Size = New System.Drawing.Size(214, 20)
        Me.chkMantenerDatos.TabIndex = 18
        Me.chkMantenerDatos.Text = "Mantener Datos Al Guardar"
        Me.chkMantenerDatos.UseVisualStyleBackColor = True
        '
        'cbClientes
        '
        Me.cbClientes.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cbClientes.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cbClientes.BackColor = System.Drawing.Color.White
        Me.cbClientes.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbClientes.ForeColor = System.Drawing.Color.Black
        Me.cbClientes.FormattingEnabled = True
        Me.cbClientes.Location = New System.Drawing.Point(86, 96)
        Me.cbClientes.Name = "cbClientes"
        Me.cbClientes.Size = New System.Drawing.Size(245, 24)
        Me.cbClientes.TabIndex = 10
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(5, 101)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(78, 15)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "CLIENTE: *"
        '
        'txtId
        '
        Me.txtId.BackColor = System.Drawing.Color.White
        Me.txtId.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtId.ForeColor = System.Drawing.Color.Black
        Me.txtId.Location = New System.Drawing.Point(86, 39)
        Me.txtId.Name = "txtId"
        Me.txtId.Size = New System.Drawing.Size(50, 22)
        Me.txtId.TabIndex = 6
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(44, 41)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(41, 15)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "NO: *"
        '
        'dtpFecha
        '
        Me.dtpFecha.CalendarForeColor = System.Drawing.Color.Black
        Me.dtpFecha.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtpFecha.Cursor = System.Windows.Forms.Cursors.Hand
        Me.dtpFecha.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpFecha.Location = New System.Drawing.Point(86, 69)
        Me.dtpFecha.Name = "dtpFecha"
        Me.dtpFecha.Size = New System.Drawing.Size(245, 22)
        Me.dtpFecha.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(20, 73)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(65, 15)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "FECHA: *"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(0, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(85, 15)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "ALMACÉN: *"
        '
        'spCreditosCapturar
        '
        Me.spCreditosCapturar.AccessibleDescription = ""
        Me.spCreditosCapturar.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.spCreditosCapturar.BackColor = System.Drawing.Color.White
        Me.spCreditosCapturar.HorizontalScrollBar.Buttons = New FarPoint.Win.Spread.FpScrollBarButtonCollection("BackwardLineButton,ThumbTrack,ForwardLineButton")
        Me.spCreditosCapturar.HorizontalScrollBar.Name = ""
        EnhancedScrollBarRenderer1.ArrowColor = System.Drawing.Color.DarkSlateGray
        EnhancedScrollBarRenderer1.ArrowHoveredColor = System.Drawing.Color.DarkSlateGray
        EnhancedScrollBarRenderer1.ArrowSelectedColor = System.Drawing.Color.DarkSlateGray
        EnhancedScrollBarRenderer1.ButtonBackgroundColor = System.Drawing.Color.CadetBlue
        EnhancedScrollBarRenderer1.ButtonBorderColor = System.Drawing.Color.SlateGray
        EnhancedScrollBarRenderer1.ButtonHoveredBackgroundColor = System.Drawing.Color.SlateGray
        EnhancedScrollBarRenderer1.ButtonHoveredBorderColor = System.Drawing.Color.DarkGray
        EnhancedScrollBarRenderer1.ButtonSelectedBackgroundColor = System.Drawing.Color.DarkGray
        EnhancedScrollBarRenderer1.ButtonSelectedBorderColor = System.Drawing.Color.CadetBlue
        EnhancedScrollBarRenderer1.TrackBarBackgroundColor = System.Drawing.Color.CadetBlue
        EnhancedScrollBarRenderer1.TrackBarSelectedBackgroundColor = System.Drawing.Color.SlateGray
        Me.spCreditosCapturar.HorizontalScrollBar.Renderer = EnhancedScrollBarRenderer1
        Me.spCreditosCapturar.HorizontalScrollBar.TabIndex = 10
        Me.spCreditosCapturar.Location = New System.Drawing.Point(378, 0)
        Me.spCreditosCapturar.Name = "spCreditosCapturar"
        Me.spCreditosCapturar.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.spCreditosCapturar_Sheet1})
        Me.spCreditosCapturar.Size = New System.Drawing.Size(653, 455)
        Me.spCreditosCapturar.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Seashell
        Me.spCreditosCapturar.TabIndex = 0
        Me.spCreditosCapturar.VerticalScrollBar.Buttons = New FarPoint.Win.Spread.FpScrollBarButtonCollection("BackwardLineButton,ThumbTrack,ForwardLineButton")
        Me.spCreditosCapturar.VerticalScrollBar.Name = ""
        EnhancedScrollBarRenderer2.ArrowColor = System.Drawing.Color.DarkSlateGray
        EnhancedScrollBarRenderer2.ArrowHoveredColor = System.Drawing.Color.DarkSlateGray
        EnhancedScrollBarRenderer2.ArrowSelectedColor = System.Drawing.Color.DarkSlateGray
        EnhancedScrollBarRenderer2.ButtonBackgroundColor = System.Drawing.Color.CadetBlue
        EnhancedScrollBarRenderer2.ButtonBorderColor = System.Drawing.Color.SlateGray
        EnhancedScrollBarRenderer2.ButtonHoveredBackgroundColor = System.Drawing.Color.SlateGray
        EnhancedScrollBarRenderer2.ButtonHoveredBorderColor = System.Drawing.Color.DarkGray
        EnhancedScrollBarRenderer2.ButtonSelectedBackgroundColor = System.Drawing.Color.DarkGray
        EnhancedScrollBarRenderer2.ButtonSelectedBorderColor = System.Drawing.Color.CadetBlue
        EnhancedScrollBarRenderer2.TrackBarBackgroundColor = System.Drawing.Color.CadetBlue
        EnhancedScrollBarRenderer2.TrackBarSelectedBackgroundColor = System.Drawing.Color.SlateGray
        Me.spCreditosCapturar.VerticalScrollBar.Renderer = EnhancedScrollBarRenderer2
        Me.spCreditosCapturar.VerticalScrollBar.TabIndex = 11
        '
        'spCreditosCapturar_Sheet1
        '
        Me.spCreditosCapturar_Sheet1.Reset()
        spCreditosCapturar_Sheet1.SheetName = "Sheet1"
        'Formulas and custom names must be loaded with R1C1 reference style
        Me.spCreditosCapturar_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
        Me.spCreditosCapturar_Sheet1.ColumnFooter.DefaultStyle.NoteIndicatorColor = System.Drawing.Color.Red
        Me.spCreditosCapturar_Sheet1.ColumnFooter.DefaultStyle.Parent = "ColumnHeaderSeashell"
        Me.spCreditosCapturar_Sheet1.ColumnFooterSheetCornerStyle.NoteIndicatorColor = System.Drawing.Color.Red
        Me.spCreditosCapturar_Sheet1.ColumnFooterSheetCornerStyle.Parent = "CornerSeashell"
        Me.spCreditosCapturar_Sheet1.ColumnHeader.DefaultStyle.NoteIndicatorColor = System.Drawing.Color.Red
        Me.spCreditosCapturar_Sheet1.ColumnHeader.DefaultStyle.Parent = "ColumnHeaderSeashell"
        Me.spCreditosCapturar_Sheet1.RowHeader.DefaultStyle.NoteIndicatorColor = System.Drawing.Color.Red
        Me.spCreditosCapturar_Sheet1.RowHeader.DefaultStyle.Parent = "RowHeaderSeashell"
        Me.spCreditosCapturar_Sheet1.SheetCornerStyle.NoteIndicatorColor = System.Drawing.Color.Red
        Me.spCreditosCapturar_Sheet1.SheetCornerStyle.Parent = "CornerSeashell"
        Me.spCreditosCapturar_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
        '
        'tpActualizar
        '
        Me.tpActualizar.BackColor = System.Drawing.Color.White
        Me.tpActualizar.Controls.Add(Me.pnlFiltradoActualizar)
        Me.tpActualizar.Controls.Add(Me.spCreditosActualizarSeleccionar)
        Me.tpActualizar.Controls.Add(Me.spCreditosActualizarGuardar)
        Me.tpActualizar.Location = New System.Drawing.Point(4, 27)
        Me.tpActualizar.Name = "tpActualizar"
        Me.tpActualizar.Padding = New System.Windows.Forms.Padding(3)
        Me.tpActualizar.Size = New System.Drawing.Size(1031, 490)
        Me.tpActualizar.TabIndex = 1
        Me.tpActualizar.Text = "ACTUALIZAR"
        '
        'pnlFiltradoActualizar
        '
        Me.pnlFiltradoActualizar.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.pnlFiltradoActualizar.AutoScroll = True
        Me.pnlFiltradoActualizar.BackColor = System.Drawing.Color.White
        Me.pnlFiltradoActualizar.Controls.Add(Me.cbClientesActualizar)
        Me.pnlFiltradoActualizar.Controls.Add(Me.Label7)
        Me.pnlFiltradoActualizar.Controls.Add(Me.chkMostrarDetalladoActualizar)
        Me.pnlFiltradoActualizar.Controls.Add(Me.btnGenerar)
        Me.pnlFiltradoActualizar.Controls.Add(Me.Label4)
        Me.pnlFiltradoActualizar.Controls.Add(Me.Label6)
        Me.pnlFiltradoActualizar.Controls.Add(Me.chkFecha)
        Me.pnlFiltradoActualizar.Controls.Add(Me.dtpFechaFinal)
        Me.pnlFiltradoActualizar.Controls.Add(Me.dtpFechaInicial)
        Me.pnlFiltradoActualizar.Controls.Add(Me.btnMostrarOcultarActualizar)
        Me.pnlFiltradoActualizar.Location = New System.Drawing.Point(0, 0)
        Me.pnlFiltradoActualizar.Name = "pnlFiltradoActualizar"
        Me.pnlFiltradoActualizar.Size = New System.Drawing.Size(378, 490)
        Me.pnlFiltradoActualizar.TabIndex = 25
        '
        'cbClientesActualizar
        '
        Me.cbClientesActualizar.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cbClientesActualizar.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cbClientesActualizar.BackColor = System.Drawing.Color.White
        Me.cbClientesActualizar.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbClientesActualizar.ForeColor = System.Drawing.Color.Black
        Me.cbClientesActualizar.FormattingEnabled = True
        Me.cbClientesActualizar.Location = New System.Drawing.Point(73, 56)
        Me.cbClientesActualizar.Name = "cbClientesActualizar"
        Me.cbClientesActualizar.Size = New System.Drawing.Size(255, 24)
        Me.cbClientesActualizar.TabIndex = 90
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(2, 61)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(68, 15)
        Me.Label7.TabIndex = 89
        Me.Label7.Text = "CLIENTE:"
        '
        'chkMostrarDetalladoActualizar
        '
        Me.chkMostrarDetalladoActualizar.AutoSize = True
        Me.chkMostrarDetalladoActualizar.Cursor = System.Windows.Forms.Cursors.Hand
        Me.chkMostrarDetalladoActualizar.Enabled = False
        Me.chkMostrarDetalladoActualizar.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMostrarDetalladoActualizar.ForeColor = System.Drawing.Color.Black
        Me.chkMostrarDetalladoActualizar.Location = New System.Drawing.Point(73, 86)
        Me.chkMostrarDetalladoActualizar.Name = "chkMostrarDetalladoActualizar"
        Me.chkMostrarDetalladoActualizar.Size = New System.Drawing.Size(151, 20)
        Me.chkMostrarDetalladoActualizar.TabIndex = 88
        Me.chkMostrarDetalladoActualizar.Text = "Mostrar Detallado"
        Me.chkMostrarDetalladoActualizar.UseVisualStyleBackColor = True
        '
        'btnGenerar
        '
        Me.btnGenerar.BackColor = System.Drawing.Color.FromArgb(CType(CType(99, Byte), Integer), CType(CType(160, Byte), Integer), CType(CType(162, Byte), Integer))
        Me.btnGenerar.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnGenerar.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.btnGenerar.FlatAppearance.BorderSize = 3
        Me.btnGenerar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.MediumAquamarine
        Me.btnGenerar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnGenerar.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGenerar.ForeColor = System.Drawing.Color.Black
        Me.btnGenerar.Location = New System.Drawing.Point(7, 112)
        Me.btnGenerar.Name = "btnGenerar"
        Me.btnGenerar.Size = New System.Drawing.Size(324, 40)
        Me.btnGenerar.TabIndex = 87
        Me.btnGenerar.Text = "GENERAR"
        Me.btnGenerar.UseVisualStyleBackColor = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(39, 5)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(139, 15)
        Me.Label4.TabIndex = 82
        Me.Label4.Text = "RANGO DE FECHAS:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(244, 8)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(53, 13)
        Me.Label6.TabIndex = 81
        Me.Label6.Text = "Aplicar?"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        '
        'chkFecha
        '
        Me.chkFecha.Appearance = System.Windows.Forms.Appearance.Button
        Me.chkFecha.BackColor = System.Drawing.Color.Black
        Me.chkFecha.Checked = True
        Me.chkFecha.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkFecha.Cursor = System.Windows.Forms.Cursors.Hand
        Me.chkFecha.FlatAppearance.BorderSize = 0
        Me.chkFecha.FlatAppearance.CheckedBackColor = System.Drawing.Color.Turquoise
        Me.chkFecha.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.chkFecha.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkFecha.ForeColor = System.Drawing.Color.White
        Me.chkFecha.Location = New System.Drawing.Point(247, 23)
        Me.chkFecha.Name = "chkFecha"
        Me.chkFecha.Size = New System.Drawing.Size(45, 25)
        Me.chkFecha.TabIndex = 80
        Me.chkFecha.Text = "SI"
        Me.chkFecha.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkFecha.UseVisualStyleBackColor = False
        '
        'dtpFechaFinal
        '
        Me.dtpFechaFinal.Cursor = System.Windows.Forms.Cursors.Hand
        Me.dtpFechaFinal.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpFechaFinal.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFechaFinal.Location = New System.Drawing.Point(142, 25)
        Me.dtpFechaFinal.Name = "dtpFechaFinal"
        Me.dtpFechaFinal.Size = New System.Drawing.Size(102, 22)
        Me.dtpFechaFinal.TabIndex = 79
        '
        'dtpFechaInicial
        '
        Me.dtpFechaInicial.Cursor = System.Windows.Forms.Cursors.Hand
        Me.dtpFechaInicial.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpFechaInicial.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFechaInicial.Location = New System.Drawing.Point(38, 25)
        Me.dtpFechaInicial.Name = "dtpFechaInicial"
        Me.dtpFechaInicial.Size = New System.Drawing.Size(102, 22)
        Me.dtpFechaInicial.TabIndex = 78
        '
        'btnMostrarOcultarActualizar
        '
        Me.btnMostrarOcultarActualizar.BackColor = System.Drawing.Color.Transparent
        Me.btnMostrarOcultarActualizar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnMostrarOcultarActualizar.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnMostrarOcultarActualizar.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.btnMostrarOcultarActualizar.FlatAppearance.BorderSize = 0
        Me.btnMostrarOcultarActualizar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.MediumAquamarine
        Me.btnMostrarOcultarActualizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnMostrarOcultarActualizar.ForeColor = System.Drawing.Color.Black
        Me.btnMostrarOcultarActualizar.Image = CType(resources.GetObject("btnMostrarOcultarActualizar.Image"), System.Drawing.Image)
        Me.btnMostrarOcultarActualizar.Location = New System.Drawing.Point(334, 0)
        Me.btnMostrarOcultarActualizar.Margin = New System.Windows.Forms.Padding(0)
        Me.btnMostrarOcultarActualizar.Name = "btnMostrarOcultarActualizar"
        Me.btnMostrarOcultarActualizar.Size = New System.Drawing.Size(40, 38)
        Me.btnMostrarOcultarActualizar.TabIndex = 77
        Me.btnMostrarOcultarActualizar.UseVisualStyleBackColor = False
        '
        'spCreditosActualizarSeleccionar
        '
        Me.spCreditosActualizarSeleccionar.AccessibleDescription = ""
        Me.spCreditosActualizarSeleccionar.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.spCreditosActualizarSeleccionar.BackColor = System.Drawing.Color.White
        Me.spCreditosActualizarSeleccionar.HorizontalScrollBar.Buttons = New FarPoint.Win.Spread.FpScrollBarButtonCollection("BackwardLineButton,ThumbTrack,ForwardLineButton")
        Me.spCreditosActualizarSeleccionar.HorizontalScrollBar.Name = ""
        EnhancedScrollBarRenderer10.ArrowColor = System.Drawing.Color.DarkSlateGray
        EnhancedScrollBarRenderer10.ArrowHoveredColor = System.Drawing.Color.DarkSlateGray
        EnhancedScrollBarRenderer10.ArrowSelectedColor = System.Drawing.Color.DarkSlateGray
        EnhancedScrollBarRenderer10.ButtonBackgroundColor = System.Drawing.Color.CadetBlue
        EnhancedScrollBarRenderer10.ButtonBorderColor = System.Drawing.Color.SlateGray
        EnhancedScrollBarRenderer10.ButtonHoveredBackgroundColor = System.Drawing.Color.SlateGray
        EnhancedScrollBarRenderer10.ButtonHoveredBorderColor = System.Drawing.Color.DarkGray
        EnhancedScrollBarRenderer10.ButtonSelectedBackgroundColor = System.Drawing.Color.DarkGray
        EnhancedScrollBarRenderer10.ButtonSelectedBorderColor = System.Drawing.Color.CadetBlue
        EnhancedScrollBarRenderer10.TrackBarBackgroundColor = System.Drawing.Color.CadetBlue
        EnhancedScrollBarRenderer10.TrackBarSelectedBackgroundColor = System.Drawing.Color.SlateGray
        Me.spCreditosActualizarSeleccionar.HorizontalScrollBar.Renderer = EnhancedScrollBarRenderer10
        Me.spCreditosActualizarSeleccionar.HorizontalScrollBar.TabIndex = 10
        Me.spCreditosActualizarSeleccionar.Location = New System.Drawing.Point(378, 0)
        Me.spCreditosActualizarSeleccionar.Name = "spCreditosActualizarSeleccionar"
        Me.spCreditosActualizarSeleccionar.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.spCreditosActualizarSeleccionar_Sheet1})
        Me.spCreditosActualizarSeleccionar.Size = New System.Drawing.Size(325, 490)
        Me.spCreditosActualizarSeleccionar.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Seashell
        Me.spCreditosActualizarSeleccionar.TabIndex = 24
        Me.spCreditosActualizarSeleccionar.VerticalScrollBar.Buttons = New FarPoint.Win.Spread.FpScrollBarButtonCollection("BackwardLineButton,ThumbTrack,ForwardLineButton")
        Me.spCreditosActualizarSeleccionar.VerticalScrollBar.Name = ""
        EnhancedScrollBarRenderer11.ArrowColor = System.Drawing.Color.DarkSlateGray
        EnhancedScrollBarRenderer11.ArrowHoveredColor = System.Drawing.Color.DarkSlateGray
        EnhancedScrollBarRenderer11.ArrowSelectedColor = System.Drawing.Color.DarkSlateGray
        EnhancedScrollBarRenderer11.ButtonBackgroundColor = System.Drawing.Color.CadetBlue
        EnhancedScrollBarRenderer11.ButtonBorderColor = System.Drawing.Color.SlateGray
        EnhancedScrollBarRenderer11.ButtonHoveredBackgroundColor = System.Drawing.Color.SlateGray
        EnhancedScrollBarRenderer11.ButtonHoveredBorderColor = System.Drawing.Color.DarkGray
        EnhancedScrollBarRenderer11.ButtonSelectedBackgroundColor = System.Drawing.Color.DarkGray
        EnhancedScrollBarRenderer11.ButtonSelectedBorderColor = System.Drawing.Color.CadetBlue
        EnhancedScrollBarRenderer11.TrackBarBackgroundColor = System.Drawing.Color.CadetBlue
        EnhancedScrollBarRenderer11.TrackBarSelectedBackgroundColor = System.Drawing.Color.SlateGray
        Me.spCreditosActualizarSeleccionar.VerticalScrollBar.Renderer = EnhancedScrollBarRenderer11
        Me.spCreditosActualizarSeleccionar.VerticalScrollBar.TabIndex = 11
        Me.spCreditosActualizarSeleccionar.Visible = False
        '
        'spCreditosActualizarSeleccionar_Sheet1
        '
        Me.spCreditosActualizarSeleccionar_Sheet1.Reset()
        spCreditosActualizarSeleccionar_Sheet1.SheetName = "Sheet1"
        'Formulas and custom names must be loaded with R1C1 reference style
        Me.spCreditosActualizarSeleccionar_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
        Me.spCreditosActualizarSeleccionar_Sheet1.ColumnFooter.DefaultStyle.NoteIndicatorColor = System.Drawing.Color.Red
        Me.spCreditosActualizarSeleccionar_Sheet1.ColumnFooter.DefaultStyle.Parent = "ColumnHeaderSeashell"
        Me.spCreditosActualizarSeleccionar_Sheet1.ColumnFooterSheetCornerStyle.NoteIndicatorColor = System.Drawing.Color.Red
        Me.spCreditosActualizarSeleccionar_Sheet1.ColumnFooterSheetCornerStyle.Parent = "CornerSeashell"
        Me.spCreditosActualizarSeleccionar_Sheet1.ColumnHeader.DefaultStyle.NoteIndicatorColor = System.Drawing.Color.Red
        Me.spCreditosActualizarSeleccionar_Sheet1.ColumnHeader.DefaultStyle.Parent = "ColumnHeaderSeashell"
        Me.spCreditosActualizarSeleccionar_Sheet1.RowHeader.DefaultStyle.NoteIndicatorColor = System.Drawing.Color.Red
        Me.spCreditosActualizarSeleccionar_Sheet1.RowHeader.DefaultStyle.Parent = "RowHeaderSeashell"
        Me.spCreditosActualizarSeleccionar_Sheet1.SheetCornerStyle.NoteIndicatorColor = System.Drawing.Color.Red
        Me.spCreditosActualizarSeleccionar_Sheet1.SheetCornerStyle.Parent = "CornerSeashell"
        Me.spCreditosActualizarSeleccionar_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
        '
        'spCreditosActualizarGuardar
        '
        Me.spCreditosActualizarGuardar.AccessibleDescription = ""
        Me.spCreditosActualizarGuardar.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.spCreditosActualizarGuardar.BackColor = System.Drawing.Color.White
        Me.spCreditosActualizarGuardar.HorizontalScrollBar.Buttons = New FarPoint.Win.Spread.FpScrollBarButtonCollection("BackwardLineButton,ThumbTrack,ForwardLineButton")
        Me.spCreditosActualizarGuardar.HorizontalScrollBar.Name = ""
        EnhancedScrollBarRenderer6.ArrowColor = System.Drawing.Color.DarkSlateGray
        EnhancedScrollBarRenderer6.ArrowHoveredColor = System.Drawing.Color.DarkSlateGray
        EnhancedScrollBarRenderer6.ArrowSelectedColor = System.Drawing.Color.DarkSlateGray
        EnhancedScrollBarRenderer6.ButtonBackgroundColor = System.Drawing.Color.CadetBlue
        EnhancedScrollBarRenderer6.ButtonBorderColor = System.Drawing.Color.SlateGray
        EnhancedScrollBarRenderer6.ButtonHoveredBackgroundColor = System.Drawing.Color.SlateGray
        EnhancedScrollBarRenderer6.ButtonHoveredBorderColor = System.Drawing.Color.DarkGray
        EnhancedScrollBarRenderer6.ButtonSelectedBackgroundColor = System.Drawing.Color.DarkGray
        EnhancedScrollBarRenderer6.ButtonSelectedBorderColor = System.Drawing.Color.CadetBlue
        EnhancedScrollBarRenderer6.TrackBarBackgroundColor = System.Drawing.Color.CadetBlue
        EnhancedScrollBarRenderer6.TrackBarSelectedBackgroundColor = System.Drawing.Color.SlateGray
        Me.spCreditosActualizarGuardar.HorizontalScrollBar.Renderer = EnhancedScrollBarRenderer6
        Me.spCreditosActualizarGuardar.HorizontalScrollBar.TabIndex = 10
        Me.spCreditosActualizarGuardar.Location = New System.Drawing.Point(703, 0)
        Me.spCreditosActualizarGuardar.Name = "spCreditosActualizarGuardar"
        Me.spCreditosActualizarGuardar.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.spCreditosActualizarGuardar_Sheet1})
        Me.spCreditosActualizarGuardar.Size = New System.Drawing.Size(325, 490)
        Me.spCreditosActualizarGuardar.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Seashell
        Me.spCreditosActualizarGuardar.TabIndex = 26
        Me.spCreditosActualizarGuardar.VerticalScrollBar.Buttons = New FarPoint.Win.Spread.FpScrollBarButtonCollection("BackwardLineButton,ThumbTrack,ForwardLineButton")
        Me.spCreditosActualizarGuardar.VerticalScrollBar.Name = ""
        EnhancedScrollBarRenderer7.ArrowColor = System.Drawing.Color.DarkSlateGray
        EnhancedScrollBarRenderer7.ArrowHoveredColor = System.Drawing.Color.DarkSlateGray
        EnhancedScrollBarRenderer7.ArrowSelectedColor = System.Drawing.Color.DarkSlateGray
        EnhancedScrollBarRenderer7.ButtonBackgroundColor = System.Drawing.Color.CadetBlue
        EnhancedScrollBarRenderer7.ButtonBorderColor = System.Drawing.Color.SlateGray
        EnhancedScrollBarRenderer7.ButtonHoveredBackgroundColor = System.Drawing.Color.SlateGray
        EnhancedScrollBarRenderer7.ButtonHoveredBorderColor = System.Drawing.Color.DarkGray
        EnhancedScrollBarRenderer7.ButtonSelectedBackgroundColor = System.Drawing.Color.DarkGray
        EnhancedScrollBarRenderer7.ButtonSelectedBorderColor = System.Drawing.Color.CadetBlue
        EnhancedScrollBarRenderer7.TrackBarBackgroundColor = System.Drawing.Color.CadetBlue
        EnhancedScrollBarRenderer7.TrackBarSelectedBackgroundColor = System.Drawing.Color.SlateGray
        Me.spCreditosActualizarGuardar.VerticalScrollBar.Renderer = EnhancedScrollBarRenderer7
        Me.spCreditosActualizarGuardar.VerticalScrollBar.TabIndex = 11
        Me.spCreditosActualizarGuardar.Visible = False
        '
        'spCreditosActualizarGuardar_Sheet1
        '
        Me.spCreditosActualizarGuardar_Sheet1.Reset()
        spCreditosActualizarGuardar_Sheet1.SheetName = "Sheet1"
        'Formulas and custom names must be loaded with R1C1 reference style
        Me.spCreditosActualizarGuardar_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
        Me.spCreditosActualizarGuardar_Sheet1.ColumnFooter.DefaultStyle.NoteIndicatorColor = System.Drawing.Color.Red
        Me.spCreditosActualizarGuardar_Sheet1.ColumnFooter.DefaultStyle.Parent = "ColumnHeaderSeashell"
        Me.spCreditosActualizarGuardar_Sheet1.ColumnFooterSheetCornerStyle.NoteIndicatorColor = System.Drawing.Color.Red
        Me.spCreditosActualizarGuardar_Sheet1.ColumnFooterSheetCornerStyle.Parent = "CornerSeashell"
        Me.spCreditosActualizarGuardar_Sheet1.ColumnHeader.DefaultStyle.NoteIndicatorColor = System.Drawing.Color.Red
        Me.spCreditosActualizarGuardar_Sheet1.ColumnHeader.DefaultStyle.Parent = "ColumnHeaderSeashell"
        Me.spCreditosActualizarGuardar_Sheet1.RowHeader.DefaultStyle.NoteIndicatorColor = System.Drawing.Color.Red
        Me.spCreditosActualizarGuardar_Sheet1.RowHeader.DefaultStyle.Parent = "RowHeaderSeashell"
        Me.spCreditosActualizarGuardar_Sheet1.SheetCornerStyle.NoteIndicatorColor = System.Drawing.Color.Red
        Me.spCreditosActualizarGuardar_Sheet1.SheetCornerStyle.Parent = "CornerSeashell"
        Me.spCreditosActualizarGuardar_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
        '
        'pnlPie
        '
        Me.pnlPie.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlPie.BackColor = System.Drawing.Color.White
        Me.pnlPie.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlPie.Controls.Add(Me.btnAyuda)
        Me.pnlPie.Controls.Add(Me.lblDescripcionTooltip)
        Me.pnlPie.Controls.Add(Me.btnEliminar)
        Me.pnlPie.Controls.Add(Me.btnSalir)
        Me.pnlPie.Controls.Add(Me.btnGuardar)
        Me.pnlPie.ForeColor = System.Drawing.Color.Black
        Me.pnlPie.Location = New System.Drawing.Point(0, 600)
        Me.pnlPie.Name = "pnlPie"
        Me.pnlPie.Size = New System.Drawing.Size(1039, 60)
        Me.pnlPie.TabIndex = 8
        '
        'btnAyuda
        '
        Me.btnAyuda.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnAyuda.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
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
        '
        'lblDescripcionTooltip
        '
        Me.lblDescripcionTooltip.AutoSize = True
        Me.lblDescripcionTooltip.BackColor = System.Drawing.Color.FromArgb(CType(CType(99, Byte), Integer), CType(CType(160, Byte), Integer), CType(CType(162, Byte), Integer))
        Me.lblDescripcionTooltip.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDescripcionTooltip.ForeColor = System.Drawing.Color.Black
        Me.lblDescripcionTooltip.Location = New System.Drawing.Point(101, 17)
        Me.lblDescripcionTooltip.Name = "lblDescripcionTooltip"
        Me.lblDescripcionTooltip.Size = New System.Drawing.Size(0, 31)
        Me.lblDescripcionTooltip.TabIndex = 4
        '
        'btnEliminar
        '
        Me.btnEliminar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnEliminar.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btnEliminar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnEliminar.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnEliminar.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.btnEliminar.FlatAppearance.BorderSize = 3
        Me.btnEliminar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.MediumAquamarine
        Me.btnEliminar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnEliminar.ForeColor = System.Drawing.Color.Black
        Me.btnEliminar.Image = CType(resources.GetObject("btnEliminar.Image"), System.Drawing.Image)
        Me.btnEliminar.Location = New System.Drawing.Point(851, 0)
        Me.btnEliminar.Margin = New System.Windows.Forms.Padding(0)
        Me.btnEliminar.Name = "btnEliminar"
        Me.btnEliminar.Size = New System.Drawing.Size(60, 60)
        Me.btnEliminar.TabIndex = 18
        Me.btnEliminar.UseVisualStyleBackColor = False
        '
        'btnSalir
        '
        Me.btnSalir.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSalir.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnSalir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnSalir.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnSalir.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.btnSalir.FlatAppearance.BorderSize = 3
        Me.btnSalir.FlatAppearance.MouseOverBackColor = System.Drawing.Color.MediumAquamarine
        Me.btnSalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSalir.ForeColor = System.Drawing.Color.Black
        Me.btnSalir.Image = CType(resources.GetObject("btnSalir.Image"), System.Drawing.Image)
        Me.btnSalir.Location = New System.Drawing.Point(977, 0)
        Me.btnSalir.Margin = New System.Windows.Forms.Padding(0)
        Me.btnSalir.Name = "btnSalir"
        Me.btnSalir.Size = New System.Drawing.Size(60, 60)
        Me.btnSalir.TabIndex = 2
        Me.btnSalir.UseVisualStyleBackColor = False
        '
        'btnGuardar
        '
        Me.btnGuardar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnGuardar.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btnGuardar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnGuardar.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnGuardar.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.btnGuardar.FlatAppearance.BorderSize = 3
        Me.btnGuardar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.MediumAquamarine
        Me.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnGuardar.ForeColor = System.Drawing.Color.Black
        Me.btnGuardar.Image = CType(resources.GetObject("btnGuardar.Image"), System.Drawing.Image)
        Me.btnGuardar.Location = New System.Drawing.Point(913, 0)
        Me.btnGuardar.Margin = New System.Windows.Forms.Padding(0)
        Me.btnGuardar.Name = "btnGuardar"
        Me.btnGuardar.Size = New System.Drawing.Size(60, 60)
        Me.btnGuardar.TabIndex = 17
        Me.btnGuardar.UseVisualStyleBackColor = False
        '
        'pnlEncabezado
        '
        Me.pnlEncabezado.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlEncabezado.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.pnlEncabezado.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlEncabezado.Controls.Add(Me.pbMarca)
        Me.pnlEncabezado.Controls.Add(Me.lblEncabezadoArea)
        Me.pnlEncabezado.Controls.Add(Me.lblEncabezadoUsuario)
        Me.pnlEncabezado.Controls.Add(Me.lblEncabezadoEmpresa)
        Me.pnlEncabezado.Controls.Add(Me.lblEncabezadoPrograma)
        Me.pnlEncabezado.ForeColor = System.Drawing.Color.White
        Me.pnlEncabezado.Location = New System.Drawing.Point(0, 0)
        Me.pnlEncabezado.Name = "pnlEncabezado"
        Me.pnlEncabezado.Size = New System.Drawing.Size(1039, 75)
        Me.pnlEncabezado.TabIndex = 7
        '
        'pbMarca
        '
        Me.pbMarca.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pbMarca.BackColor = System.Drawing.Color.Transparent
        Me.pbMarca.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pbMarca.Cursor = System.Windows.Forms.Cursors.Hand
        Me.pbMarca.Image = Global.ALMCreditos.My.Resources.Resources.Producido_por
        Me.pbMarca.ImageLocation = ""
        Me.pbMarca.Location = New System.Drawing.Point(962, 0)
        Me.pbMarca.Name = "pbMarca"
        Me.pbMarca.Size = New System.Drawing.Size(75, 75)
        Me.pbMarca.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbMarca.TabIndex = 7
        Me.pbMarca.TabStop = False
        '
        'lblEncabezadoArea
        '
        Me.lblEncabezadoArea.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblEncabezadoArea.AutoSize = True
        Me.lblEncabezadoArea.BackColor = System.Drawing.Color.Transparent
        Me.lblEncabezadoArea.Font = New System.Drawing.Font("Microsoft Sans Serif", 21.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEncabezadoArea.ForeColor = System.Drawing.Color.White
        Me.lblEncabezadoArea.Location = New System.Drawing.Point(604, 0)
        Me.lblEncabezadoArea.Name = "lblEncabezadoArea"
        Me.lblEncabezadoArea.Size = New System.Drawing.Size(0, 33)
        Me.lblEncabezadoArea.TabIndex = 5
        '
        'lblEncabezadoUsuario
        '
        Me.lblEncabezadoUsuario.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblEncabezadoUsuario.AutoSize = True
        Me.lblEncabezadoUsuario.BackColor = System.Drawing.Color.Transparent
        Me.lblEncabezadoUsuario.Font = New System.Drawing.Font("Microsoft Sans Serif", 21.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEncabezadoUsuario.ForeColor = System.Drawing.Color.White
        Me.lblEncabezadoUsuario.Location = New System.Drawing.Point(604, 35)
        Me.lblEncabezadoUsuario.Name = "lblEncabezadoUsuario"
        Me.lblEncabezadoUsuario.Size = New System.Drawing.Size(0, 33)
        Me.lblEncabezadoUsuario.TabIndex = 4
        '
        'lblEncabezadoEmpresa
        '
        Me.lblEncabezadoEmpresa.AutoSize = True
        Me.lblEncabezadoEmpresa.BackColor = System.Drawing.Color.Transparent
        Me.lblEncabezadoEmpresa.Font = New System.Drawing.Font("Microsoft Sans Serif", 21.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEncabezadoEmpresa.ForeColor = System.Drawing.Color.White
        Me.lblEncabezadoEmpresa.Location = New System.Drawing.Point(12, 35)
        Me.lblEncabezadoEmpresa.Name = "lblEncabezadoEmpresa"
        Me.lblEncabezadoEmpresa.Size = New System.Drawing.Size(0, 33)
        Me.lblEncabezadoEmpresa.TabIndex = 1
        '
        'lblEncabezadoPrograma
        '
        Me.lblEncabezadoPrograma.AutoSize = True
        Me.lblEncabezadoPrograma.BackColor = System.Drawing.Color.Transparent
        Me.lblEncabezadoPrograma.Font = New System.Drawing.Font("Microsoft Sans Serif", 21.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEncabezadoPrograma.ForeColor = System.Drawing.Color.White
        Me.lblEncabezadoPrograma.Location = New System.Drawing.Point(12, 0)
        Me.lblEncabezadoPrograma.Name = "lblEncabezadoPrograma"
        Me.lblEncabezadoPrograma.Size = New System.Drawing.Size(0, 33)
        Me.lblEncabezadoPrograma.TabIndex = 0
        '
        'temporizador
        '
        Me.temporizador.Interval = 1
        '
        'Principal
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1039, 661)
        Me.Controls.Add(Me.pnlContenido)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.Black
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Principal"
        Me.Text = "Almacén - Créditos"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlContenido.ResumeLayout(False)
        Me.pnlCuerpo.ResumeLayout(False)
        Me.tcPestanas.ResumeLayout(False)
        Me.tpCapturar.ResumeLayout(False)
        Me.pnlTotales.ResumeLayout(False)
        Me.pnlTotales.PerformLayout()
        Me.pnlCatalogos.ResumeLayout(False)
        Me.pnlCatalogos.PerformLayout()
        CType(Me.spCatalogos, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.spCatalogos_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlCapturaSuperior.ResumeLayout(False)
        Me.pnlCapturaSuperior.PerformLayout()
        CType(Me.spCreditosCapturar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.spCreditosCapturar_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpActualizar.ResumeLayout(False)
        Me.pnlFiltradoActualizar.ResumeLayout(False)
        Me.pnlFiltradoActualizar.PerformLayout()
        CType(Me.spCreditosActualizarSeleccionar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.spCreditosActualizarSeleccionar_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.spCreditosActualizarGuardar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.spCreditosActualizarGuardar_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlPie.ResumeLayout(False)
        Me.pnlPie.PerformLayout()
        Me.pnlEncabezado.ResumeLayout(False)
        Me.pnlEncabezado.PerformLayout()
        CType(Me.pbMarca, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents pnlContenido As System.Windows.Forms.Panel
    Private WithEvents pnlCuerpo As System.Windows.Forms.Panel
    Private WithEvents pnlPie As System.Windows.Forms.Panel
    Private WithEvents btnSalir As System.Windows.Forms.Button
    Private WithEvents pnlEncabezado As System.Windows.Forms.Panel
    Private WithEvents lblEncabezadoEmpresa As System.Windows.Forms.Label
    Private WithEvents lblEncabezadoPrograma As System.Windows.Forms.Label
    Friend WithEvents spCreditosCapturar As FarPoint.Win.Spread.FpSpread
    Friend WithEvents spCreditosCapturar_Sheet1 As FarPoint.Win.Spread.SheetView
    Private WithEvents lblEncabezadoArea As System.Windows.Forms.Label
    Private WithEvents lblEncabezadoUsuario As System.Windows.Forms.Label
    Private WithEvents btnEliminar As System.Windows.Forms.Button
    Private WithEvents btnGuardar As System.Windows.Forms.Button
    Friend WithEvents lblDescripcionTooltip As System.Windows.Forms.Label
    Private WithEvents spCatalogos As FarPoint.Win.Spread.FpSpread
    Private WithEvents spCatalogos_Sheet1 As FarPoint.Win.Spread.SheetView
    Friend WithEvents temporizador As System.Windows.Forms.Timer
    Private WithEvents btnAyuda As System.Windows.Forms.Button
    Friend WithEvents pnlCapturaSuperior As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dtpFecha As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtId As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents chkMantenerDatos As System.Windows.Forms.CheckBox
    Friend WithEvents cbAlmacenes As System.Windows.Forms.ComboBox
    Friend WithEvents pnlCatalogos As System.Windows.Forms.Panel
    Friend WithEvents btnIdAnterior As System.Windows.Forms.Button
    Friend WithEvents btnIdSiguiente As System.Windows.Forms.Button
    Private WithEvents btnMostrarOcultar As System.Windows.Forms.Button
    Friend WithEvents txtBuscarCatalogo As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents cbClientes As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents chkMostrarDetallado As System.Windows.Forms.CheckBox
    Friend WithEvents tcPestanas As System.Windows.Forms.TabControl
    Friend WithEvents tpCapturar As System.Windows.Forms.TabPage
    Friend WithEvents tpActualizar As System.Windows.Forms.TabPage
    Friend WithEvents pnlFiltradoActualizar As System.Windows.Forms.Panel
    Private WithEvents btnMostrarOcultarActualizar As System.Windows.Forms.Button
    Friend WithEvents spCreditosActualizarSeleccionar As FarPoint.Win.Spread.FpSpread
    Friend WithEvents spCreditosActualizarSeleccionar_Sheet1 As FarPoint.Win.Spread.SheetView
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents chkFecha As System.Windows.Forms.CheckBox
    Friend WithEvents dtpFechaFinal As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpFechaInicial As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnGenerar As System.Windows.Forms.Button
    Friend WithEvents chkMostrarDetalladoActualizar As System.Windows.Forms.CheckBox
    Friend WithEvents pbMarca As System.Windows.Forms.PictureBox
    Friend WithEvents pnlTotales As System.Windows.Forms.Panel
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtTotal As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtDescuento As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtSubTotal As System.Windows.Forms.TextBox
    Friend WithEvents cbClientesActualizar As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cbMetodosPagos As System.Windows.Forms.ComboBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents spCreditosActualizarGuardar As FarPoint.Win.Spread.FpSpread
    Friend WithEvents spCreditosActualizarGuardar_Sheet1 As FarPoint.Win.Spread.SheetView
End Class
