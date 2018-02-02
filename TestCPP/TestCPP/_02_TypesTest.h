#pragma once
#include <cppunit/extensions/HelperMacros.h>

class _02_TypesTest : public CppUnit::TestFixture
{
	/*
	CPPUNIT_TEST_SUITE declares that our Fixture's test suite.
	CPPUNIT_TEST adds a test to our test suite. The test is implemented by a method named testConstructor().
	setUp() and tearDown() are use to setUp/tearDown some fixtures. We are not using any for now.
	*/

	CPPUNIT_TEST_SUITE(_02_TypesTest);
	CPPUNIT_TEST(testSize);
	CPPUNIT_TEST_SUITE_END();

public:

	void testSize();
};

// Registers the fixture into the 'registry'
CPPUNIT_TEST_SUITE_REGISTRATION(_02_TypesTest);