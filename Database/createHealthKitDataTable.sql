CREATE TABLE `HealthKitData` (
`PersonId` int(11) unsigned,
`RecordId` int(11) unsigned unique key AUTO_INCREMENT,
`RecordingTimeStamp` datetime DEFAULT NULL,
`BloodType` varchar(11) DEFAULT NULL,
`DateOfBirth` varchar(11) DEFAULT NULL,
`Sex` varchar(11) DEFAULT NULL,
`Height` double DEFAULT NULL,
PRIMARY KEY (`PersonId`, `RecordId`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8; 

CREATE TABLE `DistanceReading` (
`TotalSteps` int(11) unsigned, 
`TotalStepsOfLastRecording` int(11) unsigned,
`TotalDistance` varchar(11) DEFAULT NULL, 
`TotalDistanceOfLastRecording` double DEFAULT NULL, 
`TotalFlightsClimed` varchar(11) DEFAULT NULL, 
`PersonId` int(11) unsigned,
`RecordId` int(11) unsigned,
FOREIGN KEY (`PersonId`) REFERENCES `HealthKitData` (`PersonId`),
FOREIGN KEY (`RecordId`) REFERENCES `HealthKitData` (`RecordId`),
PRIMARY KEY (`PersonId`, `RecordId`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8;  
