/*Tabla Producto*/
CREATE DATABASE Manana

USE Manana
/*******************Tabla Producto y SUS SP***********************/
ALTER TABLE Producto(
	IdProducto int primary key identity(1,1),
	nombreProducto varchar(255) NOT NULL,
	Descripcion varchar(500) NOT NULL,
	precioProducto decimal(10,2) NOT NULL,
	Proveedor varchar(50) NOT NULL,
	IdCategoria int NOT NULL,
	Stock int NOT NULL
)

/*SP Mostrar Producto*/
ALTER PROC MostrarProducto
AS 
BEGIN
	SELECT 	MP.IdProducto,
			MP.nombreProducto as Producto,
			MP.Descripcion,
			MP.precioProducto as Precio,
			MP.Proveedor,
			C.IdCategoria,
			C.Nombre,
			MP.Stock
		FROM Producto MP
		INNER JOIN Categoria C ON C.IdCategoria = MP.IdCategoria
		ORDER BY IdProducto ASC
	END
GO
EXEC MostrarProducto
--DROP PROCEDURE MostrarProducto

/*SP Crear Producto*/
ALTER PROCEDURE AgregarProducto
    @nombreProducto varchar(255),
    @Descripcion varchar(500),
    @precioProducto decimal(10,2),
    @Proveedor varchar(50),
    @IdCategoria int,
    @Stock int
AS
BEGIN
    -- Validación de los inputs
    IF @nombreProducto IS NULL OR @nombreProducto = ''
        OR @Descripcion IS NULL OR @Descripcion = ''
        OR @precioProducto <= 0
        OR @Proveedor IS NULL OR @Proveedor = ''
        OR @IdCategoria <= 0
        OR @Stock < 0
    BEGIN
        RAISERROR('Todos los campos deben ser proporcionados y válidos.', 16, 1);
        RETURN;
    END

    -- Insertar el nuevo producto
    INSERT INTO Producto
    (
        nombreProducto,
        Descripcion,
        precioProducto,
        Proveedor,
        IdCategoria,
        Stock
    )
    VALUES
    (
        @nombreProducto,
        @Descripcion,
        @precioProducto,
        @Proveedor,
        @IdCategoria,
        @Stock
    )
END
GO

/********/
EXEC AgregarProducto
    @nombreProducto = 'Te Limon',
    @Descripcion = 'Descripción del producto nuevo',
    @precioProducto = 29.99,
    @Proveedor = 'Proveedor XYZ',
    @IdCategoria = 1,
    @Stock = 100

	select * from Producto
/********/
/*SP Actualizar Producto*/
ALTER PROCEDURE ActualizarProducto
    @IdProducto int,
    @nombreProducto varchar(255),
    @Descripcion varchar(500),
    @precioProducto decimal(10,2),
    @Proveedor varchar(50),
    @IdCategoria int,
    @Stock int
AS
BEGIN
    UPDATE Producto SET
        nombreProducto = @nombreProducto,
        Descripcion = @Descripcion,
        precioProducto = @precioProducto,
        Proveedor = @Proveedor,
        IdCategoria = @IdCategoria,
        Stock = @Stock
    WHERE IdProducto = @IdProducto;
END
GO

EXEC ActualizarProducto
	@IdProducto = 1,
    @nombreProducto = 'Te Limon',
    @Descripcion = 'Descripción del producto nuevo',
    @precioProducto = 65,
    @Proveedor = 'Proveedor XYZ',
    @IdCategoria = 1,
    @Stock = 50


/*SP Mostrar EliminarProducto*/
CREATE PROCEDURE spEliminarProducto
    @IdProducto int
AS
BEGIN
    -- Verificar que el producto exista antes de eliminar
    IF NOT EXISTS (SELECT 1 FROM Producto WHERE IdProducto = @IdProducto)
    BEGIN
        RAISERROR('No se puede eliminar porque el producto no existe.', 16, 1);
        RETURN;
    END

    -- Eliminar el producto
    DELETE FROM Producto WHERE IdProducto = @IdProducto;
END
GO

/*******************Tabla Categoria y SUS SP***********************/
CREATE TABLE Categoria (
    IdCategoria int primary key identity(1,1),
    Nombre nvarchar(100) NOT NULL
)

/*SP Mostrar Categoria*/
CREATE PROC MostrarCategoria
AS 
BEGIN
	SELECT 	IdCategoria,
			Nombre
		FROM Categoria
		ORDER BY IdCategoria DESC
	END
GO
EXEC MostrarCategoria


/*SP Mostrar AgregarCategoria*/
CREATE PROCEDURE AgregarCategoria
    @Nombre nvarchar(100)
AS
BEGIN
    -- Validación de los inputs
    IF @Nombre IS NULL OR @Nombre = ''
    BEGIN
        RAISERROR('El nombre de la categoría es requerido.', 16, 1);
        RETURN;
    END

    -- Insertar la nueva categoría
    INSERT INTO Categoria (Nombre)
    VALUES (@Nombre);
END
GO

EXEC AgregarCategoria @Nombre = 'Electrónicos';

SELECT * FROM Categoria

/*SP Mostrar ActualizarCategoria*/
CREATE PROCEDURE ActualizarCategoria
    @IdCategoria int,
    @Nombre nvarchar(100)
AS
BEGIN
    -- Validación de los inputs
    IF @Nombre IS NULL OR @Nombre = ''
    BEGIN
        RAISERROR('El nombre de la categoría es requerido.', 16, 1);
        RETURN;
    END

    -- Verificar que la categoría exista
    IF NOT EXISTS (SELECT 1 FROM Categoria WHERE IdCategoria = @IdCategoria)
    BEGIN
        RAISERROR('La categoría especificada no existe.', 16, 1);
        RETURN;
    END

    -- Actualizar la categoría
    UPDATE Categoria SET
        Nombre = @Nombre
    WHERE IdCategoria = @IdCategoria;
END
GO

EXEC ActualizarCategoria @IdCategoria = 1, @Nombre = 'Electrodomésticos';

/*SP Mostrar EliminarCategoria*/
CREATE PROCEDURE EliminarCategoria
    @IdCategoria int
AS
BEGIN
    -- Verificar que la categoría no esté en uso por algún producto
    IF EXISTS (SELECT 1 FROM Producto WHERE IdCategoria = @IdCategoria)
    BEGIN
        RAISERROR('No se puede eliminar la categoría porque está en uso por uno o más productos.', 16, 1);
        RETURN;
    END

    -- Verificar que la categoría exista
    IF NOT EXISTS (SELECT 1 FROM Categoria WHERE IdCategoria = @IdCategoria)
    BEGIN
        RAISERROR('La categoría especificada no existe.', 16, 1);
        RETURN;
    END

    -- Eliminar la categoría
    DELETE FROM Categoria WHERE IdCategoria = @IdCategoria;
END
GO

EXEC EliminarCategoria @IdCategoria = 2;

