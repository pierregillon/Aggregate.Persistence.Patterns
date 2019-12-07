CREATE TABLE [dbo].[OrderEvent](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[AggregateId] [uniqueidentifier] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[Content] [nvarchar](max) NULL,
	[Name] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.OrderEvent] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO


