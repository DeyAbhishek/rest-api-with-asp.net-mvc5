CREATE TABLE [dbo].[ProdLinesPerform](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[LocID] [int] NOT NULL,
	[LineCode] [nvarchar](10) NOT NULL,
	[Uptime] [float] NOT NULL,
	[Yield] [float] NOT NULL,
	[Throughput] [float] NOT NULL,
	[TPLabel] [nvarchar](10) NOT NULL,
	[OEE] [float] NOT NULL,
	[TPTUse] [nvarchar](2) NOT NULL,
	[DateChange] [datetime] NOT NULL,
	[SchedFactor] [float] NOT NULL,
 CONSTRAINT [PK_ProdLinesPerform] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[ProdLinesPerform]  WITH CHECK ADD  CONSTRAINT [FK_ProdLinesPerform_Plant] FOREIGN KEY([LocID])
REFERENCES [dbo].[Plant] ([ID])
GO

ALTER TABLE [dbo].[ProdLinesPerform] CHECK CONSTRAINT [FK_ProdLinesPerform_Plant]
GO


