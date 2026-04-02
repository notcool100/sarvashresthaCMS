CREATE OR REPLACE FUNCTION get_all_offers()
RETURNS TABLE (
    id INTEGER,
    title VARCHAR,
    description TEXT,
    discount_type VARCHAR,
    discount_value DECIMAL,
    start_date TIMESTAMPTZ,
    end_date TIMESTAMPTZ,
    is_active BOOLEAN,
    applies_to_all_rooms BOOLEAN,
    image_url VARCHAR
) AS $$
BEGIN
    RETURN QUERY
    SELECT o.id, o.title, o.description, o.discount_type, o.discount_value,
           o.start_date, o.end_date, o.is_active, o.applies_to_all_rooms, o.image_url
    FROM offers o
    ORDER BY o.created_at DESC;
END;
$$ LANGUAGE plpgsql;
