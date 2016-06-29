#include "Server.h"



Server::Server(){
		
}
Server::Server(unsigned int p) {
	port = p;
}

void Server::startListening() {
	listener.listen(port);
	running = true;
	
	while (running) {
		if (selector.wait()){
			if (selector.isReady(listener)) {
				const Client client;
				if (listener.accept(client.getClietnSocket()) == sf::Socket::Done) {
					clients.push_back(std::move(client));
					
					
				}
			}
		}
	}
}
Server::~Server()
{
}
