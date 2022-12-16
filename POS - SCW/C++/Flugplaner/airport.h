#ifndef AIRPORT_H
#define AIRPORT_H

#include <list>
#include <vector>
#include <string>



class Airport
{
public:
    int id = 0;
    Airport* root = NULL;
    std::list<Airport*>* children = new std::list<Airport*>();
    double distancetoroot = 0;
    std::string name;
    std::string iata;
    int longi = 0;
    int lati  = 0;
    int airline = -1;
    int alliance = -1;
    Airport();
    Airport(int id, Airport* root, std::string name, std::string iata, int longi, int lati, int airline, int alliance);
    Airport(Airport *n);
    void addChild(Airport* n);
    void deletechildren(Airport* n);
private:
    void getDistancetoroot();
};

#endif // AIRPORT_H
