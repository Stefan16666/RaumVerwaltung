USE Innovation4austria;
GO

INSERT INTO Rolle(bezeichnung) VALUES('Firmenansprechpartner');
INSERT INTO Rolle(bezeichnung) VALUES('MitarbeiterIVA');
GO

INSERT INTO Firma(bezeichnung, strasse, nummer, plz, ort) VALUES('IBM', 'Obere-Donausstrasse', 1, 1020, 'Wien');
INSERT INTO Firma(bezeichnung, strasse, nummer, plz, ort) VALUES('Donauversicherung', 'Ringstrasse', 1, 1010, 'Wien');
INSERT INTO Firma(bezeichnung, strasse, nummer, plz, ort) VALUES('XYZ-Solution', 'Marc-Aurelstrasse', 50, 1010, 'Wien');
INSERT INTO Firma(bezeichnung, strasse, nummer, plz, ort) VALUES('IBM', 'Ameisgasse', 122, 1140, 'Wien');
GO

INSERT INTO Art(bezeichnung) VALUES('Besprechung');
INSERT INTO Art(bezeichnung) VALUES('Office');
INSERT INTO Art(bezeichnung) VALUES('Seminar');

GO

INSERT INTO Ausstattung(bezeichnung, bilddaten) VALUES('Computer', 
														(SELECT BulkColumn
														 FROM Openrowset( 
														 Bulk 'D:\LAP\Stefans_LAP-develop\Application\Innovation4Austria\Innovation4Austria.web\img\laptop.png', Single_Blob) AS import)
														 )
GO

INSERT INTO Ausstattung(bezeichnung, bilddaten) VALUES('Buffet', 
														(SELECT BulkColumn
														 FROM Openrowset( 
														 Bulk 'D:\LAP\Stefans_LAP-develop\Application\Innovation4Austria\Innovation4Austria.web\img\catering.png', Single_Blob) AS import)
														 )
GO

INSERT INTO Ausstattung(bezeichnung, bilddaten) VALUES('Flipchart', 
														(SELECT BulkColumn
														 FROM Openrowset( 
														 Bulk 'D:\LAP\Stefans_LAP-develop\Application\Innovation4Austria\Innovation4Austria.web\img\presentation-board-with-graph.png', Single_Blob) AS import)
														 )
GO

INSERT INTO Ausstattung(bezeichnung, bilddaten) VALUES('Internet', 
														(SELECT BulkColumn
														 FROM Openrowset( 
														 Bulk 'D:\LAP\Stefans_LAP-develop\Application\Innovation4Austria\Innovation4Austria.web\img\wifi.png', Single_Blob) AS import)
														 )
GO

INSERT INTO Ausstattung(bezeichnung, bilddaten) VALUES('Kaffee', 
														(SELECT BulkColumn
														 FROM Openrowset( 
														 Bulk 'D:\LAP\Stefans_LAP-develop\Application\Innovation4Austria\Innovation4Austria.web\img\tea-cup.png', Single_Blob) AS import)
														 )
GO

INSERT INTO Ausstattung(bezeichnung, bilddaten) VALUES('Drucker', 
														(SELECT BulkColumn
														 FROM Openrowset( 
														 Bulk 'D:\LAP\Stefans_LAP-develop\Application\Innovation4Austria\Innovation4Austria.web\img\printer.png', Single_Blob) AS import)
														 )
GO

INSERT INTO Bauwerk(bezeichnung, strasse, nummer, plz, ort)
VALUES('Milleniumtower', 'Handelskai', 120, 1210, 'Wien');
GO

INSERT INTO Raum(bezeichnung,beschreibung, art_id, bauwerk_id, groesse, preis, bilddaten) VALUES ('0125', 'Heller Raum mit Blick auf Donau', 1,1,34,12,
														(SELECT BulkColumn
														 FROM Openrowset( 
														 Bulk 'D:\LAP\Stefans_LAP-develop\Application\Innovation4Austria\Innovation4Austria.web\img\besprechungsraum.jpg', Single_Blob) AS import)
														 );
