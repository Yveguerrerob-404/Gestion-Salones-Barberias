DROP DATABASE SalonDB;

CREATE DATABASE SalonDB;

USE SalonDB;


CREATE TABLE Clientes (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Telefono VARCHAR(20)
);


CREATE TABLE Empleados (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Especialidad INT NOT NULL
);


CREATE TABLE Servicios (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Precio DECIMAL(10,2) NOT NULL,
    PorcentajeComision DECIMAL(5,2) NOT NULL
);


CREATE TABLE Citas (
    Id INT AUTO_INCREMENT PRIMARY KEY,

    ClienteId INT NOT NULL,
    EmpleadoId INT NOT NULL,

    Fecha DATETIME DEFAULT CURRENT_TIMESTAMP,

    Estado VARCHAR(50)
    DEFAULT 'Pendiente',

    FOREIGN KEY (ClienteId)
        REFERENCES Clientes(Id),

    FOREIGN KEY (EmpleadoId)
        REFERENCES Empleados(Id)
);


CREATE TABLE CitaServicios (
    Id INT AUTO_INCREMENT PRIMARY KEY,

    CitaId INT NOT NULL,
    ServicioId INT NOT NULL,

    FOREIGN KEY (CitaId)
        REFERENCES Citas(Id),

    FOREIGN KEY (ServicioId)
        REFERENCES Servicios(Id)
);


INSERT INTO Servicios
(Nombre, Precio, PorcentajeComision)
VALUES
('Corte', 500, 0.40),
('Barba', 300, 0.30),
('Corte + Barba', 700, 0.45),
('Perfilado de cejas', 200, 0.25),
('Lavado y secado', 600, 0.35),
('Tratamiento capilar', 800, 0.45),
('Tinte', 1000, 0.50),
('Peinado', 400, 0.30);
SELECT * FROM Citas;
