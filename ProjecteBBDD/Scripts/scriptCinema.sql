DROP TABLE IF EXISTS LINEA_TRANSACCION;
DROP TABLE IF EXISTS TRANSACCION;
DROP TABLE IF EXISTS CLIENTE;
DROP TABLE IF EXISTS USUARIO;
DROP TABLE IF EXISTS PELICULA;

CREATE TABLE USUARIO (
    id_usuario INT PRIMARY KEY AUTO_INCREMENT,
    nombre VARCHAR(30) NOT NULL,
    apellido VARCHAR(30) NOT NULL,
    correo VARCHAR(100) UNIQUE NOT NULL,
    contrasena VARCHAR(30) NOT NULL,
    fecha_nacimiento DATE NOT NULL,
    tipo_usuario VARCHAR(30) NOT NULL 
);

CREATE TABLE CLIENTE (
    codi_cliente INT PRIMARY KEY AUTO_INCREMENT,
	id_usuario_c INT NOT NULL,
    telefono VARCHAR(9) NOT NULL,
    tarjeta_vinculada INT,
    comentario_pref VARCHAR (255),
    foto_perfil_cliente VARCHAR(255),
    FOREIGN KEY (id_usuario_c) REFERENCES USUARIO(id_usuario) ON DELETE CASCADE
);

CREATE TABLE PELICULA (
    id_pelicula INT PRIMARY KEY AUTO_INCREMENT,
    portada VARCHAR(255),
    titulo VARCHAR(100) NOT NULL,
    genero VARCHAR(30) NOT NULL,
    duracion INT NOT NULL,
    descripcion VARCHAR(255) NOT NULL,
    precio DECIMAL(10,2) NOT NULL
);

CREATE TABLE TRANSACCION (
    id_transaccion INT PRIMARY KEY AUTO_INCREMENT,
    codi_cliente_t INT NOT NULL,
    tipo_transaccion VARCHAR(30) NOT NULL,
    fecha_transaccion DATE NOT NULL,
    total_t DECIMAL(10,2) NOT NULL,
    FOREIGN KEY (codi_cliente_t) REFERENCES CLIENTE(codi_cliente) ON DELETE CASCADE
);

CREATE TABLE LINEA_TRANSACCION (
    id_transaccion_lt INT NOT NULL,
    id_linea_transaccion INT PRIMARY KEY AUTO_INCREMENT,
    id_pelicula_lt INT NOT NULL,
    precio_pelicula DECIMAL(10,2) NOT NULL,
    cantidad INT NOT NULL,
    total_lt DECIMAL(10,2) NOT NULL,
    FOREIGN KEY (id_transaccion_lt) REFERENCES TRANSACCION(id_transaccion) ON DELETE CASCADE,
    FOREIGN KEY (id_pelicula_lt) REFERENCES PELICULA(id_pelicula) ON DELETE CASCADE
);