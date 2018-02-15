#include "stdafx.h"
#include "Helper.h" 
#include "_06_FunctionPointersTest.h"

double testFunction(double d1, int i1)
{
	std::string s = "double: " + std::to_string(d1) + " int: " + std::to_string(i1);
	debugOutput(s);
	return 99;
}

double testFunction(double d1, int i1, double (*pf)(double, int))
{
	return (*pf)(d1, i1);
}

void _06_FunctionPointersTest::testPointer()
{
	debugOutput("_06_FunctionPointersTest::testPointer()");

	double(*pf) (double, int);

	pf = testFunction;

	double res = pf(2, 77);//double: 2.000000 int: 77
	std::string s = "res: " + std::to_string(res);
	debugOutput(s);//res: 99.000000

	res = (*pf)(55, 23);//double: 55.000000 int: 23
	s = "res: " + std::to_string(res);
	debugOutput(s);//res: 99.000000
}

void _06_FunctionPointersTest::testTransferPointer()
{
	debugOutput("_06_FunctionPointersTest::testTransferPointer()");

	double(*pCb) (double, int);
	pCb = testFunction;
	
	double(*pf) (double, int, double(*pf)(double, int));
	pf = testFunction;

	double res = pf(789, 123, pCb);//double: 789.000000 int: 123
	std::string s = "res: " + std::to_string(res);
	debugOutput(s);//res: 99.000000
}