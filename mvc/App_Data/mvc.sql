/*
SQLyog Community v13.1.1 (64 bit)
MySQL - 10.1.38-MariaDB : Database - db_event_place
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;
CREATE DATABASE /*!32312 IF NOT EXISTS*/`db_event_place` /*!40100 DEFAULT CHARACTER SET utf8 */;

USE `db_event_place`;

/*Table structure for table `all_acceess_fee` */

DROP TABLE IF EXISTS `all_acceess_fee`;

CREATE TABLE `all_acceess_fee` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `Monday` float NOT NULL,
  `Tuesday` float NOT NULL,
  `Wednesday` float NOT NULL,
  `Thursday` float NOT NULL,
  `Friday` float NOT NULL,
  `Saturday` float NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

/*Data for the table `all_acceess_fee` */

insert  into `all_acceess_fee`(`id`,`Monday`,`Tuesday`,`Wednesday`,`Thursday`,`Friday`,`Saturday`) values 
(1,200,300,232.342,123.342,353,234);

/*Table structure for table `early_bird_end` */

DROP TABLE IF EXISTS `early_bird_end`;

CREATE TABLE `early_bird_end` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `end_time` time NOT NULL DEFAULT '00:00:00',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

/*Data for the table `early_bird_end` */

insert  into `early_bird_end`(`id`,`end_time`) values 
(1,'23:55:43');

/*Table structure for table `early_fee` */

DROP TABLE IF EXISTS `early_fee`;

CREATE TABLE `early_fee` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `early_fee` float NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

/*Data for the table `early_fee` */

insert  into `early_fee`(`id`,`early_fee`) values 
(1,2500);

/*Table structure for table `guests` */

DROP TABLE IF EXISTS `guests`;

