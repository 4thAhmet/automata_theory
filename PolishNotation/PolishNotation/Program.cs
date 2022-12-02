using System.Collections;

namespace PolishNotation
{
    internal class Program
    {
        static Stack stack = new Stack();
        static int convert(string ifade)
        {
            string sayi = "";
            for (int i = ifade.Length-1; i >= 0; i--)
            {
                char karakter = ifade[i];
                if((int)karakter >= 48 && (int)karakter<=57)
                {
                    sayi = karakter + sayi;
                }
                else if(karakter == ' ')
                {
                    if(sayi != "")
                    {
                        stack.Push(sayi);
                        sayi = "";
                    }
                }
                else if (karakter =='+' || karakter == '-' || karakter == '*' || karakter == '/')
                {
                    int s1 = Convert.ToInt32(stack.Pop());
                    int s2 = Convert.ToInt32(stack.Pop());
                    switch(karakter)
                    {
                        case '+':
                            stack.Push(s1 + s2);
                            break;
                        case '-':
                            stack.Push(s1 - s2);
                            break;
                        case '*':
                            stack.Push(s1 * s2);
                            break;
                        case '/':
                            stack.Push(s1 / s2);
                            break;
                    }
                }             
            }
            if (stack.Count == 1)
                return Convert.ToInt32(stack.Pop());
            else return 0;
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Çıkmak için 0 yazınız.");
            for(; ; )
            {
                Console.Write("İfade Giriniz: ");
                string ifade = Console.ReadLine();
                int deger = convert(ifade);
                if (ifade == "0") break;
                Console.WriteLine("Sonuç: " + deger.ToString()); 
            }

        }
    }
}