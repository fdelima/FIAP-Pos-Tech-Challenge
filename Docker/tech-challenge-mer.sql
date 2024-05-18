USE [master]
GO

IF NOT EXISTS ( SELECT 1 FROM [sys].[databases] WHERE [NAME] = 'tech-challenge-grupo-71')
BEGIN
	CREATE DATABASE [tech-challenge-grupo-71]
END
GO

USE [tech-challenge-grupo-71]
GO

IF NOT EXISTS (SELECT 1 FROM [sys].[tables] WHERE [NAME] = 'cliente')
BEGIN

	/****** Object:  Table [dbo].[cliente]    Script Date: 11/05/2024 11:16:55 ******/
	SET ANSI_NULLS ON
	SET QUOTED_IDENTIFIER ON
	CREATE TABLE [dbo].[cliente](
		[id_cliente] [uniqueidentifier] NOT NULL,
		[nome] [nvarchar](50) NOT NULL,
		[email] [nvarchar](50) NOT NULL,
		[cpf] [bigint] NOT NULL,
	 CONSTRAINT [PK_cliente] PRIMARY KEY CLUSTERED 
	(
		[id_cliente] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
	) ON [PRIMARY]

END
GO

IF NOT EXISTS (SELECT 1 FROM [sys].[tables] WHERE [NAME] = 'dispositivo')
BEGIN

	/****** Object:  Table [dbo].[dispositivo]    Script Date: 11/05/2024 11:16:55 ******/
	SET ANSI_NULLS ON
	SET QUOTED_IDENTIFIER ON
	CREATE TABLE [dbo].[dispositivo](
		[id_dispositivo] [uniqueidentifier] NOT NULL,
		[identificador] [nvarchar](100) NOT NULL,
		[modelo] [nvarchar](50) NULL,
		[serie] [nvarchar](50) NULL,
	 CONSTRAINT [PK_dispositivo] PRIMARY KEY CLUSTERED 
	(
		[id_dispositivo] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
	) ON [PRIMARY]

	INSERT INTO [dbo].[dispositivo] VALUES (NEWID(),'TABLET MESA 01', 'SAMSUNG TAB S7', '123X')
	INSERT INTO [dbo].[dispositivo] VALUES (NEWID(),'TABLET MESA 02', 'SAMSUNG TAB S7', '123C')
	INSERT INTO [dbo].[dispositivo] VALUES (NEWID(),'TABLET MESA 03', 'SAMSUNG TAB S7', '123A')
END
GO 

IF NOT EXISTS (SELECT 1 FROM [sys].[tables] WHERE [NAME] = 'notificacao')
BEGIN

	/****** Object:  Table [dbo].[notificacao]    Script Date: 11/05/2024 11:16:55 ******/
	SET ANSI_NULLS ON
	SET QUOTED_IDENTIFIER ON
	CREATE TABLE [dbo].[notificacao](
		[id_notificacao] [uniqueidentifier] NOT NULL,
		[data] [datetime] NOT NULL,
		[mensagem] [nvarchar](50) NOT NULL,
		[id_dispositivo] [uniqueidentifier] NOT NULL,
	 CONSTRAINT [PK_notificacao_1] PRIMARY KEY CLUSTERED 
	(
		[id_notificacao] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
	) ON [PRIMARY]

END
GO

IF NOT EXISTS (SELECT 1 FROM [sys].[tables] WHERE [NAME] = 'pedido')
BEGIN

	/****** Object:  Table [dbo].[pedido]    Script Date: 11/05/2024 11:16:55 ******/
	SET ANSI_NULLS ON
	SET QUOTED_IDENTIFIER ON
	CREATE TABLE [dbo].[pedido](
		[id_pedido] [uniqueidentifier] NOT NULL,
		[data] [datetime] NOT NULL,
		[id_dispositivo] [uniqueidentifier] NOT NULL,
		[id_cliente] [uniqueidentifier] NULL,
		[status] [nvarchar](50) NOT NULL,
		[data_status_pedido] [datetime] NOT NULL,
	 CONSTRAINT [PK_pedido] PRIMARY KEY CLUSTERED 
	(
		[id_pedido] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
	) ON [PRIMARY]

END
GO

IF NOT EXISTS (SELECT 1 FROM [sys].[tables] WHERE [NAME] = 'pedido_item')
BEGIN

	/****** Object:  Table [dbo].[pedido_item]    Script Date: 11/05/2024 11:16:55 ******/
	SET ANSI_NULLS ON
	SET QUOTED_IDENTIFIER ON
	CREATE TABLE [dbo].[pedido_item](
		[id_pedido_item] [uniqueidentifier] NOT NULL,
		[data] [datetime] NOT NULL,
		[id_pedido] [uniqueidentifier] NOT NULL,
		[id_produto] [uniqueidentifier] NOT NULL,
		[observacao] [nvarchar](50) NULL,
		[quantidade] [int] NOT NULL,
	 CONSTRAINT [PK_pedido_item] PRIMARY KEY CLUSTERED 
	(
		[id_pedido_item] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
	) ON [PRIMARY]

END
GO

IF NOT EXISTS (SELECT 1 FROM [sys].[tables] WHERE [NAME] = 'produto')
BEGIN

	/****** Object:  Table [dbo].[produto]    Script Date: 11/05/2024 11:16:55 ******/
	SET ANSI_NULLS ON
	SET QUOTED_IDENTIFIER ON
	CREATE TABLE [dbo].[produto](
		[id_produto] [uniqueidentifier] NOT NULL,
		[nome] [nvarchar](50) NOT NULL,
		[preco] [numeric](18, 2) NOT NULL,
		[descricao] [nvarchar](500) NOT NULL,
		[categoria] [nvarchar](50) NOT NULL,
	 CONSTRAINT [PK_produto] PRIMARY KEY CLUSTERED 
	(
		[id_produto] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
	) ON [PRIMARY]

	ALTER TABLE [dbo].[pedido] ADD  CONSTRAINT [DF_pedido_data]  DEFAULT (getdate()) FOR [data]
	ALTER TABLE [dbo].[pedido] ADD  CONSTRAINT [DF_pedido_data_status_pedido]  DEFAULT (getdate()) FOR [data_status_pedido]
	ALTER TABLE [dbo].[pedido_item] ADD  CONSTRAINT [DF_pedido_item_data]  DEFAULT (getdate()) FOR [data]
	ALTER TABLE [dbo].[pedido_item] ADD  CONSTRAINT [DF_pedido_item_quantidade]  DEFAULT ((1)) FOR [quantidade]
	ALTER TABLE [dbo].[notificacao]  WITH CHECK ADD  CONSTRAINT [FK_notificacao_dispositivo1] FOREIGN KEY([id_dispositivo])
	REFERENCES [dbo].[dispositivo] ([id_dispositivo])
	ALTER TABLE [dbo].[notificacao] CHECK CONSTRAINT [FK_notificacao_dispositivo1]
	ALTER TABLE [dbo].[pedido]  WITH CHECK ADD  CONSTRAINT [FK_pedido_cliente] FOREIGN KEY([id_cliente])
	REFERENCES [dbo].[cliente] ([id_cliente])
	ALTER TABLE [dbo].[pedido] CHECK CONSTRAINT [FK_pedido_cliente]
	ALTER TABLE [dbo].[pedido]  WITH CHECK ADD  CONSTRAINT [FK_pedido_dispositivo] FOREIGN KEY([id_dispositivo])
	REFERENCES [dbo].[dispositivo] ([id_dispositivo])
	ALTER TABLE [dbo].[pedido] CHECK CONSTRAINT [FK_pedido_dispositivo]
	ALTER TABLE [dbo].[pedido_item]  WITH CHECK ADD  CONSTRAINT [FK_pedido_item_pedido] FOREIGN KEY([id_pedido])
	REFERENCES [dbo].[pedido] ([id_pedido])
	ALTER TABLE [dbo].[pedido_item] CHECK CONSTRAINT [FK_pedido_item_pedido]
	ALTER TABLE [dbo].[pedido_item]  WITH CHECK ADD  CONSTRAINT [FK_pedido_item_produto] FOREIGN KEY([id_produto])
	REFERENCES [dbo].[produto] ([id_produto])
	ALTER TABLE [dbo].[pedido_item] CHECK CONSTRAINT [FK_pedido_item_produto]

	INSERT INTO [dbo].[produto] VALUES (NEWID(), 'LANCE DA CASA', 9.99,'Dois hamburgueres (100% carne bovina), alface americana, queijo processado sabor cheddar, molho especial, cebola, picles e pão com gergelim. 499 GRAMAS','LANCHE')
	INSERT INTO [dbo].[produto] VALUES (NEWID(), 'BATATA FRITA', 4.99,'100 GRAMAS','ACOMPANHAMENTO')
	INSERT INTO [dbo].[produto] VALUES (NEWID(), 'COCA-COLA', 2.99,'LATA 350ML','BEBIDA')
END
GO
