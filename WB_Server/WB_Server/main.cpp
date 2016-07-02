#include "System_Fucn.h"

using namespace std;


int main() {
	Server serv;
	
	
	ofstream users_write("info/users_data.txt", ios_base::app);
	
	
	serv.startListening();

	return 0;
}