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
		vector <pair <pair <string, string>, vector<string> > > all_shapes; // Pair - settings of shape - type, color, thickness in first, transform matrix in second
																  // Senond - Parts of shape in byte;
		shared_ptr <sf::TcpSocket> sock_creator;
		list <pair < shared_ptr <sf::TcpSocket>, shared_ptr <Client> > > users; // Список пользователей. Пара из клиент-сокета и клиента - пользователь.
		sf::SocketSelector members ;
	//	vector <shared_ptr <sf::TcpSocket>>  sock_of_members;
		sf::Thread * boar_main_thr = nullptr;
		sf::Thread * sendBoardThr = nullptr;

		bool saveShape(string & shapeInfo);
		void sendBoard();
public:
	
	Board(shared_ptr <Client> & _creator, shared_ptr <sf::TcpSocket> & _sock, sf::TcpListener * mem);

	void setBoard_ID(int b_ID) { board_ID = b_ID; }

	void addUser(shared_ptr <sf::TcpSocket> & _sock, shared_ptr <Client> & client);
	void broadcastPainting();
	string getCreaterName();

	~Board();
};

