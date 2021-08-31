using System;

namespace AlphaChain
{
    /// <summary>
    /// This class calculates probabilities of one-quasiparticle transitions.
    /// Shura
    /// </summary>
    public class Probabilities
    {
        public Probabilities()
        {
            IN = 0;
            IZ = 0;
        }

        public Probabilities(int IN, int IZ)
        {
            this.IN = IN;
            this.IZ = IZ;
        }

        private readonly int IZ, IN;
        public static int Levels, Nuclei;

        private double TE1, TE2, TE3, TE4, TE5;
        private double TM1, TM2, TM3, TM4, TM5;



        private void SetParameters(int nucleus)
        {
            int N = IN - 2 * nucleus;
            int Z = IZ - 2 * nucleus;

            double IA = Z + N;

            double BE1 = 6.446E-2 * Math.Pow(IA, (2.0 / 3.0));
            double BE2 = 5.940E-2 * Math.Pow(IA, (4.0 / 3.0));
            double BE3 = 5.940E-2 * Math.Pow(IA, (2.0));
            double BE4 = 6.285E-2 * Math.Pow(IA, (8.0 / 3.0));
            double BE5 = 6.928E-2 * Math.Pow(IA, (10.0 / 3.0));

            double BM1 = 1.790;
            double BM2 = 1.650 * Math.Pow(IA, (2.0 / 3.0));
            double BM3 = 1.650 * Math.Pow(IA, (4.0 / 3.0));
            double BM4 = 1.746 * Math.Pow(IA, (2.0));
            double BM5 = 1.924 * Math.Pow(IA, (8.0 / 3.0));

            TE1 = 1.587E15 * BE1;
            TE2 = 1.223E9 * BE2;
            TE3 = 5.698E2 * BE3;
            TE4 = 1.694E-4 * BE4;
            TE5 = 3.451E-11 * BE5;

            TM1 = 1.779E13 * BM1;
            TM2 = 1.371E7 * BM2;
            TM3 = 6.387E0 * BM3;
            TM4 = 1.899E-6 * BM4;
            TM5 = 3.868E-13 * BM5;
        }



        private double GetProbability(int state1, int state2, int nuclei, ref string tr)
        {
            int spin1 = new int();
            int spin2 = new int();
            double E1 = new double();
            double E2 = new double();
            double parity1 = new double();
            double parity2 = new double();
            ReadData rd = new ReadData(IN, IZ);
           
            SetParameters(nuclei);

            rd.ReadString(state1, nuclei, ref E1, ref spin1, ref parity1);
            rd.ReadString(state2, nuclei, ref E2, ref spin2, ref parity2);

            int spin = Math.Abs((spin1 - spin2) / 2);
            double parity = parity1 * parity2;

            double DeltaE = Math.Abs(E1 - E2);

            double Probability;
            switch (spin)
            {
                case 1:
                    if (parity.Equals(-1.0))
                    {
                        Probability = this.TE1 * Math.Pow(DeltaE, 3.0);
                        tr = "E1";
                    }
                    else
                    {
                        Probability = this.TM1 * Math.Pow(DeltaE, 3.0);
                        tr = "M1";
                    }
                    break;
                case 2:
                    if (parity.Equals(-1.0))
                    {
                        Probability = this.TM2 * Math.Pow(DeltaE, 5.0);
                        tr = "M2";
                    }
                    else
                    {
                        Probability = this.TE2 * Math.Pow(DeltaE, 5.0);
                        tr = "E2";
                    }
                    break;

                case 3:
                    if (parity.Equals(-1.0))
                    {
                        Probability = this.TE3 * Math.Pow(DeltaE, 7.0);
                        tr = "E3";
                    }
                    else
                    {
                        Probability = this.TM3 * Math.Pow(DeltaE, 7.0);
                        tr = "M3";
                    }
                    break;
                case 4:
                    if (parity.Equals(-1.0))
                    {
                        Probability = this.TM4 * Math.Pow(DeltaE, 9.0);
                        tr = "M4";
                    }
                    else
                    {
                        Probability = this.TE4 * Math.Pow(DeltaE, 9.0);
                        tr = "E4";
                    }
                    break;
                case 5:
                    if (parity.Equals(-1.0))
                    {
                        Probability = this.TE5 * Math.Pow(DeltaE, 11.0);
                        tr = "E5";
                    }
                    else
                    {
                        Probability = this.TM5 * Math.Pow(DeltaE, 11.0);
                        tr = "M5";
                    }
                    break;
                default:
                    {
                        Probability = 0;
                        tr = "isomeric";
                    }
                    break;
            }

            return Probability;
        }



        public double GetMaxProbability(int state, int nuclei, ref int EndState, ref string tr)
        {
            double probability;
            double MaxProb = new double();
            string tr1 = "null";

            if (state > 0)
            {
                for (int i = state - 1; i >= 0; i--)
                {
                    probability = GetProbability(state, i, nuclei, ref tr1);
                    if (probability > MaxProb)
                    {
                        MaxProb = probability;
                        EndState = i;
                        tr = tr1;

                    }
                }
            }

            return MaxProb;
        }

        public bool IsIsomeric(int state, int nuclei, ref double Qatr,
                                                    ref double Talpha)
        {
            double ltGamma, ltAlpha, Qalpha;
            int EndState = new int();
            int N = IN - 2 * nuclei;
            int Z = IZ - 2 * nuclei;
            double Ep = new double(), Ed = new double();
            string tr = "null";

            ReadData rd = new ReadData(IN, IZ);
            Qalpha = rd.Qa(N, Z);

            for (int i = 0; i < ReadData.nLevels; i++)
            {
                if (rd.Nils[nuclei, state].Equals(rd.Nils[nuclei + 1, i]))
                {
                    Ep = rd.E[nuclei, state];
                    Ed = rd.E[nuclei + 1, i];

                }
            }

            Qatr = Qalpha + Ep - Ed;

            ltAlpha = rd.Ta(Z, N, Qatr);
            ltGamma = 1.0 / GetMaxProbability(state, nuclei, ref EndState, ref tr);
            Talpha = ltAlpha;

            if (ltGamma < ltAlpha) return false;
            else
                return true;
        }

        public int GetTransitionWay(int state, int nuclei)
        {
            ReadData rd = new ReadData(IN, IZ);
            int ind = new int();

            for (int i = 0; i < ReadData.nLevels; i++)
            {
                if (rd.Nils[nuclei, state].Equals(rd.Nils[nuclei + 1, i])) ind = i;

            }

            return ind;
        }
    }
}