CREATE OR REPLACE FUNCTION get_all_resorts()
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
    FROM resorts r;
END;
$$ LANGUAGE plpgsql;
