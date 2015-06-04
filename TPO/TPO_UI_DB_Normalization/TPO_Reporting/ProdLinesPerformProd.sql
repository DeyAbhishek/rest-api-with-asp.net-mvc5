CREATE TABLE [dbo].[ProdLinesPerformProd](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[LocID] [int] NOT NULL,
	[ProdLineID] [int] NOT NULL,
	[ProductID] [int] NOT NULL,
	[Throughput] [float] NOT NULL,
	[DateChange] [datetime] NOT NULL,
 CONSTRAINT [PK_ProdLinesPerformProd] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[ProdLinesPerformProd]  WITH CHECK ADD  CONSTRAINT [FK_ProdLinesPerformProd_Plant] FOREIGN KEY([LocID])
REFERENCES [dbo].[Plant] ([ID])
GO

ALTER TABLE [dbo].[ProdLinesPerformProd] CHECK CONSTRAINT [FK_ProdLinesPerformProd_Plant]
GO

ALTER TABLE [dbo].[ProdLinesPerformProd]  WITH CHECK ADD  CONSTRAINT [FK_ProdLinesPerformProd_ProdLines] FOREIGN KEY([ProdLineID])
REFERENCES [dbo].[ProdLines] ([ID])
GO

ALTER TABLE [dbo].[ProdLinesPerformProd] CHECK CONSTRAINT [FK_ProdLinesPerformProd_ProdLines]
GO

ALTER TABLE [dbo].[ProdLinesPerformProd]  WITH CHECK ADD  CONSTRAINT [FK_ProdLinesPerformProd_TPOProducts] FOREIGN KEY([ProductID])
REFERENCES [dbo].[TPOProducts] ([ID])
GO

ALTER TABLE [dbo].[ProdLinesPerformProd] CHECK CONSTRAINT [FK_ProdLinesPerformProd_TPOProducts]
GO


