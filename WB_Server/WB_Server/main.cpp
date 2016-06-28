#include <iostream>
#include <SFML/System.hpp>
#include <SFML/Network.hpp>
#include "Client.h"
#include <list>
#include <vector>

using namespace std;
using Server = sf::TcpListener;

const unsigned int port = 8000;



void startListen(Server & server, list <Client> & client_list) {// Запускает слушающий порт и цикл для принятие новых соединений. Принимает listner-socket и список клиентов
	server.listen(port); // Слушать порт 8000

	while (1) {
		Client next_client;
		if (server.accept(*next_client.getClietnSocket()) == sf::Socket::Done) {
			client_list.push_back(next_client);
		}
	}
}

int main() {
	Server server;
	
	list <Client> client_list;
	

	


	return 0;
}