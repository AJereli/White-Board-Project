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
	size_t received_name = 0;
	size_t received_pass = 0;
	char * name_c = new char[256];
	char * pass = new char[256];
		
	client_socket->receive(name_c, 256, received_name);
	
	client_socket->receive(pass, 256, received_pass);
	char ans[1] = { already_online_code };
	for (auto it = users.begin(); it != users.end(); ++it) {
		if (it->second->getName() == string(name_c, received_name))
			if (it->second->getStatus() == USER_ONLINE) {
				client_socket->send(ans, 1);
				return false;
			}
				
	}

	for (auto it = info_of_usres.begin(); it != info_of_usres.end(); ++it) // Поиск юзера в БД
		if (it->first == string(name_c, received_name))
			if (it->second == string(pass, received_pass)) {
				client->setStatus(USER_ONLINE);
				client->setName(string(name_c, received_name));
				cout << "Welcom " << it->first << endl;
				return true;
			}

	if (name_c != nullptr)
		delete[] name_c;
	if (pass != nullptr)
		delete[] pass;
	return false;
}

bool Server::registration(shared_ptr <sf::TcpSocket> & client_socket, shared_ptr <Client> & client) {
	char * name = new char[128];
	char * passw = new char[128];
	char * email = new char[128];
	size_t rec_name = 0, rec_pass = 0, rec_email = 0;
	bool name_check = 1;
	ofstream users_write("info/users_data.txt", ios_base::app);

	client_socket->receive(name, 128, rec_name);
	for (auto it = info_of_usres.begin(); it != info_of_usres.end(); ++it)
		if (it->first == string(name, rec_name)) {
			name_check = 0;
			break;
		}
	client_socket->receive(passw, 128, rec_pass);
	client_socket->receive(email, 128, rec_email);

	if (name_check)
		users_write << string(name, rec_name) << " " << string(passw, rec_pass) << " " << string(email, rec_email) << endl;

	users_write.close();
	getUsersInfo(info_of_usres);
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

					else if ((int)answer[0] == (int)registration_code)  // Запрос регистрации
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
								users.erase(it);
								selector.remove(client);
								break;
							}
							if ((int)query_code[0] == connect_board_code) {//Подключение к доске
								
								size_t received;
								bool find = 0;
								char * name_c = new char[128];
								client.receive(name_c, 128, received);			
								for (int i = 0; i < BOARD_CNT; ++i) { //Если найдется такое имя, подключаемся к доске
									if (all_boards[i]->getCreaterName() == string(name_c, received)) {
										//connectingThread = new sf::Thread(&all_boards[i]->addUser, it->first);
										all_boards[i]->addUser(it->first, it->second);
										selector.remove(client);	
										users.erase(it);
										find = 1;
										break;
									}
								}
								
								if (!find) {
									answer[0] = board_not_found_code;
									client.send(answer, 1);
								}
								else
									break;
									
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
	if (connectingThread != nullptr) {
		connectingThread->terminate();
		delete connectingThread;
	}
	listener.close();
}
