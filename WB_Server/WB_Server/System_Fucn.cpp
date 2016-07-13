#include "System_Fucn.h"



bool getUsersInfo(vector <pair <string, string> > & info_of_usres) { // —читывает информацию о пользовател€х из users_data.txt в вектор. True в случае успеха, false иначе.
	string name, pass, email;
	ifstream users_read("info/users_data.txt");
	info_of_usres.clear();
	while (users_read.good()) {
		users_read >> name >> pass >> email;
		info_of_usres.push_back(make_pair(name, pass));	
	}
	users_read.close();
	return true;
}

