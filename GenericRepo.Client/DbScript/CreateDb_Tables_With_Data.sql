create database AMSDatabase;
use AMSDatabase;

CREATE TABLE Persons(
  code int not null identity(1,1),
  name varchar(50) null,
  surname varchar(50) null,
  id_number varchar(50) not null,

  constraint PK_Persons primary key clustered
  (
    code
  )
)
GO
CREATE TABLE Accounts(
  code int not null identity(1,1),
  person_code int not null,
  account_number varchar(50) not null,
  outstanding_balance money not null,

  constraint FK_Account_Person foreign key (person_code) references Persons(code),

  constraint PK_Accounts primary key clustered
  (
    code
  )
)
GO
CREATE TABLE Transactions(
  code int not null identity(1,1),
  account_code int not null,
  transaction_date datetime not null,
  capture_date datetime not null,
  amount money not null,
  description varchar(100) not null,

  constraint FK_Transaction_Account foreign key (account_code) references Accounts(code),

  constraint PK_Transactions primary key clustered
  (
    code
  )
)
GO
CREATE UNIQUE NONCLUSTERED INDEX IX_Person_id ON Persons(id_number)
GO
CREATE UNIQUE NONCLUSTERED INDEX IX_Account_num ON Accounts(account_number)
GO
INSERT INTO Persons (id_number,name,surname) VALUES ('63XX2907910XX','REJOCE','MAJOLA')
INSERT INTO Persons (id_number,name,surname) VALUES ('70XX2403660XX','','MOKOMELE')
INSERT INTO Persons (id_number,name,surname) VALUES ('42XX1002420XX','NTOMBIKHONA','MLAMBO')
INSERT INTO Persons (id_number,name,surname) VALUES ('39XX1400850XX','KLAAS','OCKHUIS')
INSERT INTO Persons (id_number,name,surname) VALUES ('59XX0110380XX','FERDI','LUUS')
INSERT INTO Persons (id_number,name,surname) VALUES ('67XX1807700XX','SHAUN','LOVELOT')
INSERT INTO Persons (id_number,name,surname) VALUES ('74XX2301550XX','','MOSOOANE')
INSERT INTO Persons (id_number,name,surname) VALUES ('59XX1901940XX','','MOTENO')
INSERT INTO Persons (id_number,name,surname) VALUES ('66XX0354900XX','','ZWANE')
INSERT INTO Persons (id_number,name,surname) VALUES ('72XX1806150XX','JOSEPH','MOSWEU')
INSERT INTO Persons (id_number,name,surname) VALUES ('78XX0650010XX','','ZULU')
INSERT INTO Persons (id_number,name,surname) VALUES ('72XX2205500XX','','SKINI')
INSERT INTO Persons (id_number,name,surname) VALUES ('60XX0707320XX','','MASOPA')
INSERT INTO Persons (id_number,name,surname) VALUES ('56XX2351400XX','','COETZEE')
INSERT INTO Persons (id_number,name,surname) VALUES ('65XX1003960XX','','BEN')
INSERT INTO Persons (id_number,name,surname) VALUES ('68XX1612540XX','SOPHIE','SIHLANGU')
INSERT INTO Persons (id_number,name,surname) VALUES ('71XX2451300XX','BUYISELWA','KEPATA')
INSERT INTO Persons (id_number,name,surname) VALUES ('72XX2702550XX','ZANELE','NDLOVU')
INSERT INTO Persons (id_number,name,surname) VALUES ('62XX2207640XX','','BARNABAS')
INSERT INTO Persons (id_number,name,surname) VALUES ('75XX2302470XX','','ABRAHAMS')
INSERT INTO Persons (id_number,name,surname) VALUES ('73XX1104570XX','','GOVENDER')
INSERT INTO Persons (id_number,name,surname) VALUES ('65XX0106100XX','','ABDUL')
INSERT INTO Persons (id_number,name,surname) VALUES ('47XX2706770XX','','MBIXANE')
INSERT INTO Persons (id_number,name,surname) VALUES ('59XX1002010XX','MAVIS','MBOMBO')
INSERT INTO Persons (id_number,name,surname) VALUES ('44XX0801450XX','','RAMALEPE')
INSERT INTO Persons (id_number,name,surname) VALUES ('70XX3105540XX','','MRSHALOI')
INSERT INTO Persons (id_number,name,surname) VALUES ('69XX2604250XX','','DE MEYER')
INSERT INTO Persons (id_number,name,surname) VALUES ('73XX0311610XX','MICHAELINE','SITUMA')
INSERT INTO Persons (id_number,name,surname) VALUES ('66XX2705830XX','','NDWANE')
INSERT INTO Persons (id_number,name,surname) VALUES ('76XX2904740XX','','WEAVER')
INSERT INTO Persons (id_number,name,surname) VALUES ('70XX2251240XX','REBECCA','JOOSTE')
INSERT INTO Persons (id_number,name,surname) VALUES ('67XX2750210XX','','MOKETSI')
INSERT INTO Persons (id_number,name,surname) VALUES ('77XX0952460XX','BUSISIWE','MTHANTI')
INSERT INTO Persons (id_number,name,surname) VALUES ('67XX1256390XX','MICHEAL','TRUTER')
INSERT INTO Persons (id_number,name,surname) VALUES ('70XX0355620XX','GLORY','SITHOLE')
INSERT INTO Persons (id_number,name,surname) VALUES ('57XX1907550XX','ANNA','KOBE')
INSERT INTO Persons (id_number,name,surname) VALUES ('69XX2459930XX','','PARTAB')
INSERT INTO Persons (id_number,name,surname) VALUES ('70XX1507650XX','STORY','MAIPATO')
INSERT INTO Persons (id_number,name,surname) VALUES ('69XX2005003XX','','SOTYATO')
INSERT INTO Persons (id_number,name,surname) VALUES ('73XX1306320XX','FRANS','TOSKEY')
INSERT INTO Persons (id_number,name,surname) VALUES ('58XX3100790XX','LILLIAN','DANIELS')
INSERT INTO Persons (id_number,name,surname) VALUES ('72XX0401610XX','THEMBILE','MLUMBI')
INSERT INTO Persons (id_number,name,surname) VALUES ('67XX1105810XX','BERENICE','MEINTJIES')
INSERT INTO Persons (id_number,name,surname) VALUES ('73XX1807580XX','','NOMAVUKA')
INSERT INTO Persons (id_number,name,surname) VALUES ('68XX1211490XX','NONHLANHLA','NGWENYA')
INSERT INTO Persons (id_number,name,surname) VALUES ('77XX2304150XX','ELIZABETH','MOSES')
INSERT INTO Persons (id_number,name,surname) VALUES ('76XX2902020XX','','JACOBS')
INSERT INTO Persons (id_number,name,surname) VALUES ('58XX1802150XX','','GERTENBACH')
INSERT INTO Persons (id_number,name,surname) VALUES ('78XX1701830XX','','MAMORARE')
INSERT INTO Persons (id_number,name,surname) VALUES ('45XX2605080XX','','KHUMALO')
INSERT INTO Persons (id_number,name,surname) VALUES ('4572605080992','Siphenathi', 'No Account')
GO
INSERT INTO Accounts (person_code,account_number,outstanding_balance) VALUES ((SELECT TOP(1) code FROM Persons WHERE id_number = '63XX2907910XX'),'10000577',234.99)
INSERT INTO Accounts (person_code,account_number,outstanding_balance) VALUES ((SELECT TOP(1) code FROM Persons WHERE id_number = '70XX2403660XX'),'10001085',267.61)
INSERT INTO Accounts (person_code,account_number,outstanding_balance) VALUES ((SELECT TOP(1) code FROM Persons WHERE id_number = '42XX1002420XX'),'1000373',520.67)
INSERT INTO Accounts (person_code,account_number,outstanding_balance) VALUES ((SELECT TOP(1) code FROM Persons WHERE id_number = '39XX1400850XX'),'10007792',328.7)
INSERT INTO Accounts (person_code,account_number,outstanding_balance) VALUES ((SELECT TOP(1) code FROM Persons WHERE id_number = '59XX0110380XX'),'10011773',641.7)
INSERT INTO Accounts (person_code,account_number,outstanding_balance) VALUES ((SELECT TOP(1) code FROM Persons WHERE id_number = '67XX1807700XX'),'10012044',157.6)
INSERT INTO Accounts (person_code,account_number,outstanding_balance) VALUES ((SELECT TOP(1) code FROM Persons WHERE id_number = '74XX2301550XX'),'100137',936.41)
INSERT INTO Accounts (person_code,account_number,outstanding_balance) VALUES ((SELECT TOP(1) code FROM Persons WHERE id_number = '59XX1901940XX'),'10014357',440.87)
INSERT INTO Accounts (person_code,account_number,outstanding_balance) VALUES ((SELECT TOP(1) code FROM Persons WHERE id_number = '66XX0354900XX'),'10015256',170.68)
INSERT INTO Accounts (person_code,account_number,outstanding_balance) VALUES ((SELECT TOP(1) code FROM Persons WHERE id_number = '72XX1806150XX'),'10016473',663.77)
INSERT INTO Accounts (person_code,account_number,outstanding_balance) VALUES ((SELECT TOP(1) code FROM Persons WHERE id_number = '78XX0650010XX'),'10017712',1471.27)
INSERT INTO Accounts (person_code,account_number,outstanding_balance) VALUES ((SELECT TOP(1) code FROM Persons WHERE id_number = '72XX2205500XX'),'10019324',269.82)
INSERT INTO Accounts (person_code,account_number,outstanding_balance) VALUES ((SELECT TOP(1) code FROM Persons WHERE id_number = '60XX0707320XX'),'10019766',485.78)
INSERT INTO Accounts (person_code,account_number,outstanding_balance) VALUES ((SELECT TOP(1) code FROM Persons WHERE id_number = '56XX2351400XX'),'10020241',715.83)
INSERT INTO Accounts (person_code,account_number,outstanding_balance) VALUES ((SELECT TOP(1) code FROM Persons WHERE id_number = '65XX1003960XX'),'10020918',81.35)
INSERT INTO Accounts (person_code,account_number,outstanding_balance) VALUES ((SELECT TOP(1) code FROM Persons WHERE id_number = '68XX1612540XX'),'10021663',627.13)
INSERT INTO Accounts (person_code,account_number,outstanding_balance) VALUES ((SELECT TOP(1) code FROM Persons WHERE id_number = '71XX2451300XX'),'10021698',426.43)
INSERT INTO Accounts (person_code,account_number,outstanding_balance) VALUES ((SELECT TOP(1) code FROM Persons WHERE id_number = '72XX2702550XX'),'10022821',557.3)
INSERT INTO Accounts (person_code,account_number,outstanding_balance) VALUES ((SELECT TOP(1) code FROM Persons WHERE id_number = '62XX2207640XX'),'10022996',299.2)
INSERT INTO Accounts (person_code,account_number,outstanding_balance) VALUES ((SELECT TOP(1) code FROM Persons WHERE id_number = '75XX2302470XX'),'10024492',767.25)
INSERT INTO Accounts (person_code,account_number,outstanding_balance) VALUES ((SELECT TOP(1) code FROM Persons WHERE id_number = '73XX1104570XX'),'10027262',483.55)
INSERT INTO Accounts (person_code,account_number,outstanding_balance) VALUES ((SELECT TOP(1) code FROM Persons WHERE id_number = '65XX0106100XX'),'10027602',724.26)
INSERT INTO Accounts (person_code,account_number,outstanding_balance) VALUES ((SELECT TOP(1) code FROM Persons WHERE id_number = '47XX2706770XX'),'10028579',1008.99)
INSERT INTO Accounts (person_code,account_number,outstanding_balance) VALUES ((SELECT TOP(1) code FROM Persons WHERE id_number = '59XX1002010XX'),'1002864',1059.43)
INSERT INTO Accounts (person_code,account_number,outstanding_balance) VALUES ((SELECT TOP(1) code FROM Persons WHERE id_number = '44XX0801450XX'),'10032223',719.65)
INSERT INTO Accounts (person_code,account_number,outstanding_balance) VALUES ((SELECT TOP(1) code FROM Persons WHERE id_number = '70XX3105540XX'),'10032274',0)
INSERT INTO Accounts (person_code,account_number,outstanding_balance) VALUES ((SELECT TOP(1) code FROM Persons WHERE id_number = '69XX2604250XX'),'1003267',843.59)
INSERT INTO Accounts (person_code,account_number,outstanding_balance) VALUES ((SELECT TOP(1) code FROM Persons WHERE id_number = '73XX0311610XX'),'10036466',1186.85)
INSERT INTO Accounts (person_code,account_number,outstanding_balance) VALUES ((SELECT TOP(1) code FROM Persons WHERE id_number = '66XX2705830XX'),'10036474',0)
INSERT INTO Accounts (person_code,account_number,outstanding_balance) VALUES ((SELECT TOP(1) code FROM Persons WHERE id_number = '76XX2904740XX'),'10036679',226.79)
INSERT INTO Accounts (person_code,account_number,outstanding_balance) VALUES ((SELECT TOP(1) code FROM Persons WHERE id_number = '70XX2251240XX'),'10037489',117.52)
INSERT INTO Accounts (person_code,account_number,outstanding_balance) VALUES ((SELECT TOP(1) code FROM Persons WHERE id_number = '67XX2750210XX'),'10039015',542.08)
INSERT INTO Accounts (person_code,account_number,outstanding_balance) VALUES ((SELECT TOP(1) code FROM Persons WHERE id_number = '77XX0952460XX'),'10039384',0)
INSERT INTO Accounts (person_code,account_number,outstanding_balance) VALUES ((SELECT TOP(1) code FROM Persons WHERE id_number = '67XX1256390XX'),'10040919',612.1)
INSERT INTO Accounts (person_code,account_number,outstanding_balance) VALUES ((SELECT TOP(1) code FROM Persons WHERE id_number = '70XX0355620XX'),'10041745',191.7)
INSERT INTO Accounts (person_code,account_number,outstanding_balance) VALUES ((SELECT TOP(1) code FROM Persons WHERE id_number = '57XX1907550XX'),'10044361',807.6)
INSERT INTO Accounts (person_code,account_number,outstanding_balance) VALUES ((SELECT TOP(1) code FROM Persons WHERE id_number = '69XX2459930XX'),'10045473',806.45)
INSERT INTO Accounts (person_code,account_number,outstanding_balance) VALUES ((SELECT TOP(1) code FROM Persons WHERE id_number = '70XX1507650XX'),'10045856',310.03)
INSERT INTO Accounts (person_code,account_number,outstanding_balance) VALUES ((SELECT TOP(1) code FROM Persons WHERE id_number = '69XX2005003XX'),'100460892',0)
INSERT INTO Accounts (person_code,account_number,outstanding_balance) VALUES ((SELECT TOP(1) code FROM Persons WHERE id_number = '73XX1306320XX'),'10048022',0)
INSERT INTO Accounts (person_code,account_number,outstanding_balance) VALUES ((SELECT TOP(1) code FROM Persons WHERE id_number = '58XX3100790XX'),'10048812',648.35)
INSERT INTO Accounts (person_code,account_number,outstanding_balance) VALUES ((SELECT TOP(1) code FROM Persons WHERE id_number = '72XX0401610XX'),'1005022',260.85)
INSERT INTO Accounts (person_code,account_number,outstanding_balance) VALUES ((SELECT TOP(1) code FROM Persons WHERE id_number = '67XX1105810XX'),'10050523',532.63)
INSERT INTO Accounts (person_code,account_number,outstanding_balance) VALUES ((SELECT TOP(1) code FROM Persons WHERE id_number = '73XX1807580XX'),'10052623',302.85)
INSERT INTO Accounts (person_code,account_number,outstanding_balance) VALUES ((SELECT TOP(1) code FROM Persons WHERE id_number = '68XX1211490XX'),'10052712',633.43)
INSERT INTO Accounts (person_code,account_number,outstanding_balance) VALUES ((SELECT TOP(1) code FROM Persons WHERE id_number = '77XX2304150XX'),'10053581',281.77)
INSERT INTO Accounts (person_code,account_number,outstanding_balance) VALUES ((SELECT TOP(1) code FROM Persons WHERE id_number = '76XX2902020XX'),'10053794',268.46)
INSERT INTO Accounts (person_code,account_number,outstanding_balance) VALUES ((SELECT TOP(1) code FROM Persons WHERE id_number = '58XX1802150XX'),'10054855',1803.28)
INSERT INTO Accounts (person_code,account_number,outstanding_balance) VALUES ((SELECT TOP(1) code FROM Persons WHERE id_number = '78XX1701830XX'),'10056262',789.74)
INSERT INTO Accounts (person_code,account_number,outstanding_balance) VALUES ((SELECT TOP(1) code FROM Persons WHERE id_number = '45XX2605080XX'),'10057269',359.6)
GO
INSERT INTO Transactions (account_code,transaction_date,capture_date,amount,description) VALUES ((SELECT TOP(1) code FROM Accounts WHERE account_number = '10000577'),GETDATE(),GETDATE(),234.99,'Charge Off Amount')
INSERT INTO Transactions (account_code,transaction_date,capture_date,amount,description) VALUES ((SELECT TOP(1) code FROM Accounts WHERE account_number = '10001085'),GETDATE(),GETDATE(),267.61,'Charge Off Amount')
INSERT INTO Transactions (account_code,transaction_date,capture_date,amount,description) VALUES ((SELECT TOP(1) code FROM Accounts WHERE account_number = '1000373'),GETDATE(),GETDATE(),520.67,'Charge Off Amount')
INSERT INTO Transactions (account_code,transaction_date,capture_date,amount,description) VALUES ((SELECT TOP(1) code FROM Accounts WHERE account_number = '10007792'),GETDATE(),GETDATE(),328.7,'Charge Off Amount')
INSERT INTO Transactions (account_code,transaction_date,capture_date,amount,description) VALUES ((SELECT TOP(1) code FROM Accounts WHERE account_number = '10011773'),GETDATE(),GETDATE(),641.7,'Charge Off Amount')
INSERT INTO Transactions (account_code,transaction_date,capture_date,amount,description) VALUES ((SELECT TOP(1) code FROM Accounts WHERE account_number = '10012044'),GETDATE(),GETDATE(),157.6,'Charge Off Amount')
INSERT INTO Transactions (account_code,transaction_date,capture_date,amount,description) VALUES ((SELECT TOP(1) code FROM Accounts WHERE account_number = '100137'),GETDATE(),GETDATE(),936.41,'Charge Off Amount')
INSERT INTO Transactions (account_code,transaction_date,capture_date,amount,description) VALUES ((SELECT TOP(1) code FROM Accounts WHERE account_number = '10014357'),GETDATE(),GETDATE(),440.87,'Charge Off Amount')
INSERT INTO Transactions (account_code,transaction_date,capture_date,amount,description) VALUES ((SELECT TOP(1) code FROM Accounts WHERE account_number = '10015256'),GETDATE(),GETDATE(),170.68,'Charge Off Amount')
INSERT INTO Transactions (account_code,transaction_date,capture_date,amount,description) VALUES ((SELECT TOP(1) code FROM Accounts WHERE account_number = '10016473'),GETDATE(),GETDATE(),663.77,'Charge Off Amount')
INSERT INTO Transactions (account_code,transaction_date,capture_date,amount,description) VALUES ((SELECT TOP(1) code FROM Accounts WHERE account_number = '10017712'),GETDATE(),GETDATE(),1471.27,'Charge Off Amount')
INSERT INTO Transactions (account_code,transaction_date,capture_date,amount,description) VALUES ((SELECT TOP(1) code FROM Accounts WHERE account_number = '10019324'),GETDATE(),GETDATE(),269.82,'Charge Off Amount')
INSERT INTO Transactions (account_code,transaction_date,capture_date,amount,description) VALUES ((SELECT TOP(1) code FROM Accounts WHERE account_number = '10019766'),GETDATE(),GETDATE(),485.78,'Charge Off Amount')
INSERT INTO Transactions (account_code,transaction_date,capture_date,amount,description) VALUES ((SELECT TOP(1) code FROM Accounts WHERE account_number = '10020241'),GETDATE(),GETDATE(),715.83,'Charge Off Amount')
INSERT INTO Transactions (account_code,transaction_date,capture_date,amount,description) VALUES ((SELECT TOP(1) code FROM Accounts WHERE account_number = '10020918'),GETDATE(),GETDATE(),81.35,'Charge Off Amount')
INSERT INTO Transactions (account_code,transaction_date,capture_date,amount,description) VALUES ((SELECT TOP(1) code FROM Accounts WHERE account_number = '10021663'),GETDATE(),GETDATE(),627.13,'Charge Off Amount')
INSERT INTO Transactions (account_code,transaction_date,capture_date,amount,description) VALUES ((SELECT TOP(1) code FROM Accounts WHERE account_number = '10021698'),GETDATE(),GETDATE(),426.43,'Charge Off Amount')
INSERT INTO Transactions (account_code,transaction_date,capture_date,amount,description) VALUES ((SELECT TOP(1) code FROM Accounts WHERE account_number = '10022821'),GETDATE(),GETDATE(),557.3,'Charge Off Amount')
INSERT INTO Transactions (account_code,transaction_date,capture_date,amount,description) VALUES ((SELECT TOP(1) code FROM Accounts WHERE account_number = '10022996'),GETDATE(),GETDATE(),299.2,'Charge Off Amount')
INSERT INTO Transactions (account_code,transaction_date,capture_date,amount,description) VALUES ((SELECT TOP(1) code FROM Accounts WHERE account_number = '10024492'),GETDATE(),GETDATE(),767.25,'Charge Off Amount')
INSERT INTO Transactions (account_code,transaction_date,capture_date,amount,description) VALUES ((SELECT TOP(1) code FROM Accounts WHERE account_number = '10027262'),GETDATE(),GETDATE(),483.55,'Charge Off Amount')
INSERT INTO Transactions (account_code,transaction_date,capture_date,amount,description) VALUES ((SELECT TOP(1) code FROM Accounts WHERE account_number = '10027602'),GETDATE(),GETDATE(),724.26,'Charge Off Amount')
INSERT INTO Transactions (account_code,transaction_date,capture_date,amount,description) VALUES ((SELECT TOP(1) code FROM Accounts WHERE account_number = '10028579'),GETDATE(),GETDATE(),1008.99,'Charge Off Amount')
INSERT INTO Transactions (account_code,transaction_date,capture_date,amount,description) VALUES ((SELECT TOP(1) code FROM Accounts WHERE account_number = '1002864'),GETDATE(),GETDATE(),1059.43,'Charge Off Amount')
INSERT INTO Transactions (account_code,transaction_date,capture_date,amount,description) VALUES ((SELECT TOP(1) code FROM Accounts WHERE account_number = '10032223'),GETDATE(),GETDATE(),719.65,'Charge Off Amount')
INSERT INTO Transactions (account_code,transaction_date,capture_date,amount,description) VALUES ((SELECT TOP(1) code FROM Accounts WHERE account_number = '1003267'),GETDATE(),GETDATE(),843.59,'Charge Off Amount')
INSERT INTO Transactions (account_code,transaction_date,capture_date,amount,description) VALUES ((SELECT TOP(1) code FROM Accounts WHERE account_number = '10036466'),GETDATE(),GETDATE(),1186.85,'Charge Off Amount')
INSERT INTO Transactions (account_code,transaction_date,capture_date,amount,description) VALUES ((SELECT TOP(1) code FROM Accounts WHERE account_number = '10036679'),GETDATE(),GETDATE(),226.79,'Charge Off Amount')
INSERT INTO Transactions (account_code,transaction_date,capture_date,amount,description) VALUES ((SELECT TOP(1) code FROM Accounts WHERE account_number = '10037489'),GETDATE(),GETDATE(),117.52,'Charge Off Amount')
INSERT INTO Transactions (account_code,transaction_date,capture_date,amount,description) VALUES ((SELECT TOP(1) code FROM Accounts WHERE account_number = '10039015'),GETDATE(),GETDATE(),542.08,'Charge Off Amount')
INSERT INTO Transactions (account_code,transaction_date,capture_date,amount,description) VALUES ((SELECT TOP(1) code FROM Accounts WHERE account_number = '10040919'),GETDATE(),GETDATE(),612.1,'Charge Off Amount')
INSERT INTO Transactions (account_code,transaction_date,capture_date,amount,description) VALUES ((SELECT TOP(1) code FROM Accounts WHERE account_number = '10041745'),GETDATE(),GETDATE(),191.7,'Charge Off Amount')
INSERT INTO Transactions (account_code,transaction_date,capture_date,amount,description) VALUES ((SELECT TOP(1) code FROM Accounts WHERE account_number = '10044361'),GETDATE(),GETDATE(),807.6,'Charge Off Amount')
INSERT INTO Transactions (account_code,transaction_date,capture_date,amount,description) VALUES ((SELECT TOP(1) code FROM Accounts WHERE account_number = '10045473'),GETDATE(),GETDATE(),806.45,'Charge Off Amount')
INSERT INTO Transactions (account_code,transaction_date,capture_date,amount,description) VALUES ((SELECT TOP(1) code FROM Accounts WHERE account_number = '10045856'),GETDATE(),GETDATE(),310.03,'Charge Off Amount')
INSERT INTO Transactions (account_code,transaction_date,capture_date,amount,description) VALUES ((SELECT TOP(1) code FROM Accounts WHERE account_number = '10048812'),GETDATE(),GETDATE(),648.35,'Charge Off Amount')
INSERT INTO Transactions (account_code,transaction_date,capture_date,amount,description) VALUES ((SELECT TOP(1) code FROM Accounts WHERE account_number = '1005022'),GETDATE(),GETDATE(),260.85,'Charge Off Amount')
INSERT INTO Transactions (account_code,transaction_date,capture_date,amount,description) VALUES ((SELECT TOP(1) code FROM Accounts WHERE account_number = '10050523'),GETDATE(),GETDATE(),532.63,'Charge Off Amount')
INSERT INTO Transactions (account_code,transaction_date,capture_date,amount,description) VALUES ((SELECT TOP(1) code FROM Accounts WHERE account_number = '10052623'),GETDATE(),GETDATE(),302.85,'Charge Off Amount')
INSERT INTO Transactions (account_code,transaction_date,capture_date,amount,description) VALUES ((SELECT TOP(1) code FROM Accounts WHERE account_number = '10052712'),GETDATE(),GETDATE(),633.43,'Charge Off Amount')
INSERT INTO Transactions (account_code,transaction_date,capture_date,amount,description) VALUES ((SELECT TOP(1) code FROM Accounts WHERE account_number = '10053581'),GETDATE(),GETDATE(),281.77,'Charge Off Amount')
INSERT INTO Transactions (account_code,transaction_date,capture_date,amount,description) VALUES ((SELECT TOP(1) code FROM Accounts WHERE account_number = '10053794'),GETDATE(),GETDATE(),268.46,'Charge Off Amount')
INSERT INTO Transactions (account_code,transaction_date,capture_date,amount,description) VALUES ((SELECT TOP(1) code FROM Accounts WHERE account_number = '10054855'),GETDATE(),GETDATE(),1803.28,'Charge Off Amount')
INSERT INTO Transactions (account_code,transaction_date,capture_date,amount,description) VALUES ((SELECT TOP(1) code FROM Accounts WHERE account_number = '10056262'),GETDATE(),GETDATE(),789.74,'Charge Off Amount')
INSERT INTO Transactions (account_code,transaction_date,capture_date,amount,description) VALUES ((SELECT TOP(1) code FROM Accounts WHERE account_number = '10057269'),GETDATE(),GETDATE(),359.6,'Charge Off Amount')
GO