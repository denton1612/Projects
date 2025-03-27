//
// Created by Știube Denis on 26.05.2024.
//

#ifndef LAB13_14_ERROR_H
#define LAB13_14_ERROR_H
#include <string>
using std::string;

class Error{
private:
    string mesaj;

public:
    Error(string m) : mesaj{m}{};
    const string& getMesaj() const;
};


#endif //LAB13_14_ERROR_H
