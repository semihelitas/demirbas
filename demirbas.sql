USE [DemirbasTakipSistemi]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 22.06.2019 15:53:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[CategoryID] [int] NOT NULL,
	[CategoryName] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[CategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CurrentUserData]    Script Date: 22.06.2019 15:53:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CurrentUserData](
	[currentUserRoleID] [int] NOT NULL,
	[currentUserName] [varchar](20) NOT NULL,
	[userID] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_CurrentUserData] PRIMARY KEY CLUSTERED 
(
	[userID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Location]    Script Date: 22.06.2019 15:53:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Location](
	[LocationID] [int] NOT NULL,
	[ProductName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Location] PRIMARY KEY CLUSTERED 
(
	[LocationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Login]    Script Date: 22.06.2019 15:53:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Login](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[username] [varchar](50) NOT NULL,
	[password] [varchar](50) NOT NULL,
	[role_id] [int] NOT NULL,
 CONSTRAINT [PK_Login] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Person]    Script Date: 22.06.2019 15:53:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Person](
	[PersonID] [int] NOT NULL,
	[PersonName] [varchar](30) NOT NULL,
	[PersonContact] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Person] PRIMARY KEY CLUSTERED 
(
	[PersonID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 22.06.2019 15:53:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ProductSerialNumber] [nvarchar](25) NOT NULL,
	[CategoryID] [int] NOT NULL,
	[ProductBrand] [nvarchar](20) NOT NULL,
	[PersonID] [int] NOT NULL,
	[RegisterDateTime] [datetime] NOT NULL,
	[ProductAmount] [int] NOT NULL,
	[ProductWarrantyDate] [datetime] NOT NULL,
	[ServiceContact] [nvarchar](50) NOT NULL,
	[ProductFeatures] [nvarchar](50) NOT NULL,
	[ProductImage] [nvarchar](100) NULL,
	[LocationID] [int] NOT NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[ProductSerialNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Project]    Script Date: 22.06.2019 15:53:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Project](
	[projectCode] [varchar](30) NOT NULL,
	[projectClient] [varchar](30) NOT NULL,
	[projectName] [varchar](30) NOT NULL,
	[projectStartDate] [datetime] NOT NULL,
	[projectStatus] [varchar](5) NOT NULL,
 CONSTRAINT [PK_Project] PRIMARY KEY CLUSTERED 
(
	[projectCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProjectProducts]    Script Date: 22.06.2019 15:53:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectProducts](
	[projectCode] [varchar](30) NOT NULL,
	[productSerialNumber] [nvarchar](25) NOT NULL,
	[productBrand] [nvarchar](20) NOT NULL,
	[productModel] [nvarchar](20) NOT NULL,
	[registerDateTime] [datetime] NOT NULL,
	[productAmount] [int] NOT NULL,
	[productWarrantyStartDate] [datetime] NOT NULL,
	[productServiceContact] [nvarchar](50) NOT NULL,
	[productFeatures] [nvarchar](50) NOT NULL,
	[productImage] [nvarchar](100) NULL,
	[productWarrantyFinishDate] [datetime] NOT NULL,
	[productProvider] [varchar](20) NOT NULL,
 CONSTRAINT [PK_ProjectProducts] PRIMARY KEY CLUSTERED 
(
	[productSerialNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 22.06.2019 15:53:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[role_id] [int] IDENTITY(1,1) NOT NULL,
	[role] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[role_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[Category] ([CategoryID], [CategoryName]) VALUES (1010, N'Monitör')
INSERT [dbo].[Category] ([CategoryID], [CategoryName]) VALUES (2020, N'Yazıcı')
INSERT [dbo].[Category] ([CategoryID], [CategoryName]) VALUES (3030, N'SSD')
INSERT [dbo].[Category] ([CategoryID], [CategoryName]) VALUES (4040, N'Laptop')
INSERT [dbo].[Category] ([CategoryID], [CategoryName]) VALUES (5050, N'HDD')
INSERT [dbo].[Category] ([CategoryID], [CategoryName]) VALUES (6060, N'Klavye')
SET IDENTITY_INSERT [dbo].[CurrentUserData] ON 

INSERT [dbo].[CurrentUserData] ([currentUserRoleID], [currentUserName], [userID]) VALUES (1, N'kamilkacmaz', 2066)
SET IDENTITY_INSERT [dbo].[CurrentUserData] OFF
INSERT [dbo].[Location] ([LocationID], [ProductName]) VALUES (1, N'DEMİRBAŞ')
INSERT [dbo].[Location] ([LocationID], [ProductName]) VALUES (2, N'ÜRÜN SATIŞ')
SET IDENTITY_INSERT [dbo].[Login] ON 

INSERT [dbo].[Login] ([id], [username], [password], [role_id]) VALUES (1, N'ilkeryoncaci', N'123', 1)
INSERT [dbo].[Login] ([id], [username], [password], [role_id]) VALUES (2, N'kamilkacmaz', N'123', 2)
INSERT [dbo].[Login] ([id], [username], [password], [role_id]) VALUES (3, N'semihelitas', N'123', 3)
SET IDENTITY_INSERT [dbo].[Login] OFF
INSERT [dbo].[Person] ([PersonID], [PersonName], [PersonContact]) VALUES (21897690, N'Aylin Çolak', N'05364547623')
INSERT [dbo].[Person] ([PersonID], [PersonName], [PersonContact]) VALUES (21897691, N'Selin Özsüt', N'selinozgut@gmail.com')
INSERT [dbo].[Person] ([PersonID], [PersonName], [PersonContact]) VALUES (21897692, N'Bora Karapınar', N'05331001010')
INSERT [dbo].[Person] ([PersonID], [PersonName], [PersonContact]) VALUES (21897693, N'Ferhat Demir', N'05378300706')
INSERT [dbo].[Person] ([PersonID], [PersonName], [PersonContact]) VALUES (21897694, N'Emin Eroğlu', N'emineroglu@gmail.com')
INSERT [dbo].[Person] ([PersonID], [PersonName], [PersonContact]) VALUES (21897695, N'Serhat Yılmaz', N'05349992222')
INSERT [dbo].[Person] ([PersonID], [PersonName], [PersonContact]) VALUES (21897696, N'Semih Elitaş', N'05364546565')
INSERT [dbo].[Person] ([PersonID], [PersonName], [PersonContact]) VALUES (21897697, N'Bertan Çakıcı', N'05428991010')
INSERT [dbo].[Product] ([ProductSerialNumber], [CategoryID], [ProductBrand], [PersonID], [RegisterDateTime], [ProductAmount], [ProductWarrantyDate], [ServiceContact], [ProductFeatures], [ProductImage], [LocationID]) VALUES (N'A5HHS212PB', 5050, N'Seagate', 21897693, CAST(N'2019-05-10T00:00:00.000' AS DateTime), 300, CAST(N'2019-05-17T00:00:00.000' AS DateTime), N'0536 454 65 65', N'1 TB', N'hdd_A5HHS212PB.jpg', 2)
INSERT [dbo].[Product] ([ProductSerialNumber], [CategoryID], [ProductBrand], [PersonID], [RegisterDateTime], [ProductAmount], [ProductWarrantyDate], [ServiceContact], [ProductFeatures], [ProductImage], [LocationID]) VALUES (N'F6SG2JH13K', 4040, N'Apple', 21897696, CAST(N'2019-04-15T00:00:00.000' AS DateTime), 10, CAST(N'2019-04-15T00:00:00.000' AS DateTime), N'0533 121 52 10', N'Macbook Air', N'laptop_F6SG2JH13K.jpg', 1)
INSERT [dbo].[Product] ([ProductSerialNumber], [CategoryID], [ProductBrand], [PersonID], [RegisterDateTime], [ProductAmount], [ProductWarrantyDate], [ServiceContact], [ProductFeatures], [ProductImage], [LocationID]) VALUES (N'GJGJGJSSFSW', 2020, N'Canon', 21897690, CAST(N'2019-04-15T00:00:00.000' AS DateTime), 10, CAST(N'2022-04-15T00:00:00.000' AS DateTime), N'0533 222 10 15', N'Canon Siyah Printer', N'yazıcı_GJGJGJSSFSW.jpg', 1)
INSERT [dbo].[Product] ([ProductSerialNumber], [CategoryID], [ProductBrand], [PersonID], [RegisterDateTime], [ProductAmount], [ProductWarrantyDate], [ServiceContact], [ProductFeatures], [ProductImage], [LocationID]) VALUES (N'HZFU76KTMT', 4040, N'Monster', 21897690, CAST(N'2019-05-27T00:00:00.000' AS DateTime), 3, CAST(N'2025-05-20T00:00:00.000' AS DateTime), N'0533 121 52 10', N'Gaming Laptop', N'laptop_HZFU76KTMT.jpg', 1)
INSERT [dbo].[Product] ([ProductSerialNumber], [CategoryID], [ProductBrand], [PersonID], [RegisterDateTime], [ProductAmount], [ProductWarrantyDate], [ServiceContact], [ProductFeatures], [ProductImage], [LocationID]) VALUES (N'L5SF8WB3OK', 6060, N'Asus', 21897692, CAST(N'2019-05-04T00:00:00.000' AS DateTime), 35, CAST(N'2019-05-27T00:00:00.000' AS DateTime), N'0533 121 52 10', N'Gaming Led Klavye', N'klavye_L5SF8WB3OK.png', 1)
INSERT [dbo].[Product] ([ProductSerialNumber], [CategoryID], [ProductBrand], [PersonID], [RegisterDateTime], [ProductAmount], [ProductWarrantyDate], [ServiceContact], [ProductFeatures], [ProductImage], [LocationID]) VALUES (N'R8UV6AA3DL', 3030, N'Samsung', 21897696, CAST(N'2019-05-20T00:00:00.000' AS DateTime), 250, CAST(N'2019-06-30T00:00:00.000' AS DateTime), N'0542 311 55 10', N'500GB EVO', N'ssd_R8UV6AA3DL.jpg', 2)
INSERT [dbo].[Product] ([ProductSerialNumber], [CategoryID], [ProductBrand], [PersonID], [RegisterDateTime], [ProductAmount], [ProductWarrantyDate], [ServiceContact], [ProductFeatures], [ProductImage], [LocationID]) VALUES (N'RPG832KFLG', 1010, N'Asus', 21897695, CAST(N'2019-04-15T00:00:00.000' AS DateTime), 12, CAST(N'2022-04-15T00:00:00.000' AS DateTime), N'0533 121 52 10', N'27 inch - IPS', N'monitör_RPG832KFLG.jpg', 2)
INSERT [dbo].[Product] ([ProductSerialNumber], [CategoryID], [ProductBrand], [PersonID], [RegisterDateTime], [ProductAmount], [ProductWarrantyDate], [ServiceContact], [ProductFeatures], [ProductImage], [LocationID]) VALUES (N'S4KC6PG7NB', 1010, N'HP', 21897697, CAST(N'2019-05-01T00:00:00.000' AS DateTime), 1, CAST(N'2019-05-31T00:00:00.000' AS DateTime), N'0536 333 20 20', N'27 inch - IPS', N'monitör_S4KC6PG7NB.jpg', 2)
INSERT [dbo].[Product] ([ProductSerialNumber], [CategoryID], [ProductBrand], [PersonID], [RegisterDateTime], [ProductAmount], [ProductWarrantyDate], [ServiceContact], [ProductFeatures], [ProductImage], [LocationID]) VALUES (N'SMH832AF6X', 5050, N'Seagate', 21897693, CAST(N'1996-09-08T00:00:00.000' AS DateTime), 35, CAST(N'2023-09-08T00:00:00.000' AS DateTime), N'0536 454 65 65', N'2 TB', N'hdd_SMH832AF6X.jpg', 1)
INSERT [dbo].[Product] ([ProductSerialNumber], [CategoryID], [ProductBrand], [PersonID], [RegisterDateTime], [ProductAmount], [ProductWarrantyDate], [ServiceContact], [ProductFeatures], [ProductImage], [LocationID]) VALUES (N'TUQ4UKPJ7P', 2020, N'Huawei', 21897692, CAST(N'2018-11-24T00:00:00.000' AS DateTime), 1, CAST(N'2025-01-20T00:00:00.000' AS DateTime), N'0536 333 20 20', N'Huawei Beyaz Printer', N'yazıcı_TUQ4UKPJ7P.jpg', 1)
INSERT [dbo].[Project] ([projectCode], [projectClient], [projectName], [projectStartDate], [projectStatus]) VALUES (N'19-00001', N'Microsoft', N'Microware', CAST(N'2019-05-31T00:00:00.000' AS DateTime), N'Aktif')
INSERT [dbo].[Project] ([projectCode], [projectClient], [projectName], [projectStartDate], [projectStatus]) VALUES (N'sad', N'sadsa', N'sadsa', CAST(N'2019-10-10T00:00:00.000' AS DateTime), N'Aktif')
INSERT [dbo].[Project] ([projectCode], [projectClient], [projectName], [projectStartDate], [projectStatus]) VALUES (N'sadsa', N'das', N'sada', CAST(N'2019-06-13T00:00:00.000' AS DateTime), N'Aktif')
INSERT [dbo].[Project] ([projectCode], [projectClient], [projectName], [projectStartDate], [projectStatus]) VALUES (N'safsa', N'asfsa', N'sagfa', CAST(N'2019-10-10T00:00:00.000' AS DateTime), N'Pasif')
INSERT [dbo].[ProjectProducts] ([projectCode], [productSerialNumber], [productBrand], [productModel], [registerDateTime], [productAmount], [productWarrantyStartDate], [productServiceContact], [productFeatures], [productImage], [productWarrantyFinishDate], [productProvider]) VALUES (N'19-00001', N'SN87KH93YZ', N'Asus', N'Rog-312', CAST(N'2019-05-31T00:00:00.000' AS DateTime), 1, CAST(N'2019-07-31T00:00:00.000' AS DateTime), N'05364546565', N'i5-5200U, 8GB Ram 250SSD - Siyah Renk Gaming Book', NULL, CAST(N'2023-07-31T00:00:00.000' AS DateTime), N'Aliexpress')
SET IDENTITY_INSERT [dbo].[Role] ON 

INSERT [dbo].[Role] ([role_id], [role]) VALUES (1, N'yönetici')
INSERT [dbo].[Role] ([role_id], [role]) VALUES (2, N'kullanıcı')
INSERT [dbo].[Role] ([role_id], [role]) VALUES (3, N'geliştirici')
SET IDENTITY_INSERT [dbo].[Role] OFF
ALTER TABLE [dbo].[Login]  WITH CHECK ADD  CONSTRAINT [R_10] FOREIGN KEY([role_id])
REFERENCES [dbo].[Role] ([role_id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Login] CHECK CONSTRAINT [R_10]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Category] FOREIGN KEY([CategoryID])
REFERENCES [dbo].[Category] ([CategoryID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Category]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Location] FOREIGN KEY([LocationID])
REFERENCES [dbo].[Location] ([LocationID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Location]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Person] FOREIGN KEY([PersonID])
REFERENCES [dbo].[Person] ([PersonID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Person]
GO
ALTER TABLE [dbo].[ProjectProducts]  WITH CHECK ADD  CONSTRAINT [FK_ProjectProducts_Project] FOREIGN KEY([projectCode])
REFERENCES [dbo].[Project] ([projectCode])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProjectProducts] CHECK CONSTRAINT [FK_ProjectProducts_Project]
GO
/****** Object:  StoredProcedure [dbo].[LoginByUsernamePassword]    Script Date: 22.06.2019 15:53:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================  
 -- Author:          <Author,,Name>  
 -- Create date: <Create Date,,>  
 -- Description:     <Description,,>  
 -- =============================================  
 CREATE PROCEDURE [dbo].[LoginByUsernamePassword]   
      @username varchar(50),  
      @password varchar(50)  
 AS  
 BEGIN  
      SELECT id, username, password, role_id  
      FROM Login  
      WHERE username = @username  
      AND password = @password  
 END  
 
GO
