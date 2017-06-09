CREATE TABLE [dbo].[Measurement] (
    [Id]       INT           NOT NULL IDENTITY,
    [Value]    NVARCHAR (50) NOT NULL,
    [Color]    NVARCHAR (20) DEFAULT ('lightgray') NOT NULL,
    [MetricId] INT           NOT NULL,
    [TakenDate] DATETIME NOT NULL DEFAULT GETDATE(), 
    [RepositoryId] INT NOT NULL, 
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Measurement_Metric] FOREIGN KEY ([MetricId]) REFERENCES [dbo].[Metric] ([Id]), 
    CONSTRAINT [FK_Measurement_Repository] FOREIGN KEY ([RepositoryId]) REFERENCES [Repository]([Id])
);

