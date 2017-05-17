function $$$(selector) {
    var elements;

    if (selector instanceof HTMLElement) {
        elements = [selector];
    } else {
        elements = document.querySelectorAll(selector);
    }

    return new OurJquery(elements);
}

function OurJquery(elements) {

    this.elements = elements;

    /**
     * Подвесить любое событие на группу элементов
     * @param string eventname Тип события
     * @param callable f Функция обработчик
     * @returns self Текущий объект
     */
    this.on = function (eventname, f) {
        for (var i = 0; i < this.elements.length; i++) {
            this.elements[i].addEventListener(eventname, f);
        }

        return this;
    }

    this.addClass = function (name) {
        for (var i = 0; i < this.elements.length; i++) {
            this.elements[i].classList.add(name);
        }

        return this;
    }

    this.removeClass = function (name) {
        for (var i = 0; i < this.elements.length; i++) {
            this.elements[i].classList.remove(name);
        }

        return this;
    }

    this.html = function (html) {
        if (typeof (html) === "undefined") {
            return this.elements[0].innerHTML;
        }

        for (var i = 0; i < this.elements.length; i++) {
            this.elements[i].innerHTML = html;
        }

        return this;
    }

    this.funcWithCallBack = function (time, callback1, callback2) {
        var func1 = callback1 || function(){};
        var func2 = callback2 || function(){};
        
        for (var i = 0; i < this.elements.length; i++) {
            tehFade(this.elements[i], func1, func2);
        }
    }

    function tehFade(elem, func1, func2) {
        var timer = setInterval(function() {
                clearInterval(timer);
                func1();
                func2.call(elem);//Change context to elem
            },
            1000);
    }
}