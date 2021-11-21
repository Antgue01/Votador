using System;
using System.Collections.Generic;
using System.IO;


namespace VoterTester
{
    class cmp : IComparer<KeyValuePair<double, int>>
    {
        public int Compare(KeyValuePair<double, int> a, KeyValuePair<double, int> b)
        {
            if (a.Key < b.Key)
                return 1;
            else if (a.Key > b.Key)
                return -1;
            else return 0;
        }
    }
    class Program
    {
        static void swap(int a, int b, KeyValuePair<double, int>[] pair)
        {
            KeyValuePair<double, int> aux = pair[a];
            pair[a] = pair[b];
            pair[b] = aux;
        }
        static void swapKeys(ref KeyValuePair<double, int>a,ref KeyValuePair<double, int> b)
        {
            double aux = a.Key;
            a = new KeyValuePair<double, int>(b.Key, a.Value);
            b = new KeyValuePair<double, int>(aux, b.Value);
        }

        static void Main(string[] args)
        {
            Random r = new Random();
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

            int digits = 1, auxmiembros = miembros / 10;
            while (auxmiembros > 0)
            {
                auxmiembros /= 10;
                digits++;
            }
            KeyValuePair<double, int>[] probabilidades = new KeyValuePair<double, int>[4];
            int votos = -1;
            Console.WriteLine("");
            string[] STRINGS = { "SI", "NO", "ABSTENCIÓN", "NR" };
            int limite = 100;
            int i = 0;
            int papeletas = miembros * muebles;
            while (i < STRINGS.Length - 1 && limite > 0)
            {
                do
                {
                    Console.WriteLine("Probabilidad de votar " + STRINGS[i] + " [0," + limite + "]: ");
                    parsed = int.TryParse(Console.ReadLine(), out votos);
                } while (!parsed || votos < 0 || votos > limite);
                limite -= votos;

                probabilidades[i] = (new KeyValuePair<double, int>(Math.Round(papeletas * votos / 100.0), i));
                votos = -1;
                Console.WriteLine("");
                i++;
            }

            for (int y = i; y < STRINGS.Length; y++)
            {
                probabilidades[y] = (new KeyValuePair<double, int>(Math.Round(limite / 100.0 * papeletas), y));
            }
            swapKeys(ref probabilidades[2],ref probabilidades[3]);
            Array.Sort(probabilidades, new cmp());
            int index = -1;
            int t = 0;
            while (index < 0 && t < probabilidades.Length)
            {
                if (probabilidades[t].Key < 1)
                    index = t;
                t++;
            }
            Directory.CreateDirectory("TestLists");
            int fin = index < 0 ? probabilidades.Length-1 : index;
            for (int h = 0; h < miembros; h++)
            {
                StreamWriter writer = new StreamWriter(string.Format("TestLists/colegial{0:D" + digits + "}.voto", h + 1));
                writer.WriteLine(muebles);
                for (int j = 0; j < muebles; j++)
                {
                    int rand = r.Next(0, fin);
                    writer.WriteLine(string.Format("áàéèíìñ-óòúù {0:D3}", j + 1) + "+" + probabilidades[rand].Value);
                    probabilidades[rand] = new KeyValuePair<double, int>(probabilidades[rand].Key - 1, probabilidades[rand].Value);
                    if (probabilidades[rand].Key < 1)
                    {
                        swap(fin, rand, probabilidades);
                        fin--;
                    }
                }
                writer.Close();
            }
        }
    }
}
