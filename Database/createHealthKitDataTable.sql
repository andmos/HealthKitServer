CREATE TABLE `HealthKitData` (
      `PersonId` int(11) unsigned NOT NULL AUTO_INCREMENT,
      `RecordId` int(11) DEFAULT NULL,
      `RecordTimeStamp` datetime DEFAULT NULL,
      `BloodType` varchar(11) DEFAULT NULL,
      `DateOfBirth` varchar(11) DEFAULT NULL,
      `Sex` varchar(11) DEFAULT NULL,
      `Height` double DEFAULT NULL,
      PRIMARY KEY (`PersonId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