INSERT INTO Raum(bezeichnung,beschreibung, art_id, bauwerk_id, groesse, preis, bilddaten) VALUES ('0203', 'Highclass office', 1,1,34,12,
														(SELECT BulkColumn
														 FROM Openrowset( 
														 Bulk 'D:\LAP\Stefans_LAP-develop\Application\Innovation4Austria\Innovation4Austria.web\img\1988.jpg', Single_Blob) AS import)
														 );
INSERT INTO Raum(bezeichnung,beschreibung, art_id, bauwerk_id, groesse, preis, bilddaten) VALUES ('0126', 'Heller Raum mit Blick auf Donau', 1,1,34,12,
														(SELECT BulkColumn
														 FROM Openrowset( 
														 Bulk 'D:\LAP\Stefans_LAP-develop\Application\Innovation4Austria\Innovation4Austria.web\img\besprechungsraum.jpg', Single_Blob) AS import)
														 );
INSERT INTO Raum(bezeichnung,beschreibung, art_id, bauwerk_id, groesse, preis, bilddaten) VALUES ('0201', 'Highclass office', 1,1,34,12,
														(SELECT BulkColumn
														 FROM Openrowset( 
														 Bulk 'D:\LAP\Stefans_LAP-develop\Application\Innovation4Austria\Innovation4Austria.web\img\1988.jpg', Single_Blob) AS import)
														 );
														 														 
INSERT INTO Raum(bezeichnung,beschreibung, art_id, bauwerk_id, groesse, preis, bilddaten) VALUES ('0713', 'Penthhouseoffice', 2,1,34,25,
														(SELECT BulkColumn
														 FROM Openrowset( 
														 Bulk 'D:\LAP\Stefans_LAP-develop\Application\Innovation4Austria\Innovation4Austria.web\img\bueroraum.jpg', Single_Blob) AS import)
														 );
INSERT INTO Raum(bezeichnung,beschreibung, art_id, bauwerk_id, groesse, preis, bilddaten) VALUES ('0115', 'Heller Raum mit Blick auf Donau', 1,1,34,12,
														(SELECT BulkColumn
														 FROM Openrowset( 
														 Bulk 'D:\LAP\Stefans_LAP-develop\Application\Innovation4Austria\Innovation4Austria.web\img\Raum115.jpg', Single_Blob) AS import)
														 );
INSERT INTO Raum(bezeichnung,beschreibung, art_id, bauwerk_id, groesse, preis, bilddaten) VALUES ('0233', 'Ruhiger Raum hoch gelegen', 2,1,34,12,
														(SELECT BulkColumn
														 FROM Openrowset( 
														 Bulk 'D:\LAP\Stefans_LAP-develop\Application\Innovation4Austria\Innovation4Austria.web\img\Raum233.jpg', Single_Blob) AS import)
														 );
INSERT INTO Raum(bezeichnung,beschreibung, art_id, bauwerk_id, groesse, preis, bilddaten) VALUES ('0644', 'Raum mit grossen Tischen', 1,1,34,12,
														(SELECT BulkColumn
														 FROM Openrowset( 
														 Bulk 'D:\LAP\Stefans_LAP-develop\Application\Innovation4Austria\Innovation4Austria.web\img\besprechungsRaum3.jpg', Single_Blob) AS import)
														 );
INSERT INTO Raum(bezeichnung,beschreibung, art_id, bauwerk_id, groesse, preis, bilddaten) VALUES ('0715', 'ideale Ausstattung', 2,1,34,12,
														(SELECT BulkColumn
														 FROM Openrowset( 
														 Bulk 'D:\LAP\Stefans_LAP-develop\Application\Innovation4Austria\Innovation4Austria.web\img\Raum715.jpg', Single_Blob) AS import)
														 );
INSERT INTO Raum(bezeichnung,beschreibung, art_id, bauwerk_id, groesse, preis, bilddaten) VALUES ('0514', 'Raum mit Drucker', 2,1,34,12,
														(SELECT BulkColumn
														 FROM Openrowset( 
														 Bulk 'D:\LAP\Stefans_LAP-develop\Application\Innovation4Austria\Innovation4Austria.web\img\moebel_buero.jpg', Single_Blob) AS import)
														 );
