ALTER TABLE [dbo].[Users]
ADD PasswordHash VARBINARY(MAX);

ALTER TABLE [dbo].[Users]
ADD PasswordSalt VARBINARY(MAX);