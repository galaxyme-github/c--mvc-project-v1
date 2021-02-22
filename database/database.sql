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
  `Sunday` float NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

/*Data for the table `all_acceess_fee` */

insert  into `all_acceess_fee`(`id`,`Monday`,`Tuesday`,`Wednesday`,`Thursday`,`Friday`,`Saturday`,`Sunday`) values 
(1,200,300,232.342,123.342,353,234,0);

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
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=utf8;

/*Data for the table `guests` */

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
  `Sunday` float NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

/*Data for the table `limit_acceess_fee` */

insert  into `limit_acceess_fee`(`id`,`Monday`,`Tuesday`,`Wednesday`,`Thursday`,`Friday`,`Saturday`,`Sunday`) values 
(1,125,100.7,120,134.78,231,122,0);

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
  `reason` text NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;

/*Data for the table `partyhistory` */

insert  into `partyhistory`(`id`,`partydate`,`party_time`,`event_fee`,`GuestAgreement`,`checkin_name`,`guest_id`,`TermsRejected`,`reason`) values 
(5,'2021-02-16','08:45:00',123.7,'','Dmitriy',14,1,'The reason needn\'t.');

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
(19,'admin','Dmitriy','werqewrqwer','123','aqwertyui@we.vgm','AdminLevel');

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
