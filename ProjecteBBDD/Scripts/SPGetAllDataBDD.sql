DROP PROCEDURE IF EXISTS GetAllDataBDD;

CREATE PROCEDURE GetAllDataBDD()
BEGIN
    DECLARE xml_result TEXT;

    SET SESSION group_concat_max_len = 1000000;

    SET xml_result = CONCAT(
        '<?xml version="1.0" encoding="utf-8"?>',
        '<cinema>',
            '<usuarios>',
                (
                    SELECT GROUP_CONCAT(
                        CONCAT(
                            '<usuario>',
                                '<id_usuario>', id_usuario, '</id_usuario>',
                                '<nombre>', nombre, '</nombre>',
                                '<apellido>', apellido, '</apellido>',
                                '<correo>', correo, '</correo>',
                                '<contrasena>', contrasena, '</contrasena>',
                                '<fecha_nacimiento>', fecha_nacimiento, '</fecha_nacimiento>',
                                '<tipo_usuario>', tipo_usuario, '</tipo_usuario>',
                            '</usuario>'
                        ) SEPARATOR ''
                    )
                    FROM USUARIO
                ),
            '</usuarios>',
            '<clientes>',
                (
                    SELECT GROUP_CONCAT(
                        CONCAT(
                            '<cliente>',
                                '<codi_cliente>', codi_cliente, '</codi_cliente>',
                                '<id_usuario_c>', id_usuario_c, '</id_usuario_c>',
                                '<telefono>', telefono, '</telefono>',
                                '<tarjeta_vinculada>', tarjeta_vinculada, '</tarjeta_vinculada>',
                                '<comentario_pref>', comentario_pref, '</comentario_pref>',
                                '<foto_perfil_cliente>', foto_perfil_cliente, '</foto_perfil_cliente>',
                                '<transacciones>',
                                    (
                                        SELECT GROUP_CONCAT(
                                            CONCAT(
                                                '<transaccion>',
                                                    '<id_transaccion>', id_transaccion, '</id_transaccion>',
                                                    '<codi_cliente_t>', codi_cliente_t, '</codi_cliente_t>',
                                                    '<tipo_transaccion>', tipo_transaccion, '</tipo_transaccion>',
                                                    '<fecha_transaccion>', fecha_transaccion, '</fecha_transaccion>',
                                                    '<total_t>', total_t, '</total_t>',
                                                    '<lineas_transaccion>',
                                                        (
                                                            SELECT GROUP_CONCAT(
                                                                CONCAT(
                                                                    '<linea_transaccion>',
                                                                        '<id_linea_transaccion>', id_linea_transaccion, '</id_linea_transaccion>',
                                                                        '<id_transaccion_lt>', id_transaccion_lt, '</id_transaccion_lt>',
                                                                        '<id_pelicula_lt>', id_pelicula_lt, '</id_pelicula_lt>',
                                                                        '<precio_pelicula>', precio_pelicula, '</precio_pelicula>',
                                                                        '<cantidad>', cantidad, '</cantidad>',
                                                                        '<total_lt>', total_lt, '</total_lt>',
                                                                    '</linea_transaccion>'
                                                                ) SEPARATOR ''
                                                            )
                                                            FROM LINEA_TRANSACCION
                                                            WHERE id_transaccion_lt = t.id_transaccion
                                                        ),
                                                    '</lineas_transaccion>',
                                                '</transaccion>'
                                            ) SEPARATOR ''
                                        )
                                        FROM TRANSACCION t
                                        WHERE t.codi_cliente_t = c.codi_cliente
                                    ),
                                '</transacciones>',
                            '</cliente>'
                        ) SEPARATOR ''
                    )
                    FROM CLIENTE c
                ),
            '</clientes>',
            '<peliculas>',
                (
                    SELECT GROUP_CONCAT(
                        CONCAT(
                            '<pelicula>',
                                '<id_pelicula>', id_pelicula, '</id_pelicula>',
                                '<portada>', portada, '</portada>',
                                '<titulo>', titulo, '</titulo>',
                                '<genero>', genero, '</genero>',
                                '<duracion>', duracion, '</duracion>',
                                '<descripcion>', descripcion, '</descripcion>',
                                '<precio>', precio, '</precio>',
                            '</pelicula>'
                        ) SEPARATOR ''
                    )
                    FROM PELICULA
                ),
            '</peliculas>',
        '</cinema>'
    );

    SELECT xml_result AS XML_Result;
END;