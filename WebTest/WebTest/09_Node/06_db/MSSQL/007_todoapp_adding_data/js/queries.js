var mssql = require('mssql');
var displayHandler = require('./displayhandler');  

var connection = require('./config'); 

module.exports = {

    tableRows: ``,
    // выбор всех элементов и отображение в виде таблицы 
    getAllItems: function (req, res) {
		
        var self = this; 		
		self.tableRows = ``; 

			var request = new mssql.Request(connection);  
			request.stream = true; 
			request.query("SELECT * FROM items"); 
			
			request.on('row', function(row){ 
	
				self.tableRows += ` <tr>
							<td>${row.name} </td>
							<td>${row.description}</td>
							<td>${row.completed ? 'yes' : 'no'}</td>
						</tr>` 
			}); 
			
			request.on('done', function(affected) { 
				console.log('show_items'); 
				res.render('index', { data:  self.tableRows }); 
			})		

    }, 
	// добавить элемент в бд
	insertItem: function (data, req, res) {


					var inserts = {
						
						name: data.name, 
						description: data.description, 
						completed: parseInt(data.completed)
					}
				
				   var ps = new mssql.PreparedStatement(connection);  
				   
				   ps.input('name', mssql.Text); 
				   ps.input('description', mssql.Text); 
				   ps.input('completed', mssql.Int); 
				   
				   ps.prepare("INSERT INTO items (name, description, completed) VALUES (@name , @description, @completed)", function(err) { 
						if (err) console.log(err); 
					    var request = ps.execute(inserts, function(err) { 
						
							if (err) console.log(err); 
							console.log('add item'); 
							ps.unprepare(); 

						}); 
				
				
				})
	}

}