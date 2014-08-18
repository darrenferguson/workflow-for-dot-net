IF NOT EXISTS (SELECT *
FROM INFORMATION_SCHEMA.TABLES
WHERE TABLE_NAME = 'workflowconfiguration')

CREATE TABLE dbo.workflowconfiguration (
	id int PRIMARY KEY IDENTITY,
    name nvarchar(255) NOT NULL,
    typename nvarchar(255) NOT NULL,
    isconfigurationactive BIT NOT NULL DEFAULT 0,
    locked BIT NOT NULL DEFAULT 0);

IF NOT EXISTS (SELECT *
FROM INFORMATION_SCHEMA.TABLES
WHERE TABLE_NAME = 'point')

CREATE TABLE dbo.point(
	ownerId nvarchar(255) NOT NULL,
    x int NOT NULL,
    y int NOT NULL);

IF NOT EXISTS (SELECT *
FROM INFORMATION_SCHEMA.TABLES
WHERE TABLE_NAME = 'workflowinstance')

CREATE TABLE dbo.workflowinstance(
	id int PRIMARY KEY IDENTITY,
    name nvarchar(255) NOT NULL,
    typename nvarchar(255) NOT NULL,
    instantiationtime datetime,
    running BIT NOT NULL DEFAULT 0,
    currenttask nvarchar(255));

IF NOT EXISTS (SELECT *
FROM INFORMATION_SCHEMA.TABLES
WHERE TABLE_NAME = 'workflowcriteria')

CREATE TABLE dbo.workflowcriteria(
	id int PRIMARY KEY IDENTITY,
    name nvarchar(255) NOT NULL);