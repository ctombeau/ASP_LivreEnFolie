USE [LivreEnFolie]
GO
/****** Object:  Table [dbo].[auteur]    Script Date: 13/08/2023 20:32:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[auteur](
	[id_auteur] [int] IDENTITY(1,1) NOT NULL,
	[nom] [varchar](100) NOT NULL,
	[prenom] [varchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id_auteur] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[client]    Script Date: 13/08/2023 20:32:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[client](
	[id_client] [int] IDENTITY(1,1) NOT NULL,
	[username] [varchar](100) NOT NULL,
	[email] [varchar](100) NOT NULL,
	[password] [varchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id_client] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [un_username] UNIQUE NONCLUSTERED 
(
	[username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[client_livre]    Script Date: 13/08/2023 20:32:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[client_livre](
	[id_client] [int] NOT NULL,
	[id_livre] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[livre]    Script Date: 13/08/2023 20:32:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[livre](
	[id_livre] [int] IDENTITY(1,1) NOT NULL,
	[titre] [varchar](255) NOT NULL,
	[categorie] [varchar](255) NOT NULL,
	[prix] [decimal](10, 3) NOT NULL,
	[quantite] [int] NOT NULL,
	[id_auteur] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_livre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[client_livre]  WITH CHECK ADD FOREIGN KEY([id_client])
REFERENCES [dbo].[client] ([id_client])
GO
ALTER TABLE [dbo].[client_livre]  WITH CHECK ADD FOREIGN KEY([id_livre])
REFERENCES [dbo].[livre] ([id_livre])
GO
ALTER TABLE [dbo].[livre]  WITH CHECK ADD FOREIGN KEY([id_auteur])
REFERENCES [dbo].[auteur] ([id_auteur])
GO
/****** Object:  StoredProcedure [dbo].[AjoutLivre]    Script Date: 13/08/2023 20:32:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[AjoutLivre]
(
	@titre varchar(255),
	@categorie varchar(255),
	@prix decimal(10,3),
	@quantite int,
	@nom varchar(255),
	@prenom varchar(255)
)
as

if(not exists(select * from auteur where nom=@nom and prenom=@prenom))
	begin
		insert into auteur(nom, prenom) values(@nom, @prenom)
		insert into livre (titre, categorie, prix, quantite,id_auteur) values(@titre, @categorie,@prix,@quantite,(select max(id_auteur) from auteur))
	end
else
	begin
		insert into livre (titre, categorie, prix, quantite,id_auteur) values(@titre, @categorie,@prix,@quantite,(select id_auteur from auteur where nom=@nom and prenom=@prenom))
	end
GO
/****** Object:  StoredProcedure [dbo].[getLivre]    Script Date: 13/08/2023 20:32:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[getLivre]
as
select * from livre inner join
auteur on livre.id_auteur = auteur.id_auteur;
GO
