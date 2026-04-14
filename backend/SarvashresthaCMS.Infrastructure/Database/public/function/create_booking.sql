CREATE OR REPLACE FUNCTION create_booking(
    p_room_id INTEGER,
    p_offer_id INTEGER,
    p_guest_name VARCHAR,
    p_guest_email VARCHAR,
    p_check_in TIMESTAMPTZ,
    p_check_out TIMESTAMPTZ,
    p_total_price DECIMAL,
    p_discount_amount DECIMAL,
    p_final_price DECIMAL,
    p_user_id INTEGER,
    p_number_of_people INTEGER
) RETURNS INTEGER AS $$
DECLARE
    v_booking_id INTEGER;
BEGIN
    INSERT INTO bookings (room_id, offer_id, guest_name, guest_email, check_in, check_out, total_price, discount_amount, final_price, status, user_id, number_of_people)
    VALUES (
        p_room_id,
        p_offer_id,
        p_guest_name,
        p_guest_email,
        p_check_in,
        p_check_out,
        p_total_price,
        COALESCE(p_discount_amount, 0),
        COALESCE(p_final_price, p_total_price - COALESCE(p_discount_amount, 0)),
        'Pending',
        book_by,
        COALESCE(p_number_of_people, 1)
    )
    RETURNING id INTO v_booking_id;
    
    RETURN v_booking_id;
END;
$$ LANGUAGE plpgsql;
