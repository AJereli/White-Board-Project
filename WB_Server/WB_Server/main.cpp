#include <iostream>
#include <SFML/System.hpp>
#include <SFML/Network.hpp>
#include "Client.h"
#include "Server.h"
#include <list>
#include <vector>

using namespace std;







int main() {
	
	
	Server serv;

	
	serv.startListening();

	return 0;
}