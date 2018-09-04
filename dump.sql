--
-- PostgreSQL database dump
--

-- Dumped from database version 10.2
-- Dumped by pg_dump version 10.2

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SET check_function_bodies = false;
SET client_min_messages = warning;
SET row_security = off;

--
-- Name: call_center_demo; Type: DATABASE; Schema: -; Owner: postgres
--

CREATE DATABASE call_center_demo WITH TEMPLATE = template0 ENCODING = 'UTF8' LC_COLLATE = 'Russian_Russia.1251' LC_CTYPE = 'Russian_Russia.1251';


ALTER DATABASE call_center_demo OWNER TO postgres;

\connect call_center_demo

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SET check_function_bodies = false;
SET client_min_messages = warning;
SET row_security = off;

--
-- Name: plpgsql; Type: EXTENSION; Schema: -; Owner: 
--

CREATE EXTENSION IF NOT EXISTS plpgsql WITH SCHEMA pg_catalog;


--
-- Name: EXTENSION plpgsql; Type: COMMENT; Schema: -; Owner: 
--

COMMENT ON EXTENSION plpgsql IS 'PL/pgSQL procedural language';


SET search_path = public, pg_catalog;

SET default_tablespace = '';

SET default_with_oids = false;

--
-- Name: kz_category; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE kz_category (
    id bigint NOT NULL,
    name character varying(2000) NOT NULL
);


ALTER TABLE kz_category OWNER TO postgres;

--
-- Name: kz_request; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE kz_request (
    id bigint NOT NULL,
    consumer_name character varying(2000) NOT NULL,
    phone character varying(50) NOT NULL,
    comment character varying(2000),
    date date,
    state smallint NOT NULL,
    cat_id bigint NOT NULL,
    user_id bigint NOT NULL,
    executor_id bigint,
    worker_comment character varying(2000)
);


ALTER TABLE kz_request OWNER TO postgres;

--
-- Name: kz_user; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE kz_user (
    id bigint NOT NULL,
    login character varying NOT NULL,
    pass_hash character varying,
    name character varying NOT NULL,
    role smallint NOT NULL
);


ALTER TABLE kz_user OWNER TO postgres;

--
-- Name: throughout_app_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE throughout_app_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE throughout_app_id_seq OWNER TO postgres;

--
-- Data for Name: kz_category; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO kz_category VALUES (1, '23');
INSERT INTO kz_category VALUES (5, 'wer');
INSERT INTO kz_category VALUES (8, 'Жалоба');
INSERT INTO kz_category VALUES (12, 'Предложение');
INSERT INTO kz_category VALUES (21, 'Поздравление');


--
-- Data for Name: kz_request; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO kz_request VALUES (4, '1', '1465', '432', '2018-08-19', 1, 1, 2, NULL, NULL);
INSERT INTO kz_request VALUES (11, 'фыв', '1234', 'фыв', '2018-08-21', 1, 8, 2, NULL, NULL);
INSERT INTO kz_request VALUES (14, 'dwkjb', '1465', 'ывп', '2018-08-21', 1, 8, 2, NULL, NULL);
INSERT INTO kz_request VALUES (22, 'Иванов Иван Иванович', '8927456123789', 'Хочу поздравить с ДР кого-нибудь. Помогите!', '2018-09-02', 4, 21, 16, NULL, NULL);
INSERT INTO kz_request VALUES (24, 'Иванов2 Иван Иванович', '123', 'Пойти в магазинчик купить апельсинчик', '2018-09-03', 4, 12, 16, NULL, NULL);
INSERT INTO kz_request VALUES (23, 'Иванов2 Иван Иванович', '123', 'Пойти в магазинчик купить апельсинчик', '2018-09-03', 4, 12, 16, NULL, NULL);
INSERT INTO kz_request VALUES (13, 'иванов', '1234', 'такое-то предложение', '2018-08-21', 3, 12, 2, NULL, NULL);
INSERT INTO kz_request VALUES (19, 'фыв', '161681', 'ыфв', '2018-08-26', 3, 8, 2, NULL, NULL);
INSERT INTO kz_request VALUES (3, '1', '1465', '432', '2018-08-19', 3, 1, 2, 20, 'заявка была несложной для выполнения. хочу еще заявок');
INSERT INTO kz_request VALUES (10, 'фыв', '1234', 'фыв', '2018-08-21', 4, 8, 2, 20, 'yt ,ms');
INSERT INTO kz_request VALUES (9, 'фыв', '1234', 'фыв', '2018-08-21', 3, 8, 2, 20, '1233333333333333333333333');
INSERT INTO kz_request VALUES (6, 'wfef', '3', 'wer', '2018-08-19', 4, 5, 2, 20, 'tjrt jtr jrtj');
INSERT INTO kz_request VALUES (25, 'sdf', '324', '2 g qwerfgq 34', '2018-09-04', 4, 12, 16, 20, 'testeset');


--
-- Data for Name: kz_user; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO kz_user VALUES (2, 'test user', '123<test hash>', 'Test', 1);
INSERT INTO kz_user VALUES (7, '123', '123<test hash>', 'asd', 3);
INSERT INTO kz_user VALUES (15, 'ivan admin', '', 'Иванов Иван', 1);
INSERT INTO kz_user VALUES (17, 'oper2', '', 'оператор второй', 2);
INSERT INTO kz_user VALUES (18, 'alex', '', 'Алексей', 3);
INSERT INTO kz_user VALUES (16, 'oper', '123<test hash>', '123', 2);
INSERT INTO kz_user VALUES (20, 'worker', '123<test hash>', 'worker', 3);


--
-- Name: throughout_app_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('throughout_app_id_seq', 25, true);


--
-- Name: kz_category category_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY kz_category
    ADD CONSTRAINT category_pkey PRIMARY KEY (id);


--
-- Name: kz_request request_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY kz_request
    ADD CONSTRAINT request_pkey PRIMARY KEY (id);


--
-- Name: kz_user user_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY kz_user
    ADD CONSTRAINT user_pkey PRIMARY KEY (id);


--
-- Name: fki_req_exec_id; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX fki_req_exec_id ON kz_request USING btree (executor_id);


--
-- Name: kz_request req_cat_id; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY kz_request
    ADD CONSTRAINT req_cat_id FOREIGN KEY (cat_id) REFERENCES kz_category(id);


--
-- Name: kz_request req_exec_id; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY kz_request
    ADD CONSTRAINT req_exec_id FOREIGN KEY (executor_id) REFERENCES kz_user(id);


--
-- Name: kz_request req_user_id; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY kz_request
    ADD CONSTRAINT req_user_id FOREIGN KEY (user_id) REFERENCES kz_user(id);


--
-- PostgreSQL database dump complete
--

