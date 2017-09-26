USE Innovation4austria;
GO

ALTER TABLE Buchung
ADD
CONSTRAINT FK_Buchung_Firma
FOREIGN KEY (firma_id)
REFERENCES Firma(id);
GO

ALTER TABLE Buchung
ADD
CONSTRAINT FK_Buchung_Raum
FOREIGN KEY (raum_id)
REFERENCES Raum(id);
GO

ALTER TABLE Buchungsdetails
ADD
CONSTRAINT FK_Buchungdeatails_buchung_id
FOREIGN KEY (buchung_id)
REFERENCES Buchung(id);
GO

ALTER TABLE [log]
ADD
CONSTRAINT FK_Log_benutzer
FOREIGN KEY (benutzer_id)
REFERENCES Benutzer(id);
GO


ALTER TABLE Raum
ADD
CONSTRAINT FK_Raum_Art
FOREIGN KEY (art_id)
REFERENCES Art(id);
GO

ALTER TABLE Raum
ADD
CONSTRAINT FK_Raum_Bauwerk
FOREIGN KEY (bauwerk_id)
REFERENCES Bauwerk(id);
GO


ALTER TABLE Raum_Ausstattung
ADD
CONSTRAINT FK_Raum_Ausstattung_Raum
FOREIGN KEY (raum_id)
REFERENCES Raum(id);
GO

ALTER TABLE Raum_Ausstattung
ADD
CONSTRAINT FK_Raum_Ausstattung_Ausstattung
FOREIGN KEY (ausstattung_id)
REFERENCES Ausstattung(id);
GO

ALTER TABLE Bild
ADD
CONSTRAINT FK_Bild_Raum
FOREIGN KEY (raum_id)
REFERENCES Raum(id);
GO

ALTER TABLE Rechnung
ADD
CONSTRAINT FK_Rechnung_Firma
FOREIGN KEY (fa_id)
REFERENCES Firma(id);
GO

ALTER TABLE Rechnungsdetails
ADD
CONSTRAINT FK_Rechnungsdetails_Buchungsdetails
FOREIGN KEY (buchungsdetails_id)
REFERENCES Buchungsdetails(id);
GO

ALTER TABLE Rechnungsdetails
ADD
CONSTRAINT FK_Rechnungsdetails_Rechnung
FOREIGN KEY (rechnung_id)
REFERENCES Rechnung(id);
GO


ALTER TABLE Benutzer
ADD
CONSTRAINT FK_Benutzer_Rolle
FOREIGN KEY (rolle_id)
REFERENCES Rolle(id);
GO

ALTER TABLE Benutzer
ADD
CONSTRAINT FK_Benutzer_Firma
FOREIGN KEY (firma_id)
REFERENCES Firma(id);
GO