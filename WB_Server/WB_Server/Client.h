#pragma once
#include <SFML/System.hpp>
#include <SFML/Network.hpp>
#include "consts.h"
#include "System_Fucn.h"
#include <iostream>
#include "Board.h"
class Client {
private: sf::TcpSocket * client_socket = nullptr;
		 char status = USER_WAIT_CONFIRM;
		 int board_ID = -1;
		 string name = "NoName";
public:
	Client();
	
	sf::TcpSocket * getClietnSocket() { return const_cast <sf::TcpSocket *> (client_socket); }
	char getStatus() { return status; }
	void setStatus(const char st) { status = st; }
	void setName(char * _name) { name = _name; }
	int getBoard_ID() { return board_ID; }
	void setBoard_ID(int & b_ID) { board_ID = b_ID; }
	~Client();
};

