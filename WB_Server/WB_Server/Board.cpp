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
					int cntOfPlus = 0;
					size_t rec = 0;
					if (client.receive(query, sizeof(query), rec) == sf::Socket::Done) {
						//cout << string(query, rec) << endl;
						string str(query, rec);
						for (int i = 0; i < str.size(); ++i)
							if (str[i] == '+')
								cntOfPlus++;
						if (cntOfPlus == 3)
							all_shapes.push_back(make_pair(str, vector<string>()));
						else if (cntOfPlus == 2) {
							int index = stoi(str.substr(str.find_last_of('+') + 1, str.length() - str.find_last_of('+')));
							all_shapes.at(index).second.push_back(str);
						}
						
						for (auto say_all = sock_of_members.begin(); say_all != sock_of_members.end(); ++say_all) {		
							sf::sleep(sf::microseconds(50));
							if (say_all != it)
								(**say_all).send(query, rec);
						}
					}
				}
			}
		}	

	}
}
void Board::sendBoard() {
	
}
void Board::addUser(shared_ptr <sf::TcpSocket> & _sock) {
	char query_code[1];
	query_code[0] = server_ok_code;
	_sock->send(query_code, sizeof(char));
	sf::sleep(sf::microseconds(100));
	members.add(*_sock);
	cout << "Connect" << endl;
	sock_of_members.push_back(_sock);

	
	string shapeSizeStr = to_string(all_shapes.size());
	_sock->send(shapeSizeStr.c_str(), shapeSizeStr.length());
	//cout << shapeSizeStr << endl;
	sf::sleep(sf::microseconds(100));
	for (int i = 0; i < all_shapes.size(); ++i) {
		string info = (all_shapes[i].first + "+" + to_string(all_shapes[i].second.size()));
		_sock->send(info.c_str(), info.length());
		//cout << info + " - ";
		sf::sleep(sf::microseconds(100));
		for (auto it = all_shapes[i].second.begin(); it != all_shapes[i].second.end(); ++it) {
			sf::sleep(sf::microseconds(100));
			_sock->send(it->c_str(), it->length());
		//	cout << *it + " ";
		}
		//cout << endl;
	}
	_sock->send("END", sizeof("END"));
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
	if (sendBoardThr != nullptr) {
		sendBoardThr->terminate();
		delete sendBoardThr;
	}
}
