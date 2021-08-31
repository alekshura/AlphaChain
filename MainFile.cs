using System;

namespace AlphaChain
{
	
	class MainClass
	{		
        static readonly int Levels = 70;
		static readonly int Nuclei = 7;
		static readonly int IZ = 112;
		static readonly int IN = 165;
         
		 
		public MainClass()
		{
            ReadData.nLevels = Levels;
            ReadData.HowMuchNuclei = Nuclei;

            Probabilities.Levels = Levels;
            Probabilities.Nuclei = Nuclei;
		
		}

		[STAThread]
		static void Main(string[] args)
		{
			
			MainClass main = new MainClass();

            int state  = 0;
			int nuclei = 0;

			main.WorkData(state, nuclei);
                   
			Console.ReadLine();
                
		}

		private void WorkData(int state, int nuclei)
		{
			int NextState, NextNuclei;
			int EndState  = new int();
			double Qatr = new double();
			double Talpha = new double();

			if (state < Levels && nuclei < Nuclei - 1)
			{
				 Probabilities prob = new Probabilities(IN, IZ);
				 ReadData rd = new ReadData(IN, IZ);
                 PrintResults print = new PrintResults();
				 string tr = "null";

				if (prob.IsIsomeric(state, nuclei, ref Qatr, ref Talpha))
				{
					NextState = prob.GetTransitionWay(state,nuclei);
					NextNuclei = nuclei + 1;
					
					Console.WriteLine("Is state {0} in {1} E = {2} -> {3} in {4}",
						rd.Nils[nuclei, state], nuclei, rd.E[nuclei, state], rd.E[NextNuclei, NextState],
						NextNuclei);

                    Console.WriteLine("Qa_t = {0,5:F2}, Ta = {1,7:F6}", Qatr, Talpha); 
					print.OutputToFile("Qatr.dat","Qatr = ", Qatr,"Ta = ", Talpha);
					
					nuclei= NextNuclei;
					state = NextState; 

					WorkData(state,nuclei);			
				}
				else
				{
					prob.GetMaxProbability(state, nuclei, ref EndState, ref tr);
					Console.WriteLine(tr + " {0} {1} -> {2} {3} in {4}", rd.E[nuclei,state],
						                                         rd.Nils[nuclei, state],
						                                         rd.E[nuclei, EndState],
						                                         rd.Nils[nuclei, EndState], nuclei);
                    
					if (prob.IsIsomeric(EndState, nuclei, ref Qatr, ref Talpha))
					{
						NextState = prob.GetTransitionWay(EndState, nuclei);
						nuclei += 1;
						state = NextState;
						
						print.OutputToFile("Qatr.dat", "Qatr = ", Qatr,"Ta = ", Talpha);
						Console.WriteLine("Qa_t = {0,5:F2}, Ta = {1,7:F4}", Qatr, Talpha);
			
					}
					else
					{
				      prob.GetMaxProbability(state, nuclei, ref EndState, ref tr);
 	                  state = EndState;
					
					}

					WorkData(state, nuclei);
				}
			}		
		}
	}
}
