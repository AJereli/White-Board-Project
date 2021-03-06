#pragma once
#include <SFML/System.hpp>
#include <SFML/Network.hpp>
#include <iostream>
#include "Client.h"

#include <list>
#include <memory>
class Client;
class Board;

class Server{
private: 
		unsigned int port = 8000;
		bool running = 0;
		int BOARD_CNT = 0;
	
		sf::TcpListener listener;
		sf::SocketSelector selector; // ��������� ��� ��������� �������������� � ��������

		vector <pair <string, string> > info_of_usres;
		list <pair < shared_ptr <sf::TcpSocket>, shared_ptr <Client> > > users; // ������ �������������. ���� �� ������-������ � ������� - ������������.
		vector <Board*> all_boards;
		sf::Thread * connectingThread;

		bool authorization(shared_ptr <sf::TcpSocket> & client_socket, shared_ptr <Client> & client);
		void initListen();
		bool Server::registration(shared_ptr <sf::TcpSocket> & client_socket, shared_ptr <Client> & client);
public:
	Server();
	Server(unsigned int p);
	void startListening(); // ������ �������
	void stopListening() { running = 0; }
	~Server();
};

