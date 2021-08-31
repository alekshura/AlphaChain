using System;
using System.IO;

namespace AlphaChain
{	
	public class PrintResults
	{
		public PrintResults()
		{
		}

		public void  OutputToFile(string fileName, string desc, double value1)
		{
		    StreamWriter sw = File.AppendText(fileName);
            string s = desc + "{0}";
            sw.WriteLine(s,value1);
            sw.Close();		
		}

		public void  OutputToFile(string fileName, string des1, double value1, string des2, double value2)
		{
			string s = des1 + "{0,5:F4}  " + des2 + "{1,5:F4}";

			StreamWriter sw =  File.AppendText(fileName);
			sw.WriteLine(s, value1, value2);
			sw.Close();		
		}
	}
}
