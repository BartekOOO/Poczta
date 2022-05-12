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
    /// Logika interakcji dla klasy Okno_rejestracji.xaml
    /// </summary>
    public partial class Okno_rejestracji : Window
    {
        public Okno_rejestracji()
        {
            InitializeComponent();
            losowy_napis.Content = Funkcje.losowanie();
        }

        private void rejestracja_click(object sender, RoutedEventArgs e)
        {
            string imie = imie_urzytkownik_tekst.Text;
            string nazwisko = nazwisko_urzytkownik_tekst.Text;
            string email = pseudinim_urzytkownik_tekst.Text;
            string haslo1 = haslo1_urzytkownik_tekst.Password;
            string haslo2 = haslo2_urzytkownik_tekst.Password;
            string przepisany_kod = losowy_tekst_blok.Text;
            string losowy_kod = losowy_napis.Content.ToString();
            string miasto = miasto_urzytkownik_tekst.Text;
            string ulica = ulica_urzytkownik_tekst.Text;
            string nr_domu = numer_domu_urzytkownik_tekst.Text;
            int dwadaw = 0;

            SQLiteConnection baza = new SQLiteConnection("Data Source=Baza_danych.db; Version=3;");
            baza.Open();
            SQLiteCommand komenda;


            if (string.IsNullOrEmpty(imie) || string.IsNullOrEmpty(nazwisko) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(haslo1) || string.IsNullOrEmpty(haslo2) || string.IsNullOrEmpty(losowy_kod) || string.IsNullOrEmpty(miasto) || string.IsNullOrEmpty(ulica) || string.IsNullOrEmpty(nr_domu))
            {
                MessageBox.Show("Aby się zarejestrować wypełnij formularz");
            }
            else
            {

                if (Funkcje.Sprawdz_czy_napis(imie))
                {
                    if (Funkcje.Czy_odpowiednia_ilosc_liter(imie, 25, 3))
                    {
                        if (Funkcje.Sprawdz_czy_napis(nazwisko))
                        {
                            if (Funkcje.Czy_odpowiednia_ilosc_liter(nazwisko, 30, 3))
                            {
                                if (Funkcje.Sprzawdz_czy_sie_nie_powtarza(baza, email))
                                {
                                    if (haslo1 == haslo2)
                                    {
                                        if (Funkcje.czy_dobre_haslo(haslo1))
                                        {
                                            if (Funkcje.Czy_odpowiednia_ilosc_liter(haslo1, 25, 5))
                                            {
                                                if (losowy_kod == przepisany_kod)
                                                {
                                                    if (Funkcje.Czy_odpowiednia_ilosc_liter(miasto, 50, 1))
                                                    {
                                                        if (Funkcje.Sprawdz_czy_napis(miasto))
                                                        {
                                                            if (Funkcje.Czy_odpowiednia_ilosc_liter(ulica, 50, 1))
                                                            {
                                                                if (Funkcje.Sprawdz_czy_napis(ulica))
                                                                {
                                                                    if (int.TryParse(nr_domu, out dwadaw))
                                                                    {
                                                                        if (Funkcje.Sprzawdz_czy_sie_nie_powtarza(baza,email))
                                                                        {
                                                                            if (Funkcje.czy_dobry_email(email))
                                                                            {

                                                                                komenda = baza.CreateCommand();
                                                                                komenda.CommandText = $"insert into Urzytkownik (Imie, Nazwisko , Email , Haslo , Miasto , Ulica , Nr_domu) values ('{Funkcje.Duza_literka(imie)}','{Funkcje.Duza_literka(nazwisko)}','{email + "@bartek.pl"}','{haslo1}','{Funkcje.Duza_literka(miasto)}','{Funkcje.Duza_literka(ulica)}',{nr_domu});";
                                                                                komenda.ExecuteNonQuery();
                                                                                MessageBox.Show("Pomyślnie zarejestrowano nowe konto");
                                                                                MainWindow panel_logowania = new MainWindow();
                                                                                panel_logowania.Show();
                                                                                baza.Close();
                                                                                this.Close();
                                                                                
                                                                            }
                                                                            else
                                                                            {
                                                                                MessageBox.Show("Adres email jest źle podany");
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            MessageBox.Show("Dany adres email jest już zajęty");
                                                                        }

                                                                    }
                                                                    else
                                                                    {
                                                                        MessageBox.Show("Numer domu jest źle podany");
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    MessageBox.Show("Źle podałeś ulicę");
                                                                }
                                                            }
                                                            else
                                                            {
                                                                MessageBox.Show("Ulica jest zbyt krótka/długa");
                                                            }
                                                        }
                                                        else
                                                        {
                                                            MessageBox.Show("Źle podałeś miasto");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        MessageBox.Show("Miasto jest zbyt krótkie/długie");
                                                    }
                                                }
                                                else
                                                {
                                                    MessageBox.Show("Źle przepisałeś kod");
                                                }
                                            }
                                            else
                                            {
                                                MessageBox.Show("Hasło jest zbyt krótkie");
                                            }

                                        }
                                        else
                                        {
                                            MessageBox.Show("Haslo powinno zawierać conajmniej jedną wielką, małą literę i liczbę");
                                        }

                                    }
                                    else
                                    {
                                        MessageBox.Show("Pola z hasłami się różnią");
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Nazwa urzytkownika już istnieje");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Dlugosc nazwiska jest zbyt dluga/krótka");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Błędnie podane nazwisko");
                        }

                    }
                    else
                    {
                        MessageBox.Show("Dlugosc imienia jest zbyt dluga/krótka");
                    }

                }
                else
                {
                    MessageBox.Show("Błędnie podane imie");
                }
            }

        }

       
    }
}
