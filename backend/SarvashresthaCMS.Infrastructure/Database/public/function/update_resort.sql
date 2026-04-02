CREATE OR REPLACE FUNCTION update_resort(
    p_id INTEGER,
    p_name VARCHAR,
    p_location VARCHAR,
    p_description TEXT,
    p_price_per_night DECIMAL,
    p_capacity INTEGER,
    p_is_available BOOLEAN
) RETURNS INTEGER AS $$
DECLARE
    affected_rows INTEGER;
BEGIN
    UPDATE resorts
    SET name = p_name,
        location = p_location,
        description = p_description,
        price_per_night = p_price_per_night,
        capacity = p_capacity,
        is_available = p_is_available
    WHERE id = p_id;
    
    GET DIAGNOSTICS affected_rows = ROW_COUNT;
    RETURN affected_rows;
END;
$$ LANGUAGE plpgsql;
