CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';

CREATE TABLE IF NOT EXISTS vaults(
  id INT  AUTO_INCREMENT NOT NULL primary key ,
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name TEXT NOT NULL,
  description TEXT not NULL,
  isPrivate boolean,
  creatorId VARCHAR(355),
FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';

CREATE TABLE IF NOT EXISTS keeps(
id INT AUTO_INCREMENT NOT NULL PRIMARY KEY,
name TEXT NOT NULL,
description TEXT NOT NULL,
image TEXT NOT NULL,
view INT NOT NULL,
kept INT NOT NULL,   
creatorId VARCHAR(355),
FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE 
) default charset utf8 COMMENT '';

CREATE Table IF NOT EXISTS vaultkeeps(
id int NOT NULL AUTO_INCREMENT PRIMARY KEY,
keepId INT NOT NULL,
vaultId INT NOT NULL,
FOREIGN KEY (keepId) REFERENCES keeps(id) ON DELETE CASCADE,
FOREIGN KEY (vaultId) REFERENCES vaults(id) ON DELETE CASCADE
)default charset utf8 COMMENT '';