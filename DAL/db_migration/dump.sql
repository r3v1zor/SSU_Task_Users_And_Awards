--
-- PostgreSQL database dump
--

-- Dumped from database version 11.7 (Ubuntu 11.7-0ubuntu0.19.10.1)
-- Dumped by pg_dump version 11.7 (Ubuntu 11.7-0ubuntu0.19.10.1)

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- Name: ssu_db; Type: DATABASE; Schema: -; Owner: r3v1zor
--

CREATE DATABASE ssu_db WITH TEMPLATE = template0 ENCODING = 'UTF8' LC_COLLATE = 'ru_RU.UTF-8' LC_CTYPE = 'ru_RU.UTF-8';


ALTER DATABASE ssu_db OWNER TO r3v1zor;

\connect ssu_db

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- Name: sp_add_award(character varying); Type: PROCEDURE; Schema: public; Owner: r3v1zor
--

CREATE PROCEDURE public.sp_add_award(title character varying)
    LANGUAGE sql
    AS $$
insert into award(title) values (title);
$$;


ALTER PROCEDURE public.sp_add_award(title character varying) OWNER TO r3v1zor;

--
-- Name: sp_add_award_to_user(integer, integer); Type: PROCEDURE; Schema: public; Owner: r3v1zor
--

CREATE PROCEDURE public.sp_add_award_to_user(userid integer, awardid integer)
    LANGUAGE sql
    AS $$
insert into user_to_award values (userId, awardId);
$$;


ALTER PROCEDURE public.sp_add_award_to_user(userid integer, awardid integer) OWNER TO r3v1zor;

--
-- Name: sp_add_user(character varying, timestamp without time zone, integer); Type: PROCEDURE; Schema: public; Owner: r3v1zor
--

CREATE PROCEDURE public.sp_add_user(n character varying, d timestamp without time zone, a integer)
    LANGUAGE sql
    AS $$
insert into user_(name, dateOfBirth, age) values (n, d, a)
;
$$;


ALTER PROCEDURE public.sp_add_user(n character varying, d timestamp without time zone, a integer) OWNER TO r3v1zor;

--
-- Name: sp_delete_award(integer); Type: PROCEDURE; Schema: public; Owner: r3v1zor
--

CREATE PROCEDURE public.sp_delete_award(id integer)
    LANGUAGE sql
    AS $$
delete from award a where a.id = id;
$$;


ALTER PROCEDURE public.sp_delete_award(id integer) OWNER TO r3v1zor;

--
-- Name: sp_delete_award_from_user(integer, integer); Type: PROCEDURE; Schema: public; Owner: r3v1zor
--

CREATE PROCEDURE public.sp_delete_award_from_user(userid integer, awardid integer)
    LANGUAGE sql
    AS $$
delete from user_to_award uta where uta.userId = userId and uta.awardId = awardId;
$$;


ALTER PROCEDURE public.sp_delete_award_from_user(userid integer, awardid integer) OWNER TO r3v1zor;

--
-- Name: sp_delete_user(integer); Type: PROCEDURE; Schema: public; Owner: r3v1zor
--

CREATE PROCEDURE public.sp_delete_user(id integer)
    LANGUAGE sql
    AS $$
delete from user_ u where u.id = id; 
$$;


ALTER PROCEDURE public.sp_delete_user(id integer) OWNER TO r3v1zor;

--
-- Name: sp_get_all_awards(); Type: PROCEDURE; Schema: public; Owner: r3v1zor
--

CREATE PROCEDURE public.sp_get_all_awards()
    LANGUAGE sql
    AS $$
select id, title from award; 
$$;


ALTER PROCEDURE public.sp_get_all_awards() OWNER TO r3v1zor;

--
-- Name: sp_get_all_awards_by_user_id(integer); Type: PROCEDURE; Schema: public; Owner: r3v1zor
--

CREATE PROCEDURE public.sp_get_all_awards_by_user_id(id integer)
    LANGUAGE sql
    AS $$
select a.id, a.title from user_to_award uta inner join award a on uta.awardId = a.id where uta.userId = id; 
$$;


ALTER PROCEDURE public.sp_get_all_awards_by_user_id(id integer) OWNER TO r3v1zor;

--
-- Name: sp_get_all_users(); Type: PROCEDURE; Schema: public; Owner: r3v1zor
--

CREATE PROCEDURE public.sp_get_all_users()
    LANGUAGE sql
    AS $$
select id, name, dateOfBirth, age from user_;
$$;


