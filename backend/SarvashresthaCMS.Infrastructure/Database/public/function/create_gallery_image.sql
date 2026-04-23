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