var mssql = require('mssql');
var connection = require('./config'); 

module.exports = {

    tableRows: ``,
    // выбор всех элементов и отображение в виде таблицы 
    getAllItems: function (req, res) { 
		
        var self = this; 
		self.tableRows = ``; 
		
		var request = new mssql.Request(connection);  

		request.query("SELECT * FROM items"); 
		request.stream = true; 
		
		request.on('row', function(row){
			self.tableRows += ` <tr>
						<td>${row.name} </td>
						<td>${row.description}</td>
						<td>${row.completed ? 'yes' : 'no'}</td>
					</tr>` 
		})
		
		request.on('done', function() {
			res.render('index', {data: self.tableRows}); 
		})
    }
}