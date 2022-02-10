using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Account
{
    private decimal balance; 

    public Account(decimal balance) // constructor to set balance
    {
        Balance = balance;
    }

    public virtual bool Debit(decimal amt)
    {
        if (amt <= balance) // if balnce is more than ammount
        {
            balance = balance - amt;
            return true; // widthraw
        }
        else
        {
            Console.WriteLine("Debit amount exceeded account balance");
            return false;
        }
    }

    public virtual void Credit(decimal amt) // add to balance
    {
        balance = amt + balance;
    }



    public decimal Balance 
    {
        get { return balance; }
        set
        {
            if (value >= 0.0M) // should be bigger than 0
            {
                balance = value;
            }
            else
            {
                balance = 0.0M;
                Console.WriteLine("Balance should be greater equal to 0.0 setting it to 0.0");
            }
        }
    }


}


public class SavingsAccount : Account 

{
    private decimal interestRate; //private instance 

    public SavingsAccount(decimal balance, decimal interestRate) : base(balance) //constructor of SavingsAccount
    {
        this.interestRate = interestRate;
    }

    public decimal CalculateInterest() // calculate the interestRate

    {
        return Balance * interestRate / 100m; //Balance is bigger than intrestRate
    }
}

public class CheckingAccount : Account 
{
    private decimal fee; // intrest Fee

    public CheckingAccount(decimal balance, decimal fee) : base(balance) // constructor
    {
        this.fee = fee;
    }

    public override void Credit(decimal amount) //overriding the function 
    {
        base.Credit(amount); 
        Balance = Balance - fee;
    }

    public override bool Debit(decimal amount) //overriding the function 
    {
        if (base.Debit(amount))
        {
            Balance = Balance - fee;
            return true;
        }
        else
        {
            return false;
        }
    }

}


// Processing Accounts polymorphically.
public class AccountTest
{
    public static void Main(string[] args)
    {
        // create array of accounts
        Account[] accounts = new Account[4];

        // initialize array with Accounts
        accounts[0] = new SavingsAccount(25M, .03M);
        accounts[1] = new CheckingAccount(80M, 1M);
        accounts[2] = new SavingsAccount(200M, .015M);
        accounts[3] = new CheckingAccount(400M, .5M);

        // loop through array, prompting user for debit and credit amounts
        for (int i = 0; i < accounts.Length; i++)
        {
            Console.WriteLine($"Account {i + 1} balance: {accounts[i].Balance:C}");

            Console.Write($"\nEnter an amount to withdraw from Account {i + 1}: ");
            decimal withdrawalAmount = decimal.Parse(Console.ReadLine());

            accounts[i].Debit(withdrawalAmount); // attempt to debit

            Console.Write($"\nEnter an amount to deposit into Account {i + 1}: ");
            decimal depositAmount = decimal.Parse(Console.ReadLine());

            // credit amount to Account
            accounts[i].Credit(depositAmount);

            // if Account is a SavingsAccount, calculate and add interest
            if (accounts[i] is SavingsAccount)
            {
                // downcast
                SavingsAccount currentAccount = (SavingsAccount)accounts[i];

                decimal interestEarned = currentAccount.CalculateInterest();
                Console.WriteLine($"Adding {interestEarned:C} interest to Account {i + 1} (a SavingsAccount)");
                currentAccount.Credit(interestEarned);
            }

            Console.WriteLine($"\nUpdated Account {i + 1} balance: {accounts[i].Balance:C}\n\n");

            Console.Write("\r\nThank You for using Seklawi Bank, Press any key to exit.");
            Console.ReadLine();

        }
    }
}
