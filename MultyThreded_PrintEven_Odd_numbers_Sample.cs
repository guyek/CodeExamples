class Program
	{
		const int amount = 10;
		static object monitor = new object();
		static EventWaitHandle evenReady;
		static EventWaitHandle oddReady;

		/// <summary>
		/// Some info to read:
		/// Monitor vs lock
		/// https://stackoverflow.com/questions/4978850/monitor-vs-lock
		/// 
		/// Monitor vs Mutex in c# 
		/// https://stackoverflow.com/questions/1164038/monitor-vs-mutex-in-c-sharp
		/// </summary>
		/// <param name="args"></param>
		static void Main(string[] args)
		{
			DogCat();  // new style
			Thread.Sleep(100);
			Console.WriteLine();

			OddEvenThreadsNew(); // new lock instead of Monitor.Enter
			Thread.Sleep(100);  // to make sure that lines below will show up at the end. 
			Console.WriteLine();

			OddEvenThreadsPrintOld(); // old style
			Thread.Sleep(100);  // to make sure that lines below will show up at the end. 
			Console.WriteLine();
		}


		/// <summary>
		/// lock and event demo to switch threads
		/// https://stackoverflow.com/questions/33866234/print-even-and-odd-numbers-in-c-sharp-using-two-thread-ie-even-thread-and-odd-th
		/// </summary>
		private static void DogCat()
		{
			var oddThread = new Thread(CatOddThread);
			var evenThread = new Thread(DogEvenThread);

			evenReady = new AutoResetEvent(false);
			oddReady = new AutoResetEvent(true);

			evenThread.Start();
			Thread.Sleep(100); //pause for 10 ms, to make sure even thread has started or else odd thread may start first resulting other sequence.
			oddThread.Start();
		}

		private static void DogEvenThread(object obj)
		{
			for (int i = 0; i < amount; i += 2)
			{
				oddReady.WaitOne();
				Console.Write(" woof " + i);
				evenReady.Set();
			}
		}

		private static void CatOddThread(object obj)
		{
			for (int i = 1; i < amount; i += 2)
			{
				evenReady.WaitOne();
				Console.Write(" meow " + i);
				oddReady.Set();
			}
		}


		private static void OddEvenThreadsNew()
		{
			var oddThread = new Thread(OddNew);
			var evenThread = new Thread(EvenNew);

			evenThread.Start();
			Thread.Sleep(100); //pause for 10 ms, to make sure even thread has started or else odd thread may start first resulting other sequence.
			oddThread.Start();
		}

		static void OddNew()
		{
			lock (monitor)
			{
				for (int i = 1; i <= amount; i = i + 2)
				{
					Console.Write(" " + i);
					Monitor.Pulse(monitor); //Notify other thread that is to eventhread that I'm done you do your job

					// without this logic application will wait forever
					bool isLast = i == amount - 1;
					
					if (!isLast)
					{
						Monitor.Wait(monitor); //I will wait here till even thread notify me
					}
				}
			}
		}

		//printing of even numbers
		static void EvenNew()
		{
			lock (monitor)
			{
				for (int i = 0; i <= amount; i = i + 2)
				{
					Console.Write(" " + i);
					Monitor.Pulse(monitor);

					bool isLast = i == amount;

				if (!isLast)
				{
					Monitor.Wait(monitor);}
				}
			}
		}

		/// <summary>
		/// Old style thread switch
		/// Based on incorrect (!) example:
		/// https://www.interviewsansar.com/2016/06/17/print-even-and-odd-number-sequence-with-two-threads-in-csharp/
		/// </summary>
		private static void OddEvenThreadsPrintOld()
		{
			var oddThread = new Thread(Odd);
			var evenThread = new Thread(Even);

			evenThread.Start();
			Thread.Sleep(100); //pause for 10 ms, to make sure even thread has started or else odd thread may start first resulting other sequence.
			oddThread.Start();
		}

		static void Odd()
		{
			try
			{
				Monitor.Enter(monitor); //hold lock as console is shared between threads.
				for (int i = 1; i <= amount; i = i + 2)
				{
					Console.Write(" " + i);
					Monitor.Pulse(monitor); //Notify other thread that is to eventhread that I'm done you do your job

					// without this logic application will wait forever
					bool isLast = i == amount - 1;

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
				Monitor.Enter(monitor);
				for (int i = 0; i <= amount; i = i + 2)
				{
					Console.Write(" " + i);
					Monitor.Pulse(monitor);

					bool isLast = i == amount;

					if (!isLast)
					{
						Monitor.Wait(monitor);
					}
				}
			}
			finally
			{
				Monitor.Exit(monitor);
			}

		}

		//private static void DogCatSwitch()
		//{
		//    new Thread(Dog).Start();     // because go = false
		//    Thread.Sleep(100);
		//    new Thread(Cat).Start();
		//    lock (monitor)                 // Let's now wake up the thread by
		//    {                              // setting go = true and pulsing.
		//        go = true;
		//        Monitor.Pulse(monitor);
		//    }
		//}

		//static void Dog()
		//{
		//    try
		//    {
		//        Monitor.Enter(monitor); //hold lock as console is shared between threads.
		//        for (int i = 1; i <= amount; i = i + 2)
		//        {
		//            Console.Write(" " + i);
		//            Monitor.Pulse(monitor); //Notify other thread that is to eventhread that I'm done you do your job

		//            // without this logic application will wait forever
		//            bool isLast = i == amount - 1;
		//            if (!isLast)
		//                Monitor.Wait(monitor); //I will wait here till even thread notify me
		//        }
		//    }
		//    finally
		//    {
		//        //Release lock
		//        Monitor.Exit(monitor);
		//    }
		//}

		//static void Cat()
		//{
		//    try
		//    {
		//        Monitor.Enter(monitor);
		//        for (int i = 0; i <= amount; i = i + 2)
		//        {
		//            Console.Write(" " + i);
		//            Monitor.Pulse(monitor);

		//            bool isLast = i == amount;
		//            if (!isLast)
		//                Monitor.Wait(monitor);
		//        }
		//    }
		//    finally
		//    {
		//        Monitor.Exit(monitor);
		//    }

		//}
}
