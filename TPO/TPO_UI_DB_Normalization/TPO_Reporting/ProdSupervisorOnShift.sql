CREATE TABLE [dbo].[ProdSupervisorOnShift](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[IDCode] [int] NOT NULL,
	[LocID] [int] NOT NULL,
	[LineIDCode] [nvarchar](10) NOT NULL,
	[ProdDate] [datetime] NOT NULL,
	[ProdShiftID] [int] NOT NULL,
	[Name] [nvarchar](50) NULL,
	[DateMod] [datetime] NOT NULL,
 CONSTRAINT [PK_ProdSupervisorOnShift] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[ProdSupervisorOnShift]  WITH CHECK ADD  CONSTRAINT [FK_ProdSupervisorOnShift_Plant] FOREIGN KEY([LocID])
REFERENCES [dbo].[Plant] ([ID])
GO

ALTER TABLE [dbo].[ProdSupervisorOnShift] CHECK CONSTRAINT [FK_ProdSupervisorOnShift_Plant]
GO

ALTER TABLE [dbo].[ProdSupervisorOnShift]  WITH CHECK ADD  CONSTRAINT [FK_ProdSupervisorOnShift_ProductionShift] FOREIGN KEY([ProdShiftID])
REFERENCES [dbo].[ProductionShift] ([ID])
GO

ALTER TABLE [dbo].[ProdSupervisorOnShift] CHECK CONSTRAINT [FK_ProdSupervisorOnShift_ProductionShift]
GO


