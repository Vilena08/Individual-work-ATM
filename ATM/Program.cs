using System;

namespace ATM_Simulation {
  internal class Program {
    private static readonly ATM atm = ATM.GetInstance();

    private static void Main(string[] args) {
      if (args is null) {
        throw new ArgumentNullException(nameof(args));
      }

      Console.WriteLine("=== ATM ===\n");

      while (true) {
        if (!atm.IsLoggedIn()) {
          ShowLogin();
        } else {
          ShowMenu();
        }
      }
    }

    private static void ShowLogin() {
      Console.WriteLine("--- LOGIN ---");
      Console.Write("Account number: ");
      string number = Console.ReadLine();
      Console.Write("PIN: ");
      string pin = Console.ReadLine();

      if (atm.Login(number, pin)) {
        Console.WriteLine("\nWelcome, " + atm.GetUserName() + "!\n");
      } else {
        Console.WriteLine("\nWrong account number or PIN\n");
      }
    }

    private static void ShowMenu() {
      Console.WriteLine("\n--- MENU (" + atm.GetUserName() + ") ---");
      Console.WriteLine("1 - Balance");
      Console.WriteLine("2 - Deposit");
      Console.WriteLine("3 - Withdraw");
      Console.WriteLine("4 - Transfer");
      Console.WriteLine("5 - Logout");
      Console.Write("Choose: ");

      string choice = Console.ReadLine();

      if (choice == "1") {
        ShowBalance();
      } else if (choice == "2") {
        DoDeposit();
      } else if (choice == "3") {
        DoWithdraw();
      } else if (choice == "4") {
        DoTransfer();
      } else if (choice == "5") {
        atm.Logout();
        Console.WriteLine("Logged out\n");
      } else {
        Console.WriteLine("Wrong choice\n");
      }
    }

    private static void ShowBalance() {
      Console.WriteLine("Balance: " + atm.GetBalance() + " RUB\n");
    }

    private static void DoDeposit() {
      Console.Write("Amount to deposit: ");
      string input = Console.ReadLine();

      if (decimal.TryParse(input, out decimal amount)) {
        if (amount > 0) {
          atm.Deposit(amount);
          Console.WriteLine("Deposited " + amount + " RUB");
          ShowBalance();
        } else {
          Console.WriteLine("Invalid amount\n");
        }
      } else {
        Console.WriteLine("Invalid amount\n");
      }
    }

    private static void DoWithdraw() {
      Console.Write("Amount to withdraw: ");
      string input = Console.ReadLine();

      if (decimal.TryParse(input, out decimal amount)) {
        if (amount > 0) {
          if (atm.Withdraw(amount)) {
            Console.WriteLine("Withdrawn " + amount + " RUB");
            ShowBalance();
          } else {
            Console.WriteLine("Insufficient funds\n");
          }
        } else {
          Console.WriteLine("Invalid amount\n");
        }
      } else {
        Console.WriteLine("Invalid amount\n");
      }
    }

    private static void DoTransfer() {
      Console.Write("Target account number: ");
      string target = Console.ReadLine();
      Console.Write("Amount to transfer: ");
      string input = Console.ReadLine();

      if (decimal.TryParse(input, out decimal amount)) {
        if (amount > 0) {
          if (atm.Transfer(target, amount)) {
            Console.WriteLine("Transferred " + amount + " RUB to account " + target);
            ShowBalance();
          } else {
            Console.WriteLine("Insufficient funds or wrong account\n");
          }
        } else {
          Console.WriteLine("Invalid amount\n");
        }
      } else {
        Console.WriteLine("Invalid amount\n");
      }
    }
  }
}