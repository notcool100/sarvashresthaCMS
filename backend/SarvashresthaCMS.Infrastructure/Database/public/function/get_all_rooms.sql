CREATE OR REPLACE FUNCTION get_all_rooms()
RETURNS TABLE (
    id INTEGER,
    name VARCHAR,
    location VARCHAR,
    description TEXT,
    price_per_night DECIMAL,
    capacity INTEGER,
    is_available BOOLEAN,
    view_type VARCHAR,
    amenities TEXT[],
    image_urls TEXT[],
    bed_type VARCHAR,
    size_sqft INTEGER
) AS $$
BEGIN
    RETURN QUERY
    SELECT r.id, r.name, r.location, r.description, r.price_per_night, r.capacity, r.is_available, r.view_type, r.amenities, r.image_urls, r.bed_type, r.size_sqft
    FROM rooms r;
END;
$$ LANGUAGE plpgsql;
