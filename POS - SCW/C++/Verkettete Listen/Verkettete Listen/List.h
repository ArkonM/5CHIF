#pragma once

#include "Node.h"

class List {
private:
	Node* first = nullptr;
	Node* last = nullptr;

public:
	void add(std::string name);
	void remove(std::string name);
	void print();
};

