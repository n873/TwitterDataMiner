namespace SMDST_Service
{
    static class Program
	{
		static void Main()
		{
#if DEBUG
			var SMDSTService = new Service1();
            SMDSTService.OnDebug();
			System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite);
#else
			ServiceBase[] ServicesToRun;
			ServicesToRun = new ServiceBase[] 
			{ 
				new Service1()
			};
			ServiceBase.Run(ServicesToRun);
#endif
		}
	}
}
