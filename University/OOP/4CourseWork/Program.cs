using System;

namespace HelloWorld
{
    class Dispatcher
    {
        public event Action<string, string>? OnInfoDisplay;
        public event Action<double, double, string>? OnMoneyAdded;
        public event Action<double, double, string>? OnMoneySubtracted;
        public event Action<double, double, string>? OnBudgetComparison;

        public void SendInfo(string type, string message)
        {
            OnInfoDisplay?.Invoke(type, message);
        }
        public void SendMoneyAdded(double valueUsed, double initialSum, string operationType)
        {
            OnMoneyAdded?.Invoke(valueUsed, initialSum, operationType);
        }
        public void SendMoneySubtracted(double valueUsed, double initialSum, string operationType)
        {
            OnMoneySubtracted?.Invoke(valueUsed, initialSum, operationType);
        }
        public void SendBudgetComparison(double budget, double price, string message)
        {
            onBudgetComparison?.Invoke(budget, price, message);
        }
    }

    class Loger
    {
        public void DisplayInfoOfObject(string type, string message)
        {
            Console.WriteLine($"[{type}] {message}");
        }
        public void DisplayInfoAboutOperation(double valueUsed, double initialSum, string operationType)
        {
            Console.WriteLine($"The {operationType} operation was performed with respect ot the sum of {initialSum} using the value {valueUsed}");
        }
    }


    class Summ
    {
        protected Dispatcher? dispatcher;

        public double Sum { get; set; }

        public Summ()
        {
            Sum = 0;
            dispatcher = Program.MainDispatcher;
        }
        public Summ(double sum)
        {
            Sum = sum;
            dispatcher = Program.MainDispatcher;
        }

        public virtual void Info()
        {
            dispatcher?.SendInfo("Summ", $"You have {Sum:N2}");
        }
        public virtual void Addition(double anotherSum)
        {
            dispatcher?.SendMoneyAdded(anotherSum, Sum, "Addition");
            Sum += anotherSum;
        }
        public virtual void Subtraction(double anotherSum)
        {
            dispatcher?.SendMoneySubtracted(anotherSum, Sum, "Subtraction");
            if (Sum-anotherSum < 0){
                Console.WriteLine("You don't have so much");
            } else {
                Sum -= anotherSum;
            }
        }
    }
    class Money : Summ
    {
        public long Euros { get; set; }
        public byte Cents { get; set; }

        public Money()
        {
            Euros = 0;
            Cents = 0;
            Sum = 0;
            dispatcher = Program.MainDispatcher;
        }
        public Money(long euros) 
        {
            Euros = euros;
            Cents = 0;
            UpdateSum();
            dispatcher = Program.MainDispatcher;
        }
        public Money(long euros, byte cents) 
        {
            Euros = euros;
            Cents = cents;
            Normalize(); 
            dispatcher = Program.MainDispatcher;
        }
        public Money(Summ sum) : base(sum.Sum)
        {
            UpdateFromSum();
            dispatcher = Program.MainDispatcher;
        }
        public Money(double sum)
        {
            Sum = sum;
            UpdateFromSum();
            dispatcher = Program.MainDispatcher;
        }
        protected void Normalize()
        {
            if (Cents >= 100)
            {
                Euros += Cents / 100;
                Cents = (byte)(Cents % 100);
                UpdateSum();
            }
        }
        protected void UpdateSum()
        {
            Sum = (double) Euros + Cents / 100;
        }
        protected void UpdateFromSum()
        {
            Euros = (long) Math.Floor(Sum);
            Cents = (byte) Math.Floor((Sum-Euros)*100);
        }

        public override void Info()
        {
            dispatcher?.SendInfo("Money", $"This sum is {Euros},{Cents}");
        }

        public override void Addition(double sum)
        {
            dispatcher?.SendMoneyAdded(sum, Sum, "Addition");
            Sum += sum;
            UpdateFromSum();
        }

        public override void Subtraction(double sum)
        {   
            dispatcher?.SendMoneySubtracted(sum, Sum, "Subtraction");
            if (Sum >= sum)
            {
                Sum -= sum;
                UpdateFromSum();
            }
            else 
            { 
                Console.WriteLine("Not enough money! Operation failed.");
            }
        }

        public double InvestmentReturnAnalysis(Money initialInvestment)
        {
            if (initialInvestment.Sum == 0)
            {
                Console.WriteLine("Error: Initial investment cannot be zero");
                return 0;
            }
            
            double returnMultiplier = Sum / initialInvestment.Sum;
            return returnMultiplier;
        }

        public Money TaxWithholdingCalculations(float taxRate)
        {
            if (taxRate < 0)
            {
                Console.WriteLine("Invalid tax rate");
                return new Money(0, 0);
            }
            
            double sumWithoutTax = (Sum / (1 + taxRate));
            return new Money(sumWithoutTax);
        }

        public Money InterestAccuralCalculations(float interestRate)
        {
            double finalValue = (Sum * (1 + interestRate));
            return new Money(finalValue);
        }

        public void BudgetVsActualSpending(Money spending)
        {
            string output;
            if (Sum == spending.Sum) { output = "Just enough, but is it worth it?"; }
            else if (Sum > spending.Sum) { output = $"You have {(Sum - spending.Sum) / 100.0:N2} more than you spend, good job."; }
            else { output = $"You spend €{(spending.Sum - Sum) / 100.0:N2} more than you have, please control your spending more."; }
            Console.WriteLine(output);
        }
    }

