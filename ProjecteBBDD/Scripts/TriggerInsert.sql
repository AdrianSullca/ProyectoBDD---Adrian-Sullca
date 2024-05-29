CREATE TRIGGER ActualizarTotalTransaccion 
AFTER INSERT ON LINEA_TRANSACCION
FOR EACH ROW
BEGIN
    DECLARE nuevo_total DECIMAL(10, 2);

    SELECT SUM(precio_pelicula * cantidad) INTO nuevo_total
    FROM LINEA_TRANSACCION
    WHERE id_transaccion_lt = NEW.id_transaccion_lt;

    UPDATE TRANSACCION
    SET total_t = nuevo_total
    WHERE id_transaccion = NEW.id_transaccion_lt;
END;