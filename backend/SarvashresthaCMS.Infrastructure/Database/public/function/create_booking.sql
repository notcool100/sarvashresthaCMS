CREATE OR REPLACE FUNCTION create_booking(
    p_resort_id INTEGER,
    p_guest_name VARCHAR,
    p_guest_email VARCHAR,
    p_check_in TIMESTAMPTZ,
    p_check_out TIMESTAMPTZ,
    p_total_price DECIMAL
) RETURNS INTEGER AS $$
DECLARE
    v_booking_id INTEGER;
BEGIN
    INSERT INTO bookings (resort_id, guest_name, guest_email, check_in, check_out, total_price, status)
    VALUES (p_resort_id, p_guest_name, p_guest_email, p_check_in, p_check_out, p_total_price, 'Pending')
    RETURNING id INTO v_booking_id;
    
    RETURN v_booking_id;
END;
$$ LANGUAGE plpgsql;
