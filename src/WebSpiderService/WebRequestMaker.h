#include <string>
#include <iostream>

#pragma once

#define TIMEOUT 30000

class WebRequestMaker
{
public:
	WebRequestMaker();
	WebRequestMaker(std::string url);
	~WebRequestMaker();

	std::string getUrl();
	void setUrl(std::string url);
	int makeRequest();
	int makeRequest(std::string url, std::ostream &cout, std::string path = "/");

private:
	std::string url;
};

