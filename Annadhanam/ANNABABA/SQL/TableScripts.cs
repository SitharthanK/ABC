namespace ANNABABA.SQL
{
    public class TableScripts
    {
        /*

            -- Script Date: 30-Aug-18 8:55 PM 
            DROP TABLE [COUNTRYDETAILS];
            GO
            CREATE TABLE [COUNTRYDETAILS] (
              [CountryId] int IDENTITY (1,1)  NOT NULL
            , [CountyName] nvarchar(30)  NULL
            , [IsActive] nchar(1) DEFAULT 'N'  NULL
            );
            GO
            ALTER TABLE [COUNTRYDETAILS] ADD CONSTRAINT [PK__COUNTRYDETAILS] PRIMARY KEY ([CountryId]);
            GO


            DROP TABLE [STATEDETAILS];
            GO
            CREATE TABLE [STATEDETAILS] (
              [StateId] int IDENTITY (1000,1)  NOT NULL
            , [CountryId] int  NULL
            , [StateName] nvarchar(100)  NULL
            , [IsActive] nchar(1) DEFAULT 'N'  NULL
            );
            GO
            ALTER TABLE [STATEDETAILS] ADD CONSTRAINT [PK__STATEDETAIL] PRIMARY KEY ([StateId]);
            GO
            ALTER TABLE [STATEDETAILS] ADD CONSTRAINT [FK__STATEDETAILS] FOREIGN KEY ([CountryId]) REFERENCES [COUNTRYDETAILS]([CountryId]) 
            ON DELETE NO ACTION ON UPDATE NO ACTION;
            GO

        -- Script Date: 30-Aug-18 8:55 PM  - ErikEJ.SqlCeScripting version 3.5.2.75
            DROP TABLE [CITYDETAILS];
            GO
            CREATE TABLE [CITYDETAILS] (
              [CityId] int IDENTITY (5000,1)  NOT NULL
            , [CountryId] int  NULL
            , [StateId] int  NULL
            , [CityName] nvarchar(100)  NULL
            , [IsActive] nchar(1) DEFAULT 'N'  NULL
            );
            GO
            ALTER TABLE [CITYDETAILS] ADD CONSTRAINT [PK__CITYDETAILS] PRIMARY KEY ([CityId]);
            GO
            ALTER TABLE [CITYDETAILS] ADD CONSTRAINT [FK__CITYDETAILS] FOREIGN KEY ([StateId]) REFERENCES [STATEDETAILS]([StateId]) ON DELETE NO ACTION ON UPDATE NO ACTION;
            GO
            ALTER TABLE [CITYDETAILS] ADD CONSTRAINT [FK__CITYDETAILS] FOREIGN KEY ([CountryId]) REFERENCES [COUNTRYDETAILS]([CountryId]) ON DELETE NO ACTION ON UPDATE NO ACTION;
            GO
         
         
         -- Script Date: 30-Aug-18 8:56 PM 
            DROP TABLE [AnnadhanamDetails];
            GO
            CREATE TABLE [AnnadhanamDetails] (
              [ReceiptNumber] bigint  NOT NULL
            , [DevoteeName] nvarchar(200)  NOT NULL
            , [Address] nvarchar(500)  NULL
            , [Country] nvarchar(100)  NULL
            , [State] nvarchar(100)  NULL
            , [City] nvarchar(100)  NULL
            , [Amount] bigint DEFAULT 500  NULL
            , [ReceiptCreatedDate] datetime NULL
            , [AnadhanamDate] datetime NULL
            , [ChequeNo] nvarchar(30)  NULL
            , [ChequeDate] datetime NULL
            , [ChequeDrawn] nvarchar(40)  NULL
            , [Mode] nvarchar(10)  NULL
            , [CountryCode] int  NULL
            , [StateCode] int  NULL
            , [CityCode] int  NULL
            , [ContactNumber] nvarchar(10)  NULL
            );
            GO
            ALTER TABLE [AnnadhanamDetails] ADD CONSTRAINT [PK_AnnadhanamDetails] PRIMARY KEY ([ReceiptNumber]);
            GO

         
         */
    }
}
