// соединение с бд 
var connection = require('./db');
var mssql = require('mssql');
var path = require('path'); 

module.exports = {
    // згрузка всех элементов
    loadItems: function (req, res) {

        // подключение к бд 

		var request = new mssql.Request(connection);  

		request.query("SELECT * FROM items", function(err, rows) {
			
			if (err) console.log(err); 

            console.timeEnd(req.method + ' ' + req.url); 
			res.send('selected items: ' + JSON.stringify(rows));
		}); 

    },
    // создание элемента 
    createItem: function (req, res) {

        // подключение к бд 
		var request = new mssql.Request(connection);   
		
		request.query("INSERT INTO items (name, description, completed) VALUES ('Test', 'Some text', 1)", function(err, rows) {
			
			if (err) console.log(err); 

            console.timeEnd(req.method + ' ' + req.url);  
			res.send('sample item added to database');
			
		}); 
    },

    // удаление элемента 
    removeItem: function (req, res) {

		var ps = new mssql.PreparedStatement(connection);   
		
		var inserts = {
			id: parseInt(req.params.id)  
		} 
		
		ps.input('id', mssql.Int);  
		
		ps.prepare('DELETE FROM items WHERE id=@id', function(err) {
			if (err) console.log(err); 
			
			ps.execute(inserts, function(err, rows) {
				if (err) console.log(err); 
				
				console.timeEnd(req.method + ' ' + req.url); 
				res.send('item deleted from database');  
				
				ps.unprepare(); 
			}); 
		});
    }
}