#include "Node.h"
#include <string>
#include <iostream>

using namespace std;

Node::Node(string n) : name(n) {}

void Node::add(string name)
{
	if (name < this->name) {

		Node* n = new Node(name);
		n->next = this;
		n->previous = previous;
		previous->next = n;
		previous = n;

	} else {

		next->add(name);

	}
}

void Node::remove(string name)
{
	
	if (this != nullptr) {

		if (this->name == name) {

			this->previous->next = this->next;
			this->next->previous = this->previous;
			delete this;

			/* scw Lösung:
			next = next->next;
			next->previous->next = nullptr;
			next->previous->previous = nullptr;
			delete next->previous;
			next->previous = this;
			*/
		
		} else if (next != nullptr) {

			/* scw Lösung: } else {*/
			next->remove(name);

		}
	}

}

void Node::print()
{
	cout << name << endl;
	if (next != nullptr) {
		next->print();
	}
}

Node::~Node()
{

}
