// UPN_Rechner.cpp : Diese Datei enthält die Funktion "main". Hier beginnt und endet die Ausführung des Programms.
//

#include <iostream>
#include <string>
#include <stack>
#include "UPN_Rechner.h"

using namespace std;

string getInputString();

int main()
{

    char operators[] = { '+', '-', '*', '/' };

    stack<int> numbersStack;

    string inp = getInputString();

    //cout << inp;

    for (int i = 0; i < inp.size(); i++)
    {

        if (isdigit(inp[i])) {

            numbersStack.push(inp[i] - '0'); //cast char to int

        } else if (find(begin(operators), end(operators), inp[i]) != end(operators)) {        // -> array.contains(value)

            int firstValue = numbersStack.top();
            numbersStack.pop();

            int secondValue = numbersStack.top();
            numbersStack.pop();

            switch (inp[i]) {

                case '+': numbersStack.push(firstValue + secondValue); break;
                case '-': numbersStack.push(firstValue - secondValue); break;
                case '*': numbersStack.push(firstValue * secondValue); break;
                case '/': numbersStack.push(firstValue / secondValue); break;

            }

        }
    
    }

    if (!numbersStack.empty()) {

        cout << "Ergebnis: " << numbersStack.top() << endl;
        numbersStack.pop();
    }

}

string getInputString() {

    string inp;

    cout << "Willkommen beim UPN Rechner!" << endl;
    cout << "Bitte geben Sie eine Rechnung ein: ";

    getline(cin, inp);

    cout << endl;

    return inp;

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
