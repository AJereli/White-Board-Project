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



bool getUsersInfo(vector <pair <string, string> > & info_of_usres); // ��������� ���������� � ������������� �� users_data.txt � ������. True � ������ ������, false �����.
	
vector <int> parseCharArr(char * arr, size_t size);