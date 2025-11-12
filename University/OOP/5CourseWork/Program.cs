using System;

namespace HelloWorld
{
    // Class Dispatcher that create all events and create a separete function to Invoke this event
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

	// Class that now is responsable to show everything in the terminal(almoust all was transefered there)
    class Loger
    {
		// Method to work with all Info methods from money related classes
        public void DisplayInfoOfObject(string type, string message)
        {
            Console.WriteLine($"[{type}] {message}");
        }
		// Method for addition and substraction method
        public void DisplayInfoAboutOperation(double valueUsed, double initialSum, string operationType)
        {
            Console.WriteLine($"The {operationType} operation was performed with respect ot the sum of {initialSum:N2} using the value {valueUsed:N2}");
        }
		// Method for comparison
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
		// Method for Investment Analysis at the start, to describe what will happen
        public void DisplayInvestmentAnalysis(double investment, double currentMoney)
        {
            Console.WriteLine($"[Investment Analysis] Investment: {investment}; Money now: {currentMoney}.");
            Console.WriteLine("How much you get for investment in percentage:");
        }
		// Method for end of the Investment Analysis to return the result (I am trying to do tranfer all showing there)
        public void DisplayReturnRate(float returnRate)
        {
            Console.WriteLine($"{returnRate*100}%");
        }
		// Method for calculating price without taxes
        public void DisplayWithholdingCalculation(double currentMoney, float taxRate)
        {
            Console.WriteLine($"[Withholding Calculation] Initial price: {currentMoney}; Tax rate: {taxRate}");
            Console.WriteLine("Expected price without taxes:");
        }
		// Method for Interest Calculation
        public void DisplayInterestCalculation(double currentMoney, float interestRate)
        {
            Console.WriteLine($"[Interest Calculation] Current money: {currentMoney}; Interest rate: {interestRate}");
            Console.WriteLine("Expected money:");
        }
		// Method for Updating Exchange Rate
        public void DisplayUpdateExchangeRate(float oldER, float newER)
        {
            Console.WriteLine($"Updating exchange rate from {oldER} to {newER}");
        }
    }

    // Personal Exception class
    class UserException : Exception
    {
        public UserException(string message) : base(message)
        {
        }
    }

	// Main maney class, that work only with sums 
    class GSum
    {
		// Now stores dispatcher for all next classes to trigger events(throught methods) from methods
        protected Dispatcher? dispatcher;

		// Main atribute of the class
        public double Sum { get; set; }

		// Some constructors
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

		// Overidable Info metod
        public virtual void Info()
        {
            dispatcher?.SendInfo("GSum", $"You have {Sum:N2}");
        }
		// Overidable Addition and Subtraction
        public virtual void Addition(double anotherSum)
        {
            dispatcher?.SendMoneyAdded(anotherSum, Sum, "Addition");
            Sum += anotherSum;
        }
        public virtual void Subtraction(double anotherSum)
        {
            dispatcher?.SendMoneySubtracted(anotherSum, Sum, "Subtraction");
            if (Sum-anotherSum < 0){
                throw new UserException("You can't do that. You don't have enouth.");
            } else {
                Sum -= anotherSum;
            }
        }
    }
	// Initial class of Money 
    class Money : GSum
    {
		// Main attributes of the class
        public long Euros { get; set; }
        public byte Cents { get; set; }

