BEGIN TRANSACTION;
CREATE TABLE "player" (
	`id`	INTEGER PRIMARY KEY AUTOINCREMENT,
	`name`	varchar(20),
	`level`	int,
	`exp`	int,
	`expToNextLvl`	INTEGER,
	`health`	int,
	`maxhealth`	int,
	`position_x`	float,
	`position_y`	float,
	`position_z`	float,
	`orientation_x`	float,
	`orientation_y`	float,
	`orientation_z`	float,
	`savetype`	INTEGER
);
INSERT INTO `player` (id,name,level,exp,expToNextLvl,health,maxhealth,position_x,position_y,position_z,orientation_x,orientation_y,orientation_z,savetype) VALUES (1,'player',2,30,300,250,250,43.8915,1.0161,-15.186,0.0,0.0,0.0,0);
CREATE TABLE "nemici_info" ("idSpawn" INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, "position_x" FLOAT, "position_y" FLOAT, "position_z" FLOAT, "orientation_x" FLOAT, "orientation_y" FLOAT, "orientation_z" FLOAT);
INSERT INTO `nemici_info` (idSpawn,position_x,position_y,position_z,orientation_x,orientation_y,orientation_z) VALUES (12,55.64066,1.153237,0.3282973,0.0,-0.9025854,0.0),
 (13,36.71393,1.192226,-0.5041866,0.0,-0.9153115,0.0),
 (14,26.05076,1.179713,5.989963,0.0,-0.9296158,0.0),
 (15,16.87939,1.180827,12.96266,0.0,-0.9530587,0.0),
 (16,58.64035,1.189002,-20.21585,0.0,-0.4027468,0.0),
 (17,28.45942,1.179765,-34.44283,0.0,-0.5501165,0.0);
COMMIT;
