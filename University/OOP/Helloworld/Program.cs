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
		public Money TaxWithholdingCalculations (float taxRate)
		{
			long sum_withoutTax = (Euros*100+Cents) / (1+taxRate)
			Money withoutTax = new Money(Euros/taxRate);
		}
		public InterestAccurualCalculations (float interestRate)
		{

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
