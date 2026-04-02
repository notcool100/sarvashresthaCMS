CREATE TABLE IF NOT EXISTS bookings (
    id SERIAL PRIMARY KEY,
    resort_id INTEGER REFERENCES resorts(id),
    guest_name VARCHAR(100) NOT NULL,
    guest_email VARCHAR(100) NOT NULL,
    check_in TIMESTAMPTZ NOT NULL,
    check_out TIMESTAMPTZ NOT NULL,
    total_price DECIMAL(10, 2) NOT NULL,
    status VARCHAR(20) DEFAULT 'Pending', -- Pending, Confirmed, Cancelled, Completed
    created_at TIMESTAMPTZ DEFAULT CURRENT_TIMESTAMP
);
