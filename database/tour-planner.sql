create table tour
(
    id             serial
        constraint tour_pkey
            primary key,
    name           text             not null,
    description    text             not null,
    start          text             not null,
    destination    text             not null,
    transport_type integer          not null,
    distance       double precision not null,
    image_path     text default ''::text,
    time           integer
);

create table log
(
    id              serial
        constraint log_pk
            primary key,
    tour_id         integer           not null
        constraint log_tour_fk
            references tour
            on delete cascade,
    total_time      integer           not null,
    comment         text              not null,
    difficulty      integer default 0 not null,
    rating          integer default 0 not null,
    start_time      timestamp         not null,
    end_time        timestamp         not null,
    log_start       text              not null,
    log_destination text              not null
);