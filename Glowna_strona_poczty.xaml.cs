using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;
using System.Data.SQLite;
namespace Poczta
{
    /// <summary>
    /// Logika interakcji dla klasy Glowna_strona_poczty.xaml
    /// </summary>
    public partial class Glowna_strona_poczty : Window
    {


        public Glowna_strona_poczty()
        {
            InitializeComponent();
            konto_menu.Header = Zmienna.Email;

        }

        private void zacznij(object sender, RoutedEventArgs e)
        {
            if (Zmienna.Odebrane)
            {
                wyslane.Visibility = Visibility.Hidden;
                odebrane.Visibility = Visibility.Visible;

                SQLiteConnection baza = new SQLiteConnection("Data Source=Baza_danych.db; Version=3;");
                baza.Open();

                odebrane_przycisk.Background = Brushes.LightGreen;
                wyslaane_przycisk.Background = Brushes.LightPink;

                Zmienna.Odebrane = true;
                Zmienna.Wyslane = false;

                SQLiteDataReader czytnik;

                SQLiteDataReader czytnik3;
                SQLiteCommand komenda;
                DataTable tabelka2 = new DataTable();
                DataTable tabelka = new DataTable();

                tabelka.Columns.Add("Nadawca");
                tabelka.Columns.Add("Temat");
                tabelka.Columns.Add("Treść");

                tabelka2.Columns.Add("Nadawca");
                tabelka2.Columns.Add("Temat");
                tabelka2.Columns.Add("Treść");

                komenda = baza.CreateCommand();
                komenda.CommandText = $"select  Id_wysylajacego , Temat , Tresc  from Wiadomosc_odbiorcy where Id_odbiorcy={Zmienna.Id};";
                czytnik = komenda.ExecuteReader();

                while (czytnik.Read())
                {

                    string nadawca = "";


                    komenda = baza.CreateCommand();
                    komenda.CommandText = "select Email, Id_urzytkownika from Urzytkownik;";
                    czytnik3 = komenda.ExecuteReader();



                    while (czytnik3.Read())
                    {
                        if (czytnik.GetInt32(0) == czytnik3.GetInt32(1))
                        {
                            nadawca = czytnik3.GetString(0);
                        }
                    }

                    string napis = "";
                    string napis2 = czytnik.GetString(2);
                    string napis3 = napis2;
                    for (int i = 0; i < napis2.Length && i < 10; i++)
                    {
                        napis = napis + napis2[i];
                    }

                    tabelka.Rows.Add($"{nadawca}", $"{czytnik.GetString(1)}", $"{napis}");
                    tabelka2.Rows.Add($"{nadawca}", $"{czytnik.GetString(1)}", $"{napis3}");

                }




                odebrane.ItemsSource = tabelka.AsDataView();
                odebrane.Columns[0].Width = 225;
                odebrane.Columns[1].Width = 225;
                odebrane.Columns[2].Width = 181;

                Zmienna.Tabelka = tabelka2;


                baza.Close();
            }
            if (Zmienna.Wyslane)
            {
                SQLiteConnection baza = new SQLiteConnection("Data Source=Baza_danych.db; Version=3;");
                baza.Open();
                wyslane.Visibility = Visibility.Visible;
                odebrane.Visibility = Visibility.Hidden;

                odebrane_przycisk.Background = Brushes.LightPink;
                wyslaane_przycisk.Background = Brushes.LightGreen;


                Zmienna.Odebrane = false;
                Zmienna.Wyslane = true;

                SQLiteDataReader czytnik;
                SQLiteDataReader czytnik2;

                SQLiteCommand komenda;
                DataTable tabelka = new DataTable();
                DataTable tabelka2 = new DataTable();
                tabelka.Columns.Add("Odbiorca");

                tabelka.Columns.Add("Temat");
                tabelka.Columns.Add("Treść");

                tabelka2.Columns.Add("Odbiorca");

                tabelka2.Columns.Add("Temat");
                tabelka2.Columns.Add("Treść");

                komenda = baza.CreateCommand();
                komenda.CommandText = $"select Id_odbiorcy , Temat , Tresc  from Wiadomosc_nadawcy where Id_wysylajacego={Zmienna.Id};";
                czytnik = komenda.ExecuteReader();

                while (czytnik.Read())
                {
                    string odbiorca = "";

                    komenda = baza.CreateCommand();
                    komenda.CommandText = "select Email, Id_urzytkownika from Urzytkownik;";
                    czytnik2 = komenda.ExecuteReader();



                    while (czytnik2.Read())
                    {
                        if (czytnik.GetInt32(0) == czytnik2.GetInt32(1))
                        {
                            odbiorca = czytnik2.GetString(0);
                        }
                    }


                    string napis = "";
                    string napis2 = czytnik.GetString(2);
                    string napis3 = napis2;
                    for (int i = 0; i < napis2.Length && i < 10; i++)
                    {
                        napis = napis + napis2[i];
                    }

                    tabelka.Rows.Add($"{odbiorca}", $"{czytnik.GetString(1)}", $"{napis}");
                    tabelka2.Rows.Add($"{odbiorca}", $"{czytnik.GetString(1)}", $"{napis3}");

                }




                wyslane.ItemsSource = tabelka.AsDataView();
                wyslane.Columns[0].Width = 225;
                wyslane.Columns[1].Width = 225;
                wyslane.Columns[2].Width = 181;
                Zmienna.Tabelka = tabelka2;

                baza.Close();
            }
        }



        private void ustawienia_konta_procedura(object sender, RoutedEventArgs e)
        {
            Ustawienia_konta ustawienia_konta = new Ustawienia_konta();
            ustawienia_konta.Show();
            this.Close();
        }

