CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';
CREATE TABLE IF NOT EXISTS profile(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';
CREATE TABLE IF NOT EXISTS keep(
  id INT NOT NULL AUTO_INCREMENT primary key COMMENT 'primary key',
  name varchar(255) COMMENT 'Name',
  description varchar(255) COMMENT 'Description',
  img varchar(255) COMMENT 'Picture',
  views INT default 0 COMMENT 'No of Views',
  shares INT default 0 COMMENT 'No of Views',
  keeps INT default 0 COMMENT 'No of Views',
  creatorId VARCHAR(255) NOT NULL comment 'Creator Id',
  FOREIGN KEY (creatorId) REFERENCES accounts(id)
) default charset utf8 COMMENT '';
CREATE TABLE IF NOT EXISTS vault(
  id INT NOT NULL AUTO_INCREMENT primary key COMMENT 'primary key',
  name varchar(255) COMMENT 'Name',
  description varchar(255) COMMENT 'Description',
  img varchar(255) COMMENT 'Picture',
  isPrivate TINYINT COMMENT 'Is Private',
  creatorId VARCHAR(255) NOT NULL comment 'Creator Id',
  FOREIGN KEY (creatorId) REFERENCES accounts(id)
) default charset utf8 COMMENT '';
CREATE TABLE IF NOT EXISTS vaultkeep(
  id INT NOT NULL AUTO_INCREMENT primary key COMMENT 'primary key',
  creatorId VARCHAR(255) NOT NULL comment 'Creator Id',
  vaultId INT NOT NULL comment 'vault Id',
  keepId INT NOT NULL comment 'keepId',
  FOREIGN KEY (creatorId) REFERENCES accounts(id),
  FOREIGN KEY (vaultId) REFERENCES vault(id),
  FOREIGN KEY (keepId) REFERENCES keep(id)
) default charset utf8 COMMENT '';