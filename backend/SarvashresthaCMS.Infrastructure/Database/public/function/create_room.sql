CREATE OR REPLACE FUNCTION create_room(
    p_name VARCHAR,
    p_location VARCHAR,
    p_description TEXT,
    p_price_per_night DECIMAL,
    p_capacity INTEGER,
    p_is_available BOOLEAN,
    p_view_type VARCHAR,
    p_amenities TEXT[],
    p_image_urls TEXT[],
    p_bed_type VARCHAR,
    p_size_sqft INTEGER
) RETURNS INTEGER AS $$
DECLARE
    v_room_id INTEGER;
BEGIN
    INSERT INTO rooms (name, location, description, price_per_night, capacity, is_available, view_type, amenities, image_urls, bed_type, size_sqft)
    VALUES (p_name, p_location, p_description, p_price_per_night, p_capacity, p_is_available, p_view_type, p_amenities, p_image_urls, p_bed_type, p_size_sqft)
    RETURNING id INTO v_room_id;
    
    RETURN v_room_id;
END;
$$ LANGUAGE plpgsql;