INSERT INTO Raum(bezeichnung,beschreibung, art_id, bauwerk_id, groesse, preis, bilddaten) VALUES ('0718', 'ideale Ausstattung', 2,1,34,12,
														(SELECT BulkColumn
														 FROM Openrowset( 
														 Bulk 'D:\LAP\Stefans_LAP-develop\Application\Innovation4Austria\Innovation4Austria.web\img\Raum715.jpg', Single_Blob) AS import)
														 );
INSERT INTO Raum(bezeichnung,beschreibung, art_id, bauwerk_id, groesse, preis, bilddaten) VALUES ('0555', 'Raum mit Drucker', 2,1,34,12,
														(SELECT BulkColumn
														 FROM Openrowset( 
														 Bulk 'D:\LAP\Stefans_LAP-develop\Application\Innovation4Austria\Innovation4Austria.web\img\moebel_buero.jpg', Single_Blob) AS import)
														 );													 
INSERT INTO Raum(bezeichnung,beschreibung, art_id, bauwerk_id, groesse, preis, bilddaten) VALUES ('0515', 'Besprechungsraum', 1,1,34,12,
														(SELECT BulkColumn
														 FROM Openrowset( 
														 Bulk 'D:\LAP\Stefans_LAP-develop\Application\Innovation4Austria\Innovation4Austria.web\img\besprechungsRaum4.jpg', Single_Blob) AS import)
														 );
GO
INSERT INTO Raum(bezeichnung,beschreibung, art_id, bauwerk_id, groesse, preis, bilddaten) VALUES ('0614', 'Besprechungsraum', 1,1,34,12,
														(SELECT BulkColumn
														 FROM Openrowset( 
														 Bulk 'D:\LAP\Stefans_LAP-develop\Application\Innovation4Austria\Innovation4Austria.web\img\besprechungsRaum4.jpg', Single_Blob) AS import)
														 );
GO
INSERT INTO Raum(bezeichnung,beschreibung, art_id, bauwerk_id, groesse, preis, bilddaten) VALUES ('0426', 'Ruhiger Raum hoch gelegen', 2,1,34,12,
														(SELECT BulkColumn
														 FROM Openrowset( 
														 Bulk 'D:\LAP\Stefans_LAP-develop\Application\Innovation4Austria\Innovation4Austria.web\img\Raum426.jpg', Single_Blob) AS import)
														 );
INSERT INTO Raum(bezeichnung,beschreibung, art_id, bauwerk_id, groesse, preis, bilddaten) VALUES ('0425', 'Raum mit grossen Tischen', 2,1,34,12,
														(SELECT BulkColumn
														 FROM Openrowset( 
														 Bulk 'D:\LAP\Stefans_LAP-develop\Application\Innovation4Austria\Innovation4Austria.web\img\Raum425.jpg', Single_Blob) AS import)
														 );
INSERT INTO Raum(bezeichnung,beschreibung, art_id, bauwerk_id, groesse, preis, bilddaten) VALUES ('0211', 'ideale Ausstattung', 2,1,34,12,
														(SELECT BulkColumn
														 FROM Openrowset( 
														 Bulk 'D:\LAP\Stefans_LAP-develop\Application\Innovation4Austria\Innovation4Austria.web\img\Raum211.jpg', Single_Blob) AS import)
														 );
INSERT INTO Raum(bezeichnung,beschreibung, art_id, bauwerk_id, groesse, preis, bilddaten) VALUES ('0345', 'Raum mit Drucker', 2,1,34,12,
														(SELECT BulkColumn
														 FROM Openrowset( 
														 Bulk 'D:\LAP\Stefans_LAP-develop\Application\Innovation4Austria\Innovation4Austria.web\img\Raum345.jpg', Single_Blob) AS import)
														 );
