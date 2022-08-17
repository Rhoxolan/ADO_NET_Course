-- Table: public.fruitsandvegetables

-- DROP TABLE IF EXISTS public.fruitsandvegetables;

CREATE TABLE IF NOT EXISTS public.fruitsandvegetables
(
    id integer NOT NULL DEFAULT nextval('"FruitsAndVegetables_Id_seq"'::regclass),
    name text COLLATE pg_catalog."default" NOT NULL,
    type integer NOT NULL,
    color text COLLATE pg_catalog."default" NOT NULL,
    calories integer,
    CONSTRAINT "FruitsAndVegetables_pkey" PRIMARY KEY (id)
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.fruitsandvegetables
    OWNER to postgres;