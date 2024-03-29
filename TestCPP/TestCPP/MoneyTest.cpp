#include "stdafx.h"
#include "MoneyTest.h"
#include "Money.h"

// Registers the fixture into the 'registry'
CPPUNIT_TEST_SUITE_REGISTRATION(MoneyTest);


void
MoneyTest::setUp()
{
}


void
MoneyTest::tearDown()
{
}


void
MoneyTest::testConstructor()
{
	// Set up
	const std::string currencyFF("FF");
	const double longNumber = 12345678.90123;

	// Process
	Money money(longNumber, currencyFF);

	// Check
	CPPUNIT_ASSERT_EQUAL(longNumber, money.getAmount());
	CPPUNIT_ASSERT_EQUAL(currencyFF, money.getCurrency());
}

void
MoneyTest::testConstructor2()
{
	// Set up
	const std::string currencyFF("FF");
	const double longNumber = 12345678.90123;

	// Process
	Money money(longNumber, currencyFF);

	// Check
	CPPUNIT_ASSERT_EQUAL(longNumber, money.getAmount() + 1);
	CPPUNIT_ASSERT_EQUAL(currencyFF, money.getCurrency());
}