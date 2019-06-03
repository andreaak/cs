// соединение с бд 
var connection = require('./db.js');
var mssql = require('mssql');
var path = require('path');

module.exports = {
    // загрузка всех элементов
    loadItems: function (req, res) {

        // подключение к бд 

		var request = new mssql.Request(connection);  

		request.query("SELECT * FROM items", function(err, rows) {
			
			if (err) console.log(err); 
			console.log('GET ' + req.url);
			res.send('selected items: ' + JSON.stringify(rows.recordset));
		}); 

    },
    // создание элемента 
    createItem: function (req, res) {

        // подключение к бд 
		var request = new mssql.Request(connection);   
		
		request.query("INSERT INTO items (name, description, completed) VALUES ('Test', 'Some text', 1)", function(err, rows) {
			
			if (err) console.log(err); 
			console.log('POST ' + req.url);
			res.send('sample item added to database');
			
		}); 
    },
    // обновление элемента (редактирование) 
    updateItem: function (req, res) { 
	
		var ps = new mssql.PreparedStatement(connection);   
		
		var inserts = {
			id: parseInt(req.params.id)  
		} 
		
		ps.input('id', mssql.Int); 
		
		ps.prepare('UPDATE items SET name="new Name", description="Some other text", completed=0 WHERE id=@id', function(err) {
			if (err) console.log(err); 
			
			ps.execute(inserts, function(err, rows) {
				if (err) console.log(err); 
				
				console.log('PUT ' + req.url);
				res.send('item by id ' + req.params.id + ' changed');
				ps.unprepare(); 
			}); 
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
				
				console.log('DELETE ' + req.url);
				res.send('item deleted from database');  
				
				ps.unprepare(); 
			}); 
		});
    }
}