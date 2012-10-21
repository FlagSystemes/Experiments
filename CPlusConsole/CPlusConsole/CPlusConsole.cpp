
#include <sstream>
#include "string.h"
#include "windows.h"
#include <iostream>
#include <fstream>
#include <algorithm>
#include <string>
#include <wctype.h>
#include "stdafx.h"
#include <set>


std::string StringToUpper(std::string strToConvert)
{
	//change each element of the string to upper case
	for(unsigned int i=0;i<strToConvert.length();i++)
	{
		strToConvert[i] = toupper(strToConvert[i]);
	}
	return strToConvert;//return the converted string
}

bool hasEnding (std::string const &fullString, std::string const &ending)
{
    if (fullString.length() >= ending.length()) 
	{
		std::string upperFullString = StringToUpper(fullString);
		std::string upperEnding = StringToUpper(ending);
        return (0 == upperFullString.compare (upperFullString.length() - upperEnding.length(), upperEnding.length(), upperEnding));
    }
	else 
	{
        return false;
    }
}

int _tmain(int argc, _TCHAR* argv[])
{
	
		std::string userName = getenv("USERNAME");
		std::string concatUserName = "\\" + userName;
	 std::string x ="SimonCropp";
	 std::string y ="cropp";

	 bool z = hasEnding(x, concatUserName);
	return 0;
}

