IF DB_ID(N'Trade') IS NULL
BEGIN
    CREATE DATABASE Trade;
END;
GO

USE Trade;
GO

IF OBJECT_ID(N'dbo.Roles', N'U') IS NOT NULL DROP TABLE dbo.Users;
IF OBJECT_ID(N'dbo.Roles', N'U') IS NOT NULL DROP TABLE dbo.Roles;
GO

CREATE TABLE dbo.Roles
(
    Id   INT           NOT NULL CONSTRAINT PK_Roles PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL
);
GO

CREATE TABLE dbo.Users
(
    Id       INT           NOT NULL CONSTRAINT PK_Users PRIMARY KEY,
    FullName NVARCHAR(150) NOT NULL,
    Login    NVARCHAR(100) NOT NULL CONSTRAINT UQ_Users_Login UNIQUE,
    Password NVARCHAR(100) NOT NULL,
    RoleId   INT           NOT NULL,
    CONSTRAINT FK_Users_Roles FOREIGN KEY (RoleId) REFERENCES dbo.Roles (Id)
);
GO

INSERT INTO dbo.Roles (Id, Name)
VALUES (1, N'Администратор'),
       (2, N'Менеджер'),
       (3, N'Авторизированный клиент');
GO

INSERT INTO dbo.Users (Id, FullName, Login, Password, RoleId)
VALUES (1,  N'Никифорова Весения Николаевна',     N'94d5ous@gmail.com',      N'uzWC67', 1),
       (2,  N'Сазонов Руслан Германович',         N'uth4iz@mail.com',        N'2L6KZG', 1),
       (3,  N'Одинцов Серафим Артёмович',         N'5d4zbu@tutanota.com',    N'rwVDh9', 1),
       (4,  N'Ситдикова Елена Анатольевна',       N'ptec8ym@yahoo.com',      N'LdNyos', 2),
       (5,  N'Ворсин Пётр Евгеньевич',            N'1qz4kw@mail.com',        N'gynQMT', 2),
       (6,  N'Старикова Елена Павловна',          N'4np6se@mail.com',        N'AtnDjr', 2),
       (7,  N'Никифорова Анна Семеновна',         N'yzls62@outlook.com',     N'JlFRCZ', 3),
       (8,  N'Стелина Евгения Петровна',          N'1diph5e@tutanota.com',   N'8ntwUp', 3),
       (9,  N'Михайлюк Анна Вячеславовна',        N'tjde7c@yahoo.com',       N'YOyhfR', 3),
       (10, N'Степанов Михаил Артёмович',         N'wpmrc3do@tutanota.com',  N'RSbvHv', 3);
GO

SELECT u.Id, u.FullName, u.Login, u.Password, r.Name AS Role
FROM dbo.Users u
JOIN dbo.Roles r ON r.Id = u.RoleId
ORDER BY u.Id;
GO