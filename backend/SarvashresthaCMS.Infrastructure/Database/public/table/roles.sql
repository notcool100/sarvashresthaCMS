CREATE TABLE IF NOT EXISTS roles (
    id INTEGER PRIMARY KEY,
    name VARCHAR(50) UNIQUE NOT NULL
);

-- Seed Initial Roles
INSERT INTO roles (id, name) VALUES (1, 'Admin') ON CONFLICT (id) DO NOTHING;
INSERT INTO roles (id, name) VALUES (2, 'Staff') ON CONFLICT (id) DO NOTHING;
INSERT INTO roles (id, name) VALUES (3, 'Customer') ON CONFLICT (id) DO NOTHING;
