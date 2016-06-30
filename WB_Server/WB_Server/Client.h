#pragma once
#include <SFML/System.hpp>
#include <SFML/Network.hpp>
#include "consts.h"
#include "System_Fucn.h"
#include <iostream>
class Client {
private: sf::TcpSocket * client_socket = nullptr;
		 char status = USER_WAIT_CONFIRM;
public:
	Client();
	
	sf::TcpSocket * getClietnSocket() { return const_cast <sf::TcpSocket *> (client_socket); }
	char getStatus() { return status; }
	void setStatus(const char st) { status = st; }
	~Client();
};

