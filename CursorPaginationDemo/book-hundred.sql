USE [BookHundred]
GO
/****** Object:  Table [dbo].[Book]    Script Date: 07-03-2025 20:55:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Book](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Author] [nvarchar](100) NULL,
	[Country] [nvarchar](100) NULL,
	[ImageLink] [nvarchar](100) NULL,
	[Language] [nvarchar](20) NULL,
	[Link] [nvarchar](200) NULL,
	[Pages] [int] NULL,
	[Title] [nvarchar](100) NULL,
	[Year] [int] NULL,
	[Price] [int] NULL,
 CONSTRAINT [PK_Book_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
