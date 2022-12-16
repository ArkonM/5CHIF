#ifndef AP_H
#define AP_H
#include <string>

class AP
{
public:
    AP();
    AP(std::string name, std::string iata, double longi, double lati, int airline, int alliance);
    std::string name;
    std::string iata;
    double longi;
    double lati;
    int airline;
    int alliance;
};

#endif // AP_H
