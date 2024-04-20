i created some store procedure in database for adding the customer data and creating the account for that and  transaciton procedure those are below:-

**ADD ACCOUNT**

ALTER PROCEDURE [dbo].[ADDACCOUNT](
 @CUST_FIRSTNAME VARCHAR(80),
 @CUST_MIDDLENAME VARCHAR(80),
 @CUST_LASTNAME VARCHAR(80),
 @CUST_ADDRESS VARCHAR(200),
 @DOB DATETIME,
 @MOBILE DECIMAL(10),
 @EMAIL VARCHAR(40),
 @AADHAR DECIMAL(12),
 @ACCOOUNT_TYPE INT

 )
 AS
 BEGIN
 BEGIN TRANSACTION 

 BEGIN  TRY
 DECLARE @CUSTOMER_ID INT;
 SELECT @CUSTOMER_ID = CUSTOMER_ID FROM CUSTOMERS WHERE CUSTOMER_AADHARNO = @AADHAR

IF NOT EXISTS (SELECT CUSTOMER_ID FROM CUSTOMERS WHERE CUSTOMER_AADHARNO = @AADHAR)

 BEGIN
 INSERT INTO CUSTOMERS (
 CUSTOMER_FIRSTNAME,
 CUSTOMER_MIDDLETNAME,
 CUSTOMER_LASTNAME
 ,CUSTOMER_ADDRESS,
 CUSTOMER_MOBILENO,
 CUSTOMER_EMAIL,
 CUSTOMER_DOB,
 CUSTOMER_AADHARNO)

 VALUES(
 @CUST_FIRSTNAME,
 @CUST_MIDDLENAME,
 @CUST_LASTNAME,
 @CUST_ADDRESS,
 @MOBILE,
 @EMAIL,
 @DOB,
 @AADHAR
 );

 SET @CUSTOMER_ID = SCOPE_IDENTITY();
 
 END
 DECLARE @INTEREST_RATE FLOAT;
 SELECT @INTEREST_RATE = INTEREST_RATE FROM dbo.INTEREST_RATE WHERE ACCOUNT_TYPE =  @ACCOOUNT_TYPE
 INSERT INTO ACCOUNTS( 
 CUSTOMER_ID,
 ACCOUNT_TYPE,
 INTEREST_RATE,
 OPEN_DATE

 )
 VALUES(
 @CUSTOMER_ID,
 @ACCOOUNT_TYPE,
 @INTEREST_RATE,
 GETDATE()
 
 );

 END TRY
 
  BEGIN CATCH

 ROLLBACK TRANSACTION
 END CATCH

 COMMIT TRANSACTION
 END


 **TRANSACTION BANK TO ACCOUNT**

 
ALTER PROCEDURE [dbo].[TRANSACTIONTOACCOUNT](
@AMOUNT DECIMAL(30),
@ACCOUNT_NUMBER DECIMAL(12),
@DESC VARCHAR(200) NULL,
@TRANSACTION_TYPE INT
)
AS
BEGIN
	
BEGIN TRY
IF NOT EXISTS (SELECT @ACCOUNT_NUMBER FROM ACCOUNTS WHERE ACCOUNT_NUMBER = @ACCOUNT_NUMBER)
BEGIN
RAISERROR('ACCOUNT IS NOT FOUND',16,1);
RETURN 
END

DECLARE @ACCOUNT_STATUS INT;
 

SELECT @ACCOUNT_STATUS = ACCOUNT_STATUS FROM ACCOUNTS WHERE ACCOUNT_NUMBER = @ACCOUNT_NUMBER
	
IF @ACCOUNT_STATUS = 1
BEGIN
RAISERROR('ACCOUNT IS INACTIVE PLEASE ACTIVATE FIRST',16,1);
RETURN 
END


DECLARE @CURRENT_AMOUNT DECIMAL(30)
SELECT @CURRENT_AMOUNT = AMOUNT FROM ACCOUNTS WHERE ACCOUNT_NUMBER=@ACCOUNT_NUMBER

