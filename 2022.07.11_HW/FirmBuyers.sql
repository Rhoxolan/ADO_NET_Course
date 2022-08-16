-- Table: public.FirmBuyers

-- DROP TABLE IF EXISTS public."FirmBuyers";

CREATE TABLE IF NOT EXISTS public."FirmBuyers"
(
    "Id" integer NOT NULL DEFAULT nextval('"FirmBuyers_Id_seq"'::regclass),
    "Name" text COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT "FirmBuyers_pkey" PRIMARY KEY ("Id"),
    CONSTRAINT "CK_Name_01" CHECK ("Name" <> ''::text)
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."FirmBuyers"
    OWNER to postgres;