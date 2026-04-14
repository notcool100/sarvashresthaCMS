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

-- Create gallery image
CREATE OR REPLACE FUNCTION create_gallery_image(
    p_image_url TEXT,
    p_alt_text TEXT,
    p_category VARCHAR,
    p_display_order INTEGER
) RETURNS INTEGER AS $$
DECLARE
    v_id INTEGER;
BEGIN
    INSERT INTO gallery_images (image_url, alt_text, category, display_order)
    VALUES (p_image_url, p_alt_text, p_category, p_display_order)
    RETURNING id INTO v_id;
    
    RETURN v_id;
END;
$$ LANGUAGE plpgsql;

-- Delete gallery image
CREATE OR REPLACE FUNCTION delete_gallery_image(p_id INTEGER)
RETURNS BOOLEAN AS $$
DECLARE
    v_deleted BOOLEAN;
BEGIN
    DELETE FROM gallery_images WHERE id = p_id;
    GET DIAGNOSTICS v_deleted = ROW_COUNT;
    RETURN v_deleted;
END;
$$ LANGUAGE plpgsql;
