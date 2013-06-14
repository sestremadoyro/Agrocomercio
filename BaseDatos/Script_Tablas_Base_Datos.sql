USE [Agrocomercio]
GO
/****** Object:  Table [dbo].[Articulos]    Script Date: 14/06/2013 03:37:43 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Articulos](
	[ArtCod] [int] NOT NULL,
	[PrvCod] [int] NULL,
	[CodAnt] [varchar](10) NULL,
	[ArtDescripcion] [nvarchar](80) NOT NULL,
	[ArtStock] [numeric](18, 2) NOT NULL,
	[ArtStockFac] [numeric](18, 2) NOT NULL,
	[ArtCostoProm] [numeric](18, 2) NULL,
	[ArtStockMax] [numeric](18, 2) NULL,
	[ArtStockMin] [numeric](18, 2) NULL,
	[ArtPeso] [decimal](18, 2) NULL,
	[UniCod] [int] NULL,
	[ArtFecRegis] [date] NULL,
	[ArtFecModi] [date] NULL,
	[Artestado] [bit] NOT NULL,
	[ArtFecVen] [date] NULL,
	[ArtStockIni] [numeric](18, 2) NULL,
 CONSTRAINT [PK_ARTICULOS] PRIMARY KEY CLUSTERED 
(
	[ArtCod] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Atributos]    Script Date: 14/06/2013 03:37:43 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Atributos](
	[AtrTipoCod] [int] NOT NULL,
	[AtrNivel] [int] NULL,
	[AtrCodigo] [varchar](3) NOT NULL,
	[AtrDescripcion] [varchar](100) NULL,
	[AtrEstado] [bit] NULL,
 CONSTRAINT [PK_Atributos] PRIMARY KEY CLUSTERED 
(
	[AtrTipoCod] ASC,
	[AtrCodigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Clientes]    Script Date: 14/06/2013 03:37:43 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Clientes](
	[CliCod] [int] NOT NULL,
	[CodAnt] [varchar](10) NULL,
	[CliTipoPer] [nchar](1) NULL,
	[CliTipoDoc] [nchar](3) NULL,
	[CliNumDoc] [nvarchar](12) NULL,
	[CliNombre] [nvarchar](80) NULL,
	[CliDireccion] [nvarchar](150) NULL,
	[CliTelefono] [nvarchar](18) NULL,
	[CliRepresen] [nvarchar](50) NULL,
	[CliFecRegis] [datetime] NULL,
	[CliEstado] [bit] NOT NULL,
	[CliCreAsig] [numeric](18, 2) NULL,
 CONSTRAINT [PK_CLIENTES] PRIMARY KEY CLUSTERED 
(
	[CliCod] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[det_letra]    Script Date: 14/06/2013 03:37:43 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[det_letra](
	[icodletra] [int] NOT NULL,
	[inumletra] [int] NOT NULL,
	[ccodletra] [varchar](10) NULL,
	[nmonto] [numeric](10, 2) NULL,
	[cestado] [varchar](1) NULL,
	[ninteres] [numeric](10, 2) NULL,
	[dfecvenc] [datetime] NULL,
	[dfecpago] [datetime] NULL,
	[dfecmod] [datetime] NULL,
	[idetletra] [int] NOT NULL,
	[cnumletra] [varchar](20) NULL,
 CONSTRAINT [det_letra_pk] PRIMARY KEY CLUSTERED 
(
	[idetletra] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[DetOperacion]    Script Date: 14/06/2013 03:37:43 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DetOperacion](
	[dtpCod] [bigint] NOT NULL,
	[OpeCod] [bigint] NOT NULL,
	[ArtCod] [int] NULL,
	[LotCod] [int] NULL,
	[dtpCantidad] [numeric](18, 2) NULL,
	[dtpPrecioVen] [numeric](18, 2) NULL,
	[dtpDscto] [numeric](18, 2) NULL,
	[dtpSubTotal] [numeric](18, 2) NULL,
	[UniCod] [int] NULL,
	[dtpEstado] [bit] NULL,
 CONSTRAINT [PK_DetOperacion] PRIMARY KEY CLUSTERED 
(
	[dtpCod] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DocumenOperacion]    Script Date: 14/06/2013 03:37:43 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DocumenOperacion](
	[dopCod] [bigint] NOT NULL,
	[dopNroSerie] [varchar](3) NULL,
	[dopNumero] [varchar](50) NULL,
	[OpeCod] [bigint] NOT NULL,
	[tdoCod] [int] NOT NULL,
	[dopMoneda] [char](3) NULL,
	[dopFecEmision] [smalldatetime] NULL,
	[dopFecCancela] [smalldatetime] NULL,
	[dopNroImpre] [int] NULL,
	[dopFecUltImpre] [smalldatetime] NULL,
	[dopEstado] [char](3) NULL,
	[icodletra] [int] NULL,
	[dopPunPartida] [varchar](100) NULL,
	[dopFecTraslado] [smalldatetime] NULL,
	[dopDocComple] [varchar](50) NULL,
 CONSTRAINT [PK_DocumenOperacion] PRIMARY KEY CLUSTERED 
(
	[dopCod] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[letra]    Script Date: 14/06/2013 03:37:43 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[letra](
	[icodigo] [int] NOT NULL,
	[itotcuota] [int] NULL,
	[nmontocuota] [numeric](10, 2) NULL,
	[cmoneda] [varchar](3) NULL,
	[nintpag] [numeric](10, 2) NULL,
	[iestado] [varchar](1) NULL,
	[dfeccreacion] [datetime] NULL,
	[dfecultpago] [datetime] NULL,
	[dfecmod] [datetime] NULL,
	[iusrCreacion] [int] NULL,
	[ctippago] [varchar](1) NULL,
	[nmntnota] [numeric](12, 2) NULL,
 CONSTRAINT [PK__letra__8001D0804316F928] PRIMARY KEY CLUSTERED 
(
	[icodigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[LotesArt]    Script Date: 14/06/2013 03:37:43 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LotesArt](
	[LotCod] [int] NOT NULL,
	[ArtCod] [int] NOT NULL,
	[LotNro] [int] NOT NULL,
	[LotStock] [numeric](18, 2) NULL,
	[LotPrecioCom] [numeric](18, 2) NULL,
	[LotPrecioVen] [numeric](18, 2) NULL,
	[LotFecRegis] [date] NULL,
	[LotFecVenci] [date] NULL,
	[LotFecModi] [datetime] NULL,
	[LotEstado] [bit] NOT NULL,
 CONSTRAINT [PK_LotesArt] PRIMARY KEY CLUSTERED 
(
	[LotCod] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Notas]    Script Date: 14/06/2013 03:37:43 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Notas](
	[ccodnota] [char](10) NULL,
	[dfecreg] [datetime] NULL,
	[iprvcod] [int] NULL,
	[nmontoNota] [numeric](10, 2) NULL,
	[cestadoNota] [varchar](1) NULL,
	[nmntutilizado] [numeric](10, 2) NULL,
	[inota] [int] NOT NULL,
	[cobservaciones] [text] NULL,
	[ctipo] [varchar](1) NULL,
	[OpeMoneda] [char](3) NULL,
	[ctipNota] [varchar](2) NULL,
 CONSTRAINT [Notas_pk] PRIMARY KEY CLUSTERED 
(
	[inota] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Operaciones]    Script Date: 14/06/2013 03:37:43 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Operaciones](
	[OpeCod] [bigint] NOT NULL,
	[OpeTipo] [char](1) NULL,
	[OpeFecEmision] [date] NULL,
	[OpeFecCancela] [date] NULL,
	[ZonCod] [int] NULL,
	[PerCod] [int] NULL,
	[TraCod] [int] NULL,
	[CliCod] [int] NULL,
	[PrvCod] [int] NULL,
	[OpeMoneda] [char](3) NULL,
	[OpeTipPago] [varchar](3) NULL,
	[OpeSubTotal] [numeric](18, 2) NULL,
	[OpeDscto] [numeric](18, 2) NULL,
	[OpeFlete] [numeric](18, 2) NULL,
	[OpeImpuesto] [numeric](18, 2) NULL,
	[OpeTotal] [numeric](18, 2) NULL,
	[OpeEstado] [char](1) NULL,
	[OpeModifica] [smalldatetime] NULL,
	[UsrCod] [int] NULL,
	[OpeTotPagPen] [numeric](18, 2) NULL,
	[OpeTotPagar] [numeric](18, 2) NULL,
	[nporDesc] [numeric](12, 2) NULL,
	[tcmCod] [int] NOT NULL,
	[OpeTipCiclo] [char](1) NULL,
	[OpeCiclo] [int] NULL,
 CONSTRAINT [PK_Operaciones] PRIMARY KEY CLUSTERED 
(
	[OpeCod] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Pag_letras]    Script Date: 14/06/2013 03:37:43 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Pag_letras](
	[idpagletra] [int] NOT NULL,
	[idetletra] [int] NULL,
	[nmonto] [numeric](8, 2) NULL,
	[dfecpago] [date] NULL,
	[dfecmodifi] [datetime] NULL,
	[ipercod] [int] NULL,
	[icodpago] [int] NULL,
	[inumRecibo] [char](20) NULL,
	[cobservaciones] [varchar](100) NULL,
	[ctipPag] [varchar](1) NULL,
	[inumliq] [int] NULL,
 CONSTRAINT [PK__Pag_letr__912CECB666603565] PRIMARY KEY CLUSTERED 
(
	[idpagletra] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Personal]    Script Date: 14/06/2013 03:37:43 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Personal](
	[perCod] [int] NOT NULL,
	[tpecod] [int] NOT NULL,
	[perNombres] [varchar](50) NULL,
	[perApellidoPat] [varchar](70) NULL,
	[perApellidoMat] [varchar](70) NULL,
	[perDireccion] [varchar](80) NULL,
	[perTelefono] [varchar](15) NULL,
 CONSTRAINT [PK_Personal] PRIMARY KEY CLUSTERED 
(
	[perCod] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Proveedores]    Script Date: 14/06/2013 03:37:43 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Proveedores](
	[PrvCod] [int] NOT NULL,
	[CodAnt] [varchar](10) NULL,
	[PrvNumDoc] [nchar](12) NULL,
	[PrvRazon] [nvarchar](80) NOT NULL,
	[PrvDireccion] [nvarchar](150) NULL,
	[PrvTelefono] [nvarchar](18) NULL,
	[PrvContacto] [nvarchar](50) NULL,
	[PrvFecRegis] [date] NOT NULL,
	[PrvEstado] [bit] NOT NULL,
	[PrvDscto] [numeric](18, 3) NULL,
	[PrvGanancia] [numeric](18, 3) NULL,
 CONSTRAINT [PK_PROVEEDORES] PRIMARY KEY CLUSTERED 
(
	[PrvCod] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[rel_notfactura]    Script Date: 14/06/2013 03:37:43 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[rel_notfactura](
	[idtpcod] [bigint] NULL,
	[inota] [int] NULL,
	[nmonto] [numeric](10, 2) NULL,
	[dfecmod] [datetime] NULL,
	[iusrcrc] [int] NULL,
	[irelnotfac] [int] NOT NULL,
 CONSTRAINT [rel_notfactura_pk] PRIMARY KEY CLUSTERED 
(
	[irelnotfac] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Roles]    Script Date: 14/06/2013 03:37:43 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Roles](
	[rolCod] [int] NOT NULL,
	[rolDescripcion] [varchar](50) NULL,
	[rolOpcionCrear] [bit] NULL,
	[rolOpcionModificar] [bit] NULL,
	[rolOpcionEliminar] [bit] NULL,
	[rolMenu] [varchar](50) NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[rolCod] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbliqCobranza]    Script Date: 14/06/2013 03:37:43 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbliqCobranza](
	[idliqcob] [int] NOT NULL,
	[fecreg] [date] NULL,
	[fecmin] [date] NULL,
	[fecmax] [date] NULL,
	[efectivo] [numeric](10, 2) NULL,
	[ctaViatico] [numeric](10, 2) NULL,
	[depositos] [numeric](10, 2) NULL,
	[GastVarios] [numeric](10, 2) NULL,
	[cheques] [numeric](10, 2) NULL,
	[inefectivo] [numeric](10, 2) NULL,
	[efectivodol] [numeric](10, 2) NULL,
	[ctaViaticodol] [numeric](10, 2) NULL,
	[depositosdol] [numeric](10, 2) NULL,
	[GastVariosdol] [numeric](10, 2) NULL,
	[chequesdol] [numeric](10, 2) NULL,
	[inefectivodol] [numeric](10, 2) NULL,
	[observaciones] [text] NULL,
	[idrecibidor] [int] NULL,
	[totsoles] [numeric](10, 2) NULL,
	[totdolares] [numeric](10, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[idliqcob] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TipoCambios]    Script Date: 14/06/2013 03:37:43 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TipoCambios](
	[tcmCod] [int] NOT NULL,
	[tcmMoneda] [varchar](4) NULL,
	[tcmCambio] [numeric](12, 2) NULL,
	[tcmFecha] [date] NULL,
	[tcmfecmod] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[tcmCod] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TipoPersonal]    Script Date: 14/06/2013 03:37:43 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TipoPersonal](
	[tpeCod] [int] NOT NULL,
	[tpeDescripcion] [varchar](50) NULL,
 CONSTRAINT [PK_TipoPersonal] PRIMARY KEY CLUSTERED 
(
	[tpeCod] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Transportistas]    Script Date: 14/06/2013 03:37:43 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Transportistas](
	[TraCod] [int] NOT NULL,
	[TraRuc] [varchar](12) NOT NULL,
	[TraRazonSocial] [varchar](80) NOT NULL,
	[TraDireccion] [varchar](100) NULL,
	[TraTelefono] [varchar](15) NULL,
	[TraFecRegis] [date] NULL,
	[TraEstado] [bit] NULL,
 CONSTRAINT [PK_Transportistas] PRIMARY KEY CLUSTERED 
(
	[TraCod] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Ubigeos]    Script Date: 14/06/2013 03:37:43 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Ubigeos](
	[UbiCod] [char](6) NOT NULL,
	[UbiDescripcion] [nvarchar](50) NULL,
 CONSTRAINT [PK_UBIGEOS] PRIMARY KEY CLUSTERED 
(
	[UbiCod] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Unidades]    Script Date: 14/06/2013 03:37:43 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Unidades](
	[UniCod] [int] NOT NULL,
	[UniDescripcion] [nvarchar](50) NULL,
	[UniAbrev] [nvarchar](15) NULL,
	[UniFactor] [numeric](18, 4) NULL,
	[UniSuperior] [int] NOT NULL,
	[UniEstado] [bit] NULL,
	[UniFecRegis] [datetime] NOT NULL,
 CONSTRAINT [PK_UNIDADES] PRIMARY KEY CLUSTERED 
(
	[UniCod] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 14/06/2013 03:37:43 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Usuarios](
	[usrCod] [int] NOT NULL,
	[perCod] [int] NULL,
	[usrLogin] [varchar](10) NULL,
	[usrClave] [varchar](10) NULL,
	[RolCod] [int] NULL,
 CONSTRAINT [PK_Usuarios] PRIMARY KEY CLUSTERED 
(
	[usrCod] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Clientes] ADD  CONSTRAINT [DF_Clientes_CliFecRegis]  DEFAULT (getdate()) FOR [CliFecRegis]
GO
ALTER TABLE [dbo].[det_letra] ADD  CONSTRAINT [DF__det_letra__dfecm__4CA06362]  DEFAULT (getdate()) FOR [dfecmod]
GO
ALTER TABLE [dbo].[DocumenOperacion] ADD  DEFAULT ('') FOR [dopPunPartida]
GO
ALTER TABLE [dbo].[DocumenOperacion] ADD  DEFAULT (getdate()) FOR [dopFecTraslado]
GO
ALTER TABLE [dbo].[letra] ADD  CONSTRAINT [DF__letra__ntotcuota__44FF419A]  DEFAULT ((0)) FOR [itotcuota]
GO
ALTER TABLE [dbo].[letra] ADD  CONSTRAINT [DF__letra__cmoneda__46E78A0C]  DEFAULT ('S') FOR [cmoneda]
GO
ALTER TABLE [dbo].[letra] ADD  CONSTRAINT [DF__letra__dfecmod__47DBAE45]  DEFAULT (getdate()) FOR [dfecmod]
GO
ALTER TABLE [dbo].[letra] ADD  CONSTRAINT [DF__letra__ctippago__6D0D32F4]  DEFAULT ('L') FOR [ctippago]
GO
ALTER TABLE [dbo].[Notas] ADD  CONSTRAINT [DF__Notas__ctipo__6B24EA82]  DEFAULT ('C') FOR [ctipo]
GO
ALTER TABLE [dbo].[Operaciones] ADD  CONSTRAINT [DF__Operacion__nporD__60A75C0F]  DEFAULT ((0)) FOR [nporDesc]
GO
ALTER TABLE [dbo].[rel_notfactura] ADD  CONSTRAINT [DF__rel_notfa__dfecm__534D60F1]  DEFAULT (getdate()) FOR [dfecmod]
GO
ALTER TABLE [dbo].[TipoCambios] ADD  DEFAULT (getdate()) FOR [tcmFecha]
GO
ALTER TABLE [dbo].[Unidades] ADD  CONSTRAINT [DF_Unidades_UniFecRegis]  DEFAULT (getdate()) FOR [UniFecRegis]
GO
ALTER TABLE [dbo].[Articulos]  WITH CHECK ADD  CONSTRAINT [FK_Articulos_Proveedores] FOREIGN KEY([PrvCod])
REFERENCES [dbo].[Proveedores] ([PrvCod])
GO
ALTER TABLE [dbo].[Articulos] CHECK CONSTRAINT [FK_Articulos_Proveedores]
GO
ALTER TABLE [dbo].[Articulos]  WITH CHECK ADD  CONSTRAINT [FK_Articulos_Unidades] FOREIGN KEY([UniCod])
REFERENCES [dbo].[Unidades] ([UniCod])
GO
ALTER TABLE [dbo].[Articulos] CHECK CONSTRAINT [FK_Articulos_Unidades]
GO
ALTER TABLE [dbo].[det_letra]  WITH CHECK ADD  CONSTRAINT [FK_det_letra_letra] FOREIGN KEY([icodletra])
REFERENCES [dbo].[letra] ([icodigo])
GO
ALTER TABLE [dbo].[det_letra] CHECK CONSTRAINT [FK_det_letra_letra]
GO
ALTER TABLE [dbo].[DetOperacion]  WITH CHECK ADD  CONSTRAINT [FK_DetOperacion_Articulos] FOREIGN KEY([ArtCod])
REFERENCES [dbo].[Articulos] ([ArtCod])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[DetOperacion] CHECK CONSTRAINT [FK_DetOperacion_Articulos]
GO
ALTER TABLE [dbo].[DetOperacion]  WITH CHECK ADD  CONSTRAINT [FK_DetOperacion_LotesArt] FOREIGN KEY([LotCod])
REFERENCES [dbo].[LotesArt] ([LotCod])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[DetOperacion] CHECK CONSTRAINT [FK_DetOperacion_LotesArt]
GO
ALTER TABLE [dbo].[DetOperacion]  WITH CHECK ADD  CONSTRAINT [FK_DetOperacion_Operaciones] FOREIGN KEY([OpeCod])
REFERENCES [dbo].[Operaciones] ([OpeCod])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DetOperacion] CHECK CONSTRAINT [FK_DetOperacion_Operaciones]
GO
ALTER TABLE [dbo].[DetOperacion]  WITH CHECK ADD  CONSTRAINT [FK_DetOperacion_Unidades] FOREIGN KEY([UniCod])
REFERENCES [dbo].[Unidades] ([UniCod])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[DetOperacion] CHECK CONSTRAINT [FK_DetOperacion_Unidades]
GO
ALTER TABLE [dbo].[DocumenOperacion]  WITH CHECK ADD  CONSTRAINT [FK_DocumenOperacion_Operaciones] FOREIGN KEY([OpeCod])
REFERENCES [dbo].[Operaciones] ([OpeCod])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DocumenOperacion] CHECK CONSTRAINT [FK_DocumenOperacion_Operaciones]
GO
ALTER TABLE [dbo].[LotesArt]  WITH CHECK ADD  CONSTRAINT [FK_LotesArt_Articulos] FOREIGN KEY([ArtCod])
REFERENCES [dbo].[Articulos] ([ArtCod])
GO
ALTER TABLE [dbo].[LotesArt] CHECK CONSTRAINT [FK_LotesArt_Articulos]
GO
ALTER TABLE [dbo].[Operaciones]  WITH CHECK ADD  CONSTRAINT [FK_Operaciones_Clientes] FOREIGN KEY([CliCod])
REFERENCES [dbo].[Clientes] ([CliCod])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[Operaciones] CHECK CONSTRAINT [FK_Operaciones_Clientes]
GO
ALTER TABLE [dbo].[Operaciones]  WITH CHECK ADD  CONSTRAINT [FK_Operaciones_Personal] FOREIGN KEY([PerCod])
REFERENCES [dbo].[Personal] ([perCod])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[Operaciones] CHECK CONSTRAINT [FK_Operaciones_Personal]
GO
ALTER TABLE [dbo].[Operaciones]  WITH CHECK ADD  CONSTRAINT [FK_Operaciones_Proveedores] FOREIGN KEY([PrvCod])
REFERENCES [dbo].[Proveedores] ([PrvCod])
GO
ALTER TABLE [dbo].[Operaciones] CHECK CONSTRAINT [FK_Operaciones_Proveedores]
GO
ALTER TABLE [dbo].[Operaciones]  WITH CHECK ADD  CONSTRAINT [FK_Operaciones_TipoCambios] FOREIGN KEY([tcmCod])
REFERENCES [dbo].[TipoCambios] ([tcmCod])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[Operaciones] CHECK CONSTRAINT [FK_Operaciones_TipoCambios]
GO
ALTER TABLE [dbo].[Operaciones]  WITH CHECK ADD  CONSTRAINT [FK_Operaciones_Transportistas] FOREIGN KEY([TraCod])
REFERENCES [dbo].[Transportistas] ([TraCod])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[Operaciones] CHECK CONSTRAINT [FK_Operaciones_Transportistas]
GO
ALTER TABLE [dbo].[Personal]  WITH CHECK ADD  CONSTRAINT [FK_Personal_TipoPersonal] FOREIGN KEY([tpecod])
REFERENCES [dbo].[TipoPersonal] ([tpeCod])
GO
ALTER TABLE [dbo].[Personal] CHECK CONSTRAINT [FK_Personal_TipoPersonal]
GO
ALTER TABLE [dbo].[rel_notfactura]  WITH CHECK ADD  CONSTRAINT [rel_notfactura_fk2] FOREIGN KEY([inota])
REFERENCES [dbo].[Notas] ([inota])
GO
ALTER TABLE [dbo].[rel_notfactura] CHECK CONSTRAINT [rel_notfactura_fk2]
GO
ALTER TABLE [dbo].[Usuarios]  WITH CHECK ADD  CONSTRAINT [FK_Usuarios_Personal] FOREIGN KEY([perCod])
REFERENCES [dbo].[Personal] ([perCod])
GO
ALTER TABLE [dbo].[Usuarios] CHECK CONSTRAINT [FK_Usuarios_Personal]
GO
ALTER TABLE [dbo].[Usuarios]  WITH CHECK ADD  CONSTRAINT [FK_Usuarios_Roles] FOREIGN KEY([RolCod])
REFERENCES [dbo].[Roles] ([rolCod])
GO
ALTER TABLE [dbo].[Usuarios] CHECK CONSTRAINT [FK_Usuarios_Roles]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1-pendiente de pago
2-pagado' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'det_letra', @level2type=N'COLUMN',@level2name=N'cestado'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'fecha de modificacion' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'det_letra', @level2type=N'COLUMN',@level2name=N'dfecmod'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'pk del detalle' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'det_letra', @level2type=N'COLUMN',@level2name=N'idetletra'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Codigo Unico de Documento' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DocumenOperacion', @level2type=N'COLUMN',@level2name=N'dopCod'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nro de Serie del Documento' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DocumenOperacion', @level2type=N'COLUMN',@level2name=N'dopNroSerie'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nro de Documento' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DocumenOperacion', @level2type=N'COLUMN',@level2name=N'dopNumero'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Codigo de Operacion Relacionada' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DocumenOperacion', @level2type=N'COLUMN',@level2name=N'OpeCod'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tipo de Documento' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DocumenOperacion', @level2type=N'COLUMN',@level2name=N'tdoCod'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Moneda del Documento' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DocumenOperacion', @level2type=N'COLUMN',@level2name=N'dopMoneda'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha de Emision' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DocumenOperacion', @level2type=N'COLUMN',@level2name=N'dopFecEmision'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha de Cancelacion' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DocumenOperacion', @level2type=N'COLUMN',@level2name=N'dopFecCancela'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Estado del Documento' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DocumenOperacion', @level2type=N'COLUMN',@level2name=N'dopEstado'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'FK de la tabla letra solo tendrar lleno este campo los domuentos FATURA tdoCod= 10' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DocumenOperacion', @level2type=N'COLUMN',@level2name=N'icodletra'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'numero total de cuotas de la letra' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'letra', @level2type=N'COLUMN',@level2name=N'itotcuota'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'monto total de la letra a cancelar' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'letra', @level2type=N'COLUMN',@level2name=N'nmontocuota'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'interes total pagado' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'letra', @level2type=N'COLUMN',@level2name=N'nintpag'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'estado de la letra 1 pendiente 2 parcialmente pagado 3 cancelado' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'letra', @level2type=N'COLUMN',@level2name=N'iestado'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'fecha de creacion' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'letra', @level2type=N'COLUMN',@level2name=N'dfeccreacion'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'fecha de ultimo pago' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'letra', @level2type=N'COLUMN',@level2name=N'dfecultpago'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'fecha de modificacion  getdate()' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'letra', @level2type=N'COLUMN',@level2name=N'dfecmod'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'usuario creacion' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'letra', @level2type=N'COLUMN',@level2name=N'iusrCreacion'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'determina si es letra o es for fecha ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'letra', @level2type=N'COLUMN',@level2name=N'ctippago'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'cabecera de las letras de pago de las compras' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'letra'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'fecha de registro' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Notas', @level2type=N'COLUMN',@level2name=N'dfecreg'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'codigo del provedor' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Notas', @level2type=N'COLUMN',@level2name=N'iprvcod'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'monto de la latra' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Notas', @level2type=N'COLUMN',@level2name=N'nmontoNota'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'estado de la nota: P parcialmente pagado C cancelado N No Utilizada' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Notas', @level2type=N'COLUMN',@level2name=N'cestadoNota'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'clave prikmaria' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Notas', @level2type=N'COLUMN',@level2name=N'inota'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'te dice si la nota es para las compras o es para las ventas si es compra el campo iprvcod es del proveedor si es venta el campo iprvcod es el codigo del cleinte' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Notas', @level2type=N'COLUMN',@level2name=N'ctipo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Moneda' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Notas', @level2type=N'COLUMN',@level2name=N'OpeMoneda'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'tipo de nota de la tabla atributos codigo 9' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Notas', @level2type=N'COLUMN',@level2name=N'ctipNota'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'tabla de notas de credito como de debito' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Notas'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Codigo Unico de Operacion' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Operaciones', @level2type=N'COLUMN',@level2name=N'OpeCod'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tipo de Operacion' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Operaciones', @level2type=N'COLUMN',@level2name=N'OpeTipo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha de Emision del Operacion' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Operaciones', @level2type=N'COLUMN',@level2name=N'OpeFecEmision'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha de Cancelación' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Operaciones', @level2type=N'COLUMN',@level2name=N'OpeFecCancela'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Codigo de Zona' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Operaciones', @level2type=N'COLUMN',@level2name=N'ZonCod'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Codigo de Vendedor' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Operaciones', @level2type=N'COLUMN',@level2name=N'PerCod'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Codigo de Tranportista' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Operaciones', @level2type=N'COLUMN',@level2name=N'TraCod'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Moneda de Operacion' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Operaciones', @level2type=N'COLUMN',@level2name=N'OpeMoneda'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tipo de Pago' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Operaciones', @level2type=N'COLUMN',@level2name=N'OpeTipPago'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sub Total de Operacion' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Operaciones', @level2type=N'COLUMN',@level2name=N'OpeSubTotal'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Descuento de Operacion' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Operaciones', @level2type=N'COLUMN',@level2name=N'OpeDscto'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Monto de Impuesto' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Operaciones', @level2type=N'COLUMN',@level2name=N'OpeImpuesto'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Monto Total de Operacion' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Operaciones', @level2type=N'COLUMN',@level2name=N'OpeTotal'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Estado de Operacion (A=abierta,C=Cancelada,N=Anulada)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Operaciones', @level2type=N'COLUMN',@level2name=N'OpeEstado'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha de Modificacion' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Operaciones', @level2type=N'COLUMN',@level2name=N'OpeModifica'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Codigo de Usuario que Registra' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Operaciones', @level2type=N'COLUMN',@level2name=N'UsrCod'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'porcentaje de descuento al momento de que sea compra al credito y tenga una nota enlaza' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Operaciones', @level2type=N'COLUMN',@level2name=N'nporDesc'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'enlace a la tabla det_letra' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Pag_letras', @level2type=N'COLUMN',@level2name=N'idetletra'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'monto de pago' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Pag_letras', @level2type=N'COLUMN',@level2name=N'nmonto'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'fecha de pago' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Pag_letras', @level2type=N'COLUMN',@level2name=N'dfecpago'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'fecha de modificacion' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Pag_letras', @level2type=N'COLUMN',@level2name=N'dfecmodifi'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'codigo del trabajor' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Pag_letras', @level2type=N'COLUMN',@level2name=N'ipercod'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'codigo q se le hace al pago acum' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Pag_letras', @level2type=N'COLUMN',@level2name=N'icodpago'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Numero de Recibo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Pag_letras', @level2type=N'COLUMN',@level2name=N'inumRecibo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'observaciones del recibo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Pag_letras', @level2type=N'COLUMN',@level2name=N'cobservaciones'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'C: Cheque
E: Efectivo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Pag_letras', @level2type=N'COLUMN',@level2name=N'ctipPag'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'llave foranea q relaciona la nota' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'rel_notfactura', @level2type=N'COLUMN',@level2name=N'inota'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'fecha de modificacion' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'rel_notfactura', @level2type=N'COLUMN',@level2name=N'dfecmod'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1=soles   2=dolares  3=...' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TipoCambios', @level2type=N'COLUMN',@level2name=N'tcmMoneda'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'numeric (12,2)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TipoCambios', @level2type=N'COLUMN',@level2name=N'tcmCambio'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Relacion Recursiva' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Unidades', @level2type=N'COLUMN',@level2name=N'UniSuperior'
GO
