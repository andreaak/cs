#ifndef MONEYTEST_H
#define MONEYTEST_H

#include <cppunit/extensions/HelperMacros.h>

class MoneyTest : public CppUnit::TestFixture
{
	/*
	CPPUNIT_TEST_SUITE declares that our Fixture's test suite.
	CPPUNIT_TEST adds a test to our test suite. The test is implemented by a method named testConstructor().
	setUp() and tearDown() are use to setUp/tearDown some fixtures. We are not using any for now.
	*/
	
	CPPUNIT_TEST_SUITE(MoneyTest);
	CPPUNIT_TEST(testConstructor);
	CPPUNIT_TEST(testConstructor2);
	CPPUNIT_TEST_SUITE_END();

public:
	void setUp();
	void tearDown();

	void testConstructor();
	void testConstructor2();
};

#endif  // MONEYTEST_H