CREATE OR REPLACE FUNCTION get_user_by_refresh_token(p_refresh_token VARCHAR)
RETURNS TABLE (
    id INTEGER,
    username VARCHAR,
    email VARCHAR,
    password_hash VARCHAR,
    role INTEGER,
    refresh_token VARCHAR,
    refresh_token_expiry_time TIMESTAMP
) AS $$
BEGIN
    RETURN QUERY
    SELECT u.id, u.username, u.email, u.password_hash, u.role, u.refresh_token, u.refresh_token_expiry_time
    FROM users u
    WHERE u.refresh_token = p_refresh_token;
END;
$$ LANGUAGE plpgsql;
