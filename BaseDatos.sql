Create Database DbPichincha;

Use DbPichincha;

CREATE TABLE Persona(
	PersonaId int NOT NULL AUTO_INCREMENT,
    Nombre nvarchar(100),
    Genero nvarchar(100),
    Edad int,
    Identificacion int,
    Direccion nvarchar(100),
    Telefono nvarchar(100),
    PRIMARY KEY (PersonaId)
);

CREATE TABLE Cliente(
	ClienteId int NOT NULL AUTO_INCREMENT,
    Contrasenia nvarchar(100),
    Estado bool,
    PersonaId int not null,
    PRIMARY KEY (ClienteId),
    FOREIGN KEY (PersonaId) REFERENCES Persona(PersonaId)
);

CREATE TABLE Cuenta(
	CuentaId int NOT NULL AUTO_INCREMENT,
    NumeroCuenta int,
    TipoCuenta nvarchar(100),
    SaldoInicial int,
    Estado bool,
    ClienteId int not null,
    PRIMARY KEY (CuentaId),
    FOREIGN KEY (ClienteId) REFERENCES Cliente(ClienteId)
);

CREATE TABLE Movimiento(
	MovimientoId int NOT NULL AUTO_INCREMENT,
    Fecha datetime,
    TipoMovimiento nvarchar(100),
    Valor int,
    Saldo int,
    CuentaId int not null,
    PRIMARY KEY (MovimientoId),
    FOREIGN KEY (CuentaId) REFERENCES Cuenta(CuentaId)
);

INSERT INTO Persona(Nombre,Genero,Edad,Identificacion,Direccion,Telefono) 
VALUES ("Jose Lema","Masculino",30,"12345678","Otavalo sn y principal","098254785");
INSERT INTO Persona(Nombre,Genero,Edad,Identificacion,Direccion,Telefono) 
VALUES ("Marianela Montalvo","Femenino",22,"21345678","Amazonas y NNUU","097548965");
INSERT INTO Persona(Nombre,Genero,Edad,Identificacion,Direccion,Telefono) 
VALUES ("Juan Osorio","Masculino",30,"43218765","13 junio y Equinoccial","098874587");

INSERT INTO Cliente(Contrasenia,Estado,PersonaId) 
VALUES ("1234",1,(select PersonaId from Persona where  Nombre = 'Jose Lema'));
INSERT INTO Cliente(Contrasenia,Estado,PersonaId) 
VALUES ("5678",1,(select PersonaId from Persona where  Nombre = 'Marianela Montalvo'));
INSERT INTO Cliente(Contrasenia,Estado,PersonaId) 
VALUES ("1245",1,(select PersonaId from Persona where  Nombre = 'Juan Osorio'));

INSERT INTO Cuenta(NumeroCuenta,TipoCuenta,SaldoInicial,Estado,ClienteId) 
VALUES (478758,"Ahorro",2000,1,(select c.ClienteId from Persona p 
inner join cliente c on p.PersonaId = c.PersonaId where p.Nombre = 'Jose Lema'));
INSERT INTO Cuenta(NumeroCuenta,TipoCuenta,SaldoInicial,Estado,ClienteId) 
VALUES (225487,"Corriente",100,1,(select c.ClienteId from Persona p 
inner join cliente c on p.PersonaId = c.PersonaId where p.Nombre = 'Marianela Montalvo'));
INSERT INTO Cuenta(NumeroCuenta,TipoCuenta,SaldoInicial,Estado,ClienteId) 
VALUES (495878,"Ahorro",0,1,(select c.ClienteId from Persona p 
inner join cliente c on p.PersonaId = c.PersonaId where p.Nombre = 'Juan Osorio'));
INSERT INTO Cuenta(NumeroCuenta,TipoCuenta,SaldoInicial,Estado,ClienteId) 
VALUES (496825,"Ahorro",540,1,(select c.ClienteId from Persona p 
inner join cliente c on p.PersonaId = c.PersonaId where p.Nombre = 'Marianela Montalvo'));
