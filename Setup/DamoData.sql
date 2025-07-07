-- Customers
INSERT INTO [InventorySystemDB].[dbo].[customers]
    ([FullName], [Phone], [Email], [LoyaltyPoints], [IsDeleted])
VALUES
    ('Bidyut Mazumder', '01821054102', 'bid.mder31@gmail.com', 0, 0);

-- Products
INSERT INTO [InventorySystemDB].[dbo].[products]
    ([Name], [Barcode], [Price], [StockQty], [Category], [Status], [IsDeleted])
VALUES
    ('Wireless Mouse', 'WM-123459', 1199.99, 47.00, 'Electronics', 1, 0),
    ('Wireless Mouse', 'WM-123460', 1099.99, 47.00, 'Electronics', 1, 0),
    ('Wireless Mouse', 'WM-123461', 1099.99, 50.00, 'Electronics', 1, 0);

-- Sales
INSERT INTO [InventorySystemDB].[dbo].[sales]
    ([SaleDate], [CustomerId], [TotalAmount], [PaidAmount], [DueAmount], [IsDeleted])
VALUES
    ('2025-07-07 15:21:16.4866667', 1, 3499.97, 2000.00, 1499.00, 0),
    ('2025-07-07 15:21:16.4866667', 1, 1099.99, 2000.00, -900.00, 0),
    ('2025-07-07 15:21:16.4866667', 1, 2299.98, 5000.00, -2700.00, 0);

-- SaleDetails
INSERT INTO [InventorySystemDB].[dbo].[saleDetails]
    ([SaleId], [ProductId], [Quantity], [Price], [IsDeleted])
VALUES
    (1, 1, 2.00, 1199.99, 0),
    (1, 2, 1.00, 1099.99, 0),
    (2, 2, 1.00, 1099.99, 0),
    (3, 1, 1.00, 1199.99, 0),
    (3, 2, 1.00, 1099.99, 0);
