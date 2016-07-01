#pragma comment(lib, "sfml-network.lib")
#include <SFML\Network.hpp>
#include <SFML\Graphics.hpp>
#include <SFML\Window.hpp>
#include "System_Fucn.h"
using namespace std;

const char draw_board_code = 7;

void workOnWin(sf::TcpSocket * arg) {
	sf::RenderWindow win(sf::VideoMode(150, 150), "QQ");
	sf::TcpSocket & client = *arg;
	while (1) {
		sf::Event event;
		while (win.pollEvent(event)) {
			
			
			if (event.type == sf::Event::MouseButtonPressed && event.key.code == sf::Mouse::Left) {
				//cout << sf::Mouse::getPosition(win).x << " " << sf::Mouse::getPosition(win).y << endl;
				
				
				char query_code[1];
				query_code[0] = draw_board_code;
				size_t rec;
				if (client.send(query_code, 1) == sf::Socket::Done) {
					client.send(to_string(sf::Mouse::getPosition(win).x).c_str(), sizeof(int));
				}
			}
				
		}
		win.clear(sf::Color::White);

		

		win.display();
	}
}

int main() {
	sf::TcpSocket socket;
	//sf::RenderWindow * win = new sf::RenderWindow(sf::VideoMode(200, 200), "QQ");
	
	
	int port = 8000;
	string name = "admin";
	string pass = "passw";
	
	std::size_t received = 0;

	socket.connect("127.0.0.1", port);

	sf::Thread th(&workOnWin,  &socket);

	char ns[1];
	ns[0] = to_string(name.length()).c_str()[0];
	
	socket.send(ns, 1);
	socket.send(name.c_str(), name.size() + 1);
	//system("pause");
	
	socket.send(to_string(pass.length()).c_str(), 1);
	socket.send(pass.c_str(), pass.size() + 1);

	char ans[1];
	sf::Int8 t;
	sf::Packet p;
	if (socket.receive(ans, sizeof(ans), received) == sf::Socket::Done) {
		if (static_cast <int> (ans[0]) == 0) {
			cout << "Welcome" << endl;
			int t;
			cin >> t;
			if (t == 5) {
				ans[0] = 5;
				if (socket.send(ans, 1) == sf::Socket::Done) {
					socket.receive(ans, 1, received);
					if (static_cast <int> (ans[0]) == 0) {
						cout << "BOARD CREATE" << endl;

						th.launch();
					}
				}
			}
			if (t == 6) {
				ans[0] = 6;
				if (socket.send(ans, 1) == sf::Socket::Done) {
					socket.send(to_string(pass.length()).c_str(), 1);
					socket.send(name.c_str(), name.size() + 1);
				}
			}
			
		}
		else {
			cout << "Wrong log or pass error code: " << (int)ans[0] << endl;
		}
	}
	
	
	//cout << (int)t;

	system("pause");
	
	th.terminate();
	
	
	return 0;
}