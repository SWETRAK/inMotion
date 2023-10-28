CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

-- AUTH database

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

-- FRIENDS database

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

-- POST database

START TRANSACTION;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM pg_namespace WHERE nspname = 'post') THEN
        CREATE SCHEMA post;
    END IF;
END $EF$;

CREATE TABLE post.localizations (
    "Id" uuid NOT NULL,
    name character varying(512) NULL,
    latitude double precision NOT NULL,
    longitude double precision NOT NULL,
    CONSTRAINT "PK_localizations" PRIMARY KEY ("Id")
);

CREATE TABLE post.tags (
    "Id" uuid NOT NULL,
    external_author_id uuid NOT NULL,
    name character varying(24) NOT NULL,
    creation_date timestamp with time zone NOT NULL,
    CONSTRAINT "PK_tags" PRIMARY KEY ("Id")
);

CREATE TABLE post.posts (
    "Id" uuid NOT NULL,
    external_author_id uuid NOT NULL,
    visibility integer NOT NULL,
    description character varying(2048) NULL,
    title character varying(256) NOT NULL,
    localization_id uuid NOT NULL,
    creation_date timestamp with time zone NOT NULL,
    last_modified_date timestamp with time zone NOT NULL,
    CONSTRAINT "PK_posts" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_posts_localizations_localization_id" FOREIGN KEY (localization_id) REFERENCES post.localizations ("Id") ON DELETE CASCADE
);

CREATE TABLE post.post_comments (
    "Id" uuid NOT NULL,
    external_author_id uuid NOT NULL,
    content character varying(1024) NOT NULL,
    "PostId" uuid NOT NULL,
    creation_date timestamp with time zone NOT NULL,
    last_modified_date timestamp with time zone NOT NULL,
    CONSTRAINT "PK_post_comments" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_post_comments_posts_PostId" FOREIGN KEY ("PostId") REFERENCES post.posts ("Id") ON DELETE CASCADE
);

CREATE TABLE post.post_reactions (
    "Id" uuid NOT NULL,
    external_author_id uuid NOT NULL,
    emoji text NOT NULL,
    post_id uuid NOT NULL,
    creation_date timestamp with time zone NOT NULL,
    last_modification_date timestamp with time zone NOT NULL,
    CONSTRAINT "PK_post_reactions" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_post_reactions_posts_post_id" FOREIGN KEY (post_id) REFERENCES post.posts ("Id") ON DELETE CASCADE
);

CREATE TABLE post.post_videos (
    "Id" uuid NOT NULL,
    external_author_id uuid NOT NULL,
    filename text NOT NULL,
    bucket_location text NOT NULL,
    bucket_name text NOT NULL,
    content_type text NOT NULL,
    type integer NOT NULL,
    post_id uuid NOT NULL,
    creation_date timestamp with time zone NOT NULL,
    last_edition_name timestamp with time zone NOT NULL,
    CONSTRAINT "PK_post_videos" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_post_videos_posts_post_id" FOREIGN KEY (post_id) REFERENCES post.posts ("Id") ON DELETE CASCADE
);

CREATE TABLE post.posts_tags_relations (
    "PostId" uuid NOT NULL,
    "TagsId" uuid NOT NULL,
    CONSTRAINT "PK_posts_tags_relations" PRIMARY KEY ("PostId", "TagsId"),
    CONSTRAINT "FK_posts_tags_relations_posts_PostId" FOREIGN KEY ("PostId") REFERENCES post.posts ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_posts_tags_relations_tags_TagsId" FOREIGN KEY ("TagsId") REFERENCES post.tags ("Id") ON DELETE CASCADE
);

CREATE TABLE post.post_comment_reaction (
    "Id" uuid NOT NULL,
    external_author_id uuid NOT NULL,
    emoji character varying(10) NOT NULL,
    post_comment_id uuid NOT NULL,
    creation_date timestamp with time zone NOT NULL,
    last_modification_date timestamp with time zone NOT NULL,
    CONSTRAINT "PK_post_comment_reaction" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_post_comment_reaction_post_comments_post_comment_id" FOREIGN KEY (post_comment_id) REFERENCES post.post_comments ("Id") ON DELETE CASCADE
);

CREATE INDEX "IX_localizations_Id" ON post.localizations ("Id");

CREATE INDEX "IX_post_comment_reaction_Id" ON post.post_comment_reaction ("Id");

CREATE INDEX "IX_post_comment_reaction_post_comment_id" ON post.post_comment_reaction (post_comment_id);

CREATE INDEX "IX_post_comments_Id" ON post.post_comments ("Id");

CREATE INDEX "IX_post_comments_PostId" ON post.post_comments ("PostId");

CREATE INDEX "IX_post_reactions_Id" ON post.post_reactions ("Id");

CREATE INDEX "IX_post_reactions_post_id" ON post.post_reactions (post_id);

CREATE INDEX "IX_post_videos_Id" ON post.post_videos ("Id");

CREATE INDEX "IX_post_videos_post_id" ON post.post_videos (post_id);

CREATE INDEX "IX_posts_Id" ON post.posts ("Id");

CREATE INDEX "IX_posts_localization_id" ON post.posts (localization_id);

CREATE INDEX "IX_posts_tags_relations_TagsId" ON post.posts_tags_relations ("TagsId");

CREATE INDEX "IX_tags_Id" ON post.tags ("Id");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20230916170000_Some data models fixes', '7.0.5');

COMMIT;

START TRANSACTION;

ALTER TABLE post.posts ADD iteration_id uuid NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000';

CREATE TABLE post.post_iterations (
    "Id" uuid NOT NULL,
    start_date_time timestamp with time zone NOT NULL,
    name text NOT NULL,
    CONSTRAINT "PK_post_iterations" PRIMARY KEY ("Id")
);

CREATE INDEX "IX_posts_iteration_id" ON post.posts (iteration_id);

CREATE INDEX "IX_post_iterations_Id" ON post.post_iterations ("Id");

ALTER TABLE post.posts ADD CONSTRAINT "FK_posts_post_iterations_iteration_id" FOREIGN KEY (iteration_id) REFERENCES post.post_iterations ("Id") ON DELETE CASCADE;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20230917212228_Added PostIteration entity', '7.0.5');

COMMIT;

-- USER database

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