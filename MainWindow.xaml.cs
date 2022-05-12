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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SQLite;

namespace Poczta
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var baza = new SQLiteConnection("Data Source=Baza_danych.db; Version=3;");
            var komenda = new SQLiteCommand();

            baza.Open();

            try
            {
                komenda = baza.CreateCommand();
                komenda.CommandText = "create table Wiadomosc_odbiorcy (Id_wiadomosci INTEGER PRIMARY KEY AUTOINCREMENT,Id_odbiorcy int , Id_wysylajacego int,Temat varchar ,Tresc varchar);";
                komenda.ExecuteNonQuery();
                komenda.CommandText = "create table Wiadomosc_nadawcy (Id_wiadomosci INTEGER PRIMARY KEY AUTOINCREMENT,Id_odbiorcy int , Id_wysylajacego int,Temat varchar ,Tresc varchar);";
                komenda.ExecuteNonQuery();
                komenda.CommandText = "create table Urzytkownik (Id_urzytkownika INTEGER PRIMARY KEY AUTOINCREMENT, Imie varchar, Nazwisko varchar, Email varchar, Haslo varchar, Miasto varchar, Ulica varchar, Nr_domu int);";
                komenda.ExecuteNonQuery();
            }
            catch
            {

            }
            baza.Close();
        }

        private void rejestracja_click(object sender, MouseButtonEventArgs e)
        {
            Okno_rejestracji okno_rejestracji = new Okno_rejestracji();
            okno_rejestracji.Show();
            this.Close();
        }

        private void zaloguj_click(object sender, RoutedEventArgs e)
        {
            var baza = new SQLiteConnection("Data Source=Baza_danych.db; Version=3;");
            var komenda = new SQLiteCommand();
            baza.Open();

            string email = login_pole.Text;
            string haslo = haslo_pole.Password;

            komenda = baza.CreateCommand();
            //                              0      1       2              3       4         5     6        7 
            komenda.CommandText = $"select Email, Haslo,Id_urzytkownika, Imie, Nazwisko, Miasto, Ulica, Nr_domu from Urzytkownik where Email='{email}' and Haslo='{haslo}';";
            SQLiteDataReader czytnik = komenda.ExecuteReader();

            if (czytnik.HasRows)
            {
                while (czytnik.Read())
                {
                    Zmienna.Email = czytnik.GetString(0);
                    Zmienna.Haslo = czytnik.GetString(1);
                    Zmienna.Id = czytnik.GetInt32(2);
                    Zmienna.Imie = czytnik.GetString(3);
                    Zmienna.Nazwisko = czytnik.GetString(4);
                    Zmienna.Miasto = czytnik.GetString(5);
                    Zmienna.Ulica = czytnik.GetString(6);
                    Zmienna.Nr_domu = czytnik.GetInt32(7);
                    Zmienna.Wyslane = true;
                    Zmienna.Odebrane = false;


                    Glowna_strona_poczty glowna_strona_poczty = new Glowna_strona_poczty();
                    glowna_strona_poczty.Show();
                    this.Close();

                    

                }
                baza.Close();
            }
            else
            {
                MessageBox.Show("Źle podane dane urzytkownika");
            }

        }




    }
}
