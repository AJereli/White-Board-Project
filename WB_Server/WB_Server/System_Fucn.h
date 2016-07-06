#pragma once
#include <iostream>
#include <SFML/System.hpp>
#include <SFML/Network.hpp>
#include "Client.h"
#include "Server.h"
#include <list>
#include <vector>
#include <fstream>
#include <string> 
#include <sstream>



bool getUsersInfo(vector <pair <string, string> > & info_of_usres); // —читывает информацию о пользовател€х из users_data.txt в вектор. True в случае успеха, false иначе.
	
vector <int> parseCharArr(char * arr, size_t size);