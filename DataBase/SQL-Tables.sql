CREATE TABLE Users (
    usersId INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL
);

CREATE TABLE Categories (
    categorieId INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL
);

CREATE TABLE Items (
    itemId INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Quantity INT NOT NULL,
    ExpiryDate DATETIME NOT NULL,
    CategoryId INT NOT NULL,
    UserId INT NOT NULL,
    FOREIGN KEY (CategoryId) REFERENCES Categories(categorieId),
    FOREIGN KEY (UserId) REFERENCES Users(usersId)
);
