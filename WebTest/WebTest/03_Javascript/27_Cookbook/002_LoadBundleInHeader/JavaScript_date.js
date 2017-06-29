(function () {
    var testVar1, testVar2, testVar3;
    for (var i = 0; i < 100; i++) {
        testVar1 = i;
        testVar2 = i + 1;
        testVar3 = i * 3;
    }
})();

(function () {

    alert('test');

})();

(function () {

    function myfunction() {

    }

    function veryLongName() {

    }

    function veryVeryVeryLongName() {

    }

    myfunction();
    veryLongName();
    veryVeryVeryLongName();

})();