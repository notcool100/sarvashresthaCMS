CREATE OR REPLACE FUNCTION update_offer(
    p_id INTEGER,
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
    affected_rows INTEGER;
BEGIN
    UPDATE offers
    SET title = p_title,
        description = p_description,
        discount_type = p_discount_type,
        discount_value = p_discount_value,
        start_date = p_start_date,
        end_date = p_end_date,
        is_active = p_is_active,
        applies_to_all_rooms = p_applies_to_all_rooms,
        image_url = p_image_url
    WHERE id = p_id;
    
    GET DIAGNOSTICS affected_rows = ROW_COUNT;
    RETURN affected_rows;
END;
$$ LANGUAGE plpgsql;
