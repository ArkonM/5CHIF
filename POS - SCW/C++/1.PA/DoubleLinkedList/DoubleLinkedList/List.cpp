#include "List.h"
#include <iostream>

using namespace std;


List::List(){}

void List::add(string name)
{
	if (!first) {		// first == nullptr

		first = new Node(name);
		last = first;

	} else {
		if (name < first->name) {

			Node* n = new Node(name);
			n->next = first;
			first->previous = n;
			first = n;

		} else if (name > last->name) {

			Node* n = new Node(name);
			n->previous = last;
			last->next = n;
			last = n;

		} else {

			first->add(name);

		}
	}
}

void List::remove(string name)
{

	if (first != nullptr && last != nullptr && first == last && first->name == name) {

		first = nullptr;
		last = nullptr;
		delete first;
		delete last;

	} else if (first != nullptr && first->name == name) {

		first = first->next;
		first->previous = nullptr;
		delete first->previous;
	
	} else if (last != nullptr && last->name == name)  {

		last = last->previous;
		last->next = nullptr;
		delete last->next;

	} else {

		if (first != nullptr) {
		
			first->remove(name);
		
		}

	}


	/*Node* n = first;

	while (n != nullptr && n->name != name) {
		n = n->next;
	}

	//n->remove();

	if (n->previous == nullptr) {

		first = n->next;
		first->previous = nullptr;

	}
	else if (n->next == nullptr) {

		last = n->previous;
		last->next = nullptr;

	}
	else {

		n->previous->next = n->next;
		n->next->previous = n->previous;

	}*/

}

void List::print()
{
	/*Node* n = first;
	while (n->next != nullptr) {
		cout << n->name << endl;
		n = n->next;
	}
	cout << n->name << endl;*/

	if (first != nullptr) {
		first->print();
	}
}

List::~List()
{

	// kontrollieren!
	Node* n;

	while (first != nullptr) {

		n = first;
		first = first->next;
		delete n;

	}
}
