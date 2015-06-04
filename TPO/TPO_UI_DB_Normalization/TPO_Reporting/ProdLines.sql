CREATE TABLE [dbo].[ProdLines](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[LineDescCode] [nvarchar](10) NOT NULL,
	[LineDesc] [nvarchar](50) NULL,
	[LineTypeID] [int] NOT NULL,
	[LabelID] [int] NOT NULL,
	[CrntWOID] [nvarchar](10) NOT NULL,
	[TPOMorC] [nvarchar](3) NOT NULL,
	[RepOrder] [int] NOT NULL,
	[RCComp] [nvarchar](25) NULL,
	[LocID] [int] NOT NULL,
	[ModDate] [datetime] NOT NULL,
	[LineRollConfigID] [int] NOT NULL,
 CONSTRAINT [PK_ProdLines] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[ProdLines]  WITH CHECK ADD  CONSTRAINT [FK_ProdLines_Plant] FOREIGN KEY([LocID])
REFERENCES [dbo].[Plant] ([ID])
GO

ALTER TABLE [dbo].[ProdLines] CHECK CONSTRAINT [FK_ProdLines_Plant]
GO

ALTER TABLE [dbo].[ProdLines]  WITH CHECK ADD  CONSTRAINT [FK_ProdLines_ProdLineRollConfig] FOREIGN KEY([LineRollConfigID])
REFERENCES [dbo].[ProdLineRollConfig] ([ID])
GO

ALTER TABLE [dbo].[ProdLines] CHECK CONSTRAINT [FK_ProdLines_ProdLineRollConfig]
GO

ALTER TABLE [dbo].[ProdLines]  WITH CHECK ADD  CONSTRAINT [FK_ProdLines_ProdLineType] FOREIGN KEY([LineTypeID])
REFERENCES [dbo].[ProdLineType] ([ID])
GO

ALTER TABLE [dbo].[ProdLines] CHECK CONSTRAINT [FK_ProdLines_ProdLineType]
GO


