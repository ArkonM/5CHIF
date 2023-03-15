using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
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

namespace BSP_Projekt_PA3
{
    /// <summary>
    /// Interaction logic for Client.xaml
    /// </summary>
    public partial class Client : Window
    {
        Transfer t;
        TcpClient client = null;
        MSG msg = new MSG();
        public Client()
        {
            InitializeComponent();
            // Erstellt eine neue TCP-Verbindung zum Server
            client = new TcpClient("localhost", 12345);

            // Erstellt eine neue "Transfer"-Instanz zum Senden von Nachrichten an den Server
            t = new Transfer(client);
        }

        private void Button_Ausfuehren_Click(object sender, RoutedEventArgs e)
        {
            // Liest den Text aus dem Textfeld "TextBox_Code"
            string temp = "";
            string Text = this.TextBox_Code.Text;

            // Trennt den Text anhand von Semikolons und fügt jeden Abschnitt zur "Messages"-Liste hinzu
            for (int i = 0; i < Text.Length; i++)
            {
                if (Text[i] == ';')
                {
                    msg.Messages.Add(temp);
                    temp = "";
                }
                else
                {
                    temp = temp + Text[i];
                }
            }

            // Überprüft den Code auf Gültigkeit und sendet die Nachricht an den Server
            if (check_code())
            {
                t.Send(msg);
            }
            msg.Messages.Clear();
        }
        // Überprüft den Code auf Gültigkeit
        private bool check_code()
        {
            for (int i = 0; i < msg.Messages.Count; i++)
            {
                // Überprüft, ob jede Nachricht ein gültiges Kommando enthält
                if (!(msg.Messages[i] == "vor()" || msg.Messages[i] == "rechts()" || msg.Messages[i] == "links()" || msg.Messages[i] == "zurueck()"))
                {
                    // Zeigt eine Fehlermeldung an, wenn ein ungültiges Kommando gefunden wird
                    MessageBox.Show("Du idiot kannst nicht richtig schreiben" + msg.Messages[i]);

                    // Löscht alle gesammelten Nachrichten und gibt "false" zurück, um den Sendevorgang abzubrechen
                    msg.Messages.Clear();
                    return false;
                }
            }

            // Gibt "true" zurück, wenn alle Nachrichten gültige Kommandos enthalten
            return true;
        }

        // Event-Handler-Methode, die aufgerufen wird, wenn das Hauptfenster geschlossen wird
        private void Window_Closed(object sender, EventArgs e)
        {
            // Stoppt den Transfer und schließt die TCP-Verbindung zum Server
            t.Stop();
        }
    }
}
