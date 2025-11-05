using System;

namespace HelloWorld
{
    class Dispatcher
    {
        public event Action<string, string>? OnInfoDisplay;
        public event Action<double, double, string>? OnMoneyAdded;
        public event Action<double, double, string>? OnMoneySubtracted;
        public event Action<double, double, string>? OnBudgetComparison;
        public event Action<double, double>? OnInvestmentAnalysis;
        public event Action<float>? OnEndInvestmentAnalysis;
        public event Action<double, float>? OnWithholdingCalculation;
        public event Action<double, float>? OnInterestCalculation;
        public event Action<float, float>? OnUpdateExchangeRate;

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
            OnBudgetComparison?.Invoke(budget, price, message);
        }
        public void SendInvestmentAnalysis(double investment, double currentMoney)
        {
            OnInvestmentAnalysis?.Invoke(investment, currentMoney);
        }
        public void SendEndInvestmentAnalysis(float returnMultiplier)
        {
            OnEndInvestmentAnalysis?.Invoke(returnMultiplier);
        }
        public void SendWithholdingCalculation(double currentMoney, float taxRate)
        {
            OnWithholdingCalculation?.Invoke(currentMoney, taxRate);
        }
        public void SendInterestCalculation(double currentMoney, float interestRate)
        {
            OnInterestCalculation?.Invoke(currentMoney, interestRate);
        }
        public void SendUpdateExchangeRate(float oldER, float newER)
        {
            OnUpdateExchangeRate?.Invoke(oldER, newER);
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
            Console.WriteLine($"The {operationType} operation was performed with respect ot the sum of {initialSum:N2} using the value {valueUsed:N2}");
        }
        public void DisplayComparisonInfo(double budget, double price, string message)
        {
            Console.WriteLine($"[Comarison operation] Current budget: {budget}; Current price: {price}");
            Console.WriteLine(message);
            if(budget >= price)
            {
                Console.WriteLine($"There is {budget-price:N2} left after you buy it.");
            } else
            {
                Console.WriteLine($"To buy it, you need {price-budget:N2} to be able to buy it.");
            }
        }
        public void DisplayInvestmentAnalysis(double investment, double currentMoney)
        {
            Console.WriteLine($"[Investment Analysis] Investment: {investment}; Money now: {currentMoney}.");
            Console.WriteLine("How much you get for investment in percentage:");
        }
        public void DisplayReturnRate(float returnRate)
        {
            Console.WriteLine($"{returnRate*100}%");
        }
        public void DisplayWithholdingCalculation(double currentMoney, float taxRate)
        {
            Console.WriteLine($"[Withholding Calculation] Initial price: {currentMoney}; Tax rate: {taxRate}");
            Console.WriteLine("Expected price without taxes:");
        }
        public void DisplayInterestCalculation(double currentMoney, float interestRate)
        {
            Console.WriteLine($"[Interest Calculation] Current money: {currentMoney}; Interest rate: {interestRate}");
            Console.WriteLine("Expected money:");
        }
        public void DisplayUpdateExchangeRate(float oldER, float newER)
        {
            Console.WriteLine($"Updating exchange rate from {oldER} to {newER}");
        }
    }

    class GSum
    {
        protected Dispatcher? dispatcher;

        public double Sum { get; set; }

        public GSum()
        {
            Sum = 0;
            dispatcher = Program.MainDispatcher;
        }
        public GSum(double sum)
        {
            Sum = sum;
            dispatcher = Program.MainDispatcher;
        }

        public virtual void Info()
        {
            dispatcher?.SendInfo("GSum", $"You have {Sum:N2}");
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
    class Money : GSum
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
        public Money(GSum sum) : base(sum.Sum)
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
            float fractionalPart = (float) Cents;
            Sum = (double) Euros + fractionalPart / 100;
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
            dispatcher?.SendInvestmentAnalysis(initialInvestment.Sum, Sum);
            if (initialInvestment.Sum == 0)
            {
                Console.WriteLine("Error: Initial investment cannot be zero");
                return 0;
            }
            
            float returnMultiplier = (float) (Sum / initialInvestment.Sum);
            dispatcher?.SendEndInvestmentAnalysis(returnMultiplier);
            return returnMultiplier;
        }

        public Money TaxWithholdingCalculations(float taxRate)
        {
            dispatcher?.SendWithholdingCalculation(Sum, taxRate);
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
            dispatcher?.SendInterestCalculation(Sum, interestRate);
            double finalValue = (Sum * (1 + interestRate));
            return new Money(finalValue);
        }

        public void BudgetVsPrice(Money spending)
        {
            string output;
            if (Sum == spending.Sum) { output = "You have just enouth money to buy it.";}
            else if (Sum > spending.Sum) { output = "You have money to buy it.";}
            else { output = "You have not enouth money to buy it.";}
            dispatcher?.SendBudgetComparison(Sum, spending.Sum, output);
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
            UpdateSum();
            dispatcher = Program.MainDispatcher;
        }
        public CurrencyMoney(long euros, byte cents, string currencyType, float exchangeRate)
        {
            Euros = euros;
            Cents = cents;
            CurrencyType = currencyType;
            ExchangeRate = exchangeRate;
            Normalize();
            UpdateSum();
            dispatcher = Program.MainDispatcher;
        }
        public CurrencyMoney(Money money)
            : base(money.Euros, money.Cents)
        {
            CurrencyType = "USD";
            ExchangeRate = 1;
            Normalize();
            UpdateSum();
            dispatcher = Program.MainDispatcher;
        }
        public CurrencyMoney(Money money, string currencyType, float exchangeRate)
            : base(money.Euros, money.Cents)
        {
            CurrencyType = currencyType;
            ExchangeRate = exchangeRate;
            Normalize();
            UpdateSum();
            dispatcher = Program.MainDispatcher;
        }
        public override void Info()
        {
            dispatcher?.SendInfo("CurrencyMoney", $"Currently you have: {Euros}.{Cents} {CurrencyType} that is {ExchangeRate} to USD");
        }
        public void updateExchangeRate(float newExchangeRate)
        {
            dispatcher?.SendUpdateExchangeRate(ExchangeRate, newExchangeRate);
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
            MainDispatcher.OnInvestmentAnalysis += loger.DisplayInvestmentAnalysis;
            MainDispatcher.OnEndInvestmentAnalysis += loger.DisplayReturnRate;
            MainDispatcher.OnWithholdingCalculation += loger.DisplayWithholdingCalculation;
            MainDispatcher.OnInterestCalculation += loger.DisplayInterestCalculation;
            MainDispatcher.OnUpdateExchangeRate += loger.DisplayUpdateExchangeRate;
            

            CurrencyMoney cmoney1 = new CurrencyMoney(100, 50);
            cmoney1.Info();
            
	        cmoney1.Addition(50.75); 
            cmoney1.Info();

	        cmoney1.Subtraction(50.30);
            cmoney1.Info();
            
	        cmoney1.Subtraction(200.0); // Should fail
            
            CurrencyMoney initial = new CurrencyMoney(50);
            CurrencyMoney current = new CurrencyMoney(75);
            float returnRate = (float) current.InvestmentReturnAnalysis(initial);
            
            CurrencyMoney priceWithTax = new CurrencyMoney(200);
            CurrencyMoney priceWithoutTax = new CurrencyMoney(priceWithTax.TaxWithholdingCalculations(0.20f));
            priceWithoutTax.Info();
            
            CurrencyMoney principal = new CurrencyMoney(1000);
            CurrencyMoney withInterest = new CurrencyMoney(principal.InterestAccuralCalculations(0.05f), principal.CurrencyType, principal.ExchangeRate);
            withInterest.Info();
            
            CurrencyMoney budget = new CurrencyMoney(500);
            CurrencyMoney spending = new CurrencyMoney(450);
            budget.BudgetVsPrice(spending);
            
            CurrencyMoney edgeCase = new CurrencyMoney(0, 150);
            Console.Write("150 cents should become: ");
            edgeCase.Info();

            CurrencyMoney euroMoney = new CurrencyMoney(25, 25, "EUR", 1.2f);
            euroMoney.Info();

            euroMoney.updateExchangeRate(1.5f);

            GSum newsum = new(25.259);
            newsum.Info();

            newsum.Addition(24.45);
            newsum.Info();

            newsum.Subtraction(30);
            newsum.Info();
        }
    }
}
