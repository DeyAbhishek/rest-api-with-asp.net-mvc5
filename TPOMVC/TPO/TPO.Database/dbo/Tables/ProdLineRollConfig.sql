CREATE TABLE [dbo].[ProdLineRollConfig] (
    [ID]       INT           IDENTITY (1, 1) NOT NULL,
    [RollName] NVARCHAR (50) NOT NULL,
    [TypeID]   INT           DEFAULT ((1)) NOT NULL,
    [Order]    INT           DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_ProdLineRollConfig] PRIMARY KEY CLUSTERED ([ID] ASC),
    FOREIGN KEY ([TypeID]) REFERENCES [dbo].[ProdLineType] ([ID])
);

