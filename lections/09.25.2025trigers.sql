--уменьшение количества ключей, после продажи
CREATE TRIGGER TrAddSale
ON Sale
AFTER INSERT
AS
	UPDATE Game
	SET KeysAmount -= inserted.KeysAmount
	FROM Game
		JOIN inserted ON Game.GameId = inserted.GameId;

--запрет на уменьшение цены
CREATE TRIGGER TrChangePrice
ON Game
AFTER UPDATE
AS
  IF UPDATE(Price)
    IF EXISTS (SELECT * 
	           FROM inserted i
			     JOIN deleted d ON i.GameId=d.GameId
			   WHERE i.Price < d.Price)
		THROW 50000, 'Нельзя уменьшать цену', 1; 
--Вызов ошибки THROW(код ошибки, сообщение ошибки, код локализации)

--использовали до THROW
RAISERROR('test', 10, 1); -- 1-10 ()
RAISERROR('test', 10, 1); -- 11-18 19-25(log)

RAISERROR('%s %d test', 16, 1, 'qwerty', 123);

RAISERROR('%s %d test', 1, 1);

SELECT *
FROM sys.messages
WHERE language_id = 1049;

--отключение тригера
DISABLE TRIGGER TrAddSale On Sale;

--проверка имеющегося кол-ва ключей
CREATE TRIGGER TrAddSaleWithCheck
ON Sale
AFTER INSERT
AS
BEGIN
	-- объявление переменных
	DECLARE @gamesKeys SMALLINT;
	DECLARE @salesKeys SMALLINT;
	-- присваивания значений
	SELECT @gamesKeys = g.KeysAmount, @salesKeys = i.KeysAmount
	FROM Game g JOIN inserted i ON g.GameId = i.GameId;

	IF (@gamesKeys < @salesKeys)
		THROW 50001, 'ключи закончились', 1
	ELSE
		UPDATE Game 
		SET KeysAmount -= inserted.KeysAmount
		FROM Game JOIN inserted ON Game.GameId = inserted.GameId
END;


-- тоже но instead of
CREATE TRIGGER TrAddSaleWithCheck2
ON Sale
INSTEAD OF INSERT
AS
BEGIN
	-- объявление переменных
	DECLARE @gamesKeys SMALLINT;
	DECLARE @salesKeys SMALLINT;
	-- присваивания значений
	SELECT @gamesKeys = g.KeysAmount, @salesKeys = i.KeysAmount
	FROM Game g JOIN inserted i ON g.GameId = i.GameId;

	IF (@gamesKeys < @salesKeys)
		THROW 50001, 'ключи закончились', 1
	ELSE
	BEGIN
		INSERT INTO Sale(GameId, KeysAmount)
		SELECT GameId, KeysAmount
		FROM inserted;

		UPDATE Game 
		SET KeysAmount -= inserted.KeysAmount
		FROM Game JOIN inserted ON Game.GameId = inserted.GameId
	END;
END;