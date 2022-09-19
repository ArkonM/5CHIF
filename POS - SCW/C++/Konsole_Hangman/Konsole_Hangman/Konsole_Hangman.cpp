// Konsole_Hangman.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include <iostream>
#include <stdlib.h>
#include <string>

using namespace std;


string generateWord(string word_list[]) {

    int wordIndex;
    wordIndex = rand() % word_list->length();

    return word_list[ wordIndex ];
}


string checkWordSimilarity(string solution_word, string discovered_letters) {

    bool word_found = false;
    string parts_found = "";

    for (int i = 0; i < solution_word.length(); i++) {
        for (int y = 0; y < discovered_letters.length() && !word_found; y++) {
            if (tolower(solution_word[i]) == tolower(discovered_letters[y])) {
                discovered_letters.push_back(solution_word[i]);
                parts_found.push_back(solution_word[i]);
                parts_found.push_back(' ');
                word_found = true;
            }
            /*else if (!word_found) {
                cout << "Der Buchstabe: " << discovered_letters[0] << " war leider falsch!" << endl;
            }*/
        }
        if (!word_found) {
            parts_found.append("_ ");
        }
        else {
            word_found = false;
        }
    }
    return parts_found;
}


int main()
{
    string inputText;
    string solution;
    string discoveredLetters;

    string wordList[] = { "Test", "Hangman", "Armin", "Sebi" };
    solution = generateWord(wordList);

    while (true) {
        cin >> inputText;

        discoveredLetters = checkWordSimilarity(solution, inputText.append(discoveredLetters));

        cout << discoveredLetters << endl;
        
    }


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
