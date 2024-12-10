create user DoAnNhom4 identified by thedung4;
grant create session to DoAnNhom4;
grant create table to DoAnNhom4;
alter user DoAnNhom4 quota 100m on users;

--Người dùng
CREATE TABLE Users (
    UserID NUMBER PRIMARY KEY,
    FullName VARCHAR2(30),
    Email VARCHAR2(30),
    PasswordHash VARCHAR2(255)
);

ALTER TABLE Users
ADD CONSTRAINT unique_email UNIQUE (Email);

CREATE TABLE UserDetails (
    UserID NUMBER PRIMARY KEY,
    Address VARCHAR2(100),
    PhoneNumber VARCHAR2(15),
    ManagerID NUMBER,
    
    FOREIGN KEY (ManagerID) REFERENCES UserDetails(UserID),
    FOREIGN KEY (UserID) REFERENCES Users(UserID)
);
select * from userdetails
--Tài khoản
CREATE TABLE Accounts (
    AccountID NUMBER PRIMARY KEY,
    UserID NUMBER,
    AccountNumber VARCHAR2(20),
    AccountType NVARCHAR2(30),
    Balance NUMBER,
    EncryptedDetails VARCHAR2(255),
    FOREIGN KEY (UserID) REFERENCES Users(UserID)
);

ALTER TABLE Accounts
MODIFY AccountType NVARCHAR2(30) DEFAULT 'Tiết kiệm'
ADD CONSTRAINT check_balance CHECK (Balance >= 0);


--Giao dịch
CREATE TABLE Transactions (
    TransactionID NUMBER PRIMARY KEY,
    AccountID NUMBER,
    TransactionType NVARCHAR2(20),
    Amount VARCHAR2(20),
    TransactionDate DATE,
    EncryptedDetails VARCHAR2(255),
    FOREIGN KEY (AccountID) REFERENCES Accounts(AccountID)
);

ALTER TABLE Transactions
MODIFY TransactionDate DATE DEFAULT SYSDATE
ADD CONSTRAINT check_amount CHECK (Amount > 0);


--Nhật ký
CREATE TABLE AuditLogs (
    LogID NUMBER PRIMARY KEY,
    UserID NUMBER,
    Action NVARCHAR2(20),
    Timestamp DATE,
    Details NVARCHAR2(255),
    FOREIGN KEY (UserID) REFERENCES Users(UserID)
);

ALTER TABLE AuditLogs
MODIFY Timestamp DATE DEFAULT SYSDATE;

INSERT INTO Users VALUES (1, 'Nguyen Van A', 'nva@example.com', Null);
INSERT INTO Users VALUES (2, 'Le Thi B', 'ltb@example.com', Null);
INSERT INTO Users VALUES (3, 'Tran Van C', 'tvc@example.com', Null);
INSERT INTO Users VALUES (4, 'Pham Thi D', 'ptd@example.com', Null);
INSERT INTO Users VALUES (5, 'Hoang Van E', 'hve@example.com', Null);

INSERT INTO UserDetails (UserID, Address, PhoneNumber, ManagerID) VALUES (1, '123 Main St', '0123456789', 1);
INSERT INTO UserDetails (UserID, Address, PhoneNumber, ManagerID) VALUES (2, '456 Elm St', '0987654321', 2);
INSERT INTO UserDetails (UserID, Address, PhoneNumber, ManagerID) VALUES (3, '789 Oak St', '0123987654', 3);
INSERT INTO UserDetails (UserID, Address, PhoneNumber, ManagerID) VALUES (4, '321 Pine St', '0987123456', 4);
INSERT INTO UserDetails (UserID, Address, PhoneNumber, ManagerID) VALUES (5, '654 Maple St', '0123678901', 5);

INSERT INTO Accounts VALUES (1, 1, '1234567890', N'Thanh toán', 1000000, Null);
INSERT INTO Accounts VALUES (2, 2, '2345678901', N'Tiết kiệm', 2000000, Null);
INSERT INTO Accounts VALUES (3, 3, '3456789012', N'Thanh toán', 1500000, Null);
INSERT INTO Accounts VALUES (4, 4, '4567890123', N'Tiết kiệm', 2500000, Null);
INSERT INTO Accounts VALUES (5, 5, '5678901234', N'Thanh toán', 3000000, Null);

