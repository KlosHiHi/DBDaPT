--���������� ���������� ������, ����� �������
CREATE TRIGGER TrAddSale
ON Sale
AFTER INSERT
AS
	UPDATE Game
	SET KeysAmount -= inserted.KeysAmount
	FROM Game
		JOIN inserted ON Game.GameId = inserted.GameId;

--������ �� ���������� ����
CREATE TRIGGER TrChangePrice
ON Game
AFTER UPDATE
AS
  IF UPDATE(Price)
    IF EXISTS (SELECT * 
	           FROM inserted i
			     JOIN deleted d ON i.GameId=d.GameId
			   WHERE i.Price < d.Price)
		THROW 50000, '������ ��������� ����', 1; 
--����� ������ THROW(��� ������, ��������� ������, ��� �����������)

--������������ �� THROW
RAISERROR('test', 10, 1); -- 1-10 ()
RAISERROR('test', 10, 1); -- 11-18 19-25(log)

RAISERROR('%s %d test', 16, 1, 'qwerty', 123);

RAISERROR('%s %d test', 1, 1);

SELECT *
FROM sys.messages
WHERE language_id = 1049;

--���������� �������
DISABLE TRIGGER TrAddSale On Sale;

--�������� ���������� ���-�� ������
CREATE TRIGGER TrAddSaleWithCheck
ON Sale
AFTER INSERT
AS
BEGIN
	-- ���������� ����������
	DECLARE @gamesKeys SMALLINT;
	DECLARE @salesKeys SMALLINT;
	-- ������������ ��������
	SELECT @gamesKeys = g.KeysAmount, @salesKeys = i.KeysAmount
	FROM Game g JOIN inserted i ON g.GameId = i.GameId;

	IF (@gamesKeys < @salesKeys)
		THROW 50001, '����� �����������', 1
	ELSE
		UPDATE Game 
		SET KeysAmount -= inserted.KeysAmount
		FROM Game JOIN inserted ON Game.GameId = inserted.GameId
END;


-- ���� �� instead of
CREATE TRIGGER TrAddSaleWithCheck2
ON Sale
INSTEAD OF INSERT
AS
BEGIN
	-- ���������� ����������
	DECLARE @gamesKeys SMALLINT;
	DECLARE @salesKeys SMALLINT;
	-- ������������ ��������
	SELECT @gamesKeys = g.KeysAmount, @salesKeys = i.KeysAmount
	FROM Game g JOIN inserted i ON g.GameId = i.GameId;

	IF (@gamesKeys < @salesKeys)
		THROW 50001, '����� �����������', 1
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