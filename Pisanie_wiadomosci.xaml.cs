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
using System.Data.SQLite;

namespace Poczta
{
    /// <summary>
    /// Logika interakcji dla klasy Pisanie_wiadomosci.xaml
    /// </summary>
    public partial class Pisanie_wiadomosci : Window
    {
        public Pisanie_wiadomosci()
        {
            InitializeComponent();
            od_kogo.Text = Zmienna.Email;
        }

        private void wyczysc_wiadomosci(object sender, RoutedEventArgs e)
        {
            do_kogo.Text = "";
            temat_wiadomosci.Text = "";
            tresc_wiadomosci.Text = "";
        }

        private void wyslij_wiadomosc(object sender, RoutedEventArgs e)
        {
            SQLiteConnection baza = new SQLiteConnection("Data Source=Baza_danych.db; Version=3;");
            SQLiteCommand komenda = new SQLiteCommand();

            baza.Open();
            string od_kogo_string = Zmienna.Email;
            string do_kogo_string = do_kogo.Text;
            string temat = temat_wiadomosci.Text;
            string wiadomosc = tresc_wiadomosci.Text;
            if(Funkcje.Sprzawdz_czy_sie_nie_powtarza(baza,do_kogo_string)==false)
            {
                if (MessageBox.Show("Czy na pewno chcesz wysłać wiadomość?", "Potwierdzenie", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    komenda = baza.CreateCommand();
                    komenda.CommandText = $"insert into Wiadomosc_nadawcy (Id_odbiorcy,Id_wysylajacego,Temat,Tresc) values ({(Funkcje.znajdz_indeks(baza, do_kogo_string))},{Zmienna.Id},'{temat}','{wiadomosc}');";
                    komenda.ExecuteNonQuery();
                    komenda.CommandText = $"insert into Wiadomosc_odbiorcy (Id_odbiorcy,Id_wysylajacego,Temat,Tresc) values ({(Funkcje.znajdz_indeks(baza, do_kogo_string))},{Zmienna.Id},'{temat}','{wiadomosc}');";
                    komenda.ExecuteNonQuery();
                    MessageBox.Show("Wysłano wiadomość");
                    Glowna_strona_poczty glowna = new Glowna_strona_poczty();
                    glowna.Show();
                    baza.Close();
                    this.Close();
                }
                else
                {

                }
                
           
            }
            else
            {
                MessageBox.Show("Adresat nie istnieje");
            }
        }

        private void anuluj_wiadomosc(object sender, RoutedEventArgs e)
        {
            Glowna_strona_poczty glowna = new Glowna_strona_poczty();
            glowna.Show();
            this.Close();
        }
    }
}
