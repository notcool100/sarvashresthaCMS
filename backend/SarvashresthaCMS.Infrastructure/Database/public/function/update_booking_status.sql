CREATE OR REPLACE FUNCTION update_booking_status(
    p_id INTEGER,
    p_status VARCHAR
) RETURNS INTEGER AS $$
DECLARE
    affected_rows INTEGER;
BEGIN
    UPDATE bookings
    SET status = p_status
    WHERE id = p_id;
    
    GET DIAGNOSTICS affected_rows = ROW_COUNT;
    RETURN affected_rows;
END;
$$ LANGUAGE plpgsql;
