#pragma once

#include <string>

#include "Node.h"

class List
{

private:
	Node* first = nullptr;
	Node* last = nullptr;

public:
	List();
	void add(std::string name);
	void remove(std::string name);
	void print();
	~List();
};

