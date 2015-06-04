CREATE TABLE [dbo].[TPOProductCapCoreDetail] (
    [ID]           INT        IDENTITY (1, 1) NOT NULL,
    [CapThickMin]  FLOAT (53) NULL,
    [CapThickAvg]  FLOAT (53) NULL,
    [CapThickMax]  FLOAT (53) NULL,
    [CoreThickMin] FLOAT (53) NULL,
    [CoreThickAvg] FLOAT (53) NULL,
    [CoreThickMax] FLOAT (53) NULL,
    [CapAshMin]    FLOAT (53) NULL,
    [CapAshMax]    FLOAT (53) NULL,
    [CoreAshMin]   FLOAT (53) NULL,
    [CoreAshMax]   FLOAT (53) NULL,
    CONSTRAINT [PK_ProductCapCoreDetail] PRIMARY KEY CLUSTERED ([ID] ASC)
);