ALTER PROCEDURE public.sp_get_all_users() OWNER TO r3v1zor;

--
-- Name: sp_get_award_by_id(integer); Type: PROCEDURE; Schema: public; Owner: r3v1zor
--

CREATE PROCEDURE public.sp_get_award_by_id(id integer)
    LANGUAGE sql
    AS $$
select id, title from award a where a.id = id;
$$;


ALTER PROCEDURE public.sp_get_award_by_id(id integer) OWNER TO r3v1zor;

SET default_tablespace = '';

SET default_with_oids = false;

--
-- Name: award; Type: TABLE; Schema: public; Owner: r3v1zor
--

CREATE TABLE public.award (
    id integer NOT NULL,
    title character varying(200)
);


ALTER TABLE public.award OWNER TO r3v1zor;

--
-- Name: award_id_seq; Type: SEQUENCE; Schema: public; Owner: r3v1zor
--

CREATE SEQUENCE public.award_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.award_id_seq OWNER TO r3v1zor;

--
-- Name: award_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: r3v1zor
--

ALTER SEQUENCE public.award_id_seq OWNED BY public.award.id;


--
-- Name: user_; Type: TABLE; Schema: public; Owner: r3v1zor
--

CREATE TABLE public.user_ (
    id integer NOT NULL,
    name character varying(50),
    dateofbirth timestamp without time zone,
    age integer
);


ALTER TABLE public.user_ OWNER TO r3v1zor;

--
-- Name: user__id_seq; Type: SEQUENCE; Schema: public; Owner: r3v1zor
--

CREATE SEQUENCE public.user__id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.user__id_seq OWNER TO r3v1zor;

--
-- Name: user__id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: r3v1zor
--

ALTER SEQUENCE public.user__id_seq OWNED BY public.user_.id;


--
-- Name: user_to_award; Type: TABLE; Schema: public; Owner: r3v1zor
--

CREATE TABLE public.user_to_award (
    userid integer,
    awardid integer
);


ALTER TABLE public.user_to_award OWNER TO r3v1zor;

--
-- Name: award id; Type: DEFAULT; Schema: public; Owner: r3v1zor
--

ALTER TABLE ONLY public.award ALTER COLUMN id SET DEFAULT nextval('public.award_id_seq'::regclass);


--
-- Name: user_ id; Type: DEFAULT; Schema: public; Owner: r3v1zor
--

ALTER TABLE ONLY public.user_ ALTER COLUMN id SET DEFAULT nextval('public.user__id_seq'::regclass);


--
-- Data for Name: award; Type: TABLE DATA; Schema: public; Owner: r3v1zor
--

COPY public.award (id, title) FROM stdin;
1	some
2	some2
\.


--
-- Data for Name: user_; Type: TABLE DATA; Schema: public; Owner: r3v1zor
--

COPY public.user_ (id, name, dateofbirth, age) FROM stdin;
2	Vasya	2000-04-17 00:00:00	20
\.


--
-- Data for Name: user_to_award; Type: TABLE DATA; Schema: public; Owner: r3v1zor
--

COPY public.user_to_award (userid, awardid) FROM stdin;
2	1
2	2
\.


--
-- Name: award_id_seq; Type: SEQUENCE SET; Schema: public; Owner: r3v1zor
--

SELECT pg_catalog.setval('public.award_id_seq', 2, true);


--
-- Name: user__id_seq; Type: SEQUENCE SET; Schema: public; Owner: r3v1zor
--

SELECT pg_catalog.setval('public.user__id_seq', 2, true);


--
-- Name: award award_pk; Type: CONSTRAINT; Schema: public; Owner: r3v1zor
--

ALTER TABLE ONLY public.award
    ADD CONSTRAINT award_pk PRIMARY KEY (id);


--
-- Name: user_ user_pk; Type: CONSTRAINT; Schema: public; Owner: r3v1zor
--

ALTER TABLE ONLY public.user_
    ADD CONSTRAINT user_pk PRIMARY KEY (id);


--
-- Name: user_to_award user_to_award_awardid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: r3v1zor
--

ALTER TABLE ONLY public.user_to_award
    ADD CONSTRAINT user_to_award_awardid_fkey FOREIGN KEY (awardid) REFERENCES public.award(id);


--
-- Name: user_to_award user_to_award_userid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: r3v1zor
--

ALTER TABLE ONLY public.user_to_award
    ADD CONSTRAINT user_to_award_userid_fkey FOREIGN KEY (userid) REFERENCES public.user_(id);


--
-- PostgreSQL database dump complete
--

