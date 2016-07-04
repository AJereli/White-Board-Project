#pragma comment(lib, "sfml-network.lib")
#include <SFML\Network.hpp>
#include <SFML\Graphics.hpp>
#include <SFML\Window.hpp>
#include "System_Fucn.h"
using namespace std;

const char draw_board_code = 7;
const char server_ok_code = 0;

sf::VertexArray draw_arr;

string makeDrawPacket(sf::Vector2i & coords, int mode = 2) {
	string draw_packet_str = to_string(coords.x) + '+' + to_string(coords.y) + '+' + to_string(mode);	
	return draw_packet_str;
}
pair <sf::Vector2i, int> parsePacket(char * draw_packet) {
	int mode_max_len = 5; // Ќасколько длинным может быть значением mode (parsed.second)
	pair <sf::Vector2i, int> parsed;
	string dp_str = draw_packet;
	int f_p = dp_str.find_first_of('+'); // f_p - first plus, находит место первого вхождени€ знака '+'
	int e_p = dp_str.find_last_of('+');// e_p - end plus, находит место последнего вхождени€ знака '+'
	parsed.first.x = atoi(dp_str.substr(0, f_p).c_str());
	parsed.first.y = atoi(dp_str.substr(f_p + 1, e_p - f_p - 1).c_str());
	parsed.second = atoi(dp_str.substr(e_p + 1, mode_max_len).c_str());

	cout << "parsed: " << parsed.first.x << " " << parsed.first.y << " " << parsed.second << endl;

	return parsed;
}

void drawPoint( pair <sf::Vector2i, int> & pnt) {
	sf::Vertex vertex;
	vertex.color = sf::Color::Black;
	for (int i = pnt.first.x; i < pnt.first.x + pnt.second; i++) {
		for (int j = pnt.first.y; j < pnt.first.y + pnt.second; j++) {
			vertex.position = sf::Vector2f(i, j);
			draw_arr.append(vertex);
		}
	}
	cout << draw_arr.getVertexCount() << endl;
}

void workOnWin(sf::TcpSocket * arg) {
	sf::RenderWindow win(sf::VideoMode(500, 150), "QQ");
	sf::TcpSocket & client = *arg;
	sf::SocketSelector sel;
	sel.add(client);
	size_t rec;
	char draw_packet[12];
	bool other_chenge = 0;
	bool you_chenge = 0;
	while (1) {
		sf::sleep(sf::microseconds(100));
		sf::Event event;
		if (sel.wait(sf::seconds(0.003f))) {
			if (sel.isReady(client)) {
				if (client.receive(draw_packet, sizeof(int) * 3, rec) == sf::Socket::Done) {
					drawPoint(parsePacket(draw_packet));
				}
			}
		}
		while (win.pollEvent(event)) {
			
			
			if (event.type == sf::Event::MouseButtonPressed && event.key.code == sf::Mouse::Left) {
				//cout << sf::Mouse::getPosition(win).x << " " << sf::Mouse::getPosition(win).y << endl;
				
				
				char query_code[1];
				query_code[0] = draw_board_code;
				size_t rec;
				
				drawPoint(make_pair(sf::Mouse::getPosition(win), 2));
				cout << makeDrawPacket(sf::Mouse::getPosition(win)) << endl;
				if (client.send(query_code, 1) == sf::Socket::Done) {
					client.send(makeDrawPacket(sf::Mouse::getPosition(win), 2).c_str(), sizeof(int)*3);
				}
			}
				
		}
		win.clear(sf::Color::White);

		
		win.draw(draw_arr);
		
		

		win.display();
	}
}

int main() {
	sf::TcpSocket socket;
	//sf::RenderWindow * win = new sf::RenderWindow(sf::VideoMode(200, 200), "QQ");
	
	draw_arr.setPrimitiveType(sf::LinesStrip);

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
					socket.receive(ans, 1, received);
					if (ans[0] == server_ok_code) {
						th.launch();
					}
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