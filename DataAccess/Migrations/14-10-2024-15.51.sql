-- Start transaction
BEGIN;
SET
FOREIGN_KEY_CHECKS = 0; 
-- Drop tables if they exist in the reverse order of their creation
DROP TABLE IF EXISTS collaborative_space_message;
DROP TABLE IF EXISTS collaborative_space_user;
DROP TABLE IF EXISTS event_user;
DROP TABLE IF EXISTS volenteer_work_user;
DROP TABLE IF EXISTS mentor_program_user;
DROP TABLE IF EXISTS interest;
DROP TABLE IF EXISTS collaborative_space;
DROP TABLE IF EXISTS volenteer_work;
DROP TABLE IF EXISTS mentor_program;
DROP TABLE IF EXISTS activity;
DROP TABLE IF EXISTS user_role;
DROP TABLE IF EXISTS user;

-- Create user_role table
CREATE TABLE user_role
(
    id   INTEGER PRIMARY KEY,
    name VARCHAR(255)
);

-- Create user table
CREATE TABLE user
(
    id                  INTEGER PRIMARY KEY,
    role_id             INTEGER,
    email               VARCHAR(255) UNIQUE,
    password            VARCHAR(255),
    salt                VARCHAR(255),
    active              BOOLEAN,
    google_id           INTEGER,
    facebook_id         INTEGER,
    first_name          VARCHAR(255),
    name_affix          VARCHAR(255),
    last_name           VARCHAR(255),
    address             VARCHAR(255),
    postal_code         VARCHAR(255),
    county              VARCHAR(255),
    city                VARCHAR(255),
    phone_number        VARCHAR(15),
    date_of_birth       TIMESTAMP,
    profile_picture_url VARCHAR(255),
    created_at          DATETIME,
    updated_at          DATETIME,
    FOREIGN KEY (role_id) REFERENCES user_role (id)
);

-- Create interest table
CREATE TABLE interest
(
    id      INTEGER PRIMARY KEY,
    user_id INTEGER,
    FOREIGN KEY (user_id) REFERENCES user (id)
);

-- Create mentor_program table
CREATE TABLE mentor_program
(
    id          INTEGER PRIMARY KEY,
    title       VARCHAR(255),
    description VARCHAR(255),
    location    VARCHAR(255),
    start       DATETIME,
    end         DATETIME,
    image_url   VARCHAR(255),
    is_active   BOOLEAN,
    created_at  DATETIME,
    updated_at  DATETIME
);

-- Create mentor_program_user table
CREATE TABLE mentor_program_user
(
    mentor_program_id INTEGER,
    user_id           INTEGER,
    is_creator        BOOLEAN,
    PRIMARY KEY (mentor_program_id, user_id),
    FOREIGN KEY (mentor_program_id) REFERENCES mentor_program (id),
    FOREIGN KEY (user_id) REFERENCES user (id)
);

-- Create volenteer_work table
CREATE TABLE volenteer_work
(
    id             INTEGER PRIMARY KEY,
    title          VARCHAR(100),
    description    TEXT,
    start          DATETIME,
    end            DATETIME,
    location       VARCHAR(255),
    max_volenteers INTEGER,
    image_url      VARCHAR(255),
    is_active      BOOLEAN,
    created_at     DATETIME,
    updated_at     DATETIME
);

-- Create volenteer_work_user table
CREATE TABLE volenteer_work_user
(
    user_id           INTEGER,
    volenteer_work_id INTEGER,
    is_creator        BOOLEAN,
    PRIMARY KEY (user_id, volenteer_work_id),
    FOREIGN KEY (user_id) REFERENCES user (id),
    FOREIGN KEY (volenteer_work_id) REFERENCES volenteer_work (id)
);

-- Create activity table
CREATE TABLE activity
(
    id               INTEGER PRIMARY KEY,
    title            VARCHAR(100),
    description      TEXT,
    location         VARCHAR(255),
    start            DATETIME,
    end              DATETIME,
    max_participants INTEGER,
    created_at       DATETIME,
    is_active        BOOLEAN,
    updated_at       DATETIME,
    image_url        VARCHAR(255)
);

-- Create event_user table
CREATE TABLE event_user
(
    user_id     INTEGER,
    activity_id INTEGER,
    is_creator  BOOLEAN,
    PRIMARY KEY (user_id, activity_id),
    FOREIGN KEY (user_id) REFERENCES user (id),
    FOREIGN KEY (activity_id) REFERENCES activity (id)
);

-- Create collaborative_space table
CREATE TABLE collaborative_space
(
    id          INTEGER PRIMARY KEY,
    name        VARCHAR(100),
    type        ENUM('CHAT', 'FORM'),
    description TEXT,
    created_at  DATETIME,
    updated_at  DATETIME,
    image_url   VARCHAR(255),
    is_active   BOOLEAN
);

-- Create collaborative_space_user table
CREATE TABLE collaborative_space_user
(
    collaborative_space_id INTEGER,
    user_id                INTEGER,
    is_creator             BOOLEAN,
    PRIMARY KEY (collaborative_space_id, user_id),
    FOREIGN KEY (collaborative_space_id) REFERENCES collaborative_space (id),
    FOREIGN KEY (user_id) REFERENCES user (id)
);

-- Create collaborative_space_message table
CREATE TABLE collaborative_space_message
(
    id                     INTEGER PRIMARY KEY,
    message                TEXT,
    parent_id              INTEGER,
    user_id                INTEGER,
    collaborative_space_id INTEGER,
    created_at             DATETIME,
    updated_at             DATETIME,
    image_url              VARCHAR(255),
    is_active              BOOLEAN,
    FOREIGN KEY (parent_id) REFERENCES collaborative_space_message (id),
    FOREIGN KEY (user_id) REFERENCES user (id),
    FOREIGN KEY (collaborative_space_id) REFERENCES collaborative_space (id)
);
SET
FOREIGN_KEY_CHECKS = 1; 
-- Commit transaction
COMMIT;
