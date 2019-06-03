var mssql = require('mssql'); 

var connection = require('./db_handler'); 
var pass_handler = require('./password_handler'); 

module.exports = {
	addUser: function(req, res) { 
	
		var inserts = {
			username: req.body.username, 
			password_hash: pass_handler.encrypt_pass(req.body.password) 
		}; 
	
		var ps = new mssql.PreparedStatement(connection); 
		
		ps.input('username', mssql.Text); 
		ps.input('password_hash', mssql.Text); 
		
		ps.prepare('INSERT INTO users (username, password) VALUES (@username, @password_hash)', function(err) {
			if (err) console.log(err); 
			
			ps.execute(inserts, function(err, rows) {
				if (!err) res.status(200).send('user created successfully!'); 
				else console.log(err); 
				ps.unprepare(); 
			})
		})
	}
}