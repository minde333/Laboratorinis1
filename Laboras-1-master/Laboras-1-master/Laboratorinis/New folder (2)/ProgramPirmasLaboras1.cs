using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Programa skirta dirbti su žiedo pateikimo duomenimis
/// </summary>

namespace Laboratorinis1.Juvelyrikos_parduotuvė
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            Program p = new Program();
            List<Ziedas> ziedai = p.SkaitytiZieduDuomenis("L1Data.csv");

            Ziedas brangiausias = p.BrangiausiasZiedas(ziedai);
            Console.WriteLine("Brangiausias žiedas\n\n" + "Pavadinimas: " + brangiausias.Pavadinimas + "\n" + "Metalas: " + brangiausias.Metalas +
                "\n" + "Skersmuo: " + brangiausias.Dydis + "\n" + "Svoris: " + brangiausias.Svoris + "\n" + "Praba: " + brangiausias.Praba + "\n");

            Console.WriteLine("Daugiausia yra " + p.DaugiausiaZieduPraba(ziedai) + " prabos žiedų, o jų kiekis yra " + p.DaugiausiaZieduKiekis(ziedai));

            List <Ziedas> pigus = p.BaltoAuksoPigesniZiedai(ziedai);
            p.IssaugotiAtaskaitaIFaila(pigus, "BA300.csv");

            List<Ziedas> tarpKainu = p.ZiedaiTarpKainu(ziedai);                     
            p.IssaugotiAtaskaitaIFaila(tarpKainu, "Ziedai3.csv");

            p.SukurtiPradiniuDuomenuAtaskaita(ziedai, "PradiniaiDuomenys.csv");

            Console.ReadKey();
        }

        /// <summary>
        /// Skaito žiedo duomenis iš failo
        /// </summary>
        /// <param name="failoPavadinimas"> Įvesties duomenų failo pavadinimas </param>
        /// <returns> Žiedų sąrašą </returns>
        List<Ziedas> SkaitytiZieduDuomenis(string failoPavadinimas)
        {
            List<Ziedas> ziedai = new List<Ziedas>();

            string[] lines = File.ReadAllLines(failoPavadinimas);
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

                Ziedas ziedas = new Ziedas(gamintojas, pavadinimas, metalas, svoris, dydis, praba, kaina);
                ziedai.Add(ziedas);
            }
            return ziedai;
        }

        /// <summary>
        /// Išsaugo ataskaitą į failą
        /// </summary>
        /// <param name="ziedai"> Žiedų sarašas išsaugojimui </param>
        /// <param name="failoPavadinimas"> Failo vardas, kuriame išsaugojami duomenys </param>
        void IssaugotiAtaskaitaIFaila(List<Ziedas> ziedai, string failoPavadinimas)
            {
                using (StreamWriter writer = new StreamWriter(failoPavadinimas))
                    if (ziedai.Count == 0)
                    {
                        writer.WriteLine("Pageidautinas žiedų sąrašas yra tuščias");
                    }
                    else
                    {
                        writer.WriteLine("sep=");
                        // Reikalingas Exceliui tinkamai suformatuoti failą
                        writer.WriteLine("Gamintojas;Pavadinimas;Metalas;Svoris;Dydis;Praba;Kaina");
                        foreach (var ziedas in ziedai)
                        {
                            writer.WriteLine("{0};{1};{2};{3};{4};{5};{6}", ziedas.Gamintojas, ziedas.Pavadinimas, ziedas.Metalas, ziedas.Svoris, ziedas.Dydis, ziedas.Praba, ziedas.Kaina);
                        }
                    }
            }

        /// <summary>
        /// Suranda brangiausią žiedą sąraše
        /// </summary>
        /// <param name="ziedai"> Žiedų sąrašas, kuriame ieško brangiausio žiedo </param>
        /// <returns> Brangiausią žiedą sąraše </returns>
        public Ziedas BrangiausiasZiedas(List<Ziedas> ziedai)
        {
            double brangiausias = 0;
            int indeksas = 0; // brangiausio žiedo indeksas(vieta sąraše).
            for (int i = 0; i < ziedai.Count; i++)
            {
                if (ziedai[i].Kaina > brangiausias)
                {
                    brangiausias = ziedai[i].Kaina;
                    indeksas = i;
                }
            }
                return ziedai[indeksas];
        }

        /// <summary>
        /// Suranda kokios prabos žiedų parduotuvėje daugiausia
        /// </summary>
        /// <param name="ziedai"> Žiedų sąrašas, kuriame ieško kokios prabos žiedų daugiausia </param>
        /// <returns> Daugiausiai kartų pasikartojančią prabą </returns>
        int DaugiausiaZieduPraba(List<Ziedas> ziedai)
        {
            int max = 0;
            int pasikartoja = 0;
            int praba = 0;
            for (int i = 0; i < ziedai.Count; i++)
            {
                foreach (var ziedas in ziedai)
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
            int atsakymas = praba;

            return atsakymas;
        }

        /// <summary>
        /// Suranda dažniausiai pasikartojančios prabos kiekį
        /// </summary>
        /// <param name="ziedai"> Žiedų sąrašas, kuriame ieško dažniausiai pasikartojančios prabos kiekį </param>
        /// <returns> Dažniausiai pasikartojančios prabos kiekį </returns>
        int DaugiausiaZieduKiekis(List<Ziedas> ziedai)
        {
            int max = 0;
            int pasikartoja = 0;
            int praba = 0;
            for (int i = 0; i < ziedai.Count; i++)
            {
                foreach (var ziedas in ziedai)
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
            int atsakymas = max;

            return atsakymas;
        }

        /// <summary>
        /// Suranda balto aukso žiedus kurie yra pigesni nei 300 eurų
        /// </summary>
        /// <param name="ziedai"> Žiedų sąrašas, kuriame ieško balto aukso žiedų pigesnių nei 300 eurų </param>
        /// <returns> Balto aukso žiedus, kurie yra pigesni nei 300 eurų </returns>
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
        
        /// <summary>
        /// Suranda 3 žiedus, kurių kaina yra tarp 300 ir 500 eurų
        /// </summary>
        /// <param name="ziedai"> Žiedų sąrašas, kuriame ieško 3 žiedus kurių kaina yra tarp 300 ir 500 eurų </param>
        /// <returns> 3 žiedus, kurių kaina yra tarp 300 ir 500 eurų </returns>
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

        /// <summary>
        /// Sukuria visų žiedų pradinių duomenų ataskaitos lentelę
        /// </summary>
        /// <param name="ziedai"> Žiedų ataskaita, kurioje yra žiedų pradiniai duomenys </param>
        /// <param name="failoPavadinimas"> Failas, kuriame yra visų žiedų pradiniai duomenys lentelėje </param>
        public void SukurtiPradiniuDuomenuAtaskaita(List<Ziedas> ziedai, string failoPavadinimas)
        {
            using (StreamWriter writer = new StreamWriter("PradiniaiDuomenys.csv"))
            {
                writer.WriteLine("Žiedų sąrašas");
                writer.WriteLine(new String('-', 88));
                writer.WriteLine("| {0, -19} | {1, -11} | {2, -16}| {3, -6} | {4, -5} | {5, -5} | {6, -5} |", "Gamintojas", "Pavadinimas", "Metalas", "Svoris", "Dydis", "Praba", "Kaina");
                writer.WriteLine(new String('-', 88));

                foreach (var ziedas in ziedai)
                {
                    writer.WriteLine("| {0, -19} | {1, -11} | {2, -16}| {3, -6} | {4, -5} | {5, -5} | {6, -5} |", ziedas.Gamintojas, ziedas.Pavadinimas, ziedas.Metalas, ziedas.Svoris, ziedas.Dydis, ziedas.Praba, ziedas.Kaina);
                    writer.WriteLine(new String('-', 88));
                }
            }
        }
    }
}
    
