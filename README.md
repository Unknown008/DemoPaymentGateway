# DemoPaymentGateway

This is a demo application that simulates a payment gateway and a bank.

Requirements were as follows:
1. A merchant should be able to process a payment through the payment gateway and receive either a successful or unsuccessful response.
2. A merchant should be able to retrieve the details of a previously made payment.

### Considerations for solution design
- The bank simulator was created following a monolithic architecture since it was faster to create it that way
- The payment gateway was created following Domain Driven Design principles, SOLID principles, CQRS pattern and CLEAN architecture. Most of the application here was inspired from the repository [AspnetMicroservices](https://github.com/mehmetozkaya/AspnetMicroservices).

### Project set up

##### Bank Simulator
1. Run the SQL script `BankDemoCreation.sql` from the `BankSimulator` directory to create the database, tables and populate the tables with some basic sample data.
2. Open `BankSimulator.csproj` in visual studio from the `BankSimulator\BankSimulator` directory. This will start the Bank Simulator API (a swagger page will open in the default browser).

##### Payment Gateway
1. Create a user named `demouser` in MS SQL with password `Password1` and give it access to create and manage databases.
2. Open `Payment.API.csproj` in visual studio from the `PaymentGateway\src\Services\Payment\Payment.API` directory, set Payment.API as startup project and start the application. This will create the database, tables and insert some sample records if the SQL user was properly set up, and start the Payment Gateway API (a swagger page will open in the default browser).
3. The sample payment created in the database has Bank Transaction ID 1, so the GET API can be run with an ID of 1.
4. To forward a payment, the post API endpoint can be used. For the body payload, the proper details should be inserted (working details can be obtained from the Bank Simulator Database named `BankDemo` from the table `Accounts`). An example payload:
    ```
    {
        "cardNumber": "4111111111111111",
        "expiryMonth": 12,
        "expiryYear": 2022,
        "amount": 12,
        "currency": "MUR",
        "cvv": "123"
    }
    ```
   Sending the above should give a successful response from the Bank Simulator with the transaction ID. That ID can then be used in the GET API endpoint to view the payment.

### Assumptions and Improvements
- A lot of the otherwise important stuff were excluded for the sake of the demo, for instance, the API endpoints should be protected and behind an authentication system with encryption where required.
- The only mode of payment is via card, obviously there are many more modes of payment nowadays including cryptocurrency, bank transfer, MCB Juice, etc which have been ignored here.
- No validation on the currency has been implemented really, and further implementation to include exchange rates and fees could be added.
- Transaction fees have been ignored, and could be implemented.
- Status of the transaction has been limited to 3 status in the implementation, but more could be added
- The solution can be deployed to cloud services like Azure or AWS to ensure high availability, security, scalability and resilience
- Performing appropriate unit tests while implementing the different features. I learned how to unit test simple methods, but have been unsuccessful in testing more complex ones that involve async methods so far. I just could not get the opportunity to research on it properly.