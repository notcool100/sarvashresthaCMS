-- Get all gallery images
CREATE OR REPLACE FUNCTION get_all_gallery_images()
RETURNS TABLE (
    id INTEGER,
    image_url TEXT,
    alt_text TEXT,
    category VARCHAR,
    display_order INTEGER,
    created_at TIMESTAMPTZ
) AS $$
BEGIN
    RETURN QUERY
    SELECT g.id, g.image_url, g.alt_text, g.category, g.display_order, g.created_at
    FROM gallery_images g
    ORDER BY g.display_order ASC, g.created_at DESC;
END;
$$ LANGUAGE plpgsql;

