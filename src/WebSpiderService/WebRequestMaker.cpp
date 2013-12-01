#include "WebRequestMaker.h"

WebRequestMaker::WebRequestMaker()
{
}

WebRequestMaker::WebRequestMaker(std::string url)
{
	this->url = url;
}

WebRequestMaker::~WebRequestMaker()
{
}

std::string WebRequestMaker::getUrl()
{
	return this->url;
}

void WebRequestMaker::setUrl(std::string url)
{
	this->url = url;
}

std::string WebRequestMaker::makeRequest()
{
	return nullptr;
}

std::string WebRequestMaker::makeRequest(std::string url)
{
	return nullptr;
}
