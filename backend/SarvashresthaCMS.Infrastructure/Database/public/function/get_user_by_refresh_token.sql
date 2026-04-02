CREATE OR REPLACE FUNCTION get_user_by_refresh_token(p_refresh_token VARCHAR)
RETURNS TABLE (
    id INTEGER,
    username VARCHAR,
    email VARCHAR,
    password_hash VARCHAR,
    role INTEGER, -- Alias for role_id for Dapper compatibility
    refresh_token VARCHAR,
    refresh_token_expiry_time TIMESTAMPTZ
) AS $$
BEGIN
    RETURN QUERY
    SELECT u.id, u.username, u.email, u.password_hash, u.role_id, u.refresh_token, u.refresh_token_expiry_time
    FROM users u
    WHERE u.refresh_token = p_refresh_token;
END;
$$ LANGUAGE plpgsql;
