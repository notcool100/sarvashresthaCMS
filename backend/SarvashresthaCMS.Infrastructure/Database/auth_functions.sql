-- SarvashresthaCMS Authentication Functions (PostgreSQL)

-- 1. Create User Function
CREATE OR REPLACE FUNCTION create_user(
    p_username VARCHAR,
    p_email VARCHAR,
    p_password_hash VARCHAR,
    p_role INTEGER
) RETURNS INTEGER AS $$
DECLARE
    v_user_id INTEGER;
BEGIN
    INSERT INTO users (username, email, password_hash, role, created_at)
    VALUES (p_username, p_email, p_password_hash, p_role, CURRENT_TIMESTAMP)
    RETURNING id INTO v_user_id;
    
    RETURN v_user_id;
END;
$$ LANGUAGE plpgsql;

-- 2. Get User By Email Function
CREATE OR REPLACE FUNCTION get_user_by_email(p_email VARCHAR)
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
    WHERE u.email = p_email;
END;
$$ LANGUAGE plpgsql;

-- 3. Get User By ID Function
CREATE OR REPLACE FUNCTION get_user_by_id(p_id INTEGER)
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
    WHERE u.id = p_id;
END;
$$ LANGUAGE plpgsql;

-- 4. Update Refresh Token Function
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

-- 5. Get User By Refresh Token Function
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
