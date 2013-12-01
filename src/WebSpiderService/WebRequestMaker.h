#include <string>

#pragma once
class WebRequestMaker
{
public:
	WebRequestMaker();
	WebRequestMaker(std::string url);
	~WebRequestMaker();

	std::string getUrl();
	void setUrl(std::string url);
	std::string makeRequest();
	std::string makeRequest(std::string url);

private:
	std::string url;
};