INSERT INTO Transactions (AccountID,TransactionType,Amount,TransactionDate,EncryptedDetails) VALUES (1, N'Nạp tiền', 500000, TO_DATE('2023-08-01', 'YYYY-MM-DD'), Null);
INSERT INTO Transactions (AccountID,TransactionType,Amount,TransactionDate,EncryptedDetails) VALUES (2, N'Rút tiền', 300000, TO_DATE('2023-08-02', 'YYYY-MM-DD'), Null);
INSERT INTO Transactions (AccountID,TransactionType,Amount,TransactionDate,EncryptedDetails) VALUES (3, N'Chuyển tiền', 200000, TO_DATE('2023-08-03', 'YYYY-MM-DD'), Null);
INSERT INTO Transactions (AccountID,TransactionType,Amount,TransactionDate,EncryptedDetails) VALUES (4, N'Nạp tiền', 100000, TO_DATE('2023-08-04', 'YYYY-MM-DD'), Null);
INSERT INTO Transactions (AccountID,TransactionType,Amount,TransactionDate,EncryptedDetails) VALUES (5, N'Rút tiền', 400000, TO_DATE('2023-08-05', 'YYYY-MM-DD'), Null);

INSERT INTO AuditLogs VALUES (1, 1, N'Đăng nhập', TO_DATE('2023-08-01 08:00:00', 'YYYY-MM-DD HH24:MI:SS'), N'Đăng nhập thành công');
INSERT INTO AuditLogs VALUES (2, 2, N'Chuyển tiền', TO_DATE('2023-08-02 09:00:00', 'YYYY-MM-DD HH24:MI:SS'), N'Đã chuyển 300000 vào tài khoản số 3');
INSERT INTO AuditLogs VALUES (3, 3, N'Đăng nhập', TO_DATE('2023-08-03 10:00:00', 'YYYY-MM-DD HH24:MI:SS'), N'Đăng nhập thất bại - Mật khẩu sai');
INSERT INTO AuditLogs VALUES (4, 4, N'Rút tiền', TO_DATE('2023-08-04 11:00:00', 'YYYY-MM-DD HH24:MI:SS'), N'Đã rút 100000');
INSERT INTO AuditLogs VALUES (5, 5, N'Thay đổi thông tin', TO_DATE('2023-08-05 12:00:00', 'YYYY-MM-DD HH24:MI:SS'), N'Đã thay đổi email thành newemail@example.com');

SELECT * FROM transactions
INSERT INTO TRANSACTIONS (AccountID,TransactionType,Amount,TransactionDate,EncryptedDetails) VALUES ('2','Rut tien','SY4MMM02', TO_DATE('2024-10-01', 'YYYY-MM-DD'), '4MG GMYG GAM YGMAG Y02')
ALTER TABLE Transactions DROP CONSTRAINT check_amount;

COMMIT;

----------------17/9/2024------------------------
GRANT CREATE SEQUENCE TO DoAnNhom4;
GRANT CREATE TRIGGER TO DoAnNhom4;

CREATE SEQUENCE transaction_seq
START WITH 1
INCREMENT BY 1
NOCACHE;
/
CREATE OR REPLACE TRIGGER transaction_bir
BEFORE INSERT ON Transactions
FOR EACH ROW
BEGIN
    SELECT transaction_seq.NEXTVAL
    INTO :NEW.TransactionID
    FROM dual;

END;

drop sequence transaction_seq;
drop trigger transaction_bir;


------------------Thủ tục đăng ký user---------------------------------
CREATE USER create_user_account01 identified by 123;
GRANT CREATE PROCEDURE TO create_user_account01;
GRANT CREATE USER TO create_user_account01;
grant create session to create_user_account01 with admin option;
GRANT CREATE TABLE TO create_user_account01 with admin option;


CREATE OR REPLACE PROCEDURE CreateUser (username IN VARCHAR2, password IN VARCHAR2) IS
BEGIN
    EXECUTE IMMEDIATE 'CREATE USER ' || username || ' IDENTIFIED BY ' || password;
    EXECUTE IMMEDIATE 'GRANT CREATE SESSION TO ' || username;
    EXECUTE IMMEDIATE 'GRANT CREATE TABLE TO ' || username;
    EXECUTE IMMEDIATE 'ALTER USER ' || username ||' QUOTA 100M ON USERS';
