CREATE TABLE [dbo].[ProdLineType](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProdLineType] [nvarchar](5) NOT NULL,
	[ProdLineTypeDesc] [nvarchar](50) NOT NULL,
	[LineRollConfigID] [int] NOT NULL,
 CONSTRAINT [PK_ProdLineType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[ProdLineType]  WITH CHECK ADD  CONSTRAINT [FK_ProdLineType_ProdLineRollConfig] FOREIGN KEY([LineRollConfigID])
REFERENCES [dbo].[ProdLineRollConfig] ([ID])
GO

ALTER TABLE [dbo].[ProdLineType] CHECK CONSTRAINT [FK_ProdLineType_ProdLineRollConfig]
GO

ALTER TABLE [dbo].[ProdLineType]  WITH CHECK ADD  CONSTRAINT [FK_ProdLineType_ProdLineType] FOREIGN KEY([ID])
REFERENCES [dbo].[ProdLineType] ([ID])
GO

ALTER TABLE [dbo].[ProdLineType] CHECK CONSTRAINT [FK_ProdLineType_ProdLineType]
GO


