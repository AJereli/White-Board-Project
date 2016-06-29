#include "Server.h"
#pragma comment(lib, "sfml-network.lib")


Server::Server(){
	getUsersInfo(info_of_usres);
}
Server::Server(unsigned int p) {
	port = p;
	getUsersInfo(info_of_usres);
}
void Server::initListen() { // �������������� ��������� �����
	if (listener.listen(port) == sf::Socket::Done) {
		cout << "Start server" << endl;
		running = true;
	}
	else {
		////// ���������� �� �����������
		int ex;
		cout << "Some error with init server \n Press some key" << endl;
		cin >> ex;
		return;
		////// ���������� �� �����������
	}
	selector.add(listener);

}
bool Server::authorization(shared_ptr <sf::TcpSocket> & client_socket, shared_ptr <Client> & client) {// ����������� �� �������. True ���� �������, false �����. 
	
	char name_size[3];
	std::size_t received = 0;
	client_socket->receive(name_size, sizeof(name_size), received);
	string name(name_size);
	name.erase(3);
	
	std::cout << "The client said: " << name << std::endl;
	return true;
}

void Server::startListening() {

	initListen();
	while (running) {
		
		if (selector.wait(sf::Time(sf::seconds(0.3)))){ // �������� ������������ ������
			
			if (selector.isReady(listener)) {// ���� ��������� ����� ����� ���������
				
				shared_ptr <sf::TcpSocket> client_socket(new sf::TcpSocket); // ���������� �����
				shared_ptr <Client> client(new Client); // ���������� � ������� � ��� ���������.
			
				
				if (listener.accept(*client_socket) == sf::Socket::Done) { // ���� ������ ������� ������������� � ������
					cout << "some one try to connecnt" << endl;

					authorization(client_socket, client);
					users.push_back(make_pair(client_socket, client));
					selector.add(*client_socket);
				}
			}
			else {
				for (auto it = users.begin(); it != users.end(); ++it) {
					sf::TcpSocket & client = *it->first;
					if (selector.isReady(client) == sf::Socket::Done && it->second->getStatus() == USER_CONFIRMED) {
						//Send some Packet
					}
				}
			}
		}
		
	}
	
}
Server::~Server()
{
}
