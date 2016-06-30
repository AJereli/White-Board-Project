#pragma once
#include "Client.h"

class Board
{
private: 
		 int BOARD_ID = -1;
		 //vector <shared_ptr <sf::TcpSocket> > sockets;
		 shared_ptr <Client> creator;
		 vector <shared_ptr <Client> > clients;
		 sf::SocketSelector selector;
public:
	Board();
	~Board();
};

