CREATE TABLE [dbo].[RawMaterialVendor] (
    [ID]           INT            IDENTITY (1, 1) NOT NULL,
    [PlantID]      INT            NOT NULL,
    [Vendor]       NVARCHAR (50)  NOT NULL,
    [DateEntered]  DATETIME       NOT NULL,
    [EnteredBy]    NVARCHAR (100) NOT NULL,
    [LastModified] DATETIME       NOT NULL,
    [ModifiedBy]   NVARCHAR (100) NOT NULL,
    CONSTRAINT [PK_RawMaterialVendor] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_RawMaterialVendor_Plant] FOREIGN KEY ([PlantID]) REFERENCES [dbo].[Plant] ([ID])
);

