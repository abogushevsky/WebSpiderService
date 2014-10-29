//
//  main.cpp
//  BoostTest
//
//  Created by Anton Bogushevsky on 26.10.14.
//  Copyright (c) 2014 Anton Bogushevsky. All rights reserved.
//

#include <iostream>
#include <boost/regex.hpp>
#include <string>
#include <boost/asio.hpp>
//#include <boost/regex>
//#include <boost/asio>

using namespace boost;
using namespace boost::asio;
using namespace std;

int main(int argc, const char * argv[]) {
    std::string line;
    //std::cout << "Test" << std::endl;
    //boost::regex pat( "^Subject: (Re: |Aw: )*(.*)" );
    
    io_service service;
    ip::tcp::resolver resolver(service);
    ip::tcp::resolver::query query("www.yahoo.com", "80");
    ip::tcp::resolver::iterator iter = resolver.resolve( query);
    ip::tcp::resolver::iterator end;
    
    while(iter != end) {
        ip::tcp::endpoint ep = *iter++;
        std::cout << ep.address().to_string() << std::endl;
    }
    
    /*while (std::cin)
    {
        std::getline(std::cin, line);
        boost::smatch matches;
        if (boost::regex_match(line, matches, pat))
            std::cout << matches[2] << std::endl;
    }*/
}
