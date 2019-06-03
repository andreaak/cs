var mssql = require('mssql');

var queries = require('./queries');  

module.exports = {
    displayItems: function(req, res) {  

			var query = queries.getAllItems(req, res)

    }
}