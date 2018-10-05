using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorinis1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            Program p = new Program();
            List<Ziedas> ziedai = p.SkaitytiZieduDuomenis();

            Ziedas brangiausias = p.BrangiausiasZiedas(ziedai);
            Console.WriteLine("Brangiausias žiedas\n\n" + "Pavadinimas: " + brangiausias.Pavadinimas + "\n" + "Metalas: " + brangiausias.Metalas +
                "\n" + "Skersmuo: " + brangiausias.Dydis + "\n" + "Svoris: " + brangiausias.Svoris + "\n" + "Praba: " + brangiausias.Praba + "\n");

            Console.WriteLine(p.DaugiausiaZiedu(ziedai));

            List<Ziedas> pigus = p.BaltoAuksoPigesniZiedai(ziedai);
            p.SpausdintPigesniusZiedus(pigus);

            List<Ziedas> tarpKainu = p.ZiedaiTarpKainu(ziedai);                     
            p.SpausdintTarpKainu(tarpKainu);

            

            Console.ReadKey();
        }
        List<Ziedas> SkaitytiZieduDuomenis()
        {
            List<Ziedas> ziedai = new List<Ziedas>();
     
            string[] lines = File.ReadAllLines(@"L1Data.csv");
            foreach (string line in lines)
            {
                string[] values = line.Split(',');
                string gamintojas = values[0];
                string pavadinimas = values[1];
                string metalas = values[2];
                double svoris = Convert.ToDouble(values[3]);
                double dydis = Convert.ToDouble(values[4]);
                int praba = int.Parse(values[5]);
                double kaina = Convert.ToDouble(values[6]);

                Ziedas ziedas = new Ziedas(gamintojas, pavadinimas, metalas, svoris, dydis, praba,kaina);
                ziedai.Add(ziedas);
            }
            return ziedai;
        }
        public Ziedas BrangiausiasZiedas(List<Ziedas> ziedai)
        {
            double brangiausias = 0;
            int indexas = 0; // brangiausio žiedo indexas(vieta liste).
            for (int i = 0; i < ziedai.Count; i++)
            {
                if (ziedai[i].Kaina > brangiausias)
                {
                    brangiausias = ziedai[i].Kaina;
                    indexas = i;
                }

            }
                return ziedai[indexas];
        }
        string DaugiausiaZiedu(List<Ziedas> ziedai)
        {
            int max = 0;
            int pasikartoja = 0;
            int praba = 0;
            for (int i = 0; i < ziedai.Count; i++)
            {
                foreach (Ziedas ziedas in ziedai)
                {
                    if (ziedai[i].Praba == ziedas.Praba)
                    {
                        pasikartoja++;
                    }
                }
                if (pasikartoja >= max)
                {
                    max = pasikartoja;
                    praba = ziedai[i].Praba;
                }
                pasikartoja = 0;
            }
            string atsakymas = "Daugiausia yra " + praba + " prabos žiedų, o jų kiekis yra " + max;
            return atsakymas;

        }
        List<Ziedas> BaltoAuksoPigesniZiedai(List<Ziedas> ziedai)
        {
            List<Ziedas> pigus = new List<Ziedas>();

            foreach (var ziedas in ziedai)
            {
                if (ziedas.Metalas == "Baltas auksas" && ziedas.Kaina < 300)
                {
                    pigus.Add(ziedas);
                }
            }
            return pigus;
        }
        public void SpausdintPigesniusZiedus(List<Ziedas> pigus)
        {
            using (StreamWriter writer = new StreamWriter("BA300.csv"))
            {
                foreach (var ziedas in pigus)
                {
                    writer.WriteLine(ziedas.Gamintojas + ziedas.Pavadinimas + ziedas.Metalas +
                        ziedas.Svoris + ziedas.Dydis + ziedas.Praba + ziedas.Kaina);
                }
            }
        }
        List<Ziedas> ZiedaiTarpKainu(List<Ziedas> ziedai)
        {
            List<Ziedas> tarpKainu = new List<Ziedas>();
            int count = 0;
            foreach (var ziedas in ziedai)
            {
                if(count >= 3)
                {
                    break;
                }
                if(ziedas.Kaina <= 500 && ziedas.Kaina >= 300)
                {
                    tarpKainu.Add(ziedas);
                    count++;
                }
            }
            return tarpKainu;
        }
        public void SpausdintTarpKainu(List<Ziedas> tarpKainu)
        {
            using (StreamWriter writer = new StreamWriter("rezultatai.csv"))
            {
                writer.WriteLine("-");
                writer.WriteLine("{1, 8} {2, 5} {3, 8} {4, 4} {5, 4} {6, 4} {7, 5}", "Gamintojas", "Pavadinimas", "Metalas", "Svoris", "Dydis", "Praba", "Kaina");
                foreach (var ziedas in tarpKainu)
                {
                    
                    writer.WriteLine("| {1, 8}|" + "|{2, 5}|" + "|{3, 8}|" + "|{4, 4}|" + "|{5, 4}|" + "|{6, 4}|" + "|{7, 5}|", ziedas.Gamintojas, ziedas.Pavadinimas, ziedas.Metalas, ziedas.Svoris, ziedas.Dydis, ziedas.Praba, ziedas.Kaina);
                }
                writer.WriteLine("-----------------------");
            }
        }



    }
}
    
