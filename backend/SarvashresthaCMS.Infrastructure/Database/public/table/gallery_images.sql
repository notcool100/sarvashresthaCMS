CREATE TABLE IF NOT EXISTS gallery_images (
    id SERIAL PRIMARY KEY,
    image_url TEXT NOT NULL,
    alt_text TEXT,
    category VARCHAR(50) NOT NULL,
    display_order INTEGER DEFAULT 0,
    created_at TIMESTAMPTZ DEFAULT CURRENT_TIMESTAMP
);