END;

------------------Symmetric Encryption---------------------------------------
GRANT EXECUTE ON DBMS_CRYPTO TO DoAnNhom4;
GRANT CREATE PROCEDURE TO DoAnNhom4;

--HAM MA HOA DES 
/
CREATE OR REPLACE FUNCTION MaHoaDES (
    p_plaintext CHAR
) RETURN RAW IS
    i_key RAW(8) := UTL_RAW.cast_to_raw('Th3Jung4'); 
    i_encrypted RAW(2000);
BEGIN
    i_encrypted := DBMS_CRYPTO.ENCRYPT(
        src => UTL_RAW.cast_to_raw(p_plaintext), 
        typ => DBMS_CRYPTO.DES_CBC_PKCS5,      
        key => i_key
    );
    RETURN i_encrypted;
END;
/

-- Mã hóa dữ liệu trong bảng Accounts
UPDATE Accounts
SET EncryptedDetails = MaHoaDES(AccountNumber);

SELECT * FROM ACCOUNTS


--HAM GIAI MA DES
/
CREATE OR REPLACE FUNCTION GiaiMaDes (
    p_encrypted RAW
) RETURN VARCHAR2 IS
    i_key RAW(8) := UTL_RAW.cast_to_raw('Th3Jung4');
    i_decrypted RAW(2000);
BEGIN
    i_decrypted := DBMS_CRYPTO.DECRYPT(
        src => p_encrypted,
        typ => DBMS_CRYPTO.DES_CBC_PKCS5,
        key => i_key
    );
    RETURN UTL_RAW.cast_to_varchar2(i_decrypted);
END;
/

SELECT GiaiMaDes(EncryptedDetails) FROM Accounts;

--------------------Asymmetric Encryption---------------------------
-- HAM MA HOA RSA
CREATE OR REPLACE FUNCTION MaHoaRSA (
    plain_text IN VARCHAR2,
    public_key IN NUMBER,
    p_n IN NUMBER
) RETURN VARCHAR2 IS
    l_encrypted_text VARCHAR2(4000) := '';
    l_char_array VARCHAR2(256) := 'ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789,.!@#$%^&*()-_+=/';
    l_index NUMBER;
    l_number NUMBER;
BEGIN
    FOR i IN 1..LENGTH(plain_text) LOOP
        -- Lấy ký tự hiện tại
        l_index := INSTR(l_char_array, SUBSTR(plain_text, i, 1)) - 1;

        IF l_index >= 0 AND l_index < LENGTH(l_char_array) THEN
            l_number := MOD(POWER(l_index, public_key), p_n); -- Mã hóa
            
            -- Sử dụng phép MOD để đảm bảo l_number không vượt quá chiều dài của l_char_array
            l_number := MOD(l_number, LENGTH(l_char_array));

            l_encrypted_text := l_encrypted_text || SUBSTR(l_char_array, l_number + 1, 1); -- Thêm ký tự đã mã hóa
        END IF;
    END LOOP;

    RETURN l_encrypted_text;
END;
/
 
UPDATE USERS
SET FULLNAME = MaHoaRSA (FULLNAME,5,35)

SELECT * FROM USERS
 
--GiaiMa RSA

CREATE OR REPLACE FUNCTION GiaiMaRSA (
    encrypted_text IN VARCHAR2,
    private_key IN NUMBER,
    p_n IN NUMBER
) RETURN VARCHAR2 IS
    l_decrypted_text VARCHAR2(4000) := '';
    l_char_array VARCHAR2(256) := 'ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789,.!@#$%^&*()-_+=/';
    l_index NUMBER;
    l_number NUMBER;
BEGIN
    FOR i IN 1..LENGTH(encrypted_text) LOOP
        l_index := INSTR(l_char_array, SUBSTR(encrypted_text, i, 1)) - 1;

        IF l_index >= 0 AND l_index < LENGTH(l_char_array) THEN
            
            l_number := MOD(POWER(l_index, private_key), p_n);
            
           
            l_number := MOD(l_number, LENGTH(l_char_array));
            
            l_decrypted_text := l_decrypted_text || SUBSTR(l_char_array, l_number + 1, 1);
        ELSE
            l_decrypted_text := l_decrypted_text || '?'; -- Ký tự không hợp lệ
        END IF;
    END LOOP;

    RETURN l_decrypted_text;
