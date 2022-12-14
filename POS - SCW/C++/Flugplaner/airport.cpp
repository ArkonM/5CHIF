#include "airport.h"
#include <math.h>

#include <iostream>

Airport::Airport(){
    this->id = -1;
};

Airport::Airport(int id, Airport* root, std::string name, std::string iata, int longi, int lati, int airline, int alliance) {
    this->id = id;
    this->root = root;
    this->name = name;
    this->iata = iata;
    this->longi = longi;
    this->lati = lati;
    this->airline = airline;
    this->alliance = alliance;
    if(root != nullptr) {
        getDistancetoroot();
    }
}

Airport::Airport(Airport *n) {
    this->id = n->id;
}

void Airport::addChild(Airport* n) {
    if((std::find(children->begin(), children->end(), n) != children->end()) == false) {
        children->push_back(n);
    }
}

void Airport::deletechildren(Airport* n) {
    children->remove(n);
}

void Airport::getDistancetoroot() {
    int longidif = 0;
    int latidif = 0;
    longidif = this->longi - this->root->longi;
    latidif = this->lati - this->root->lati;

    if(airline == -1 && alliance == -1) {
        this->distancetoroot = ((int) sqrt(((pow(longidif, 2))+(pow(latidif, 2))))) + this->root->distancetoroot + 0.002;
    } else if(airline == -1 && alliance != -1) {
        this->distancetoroot = ((int) sqrt(((pow(longidif, 2))+(pow(latidif, 2))))) + this->root->distancetoroot + 0.001;
    } else {
        this->distancetoroot = ((int) sqrt(((pow(longidif, 2))+(pow(latidif, 2))))) + this->root->distancetoroot;
    }
}
