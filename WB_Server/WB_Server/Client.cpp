#include "Client.h"



Client::Client()
{
	client_socket = new sf::TcpSocket;
}


Client::~Client()
{
	if (client_socket != nullptr) {
		cout << "del client_socket" << endl;
		delete client_socket;
	}
}
