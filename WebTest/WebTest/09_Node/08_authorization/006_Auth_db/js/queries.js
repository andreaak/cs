var mssql = require('mssql'); 

var connection = require('./db_handler'); 
var pass_handler = require('./password_handler'); 

module.exports = {
	curr_user: '', 
	get_users: function(req, res){
		var request = new mssql.Request(connection); 

		request.query('SELECT * FROM users', function(err, rows) {
			var users = rows.map((row) => {
				return '<h3> ' + row.username + ' </h3>'
			}); 
			
			res.send(users.join('')); 
		})
		
	}, 
	check_user: function(req, res, next) { 
	
		var self = this;
	
		var ps = new mssql.PreparedStatement(connection); 

		var inserts = {
			username: req.body.username
		}; 
		
		ps.input('username', mssql.Text); 
		
		ps.prepare('SELECT * FROM users WHERE username LIKE @username', function(err) {
			if (err) console.log(err); 
			
			ps.execute(inserts, function(err, rows) {
				if (err) console.log(err); 
				
				ps.unprepare();  
				// имя пользователя найдено 
				if (rows.length > 0) {
					self.curr_user = rows[0].username;  
					
					// перейти к проверке пароля 
					next()
				} 
				
				// имя пользователя не найдено 
				else {
				
					res.status(404).send('user not found!'); 
				}
			})
		}); 
		
	}, 

    // проверка пароля 
    check_pass: function (req, res) { 
	
		var self = this; 
		var inserts = {
			password_hash: pass_handler.encrypt_pass(req.body.password) // хэширование пароля 
		}
	
		var ps = new mssql.PreparedStatement(connection); 
		
		ps.input('password_hash', mssql.Text); 
		
		ps.prepare('SELECT * FROM users WHERE password LIKE @password_hash', function(err) {
			if (err) console.log(err); 
			
			ps.execute(inserts, function(err, rows) {
				if (err) console.log(err); 
				
				ps.unprepare(); 
				// пароль верный 
				if (rows) {
					req.session.username = self.curr_user; 
					res.status(200).send('user ' + req.session.username + ' logged in!'); 
					
				} 
				// пароль неверный 
				else {
					res.status(404).send('wrong password!'); 
				}
			})
		}); 


	}
}