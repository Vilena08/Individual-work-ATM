using System.Collections.Generic;

namespace ATM_Simulation {
  internal class ATM {
    private static ATM instance;
    private readonly List<Account> accounts;
    private Account currentUser;

    private ATM() {
      accounts = new List<Account>();
      accounts.Add(new Account("123456", "1111", "Ivan", 5000));
      accounts.Add(new Account("789012", "2222", "Maria", 3000));
      accounts.Add(new Account("345678", "3333", "Alexey", 10000));
    }

    public static ATM GetInstance() {
      if (instance == null) {
        instance = new ATM();
      }
      return instance;
    }

    public bool Login(string number, string pin) {
      foreach (Account acc in accounts) {
        if (acc.Number == number && acc.Pin == pin) {
          currentUser = acc;
          return true;
        }
      }
      return false;
    }

    public void Logout() {
      currentUser = null;
    }

    public bool IsLoggedIn() {
      return currentUser != null;
    }

    public string GetUserName() {
      return currentUser.Name;
    }

    public decimal GetBalance() {
      return currentUser.Balance;
    }

    public void Deposit(decimal amount) {
      if (amount > 0) {
        currentUser.Balance += amount;
      }
    }

    public bool Withdraw(decimal amount) {
      if (amount > 0 && currentUser.Balance >= amount) {
        currentUser.Balance -= amount;
        return true;
      }
      return false;
    }

    public bool Transfer(string targetNumber, decimal amount) {
      Account target = null;

      foreach (Account acc in accounts) {
        if (acc.Number == targetNumber) {
          target = acc;
          break;
        }
      }

      if (target == null) {
        return false;
      }

      if (amount <= 0) {
        return false;
      }

      if (currentUser.Balance >= amount) {
        currentUser.Balance -= amount;
        target.Balance += amount;
        return true;
      }

      return false;
    }
  }
}