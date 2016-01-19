// При написании Unit тестов используется принцып AAA (Arrange/Act/Assert)
// Arrange - установка тестовых данных перед прохождением кадого теста (этот шаг называется setUp).
// Act - описание действий, которые должен выполнить метод.
// Assert - проверка результатов работы функции и ожидаемых результатов

// describe - групировка нескольких связанных тестов
describe("First simple test", function () {

    // Arrange
    var value;

    // beforeEach - функция выполняется перед каждым тестом
    beforeEach(function () {
        value = 0;
    });

    // it - запуск функции тестирования (Act)
    it("increments value", function () {
        value++;
        
        // expect - идентифицирует результат работы теста (Assert)
        expect(value).toEqual(1);
    })

    it("decrements value", function () {
        value--;
        expect(value).toEqual(-1);
        //expect(value).toEqual(0);
    })
});


// Обычно этап Assert содержит множество функций которые используются для определения точного условия успешного прохождения теста:

// expect(x).toEqual(val)       проверяет содержится ли в х такое же значение как и в val (но не обязательно один и тот же объект)
// expect(x).toBe(obj)          проверяет что х и obj одинаковые объекты
// expect(x).toMatch(regexp)    проверяет что х подходит под определение регулярного выражения
// expect(x).toBeDefined()      проверяет что х определен
// expect(x).toBeUndefined()    проверяет что х не определен (undefined)
// expect(x).toBeNull()         проверяет что х равно null
// expect(x).toBeTruthy()       проверяет что х true
// expect(x).toBeFalsy()        проверяет что х false
// expect(x).toContain(y)       проверяет что х строка которая содержит у
// expect(x).toBeGreaterThan(y) проверяет что х больше у

// Для тестирования ситуаций ожиданием противоположного результата следует использовать приставку not:
// expect(x).not.toEqual(val)