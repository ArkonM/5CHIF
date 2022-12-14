#ifndef TREE_H
#define TREE_H

#include <list>
#include "airport.h"
#include <string>



class Tree
{
public:
    Airport* root = nullptr;
    Airport* temp = nullptr;
    int target = 0;
    double mindist = 0;
    std::string name;
    std::string iata;
    Airport* minleaf;
    std::list<Airport> minroute;
    std::list<int>* enthalten = new std::list<int>();
    int added = 0;
    Tree(Airport* root);
    Tree();
    int add(int n, std::string name, std::string iata, double longi, double lati, int airline, int alliance);
    int addfromtemproot(int n, std::string name, std::string iata, double longi, double lati, int airline, int alliance);
    void setTarget(int n);
    std::list<Airport*>* getChildren();
    Airport getRoot();
private:
    void deleteminleaf();
};

#endif // TREE_H
