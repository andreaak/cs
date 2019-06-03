var mssql = require('mssql'); 
var queries = require('./queries'); 

module.exports = {
	loadEditPage: function(req, res) {
		queries.loadItemById(req, res); 
	}, 
	changeItem: function(req, res) {
		queries.updateItem(req, res); 
	}
}

