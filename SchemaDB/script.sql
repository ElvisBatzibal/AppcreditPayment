/****** Object:  Table [dbo].[Account]    Script Date: 12/05/2020 12:07:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[AccountNum] [int] IDENTITY(1,1) NOT NULL,
	[AccountName] [nvarchar](40) NOT NULL,
	[AccountRef] [nvarchar](40) NOT NULL,
	[Currency] [nvarchar](3) NOT NULL,
	[CardCode] [int] NULL,
	[InitialBalance] [decimal](30, 4) NULL,
	[PaidToDate] [decimal](30, 4) NULL,
	[Status] [nvarchar](10) NULL,
	[Active] [bit] NULL,
	[CreationDate] [datetime] NULL,
	[UserCreation] [int] NULL,
	[UpdateDate] [datetime] NULL,
	[UserUpdate] [int] NULL,
	[IdHostCreation] [nvarchar](50) NULL,
 CONSTRAINT [PK__Account__B9572BDA709F62E8] PRIMARY KEY CLUSTERED 
(
	[AccountNum] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AccountUser]    Script Date: 12/05/2020 12:07:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AccountUser](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](40) NULL,
	[Password] [nvarchar](40) NULL,
	[Active] [bit] NULL,
	[CreationDate] [datetime] NULL,
	[UserCreation] [int] NULL,
	[UpdateDate] [datetime] NULL,
	[UserUpdate] [int] NULL,
	[IdHostCreation] [nvarchar](50) NULL,
 CONSTRAINT [PK__AccountU__1788CC4C86953B50] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CreditAccount]    Script Date: 12/05/2020 12:07:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CreditAccount](
	[TransactionId] [int] IDENTITY(1,1) NOT NULL,
	[AccountNum] [int] NULL,
	[DocTotal] [decimal](30, 4) NULL,
	[Active] [bit] NULL,
	[CreationDate] [datetime] NULL,
	[UserCreation] [int] NULL,
	[UpdateDate] [datetime] NULL,
	[UserUpdate] [int] NULL,
	[IdHostCreation] [nvarchar](50) NULL,
 CONSTRAINT [PK__CreditAc__55433A6BA31C3951] PRIMARY KEY CLUSTERED 
(
	[TransactionId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 12/05/2020 12:07:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[CardCode] [int] IDENTITY(1,1) NOT NULL,
	[CustomerName] [nvarchar](20) NOT NULL,
	[Address] [nvarchar](50) NOT NULL,
	[Tel] [nvarchar](10) NOT NULL,
	[Balance] [decimal](30, 4) NULL,
	[Active] [bit] NULL,
	[CreationDate] [datetime] NULL,
	[UserCreation] [int] NULL,
	[UpdateDate] [datetime] NULL,
	[UserUpdate] [int] NULL,
	[IdHostCreation] [nvarchar](50) NULL,
 CONSTRAINT [PK__Customer__3D5317062589C280] PRIMARY KEY CLUSTERED 
(
	[CardCode] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Account]  WITH NOCHECK ADD  CONSTRAINT [FK_AccountCardCode] FOREIGN KEY([CardCode])
REFERENCES [dbo].[Customer] ([CardCode])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Account] CHECK CONSTRAINT [FK_AccountCardCode]
GO
ALTER TABLE [dbo].[CreditAccount]  WITH NOCHECK ADD  CONSTRAINT [FK_CreditAccountNum] FOREIGN KEY([AccountNum])
REFERENCES [dbo].[Account] ([AccountNum])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CreditAccount] CHECK CONSTRAINT [FK_CreditAccountNum]
GO
/****** Object:  StoredProcedure [dbo].[Sp_Account]    Script Date: 12/05/2020 12:07:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Sp_Account]
	@NumConsulta INT , 
	@AccountName NVARCHAR(50)= NULL,
	@Currency NVARCHAR(50)= NULL,
	@CardCode NVARCHAR(50)= NULL,
	@InitialBalance NVARCHAR(50)= NULL
AS
BEGIN
   --Create Cuenta
	IF(@NumConsulta=1)
	BEGIN 	
		 INSERT INTO Account(AccountName,AccountRef, Currency,  CardCode, InitialBalance, PaidToDate, Status, Active,    CreationDate ,  UserCreation ,  UpdateDate ,  UserUpdate)
		 VALUES(
		 @AccountName,'SALES-'+@CardCode,@Currency,@CardCode, @InitialBalance,0,'Abierto'
		 , 1 ,GETDATE() ,1 ,GETDATE() ,1 
		 );
		 --Update Balance
		 UPDATE Customer SET Balance=Balance+CONVERT(DECIMAL(30,4),@InitialBalance) WHERE CardCode=@CardCode;
		GOTO FIN
	END	
FIN:
END
GO
/****** Object:  StoredProcedure [dbo].[Sp_CreditAccount]    Script Date: 12/05/2020 12:07:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Sp_CreditAccount]
	@NumConsulta INT , 
	@AccountNum NVARCHAR(50)= NULL,
	@DocTotal NVARCHAR(50)= NULL,
	@CardCode NVARCHAR(50)= NULL
AS
BEGIN
   --Create Abono
	IF(@NumConsulta=1)
	BEGIN 	
		 INSERT INTO CreditAccount(AccountNum, DocTotal, Active,    CreationDate ,  UserCreation ,  UpdateDate ,  UserUpdate)
		 VALUES(
		 @AccountNum,@DocTotal
		 , 1 ,GETDATE() ,1 ,GETDATE() ,1 
		 );
		 --Update Account
		 UPDATE Account SET PaidToDate=PaidToDate+CONVERT(DECIMAL(30,4),@DocTotal) WHERE AccountNum=@AccountNum;
		 SELECT  @CardCode=CardCode FROM Account WHERE AccountNum=@AccountNum;
		 --Update Balance
		 UPDATE Customer SET Balance=Balance-CONVERT(DECIMAL(30,4),@DocTotal) WHERE CardCode=@CardCode;
		GOTO FIN
	END	
FIN:
END
GO
/****** Object:  StoredProcedure [dbo].[Sp_Customer]    Script Date: 12/05/2020 12:07:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Sp_Customer]
	@NumConsulta INT , 
	@CardName NVARCHAR(50)= NULL,
	@Address NVARCHAR(50)= NULL,
	@Tel NVARCHAR(50)= NULL,
	@CardCode NVARCHAR(50)= NULL
AS
BEGIN
   --Create
	IF(@NumConsulta=1)
	BEGIN 
		INSERT INTO Customer( 
		  CustomerName, Address, Tel,  Balance, 
		  Active,CreationDate,UserCreation,UpdateDate,UserUpdate)
		 VALUES(
		  @CardName ,@Address ,@Tel ,0 
		 ,1,GETDATE(),1,GETDATE(),1 
		 );
		GOTO FIN
	END
	--Create
	IF(@NumConsulta=2)
	BEGIN 
		 UPDATE Customer
		     SET CustomerName=@CardName
			 , Address=@Address
			 , Tel=@Tel
			 , UpdateDate=GETDATE()
			 , UserUpdate=1
		 WHERE CardCode=@CardCode;
		GOTO FIN
	END
FIN:
END
GO
/****** Object:  StoredProcedure [dbo].[Sp_User]    Script Date: 12/05/2020 12:07:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Sp_User]
	@NumConsulta INT , 
	@Param1 NVARCHAR(50)= NULL,
	@Param2 NVARCHAR(50)= NULL
AS
BEGIN
   --Login
	IF(@NumConsulta=1)
	BEGIN 
		SELECT 
		T0.UserName
		, T0.UserId
		FROM 
		AccountUser T0
		WHERE T0.UserName=@Param1
		AND T0.Password=@Param2
		GOTO FIN
	END

FIN:
END
GO
