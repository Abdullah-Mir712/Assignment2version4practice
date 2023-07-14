using System;
using System.Collections.Generic;

// Abstract class representing a bank account
public abstract class BankAccount
{
    private string accountNumber; // Encapsulation - private data member
    private string accountHolderName; // Encapsulation - private data member
    private decimal balance; // Encapsulation - private data member
    private List<string> transactions; // Encapsulation - private data member

    public string AccountNumber { get { return accountNumber; } } // Abstraction - public property
    public string AccountHolderName { get { return accountHolderName; } } // Abstraction - public property
    public decimal Balance { get { return balance; } } // Abstraction - public property

    public BankAccount(string accountNumber, string accountHolderName)
    {
        this.accountNumber = accountNumber;
        this.accountHolderName = accountHolderName;
        this.balance = 0;
        this.transactions = new List<string>();
    }

    public abstract decimal CalculateInterest(); // Polymorphism - abstract method

    public void Deposit(decimal amount) // Method Overloading
    {
        if (amount > 0)
        {
            balance += amount;
            transactions.Add($"Deposited {amount}");
        }
    }

    public void Withdraw(decimal amount) // Method Overloading
    {
        if (amount <= balance)
        {
            balance -= amount;
            transactions.Add($"Withdrew {amount}");
        }
        else
        {
            Console.WriteLine("Insufficient funds.");
        }
    }

    public List<string> GetTransactionHistory()
    {
        return transactions;
    }
}

// Concrete subclass representing a savings account
public class SavingsAccount : BankAccount, ITransaction
{
    public SavingsAccount(string accountNumber, string accountHolderName)
        : base(accountNumber, accountHolderName)
    {
    }

    public override decimal CalculateInterest() // Polymorphism - overriding method
    {
        // Implement interest calculation specific to savings account
        decimal interest = Balance * 0.02m; // Assuming 2% interest rate
        return interest;
    }

    public void ExecuteTransaction(decimal amount) // Implementation of ExecuteTransaction from ITransaction
    {
        Deposit(amount); // Execute a transaction by depositing money into the savings account
    }

    public void PrintTransaction() // Implementation of PrintTransaction from ITransaction
    {
        Console.WriteLine($"Savings Account Transaction History for Account Number: {AccountNumber}");
        foreach (string transaction in GetTransactionHistory())
        {
            Console.WriteLine(transaction);
        }
    }
}

// Concrete subclass representing a checking account
public class CheckingAccount : BankAccount, ITransaction
{
    public CheckingAccount(string accountNumber, string accountHolderName)
        : base(accountNumber, accountHolderName)
    {
    }

    public override decimal CalculateInterest() // Polymorphism - overriding method
    {
        // Implement interest calculation specific to checking account
        decimal interest = Balance * 0.01m; // Assuming 1% interest rate
        return interest;
    }

    public void ExecuteTransaction(decimal amount) // Implementation of ExecuteTransaction from ITransaction
    {
        Withdraw(amount); // Execute a transaction by withdrawing money from the checking account
    }

    public void PrintTransaction() // Implementation of PrintTransaction from ITransaction
    {
        Console.WriteLine($"Checking Account Transaction History for Account Number: {AccountNumber}");
        foreach (string transaction in GetTransactionHistory())
        {
            Console.WriteLine(transaction);
        }
    }
}

// Concrete subclass representing a loan account
public class LoanAccount : BankAccount, ITransaction
{
    public LoanAccount(string accountNumber, string accountHolderName)
        : base(accountNumber, accountHolderName)
    {
    }

    public override decimal CalculateInterest() // Polymorphism - overriding method
    {
        // Implement interest calculation specific to loan account
        decimal interest = Balance * 0.05m; // Assuming 5% interest rate
        return interest;
    }

    public void ExecuteTransaction(decimal amount) // Implementation of ExecuteTransaction from ITransaction
    {
        Deposit(amount); // Execute a transaction by depositing money into the loan account
    }

    public void PrintTransaction() // Implementation of PrintTransaction from ITransaction
    {
        Console.WriteLine($"Loan Account Transaction History for Account Number: {AccountNumber}");
        foreach (string transaction in GetTransactionHistory())
        {
            Console.WriteLine(transaction);
        }
    }
}

// Interface representing a transaction
public interface ITransaction
{
    void ExecuteTransaction(decimal amount);
    void PrintTransaction();
}

// Class representing a bank
public class Bank
{
    private Dictionary<string, BankAccount> accounts; // Aggregation - dictionary of BankAccount objects

    public Bank()
    {
        accounts = new Dictionary<string, BankAccount>();
    }

    public void AddAccount(BankAccount account) // Association - adding BankAccount objects
    {
        accounts.Add(account.AccountNumber, account);
    }

    public BankAccount GetAccount(string accountNumber) // Association - retrieving BankAccount objects
    {
        if (accounts.ContainsKey(accountNumber))
            return accounts[accountNumber];
        return null;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Bank bank = new Bank();

        SavingsAccount savingsAccount = new SavingsAccount("SA001", "John Doe");
        bank.AddAccount(savingsAccount);

        CheckingAccount checkingAccount = new CheckingAccount("CA001", "Jane Smith");
        bank.AddAccount(checkingAccount);

        LoanAccount loanAccount = new LoanAccount("LA001", "Michael Johnson");
        bank.AddAccount(loanAccount);

        // Deposit and withdraw money from accounts
        savingsAccount.Deposit(500);
        savingsAccount.Withdraw(200);

        checkingAccount.Deposit(1000);
        checkingAccount.Withdraw(500);

        loanAccount.Deposit(2000);
        loanAccount.Withdraw(1000);

        // Calculate interest
        decimal savingsInterest = savingsAccount.CalculateInterest();
        decimal checkingInterest = checkingAccount.CalculateInterest();
        decimal loanInterest = loanAccount.CalculateInterest();

        // Print transaction history
        savingsAccount.PrintTransaction();
        Console.WriteLine();

        checkingAccount.PrintTransaction();
        Console.WriteLine();

        loanAccount.PrintTransaction();
        Console.WriteLine();

        // Print account balances and interest
        Console.WriteLine("Account Balances:");
        Console.WriteLine($"Savings Account Balance: {savingsAccount.Balance}");
        Console.WriteLine($"Checking Account Balance: {checkingAccount.Balance}");
        Console.WriteLine($"Loan Account Balance: {loanAccount.Balance}");
        Console.WriteLine();

        Console.WriteLine("Account Interests:");
        Console.WriteLine($"Savings Account Interest: {savingsInterest}");
        Console.WriteLine($"Checking Account Interest: {checkingInterest}");
        Console.WriteLine($"Loan Account Interest: {loanInterest}");
    }
}
