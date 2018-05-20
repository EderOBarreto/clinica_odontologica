CREATE DATABASE  IF NOT EXISTS `clinica_odontologica` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `clinica_odontologica`;
-- MySQL dump 10.13  Distrib 5.7.12, for Win32 (AMD64)
--
-- Host: localhost    Database: clinica_odontologica
-- ------------------------------------------------------
-- Server version	5.7.20-log

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `agenda`
--

DROP TABLE IF EXISTS `agenda`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `agenda` (
  `agd_id_consulta` int(11) NOT NULL AUTO_INCREMENT,
  `agd_id_paciente` int(11) DEFAULT NULL,
  `agd_id_funcionario` int(11) DEFAULT NULL,
  `agd_data_consulta` datetime DEFAULT NULL,
  `agd_hora_consulta` time DEFAULT NULL,
  `agd_preco_consulta` float DEFAULT NULL,
  `agd_exames` varchar(1000) DEFAULT NULL,
  `agd_data_retorno` datetime DEFAULT NULL,
  `agd_diagnostico` varchar(1000) DEFAULT NULL,
  `agd_data_agendamento` datetime DEFAULT NULL,
  PRIMARY KEY (`agd_id_consulta`),
  KEY `agd_id_paciente` (`agd_id_paciente`),
  KEY `agd_id_funcionario` (`agd_id_funcionario`),
  CONSTRAINT `agenda_ibfk_1` FOREIGN KEY (`agd_id_paciente`) REFERENCES `pacientes` (`pac_id`),
  CONSTRAINT `agenda_ibfk_2` FOREIGN KEY (`agd_id_funcionario`) REFERENCES `funcionarios` (`fun_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `agenda`
--

LOCK TABLES `agenda` WRITE;
/*!40000 ALTER TABLE `agenda` DISABLE KEYS */;
/*!40000 ALTER TABLE `agenda` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `convenios`
--

DROP TABLE IF EXISTS `convenios`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `convenios` (
  `con_id` int(11) NOT NULL AUTO_INCREMENT,
  `con_convenio` varchar(40) DEFAULT NULL,
  `con_cnpj` varchar(20) DEFAULT NULL,
  `con_contato` varchar(20) DEFAULT NULL,
  `con_telefone` varchar(16) DEFAULT NULL,
  PRIMARY KEY (`con_id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `convenios`
--

LOCK TABLES `convenios` WRITE;
/*!40000 ALTER TABLE `convenios` DISABLE KEYS */;
INSERT INTO `convenios` VALUES (1,'Teste','123','123','123');
/*!40000 ALTER TABLE `convenios` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `funcionarios`
--

DROP TABLE IF EXISTS `funcionarios`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `funcionarios` (
  `fun_id` int(11) NOT NULL AUTO_INCREMENT,
  `fun_nome` varchar(15) DEFAULT NULL,
  `fun_cpf` varchar(14) DEFAULT NULL,
  `fun_senha` varchar(10) DEFAULT NULL,
  `fun_usuario` varchar(15) DEFAULT NULL,
  `fun_tipo` varchar(10) DEFAULT NULL,
  `fun_email` varchar(50) DEFAULT NULL,
  `fun_celular` varchar(20) DEFAULT NULL,
  `fun_especialidade` varchar(20) DEFAULT NULL,
  PRIMARY KEY (`fun_id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `funcionarios`
--

LOCK TABLES `funcionarios` WRITE;
/*!40000 ALTER TABLE `funcionarios` DISABLE KEYS */;
INSERT INTO `funcionarios` VALUES (1,'Eder','12345','batata','batman','adm','eder@batman.com','221545564','caardiologista'),(2,'','   ,   ,   -','','','Periodonti','','(  )     -','');
/*!40000 ALTER TABLE `funcionarios` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `pacientes`
--

DROP TABLE IF EXISTS `pacientes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `pacientes` (
  `pac_id` int(11) NOT NULL AUTO_INCREMENT,
  `pac_id_convenio` int(11) DEFAULT NULL,
  `pac_nome` varchar(40) DEFAULT NULL,
  `pac_sexo` varchar(9) DEFAULT NULL,
  `pac_cpf` varchar(10) DEFAULT NULL,
  `pac_data_nascimento` datetime DEFAULT NULL,
  `pac_celular` varchar(20) DEFAULT NULL,
  `pac_email` varchar(40) DEFAULT NULL,
  PRIMARY KEY (`pac_id`),
  KEY `pac_id_convenio` (`pac_id_convenio`),
  CONSTRAINT `pacientes_ibfk_1` FOREIGN KEY (`pac_id_convenio`) REFERENCES `convenios` (`con_id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `pacientes`
--

LOCK TABLES `pacientes` WRITE;
/*!40000 ALTER TABLE `pacientes` DISABLE KEYS */;
/*!40000 ALTER TABLE `pacientes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping events for database 'clinica_odontologica'
--

--
-- Dumping routines for database 'clinica_odontologica'
--
/*!50003 DROP PROCEDURE IF EXISTS `alterarPaciente` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `alterarPaciente`(IN pId INT, IN pConvenio INT, IN pNome VARCHAR(40), IN pSexo VARCHAR(9),
									IN pNascimento DATETIME, IN pCelular VARCHAR(20), pEmail VARCHAR(40))
BEGIN
	UPDATE paciente SET
			pac_id_convenio = pConvenio
			, pac_nome = pnome
			, pac_sexo = pSexo
			, pac_data_nascimento = pNascimento
			, pac_celular = pCelular
			, pac_email = pEmail
		WHERE
			pac_id = pid;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `excluirPaciente` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `excluirPaciente`(IN pid INT)
BEGIN
	DELETE FROM clinica_odontologica.pacientes
		WHERE clinica_odontologica.pacientes.pac_id = pid;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `inserirPaciente` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `inserirPaciente`(IN pConvenio INT, IN pNome VARCHAR(40), IN pSexo VARCHAR(9),
									IN pNascimento DATETIME, IN pCelular VARCHAR(20), pEmail VARCHAR(40))
BEGIN
	INSERT INTO clinica_odontologica.pacientes
		VALUES (DEFAULT, pConvenio, pNome,
				pSexo, pNascimento, pCelular,
                pEmail);
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2018-05-19 18:39:02
