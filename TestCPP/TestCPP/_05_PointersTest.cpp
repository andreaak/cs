#include "stdafx.h"
#include "Helper.h" 
#include "_05_PointersTest.h"

void _05_PointersTest::testPointer()
{
	debugOutput("_05_PointersTest::testPointer()");

	short sh = 5;
	short *psh = &sh;
	
	std::string s = "short: " + std::to_string(sh);
	debugOutput(s);//short: 2

	s = "*psh: " + std::to_string(*psh);
	debugOutput(s);//*psh: 5

	sh++;

	s = "*psh: " + std::to_string(*psh);
	debugOutput(s);//*psh: 6

	*psh = 98;

	s = "*psh: " + std::to_string(*psh);
	debugOutput(s);//*psh: 98
}

void _05_PointersTest::testVoidPointer()
{
	debugOutput("_05_PointersTest::testVoidPointer()");
	short sh = 5;
	void *pv = &sh;

	std::string s = "short: " + std::to_string(sh);
	debugOutput(s);//short: 2

	short *psh = (short *)pv;
	
	s = "*psh: " + std::to_string(*psh);
	debugOutput(s);//*psh : 5

	int *pi = (int *)pv;

	s = "*pi: " + std::to_string(*pi);
	debugOutput(s);//*pi : -859045883
}

void _05_PointersTest::testChangeConst()
{
	debugOutput("_05_PointersTest::testChangeConst()");
	const short sh = 5;
	const short *psh = &sh;

	std::string s = "short: " + std::to_string(sh);
	debugOutput(s);//short: 5

	s = "*psh: " + std::to_string(*psh);
	debugOutput(s);//*psh: 5

	//*psh = 9;//error

	short x = *psh;
	s = "x: " + std::to_string(x);
	debugOutput(s);//x: 5

	short *pmsh = (short *)psh;
	*pmsh = 99;

	s = "psh: " + std::to_string(*psh);
	debugOutput(s);//psh: 99

	s = "pmsh: " + std::to_string(*pmsh);
	debugOutput(s);//pmsh: 99

	volatile short tmp = sh;
	s = "short: " + std::to_string(tmp);
	debugOutput(s);//short: 5
}

void _05_PointersTest::testIncrementPointer()
{
	debugOutput("_05_PointersTest::testIncrementPointer()");
	short sh1[] = {5 , 26, 55, 99};
	short *psh = sh1;

	auto s = "*psh: " + std::to_string(*psh);
	debugOutput(s);//*psh: 5

	psh++;

	s = "*psh: " + std::to_string(*psh);
	debugOutput(s);//*psh: 26

	psh += 2;

	s = "*psh: " + std::to_string(*psh);
	debugOutput(s);//*psh: 99
}

void _05_PointersTest::testDiffPointer()
{
	debugOutput("_05_PointersTest::testDiffPointer()");
	short sh1[] = { 5 , 26 };
	short *psh = sh1;
	short *psh2 = psh + 1;

	auto s = "*psh: " + std::to_string(*psh);
	debugOutput(s);//*psh: 5

	s = "*psh2: " + std::to_string(*psh2);
	debugOutput(s);//*psh2: 26

	int diff = psh2 - psh;

	s = "diff: " + std::to_string(diff);
	debugOutput(s);//diff: 1
}