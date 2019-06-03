module.exports = function (app) {
	app.get('/logout', function(req, res) {
		req.session.username = '';
		console.log('logged out'); 
		res.send('logged out!');  
	}); 

	// ограничение доступа к контенту на основе авторизации 
	app.get('/admin', function (req, res) {
		// страница доступна только для админа 
		if (req.session.username == 'admin') {
			console.log(req.session.username + ' requested admin page');
			res.render('admin_page');
		} else {
			res.status(403).send('Access Denied!');
		}

	}); 

	app.get('/user', function (req, res) {

		// страница доступна для любого залогиненного пользователя 
		if (req.session.username.length > 0) {
			console.log(req.session.username + ' requested user page');
			res.render('user_page');
		} else {
			res.status(403).send('Access Denied!');
		}

	});

	app.get('/guest', function (req, res) {

		// страница без ограничения доступа 
		res.render('guest_page'); 
	})
}

