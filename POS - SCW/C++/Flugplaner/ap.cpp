#include "ap.h"

AP::AP() {};

AP::AP(std::string name, std::string iata, double longi, double lati, int airline, int alliance)
{
    this->name = name;
    this->iata = iata;
    this->longi = longi;
    this->lati = lati;
    this->airline = airline;
    this->alliance = alliance;
}
