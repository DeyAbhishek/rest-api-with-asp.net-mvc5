CREATE TABLE [dbo].[TPOProductGrabDetail] (
    [ID]             INT        IDENTITY (1, 1) NOT NULL,
    [GrabTensMinMD]  FLOAT (53) NULL,
    [GrabElongMinMD] FLOAT (53) NULL,
    [GrabTearMin]    FLOAT (53) NULL,
    [GrabTensMinTD]  FLOAT (53) NULL,
    [GrabElongMinTD] FLOAT (53) NULL,
    [GrabTearMinTD]  FLOAT (53) NULL,
    CONSTRAINT [PK_ProductGrabDetail] PRIMARY KEY CLUSTERED ([ID] ASC)
);

