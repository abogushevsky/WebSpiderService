#pragma once
class WebRequestMaker
{
public:
	WebRequestMaker();
	WebRequestMaker(char* url);
	~WebRequestMaker();

	char* getUrl();
	void setUrl(char* url);
	char* makeRequest();

private:
	char* url;
};

