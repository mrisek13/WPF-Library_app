using System.Collections.Generic;

namespace WpfLibraryApp
{
    // Klasa koja implementira IComparer sučelje i služi za uspoređivanje po šifri
    // koristi se kod sortiranja knjiga
    class SortSifra : IComparer<Knjiga>
    {
        // Prima dva objekta tipa Knjiga (označena kao x i y) i vraća cijeli broj.
        // Vraćena vrijednost koristi se za određivanje redoslijeda objekata u sortiranju
        public int Compare(Knjiga x, Knjiga y)
        {
            // metoda CompareTo uspoređuje šifre objekata x i y
            // Vraćena vrijednost koristi se za određivanje redoslijeda objekata u sortiranju:
            // Ako je rezultat manji od 0: Objekt x dolazi prije objekta y
            // Ako je rezultat 0: Objekti x i y se smatraju jednakima po kriteriju sortiranja
            // Ako je rezultat veći od 0: Objekt x dolazi nakon objekta y
            return x.Sifra.CompareTo(y.Sifra);
        }
    }

    class SortNaziv : IComparer<Knjiga>
    {
        public int Compare(Knjiga x, Knjiga y)
        {
            return x.Naziv.CompareTo(y.Naziv);
        }
    }

    public class SortZanr : IComparer<Knjiga>
    {
        public int Compare(Knjiga x, Knjiga y)
        {
            return string.Compare(x.Zanr, y.Zanr, true);
        }
    }

    public class SortBrojStranica : IComparer<Knjiga>
    {
        public int Compare(Knjiga x, Knjiga y)
        {
            return x.BrojStranica.CompareTo(y.BrojStranica);
        }
    }

    public class SortGodina : IComparer<Knjiga>
    {
        public int Compare(Knjiga x, Knjiga y)
        {
            return x.GodinaIzdanja.CompareTo(y.GodinaIzdanja);
        }
    }
}
