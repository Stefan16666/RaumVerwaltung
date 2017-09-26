CREATE VIEW AlleRechnungenEinerFirma
AS
	SELECT  r.datum AS RechnungsAussstellungsDatum,
			r.id AS Rechnungs_id,
			rd.id AS Rechnungsdetails_id,
			bd.id AS Buchungsdetails_id,
			bd.datum AS BuchungsDetailDatum,
			bd.preis AS BuchungsDetailPreis,
			b.id AS Buchungs_id,
			b.aktiv AS BuchungAktiv,
			b.raum_id,
			ra.bezeichnung AS Raumbezeichnung,
			a.bezeichnung AS Raumartbezeichnung			
	FROM Rechnung AS r
	JOIN Rechnungsdetails AS rd
	ON r.id = rd.rechnung_id
	JOIN Buchungsdetails AS bd
	ON rd.buchungsdetails_id = bd.id
	JOIN Buchung AS b
	ON b.id = bd.buchung_id
	JOIN Raum AS ra
	ON b.raum_id = ra.bezeichnung
	JOIN Art AS a
	ON ra.art_id = a.id
GO