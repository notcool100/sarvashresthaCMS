CREATE OR REPLACE FUNCTION get_resort_by_id(p_id INTEGER)
RETURNS TABLE (
    id INTEGER,
    name VARCHAR,
    location VARCHAR,
    description TEXT,
    price_per_night DECIMAL,
    capacity INTEGER,
    is_available BOOLEAN
) AS $$
BEGIN
    RETURN QUERY
    SELECT r.id, r.name, r.location, r.description, r.price_per_night, r.capacity, r.is_available
    FROM resorts r
    WHERE r.id = p_id;
END;
$$ LANGUAGE plpgsql;
