BEGIN;
SET
    FOREIGN_KEY_CHECKS = 0;
DROP TABLE IF EXISTS user_interest;
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

CREATE TABLE user_role
(
    id   INTEGER AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(255)
);

CREATE TABLE user
(
    id                  INTEGER AUTO_INCREMENT PRIMARY KEY,
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
    country             VARCHAR(255),
    city                VARCHAR(255),
    phone_number        VARCHAR(255),
    date_of_birth       DATE,
    profile_picture_url VARCHAR(255),
    created_at          DATETIME,
    updated_at          DATETIME null,
    FOREIGN KEY (role_id) REFERENCES user_role (id)
);

CREATE TABLE interest
(
    id   INTEGER AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(255)
);

CREATE TABLE user_interest
(
    id          INTEGER AUTO_INCREMENT PRIMARY KEY,
    interest_id INTEGER,
    user_id     INTEGER,
    FOREIGN KEY (interest_id) REFERENCES interest (id),
    FOREIGN KEY (user_id) REFERENCES user (id)
);

CREATE TABLE mentor_program
(
    id          INTEGER AUTO_INCREMENT PRIMARY KEY,
    title       VARCHAR(255),
    description VARCHAR(255),
    location    VARCHAR(255),
    start       DATETIME,
    end         DATETIME,
    image_url   VARCHAR(255),
    is_active   BOOLEAN,
    created_at  DATETIME,
    updated_at  DATETIME null
);

CREATE TABLE mentor_program_user
(
    mentor_program_id INTEGER,
    user_id           INTEGER,
    is_creator        BOOLEAN,
    PRIMARY KEY (mentor_program_id, user_id),
    FOREIGN KEY (mentor_program_id) REFERENCES mentor_program (id),
    FOREIGN KEY (user_id) REFERENCES user (id)
);

CREATE TABLE volenteer_work
(
    id             INTEGER AUTO_INCREMENT PRIMARY KEY,
    title          VARCHAR(100),
    description    TEXT,
    start          DATETIME,
    end            DATETIME,
    location       VARCHAR(255),
    max_volenteers INTEGER,
    image_url      VARCHAR(255),
    is_active      BOOLEAN,
    created_at     DATETIME,
    updated_at     DATETIME null
);

CREATE TABLE volenteer_work_user
(
    user_id           INTEGER,
    volenteer_work_id INTEGER,
    is_creator        BOOLEAN,
    PRIMARY KEY (user_id, volenteer_work_id),
    FOREIGN KEY (user_id) REFERENCES user (id),
    FOREIGN KEY (volenteer_work_id) REFERENCES volenteer_work (id)
);

CREATE TABLE activity
(
    id               INTEGER AUTO_INCREMENT PRIMARY KEY,
    title            VARCHAR(100),
    description      TEXT,
    location         VARCHAR(255),
    start            DATETIME,
    end              DATETIME,
    max_participants INTEGER,
    created_at       DATETIME,
    is_active        BOOLEAN,
    updated_at       DATETIME null,
    image_url        VARCHAR(255)
);

CREATE TABLE event_user
(
    user_id     INTEGER,
    activity_id INTEGER,
    is_creator  BOOLEAN,
    PRIMARY KEY (user_id, activity_id),
    FOREIGN KEY (user_id) REFERENCES user (id),
    FOREIGN KEY (activity_id) REFERENCES activity (id)
);

CREATE TABLE collaborative_space
(
    id                INTEGER AUTO_INCREMENT PRIMARY KEY,
    name              VARCHAR(100),
    type              ENUM ('CHAT', 'FORM'),
    description       TEXT,
    image_url         VARCHAR(255),
    created_at        DATETIME,
    updated_at        DATETIME null,
    is_active         BOOLEAN,
    is_direct_message BOOLEAN
);

CREATE TABLE collaborative_space_user
(
    collaborative_space_id INTEGER,
    user_id                INTEGER,
    is_creator             BOOLEAN,
    PRIMARY KEY (collaborative_space_id, user_id),
    FOREIGN KEY (collaborative_space_id) REFERENCES collaborative_space (id),
    FOREIGN KEY (user_id) REFERENCES user (id)
);

CREATE TABLE collaborative_space_message
(
    id                     INTEGER AUTO_INCREMENT PRIMARY KEY,
    message                TEXT,
    parent_id              INTEGER  null,
    user_id                INTEGER,
    collaborative_space_id INTEGER,
    created_at             DATETIME,
    updated_at             DATETIME null,
    image_url              VARCHAR(255),
    is_active              BOOLEAN,
    FOREIGN KEY (parent_id) REFERENCES collaborative_space_message (id),
    FOREIGN KEY (user_id) REFERENCES user (id),
    FOREIGN KEY (collaborative_space_id) REFERENCES collaborative_space (id)
);
SET
    FOREIGN_KEY_CHECKS = 1;
COMMIT;
