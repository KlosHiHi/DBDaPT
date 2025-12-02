CREATE TABLE [dbo].[PW4Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Login] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](20) NOT NULL,
	[EncryptedPassword] [binary](32) NULL,
 CONSTRAINT [PK_PW4Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, 
	STATISTICS_NORECOMPUTE = OFF, 
	IGNORE_DUP_KEY = OFF, 
	ALLOW_ROW_LOCKS = ON, 
	ALLOW_PAGE_LOCKS = ON, 
	OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

INSERT INTO PW4Users ([Login], [Password])
VALUES 
	('user1', '1'),
	('user2', '1'),
	('user3', '1');


UPDATE PW4Users 
SET EncryptedPassword = HASHBYTES('SHA2_256', Password)
WHERE EncryptedPassword IS NULL 
    OR EncryptedPassword != HASHBYTES('SHA2_256', Password)