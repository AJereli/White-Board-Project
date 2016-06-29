#pragma comment(lib, "sfml-network.lib")
#include <SFML\Network.hpp>
#include "System_Fucn.h"
using namespace std;



int main() {
	int port = 8000;
	string name = "adm2in";
	string pass = "password";
	sf::TcpSocket socket;
	socket.connect("127.0.0.1", port);

	cout << name.size() << endl;

	char ns[1];
	ns[0] = to_string(name.length()).c_str()[0];
	
	socket.send(ns, 1);
	socket.send(name.c_str(), name.size() + 1);
	//system("pause");

	
	ns[0] = to_string(pass.length()).c_str()[0];

	socket.send(ns, 1);
	socket.send(pass.c_str(), pass.size() + 1);

	cout << "Data sended" << endl;

	system("pause");
	return 0;
}