#include <boost/asio.hpp>
#include "WebRequestMaker.h"

using namespace std;
using boost::asio::ip::tcp;

WebRequestMaker::WebRequestMaker()
{
}

WebRequestMaker::WebRequestMaker(string url)
{
	this->url = url;
}

WebRequestMaker::~WebRequestMaker()
{
}

string WebRequestMaker::getUrl()
{
	return this->url;
}

void WebRequestMaker::setUrl(string url)
{
	this->url = url;
}

int WebRequestMaker::makeRequest()
{
	return 0;
}

int WebRequestMaker::makeRequest(string url, ostream &cout, string path)
{
	try
	{
		//char* path = "/";
		
		boost::asio::io_service io_service;

		cout << "Begin requset" << endl;

		// Get a list of endpoints corresponding to the server name.
		tcp::resolver resolver(io_service);
		tcp::resolver::query query(url, "http");
		tcp::resolver::iterator endpoint_iterator = resolver.resolve(query);
		tcp::resolver::iterator end;

		// Try each endpoint until we successfully establish a connection.
		tcp::socket socket(io_service);
		boost::system::error_code error = boost::asio::error::host_not_found;
		while (error && endpoint_iterator != end)
		{
			socket.close();
			socket.connect(*endpoint_iterator++, error);
		}
		if (error)
			throw boost::system::system_error(error);

		// Form the request. We specify the "Connection: close" header so that the
		// server will close the socket after transmitting the response. This will
		// allow us to treat all data up until the EOF as the content.
		boost::asio::streambuf request;
		ostream request_stream(&request);
		request_stream << "GET " << path << " HTTP/1.1\r\n";
		request_stream << "Host: " << url << "\r\n";
		request_stream << "Accept: */*\r\n";
		request_stream << "Connection: close\r\n\r\n";

		// Send the request.
		boost::asio::write(socket, request);

		// Read the response status line.
		boost::asio::streambuf response;
		boost::asio::read_until(socket, response, "\r\n");

		// Check that response is OK.
		istream response_stream(&response);
		string http_version;
		response_stream >> http_version;
		unsigned int status_code;
		response_stream >> status_code;
		string status_message;
		getline(response_stream, status_message);
		if (!response_stream || http_version.substr(0, 5) != "HTTP/")
		{
			cout << "Invalid response\n";
			return 1;
		}
		if (status_code != 200)
		{
			cout << "Response returned with status code " << status_code << "\n";
			return 1;
		}

		// Read the response headers, which are terminated by a blank line.
		boost::asio::read_until(socket, response, "\r\n\r\n");

		// Process the response headers.
		string header;
		while (getline(response_stream, header) && header != "\r")
			cout << header << "\n";
		cout << "\n";

		// Write whatever content we already have to output.
		if (response.size() > 0)
			cout << &response;

		// Read until EOF, writing data to output as we go.
		while (boost::asio::read(socket, response,
			boost::asio::transfer_at_least(1), error))
			cout << &response;
		if (error != boost::asio::error::eof)
			throw boost::system::system_error(error);
	}
	catch (exception& e)
	{
		cout << "Exception: " << e.what() << "\n";
	}

	return 0;
}	

