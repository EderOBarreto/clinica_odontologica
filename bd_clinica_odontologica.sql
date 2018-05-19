CREATE DATABASE  IF NOT EXISTS `clinica_odontologica` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `clinica_odontologica`;
-- MySQL dump 10.13  Distrib 5.7.17, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: clinica_odontologica
-- ------------------------------------------------------
-- Server version	5.5.5-10.1.29-MariaDB

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
  `pac_sexo` varchar(1) DEFAULT NULL,
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
INSERT INTO `pacientes` VALUES (1,1,'Teste','M','123','1997-12-31 00:00:00','15485','ederoliveira@gmail.com','');
/*!40000 ALTER TABLE `pacientes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping routines for database 'clinica_odontologica'
--
/*!50003 DROP PROCEDURE IF EXISTS `alterar_agenda` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `alterar_agenda`(
IN id_consulta int,
IN id_paciente int,
IN id_funcionario int,
IN data_agendamento date,
IN data_consulta date,
IN hora_consulta time,
IN preco_consulta float,
IN exames varchar(1000),
IN data_retorno date,
IN diagnostico varchar(1000))
BEGIN

	update agenda set  
	agd_id_paciente = id_paciente,
	agd_id_funcionario = id_funcionario,
	agd_data_agendamento = data_agendamento,
	agd_data_consulta = data_consulta,
	agd_hora_consulta = hora_consulta,
	agd_preco_consulta = preco_consulta,
	agd_exames = exames,
	agd_data_retorno = data_retorno,
	agd_diagnostico = diagnostico
    where agd_id_consulta = id_consulta;
    
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `alterar_convenio` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `alterar_convenio`(
IN id int,
IN convenio varchar(40),
IN cnpj varchar(20),
IN contato varchar(20),
IN telefone varchar(16))
BEGIN

	update convenios set 
    con_convenio = convenio,
    con_cnpj = cnpj,
    con_contato = contato,
    con_telefone = telefone
    where con_id = id;

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `alterar_funcionario` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `alterar_funcionario`(
IN id int,
IN nome varchar(15),
IN cpf varchar(14), 
IN senha varchar(10), 
IN usuario varchar(15),
IN tipo varchar(10), 
IN email varchar(50), 
IN celular varchar(20), 
IN especialidade varchar(20))
BEGIN
	update funcionarios set
    fun_nome = nome,
    fun_cpf = cpf, 
    fun_senha = senha,
    fun_usuario = usuario,
    fun_tipo = tipo, 
    fun_email = email, 
    fun_celular = celular,
    fun_especialidade = especialidade
    where fun_id = id;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `alterar_paciente` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `alterar_paciente`(
IN id_paciente int,
IN id_convenio int,
IN nome varchar(40),
IN sexo varchar(1),
IN cpf varchar(10),
IN data_nascimento datetime,
IN celular varchar(20),
IN email varchar(40),
IN historico varchar(0))
BEGIN

	update pacientes 
	set
	pac_id_convenio = id_convenio,
	pac_nome = nome,
	pac_sexo = sexo,
	pac_cpf = cpf,
	pac_data_nascimento = data_nascimento,
	pac_celular = celular,
	pac_email = email,
	pac_historico = historico  
	where pac_id = id_paciente;

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `excluir_agenda` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `excluir_agenda`(IN id_consulta int)
BEGIN
	delete from agenda where agd_id_consulta = id_consulta;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `excluir_convenio` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `excluir_convenio`(IN id_convenio int)
BEGIN

	delete from convenios where con_id = id_convenio;

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `excluir_funcionario` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `excluir_funcionario`(IN id_funcionario int)
BEGIN

	delete from funcionarios where fun_id = id_funcionario;

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `excluir_paciente` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `excluir_paciente`(IN id_paciente int)
BEGIN

	delete from pacientes where pac_id = id_paciente;
    
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `inserir_agenda` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `inserir_agenda`(
IN id_paciente int,
IN id_funcionario int,
IN data_agendamento date,
IN data_consulta date,
IN hora_consulta time,
IN preco_consulta float,
IN exames varchar(1000),
IN data_retorno date,
IN diagnostico varchar(1000)
)
BEGIN

	insert into agenda(agd_id_paciente, agd_id_funcionario, agd_data_consulta,
    agd_hora_consulta, agd_preco_consulta, agd_exames, agd_data_retorno, agd_diagnostico,agd_data_agendamento)
    values
    (id_paciente, id_funcionario, data_consulta, hora_consulta,
    preco_consulta, exames, data_retorno, diagnostico,data_agendamento);

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `inserir_convenio` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `inserir_convenio`(
IN convenio varchar(40),
IN cnpj varchar(20),
IN contato varchar(20),
IN telefone varchar(16))
BEGIN

	insert into convenios 
    (con_convenio, con_cnpj, con_contato, con_telefone) 
    values 
    (convenio, cnpj, contato, telefone);

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `inserir_funcionario` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `inserir_funcionario`(
IN nome varchar(15), 
IN cpf varchar(14), 
IN senha varchar(10), 
IN usuario varchar(15),
IN tipo varchar(10), 
IN email varchar(50), 
IN celular varchar(20), 
IN especialidade varchar(20))
BEGIN

	insert into funcionarios
	(fun_nome,
	fun_cpf, 
	fun_senha,
	fun_usuario,
	fun_tipo, 
	fun_email, 
	fun_celular,
	fun_especialidade) 
	values 
	(nome, cpf, senha, usuario, tipo, email, celular, especialidade);

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `inserir_paciente` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `inserir_paciente`(
IN id_convenio int,
IN nome varchar(40),
IN sexo varchar(1),
IN cpf varchar(10),
IN data_nascimento datetime,
IN celular varchar(20),
IN email varchar(40),
IN historico varchar(0))
BEGIN

	insert into pacientes 
	(pac_id_convenio,
	 pac_nome,
	 pac_sexo,
	 pac_cpf,
	 pac_data_nascimento,
	 pac_celular,
	 pac_email,
	 pac_historico) 
	values
	(id_convenio, nome, sexo, cpf, data_nascimento, celular, email, historico);

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `selecionar_funcionario` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `selecionar_funcionario`(IN filtro varchar(50))
BEGIN
	
    IF filtro like "" then
    
		select fun_id, fun_nome, fun_cpf, fun_usuario, 
        fun_tipo, fun_email, fun_celular,
        fun_especialidade from funcionarios;

	ELSE
		select fun_id, fun_nome, fun_cpf, fun_usuario, fun_tipo, fun_email, fun_celular, fun_especialidade from funcionarios 
		where 
			fun_id like filtro or
			fun_nome like concat('%',filtro,'%') or
			fun_cpf like concat('%',filtro,'%') or
			fun_usuario like concat('%',filtro,'%') or
			fun_tipo like concat('%',filtro,'%') or
			fun_email like concat('%',filtro,'%') or
			fun_celular like concat('%',filtro,'%') or
			fun_especialidade like concat('%',filtro,'%')
		order by fun_nome;
	END IF;
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

-- Dump completed on 2018-05-16 16:13:32