		// Constructors
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
		// Methods to udate or check for correct format of the atributs
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
		// overided methods
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
                throw new UserException("Not enough money! Operation failed.");
            }
        }

		// Main methods of the class for each there are at least one event
        public double InvestmentReturnAnalysis(Money initialInvestment)
        {
            dispatcher?.SendInvestmentAnalysis(initialInvestment.Sum, Sum);
            if (initialInvestment.Sum == 0)
            {
                throw new UserException("Initial investment cannot be zero.");
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
                throw new UserException("Invalid tax rate");
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

	// The most advanced money class that still didn't impemented till the end
    class CurrencyMoney : Money
    {
		// New attributs for this class
        public string CurrencyType{ get; set; }

        public float ExchangeRate{ get; set; }

		// Constructors 
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
		// Constructors for converting from Money class
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
		// Overided Info method
        public override void Info()
        {
            dispatcher?.SendInfo("CurrencyMoney", $"Currently you have: {Euros}.{Cents} {CurrencyType} that is {ExchangeRate} to USD");
        }
		// Only one new method that updates Exchange rate
        public void updateExchangeRate(float newExchangeRate)
        {
            dispatcher?.SendUpdateExchangeRate(ExchangeRate, newExchangeRate);
            ExchangeRate = newExchangeRate;
            Info();
        }
    }


    class Program
    {	
		// Defining this atribute to store dispatcher object here and then accessing it in every constructor of the classes
        public static Dispatcher? MainDispatcher;

        public static void Main(string[] args)
        {	
			// Printing name of the author and time it has been run
            Console.WriteLine($"The program is running by Anton Kurochkin and current time is {DateTime.Now}");
            Console.WriteLine();
            
            MainDispatcher = new Dispatcher(); // not a local variable, this is field

			// Create a loger and subscribing each method to the corresponding event
            Loger loger = new ();

            MainDispatcher.OnInfoDisplay += loger.DisplayInfoOfObject;
            MainDispatcher.OnMoneyAdded += loger.DisplayInfoAboutOperation;
            MainDispatcher.OnMoneySubtracted += loger.DisplayInfoAboutOperation;
            MainDispatcher.OnInvestmentAnalysis += loger.DisplayInvestmentAnalysis;
            MainDispatcher.OnEndInvestmentAnalysis += loger.DisplayReturnRate;
            MainDispatcher.OnWithholdingCalculation += loger.DisplayWithholdingCalculation;
            MainDispatcher.OnInterestCalculation += loger.DisplayInterestCalculation;
            MainDispatcher.OnUpdateExchangeRate += loger.DisplayUpdateExchangeRate;
            
            try
            {
                CurrencyMoney cmoney1 = new CurrencyMoney(100, 50);
                cmoney1.Info();
                
                cmoney1.Addition(50.75); 
                cmoney1.Info();

                cmoney1.Subtraction(50.30);
                cmoney1.Info();
                
                cmoney1.Subtraction(200.0); // Should fail
            }
            catch (UserException ex)
            {
                Console.WriteLine($"Showing expected error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Showing unexpected error: {ex.Message}");
            }
			// Just running all test, where evrything else will be desribed by loger methods that will be running by events in methods of the money classes
            CurrencyMoney cmoney1 = new CurrencyMoney(100, 50);
            cmoney1.Info();
            
	        cmoney1.Addition(50.75); 
            cmoney1.Info();

            try
            {
                CurrencyMoney initial = new CurrencyMoney(50);
                CurrencyMoney current = new CurrencyMoney(75);
                float returnRate = (float) current.InvestmentReturnAnalysis(initial);
            }
            catch (UserException ex)
            {
                Console.WriteLine($"Showing expected error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Showing unexpected error: {ex.Message}");
            }
            try
            {
                CurrencyMoney priceWithTax = new CurrencyMoney(200);
                CurrencyMoney priceWithoutTax = new CurrencyMoney(priceWithTax.TaxWithholdingCalculations(0.20f));
                priceWithoutTax.Info();
            }
            catch (UserException ex)
            {
                Console.WriteLine($"Showing expected error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Showing unexpected error: {ex.Message}");
            }
            
            try
            {
                CurrencyMoney principal = new CurrencyMoney(1000);
                CurrencyMoney withInterest = new CurrencyMoney(principal.InterestAccuralCalculations(0.05f), principal.CurrencyType, principal.ExchangeRate);
                withInterest.Info();
            }
            catch (UserException ex)
            {
                Console.WriteLine($"Showing expected error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Showing unexpected error: {ex.Message}");
            }
            
            try
            {
                CurrencyMoney budget = new CurrencyMoney(500);
                CurrencyMoney spending = new CurrencyMoney(450);
                budget.BudgetVsPrice(spending);
            }
            catch (UserException ex)
            {
                Console.WriteLine($"Showing expected error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Showing unexpected error: {ex.Message}");
            }
           
            try
            {
                CurrencyMoney edgeCase = new CurrencyMoney(0, 150);
                Console.Write("150 cents should become: ");
                edgeCase.Info();
            }
            catch (UserException ex)
            {
                Console.WriteLine($"Showing expected error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Showing unexpected error: {ex.Message}");
            }

            try
            {
                CurrencyMoney euroMoney = new CurrencyMoney(25, 25, "EUR", 1.2f);
                euroMoney.Info();
                euroMoney.updateExchangeRate(1.5f);
            }
            catch (UserException ex)
            {
                Console.WriteLine($"Showing expected error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Showing unexpected error: {ex.Message}");
            }

            try
            {
                GSum newsum = new(25.259);
                newsum.Info();
                newsum.Subtraction(30);
                newsum.Info();
            }
            catch (UserException ex)
            {
                Console.WriteLine($"Showing expected error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Showing unexpected error: {ex.Message}");
            }

            try
            {
                Console.Write("Enter a number to make an error in the program(you know what to do): ");
                int errorNumber = int.Parse(Console.ReadLine());
                int result = 1/errorNumber;
            }
            catch (UserException ex)
            {
                Console.WriteLine($"Showing expected error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Showing unexpected error: {ex.Message}");
            }
        }
    }
}
