#include "stdafx.h"
#include "Helper.h" 
#include "_02_TypesTest.h"

void _02_TypesTest::testSize()
{
	debugOutput("_02_TypesTest::testSize()");

	short sh = 5;
	int i = 8;
	long l = 56;
	long long ll = 589;
	
	std::string s = "short: " + std::to_string(sizeof sh);
	debugOutput(s);

	s = "int: " + std::to_string(sizeof i);
	debugOutput(s);

	s = "long: " + std::to_string(sizeof l);
	debugOutput(s);

	s = "long long: " + std::to_string(sizeof ll);
	debugOutput(s);
	/*
	short: 2
	int: 4
	long: 4
	long long: 8
	*/
}

