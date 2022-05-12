using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Data.SQLite;

namespace Poczta
{
    public static class Funkcje
    {

        public static string losowanie()
        {
            string tekst = "";
            Random random = new Random();
            for (int i = 0; i < 6; i++)
            {
                int a = random.Next(9);
                tekst = tekst + a.ToString();
            }
            return tekst;
        }

        public static bool Sprawdz_czy_napis(string a)
        {
            bool prawda = true;

            for (int i = 0; i < a.Length; i++)
            {
                if (Regex.IsMatch(a[i].ToString(), @"^\d+$"))
                {
                    prawda = false;
                }
            }

            return prawda;
        }

        public static string Duza_literka(string a)
        {
            string napis = "";
            string literka = a[0].ToString().ToUpper();

            for (int i = 0; i < a.Length; i++)
            {
                if (i == 0)
                {
                    napis = napis + literka;
                }
                else
                {
                    napis = napis + a[i];
                }
            }

            return napis;
        }

        public static bool Czy_odpowiednia_ilosc_liter(string a, int b, int c = 3)
        {
            bool prawda = true;
            int i = 0;
            for (i = 0; i < a.Length; i++)
            {
                if (i > b)
                {
                    prawda = false;
                }
            }
            if (i < c)
            {
                prawda = false;
            }

            return prawda;
        }


        public static bool Sprzawdz_czy_sie_nie_powtarza(SQLiteConnection baza, string a)
        {
            bool prawda = true;

            SQLiteCommand komenda;

            komenda = baza.CreateCommand();
            komenda.CommandText = "select Email from Urzytkownik";
            SQLiteDataReader czytnik = komenda.ExecuteReader();
            while (czytnik.Read())
            {
                if (czytnik.GetString(0) == a)
                {
                    prawda = false;
                }
            }

            return prawda;
        }

        public static bool czy_dobre_haslo(string a)
        {
            bool prawda = true;

            int ilosc_duzych_liter = 0;
            int ilosc_malych_liter = 0;
            int liczba = 0;
            int ilosc_liczb = 0;

            for (int i = 0; i < a.Length; i++)
            {
                if (char.IsUpper(a[i]))
                {
                    ilosc_duzych_liter++;
                }
                if (char.IsLower(a[i]))
                {
                    ilosc_malych_liter++;
                }
                if (int.TryParse(a[i].ToString(), out liczba))
                {
                    ilosc_liczb++;
                }
                liczba = i;
            }

            if (ilosc_malych_liter < 1)
            {
                prawda = false;
            }
            if (ilosc_duzych_liter < 1)
            {
                prawda = false;
            }
            if (ilosc_liczb < 1 && ilosc_liczb != liczba)
            {
                prawda = false;
            }

            return prawda;
        }

        public static bool czy_dobry_email(string a)
        {
            bool prawda = true;

            for(int i=0;i<a.Length;i++)
            {
                if(a[i]=='@')
                {
                    prawda = false;
                }
            }

            return prawda;
        }


        public static string zagwiazdkuj(string a)
        {
            string napis = "";

            for (int i = 0; i < a.Length; i++)
            {
                napis = napis + "*";
            }

            return napis;
        }

        public static int znajdz_indeks(SQLiteConnection baza, string email)
        {
            SQLiteCommand komenda = new SQLiteCommand();
            SQLiteDataReader czytnik;
            int id = 0;
            komenda = baza.CreateCommand();
            komenda.CommandText = $"select Id_urzytkownika from Urzytkownik where Email='{email}';";
            czytnik = komenda.ExecuteReader();

            while(czytnik.Read())
            {
                id = czytnik.GetInt32(0);
            }

            return id;
        }




    }
}
