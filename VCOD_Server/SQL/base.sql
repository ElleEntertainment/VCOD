-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Versione server:              10.1.9-MariaDB - mariadb.org binary distribution
-- S.O. server:                  Win32
-- HeidiSQL Versione:            9.3.0.4984
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

-- Dump della struttura di tabella vcod.nemici_info
DROP TABLE IF EXISTS `nemici_info`;
CREATE TABLE IF NOT EXISTS `nemici_info` (
  `idSpawn` int(11) NOT NULL AUTO_INCREMENT,
  `position_x` float DEFAULT NULL,
  `position_y` float DEFAULT NULL,
  `position_z` float DEFAULT NULL,
  `orientation_x` float DEFAULT NULL,
  `orientation_y` float DEFAULT NULL,
  `orientation_z` float DEFAULT NULL,
  PRIMARY KEY (`idSpawn`)
) ENGINE=InnoDB AUTO_INCREMENT=18 DEFAULT CHARSET=latin1;

-- Dump dei dati della tabella vcod.nemici_info: ~0 rows (circa)
/*!40000 ALTER TABLE `nemici_info` DISABLE KEYS */;
INSERT INTO `nemici_info` (`idSpawn`, `position_x`, `position_y`, `position_z`, `orientation_x`, `orientation_y`, `orientation_z`) VALUES
	(12, 55.6407, 1.15324, 0.328297, 0, -0.902585, 0),
	(13, 36.7139, 1.19223, -0.504187, 0, -0.915312, 0),
	(14, 26.0508, 1.17971, 5.98996, 0, -0.929616, 0),
	(15, 16.8794, 1.18083, 12.9627, 0, -0.953059, 0),
	(16, 58.6404, 1.189, -20.2159, 0, -0.402747, 0),
	(17, 28.4594, 1.17976, -34.4428, 0, -0.550116, 0);
/*!40000 ALTER TABLE `nemici_info` ENABLE KEYS */;


-- Dump della struttura di tabella vcod.player
DROP TABLE IF EXISTS `player`;
CREATE TABLE IF NOT EXISTS `player` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(20) DEFAULT NULL,
  `level` int(11) DEFAULT NULL,
  `exp` int(11) DEFAULT NULL,
  `expToNextLvl` int(11) DEFAULT NULL,
  `health` int(11) DEFAULT NULL,
  `maxhealth` int(11) DEFAULT NULL,
  `position_x` float DEFAULT NULL,
  `position_y` float DEFAULT NULL,
  `position_z` float DEFAULT NULL,
  `orientation_x` float DEFAULT NULL,
  `orientation_y` float DEFAULT NULL,
  `orientation_z` float DEFAULT NULL,
  `savetype` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;

-- Dump dei dati della tabella vcod.player: ~0 rows (circa)
/*!40000 ALTER TABLE `player` DISABLE KEYS */;
INSERT INTO `player` (`id`, `name`, `level`, `exp`, `expToNextLvl`, `health`, `maxhealth`, `position_x`, `position_y`, `position_z`, `orientation_x`, `orientation_y`, `orientation_z`, `savetype`) VALUES
	(1, 'player', 2, 30, 300, 250, 250, 43.8915, 1.0161, -15.186, 0, 0, 0, 0);
/*!40000 ALTER TABLE `player` ENABLE KEYS */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
