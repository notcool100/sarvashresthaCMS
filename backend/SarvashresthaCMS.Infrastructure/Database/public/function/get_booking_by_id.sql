CREATE OR REPLACE FUNCTION get_booking_by_id(p_id INTEGER)
RETURNS TABLE (
    id INTEGER,
    resort_id INTEGER,
    guest_name VARCHAR,
    guest_email VARCHAR,
    check_in TIMESTAMPTZ,
    check_out TIMESTAMPTZ,
    total_price DECIMAL,
    status VARCHAR
) AS $$
BEGIN
    RETURN QUERY
    SELECT b.id, b.resort_id, b.guest_name, b.guest_email, b.check_in, b.check_out, b.total_price, b.status
    FROM bookings b
    WHERE b.id = p_id;
END;
$$ LANGUAGE plpgsql;
