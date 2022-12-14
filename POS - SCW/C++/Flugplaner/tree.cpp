#include "tree.h"
#include <iostream>

using namespace std;

Tree::Tree() {
}

Tree::Tree(Airport* root) {
    this->root = root;
}

int Tree::add(int n, std::string name, std::string iata, double longi, double lati, int airline, int alliance) {
    int longitude = (int) (longi+180);
    int latitude = (int) (90-lati);
    bool contains = (std::find(enthalten->begin(), enthalten->end(), n) != enthalten->end());
    if(root != nullptr && contains == false) {
        if(n == target) {
            Airport* temp = new Airport(n, root, name, iata, longitude, latitude, airline, alliance);
            mindist = temp->distancetoroot;
            cout << (mindist) << endl;
            //return temp;
            return n;
         } else {
            Airport* temp = new Airport(n, root, name, iata, longitude, latitude, airline, alliance);
            if((temp->distancetoroot < mindist && mindist != 0) || mindist == 0) {
                root->addChild(temp);
                enthalten->push_back(n);
                added++;
             }
          }
    } else if (root == NULL) {
        root = new Airport(n, root, name, iata, longitude, latitude, airline, alliance);
    }
    //return Airport();
    return 0;
}

int Tree::addfromtemproot(int n, std::string name, std::string iata, double longi, double lati, int airline, int alliance) {
    int longitude = (int) (longi+180);
    int latitude = (int) (90-lati);
    bool contains = (std::find(enthalten->begin(), enthalten->end(), n) != enthalten->end());
    Airport* t = new Airport(n, temp, name, iata, longitude, latitude, airline, alliance);
    if(root != nullptr && contains == false) {
        if(n == target) {
            if(t->distancetoroot < mindist || mindist == 0) {
                if(mindist != 0) {
                    deleteminleaf();
                }
                minleaf = t;
                mindist = t->distancetoroot;
                root->addChild(t);
                cout << ("setminroute") << endl;
            }
            Airport* temp = minleaf;
            while(temp != root) {
                temp = temp->root;
            }
            //return Airport();
            return 0;
        } else {
            if((t->distancetoroot < mindist && mindist != 0) || mindist == 0) {
                root->addChild(t);
                enthalten->push_back(n);
                added++;
                cout << ("new Node") << " " << (n) << endl;
            }
        }
    } else if(root == nullptr) {
        root = t;
    }
    //return Airport();
    return 0;
}

void Tree::setTarget(int n) {
    target = n;
}

list<Airport*>* Tree::getChildren() {
    return root->children;
}

Airport Tree::getRoot() {
    return root;
}

void Tree::deleteminleaf() {
    minleaf->root->deletechildren(minleaf);
}
