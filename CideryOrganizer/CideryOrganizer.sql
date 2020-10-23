-- MySQL dump 10.13  Distrib 8.0.15, for macos10.14 (x86_64)
--
-- Host: localhost    Database: grant_eadie0
-- ------------------------------------------------------
-- Server version	8.0.15

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
 SET NAMES utf8 ;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `__EFMigrationsHistory`
--

DROP TABLE IF EXISTS `__EFMigrationsHistory`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `__EFMigrationsHistory` (
  `MigrationId` varchar(95) NOT NULL,
  `ProductVersion` varchar(32) NOT NULL,
  PRIMARY KEY (`MigrationId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `__EFMigrationsHistory`
--

LOCK TABLES `__EFMigrationsHistory` WRITE;
/*!40000 ALTER TABLE `__EFMigrationsHistory` DISABLE KEYS */;
INSERT INTO `__EFMigrationsHistory` VALUES ('20201023202930_Initial','2.2.4-servicing-10062'),('20201023224641_Second','2.2.4-servicing-10062');
/*!40000 ALTER TABLE `__EFMigrationsHistory` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `AppleCider`
--

DROP TABLE IF EXISTS `AppleCider`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `AppleCider` (
  `AppleCiderId` int(11) NOT NULL AUTO_INCREMENT,
  `AppleId` int(11) NOT NULL,
  `CiderId` int(11) NOT NULL,
  PRIMARY KEY (`AppleCiderId`),
  KEY `IX_AppleCider_AppleId` (`AppleId`),
  KEY `IX_AppleCider_CiderId` (`CiderId`),
  CONSTRAINT `FK_AppleCider_Apples_AppleId` FOREIGN KEY (`AppleId`) REFERENCES `apples` (`AppleId`) ON DELETE CASCADE,
  CONSTRAINT `FK_AppleCider_Ciders_CiderId` FOREIGN KEY (`CiderId`) REFERENCES `ciders` (`CiderId`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `AppleCider`
--

LOCK TABLES `AppleCider` WRITE;
/*!40000 ALTER TABLE `AppleCider` DISABLE KEYS */;
INSERT INTO `AppleCider` VALUES (1,3,1);
/*!40000 ALTER TABLE `AppleCider` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `AppleMaker`
--

DROP TABLE IF EXISTS `AppleMaker`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `AppleMaker` (
  `AppleMakerId` int(11) NOT NULL AUTO_INCREMENT,
  `AppleId` int(11) NOT NULL,
  `MakerId` int(11) NOT NULL,
  PRIMARY KEY (`AppleMakerId`),
  KEY `IX_AppleMaker_AppleId` (`AppleId`),
  KEY `IX_AppleMaker_MakerId` (`MakerId`),
  CONSTRAINT `FK_AppleMaker_Apples_AppleId` FOREIGN KEY (`AppleId`) REFERENCES `apples` (`AppleId`) ON DELETE CASCADE,
  CONSTRAINT `FK_AppleMaker_Makers_MakerId` FOREIGN KEY (`MakerId`) REFERENCES `makers` (`MakerId`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `AppleMaker`
--

LOCK TABLES `AppleMaker` WRITE;
/*!40000 ALTER TABLE `AppleMaker` DISABLE KEYS */;
INSERT INTO `AppleMaker` VALUES (1,3,4);
/*!40000 ALTER TABLE `AppleMaker` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Apples`
--

DROP TABLE IF EXISTS `Apples`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `Apples` (
  `AppleId` int(11) NOT NULL AUTO_INCREMENT,
  `AppleName` longtext,
  `UserId` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`AppleId`),
  KEY `IX_Apples_UserId` (`UserId`),
  CONSTRAINT `FK_Apples_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE RESTRICT
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Apples`
--

LOCK TABLES `Apples` WRITE;
/*!40000 ALTER TABLE `Apples` DISABLE KEYS */;
INSERT INTO `Apples` VALUES (2,'Foxwelp',NULL),(3,'Ribston Pippin','f14f97c8-9c66-4b66-b4b3-e1219c3a9248'),(4,'Foxwelp','f14f97c8-9c66-4b66-b4b3-e1219c3a9248');
/*!40000 ALTER TABLE `Apples` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `AppleStyle`
--

DROP TABLE IF EXISTS `AppleStyle`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `AppleStyle` (
  `AppleStyleId` int(11) NOT NULL AUTO_INCREMENT,
  `AppleId` int(11) NOT NULL,
  `StyleId` int(11) NOT NULL,
  PRIMARY KEY (`AppleStyleId`),
  KEY `IX_AppleStyle_AppleId` (`AppleId`),
  KEY `IX_AppleStyle_StyleId` (`StyleId`),
  CONSTRAINT `FK_AppleStyle_Apples_AppleId` FOREIGN KEY (`AppleId`) REFERENCES `apples` (`AppleId`) ON DELETE CASCADE,
  CONSTRAINT `FK_AppleStyle_Styles_StyleId` FOREIGN KEY (`StyleId`) REFERENCES `styles` (`StyleId`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `AppleStyle`
--

LOCK TABLES `AppleStyle` WRITE;
/*!40000 ALTER TABLE `AppleStyle` DISABLE KEYS */;
/*!40000 ALTER TABLE `AppleStyle` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `AspNetRoleClaims`
--

DROP TABLE IF EXISTS `AspNetRoleClaims`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `AspNetRoleClaims` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `RoleId` varchar(255) NOT NULL,
  `ClaimType` longtext,
  `ClaimValue` longtext,
  PRIMARY KEY (`Id`),
  KEY `IX_AspNetRoleClaims_RoleId` (`RoleId`),
  CONSTRAINT `FK_AspNetRoleClaims_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `aspnetroles` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `AspNetRoleClaims`
--

LOCK TABLES `AspNetRoleClaims` WRITE;
/*!40000 ALTER TABLE `AspNetRoleClaims` DISABLE KEYS */;
/*!40000 ALTER TABLE `AspNetRoleClaims` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `AspNetRoles`
--

DROP TABLE IF EXISTS `AspNetRoles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `AspNetRoles` (
  `Id` varchar(255) NOT NULL,
  `Name` varchar(256) DEFAULT NULL,
  `NormalizedName` varchar(256) DEFAULT NULL,
  `ConcurrencyStamp` longtext,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `RoleNameIndex` (`NormalizedName`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `AspNetRoles`
--

LOCK TABLES `AspNetRoles` WRITE;
/*!40000 ALTER TABLE `AspNetRoles` DISABLE KEYS */;
/*!40000 ALTER TABLE `AspNetRoles` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `AspNetUserClaims`
--

DROP TABLE IF EXISTS `AspNetUserClaims`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `AspNetUserClaims` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `UserId` varchar(255) NOT NULL,
  `ClaimType` longtext,
  `ClaimValue` longtext,
  PRIMARY KEY (`Id`),
  KEY `IX_AspNetUserClaims_UserId` (`UserId`),
  CONSTRAINT `FK_AspNetUserClaims_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `AspNetUserClaims`
--

LOCK TABLES `AspNetUserClaims` WRITE;
/*!40000 ALTER TABLE `AspNetUserClaims` DISABLE KEYS */;
/*!40000 ALTER TABLE `AspNetUserClaims` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `AspNetUserLogins`
--

DROP TABLE IF EXISTS `AspNetUserLogins`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `AspNetUserLogins` (
  `LoginProvider` varchar(255) NOT NULL,
  `ProviderKey` varchar(255) NOT NULL,
  `ProviderDisplayName` longtext,
  `UserId` varchar(255) NOT NULL,
  PRIMARY KEY (`LoginProvider`,`ProviderKey`),
  KEY `IX_AspNetUserLogins_UserId` (`UserId`),
  CONSTRAINT `FK_AspNetUserLogins_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `AspNetUserLogins`
--

LOCK TABLES `AspNetUserLogins` WRITE;
/*!40000 ALTER TABLE `AspNetUserLogins` DISABLE KEYS */;
/*!40000 ALTER TABLE `AspNetUserLogins` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `AspNetUserRoles`
--

DROP TABLE IF EXISTS `AspNetUserRoles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `AspNetUserRoles` (
  `UserId` varchar(255) NOT NULL,
  `RoleId` varchar(255) NOT NULL,
  PRIMARY KEY (`UserId`,`RoleId`),
  KEY `IX_AspNetUserRoles_RoleId` (`RoleId`),
  CONSTRAINT `FK_AspNetUserRoles_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `aspnetroles` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_AspNetUserRoles_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `AspNetUserRoles`
--

LOCK TABLES `AspNetUserRoles` WRITE;
/*!40000 ALTER TABLE `AspNetUserRoles` DISABLE KEYS */;
/*!40000 ALTER TABLE `AspNetUserRoles` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `AspNetUsers`
--

DROP TABLE IF EXISTS `AspNetUsers`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `AspNetUsers` (
  `Id` varchar(255) NOT NULL,
  `UserName` varchar(256) DEFAULT NULL,
  `NormalizedUserName` varchar(256) DEFAULT NULL,
  `Email` varchar(256) DEFAULT NULL,
  `NormalizedEmail` varchar(256) DEFAULT NULL,
  `EmailConfirmed` bit(1) NOT NULL,
  `PasswordHash` longtext,
  `SecurityStamp` longtext,
  `ConcurrencyStamp` longtext,
  `PhoneNumber` longtext,
  `PhoneNumberConfirmed` bit(1) NOT NULL,
  `TwoFactorEnabled` bit(1) NOT NULL,
  `LockoutEnd` datetime(6) DEFAULT NULL,
  `LockoutEnabled` bit(1) NOT NULL,
  `AccessFailedCount` int(11) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `UserNameIndex` (`NormalizedUserName`),
  KEY `EmailIndex` (`NormalizedEmail`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `AspNetUsers`
--

LOCK TABLES `AspNetUsers` WRITE;
/*!40000 ALTER TABLE `AspNetUsers` DISABLE KEYS */;
INSERT INTO `AspNetUsers` VALUES ('f14f97c8-9c66-4b66-b4b3-e1219c3a9248','admin@admin.com','ADMIN@ADMIN.COM',NULL,NULL,_binary '\0','AQAAAAEAACcQAAAAECm7jOBv6HZxxOSePUg+kM4h9t5c71qr8XGi/6ifcbQ4mcZ0Ym/udxD9tuz9WYakvw==','3RVB4Z5DX4WWNJQREVKOPFX6RT3WAEN5','a5e4624a-a00c-494c-9aee-6a6826b9a206',NULL,_binary '\0',_binary '\0',NULL,_binary '',0);
/*!40000 ALTER TABLE `AspNetUsers` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `AspNetUserTokens`
--

DROP TABLE IF EXISTS `AspNetUserTokens`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `AspNetUserTokens` (
  `UserId` varchar(255) NOT NULL,
  `LoginProvider` varchar(255) NOT NULL,
  `Name` varchar(255) NOT NULL,
  `Value` longtext,
  PRIMARY KEY (`UserId`,`LoginProvider`,`Name`),
  CONSTRAINT `FK_AspNetUserTokens_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `AspNetUserTokens`
--

LOCK TABLES `AspNetUserTokens` WRITE;
/*!40000 ALTER TABLE `AspNetUserTokens` DISABLE KEYS */;
/*!40000 ALTER TABLE `AspNetUserTokens` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `CiderMaker`
--

DROP TABLE IF EXISTS `CiderMaker`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `CiderMaker` (
  `CiderMakerId` int(11) NOT NULL AUTO_INCREMENT,
  `CiderId` int(11) NOT NULL,
  `MakerId` int(11) NOT NULL,
  PRIMARY KEY (`CiderMakerId`),
  KEY `IX_CiderMaker_CiderId` (`CiderId`),
  KEY `IX_CiderMaker_MakerId` (`MakerId`),
  CONSTRAINT `FK_CiderMaker_Ciders_CiderId` FOREIGN KEY (`CiderId`) REFERENCES `ciders` (`CiderId`) ON DELETE CASCADE,
  CONSTRAINT `FK_CiderMaker_Makers_MakerId` FOREIGN KEY (`MakerId`) REFERENCES `makers` (`MakerId`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `CiderMaker`
--

LOCK TABLES `CiderMaker` WRITE;
/*!40000 ALTER TABLE `CiderMaker` DISABLE KEYS */;
INSERT INTO `CiderMaker` VALUES (1,4,4),(2,5,4);
/*!40000 ALTER TABLE `CiderMaker` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Ciders`
--

DROP TABLE IF EXISTS `Ciders`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `Ciders` (
  `CiderId` int(11) NOT NULL AUTO_INCREMENT,
  `CiderName` longtext,
  `UserId` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`CiderId`),
  KEY `IX_Ciders_UserId` (`UserId`),
  CONSTRAINT `FK_Ciders_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE RESTRICT
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Ciders`
--

LOCK TABLES `Ciders` WRITE;
/*!40000 ALTER TABLE `Ciders` DISABLE KEYS */;
INSERT INTO `Ciders` VALUES (1,'Elderflower Quince',NULL),(2,'Son of Man','f14f97c8-9c66-4b66-b4b3-e1219c3a9248'),(3,'Willamette Heritage','f14f97c8-9c66-4b66-b4b3-e1219c3a9248'),(4,'2019 Golden Russet','f14f97c8-9c66-4b66-b4b3-e1219c3a9248'),(5,'Lafayette','f14f97c8-9c66-4b66-b4b3-e1219c3a9248'),(6,'Cuvee Blanche Cider','f14f97c8-9c66-4b66-b4b3-e1219c3a9248');
/*!40000 ALTER TABLE `Ciders` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `CiderStyle`
--

DROP TABLE IF EXISTS `CiderStyle`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `CiderStyle` (
  `CiderStyleId` int(11) NOT NULL AUTO_INCREMENT,
  `CiderId` int(11) NOT NULL,
  `StyleId` int(11) NOT NULL,
  PRIMARY KEY (`CiderStyleId`),
  KEY `IX_CiderStyle_CiderId` (`CiderId`),
  KEY `IX_CiderStyle_StyleId` (`StyleId`),
  CONSTRAINT `FK_CiderStyle_Ciders_CiderId` FOREIGN KEY (`CiderId`) REFERENCES `ciders` (`CiderId`) ON DELETE CASCADE,
  CONSTRAINT `FK_CiderStyle_Styles_StyleId` FOREIGN KEY (`StyleId`) REFERENCES `styles` (`StyleId`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `CiderStyle`
--

LOCK TABLES `CiderStyle` WRITE;
/*!40000 ALTER TABLE `CiderStyle` DISABLE KEYS */;
/*!40000 ALTER TABLE `CiderStyle` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Makers`
--

DROP TABLE IF EXISTS `Makers`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `Makers` (
  `MakerId` int(11) NOT NULL AUTO_INCREMENT,
  `MakerName` longtext,
  `UserId` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`MakerId`),
  KEY `IX_Makers_UserId` (`UserId`),
  CONSTRAINT `FK_Makers_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE RESTRICT
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Makers`
--

LOCK TABLES `Makers` WRITE;
/*!40000 ALTER TABLE `Makers` DISABLE KEYS */;
INSERT INTO `Makers` VALUES (1,'Wildcraft Cider Works',NULL),(2,'Wildcraft Cider Works','f14f97c8-9c66-4b66-b4b3-e1219c3a9248'),(3,'Manoir du Kinkiz','f14f97c8-9c66-4b66-b4b3-e1219c3a9248'),(4,'Liberty Ciderworks','f14f97c8-9c66-4b66-b4b3-e1219c3a9248');
/*!40000 ALTER TABLE `Makers` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `MakerStyle`
--

DROP TABLE IF EXISTS `MakerStyle`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `MakerStyle` (
  `MakerStyleId` int(11) NOT NULL AUTO_INCREMENT,
  `MakerId` int(11) NOT NULL,
  `StyleId` int(11) NOT NULL,
  PRIMARY KEY (`MakerStyleId`),
  KEY `IX_MakerStyle_MakerId` (`MakerId`),
  KEY `IX_MakerStyle_StyleId` (`StyleId`),
  CONSTRAINT `FK_MakerStyle_Makers_MakerId` FOREIGN KEY (`MakerId`) REFERENCES `makers` (`MakerId`) ON DELETE CASCADE,
  CONSTRAINT `FK_MakerStyle_Styles_StyleId` FOREIGN KEY (`StyleId`) REFERENCES `styles` (`StyleId`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `MakerStyle`
--

LOCK TABLES `MakerStyle` WRITE;
/*!40000 ALTER TABLE `MakerStyle` DISABLE KEYS */;
INSERT INTO `MakerStyle` VALUES (1,4,4);
/*!40000 ALTER TABLE `MakerStyle` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Styles`
--

DROP TABLE IF EXISTS `Styles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `Styles` (
  `StyleId` int(11) NOT NULL AUTO_INCREMENT,
  `StyleName` longtext,
  `UserId` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`StyleId`),
  KEY `IX_Styles_UserId` (`UserId`),
  CONSTRAINT `FK_Styles_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE RESTRICT
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Styles`
--

LOCK TABLES `Styles` WRITE;
/*!40000 ALTER TABLE `Styles` DISABLE KEYS */;
INSERT INTO `Styles` VALUES (1,'Basque',NULL),(2,'Basque','f14f97c8-9c66-4b66-b4b3-e1219c3a9248'),(3,'French cidre','f14f97c8-9c66-4b66-b4b3-e1219c3a9248'),(4,'Wild Ferment','f14f97c8-9c66-4b66-b4b3-e1219c3a9248'),(5,'Ice Cider','f14f97c8-9c66-4b66-b4b3-e1219c3a9248');
/*!40000 ALTER TABLE `Styles` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2020-10-23 16:55:06