INSERT INTO Raum(bezeichnung,beschreibung, art_id, bauwerk_id, groesse, preis, bilddaten) VALUES ('0789', 'Raum mit Fax', 2,1,34,12,
														(SELECT BulkColumn
														 FROM Openrowset( 
														 Bulk 'D:\LAP\Stefans_LAP-develop\Application\Innovation4Austria\Innovation4Austria.web\img\Raum789.jpg', Single_Blob) AS import)
														 );
GO
INSERT INTO Raum(bezeichnung,beschreibung, art_id, bauwerk_id, groesse, preis, bilddaten) VALUES ('0321', 'Raum mit grossen Tischen', 2,1,34,12,
														(SELECT BulkColumn
														 FROM Openrowset( 
														 Bulk 'D:\LAP\Stefans_LAP-develop\Application\Innovation4Austria\Innovation4Austria.web\img\Raum425.jpg', Single_Blob) AS import)
														 );
INSERT INTO Raum(bezeichnung,beschreibung, art_id, bauwerk_id, groesse, preis, bilddaten) VALUES ('0322', 'ideale Ausstattung', 2,1,34,12,
														(SELECT BulkColumn
														 FROM Openrowset( 
														 Bulk 'D:\LAP\Stefans_LAP-develop\Application\Innovation4Austria\Innovation4Austria.web\img\Raum211.jpg', Single_Blob) AS import)
														 );
INSERT INTO Raum(bezeichnung,beschreibung, art_id, bauwerk_id, groesse, preis, bilddaten) VALUES ('0256', 'Raum mit Drucker', 2,1,34,12,
														(SELECT BulkColumn
														 FROM Openrowset( 
														 Bulk 'D:\LAP\Stefans_LAP-develop\Application\Innovation4Austria\Innovation4Austria.web\img\Raum345.jpg', Single_Blob) AS import)
														 );
INSERT INTO Raum(bezeichnung,beschreibung, art_id, bauwerk_id, groesse, preis, bilddaten) VALUES ('0514', 'Raum mit Fax', 2,1,34,12,
														(SELECT BulkColumn
														 FROM Openrowset( 
														 Bulk 'D:\LAP\Stefans_LAP-develop\Application\Innovation4Austria\Innovation4Austria.web\img\Raum789.jpg', Single_Blob) AS import)
														 );
INSERT INTO Raum(bezeichnung,beschreibung, art_id, bauwerk_id, groesse, preis, bilddaten) VALUES ('0615', 'Seminarraum', 3,1,34,12,
														(SELECT BulkColumn
														 FROM Openrowset( 
														 Bulk 'D:\LAP\Stefans_LAP-develop\Application\Innovation4Austria\Innovation4Austria.web\img\seewli.jpg', Single_Blob) AS import)
														 );	
INSERT INTO Raum(bezeichnung,beschreibung, art_id, bauwerk_id, groesse, preis, bilddaten) VALUES ('0617', 'Seminare', 3,1,34,12,
														(SELECT BulkColumn
														 FROM Openrowset( 
														 Bulk 'D:\LAP\Stefans_LAP-develop\Application\Innovation4Austria\Innovation4Austria.web\img\Seminar1.jpg', Single_Blob) AS import)
														 );	
INSERT INTO Raum(bezeichnung,beschreibung, art_id, bauwerk_id, groesse, preis, bilddaten) VALUES ('0618', 'Raum mit Fax', 3,1,34,12,
														(SELECT BulkColumn
														 FROM Openrowset( 
														 Bulk 'D:\LAP\Stefans_LAP-develop\Application\Innovation4Austria\Innovation4Austria.web\img\Seminar2.jpg', Single_Blob) AS import)
														 );	
INSERT INTO Raum(bezeichnung,beschreibung, art_id, bauwerk_id, groesse, preis, bilddaten) VALUES ('0619', 'Raum mit Fax', 3,1,34,12,
														(SELECT BulkColumn
														 FROM Openrowset( 
														 Bulk 'D:\LAP\Stefans_LAP-develop\Application\Innovation4Austria\Innovation4Austria.web\img\Seminar3.jpg', Single_Blob) AS import)
														 );															 
