
CREATE PROCEDURE [dbo].[GetAllCustomersNextPredictedOrderDate]
	@CompanyName NVARCHAR(255)
AS
BEGIN
	WITH OrderIntervals AS (
        SELECT 
            c.custid, 
            o.orderdate,
            DATEDIFF(day, LAG(o.orderdate) OVER (PARTITION BY c.custid ORDER BY o.orderdate), o.orderdate) AS OrderInterval
        FROM 
            Sales.Customers c
        JOIN 
            Sales.Orders o ON c.custid = o.custid
    ),
    AvgInterval AS (
        SELECT 
            custid,
            AVG(OrderInterval) AS AvgOrderInterval
        FROM 
            OrderIntervals
        WHERE 
            OrderInterval IS NOT NULL
        GROUP BY 
            custid
    )
    SELECT 
        c.custid AS custid, 
        c.companyname AS companyname, 
        o.orderdate AS lastorderdate,
        DATEADD(day, ai.AvgOrderInterval, o.orderdate) AS nextpredictedorderdate
    FROM 
        Sales.Customers c
    JOIN 
        Sales.Orders o ON c.custid = o.custid
    JOIN 
        AvgInterval ai ON o.custid = ai.custid
    WHERE 
        o.orderdate = (
            SELECT MAX(orderdate) 
            FROM Sales.Orders 
            WHERE custid = o.custid
        )
		 AND c.companyname LIKE '%' + @CompanyName + '%'
    ORDER BY 
        c.custid;

END;
GO


CREATE PROCEDURE [dbo].[InsertOrder]
    @CustId INT,
    @EmpId INT,
    @ShipperId INT,
    @ShipName NVARCHAR(255),
    @ShipAddress NVARCHAR(255),
    @ShipCity NVARCHAR(255),
    @ShipCountry NVARCHAR(255),
    @OrderDate DATETIME,
    @RequiredDate DATETIME,
    @ShippedDate DATETIME,
    @Freight DECIMAL(18, 2),
    @ProductId INT,
    @UnitPrice DECIMAL(18, 2),
    @Quantity INT,
    @Discount DECIMAL(18, 2)
AS
BEGIN
    DECLARE @OrderId INT;

    INSERT INTO Sales.Orders (
        CustId, EmpId, ShipperId, ShipName, ShipAddress, ShipCity, ShipCountry,
        OrderDate, RequiredDate, ShippedDate, Freight
    )
    VALUES (
        @CustId, @EmpId, @ShipperId, @ShipName, @ShipAddress, @ShipCity, @ShipCountry,
        @OrderDate, @RequiredDate, @ShippedDate, @Freight
    );

    SET @OrderId = SCOPE_IDENTITY();

    INSERT INTO Sales.OrderDetails (
        OrderId, ProductId, UnitPrice, qty, Discount
    )
    VALUES (
        @OrderId, @ProductId, @UnitPrice, @Quantity, @Discount
    );

END;
GO


