USE [CsvImporter]
GO

/****** Object:  Table [dbo].[Importers]    Script Date: 12/04/2021 14:37:54 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Importers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Date] [varchar](max) NULL,
	[PointOfSale] [varchar](max) NULL,
	[Product] [varchar](max) NULL,
	[Stock] [varchar](max) NULL,
 CONSTRAINT [PK_Importers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


