DROP PROCEDURE IF EXISTS GetClienteConMasTransaccionesXml;

CREATE PROCEDURE GetClienteConMasTransaccionesXml()
BEGIN
    DECLARE xml_result TEXT DEFAULT '';

    SELECT 
        C.codi_cliente, 
        U.nombre, 
        U.apellido, 
        COUNT(T.id_transaccion) AS num_transacciones
    INTO
        @codi_cliente,
        @nombre_usuario,
        @apellido_usuario,
        @num_transacciones
    FROM 
        TRANSACCION T
    JOIN 
        CLIENTE C ON T.codi_cliente_t = C.codi_cliente
    JOIN 
        USUARIO U ON C.id_usuario_c = U.id_usuario
    GROUP BY 
        C.codi_cliente, U.nombre, U.apellido
    ORDER BY 
        num_transacciones DESC
    LIMIT 1;

    SET xml_result = CONCAT('<cliente codi_cliente="', @codi_cliente, '">',
                            '<nombre>', @nombre_usuario, '</nombre>',
                            '<apellido>', @apellido_usuario, '</apellido>',
                            '<num_transacciones>', @num_transacciones, '</num_transacciones>',
                            '</cliente>');

    SELECT xml_result AS XML_Result;
END;