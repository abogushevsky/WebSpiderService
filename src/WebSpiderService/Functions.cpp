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
		logFileStream.close();

		ifstream requesResultStream(filePath);
		regex aPattern{ "(<a href=\"/)+.+(</a>)+" };

		string line;
		ofstream linksFileStream;
		string linksFilePath = "C:\\WebContent\\" + url + "_links.txt";
		linksFileStream.open(linksFilePath, std::ios::app);

		while (getline(requesResultStream, line)) {
			smatch matches;
			if (regex_search(line, matches, aPattern)) {
				//cout << matches[0] << endl;
				linksFileStream << matches[0] << endl;

				if (matches.size() > 1 && matches[1].matched) {
					//cout << "\t" << matches[1] << endl;
				}
			}
		}

		linksFileStream.close();
		requesResultStream.close();
	}
}