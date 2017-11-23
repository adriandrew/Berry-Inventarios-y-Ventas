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
        Dim EnhancedScrollBarRenderer1 As FarPoint.Win.Spread.EnhancedScrollBarRenderer = New FarPoint.Win.Spread.EnhancedScrollBarRenderer()
        Dim EnhancedScrollBarRenderer2 As FarPoint.Win.Spread.EnhancedScrollBarRenderer = New FarPoint.Win.Spread.EnhancedScrollBarRenderer()
        Dim EnhancedScrollBarRenderer3 As FarPoint.Win.Spread.EnhancedScrollBarRenderer = New FarPoint.Win.Spread.EnhancedScrollBarRenderer()
        Dim EnhancedScrollBarRenderer4 As FarPoint.Win.Spread.EnhancedScrollBarRenderer = New FarPoint.Win.Spread.EnhancedScrollBarRenderer()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Principal))
        Dim EnhancedScrollBarRenderer5 As FarPoint.Win.Spread.EnhancedScrollBarRenderer = New FarPoint.Win.Spread.EnhancedScrollBarRenderer()
        Dim NamedStyle1 As FarPoint.Win.Spread.NamedStyle = New FarPoint.Win.Spread.NamedStyle("Style1")
        Dim NamedStyle2 As FarPoint.Win.Spread.NamedStyle = New FarPoint.Win.Spread.NamedStyle("DataAreaGrayscale")
        Dim GeneralCellType1 As FarPoint.Win.Spread.CellType.GeneralCellType = New FarPoint.Win.Spread.CellType.GeneralCellType()
        Dim NamedStyle3 As FarPoint.Win.Spread.NamedStyle = New FarPoint.Win.Spread.NamedStyle("ColumnHeaderMidnight")
        Dim NamedStyle4 As FarPoint.Win.Spread.NamedStyle = New FarPoint.Win.Spread.NamedStyle("RowHeaderMidnight")
        Dim NamedStyle5 As FarPoint.Win.Spread.NamedStyle = New FarPoint.Win.Spread.NamedStyle("CornerMidnight")
        Dim EnhancedCornerRenderer1 As FarPoint.Win.Spread.CellType.EnhancedCornerRenderer = New FarPoint.Win.Spread.CellType.EnhancedCornerRenderer()
        Dim NamedStyle6 As FarPoint.Win.Spread.NamedStyle = New FarPoint.Win.Spread.NamedStyle("DataAreaMidnght")
        Dim GeneralCellType2 As FarPoint.Win.Spread.CellType.GeneralCellType = New FarPoint.Win.Spread.CellType.GeneralCellType()
        Dim SpreadSkin1 As FarPoint.Win.Spread.SpreadSkin = New FarPoint.Win.Spread.SpreadSkin()
        Dim EnhancedFocusIndicatorRenderer1 As FarPoint.Win.Spread.EnhancedFocusIndicatorRenderer = New FarPoint.Win.Spread.EnhancedFocusIndicatorRenderer()
        Dim EnhancedInterfaceRenderer1 As FarPoint.Win.Spread.EnhancedInterfaceRenderer = New FarPoint.Win.Spread.EnhancedInterfaceRenderer()
        Dim EnhancedScrollBarRenderer6 As FarPoint.Win.Spread.EnhancedScrollBarRenderer = New FarPoint.Win.Spread.EnhancedScrollBarRenderer()
        Dim EnhancedScrollBarRenderer7 As FarPoint.Win.Spread.EnhancedScrollBarRenderer = New FarPoint.Win.Spread.EnhancedScrollBarRenderer()
        Dim EnhancedScrollBarRenderer8 As FarPoint.Win.Spread.EnhancedScrollBarRenderer = New FarPoint.Win.Spread.EnhancedScrollBarRenderer()
        Dim EnhancedScrollBarRenderer9 As FarPoint.Win.Spread.EnhancedScrollBarRenderer = New FarPoint.Win.Spread.EnhancedScrollBarRenderer()
        Me.temporizador = New System.Windows.Forms.Timer(Me.components)
        Me.pdRecibo = New System.Drawing.Printing.PrintDocument()
        Me.pdVale = New System.Drawing.Printing.PrintDocument()
        Me.pnlContenido = New System.Windows.Forms.Panel()
        Me.pnlCuerpo = New System.Windows.Forms.Panel()
        Me.spListadosOtros = New FarPoint.Win.Spread.FpSpread()
        Me.spListadosOtros_Sheet1 = New FarPoint.Win.Spread.SheetView()
        Me.spVales = New FarPoint.Win.Spread.FpSpread()
        Me.spVales_Sheet1 = New FarPoint.Win.Spread.SheetView()
        Me.pnlTotales = New System.Windows.Forms.Panel()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtTotal = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtDescuento = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtSubTotal = New System.Windows.Forms.TextBox()
        Me.pnlCapturaSuperior = New System.Windows.Forms.Panel()
        Me.txtImporteVale2 = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtImporteVale1 = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtImporteCambio = New System.Windows.Forms.TextBox()
        Me.txtImportePagado = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cbMetodosPagos = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.chkMostrarDetallado = New System.Windows.Forms.CheckBox()
        Me.cbClientes = New System.Windows.Forms.ComboBox()
        Me.btnMostrarOcultar = New System.Windows.Forms.Button()
        Me.btnIdSiguiente = New System.Windows.Forms.Button()
        Me.btnIdAnterior = New System.Windows.Forms.Button()
        Me.chkConservarDatos = New System.Windows.Forms.CheckBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtId = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.dtpFecha = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.pnlCatalogos = New System.Windows.Forms.Panel()
        Me.txtBuscarCatalogo = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.spCatalogos = New FarPoint.Win.Spread.FpSpread()
        Me.spCatalogos_Sheet1 = New FarPoint.Win.Spread.SheetView()
        Me.spVentas = New FarPoint.Win.Spread.FpSpread()
        Me.spVentas_Sheet1 = New FarPoint.Win.Spread.SheetView()
        Me.pnlPie = New System.Windows.Forms.Panel()
        Me.btnImprimir = New System.Windows.Forms.Button()
        Me.btnConfigurarImpresion = New System.Windows.Forms.Button()
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
        Me.pnlContenido.SuspendLayout()
        Me.pnlCuerpo.SuspendLayout()
        CType(Me.spListadosOtros, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.spListadosOtros_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.spVales, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.spVales_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlTotales.SuspendLayout()
        Me.pnlCapturaSuperior.SuspendLayout()
        Me.pnlCatalogos.SuspendLayout()
        CType(Me.spCatalogos, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.spCatalogos_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.spVentas, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.spVentas_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlPie.SuspendLayout()
        Me.pnlEncabezado.SuspendLayout()
        CType(Me.pbMarca, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'temporizador
        '
        Me.temporizador.Interval = 1
        '
        'pdRecibo
        '
        '
        'pdVale
        '
        '
        'pnlContenido
        '
        Me.pnlContenido.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlContenido.BackColor = System.Drawing.Color.DarkSlateGray
        Me.pnlContenido.BackgroundImage = Global.ALMVentas.My.Resources.Resources.Logo3
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
        Me.pnlCuerpo.BackColor = System.Drawing.Color.Transparent
        Me.pnlCuerpo.Controls.Add(Me.spListadosOtros)
        Me.pnlCuerpo.Controls.Add(Me.spVales)
        Me.pnlCuerpo.Controls.Add(Me.pnlTotales)
        Me.pnlCuerpo.Controls.Add(Me.pnlCapturaSuperior)
        Me.pnlCuerpo.Controls.Add(Me.pnlCatalogos)
        Me.pnlCuerpo.Controls.Add(Me.spVentas)
        Me.pnlCuerpo.Location = New System.Drawing.Point(0, 77)
        Me.pnlCuerpo.Name = "pnlCuerpo"
        Me.pnlCuerpo.Size = New System.Drawing.Size(1039, 521)
        Me.pnlCuerpo.TabIndex = 9
        '
        'spListadosOtros
        '
        Me.spListadosOtros.AccessibleDescription = ""
        Me.spListadosOtros.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.spListadosOtros.BackColor = System.Drawing.Color.White
        Me.spListadosOtros.HorizontalScrollBar.Buttons = New FarPoint.Win.Spread.FpScrollBarButtonCollection("BackwardLineButton,ThumbTrack,ForwardLineButton")
        Me.spListadosOtros.HorizontalScrollBar.Name = ""
        EnhancedScrollBarRenderer1.ArrowColor = System.Drawing.Color.Crimson
        EnhancedScrollBarRenderer1.ArrowHoveredColor = System.Drawing.Color.Crimson
        EnhancedScrollBarRenderer1.ArrowSelectedColor = System.Drawing.Color.Crimson
        EnhancedScrollBarRenderer1.ButtonBackgroundColor = System.Drawing.Color.PaleVioletRed
        EnhancedScrollBarRenderer1.ButtonBorderColor = System.Drawing.Color.DeepPink
        EnhancedScrollBarRenderer1.ButtonHoveredBackgroundColor = System.Drawing.Color.DeepPink
        EnhancedScrollBarRenderer1.ButtonHoveredBorderColor = System.Drawing.Color.MediumVioletRed
        EnhancedScrollBarRenderer1.ButtonSelectedBackgroundColor = System.Drawing.Color.MediumVioletRed
        EnhancedScrollBarRenderer1.ButtonSelectedBorderColor = System.Drawing.Color.PaleVioletRed
        EnhancedScrollBarRenderer1.TrackBarBackgroundColor = System.Drawing.Color.LightPink
        EnhancedScrollBarRenderer1.TrackBarSelectedBackgroundColor = System.Drawing.Color.DeepPink
        Me.spListadosOtros.HorizontalScrollBar.Renderer = EnhancedScrollBarRenderer1
        Me.spListadosOtros.HorizontalScrollBar.TabIndex = 14
        Me.spListadosOtros.Location = New System.Drawing.Point(658, 0)
        Me.spListadosOtros.Name = "spListadosOtros"
        Me.spListadosOtros.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.spListadosOtros_Sheet1})
        Me.spListadosOtros.Size = New System.Drawing.Size(260, 150)
        Me.spListadosOtros.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Rose
        Me.spListadosOtros.TabIndex = 27
        Me.spListadosOtros.VerticalScrollBar.Buttons = New FarPoint.Win.Spread.FpScrollBarButtonCollection("BackwardLineButton,ThumbTrack,ForwardLineButton")
        Me.spListadosOtros.VerticalScrollBar.Name = ""
        EnhancedScrollBarRenderer2.ArrowColor = System.Drawing.Color.Crimson
        EnhancedScrollBarRenderer2.ArrowHoveredColor = System.Drawing.Color.Crimson
        EnhancedScrollBarRenderer2.ArrowSelectedColor = System.Drawing.Color.Crimson
        EnhancedScrollBarRenderer2.ButtonBackgroundColor = System.Drawing.Color.PaleVioletRed
        EnhancedScrollBarRenderer2.ButtonBorderColor = System.Drawing.Color.DeepPink
        EnhancedScrollBarRenderer2.ButtonHoveredBackgroundColor = System.Drawing.Color.DeepPink
        EnhancedScrollBarRenderer2.ButtonHoveredBorderColor = System.Drawing.Color.MediumVioletRed
        EnhancedScrollBarRenderer2.ButtonSelectedBackgroundColor = System.Drawing.Color.MediumVioletRed
        EnhancedScrollBarRenderer2.ButtonSelectedBorderColor = System.Drawing.Color.PaleVioletRed
        EnhancedScrollBarRenderer2.TrackBarBackgroundColor = System.Drawing.Color.LightPink
        EnhancedScrollBarRenderer2.TrackBarSelectedBackgroundColor = System.Drawing.Color.DeepPink
        Me.spListadosOtros.VerticalScrollBar.Renderer = EnhancedScrollBarRenderer2
        Me.spListadosOtros.VerticalScrollBar.TabIndex = 15
        Me.spListadosOtros.Visible = False
        '
        'spListadosOtros_Sheet1
        '
        Me.spListadosOtros_Sheet1.Reset()
        spListadosOtros_Sheet1.SheetName = "Sheet1"
        'Formulas and custom names must be loaded with R1C1 reference style
        Me.spListadosOtros_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
        Me.spListadosOtros_Sheet1.ColumnFooter.DefaultStyle.NoteIndicatorColor = System.Drawing.Color.Red
        Me.spListadosOtros_Sheet1.ColumnFooter.DefaultStyle.Parent = "ColumnHeaderRose"
        Me.spListadosOtros_Sheet1.ColumnFooterSheetCornerStyle.NoteIndicatorColor = System.Drawing.Color.Red
        Me.spListadosOtros_Sheet1.ColumnFooterSheetCornerStyle.Parent = "CornerRose"
        Me.spListadosOtros_Sheet1.ColumnHeader.DefaultStyle.NoteIndicatorColor = System.Drawing.Color.Red
        Me.spListadosOtros_Sheet1.ColumnHeader.DefaultStyle.Parent = "ColumnHeaderRose"
        Me.spListadosOtros_Sheet1.RowHeader.DefaultStyle.NoteIndicatorColor = System.Drawing.Color.Red
        Me.spListadosOtros_Sheet1.RowHeader.DefaultStyle.Parent = "RowHeaderRose"
        Me.spListadosOtros_Sheet1.SheetCornerStyle.NoteIndicatorColor = System.Drawing.Color.Red
        Me.spListadosOtros_Sheet1.SheetCornerStyle.Parent = "CornerRose"
        Me.spListadosOtros_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
        '
        'spVales
        '
        Me.spVales.AccessibleDescription = ""
        Me.spVales.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.spVales.BackColor = System.Drawing.Color.White
        Me.spVales.HorizontalScrollBar.Buttons = New FarPoint.Win.Spread.FpScrollBarButtonCollection("BackwardLineButton,ThumbTrack,ForwardLineButton")
        Me.spVales.HorizontalScrollBar.Name = ""
        EnhancedScrollBarRenderer3.ArrowColor = System.Drawing.Color.DarkSlateGray
        EnhancedScrollBarRenderer3.ArrowHoveredColor = System.Drawing.Color.DarkSlateGray
        EnhancedScrollBarRenderer3.ArrowSelectedColor = System.Drawing.Color.DarkSlateGray
        EnhancedScrollBarRenderer3.ButtonBackgroundColor = System.Drawing.Color.CadetBlue
        EnhancedScrollBarRenderer3.ButtonBorderColor = System.Drawing.Color.SlateGray
        EnhancedScrollBarRenderer3.ButtonHoveredBackgroundColor = System.Drawing.Color.SlateGray
        EnhancedScrollBarRenderer3.ButtonHoveredBorderColor = System.Drawing.Color.DarkGray
        EnhancedScrollBarRenderer3.ButtonSelectedBackgroundColor = System.Drawing.Color.DarkGray
        EnhancedScrollBarRenderer3.ButtonSelectedBorderColor = System.Drawing.Color.CadetBlue
        EnhancedScrollBarRenderer3.TrackBarBackgroundColor = System.Drawing.Color.CadetBlue
        EnhancedScrollBarRenderer3.TrackBarSelectedBackgroundColor = System.Drawing.Color.SlateGray
        Me.spVales.HorizontalScrollBar.Renderer = EnhancedScrollBarRenderer3
        Me.spVales.HorizontalScrollBar.TabIndex = 10
        Me.spVales.Location = New System.Drawing.Point(335, 416)
        Me.spVales.Name = "spVales"
        Me.spVales.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.spVales_Sheet1})
        Me.spVales.Size = New System.Drawing.Size(454, 103)
        Me.spVales.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Seashell
        Me.spVales.TabIndex = 26
        Me.spVales.VerticalScrollBar.Buttons = New FarPoint.Win.Spread.FpScrollBarButtonCollection("BackwardLineButton,ThumbTrack,ForwardLineButton")
        Me.spVales.VerticalScrollBar.Name = ""
        EnhancedScrollBarRenderer4.ArrowColor = System.Drawing.Color.DarkSlateGray
        EnhancedScrollBarRenderer4.ArrowHoveredColor = System.Drawing.Color.DarkSlateGray
        EnhancedScrollBarRenderer4.ArrowSelectedColor = System.Drawing.Color.DarkSlateGray
        EnhancedScrollBarRenderer4.ButtonBackgroundColor = System.Drawing.Color.CadetBlue
        EnhancedScrollBarRenderer4.ButtonBorderColor = System.Drawing.Color.SlateGray
        EnhancedScrollBarRenderer4.ButtonHoveredBackgroundColor = System.Drawing.Color.SlateGray
        EnhancedScrollBarRenderer4.ButtonHoveredBorderColor = System.Drawing.Color.DarkGray
        EnhancedScrollBarRenderer4.ButtonSelectedBackgroundColor = System.Drawing.Color.DarkGray
        EnhancedScrollBarRenderer4.ButtonSelectedBorderColor = System.Drawing.Color.CadetBlue
        EnhancedScrollBarRenderer4.TrackBarBackgroundColor = System.Drawing.Color.CadetBlue
        EnhancedScrollBarRenderer4.TrackBarSelectedBackgroundColor = System.Drawing.Color.SlateGray
        Me.spVales.VerticalScrollBar.Renderer = EnhancedScrollBarRenderer4
        Me.spVales.VerticalScrollBar.TabIndex = 11
        '
        'spVales_Sheet1
        '
        Me.spVales_Sheet1.Reset()
        spVales_Sheet1.SheetName = "Sheet1"
        'Formulas and custom names must be loaded with R1C1 reference style
        Me.spVales_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
        Me.spVales_Sheet1.ColumnFooter.DefaultStyle.NoteIndicatorColor = System.Drawing.Color.Red
        Me.spVales_Sheet1.ColumnFooter.DefaultStyle.Parent = "ColumnHeaderSeashell"
        Me.spVales_Sheet1.ColumnFooterSheetCornerStyle.NoteIndicatorColor = System.Drawing.Color.Red
        Me.spVales_Sheet1.ColumnFooterSheetCornerStyle.Parent = "CornerSeashell"
        Me.spVales_Sheet1.ColumnHeader.DefaultStyle.NoteIndicatorColor = System.Drawing.Color.Red
        Me.spVales_Sheet1.ColumnHeader.DefaultStyle.Parent = "ColumnHeaderSeashell"
        Me.spVales_Sheet1.RowHeader.DefaultStyle.NoteIndicatorColor = System.Drawing.Color.Red
        Me.spVales_Sheet1.RowHeader.DefaultStyle.Parent = "RowHeaderSeashell"
        Me.spVales_Sheet1.SheetCornerStyle.NoteIndicatorColor = System.Drawing.Color.Red
        Me.spVales_Sheet1.SheetCornerStyle.Parent = "CornerSeashell"
        Me.spVales_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
        '
        'pnlTotales
        '
        Me.pnlTotales.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlTotales.BackColor = System.Drawing.Color.White
        Me.pnlTotales.Controls.Add(Me.Label12)
        Me.pnlTotales.Controls.Add(Me.txtTotal)
        Me.pnlTotales.Controls.Add(Me.Label11)
        Me.pnlTotales.Controls.Add(Me.txtDescuento)
        Me.pnlTotales.Controls.Add(Me.Label9)
        Me.pnlTotales.Controls.Add(Me.txtSubTotal)
        Me.pnlTotales.Location = New System.Drawing.Point(790, 416)
        Me.pnlTotales.Name = "pnlTotales"
        Me.pnlTotales.Size = New System.Drawing.Size(249, 103)
        Me.pnlTotales.TabIndex = 25
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(52, 70)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(51, 13)
        Me.Label12.TabIndex = 92
        Me.Label12.Text = "TOTAL:"
        '
        'txtTotal
        '
        Me.txtTotal.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtTotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotal.ForeColor = System.Drawing.Color.Black
        Me.txtTotal.Location = New System.Drawing.Point(105, 66)
        Me.txtTotal.Name = "txtTotal"
        Me.txtTotal.ReadOnly = True
        Me.txtTotal.Size = New System.Drawing.Size(123, 20)
        Me.txtTotal.TabIndex = 91
        Me.txtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(16, 46)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(87, 13)
        Me.Label11.TabIndex = 90
        Me.Label11.Text = "DESCUENTO:"
        '
        'txtDescuento
        '
        Me.txtDescuento.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtDescuento.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDescuento.ForeColor = System.Drawing.Color.Black
        Me.txtDescuento.Location = New System.Drawing.Point(105, 42)
        Me.txtDescuento.Name = "txtDescuento"
        Me.txtDescuento.ReadOnly = True
        Me.txtDescuento.Size = New System.Drawing.Size(123, 20)
        Me.txtDescuento.TabIndex = 89
        Me.txtDescuento.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(27, 22)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(76, 13)
        Me.Label9.TabIndex = 88
        Me.Label9.Text = "SUBTOTAL:"
        '
        'txtSubTotal
        '
        Me.txtSubTotal.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSubTotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSubTotal.ForeColor = System.Drawing.Color.Black
        Me.txtSubTotal.Location = New System.Drawing.Point(105, 18)
        Me.txtSubTotal.Name = "txtSubTotal"
        Me.txtSubTotal.ReadOnly = True
        Me.txtSubTotal.Size = New System.Drawing.Size(123, 20)
        Me.txtSubTotal.TabIndex = 87
        Me.txtSubTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'pnlCapturaSuperior
        '
        Me.pnlCapturaSuperior.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.pnlCapturaSuperior.AutoScroll = True
        Me.pnlCapturaSuperior.BackColor = System.Drawing.Color.White
        Me.pnlCapturaSuperior.Controls.Add(Me.txtImporteVale2)
        Me.pnlCapturaSuperior.Controls.Add(Me.Label7)
        Me.pnlCapturaSuperior.Controls.Add(Me.txtImporteVale1)
        Me.pnlCapturaSuperior.Controls.Add(Me.Label6)
        Me.pnlCapturaSuperior.Controls.Add(Me.Label5)
        Me.pnlCapturaSuperior.Controls.Add(Me.txtImporteCambio)
        Me.pnlCapturaSuperior.Controls.Add(Me.txtImportePagado)
        Me.pnlCapturaSuperior.Controls.Add(Me.Label4)
        Me.pnlCapturaSuperior.Controls.Add(Me.cbMetodosPagos)
        Me.pnlCapturaSuperior.Controls.Add(Me.Label1)
        Me.pnlCapturaSuperior.Controls.Add(Me.chkMostrarDetallado)
        Me.pnlCapturaSuperior.Controls.Add(Me.cbClientes)
        Me.pnlCapturaSuperior.Controls.Add(Me.btnMostrarOcultar)
        Me.pnlCapturaSuperior.Controls.Add(Me.btnIdSiguiente)
        Me.pnlCapturaSuperior.Controls.Add(Me.btnIdAnterior)
        Me.pnlCapturaSuperior.Controls.Add(Me.chkConservarDatos)
        Me.pnlCapturaSuperior.Controls.Add(Me.Label8)
        Me.pnlCapturaSuperior.Controls.Add(Me.txtId)
        Me.pnlCapturaSuperior.Controls.Add(Me.Label3)
        Me.pnlCapturaSuperior.Controls.Add(Me.dtpFecha)
        Me.pnlCapturaSuperior.Controls.Add(Me.Label2)
        Me.pnlCapturaSuperior.Location = New System.Drawing.Point(0, 0)
        Me.pnlCapturaSuperior.Name = "pnlCapturaSuperior"
        Me.pnlCapturaSuperior.Size = New System.Drawing.Size(335, 521)
        Me.pnlCapturaSuperior.TabIndex = 23
        '
        'txtImporteVale2
        '
        Me.txtImporteVale2.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtImporteVale2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtImporteVale2.ForeColor = System.Drawing.Color.Black
        Me.txtImporteVale2.Location = New System.Drawing.Point(112, 156)
        Me.txtImporteVale2.Name = "txtImporteVale2"
        Me.txtImporteVale2.ReadOnly = True
        Me.txtImporteVale2.Size = New System.Drawing.Size(177, 20)
        Me.txtImporteVale2.TabIndex = 90
        Me.txtImporteVale2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(-2, 159)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(113, 13)
        Me.Label7.TabIndex = 89
        Me.Label7.Text = "IMPORTE VALE 2:"
        '
        'txtImporteVale1
        '
        Me.txtImporteVale1.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtImporteVale1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtImporteVale1.ForeColor = System.Drawing.Color.Black
        Me.txtImporteVale1.Location = New System.Drawing.Point(112, 128)
        Me.txtImporteVale1.Name = "txtImporteVale1"
        Me.txtImporteVale1.ReadOnly = True
        Me.txtImporteVale1.Size = New System.Drawing.Size(177, 20)
        Me.txtImporteVale1.TabIndex = 88
        Me.txtImporteVale1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(-2, 131)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(113, 13)
        Me.Label6.TabIndex = 87
        Me.Label6.Text = "IMPORTE VALE 1:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(52, 215)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(58, 13)
        Me.Label5.TabIndex = 86
        Me.Label5.Text = "CAMBIO:"
        '
        'txtImporteCambio
        '
        Me.txtImporteCambio.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtImporteCambio.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtImporteCambio.ForeColor = System.Drawing.Color.Black
        Me.txtImporteCambio.Location = New System.Drawing.Point(112, 212)
        Me.txtImporteCambio.Name = "txtImporteCambio"
        Me.txtImporteCambio.ReadOnly = True
        Me.txtImporteCambio.Size = New System.Drawing.Size(177, 20)
        Me.txtImporteCambio.TabIndex = 85
        Me.txtImporteCambio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtImportePagado
        '
        Me.txtImportePagado.BackColor = System.Drawing.Color.White
        Me.txtImportePagado.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtImportePagado.ForeColor = System.Drawing.Color.Black
        Me.txtImportePagado.Location = New System.Drawing.Point(112, 184)
        Me.txtImportePagado.Name = "txtImportePagado"
        Me.txtImportePagado.Size = New System.Drawing.Size(177, 20)
        Me.txtImportePagado.TabIndex = 84
        Me.txtImportePagado.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(5, 187)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(105, 13)
        Me.Label4.TabIndex = 83
        Me.Label4.Text = "IMPORTE PAGO:"
        '
        'cbMetodosPagos
        '
        Me.cbMetodosPagos.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cbMetodosPagos.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cbMetodosPagos.BackColor = System.Drawing.Color.White
        Me.cbMetodosPagos.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbMetodosPagos.ForeColor = System.Drawing.Color.Black
        Me.cbMetodosPagos.FormattingEnabled = True
        Me.cbMetodosPagos.Location = New System.Drawing.Point(112, 98)
        Me.cbMetodosPagos.Name = "cbMetodosPagos"
        Me.cbMetodosPagos.Size = New System.Drawing.Size(177, 21)
        Me.cbMetodosPagos.TabIndex = 82
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(0, 102)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(111, 13)
        Me.Label1.TabIndex = 81
        Me.Label1.Text = "MÉTODO PAGO: *"
        '
        'chkMostrarDetallado
        '
        Me.chkMostrarDetallado.AutoSize = True
        Me.chkMostrarDetallado.Cursor = System.Windows.Forms.Cursors.Hand
        Me.chkMostrarDetallado.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMostrarDetallado.ForeColor = System.Drawing.Color.Black
        Me.chkMostrarDetallado.Location = New System.Drawing.Point(112, 259)
        Me.chkMostrarDetallado.Name = "chkMostrarDetallado"
        Me.chkMostrarDetallado.Size = New System.Drawing.Size(126, 17)
        Me.chkMostrarDetallado.TabIndex = 80
        Me.chkMostrarDetallado.Text = "Mostrar Detallado"
        Me.chkMostrarDetallado.UseVisualStyleBackColor = True
        '
        'cbClientes
        '
        Me.cbClientes.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cbClientes.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cbClientes.BackColor = System.Drawing.Color.White
        Me.cbClientes.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbClientes.ForeColor = System.Drawing.Color.Black
        Me.cbClientes.FormattingEnabled = True
        Me.cbClientes.Location = New System.Drawing.Point(74, 68)
        Me.cbClientes.Name = "cbClientes"
        Me.cbClientes.Size = New System.Drawing.Size(215, 21)
        Me.cbClientes.TabIndex = 79
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
        Me.btnMostrarOcultar.Location = New System.Drawing.Point(295, 0)
        Me.btnMostrarOcultar.Margin = New System.Windows.Forms.Padding(0)
        Me.btnMostrarOcultar.Name = "btnMostrarOcultar"
        Me.btnMostrarOcultar.Size = New System.Drawing.Size(40, 38)
        Me.btnMostrarOcultar.TabIndex = 78
        Me.btnMostrarOcultar.UseVisualStyleBackColor = False
        '
        'btnIdSiguiente
        '
        Me.btnIdSiguiente.BackColor = System.Drawing.Color.FromArgb(CType(CType(99, Byte), Integer), CType(CType(160, Byte), Integer), CType(CType(162, Byte), Integer))
        Me.btnIdSiguiente.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnIdSiguiente.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnIdSiguiente.Location = New System.Drawing.Point(149, 9)
        Me.btnIdSiguiente.Name = "btnIdSiguiente"
        Me.btnIdSiguiente.Size = New System.Drawing.Size(25, 28)
        Me.btnIdSiguiente.TabIndex = 23
        Me.btnIdSiguiente.Text = ">"
        Me.btnIdSiguiente.UseVisualStyleBackColor = False
        '
        'btnIdAnterior
        '
        Me.btnIdAnterior.BackColor = System.Drawing.Color.FromArgb(CType(CType(99, Byte), Integer), CType(CType(160, Byte), Integer), CType(CType(162, Byte), Integer))
        Me.btnIdAnterior.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnIdAnterior.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnIdAnterior.Location = New System.Drawing.Point(125, 9)
        Me.btnIdAnterior.Name = "btnIdAnterior"
        Me.btnIdAnterior.Size = New System.Drawing.Size(25, 28)
        Me.btnIdAnterior.TabIndex = 22
        Me.btnIdAnterior.Text = "<"
        Me.btnIdAnterior.UseVisualStyleBackColor = False
        '
        'chkConservarDatos
        '
        Me.chkConservarDatos.AutoSize = True
        Me.chkConservarDatos.Checked = True
        Me.chkConservarDatos.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkConservarDatos.Cursor = System.Windows.Forms.Cursors.Hand
        Me.chkConservarDatos.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkConservarDatos.ForeColor = System.Drawing.Color.Black
        Me.chkConservarDatos.Location = New System.Drawing.Point(112, 238)
        Me.chkConservarDatos.Name = "chkConservarDatos"
        Me.chkConservarDatos.Size = New System.Drawing.Size(180, 17)
        Me.chkConservarDatos.TabIndex = 18
        Me.chkConservarDatos.Text = "Mantener Datos Al Guardar"
        Me.chkConservarDatos.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(0, 72)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(72, 13)
        Me.Label8.TabIndex = 15
        Me.Label8.Text = "CLIENTE: *"
        '
        'txtId
        '
        Me.txtId.BackColor = System.Drawing.Color.White
        Me.txtId.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtId.ForeColor = System.Drawing.Color.Black
        Me.txtId.Location = New System.Drawing.Point(74, 12)
        Me.txtId.Name = "txtId"
        Me.txtId.Size = New System.Drawing.Size(50, 20)
        Me.txtId.TabIndex = 6
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(34, 16)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(38, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "NO: *"
        '
        'dtpFecha
        '
        Me.dtpFecha.CalendarForeColor = System.Drawing.Color.Black
        Me.dtpFecha.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtpFecha.Cursor = System.Windows.Forms.Cursors.Hand
        Me.dtpFecha.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpFecha.Location = New System.Drawing.Point(74, 40)
        Me.dtpFecha.Name = "dtpFecha"
        Me.dtpFecha.Size = New System.Drawing.Size(215, 20)
        Me.dtpFecha.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(12, 44)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(60, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "FECHA: *"
        '
        'pnlCatalogos
        '
        Me.pnlCatalogos.BackColor = System.Drawing.Color.Indigo
        Me.pnlCatalogos.Controls.Add(Me.txtBuscarCatalogo)
        Me.pnlCatalogos.Controls.Add(Me.Label10)
        Me.pnlCatalogos.Controls.Add(Me.spCatalogos)
        Me.pnlCatalogos.Location = New System.Drawing.Point(392, 0)
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
        Me.txtBuscarCatalogo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBuscarCatalogo.ForeColor = System.Drawing.Color.Black
        Me.txtBuscarCatalogo.Location = New System.Drawing.Point(64, 127)
        Me.txtBuscarCatalogo.MaxLength = 300
        Me.txtBuscarCatalogo.Name = "txtBuscarCatalogo"
        Me.txtBuscarCatalogo.Size = New System.Drawing.Size(195, 20)
        Me.txtBuscarCatalogo.TabIndex = 57
        '
        'Label10
        '
        Me.Label10.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.White
        Me.Label10.Location = New System.Drawing.Point(0, 131)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(61, 13)
        Me.Label10.TabIndex = 56
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
        EnhancedScrollBarRenderer5.ArrowColor = System.Drawing.Color.MidnightBlue
        EnhancedScrollBarRenderer5.ArrowHoveredColor = System.Drawing.Color.MidnightBlue
        EnhancedScrollBarRenderer5.ArrowSelectedColor = System.Drawing.Color.MidnightBlue
        EnhancedScrollBarRenderer5.ButtonBackgroundColor = System.Drawing.Color.DarkSlateBlue
        EnhancedScrollBarRenderer5.ButtonBorderColor = System.Drawing.Color.MidnightBlue
        EnhancedScrollBarRenderer5.ButtonHoveredBackgroundColor = System.Drawing.Color.MidnightBlue
        EnhancedScrollBarRenderer5.ButtonHoveredBorderColor = System.Drawing.Color.Black
        EnhancedScrollBarRenderer5.ButtonSelectedBackgroundColor = System.Drawing.Color.SteelBlue
        EnhancedScrollBarRenderer5.ButtonSelectedBorderColor = System.Drawing.Color.DarkSlateBlue
        EnhancedScrollBarRenderer5.TrackBarBackgroundColor = System.Drawing.Color.SteelBlue
        EnhancedScrollBarRenderer5.TrackBarSelectedBackgroundColor = System.Drawing.Color.DarkSlateBlue
        Me.spCatalogos.HorizontalScrollBar.Renderer = EnhancedScrollBarRenderer5
        Me.spCatalogos.HorizontalScrollBar.TabIndex = 10
        Me.spCatalogos.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never
        Me.spCatalogos.Location = New System.Drawing.Point(0, 0)
        Me.spCatalogos.Name = "spCatalogos"
        NamedStyle1.ForeColor = System.Drawing.Color.White
        NamedStyle1.HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
        NamedStyle1.Locked = False
        NamedStyle1.NoteIndicatorColor = System.Drawing.Color.Red
        NamedStyle1.VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
        NamedStyle2.BackColor = System.Drawing.Color.Gainsboro
        NamedStyle2.CellType = GeneralCellType1
        NamedStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        NamedStyle2.Locked = False
        NamedStyle2.NoteIndicatorColor = System.Drawing.Color.Red
        NamedStyle2.Renderer = GeneralCellType1
        NamedStyle3.BackColor = System.Drawing.Color.DarkSlateBlue
        NamedStyle3.ForeColor = System.Drawing.SystemColors.ControlText
        NamedStyle3.HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
        NamedStyle3.NoteIndicatorColor = System.Drawing.Color.Red
        NamedStyle3.VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
        NamedStyle4.BackColor = System.Drawing.Color.DarkSlateBlue
        NamedStyle4.ForeColor = System.Drawing.SystemColors.ControlText
        NamedStyle4.HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
        NamedStyle4.NoteIndicatorColor = System.Drawing.Color.Red
        NamedStyle4.VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
        NamedStyle5.BackColor = System.Drawing.Color.MidnightBlue
        NamedStyle5.ForeColor = System.Drawing.SystemColors.ControlText
        NamedStyle5.HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
        NamedStyle5.NoteIndicatorColor = System.Drawing.Color.Red
        EnhancedCornerRenderer1.ActiveBackgroundColor = System.Drawing.Color.DarkSlateBlue
        EnhancedCornerRenderer1.GridLineColor = System.Drawing.Color.Empty
        EnhancedCornerRenderer1.NormalBackgroundColor = System.Drawing.Color.MidnightBlue
        NamedStyle5.Renderer = EnhancedCornerRenderer1
        NamedStyle5.VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
        NamedStyle6.BackColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(230, Byte), Integer), CType(CType(255, Byte), Integer))
        NamedStyle6.CellType = GeneralCellType2
        NamedStyle6.ForeColor = System.Drawing.SystemColors.WindowText
        NamedStyle6.Locked = False
        NamedStyle6.NoteIndicatorColor = System.Drawing.Color.Red
        NamedStyle6.Renderer = GeneralCellType2
        Me.spCatalogos.NamedStyles.AddRange(New FarPoint.Win.Spread.NamedStyle() {NamedStyle1, NamedStyle2, NamedStyle3, NamedStyle4, NamedStyle5, NamedStyle6})
        Me.spCatalogos.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.spCatalogos_Sheet1})
        Me.spCatalogos.Size = New System.Drawing.Size(260, 120)
        SpreadSkin1.ColumnFooterDefaultStyle = NamedStyle3
        SpreadSkin1.ColumnHeaderDefaultStyle = NamedStyle3
        SpreadSkin1.CornerDefaultStyle = NamedStyle5
        SpreadSkin1.DefaultStyle = NamedStyle6
        SpreadSkin1.FocusRenderer = EnhancedFocusIndicatorRenderer1
        EnhancedInterfaceRenderer1.ArrowColorEnabled = System.Drawing.Color.MidnightBlue
        EnhancedInterfaceRenderer1.GrayAreaColor = System.Drawing.Color.LightSlateGray
        EnhancedInterfaceRenderer1.RangeGroupBackgroundColor = System.Drawing.Color.DarkSlateBlue
        EnhancedInterfaceRenderer1.RangeGroupButtonBorderColor = System.Drawing.Color.MidnightBlue
        EnhancedInterfaceRenderer1.RangeGroupLineColor = System.Drawing.Color.MidnightBlue
        EnhancedInterfaceRenderer1.ScrollBoxBackgroundColor = System.Drawing.Color.SteelBlue
        EnhancedInterfaceRenderer1.SheetTabBorderColor = System.Drawing.Color.MidnightBlue
        EnhancedInterfaceRenderer1.SheetTabLowerActiveColor = System.Drawing.Color.DarkGray
        EnhancedInterfaceRenderer1.SheetTabLowerNormalColor = System.Drawing.Color.MidnightBlue
        EnhancedInterfaceRenderer1.SheetTabUpperActiveColor = System.Drawing.Color.LightGray
        EnhancedInterfaceRenderer1.SheetTabUpperNormalColor = System.Drawing.Color.DarkSlateBlue
        EnhancedInterfaceRenderer1.SplitBarBackgroundColor = System.Drawing.Color.DarkSlateBlue
        EnhancedInterfaceRenderer1.SplitBarDarkColor = System.Drawing.Color.MidnightBlue
        EnhancedInterfaceRenderer1.SplitBarLightColor = System.Drawing.Color.DarkGray
        EnhancedInterfaceRenderer1.SplitBoxBackgroundColor = System.Drawing.Color.DarkSlateBlue
        EnhancedInterfaceRenderer1.SplitBoxBorderColor = System.Drawing.Color.MidnightBlue
        EnhancedInterfaceRenderer1.TabStripBackgroundColor = System.Drawing.Color.SteelBlue
        EnhancedInterfaceRenderer1.TabStripButtonBorderColor = System.Drawing.Color.MidnightBlue
        EnhancedInterfaceRenderer1.TabStripButtonFlatStyle = System.Windows.Forms.FlatStyle.Flat
        EnhancedInterfaceRenderer1.TabStripButtonLowerActiveColor = System.Drawing.Color.DarkSlateBlue
        EnhancedInterfaceRenderer1.TabStripButtonLowerNormalColor = System.Drawing.Color.MidnightBlue
        EnhancedInterfaceRenderer1.TabStripButtonLowerPressedColor = System.Drawing.Color.DimGray
        EnhancedInterfaceRenderer1.TabStripButtonUpperActiveColor = System.Drawing.Color.DarkGray
        EnhancedInterfaceRenderer1.TabStripButtonUpperNormalColor = System.Drawing.Color.SlateBlue
        EnhancedInterfaceRenderer1.TabStripButtonUpperPressedColor = System.Drawing.Color.DarkSlateBlue
        SpreadSkin1.InterfaceRenderer = EnhancedInterfaceRenderer1
        SpreadSkin1.Name = "MidnightPersonalizado"
        SpreadSkin1.RowHeaderDefaultStyle = NamedStyle4
        EnhancedScrollBarRenderer6.ArrowColor = System.Drawing.Color.MidnightBlue
        EnhancedScrollBarRenderer6.ArrowHoveredColor = System.Drawing.Color.MidnightBlue
        EnhancedScrollBarRenderer6.ArrowSelectedColor = System.Drawing.Color.MidnightBlue
        EnhancedScrollBarRenderer6.ButtonBackgroundColor = System.Drawing.Color.DarkSlateBlue
        EnhancedScrollBarRenderer6.ButtonBorderColor = System.Drawing.Color.MidnightBlue
        EnhancedScrollBarRenderer6.ButtonHoveredBackgroundColor = System.Drawing.Color.MidnightBlue
        EnhancedScrollBarRenderer6.ButtonHoveredBorderColor = System.Drawing.Color.Black
        EnhancedScrollBarRenderer6.ButtonSelectedBackgroundColor = System.Drawing.Color.SteelBlue
        EnhancedScrollBarRenderer6.ButtonSelectedBorderColor = System.Drawing.Color.DarkSlateBlue
        EnhancedScrollBarRenderer6.TrackBarBackgroundColor = System.Drawing.Color.SteelBlue
        EnhancedScrollBarRenderer6.TrackBarSelectedBackgroundColor = System.Drawing.Color.DarkSlateBlue
        SpreadSkin1.ScrollBarRenderer = EnhancedScrollBarRenderer6
        SpreadSkin1.SelectionRenderer = New FarPoint.Win.Spread.GradientSelectionRenderer(System.Drawing.Color.MidnightBlue, System.Drawing.Color.MidnightBlue, System.Drawing.Drawing2D.LinearGradientMode.Horizontal, 80)
        Me.spCatalogos.Skin = SpreadSkin1
        Me.spCatalogos.TabIndex = 22
        Me.spCatalogos.VerticalScrollBar.Buttons = New FarPoint.Win.Spread.FpScrollBarButtonCollection("BackwardLineButton,ThumbTrack,ForwardLineButton")
        Me.spCatalogos.VerticalScrollBar.Name = ""
        EnhancedScrollBarRenderer7.ArrowColor = System.Drawing.Color.MidnightBlue
        EnhancedScrollBarRenderer7.ArrowHoveredColor = System.Drawing.Color.MidnightBlue
        EnhancedScrollBarRenderer7.ArrowSelectedColor = System.Drawing.Color.MidnightBlue
        EnhancedScrollBarRenderer7.ButtonBackgroundColor = System.Drawing.Color.DarkSlateBlue
        EnhancedScrollBarRenderer7.ButtonBorderColor = System.Drawing.Color.MidnightBlue
        EnhancedScrollBarRenderer7.ButtonHoveredBackgroundColor = System.Drawing.Color.MidnightBlue
        EnhancedScrollBarRenderer7.ButtonHoveredBorderColor = System.Drawing.Color.Black
        EnhancedScrollBarRenderer7.ButtonSelectedBackgroundColor = System.Drawing.Color.SteelBlue
        EnhancedScrollBarRenderer7.ButtonSelectedBorderColor = System.Drawing.Color.DarkSlateBlue
        EnhancedScrollBarRenderer7.TrackBarBackgroundColor = System.Drawing.Color.SteelBlue
        EnhancedScrollBarRenderer7.TrackBarSelectedBackgroundColor = System.Drawing.Color.DarkSlateBlue
        Me.spCatalogos.VerticalScrollBar.Renderer = EnhancedScrollBarRenderer7
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
        'spVentas
        '
        Me.spVentas.AccessibleDescription = ""
        Me.spVentas.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.spVentas.BackColor = System.Drawing.Color.White
        Me.spVentas.HorizontalScrollBar.Buttons = New FarPoint.Win.Spread.FpScrollBarButtonCollection("BackwardLineButton,ThumbTrack,ForwardLineButton")
        Me.spVentas.HorizontalScrollBar.Name = ""
        EnhancedScrollBarRenderer8.ArrowColor = System.Drawing.Color.DarkSlateGray
        EnhancedScrollBarRenderer8.ArrowHoveredColor = System.Drawing.Color.DarkSlateGray
        EnhancedScrollBarRenderer8.ArrowSelectedColor = System.Drawing.Color.DarkSlateGray
        EnhancedScrollBarRenderer8.ButtonBackgroundColor = System.Drawing.Color.CadetBlue
        EnhancedScrollBarRenderer8.ButtonBorderColor = System.Drawing.Color.SlateGray
        EnhancedScrollBarRenderer8.ButtonHoveredBackgroundColor = System.Drawing.Color.SlateGray
        EnhancedScrollBarRenderer8.ButtonHoveredBorderColor = System.Drawing.Color.DarkGray
        EnhancedScrollBarRenderer8.ButtonSelectedBackgroundColor = System.Drawing.Color.DarkGray
        EnhancedScrollBarRenderer8.ButtonSelectedBorderColor = System.Drawing.Color.CadetBlue
        EnhancedScrollBarRenderer8.TrackBarBackgroundColor = System.Drawing.Color.CadetBlue
        EnhancedScrollBarRenderer8.TrackBarSelectedBackgroundColor = System.Drawing.Color.SlateGray
        Me.spVentas.HorizontalScrollBar.Renderer = EnhancedScrollBarRenderer8
        Me.spVentas.HorizontalScrollBar.TabIndex = 0
        Me.spVentas.Location = New System.Drawing.Point(335, 0)
        Me.spVentas.Name = "spVentas"
        Me.spVentas.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.spVentas_Sheet1})
        Me.spVentas.Size = New System.Drawing.Size(704, 415)
        Me.spVentas.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Seashell
        Me.spVentas.TabIndex = 0
        Me.spVentas.VerticalScrollBar.Buttons = New FarPoint.Win.Spread.FpScrollBarButtonCollection("BackwardLineButton,ThumbTrack,ForwardLineButton")
        Me.spVentas.VerticalScrollBar.Name = ""
        EnhancedScrollBarRenderer9.ArrowColor = System.Drawing.Color.DarkSlateGray
        EnhancedScrollBarRenderer9.ArrowHoveredColor = System.Drawing.Color.DarkSlateGray
        EnhancedScrollBarRenderer9.ArrowSelectedColor = System.Drawing.Color.DarkSlateGray
        EnhancedScrollBarRenderer9.ButtonBackgroundColor = System.Drawing.Color.CadetBlue
        EnhancedScrollBarRenderer9.ButtonBorderColor = System.Drawing.Color.SlateGray
        EnhancedScrollBarRenderer9.ButtonHoveredBackgroundColor = System.Drawing.Color.SlateGray
        EnhancedScrollBarRenderer9.ButtonHoveredBorderColor = System.Drawing.Color.DarkGray
        EnhancedScrollBarRenderer9.ButtonSelectedBackgroundColor = System.Drawing.Color.DarkGray
        EnhancedScrollBarRenderer9.ButtonSelectedBorderColor = System.Drawing.Color.CadetBlue
        EnhancedScrollBarRenderer9.TrackBarBackgroundColor = System.Drawing.Color.CadetBlue
        EnhancedScrollBarRenderer9.TrackBarSelectedBackgroundColor = System.Drawing.Color.SlateGray
        Me.spVentas.VerticalScrollBar.Renderer = EnhancedScrollBarRenderer9
        Me.spVentas.VerticalScrollBar.TabIndex = 11
        '
        'spVentas_Sheet1
        '
        Me.spVentas_Sheet1.Reset()
        spVentas_Sheet1.SheetName = "Sheet1"
        'Formulas and custom names must be loaded with R1C1 reference style
        Me.spVentas_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
        Me.spVentas_Sheet1.ColumnFooter.DefaultStyle.NoteIndicatorColor = System.Drawing.Color.Red
        Me.spVentas_Sheet1.ColumnFooter.DefaultStyle.Parent = "ColumnHeaderSeashell"
        Me.spVentas_Sheet1.ColumnFooterSheetCornerStyle.NoteIndicatorColor = System.Drawing.Color.Red
        Me.spVentas_Sheet1.ColumnFooterSheetCornerStyle.Parent = "CornerSeashell"
        Me.spVentas_Sheet1.ColumnHeader.DefaultStyle.NoteIndicatorColor = System.Drawing.Color.Red
        Me.spVentas_Sheet1.ColumnHeader.DefaultStyle.Parent = "ColumnHeaderSeashell"
        Me.spVentas_Sheet1.RowHeader.DefaultStyle.NoteIndicatorColor = System.Drawing.Color.Red
        Me.spVentas_Sheet1.RowHeader.DefaultStyle.Parent = "RowHeaderSeashell"
        Me.spVentas_Sheet1.SheetCornerStyle.NoteIndicatorColor = System.Drawing.Color.Red
        Me.spVentas_Sheet1.SheetCornerStyle.Parent = "CornerSeashell"
        Me.spVentas_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
        '
        'pnlPie
        '
        Me.pnlPie.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlPie.BackColor = System.Drawing.Color.White
        Me.pnlPie.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlPie.Controls.Add(Me.btnImprimir)
        Me.pnlPie.Controls.Add(Me.btnConfigurarImpresion)
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
        'btnImprimir
        '
        Me.btnImprimir.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnImprimir.BackColor = System.Drawing.Color.White
        Me.btnImprimir.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnImprimir.Enabled = False
        Me.btnImprimir.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.btnImprimir.FlatAppearance.BorderSize = 3
        Me.btnImprimir.FlatAppearance.MouseOverBackColor = System.Drawing.Color.MediumAquamarine
        Me.btnImprimir.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnImprimir.Image = CType(resources.GetObject("btnImprimir.Image"), System.Drawing.Image)
        Me.btnImprimir.Location = New System.Drawing.Point(789, 0)
        Me.btnImprimir.Name = "btnImprimir"
        Me.btnImprimir.Size = New System.Drawing.Size(60, 60)
        Me.btnImprimir.TabIndex = 52
        Me.btnImprimir.UseVisualStyleBackColor = False
        '
        'btnConfigurarImpresion
        '
        Me.btnConfigurarImpresion.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnConfigurarImpresion.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnConfigurarImpresion.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnConfigurarImpresion.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.btnConfigurarImpresion.FlatAppearance.BorderSize = 3
        Me.btnConfigurarImpresion.FlatAppearance.MouseOverBackColor = System.Drawing.Color.MediumAquamarine
        Me.btnConfigurarImpresion.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnConfigurarImpresion.ForeColor = System.Drawing.Color.Black
        Me.btnConfigurarImpresion.Image = CType(resources.GetObject("btnConfigurarImpresion.Image"), System.Drawing.Image)
        Me.btnConfigurarImpresion.Location = New System.Drawing.Point(64, 0)
        Me.btnConfigurarImpresion.Margin = New System.Windows.Forms.Padding(0)
        Me.btnConfigurarImpresion.Name = "btnConfigurarImpresion"
        Me.btnConfigurarImpresion.Size = New System.Drawing.Size(60, 60)
        Me.btnConfigurarImpresion.TabIndex = 27
        Me.btnConfigurarImpresion.UseVisualStyleBackColor = False
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
        Me.lblDescripcionTooltip.Location = New System.Drawing.Point(150, 13)
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
        Me.pbMarca.Image = Global.ALMVentas.My.Resources.Resources.Producido_por
        Me.pbMarca.ImageLocation = ""
        Me.pbMarca.Location = New System.Drawing.Point(962, 0)
        Me.pbMarca.Name = "pbMarca"
        Me.pbMarca.Size = New System.Drawing.Size(75, 75)
        Me.pbMarca.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbMarca.TabIndex = 6
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
        Me.Text = "Almacén - Ventas"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlContenido.ResumeLayout(False)
        Me.pnlCuerpo.ResumeLayout(False)
        CType(Me.spListadosOtros, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.spListadosOtros_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.spVales, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.spVales_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlTotales.ResumeLayout(False)
        Me.pnlTotales.PerformLayout()
        Me.pnlCapturaSuperior.ResumeLayout(False)
        Me.pnlCapturaSuperior.PerformLayout()
        Me.pnlCatalogos.ResumeLayout(False)
        Me.pnlCatalogos.PerformLayout()
        CType(Me.spCatalogos, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.spCatalogos_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.spVentas, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.spVentas_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents spVentas As FarPoint.Win.Spread.FpSpread
    Friend WithEvents spVentas_Sheet1 As FarPoint.Win.Spread.SheetView
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
    Friend WithEvents dtpFecha As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtId As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents chkConservarDatos As System.Windows.Forms.CheckBox
    Friend WithEvents pnlCatalogos As System.Windows.Forms.Panel
    Friend WithEvents btnIdSiguiente As System.Windows.Forms.Button
    Friend WithEvents btnIdAnterior As System.Windows.Forms.Button
    Private WithEvents btnMostrarOcultar As System.Windows.Forms.Button
    Friend WithEvents txtBuscarCatalogo As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents cbClientes As System.Windows.Forms.ComboBox
    Friend WithEvents chkMostrarDetallado As System.Windows.Forms.CheckBox
    Private WithEvents btnConfigurarImpresion As System.Windows.Forms.Button
    Friend WithEvents pdRecibo As System.Drawing.Printing.PrintDocument
    Friend WithEvents pdVale As System.Drawing.Printing.PrintDocument
    Friend WithEvents btnImprimir As System.Windows.Forms.Button
    Friend WithEvents cbMetodosPagos As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtImportePagado As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtImporteCambio As System.Windows.Forms.TextBox
    Friend WithEvents pnlTotales As System.Windows.Forms.Panel
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtTotal As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtDescuento As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtSubTotal As System.Windows.Forms.TextBox
    Friend WithEvents spVales As FarPoint.Win.Spread.FpSpread
    Friend WithEvents spVales_Sheet1 As FarPoint.Win.Spread.SheetView
    Friend WithEvents pbMarca As System.Windows.Forms.PictureBox
    Friend WithEvents txtImporteVale1 As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtImporteVale2 As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents spListadosOtros As FarPoint.Win.Spread.FpSpread
    Friend WithEvents spListadosOtros_Sheet1 As FarPoint.Win.Spread.SheetView
End Class
