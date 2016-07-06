#include "Server.h"
#include "Board.h"
#pragma comment(lib, "sfml-network.lib")

class Board;
Server::Server() {
	getUsersInfo(info_of_usres);
	all_boards.resize(100);

}
Server::Server(unsigned int p) {
	port = p;
	getUsersInfo(info_of_usres);
}
void Server::initListen() { // Инициализирует слушающий сокет
	if (listener.listen(port) == sf::Socket::Done) {
		cout << "Start server" << endl;
		running = true;
	}
	else {
		////// Переписать на исключениях
		int ex;
		cout << "Some error with init server \n Press some key" << endl;
		cin >> ex;
		return;
		////// Переписать на исключениях
	}
	selector.add(listener);
}
bool Server::authorization(shared_ptr <sf::TcpSocket> & client_socket, shared_ptr <Client> & client) {// Авторизация на сервере. True если успешно, false иначе. 	
	char name_size[1];
	char pass_size[1];
	size_t received = 0;
	char * name_c = nullptr;
	char * pas = nullptr;
	int iname_size = 0;
	int ipass_size = 0;


	if (client_socket->receive(name_size, sizeof(name_size), received) == sf::Socket::Done) {
		iname_size = atoi(name_size);
		name_c = new char[iname_size];
		client_socket->receive(name_c, iname_size, received);
	}
	if (client_socket->receive(pass_size, sizeof(pass_size), received) == sf::Socket::Done) {
		ipass_size = atoi(pass_size) + 1;
		pas = new char[ipass_size];
		client_socket->receive(pas, ipass_size, received);
	}

	for (auto it = info_of_usres.begin(); it != info_of_usres.end(); ++it) // Поиск юзера в БД
		if (it->first == string(name_c, iname_size))
			if (it->second == string(pas, iname_size)) {
				client->setStatus(USER_ONLINE);
				client->setName(name_c);
				cout << "Welcom " << it->first << endl;
				return true;
			}

	if (name_c != nullptr)
		delete[] name_c;
	if (pas != nullptr)
		delete[] pas;
	return false;
}

bool Server::registration(shared_ptr <sf::TcpSocket> & client_socket, shared_ptr <Client> & client) {
	char info_size[1];
	int i_n, i_p, i_e = 0; // Кол-во символов в name, passw, email
	char * name = nullptr;
	char * passw = nullptr;
	char * email = nullptr;
	size_t received = 0;
	bool name_check = 1;
	ofstream users_write("info/users_data.txt", ios_base::app);

	if (client_socket->receive(info_size, 1, received) == sf::Socket::Done)
		i_n = atoi(info_size);

	name = new char[i_n];
	client_socket->receive(name, i_n, received);
	for (auto it = info_of_usres.begin(); it != info_of_usres.end(); ++it) 
		if (it->first == string(name, i_n)) {
			name_check = 0;
			break;
		}
	
	if (client_socket->receive(info_size, 1, received) == sf::Socket::Done)
		i_p = atoi(info_size);
	passw = new char[i_p];
	client_socket->receive(passw, i_p, received);
	
	if (client_socket->receive(info_size, 1, received) == sf::Socket::Done)
		i_e = atoi(info_size);
	email = new char[i_e];
	client_socket->receive(email, i_e, received);

	if (name_check)
		users_write  << string(name, i_n) << " " << string(passw, i_p) << " " << string(email, i_e) << endl;

	users_write.close();
	name != nullptr ? delete[] name : 1;
	passw != nullptr ? delete[] passw : 1;
	email != nullptr ? delete[] email : 1;

	if (name_check)
		return true;
	else
		return false;
}

void Server::startListening() {
	char answer[1];
	size_t rec = 0;

	initListen();

	while (running) {
		sf::sleep(sf::microseconds(1000));
		if (selector.wait(sf::seconds(0.3f))) { // Ожидание принимающего сокета

			if (selector.isReady(listener)) {// Если слушающий сокет готов принимать

				shared_ptr <sf::TcpSocket> client_socket(new sf::TcpSocket); // Клиентский сокет
				shared_ptr <Client> client(new Client); // Информация о клиенте и его действиях.


				if (listener.accept(*client_socket) == sf::Socket::Done) { // Если клиент успешно подсоеденился к сокету
					
					client_socket->receive(answer, 1, rec);
					cout << "some one try to connecnt with code: " << answer[0];
					if (answer[0] == authorize_code) // Запрос авторизации
						if (authorization(client_socket, client)) {
							cout << "authorization\n";
							users.push_back(make_pair(client_socket, client));
							selector.add(*client_socket);
							answer[0] = server_ok_code;
							client_socket->send(answer, 1);
						}
						else {
							answer[0] = wrong_pass_code;
							client_socket->send(answer, 1);
						}
					
					else if ((int)answer[0] ==(int) registration_code)  // Запрос регистрации
						if (registration(client_socket, client)) {
							answer[0] = server_ok_code;
							client_socket->send(answer, 1);
						}
						else {
							answer[0] = wrong_name_code;
							client_socket->send(answer, 1);
						}
					
				}
			}
			else {
				for (auto it = users.begin(); it != users.end(); ++it) {
					sf::TcpSocket & client = *it->first;
					if (selector.isReady(client) && it->second->getStatus() == USER_ONLINE) {
						char query_code[1];

						if (client.receive(query_code, 1, rec) == sf::Socket::Done) {

							cout << "New query, code: " << (int)query_code[0] << endl;

							if ((int)query_code[0] == new_board_code && BOARD_CNT < 100) {
								answer[0] = server_ok_code;
								client.send(answer, 1);

								all_boards[BOARD_CNT] = new Board(it->second, it->first, &listener);
								all_boards[BOARD_CNT]->setBoard_ID(BOARD_CNT);
								BOARD_CNT++;

								selector.remove(client);
							}
							if ((int)query_code[0] == connect_board_code) {//Подключение к доске
								char name_size[1];
								size_t received;
								if (client.receive(name_size, sizeof(name_size), received) == sf::Socket::Done) {// Узнаем имя основателя доски
									int iname_size = atoi(name_size) + 1;
									char * name_c = new char[iname_size];
									client.receive(name_c, iname_size, received);
									for (int i = 0; i < BOARD_CNT; ++i) { //Если найдется такое имя, подключаемся к доске
										if (all_boards[i]->getCreaterName() == name_c) {
											selector.remove(client);
											all_boards[i]->addUser(it->first);
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
Server::~Server()
{
	listener.close();
}
