#pragma once
#include <SFML/System.hpp>
#include <SFML/Network.hpp>
#include <iostream>
#include "Client.h"
#include <list>

class Server{
private: 
		unsigned int port = 8000;
		bool running = 0;
		sf::TcpListener listener;
		sf::SocketSelector selector; // Контейнер для массового взаимодействия с сокетами
		
		std::list<Client > clients; 
public:
	Server();
	Server(unsigned int p);
	void startListening(); // Запуск сервера
	void stopListening() { running = 0; }
	~Server();
};

