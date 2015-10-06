CREATE TABLE [dbo].[Order](
	[Id] [uniqueidentifier] NOT NULL,
	[OrderStatus] [int] NOT NULL,
	[SubmitDate] [datetime] NULL,
	[TotalCost] [float] NOT NULL,
 CONSTRAINT [PK_dbo.Order] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


