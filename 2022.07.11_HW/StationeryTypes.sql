-- Table: public.StationeryTypes

-- DROP TABLE IF EXISTS public."StationeryTypes";

CREATE TABLE IF NOT EXISTS public."StationeryTypes"
(
    "Id" integer NOT NULL DEFAULT nextval('"StationeryTypes_Id_seq"'::regclass),
    "Name" text COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT "StationeryTypes_pkey" PRIMARY KEY ("Id"),
    CONSTRAINT "CK_Name_01" CHECK ("Name" <> ''::text)
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."StationeryTypes"
    OWNER to postgres;