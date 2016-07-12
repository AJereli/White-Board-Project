#include "Board.h"





Board::Board(shared_ptr <Client> & _creator, shared_ptr <sf::TcpSocket> & _sock, sf::TcpListener * listener) {
	creator = (_creator);
	sock_creator = _sock;
	members.add(*listener);
	//sock_of_members.push_back(sock_creator);
	members.add(*sock_creator);
	users.push_back(make_pair(sock_creator, creator));
	board_online = 1;
	boar_main_thr = new sf::Thread(&Board::broadcastPainting, this);
	boar_main_thr->launch();

}
bool Board::saveShape(string & shapeInfo) {
	bool sayThis = true;
	int cntOfPlus = 0;
	for (int i = 0; i < shapeInfo.size(); ++i)
		if (shapeInfo[i] == '+')
			cntOfPlus++;
	if (cntOfPlus == 3)
		if (shapeInfo.find("-") != string::npos) {
			string identityMatr = "1!0!0!1!0!0";
			all_shapes.push_back(make_pair(make_pair(shapeInfo, identityMatr), vector<string>()));
		}
		else
			sayThis = false;
	else if (cntOfPlus == 2) {
		int lastPlus = shapeInfo.find_last_of('+');
		if (shapeInfo[0] == '0' && shapeInfo.find("!") != string::npos) {
			int id = stoi(shapeInfo.substr(2, lastPlus - 1));
			all_shapes.at(id).first.second = shapeInfo.substr(shapeInfo.find_last_of('+') + 1, shapeInfo.length());
		}
		else {
			int id = stoi(shapeInfo.substr(shapeInfo.find_last_of('+') + 1, shapeInfo.length()));
			try {
				all_shapes.at(id).second.push_back(shapeInfo);
			}
			catch (out_of_range) {
				all_shapes.erase(all_shapes.end()--);
				return false;
			}
		}

	}


	return sayThis;
	cout << "Shapes on board " << creator->getName() + " : " << all_shapes.size() << endl;

}
void Board::broadcastPainting() {
	while (board_online) {
		sf::sleep(sf::microseconds(10));
		if (members.wait(sf::seconds(1.3f))) {

			for (auto it = users.begin(); it != users.end(); ++it) {
				sf::TcpSocket & client = (*it->first);
				if (members.isReady(client)) {

					char query[512];

					size_t rec = 0;
					if (client.receive(query, sizeof(query), rec) == sf::Socket::Done) {
						if (string(query, rec) == "goodbye") {
							members.remove(client);
							cout << it->second->getName() << " disconnect\n";
							users.erase(it);
							break;
						}

						// ���������� ������
						bool sayThis = saveShape(string(query, rec));
						if (sayThis)
							for (auto say_all = users.begin(); say_all != users.end(); ++say_all) {

								if (say_all != it)
									(*say_all->first).send(query, rec);
							}
					}
				}
			}
		}

	}
}
void Board::sendBoard() {

}
void Board::addUser(shared_ptr <sf::TcpSocket> & _sock, shared_ptr <Client> & client) {
	char query_code[64];
	size_t rec = 0;
	query_code[0] = server_ok_code;
	_sock->send(query_code, sizeof(char));
	sf::sleep(sf::microseconds(10000));
	members.add(*_sock);
	cout << "Connect" << endl;
	users.push_back(make_pair(_sock, client));

	char * end = "END";
	if (all_shapes.size() == 0) {
		_sock->send(end, sizeof(end));
		return;
	}

	string shapeSizeStr = to_string(all_shapes.size());

	_sock->send(shapeSizeStr.c_str(), shapeSizeStr.length());


	cout << "Send size: " << shapeSizeStr << endl;

	for (int i = 0; i < all_shapes.size(); ++i) {
		string info = (
			all_shapes[i].first.first // type, color, thickness, id
			+ "+"
			+ to_string(all_shapes[i].second.size())  // Numb of part
			+ "+"
			+ all_shapes[i].first.second); // Matrix

		_sock->receive(query_code, 64, rec);
		_sock->send(info.c_str(), info.length());

		string oneBigString;
		try {
			oneBigString.append(all_shapes[i].second.at(0));
		}
		catch (out_of_range) {
			continue;
		}
		for (auto it = 1; it < all_shapes[i].second.size(); ++it)
			oneBigString.append("-" + all_shapes[i].second[it]);


		_sock->send(oneBigString.c_str(), oneBigString.length());
	}

	//users.push_back(make_pair(_sock, client));
	cout << "User connected, total users: " << users.size() << endl;
}

string Board::getCreaterName() {
	if (creator != nullptr)
		return creator->getName();
}

Board::~Board() {
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
