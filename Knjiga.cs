using System;

namespace WpfLibraryApp
{
    // 1.	Razred Knjiga
    // IComparable sučelje u C# koje zahtijeva implementaciju metode CompareTo. Sučelje omogućava da
    // objekti jednog tipa budu usporedivi međusobno
    public class Knjiga : IComparable<Knjiga>
    {
        //  eksplicitno definirate privatnu varijablu sifra
        private int sifra;
        // 1.	Svojstvo Sifra (string)
        //  omogućuje veću kontrolu nad ponašanjem svojstva, kao što je dodavanje dodatne logike u get/set metode
        // kao što je validacija ulaznih podataka
        public int Sifra
        {
            get { return sifra; }
            set { sifra = value; }
        }

        private string autor;
        public string Autor
        {
            get { return autor; }
            set { autor = value; }
        }

        private string naziv;
        public string Naziv
        {
            get { return naziv; }
            set { naziv = value; }
        }


        /* Auto-implementirana svojstva - svojstva koja omogućuju
             da C# kompajler automatski generira privatnu varijablu u pozadini. Ne moramo eksplicitno
             pisati kod za privatnu varijablu.
             Kompajler stvara privatnu varijablu koja se koristi unutar get i set metoda,
             ali mi nemamo izravan pristup toj varijabli.

             auto-implementirana svojstva su korisna za jednostavne implementacije gdje
             dodatna logika nije potrebna, dok su svojstva s definiranim privatnim varijablama
             bolja za scenarije gdje je potrebna veća kontrola i prilagodljivost.
        */

        // 4.	Auto implementirano svojstvo Zanr (string)
        public string Zanr { get; set; }

        //5.	Auto implementirano svojstvo GodinaIzdanja (int)
        public int GodinaIzdanja { get; set; }
        public int BrojStranica { get; set; }
        public string Izdavac { get; set; }
        public string Uvez { get; set; }

        // Ova metoda je ključna za omogućavanje sortiranja i niza drugih operacija koje
        // zahtijevaju redoslijed ili rangiranje objekata
        // ovu metodu moramo implementirati jer implementiramo sučelje IComparable
        // služi za definiranje pravila usporedbe između dva objekta istog tipa
        public int CompareTo(Knjiga other)
        {
            int retVal = 0;
            if (BrojStranica < other.BrojStranica)
                retVal = -1;
            else if (BrojStranica > other.BrojStranica)
                retVal = 1;
            else
                retVal = 0;

            /*
             * Manje od 0 - ovaj objekt je prije drugog u poretku sortiranja
             * Nula - ovaj objekt je na istoj poziciji kao i drugi
             * Više od 0 - ovaj objekt je poslije drugog u poretku sortiranja
             * */
            return retVal;
        }
    }

}
