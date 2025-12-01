USE [master]
GO

/* For security reasons the login is created disabled and with a random password. */
/****** Object:  Login [ispp3101]    Script Date: 01.12.2025 10:08:07 ******/
CREATE LOGIN [ispp3101] 
WITH PASSWORD=N'0m+I3l55HkOKKxuWP1Tl4hIUhZgK17M4ws1XUMGnXTo=', --Рандомно сгенерированный пароль
DEFAULT_DATABASE=[ispp3101], 
DEFAULT_LANGUAGE=[русский], 
CHECK_EXPIRATION=OFF, --Нужно ли добавлять обязательность смены пароля
CHECK_POLICY=OFF --Применять ли проверку надёжности пароля
GO

ALTER LOGIN [ispp3101] DISABLE
GO


