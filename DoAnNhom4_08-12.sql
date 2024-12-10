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
    Amount VARCHAR2(2000),
    TransactionDate DATE,
    EncryptedDetails VARCHAR2(2000),
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

CREATE OR REPLACE FUNCTION SHIFTCHAR(C CHAR, K NUMBER)
RETURN CHAR
IS
    T NUMBER(2);
    CHAR_CODE NUMBER(3);
    OFFSET NUMBER;
BEGIN
    CHAR_CODE := ASCII(C);

    -- Xử lý ký tự chữ cái (A-Z)
    IF CHAR_CODE BETWEEN ASCII('A') AND ASCII('Z') THEN
        T := ASCII('A');
        OFFSET := (ASCII(C) - T + K) MOD 26;
        IF OFFSET < 0 THEN OFFSET := OFFSET + 26; END IF;
        RETURN CHR(T + OFFSET);

    -- Xử lý ký tự chữ cái thường (a-z)
    ELSIF CHAR_CODE BETWEEN ASCII('a') AND ASCII('z') THEN
        T := ASCII('a');
        OFFSET := (ASCII(C) - T + K) MOD 26;
        IF OFFSET < 0 THEN OFFSET := OFFSET + 26; END IF;
        RETURN CHR(T + OFFSET);

    -- Xử lý ký tự số (0-9)
    ELSIF CHAR_CODE BETWEEN ASCII('0') AND ASCII('9') THEN
        T := ASCII('0');
        OFFSET := (ASCII(C) - T + K) MOD 10;
        IF OFFSET < 0 THEN OFFSET := OFFSET + 10; END IF;
        RETURN CHR(T + OFFSET);

    ELSE
        RETURN NULL; -- Không xử lý các ký tự khác
    END IF;
END;
/
CREATE OR REPLACE FUNCTION ENCRYPTCAESARCIPHER(STR VARCHAR, K NUMBER)
RETURN VARCHAR
AS
    I NUMBER(2);
    LEN NUMBER(5);
    KQ VARCHAR(100):='';
    PLAINTEXT VARCHAR(250);
BEGIN
    PLAINTEXT := UPPER(STR);  -- Chuyển đổi toàn bộ chuỗi thành chữ hoa
    LEN := LENGTH(STR);
    FOR I IN 1..LEN LOOP
        KQ := KQ || SHIFTCHAR(SUBSTR(PLAINTEXT, I, 1), K);  -- Dịch từng ký tự theo khóa K
    END LOOP;
    RETURN KQ;  -- Trả về kết quả mã hóa
END;
/
CREATE OR REPLACE FUNCTION DECRYPTCAESARCIPHER(STR VARCHAR, K NUMBER)
RETURN VARCHAR
AS
BEGIN
    -- Đảo ngược bằng cách sử dụng khóa K âm
    RETURN ENCRYPTCAESARCIPHER(STR, -K);
END;
/
-- Mã hóa
DECLARE
    encrypted_text VARCHAR2(100);
BEGIN
    encrypted_text := ENCRYPTCAESARCIPHER('Hello123', 5);  -- Mã hóa chuỗi 'Hello123' với khóa 5
    DBMS_OUTPUT.PUT_LINE('Encrypted: ' || encrypted_text);
END;
/
-- Giải mã
DECLARE
    decrypted_text VARCHAR2(100);
BEGIN
    decrypted_text := DECRYPTCAESARCIPHER('MJQQT678', 5);  -- Giải mã chuỗi 'MJQQT678' với khóa 5
    DBMS_OUTPUT.PUT_LINE('Decrypted: ' || decrypted_text);
END;


--GRANT EXECUTE ON DBMS_CRYPTO TO DOANNHOM4;
--GRANT CREATE procedure TO DOANNHOM4;
--GRANT EXECUTE ON UTL_RAW TO DOANNHOM4;
--GRANT EXECUTE ON UTL_ENCODE TO DOANNHOM4;


