#include "Board.h"





Board::Board(shared_ptr <Client> & _creator, shared_ptr <sf::TcpSocket> & _sock, sf::TcpListener * listener) {
	creator = (_creator);
	sock_creator = _sock;
	members.add(*listener);
	sock_of_members.push_back(sock_creator);
	members.add(*sock_creator);

	board_online = 1;
	boar_main_thr = new sf::Thread(&Board::workingOnBoard, this);
	boar_main_thr->launch();
	
}

void Board::workingOnBoard() {
	while (board_online) {
		sf::sleep(sf::microseconds(1000));
		if (members.wait(sf::seconds(0.3f))) {
			
			for (auto it = sock_of_members.begin(); it != sock_of_members.end(); ++it) {
				sf::TcpSocket & client = **it;
				if (members.isReady(client)) {
					
					char query_code[1];
					size_t rec;
					if (client.receive(query_code, sizeof(char), rec) == sf::Socket::Done) {
						
						if (query_code[0] == draw_board_code) { // Запрос на рисование
							
							char draw_packet[12];
							if (client.receive(draw_packet, sizeof(int)*3, rec) == sf::Socket::Done) {// Пересылка рисовальческой инфы
								
								for (auto mem_it = sock_of_members.begin(); mem_it != sock_of_members.end(); ++mem_it) {
									if (mem_it != it) {
										sf::TcpSocket & user = **mem_it;
										if (user.send(draw_packet, sizeof(int) * 3) == sf::Socket::Done){
											cout << "SENDED: " << draw_packet << endl;
										}
									}
								}
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
