#include "Board.h"



Board::Board()
{
	
}

Board::Board(shared_ptr <Client> & _creator, shared_ptr <sf::TcpSocket> & _sock) {
	cout << "Before: " <<_creator.use_count() << endl;
	creator = (_creator);
	sock_creator = _sock;
	members.add(*sock_creator);
	cout << "After: " << _creator.use_count() << endl;
}

Board::~Board()
{
}
