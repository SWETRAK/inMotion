CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM pg_namespace WHERE nspname = 'friends') THEN
        CREATE SCHEMA friends;
    END IF;
END $EF$;

CREATE TABLE friends.friendships (
    "Id" uuid NOT NULL,
    first_user_id uuid NOT NULL,
    second_user_id uuid NOT NULL,
    status integer NOT NULL,
    creation_date timestamp with time zone NOT NULL,
    last_modification_date timestamp with time zone NOT NULL,
    CONSTRAINT "PK_friendships" PRIMARY KEY ("Id")
);

CREATE INDEX "IX_friendships_Id" ON friends.friendships ("Id") INCLUDE (second_user_id, first_user_id);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20230814141922_Initial migration from Friends', '7.0.10');

COMMIT;

START TRANSACTION;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20230830145835_Initial migration for User', '7.0.10');

COMMIT;

