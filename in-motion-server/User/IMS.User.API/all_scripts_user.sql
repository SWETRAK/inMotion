CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM pg_namespace WHERE nspname = 'user') THEN
        CREATE SCHEMA "user";
    END IF;
END $EF$;

CREATE TABLE "user".user_profile_videos (
    id uuid NOT NULL,
    author_external_id uuid NOT NULL,
    "UserMetasId" uuid NOT NULL,
    filename text NOT NULL,
    bucket_location text NOT NULL,
    bucket_name text NOT NULL,
    content_type text NOT NULL,
    creation_date timestamp with time zone NOT NULL,
    last_edition_name timestamp with time zone NOT NULL,
    CONSTRAINT "PK_user_profile_videos" PRIMARY KEY (id),
    CONSTRAINT "AK_user_profile_videos_UserMetasId" UNIQUE ("UserMetasId")
);

CREATE TABLE "user".user_metas (
    id uuid NOT NULL,
    "UserExternalId" uuid NOT NULL,
    bio character varying(1024) NULL,
    profile_video_id uuid NULL,
    CONSTRAINT "PK_user_metas" PRIMARY KEY (id),
    CONSTRAINT "FK_user_metas_user_profile_videos_profile_video_id" FOREIGN KEY (profile_video_id) REFERENCES "user".user_profile_videos ("UserMetasId")
);

CREATE INDEX "IX_user_metas_id" ON "user".user_metas (id);

CREATE UNIQUE INDEX "IX_user_metas_profile_video_id" ON "user".user_metas (profile_video_id);

CREATE INDEX "IX_user_profile_videos_id" ON "user".user_profile_videos (id);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20230910124058_Init user migration', '7.0.10');

COMMIT;

START TRANSACTION;

DROP INDEX "user"."IX_user_profile_videos_id";

DROP INDEX "user"."IX_user_metas_id";

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20231021102340_migrationFix', '7.0.10');

COMMIT;

START TRANSACTION;

ALTER TABLE "user".user_metas DROP CONSTRAINT "FK_user_metas_user_profile_videos_profile_video_id";

ALTER TABLE "user".user_profile_videos DROP CONSTRAINT "AK_user_profile_videos_UserMetasId";

UPDATE "user".user_metas SET profile_video_id = '00000000-0000-0000-0000-000000000000' WHERE profile_video_id IS NULL;
ALTER TABLE "user".user_metas ALTER COLUMN profile_video_id SET NOT NULL;
ALTER TABLE "user".user_metas ALTER COLUMN profile_video_id SET DEFAULT '00000000-0000-0000-0000-000000000000';

ALTER TABLE "user".user_metas ADD CONSTRAINT "FK_user_metas_user_profile_videos_profile_video_id" FOREIGN KEY (profile_video_id) REFERENCES "user".user_profile_videos (id) ON DELETE CASCADE;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20231021104013_migrationFix2', '7.0.10');

COMMIT;

START TRANSACTION;

ALTER TABLE "user".user_metas DROP CONSTRAINT "FK_user_metas_user_profile_videos_profile_video_id";

DROP INDEX "user"."IX_user_metas_profile_video_id";

ALTER TABLE "user".user_metas ADD CONSTRAINT "AK_user_metas_profile_video_id" UNIQUE (profile_video_id);

CREATE UNIQUE INDEX "IX_user_profile_videos_UserMetasId" ON "user".user_profile_videos ("UserMetasId");

ALTER TABLE "user".user_profile_videos ADD CONSTRAINT "FK_user_profile_videos_user_metas_UserMetasId" FOREIGN KEY ("UserMetasId") REFERENCES "user".user_metas (profile_video_id) ON DELETE CASCADE;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20231021104405_migrationFix3', '7.0.10');

COMMIT;

START TRANSACTION;

ALTER TABLE "user".user_profile_videos DROP CONSTRAINT "FK_user_profile_videos_user_metas_UserMetasId";

DROP INDEX "user"."IX_user_profile_videos_UserMetasId";

ALTER TABLE "user".user_metas DROP CONSTRAINT "AK_user_metas_profile_video_id";

CREATE UNIQUE INDEX "IX_user_metas_profile_video_id" ON "user".user_metas (profile_video_id);

ALTER TABLE "user".user_metas ADD CONSTRAINT "FK_user_metas_user_profile_videos_profile_video_id" FOREIGN KEY (profile_video_id) REFERENCES "user".user_profile_videos (id) ON DELETE CASCADE;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20231021105322_migrationFix4', '7.0.10');

COMMIT;

START TRANSACTION;

ALTER TABLE "user".user_profile_videos DROP COLUMN "UserMetasId";

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20231021110528_migrationFix5', '7.0.10');

COMMIT;

START TRANSACTION;

ALTER TABLE "user".user_metas DROP CONSTRAINT "FK_user_metas_user_profile_videos_profile_video_id";

ALTER TABLE "user".user_metas ALTER COLUMN profile_video_id DROP NOT NULL;

ALTER TABLE "user".user_metas ADD CONSTRAINT "FK_user_metas_user_profile_videos_profile_video_id" FOREIGN KEY (profile_video_id) REFERENCES "user".user_profile_videos (id);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20231021111231_migrationFix6', '7.0.10');

COMMIT;

