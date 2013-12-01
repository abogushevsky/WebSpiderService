#include <iostream>
#include <istream>
#include <ostream>
#include <fstream>
#include <vector>

#include "Functions.h"
#include "stdafx.h"

using namespace std;

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
		logFileStream.open("C:\\WebContent\\" + url + ".txt", std::ios::app);
		WebRequestMaker *requestMaker = new WebRequestMaker();
		requestMaker->makeRequest("url", logFileStream);
		logFileStream.close();
	}
}