#pragma once
#include <SFML/System.hpp>
#include <SFML/Network.hpp>
#include <iostream>
class Client {
private: sf::TcpSocket client_socket;
public:
	Client();
	Client(sf::TcpSocket * _client_socket);
	const sf::TcpSocket& getClietnSocket() { return std::ref(client_socket); }
	~Client();
};

