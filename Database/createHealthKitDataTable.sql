CREATE TABLE `HealthKitData` (
`PersonId` int(11) unsigned NOT NULL,
`RecordingId` int(11) unsigned NOT NULL AUTO_INCREMENT,
`RecordingTimeStamp` datetime DEFAULT NULL,
`BloodType` varchar(11) DEFAULT NULL,
`DateOfBirth` varchar(11) DEFAULT NULL,
`Sex` varchar(11) DEFAULT NULL,
`Height` double DEFAULT NULL,
PRIMARY KEY (`RecordingId`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8;
