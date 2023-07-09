CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

CREATE TABLE localizations (
    "Id" uuid NOT NULL,
    name character varying(512) NULL,
    latitude double precision NOT NULL,
    longitude double precision NOT NULL,
    CONSTRAINT "PK_localizations" PRIMARY KEY ("Id")
);

CREATE TABLE user_profile_videos (
    "Id" uuid NOT NULL,
    author_id uuid NOT NULL,
    filename text NOT NULL,
    bucket_location text NOT NULL,
    bucket_name text NOT NULL,
    content_type text NOT NULL,
    creation_date timestamp with time zone NOT NULL,
    last_edition_name timestamp with time zone NOT NULL,
    CONSTRAINT "PK_user_profile_videos" PRIMARY KEY ("Id"),
    CONSTRAINT "AK_user_profile_videos_author_id" UNIQUE (author_id)
);

CREATE TABLE users (
    "Id" uuid NOT NULL,
    "email`" text NOT NULL,
    hashed_password text NULL,
    "ConfirmedAccount" boolean NOT NULL,
    activation_token text NULL,
    nickname character varying(24) NOT NULL,
    bio character varying(1024) NULL,
    profile_video_id uuid NOT NULL,
    "Role" integer NOT NULL,
    CONSTRAINT "PK_users" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_users_user_profile_videos_profile_video_id" FOREIGN KEY (profile_video_id) REFERENCES user_profile_videos (author_id) ON DELETE CASCADE
);

CREATE TABLE friendships (
    "Id" uuid NOT NULL,
    first_user_id uuid NOT NULL,
    second_user_id uuid NOT NULL,
    status integer NOT NULL,
    creation_date timestamp with time zone NOT NULL,
    last_modification_date timestamp with time zone NOT NULL,
    CONSTRAINT "PK_friendships" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_friendships_users_first_user_id" FOREIGN KEY (first_user_id) REFERENCES users ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_friendships_users_second_user_id" FOREIGN KEY (second_user_id) REFERENCES users ("Id") ON DELETE CASCADE
);

CREATE TABLE post_videos (
    "Id" uuid NOT NULL,
    type integer NOT NULL,
    post_front_id uuid NOT NULL,
    post_rear_id uuid NOT NULL,
    author_id uuid NOT NULL,
    "AuthorId" uuid NULL,
    filename text NOT NULL,
    bucket_location text NOT NULL,
    bucket_name text NOT NULL,
    content_type text NOT NULL,
    creation_date timestamp with time zone NOT NULL,
    last_edition_name timestamp with time zone NOT NULL,
    CONSTRAINT "PK_post_videos" PRIMARY KEY ("Id"),
    CONSTRAINT "AK_post_videos_post_front_id" UNIQUE (post_front_id),
    CONSTRAINT "AK_post_videos_post_rear_id" UNIQUE (post_rear_id),
    CONSTRAINT "FK_post_videos_users_AuthorId" FOREIGN KEY ("AuthorId") REFERENCES users ("Id")
);

CREATE TABLE providers (
    "Id" uuid NOT NULL,
    user_id uuid NOT NULL,
    name integer NOT NULL,
    auth_key text NOT NULL,
    CONSTRAINT "PK_providers" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_providers_users_user_id" FOREIGN KEY (user_id) REFERENCES users ("Id") ON DELETE CASCADE
);

CREATE TABLE tags (
    "Id" uuid NOT NULL,
    author_id uuid NOT NULL,
    name character varying(24) NOT NULL,
    creation_date timestamp with time zone NOT NULL,
    CONSTRAINT "PK_tags" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_tags_users_author_id" FOREIGN KEY (author_id) REFERENCES users ("Id") ON DELETE CASCADE
);

CREATE TABLE posts (
    "Id" uuid NOT NULL,
    author_id uuid NOT NULL,
    description character varying(2048) NULL,
    title character varying(256) NOT NULL,
    creation_date timestamp with time zone NOT NULL,
    last_modified_date timestamp with time zone NOT NULL,
    localization_id uuid NOT NULL,
    front_video_id uuid NOT NULL,
    rear_video_id uuid NOT NULL,
    CONSTRAINT "PK_posts" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_posts_localizations_localization_id" FOREIGN KEY (localization_id) REFERENCES localizations ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_posts_post_videos_front_video_id" FOREIGN KEY (front_video_id) REFERENCES post_videos (post_front_id) ON DELETE CASCADE,
    CONSTRAINT "FK_posts_post_videos_rear_video_id" FOREIGN KEY (rear_video_id) REFERENCES post_videos (post_rear_id) ON DELETE CASCADE,
    CONSTRAINT "FK_posts_users_author_id" FOREIGN KEY (author_id) REFERENCES users ("Id") ON DELETE CASCADE
);

CREATE TABLE post_comments (
    "Id" uuid NOT NULL,
    "PostId" uuid NOT NULL,
    author_id uuid NOT NULL,
    content character varying(1024) NOT NULL,
    creation_date timestamp with time zone NOT NULL,
    last_modified_date timestamp with time zone NOT NULL,
    CONSTRAINT "PK_post_comments" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_post_comments_posts_PostId" FOREIGN KEY ("PostId") REFERENCES posts ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_post_comments_users_author_id" FOREIGN KEY (author_id) REFERENCES users ("Id") ON DELETE CASCADE
);

