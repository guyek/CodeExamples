//FactoryMethodDesignPattern
class Program
{
	static void Main(string[] args)
	{
		var creditCard = new PlatinumFactory().CreateProduct();
		
		if (creditCard != null)
		{
			Console.WriteLine("Card Type : " + creditCard.GetCardType());
			Console.WriteLine("Credit Limit : " + creditCard.GetCreditLimit());
			Console.WriteLine("Annual Charge :" + creditCard.GetAnnualCharge());
		}
		else
		{
			Console.Write("Invalid Card Type");
		}
		
		Console.WriteLine("--------------");
		
		creditCard = new MoneyBackFactory().CreateProduct();
		
		if (creditCard != null)
		{
			Console.WriteLine("Card Type : " + creditCard.GetCardType());
			Console.WriteLine("Credit Limit : " + creditCard.GetCreditLimit());
			Console.WriteLine("Annual Charge :" + creditCard.GetAnnualCharge());
		}
		else
		{
			Console.Write("Invalid Card Type");
		}
		
		Console.ReadLine();
	}
}

public abstract class CreditCardFactory
{
	protected abstract CreditCard MakeProduct();

	public CreditCard CreateProduct()
	{
		return MakeProduct();
	}
}

public interface CreditCard
{
	string GetCardType();
	int GetCreditLimit();
	int GetAnnualCharge();
}

public class MoneyBackFactory : CreditCardFactory
{
	protected override CreditCard MakeProduct()
	{
		var product = new MoneyBack();
		return product;
	}
}

public class PlatinumFactory : CreditCardFactory
{
	protected override CreditCard MakeProduct()
	{
		var product = new Platinum();
		return product;
	}
}

public class TitaniumFactory : CreditCardFactory
{
	protected override CreditCard MakeProduct()
	{
		var product = new Titanium();
		return product;
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
