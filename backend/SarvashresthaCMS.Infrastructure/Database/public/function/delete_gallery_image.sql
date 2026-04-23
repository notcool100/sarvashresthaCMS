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
