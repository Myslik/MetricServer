CREATE TABLE [dbo].[Measurement] (
    [Id]       INT           NOT NULL,
    [Value]    NVARCHAR (50) NOT NULL,
    [Color]    NVARCHAR (20) DEFAULT ('lightgray') NOT NULL,
    [MetricId] INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Measurement_Metric] FOREIGN KEY ([MetricId]) REFERENCES [dbo].[Metric] ([Id])
);

