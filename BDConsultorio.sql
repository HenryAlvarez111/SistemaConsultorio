GO
CREATE DATABASE [BDConsultorio]
GO
USE [BDConsultorio]
GO
/****** Object:  Table [dbo].[Consultas]    Script Date: 31/01/2025 6:09:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Consultas](
	[IdConsulta] [int] IDENTITY(1,1) NOT NULL,
	[IdPaciente] [int] NOT NULL,
	[IdTratamiento] [int] NULL,
	[FechaConsulta] [datetime] NOT NULL,
	[Observaciones] [nvarchar](500) NULL,
	[IdDoctor] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdConsulta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Doctores]    Script Date: 31/01/2025 6:09:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Doctores](
	[IdDoctor] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](100) NOT NULL,
	[Apellido] [nvarchar](100) NOT NULL,
	[Especialidad] [nvarchar](100) NULL,
	[Telefono] [nvarchar](20) NULL,
	[Email] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdDoctor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pacientes]    Script Date: 31/01/2025 6:09:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pacientes](
	[IdPaciente] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](100) NOT NULL,
	[Apellido] [nvarchar](100) NOT NULL,
	[FechaNacimiento] [date] NOT NULL,
	[Telefono] [nvarchar](15) NULL,
	[Email] [nvarchar](100) NULL,
	[Direccion] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdPaciente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tratamientos]    Script Date: 31/01/2025 6:09:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tratamientos](
	[IdTratamiento] [int] IDENTITY(1,1) NOT NULL,
	[NombreTratamiento] [nvarchar](100) NOT NULL,
	[Descripcion] [nvarchar](255) NULL,
	[DuracionDias] [int] NULL,
	[Costo] [decimal](10, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdTratamiento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Consultas] ADD  DEFAULT (getdate()) FOR [FechaConsulta]
GO
ALTER TABLE [dbo].[Consultas]  WITH CHECK ADD FOREIGN KEY([IdPaciente])
REFERENCES [dbo].[Pacientes] ([IdPaciente])
GO
ALTER TABLE [dbo].[Consultas]  WITH CHECK ADD FOREIGN KEY([IdTratamiento])
REFERENCES [dbo].[Tratamientos] ([IdTratamiento])
GO
ALTER TABLE [dbo].[Consultas]  WITH CHECK ADD  CONSTRAINT [FK_Consultas_Doctores] FOREIGN KEY([IdDoctor])
REFERENCES [dbo].[Doctores] ([IdDoctor])
GO
ALTER TABLE [dbo].[Consultas] CHECK CONSTRAINT [FK_Consultas_Doctores]
GO
/****** Object:  StoredProcedure [dbo].[sp_ActualizarConsulta]    Script Date: 31/01/2025 6:09:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_ActualizarConsulta]
    @IdConsulta INT,
    @IdPaciente INT,
    @IdTratamiento INT,
    @FechaConsulta DATE,
    @Observaciones NVARCHAR(200),
    @IdDoctor INT
AS
BEGIN
    UPDATE Consultas
    SET IdPaciente = @IdPaciente, IdTratamiento = @IdTratamiento, 
        FechaConsulta = @FechaConsulta, Observaciones = @Observaciones, IdDoctor = @IdDoctor
    WHERE IdConsulta = @IdConsulta;
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_ActualizarDoctor]    Script Date: 31/01/2025 6:09:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_ActualizarDoctor]
    @IdDoctor INT,
    @Nombre NVARCHAR(100),
    @Apellido NVARCHAR(100),
    @Especialidad NVARCHAR(100),
    @Telefono NVARCHAR(20),
    @Email NVARCHAR(100)
AS
BEGIN
    UPDATE Doctores
    SET Nombre = @Nombre, Apellido = @Apellido, Especialidad = @Especialidad, 
        Telefono = @Telefono, Email = @Email
    WHERE IdDoctor = @IdDoctor;
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_ActualizarPaciente]    Script Date: 31/01/2025 6:09:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_ActualizarPaciente]
    @IdPaciente INT,
    @Nombre NVARCHAR(100),
    @Apellido NVARCHAR(100),
    @FechaNacimiento DATE,
    @Telefono NVARCHAR(20),
    @Email NVARCHAR(100),
    @Direccion NVARCHAR(200)
AS
BEGIN
    UPDATE Pacientes
    SET Nombre = @Nombre, Apellido = @Apellido, FechaNacimiento = @FechaNacimiento, 
        Telefono = @Telefono, Email = @Email, Direccion = @Direccion
    WHERE IdPaciente = @IdPaciente;
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_ActualizarTratamiento]    Script Date: 31/01/2025 6:09:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_ActualizarTratamiento]
    @IdTratamiento INT,
    @NombreTratamiento NVARCHAR(100),
    @Descripcion NVARCHAR(200),
    @DuracionDias INT,
    @Costo DECIMAL(10, 2)
AS
BEGIN
    UPDATE Tratamientos
    SET NombreTratamiento = @NombreTratamiento, Descripcion = @Descripcion, 
        DuracionDias = @DuracionDias, Costo = @Costo
    WHERE IdTratamiento = @IdTratamiento;
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_EliminarConsulta]    Script Date: 31/01/2025 6:09:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_EliminarConsulta]
    @IdConsulta INT
AS
BEGIN
    DELETE FROM Consultas WHERE IdConsulta = @IdConsulta;
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_EliminarDoctor]    Script Date: 31/01/2025 6:09:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_EliminarDoctor]
    @IdDoctor INT
AS
BEGIN
    DELETE FROM Doctores WHERE IdDoctor = @IdDoctor;
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_EliminarPaciente]    Script Date: 31/01/2025 6:09:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_EliminarPaciente]
    @IdPaciente INT
AS
BEGIN
    DELETE FROM Pacientes WHERE IdPaciente = @IdPaciente;
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_EliminarTratamiento]    Script Date: 31/01/2025 6:09:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_EliminarTratamiento]
    @IdTratamiento INT
AS
BEGIN
    DELETE FROM Tratamientos WHERE IdTratamiento = @IdTratamiento;
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_InsertarConsulta]    Script Date: 31/01/2025 6:09:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_InsertarConsulta]
    @IdPaciente INT,
    @IdTratamiento INT,
    @FechaConsulta DATE,
    @Observaciones NVARCHAR(200),
    @IdDoctor INT
AS
BEGIN
    INSERT INTO Consultas (IdPaciente, IdTratamiento, FechaConsulta, Observaciones, IdDoctor)
    VALUES (@IdPaciente, @IdTratamiento, @FechaConsulta, @Observaciones, @IdDoctor);
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_InsertarDoctor]    Script Date: 31/01/2025 6:09:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_InsertarDoctor]
    @Nombre NVARCHAR(100),
    @Apellido NVARCHAR(100),
    @Especialidad NVARCHAR(100),
    @Telefono NVARCHAR(20),
    @Email NVARCHAR(100)
AS
BEGIN
    INSERT INTO Doctores (Nombre, Apellido, Especialidad, Telefono, Email)
    VALUES (@Nombre, @Apellido, @Especialidad, @Telefono, @Email);
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_InsertarPaciente]    Script Date: 31/01/2025 6:09:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_InsertarPaciente]
    @Nombre NVARCHAR(100),
    @Apellido NVARCHAR(100),
    @FechaNacimiento DATE,
    @Telefono NVARCHAR(20),
    @Email NVARCHAR(100),
    @Direccion NVARCHAR(200)
AS
BEGIN
    INSERT INTO Pacientes (Nombre, Apellido, FechaNacimiento, Telefono, Email, Direccion)
    VALUES (@Nombre, @Apellido, @FechaNacimiento, @Telefono, @Email, @Direccion);
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_InsertarTratamiento]    Script Date: 31/01/2025 6:09:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_InsertarTratamiento]
    @NombreTratamiento NVARCHAR(100),
    @Descripcion NVARCHAR(200),
    @DuracionDias INT,
    @Costo DECIMAL(10, 2)
AS
BEGIN
    INSERT INTO Tratamientos (NombreTratamiento, Descripcion, DuracionDias, Costo)
    VALUES (@NombreTratamiento, @Descripcion, @DuracionDias, @Costo);
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_ObtenerConsultas]    Script Date: 31/01/2025 6:09:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_ObtenerConsultas]
AS
BEGIN
    SELECT * FROM Consultas;
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_ObtenerDoctores]    Script Date: 31/01/2025 6:09:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_ObtenerDoctores]
AS
BEGIN
    SELECT * FROM Doctores;
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_ObtenerPacientes]    Script Date: 31/01/2025 6:09:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_ObtenerPacientes]
AS
BEGIN
    SELECT * FROM Pacientes;
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_ObtenerTratamientos]    Script Date: 31/01/2025 6:09:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_ObtenerTratamientos]
AS
BEGIN
    SELECT * FROM Tratamientos;
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_ReporteCitasPorDoctor]    Script Date: 31/01/2025 6:09:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_ReporteCitasPorDoctor]
    @IdDoctor INT
AS
BEGIN
    SELECT 
        c.IdConsulta,
        c.FechaConsulta,
        CONCAT(p.Nombre, ' ', p.Apellido) AS NombrePaciente,
        t.NombreTratamiento,
        c.Observaciones
    FROM Consultas c
    INNER JOIN Pacientes p 
        ON c.IdPaciente = p.IdPaciente
    INNER JOIN Tratamientos t 
        ON c.IdTratamiento = t.IdTratamiento
    WHERE c.IdDoctor = @IdDoctor
    ORDER BY c.FechaConsulta;
END;
GO
