var mssql = require('mssql');

var connection = require('./db_handler');

module.exports = {
	curr_user: '',
	// загрузить информацию о зарегистрированных пользователях 
	get_users: function (req, res) {
		var request = new mssql.Request(connection);

		request.query('SELECT * FROM users', function (err, rows) {
			var users = rows.map((row) => {
				return '<h3> ' + row.username + ' : ' + row.password + ' </h3>'
			});

			res.send(users.join(''));
		})

	},
	// проверить имя пользователя 
	check_user: function (req, res, next) {

		var self = this;

		var ps = new mssql.PreparedStatement(connection);

		var inserts = {
			username: req.body.username
		};

		ps.input('username', mssql.Text);

		ps.prepare('SELECT * FROM users WHERE username LIKE @username', function (err) {
			if (err) console.log(err);

			ps.execute(inserts, function (err, rows) {
				if (err) console.log(err);

				ps.unprepare();
				if (rows.length > 0) {
					self.curr_user = rows[0].username;

					next()
				} else {
					res.status(404).send('user not found!');
				}
			})
		});

	},
	// проверить парооль
	check_pass: function (req, res) {
		var self = this;

		var ps = new mssql.PreparedStatement(connection);
		var inserts = {
			password: req.body.password
		};
		console.log(req.body)
		ps.input('password', mssql.Text);

		ps.prepare('SELECT * FROM users WHERE password LIKE @password', function (err) {
			if (err) console.log(err);

			ps.execute(inserts, function (err, rows) {
				if (err) console.log(err);

				ps.unprepare();
				if (rows.length > 0) {
					req.session.username = curr_user;
					res.status(200).send('user ' + req.session.username + ' logged in');
				} else {
					res.status('404').send('wrong password!');
				}

			})
		});

	}
}