CREATE TABLE post_reactions (
    "Id" uuid NOT NULL,
    post_id uuid NOT NULL,
    author_id uuid NOT NULL,
    emoji text NOT NULL,
    creation_date timestamp with time zone NOT NULL,
    last_modification_date timestamp with time zone NOT NULL,
    CONSTRAINT "PK_post_reactions" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_post_reactions_posts_post_id" FOREIGN KEY (post_id) REFERENCES posts ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_post_reactions_users_author_id" FOREIGN KEY (author_id) REFERENCES users ("Id") ON DELETE CASCADE
);

CREATE TABLE posts_tags_relations (
    "PostId" uuid NOT NULL,
    "TagsId" uuid NOT NULL,
    CONSTRAINT "PK_posts_tags_relations" PRIMARY KEY ("PostId", "TagsId"),
    CONSTRAINT "FK_posts_tags_relations_posts_PostId" FOREIGN KEY ("PostId") REFERENCES posts ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_posts_tags_relations_tags_TagsId" FOREIGN KEY ("TagsId") REFERENCES tags ("Id") ON DELETE CASCADE
);

CREATE TABLE post_comment_reaction (
    "Id" uuid NOT NULL,
    post_comment_id uuid NOT NULL,
    author_id uuid NOT NULL,
    emoji character varying(10) NOT NULL,
    creation_date timestamp with time zone NOT NULL,
    last_modification_date timestamp with time zone NOT NULL,
    CONSTRAINT "PK_post_comment_reaction" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_post_comment_reaction_post_comments_post_comment_id" FOREIGN KEY (post_comment_id) REFERENCES post_comments ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_post_comment_reaction_users_author_id" FOREIGN KEY (author_id) REFERENCES users ("Id") ON DELETE CASCADE
);

CREATE INDEX "IX_friendships_first_user_id" ON friendships (first_user_id);

CREATE INDEX "IX_friendships_Id" ON friendships ("Id");

CREATE INDEX "IX_friendships_second_user_id" ON friendships (second_user_id);

CREATE INDEX "IX_localizations_Id" ON localizations ("Id");

CREATE INDEX "IX_post_comment_reaction_author_id" ON post_comment_reaction (author_id);

CREATE INDEX "IX_post_comment_reaction_Id" ON post_comment_reaction ("Id");

CREATE INDEX "IX_post_comment_reaction_post_comment_id" ON post_comment_reaction (post_comment_id);

CREATE INDEX "IX_post_comments_author_id" ON post_comments (author_id);

CREATE INDEX "IX_post_comments_Id" ON post_comments ("Id");

CREATE INDEX "IX_post_comments_PostId" ON post_comments ("PostId");

CREATE INDEX "IX_post_reactions_author_id" ON post_reactions (author_id);

CREATE INDEX "IX_post_reactions_Id" ON post_reactions ("Id");

CREATE INDEX "IX_post_reactions_post_id" ON post_reactions (post_id);

CREATE INDEX "IX_post_videos_AuthorId" ON post_videos ("AuthorId");

CREATE INDEX "IX_post_videos_Id" ON post_videos ("Id");

CREATE INDEX "IX_posts_author_id" ON posts (author_id);

CREATE UNIQUE INDEX "IX_posts_front_video_id" ON posts (front_video_id);

CREATE INDEX "IX_posts_Id" ON posts ("Id");

CREATE INDEX "IX_posts_localization_id" ON posts (localization_id);

CREATE UNIQUE INDEX "IX_posts_rear_video_id" ON posts (rear_video_id);

CREATE INDEX "IX_posts_tags_relations_TagsId" ON posts_tags_relations ("TagsId");

CREATE INDEX "IX_providers_Id" ON providers ("Id");

CREATE INDEX "IX_providers_user_id" ON providers (user_id);

CREATE INDEX "IX_tags_author_id" ON tags (author_id);

CREATE INDEX "IX_tags_Id" ON tags ("Id");

CREATE INDEX "IX_user_profile_videos_Id" ON user_profile_videos ("Id");

CREATE INDEX "IX_users_Id" ON users ("Id");

CREATE UNIQUE INDEX "IX_users_profile_video_id" ON users (profile_video_id);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20230709111306_Initial', '7.0.5');

COMMIT;

START TRANSACTION;

ALTER TABLE users DROP CONSTRAINT "FK_users_user_profile_videos_profile_video_id";

ALTER TABLE users ALTER COLUMN profile_video_id DROP NOT NULL;

ALTER TABLE users ADD CONSTRAINT "FK_users_user_profile_videos_profile_video_id" FOREIGN KEY (profile_video_id) REFERENCES user_profile_videos (author_id);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20230709112411_Fixed relation exception', '7.0.5');

COMMIT;

START TRANSACTION;

ALTER TABLE users RENAME COLUMN "email`" TO email;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20230709112908_Fixed user email name', '7.0.5');

COMMIT;

