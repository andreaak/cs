#include "stdafx.h"
#include "Helper.h"
#include <Windows.h>

void debugOutput(std::string s)
{
	s += "\n";
	std::wstring stemp = std::wstring(s.begin(), s.end());
	LPCWSTR  info = (LPCWSTR)stemp.c_str();
	OutputDebugString(info);
}
