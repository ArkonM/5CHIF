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
	if (name == this->name) {
		//delete
		this->prev->next = this->next;
		this->next->prev = this->prev;
		delete this;
	}
	else {
		this->next->remove(name);
	}
}

void Node::print()
{
	
}
