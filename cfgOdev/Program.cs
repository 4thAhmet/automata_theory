using System.Collections;
namespace cfgOdev
{
    internal class Program
    {
        static ArrayList uretilen = new ArrayList();
        static ArrayList tekrarlanan = new ArrayList();
        static ArrayList gecici = new ArrayList();
        static char x;
        static string[] yerinegececek;

        static void uret(ArrayList dizi)
        {
            ArrayList yedek = new ArrayList();

            foreach (string i in gecici)
            {
                foreach(string k in yerinegececek)
                {
                    yedek.Add(i + k);
                }
            }
            gecici = yedek;
        }
        static void func(string[] terminaller, string[] yerinegececekler,char karakter)
        {           
            foreach(string i in terminaller)
            {
                if(i.IndexOf(karakter) != -1)
                {
                    gecici.Add("");
                    foreach(char ch in i)
                    {
                        if(ch != karakter)
                        {
                            for(int k=0; k<gecici.Count; k++)
                            {
                                gecici[k] += ch.ToString();
                            }      
                        }
                        else uret(gecici);
                    }
                    foreach (string g in gecici)
                    {
                        uretilen.Add(g);
                    }
                    gecici.Clear();
                    
                }
                else
                {
                    uretilen.Add(i);
                }
            }

        }

        static void Main(string[] args)
        { 
            string CFG = "S-->aa|bX|aXX,X-->a|b";
            string[] parca = CFG.Split(',');
            string[] terminal = parca[0].Split('|');
            terminal[0] = terminal[0].Substring(4, terminal[0].Length-4);
            ArrayList temp = new ArrayList();
            yerinegececek = parca[1].Split('|');
            x = Convert.ToChar(yerinegececek[0].Substring(0, 1));
            yerinegececek[0] = yerinegececek[0].Substring(4,yerinegececek[0].Length-4);

            func(terminal, yerinegececek,x);

            foreach (string al in uretilen)
            {
                if(temp.IndexOf(al) == -1)
                {
                    temp.Add(al);
                }
                else
                {
                    tekrarlanan.Add(al);
                }
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("-------------------------");
            Console.WriteLine("Üretilenler: ");
            Console.ResetColor();

            foreach (string yaz in temp)
                Console.WriteLine(yaz);

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("-------------------------");
            Console.WriteLine("Tekrarlananlar: ");
            Console.ResetColor();

            foreach (string yaz in tekrarlanan)
                Console.WriteLine(yaz);

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("-------------------------");
            Console.ResetColor();
        }
    }
}