----HAM MA HOA DES 
--/
--CREATE OR REPLACE FUNCTION MaHoaDES (
--    p_plaintext VARCHAR2
--) RETURN VARCHAR2 IS
--    i_key RAW(8) := UTL_RAW.cast_to_raw('Th3Jung4'); 
--    i_encrypted RAW(2000);
--BEGIN
--    i_encrypted := DBMS_CRYPTO.ENCRYPT(
--        src => UTL_RAW.cast_to_raw(p_plaintext), 
--        typ => DBMS_CRYPTO.DES_CBC_PKCS5,      
--        key => i_key
--    );
--    RETURN UTL_RAW.cast_to_varchar2(UTL_ENCODE.BASE64_ENCODE(i_encrypted));
--END;
--/
--
----HAM GIAI MA DES
--CREATE OR REPLACE FUNCTION GiaiMaDES (
--    p_encrypted VARCHAR2
--) RETURN VARCHAR2 IS
--    i_key RAW(8) := UTL_RAW.cast_to_raw('Th3Jung4');
--    i_encrypted RAW(2000);
--    i_decrypted RAW(2000);
--BEGIN
--    i_encrypted := UTL_ENCODE.BASE64_DECODE(UTL_RAW.cast_to_raw(p_encrypted));
--
--    i_decrypted := DBMS_CRYPTO.DECRYPT(
--        src => i_encrypted,
--        typ => DBMS_CRYPTO.DES_CBC_PKCS5,
--        key => i_key
--    );
--    RETURN UTL_RAW.cast_to_varchar2(i_decrypted);
--END;
--/
--
--
---- Test Mã hóa Des
--SET SERVEROUTPUT ON;
--DECLARE
--    cipher VARCHAR2(2000);
--    plain VARCHAR2(1000);
--BEGIN
--    cipher := MaHoaDES('test');
--    plain := GiaiMaDES(cipher);
--
--    DBMS_OUTPUT.PUT_LINE('Cipher (Base64): ' || cipher);
--    DBMS_OUTPUT.PUT_LINE('Plain: ' || plain);
--END;
--/


--------------------Asymmetric Encryption---------------------------
--+++++++++++++++++++++++++++++++++++++++++++++++++++++++
--START : PLEASE DO NOT MAKE ANY CHANGES TO THIS SECTION.
--+++++++++++++++++++++++++++++++++++++++++++++++++++++++
SET define on
SET echo on
SET linesize 2048
SET escape off
SET timing on
SET trimspool on
SET serveroutput on
--++++++++++++++++++++++++++++++++++++++++++++++++++++++++
--END : PLEASE DO NOT MAKE ANY CHANGES TO THIS SECTION.							 
--++++++++++++++++++++++++++++++++++++++++++++++++++++++++

CREATE OR REPLACE PACKAGE CRYPTO AS 
FUNCTION RSA_ENCRYPT(PLAIN_TEXT VARCHAR2,PRIVATE_KEY VARCHAR2) RETURN VARCHAR2
AS
LANGUAGE JAVA NAME 'com/dishtavar/crypto4ora/RSAUtil.encrypt (java.lang.String,java.lang.String) return java.lang.String';


FUNCTION RSA_DECRYPT(ENCRYPTED_TEXT VARCHAR2,PUBLIC_KEY VARCHAR2) RETURN VARCHAR2
AS
LANGUAGE JAVA NAME 'com/dishtavar/crypto4ora/RSAUtil.decrypt (java.lang.String,java.lang.String) return java.lang.String';


FUNCTION RSA_SIGN(HASH_MESSAGE VARCHAR2,PUBLIC_KEY VARCHAR2) RETURN VARCHAR2
AS
LANGUAGE JAVA NAME 'com/dishtavar/crypto4ora/RSAUtil.sign (java.lang.String,java.lang.String) return java.lang.String';


FUNCTION RSA_VERIFY(PLAIN_HASH VARCHAR2,SIGNNED_HASH VARCHAR2,PRIVATE_KEY VARCHAR2) RETURN BOOLEAN
AS
LANGUAGE JAVA NAME 'com/dishtavar/crypto4ora/RSAUtil.verify (java.lang.String,java.lang.String,java.lang.String) return java.lang.Boolean';

FUNCTION RSA_GENERATE_KEYS(KEY_SIZE NUMBER) RETURN VARCHAR2
AS
LANGUAGE JAVA NAME 'com/dishtavar/crypto4ora/GenerateKey.generateRSAKeys (java.lang.Integer) return java.lang.String';

END CRYPTO;
/

--Test Mã hóa RSA
SELECT CRYPTO.RSA_GENERATE_KEYS(KEY_SIZE => 1024) FROM DUAL;

SELECT CRYPTO.RSA_ENCRYPT('test', 'MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQDfg6cMhju14dlXe9FXdX+qWL4wWWBP+t7djskM3B1ys7iHBTdmyztFTlaxXcGAJwU2AmeI5p3YmsbAIlusvLEWZVIwzp4k/CoWd4DX8rPPrUKTx6vnVo/sz9qq8F5fOLCFOfVLfyEUCwlrhWprEZyMBKgIuhXxwBvrS4Enyp6zvwIDAQAB')
FROM DUAL;

