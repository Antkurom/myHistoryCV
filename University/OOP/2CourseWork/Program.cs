using System;

namespace HelloWorld
{
    class Money
    {
        public long Euros { get; set; }
        public byte Cents { get; set; }

        public Money()
        {
            Euros = 0;
            Cents = 0;
        }
        public Money(long euros) 
        {
            Euros = euros;
            Cents = 0;
        }
        public Money(long euros, byte cents) 
        {
            Euros = euros;
            Cents = cents;
            Normalize(); 
        }
        
        // Method to be sure cents are 0-99
        protected void Normalize()
        {
            if (Cents >= 100)
            {
                Euros += Cents / 100;
                Cents = (byte)(Cents % 100);
            }
        }

        public virtual void Display()
        {
            Console.WriteLine($"This sum is {Euros},{Cents}");
        }

        public void Addition(long additional_euros, byte additional_cents)
        {
            Euros += additional_euros;
            Cents += additional_cents;
            Normalize();
        }

        public void Subtraction(long minus_euros, byte minus_cents)
        {   
            // Convert everything to cents for easier comparison
            long totalCents = Euros * 100 + Cents;
            long minusTotalCents = minus_euros * 100 + minus_cents;
            
            if (totalCents >= minusTotalCents)
            {
                long resultCents = totalCents - minusTotalCents;
                Euros = resultCents / 100;
                Cents = (byte)(resultCents % 100);
            }
            else 
            { 
                Console.WriteLine("Not enough money! Operation failed.");
            }
        }

        public float InvestmentReturnAnalysis(Money initialInvestment)
        {
            float currentValue = Euros + Cents / 100;
            float initialValue = initialInvestment.Euros + initialInvestment.Cents / 100;
            
            if (initialValue == 0)
            {
                Console.WriteLine("Error: Initial investment cannot be zero");
                return 0;
            }
            
            float returnMultiplier = currentValue / initialValue;
            return returnMultiplier;
        }

        public Money TaxWithholdingCalculations(float taxRate)
        {
            if (taxRate < 0) // Tax rate can't be negative
            {
                Console.WriteLine("Invalid tax rate");
                return new Money(0, 0);
            }
            
            long totalCents = Euros * 100 + Cents;
            long sumWithoutTax = (long)(totalCents / (1 + taxRate));
            
            long euros = sumWithoutTax / 100;
            byte cents = (byte)(sumWithoutTax % 100);            

            return new Money(euros, cents);

        }

        public Money InterestAccrualCalculations(float interestRate)
        {
            long totalCents = Euros * 100 + Cents;
            long finalValue = (long)(totalCents * (1 + interestRate));
            
            long euros = finalValue / 100;
            byte cents = (byte)(finalValue % 100);
            
            return new Money(euros, cents);
        }

        public void BudgetVsActualSpending(Money spending)
        {
            long currentSum = Euros * 100 + Cents;
            long spendingSum = spending.Euros * 100 + spending.Cents;
            string output;
            
            if (currentSum == spendingSum) { output = "Just enough, but is it worth it?"; }
            else if (currentSum > spendingSum) { output = $"You have €{(currentSum - spendingSum) / 100.0:F2} more than you spend, good job."; }
            else {  output = $"You spend €{(spendingSum - currentSum) / 100.0:F2} more than you have, please control your spending more."; }
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
            CurrencyType = "USD";
            ExchangeRate = 1;
        }
        public CurrencyMoney(long euros)
        {
            Euros = euros;
            Cents = 0;
            CurrencyType = "USD";
            ExchangeRate = 1;
        }
        public CurrencyMoney(long euros, byte cents)
        {
            Euros = euros;
            Cents = cents;
            CurrencyType = "USD";
            ExchangeRate = 1;
            Normalize();
        }
        public CurrencyMoney(long euros, byte cents, string currencyType, float exchangeRate)
        {
            Euros = euros;
            Cents = cents;
            CurrencyType = currencyType;
            ExchangeRate = exchangeRate;
            Normalize();
        }
        public override void  Display()
        {
            Console.WriteLine($"The amout of money you have is {Euros}.{Cents} {CurrencyType}. Exchange rate to USD is {ExchangeRate}");
            Normalize();
        }
        
        public CurrencyMoney(Money money, string currencyType, float exchangeRate)
            : base(money.Euros, money.Cents)
        {
            CurrencyType = currencyType;
            ExchangeRate = exchangeRate;
        }
        public void updateExchangeRate(float newExchangeRate)
        {
            ExchangeRate = newExchangeRate;
            Display();
        }
    }

    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine($"The program is running by Anton Kurochkin and current time is {DateTime.Now}");
            Console.WriteLine();
            
            // Check old functionality
            CurrencyMoney cmoney1 = new CurrencyMoney(100, 50);
            cmoney1.Display();
            
	        cmoney1.Addition(50, 75); 
            cmoney1.Display();
            
	        cmoney1.Subtraction(50, 30);
            cmoney1.Display();
            
	        cmoney1.Subtraction(200, 0); // Should fail
            
            CurrencyMoney initial = new CurrencyMoney(50);
            CurrencyMoney current = new CurrencyMoney(75);
            float returnRate = current.InvestmentReturnAnalysis(initial);
            Console.WriteLine($"Return multiplier: {returnRate}");
            
            CurrencyMoney priceWithTax = new CurrencyMoney(200);
            CurrencyMoney priceWithoutTax = new CurrencyMoney(priceWithTax.TaxWithholdingCalculations(0.20f), priceWithTax.CurrencyType, priceWithTax.ExchangeRate);
            priceWithoutTax.Display();
            
            CurrencyMoney principal = new CurrencyMoney(1000);
            CurrencyMoney withInterest = new CurrencyMoney(principal.InterestAccrualCalculations(0.05f), principal.CurrencyType, principal.ExchangeRate);
            Console.Write("Amount with 5% interest: ");
            withInterest.Display();
            
            CurrencyMoney budget = new CurrencyMoney(500);
            CurrencyMoney spending = new CurrencyMoney(450);
            budget.BudgetVsActualSpending(spending);
            
            CurrencyMoney edgeCase = new CurrencyMoney(0, 150); // Should normalize to 1.50
            Console.Write("150 cents should become: ");
            edgeCase.Display();

            //Check new functionality
            CurrencyMoney euroMoney = new CurrencyMoney(25, 25, "EUR", 1.2f);
            euroMoney.Display();

            euroMoney.updateExchangeRate(1.5f); }
    }
}
