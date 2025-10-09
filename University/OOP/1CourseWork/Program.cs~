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
		}
		
		public void Display()
		{
			Console.WriteLine($"This sum is {Euros},{Cents}");
		}

		public void Addition (long additional_euros, byte additional_cents)
		{
			Euros += additional_euros;
			Cents += additional_cents;
		}
		public void Substration (long minus_euros, byte minus_cents)
		{	
			if (Euros - minus_euros >= 0 || (Euros == 0 && Cents - minus_cents < 0))
			{
				Euros -= minus_euros;
				Cents -= minus_cents;
			}
		       	else { Console.WriteLine("Not enouth money! Operation failed.");}
		}
		public float InvestmentReturnAnalysis (Money initialInvestment)
		{
			float returnMultiplier = Euros / initialInvestment.Euros;
			return returnMultiplier;
		}
		public void Money TaxWithholdingCalculations (float taxRate)
		{
			long sum_withoutTax = (Euros*100+Cents) / (1+taxRate);
			Console.WriteLine($"The price without taxes is {sum_withoutTax/100}"); 
		}
		public void InterestAccurualCalculations (float interestRate)
		{
			long finalValue = (Euros*100+Cents) * (1+interestRate);
			Console.WriteLine($"The current money will turn into {finalValue} with {interstRate} interest rate");
		}
		public void BudgetVsActualSpending(Money spending)
		{
			long currentSum = Euros*100+Cents;
			long spendingSum = spending.Euros*100+Cents;
			string output;
			if (currentSum == spendingSum) { output = "Just enouth, but is it worth it?"; }
			else if (currentSum > spendingSum) { output = "You have more that you spend, good job."; }
			else { output = "You spend more than you have, please control your spending more."; }
			Console.WriteLine(output);
		}
	}

	class Program
	{
		public static void Main(string[] args)
		{
			Console.WriteLine($"The programm is running by Antkurom and current time is {DateTime.Now}");
		}
	}
}
