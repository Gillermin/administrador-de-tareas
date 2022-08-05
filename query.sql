CREATE TABLE public.tbl_collaborator
(
    id_collaborator integer NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 99999 CACHE 1 ),
    collaborator_name text,
    PRIMARY KEY (id_collaborator)
);

ALTER TABLE IF EXISTS public.tbl_collaborator
    OWNER to postgres;

    INSERT INTO public.tbl_collaborator(collaborator_name)	VALUES ('Juan Perez');
    INSERT INTO public.tbl_collaborator(collaborator_name)	VALUES ('Pablo Rojas');
    INSERT INTO public.tbl_collaborator(collaborator_name)	VALUES ('Jose Arce');


CREATE TABLE public.tbl_task
(
    id_task integer NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 99999 CACHE 1 ),
    id_collaborator integer,
    description text,
    status text DEFAULT 'PENDIENTE',
    priority text,
    from_date date,
    to_date date,
    notes text,
    PRIMARY KEY (id_task)
);

ALTER TABLE IF EXISTS public.tbl_task
    OWNER to postgres;

    