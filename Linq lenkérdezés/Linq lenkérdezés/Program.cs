using System;
using System.Linq;

namespace Linq_lenkérdezés
{
    class Program
    {
        static void Main(string[] args)
        { 
            const int sorok = 3;
            const int szekek = 10;
            //o - szabad, x - foglalt
            string[] foglaltsag = new string[] { "o o x o x x o o o o o", "x x x x o o o x o o", "x o x o o o x x o x" };
            string[] foglaltsag1 = new string[] { "ooxoxxooooo", "xxxxoooxoo", "xoxoooxxox" };
            int[] kategoria = new int[sorok * szekek]; //a hely melyik árkategóriába tartozik 1-5-ig

            Random r = new Random();
            for (int i = 0; i < sorok * szekek; i++)
            {
                kategoria[i] = r.Next(1, 6);
                //kiírás:
                if (i % szekek == 0) Console.WriteLine();
                Console.Write(kategoria[i] + " ");
            }


            //megszámoljuk hány x van, vagyis hány foglalt hely, hány jegyet adtak el eddig

            var stringek = from sztring in foglaltsag1 select sztring;

            int szum = 0;
            foreach (var hh in stringek)
            {
                szum += hh.ToCharArray().Count(g => g == 'x');
            }
            Console.WriteLine();
            Console.WriteLine("Szum: "+ szum);

            //2. Változat

            var fogl1 = from x in foglaltsag1 //az x string, az y char
                        from y in x //string -> char
                        where y == 'x'
                        select y;



            int db1 = fogl1.ToList().Count();
            Console.WriteLine("\nAz előadásra {0} darab jegyet adtak el, ez a nézőtér {1:N2} %-a", db1, 100 * db1 / (sorok * szekek));


            int szám = 0;
            var zsák = from karton in foglaltsag select karton;
            foreach (var jj in zsák)
            {
              szám +=  jj.ToCharArray().Count(g => g == 'x');
            }
            Console.WriteLine("Foglaltság: "+ szám);

            //3. Verzió
            var fogl = from x in foglaltsag
                       let y = x.Split(' ') //y egy string []
                       from z in y //z egy string
                       where z == "x"
                       select z;

            int db = fogl.ToList().Count(); //a fogl IEnumerable<string> típusú, annak nem tudja kiírni a darabszámát
            //2 tizedesjegy pontossággal írjuk ki a százalékot:
            Console.WriteLine("\nAz előadásra {0} darab jegyet adtak el, ez a nézőtér {1:N2} %-a", db, 100 * db / (sorok * szekek));

            //4.Verzió

            var fogl2 = from x in foglaltsag
                        let y = x.Split(' ')
                        from z in y
                        select z;

            int db2 = fogl2.ToList().Count(i => i == "x");


            //Melyik kategóriában van a legtöbb


            // Az összes helyre a kategóriák megszámolása:
            int[] kat = new int[6];
            for (int i = 1; i < 6; i++)
            {
                kat[i] = kategoria.Count(x => x == i);
            }
            //melyikből van a legtöbb?
            int maxindex = kat.ToList().IndexOf(kat.Max()); //kat.Max() - legnagyobb elem
            Console.WriteLine("A legtöbb jegy " + maxindex + " árkategóriájú.");
            //ellenőrzésként írja ki az összes kategória elemszámát:
            for (int i = 1; i < 6; i++)
            {
                Console.WriteLine(i + ": " + kat[i] + " db");
            }

        }
    }
}