    class CurrencyMoney : Money
    {
        public string CurrencyType{ get; set; }

        public float ExchangeRate{ get; set; }

        public CurrencyMoney()
        {
            Euros = 0;
            Cents = 0;
            Sum = 0;
            CurrencyType = "USD";
            ExchangeRate = 1;
            dispatcher = Program.MainDispatcher;
        }
        public CurrencyMoney(long euros)
        {
            Euros = euros;
            Cents = 0;
            Sum = euros;
            CurrencyType = "USD";
            ExchangeRate = 1;
            dispatcher = Program.MainDispatcher;
        }
        public CurrencyMoney(long euros, byte cents)
        {
            Euros = euros;
            Cents = cents;
            CurrencyType = "USD";
            ExchangeRate = 1;
            Normalize();
            dispatcher = Program.MainDispatcher;
        }
        public CurrencyMoney(long euros, byte cents, string currencyType, float exchangeRate)
        {
            Euros = euros;
            Cents = cents;
            CurrencyType = currencyType;
            ExchangeRate = exchangeRate;
            Normalize();
            dispatcher = Program.MainDispatcher;
        }
        public CurrencyMoney(Money money)
            : base(money.Euros, money.Cents)
        {
            CurrencyType = "USD";
            ExchangeRate = 1;
            Normalize();
            dispatcher = Program.MainDispatcher;
        }
        public CurrencyMoney(Money money, string currencyType, float exchangeRate)
            : base(money.Euros, money.Cents)
        {
            CurrencyType = currencyType;
            ExchangeRate = exchangeRate;
            Normalize();
            dispatcher = Program.MainDispatcher;
        }
        public override void Info()
        {
            dispatcher?.SendInfo("CurrencyMoney", $"Currently you have: {Euros}.{Cents} {CurrencyType} that is {ExchangeRate} to USD");
        }
        public void updateExchangeRate(float newExchangeRate)
        {
            ExchangeRate = newExchangeRate;
            Info();
        }
    }


    class Program
    {
        public static Dispatcher? MainDispatcher;

        public static void Main(string[] args)
        {
            Console.WriteLine($"The program is running by Anton Kurochkin and current time is {DateTime.Now}");
            Console.WriteLine();
            
            MainDispatcher = new Dispatcher(); // not a local variable, this is field
            
            Loger loger = new ();

            MainDispatcher.OnInfoDisplay += loger.DisplayInfoOfObject;
            MainDispatcher.OnMoneyAdded += loger.DisplayInfoAboutOperation;
            MainDispatcher.OnMoneySubtracted += loger.DisplayInfoAboutOperation;

            Console.WriteLine("Creating CurrencyMoney with 100.50 on balanse.");
            CurrencyMoney cmoney1 = new CurrencyMoney(100, 50);
            cmoney1.Info();
            
            Console.WriteLine("Adding 50.75");
	        cmoney1.Addition(50.75); 
            cmoney1.Info();

            Console.WriteLine("Substracting 50.30");
	        cmoney1.Subtraction(50.30);
            cmoney1.Info();
            
            Console.WriteLine("Substracting 200 to check an edge case.");
	        cmoney1.Subtraction(200.0); // Should fail
            
            Console.WriteLine("Checking investment return analysis method if initial is 50 and current is 75");
            CurrencyMoney initial = new CurrencyMoney(50);
            CurrencyMoney current = new CurrencyMoney(75);
            double returnRate = current.InvestmentReturnAnalysis(initial);
            Console.WriteLine($"Return multiplier: {returnRate}");
            
            Console.WriteLine("Checking tax with holding calculation methond if price with tax is 200 and tax rate is 20%");
            CurrencyMoney priceWithTax = new CurrencyMoney(200);
            CurrencyMoney priceWithoutTax = new CurrencyMoney(priceWithTax.TaxWithholdingCalculations(0.20f));
            priceWithoutTax.Info();
            
            Console.WriteLine("Cheking interest accural calculation method if principal is 1000.");
            CurrencyMoney principal = new CurrencyMoney(1000);
            CurrencyMoney withInterest = new CurrencyMoney(principal.InterestAccuralCalculations(0.05f), principal.CurrencyType, principal.ExchangeRate);
            Console.Write("Amount with 5% interest: ");
            withInterest.Info();
            
            Console.WriteLine("Cheking budget vs actual spending if budget is 500 and spending is 450");
            CurrencyMoney budget = new CurrencyMoney(500);
            CurrencyMoney spending = new CurrencyMoney(450);
            budget.BudgetVsActualSpending(spending);
            
            Console.WriteLine("Cheking if Normalization works if there are 150 cents entered");
            CurrencyMoney edgeCase = new CurrencyMoney(0, 150);
            Console.Write("150 cents should become: ");
            edgeCase.Info();

            Console.WriteLine("Cheking if creating a currency money with all atributes works correctly.");
            CurrencyMoney euroMoney = new CurrencyMoney(25, 25, "EUR", 1.2f);
            euroMoney.Info();

            Console.WriteLine("Cheking if updating the rate is working.");
            euroMoney.updateExchangeRate(1.5f);

            Console.WriteLine("Cheking the summ class work");
            Summ newsum = new(25.259);
            newsum.Info();

            Console.WriteLine("Addition");
            newsum.Addition(24.45);
            newsum.Info();

            Console.WriteLine("Subtraction");
            newsum.Subtraction(30);
            newsum.Info();
        }
    }
}
