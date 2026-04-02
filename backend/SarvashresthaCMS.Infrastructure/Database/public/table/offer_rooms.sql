CREATE TABLE IF NOT EXISTS offer_rooms (
    offer_id INTEGER NOT NULL REFERENCES offers(id) ON DELETE CASCADE,
    room_id INTEGER NOT NULL REFERENCES rooms(id) ON DELETE CASCADE,
    PRIMARY KEY (offer_id, room_id)
);
