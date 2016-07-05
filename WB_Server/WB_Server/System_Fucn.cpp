#include "System_Fucn.h"



bool getUsersInfo(vector <pair <string, string> > & info_of_usres) { // —читывает информацию о пользовател€х из users_data.txt в вектор. True в случае успеха, false иначе.
	string name, pass;
	ifstream users_read("info/users_data.txt");
	while (users_read.good()) {
		users_read >> name >> pass;
		info_of_usres.push_back(make_pair(name, pass));
	}
	users_read.close();
	return true;
}

vector <int> parseCharArr(char * arr, size_t size) {
	int maxPart = 1;
	vector <int> parsed;
	/*for (int i = 0, maxPart = 0; i < sizeof(arr); i++)
		arr[i] == '+' ? maxPart++ : 1;*/
	string nonPars(arr, size);
	int f_p = nonPars.find_first_of('+'); // f_p - first plus, находит место первого вхождени€ знака '+'
	parsed.push_back(atoi(nonPars.substr(0, f_p).c_str()));
	parsed.push_back(atoi(nonPars.substr(f_p + 1, size).c_str()));

	cout << "quer: " << (int)arr[0] << " type: " << (int)arr[1] << endl;
	return parsed;
}