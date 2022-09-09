#include "List.h"

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
}

void List::print()
{
}