GO
INSERT INTO Raum_Ausstattung(raum_id, ausstattung_id, Anzahl_Ausstattung) VALUES(1,1, 10);
INSERT INTO Raum_Ausstattung(raum_id, ausstattung_id, Anzahl_Ausstattung) VALUES(1,2, 15);
INSERT INTO Raum_Ausstattung(raum_id, ausstattung_id, Anzahl_Ausstattung) VALUES(2,1, 12);
INSERT INTO Raum_Ausstattung(raum_id, ausstattung_id, Anzahl_Ausstattung) VALUES(2,3, 12);
INSERT INTO Raum_Ausstattung(raum_id, ausstattung_id, Anzahl_Ausstattung) VALUES(1,5, 12);
INSERT INTO Raum_Ausstattung(raum_id, ausstattung_id, Anzahl_Ausstattung) VALUES(3,1, 12);
INSERT INTO Raum_Ausstattung(raum_id, ausstattung_id, Anzahl_Ausstattung) VALUES(2,2, 12);
INSERT INTO Raum_Ausstattung(raum_id, ausstattung_id, Anzahl_Ausstattung) VALUES(4,2, 12);
INSERT INTO Raum_Ausstattung(raum_id, ausstattung_id, Anzahl_Ausstattung) VALUES(2,5, 8);
INSERT INTO Raum_Ausstattung(raum_id, ausstattung_id, Anzahl_Ausstattung) VALUES(2,4, 6);
INSERT INTO Raum_Ausstattung(raum_id, ausstattung_id, Anzahl_Ausstattung) VALUES(3,1, 12);
INSERT INTO Raum_Ausstattung(raum_id, ausstattung_id, Anzahl_Ausstattung) VALUES(5,2, 12);
INSERT INTO Raum_Ausstattung(raum_id, ausstattung_id, Anzahl_Ausstattung) VALUES(6,2, 6);
INSERT INTO Raum_Ausstattung(raum_id, ausstattung_id, Anzahl_Ausstattung) VALUES(2,6, 5);
INSERT INTO Raum_Ausstattung(raum_id, ausstattung_id, Anzahl_Ausstattung) VALUES(4,2, 3);

INSERT INTO Raum_Ausstattung(raum_id, ausstattung_id, Anzahl_Ausstattung) VALUES(7,1, 10);
INSERT INTO Raum_Ausstattung(raum_id, ausstattung_id, Anzahl_Ausstattung) VALUES(8,2, 15);
INSERT INTO Raum_Ausstattung(raum_id, ausstattung_id, Anzahl_Ausstattung) VALUES(22,6, 12);
INSERT INTO Raum_Ausstattung(raum_id, ausstattung_id, Anzahl_Ausstattung) VALUES(10,3, 12);
INSERT INTO Raum_Ausstattung(raum_id, ausstattung_id, Anzahl_Ausstattung) VALUES(11,5, 12);
INSERT INTO Raum_Ausstattung(raum_id, ausstattung_id, Anzahl_Ausstattung) VALUES(13,1, 12);
INSERT INTO Raum_Ausstattung(raum_id, ausstattung_id, Anzahl_Ausstattung) VALUES(12,2, 12);
INSERT INTO Raum_Ausstattung(raum_id, ausstattung_id, Anzahl_Ausstattung) VALUES(14,2, 12);
INSERT INTO Raum_Ausstattung(raum_id, ausstattung_id, Anzahl_Ausstattung) VALUES(15,5, 8);
INSERT INTO Raum_Ausstattung(raum_id, ausstattung_id, Anzahl_Ausstattung) VALUES(16,4, 6);
INSERT INTO Raum_Ausstattung(raum_id, ausstattung_id, Anzahl_Ausstattung) VALUES(17,1, 12);
INSERT INTO Raum_Ausstattung(raum_id, ausstattung_id, Anzahl_Ausstattung) VALUES(18,1, 12);
INSERT INTO Raum_Ausstattung(raum_id, ausstattung_id, Anzahl_Ausstattung) VALUES(19,2, 6);
INSERT INTO Raum_Ausstattung(raum_id, ausstattung_id, Anzahl_Ausstattung) VALUES(20,6, 5);
INSERT INTO Raum_Ausstattung(raum_id, ausstattung_id, Anzahl_Ausstattung) VALUES(21,2, 3);

