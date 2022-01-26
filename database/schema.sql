-- Table: public.events

--DROP TABLE IF EXISTS public.events;

CREATE TABLE IF NOT EXISTS public.events
(
    eventid integer NOT NULL serial primary key,
    "date" timestamp without time zone,
    "time" abstime,
	name text COLLATE pg_catalog."default",
    venue text COLLATE pg_catalog."default",
    address text COLLATE pg_catalog."default",
    city text COLLATE pg_catalog."default",
    state text COLLATE pg_catalog."default",
    "map" text COLLATE pg_catalog."default",
	frequency text COLLATE pg_catalog."default",
    cost numeric,
    info text COLLATE pg_catalog."default",
    email text COLLATE pg_catalog."default",
    link text COLLATE pg_catalog."default",
    phone text COLLATE pg_catalog."default",
    reviews text COLLATE pg_catalog."default",
    CONSTRAINT events_pkey PRIMARY KEY (eventid)
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.events
    OWNER to postgres;
