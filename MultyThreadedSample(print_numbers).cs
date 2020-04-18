class Program
{
	// upto the limit numbers will be printed.
	const int numberLimit = 10;
	static object monitor = new object();

	static void Main(string[] args)
	{
		var oddThread = new Thread(Odd);
		var evenThread = new Thread(Even);

		//Start even thread.
		evenThread.Start();

		//puase for 10 ms, to make sure even thread has started
		//or else odd thread may start first resulting other sequence.
		Thread.Sleep(100);

		//Start odd thread.
		oddThread.Start();
		oddThread.Join();
		evenThread.Join();
	}

	//printing of Odd numbers
	static void Odd()
	{
		try
		{
			//hold lock as console is shared between threads.
			Monitor.Enter(monitor);
			
			for (int i = 1; i <= numberLimit; i = i + 2)
			{
				//Complete the task ( printing odd number on console)
				Console.Write(" " + i);
				//Notify other thread that is to eventhread
				//that I'm done you do your job
				Monitor.Pulse(monitor);

				//I will wait here till even thread notify me
				// Monitor.Wait(monitor);

				// without this logic application will wait forever
				bool isLast = i == numberLimit - 1;

				if (!isLast)
				{
					Monitor.Wait(monitor); //I will wait here till even thread notify me
				}
			}
		}
		finally
		{
			//Release lock
			Monitor.Exit(monitor);
		}
	}

	//printing of even numbers
	static void Even()
	{
		try
		{
			//hold lock
			Monitor.Enter(monitor);
			for (int i = 0; i <= numberLimit; i = i + 2)
			{
				//Complete the task ( printing even number on console)
				Console.Write(" " + i);
				//Notify other thread- here odd thread
				//that I'm done, you do your job
				Monitor.Pulse(monitor);
				//I will wait here till odd thread notify me
				// Monitor.Wait(monitor);

				bool isLast = i == numberLimit;

				if (!isLast)
				{
					Monitor.Wait(monitor);
				}
			}
		}
		finally
		{
			Monitor.Exit(monitor);//release the lock
		}

	}
}