SELECT CRYPTO.RSA_DECRYPT('XV9bbDGx9JWZXVnnWcTIekHAuT6U5aitsOLfWS9tHYaa1GqUJF0vZGO6zCrLzOOrJe0ZhUl4aKxF4uDfevU4UqhQGyMFn+src21IgCUtd4LWbq4YDO2mM4oKWKIClU+OD7da3bdU1XQRsHwu5C6omW4/d4dDt9TkMyMZEje8vac=',
                          'MIICdwIBADANBgkqhkiG9w0BAQEFAASCAmEwggJdAgEAAoGBAN+DpwyGO7Xh2Vd70Vd1f6pYvjBZYE/63t2OyQzcHXKzuIcFN2bLO0VOVrFdwYAnBTYCZ4jmndiaxsAiW6y8sRZlUjDOniT8KhZ3gNfys8+tQpPHq+dWj+zP2qrwXl84sIU59Ut/IRQLCWuFamsRnIwEqAi6FfHAG+tLgSfKnrO/AgMBAAECgYEAmFcm+EZVVDZG6HWfzThsdzJdDp8cIecfF2tGZNlxyMftsTlA9XL3RtmKBQGd7TarOpCQ+KIWW5fCdxnz2dwR5ae5l1JZgkjIWJUmdFwSZjUIel9J3ADNCyl0GdEzAQfWfUS3eSelEW2LE6iTE4D8pub7UOClpsb2rpirGO8LcQECQQDxnCm54F/2pUAcKpBJmnSqn/G6LHtTmeRVsa0HM8LDL6CZVXE6Bw4A1M9zHX0eJKDu3KEh3hM2yuQVcTQEKXffAkEA7NOVQqG6pX7HAvfGlH8yZBh+BDZAGHVbZ4bNyNCkVCx6YEVENcCWloZObXTRaNGrSa7u2bj2JTIvfltpQIHAIQJASs0wVe3TiAcNXCsJVOBO8mxmaF9RJ0bj3GwPx8UMrWVXcWF0lqSMf1FjkJ42mFh6wrjn4hZhGHukNcdAdXFpPQJBAI9BUYlzwS54qLNf5AxRgM7RjfDITC8/ViIihfpSUwTjvsbbP25wZ+b3qRtGzaFlKwKwQaUL4EERwW7ipqExm2ECQGZGqBocODdzTGYHXZybLG5fSZ3DdTZUNdpF9bjBSiabWXQkbwO6csNtQqO1h1W3J3VaTnaa1y7eQuTgvaqJlBc=')
FROM DUAL;

---- HAM MA HOA RSA
--CREATE OR REPLACE FUNCTION MaHoaRSA (
--    plain_text IN VARCHAR2,
--    public_key IN NUMBER,
--    p_n IN NUMBER
--) RETURN VARCHAR2 IS
--    l_encrypted_text VARCHAR2(4000) := '';
--    l_char_array VARCHAR2(256) := 'ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789,.!@#$%^&*()-_+=/';
--    l_index NUMBER;
--    l_number NUMBER;
--BEGIN
--    FOR i IN 1..LENGTH(plain_text) LOOP
--        -- Lấy ký tự hiện tại
--        l_index := INSTR(l_char_array, SUBSTR(plain_text, i, 1)) - 1;
--
--        IF l_index >= 0 AND l_index < LENGTH(l_char_array) THEN
--            l_number := MOD(POWER(l_index, public_key), p_n); -- Mã hóa
--            
--            -- Sử dụng phép MOD để đảm bảo l_number không vượt quá chiều dài của l_char_array
--            l_number := MOD(l_number, LENGTH(l_char_array));
--
--            l_encrypted_text := l_encrypted_text || SUBSTR(l_char_array, l_number + 1, 1); -- Thêm ký tự đã mã hóa
--        END IF;
--    END LOOP;
--
--    RETURN l_encrypted_text;
--END;
--/
-- 
----GiaiMa RSA
--
--CREATE OR REPLACE FUNCTION GiaiMaRSA (
--    encrypted_text IN VARCHAR2,
--    private_key IN NUMBER,
--    p_n IN NUMBER
--) RETURN VARCHAR2 IS
--    l_decrypted_text VARCHAR2(4000) := '';
--    l_char_array VARCHAR2(256) := 'ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789,.!@#$%^&*()-_+=/';
--    l_index NUMBER;
--    l_number NUMBER;
--BEGIN
--    FOR i IN 1..LENGTH(encrypted_text) LOOP
--        l_index := INSTR(l_char_array, SUBSTR(encrypted_text, i, 1)) - 1;
--
--        IF l_index >= 0 AND l_index < LENGTH(l_char_array) THEN
--            
--            l_number := MOD(POWER(l_index, private_key), p_n);
--            
--           
--            l_number := MOD(l_number, LENGTH(l_char_array));
--            
--            l_decrypted_text := l_decrypted_text || SUBSTR(l_char_array, l_number + 1, 1);
--        ELSE
--            l_decrypted_text := l_decrypted_text || '?'; -- Ký tự không hợp lệ
--        END IF;
--    END LOOP;
--
--    RETURN l_decrypted_text;
--END;
--/
--
-------- Test Case
--SET SERVEROUTPUT ON;
--
--DECLARE
--    encrypted_value VARCHAR2(4000);
--    decrypted_value VARCHAR2(4000);
--BEGIN
--    -- Ví dụ mã hóa
--    encrypted_value := MaHoaRSA('THEANH12345', 5, 35);
--    DBMS_OUTPUT.PUT_LINE('Encrypted Value: ' || encrypted_value);
--    
--    -- Giải mã
--    decrypted_value := GiaiMaRSA(encrypted_value, 5, 35);
--    DBMS_OUTPUT.PUT_LINE('Decrypted Value: ' || decrypted_value);
--END;
--/

