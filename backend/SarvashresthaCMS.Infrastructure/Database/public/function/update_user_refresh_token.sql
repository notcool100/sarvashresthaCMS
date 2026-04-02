CREATE OR REPLACE FUNCTION update_user_refresh_token(
    p_user_id INTEGER,
    p_refresh_token VARCHAR,
    p_expiry_time TIMESTAMP
) RETURNS VOID AS $$
BEGIN
    UPDATE users
    SET refresh_token = p_refresh_token,
        refresh_token_expiry_time = p_expiry_time
    WHERE id = p_user_id;
END;
$$ LANGUAGE plpgsql;
