using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace WpfLibraryApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string datotekaKnjiga = "knjige.json";

        // deklariramo polje zanrovi koje je popunjen s nizom stringova koji predstavljaju različite žanrove knjiga
        private string[] zanrovi = new string[] { "Povijesni roman", "Ljubavni roman", "Triler i krimininalistički roman", "Fantasy", "SF i horor",
            "Beletristika", "Klasici", "Poezija", "Drama", "Ostalo" };

        private string[] sort = new string[] { "Šifra", "Naziv", "Žanr", "Broj stranica", "Godina" };

        public List<Knjiga> Knjige { get; set; }

        // Konstruktor koji se pokreće kod instanciranja klase MainWindow -> kod pokretanja aplikacije
        public MainWindow()
        {
            InitializeComponent();

            /*
             * ItemsSource je svojstvo koje određuje izvor podataka za popis stavki unutar ComboBox-a.
             * Dodjeljivanjem polja zanrovi kao ItemsSource, svi elementi polja će biti prikazani kao
             * opcije unutar ComboBox-a cbZanr.
             * Drugim riječima, ComboBox će imati opcije kao što su "Povijesni roman", "Ljubavni roman", i tako dalje,
             * omogućavajući korisniku da odabere jedan od tih žanrova.
             * */
            cbZanr.ItemsSource = zanrovi;

            // punimo ComboBox s opcijama za sortiranje
            cbSort.ItemsSource = sort;

            // punimo ComboBox za godine pomoću for petlje 1980 - do trenutačne
            for (int i = 1980; i <= DateTime.Now.Year; i++)
            {
                cbGodina.Items.Add(i);
            }

            // inicijalizacija liste knjiga
            Knjige = new List<Knjiga>();
        }

        private void btnSpremi_Click(object sender, RoutedEventArgs e)
        {
            // Provjeravamo ako je polje za šifru prazno
            // metoda IsNullOrWhiteSpace klase String vraća true ako je vrijednost nekog stringa prazna
            if (String.IsNullOrWhiteSpace(txtSifra.Text))
            {
                // ako je prikazujemo poruku obavijesti da je potrebno upisati šifru
                MessageBox.Show("Upišite šifru!");
                // zaustavljamo daljnje izvršavanje metode
                return;
            }

            int sifra = 0;

            // parsiramo sadržaj polja Šifra koje je tipa string u int tip podatka
            // koristimo metodu TryParse koja vraća true ako može pretvoriti string u broj ili false ako ne može
            // metoda ima 2 parametra: string koji se pretvara u broj i varijablu u koju će spremiti broj - koristi se ključna riječ out
            if (int.TryParse(txtSifra.Text, out sifra) == false)
            {
                MessageBox.Show("Šifra mora biti broj!");
                return;
            }

            //2.	Prilikom kreiranja instance klase Knjiga provjeriti ako šifra nije zauzeta
            foreach (Knjiga knj in Knjige)
            {
                // ako postoji knjiga s istom oznakom onda ispisujemo poruku i
                // prekidamo daljnje izvršavanje ove metode naredbom return;
                if (knj.Sifra == sifra)
                {
                    MessageBox.Show("Već postoji knjiga s tom šifrom!");
                    return;
                }
            }

            if (String.IsNullOrWhiteSpace(txtAutor.Text))
            {
                MessageBox.Show("Upišite autora!");
                return;
            }

            if (String.IsNullOrWhiteSpace(txtNaziv.Text))
            {
                MessageBox.Show("Upišite naziv!");
                return;
            }

            if (String.IsNullOrWhiteSpace(txtIzdavac.Text))
            {
                MessageBox.Show("Upišite izdavača!");
                return;
            }

            if (String.IsNullOrWhiteSpace(txtBrojStranica.Text))
            {
                MessageBox.Show("Upišite broj stranica!");
                return;
            }

            // Provjeravamo ako je odabran žanr u listi vrijednosti cbZanr, tj. ako je selektirana neka vrijednost
            // SelectedItem vraća null ako korisnik nije odabrao neku vrijednost
            if (cbZanr.SelectedItem == null)
            {
                MessageBox.Show("Odaberite žanr!");
                return;
            }

            if (cbGodina.SelectedItem == null)
            {
                MessageBox.Show("Odaberite godinu!");
                return;
            }

            int brojStranica = 0;

            // parsiramo sadržaj polja Broj stranica koje je tipa string u int tip podatka
            // koristimo metodu TryParse koja vraća true ako može pretvoriti string u broj ili false ako ne može
            // metoda ima 2 parametra: string koji se pretvara u broj i varijablu u koju će spremiti broj - koristi se ključna riječ out
            if (int.TryParse(txtBrojStranica.Text, out brojStranica) == false)
            {
                MessageBox.Show("Upišite broj stranica!");
                return;
            }

            // Instanciramo objekt o klase Knjiga
            Knjiga k = new Knjiga();

            // Postavljamo vrijednost svojstva (propertya) Sifra na sadržaj tekstualnog polja txtSifra
            k.Sifra = sifra;
            k.Autor = txtAutor.Text;
            k.Naziv = txtNaziv.Text;

            // U svojstvo GodinaIzdanja objekta o klase Knjiga spremamo odabranu vrijednost u listi godina
            // u SelectedItem može biti bilo koja vrsta podatka (int, string, objekt neke klase...) kod nas je int pa pretvaramo u int (unboxing)
            k.GodinaIzdanja = (int)cbGodina.SelectedItem;

            k.Izdavac = txtIzdavac.Text;
            k.BrojStranica = brojStranica;

            // U svojstvo Zanr objekta o klase Knjiga spremamo odabranu vrijednost u listi žanrova
            // u SelectedItem može biti bilo koja vrsta podatka (int, string, objekt neke klase...) kod nas je string pa pretvaramo u string (unboxing)
            k.Zanr = (string)cbZanr.SelectedItem;

            // ako je označeno da je tvrdi uvez spremiti odgovarajuću vrijednost u property Uvez
            if (rbTvrdi.IsChecked.Value)
                k.Uvez = "Tvrdi";
            // ako je označeno da je meki uvez spremiti odgovarajuću vrijednost u property Uvez
            else if (rbMeki.IsChecked.Value)
                k.Uvez = "Meki";
            // ako nije označeno da je tvrdi ili meki uvez
            else
                k.Uvez = "Nedefinirano";

            // dodajemo objekt k u listu objekata klase Knjiga
            Knjige.Add(k);

            // punimo DataGrid za prikaz na formi
            listKnjiga.ItemsSource = Knjige;
            listKnjiga.Items.Refresh();

            // pozivamo metodu koja prazni polja na formi
            Ponisti();
        }

        private void btnPonisti_Click(object sender, RoutedEventArgs e)
        {
            Ponisti();
        }

        // metoda koja prazni polja na formi
        private void Ponisti()
        {
            txtSifra.Text = "";
            txtAutor.Text = "";
            txtNaziv.Text = "";
            txtBrojStranica.Text = "";
            txtIzdavac.Text = "";
            cbGodina.SelectedItem = null;
            cbZanr.SelectedItem = null;
            GenerirajSifru();
            txtAutor.Focus();
        }

        // 3.	Generirati šifru knjige (maksimalna šifra u listi + 1)
        private void GenerirajSifru()
        {
            // šifra je inicijalno 0
            int sifra = 0;

            // pomoću petlje prolazimo kroz listu knjiga
            foreach (Knjiga k in Knjige)
            {
                // ako je šifra knjige u trenutačnoj iteraciji petlje veća od vrijednosti varijable sifra
                if (k.Sifra > sifra)
                    // vrijednost varijable sifra postaje vrijednost sifre knjige u trenutačnoj iteraciji
                    sifra = k.Sifra;
            }

            // uvećavamo vrijednost varijable sifra za 1 i upisujemo u txtSifra polje na formi
            txtSifra.Text = (sifra + 1).ToString();
        }

        //5.	Prilikom pokretanja aplikacije učitati listu knjiga iz datoteke
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UcitajKnjige();
            listKnjiga.ItemsSource = Knjige;
            listKnjiga.Items.Refresh();

            // inicijalno generiramo sljedeću šifru knjige
            GenerirajSifru();

            // stavljamo fokus na polje za unos autora
            txtAutor.Focus();
        }

        //4.	Prilikom zatvaranja aplikacije spremiti listu knjiga u datoteku (koristite Newtosoft.Json biblioteku)
        // metoda koja se pokreće kod izlaska iz aplikacije
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SpremiKnjige();
        }

        private void SpremiKnjige()
        {
            // za spremanje u tekstualni oblik koristimo Newtonsoft.Json biblioteku koja nam
            // listu knjiga vraća u JSON formatu
            string json = JsonConvert.SerializeObject(Knjige);

            // pozivom metode WriteAllText iz statičke klase File spremamo JSON dobiven prethodnom linijom
            // u datoteku koja je definirana u varijabli datotekaKnjiga
            // datoteka se generira u folderu gdje je i aplikacija
            // Desni klikn na projekt s desne strane -> Open folder in file explorer
            // -> \bin\debug\knjige.json
            File.WriteAllText(datotekaKnjiga, json);
        }
        private void UcitajKnjige()
        {
            // provjeravamo ako postoji datoteka s knjigama
            // koristimo statičku klasu File i metodu Exists koja nam vraća
            // true - datoteka postoji ili false - datoteka ne postoji
            if (File.Exists(datotekaKnjiga))
            {
                // čitanje sadržaja datoteke ili konverzija učitanih podataka u listu filmova
                // može prouzročiti greške pa te linije koda stavljamo u try-catch blok
                // da ne koristimo try-catch i desi se greška kod učitavanja datoteke
                // aplikacija bi se srušila na samom početku
                try
                {
                    // učitavamo sadržaj datoteke u string varijablu json
                    // koristimo statičku klasu File i metodu ReadAllText
                    string json = File.ReadAllText(datotekaKnjiga);

                    // deserijaliziramo tekstualni oblik u listu knjiga
                    Knjige = JsonConvert.DeserializeObject<List<Knjiga>>(json);
                }
                catch (Exception e)
                {
                    // ako je došlo do greške u try bloku, ovdje ispisujemo o kakvoj grešci se radi
                    // e.Message vraća kratki opis greške
                    MessageBox.Show($"Greška kod učitavanja knjiga: {e.Message}");
                }
            }
        }

        // 6.	Dodati funkcionalnost pretraživanja knjiga po nazivu
        private void btnPretraga_Click(object sender, RoutedEventArgs e)
        {
            // provjeravamo ako je unešen pojam pretrage
            if (!String.IsNullOrWhiteSpace(txtPretraga.Text))
            {
                string filter = txtPretraga.Text.ToLower();

                // Kreiramo posebnu listu gdje ćemo spremati instance koje zadovoljavaju pojam pretrage
                List<Knjiga> filtrirano = new List<Knjiga>();

                foreach (Knjiga k in Knjige)
                {
                    // provjeravamo ako trenutačna instanca u iteraciji u nazivu sadrži upisan pojam pretrage
                    // koristimo metodu Contains koja vraća true ako string sadrži drugi string
                    // radimo ToLower() da bi oba stringa bila malim slovima kako bi pretraživanje bilo neovisno
                    // o malim/velikim slovima (Case insensitive)
                    if (k.Naziv.ToLower().Contains(filter)
                        || k.Zanr.ToLower().Contains(filter))
                    {
                        filtrirano.Add(k);
                    }
                }

                // punimo DataGrid za prikaz na formi s listom filtriranih knjiga koje zadovoljavaju uvjet pretrage
                listKnjiga.ItemsSource = filtrirano;
                listKnjiga.Items.Refresh();
            }
            // ako nije unešen pojam pretrage osvježavamo DataGrid sa svim knjigama
            else
            {
                listKnjiga.ItemsSource = Knjige;
                listKnjiga.Items.Refresh();
            }
        }

        //7.	Dodati funkcionalnost sortiranja knjiga po broju stranica
        private void btnSortBrStr_Click(object sender, RoutedEventArgs e)
        {
            // metoda Sort pokreće sortiranje elemenata liste
            // da bi ovo funkcioniralo klasa kojeg je tipa lista, u našem slučaju Knjiga
            // mora imati implementirano sučelje IComparable gdje je razrađen kriterij sortiranja
            Knjige.Sort();
            listKnjiga.ItemsSource = Knjige;
            listKnjiga.Items.Refresh();
        }

        // 8.	Dodati funkcionalnost sortiranja Knjiga po šifri / nazivu / žanru / broju stranica / godini izdanja u padajući izbornik
        private void btnSort_Click(object sender, RoutedEventArgs e)
        {
            if (cbSort.SelectedItem != null)
            {
                switch (cbSort.SelectedItem)
                {
                    case "Šifra":
                        SortSifra sort = new SortSifra();
                        Knjige.Sort(sort);
                        break;
                    // ako je odabrano sortiranje po nazivu
                    case "Naziv":
                        // kreiramo instancu klase SortNaziv
                        SortNaziv sortNaziv = new SortNaziv();
                        // metoda Sort kao argument prima instancu klase u kojoj je razređen kriterij sortiranja
                        Knjige.Sort(sortNaziv);
                        break;
                    case "Žanr":
                        SortZanr sortZanr = new SortZanr();
                        Knjige.Sort(sortZanr);
                        break;
                    case "Broj stranica":
                        SortBrojStranica sortBrStranica = new SortBrojStranica();
                        Knjige.Sort(sortBrStranica);
                        break;
                    case "Godina":
                        SortGodina sortGodina = new SortGodina();
                        Knjige.Sort(sortGodina);
                        break;
                    default: break;
                }
                listKnjiga.ItemsSource = Knjige;
                listKnjiga.Items.Refresh();
            }
        }

        // 9.	Dodati funkcionalnost brisanja označene knjige
        private void btnIzbrisi_Click(object sender, RoutedEventArgs e)
        {
            // provjeravamo ako je u Datagridu označena neka knjiga
            if (listKnjiga.SelectedItem != null)
            {
                // SelectedItem je tipa object jer se u DataGrid mže spremiti bilo koji tip podataka
                // pa je on u object tipu pa ga mi moramo pretvoriti (casting)
                // u Knjiga tip podatka te takvu instancu onda možemo brisati iz liste
                // pozivom metode Remove - šaljemo joj instancu koju želimo izbrisati
                Knjige.Remove((Knjiga)listKnjiga.SelectedItem);
                listKnjiga.ItemsSource = Knjige;
                listKnjiga.Items.Refresh();
            }
        }
    }
}
