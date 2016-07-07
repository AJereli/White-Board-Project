#pragma once
#include <vector>
#include "System_Fucn.h"
#include "Client.h"


class Board
{
private:
		int board_ID;
		bool board_online = 0;
		shared_ptr <Client> creator = nullptr;
		vector <pair <string, vector<string> > > all_shapes; // First - settings of shape - type, id, color, thickness !!! Not otherwise !!!!
																  // Senond Part of shape in byte;
		shared_ptr <sf::TcpSocket> sock_creator;
		sf::SocketSelector members ;
		vector <shared_ptr <sf::TcpSocket>>  sock_of_members;
		sf::Thread * boar_main_thr = nullptr;
		sf::Thread * sendBoardThr = nullptr;

		void sendBoard();
public:
	
	Board(shared_ptr <Client> & _creator, shared_ptr <sf::TcpSocket> & _sock, sf::TcpListener * mem);

	void setBoard_ID(int b_ID) { board_ID = b_ID; }

	void addUser(shared_ptr <sf::TcpSocket> & _sock);
	void broadcastPainting();
	string getCreaterName();

	~Board();
};

