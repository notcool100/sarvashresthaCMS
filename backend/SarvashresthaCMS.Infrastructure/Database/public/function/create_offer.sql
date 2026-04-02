CREATE OR REPLACE FUNCTION create_offer(
    p_title VARCHAR,
    p_description TEXT,
    p_discount_type VARCHAR,
    p_discount_value DECIMAL,
    p_start_date TIMESTAMPTZ,
    p_end_date TIMESTAMPTZ,
    p_is_active BOOLEAN,
    p_applies_to_all_rooms BOOLEAN,
    p_image_url VARCHAR
) RETURNS INTEGER AS $$
DECLARE
    v_offer_id INTEGER;
BEGIN
    INSERT INTO offers (
        title, description, discount_type, discount_value,
        start_date, end_date, is_active, applies_to_all_rooms, image_url
    )
    VALUES (
        p_title, p_description, p_discount_type, p_discount_value,
        p_start_date, p_end_date, p_is_active, p_applies_to_all_rooms, p_image_url
    )
    RETURNING id INTO v_offer_id;
    
    RETURN v_offer_id;
END;
$$ LANGUAGE plpgsql;
