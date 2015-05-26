SET IDENTITY_INSERT [dbo].[Sizes] ON
INSERT INTO [dbo].[Sizes] ([Id], [Name]) VALUES (1, N'Small')
INSERT INTO [dbo].[Sizes] ([Id], [Name]) VALUES (2, N'Medium')
INSERT INTO [dbo].[Sizes] ([Id], [Name]) VALUES (3, N'Large')
SET IDENTITY_INSERT [dbo].[Sizes] OFF

SET IDENTITY_INSERT [dbo].[GroupOfferings] ON
INSERT INTO [dbo].[GroupOfferings] ([Id], [Name]) VALUES (1, N'Base Tea')
INSERT INTO [dbo].[GroupOfferings] ([Id], [Name]) VALUES (2, N'Flavor')
INSERT INTO [dbo].[GroupOfferings] ([Id], [Name]) VALUES (3, N'Toppings')
SET IDENTITY_INSERT [dbo].[GroupOfferings] OFF

SET IDENTITY_INSERT [dbo].[Items] ON
INSERT INTO [dbo].[Items] ([Id], [Name]) VALUES (1, N'Green Tea')
INSERT INTO [dbo].[Items] ([Id], [Name]) VALUES (2, N'Black Tea')
INSERT INTO [dbo].[Items] ([Id], [Name]) VALUES (3, N'Milk Tea')
INSERT INTO [dbo].[Items] ([Id], [Name]) VALUES (4, N'Lemon')
INSERT INTO [dbo].[Items] ([Id], [Name]) VALUES (5, N'Passionfruit')
INSERT INTO [dbo].[Items] ([Id], [Name]) VALUES (6, N'Yoghurt')
INSERT INTO [dbo].[Items] ([Id], [Name]) VALUES (7, N'Boba')
INSERT INTO [dbo].[Items] ([Id], [Name]) VALUES (8, N'Red Bean')
INSERT INTO [dbo].[Items] ([Id], [Name]) VALUES (9, N'Ai-Yu Jelly')
INSERT INTO [dbo].[Items] ([Id], [Name]) VALUES (10, N'Basil Seeds')
SET IDENTITY_INSERT [dbo].[Items] OFF

SET IDENTITY_INSERT [dbo].[ItemPrices] ON
INSERT INTO [dbo].[ItemPrices] ([Id], [ItemId], [Tax], [Price], [IsActive]) VALUES (2, 1, CAST(0.00 AS Decimal(18, 2)), CAST(2.00 AS Decimal(18, 2)), 1)
INSERT INTO [dbo].[ItemPrices] ([Id], [ItemId], [Tax], [Price], [IsActive]) VALUES (3, 2, CAST(0.00 AS Decimal(18, 2)), CAST(3.00 AS Decimal(18, 2)), 1)
INSERT INTO [dbo].[ItemPrices] ([Id], [ItemId], [Tax], [Price], [IsActive]) VALUES (4, 3, CAST(0.00 AS Decimal(18, 2)), CAST(3.00 AS Decimal(18, 2)), 1)
INSERT INTO [dbo].[ItemPrices] ([Id], [ItemId], [Tax], [Price], [IsActive]) VALUES (5, 4, CAST(0.00 AS Decimal(18, 2)), CAST(4.00 AS Decimal(18, 2)), 1)
INSERT INTO [dbo].[ItemPrices] ([Id], [ItemId], [Tax], [Price], [IsActive]) VALUES (6, 5, CAST(0.00 AS Decimal(18, 2)), CAST(5.00 AS Decimal(18, 2)), 1)
INSERT INTO [dbo].[ItemPrices] ([Id], [ItemId], [Tax], [Price], [IsActive]) VALUES (7, 6, CAST(0.00 AS Decimal(18, 2)), CAST(2.00 AS Decimal(18, 2)), 1)
INSERT INTO [dbo].[ItemPrices] ([Id], [ItemId], [Tax], [Price], [IsActive]) VALUES (8, 7, CAST(0.00 AS Decimal(18, 2)), CAST(3.00 AS Decimal(18, 2)), 1)
INSERT INTO [dbo].[ItemPrices] ([Id], [ItemId], [Tax], [Price], [IsActive]) VALUES (9, 8, CAST(0.00 AS Decimal(18, 2)), CAST(4.00 AS Decimal(18, 2)), 1)
INSERT INTO [dbo].[ItemPrices] ([Id], [ItemId], [Tax], [Price], [IsActive]) VALUES (10, 9, CAST(0.00 AS Decimal(18, 2)), CAST(5.00 AS Decimal(18, 2)), 1)
INSERT INTO [dbo].[ItemPrices] ([Id], [ItemId], [Tax], [Price], [IsActive]) VALUES (11, 10, CAST(0.00 AS Decimal(18, 2)), CAST(3.00 AS Decimal(18, 2)), 1)
SET IDENTITY_INSERT [dbo].[ItemPrices] OFF

SET IDENTITY_INSERT [dbo].[GroupItems] ON
INSERT INTO [dbo].[GroupItems] ([Id], [GroupOfferingId], [ItemId]) VALUES (1, 1, 1)
INSERT INTO [dbo].[GroupItems] ([Id], [GroupOfferingId], [ItemId]) VALUES (2, 1, 2)
INSERT INTO [dbo].[GroupItems] ([Id], [GroupOfferingId], [ItemId]) VALUES (4, 1, 3)
INSERT INTO [dbo].[GroupItems] ([Id], [GroupOfferingId], [ItemId]) VALUES (5, 2, 4)
INSERT INTO [dbo].[GroupItems] ([Id], [GroupOfferingId], [ItemId]) VALUES (6, 2, 5)
INSERT INTO [dbo].[GroupItems] ([Id], [GroupOfferingId], [ItemId]) VALUES (7, 2, 6)
INSERT INTO [dbo].[GroupItems] ([Id], [GroupOfferingId], [ItemId]) VALUES (8, 3, 7)
INSERT INTO [dbo].[GroupItems] ([Id], [GroupOfferingId], [ItemId]) VALUES (9, 3, 8)
INSERT INTO [dbo].[GroupItems] ([Id], [GroupOfferingId], [ItemId]) VALUES (10, 3, 9)
INSERT INTO [dbo].[GroupItems] ([Id], [GroupOfferingId], [ItemId]) VALUES (11, 3, 10)
SET IDENTITY_INSERT [dbo].[GroupItems] OFF
