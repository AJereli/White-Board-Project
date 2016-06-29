#pragma once
#include <iostream>
#include <SFML/System.hpp>
#include <SFML/Network.hpp>
#include "Client.h"
#include "Server.h"
#include <list>
#include <vector>
#include <fstream>

bool getUsersInfo(vector <pair <string, string> > & info_of_usres); // —читывает информацию о пользовател€х из users_data.txt в вектор. True в случае успеха, false иначе.
	
