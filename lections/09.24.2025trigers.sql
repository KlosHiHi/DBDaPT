-- Вывод количества изменённых строк
CREATE TRIGGER TrGamesRowsCount
    ON Game
    AFTER DELETE, INSERT, UPDATE
    AS
		PRINT 'Количество изменённых строк:' + CAST(@@ROWCOUNT AS VARCHAR(10))
UPDATE Game
SET Price+=1;

--Сохранение старых цен
CREATE TRIGGER TrSavePrice
    ON Game
    AFTER UPDATE
    AS
		IF UPDATE(Price)
			INSERT INTO GamePrice(GameId, OldPrice)
			SELECT GameId, Price
			FROM deleted;

UPDATE Game
SET Price+=10
WHERE GameId = 5;

--Сохранение старых категорий
ALTER TRIGGER TrSaveCategory
    ON Category
    AFTER DELETE
    AS
		INSERT INTO DeletedCategory(CategoryId, [Name], DeletedDate, [Login])
		SELECT CategoryId, [Name], GETDATE(), ORIGINAL_LOGIN()
		FROM deleted;

INSERT INTO Category(name) values('рогалик'), ('jRPGs'), ('метроидвания');

--вместо удаления игр создаём метку, что данные удалены
CREATE TRIGGER TrDeleteGame
	ON Game
	INSTEAD OF DELETE
	AS
		UPDATE GAME
		SET IsDeleted = 1
		WHERE GameId IN(SELECT GameId
						FROM deleted);