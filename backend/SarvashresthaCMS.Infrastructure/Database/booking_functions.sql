-- PG functions for SarvashresthaCMS Bookings

-- 1. Create Booking Function
CREATE OR REPLACE FUNCTION create_booking(
    p_resort_id INTEGER,
    p_guest_name VARCHAR,
    p_guest_email VARCHAR,
    p_check_in TIMESTAMP,
    p_check_out TIMESTAMP,
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

-- 2. Get All Bookings Function
CREATE OR REPLACE FUNCTION get_all_bookings()
RETURNS TABLE (
    id INTEGER,
    resort_id INTEGER,
    guest_name VARCHAR,
    guest_email VARCHAR,
    check_in TIMESTAMP,
    check_out TIMESTAMP,
    total_price DECIMAL,
    status VARCHAR
) AS $$
BEGIN
    RETURN QUERY
    SELECT b.id, b.resort_id, b.guest_name, b.guest_email, b.check_in, b.check_out, b.total_price, b.status
    FROM bookings b;
END;
$$ LANGUAGE plpgsql;

-- 3. Get Booking By ID Function
CREATE OR REPLACE FUNCTION get_booking_by_id(p_id INTEGER)
RETURNS TABLE (
    id INTEGER,
    resort_id INTEGER,
    guest_name VARCHAR,
    guest_email VARCHAR,
    check_in TIMESTAMP,
    check_out TIMESTAMP,
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

-- 4. Update Booking Status Function
-- Updated to return INTEGER (affected row count) for Dapper success detection.
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

-- 5. Delete Booking Function
-- Updated to return INTEGER (affected row count) for Dapper success detection.
CREATE OR REPLACE FUNCTION delete_booking(p_id INTEGER)
RETURNS INTEGER AS $$
DECLARE
    affected_rows INTEGER;
BEGIN
    DELETE FROM bookings
    WHERE id = p_id;
    
    GET DIAGNOSTICS affected_rows = ROW_COUNT;
    RETURN affected_rows;
END;
$$ LANGUAGE plpgsql;
