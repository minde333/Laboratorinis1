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
            List<Ziedas> pigus = p.Pigesni300(ziedai);
            List<Ziedas> atitinka = p.ZiedaiIki500(ziedai);
            
            Console.WriteLine("brangiausias ziedas: " + brangiausias.Pavadinimas + " " + brangiausias.Metalas + " " + brangiausias.Dydis +
                " " + brangiausias.Svoris + " " + brangiausias.Praba);
            p.SpausdintPigius(pigus);
            p.ZiedaiIki500(ziedai);
            p.SpausdintAtitinka(atitinka);

            p.DaugiausiaZiedu(ziedai);

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
                double praba = Convert.ToDouble(values[5]);
                double kaina = Convert.ToDouble(values[6]);

                Ziedas ziedas = new Ziedas(gamintojas, pavadinimas, metalas, svoris, dydis, praba,kaina);
                ziedai.Add(ziedas);
            }
            return ziedai;
        }
        public Ziedas BrangiausiasZiedas(List<Ziedas> ziedai)
        {
            double brangiausias = 0;
            int indexas = 0;// brangiausio ziedo indexas(vieta liste).
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
            string praba;
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
            return praba;

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
        public void SpausdintPigius(List<Ziedas> pigus)
        {
            using (StreamWriter writer = new StreamWriter("BA300.csv"))
            {
                foreach (var ziedas in pigus)
                {
                    writer.WriteLine(ziedas.Pavadinimas + ziedas.Praba);
                }
            }
        }
        List<Ziedas> ZiedaiIki500(List<Ziedas> ziedai)
        {
            List<Ziedas> atitinka = new List<Ziedas>();
            int count = 0;
            foreach (var ziedas in ziedai)
            {
                if(count >= 3)
                {
                    break;
                }
                if(ziedas.Kaina <= 500 && ziedas.Kaina >= 300)
                {
                    atitinka.Add(ziedas);
                    count++;
                }
            }
            return atitinka;
        }
        public void SpausdintAtitinka(List<Ziedas> atitinka)
        {
            using (StreamWriter writer = new StreamWriter("rezultatai.csv"))
            {
                writer.WriteLine("Pavadinimas    Praba    ");
                foreach (var ziedas in atitinka)
                {
                    
                    writer.WriteLine(ziedas.Pavadinimas +"        "+ ziedas.Praba);
                }
                writer.WriteLine("-----------------------");
            }
        }



    }
}
    
