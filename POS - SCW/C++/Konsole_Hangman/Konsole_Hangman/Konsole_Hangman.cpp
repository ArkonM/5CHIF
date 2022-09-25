// Konsole_Hangman.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include <iostream>
#include <stdlib.h>
#include <string>

using namespace std;

//Player health
int lifesLeft = 8;

string generateWord(string word_list[]) {

    int wordIndex;
    wordIndex = rand() % word_list->length();
    return word_list[ wordIndex ];
}


string checkWordSimilarity(string solution_word, string discovered_letters) {

    bool letter = false;
    bool found_anything = false;
    string parts_found = "";

    for (int i = 0; i < solution_word.length(); i++) {
        for (int y = 0; y < discovered_letters.length() && !letter; y++) {
            if (tolower(solution_word[i]) == tolower(discovered_letters[y])) {
                discovered_letters.push_back(solution_word[i]);
                parts_found.push_back(solution_word[i]);
                parts_found.push_back(' ');
                letter = true;
            }
        }
        if (!letter) {
            parts_found.append("_ ");
        }
        else {
            letter = false;
        }
    }
    if (!found_anything) {
        lifesLeft -= 1;
    }
    return parts_found;
}


bool checkWin(string discovered_letters) {
    
    for (int i = 0; i < discovered_letters.size(); i++) {
        if (discovered_letters[i] == '_') {
            return false;
        }
        else if (lifesLeft == 0) {
            return true;
        }
    }
    return true;
}


void zeroLifes() {
    cout << "  ________" << endl;
    cout << "  |      |" << endl;
    cout << "  |      o" << endl;
    cout << "  |     \\|/" << endl;
    cout << "  |      |" << endl;
    cout << "  |    _/ \\_" << endl;
    cout << "=====" << endl;
    cout << "|   |" << endl;
    return;
}

void oneLife() {
    cout << "  ________" << endl;
    cout << "  |      |" << endl;
    cout << "  |      o" << endl;
    cout << "  |     \\|/" << endl;
    cout << "  |      |" << endl;
    cout << "  |" << endl;
    cout << "=====" << endl;
    cout << "|   |" << endl;
    return;
}

void twoLifes() {
    cout << "  ________" << endl;
    cout << "  |      |" << endl;
    cout << "  |      o" << endl;
    cout << "  |      |" << endl;
    cout << "  |      |" << endl;
    cout << "  |" << endl;
    cout << "=====" << endl;
    cout << "|   |" << endl;
    return;
}

void threeLifes() {
    cout << "  ________" << endl;
    cout << "  |      |" << endl;
    cout << "  |      o" << endl;
    cout << "  |" << endl;
    cout << "  |" << endl;
    cout << "  |" << endl;
    cout << "=====" << endl;
    cout << "|   |" << endl;
    return;
}

void fourLifes() {
    cout << "  ________" << endl;
    cout << "  |      |" << endl;
    cout << "  |" << endl;
    cout << "  |" << endl;
    cout << "  |" << endl;
    cout << "  |" << endl;
    cout << "=====" << endl;
    cout << "|   |" << endl;
    return;
}

void fiveLifes() {
    cout << "  ________" << endl;
    cout << "  |" << endl;
    cout << "  |" << endl;
    cout << "  |" << endl;
    cout << "  |" << endl;
    cout << "  |" << endl;
    cout << "=====" << endl;
    cout << "|   |" << endl;
    return;
}

void sixLifes() {
    cout << "" << endl;
    cout << "  |" << endl;
    cout << "  |" << endl;
    cout << "  |" << endl;
    cout << "  |" << endl;
    cout << "  |" << endl;
    cout << "=====" << endl;
    cout << "|   |" << endl;
    return;
}

void sevenLifes() {
    cout << "" << endl;
    cout << "" << endl;
    cout << "" << endl;
    cout << "" << endl;
    cout << "" << endl;
    cout << "" << endl;
    cout << "=====" << endl;
    cout << "|   |" << endl;
    return;
}

void eightLifes() {
    cout << "" << endl;
    cout << "" << endl;
    cout << "" << endl;
    cout << "" << endl;
    cout << "" << endl;
    cout << "" << endl;
    cout << "" << endl;
    cout << "" << endl;
    return;
}


void printHangman() {

    switch (lifesLeft) {

        case 0: zeroLifes();  break;
        case 1: oneLife();    break;
        case 2: twoLifes();   break;
        case 3: threeLifes(); break;
        case 4: fourLifes();  break;
        case 5: fiveLifes();  break;
        case 6: sixLifes();   break;
        case 7: sevenLifes(); break;
        case 8: eightLifes(); break;
    }
}


//Hangman prints



int main()
{
    srand(time(NULL));

    string inputText;
    string solution;
    string discoveredLetters;

    string wordList[] = { "Test", "Hangman", "Armin", "Sebi" };
    solution = generateWord(wordList);

    printHangman();
    

    do {
        cin >> inputText;
        system("cls");
        discoveredLetters = checkWordSimilarity(solution, inputText.append(discoveredLetters));
        printHangman();
        cout << discoveredLetters << endl;
        cout << lifesLeft << endl;
    } while (!checkWin(discoveredLetters));

}

// Run program: Ctrl + F5 or Debug > Start Without Debugging menu
// Debug program: F5 or Debug > Start Debugging menu

// Tips for Getting Started: 
//   1. Use the Solution Explorer window to add/manage files
//   2. Use the Team Explorer window to connect to source control
//   3. Use the Output window to see build output and other messages
//   4. Use the Error List window to view errors
//   5. Go to Project > Add New Item to create new code files, or Project > Add Existing Item to add existing code files to the project
//   6. In the future, to open this project again, go to File > Open > Project and select the .sln file