END;
/



-- Giải mã dữ liệu trong bảng Transactions
SELECT USERID, GiaiMaRSA(FULLNAME,5 , 35) AS DecryptedEMAIL FROM USERS;


------ Test Case
SET SERVEROUTPUT ON;

DECLARE
    encrypted_value VARCHAR2(4000);
    decrypted_value VARCHAR2(4000);
BEGIN
    -- Ví dụ mã hóa
    encrypted_value := MaHoaRSA('THEANH12345', 5, 35);
    DBMS_OUTPUT.PUT_LINE('Encrypted Value: ' || encrypted_value);
    
    -- Giải mã
    decrypted_value := GiaiMaRSA(encrypted_value, 5, 35);
    DBMS_OUTPUT.PUT_LINE('Decrypted Value: ' || decrypted_value);
END;
/


-- Session
Select * from users
SELECT SID, SERIAL#, USERNAME, STATUS
FROM v$session;



create profile Check_profile limit sessions_per_user 1;
alter user DoAnNhom4 identified by thedung4 profile Check_profile
grant create session to DoAnNhom4

SELECT * FROM user_sys_privs WHERE username = 'DoAnNhom4';
SELECT SID, USERNAME, STATUS
FROM v$session
WHERE USERNAME = 'DoAnNhom4';  

--=================-----------

--Profile kiểm tra người dùng ( sai mk, phiên, tgian chờ)
ALTER SYSTEM SET RESOURCE_LIMIT = TRUE;
GRANT CREATE PROFILE TO DoAnNhom4;

CREATE PROFILE CHECK_LOGIN_USERS
LIMIT FAILED_LOGIN_ATTEMPTS 5
Sessions_per_user 1
Idle_time 30

SELECT DISTINCT * 
FROM dba_profiles
WHERE PROFILE = 'CHECK_LOGIN_USERS' AND LIMIT !='DEFAULT';




--TEST============================

CREATE USER TESTPROFILE IDENTIFIED BY TESTPROFILE
grant create session to TESTPROFILE;
grant create table to TESTPROFILE;
alter user TESTPROFILE quota 100m on users;
Alter user role1 PROFILE CHECK_LOGIN_USERS;

ALTER USER TESTPROFILE IDENTIFIED BY TESTPROFILE ACCOUNT UNLOCK


--==================================================================
--Thủ tục xem bảng của User


SET SERVEROUTPUT ON;
EXEC CheckUserTables('DOANNHOM4');

--================
--Nhóm quyền ROLE

CREATE ROLE CUSTOMERS_ROLE;
CREATE ROLE SELLERS_ROLE;

--gán role cho user 
create user role1 identified by role1;
grant create session to role1;
grant create table to role1;
alter user role1 quota 100m on users;
GRANT CUSTOMERS_ROLE TO role1
/*=================================================================================
===================================================================================*/

-- Bài tuần 12

--Thủ tục đổi mk

CREATE OR REPLACE PROCEDURE P_CHANGE_PASSWORD
(
    P_USERNAME IN VARCHAR2,
    P_PASS IN VARCHAR2
)
IS
BEGIN
    EXECUTE IMMEDIATE 'ALTER USER ' || P_USERNAME || ' IDENTIFIED BY ' || P_PASS;
END;
/

EXEC P_CHANGE_PASSWORD('role1','role123');


--Thủ tục mở khóa tài khoản

CREATE OR REPLACE PROCEDURE P_UNLOCK_ACCOUNT
(
    P_USERNAME IN VARCHAR2,
    P_PASS IN VARCHAR2
)
IS
BEGIN
    EXECUTE IMMEDIATE 'ALTER USER ' || P_USERNAME || ' IDENTIFIED BY ' || P_PASS || ' ACCOUNT UNLOCK ';
END;
/

EXEC P_UNLOCK_ACCOUNT('role1','role123');
ALTER USER role1 IDENTIFIED BY role123 ACCOUNT LOCK;

--THỦ TỤC GÁN TABLE SPACE 

--TẠO MỚI 1 TABLE SPACE
CREATE TABLESPACE CUSTOMERS
    DATAFILE 'D:\CUSTOMERS_DATA.dbf' SIZE 100M
    AUTOEXTEND ON NEXT 50M MAXSIZE UNLIMITED;
