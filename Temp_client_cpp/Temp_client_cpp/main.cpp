#pragma comment(lib, "sfml-network.lib")
#include <SFML\Network.hpp>
#include "System_Fucn.h"
using namespace std;



int main() {
	int port = 8000;
	string name = "admdfin";
	string pass = "passss";
	sf::TcpSocket socket;
	std::size_t received = 0;

	socket.connect("127.0.0.1", port);

	

	char ns[1];
	ns[0] = to_string(name.length()).c_str()[0];
	
	socket.send(ns, 1);
	socket.send(name.c_str(), name.size() + 1);
	//system("pause");
	
	socket.send(to_string(pass.length()).c_str(), 1);
	socket.send(pass.c_str(), pass.size() + 1);

	char ans[1];
	sf::Int8 t;
	sf::Packet p;
	if (socket.receive(ans, sizeof(ans), received) == sf::Socket::Done) {
		if (static_cast <int> (ans[1] == 0)) {
			cout << "Welcome" << endl;
		}
		else {
			cout << "Wrong log or pass" << endl;
		}
	}
	
	
	//cout << (int)t;

	

	system("pause");
	return 0;
}