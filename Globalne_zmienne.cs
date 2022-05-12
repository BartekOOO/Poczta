using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Poczta
{
    class Zmienna
    {
        private static int globalne_id;

        public static int Id
        {
            get { return globalne_id; }
            set { globalne_id = value; }
        }

        private static int globalny_nr_domu;

        public static int Nr_domu
        {
            get { return globalny_nr_domu; }
            set { globalny_nr_domu = value; }
        }


        private static string globalny_email;

        public static string Email
        {
            get { return globalny_email; }
            set { globalny_email = value; }
        }

        private static string globalne_imie;

        public static string Imie
        {
            get { return globalne_imie; }
            set { globalne_imie = value; }
        }

        private static string globalne_nazwisko;

        public static string Nazwisko
        {
            get { return globalne_nazwisko; }
            set { globalne_nazwisko = value; }
        }

        private static string globalne_miasto;

        public static string Miasto
        {
            get { return globalne_miasto; }
            set { globalne_miasto = value; }
        }

        private static string globalna_ulica;

        public static string Ulica
        {
            get { return globalna_ulica; }
            set { globalna_ulica = value; }
        }

       

        private static string globalne_haslo;

        public static string Haslo
        {
            get { return globalne_haslo; }
            set { globalne_haslo = value; }
        }


        private static bool wyslane_bool;

        public static bool Wyslane
        {
            get { return wyslane_bool; }
            set { wyslane_bool = value; }
        }

        private static bool odebrane_bool;

        public static bool Odebrane
        {
            get { return odebrane_bool; }
            set { odebrane_bool = value; }
        }

        private static int globalny_indeks_tabelki;

        public static int Indeks
        {
            get { return globalny_indeks_tabelki; }
            set { globalny_indeks_tabelki = value; }
        }


        private static DataTable globalna_tabelka;

        public static DataTable Tabelka
        {
            get { return globalna_tabelka; }
            set { globalna_tabelka = value; }
        }


        private static string odpowiedz_do_kogo;

        public static string Opowiadanie_do_kogo
        {
            get { return odpowiedz_do_kogo; }
            set { odpowiedz_do_kogo = value; }
        }

    }
}
