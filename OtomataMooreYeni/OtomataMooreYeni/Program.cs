namespace OtomataMooreYeni
{
    using System.Collections;
    using System.IO;
    internal class Program
    {
        static int active_State = 0;
        static int[,] table;
        static int[] Output_table;
        static int qsayac = 0, inputsayac = 0;
        static ArrayList inputs = new ArrayList();
        static void read_Count()
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
                        if ((int)char.Parse(line.Substring(i, 1)) >= 97 && (int)char.Parse(line.Substring(i, 1)) <= 122)
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
            Output_table = new int[qsayac];
            sr.Close();
            fs.Close();
        }
        static void Read_Txt()
        {
            string kopar = "";
            int sayac = 0, s = 1;
            int columnindex = 0, rowindex = 0;
            FileStream fs = new FileStream("GECISTABLOSU.txt", FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            string line = sr.ReadLine();
            line = sr.ReadLine();
            while (line != null)
            {
                s = 1;
                line += "\t";
                for (int k = 0; k < line.Length; k++)
                {
                    if ((int)char.Parse(line.Substring(k, 1)) >= 48 && (int)char.Parse(line.Substring(k, 1)) <= 57)
                    {
                        kopar += line.Substring(k, 1);
                    }
                    else if (char.Parse(line.Substring(k, 1)) == '\t')
                    {
                        if (s == 1)
                        {
                            s++;
                            kopar = "";
                            continue;
                        }
                        table[rowindex, columnindex] = Convert.ToInt32(kopar);
                        columnindex++;
                        kopar = "";
                    }
                }
                columnindex = 0;
                rowindex++;
                line = sr.ReadLine();
            }
            sr.Close();
            fs.Close();
            FileStream fsoutput = new FileStream("OUTPUT.txt", FileMode.Open, FileAccess.Read);
            StreamReader sroutput = new StreamReader(fsoutput);
            string lineoutput = sroutput.ReadLine();
            lineoutput = sroutput.ReadLine();
            while (lineoutput != null)
            {
                string output_kes = lineoutput.Substring(lineoutput.Length - 1, 1);
                Output_table[sayac] = Convert.ToInt32(output_kes);
                sayac++;
                lineoutput = sroutput.ReadLine();
            }
            sroutput.Close();
            fsoutput.Close();
        }


        static void Durum_kontrol(string ifade)
        {
            string karakter;
            stateitem += "q" + active_State + "\t";
            outputitem += Output_table[active_State].ToString() + "\t";
            for (int i = 0; i < ifade.Length; i++)
            {
                karakter = ifade.Substring(i, 1);
                int ind = inputs.IndexOf(karakter);
                active_State = table[active_State, ind];
                stateitem += "q" + active_State + "\t";
                outputitem += Output_table[active_State].ToString() + "\t";
            }
        }

        static string stateitem = "State \t\t";
        static string outputitem = "Output \t\t";
        static void Main(string[] args)
        {
            string ifade = "";
            int check = 1;
            read_Count();
            Read_Txt();
            while (true)
            {
                string baslik = "";
                string items = "";
                string ifadeitem = "";
                active_State = 0;
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
                    baslik = "Old State:\t";
                    for (int i = 0; i < inputs.Count; i++)
                    {
                        baslik += "After Input " + inputs[i] + "\t";
                    }
                    baslik += "Output";
                    Console.WriteLine(baslik);

                    for (int i = 0; i < qsayac; i++)
                    {
                        items = "q" + i + "\t\t";
                        for (int j = 0; j < inputsayac; j++)
                        {
                            items += "q" + table[i, j] + "\t\t";
                        }
                        items += Output_table[i];
                        Console.WriteLine(items);
                    }
                    ifadeitem += "Input String\t";
                    for (int i = 0; i < ifade.Length; i++)
                    {
                        ifadeitem += "\t" + ifade.Substring(i, 1);
                    }
                    Durum_kontrol(ifade);
                    Console.Write("\n" + ifadeitem + "\n" + stateitem + "\n" + outputitem + "\n\n");
                }
            }
        }
    }
}