ALTER TABLE USERDETAILS MOVE TABLESPACE CUSTOMERS 

select tablespace_name from dba_tablespaces

--THỦ TỤC
CREATE OR REPLACE PROCEDURE P_GET_TABLESPACES (p_cursor OUT SYS_REFCURSOR)
IS
BEGIN
    OPEN p_cursor FOR
        SELECT tablespace_name FROM dba_tablespaces;
END;
/

DECLARE
    v_cursor SYS_REFCURSOR;
    v_tablespace_name VARCHAR2(100);
BEGIN
    P_GET_TABLESPACES(v_cursor); 
    LOOP
        FETCH v_cursor INTO v_tablespace_name;  
        EXIT WHEN v_cursor%NOTFOUND;  
        DBMS_OUTPUT.PUT_LINE(v_tablespace_name);  
    END LOOP;
    CLOSE v_cursor;  
END;
/


-==================

--THỦ TỤC CẤP QUOTA
CREATE OR REPLACE PROCEDURE SET_QUOTA
(
    v_username VARCHAR2,
    v_tablespace VARCHAR2, 
    v_quota VARCHAR2
)
IS
BEGIN
    -- Kiểm tra cú pháp và thực thi câu lệnh ALTER USER
    BEGIN
        EXECUTE IMMEDIATE 'ALTER USER ' || v_username || ' QUOTA ' || v_quota || ' ON ' || v_tablespace;
        DBMS_OUTPUT.PUT_LINE('QUOTA CHO USERS: ' || v_username || ' ĐÃ CẬP NHẬT TRONG TABLESPACE: ' || v_tablespace);
    END;
END;


EXEC SET_QUOTA ('ROLE1','USERS','10M');
--TEST UPDATE QUOTA
SELECT 
    username, 
    tablespace_name, 
    bytes / 1024 / 1024 AS "Used (MB)", -- Dung lượng đã sử dụng
    max_bytes / 1024 / 1024 AS "Max (MB)" -- Dung lượng tối đa
FROM 
    dba_ts_quotas;

SET SERVEROUTPUT ON
--======================
--THỦ TỤC PROFILE
CREATE OR REPLACE PROCEDURE P_GET_PROFILES (p_cursor OUT SYS_REFCURSOR)
IS
BEGIN
    OPEN p_cursor FOR
        SELECT DISTINCT PROFILE FROM dba_profiles;
END;
/

DECLARE
    v_cursor SYS_REFCURSOR;
    v_profiles_name VARCHAR2(100);
BEGIN
    P_GET_PROFILES(v_cursor); 
    LOOP
        FETCH v_cursor INTO v_profiles_name;  
        EXIT WHEN v_cursor%NOTFOUND;  
        DBMS_OUTPUT.PUT_LINE(v_profiles_name);  
    END LOOP;
    CLOSE v_cursor;  
END;
/



-=============================================
--THỦ TỤC CHECK TABLE

CREATE OR REPLACE PROCEDURE CHECK_USER_TABLES (p_username IN VARCHAR2) IS
BEGIN
       FOR rec IN (
        SELECT table_name
        FROM all_tables
        WHERE owner = p_username
    ) LOOP
        DBMS_OUTPUT.PUT_LINE(rec.table_name);
    END LOOP;
    IF SQL%ROWCOUNT = 0 THEN
        DBMS_OUTPUT.PUT_LINE('NGƯỜI DÙNG KHÔNG CÓ DỮ LIỆU: ' || p_username);
    END IF;
END ;
/
EXEC CHECK_USER_TABLES('DOANNHOM4');

-==========================
-- THỦ TỤC CHECK PRIVILEGE


CREATE OR REPLACE PROCEDURE CHECK_USER_PRIVILEGES (p_username IN VARCHAR2) IS
BEGIN
      FOR rec IN (
        SELECT privilege
        FROM dba_sys_privs
        WHERE grantee = p_username
    ) LOOP
        DBMS_OUTPUT.PUT_LINE(rec.privilege);
    END LOOP;

    
    IF SQL%ROWCOUNT = 0 THEN
        DBMS_OUTPUT.PUT_LINE('NGƯỜI DÙNG KHÔNG CÓ QUYỀN HỆ THỐNG: ' || p_username);
    END IF;
END;
EXEC CHECK_USER_PRIVILEGES('SYS');


---==============================================

