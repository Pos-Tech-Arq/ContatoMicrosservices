IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'ContatosDb')
BEGIN
CREATE DATABASE ContatosDb;
END
GO
IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'RegioesDb')
BEGIN
CREATE DATABASE RegioesDb;
END
GO