CREATE TABLE `guests` (
  `guest_id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `firstname` varchar(64) NOT NULL DEFAULT '',
  `lastname` varchar(64) NOT NULL DEFAULT '',
  `birthdate` date NOT NULL DEFAULT '0000-00-00',
  `middlename` varchar(64) NOT NULL DEFAULT '',
  `streetaddress` varchar(64) NOT NULL DEFAULT '',
  `city` varchar(64) NOT NULL DEFAULT '',
  `state` varchar(64) NOT NULL DEFAULT '',
  `driverlicensenumber` varchar(64) NOT NULL DEFAULT '',
  `expirationdate` date NOT NULL DEFAULT '0000-00-00',
  `sex` enum('Male','Female') NOT NULL DEFAULT 'Male',
  `height` float NOT NULL,
  `weight` float NOT NULL,
  `eyes` enum('Blue','Brown','Gray','Green','Hazel','Red') NOT NULL DEFAULT 'Gray',
  `phonenumber` varchar(64) NOT NULL DEFAULT '',
  `emailaddress` varchar(64) NOT NULL DEFAULT '',
  `guestID` varchar(64) NOT NULL DEFAULT '',
  `LoyaltyPoints` int(11) NOT NULL,
  `access_rule` enum('AA','LA') NOT NULL DEFAULT 'LA',
  `gueststanding` enum('GOOD','VIP','BAD') NOT NULL DEFAULT 'GOOD',
  PRIMARY KEY (`guest_id`)
) ENGINE=InnoDB AUTO_INCREMENT=57 DEFAULT CHARSET=utf8;

/*Data for the table `guests` */

insert  into `guests`(`guest_id`,`firstname`,`lastname`,`birthdate`,`middlename`,`streetaddress`,`city`,`state`,`driverlicensenumber`,`expirationdate`,`sex`,`height`,`weight`,`eyes`,`phonenumber`,`emailaddress`,`guestID`,`LoyaltyPoints`,`access_rule`,`gueststanding`) values 
(43,'Dmitriy','Abrosimov','2021-02-25','Andrew','shanghai','chenjinsdfsdfsdf','china','WP1232452','0000-00-00','Male',234,231,'Blue','1928023302312','dmabdeveloper@gmail.com','DA2452',3,'LA','GOOD'),
(45,'Dmitriy','pppp','2021-02-17','etrt','ertert','sariwom','chinaes','1231928','0000-00-00','Male',234,231,'Blue','192802330239','sda@dsad.com','Dp1928',1,'AA','BAD'),
(46,'Dmitriy','qwewe','1996-01-10','etrt','ertert','sariwom','chinaes','1231928','0000-00-00','Male',234,231,'Blue','192802330239','sda@dsad.com','Dq1928',1,'AA','GOOD'),
(48,'yrt','pppp','2021-02-10','Abrosimovqweqwe','ertert','sariwomqweqw','tert','erterte1928','0000-00-00','Female',234,6412,'Blue','192802330239','sda@dsad.com','yp1928',1,'AA','GOOD'),
(50,'mans','werqewrqwer','1978-02-17','Abrosimov','ertert','sariwom','chinaeseqweqw','erterte','0000-00-00','Female',234,21,'Red','','','mwerte',1,'AA','GOOD'),
(51,'rim','Abrosimov','2021-02-03','etrt','shanghai','erter','chinaeseqweqw','erterte1928','2021-02-17','Male',1723210,64,'Blue','123213','misha@gmail.com','rA1928',1,'AA','BAD'),
(52,'rim','mmhrosimov','2021-02-03','etrt','shanghai','erter','chinaeseqweqw','erterte1928','2021-02-17','Male',1723210,64,'Blue','123213','misha@gmail.com','rm1928',1,'AA','BAD'),
(53,'rim','mmhrosimov','1989-02-03','etrt','shanghai','erter','chinaeseqweqw','erterte213123','2021-02-17','Male',1723210,64,'Blue','123213','misha@gmail.com','rm3123',1,'AA','GOOD'),
(54,'xx','a','1989-03-23','Abrosimov','shanghai','sariwom','','1231928','0000-00-00','Male',172,231,'Gray','123213','23213@342.wer','xa1928',1,'AA','GOOD'),
(55,'qqwqweq','a','2021-02-04','Abrosimov','','sariwom','','1231928','0000-00-00','Male',234,231,'Brown','','','qa1928',1,'AA','BAD'),
(56,'qqwqweq','weqweqew','1964-06-16','Abrosimov','','sariwom','','1231928','0000-00-00','Male',234,231,'Brown','','','qw1928',1,'AA','GOOD');

/*Table structure for table `late_fee` */

DROP TABLE IF EXISTS `late_fee`;

CREATE TABLE `late_fee` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `late_fee` float NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

/*Data for the table `late_fee` */

insert  into `late_fee`(`id`,`late_fee`) values 
(1,12099);

/*Table structure for table `limit_acceess_fee` */

DROP TABLE IF EXISTS `limit_acceess_fee`;

CREATE TABLE `limit_acceess_fee` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `Monday` float NOT NULL,
  `Tuesday` float NOT NULL,
  `Wednesday` float NOT NULL,
  `Thursday` float NOT NULL,
  `Friday` float NOT NULL,
  `Saturday` float NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

/*Data for the table `limit_acceess_fee` */

insert  into `limit_acceess_fee`(`id`,`Monday`,`Tuesday`,`Wednesday`,`Thursday`,`Friday`,`Saturday`) values 
(1,125,100.7,120,134.78,231,122);

/*Table structure for table `messages` */

DROP TABLE IF EXISTS `messages`;

CREATE TABLE `messages` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `event` varchar(64) NOT NULL DEFAULT '',
  `message` varchar(255) NOT NULL DEFAULT '',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

/*Data for the table `messages` */

insert  into `messages`(`id`,`event`,`message`) values 
(1,'home','Welcome to Our Venue!');

/*Table structure for table `partyhistory` */

DROP TABLE IF EXISTS `partyhistory`;

CREATE TABLE `partyhistory` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `partydate` date NOT NULL DEFAULT '0000-00-00',
  `party_time` time NOT NULL DEFAULT '00:00:00',
  `event_fee` float NOT NULL,
  `GuestAgreement` varchar(255) NOT NULL DEFAULT '',
  `checkin_name` varchar(64) NOT NULL DEFAULT '',
  `guest_id` int(11) NOT NULL,
  `TermsRejected` int(11) NOT NULL DEFAULT '1',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=24 DEFAULT CHARSET=utf8;

/*Data for the table `partyhistory` */

insert  into `partyhistory`(`id`,`partydate`,`party_time`,`event_fee`,`GuestAgreement`,`checkin_name`,`guest_id`,`TermsRejected`) values 
(14,'2021-02-09','11:34:00',34.5,'Photopath','checkinname',43,1),
(15,'2021-02-09','11:37:00',34.5,'Photopath','checkinname',44,1),
(16,'2021-02-09','11:58:00',34.5,'Photopath','checkinname',46,1),
(17,'2021-02-09','12:00:00',34.5,'Photopath','checkinname',48,1),
(18,'2021-02-09','12:01:00',34.5,'Photopath','checkinname',50,1),
(19,'2021-02-09','14:46:00',34.5,'Photopath','checkinname',43,1),
(20,'2021-02-09','14:46:00',34.5,'Photopath','checkinname',43,1),
(21,'2021-02-09','15:24:00',34.5,'Photopath','checkinname',53,1),
(22,'2021-02-09','15:25:00',34.5,'Photopath','checkinname',54,1),
(23,'2021-02-09','15:26:00',34.5,'Photopath','checkinname',56,1);

/*Table structure for table `users` */

DROP TABLE IF EXISTS `users`;

CREATE TABLE `users` (
  `user_id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `username` varchar(64) NOT NULL DEFAULT '',
  `firstname` varchar(64) NOT NULL DEFAULT '',
  `lastname` varchar(64) NOT NULL DEFAULT '',
  `password` varchar(64) NOT NULL DEFAULT '',
  `useremail` varchar(64) NOT NULL DEFAULT '',
  `userlevel` enum('Level1','Level2','AdminLevel') NOT NULL,
  PRIMARY KEY (`user_id`)
) ENGINE=InnoDB AUTO_INCREMENT=20 DEFAULT CHARSET=utf8;

/*Data for the table `users` */

insert  into `users`(`user_id`,`username`,`firstname`,`lastname`,`password`,`useremail`,`userlevel`) values 
(19,'a','Dmitriy','werqewrqwer','123','aqwertyui@we.vgm','Level2');

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
