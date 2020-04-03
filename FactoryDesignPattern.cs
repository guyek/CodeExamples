//FactoryDesignPattern
	public class Program
	{
		static void Main(string[] args)
		{
			CreditCard cardDetails = CreditCardFactory.GetCreditCard("Platinum");

			if (cardDetails != null)
			{
				Console.WriteLine("CardType : " + cardDetails.GetCardType());
				Console.WriteLine("CreditLimit : " + cardDetails.GetCreditLimit());
				Console.WriteLine("AnnualCharge :" + cardDetails.GetAnnualCharge());
			}
			else
			{
				Console.Write("Invalid Card Type");
			}

			Console.ReadLine();
		}
	}

	public interface CreditCard
	{
		string GetCardType();
		int GetCreditLimit();
		int GetAnnualCharge();
	}

	public class CreditCardFactory
	{
		public static CreditCard GetCreditCard(string cardType)
		{
			CreditCard cardDetails = null;

			if (cardType == "MoneyBack")
			{
				cardDetails = new MoneyBack();
			}
			else if (cardType == "Titanium")
			{
				cardDetails = new Titanium();
			}
			else if (cardType == "Platinum")
			{
				cardDetails = new Platinum();
			}

			return cardDetails;
		}
	}

	public class MoneyBack : CreditCard
	{
		public string GetCardType()
		{
			return "MoneyBack";
		}

		public int GetCreditLimit()
		{
			return 15000;
		}

		public int GetAnnualCharge()
		{
			return 500;
		}
	}

	public class Titanium : CreditCard
	{
		public string GetCardType()
		{
			return "Titanium Edge";
		}

		public int GetCreditLimit()
		{
			return 25000;
		}

		public int GetAnnualCharge()
		{
			return 1500;
		}
	}

	public class Platinum : CreditCard
	{
		public string GetCardType()
		{
			return "Platinum Plus";
		}
		public int GetCreditLimit()
		{
			return 35000;
		}
		public int GetAnnualCharge()
		{
			return 2000;
		}
	}
