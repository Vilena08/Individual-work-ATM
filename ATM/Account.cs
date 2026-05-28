namespace ATM_Simulation {
  internal class Account {
    public string Number;
    public string Pin;
    public decimal Balance;
    public string Name;

    public Account(string number, string pin, string name, decimal balance) {
      Number = number;
      Pin = pin;
      Name = name;
      Balance = balance;
    }
  }
}