INSERT INTO Raum_Ausstattung(raum_id, ausstattung_id, Anzahl_Ausstattung) VALUES(22,1, 10);
INSERT INTO Raum_Ausstattung(raum_id, ausstattung_id, Anzahl_Ausstattung) VALUES(23,2, 15);
INSERT INTO Raum_Ausstattung(raum_id, ausstattung_id, Anzahl_Ausstattung) VALUES(22,4, 12);
INSERT INTO Raum_Ausstattung(raum_id, ausstattung_id, Anzahl_Ausstattung) VALUES(21,3, 12);
INSERT INTO Raum_Ausstattung(raum_id, ausstattung_id, Anzahl_Ausstattung) VALUES(19,5, 12);
INSERT INTO Raum_Ausstattung(raum_id, ausstattung_id, Anzahl_Ausstattung) VALUES(20,1, 12);
INSERT INTO Raum_Ausstattung(raum_id, ausstattung_id, Anzahl_Ausstattung) VALUES(16,2, 12);
INSERT INTO Raum_Ausstattung(raum_id, ausstattung_id, Anzahl_Ausstattung) VALUES(13,2, 12);
INSERT INTO Raum_Ausstattung(raum_id, ausstattung_id, Anzahl_Ausstattung) VALUES(15,5, 8);
INSERT INTO Raum_Ausstattung(raum_id, ausstattung_id, Anzahl_Ausstattung) VALUES(9,4, 6);
INSERT INTO Raum_Ausstattung(raum_id, ausstattung_id, Anzahl_Ausstattung) VALUES(8,4, 12);
INSERT INTO Raum_Ausstattung(raum_id, ausstattung_id, Anzahl_Ausstattung) VALUES(11,2, 12);
INSERT INTO Raum_Ausstattung(raum_id, ausstattung_id, Anzahl_Ausstattung) VALUES(8,6, 6);
INSERT INTO Raum_Ausstattung(raum_id, ausstattung_id, Anzahl_Ausstattung) VALUES(15,6, 5);
INSERT INTO Raum_Ausstattung(raum_id, ausstattung_id, Anzahl_Ausstattung) VALUES(13,1, 3);

INSERT INTO Raum_Ausstattung(raum_id, ausstattung_id, Anzahl_Ausstattung) VALUES(22,2, 10);
INSERT INTO Raum_Ausstattung(raum_id, ausstattung_id, Anzahl_Ausstattung) VALUES(23,1, 15);
INSERT INTO Raum_Ausstattung(raum_id, ausstattung_id, Anzahl_Ausstattung) VALUES(15,1, 12);
INSERT INTO Raum_Ausstattung(raum_id, ausstattung_id, Anzahl_Ausstattung) VALUES(21,1, 12);
INSERT INTO Raum_Ausstattung(raum_id, ausstattung_id, Anzahl_Ausstattung) VALUES(19,3, 12);
INSERT INTO Raum_Ausstattung(raum_id, ausstattung_id, Anzahl_Ausstattung) VALUES(20,5, 12);
INSERT INTO Raum_Ausstattung(raum_id, ausstattung_id, Anzahl_Ausstattung) VALUES(16,4, 12);
INSERT INTO Raum_Ausstattung(raum_id, ausstattung_id, Anzahl_Ausstattung) VALUES(13,3, 12);
INSERT INTO Raum_Ausstattung(raum_id, ausstattung_id, Anzahl_Ausstattung) VALUES(15,3, 8);
INSERT INTO Raum_Ausstattung(raum_id, ausstattung_id, Anzahl_Ausstattung) VALUES(9,6, 6);
INSERT INTO Raum_Ausstattung(raum_id, ausstattung_id, Anzahl_Ausstattung) VALUES(8,5, 12);
INSERT INTO Raum_Ausstattung(raum_id, ausstattung_id, Anzahl_Ausstattung) VALUES(11,1, 12);
INSERT INTO Raum_Ausstattung(raum_id, ausstattung_id, Anzahl_Ausstattung) VALUES(8,1, 6);
INSERT INTO Raum_Ausstattung(raum_id, ausstattung_id, Anzahl_Ausstattung) VALUES(15,4, 5);
INSERT INTO Raum_Ausstattung(raum_id, ausstattung_id, Anzahl_Ausstattung) VALUES(13,3, 3);

