CREATE TABLE [dbo].[ProdDteChng](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[LocID] [int] NOT NULL,
	[LineCode] [nvarchar](10) NOT NULL,
	[ChangeType] [int] NOT NULL,
	[DateChange] [datetime] NOT NULL,
	[RotationStart] [datetime] NOT NULL,
 CONSTRAINT [PK_ProdDteChng] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[ProdDteChng]  WITH CHECK ADD  CONSTRAINT [FK_ProdDteChng_Plant] FOREIGN KEY([LocID])
REFERENCES [dbo].[Plant] ([ID])
GO

ALTER TABLE [dbo].[ProdDteChng] CHECK CONSTRAINT [FK_ProdDteChng_Plant]
GO


