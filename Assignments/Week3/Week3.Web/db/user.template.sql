-- Template for creating an application MySQL user and granting privileges
-- Usage:
--   1) Copy this file to user.sql
--   2) Replace placeholders <DB_NAME>, <APP_USER>, <APP_PASSWORD>
--   3) Run:  mysql -u root -p < /path/to/user.sql

-- Create user (both localhost and any host variants if needed)
CREATE USER IF NOT EXISTS '<APP_USER>'@'localhost' IDENTIFIED BY '<APP_PASSWORD>';
CREATE USER IF NOT EXISTS '<APP_USER>'@'%' IDENTIFIED BY '<APP_PASSWORD>';

-- Grant least-privilege access to application database
GRANT SELECT, INSERT, UPDATE, DELETE, CREATE, ALTER, INDEX, REFERENCES ON <DB_NAME>.* TO '<APP_USER>'@'localhost';
GRANT SELECT, INSERT, UPDATE, DELETE, CREATE, ALTER, INDEX, REFERENCES ON <DB_NAME>.* TO '<APP_USER>'@'%';

FLUSH PRIVILEGES;

-- Optional: seed default roles in the target DB
INSERT INTO <DB_NAME>.roles (name, description)
VALUES ('User', 'Varsayılan kullanıcı rolü')
ON DUPLICATE KEY UPDATE description = VALUES(description);

INSERT INTO <DB_NAME>.roles (name, description)
VALUES ('Admin', 'Yönetici rolü')
ON DUPLICATE KEY UPDATE description = VALUES(description);


