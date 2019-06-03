// Сеансы - один из способов хранения состояния. Для работы с сеансами в express установим модуль express-session
// При использовании этого middleware сессии будут автоматически создаваться для кажого пользователя и все сессионные переменные
// автоматически попадут в req.session

var cookieSession = require('cookie-session')
var express = require('express')

var app = express()

app.use(cookieSession({
  // Имя сессии
  name: 'session',
  // серкретные ключи
  keys: ['key1', 'key2']
}));

app.get('/', function (req, res, next) {
  console.log(req.ip)
  // Создаем свойство в сессии

  if(req.session.isNew)
    console.log('Session created!');

  req.session.views = (req.session.views || 0) + 1;

  res.end(req.session.views + ' views')
})

app.listen(8080)