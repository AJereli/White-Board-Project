#include "Board.h"





Board::Board(shared_ptr <Client> & _creator, shared_ptr <sf::TcpSocket> & _sock, sf::TcpListener * listener) {
	creator = (_creator);
	sock_creator = _sock;
	members.add(*listener);
	sock_of_members.push_back(sock_creator);
	members.add(*sock_creator);
	
	board_online = 1;
	boar_main_thr = new sf::Thread(&Board::broadcastPainting, this);
	boar_main_thr->launch();
	
}

void Board::broadcastPainting() {
	while (board_online) {
		sf::sleep(sf::microseconds(1000));
		if (members.wait(sf::seconds(0.3f))) {
			
			for (auto it = sock_of_members.begin(); it != sock_of_members.end(); ++it) {
				sf::TcpSocket & client = **it;
				if (members.isReady(client)) {
					
					char query[512];
					
					size_t rec = 0;
					if (client.receive(query, sizeof(query), rec) == sf::Socket::Done) {
						//cout << string(query, rec) << endl;
						for (auto say_all = sock_of_members.begin(); say_all != sock_of_members.end(); ++say_all) {
							sf::sleep(sf::microseconds(25));
							if (say_all != it)
								(**say_all).send(query, rec);
						}
					}
				}
			}
		}	

	}
}

void Board::addUser(shared_ptr <sf::TcpSocket> & _sock) {
	members.add(*_sock);
	cout << "Connect" << endl;
	sock_of_members.push_back(_sock);
	
	cout << sock_of_members.size() << endl;
}

string Board::getCreaterName() {
	if (creator != nullptr)
		return creator->getName();
}

Board::~Board(){
	cout << "del" << endl;
	board_online = 0;
	if (boar_main_thr != nullptr) {
		boar_main_thr->terminate();
		delete boar_main_thr;
	}
}
