#include "List.h"
#include <iostream>



void List::add(std::string name)
{
	if (!first) {
		first = new Node(name);
		last = first;
	}
	else {
		if (name < first->name) {
			Node* n = new Node(name);
			n->next = first;
			first->prev = n;
			first = n;
		}
		else if (name > last->name) {
			Node* n = new Node(name);
			n->prev = last;
			last->next = n;
			last = n;
		}
		else {
			first->add(name);
		}
	}
}

void List::remove(std::string name)
{
	if (first->name == name && first != last) {
		first = first->next;
		first->prev->next = nullptr;
		delete first->prev;
		first->prev == nullptr;
	}
	else if (first->name == name && first == last) {
		delete first;
		first = nullptr;
		last = first;
	}
	else if (first->name < name && name < last->name) {
		first->remove(name);
	}
	else if (last->name == name) {
		last = last->prev;
		last->next->prev = nullptr;
		delete last->next;
		last->next = nullptr;
	}
	else 
	{
		std::cout << "Name: \"" + name + "\" konnte nicht geloescht werden!" << std::endl;
	}
}

void List::print()
{

	if (!first) {
		std::cout << "Keine Liste vorhanden!" << std::endl;
	}
	else {

		std::cout << "--------------PRINT!-------------" << std::endl;
		Node* current = first;
		while (current != last) {
			std::cout << current->name << std::endl;
			current = current->next;
		}
		std::cout << current->name << std::endl;

	}
}
