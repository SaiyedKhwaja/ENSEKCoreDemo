# ENSEKCoreDemo
Upload CSV file and process data
=================================
Project Resource Guideline
---------------------------
First, download the given Project

•	Open MS SQL Server Management Studio

•	Please open ENSEKCoreDemo_Database.sql file. CREATE ENSEKCoreDemo and Run the script in SQL Server Management Studio.
Note: It will create two tables called Account & MeterReading.

If there is any issue with database script. Please run database script which is given in this document.
•	Open ENSEKCoreDemo.sln solution

•	Open ENSEKCoreAPI  

•	Open appsettings.json file

“DefaultConnection” : “Server=<DESKTOP-R097NI9>
Change <DESKTOP-R097NI9> to Your SQL Server Name
 
•	Run the Project (I’ve already done the following setting to make sure API service is running when we consume API in ENSEKCoreWebApp)
Please make sure, Startup Project has got following settings.
(Right Click on Solution > Common Properties > Startup Project
 


Database Script
---------------
Step 1
Create a database called "ENSEKCoreDemo" and run this script on it

Step 2

Run Following Script

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Account](
	[AccountId] [int] NOT NULL,
	[FirstName] [nvarchar](150) NULL,
	[LastName] [nvarchar](150) NULL,
 CONSTRAINT [PK_Accounts] PRIMARY KEY CLUSTERED 
(
	[AccountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


INSERT INTO 
dbo.Account( 
				AccountId, FirstName, LastName
            )
VALUES
	(2344,'Tommy','Test'),
	(2233,'Barry','Test'),
	(8766,'Sally','Test'),
	(2345,'Jerry','Test'),
	(2346,'Ollie','Test'),
	(2347,'Tara','Test'),
	(2348,'Tammy','Test'),
	(2349,'Simon','Test'),
	(2350,'Colin','Test'),
	(2351,'Gladys','Test'),
	(2352,'Greg','Test'),
	(2353,'Tony','Test'),
	(2355,'Arthur','Test'),
	(2356,'Craig','Test'),
	(6776,'Laura','Test'),
	(4534,'JOSH','TEST'),
	(1234,'Freya','Test');

GO


SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[MeterReading](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AccountId] [int] NOT NULL,
	[MeterReadingDateTime] [datetime] NULL,
	[MeterReadingValue] [int] NOT NULL,
 CONSTRAINT [PK_MeterReading] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

