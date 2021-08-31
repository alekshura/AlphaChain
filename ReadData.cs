using System;
using System.IO;

namespace AlphaChain
{
    /// <summary>
    /// Summary description for Chain.
    /// </summary>
    public class ReadData
    {
        public ReadData()
        {

        }

        public ReadData(int IN, int IZ)
        {
            INstart = IN;
            IZstart = IZ;
            ReadDataFromFiles(HowMuchNuclei, nLevels);
        }


        private readonly int INstart;
        private readonly int IZstart;

        public static int nLevels;
        public static int HowMuchNuclei;

        public string[,] Nils = new string[HowMuchNuclei, nLevels];
        public double[,] E = new double[HowMuchNuclei, nLevels];



        private void ReadDataFromFiles(int HowMuchNuclei, int nLevels)
        {
            string fileName = "null";
            string odd = "null";
            string path;
            string inputLine;
            string Ener, temp;
            int k = 0;
            for (int i = 0; i < HowMuchNuclei; i++)
            {
                int IZ = IZstart - 2 * i;
                int IN = INstart - 2 * i;
                // N odd
                if (Math.Pow(-1, IN) == -1)
                {
                    path = @"../../data/NEUT/";
                    fileName = path + IZ.ToString();
                    odd = "even-odd";
                }
                // Z odd
                if (Math.Pow(-1, IZ) == -1)
                {
                    path = @"../../data/PROT/";
                    fileName = path + IN.ToString();
                    odd = "odd-even";
                }


                if (Math.Pow(-1, IZ) == -1 && Math.Pow(-1, IN) == -1)
                {
                    path = @"../../data/ODD-ODD/";
                    fileName = path + IN.ToString() + "-" + IZ.ToString();
                    odd = "odd-odd";
                }

                StreamReader sr = File.OpenText(fileName);

                int EndLine;
                if (odd.Equals("odd-odd")) EndLine = 16;
                else EndLine = 7;

                while (k < nLevels)
                {
                    inputLine = sr.ReadLine();
                    temp = inputLine.Substring(0, 3);
                    Ener = temp + "," + inputLine.Substring(4, 5);

                    Nils[i, k] = inputLine.Substring(10, EndLine);
                    E[i, k] = double.Parse(Ener);
                    k++;
                }
                k = 0;
                sr.Close();
            }
        }

        public void ReadString(int state, int nuclei, ref double Energy, ref int spin, ref double parity)
        {
            int Z = IZstart - 2 * nuclei;
            int N = INstart - 2 * nuclei;


            Energy = E[nuclei, state];
            if (Math.Pow(-1, N) == -1 && Math.Pow(-1, Z) == -1)
            {

                int ind_n1 = Nils[nuclei, state].IndexOf("[");
                int ind_n2 = Nils[nuclei, state].IndexOf("]");

                string prot = Nils[nuclei, state].Substring(ind_n2 + 1);
                string neut = Nils[nuclei, state].Substring(0, ind_n2);

                int ind_p1 = prot.IndexOf("[");
                int ind_p2 = prot.IndexOf("]");

                int On = int.Parse(neut.Substring(0, ind_n1));
                int Op = int.Parse(prot.Substring(0, ind_p1));

                int Nn = int.Parse(neut.Substring(ind_n1 + 1, 1));
                int Np = int.Parse(prot.Substring(ind_p1 + 1, 1));

                int Ln = int.Parse(neut.Substring(ind_n2 - 1, 1));
                int Lp = int.Parse(prot.Substring(ind_p2 - 1, 1));

                int Sn = On - 2 * Ln;
                int Sp = Op - 2 * Lp;

                parity = Math.Pow(-1.0, Nn + Np);

                if (Sn * Sp > 0) spin = (On + Op);
                else spin = Math.Abs(On - Op);
            }
            else
            {
                string tmp = Nils[nuclei, state];
                int ind_n1 = tmp.IndexOf("[");
                int ind_n2 = tmp.IndexOf("]");

                int O = int.Parse(tmp.Substring(0, ind_n1));
                int NN = int.Parse(tmp.Substring(ind_n1 + 1, 1));

                spin = O;
                parity = Math.Pow(-1, NN);

            }
        }



        public double Qa(int N, int Z)
        {
            string fileName = @"../../Data/INPUT/obr_4_10k.dat";
            string Input, ZZ, NN, b2, b3, b4, b5, b6, b7, b8;
            string Edef, Esh, Mth, Mexp, Qath, Qaexp, Snth, Snexp, Spth, Spexp, Tath, Taexp, lgTath, lgTaexp;
            double Qalpha = new double();

            StreamReader sr = File.OpenText(fileName);

            int i = 0;
            while (i < 3000)
            {
                Input = sr.ReadLine();
                i += 1;
                ZZ = Input.Substring(0, 3);
                NN = Input.Substring(4, 3);
                b2 = Input.Substring(9, 5);
                b3 = Input.Substring(15, 5);
                b4 = Input.Substring(21, 5);
                b5 = Input.Substring(27, 5);
                b6 = Input.Substring(33, 5);
                b7 = Input.Substring(39, 5);
                b8 = Input.Substring(45, 5);

                Edef = Input.Substring(51, 6);
                Esh = Input.Substring(58, 6);
                Mth = Input.Substring(65, 7);
                Mexp = Input.Substring(73, 7);
                Qath = Input.Substring(81, 7);
                Qaexp = Input.Substring(89, 7);
                Snth = Input.Substring(97, 7);
                Snexp = Input.Substring(105, 7);
                Spth = Input.Substring(113, 7);
                Spexp = Input.Substring(121, 7);
                Tath = Input.Substring(129, 10);
                Taexp = Input.Substring(140, 10);
                lgTath = Input.Substring(151, 7);
                lgTaexp = Input.Substring(159, 6);

                if (Z == int.Parse(ZZ) && N == int.Parse(NN))
                {
                    string temp1, temp2;
                    temp1 = Qath.Substring(0, Qath.IndexOf("."));
                    temp2 = Qath.Substring(Qath.IndexOf(".") + 1);
                    Qalpha = double.Parse(temp1 + "," + temp2);
                }

            }
            return Qalpha;
        }

        public double Ta(int Z, int N, double Q)
        {
            //   smolanczuk and Sobiczewski 1995 St.Petersburg even-even 
            //	 parameters for Viola-Seaborg formula:	   
            //	   double  a =   1.8104;
            //	   double  b = -21.7199;
            //	   double  c = -0.26488;
            //	   double  d = -28.1319;
            //    end of set of parameters;

            //   Sobiczewski and Parkhomenko 
            //	 parameters for even-even Viola-Seaborg formula:
            double a = 1.5372;
            double b = -0.1607;
            double c = -36.5731;
            double TaLOG = a * Z / Math.Sqrt(Q) + b * Z + c;
            return Math.Pow(10.0, TaLOG);
        }
    }
}
