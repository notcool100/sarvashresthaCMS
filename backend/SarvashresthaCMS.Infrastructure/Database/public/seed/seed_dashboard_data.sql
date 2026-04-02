-- Seed Mock Rooms
INSERT INTO rooms (id, name, location, description, price_per_night, capacity, is_available) VALUES
(1, 'Annapurna Suite', 'Annapurna Sanctuary', 'Luxury suite with mountain view', 1200.00, 2, true),
(2, 'Forest Villa 04', 'Ghandruk Forest', 'Quiet villa surrounded by nature', 850.00, 4, true),
(3, 'Luxury Zen Pod', 'Riverside', 'Minimalist design for deep relaxation', 500.00, 2, true),
(4, 'Heritage Royal Suite', 'Main Block', 'Traditional architecture with modern comfort', 1500.00, 2, true),
(5, 'Zen Garden Villa', 'Garden Area', 'Direct access to zen gardens', 900.00, 3, true),
(6, 'Forest Edge Studio', 'Forest Edge', 'Compact studio for solo travelers or couples', 450.00, 2, true)
ON CONFLICT (id) DO NOTHING;

-- Seed Mock Bookings (Last 6 months)
INSERT INTO bookings (room_id, guest_name, guest_email, check_in, check_out, total_price, status, created_at) VALUES
(1, 'Hari Ram', 'hari.ram@example.com', CURRENT_DATE - INTERVAL '2 days', CURRENT_DATE + INTERVAL '3 days', 6000.00, 'Confirmed', CURRENT_DATE - INTERVAL '10 days'),
(5, 'Shyam Thapa', 'shyam.thapa@example.com', CURRENT_DATE - INTERVAL '1 day', CURRENT_DATE + INTERVAL '4 days', 4500.00, 'Confirmed', CURRENT_DATE - INTERVAL '12 days'),
(6, 'Bikash Gurung', 'bikash.gurung@example.com', CURRENT_DATE + INTERVAL '5 days', CURRENT_DATE + INTERVAL '7 days', 900.00, 'Pending', CURRENT_DATE - INTERVAL '1 day'),
(4, 'Sita Basnet', 'sita.basnet@example.com', CURRENT_DATE - INTERVAL '1 month', CURRENT_DATE - INTERVAL '25 days', 7500.00, 'Completed', CURRENT_DATE - INTERVAL '40 days'),
(2, 'Ramesh Sharma', 'ramesh.sharma@example.com', CURRENT_DATE - INTERVAL '2 months', CURRENT_DATE - INTERVAL '55 days', 4250.00, 'Completed', CURRENT_DATE - INTERVAL '70 days'),
(3, 'Laxmi Rai', 'laxmi.rai@example.com', CURRENT_DATE - INTERVAL '3 months', CURRENT_DATE - INTERVAL '85 days', 2500.00, 'Completed', CURRENT_DATE - INTERVAL '100 days'),
(1, 'Ganesh Karki', 'ganesh.karki@example.com', CURRENT_DATE - INTERVAL '4 months', CURRENT_DATE - INTERVAL '115 days', 6000.00, 'Completed', CURRENT_DATE - INTERVAL '130 days'),
(5, 'Anita Shrestha', 'anita.shrestha@example.com', CURRENT_DATE - INTERVAL '5 months', CURRENT_DATE - INTERVAL '145 days', 4500.00, 'Completed', CURRENT_DATE - INTERVAL '160 days');
