#include "Node.h"
#include <string>


Node::Node(std::string name)
{
	this->name = name;
}

void Node::add(std::string name)
{
	if (name < this->name) {
		Node* n = new Node(name);
		n->next = this;
		n->prev = prev;
		prev->next = n;
		prev = n;
	}
	else {
		next->add(name);
	}
}

void Node::remove(std::string name)
{
	if (next) {
		if (name == next->name) {
			//delete
			next = next->next;
			next->prev->next = nullptr;
			next->prev->prev = nullptr;
			delete next->prev;
			next->prev = this;
		}
		else {
			next->remove(name);
		}
	}
}

void Node::print()
{
	
}
