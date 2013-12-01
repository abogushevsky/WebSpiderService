#include "WebRequestMaker.h"

WebRequestMaker::WebRequestMaker()
{
}

WebRequestMaker::WebRequestMaker(char* url)
{
	this->url = url;
}

WebRequestMaker::~WebRequestMaker()
{
}

char* WebRequestMaker::getUrl()
{
	return this->url;
}

void WebRequestMaker::setUrl(char* url)
{
	this->url = url;
}

char* WebRequestMaker::makeRequest()
{
	return nullptr;
}
