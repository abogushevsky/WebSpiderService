#include <iostream>
#include <istream>
#include <ostream>
#include <fstream>
#include <vector>
#include <regex>

#include "Functions.h"
#include "stdafx.h"

using namespace std;

//<a> regex: (<a href="/)+.+(</a>)+
//href regex: href\s*=\s*[\"']?([^\"' >]+)[\"' >]

void PerformRequests()
{
	ifstream targetsStream("C:\\WebContent\\targets.txt");
	vector<string> *fileNames = new vector<string>();

	string temp;
	while (targetsStream >> temp) {
		fileNames->push_back(temp);
	}
	targetsStream.close();

	for (int i = 0; i < fileNames->size(); i++) {
		cout << fileNames->at(i) << endl;
		string url = fileNames->at(i);
		std::ofstream logFileStream;
		string filePath = "C:\\WebContent\\" + url + ".txt";
		logFileStream.open(filePath, std::ios::app);
		WebRequestMaker *requestMaker = new WebRequestMaker();
		requestMaker->makeRequest(url, logFileStream);
		delete requestMaker;
		logFileStream.close();

		ifstream requesResultStream(filePath);
		regex aPattern("(<a href=\"/)+.+(</a>)+");
		regex hrefPattern("href\s*=\s*[\"']?([^\"' >]+)[\"' >]");

		string line;
		ofstream linksFileStream;
		string linksFilePath = "C:\\WebContent\\" + url + "_links.txt";
		linksFileStream.open(linksFilePath, std::ios::app);
		
		int j = 0;
		while (getline(requesResultStream, line)) {
			smatch matches;
			if (regex_search(line, matches, aPattern)) {
				//cout << matches[0] << endl;
				string match = matches[0];
				linksFileStream << match << endl;
				smatch hrefMatches;
				if(regex_search(match, hrefMatches, hrefPattern)) {
					string hrefStr = hrefMatches[0];
					hrefStr = hrefStr.substr(6, hrefStr.size() - 7);
					
					filePath = "C:\\WebContent\\" + url + "_" + to_string(j) + ".txt";
					logFileStream.open(filePath, std::ios::app);
					WebRequestMaker *requestMaker = new WebRequestMaker();
					requestMaker->makeRequest(url, logFileStream, hrefStr);
					delete requestMaker;
					logFileStream.close();
				}
			}
			j++;
		}
		
		linksFileStream.close();
		requesResultStream.close();
	}
}