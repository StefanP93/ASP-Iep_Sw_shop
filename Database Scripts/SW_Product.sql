USE [IEPProjekatPS120511_db]
GO

/****** Object:  Table [dbo].[SW_Product]    Script Date: 6/26/2015 9:25:45 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[SW_Product](
	[IDProduct] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Tag] [varchar](50) NOT NULL,
	[Description] [varchar](1024) NULL,
	[Logo] [varbinary](max) NOT NULL,
	[Image] [varbinary](max) NULL,
	[Price] [float] NOT NULL,
	[Deleted] [int] NOT NULL DEFAULT ((0)),
 CONSTRAINT [PK_SW_Product] PRIMARY KEY CLUSTERED 
(
	[IDProduct] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO

SET ANSI_PADDING OFF
GO

