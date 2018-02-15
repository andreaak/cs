// TestCPP.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <cppunit/CompilerOutputter.h>
#include <cppunit/extensions/TestFactoryRegistry.h>
#include <cppunit/ui/text/TestRunner.h>
#include <TestResult.h>
#include <TestResultCollector.h>
#include <BriefTestProgressListener.h>

#include "MoneyTest.h"
#include "_02_TypesTest.h"
#include "_05_PointersTest.h"
#include "_06_FunctionPointersTest.h"

int main(int argc, char* argv[])
{
	//// Get the top level suite from the registry
	//CppUnit::Test *suite = CppUnit::TestFactoryRegistry::getRegistry().makeTest();

	//// Adds the test to the list of test to run
	//CppUnit::TextUi::TestRunner runner;
	//runner.addTest(suite);

	//// Change the default outputter to a compiler error format outputter
	//runner.setOutputter(new CppUnit::CompilerOutputter(&runner.result(),
	//	std::cerr));
	//// Run the tests.
	//bool wasSucessful = runner.run();

	CppUnit::TextUi::TestRunner runner;

	//runner.addTest(MoneyTest::suite());
	runner.addTest(_02_TypesTest::suite());
	runner.addTest(_05_PointersTest::suite());
	runner.addTest(_06_FunctionPointersTest::suite());

	bool wasSucessful = runner.run();

	//// Return error code 1 if the one of test failed.
	//return wasSucessful ? 0 : 1;

	//CppUnit::TestResult controller;

	//CppUnit::TestResultCollector result;
	//controller.addListener(&result);

	//CppUnit::BriefTestProgressListener progressListener;
	//controller.addListener(&progressListener);

	//CppUnit::TestRunner runner;
	//runner.addTest(CppUnit::TestFactoryRegistry::getRegistry().makeTest());

	//runner.run(controller);
	if (!wasSucessful)
	{
		system("PAUSE");
	}
}

