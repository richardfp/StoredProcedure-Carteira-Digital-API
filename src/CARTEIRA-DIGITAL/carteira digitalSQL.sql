use CARTEIRA_DIGITAL;


CREATE TABLE UserAccounts (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(255) NOT NULL,  
    Balance DECIMAL(18, 2) NOT NULL DEFAULT 0.00,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE()
);


CREATE TABLE TransactionLogs (
    Id INT PRIMARY KEY IDENTITY(1,1),
    UserId INT NOT NULL,
    Amount DECIMAL(18, 2) NOT NULL,
    Type NVARCHAR(50) NOT NULL,  
    TransactionDate DATETIME NOT NULL DEFAULT GETDATE(),
    FOREIGN KEY (UserId) REFERENCES UserAccounts(Id) ON DELETE CASCADE
);

DROP PROCEDURE sp_CreateUserAccount

CREATE PROCEDURE sp_CreateUserAccount
    @Name NVARCHAR(100),
    @Email NVARCHAR(100),
    @PasswordHash NVARCHAR(255)
AS
BEGIN
 
    IF EXISTS (SELECT 1 FROM UserAccounts WHERE Email = @Email)
    BEGIN
        RAISERROR('E-mail já registrado.', 16, 1);
        RETURN; 
    END;
    

    BEGIN TRY
        BEGIN TRANSACTION;
        

        INSERT INTO UserAccounts (Name, Email, PasswordHash, Balance, CreatedAt)
        VALUES (@Name, @Email, @PasswordHash, 100, GETDATE());


        COMMIT;
    END TRY
    BEGIN CATCH

        ROLLBACK;
        

        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(@ErrorMessage, 16, 1);
    END CATCH;
END;
GO

DROP PROCEDURE sp_GetAccountBalance
GO

CREATE PROCEDURE sp_GetAccountBalance
    @UserId INT,                   
    @PasswordHash NVARCHAR(255),   
    @Balance DECIMAL(18,2) OUTPUT  
AS
BEGIN

    BEGIN TRY

        IF NOT EXISTS (SELECT 1 FROM UserAccounts WHERE Id = @UserId)
        BEGIN
            RAISERROR('Conta não encontrada.', 16, 1);
            RETURN; 
        END;


        IF NOT EXISTS (SELECT 1 FROM UserAccounts WHERE Id = @UserId AND PasswordHash = @PasswordHash)
        BEGIN
            RAISERROR('Senha incorreta.', 16, 1);
            RETURN; 
        END;

        SELECT @Balance = Balance
        FROM UserAccounts
        WHERE Id = @UserId;

		INSERT INTO TransactionLogs (UserId, Amount, Type, TransactionDate)
        VALUES (@UserId, @Balance, 'Verificação de Saldo', GETDATE());

    END TRY
    BEGIN CATCH

        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(@ErrorMessage, 16, 1);
    END CATCH
END;


CREATE PROCEDURE sp_Withdraw
    @UserId INT,                   
    @PasswordHash NVARCHAR(255),   
    @Amount DECIMAL(18,2)          
AS
BEGIN

    DECLARE @CurrentBalance DECIMAL(18,2);


    BEGIN TRY

        IF NOT EXISTS (SELECT 1 FROM UserAccounts WHERE Id = @UserId)
        BEGIN
            RAISERROR('Conta não encontrada.', 16, 1);
            RETURN; 
        END;


        IF NOT EXISTS (SELECT 1 FROM UserAccounts WHERE Id = @UserId AND PasswordHash = @PasswordHash)
        BEGIN
            RAISERROR('Senha incorreta.', 16, 1);
            RETURN; 
        END;


        SELECT @CurrentBalance = Balance FROM UserAccounts WHERE Id = @UserId;


        IF @CurrentBalance < @Amount
        BEGIN
            RAISERROR('Saldo insuficiente.', 16, 1);
            RETURN; 
        END;


        BEGIN TRANSACTION;


        UPDATE UserAccounts
        SET Balance = Balance - @Amount
        WHERE Id = @UserId;


        INSERT INTO TransactionLogs (UserId, Amount, Type, TransactionDate)
        VALUES (@UserId, @Amount, 'Saque', GETDATE());

        COMMIT;
    END TRY
    BEGIN CATCH

        ROLLBACK;


        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(@ErrorMessage, 16, 1);
    END CATCH
END;

CREATE PROCEDURE sp_GetTransactionLogsByUserId
    @UserId INT,                     
    @StatusMessage NVARCHAR(255) OUTPUT 
AS
BEGIN
    BEGIN TRY
  
        IF NOT EXISTS (SELECT 1 FROM UserAccounts WHERE Id = @UserId)
        BEGIN
            SET @StatusMessage = 'Usuário não encontrado.'; -- Define a mensagem de status
            RETURN; 
        END;


        SELECT 
            Id AS TransactionId,
            Amount,
            Type,
            TransactionDate
        FROM TransactionLogs
        WHERE UserId = @UserId
        ORDER BY TransactionDate DESC; 

        SET @StatusMessage = 'Logs recuperados com sucesso.'; 
    END TRY
    BEGIN CATCH

        SET @StatusMessage = ERROR_MESSAGE();
    END CATCH
END;


SELECT * FROM UserAccounts