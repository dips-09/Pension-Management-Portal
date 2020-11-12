By: Kumari Dipika

Employee Id: **848954**

## MFRP: Pension Management Portal

### POD Members

|Name|Work|Employee Id
|---|---|---|
|Kumari Dipika (**Leader**)|Authentication and Mvc|848954
|Devashish Raut|Pension Disbursement|848597
|Ritik Raheja|Process Pension|848580
|Nikunj Kumar Garg|Pensioner Detail|849428

---

## **OBJECTIVE**

### Pension Management is an application designed to cater the needs of pensioner who are looking for their pension withdrawl.
### User needs to login to the portal and provide their details i.e(Aadhar Number, Date Of Birth, Pension-Type).
### The application will provide the pension amount associated to that particular pensioner.
### Upon admin confirmation, the payment amount will be pay out to that particular pensioner.

---

## **MICRO SERVICES FUNCTIONALITY**

Process Pension Microservice:
- Firstly check the pension type.
- Calculate the Pension amount post authentication.
- Return the calculated pension amount. 
- Receive Input from web application.


Pensioner Details Microservice:
- Provide the Information of the registered pensioners.
- It takes Pensioner Aadhar number as an input.

Pension Disbursement Microservice:
- It Disburse the the pensioner pension to its specified bank account.
- It calls two microservice to perform its functionality
   - Process Pension Microservice: (_Pension amount and aadhar details_)
   - Pensioner Details Microservice : (_Bank Details_)
- This microservice is invoked from **Process Pension Microservice**

Authentication Microservice:
- It generates the JWT TOKEN for user authentication purpose.

---

## GitHub link: https://github.com/dips-09/Pension-Management-Portal

---

# EndPoints: 

1. Authorization Microservice: 
[Authorization Microservice](http://52.147.222.252/swagger/index.html)

2. Pension Disbursement Microservice: 
[Pension Disbursement Microservice](http://52.191.87.4/swagger/index.html)

3. Pensioner Details Microservice:
[Pensioner details Microservice](http://52.154.69.176/swagger/index.html)

4. Process Pension Microservice:
[Process Pension Microservice](http://40.76.145.114/swagger/index.html)

---

```
NOTE: To run the project, extract the files and change IP Address in the AppSettings.json file of Penison-Management-Portal and Authorization Microservice to your localhost address.
```




