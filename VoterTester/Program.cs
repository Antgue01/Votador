using System;
using System.IO;

namespace VoterTester
{
    class Program
    {
        static void Main(string[] args)
        {
            Random r = new Random(DateTime.Now.Second + DateTime.Now.Minute * 60 + DateTime.Now.Hour * 60); ;
            int miembros, muebles;
            bool parsed = false;
            do
            {
                Console.WriteLine("Número de miembros del consejo: ");
                parsed = int.TryParse(Console.ReadLine(), out miembros);

            }
            while (!parsed || miembros <= 0);
            parsed = false;
            Console.WriteLine("");
            do
            {
                Console.WriteLine("Número de muebles a votar [1,106]: ");
                parsed = int.TryParse(Console.ReadLine(), out muebles);
            } while (!parsed || muebles <= 0 || muebles > 106);

            int digits=1,auxmiembros =miembros / 10;
            while (auxmiembros > 0)
            {
                auxmiembros /= 10;
                digits++;
            }
            Directory.CreateDirectory("TestLists");
            for (int i = 0; i < miembros; i++)
            {
                StreamWriter writer = new StreamWriter(string.Format("TestLists/colegial{0:D" + digits + "}.voto", i + 1));
                writer.WriteLine(muebles);
                for (int j = 0; j < muebles; j++)
                {
                    writer.WriteLine(string.Format("áàéèíìñ-óòúù {0:D3}", j + 1) + "+" + r.Next(0, 4));
                }
                writer.Close();
            }
        }
    }
}