----------------------------- Session ---------------------------------------

-- Cấp quyền KILL SESSION
CREATE USER KillSession IDENTIFIED BY 123; 

GRANT CREATE SESSION TO KillSession; 
GRANT CREATE PROCEDURE TO KillSession;
GRANT SELECT ON v_$session TO KillSession;
GRANT ALTER SESSION TO KillSession;
GRANT ALTER SYSTEM TO KillSession;

GRANT EXECUTE ON kill_all_sessions_other TO KillSession;

-- Thủ tục Kill Session

CREATE OR REPLACE PROCEDURE kill_all_sessions_other(p_username IN VARCHAR2) AS
BEGIN
    FOR rec IN (SELECT SID, SERIAL#
                FROM v$session
                WHERE USERNAME = UPPER(p_username) 
                  AND SID != SYS_CONTEXT('USERENV', 'SESSIONID')) LOOP
        BEGIN
            -- Sử dụng IMMEDIATE để đảm bảo session bị ngắt ngay lập tức
            EXECUTE IMMEDIATE 'ALTER SYSTEM KILL SESSION ''' || rec.SID || ',' || rec.SERIAL# || ''' IMMEDIATE';
            DBMS_OUTPUT.PUT_LINE('Ngắt kết nối phiên: ' || rec.SID || ', ' || rec.SERIAL#);

            -- Giải phóng các tài nguyên liên quan (nếu cần thiết)
            EXECUTE IMMEDIATE 'ALTER SYSTEM DISCONNECT SESSION ''' || rec.SID || ',' || rec.SERIAL# || ''' POST_TRANSACTION';
        EXCEPTION
            WHEN OTHERS THEN
                -- Ghi log lỗi nếu không thể ngắt session
                DBMS_OUTPUT.PUT_LINE('Không thể ngắt phiên: ' || rec.SID || ', ' || rec.SERIAL# || '. Lỗi: ' || SQLERRM);
        END;
    END LOOP;

    -- Kiểm tra nếu không có phiên nào để ngắt
    IF SQL%ROWCOUNT = 0 THEN
        DBMS_OUTPUT.PUT_LINE('Không có phiên nào để ngắt cho người dùng: ' || p_username);
    END IF;

EXCEPTION
    WHEN OTHERS THEN
        -- Bắt và xử lý bất kỳ lỗi nào xảy ra trong quá trình ngắt session
        RAISE_APPLICATION_ERROR(-20001, 'Lỗi khi ngắt session: ' || SQLERRM);
END;
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

ALTER PROFILE CHECK_LOGIN_USERS
LIMIT FAILED_LOGIN_ATTEMPTS 2
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
CREATE ROLE DATAENTRY;
CREATE ROLE SUPERVISOR;
CREATE ROLE MANAGEMENT;
CREATE USER JOE IDENTIFIED BY JOE QUOTA 10M ON USERS PROFILE CHECK_LOGIN_USERS;
CREATE USER FRED IDENTIFIED BY FRED QUOTA 10M ON USERS PROFILE CHECK_LOGIN_USERS;
CREATE USER AMY IDENTIFIED BY AMY QUOTA 10M ON USERS PROFILE CHECK_LOGIN_USERS;
CREATE USER BETH IDENTIFIED BY BETH QUOTA 10M ON USERS PROFILE CHECK_LOGIN_USERS;
GRANT DATAENTRY TO JOHN,JOE;
GRANT SUPERVISOR TO FRED;
GRANT MANAGEMENT TO AMY,BETH;

----------------------------Standard Auditing--------------------------------
-- Tạo user audit_test
CREATE USER audit_test IDENTIFIED BY 123
DEFAULT TABLESPACE users
TEMPORARY TABLESPACE temp
QUOTA UNLIMITED ON users;

-- gán quyền
GRANT CONNECT TO audit_test;
GRANT CREATE SESSION TO audit_test;
GRANT CREATE TABLE TO audit_test;
GRANT SELECT ANY TABLE TO audit_test;
GRANT UPDATE ANY TABLE TO audit_test;
GRANT DELETE ANY TABLE TO audit_test;
GRANT INSERT ANY TABLE TO audit_test;
GRANT CREATE PROCEDURE TO audit_test;

-- Thực hiện giám sát user audit_test 
AUDIT Select TABLE BY audit_test BY ACCESS; 
AUDIT Insert TABLE BY audit_test BY ACCESS; 
AUDIT update TABLE BY audit_test BY ACCESS; 
AUDIT Delete TABLE BY audit_test BY ACCESS;

-- Thủ tục SELECT để kiểm tra
CREATE OR REPLACE PROCEDURE PRO_SYS_SELECT_USER_DML(CUR OUT SYS_REFCURSOR)
IS
BEGIN
    OPEN CUR FOR
        SELECT USERNAME FROM DBA_USERS ORDER BY USERNAME ASC;
END;
/
--USER SYS : Thủ tục tạo giám sát
CREATE OR REPLACE PROCEDURE PRO_CREATE_AUDIT(
    P_STATEMENT IN VARCHAR2, P_USERNAME IN VARCHAR2)
AS
    V_AUDIT_COMMAND VARCHAR2(400);
BEGIN
    -- TAO CAU LENH AUDIT
    V_AUDIT_COMMAND := 'AUDIT ' || P_STATEMENT || ' BY ' || P_USERNAME;
    -- THUC THI CAU LENH AUDIT
    EXECUTE IMMEDIATE V_AUDIT_COMMAND;
    -- IN THONG BAO THANH CONG
    DBMS_OUTPUT.PUT_LINE('AUDIT COMMAND EXECUTED SUCCESSFULLY.');
EXCEPTION
    WHEN OTHERS THEN
        -- XU LY LOI
        DBMS_OUTPUT.PUT_LINE('ERROR EXECUTING AUDIT COMMAND: ' || SQLERRM);
        -- NEM LAI LOI DE C# CO THE BAT DUOC
        RAISE;
END;
/

--USER SYS : Thủ tục xóa giám xát
CREATE OR REPLACE PROCEDURE PRO_DROP_AUDIT(
    P_STATEMENT IN VARCHAR2, P_USERNAME IN VARCHAR2)
AS
    V_AUDIT_COMMAND VARCHAR2(400);
BEGIN
    -- TAO CAU LENH AUDIT
    V_AUDIT_COMMAND := 'NOAUDIT ' || P_STATEMENT || ' BY ' || P_USERNAME;
    -- THUC THI CAU LENH AUDIT
    EXECUTE IMMEDIATE V_AUDIT_COMMAND;
    -- IN THONG BAO THANH CONG
    DBMS_OUTPUT.PUT_LINE('AUDIT COMMAND EXECUTED SUCCESSFULLY.');
EXCEPTION
    WHEN OTHERS THEN
        -- XU LY LOI
        DBMS_OUTPUT.PUT_LINE('ERROR EXECUTING AUDIT COMMAND: ' || SQLERRM);
        -- NEM LAI LOI DE C# CO THE BAT DUOC
        RAISE;
END;
/

--USER SYS: Thủ tục kiểm tra USER bị giám sát hoạt động nào
CREATE OR REPLACE PROCEDURE PRO_SELECT_STMT_AUDIT_OPTS
(USERNAME IN VARCHAR2, CUR OUT SYS_REFCURSOR)
IS
BEGIN
    OPEN CUR FOR
        SELECT * FROM DBA_STMT_AUDIT_OPTS
        WHERE USER_NAME = USERNAME;
END;
/

CREATE TABLE TAB(
    ID VARCHAR(10)
);

INSERT INTO TAB
VALUES ('ID01');
INSERT INTO TAB
VALUES ('ID02');

UPDATE AUDIT_TEST.TAB SET ID='ID1'
WHERE ID='ID01';

SELECT * FROM TAB;

DELETE TAB;

DROP TABLE TAB;
-- Thủ tục xem, Kiểm tra giám sát hoạt động của user
create or replace procedure pro_select_audit_trail_user
(username in VARCHAR2, cur out sys_refcursor)
is
begin
    open cur for
    SELECT Session_ID, Extended_timestamp, DB_User, UserHost,
    Object_schema, Object_name, Statement_Type, SQL_Bind, SQL_Text
    FROM dba_common_audit_trail
    WHERE AUDIT_TYPE = 'Standard Audit'
    AND DB_USER = username
    AND object_name = 'TAB'
    ORDER BY extended_timestamp DESC;
end;

-----------------------VPD---------------------------
GRANT SELECT,INSERT,UPDATE,DELETE ON DOANNHOM4.USERS TO SELLERS_ROLE;
GRANT SELECT,INSERT,UPDATE,DELETE ON DOANNHOM4.USERDETAILS TO SELLERS_ROLE;

GRANT CREATE SESSION TO JOHN , JOE,AMY,BETH;
--
--Kiểm tra những user nào đã được gán vào role
SELECT * 
FROM DBA_ROLE_PRIVS
WHERE GRANTED_ROLE = 'CUSTOMERS_ROLE';


--==================

--KIỂM TRA TẤT CẢ POLICY 
SELECT *
FROM DBA_POLICIES


--VPD
/*
chính sách cho những user cửa CUSTOMERS_ROLE
những user thuộc nhóm khách hàng thì 
chỉ có quyền xem và sửa dữ liệu của chính mình

*/
CREATE OR REPLACE FUNCTION func_USERDETAILS_SU (
    p_schema VARCHAR2,
    p_object VARCHAR2
)
RETURN VARCHAR2
IS
BEGIN
    IF USER ='JOHN' THEN
        RETURN 'UserID = ''1''';
    ELSIF USER ='JOE' THEN
        RETURN 'UserID = ''2''';
    ELSIF USER ='DOANNHOM4' THEN
        RETURN '';
        ELSIF USER ='SYS' THEN
        RETURN '';
    END IF;
END;
/

BEGIN
    DBMS_RLS.add_POLICY(
        object_schema   => 'DOANNHOM4',
        object_name     => 'USERDETAILS',
        policy_name     => 'USERDETAILS_POLICY_CTRL01',
        policy_function => 'func_USERDETAILS_SU',
        statement_types => 'SELECT,UPDATE',
        update_check=> TRUE
    );
END;
/
---=========================================================

-- TEST CASE VPD

--test lần lượt những usser được gán policy; 
SELECT*FROM DOANNHOM4.USERDETAILS;
-- sửa dữ liệu của bản thân từ user thuộc role customers;
UPDATE DOANNHOM4.USERDETAILS
SET ADDRESS='140 Le Trong Tan'
WHERE USERID=1;
COMMIT;
---=========================================================

CREATE OR REPLACE FUNCTION func_USERDETAILS_ID (
    p_schema VARCHAR2,
    p_object VARCHAR2
)
RETURN VARCHAR2
IS
BEGIN
    IF USER ='DOANNHOM4' THEN
        RETURN '';
    END IF;
END;
/


BEGIN
    DBMS_RLS.add_POLICY(
        object_schema   => 'DOANNHOM4',
        object_name     => 'USERDETAILS',
        policy_name     => 'USERDETAILS_POLICY_CTRL02',
        policy_function => 'func_USERDETAILS_ID',
        statement_types => 'INSERT,DELETE',
        update_check=> TRUE
    );
END;
/
---=========================================================

-- TEST CASE VPD
SELECT * FROM USERS;
SELECT * FROM USERDETAILS;

INSERT INTO USERS VALUES(6,NULL,NULL,'(null)');
INSERT INTO USERDETAILS VALUES(6,NULL,NULL,2);

DELETE FROM USERDETAILS
WHERE USERID=6;

commit all
---=========================================================

--OLS