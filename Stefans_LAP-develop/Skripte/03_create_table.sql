USE Innovation4austria;
GO

CREATE TABLE Ausstattung(
	id INT IDENTITY NOT NULL,
	bezeichnung NVARCHAR(50) NOT NULL,
	bilddaten VARBINARY(MAX) NOT NULL,
	aktiv bit default 1 NOT NULL
);


CREATE TABLE Bauwerk(
	id INT IDENTITY NOT NULL,	
	strasse NVARCHAR(50) NOT NULL,
	nummer INT NOT NULL,
	plz INT NOT NULL,
	ort NVARCHAR(50) NOT NULL,
	bezeichnung NVARCHAR(50) NOT NULL,
	aktiv bit default 1 NOT NULL
);

CREATE TABLE Benutzer (
	id INT IDENTITY NOT NULL,	
	passwort VARBINARY(1000) NOT NULL,
	email NVARCHAR(50) NOT NULL,
	vorname NVARCHAR(50) NOT NULL,
	nachname NVARCHAR(50) NOT NULL,	
	Firma_id INT,
	Rolle_id INT NOT NULL,
	aktiv bit default 1 NOT NULL
);

CREATE TABLE Bild (
	id INT IDENTITY NOT NULL,
	bilddaten VARBINARY(MAX) NOT NULL,
	raum_id INT
);


CREATE TABLE Buchung (
	id INT IDENTITY NOT NULL,	
	raum_id INT NOT NULL,
	firma_id INT NOT NULL,
	aktiv bit default 1 NOT NULL
);

CREATE TABLE Buchungsdetails(
	id INT IDENTITY NOT NULL,
	buchung_id INT NOT NULL,
	preis DECIMAL NOT NULL,
	datum DATETIME NOT NULL
);

CREATE TABLE Firma(
	id INT IDENTITY NOT NULL,	
	strasse NVARCHAR(50) NOT NULL,
	nummer NVARCHAR(5) NOT NULL,
	plz NVARCHAR(5) NOT NULL,
	ort NVARCHAR(50) NOT NULL,
	bezeichnung NVARCHAR(50) NOT NULL,
	aktiv bit default 1 NOT NULL
);

CREATE TABLE Kreditkarte(
	id INT IDENTITY NOT NULL,	
	bezeichnung NVARCHAR(50) NOT NULL
);

CREATE TABLE Stornierung(
	id INT IDENTITY NOT NULL,
	Benutzer_id INT NOT NULL,
	Datum DATETIME NOT NULL
);

CREATE TABLE [dbo].[Log] (
    [Id] [int] IDENTITY (1, 1) NOT NULL,
    [Date] [datetime] NOT NULL,
    [Thread] [varchar] (255) NOT NULL,
    [Level] [varchar] (50) NOT NULL,
    [Logger] [varchar] (255) NOT NULL,
    [Message] [varchar] (4000) NOT NULL,
    [Exception] [varchar] (2000) NULL,
	InnerException VARCHAR (2000) NULL,
	benutzer_id INT
);

CREATE TABLE Raum (
	id INT IDENTITY NOT NULL,	
	bezeichnung NVARCHAR(50) NOT NULL,
	beschreibung NVARCHAR(31) NOT NULL,
	bilddaten VARBINARY(MAX) NOT NULL,
	bauwerk_id INT NOT NULL,
	groesse INT NOT NULL,
	art_id INT NOT NULL,	
	preis INT NOT NULL,
	aktiv bit default 1 NOT NULL
);

CREATE TABLE Art (
	id INT IDENTITY NOT NULL,
	bezeichnung NVARCHAR(50) NOT NULL	
);

CREATE TABLE Raum_Ausstattung (
	id INT IDENTITY NOT NULL,
	ausstattung_id INT NOT NULL,
	raum_id INT,
	Anzahl_Ausstattung INT NOT NULL,
	aktiv bit default 1 NOT NULL

);

CREATE TABLE Rechnung(
	id INT IDENTITY NOT NULL,
	datum DATE,
	bezahlt BIT DEFAULT 0 NOT NULL,
	fa_id INT NOT NULL	
);

CREATE TABLE Rechnungsdetails(
	id INT IDENTITY NOT NULL,
	rechnung_id INT NOT NULL,
	buchungsdetails_id INT NOT NULL
);

CREATE TABLE Rolle (
	id INT IDENTITY NOT NULL,
	bezeichnung NVARCHAR(255) NOT NULL,
	aktiv bit default 1 NOT NULL
);

GO