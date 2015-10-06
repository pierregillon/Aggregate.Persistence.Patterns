CREATE TABLE [dbo].[OrderLine](
	[OrderId] [uniqueidentifier] NOT NULL,
	[Product] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.OrderLine] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC,
	[Product] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[OrderLine]  WITH CHECK ADD  CONSTRAINT [FK_dbo.OrderLine_dbo.Order_OrderId] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Order] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[OrderLine] CHECK CONSTRAINT [FK_dbo.OrderLine_dbo.Order_OrderId]
GO


