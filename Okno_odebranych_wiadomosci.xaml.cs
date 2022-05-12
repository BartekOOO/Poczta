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

namespace Poczta
{
    /// <summary>
    /// Logika interakcji dla klasy Okno_odebranych_wiadomosci.xaml
    /// </summary>
    public partial class Okno_odebranych_wiadomosci : Window
    {
        public Okno_odebranych_wiadomosci()
        {
            InitializeComponent();
        }

        private void odpowiedz_na_wiadomosc(object sender, RoutedEventArgs e)
        {
            Pisanie_wiadomosci wiadomosc = new Pisanie_wiadomosci();
            wiadomosc.do_kogo.Text = this.od_kogo.Text;
            wiadomosc.od_kogo.Text = this.do_kogo.Text;
            wiadomosc.Show();
            this.Close();


        }

        private void zmkanij_wiadomosc(object sender, RoutedEventArgs e)
        {
            Glowna_strona_poczty glowna_strona_poczty = new Glowna_strona_poczty();
            glowna_strona_poczty.Show();
            this.Close();
        }

        private void wczytaj_tresc(object sender, RoutedEventArgs e)
        {
            if (Zmienna.Odebrane)
            {
                try
                {
                    od_kogo.Text = Zmienna.Tabelka.Rows[Zmienna.Indeks][0].ToString();
                    do_kogo.Text = Zmienna.Email;
                    temat_wiadomosci.Text = Zmienna.Tabelka.Rows[Zmienna.Indeks][1].ToString();
                    tresc_wiadomosci.Text = Zmienna.Tabelka.Rows[Zmienna.Indeks][2].ToString();
                }
                catch
                {
                    Glowna_strona_poczty glowna_strona_poczty = new Glowna_strona_poczty();
                    glowna_strona_poczty.Show();      
                    this.Close();
                }
            }
            if (Zmienna.Wyslane)
            {
                try
                {
                    odpowiadanie_przycisk.Visibility = Visibility.Hidden;
                    od_kogo.Text = Zmienna.Email;
                    do_kogo.Text = Zmienna.Tabelka.Rows[Zmienna.Indeks][0].ToString();
                    temat_wiadomosci.Text = Zmienna.Tabelka.Rows[Zmienna.Indeks][1].ToString();
                    tresc_wiadomosci.Text = Zmienna.Tabelka.Rows[Zmienna.Indeks][2].ToString();
                }
                catch
                {
                    Glowna_strona_poczty glowna_strona_poczty = new Glowna_strona_poczty();
                    glowna_strona_poczty.Show();
                    this.Close();
                }
            }
        }
    }
}
