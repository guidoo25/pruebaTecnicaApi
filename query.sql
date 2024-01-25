SELECT b.*
FROM BookingEntities b
INNER JOIN BillboardEntities bb ON b.BillboardId = bb.Id
INNER JOIN MovieEntities m ON bb.MovieId = m.Id
WHERE b.Date >= '2024-01-01' AND b.Date <= '2024-01-31' AND m.Genre = 5

