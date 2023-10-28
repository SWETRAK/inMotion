CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM pg_namespace WHERE nspname = 'auth') THEN
        CREATE SCHEMA auth;
    END IF;
END $EF$;

CREATE TABLE auth.users (
    "Id" uuid NOT NULL,
    email text NOT NULL,
    hashed_password text NULL,
    nickname character varying(24) NOT NULL,
    "ConfirmedAccount" boolean NOT NULL,
    activation_token text NULL,
    "Role" integer NOT NULL,
    CONSTRAINT "PK_users" PRIMARY KEY ("Id")
);

CREATE TABLE auth.providers (
    "Id" uuid NOT NULL,
    user_id uuid NOT NULL,
    name integer NOT NULL,
    auth_key text NOT NULL,
    CONSTRAINT "PK_providers" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_providers_users_user_id" FOREIGN KEY (user_id) REFERENCES auth.users ("Id") ON DELETE CASCADE
);

CREATE INDEX "IX_providers_Id" ON auth.providers ("Id");

CREATE INDEX "IX_providers_user_id" ON auth.providers (user_id);

CREATE INDEX "IX_users_Id" ON auth.users ("Id");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20230719162332_Initial commit', '7.0.5');

COMMIT;

