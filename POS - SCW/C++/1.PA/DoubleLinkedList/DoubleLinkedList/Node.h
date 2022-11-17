#pragma once

#include <string>

class List;

class Node
{
	friend List;		// für List-Klasse ist jz alles  von Node-Klasse public
private:
	std::string name;
	Node* previous = nullptr;
	Node* next = nullptr;

public:
	Node(std::string name);
	void add(std::string name);
	void remove(std::string name);
	void print();
	std::string get_name() { return name; }
	~Node();
};

