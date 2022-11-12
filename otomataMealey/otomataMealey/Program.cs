using System.IO;
using System.Collections;
namespace otomataMealey
{
    internal class Program
    {
        static int active_State = 0;
        static int[,] table;
        static int[,] Output_table;
        static int qsayac = 0, inputsayac = 0;
        static ArrayList inputs = new ArrayList();
        static void read_count()
        {
            int sayac = 0;
            FileStream fs = new FileStream("INPUT.txt", FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            string line = sr.ReadLine();
            while (line != null)
            {
                for (int i = 0; i < line.Length; i++)
                {
                    if (sayac == 0)
                    {
                        if (line.Substring(i, 1) == "q")
                            qsayac++;
                    }
                    else if (sayac == 1)
                    {
                        line = line.Replace('\t', ' ');
                        if ((int)char.Parse(line.Substring(i, 1)) >= 48 && (int)char.Parse(line.Substring(i, 1)) <= 57)
                        {
                            inputsayac++;
                            inputs.Add(line.Substring(i, 1));
                        }
                    }
                }
                sayac++;
                line = sr.ReadLine();
            }
            table = new int[qsayac, inputsayac];
            Output_table = new int[qsayac, inputsayac];
            sr.Close();
            fs.Close();

        }
        static string stateitem = "State \t\t";
        static string outputitem = "Output \t\t";
        static void read_Txt()
        {
            string kopar = "";
            int sayac = 0, s = 1;
            int columindex = 0, rowindex = 0;
            FileStream fs = new FileStream("GECISDIYAGRAMI.txt", FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            string line = sr.ReadLine();
            line = sr.ReadLine();
            while (line != null)
            {
                s = 1;
                line += "\t";
                for (int k = 0; k < line.Length; k++)
                {
                    char character = line[k];
                    if (character == 'q')
                    {
                        kopar = "";
                    }
                    else if (((int)character >= 48 && (int)character <= 57))
                    {
                        kopar += line.Substring(k, 1);
                    }
                    else if (character == '\t')
                    {
                        if (s == 1)
                        {
                            s++;
                            kopar = "";
                            continue;
                        }
                        Output_table[rowindex, columindex] = Convert.ToInt32(kopar);
                        columindex++;
                    }
                    else if (character == '/')
                    {
                        table[rowindex, columindex] = Convert.ToInt32(kopar);
                        kopar = "";
                    }
                }
                columindex = 0;
                rowindex++;
                line = sr.ReadLine();
            }
        }

        static void Durum_kontrol(string ifade)
        {
            string karakter;
            int temp;
            stateitem += "q" + active_State + "\t";
             outputitem += "\t";
            for (int i = 0; i < ifade.Length; i++)
            {
                karakter = ifade.Substring(i, 1);
                int ind = inputs.IndexOf(karakter);
                temp = active_State;
                active_State = table[active_State, ind];
                stateitem += "q" + active_State + "\t";
                outputitem += Output_table[temp, ind].ToString() + "\t";           
            }
        }
        static void Main(string[] args)
        {
            string baslik = "";
            string items = "";
            string ifadeitem = "";
            string ifade = "";
            int check = 1;
            read_count();
            read_Txt();
            Console.Write("İfade Giriniz: ");
            ifade = Console.ReadLine();
            Console.WriteLine("");
            for (int i = 0; i < ifade.Length; i++)
            {
                if (inputs.IndexOf(ifade.Substring(i, 1)) == -1)
                {
                    Console.WriteLine("Yanlış ifade girdiniz. Tekrar giriniz!\n");
                    check = 0;
                    break;
                }
                else check = 1;
            }
            if (check == 1)
            {
                baslik = "\t";
                for (int i = 0; i < inputs.Count; i++)
                {
                    baslik += "\t|After input " + inputs[i] + "\t\t";
                }
                Console.WriteLine(baslik);
                baslik = "";
                baslik += "Old State\t";
                for(int i=0; i<inputs.Count; i++)
                {
                    baslik += "|New State\t|Output\t\t";
                }
                Console.WriteLine(baslik);
                for(int i=0; i<qsayac; i++)
                {
                    items += "q" + i + "\t\t";
                    for(int j=0; j<inputsayac; j++)
                    {
                        items += "|q" + table[i, j] + "\t\t|" + Output_table[i, j] + "\t\t";
                    }
                    Console.WriteLine(items);
                    items = "";
                }
                Durum_kontrol(ifade);
                ifadeitem = "Input String\t";
                for(int i=0; i<ifade.Length; i++)
                {
                    ifadeitem += "\t" + ifade[i];
                }
                 Console.Write("\n" + ifadeitem + "\n" + stateitem + "\n" + outputitem + "\n\n");
            }
        }
    }
}