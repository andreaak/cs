#pragma once

#include <cppunit/extensions/HelperMacros.h>

class _05_PointersTest : public CppUnit::TestFixture
{
	/*
	CPPUNIT_TEST_SUITE declares that our Fixture's test suite.
	CPPUNIT_TEST adds a test to our test suite. The test is implemented by a method named testConstructor().
	setUp() and tearDown() are use to setUp/tearDown some fixtures. We are not using any for now.
	*/

	CPPUNIT_TEST_SUITE(_05_PointersTest);
	CPPUNIT_TEST(testPointer);
	CPPUNIT_TEST(testVoidPointer);
	CPPUNIT_TEST(testChangeConst);
	CPPUNIT_TEST(testIncrementPointer);
	CPPUNIT_TEST(testDiffPointer);
	CPPUNIT_TEST_SUITE_END();

public:

	void testPointer();
	void testVoidPointer();
	void testChangeConst();
	void testIncrementPointer();
	void testDiffPointer();

};

// Registers the fixture into the 'registry'
//CPPUNIT_TEST_SUITE_REGISTRATION(_05_PointersTest);