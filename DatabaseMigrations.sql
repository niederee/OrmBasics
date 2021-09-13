REASSIGN OWNED BY orm_user TO postgres;  -- or some other trusted role
DROP OWNED BY orm_user;
drop role if exists orm_user;

CREATE ROLE orm_user WITH
	LOGIN
	NOSUPERUSER
	NOCREATEDB
	NOCREATEROLE
	INHERIT
	NOREPLICATION
	CONNECTION LIMIT -1
	PASSWORD 'password';


drop table if exists account_owner;
CREATE TABLE account_owner (
first_name varchar(50),
last_name varchar(50),
account_created_date date
);

insert into account_owner values('Mike', 'Smith', '2020-10-05');

grant select on account_owner to orm_user;