USE [LinkShortener]
GO

/****** Object:  User [svc_linkmgt_api]    Script Date: 22/10/2021 19:49:13 ******/
CREATE USER [svc_linkmgt_api] FOR LOGIN [svc_linkmgt_api] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [svc_linkmgt_api]
GO

/****** Object:  Table [dbo].[Link]    Script Date: 22/10/2021 19:49:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Link](
	[LinkID] [bigint] NOT NULL,
	[LinkCreatedDate] [datetime] NOT NULL,
	[LinkToken] [varchar](8) NOT NULL,
	[OriginalLink] [varchar](1000) NOT NULL,
	[ShortenedLink] [varchar](128) NOT NULL,
	[LinkExpiryDate] [datetime] NULL,
 CONSTRAINT [PK_Link] PRIMARY KEY CLUSTERED 
(
	[LinkID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[LinkUsage]    Script Date: 22/10/2021 19:49:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LinkUsage](
	[LinkUsageID] [bigint] IDENTITY(1,1) NOT NULL,
	[LinkID] [bigint] NOT NULL,
	[LinkClickedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_LinkUsage] PRIMARY KEY CLUSTERED 
(
	[LinkUsageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Index [IX_LinkUsage_01]    Script Date: 22/10/2021 19:49:13 ******/
CREATE NONCLUSTERED INDEX [IX_LinkUsage_01] ON [dbo].[LinkUsage]
(
	[LinkID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

/****** Object:  StoredProcedure [dbo].[GetNextLinkID]    Script Date: 22/10/2021 19:49:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Sam Day
-- Create date: 2021-10-22
-- Description:	Simple function to get the next ID value for the dbo.Link table
-- =============================================
CREATE PROCEDURE [dbo].[GetNextLinkID]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT NEXT VALUE FOR dbo.LinkSequence
END
GO

/****** Object:  Sequence [dbo].[LinkSequence]    Script Date: 22/10/2021 19:49:28 ******/
CREATE SEQUENCE [dbo].[LinkSequence] 
 AS [bigint]
 START WITH 1000000
 INCREMENT BY 1
 MINVALUE -9223372036854775808
 MAXVALUE 9223372036854775807
 CACHE 
GO
