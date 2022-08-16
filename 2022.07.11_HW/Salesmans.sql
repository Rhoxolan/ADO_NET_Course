-- Table: public.Salesmans

-- DROP TABLE IF EXISTS public."Salesmans";

CREATE TABLE IF NOT EXISTS public."Salesmans"
(
    "Id" integer NOT NULL DEFAULT nextval('"Salesmans_Id_seq"'::regclass),
    "Name" text COLLATE pg_catalog."default" NOT NULL,
    "Surname" text COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT "Salesmans_pkey" PRIMARY KEY ("Id"),
    CONSTRAINT "CK_Name_01" CHECK ("Name" <> ''::text),
    CONSTRAINT "CK_Surname_01" CHECK ("Surname" <> ''::text)
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."Salesmans"
    OWNER to postgres;