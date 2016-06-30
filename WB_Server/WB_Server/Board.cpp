#include "Board.h"





Board::Board(shared_ptr <Client> & _creator, shared_ptr <sf::TcpSocket> & _sock, sf::TcpListener * listener) {
	cout << "Before: " << _creator.use_count() << endl;
	creator = (_creator);
	sock_creator = _sock;
	members.add(*listener);
	sock_of_members.push_back(sock_creator);
	members.add(*sock_creator);

	board_online = 1;
	boar_main_thr = new sf::Thread(&Board::workingOnBoard, this);
	boar_main_thr->launch();
	cout << "After: " << _creator.use_count() << endl;
}

void Board::workingOnBoard() {
	while (board_online) {
		sf::sleep(sf::microseconds(100));
		if (members.wait()) {
			
			for (auto it = sock_of_members.begin(); it != sock_of_members.end(); ++it) {
				sf::TcpSocket & client = **it;
				if (members.isReady(client)) {
					
					char query_code[1];
					size_t rec;
					if (client.receive(query_code, sizeof(char), rec) == sf::Socket::Done) {
						
						if (query_code[0] == draw_board_code) {
							
							char coord[4];
							if (client.receive(coord, sizeof(int), rec) == sf::Socket::Done) {
								cout << coord << endl;
							}
						}
					}
				}
			}
		}	

	}
}

void Board::addUser(shared_ptr <sf::TcpSocket> & _sock) {
	members.add(*_sock);
	sock_of_members.push_back(_sock);
	char query_code[1];
	query_code[0] = server_ok_code;
	_sock->send(query_code, sizeof(char));
}

Board::~Board(){
	cout << "del" << endl;
	board_online = 0;
	if (boar_main_thr != nullptr) {
		boar_main_thr->terminate();
		delete boar_main_thr;
	}
}