IF @TRANSACTION_TYPE=1 AND @AMOUNT > @CURRENT_AMOUNT 
BEGIN
RAISERROR('UNSUFFICIENT BALANCE FOR WITHDRAW',16,1);
RETURN 
END


INSERT INTO TRANSACTIONS(
ACCOUNT_NUMBER,
TRANSACTION_AMOUNT,
TRANSACTION_DESCRIPTION,
TRANSACTION_DATE,
TANSACTION_TYPE
)
VALUES(
@ACCOUNT_NUMBER,
@AMOUNT,
@DESC,
GETDATE(),
@TRANSACTION_TYPE
)
IF @TRANSACTION_TYPE =1
BEGIN
UPDATE ACCOUNTS 
SET AMOUNT = @CURRENT_AMOUNT-@AMOUNT WHERE ACCOUNT_NUMBER = @ACCOUNT_NUMBER
END

ELSE
BEGIN
UPDATE ACCOUNTS 
SET AMOUNT = @CURRENT_AMOUNT+@AMOUNT WHERE ACCOUNT_NUMBER = @ACCOUNT_NUMBER
END


END TRY

BEGIN CATCH

DECLARE  @ERRORMESSAGE NVARCHAR(200) = ERROR_MESSAGE();
THROW 50001,@ERRORMESSAGE,1
	
END CATCH

END

**TRANSFER MONEY FROM ACCOUNT TO ACCOUNT**


ALTER PROCEDURE [dbo].[TRANSFERAMOUNT](
@AMOUNT DECIMAL(30),
@ACCOUNT_NUMBER DECIMAL(12),
@RECEIVER_ACCOUNT_NUMBER DECIMAL(12),
@DESC VARCHAR(200) NULL
)
AS
BEGIN


BEGIN TRY
IF @ACCOUNT_NUMBER = @RECEIVER_ACCOUNT_NUMBER
BEGIN
RAISERROR('NOT TRANSFER TO SAME ACCOUNT',16,1);
END
IF NOT EXISTS (SELECT ACCOUNT_NUMBER FROM ACCOUNTS WHERE ACCOUNT_NUMBER = @ACCOUNT_NUMBER)
BEGIN
RAISERROR('ACCOUNT IS NOT FOUND',16,1);
RETURN;
END

IF NOT EXISTS (SELECT ACCOUNT_NUMBER FROM ACCOUNTS WHERE ACCOUNT_NUMBER =@RECEIVER_ACCOUNT_NUMBER)
BEGIN
RAISERROR('ACCOUNT IS NOT FOUND',16,1);
RETURN; 
END

DECLARE @ACCOUNT_STATUS INT;
 

SELECT @ACCOUNT_STATUS = ACCOUNT_STATUS FROM ACCOUNTS WHERE ACCOUNT_NUMBER = @ACCOUNT_NUMBER
	
IF @ACCOUNT_STATUS = 1
BEGIN

RAISERROR('ACCOUNT IS INACTIVE PLEASE ACTIVATE FIRST',16,1);
RETURN 
END

SELECT @ACCOUNT_STATUS = ACCOUNT_STATUS FROM ACCOUNTS WHERE ACCOUNT_NUMBER = @RECEIVER_ACCOUNT_NUMBER

IF @ACCOUNT_STATUS = 1
BEGIN

RAISERROR('ACCOUNT IS INACTIVE PLEASE ACTIVATE FIRST',16,1);
RETURN 
END

DECLARE @CURRENT_AMOUNT DECIMAL(30)
SELECT @CURRENT_AMOUNT = AMOUNT FROM ACCOUNTS WHERE ACCOUNT_NUMBER=@ACCOUNT_NUMBER

IF  @AMOUNT > @CURRENT_AMOUNT 
BEGIN

RAISERROR('UNSUFFICIENT BALANCE FOR WITHDRAW',16,1);
RETURN 
END


