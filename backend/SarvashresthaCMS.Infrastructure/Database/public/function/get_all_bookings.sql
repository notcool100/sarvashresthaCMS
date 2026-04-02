CREATE OR REPLACE FUNCTION get_all_bookings()
RETURNS TABLE (
    id INTEGER,
    room_id INTEGER,
    offer_id INTEGER,
    guest_name VARCHAR,
    guest_email VARCHAR,
    check_in TIMESTAMPTZ,
    check_out TIMESTAMPTZ,
    total_price DECIMAL,
    discount_amount DECIMAL,
    final_price DECIMAL,
    status VARCHAR
) AS $$
BEGIN
    RETURN QUERY
    SELECT b.id, b.room_id, b.offer_id, b.guest_name, b.guest_email, b.check_in, b.check_out, b.total_price, b.discount_amount, b.final_price, b.status
    FROM bookings b;
END;
$$ LANGUAGE plpgsql;
