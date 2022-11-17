// Hangman.cpp : Diese Datei enthält die Funktion "main". Hier beginnt und endet die Ausführung des Programms.
//

#include <iostream>

using namespace std;


void decryptWord(string word, string &guessWord, char inp, bool &letterFound);


int main()
{

    string word = "Datentechniker", guessWord = "";
    const int ARRAY_SIZE = 4;
    string galgenArray[ARRAY_SIZE] = { "________\n"
                                      " |      |\n"
                                      " |        \n"
                                      " |        \n"
                                      " |        \n"
                                      " |        \n"
                                   "  =====      \n"
                                   "  |   |      \n",
                              "________\n"
                             " |      |\n"
                             " |      O\n"
                             " |        \n"
                             " |        \n"
                             " |        \n"
                          "  =====      \n"
                          "  |   |      \n",
                              "________\n"
                             " |      |\n"
                             " |      O\n"
                             " |     \\|/\n"
                             " |        \n"
                             " |        \n"
                          "  =====      \n"
                          "  |   |      \n",
                              "________\n"
                             " |      |\n"
                             " |      O\n"
                             " |     \\|/\n"
                             " |      |\n"
                             " |    _/ \\_\n"
                          "  =====      \n"
                          "  |   |      \n",
    };
    
    int notFoundIdx = 0;

    char inp = ' ';
    bool letterFound = false, hasLost = false;

    cout << "Willkommen beim HTL Hangman!" << endl;

    for (int i = 0; i < word.size(); i++) {
        guessWord += "-";
    }

    cout << "Das gesuchte Wort ist: " << guessWord << endl;

    while (guessWord != word) {

        if (notFoundIdx == ARRAY_SIZE) {
            hasLost = true;
            cout << "Sie haben leider verloren, das Spiel ist vorbei!" << endl;
            cout << "Das gesuchte Wort ist: " << word << endl;
            break;
        }

        cout << "Gib bitte einen Buchstaben ein: ";
        cin >> inp;

        decryptWord(word, guessWord, inp, letterFound);

        if (!letterFound) {

            cout << "Dieser Buchstabe wurde leider nicht gefunden" << endl;
            cout << galgenArray[notFoundIdx];
            notFoundIdx++;

        } else {
            cout << "Das gesuchte Wort ist: " << guessWord << endl;
        }

    }

    if (!hasLost) cout << "Glückwunsch, Sie haben das Wort erraten!" << endl;
    
}


// Programm ausführen: STRG+F5 oder Menüeintrag "Debuggen" > "Starten ohne Debuggen starten"
// Programm debuggen: F5 oder "Debuggen" > Menü "Debuggen starten"

// Tipps für den Einstieg: 
//   1. Verwenden Sie das Projektmappen-Explorer-Fenster zum Hinzufügen/Verwalten von Dateien.
//   2. Verwenden Sie das Team Explorer-Fenster zum Herstellen einer Verbindung mit der Quellcodeverwaltung.
//   3. Verwenden Sie das Ausgabefenster, um die Buildausgabe und andere Nachrichten anzuzeigen.
//   4. Verwenden Sie das Fenster "Fehlerliste", um Fehler anzuzeigen.
//   5. Wechseln Sie zu "Projekt" > "Neues Element hinzufügen", um neue Codedateien zu erstellen, bzw. zu "Projekt" > "Vorhandenes Element hinzufügen", um dem Projekt vorhandene Codedateien hinzuzufügen.
//   6. Um dieses Projekt später erneut zu öffnen, wechseln Sie zu "Datei" > "Öffnen" > "Projekt", und wählen Sie die SLN-Datei aus.


void decryptWord(string word, string &guessWord, char inp, bool& letterFound)
{
    
    letterFound = false;

    if (word.find(inp) < word.length()) {       // geratener Buchstabe ist im Wort vorhanden

        for (int i = 0; i < word.size(); i++) {

            if (word[i] == inp) {
                guessWord[i] = inp;
            }

        }

        letterFound = true;
    }


}