INSERT INTO TRANSACTIONS(
ACCOUNT_NUMBER,
RECEIVER_ACCOUNT_NUMBER,
TRANSACTION_AMOUNT,
TRANSACTION_DESCRIPTION,
TRANSACTION_DATE,
TANSACTION_TYPE
)
VALUES(
@ACCOUNT_NUMBER,
@RECEIVER_ACCOUNT_NUMBER,
@AMOUNT,
@DESC,
GETDATE(),
1
)

INSERT INTO TRANSACTIONS(
ACCOUNT_NUMBER,
RECEIVER_ACCOUNT_NUMBER,
TRANSACTION_AMOUNT,
TRANSACTION_DESCRIPTION,
TRANSACTION_DATE,
TANSACTION_TYPE
)
VALUES(
@RECEIVER_ACCOUNT_NUMBER,
@ACCOUNT_NUMBER,
@AMOUNT,
@DESC,
GETDATE(),
0
)

UPDATE ACCOUNTS 
SET AMOUNT = @CURRENT_AMOUNT-@AMOUNT WHERE ACCOUNT_NUMBER = @ACCOUNT_NUMBER

SELECT @CURRENT_AMOUNT = AMOUNT FROM ACCOUNTS WHERE ACCOUNT_NUMBER=@RECEIVER_ACCOUNT_NUMBER

UPDATE ACCOUNTS 
SET AMOUNT = @CURRENT_AMOUNT+@AMOUNT WHERE ACCOUNT_NUMBER = @RECEIVER_ACCOUNT_NUMBER

END TRY

BEGIN CATCH

DECLARE  @ERRORMESSAGE NVARCHAR(200) = ERROR_MESSAGE();
THROW 500001,@ERRORMESSAGE,1

END CATCH

END
![Screenshot 2024-04-20 104519](https://github.com/Vasindha/Bank-Management-Backend-Asp.net-core-web-api/assets/103803533/f2b5e63d-f9d2-4960-b1d5-8f96b8bb5fa5)
![Screenshot 2024-04-20 104245](https://github.com/Vasindha/Bank-Management-Backend-Asp.net-core-web-api/assets/103803533/d246fdeb-f2e2-4062-99aa-e7b7c67d8922)
![Screenshot 2024-04-20 104220](https://github.com/Vasindha/Bank-Management-Backend-Asp.net-core-web-api/assets/103803533/97a01c2b-7d61-45ea-a4e5-424f9c64f200)
![Screenshot 2024-04-20 104148](https://github.com/Vasindha/Bank-Management-Backend-Asp.net-core-web-api/assets/103803533/7c3c180b-7596-4432-a0c3-094ff9f0bbfb)
![Screenshot 2024-04-20 104007](https://github.com/Vasindha/Bank-Management-Backend-Asp.net-core-web-api/assets/103803533/743c50a0-13bd-4304-a4a9-5a34cb54d2c0)
![Screenshot 2024-04-20 103942](https://github.com/Vasindha/Bank-Management-Backend-Asp.net-core-web-api/assets/103803533/2efe2e88-d674-4024-a2b9-dd71a769047c)
![Screenshot 2024-04-20 103914](https://github.com/Vasindha/Bank-Management-Backend-Asp.net-core-web-api/assets/103803533/0e7a7d3e-6c5f-4622-b000-feb5083d5e3a)
![Screenshot 2024-04-20 103849](https://github.com/Vasindha/Bank-Management-Backend-Asp.net-core-web-api/assets/103803533/7e29f7bf-f6e3-4b7a-a62c-85c12266fd48)
![Screenshot 2024-04-20 103709](https://github.com/Vasindha/Bank-Management-Backend-Asp.net-core-web-api/assets/103803533/f9ff518d-76cd-43d8-999e-352e4b030532)
![Screenshot 2024-04-20 103629](https://github.com/Vasindha/Bank-Management-Backend-Asp.net-core-web-api/assets/103803533/b5611d49-481a-4b15-a775-b2109f9b29e1)
