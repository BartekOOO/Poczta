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
    /// Logika interakcji dla klasy Ustawienia_konta.xaml
    /// </summary>
    public partial class Ustawienia_konta : Window
    {
        public Ustawienia_konta()
        {
            InitializeComponent();
            imie_blok.Text = Zmienna.Imie;
            nazwisko_blok.Text = Zmienna.Nazwisko;
            haslo_blok.Text = Funkcje.zagwiazdkuj(Zmienna.Haslo);
            miasto_blok.Text = Zmienna.Miasto;
            numer_domu_i_ulica_blok.Text = $"{Zmienna.Ulica} {Zmienna.Nr_domu}";
        }

        private void zmien_imie_i_nazwisko_procedura(object sender, RoutedEventArgs e)
        {
           
            zmiana_imienia_i_nazwiska.Visibility = Visibility.Visible;
            zmiana_miejsca_zamieszkania.Visibility = Visibility.Hidden;
            zmiana_hasla.Visibility = Visibility.Hidden;
            usuwanie_konta_name.Visibility = Visibility.Hidden;
        }

        private void zmien_miejsce_zamieszkania(object sender, RoutedEventArgs e)
        {
            zmiana_hasla.Visibility = Visibility.Hidden;
            zmiana_imienia_i_nazwiska.Visibility = Visibility.Hidden;
            zmiana_miejsca_zamieszkania.Visibility = Visibility.Visible;
            usuwanie_konta_name.Visibility = Visibility.Hidden;
        }

        private void powrot_do_glownej_strony_admina(object sender, RoutedEventArgs e)
        {
            Glowna_strona_poczty glowna_strona_poczty = new Glowna_strona_poczty();
            glowna_strona_poczty.Show();
            this.Close();
        }

        bool haslo = true;
        private void pokaz_haslo(object sender, RoutedEventArgs e)
        {
            if (haslo)
            {
                haslo = false;
                haslo_blok.Text = Zmienna.Haslo;

            }
            else
            {
                haslo = true;
                haslo_blok.Text = Funkcje.zagwiazdkuj(Zmienna.Haslo);
            }



        }

      

        private void zatwierdz_zmiane_imienia_i_nazwiska(object sender, RoutedEventArgs e)
        {
            string imie = imie_blok.Text;
            string nazwisko = nazwisko_blok.Text;
            SQLiteCommand komenda = new SQLiteCommand();
            SQLiteConnection baza = new SQLiteConnection("Data Source=Baza_danych.db; Version=3;");
            baza.Open();

            if (string.IsNullOrEmpty(nazwisko) || string.IsNullOrEmpty(imie))
            {
                MessageBox.Show("Pole na nazwisko lub imie jest puste");
            }
            else
            {
                if (Funkcje.Sprawdz_czy_napis(imie) && Funkcje.Sprawdz_czy_napis(nazwisko))
                {
                    if (Funkcje.Czy_odpowiednia_ilosc_liter(imie, 25, 3) && Funkcje.Czy_odpowiednia_ilosc_liter(nazwisko, 30, 3))
                    {

                        Zmienna.Imie = Funkcje.Duza_literka(nowe_imie_blok.Text);
                        Zmienna.Nazwisko = Funkcje.Duza_literka(nowe_nazwisko_blok.Text);

                        imie_blok.Text = Zmienna.Imie;
                        nazwisko_blok.Text = Zmienna.Nazwisko;

                        komenda = baza.CreateCommand();
                        komenda.CommandText = $"update Urzytkownik set Imie='{Zmienna.Imie}', Nazwisko='{Zmienna.Nazwisko}' where Id_urzytkownika={Zmienna.Id};";
                        komenda.ExecuteNonQuery();
                        MessageBox.Show("Imie i nazwisko zostało zmienione");
                        nowe_imie_blok.Text = "";
                        nowe_nazwisko_blok.Text = "";
                        zmiana_imienia_i_nazwiska.Visibility = Visibility.Hidden;


                    }
                    else
                    {
                        MessageBox.Show("Pole imie lub nazwisko jest zbyt krótkie/długie");
                    }
                }
                else
                {
                    MessageBox.Show("Imie lub nazwisko jest źle podane");
                }
            }
        }

        private void zatwierdz_zmiane_zamieszkania(object sender, RoutedEventArgs e)
        {
            string Miasto = nowe_miasto_blok.Text;
            string Ulica = nowa_ulica_blok.Text;
            string Nr_domu = nowy_nr_blok.Text;
            int ok = 0;
            SQLiteCommand komenda = new SQLiteCommand();
            SQLiteConnection baza = new SQLiteConnection("Data Source=Baza_danych.db; Version=3;");
            baza.Open();

            if (string.IsNullOrEmpty(Miasto) || string.IsNullOrEmpty(Ulica))
            {
                MessageBox.Show("Pola miasto bądź ulicą są puste");
            }
            else
            {
                if (Funkcje.Sprawdz_czy_napis(Miasto) && Funkcje.Sprawdz_czy_napis(Ulica))
                {
                    if (Funkcje.Czy_odpowiednia_ilosc_liter(Miasto, 50, 1) && Funkcje.Czy_odpowiednia_ilosc_liter(Ulica, 50, 1))
                    {
                        if (int.TryParse(Nr_domu, out ok))
                        {

                            Zmienna.Ulica = Funkcje.Duza_literka(Ulica);
                            Zmienna.Miasto = Funkcje.Duza_literka(Miasto);
                            Zmienna.Nr_domu = ok;

                            miasto_blok.Text = Zmienna.Miasto;
                            numer_domu_i_ulica_blok.Text = $"{Zmienna.Ulica} {Zmienna.Nr_domu}";

                            komenda = baza.CreateCommand();
                            komenda.CommandText = $"update Urzytkownik set Miasto='{Zmienna.Miasto}', Ulica='{Zmienna.Ulica}', Nr_domu='{Zmienna.Nr_domu}' where Id_urzytkownika={Zmienna.Id};";
                            komenda.ExecuteNonQuery();
                            MessageBox.Show("Nowe dane zamieszkania zostały zmienione");
                            nowe_imie_blok.Text = "";
                            nowe_nazwisko_blok.Text = "";
                            zmiana_miejsca_zamieszkania.Visibility = Visibility.Hidden;


                        }
                        else
                        {
                            MessageBox.Show("Numer domu jest źle podany");
                        }

                    }
                    else
                    {
                        MessageBox.Show("Miasto lub ulica są zbyt krótkie/długie");
                    }
                }
                else
                {
                    MessageBox.Show("Miasto lub ulica są źle podane");
                }
            }
        }

        private void zmiana_hasla_click(object sender, RoutedEventArgs e)
        {
            zmiana_hasla.Visibility = Visibility.Visible;
            zmiana_imienia_i_nazwiska.Visibility = Visibility.Hidden;
            zmiana_miejsca_zamieszkania.Visibility = Visibility.Hidden;
            usuwanie_konta_name.Visibility = Visibility.Hidden;
        }

        private void zatwierdz_zmiane_hasla(object sender, RoutedEventArgs e)
        {
            string haslo1 = nowe_haslo1_blok.Text;
            string haslo2 = nowe_haslo2_blok.Text;
            
            
            SQLiteCommand komenda = new SQLiteCommand();
            SQLiteConnection baza = new SQLiteConnection("Data Source=Baza_danych.db; Version=3;");
            baza.Open();

            if(haslo1==haslo2)
            {
                if (Funkcje.czy_dobre_haslo(haslo1))
                {
                    if (Funkcje.Czy_odpowiednia_ilosc_liter(haslo1, 25, 5))
                    {

                        komenda = baza.CreateCommand();
                        komenda.CommandText = $"update Urzytkownik set Haslo='{haslo1}' where Id_urzytkownika={Zmienna.Id};";
                        komenda.ExecuteNonQuery();
                        MessageBox.Show("Hasło zostało pomyślnie zmienione");
                        nowe_haslo1_blok.Text = "";
                        nowe_haslo2_blok.Text = "";
                        zmiana_hasla.Visibility = Visibility.Hidden;

                    }
                    else
                    {
                        MessageBox.Show("Hasło jest zbyt długie/krótkie");
                    }

                }
                else
                {
                    MessageBox.Show("Haslo powinno zawierać conajmniej jedną wielką, małą literę i liczbę");
                }
            }
            else
            {
                MessageBox.Show("Hasła są różne");
            }







        }

        private void usun_konto_pokaz(object sender, RoutedEventArgs e)
        {
            usuwanie_konta_name.Visibility = Visibility.Visible;

            zmiana_hasla.Visibility = Visibility.Hidden;
            zmiana_imienia_i_nazwiska.Visibility = Visibility.Hidden;
            zmiana_miejsca_zamieszkania.Visibility = Visibility.Hidden;

        }

        private void zatwierdz_usuwanie_konta(object sender, RoutedEventArgs e)
        {
            string haslo1 = nowe_haslo1_blok1.Text;
            string haslo2 = nowe_haslo2_blok1.Text;


            SQLiteCommand komenda = new SQLiteCommand();
            SQLiteConnection baza = new SQLiteConnection("Data Source=Baza_danych.db; Version=3;");
            baza.Open();

            if (string.IsNullOrEmpty(nowe_haslo1_blok1.Text) || string.IsNullOrEmpty(nowe_haslo2_blok1.Text))
            {
                MessageBox.Show("Pola są puste");
            }
            else
            {
                if (haslo1 == haslo2)
                {
                    if (Funkcje.czy_dobre_haslo(haslo1))
                    {
                        if (Funkcje.Czy_odpowiednia_ilosc_liter(haslo1, 25, 5))
                        {
                            if (MessageBox.Show("Czy na pewno chcesz usunąć konto?", "Potwierdzenie", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                            {
                                komenda = baza.CreateCommand();
                                komenda.CommandText = $"delete from Urzytkownik where Id_urzytkownika={Zmienna.Id};";
                                komenda.ExecuteNonQuery();
                                MainWindow strona_logowania = new MainWindow();
                                strona_logowania.Show();
                                baza.Close();
                                this.Close();
                            }
                            else
                            {

                                nowe_haslo1_blok1.Text = "";
                                nowe_haslo2_blok1.Text = "";
                                usuwanie_konta_name.Visibility = Visibility.Hidden;
                            }



                        }
                        else
                        {
                            MessageBox.Show("Hasło jest zbyt długie/krótkie");
                        }

                    }
                    else
                    {
                        MessageBox.Show("Haslo powinno zawierać conajmniej jedną wielką, małą literę i liczbę");
                    }
                }
                else
                {
                    MessageBox.Show("Hasła są różne");
                }
            }
        }
    }
}
