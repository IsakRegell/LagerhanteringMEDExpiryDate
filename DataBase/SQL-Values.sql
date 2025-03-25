-- Users
INSERT INTO Users (Name, Email) VALUES
('Alice Andersson', 'alice@example.com'),
('Bob Berg', 'bob@example.com');

-- Categories
INSERT INTO Categories (Name) VALUES
('Food'),
('Medicine'),
('Tools');

-- Items
INSERT INTO Items (Name, Quantity, ExpiryDate, CategoryId, UserId) VALUES
('Milk', 2, '2025-03-30', 1, 1),
('Painkillers', 1, '2025-09-21', 2, 1),
('Hammer', 1, '2030-01-01', 3, 2),
('Yogurt', 4, '2025-03-20', 1, 2); -- expired

