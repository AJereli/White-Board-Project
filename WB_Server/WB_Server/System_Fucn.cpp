#include "System_Fucn.h"



bool getUsersInfo(vector <pair <string, string> > & info_of_usres) { // ��������� ���������� � ������������� �� users_data.txt � ������. True � ������ ������, false �����.
	string name, pass;
	ifstream users_read("info/users_data.txt");
	while (users_read.good()) {
		users_read >> name >> pass;
		info_of_usres.push_back(make_pair(name, pass));
	}
	users_read.close();
	return true;
}