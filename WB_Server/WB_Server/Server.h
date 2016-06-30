#pragma once
#include <SFML/System.hpp>
#include <SFML/Network.hpp>
#include <iostream>
#include "Client.h"
#include <list>
#include <memory>
class Client;
class Server{
private: 
		unsigned int port = 8000;
		bool running = 0;

		sf::TcpListener listener;
		sf::SocketSelector selector; // Контейнер для массового взаимодействия с сокетами

		vector <pair <string, string> > info_of_usres;
		list <pair < shared_ptr <sf::TcpSocket>, shared_ptr <Client> > > users; // Список пользователей. Пара из клиент-сокета и клиента - пользователь.

		bool authorization(shared_ptr <sf::TcpSocket> & client_socket, shared_ptr <Client> & client);
		void initListen();
public:
	Server();
	Server(unsigned int p);
	void startListening(); // Запуск сервера
	void stopListening() { running = 0; }
	~Server();
};