--GET USER
CREATE OR REPLACE PROCEDURE P_GET_USERS
IS
    CURSOR user_cursor IS
        SELECT username
        FROM dba_users
        --WHERE created > SYSDATE - 10 OR username ='SYS'
        ORDER BY username;
    
    v_username VARCHAR2(30);
    
BEGIN
    OPEN user_cursor;
    LOOP
        FETCH user_cursor INTO v_username;
        EXIT WHEN user_cursor%NOTFOUND;
        DBMS_OUTPUT.PUT_LINE(v_username);
    END LOOP;
    CLOSE user_cursor;
END;
/

EXEC P_GET_USERS
SET SERVEROUTPUT ON

--===============================
--W13--

--===============================
CREATE OR REPLACE PACKAGE PKG_PHANQUYEN
AS
--1
PROCEDURE PRO_SELECT_PROCEDURE_USER(USEOWNER IN VARCHAR2, PRO_TYPE IN VARCHAR2, CUR OUT SYS_REFCURSOR);
--2
PROCEDURE PRO_SELECT_USER(CUR OUT SYS_REFCURSOR);
--3
PROCEDURE PRO_SELECT_ROLES(CUR OUT SYS_REFCURSOR);
--4
PROCEDURE PRO_USER_ROLES(USERNAME IN VARCHAR2, CUR OUT SYS_REFCURSOR);
--5
PROCEDURE PRO_USER_ROLES_CHECK(USERNAME IN VARCHAR2, ROLES IN VARCHAR2, COUT OUT NUMBER);
--6
PROCEDURE PRO_SELECT_TABLE(USENAME IN VARCHAR2, CUR OUT SYS_REFCURSOR);
--7
PROCEDURE PRO_SELECT_GRANT(USERNAME IN VARCHAR2, USERSCHEMA IN VARCHAR2, TABLENAME IN VARCHAR2, CUR OUT SYS_REFCURSOR);
--8
PROCEDURE PRO_SELECT_GRANT_USER(USERNAME IN VARCHAR2, CUR OUT SYS_REFCURSOR);
--9
PROCEDURE PRO_GRANT_REVOKE(USERNAME IN VARCHAR2, SCHEMA_USER IN VARCHAR2, PRO_TAB IN VARCHAR2, TYPE_PRO IN VARCHAR2, DK IN NUMBER); 
--10
PROCEDURE PRO_GRANT_REVOKE_ROLES(USERNAME IN VARCHAR2, ROLES IN VARCHAR2, DK IN NUMBER);

END;