INSERT INTO Raum_Ausstattung(raum_id, ausstattung_id, Anzahl_Ausstattung) VALUES(1,5, 10);
INSERT INTO Raum_Ausstattung(raum_id, ausstattung_id, Anzahl_Ausstattung) VALUES(1,4, 15);
INSERT INTO Raum_Ausstattung(raum_id, ausstattung_id, Anzahl_Ausstattung) VALUES(9,1, 12);
INSERT INTO Raum_Ausstattung(raum_id, ausstattung_id, Anzahl_Ausstattung) VALUES(9,3, 12);
INSERT INTO Raum_Ausstattung(raum_id, ausstattung_id, Anzahl_Ausstattung) VALUES(11,5, 12);
INSERT INTO Raum_Ausstattung(raum_id, ausstattung_id, Anzahl_Ausstattung) VALUES(23,3, 12);
INSERT INTO Raum_Ausstattung(raum_id, ausstattung_id, Anzahl_Ausstattung) VALUES(22,2, 12);
INSERT INTO Raum_Ausstattung(raum_id, ausstattung_id, Anzahl_Ausstattung) VALUES(21,2, 12);
INSERT INTO Raum_Ausstattung(raum_id, ausstattung_id, Anzahl_Ausstattung) VALUES(18,5, 8);
INSERT INTO Raum_Ausstattung(raum_id, ausstattung_id, Anzahl_Ausstattung) VALUES(18,4, 6);
INSERT INTO Raum_Ausstattung(raum_id, ausstattung_id, Anzahl_Ausstattung) VALUES(16,1, 12);
INSERT INTO Raum_Ausstattung(raum_id, ausstattung_id, Anzahl_Ausstattung) VALUES(15,2, 12);
INSERT INTO Raum_Ausstattung(raum_id, ausstattung_id, Anzahl_Ausstattung) VALUES(12,2, 6);
INSERT INTO Raum_Ausstattung(raum_id, ausstattung_id, Anzahl_Ausstattung) VALUES(11,6, 5);
INSERT INTO Raum_Ausstattung(raum_id, ausstattung_id, Anzahl_Ausstattung) VALUES(10,2, 3);

INSERT INTO Raum_Ausstattung(raum_id, ausstattung_id, Anzahl_Ausstattung) VALUES(24,4, 10);
INSERT INTO Raum_Ausstattung(raum_id, ausstattung_id, Anzahl_Ausstattung) VALUES(25,1, 12);
GO


INSERT INTO Benutzer(email, passwort, vorname, nachname, rolle_id, firma_id)
VALUES('muster@gmail.at', HASHBYTES('SHA2_512', '123user!'), 'Max', 'Muster', 1, 1);
INSERT INTO Benutzer(email, passwort, vorname, nachname, rolle_id, firma_id)
VALUES('marco@gmail.at', HASHBYTES('SHA2_512', '123user!'), 'Marco', 'Wurz',1, 2);
INSERT INTO Benutzer(email, passwort, vorname, nachname, rolle_id, firma_id)
VALUES('claudia@gmail.at', HASHBYTES('SHA2_512', '123user!'), 'Claudia', 'Stiegl', 1, 3);
INSERT INTO Benutzer(email, passwort, vorname, nachname, rolle_id, firma_id)
VALUES('daniel@gmail.at', HASHBYTES('SHA2_512', '123user!'), 'Daniel', 'Zalli', 1, 4);
INSERT INTO Benutzer(email, passwort, vorname, nachname, rolle_id, firma_id)
VALUES('stefan@gmail.at', HASHBYTES('SHA2_512', '123user!'), 'Stefan', 'Groinig', 2, null);
GO


