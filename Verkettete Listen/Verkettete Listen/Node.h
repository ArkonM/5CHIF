#pragma once
#include <string>

class Node
{
private:
	Node* next = nullptr;
	Node* prev = nullptr;
	std::string name;

public:
	Node(std::string name);
	void add(std::string name);
	void remove(std::string name);
	void print();
};