CREATE OR REPLACE PACKAGE BODY PKG_PHANQUYEN
AS
    PROCEDURE PRO_SELECT_PROCEDURE_USER (USEOWNER IN VARCHAR2, PRO_TYPE IN VARCHAR2, CUR OUT SYS_REFCURSOR)
    IS
    BEGIN
        OPEN CUR FOR
            SELECT OBJECT_NAME FROM DBA_PROCEDURES WHERE OWNER = USEOWNER AND OBJECT_TYPE = PRO_TYPE;
    END PRO_SELECT_PROCEDURE_USER;
    
    --2--
    PROCEDURE PRO_SELECT_USER(CUR OUT SYS_REFCURSOR) IS
    BEGIN
        OPEN CUR FOR
            SELECT USERNAME 
            FROM DBA_USERS 
            WHERE ACCOUNT_STATUS = 'OPEN';
    END PRO_SELECT_USER;
    
        --3--
    PROCEDURE PRO_SELECT_ROLES(CUR OUT SYS_REFCURSOR) IS
    BEGIN
        OPEN CUR FOR
            SELECT ROLE 
            FROM DBA_ROLES;
    END PRO_SELECT_ROLES;
    
    
    --4--
    PROCEDURE PRO_USER_ROLES(USERNAME IN VARCHAR2, CUR OUT SYS_REFCURSOR) IS
    BEGIN
        OPEN CUR FOR
            SELECT GRANTED_ROLE 
            FROM DBA_ROLE_PRIVS 
            WHERE GRANTEE = USERNAME;
    END PRO_USER_ROLES;
    
    --5--
    PROCEDURE PRO_USER_ROLES_CHECK(USERNAME IN VARCHAR2, ROLES IN VARCHAR2, COUT OUT NUMBER) IS
    BEGIN
        SELECT COUNT(*) INTO COUT
        FROM DBA_ROLE_PRIVS 
        WHERE GRANTEE = USERNAME 
        AND GRANTED_ROLE = ROLES;
    END PRO_USER_ROLES_CHECK;
    
     
    --6--
    PROCEDURE PRO_SELECT_TABLE(USENAME IN VARCHAR2, CUR OUT SYS_REFCURSOR) IS
    BEGIN
        OPEN CUR FOR
            SELECT TABLE_NAME 
            FROM DBA_ALL_TABLES 
            WHERE OWNER = USENAME;
    END PRO_SELECT_TABLE;
    
    --7
    PROCEDURE PRO_SELECT_GRANT(USERNAME IN VARCHAR2, USERSCHEMA IN VARCHAR2, TABLENAME IN VARCHAR2, CUR OUT SYS_REFCURSOR) IS
    BEGIN
        OPEN CUR FOR
            SELECT PRIVILEGE 
            FROM DBA_TAB_PRIVS 
            WHERE GRANTEE ='ROLE1'-- USERNAME 
            AND TABLE_NAME = 'ACCOUNTS'--TABLENAME 
            AND OWNER = 'DOANNHOM4'--USERSCHEMA;
    END PRO_SELECT_GRANT;
     
    --8--
    PROCEDURE PRO_SELECT_GRANT_USER(USERNAME IN VARCHAR2, CUR OUT SYS_REFCURSOR) IS
    BEGIN
        OPEN CUR FOR
            SELECT TABLE_NAME, TYPE, OWNER 
            FROM DBA_TAB_PRIVS 
            WHERE GRANTEE = USERNAME 
            AND TYPE IN ('PROCEDURE', 'FUNCTION', 'PACKAGE');
    END PRO_SELECT_GRANT_USER;
    
    --9--
    PROCEDURE PRO_GRANT_REVOKE (USERNAME IN VARCHAR2, SCHEMA_USER IN VARCHAR2, PRO_TAB IN VARCHAR2, TYPE_PRO IN VARCHAR2, DK IN NUMBER) IS
    BEGIN
        IF DK = 1 THEN
            EXECUTE IMMEDIATE 'GRANT ' || TYPE_PRO || ' ON ' || SCHEMA_USER || '.' || PRO_TAB || ' TO ' || USERNAME;
        ELSE
            EXECUTE IMMEDIATE 'REVOKE ' || TYPE_PRO || ' ON ' || SCHEMA_USER || '.' || PRO_TAB || ' FROM ' || USERNAME;
        END IF;
    END PRO_GRANT_REVOKE;
    
    --10--
    PROCEDURE PRO_GRANT_REVOKE_ROLES (USERNAME IN VARCHAR2, ROLES IN VARCHAR2, DK IN NUMBER) IS
    BEGIN
        IF DK = 1 THEN
            EXECUTE IMMEDIATE 'GRANT ' || ROLES || ' TO ' || USERNAME;
        ELSE
            EXECUTE IMMEDIATE 'REVOKE ' || ROLES || ' FROM ' || USERNAME;
        END IF;
    END PRO_GRANT_REVOKE_ROLES;
        
END;    


--==================================================
-- Thực thi các thủ tục , hàm trong package

SET SERVEROUTPUT ON
--
--Kiểm tra các thủ tục của User sở hữu
--1
DECLARE
  CUR SYS_REFCURSOR;
BEGIN

  PKG_PHANQUYEN.PRO_SELECT_PROCEDURE_USER('DOANNHOM4', 'PROCEDURE', CUR);

  LOOP
    FETCH CUR INTO :object_name;
    EXIT WHEN CUR%NOTFOUND;
    DBMS_OUTPUT.PUT_LINE('Object Name: ' || :object_name);
  END LOOP;
  CLOSE CUR;
END;
/

--2
--Láy tất cả User có trạng thái OPEN
DECLARE
  CUR SYS_REFCURSOR;
BEGIN
  PKG_PHANQUYEN.PRO_SELECT_USER(CUR);
  LOOP
    FETCH CUR INTO :username;
    EXIT WHEN CUR%NOTFOUND;
    DBMS_OUTPUT.PUT_LINE('USER: ' || :username);
  END LOOP;
  CLOSE CUR;
END;
/
alter user JOHN account lock;
alter user JOHN account unlock;