INSERT INTO Buchung (Raum_id, firma_id) VALUES (4, 1);
INSERT INTO Buchung (Raum_id, firma_id) VALUES (2, 3);
INSERT INTO Buchung (Raum_id, firma_id) VALUES (4, 1);
INSERT INTO Buchung (Raum_id, firma_id) VALUES (6, 2);
INSERT INTO Buchung (Raum_id, firma_id) VALUES (1, 3);
INSERT INTO Buchung (Raum_id, firma_id) VALUES (4, 1);
GO


INSERT INTO Buchungsdetails (buchung_id, datum, preis) VALUES( 4, '14-06-2017', 12.0);
INSERT INTO Buchungsdetails (buchung_id, datum, preis) VALUES( 4, '15-06-2017', 12.0);
INSERT INTO Buchungsdetails (buchung_id, datum, preis) VALUES( 1, '09-10-2017', 12.0);
INSERT INTO Buchungsdetails (buchung_id, datum, preis) VALUES( 1, '10-10-2017', 12.0);
INSERT INTO Buchungsdetails (buchung_id, datum, preis) VALUES( 1, '11-10-2017', 12.0);
INSERT INTO Buchungsdetails (buchung_id, datum, preis) VALUES( 5, '23-12-2017', 12.0);
INSERT INTO Buchungsdetails (buchung_id, datum, preis) VALUES( 5, '24-12-2017', 12.0);
INSERT INTO Buchungsdetails (buchung_id, datum, preis) VALUES( 3, '28-10-2017', 12.0);
INSERT INTO Buchungsdetails (buchung_id, datum, preis) VALUES( 2, '29-11-2017', 12.0);
INSERT INTO Buchungsdetails (buchung_id, datum, preis) VALUES( 6, '29-11-2017', 12.0);
INSERT INTO Buchungsdetails (buchung_id, datum, preis) VALUES( 2, '29-11-2016', 12.0);
INSERT INTO Buchungsdetails (buchung_id, datum, preis) VALUES( 6, '29-11-2016', 12.0);
INSERT INTO Buchungsdetails (buchung_id, datum, preis) VALUES( 2, '29-10-2016', 12.0);
INSERT INTO Buchungsdetails (buchung_id, datum, preis) VALUES( 6, '29-10-2016', 12.0);
GO

INSERT INTO Rechnung (datum, fa_id) VALUES ('2017-02-26', 1)
INSERT INTO Rechnung (datum, fa_id) VALUES ('2017-01-12', 2)
INSERT INTO Rechnung (datum, fa_id) VALUES ('2017-02-28', 3)
INSERT INTO Rechnung (datum, fa_id) VALUES ('2017-01-16', 4)
GO

INSERT INTO Rechnungsdetails (rechnung_id, buchungsdetails_id) VAlUES (1,9);
INSERT INTO Rechnungsdetails (rechnung_id, buchungsdetails_id) VAlUES (1,1);
INSERT INTO Rechnungsdetails (rechnung_id, buchungsdetails_id) VAlUES (1,2);
INSERT INTO Rechnungsdetails (rechnung_id, buchungsdetails_id) VAlUES (1,4);
INSERT INTO Rechnungsdetails (rechnung_id, buchungsdetails_id) VAlUES (2,3);
INSERT INTO Rechnungsdetails (rechnung_id, buchungsdetails_id) VAlUES (2,6);
INSERT INTO Rechnungsdetails (rechnung_id, buchungsdetails_id) VAlUES (4,5);
INSERT INTO Rechnungsdetails (rechnung_id, buchungsdetails_id) VAlUES (1,7);
INSERT INTO Rechnungsdetails (rechnung_id, buchungsdetails_id) VAlUES (4,8);
GO

INSERT INTO Kreditkarte (bezeichnung) VAlUES ('Mastercard');
INSERT INTO Kreditkarte (bezeichnung) VAlUES ('Visa');
INSERT INTO Kreditkarte (bezeichnung) VAlUES ('Diners');
INSERT INTO Kreditkarte (bezeichnung) VAlUES ('Amex');
GO
--------