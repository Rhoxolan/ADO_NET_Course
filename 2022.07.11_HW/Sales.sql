-- Table: public.Sales

-- DROP TABLE IF EXISTS public."Sales";

CREATE TABLE IF NOT EXISTS public."Sales"
(
    "Id" integer NOT NULL DEFAULT nextval('"Sales_Id_seq"'::regclass),
    "StationeryId" integer NOT NULL,
    "SalesmanId" integer NOT NULL,
    "FirmBuyerId" integer NOT NULL,
    "DateOfSale" date NOT NULL,
    "UnitPrice" money NOT NULL,
    "UnitsAmount" integer NOT NULL,
    CONSTRAINT "Sales_pkey" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_FirmBuyerId_01" FOREIGN KEY ("FirmBuyerId")
        REFERENCES public."FirmBuyers" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT "FK_SalesmanId_01" FOREIGN KEY ("SalesmanId")
        REFERENCES public."Salesmans" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT "FK_StationeryId_01" FOREIGN KEY ("StationeryId")
        REFERENCES public."Stationeries" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT "CK_UnitPrice_01" CHECK ("UnitPrice" > '-1'::integer::money),
    CONSTRAINT "CK_UnitsAmount_01" CHECK ("UnitsAmount" > '-1'::integer)
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."Sales"
    OWNER to postgres;