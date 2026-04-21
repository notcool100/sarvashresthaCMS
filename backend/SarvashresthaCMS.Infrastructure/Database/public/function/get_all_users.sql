CREATE OR REPLACE FUNCTION get_all_users(p_users INTEGER)
RETURNS TABLE (
    id INTEGER,
    
    username VARCHAR,
    email VARCHAR,
    
) AS $$
BEGIN
    RETURN QUERY
    SELECT u.id, , u.username, u.email 
    FROM users u;
   
END;
$$ LANGUAGE plpgsql;
