CREATE OR REPLACE FUNCTION update_room(
    p_id INTEGER,
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
    affected_rows INTEGER;
BEGIN
    UPDATE rooms
    SET name = p_name,
        location = p_location,
        description = p_description,
        price_per_night = p_price_per_night,
        capacity = p_capacity,
        is_available = p_is_available,
        view_type = p_view_type,
        amenities = p_amenities,
        image_urls = p_image_urls,
        bed_type = p_bed_type,
        size_sqft = p_size_sqft
    WHERE id = p_id;
    
    GET DIAGNOSTICS affected_rows = ROW_COUNT;
    RETURN affected_rows;
END;
$$ LANGUAGE plpgsql;
