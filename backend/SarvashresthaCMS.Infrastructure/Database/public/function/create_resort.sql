CREATE OR REPLACE FUNCTION create_resort(
    p_name VARCHAR,
    p_location VARCHAR,
    p_description TEXT,
    p_price_per_night DECIMAL,
    p_capacity INTEGER,
    p_is_available BOOLEAN
) RETURNS INTEGER AS $$
DECLARE
    v_resort_id INTEGER;
BEGIN
    INSERT INTO resorts (name, location, description, price_per_night, capacity, is_available)
    VALUES (p_name, p_location, p_description, p_price_per_night, p_capacity, p_is_available)
    RETURNING id INTO v_resort_id;
    
    RETURN v_resort_id;
END;
$$ LANGUAGE plpgsql;