--Lấy nhóm quyền có trong csdl
--3
DECLARE
  CUR SYS_REFCURSOR;
BEGIN
  PKG_PHANQUYEN.PRO_SELECT_ROLES(CUR);

  LOOP
    FETCH CUR INTO :role;
    EXIT WHEN CUR%NOTFOUND;
    DBMS_OUTPUT.PUT_LINE('Role: ' || :role);
  END LOOP;
  CLOSE CUR;
END;
/

--Roles người dùng được cấp 
DECLARE
  CUR SYS_REFCURSOR;
BEGIN

  sys.PKG_PHANQUYEN.PRO_USER_ROLES('DOANNHOM4', CUR);
  LOOP
    FETCH CUR INTO :granted_role;
    EXIT WHEN CUR%NOTFOUND;
    DBMS_OUTPUT.PUT_LINE('Granted Role: ' || :granted_role);
  END LOOP;
  CLOSE CUR;
END;
/

--KTRA xem có quyền trong Role không, Có thì trả về 1
DECLARE
  COUT NUMBER;
BEGIN

  PKG_PHANQUYEN.PRO_USER_ROLES_CHECK('DOANNHOM4', 'DBA', COUT);

  DBMS_OUTPUT.PUT_LINE('Count: ' || COUT);
END;
/

-- thông tin các bảng mà user sở hữu
--6
DECLARE
  CUR SYS_REFCURSOR;
BEGIN
  PKG_PHANQUYEN.PRO_SELECT_TABLE('DOANNHOM4', CUR);
  LOOP
    FETCH CUR INTO :table_name;
    EXIT WHEN CUR%NOTFOUND;
    DBMS_OUTPUT.PUT_LINE('Table: ' || :table_name);
  END LOOP;
  CLOSE CUR;
END;
/

--Quyền sử dụng bảng của người dùng
--7
DECLARE
  CUR SYS_REFCURSOR;
BEGIN

  PKG_PHANQUYEN.PRO_SELECT_GRANT('ROLE1', 'DOANNHOM4', 'ACCOUNTS', CUR);  

  LOOP
    FETCH CUR INTO :privilege;
    EXIT WHEN CUR%NOTFOUND;
    DBMS_OUTPUT.PUT_LINE('Privilege: ' || :privilege);
  END LOOP;
  CLOSE CUR;
END;
/

--8
DECLARE
  CUR SYS_REFCURSOR;
BEGIN
  PKG_PHANQUYEN.PRO_SELECT_GRANT_USER('DOANNHOM4', CUR);
  LOOP
    FETCH CUR INTO :table_name, :type, :owner;
    EXIT WHEN CUR%NOTFOUND;
    DBMS_OUTPUT.PUT_LINE('Table: ' || :table_name || ', Type: ' || :type || ', Owner: ' || :owner);
  END LOOP;
  CLOSE CUR;
END;
/

--Cấp /hủy quyền cho user thao tác trên bảng của user
--9
BEGIN
--thực thi 1 trong 2 ; đừng xóa nha
  PKG_PHANQUYEN.PRO_GRANT_REVOKE('ROLE1', 'DOANNHOM4', 'ACCOUNTS', 'SELECT', 1);  
  --PKG_PHANQUYEN.PRO_GRANT_REVOKE('ROLE1', 'DOANNHOM4', 'ACCOUNTS', 'SELECT', 0);
END;
/
SELECT * FROM DOANNHOM4.ACCOUNTS;
alter user role1 identified by 1;

-- Cấp/hủy Roles của user
BEGIN
  --PKG_PHANQUYEN.PRO_GRANT_REVOKE_ROLES('ROLE1', 'SELLERS_ROLE', 1);

  PKG_PHANQUYEN.PRO_GRANT_REVOKE_ROLES('ROLE1', 'SELLERS_ROLE', 0);
END;
/

SELECT GRANTEE, GRANTED_ROLE
FROM DBA_ROLE_PRIVS
WHERE GRANTEE = 'ROLE1';

--====
SELECT * FROM DOANNHOM4.ACCOUNTS;
UPDATE DOANNHOM4.ACCOUNTS 
SET ACCOUNTNUMBER = '097728193'
WHERE ACCOUNTID=6;


--============
CREATE ROLE GRANT_PRIVILEGE_USER;