#pragma once
#include <vector>
#include "Client.h"


class Board
{
private:
		int board_ID;
		shared_ptr <Client> creator;
		shared_ptr <sf::TcpSocket> sock_creator;
		sf::SocketSelector members;
public:
	Board();
	Board(shared_ptr <Client> & _creator, shared_ptr <sf::TcpSocket> & _sock);

	void setBoard_ID(int b_ID) { board_ID = b_ID; }

	~Board();
};

