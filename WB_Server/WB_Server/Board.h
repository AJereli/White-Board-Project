#pragma once
#include <vector>
#include "Client.h"


class Board
{
private:
		int board_ID;
		bool board_online = 0;
		shared_ptr <Client> creator;

		shared_ptr <sf::TcpSocket> sock_creator;
		sf::SocketSelector members ;
		vector <shared_ptr <sf::TcpSocket>>  sock_of_members;
		sf::Thread * boar_main_thr = nullptr;
public:
	
	Board(shared_ptr <Client> & _creator, shared_ptr <sf::TcpSocket> & _sock, sf::TcpListener * mem);

	void setBoard_ID(int b_ID) { board_ID = b_ID; }

	void addUser();
	void workingOnBoard();

	~Board();
};

