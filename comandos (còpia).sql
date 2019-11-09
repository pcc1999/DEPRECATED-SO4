DROP DATABASE IF EXISTS Juego;

CREATE DATABASE Juego;

USE Juego;

CREATE TABLE Jugador (
Identificador INTEGER,
Username TEXT,
Password TEXT,
Experiencia INTEGER
)ENGINE = InnoDB;

CREATE TABLE Partida (
Identificador INTEGER,
FechaFinal datetime,
Duracion TIME,
Ganador TEXT
)ENGINE = InnoDB;

CREATE TABLE Relacion (
Partida INTEGER,
Jugador INTEGER,
Ganador INTEGER
)ENGINE = InnoDB;

INSERT INTO Jugador VALUES (1,'Axel','pobresonia',0);
INSERT INTO Jugador VALUES (2,'Marc','madrid',0);
INSERT INTO Jugador VALUES (3,'Pol','sotodelreal',0);
INSERT INTO Jugador VALUES (4,'Toni','menores',75);

INSERT INTO Partida VALUES (1,'2019-10-01 16:26:32', '00:30:00', 'Axel');
INSERT INTO Partida VALUES (2,'2019-10-01 16:56:32', '00:23:51', 'Pol');
INSERT INTO Partida VALUES (3,'2019-10-01 17:20:32', '00:15:07', 'Pol');
INSERT INTO Partida VALUES (4,'2019-10-01 17:36:32', '00:14:42', 'Toni');

INSERT INTO Relacion VALUES (1,1,1);
INSERT INTO Relacion VALUES (1,2,0);
INSERT INTO Relacion VALUES (1,3,0);
INSERT INTO Relacion VALUES (2,1,0);
INSERT INTO Relacion VALUES (2,3,1);
INSERT INTO Relacion VALUES (2,4,0);
INSERT INTO Relacion VALUES (3,1,0);
INSERT INTO Relacion VALUES (3,2,1);
INSERT INTO Relacion VALUES (4,1,0);
INSERT INTO Relacion VALUES (4,2,0);
INSERT INTO Relacion VALUES (4,4,1);




