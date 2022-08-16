-- Table: public.Stationeries

-- DROP TABLE IF EXISTS public."Stationeries";

CREATE TABLE IF NOT EXISTS public."Stationeries"
(
    "Id" integer NOT NULL DEFAULT nextval('"Stationeries_Id_seq"'::regclass),
    "Name" text COLLATE pg_catalog."default" NOT NULL,
    "TypeId" integer NOT NULL,
    "Price" money NOT NULL DEFAULT 0,
    "Amount" integer NOT NULL DEFAULT 0,
    CONSTRAINT "Stationeries_pkey" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_TypeId_01" FOREIGN KEY ("TypeId")
        REFERENCES public."StationeryTypes" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT "CK_Name_01" CHECK ("Name" <> ''::text),
    CONSTRAINT "CK_Price_01" CHECK ("Price" > '-1'::integer::money),
    CONSTRAINT "CK_Amount_01" CHECK ("Amount" > '-1'::integer)
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."Stationeries"
    OWNER to postgres;