CREATE DATABASE TestDb
    WITH
    OWNER = postgres
    ENCODING = 'UTF8'
    LC_COLLATE = 'English_United States.1252'
    LC_CTYPE = 'English_United States.1252'
    LOCALE_PROVIDER = 'libc'
    TABLESPACE = pg_default
    CONNECTION LIMIT = -1
    IS_TEMPLATE = False;

Create Table Classes (name char(511));

Insert Into Classes (name) Values
                               ('Class1'),
                               ('Class2'),
                               ('Class3')
