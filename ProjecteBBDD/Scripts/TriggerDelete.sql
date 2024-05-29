CREATE TRIGGER ActualizarTotalTransaccionDelete 
AFTER DELETE ON LINEA_TRANSACCION
FOR EACH ROW
BEGIN
    DECLARE nuevo_total DECIMAL(10, 2);

    SELECT SUM(precio_pelicula * cantidad) INTO nuevo_total
    FROM LINEA_TRANSACCION
    WHERE id_transaccion_lt = OLD.id_transaccion_lt;

    IF nuevo_total IS NULL THEN
        SET nuevo_total = 0;
    END IF;

    UPDATE TRANSACCION
    SET total_t = nuevo_total
    WHERE id_transaccion = OLD.id_transaccion_lt;
END;