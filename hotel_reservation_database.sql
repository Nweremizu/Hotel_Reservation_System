-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1:3306
-- Generation Time: Jun 27, 2023 at 12:59 PM
-- Server version: 8.0.31
-- PHP Version: 8.0.26

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `hotel_reservation_system`
--
CREATE DATABASE IF NOT EXISTS `hotel_reservation_system` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci;
USE `hotel_reservation_system`;

-- --------------------------------------------------------

--
-- Table structure for table `customers`
--

DROP TABLE IF EXISTS `customers`;
CREATE TABLE IF NOT EXISTS `customers` (
  `customerID` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(50) NOT NULL,
  `Address` varchar(100) NOT NULL,
  `State` varchar(50) NOT NULL,
  `Country` varchar(50) NOT NULL,
  `RoomNO` int NOT NULL,
  PRIMARY KEY (`customerID`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `customers`
--

INSERT INTO `customers` (`customerID`, `Name`, `Address`, `State`, `Country`, `RoomNO`) VALUES
(1, 'Nweremizu Bruno', '11, Jasera Street', 'Lagos', 'Nigeria', 202);

-- --------------------------------------------------------

--
-- Table structure for table `reservation`
--

DROP TABLE IF EXISTS `reservation`;
CREATE TABLE IF NOT EXISTS `reservation` (
  `reservationID` int NOT NULL AUTO_INCREMENT,
  `RoomID` int NOT NULL,
  `customerID` int NOT NULL,
  `CheckInDate` date NOT NULL,
  `CheckOutDate` date NOT NULL,
  `TotalPrice` int NOT NULL,
  PRIMARY KEY (`reservationID`),
  KEY `linker` (`customerID`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `reservation`
--

INSERT INTO `reservation` (`reservationID`, `RoomID`, `customerID`, `CheckInDate`, `CheckOutDate`, `TotalPrice`) VALUES
(4, 4, 1, '2023-06-27', '2023-07-04', 1260);

-- --------------------------------------------------------

--
-- Table structure for table `rooms`
--

DROP TABLE IF EXISTS `rooms`;
CREATE TABLE IF NOT EXISTS `rooms` (
  `RoomID` int NOT NULL AUTO_INCREMENT,
  `RoomNO` int NOT NULL,
  `Type` varchar(10) NOT NULL,
  `Capacity` int NOT NULL,
  `Avaliablity` tinyint(1) NOT NULL,
  `Price` varchar(5) NOT NULL,
  PRIMARY KEY (`RoomID`)
) ENGINE=InnoDB AUTO_INCREMENT=61 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `rooms`
--

INSERT INTO `rooms` (`RoomID`, `RoomNO`, `Type`, `Capacity`, `Avaliablity`, `Price`) VALUES
(1, 101, 'Single', 2, 1, '100'),
(2, 102, 'Double', 2, 1, '150'),
(3, 201, 'Single', 2, 1, '120'),
(4, 202, 'Double', 2, 0, '180'),
(5, 301, 'Suite', 4, 1, '250'),
(6, 302, 'Suite', 4, 1, '250'),
(7, 303, 'Suite', 4, 1, '250'),
(8, 304, 'Suite', 4, 1, '250'),
(9, 305, 'Suite', 4, 1, '250'),
(10, 401, 'Family', 6, 1, '350'),
(11, 402, 'Family', 6, 1, '350'),
(12, 403, 'Family', 6, 1, '350'),
(13, 404, 'Family', 6, 1, '350'),
(14, 405, 'Family', 6, 1, '350'),
(15, 501, 'Standard', 2, 1, '200'),
(16, 502, 'Standard', 2, 1, '200'),
(17, 503, 'Standard', 2, 1, '200'),
(18, 504, 'Standard', 2, 1, '200'),
(19, 505, 'Standard', 2, 1, '200'),
(20, 601, 'Deluxe', 2, 1, '300'),
(41, 701, 'Suite', 4, 1, '280'),
(42, 702, 'Suite', 4, 1, '280'),
(43, 703, 'Suite', 4, 1, '280'),
(44, 704, 'Suite', 4, 1, '280'),
(45, 705, 'Suite', 4, 1, '280'),
(46, 801, 'Double', 2, 1, '180'),
(47, 802, 'Double', 2, 1, '180'),
(48, 803, 'Double', 2, 1, '180'),
(49, 804, 'Double', 2, 1, '180'),
(50, 805, 'Double', 2, 1, '180'),
(51, 901, 'Single', 2, 1, '120'),
(52, 902, 'Single', 2, 1, '120'),
(53, 903, 'Single', 2, 1, '120'),
(54, 904, 'Single', 2, 1, '120'),
(55, 905, 'Single', 2, 1, '120'),
(56, 1001, 'Standard', 2, 1, '200'),
(57, 1002, 'Standard', 2, 1, '200'),
(58, 1003, 'Standard', 2, 1, '200'),
(59, 1004, 'Standard', 2, 1, '200'),
(60, 1005, 'Standard', 2, 1, '200');

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

DROP TABLE IF EXISTS `users`;
CREATE TABLE IF NOT EXISTS `users` (
  `UserID` int NOT NULL AUTO_INCREMENT,
  `username` varchar(20) NOT NULL,
  `FullName` text CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Phone` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Password` varchar(16) NOT NULL,
  PRIMARY KEY (`UserID`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `users`
--

INSERT INTO `users` (`UserID`, `username`, `FullName`, `Phone`, `Password`) VALUES
(1, 'brunex', 'Nweremizu Bruno', '+2347032066461', '12345'),
(2, 'Obs', 'Obasan Micheal', '09056576446', '2345'),
(3, 'Dasaka', 'Ohiani Samuel', '09094843157', 'sam123'),
(4, 'dele', 'Bamidele', '1234567890', 'delejoor'),
(5, 'praise', 'Adepoju Praise', '1234567890', 'praise');

--
-- Constraints for dumped tables
--

--
-- Constraints for table `reservation`
--
ALTER TABLE `reservation`
  ADD CONSTRAINT `linker` FOREIGN KEY (`customerID`) REFERENCES `customers` (`customerID`) ON DELETE CASCADE ON UPDATE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
