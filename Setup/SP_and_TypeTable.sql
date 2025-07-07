USE [InventorySystemDB]

CREATE TYPE SaleDetailType AS TABLE
(
    ProductId INT,
    Quantity DECIMAL(18, 2)
);


USE [InventorySystemDB]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[CreateSaleTransaction]
(
    @CustomerId INT = NULL,
    @PaidAmount DECIMAL(18,2),
    @SaleDate DATETIME,
    @SaleDetails SaleDetailType READONLY
)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    DECLARE @SaleId INT = 0;
    DECLARE @TotalAmount DECIMAL(18,2) = 0;
    DECLARE @DueAmount INT = 0;
    DECLARE @IsSuccessful BIT = 0;
    DECLARE @Message NVARCHAR(MAX) = '';

    BEGIN TRY
        BEGIN TRANSACTION;
        SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

        -----------------------------------------------
        --Step 0: Check Invalid ProductIds
        -----------------------------------------------
        IF EXISTS (
            SELECT 1
            FROM @SaleDetails sd
            LEFT JOIN Products p ON sd.ProductId = p.ProductId
            WHERE p.ProductId IS NULL
        )
        BEGIN
            SELECT @Message = STRING_AGG(CAST(sd.ProductId AS NVARCHAR), ', ')
            FROM @SaleDetails sd
            LEFT JOIN Products p ON sd.ProductId = p.ProductId
            WHERE p.ProductId IS NULL;

            SET @Message = 'Invalid ProductIds: ' + @Message;

            SELECT 
                CAST(0 AS BIT) AS isSuccessful,
                0 AS SaleId,
                @Message AS Message,
                0 AS DueAmount;
            ROLLBACK TRANSACTION;
            RETURN;
        END

        -----------------------------------------------
        --Step 1: Check Insufficient Stock
        -----------------------------------------------
        IF EXISTS (
            SELECT 1
            FROM @SaleDetails sd
            JOIN Products p WITH (UPDLOCK, ROWLOCK)
              ON sd.ProductId = p.ProductId
            WHERE p.StockQty < sd.Quantity
        )
        BEGIN
            SELECT @Message = STRING_AGG(CAST(sd.ProductId AS NVARCHAR), ', ')
            FROM @SaleDetails sd
            JOIN Products p ON sd.ProductId = p.ProductId
            WHERE p.StockQty < sd.Quantity;

            SET @Message = 'Insufficient stock for ProductIds: ' + @Message;

            SELECT 
                CAST(0 AS BIT) AS isSuccessful,
                0 AS SaleId,
                @Message AS Message,
                0 AS DueAmount;
            ROLLBACK TRANSACTION;
            RETURN;
        END

        -----------------------------------------------
        --Step 2: Calculate totals
        -----------------------------------------------
        SELECT @TotalAmount = SUM(sd.Quantity * p.Price)
        FROM @SaleDetails sd
        JOIN Products p ON p.ProductId = sd.ProductId;

        SET @DueAmount = CAST(@TotalAmount - @PaidAmount AS INT);

        -----------------------------------------------
        --Step 3: Insert Sale
        -----------------------------------------------
        INSERT INTO Sales (SaleDate, CustomerId, TotalAmount, PaidAmount, DueAmount, IsDeleted)
        VALUES (@SaleDate, @CustomerId, @TotalAmount, @PaidAmount, @DueAmount, 0);

        SET @SaleId = SCOPE_IDENTITY();

        -----------------------------------------------
        --Step 4: Insert SaleDetails
        -----------------------------------------------
        INSERT INTO SaleDetails (SaleId, ProductId, Quantity, Price, IsDeleted)
        SELECT @SaleId, sd.ProductId, sd.Quantity, p.Price, 0
        FROM @SaleDetails sd
        JOIN Products p ON p.ProductId = sd.ProductId;

        -----------------------------------------------
        --Step 5: Reduce Stock
        -----------------------------------------------
        UPDATE p
        SET p.StockQty = p.StockQty - sd.Quantity
        FROM Products p
        JOIN @SaleDetails sd ON sd.ProductId = p.ProductId;

        -----------------------------------------------
        --Step 6: Return success
        -----------------------------------------------
        COMMIT TRANSACTION;

        SET @IsSuccessful = 1;
        SET @Message = 'Sale created successfully.';

        SELECT 
            @IsSuccessful AS isSuccessful,
            @SaleId AS SaleId,
            @Message AS Message,
            @DueAmount AS DueAmount;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK;

        SELECT 
            CAST(0 AS BIT) AS isSuccessful,
            0 AS SaleId,
            ERROR_MESSAGE() AS Message,
            0 AS DueAmount;
    END CATCH
END