        private void wylgouj_procedura(object sender, RoutedEventArgs e)
        {
            MainWindow logowanie = new MainWindow();
            logowanie.Show();
            this.Close();

        }

        private void napisz_wiadomosc(object sender, RoutedEventArgs e)
        {
            Pisanie_wiadomosci nowa_wiadomosc_okno = new Pisanie_wiadomosci();
            nowa_wiadomosc_okno.Show();
            this.Close();
        }




        public void wyslane_pokaz(object sender, RoutedEventArgs e)
        {

            SQLiteConnection baza = new SQLiteConnection("Data Source=Baza_danych.db; Version=3;");
            baza.Open();
            wyslane.Visibility = Visibility.Visible;
            odebrane.Visibility = Visibility.Hidden;

            odebrane_przycisk.Background = Brushes.LightPink;
            wyslaane_przycisk.Background = Brushes.LightGreen;

            Zmienna.Odebrane = false;
            Zmienna.Wyslane = true;

            SQLiteDataReader czytnik;
            SQLiteDataReader czytnik2;

            SQLiteCommand komenda;
            DataTable tabelka = new DataTable();
            DataTable tabelka2 = new DataTable();
            tabelka.Columns.Add("Odbiorca");

            tabelka.Columns.Add("Temat");
            tabelka.Columns.Add("Treść");

            tabelka2.Columns.Add("Odbiorca");

            tabelka2.Columns.Add("Temat");
            tabelka2.Columns.Add("Treść");

            komenda = baza.CreateCommand();
            komenda.CommandText = $"select Id_odbiorcy , Temat , Tresc  from Wiadomosc_nadawcy where Id_wysylajacego={Zmienna.Id};";
            czytnik = komenda.ExecuteReader();

            while (czytnik.Read())
            {
                string odbiorca = "";

                komenda = baza.CreateCommand();
                komenda.CommandText = "select Email, Id_urzytkownika from Urzytkownik;";
                czytnik2 = komenda.ExecuteReader();



                while (czytnik2.Read())
                {
                    if (czytnik.GetInt32(0) == czytnik2.GetInt32(1))
                    {
                        odbiorca = czytnik2.GetString(0);
                    }
                }


                string napis = "";
                string napis2 = czytnik.GetString(2);
                string napis3 = napis2;
                for (int i = 0; i < napis2.Length && i < 10; i++)
                {
                    napis = napis + napis2[i];
                }

                tabelka.Rows.Add($"{odbiorca}", $"{czytnik.GetString(1)}", $"{napis}");
                tabelka2.Rows.Add($"{odbiorca}", $"{czytnik.GetString(1)}", $"{napis3}");

            }




            wyslane.ItemsSource = tabelka.AsDataView();
            wyslane.Columns[0].Width = 225;
            wyslane.Columns[1].Width = 225;
            wyslane.Columns[2].Width = 181;
            Zmienna.Tabelka = tabelka2;

            baza.Close();

        }

        private void odebrane_pokaz(object sender, RoutedEventArgs e)
        {
            wyslane.Visibility = Visibility.Hidden;
            odebrane.Visibility = Visibility.Visible;

            SQLiteConnection baza = new SQLiteConnection("Data Source=Baza_danych.db; Version=3;");
            baza.Open();

            odebrane_przycisk.Background = Brushes.LightGreen;
            wyslaane_przycisk.Background = Brushes.LightPink;

            Zmienna.Odebrane = true;
            Zmienna.Wyslane = false;

            SQLiteDataReader czytnik;

            SQLiteDataReader czytnik3;
            SQLiteCommand komenda;
            DataTable tabelka2 = new DataTable();
            DataTable tabelka = new DataTable();

            tabelka.Columns.Add("Nadawca");
            tabelka.Columns.Add("Temat");
            tabelka.Columns.Add("Treść");

            tabelka2.Columns.Add("Nadawca");
            tabelka2.Columns.Add("Temat");
            tabelka2.Columns.Add("Treść");

            komenda = baza.CreateCommand();
            komenda.CommandText = $"select  Id_wysylajacego , Temat , Tresc  from Wiadomosc_odbiorcy where Id_odbiorcy={Zmienna.Id};";
            czytnik = komenda.ExecuteReader();

            while (czytnik.Read())
            {

                string nadawca = "";


                komenda = baza.CreateCommand();
                komenda.CommandText = "select Email, Id_urzytkownika from Urzytkownik;";
                czytnik3 = komenda.ExecuteReader();



                while (czytnik3.Read())
                {
                    if (czytnik.GetInt32(0) == czytnik3.GetInt32(1))
                    {
                        nadawca = czytnik3.GetString(0);
                    }
                }

                string napis = "";
                string napis2 = czytnik.GetString(2);
                string napis3 = napis2;
                for (int i = 0; i < napis2.Length && i < 10; i++)
                {
                    napis = napis + napis2[i];
                }

                tabelka.Rows.Add($"{nadawca}", $"{czytnik.GetString(1)}", $"{napis}");
                tabelka2.Rows.Add($"{nadawca}", $"{czytnik.GetString(1)}", $"{napis3}");

            }




            odebrane.ItemsSource = tabelka.AsDataView();
            odebrane.Columns[0].Width = 225;
            odebrane.Columns[1].Width = 225;
            odebrane.Columns[2].Width = 181;

            Zmienna.Tabelka = tabelka2;


            baza.Close();



        }

        private void otworz_wiadomosc(object sender, RoutedEventArgs e)
        {
            if (Zmienna.Wyslane)
            {
                Zmienna.Indeks = wyslane.SelectedIndex;
            }
            if (Zmienna.Odebrane)
            {
                Zmienna.Indeks = odebrane.SelectedIndex;
            }


            Okno_odebranych_wiadomosci okno = new Okno_odebranych_wiadomosci();
            okno.Show();
            this.Close();
        }


    }




}
